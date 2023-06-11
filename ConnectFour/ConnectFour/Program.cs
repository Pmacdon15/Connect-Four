using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;

namespace ConnectFour
{

    public abstract class Player //abstract class cannot be instantiated
    {
        protected string Name { get; set; }
        protected int PlayerNumber { get; set; }
        protected int GamesWon { get; set; }
        protected int GamesLost { get; set; }
        protected int GamesDrawn { get; set; }
        protected int TotalGames { get; set; }
        public Player(int playerNumber)
        {
            Name = "Ken Knif";// change this b4 you hand it in .
            GamesWon = 10;
            GamesLost = 5;
            GamesDrawn = 1;
            TotalGames = 16;
            PlayerNumber = playerNumber;
        }
        public Player(string name, int playerNumber)
        {
            Name = name;
            GamesWon = 0;
            GamesLost = 0;
            GamesDrawn = 0;
            TotalGames = 0;
            PlayerNumber = playerNumber;
        }
        public Player(string name, int playerNumber, int gamesWon, int gamesLost, int gamesDrawn, int totalGames)// for entering custum stats
        {
            Name = name;
            PlayerNumber = playerNumber;
            GamesWon = gamesWon;
            GamesLost = gamesLost;
            GamesDrawn = gamesDrawn;
            TotalGames = totalGames;
        }
        //--------------------------------------------
        //Using abstract for Proof of concept ,Abstract in this case is not necessary in this case 
        public abstract void AddWin();

        public void AddLoss()
        {
            GamesLost++;
            TotalGames++;
        }
        public void AddDraw()
        {
            GamesDrawn++;
            TotalGames++;
        }


    }// End of Player Class

    public class HumanPlayer : Player
    {
        public bool IsHuman { get; set; }

        public HumanPlayer(string name, int playerNumber) : base(name, playerNumber)
        {
            IsHuman = true;
        }
        public override void AddWin()
        {
            GamesWon++;
            TotalGames++;
        }
        public override string ToString()
        {
            return $"Name: {Name} \nGames Won: {GamesWon} \nGames Lost: {GamesLost} \nGames Drawn: {GamesDrawn} \nTotal Games: {TotalGames}\n------------------------";
        }

    }// End of Human Class

    public class ComputerPlayer : Player
    {
        //Todo add a random name generator for the computer player
        //Todo Add a random next move
        //Todo add a random next move that is a winning move

        public bool IsHuman { get; set; }
        public ComputerPlayer(int playerNumber) : base(playerNumber)// used when entering playername is not nessary
        {

        }
        public ComputerPlayer(string name, int playerNumber) : base(name, playerNumber)
        {
            IsHuman = false;
        }
        public override void AddWin()
        {
            GamesWon++;
            TotalGames++;
        }

        public override string ToString()
        {
            return $"Name: {Name} \nGames Won: {GamesWon} \nGames Lost: {GamesLost} \nGames Drawn: {GamesDrawn} \nTotal Games: {TotalGames}\n------------------------";
        }

    }// End of Computer Class

