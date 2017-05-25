using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAABasketball
{
    class Species : IComparable
    {
        Operation op;
        int numCorrect;

        public Species(Operation operation)
        {
            this.op = operation;
            this.numCorrect = 0;
        }

        public int CompareTo(Object other)
        {
            if (this.numCorrect == ((Species)other).numCorrect)
            {
                // If the same score, take smaller sized one
                return((Species)other).op.size().CompareTo(this.op.size());
            }
            return this.numCorrect.CompareTo(((Species)other).numCorrect);
        }

        public void correctPrediction()
        {
            numCorrect++;
        }

        public void resetFitness()
        {
            numCorrect = 0;
        }

        public Operation getOp()
        {
            return op;
        }

        public int getNumCorrect()
        {
            return numCorrect;
        }

        public int Predict(TeamStats team1, TeamStats team2)
        {
            double team1Score = op.evaluate(team1, team2);
            double team2Score = op.evaluate(team2, team1);
            // If team1 scores higher, return 1
            // If team2 scores higher, return 2
            // Default to team1 if tie
            if (team1Score >= team2Score)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public override string ToString()
        {
            return "CORRECT: " + numCorrect + " OPERATION: " + op.ToString();
        }
    }
}
