using Saints.Logic;
namespace Saints
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Tests to ensure all code is working correctly
            Test tester = new Test();
            
            //Creates a SearchAlgorithm with all the saints added
            
            
            
        }

        //Adds all the Saints to the program from memory 
        private static SearchAlgorithm AddAllSaints()
        {
            Database database = new Database();
            SearchAlgorithm searchAlgorithm = new SearchAlgorithm(database);
            
            //Pulls the SaintList.txt file containing all the saints info
            
            return searchAlgorithm;
        }
    }
}