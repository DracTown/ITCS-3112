using System;


namespace Craps_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int ChipStart = 100;

            int roll = 1;
            int bet = 0;
            int letsGo = 2;
            int count = 1;


            //Prints the instuctions
            GameConditions();

            //Start of the program

            //First do loop witchs asks the user each time if they want to play agin
            do
            {
                //Second Do loop runs till a vaild bet is placed
                do
                {
                    Console.WriteLine("What is the bet you wish to place You have: " + ChipStart + " Chips");
                    while (!int.TryParse(Console.ReadLine(), out bet))
                    {
                        Console.WriteLine("This is not valid input. Please enter an integer value: ");
                    }
                    if (bet > ChipStart)
                    {
                        Console.WriteLine("Bet is to large");
                    }
                } while (bet > ChipStart);

                // determins if the player wins/losses on first roll if not sets player point
                int pointSet = Win_Lose_pointSet(DiceGen(rand.Next(1, 7), rand.Next(1, 7)), roll);


                if (pointSet == 0 || pointSet == -1)
                {
                    //if they won or lost updates Chips accordingly
                    ChipStart = profit_losses(pointSet, ChipStart, bet);

                }
                // if player point is set
                else
                {
                    Console.WriteLine("Playes Point: " + pointSet);
                    //Do loop runs till either player rolls there player point or a 7
                    count = 0;
                    do
                    {
                        count++;
                        letsGo = gameOn(pointSet, DiceGen(rand.Next(1, 7), rand.Next(1, 7)));
                        if (letsGo != 0 && letsGo != -1)
                        {
                            Console.WriteLine(count + " Roll Dice Total: " + letsGo);
                        }

                        else if (letsGo == 0)
                        {
                            Console.WriteLine(count + " Roll Dice Total: " + pointSet);
                        }
                        else if (letsGo == -1)
                        {
                            Console.WriteLine(count + " Roll Dice Total: 7");
                        }





                    } while (letsGo != 0 && letsGo != -1);
                    // updates the player chips based on if they got there player point or lost by rolling a 7
                    ChipStart = profit_losses(letsGo, ChipStart, bet);



                }




                //prints out the players remaing chips
                Console.WriteLine(ChipStart);
            } while (stop_Continue(ChipStart) != -1);

        }
        //This method Gentrates the Two Dice Rolls and returns the total
        public static int DiceGen(int dice1, int dice2)
        {
            int DiceTotal = dice1 + dice2;
            return DiceTotal;
        }

        //This method Determins if you win/lose or set playerPoint
        public static int Win_Lose_pointSet(int total, int roll)
        {
            int pointSet = 0;
            if (roll == 1 && total == 7 || total == 11)
            {
                Console.WriteLine("DiceRoll: " + total);
                pointSet = 0;
            }
            else if (roll == 1 && total == 2 || total == 3 || total == 12)
            {
                Console.WriteLine("DiceRoll: " + total);
                pointSet = -1;
            }
            else if (roll == 1 && total == 4 || total == 5 || total == 6 || total == 8 || total == 9 || total == 10)
            {
                pointSet = total;
            }
            return pointSet;
        }
        // This methods goes till you hit your palyer point or roll a 7
        public static int gameOn(int pointSet, int total)
        {
            if (total == pointSet)
            {
                total = 0;
            }
            else if (total == 7)
            {
                total = -1;
            }
            return total;
        }
        // caluclates your win/ loss and updates your chips 
        public static int profit_losses(int pointSet_Game, int ChipStart, int bet)
        {

            if (pointSet_Game == 0)
            {
                Console.Write("You won: ");
                return ChipStart += (2 * (bet));

            }
            else if (pointSet_Game == -1)
            {
                Console.Write("You Lost: ");
                return ChipStart -= (bet);

            }
            else
            {

                return ChipStart;
            }

        }
        // Method for asking the user if they wish to continue or tells them they are out of chips
        public static int stop_Continue(int chips)
        {
            String stop = "NUll";
            do
            {
                if (chips != 0)
                {
                    Console.WriteLine("Do You wish to continue playing 'y' , 'n' " + "Remaing chips: " + chips);
                    stop = Console.ReadLine();
                }
                if (stop.ToLower().Equals("y"))
                {
                    return 0;
                }
                else if (stop.ToLower().Equals("n") || chips <= 0)
                {
                    if (chips <= 0)
                        Console.WriteLine("You are out of money");

                    return -1;
                }
                else
                {
                    stop = "NUll";
                    Console.WriteLine("That is not a Valid option Try again Y,N");
                }

            } while (stop.Equals("NUll"));
            return -1;
        }
        // Prints the instructions to the console
        public static void GameConditions()
        {
            Console.WriteLine("Welcome to the Game of Craps let the games begin");
            Console.WriteLine("Game Conditions for Winning and losing ");
            Console.WriteLine("Winning Rolls net you Double your wager");
            Console.WriteLine();
            Console.WriteLine("You Win on the first Roll if your combined dice total is");
            Console.WriteLine("7 or 11  ");
            Console.WriteLine("You lose on the first roll if your combined dice total is");
            Console.WriteLine("2, 3 or 12");
            Console.WriteLine();
            Console.WriteLine("If neither of these conditions are meet on the first roll the total of");
            Console.WriteLine("The Two Dice is the number you are now tring to match This is known as the playes point");
            Console.WriteLine();
            Console.WriteLine("The player must now roll the dice till they either get there player point or roll a 7");
            Console.WriteLine("If the player rolls there player point they win if they roll a 7 they lose");
            Console.WriteLine();
        }


    }
}
