using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NCAABasketball
{
    class GeneticAlgorithm
    {
        public static int trainSpeciesList(List<Species> species, int numGames)
        {
            // Dictionary of team name => team stats
            Dictionary<String, TeamStats> teams = new Dictionary<String, TeamStats>();
            int predictableGames = 0;
            int speciesLength = species.Count();

            // for every game
            for (int i = 1; i <= numGames; i++)
            {
                String[] games = GameFileReader.getGameInfoArray(i); // Each array value is a string of stats for 1 game
                String teamName1 = TeamStats.getNameFromGameInfo(games[0]);
                String teamName2 = TeamStats.getNameFromGameInfo(games[1]);

                // if 1 team not in hashtable, add to hashtable and update both teams
                if (!teams.ContainsKey(teamName1) || !teams.ContainsKey(teamName2))
                {
                    if (!teams.ContainsKey(teamName1))
                    {
                        teams.Add(teamName1, new TeamStats());
                    }
                    if (!teams.ContainsKey(teamName2))
                    {
                        teams.Add(teamName2, new TeamStats());
                    }
                    teams[teamName1].updateStats(games[0]);
                    teams[teamName2].updateStats(games[1]);
                    continue; // Immediately go to next game
                }

                // If we get here, both teams exist in the hashtable (which is a Dictionary in c#)
                predictableGames++;
                List<int> predictVector = new List<int>();
                for (int j = 0; j < speciesLength; j++)
                {
                    // Evaluate prediction of 1 or 2 and add to prediction vector
                    predictVector.Add(species[j].Predict(teams[teamName1], teams[teamName2]));
                }
                
                // Evaluate real result (consequently updating both teams)
                teams[teamName1].updateStats(games[0]);
                teams[teamName2].updateStats(games[1]);

                int correctPrediction = 2;  // Defalt to pick 2 just to reduce to 1 if statement
                if (teams[teamName1].lastGameScore() > teams[teamName2].lastGameScore()) // Don't think you can tie but it would be akward to see this not work
                {
                    correctPrediction = 1;
                    teams[teamName1].win();
                    teams[teamName2].lose();
                }
                else
                {
                    teams[teamName2].win();
                    teams[teamName1].lose();
                }

                // For every species
                for (int j = 0; j < speciesLength; j++)
                {
                    if (predictVector[j] == correctPrediction)
                    {
                        // If specie predicted correctly, update fitness
                        species[j].correctPrediction();
                    }
                }
            }
            return predictableGames; // Returns number of predictable games, good to know
            
        }

        public static List<Species> speciesUpdate(int nBest, List<Species> speciesList, Random rnd)
        {
            List<Species> newSpeciesList = new List<Species>();
            int listSize = speciesList.Count();
            // Sort species list of size l
            speciesList.Sort();
            speciesList.Reverse();  // Can switch to descending if too computationally inefficient to reverse list
            // Take n best results
            for (int i = 1; i <= nBest; i++)
            {
                speciesList[i-1].resetFitness(); // Reset fitnesses even though just going to recalculate
                newSpeciesList.Add(speciesList[i-1]);
            }
            // Create l-n mutations of n species
            for (int i = nBest; i < listSize; i++)
            {
                // First choose which of nBest to modify
                int nSpecie = rnd.Next(0, nBest);

                // Add mutation of that specie
                newSpeciesList.Add(new Species(newSpeciesList[nSpecie].getOp().mutate(rnd)));
            }

            return newSpeciesList;
        }

        public static List<Species> generateInitialSpeciesList(int size, Random rnd)
        {
            List<Species> speciesList = new List<Species>();
            // Create initial list with species with constant values
            for (int i = 0; i < size; i++)
            {
                speciesList.Add(new Species(Constant.generateInitialConstant(rnd)));
            }

            return speciesList;
        }

        public static void printStats(List<Species> speciesList)
        {
            // Have to go through all species to get mean
            int length = speciesList.Count();
            int total = 0;
            for (int i = 0; i < length; i++)
            {
                total += speciesList[i].getNumCorrect();
            }
            double mean = System.Convert.ToDouble(total) / System.Convert.ToDouble(length);
            Species max = speciesList.Max();
            Species min = speciesList.Min();
            Console.WriteLine("MAX: " + max);
            Console.WriteLine("MIN: " + min);
            Console.WriteLine("MEAN: " + mean);
        }

        public static void printNSorted(int n, List<Species> speciesList)
        {
            speciesList.Sort();
            speciesList.Reverse();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(speciesList[i]);
            }
        }
    }
}
