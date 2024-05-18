using System.Collections.Generic;

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
        private Dictionary<string, HashSet<int>> _patron;
        private Dictionary<string, HashSet<int>> _titles;
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

        //Adds to the list of saints
        //Takes in the info for one saint and adds them to the database
        //Does not detect repeats of already added saints
        //!!!!!Should only be used at the launch of the program!!!!!
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
        
        //As name implies
        public int GetSaintCount()
        {
            return _index;
        }
        
        //Short for "Get Saint With Index"
        //As name implies
        public SaintCard GetSaintWIndex(int index)
        {
            return _data[index];
        }
        
        //Any Command below here is used for testing
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