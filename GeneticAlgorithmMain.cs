using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAABasketball
{
    class GeneticAlgorithmMain
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int numGames = 5369;
            int numSpecies = 300;
            int nBest = 10;
            int numGenerations = 50;
            int predictableGames;
            List<Species> species = GeneticAlgorithm.generateInitialSpeciesList(numSpecies, rnd);

            // First print out initial results for initial generation
            predictableGames = GeneticAlgorithm.trainSpeciesList(species, numGames);
            Console.WriteLine("Number of evaluatable games used for prediction: " + predictableGames);
            Console.WriteLine("----Generation 0 of " + numGenerations+ "----");
            GeneticAlgorithm.printStats(species);
            //Console.WriteLine("Printing Top 10");
            //GeneticAlgorithm.printNSorted(1, species);
            Console.WriteLine();

            // Do numGenerations iterations to see what happens
            for (int i = 1; i <= numGenerations; i++)
            {
                species = GeneticAlgorithm.speciesUpdate(nBest, species, rnd);
                GeneticAlgorithm.trainSpeciesList(species, numGames);
                Console.WriteLine("----Generation " + i + " of " + numGenerations + "----");
                GeneticAlgorithm.printStats(species);
                //Console.WriteLine("Printing Top 10");
                //GeneticAlgorithm.printNSorted(1, species);
                Console.WriteLine();
            }

            // Get best specie, make prediction with it
            species.Sort();
            species.Reverse();
            Species best = species[0];

            Bracket.predictAndPrint(best, numGames);
            
            Console.WriteLine("Press any key to close.");
            Console.ReadLine();
            
        }
    }
}