    public class Game
    {
        private char[,] GameBoard { get; set; } = new char[6, 7];
        public bool Status { get; set; }
        public List<Player> CurrentPlayersInGame;
        public Game()
        {
            CurrentPlayersInGame = new List<Player>();

            GameBoard = new char[6, 7];
            for (int i = 0; i < 6; i++)              //6 is the number of rows
            {
                for (int j = 0; j < 7; j++)          //7 is the number of columns
                {
                    GameBoard[i, j] = '#'; // 'X' is the assigned value of the char type
                }
            }
            Status = true;

        }
        public void DisplayBoard()
        {
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    Console.Write(GameBoard[i, j] + " ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("----------------");
            Console.WriteLine(" 1 2 3 4 5 6 7");
        }
        // Add PLayers
        public void AddHumanPlayer(string name, int playerNumber)
        {
            CurrentPlayersInGame.Add(new HumanPlayer(name, playerNumber));
        }
        public void AddComputerPlayer(int playerNumber)// For testing pourpouses later will add the paramater "string name" so we can enter the name instead of using the default
        {
            CurrentPlayersInGame.Add(new ComputerPlayer(playerNumber));
        }

        // MakeAMove
        public bool MakeAMove(int columnNumber, char playerSymbol)
        {
            if (columnNumber <= 7 && columnNumber > 0)
            {
                for (int i = GameBoard.GetLength(0) - 1; i >= 0; i--)
                {
                    if (GameBoard[i, columnNumber - 1] == '#')
                    {
                        GameBoard[i, columnNumber - 1] = playerSymbol;
                        return true;
                    }
                }
                Console.WriteLine("Column is full");
                return false;
            }
            Console.WriteLine("Please enter a valid Column Number!!!");
            return false;

        }

        //ToDo check for whiiner 
        public bool CheckForWinner()
        {
            int player1Count = 0;
            int player2Count = 0;
            while (true)
            {
                if (CheckWinningRows(GameBoard) == true) return true; // When a method returns false it goes to the next step if it returns true stop checking for a winner 
                if (CheckWinningColumns(GameBoard)== true) return true;
                if (CheckWinningDiagonal1(GameBoard) == true) return true;
                //if (CheckWinningDiagonal2(GameBoard) == true) return true;

                /*
                for (int i = 0; i < GameBoard.GetLength(0); i++)// This loop is for checking each line
                {
                    player1Count = 0; // resets each row 
                    player2Count = 0;
                    for (int j = 0; j < GameBoard.GetLength(1); j++)
                    {
                        

                        if (GameBoard[i, j] == 'X')
                        {
                            player1Count++;
                            if (player1Count == 4)
                            {
                                Console.WriteLine("Player 1 Wins");
                                return true;
                            }
                        }
                        else if (GameBoard[i, j] == 'O')
                        {
                            player2Count++;
                            if (player2Count == 4)
                            {
                                Console.WriteLine("Player 2 Wins");
                                return true;
                            }
                        }
                        else//if there is a # player counts are reset
                        {
                            player1Count = 0;
                            player2Count = 0;
                        }
                    }

                }// End of for loop  

                for (int i = 0; i < GameBoard.GetLength(1); i++)// This loop is for checking each column
                {
                    player1Count = 0; // resets each row 
                    player2Count = 0;
                    for (int j = 0; j < GameBoard.GetLength(0); j++)
                    {
                        if (GameBoard[j, i] == 'X')
                        {
                            player1Count++;
                            if (player1Count == 4)
                            {
                                Console.WriteLine("Player 1 Wins");
                                return true;
                            }
                        }
                        else if (GameBoard[j, i] == 'O')
                        {
                            player2Count++;
                            if (player2Count == 4)
                            {
                                Console.WriteLine("Player 2 Wins");
                                return true;
                            }
                        }
                        else//if there is a # player counts are reset
                        {
                            player1Count = 0;
                            player2Count = 0;
                        }
                    }

                }// End of for loop
                 //--ToDo check Diagonal One Direction for winners and then diagonal the other direction for winners                 
                 // there are 6 possible diagonal lines that can have a winner
                */
               // Diagonal 1 lt rb
                player1Count = 0;
                player2Count = 0;

                int k = 0;
                for (int i = 2; i <= 5; i++)
                {
                    if (GameBoard[i, k] == 'X')
                    {
                        player1Count++;
                        if (player1Count == 4)
                        {
                            Console.WriteLine("Player 1 Wins");
                            return true;
                        }
                    }
                    else if (GameBoard[i, k] == 'O')
                    {
                        player2Count++;
                        if (player2Count == 4)
                        {
                            Console.WriteLine("Player 2 Wins");
                            return true;
                        }
                    }
                    else//if there is a # player counts are reset
                    {
                        player1Count = 0;
                        player2Count = 0;
                    }
                    k++;
                }//End of for loop              

                return false;// We return false after checking for all winning possiblies
            }

        }  //End of Game class    

        //____
        public static bool CheckWinningRows(char[,] GameBoard)
        {
            int player1Count = 0;
            int player2Count = 0;

            for (int i = 0; i < GameBoard.GetLength(0); i++)// This loop is for checking each line
            {
                player1Count = 0; // resets each row 
                player2Count = 0;
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {

                    if (GameBoard[i, j] == 'X')
                    {
                        player1Count++;
                        if (player1Count == 4)
                        {
                            Console.WriteLine("Player 1 Wins");
                            return true;
                        }
                    }
                    else if (GameBoard[i, j] == 'O')
                    {
                        player2Count++;
                        if (player2Count == 4)
                        {
                            Console.WriteLine("Player 2 Wins");
                            return true;
                        }
                    }
                    else//if there is a # player counts are reset
                    {
                        player1Count = 0;
                        player2Count = 0;
                    }
                }

            }// End of for loop
            return false; // if we get here there is no winner we will move to the next step to check for a winner

        }// End of CheckWinningRows
        public static bool CheckWinningColumns(char[,] GameBoard)
        {
            int player1Count = 0; // resets each row 
            int player2Count = 0;
            for (int i = 0; i < GameBoard.GetLength(1); i++)// This loop is for checking each column
            {
                
                for (int j = 0; j < GameBoard.GetLength(0); j++)
                {
                    if (GameBoard[j, i] == 'X')
                    {
                        player1Count++;
                        if (player1Count == 4)
                        {
                            Console.WriteLine("Player 1 Wins");
                            return true;
                        }
                    }
                    else if (GameBoard[j, i] == 'O')
                    {
                        player2Count++;
                        if (player2Count == 4)
                        {
                            Console.WriteLine("Player 2 Wins");
                            return true;
                        }
                    }
                    else//if there is a # player counts are reset
                    {
                        player1Count = 0;
                        player2Count = 0;
                    }
                }

            }// End of for loop
            return false; // will return false with no winner leading the main method to go to the next step
        }// End of CheckWinningColumns
        public static bool CheckWinningDiagonal1(char[,] GameBoard)
        {
            int player1Count = 0;
            int player2Count = 0;

            int k = 0;
            for (int i = 2; i <= 5; i++)
            {
                if (GameBoard[i, k] == 'X')
                {
                    player1Count++;
                    if (player1Count == 4)
                    {
                        Console.WriteLine("Player 1 Wins");
                        return true;
                    }
                }
                else if (GameBoard[i, k] == 'O')
                {
                    player2Count++;
                    if (player2Count == 4)
                    {
                        Console.WriteLine("Player 2 Wins");
                        return true;
                    }
                }
                else//if there is a # player counts are reset
                {
                    player1Count = 0;
                    player2Count = 0;
                }
                k++;
            }//End of for loop              

            return false;// We return false after checking for all winning possiblies
        }
    


    }// End of Game class

