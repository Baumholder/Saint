using System.Collections;
using System.Data.Common;
using System.Collections.Generic;

namespace Saints.Logic
{
    public class Database
    {
        //list of possible searchable attributes
        //_data stores the saint class
        private Dictionary<int, SaintCard> _data;
        private Dictionary<string, HashSet<int>> _name;
        private Dictionary<string, HashSet<int>> _traits;
        private Dictionary<string, HashSet<int>> _virtues;
        private Dictionary<string, HashSet<int>> _patron;
        private Dictionary<string, HashSet<int>> _titles;
        private int Index;
        
            
        //Constructor 
        //Adds all saints
        //todo add logic
        public Database()
        {
            _data = new Dictionary<int, SaintCard>();
            _name = new Dictionary<string, HashSet<int>>();
            _traits = new Dictionary<string, HashSet<int>>();
            _virtues = new Dictionary<string, HashSet<int>>();
            _patron = new Dictionary<string, HashSet<int>>();
            _titles = new Dictionary<string, HashSet<int>>();
            Index = 0;
        }

        //Updates the list of saints
        //Takes in the info for one saint and adds them to the database
        //Don't forget to delete saint card before updating if applicable
        //!!!!!Should only be used at the launch of the program!!!!!
        public void Update(SaintCard saint)
        {
            _data.Add(Index, saint);
            
            
            
            Index++;
        }
        
    }
}