using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAABasketball
{
    interface Operation
    {
        double evaluate(TeamStats team1, TeamStats team2);
        int size();
        Operation mutate(Random rnd);
        bool isConstant();
        Operation copy();
    }

    class Constant : Operation
    {
        double constant;
        int label;

        public Constant(double c, int l)
        {
            constant = c;
            label = l;
        }

        public int size()
        {
            return 1;
        }

        public bool isConstant()
        {
            return true;
        }

        public Operation copy()
        {
            return new Constant(this.constant, this.label);
        }

        public double evaluate(TeamStats team1, TeamStats team2)
        {
            if (label >= 0 && label <= 38)
            {
                return team1.returnAvaerageStat(label) * constant;
            }
            else if (label >= 39 && label <= 77)
            {
                return team2.returnAvaerageStat(label % 39) * constant;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Bad label for evaluation");
            }
            
        }

        public static Operation generateInitialConstant(Random rnd)
        {
            return new Constant((rnd.NextDouble() * 2), rnd.Next(0, 78)); // Initialize w/ double from 0.00-2.00 and int from 0-77
        }

        public Operation mutate(Random rnd)
        {
            double chance = rnd.NextDouble();
            if (chance < 0.25)
            {
                // Return new operation with modified constant
                return new Constant((rnd.NextDouble() * 2), this.label);
            } 
            else if (chance < 0.5)
            {
                // New var and constant
                return generateInitialConstant(rnd);
            }
            else if (chance < 0.75)
            {
                // Return completely new operation
                return Operator.generateInitialOpt(rnd);
            }
            else
            {
                // Return an operation with the old constant!
                Constant copy = new Constant(this.constant, this.label); // First copy old constant
                return new Operator(copy, Constant.generateInitialConstant(rnd), rnd.Next(0, 4)); // Return new operator
            }
        }

        public override String ToString()
        {
            return this.constant + "*[" + this.label + "]";
        }
    }

    class Operator : Operation
    {
        Operation left;
        Operation right;
        int operationType; // 0 is +, 1 is -, 2 is *, 3 is /

        public Operator(Operation leftOpt, Operation rightOpt, int optType)
        {
            left = leftOpt;
            right = rightOpt;
            operationType = optType;
        }

        public int size()
        {
            return 1 + left.size() + right.size();
        }

        public bool isConstant()
        {
            return false;
        }

        public Operation getLeft()
        {
            return this.left;
        }

        public Operation getRight()
        {
            return this.right;
        }

        public double evaluate(TeamStats team1, TeamStats team2)
        {
            switch (operationType)
            {
                case 0:
                    return left.evaluate(team1, team2) + right.evaluate(team1, team2);
                case 1:
                    return left.evaluate(team1, team2) - right.evaluate(team1, team2);
                case 2:
                    return left.evaluate(team1, team2) * right.evaluate(team1, team2);
                case 3:
                    return left.evaluate(team1, team2) / right.evaluate(team1, team2);
                default:
                    throw new ArgumentOutOfRangeException("Invalid operation number");
            }
        }

        public static Operation generateInitialOpt(Random rnd)
        {
            return new Operator(Constant.generateInitialConstant(rnd), Constant.generateInitialConstant(rnd), rnd.Next(0, 4));
        }

        public Operation mutate(Random rnd)
        {
            // The number of possible locations to modify is 2 * size - 1 (since size only counts leaves)
            // This function will ensure 1, and 1 only operation is modified
            return mutate_helper(this, rnd.NextDouble(), (2 * this.size()) - 1, 1, rnd);
        }

        private static Operation mutate_helper(Operator op, double randChance, int chance, int position, Random rnd)
        {
            // First evaluate if this is the right node to modify
            if (randChance - (System.Convert.ToDouble(position) / System.Convert.ToDouble(chance)) <= 0)
            {
                double mutateChance = rnd.NextDouble();
                if (mutateChance < 0.25)
                {
                    // Generate new constant pruning possible subtree
                    return Constant.generateInitialConstant(rnd);
                }
                else if (mutateChance < 0.50)
                {
                    // Generate new operation pruning possible subtree
                    return Operator.generateInitialOpt(rnd);
                }
                else if (mutateChance < 0.75)
                {
                    // Just modify the operation, this has a possibility to not modify the tree and get a duplicate if same
                    // operation is chosen
                    return new Operator(op.left.copy(), op.right.copy(), rnd.Next(0, 4));
                }
                else
                {
                    // Create new operation with old operation as a branch
                    return new Operator(op.copy(), Constant.generateInitialConstant(rnd), rnd.Next(0, 4)); // Return new operator
                }
            }
            else
            {
                // Check to see if node to modify is in left tree, if so, recursively mutate, else, skip to right
                if (randChance - (System.Convert.ToDouble(position + (2 * op.left.size() - 1)) / System.Convert.ToDouble(chance)) <= 0)
                {
                    // Not the correct node to modify, so visit children
                    // If left child is leaf, modify if chance fits, otherwise copy, left child is always 1 position away
                    if (op.left.isConstant())
                    {
                        if (randChance - (System.Convert.ToDouble(position + 1) / System.Convert.ToDouble(chance)) <= 0)
                        {
                            // Left needs to be modified because correct node to modify
                            return new Operator(op.left.mutate(rnd), op.right.copy(), op.operationType);
                        }
                        else
                        {
                            // Loof further down tree for node to mutate
                            // Cast to operator because know it isn't a leaf
                            return new Operator(mutate_helper((Operator)op.left,randChance,chance,position++,rnd),op.right.copy(),op.operationType);
                        }
                    }
                }

                // At this point we know it was not in left tree, if it is a constant we know it has to be modified
                if (op.right.isConstant())
                {
                    return new Operator(op.left.copy(), op.right.mutate(rnd), op.operationType);
                }

                // We know we have to recurse right now because not a leaf and not in left tree
                // Right position is position + 2*left(size)
                return new Operator(op.left.copy(), mutate_helper((Operator)op.right, randChance, chance, position + 2 * op.left.size(), rnd), op.operationType);
                
            }
        }

        public Operation copy()
        {
            return new Operator(this.left.copy(), this.right.copy(), this.operationType);
        }

        public override String ToString()
        {
            String opString = "";
            switch (this.operationType)
            {
                case 0:
                    opString = "+"; break;
                case 1:
                    opString = "-"; break;
                case 2:
                    opString = "*"; break;
                case 3:
                    opString = "÷"; break;
            }

            return "(" + this.left.ToString() + ")" + opString + "(" + this.right.ToString() + ")";

        }
    }
}