    internal class Program
    {
        static void Main(string[] args)
        {
            // There will be a loop over everything in the main method it will be a do while doYouWantToPlayAgain == "Y" 

            //Todo after everything is done turn the main in to functions 
            //Start new game 
            Game currentGame = new Game();

            // Input player 1
            Console.WriteLine("Enter Player 1's name: ");
            string player1Name = Console.ReadLine();
            // Add Player 1
            currentGame.AddHumanPlayer(player1Name, 1);

            // Input player 2
            Console.WriteLine("Will Player 2 be Human (Y/N)? ");
            string willPlayer2BeHuman = Console.ReadLine();

            string player2Name = "";
            if (willPlayer2BeHuman.ToUpper() == "Y")
            {
                Console.WriteLine("Enter Player 2's name: ");
                player2Name = Console.ReadLine();
                currentGame.AddHumanPlayer(player2Name, 2);// Add human player2
            }

            // Add player 2 if computer
            currentGame.AddComputerPlayer(2);// This is selected to be a computer, no need to pick a name one is provided, Stats are provided.

            //Display Players for testing purposes
            foreach (Player p in currentGame.CurrentPlayersInGame)
            {
                Console.WriteLine(p.ToString());
            }

            //Display Board b4 game starts
            currentGame.DisplayBoard();

            bool moveComplete = false;
            int columnNumber;
            int numberOfMoves = 0;
            // Loop until there is a winner or a draw
            while (true)
            {//------------------------------------------------------------------------

                // Next two loops are for obtaininga valid move from each player
                do// Loop until a valid move is made then display the board
                {//--------------------------------------------------------------------
                 // Prompt player 1 to enter a column number for thier move
                    Console.WriteLine("Player 1 please enter a column number for your move : ");
                    columnNumber = int.Parse(Console.ReadLine());
                    moveComplete = currentGame.MakeAMove(columnNumber, 'X');

                } while (!moveComplete);
                numberOfMoves++;
                moveComplete = false;
                Console.Clear();

                currentGame.DisplayBoard();
                if (numberOfMoves > 6)
                {
                    if (currentGame.CheckForWinner()) break;
                }


                do// Loop until a valid move is made then display the board
                {//---------------------------------------------------------------------
                    Console.WriteLine("Player 2 please enter a column number for your move : ");
                    columnNumber = int.Parse(Console.ReadLine());
                    moveComplete = currentGame.MakeAMove(columnNumber, 'O');

                } while (!moveComplete);
                numberOfMoves++;
                moveComplete = false;
                Console.Clear();
                currentGame.DisplayBoard();

                //Todo check for winner after 7 moves. you will need a counter.

                if (numberOfMoves > 6)
                {
                    if (currentGame.CheckForWinner()) break;
                }

                //ToDo some kind of winning screen and assigning values to propteries in the player class






            }
        }// End of Main
    }     
    
}