using System.Collections.Generic;

namespace Saints.Logic
{
    public class SaintCard
    {
        //List of all card types
        //Martyr, Apostle, Evangelist, Doctor, Virgin, Monastic, Hermit, Pope, Clergy, Noble, VirginMartyr
        //Social, Mystic, Missionary, Military, PiousNonDescriptive, Layperson, Child, Woman, Man, Local, Unknown
        
        //list of the different SaintCard Values
        public string Name { get; set; }
        private string Directory { get; set; }
        private List<string> Traits { get; set; } = new List<string>();
        private List<string> Nicknames { get; set; } = new List<string>();
        private List<string> Titles { get; set; } = new List<string>();

        //Card Constructor
        //Note only name, info directory, & traits are required
        public SaintCard(string name, string Directory, string[] traits, string[] nicknames = null, string[] titles = null)
        {
            Name = name;
            Directory = Directory;
            if (traits != null) { Traits.AddRange(traits); }
            if (nicknames != null) { Nicknames.AddRange(nicknames); }
            if (titles != null) { Titles.AddRange(titles); }
        }
        
        //gets the directory for the Saint description & info blurb
        public string GetDirectory() { return Directory; }
        
        //checks if the saint has a specific trait
        public bool HasTrait(string trait) { return Traits.Contains(trait); }
        
        //gets a list of the saints traits
        public string[] GetTraits() { return Traits.ToArray(); }
        
        //gets a list of the saints nicknames
        public string[] GetNicknames() { return Nicknames.ToArray(); }
        
        //gets a list of the saints titles
        public string[] GetTitles() { return Titles.ToArray(); }
        
        
    }
}