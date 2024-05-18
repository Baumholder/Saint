using System;
using System.Collections.Generic;
using System.Linq;

namespace Saints.Logic
{
    public class Test
    {
        public Test()
        {
            Console.Write(Environment.NewLine);
            SaintCardTest();
            Console.Write(Environment.NewLine);
            DatabaseTest();
        }
        
        //Test for the SaintCard Class
        private void SaintCardTest()
        {
            string[] traits = new string[] {"Martyr", "Man"};
            string[] virtues = new string[] {"Prudence"};
            SaintCard tester1 = new SaintCard("Tester1", "c/", traits, virtues);
            string[] patron = new string[] {"Testing"};
            string[] titles = new string[] {"Cardinal", "Baron"};
            string[] nicknames = new string[] { "Test Dummy" };
            SaintCard tester2 = new SaintCard("Tester2", "c/", traits, virtues, patron, titles, nicknames);

            if (tester1.Name != "Tester1")
            {
                Console.WriteLine("Expected Name: Tester1, got name: {0}", tester1.Name);
                return;
            }
            else if (tester1.Patron.Count != 0)
            {
                Console.WriteLine("Expected Patron Count: 0, got Patron count: {0}", tester1.Patron.Count);
                return;
            }
            else if (tester2.Patron[0] != "Testing")
            {
                Console.WriteLine("Expected Patron: Testing, got Patron: {0}", tester2.Patron[0]);
                return;
            }
            else if (tester2.Titles[1] != "Baron")
            {
                Console.WriteLine("Expected Second Title: Baron, got Second Title: {0}", tester2.Titles[1]);
                return;
            }
            
            Console.Write("All Saint Card Tests Passed");
        }
        
        //Tests for the Database Class
        private void DatabaseTest()
        {
            string[] traits = new string[] {"Martyr", "Man"};
            string[] virtues = new string[] {"Prudence"};
            SaintCard tester1 = new SaintCard("Tester", "c/", traits, virtues);
            string[] patron = new string[] {"Testing"};
            string[] titles = new string[] {"Cardinal", "Baron"};
            string[] nicknames = new string[] { "Test Dummy" };
            SaintCard tester2 = new SaintCard("Tester", "c/", traits, virtues, patron, titles, nicknames);
            Database database = new Database();
            database.Add(tester1);
            
            if (database.GetSaintCount() != 1)
            {
                Console.WriteLine("Expected item count of 1, got count: {0}", database.GetSaintCount());
                return;
            }
            if (database.GetSaintWIndex(0) != tester1)
            {
                Console.WriteLine("Expected tester1 SaintCard");
                return;
            }
            //tests if names were added correctly
            HashSet<int> testHash = database.TestNames("Tester");
            bool contains = testHash.Contains(0);
            int count = testHash.Count();
            if (!contains || count != 1)
            {
                Console.WriteLine("Incorrect name dictionary. Contains index: {0}, size: {1}", contains, count);
                return;
            }
            
            
            //adds second card
            database.Add(tester2);
            
            //tests nicknames being added to the name
            testHash = database.TestNames("Test Dummy");
            contains = testHash.Contains(1);
            count = testHash.Count();
            if (!contains || count != 1)
            {
                Console.WriteLine("Incorrect nickname dictionary. Contains index: {0}, size: {1}", contains, count);
                return;
            }
            
            //tests for multiple of the same name
            testHash = database.TestNames("Tester");
            contains = testHash.Contains(0) & testHash.Contains(1);
            count = testHash.Count();
            if (!contains || count != 2)
            {
                Console.WriteLine("Incorrect multi-name dictionary. Contains index: {0}, size: {1}", contains, count);
                return;
            }
            
            Console.Write("All Database Tests Passed");
        } 
    }
}