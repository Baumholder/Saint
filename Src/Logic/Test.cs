using System;
namespace Saints.Logic
{
    public class Test
    {
        public Test()
        {
            SaintCardTest();
        }
        
        //Test for the SaintCard Class
        public void SaintCardTest()
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
            }
            else
            {
                Console.Write("All Saint Card Tests Passed");
            }
        }
    }
    
    //Tests for the Database Class
}