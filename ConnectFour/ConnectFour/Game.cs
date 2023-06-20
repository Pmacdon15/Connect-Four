using System;
using System.Collections.Generic;

namespace ConnectFour
{
    /// <summary>
    /// This game class and controls most parts of the Game.
    /// </summary>
    public class Game
    {        
        /// <summary>
        /// The GameBoard is a 2D array of char type that is used to display the game board and keep track of the game state. it is public so that it can be accessed by the game controller class.
        /// </summary>
        public char[,] GameBoard { get; set; } = new char[6, 7];
        /// <summary>
        /// A game object will keep a list of players in the game. 
        /// </summary>
        public  List<Player> CurrentPlayersInGame;
        /// <summary>
        /// Default Constructor for the Game Class,  Initializes the game board and creates a list of current players to be populated later on.
        /// </summary>
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
            //Status = true; // on its way out want to remove but scared to break it

        }
        /// <summary>
        /// Resets the GameBoard to its default state.
        /// </summary>
        public void ResetBoard()
        {
            for (int i = 0; i < 6; i++)              //6 is the number of rows
            {
                for (int j = 0; j < 7; j++)          //7 is the number of columns
                {
                    GameBoard[i, j] = '#'; // 'X' is the assigned value of the char type
                }
            }
        }
        /// <summary>
        /// This method displays the board.
        /// </summary>
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
        /// <summary>
        /// This will be default for adding a human player
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerNumber"></param>
        public void AddHumanPlayer(string name, int playerNumber) 
        {
            CurrentPlayersInGame.Add(new HumanPlayer(name, playerNumber));
        }
        /// <summary>
        /// For adding The default computer player.
        /// </summary>
        /// <param name="playerNumber"></param>
        public void AddComputerPlayer(int playerNumber)
        {
            CurrentPlayersInGame.Add(new ComputerPlayer(playerNumber));
        }
        /// <summary>
        /// Adds a computer player with a name assuming all computers will be player 2 unless other wise entered.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerNumber"></param>
        public void AddComputerPlayer(string name, int playerNumber = 2) 
        {
            CurrentPlayersInGame.Add(new ComputerPlayer(name, playerNumber = 2)); 
        }
        /// <summary>
        /// MakeAMove Allows the player to make a move on the game board. Value is passed by reference. Might be removed at a later date.
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <param name="playerSymbol"></param>
        /// <returns></returns>
        public bool MakeAMove(int columnNumber, ref char playerSymbol)
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
        /// <summary>
        /// CheckForWinner checks for a winner in the game by using the methods CheckWinningRows, CheckWinningColumns, CheckWinningDiagonals top left to bottom right and top right to bottom left method.
        /// </summary>
        /// <param name="numberOfMoves"></param>
        /// <returns></returns>
        public bool CheckForWinner(int numberOfMoves)
        {   
            if (numberOfMoves==42)
            {
                Console.Clear();
                Console.WriteLine("Game is a draw");
                foreach (Player player in CurrentPlayersInGame)
                {
                    player.AddDraw();
                }
                return true;
            }
            // If not a tie check for a winner, if there is a winner return true otherwise after everything return false
            while (true)
            {             
                if (CheckWinningRows(GameBoard, CurrentPlayersInGame) == true) return true; // When a method returns false it goes to the next step if it returns true stop checking for a winner 
                if (CheckWinningColumns(GameBoard, CurrentPlayersInGame)== true) return true;

                // Diagonal lines top right to bottom left, there are six possibilities that can hold a winner in this direction 
                List<(int iStarter, int k, int iLimit)> winningLines1 = new List<(int, int, int)>()// Winning lines top left to bottom right 
                {
                    (2, 0, 5),
                    (1, 0, 5),
                    (0, 0, 5),
                    (0, 1, 5),
                    (0, 2, 4),
                    (0, 3, 3),
                    (0, 3, 3)
                };
                foreach (var line in winningLines1)
                {
                    if (CheckWinningDiagonalTopLeftToBottomRight(GameBoard, CurrentPlayersInGame, line.iStarter, line.k, line.iLimit) == true) return true;// Checks the each line
                }
                //Diagonal lines top left to bottom right , There are six possible lines that could hold a winner in this direction               
                List<(int iStarter, int k, int iLimit)> winningLines2 = new List<(int, int, int)>() // Winnning lines top right to bottom left
                {                    
                    (0, 3, 3),
                    (0, 4, 4),
                    (0, 5, 5),
                    (0, 6, 5),
                    (1, 6, 5),
                    (2, 6, 5)
                };
                foreach (var line in winningLines2)
                {
                    if (CheckWinningDiagonalTopRightToBottomLeft(GameBoard, CurrentPlayersInGame, line.iStarter, line.k, line.iLimit) == true) return true;// Checks the each line
                }

                return false;// We return false after checking for all winning possiblies
            }
        }   
        /// <summary>
        /// Checks The rows for a winner.
        /// </summary>
        /// <param name="GameBoard"></param>
        /// <param name="CurrentPlayersInGame"></param>
        /// <returns></returns>
        public static bool CheckWinningRows(char[,] GameBoard, List<Player> CurrentPlayersInGame)
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
                        player2Count = 0;
                        if (player1Count == 4)
                        {                         
                            foreach(Player player in CurrentPlayersInGame)
                            {
                                if(player.GetNumber() == 1)
                                {
                                    Console.Clear();                                    
                                    Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                                    player.AddWin();
                                }
                                else
                                {
                                    player.AddLoss();
                                }
                            }                           
                            return true;
                        }
                    }
                    else if (GameBoard[i, j] == 'O')
                    {
                        player2Count++;
                        player1Count = 0;
                        if (player2Count == 4)
                        {
                            
                            foreach (Player player in CurrentPlayersInGame)
                            {
                                if (player.GetNumber() == 2)
                                {
                                    Console.Clear();
                                    player.AddWin();
                                    Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                                }
                                else
                                {
                                    player.AddLoss();
                                }
                            }
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
        /// <summary>
        /// Checks the columns for a winner.
        /// </summary>
        /// <param name="GameBoard"></param>
        /// <param name="CurrentPlayersInGame"></param>
        /// <returns></returns>
        public static bool CheckWinningColumns(char[,] GameBoard, List<Player> CurrentPlayersInGame)
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
                        player2Count = 0;
                        if (player1Count == 4)
                        {                                                       
                            foreach (Player player in CurrentPlayersInGame)
                            {
                                if (player.GetNumber() == 1)
                                {
                                    Console.Clear();
                                    player.AddWin();
                                    Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                                }
                                else
                                {
                                    player.AddLoss();
                                }
                            }                            
                            return true;
                        }
                    }
                    else if (GameBoard[j,i] == 'O')
                    {
                        player2Count++;
                        player1Count = 0;
                        if (player2Count == 4)
                        {
                           foreach (Player player in CurrentPlayersInGame)
                            {
                                if (player.GetNumber() == 2)
                                {
                                    Console.Clear();
                                    player.AddWin();
                                    Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                                }
                                else
                                {
                                    player.AddLoss();
                                }
                            }
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
        }// End of CheckWinningColumns
        /// <summary>
        /// Checks the diagonal lines from top left to bottom right for a winner.
        /// </summary>
        /// <param name="GameBoard"></param>
        /// <param name="CurrentPlayersInGame"></param>
        /// <param name="iStarter"></param>
        /// <param name="k"></param>
        /// <param name="iLimit"></param>
        /// <returns></returns>
        public static bool CheckWinningDiagonalTopLeftToBottomRight(char[,] GameBoard, List<Player> CurrentPlayersInGame, int iStarter,int k, int iLimit)
        {
            int player1Count = 0;
            int player2Count = 0;

            //int k = 0;
            for (int i =iStarter; i <= iLimit; i++)
            {
                if (GameBoard[i, k] == 'X')
                {
                    player1Count++;
                    player2Count = 0;
                    if (player1Count == 4)
                    {                                             
                        foreach (Player player in CurrentPlayersInGame)
                        {
                            if (player.GetNumber() == 1)
                            {
                                Console.Clear();
                                player.AddWin();
                                Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                            }
                            else
                            {
                                player.AddLoss();
                            }
                        }
                        return true;
                    }
                }
                else if (GameBoard[i, k] == 'O')
                {
                    player2Count++;
                    player1Count = 0;
                    if (player2Count == 4)
                    {                                              
                        foreach (Player player in CurrentPlayersInGame)
                        {
                            if (player.GetNumber() == 2)
                            {
                                Console.Clear();
                                player.AddWin();
                                Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                            }
                            else
                            {
                                player.AddLoss();
                            }
                        }
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
        }// End of CheckWinningDiagonal
        /// <summary>
        /// Checks for a winner from top right to bottom left.
        /// </summary>
        /// <param name="GameBoard"></param>
        /// <param name="CurrentPlayersInGame"></param>
        /// <param name="iStarter"></param>
        /// <param name="k"></param>
        /// <param name="iLimit"></param>
        /// <returns></returns>
        public static bool CheckWinningDiagonalTopRightToBottomLeft(char[,] GameBoard,List<Player> CurrentPlayersInGame,int iStarter,int  k, int iLimit)
        {
            int player1Count = 0;
            int player2Count = 0;

            //int k = 0;
            for (int i = iStarter; i <= iLimit; i++)
            {
                if (GameBoard[i, k] == 'X')
                {
                    player1Count++;
                    player2Count = 0;
                    if (player1Count == 4)
                    {
                        foreach (Player player in CurrentPlayersInGame)
                        {
                            if (player.GetNumber() == 1)
                            {
                                Console.Clear();
                                player.AddWin();
                                Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                            }
                            else
                            {
                                player.AddLoss();
                            }
                        }
                        return true;
                    }
                }
                else if (GameBoard[i, k] == 'O')
                {
                    player2Count++;
                    player1Count = 0;
                    if (player2Count == 4)
                    {
                        foreach (Player player in CurrentPlayersInGame)
                        {
                            if (player.GetNumber() == 2)
                            {
                                Console.Clear();
                                player.AddWin();
                                Console.WriteLine($"{player.Name} Wins!!!!!!!!!!");
                            }
                            else
                            {
                                player.AddLoss();
                            }
                        }
                        return true;
                    }
                }
                else//if there is a # player counts are reset
                {
                    player1Count = 0;
                    player2Count = 0;
                }
                k--;
            }//End of for loop            
            return false;// We return false after checking for all winning possiblies
        }// End of CheckWinningDiagonal2
    }// End of Game class
}// End of namespace.