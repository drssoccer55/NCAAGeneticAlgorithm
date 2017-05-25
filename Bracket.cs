using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAABasketball
{
    class Bracket
    {
        // Each bracket has nothing at index 0 just cuz I counted that way originally
        

        public static void predictAndPrint(Species specie, int numGames)
        {
            String[] sout = { "", "Kansas", "", "Austin Peay", "", "Colorado", "", "Connecticut", "", "Maryland", "", "South Dakota State", "", "University of California", "", "Hawaii",
                                          "", "Arizona", "", "", "", "Miami FL", "", "Buffalo", "", "Iowa", "", "Temple", "", "Villanova", "", "North Carolina-Asheville" };
            String[] east = { "", "North Carolina", "", "", "", "Southern California", "", "Providence", "", "Indiana", "", "Chattanooga", "", "Kentucky", "", "Stony Brook", 
                                          "", "Notre Dame", "", "", "", "West Virginia", "", "Stephen F. Austin", "", "Wisconsin", "", "Pittsburgh", "", "Xavier", "", "Weber State" };
            String[] west = { "", "Oregon", "", "", "", "Saint Joseph's", "", "Cincinnati", "", "Baylor", "", "Yale", "", "Duke", "", "North Carolina-Wilmington",
                                "", "Texas", "", "Northern Iowa", "", "Texas A&M", "", "Green Bay", "", "Oregon State", "", "Virginia Commonwealth", "", "Oklahoma", "", "Cal State Bakersfield" };
            String[] midw = { "", "Virginia", "", "Hampton", "", "Texas Tech", "", "Butler", "", "Purdue", "", "Arkansas-Little Rock", "", "Iowa State",
                                          "", "Iona", "", "Seton Hall", "", "Gonzaga", "", "Utah", "", "Fresno State", "", "Dayton", "", "Syracuse", "", "Michigan State", "", "Middle Tennessee" };
            String[] four = { "", "", "", "", "", "", "", "" };
            String[] west16 = { "", "Holy Cross", "", "Southern" };
            String[] east16 = { "", "Florida Gulf Coast", "", "Fairleigh Dickinson" };
            String[] east11 = { "", "Michigan", "", "Tulsa" };
            String[] sout11 = { "", "Vanderbilt", "", "Wichita State" };

            Dictionary<String, TeamStats> stats = getTeamStatsList(numGames);
            predictLast4(specie, stats, west16, west, 3);
            predictLast4(specie, stats, east16, east, 3);
            predictLast4(specie, stats, east11, east, 19);
            predictLast4(specie, stats, sout11, sout, 19);
            predict16Bracket(specie, east, stats);
            predict16Bracket(specie, midw, stats);
            predict16Bracket(specie, sout, stats);
            predict16Bracket(specie, west, stats);
            predictFinalFour(specie, stats, east, midw, sout, west, four);
            Console.WriteLine("----BEST FORMULA FOUND----");
            Console.WriteLine(specie);
            Console.WriteLine("-------------------------");
            Console.WriteLine("----East 16 Seed----");
            printBracket(east16);
            Console.WriteLine("----East 11 Seed----");
            printBracket(east11);
            Console.WriteLine("----West 16 Seed----");
            printBracket(west16);
            Console.WriteLine("----South 11 Seed----");
            printBracket(sout11);
            Console.WriteLine("----SOUTH----");
            printBracket(sout);
            Console.WriteLine("----EAST----");
            printBracket(east);
            Console.WriteLine("----WEST----");
            printBracket(west);
            Console.WriteLine("----MIDWEST----");
            printBracket(midw);
            Console.WriteLine("----FINAL FOUR----");
            printBracket(four);

        }

        public static void predict16Bracket(Species specie, String[] bracket,Dictionary<String, TeamStats> stats)
        {
            // Reduce 16 to 8 games
            bracket[2] = (specie.Predict(stats[bracket[1]], stats[bracket[3]]) == 1) ? bracket[1] : bracket[3];
            bracket[6] = (specie.Predict(stats[bracket[5]], stats[bracket[7]]) == 1) ? bracket[5] : bracket[7];
            bracket[10] = (specie.Predict(stats[bracket[9]], stats[bracket[11]]) == 1) ? bracket[9] : bracket[11];
            bracket[14] = (specie.Predict(stats[bracket[13]], stats[bracket[15]]) == 1) ? bracket[13] : bracket[15];
            bracket[18] = (specie.Predict(stats[bracket[17]], stats[bracket[19]]) == 1) ? bracket[17] : bracket[19];
            bracket[22] = (specie.Predict(stats[bracket[21]], stats[bracket[23]]) == 1) ? bracket[21] : bracket[23];
            bracket[26] = (specie.Predict(stats[bracket[25]], stats[bracket[27]]) == 1) ? bracket[25] : bracket[27];
            bracket[30] = (specie.Predict(stats[bracket[29]], stats[bracket[31]]) == 1) ? bracket[29] : bracket[31];
            // Reduce 8 to 4 games
            bracket[4] = (specie.Predict(stats[bracket[2]], stats[bracket[6]]) == 1) ? bracket[2] : bracket[6];
            bracket[12] = (specie.Predict(stats[bracket[10]], stats[bracket[14]]) == 1) ? bracket[10] : bracket[14];
            bracket[20] = (specie.Predict(stats[bracket[18]], stats[bracket[22]]) == 1) ? bracket[18] : bracket[22];
            bracket[28] = (specie.Predict(stats[bracket[26]], stats[bracket[30]]) == 1) ? bracket[26] : bracket[30];
            // Reduce 4 to 2 games
            bracket[8] = (specie.Predict(stats[bracket[4]], stats[bracket[12]]) == 1) ? bracket[4] : bracket[12];
            bracket[24] = (specie.Predict(stats[bracket[20]], stats[bracket[28]]) == 1) ? bracket[20] : bracket[28];
            // Predict regional winner
            bracket[16] = (specie.Predict(stats[bracket[8]], stats[bracket[24]]) == 1) ? bracket[8] : bracket[24];
        }

        public static void predictFinalFour(Species specie, Dictionary<String, TeamStats> stats, String[] east, String[] midw, String[] sout, String[] west, String[] four)
        {
            // First populate qualified teams
            four[1] = east[16];
            four[3] = midw[16];
            four[5] = sout[16];
            four[7] = west[16];
            // Predict first 2 final four matches
            four[2] = (specie.Predict(stats[four[1]], stats[four[3]]) == 1) ? four[1] : four[3];
            four[6] = (specie.Predict(stats[four[5]], stats[four[7]]) == 1) ? four[5] : four[7];
            // Predict final!
            four[4] = (specie.Predict(stats[four[2]], stats[four[6]]) == 1) ? four[2] : four[6];
        }

        public static void predictLast4(Species specie, Dictionary<String, TeamStats> stats, String[] bracketIn, String[] bracketOut, int bracketPos)
        {
            // Place winner in both own bracket and bracket out with seed assigned
            bracketIn[2] = (specie.Predict(stats[bracketIn[1]], stats[bracketIn[3]]) == 1) ? bracketIn[1] : bracketIn[3];
            bracketOut[bracketPos] = bracketIn[2];
        }

        public static void printBracket(String[] bracket)
        {
            // Each bracket has nothing at index 0
            String indent = "                ";
            int count = bracket.Length;

            for (int i = 1; i < count; i++)
            {
                if (i % 16 == 0)
                {
                    Console.WriteLine(indent + indent + indent + indent + bracket[i]);
                    continue;
                }
                if (i % 8 == 0)
                {
                    Console.WriteLine(indent + indent + indent + bracket[i]);
                    continue;
                }
                if (i % 4 == 0)
                {
                    Console.WriteLine(indent + indent + bracket[i]);
                    continue;
                }
                if (i % 2 == 0)
                {
                    Console.WriteLine(indent + bracket[i]);
                    continue;
                }
                Console.WriteLine(bracket[i]);
            }

        }

        public static Dictionary<String, TeamStats> getTeamStatsList(int numGames)
        {
            Dictionary<String, TeamStats> teams = new Dictionary<String, TeamStats>();
            for (int i = 1; i <= numGames; i++)
            {
                String[] games = GameFileReader.getGameInfoArray(i); // Each array value is a string of stats for 1 game
                String teamName1 = TeamStats.getNameFromGameInfo(games[0]);
                String teamName2 = TeamStats.getNameFromGameInfo(games[1]);


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
            }
            return teams;
        }
    }

  
}
