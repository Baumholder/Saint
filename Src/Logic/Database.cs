using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Saints.Logic
{
    public class Database
    {
        //list of possible searchable attributes
        //_data stores the saint class
        private readonly Dictionary<int, SaintCard> _data;
        private readonly Dictionary<string, HashSet<int>> _name;
        private readonly Dictionary<string, HashSet<int>> _traits;
        private readonly Dictionary<string, HashSet<int>> _virtues;
        private readonly Dictionary<string, HashSet<int>> _patron;
        private readonly Dictionary<string, HashSet<int>> _titles;
        private int _index;
        
            
        //Constructor 
        //Adds all saints
        //todo add logic
        public Database()
        {
            //Initializes the variables
            _data = new Dictionary<int, SaintCard>();
            _name = new Dictionary<string, HashSet<int>>();
            _traits = new Dictionary<string, HashSet<int>>();
            _virtues = new Dictionary<string, HashSet<int>>();
            _patron = new Dictionary<string, HashSet<int>>();
            _titles = new Dictionary<string, HashSet<int>>();
            _index = 0;
            
            //Adds allowed Traits & Virtues
            HashSet<int> adder = new HashSet<int>();
            AddTraits(adder);
            AddVirtues(adder);
            //TODO above

        }
        
        //Adds the preset Traits
        private void AddTraits(HashSet<int> adder)
        {
            string[] traits = new string[] { "Martyr", "Apostle", "Evangelist", "Doctor", "Virgin", "Monastic", "Hermit",
                "Pope", "Clergy", "Noble", "VirginMartyr", "Social", "Mystic", "Missionary", "Military", "Layperson",
                "Child", "Woman", "Man", "Local", "Unknown", "Mythical"};
            foreach (string i in traits)
            {
                _traits.Add(i, new HashSet<int>(adder));
            }
        }
        
        //adds the preset Virtues
        private void AddVirtues(HashSet<int> adder)
        {
            string[] virtues = new string[] { "Prudence", "Justice", "Fortitude", "Temperance", "Faith", "Hope",
                "Charity", "Chastity", "Good Works", "Concord", "Sobriety", "Patience", "Humility", "Diligence",
                "Kindness"};
            foreach (string i in virtues)
            {
                _virtues.Add(i, new HashSet<int>(adder));
            }
        }
        
        /*
         * Below is the logic to add Saints to the database
         * Note the add function can not detect repeat saints
         * Note for thread safety purposes, Saints should only be added when their is no current search
         */
        
        //Adds a saint to the database
        public void Add(SaintCard saint)
        {
            //Initial Variables
            //They called here and then recycled to help memory allocation by non constantly making a new var.
            HashSet<int> adder = new HashSet<int>();
            int count;
            int index = _index;
            
            //Adds Saint Object
            _data.Add(index, saint);
            
            //Adds Saint's Names + Nicknames. Does allow repeated names. All 700 Saint Marys will be included xD.
            NameAdd(adder, saint.Name, index);
            
            //Adds nicknames if applicable
            count = saint.Nicknames.Count;
            if (count > 0) {
                foreach (string nickname in saint.Nicknames) {
                    NameAdd(adder, nickname, index);
                }
            }
            
            //Adds Saint's Traits. Note all possible traits are already defined.
            count = saint.Traits.Count;
            if (count > 0) {
                foreach (string traits in saint.Traits) {
                    _traits[traits].Add(index);
                }
            }
            
            //Adds Saint's Virtues. Note all possible traits are already defined.
            count = saint.Virtues.Count;
            if (count > 0) {
                foreach (string virtues in saint.Virtues) {
                    _virtues[virtues].Add(index);
                }
            }
            
            //Adds Saint's Patronages
            count = saint.Patron.Count;
            if (count > 0) {
                foreach (string patron in saint.Patron) {
                    PatronAdd(adder, patron, index);
                }
            }
            
            //Adds Saint's Titles
            count = saint.Titles.Count;
            if (count > 0) {
                foreach (string titles in saint.Titles) {
                    TitleAdd(adder, titles, index);
                }
            }
            
            _index++;
        }
        
        //code to add Saint's names + nicknames to database
        private void NameAdd(HashSet<int> adder, string name, int index)
        {
            //checks if saint name is already in the list, creating a new hashset if false.
            if (!_name.ContainsKey(name)) {
                adder.Add(index);
                _name.Add(name, new HashSet<int>(adder));
                adder.Clear();
            }
            //code for a Saint name already used. Repeats are allowed
            else {
                _name[name].Add(index);
            }
        }
        
        //code to add Saint's patronage to database
        private void PatronAdd(HashSet<int> adder, string name, int index)
        {
            //checks if saint patronage is already in the list, creating a new hashset if false.
            if (!_patron.ContainsKey(name)) {
                adder.Add(index);
                _patron.Add(name, new HashSet<int>(adder));
                adder.Clear();
            }
            //code for a Saint patronage already used. Repeats are allowed
            else {
                _patron[name].Add(index);
            }
        }
        
        //code to add Saint's titles to database
        private void TitleAdd(HashSet<int> adder, string name, int index)
        {
            //checks if saint name is already in the list, creating a new hashset if false.
            if (!_titles.ContainsKey(name)) {
                adder.Add(index);
                _titles.Add(name, new HashSet<int>(adder));
                adder.Clear();
            }
            //code for a Saint name already used. Repeats are allowed
            else {
                _titles[name].Add(index);
            }
        }
        
        /*
         * Below are Get Functions
         * todo make the get commands have tolerance
         */
        
        //Gets the total number of Saints stored
        public int GetSaintCount()
        {
            return _index;
        }
        
        //Short for "Get Saint With Index"
        //Gets the SaintCard matching a provided Index
        public SaintCard GetSaintWIndex(int index)
        {
            return _data.TryGetValue(index, out var data) ? data : null;
        }

        //Short for "Get Index With Name"
        //Gets a Hashset of Saint Index's matching a provided name 
        public HashSet<int> GetIndexWName(string name)
        {
            return _name.TryGetValue(name, out var data) ? data : null;
        }
        
        //Short for "Get Index With Traits"
        //Gets a Hashset of Saint Index's matching a provided trait 
        public HashSet<int> GetIndexWTrait(string trait)
        {
            return _traits.TryGetValue(trait, out var data) ? data : null;
        }
        
        //Short for "Get Index With Virtues"
        //Gets a Hashset of Saint Index's matching a provided virtue 
        public HashSet<int> GetIndexWVirtue(string virtue)
        {
            return _virtues.TryGetValue(virtue, out var data) ? data : null;
        }
        
        //Short for "Get Index With Patronage"
        //Gets a Hashset of Saint Index's matching a provided patronage 
        public HashSet<int> GetIndexWPatron(string patron)
        {
            return _patron.TryGetValue(patron, out var data) ? data : null;
        }
        
        //Short for "Get Index With Title"
        //Gets a Hashset of Saint Index's matching a provided title 
        public HashSet<int> GetIndexWTitle(string title)
        {
            return _titles.TryGetValue(title, out var data) ? data : null;
        }
        
        /*
         * Below are Testing Functions
         */
        public HashSet<int> TestNames(string testName)
        {
            return _name[testName];
        }

        public List<string> KeyList() //todo temp
        {
            return new List<string>(_name.Keys);
        }
    }
}