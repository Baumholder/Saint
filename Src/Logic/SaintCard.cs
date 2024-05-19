using System.Collections.Generic;

namespace Saints.Logic
{
    public class SaintCard
    {
        //List of all Saint traits
        //Martyr, Apostle, Evangelist, Doctor, Virgin, Monastic, Hermit, Pope, Clergy, Noble, VirginMartyr
        //Social, Mystic, Missionary, Military, Layperson, Child, Woman, Man, Local, Unknown
        
        //List of all possible Virtues. From the Cardinal Virtues, then Theological Virtues, then both Capital virtues
        //Prudence, Justice, Fortitude, Temperance | Faith, Hope, Charity |Chastity, Faith, Good Works, Concord,
        //Sobriety, Patience, Humility | Chastity, Temperance, Charity, Diligence, Kindness, Patience, Humility
        
        
        //list of the different SaintCard Values
        public string Name { get; set; }
        private string Directory { get; set; }
        public List<string> Traits { get; set; } = new List<string>();
        public List<string> Virtues { get; set; } = new List<string>();
        public List<string> Patron { get; set; } = new List<string>();
        public List<string> Titles { get; set; } = new List<string>();
        public List<string> Nicknames { get; set; } = new List<string>();
        public List<string> FeastDay { get; set; } = new List<string>();
        public int Index { get; set; }
        public int Popularity;

        //Card Constructor
        //Note only name, info directory, & traits are required
        public SaintCard(string name, string directory, string[] traits, string[] virtues, string[] patron = null, 
            string[] titles = null, string[] nicknames = null, string[] feastday = null, int popularity = 5)
        {
            Name = name;
            Directory = directory;
            if (traits != null) { Traits.AddRange(traits); }
            if (virtues != null) { Virtues.AddRange(virtues); }
            if (patron != null) { Patron.AddRange(patron); }
            if (titles != null) { Titles.AddRange(titles); }
            if (nicknames != null) { Nicknames.AddRange(nicknames); }
            if (feastday != null) { Nicknames.AddRange(feastday); }
            Popularity = popularity;
        }
        
        //checks if the saint has a specific trait
        public bool HasTrait(string trait) { return Traits.Contains(trait); }
        
        //checks if the saint has a specific virtue
        public bool HasVirtue(string virtue) { return Traits.Contains(virtue); }
    }
}