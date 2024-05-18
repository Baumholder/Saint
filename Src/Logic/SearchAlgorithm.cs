using System.Collections.Generic;

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
        
        
        public void Search(string name, string[] traits, string[] virtues, string[] patron, string[] titles)
        {
            //wipes previous search results
            _indexWeighted.Clear();
            
            //looks up the Name
            if (name != null)
                AddResults(_database.GetIndexWName(name), NameWeight);
            
            //Looks up the Traits
            foreach (string temp in traits)
                AddResults(_database.GetIndexWName(temp), TraitsWeight);
            
            //Looks up the Virtues
            foreach (string temp in virtues)
                AddResults(_database.GetIndexWName(temp), VirtuesWeight);
            
            //Looks up the Patronages
            foreach (string temp in patron)
                AddResults(_database.GetIndexWName(temp), PatronWeight);
            
            //Look up the Titles
            foreach (string temp in titles)
                AddResults(_database.GetIndexWName(temp), TitleWeight);
            
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