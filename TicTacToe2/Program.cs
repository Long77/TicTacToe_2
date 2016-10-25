using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe2
{
    class Program
    {   /*
        GAMEPLAY objectives: 
        Print the board
        XvsO
        Update score with each turn
        Check player and opponent moves, vaildate
        make smart computer opponent
        Check for win, loss, or catscratch(draw)
        Prompt for rematch
        reset board to empty
        */

        static string[] board = { " ", " ", " ",
                                  " ", " ", " ",
                                  " ", " ", " ",}; // board layout arranged in columns and rows
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine("You are X. Let's Play!");
            newBoard();
            play();
        }


        static void play()
        {
            Console.WriteLine("Pick a new Spot. \"1,1\" to \"3,3\"");
            char[] Comma = { ',' }; // split by comma, Cartesian coords
            string[] squares = Console.ReadLine().Split(Comma); //splits x and y
            int x = Int32.Parse(squares[0]);
            int y = Int32.Parse(squares[1]);
            if (!(checkXY(x) && checkXY(y)))
            {
                play();
                
            }
            
            int index = getIndex(x, y);
            if (spotEmpty(index))
            {
                board[index] = "X";
            }
            else
            {
                play();
            }
            if (ifWinner())
            {
                newBoard();
                Console.WriteLine("You Win! Well Done!");
                clearBoard();
            }
            else
            {
                checkCatScratch();
            }

            opponentMove();
            newBoard();
            play();
        }

        static void opponentMove()
        {      // work on making smart AI opponent after it works
                // random but not blocking player moves yet
            int[] openSpots = { -1, -1, -1, -1, -1, -1, -1, -1 };
            int count = 0;
            int winningPosition = 0;
            for (int i = 0; i <= 8; i++)
            {
                if (board[i] == " ")
                {
                    openSpots[count] = i;
                    count = count + 1;
                }
            }
            //check for winning move for computer

            winningPosition = checkWinningMove("O", openSpots);
            if (winningPosition != -1)
            {
                board[winningPosition] = "O";
            }
            else
            {
                winningPosition = checkWinningMove("X", openSpots);
                if (winningPosition != -1)
                {
                    board[winningPosition] = "O";
                }
                else
                {
                    Random rnd = new Random();
                    int randomInteger = rnd.Next(0, count);

                    board[openSpots[randomInteger]] = "O";
                }
            }
            if (ifWinner() == true)
            {
                newBoard();
                Console.WriteLine("You Lose! Nooo...");

                clearBoard();
                play();
            }
        }
        static int checkWinningMove(string playerMove, int[] openPositions)
        {
            int maxAvailable = 0;
            for (int i = 0; i <= openPositions.Length - 1; i++)
            {
                if (openPositions[i] != -1) { maxAvailable = i; };
            }

            for (int i = 0; i <= maxAvailable; i++)
            {
                if (board[openPositions[i]] != " ")
                {
                    board[openPositions[i]] = playerMove;
                    if (ifWinner() == true)
                    {
                        return (openPositions[i]);
                    }
                }
                board[openPositions[i]] = " ";
            }
            return -1;
        }

        static int getIndex(int x, int y)
        {
            return (x - 1) + (y - 1) * 3;
        }
        static void newBoard()
        {   //updates board by move
            Console.WriteLine();
            Console.WriteLine("[ {0} ] [ {1} ] [ {2} ]", board[0], board[1], board[2]);
            Console.WriteLine("[ {0} ] [ {1} ] [ {2} ]", board[3], board[4], board[5]);
            Console.WriteLine("[ {0} ] [ {1} ] [ {2} ]", board[6], board[7], board[8]);
            Console.WriteLine();
        }

        static bool spotEmpty(int i)
        {   // find a home for x
            if (board[i] != " ")
            {
                Console.WriteLine("Sorry, Seat's Taken! Choose a Different Spot.");
                play();
                return false;
            }
            else
            {
                return true;
            }
        }
        static bool checkXY(int a)
        {   //make valid input, safeguard against crashing program w/bad input if possible
            if (a > 0 && a <= 3)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid Entry! Try again. Please use a number 1-3 \"1,1\"; etc.");
                return false;
            }
        }

        static bool ifWinner()
        { // make all possible winning combinations valid
            if (
                (board[0] == board[1] && board[1] == board[2] && board[2] != " ") ||
                (board[3] == board[4] && board[4] == board[5] && board[5] != " ") ||
                (board[6] == board[7] && board[7] == board[8] && board[8] != " ") ||
                (board[0] == board[4] && board[4] == board[8] && board[8] != " ") ||
                (board[2] == board[4] && board[4] == board[6] && board[6] != " ") ||
                (board[0] == board[3] && board[3] == board[6] && board[6] != " ") ||
                (board[1] == board[4] && board[4] == board[7] && board[7] != " ") ||
                (board[2] == board[5] && board[5] == board[8] && board[8] != " ")
                )
            {
             return true;
            }
            else
            {
             return false;
            }
        }
        static bool checkCatScratch()
        {   // if no winning combinations have been achieved and there are no empty spaces
            if ((board[0] != " " && ifWinner() == false) &&
                (board[1] != " " && ifWinner() == false) &&
                (board[2] != " " && ifWinner() == false) &&
                (board[3] != " " && ifWinner() == false) &&
                (board[4] != " " && ifWinner() == false) &&
                (board[5] != " " && ifWinner() == false) &&
                (board[6] != " " && ifWinner() == false) &&
                (board[7] != " " && ifWinner() == false) &&
                (board[8] != " " && ifWinner() == false))
            {
                Console.WriteLine("Catscratch. C'est la vie!");
                clearBoard();
                return true;
            }
            else
            {
                return false;
            }
        }

        static void clearBoard()
        {       //clear the Board for the next game
            Console.WriteLine("Rematch? (Y/N)");
            string answer = Console.ReadLine().ToUpper();
            if (answer == "Y")
            {
                board[0] = " ";
                board[1] = " ";
                board[2] = " ";
                board[3] = " ";
                board[4] = " ";
                board[5] = " ";
                board[6] = " ";
                board[7] = " ";
                board[8] = " ";
                newBoard();
                play();
            }
            else
            {   //#byefelicia
                Console.WriteLine("See You Later!");
                 System.Environment.Exit(0);
            }
              
        }
    }

}

