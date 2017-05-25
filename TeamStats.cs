using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAABasketball
{
    class TeamStats
    {
        String teamName = null;
        int minutesPlayed = 0;                  // Label 0
        int fieldGoalsMade = 0;                 // Label 1
        int fieldGoalsAttempted = 0;            // Label 2
        // static double fieldGoalPercent;             // Label 3
        int twoPointMade = 0;                   // Label 4
        int twoPointAttempted = 0;              // Label 5
        // static double twoPointPercent;              // Label 6
        int threePointMade = 0;                 // Label 7
        int threePointAttempted;                // Label 8
        //static double threePointPercent;             // Label 9
        int freeThrowsMade = 0;                 // Label 10
        int freeThrowsAttempted = 0;            // Label 11
        //static double freeThrowPercent;              // Label 12
        int offensiveRebounds = 0;              // Label 13
        int defensiveRebounds = 0;              // Label 14
        // static int totalRebounds = 0;               // Label 15
        int assists = 0;                        // Label 16
        int steals = 0;                         // Label 17
        int blocks = 0;                         // Label 18
        int turnovers = 0;                      // Label 19
        int personalFouls = 0;                  // Label 20
        int points = 0;                         // Label 21
        // Minutues Played Appears Again
        double trueShootingPercent = 0;         // Label 22
        double effectiveFieldGoalPercent = 0;   // Label 23
        double threePointAttemptRate = 0;       // Label 24
        double freeThrowAttemptRate = 0;        // Label 25
        double offensiveReboundPercent = 0;     // Label 26
        double defensiveReboundPercent = 0;     // Label 27
        double totalReboundPercent = 0;         // Label 28
        double assistPercent = 0;               // Label 29
        double stealPercent = 0;                // Label 30
        double blockPercent = 0;                // Label 31
        double turnoverPercent = 0;             // Label 32
        // Usage percentage is always 100
        double offensiveRating = 0;             // Label 33
        double defensiveRating = 0;             // Label 34
        int gamesPlayed = 0;                    // Label 35
        int winStreak = 0;                      // Label 36
        int loseStreak = 0;                     // Label 37
        int wins = 0;                           // Label 38
        // 39 Options to pick from, 78 from both teams
        // Will return avarage stat for nearly everything since that makes the most sense

        int lastGamePoints = 0; // Variable to hold number of points scored in last game processed

        // Update all stats based on csv string to total values
        public void updateStats(string gameInfo)
        {
            String[] stats = gameInfo.Split(new char[] { ',' }); // First split up stats
            if (teamName == null)
            {
                teamName = stats[0]; // Add team name if doesn't already exists
            }
            minutesPlayed += Int32.Parse(stats[1]);  // Update total minutes played
            fieldGoalsMade += Int32.Parse(stats[2]);
            fieldGoalsAttempted += Int32.Parse(stats[3]);
            // Skip FG% because calculatable
            twoPointMade += Int32.Parse(stats[5]);
            twoPointAttempted += Int32.Parse(stats[6]);
            // Skip 2P%
            threePointMade += Int32.Parse(stats[8]);
            threePointAttempted += Int32.Parse(stats[9]);
            // Skip 3P%
            freeThrowsMade += Int32.Parse(stats[11]);
            freeThrowsAttempted += Int32.Parse(stats[12]);
            // Skip FT%
            offensiveRebounds += Int32.Parse(stats[14]);
            defensiveRebounds += Int32.Parse(stats[15]);
            // Skip Total Rebounds
            assists += Int32.Parse(stats[17]);
            steals += Int32.Parse(stats[18]);
            blocks += Int32.Parse(stats[19]);
            turnovers += Int32.Parse(stats[20]);
            personalFouls += Int32.Parse(stats[21]);
            points += Int32.Parse(stats[22]);
            // Skip duplicate minutes played
            trueShootingPercent += Double.Parse(stats[24]);
            effectiveFieldGoalPercent += Double.Parse(stats[25]);
            threePointAttemptRate += Double.Parse(stats[26]);
            freeThrowAttemptRate += Double.Parse(stats[27]);
            offensiveReboundPercent += Double.Parse(stats[28]);
            defensiveReboundPercent += Double.Parse(stats[29]);
            totalReboundPercent += Double.Parse(stats[30]);
            assistPercent += Double.Parse(stats[31]);
            stealPercent += Double.Parse(stats[32]);
            blockPercent += Double.Parse(stats[33]);
            turnoverPercent += Double.Parse(stats[34]);
            // Skip Usage%
            offensiveRating += Double.Parse(stats[36]);
            defensiveRating += Double.Parse(stats[37]);
            gamesPlayed += 1; // Each time stats processed should be 1 game

            // Now just record last game points
            lastGamePoints = Int32.Parse(stats[22]);
        }

        public void win()
        {
            winStreak += 1;
            wins += 1;
            loseStreak = 0;
        }

        public void lose()
        {
            loseStreak += 1;
            winStreak = 0;
        }

        public int lastGameScore()
        {
            return lastGamePoints;
        }

        public static string getNameFromGameInfo(string gameInfo)
        {
            int commaOccur = gameInfo.IndexOf(',');
            return gameInfo.Substring(0,commaOccur);
        }

        public double returnAvaerageStat(int labelNum)
        {
            switch (labelNum)
            {
                case 0:
                    return System.Convert.ToDouble(minutesPlayed);  // This is an exception in that it returns a total and not an average
                case 1:
                    return System.Convert.ToDouble(fieldGoalsMade) / System.Convert.ToDouble(gamesPlayed); // FG made / Game
                case 2:
                    return System.Convert.ToDouble(fieldGoalsAttempted) / System.Convert.ToDouble(gamesPlayed); // FG Attempted / Game
                case 3:
                    return System.Convert.ToDouble(fieldGoalsMade) / System.Convert.ToDouble(fieldGoalsAttempted) * 100; // Return FG%
                case 4:
                    return System.Convert.ToDouble(twoPointMade) / System.Convert.ToDouble(gamesPlayed); // 2P made / Game
                case 5:
                    return System.Convert.ToDouble(twoPointAttempted) / System.Convert.ToDouble(gamesPlayed); // 2P Attempted / Game
                case 6:
                    return System.Convert.ToDouble(twoPointMade) / System.Convert.ToDouble(twoPointAttempted) * 100; // 2P %
                case 7:
                    return System.Convert.ToDouble(threePointMade) / System.Convert.ToDouble(gamesPlayed); //  3P Made / Game
                case 8:
                    return System.Convert.ToDouble(threePointAttempted) / System.Convert.ToDouble(gamesPlayed); // 3PA / Game
                case 9:
                    return System.Convert.ToDouble(threePointMade) / System.Convert.ToDouble(threePointAttempted) * 100; // 3P%
                case 10:
                    return System.Convert.ToDouble(freeThrowsMade) / System.Convert.ToDouble(gamesPlayed); // FTM / Game
                case 11:
                    return System.Convert.ToDouble(freeThrowsAttempted) / System.Convert.ToDouble(gamesPlayed); // FTA / Game
                case 12:
                    return System.Convert.ToDouble(freeThrowsMade) / System.Convert.ToDouble(freeThrowsAttempted) * 100; // FT%
                case 13:
                    return System.Convert.ToDouble(offensiveRebounds / System.Convert.ToDouble(gamesPlayed)); // Off Rebounds / Game
                case 14:
                    return System.Convert.ToDouble(defensiveRebounds / System.Convert.ToDouble(gamesPlayed)); // Def Rebounds / Game
                case 15:
                    return (System.Convert.ToDouble(offensiveRebounds) + System.Convert.ToDouble(defensiveRebounds)) / System.Convert.ToDouble(gamesPlayed); // Total Rebounds / Game
                case 16:
                    return System.Convert.ToDouble(assists) / System.Convert.ToDouble(gamesPlayed); // Assists / Game
                case 17:
                    return System.Convert.ToDouble(steals) / System.Convert.ToDouble(gamesPlayed); // Steals / Game
                case 18:
                    return System.Convert.ToDouble(blocks) / System.Convert.ToDouble(gamesPlayed); // Blocks / Game
                case 19:
                    return System.Convert.ToDouble(turnovers) / System.Convert.ToDouble(gamesPlayed);  // Turnovers / Game
                case 20:
                    return System.Convert.ToDouble(personalFouls) / System.Convert.ToDouble(gamesPlayed); // Personal Fouls / Game
                case 21:
                    return System.Convert.ToDouble(points) / System.Convert.ToDouble(gamesPlayed); // Points / Game
                case 22:
                    return trueShootingPercent / System.Convert.ToDouble(gamesPlayed) * 100; // Average true shooting %
                case 23:
                    return effectiveFieldGoalPercent / System.Convert.ToDouble(gamesPlayed) * 100;  // Average effective Field Goal %
                case 24:
                    return threePointAttemptRate / System.Convert.ToDouble(gamesPlayed) * 100; // Average three point attempt rate
                case 25:
                    return freeThrowAttemptRate / System.Convert.ToDouble(gamesPlayed) * 100; // Average free throw attempt rate
                case 26:
                    return offensiveReboundPercent / System.Convert.ToDouble(gamesPlayed); // Average off rebound %
                case 27:
                    return defensiveReboundPercent / System.Convert.ToDouble(gamesPlayed); // Average def rebound %
                case 28:
                    return totalReboundPercent / System.Convert.ToDouble(gamesPlayed); // Average total rebound %
                case 29:
                    return assistPercent / System.Convert.ToDouble(gamesPlayed); // Average assist %
                case 30:
                    return stealPercent / System.Convert.ToDouble(gamesPlayed); // Average steal %
                case 31:
                    return blockPercent / System.Convert.ToDouble(gamesPlayed); // Average block %
                case 32:
                    return turnoverPercent / System.Convert.ToDouble(gamesPlayed); // Average turnover %
                case 33:
                    return offensiveRating / System.Convert.ToDouble(gamesPlayed); // Average offensive rating
                case 34:
                    return defensiveRating / System.Convert.ToDouble(gamesPlayed); // Average defensive rating
                case 35:
                    return System.Convert.ToDouble(gamesPlayed); // Exception to average, number of games played
                case 36:
                    return System.Convert.ToDouble(winStreak); // Exception to average, winstreak length
                case 37:
                    return System.Convert.ToDouble(loseStreak); // Exception to average, losestreak length
                case 38:
                    return System.Convert.ToDouble(wins) / System.Convert.ToDouble(gamesPlayed); ; // Return win percentage
                default:
                    throw new System.ArgumentOutOfRangeException("Invalid integer label for stat value");

            }
        }

        public string getTeamName()
        {
            return teamName;
        }
    }

}
