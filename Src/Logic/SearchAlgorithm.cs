using System.Collections.Generic;
using System.Linq;

namespace Saints.Logic
{
    public class SearchAlgorithm
    {
        private readonly Database _database;
        private readonly Dictionary<int, int> _indexWeighted;
        
        //Constructor
        public SearchAlgorithm (Database database)
        {
            _database = database;
            _indexWeighted = new Dictionary<int, int>();
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
        private int NameWeight = 4;
        private int TraitsWeight = 2;
        private int VirtuesWeight = 1;
        private int PatronWeight = 4;
        private int TitleWeight = 1;
        
        //Search Algorithm. Returns an array of Saint Index
        //todo add threads to the dictionary searches
        public int[] Search(string name, string[] traits, string[] virtues, string[] patron, string[] titles)
        {
            //wipes previous search results
            _indexWeighted.Clear();
            
            //looks up the Name
            if (name != null)
                AddResults(_database.GetIndexWName(name), NameWeight);
            
            //Looks up the Traits
            foreach (string temp in traits)
                AddResults(_database.GetIndexWTrait(temp), TraitsWeight);
            
            //Looks up the Virtues
            foreach (string temp in virtues)
                AddResults(_database.GetIndexWVirtue(temp), VirtuesWeight);
            
            //Looks up the Patronages
            foreach (string temp in patron)
                AddResults(_database.GetIndexWPatron(temp), PatronWeight);
            
            //Look up the Titles
            foreach (string temp in titles)
                AddResults(_database.GetIndexWTitle(temp), TitleWeight);
            
            //sorts the search results by weight & returns the sorted array
            var indexUnsorted = _indexWeighted.ToList();
            indexUnsorted.Sort((x, y) => y.Value.CompareTo(x.Value));
            return indexUnsorted.Select(x => x.Key).ToArray();
        }

        //Adds the results of a specific search to a dictionary of the complete search results
        private void AddResults(HashSet<int> indexResults, int weight)
        {
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