using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace NCAABasketball
{
    class GameFileReader
    {
        // Returns a string with all csv values for a game in one line (38 + 38 = 76)
        public static String getAllGameInfo(int gameNum)
        {
            String team1, team2;
            using (System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Douglas\Documents\Visual Studio 2012\Projects\NCAABasketball\NCAABasketball\GameData\game" + gameNum.ToString() + ".txt"))
            {
                // There are 38 things to read per line (excluding the fact minutes played is duplicated)
                team1 = file.ReadLine(); 
                team2 = file.ReadLine();
            }
            return team1 + team2;
        }

        // Returns a string with all csv values for a game in one line (38)
        public static String getTeam2GameInfo(int gameNum)
        {
            String team1, team2;
            using (System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Douglas\Documents\Visual Studio 2012\Projects\NCAABasketball\NCAABasketball\GameData\game" + gameNum.ToString() + ".txt"))
            {
                // There are 38 things to read per line (excluding the fact minutes played is duplicated)
                team1 = file.ReadLine();
                team2 = file.ReadLine();
            }
            return team2;
        }

        // Returns a string with all csv values for a game in one line (38)
        public static String getTeam1GameInfo(int gameNum)
        {
            String team1;
            using (System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Douglas\Documents\Visual Studio 2012\Projects\NCAABasketball\NCAABasketball\GameData\game" + gameNum.ToString() + ".txt"))
            {
                // There are 38 things to read per line (excluding the fact minutes played is duplicated)
                team1 = file.ReadLine();
            }
            return team1;
        }

        // Return a string array with each entry in array being a team (this is the best method)
        public static String[] getGameInfoArray(int gameNum)
        {
            String[] teamStats = new String[2];
            using (System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Douglas\Documents\Visual Studio 2012\Projects\NCAABasketball\NCAABasketball\GameData\game" + gameNum.ToString() + ".txt"))
            {
                // There are 38 things to read per line (excluding the fact minutes played is duplicated)
                teamStats[0] = file.ReadLine();
                teamStats[1] = file.ReadLine();
            }
            return teamStats;
        }
    }
}
