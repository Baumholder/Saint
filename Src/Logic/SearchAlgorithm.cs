﻿using System.Collections.Generic;
using System.Linq;

namespace Saints.Logic
{
    public class SearchAlgorithm
    {
        private readonly Database _database;
        private readonly Dictionary<int, int> _indexWeighted;
        private int _nameWeight;
        private int _traitsWeight;
        private int _virtuesWeight;
        private int _patronWeight;
        private int _titleWeight;
        
        //Constructor
        public SearchAlgorithm (Database database)
        {
            _database = database;
            _indexWeighted = new Dictionary<int, int>();
            ChangeWeights();
        }

        //Adds a saint to the Database
        public void AddSaint (SaintCard saint)
        {
            _database.Add(saint);
        }

        public void RemoveSaint(int index)
        {
            //todo
            //_database.Remove(index);
        }

        /*
         * Search Algorithm Below
         * Note Search Weight
         */
        
        //command to change the search algorithm
        //Type the command with no parameters to reset to default
        public void ChangeWeights(int name = 4, int traits = 2, int virtues = 1, int patron = 4, int title = 1)
        {
            _nameWeight = name;
            _traitsWeight = traits;
            _virtuesWeight = virtues;
            _patronWeight = patron;
            _titleWeight = title;
        }
        
        
        //Search Algorithm. Returns an array of Saint Index
        //todo add threads to the dictionary searches
        public int[] Search(string name, string[] traits, string[] virtues, string[] patron, string[] titles)
        {
            //wipes previous search results
            _indexWeighted.Clear();
            
            //looks up the Name
            if (name != null)
                AddResults(_database.GetIndexWName(name), _nameWeight);
            
            //Looks up the Traits
            if (traits != null)
                foreach (string temp in traits)
                    AddResults(_database.GetIndexWTrait(temp), _traitsWeight);
            
            //Looks up the Virtues
            if (virtues != null)
                foreach (string temp in virtues)
                    AddResults(_database.GetIndexWVirtue(temp), _virtuesWeight);
            
            //Looks up the Patronages
            if (patron != null)
                foreach (string temp in patron)
                    AddResults(_database.GetIndexWPatron(temp), _patronWeight);
            
            //Look up the Titles
            if (titles != null)
                foreach (string temp in titles)
                    AddResults(_database.GetIndexWTitle(temp), _titleWeight);
            
            //sorts the search results by weight & returns the sorted array
            var indexUnsorted = _indexWeighted.ToList();
            indexUnsorted.Sort((x, y) => y.Value.CompareTo(x.Value));
            return indexUnsorted.Select(x => x.Key).ToArray();
        }

        //Adds the results of the search to a tally
        private void AddResults(HashSet<int> indexResults, int weight)
        {
            //If the search results were empty then no action is taken
            if (indexResults == null)
            {
                return;
            }
            //Adds the results of the search
            foreach (int index in indexResults)
            {
                if (_indexWeighted.ContainsKey(index))
                {
                    _indexWeighted[index] += weight;
                }
                else
                {
                    _indexWeighted[index] = weight;
                }
            }
        }
    }
}