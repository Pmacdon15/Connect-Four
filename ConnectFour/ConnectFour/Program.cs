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
        public Player(int playerNumber) // a default Character for when no name is entered 
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
        public Player(string name, int playerNumber, int gamesWon, int gamesLost, int gamesDrawn, int totalGames)// for entering custum stats in the future
        {
            Name = name;
            PlayerNumber = playerNumber;
            GamesWon = gamesWon;
            GamesLost = gamesLost;
            GamesDrawn = gamesDrawn;
            TotalGames = totalGames;
        }
        
        public int GetNumber()
        {
            return PlayerNumber ;
        }
        public string GetName()
        {
            return Name;
        }
        public abstract void AddWin();

        public void AddLoss()// ToDo make these abstract becuase no player wil play a game b4 they are decided to be human or computer
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
    /// <summary>
    /// This game class and controls most parts of the game.
    /// </summary>
    public class Game
    {
        private char[,] GameBoard { get; set; } = new char[6, 7];
        //public bool Status { get; set; }
        public  List<Player> CurrentPlayersInGame;
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
        public void resetBoard()
        {
            for (int i = 0; i < 6; i++)              //6 is the number of rows
            {
                for (int j = 0; j < 7; j++)          //7 is the number of columns
                {
                    GameBoard[i, j] = '#'; // 'X' is the assigned value of the char type
                }
            }
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
        }//This method displays the board
        // Add PLayers
        public void AddHumanPlayer(string name, int playerNumber) // This will be default for adding a human player
        {
            CurrentPlayersInGame.Add(new HumanPlayer(name, playerNumber));
        }
        public void AddComputerPlayer(int playerNumber)// For adding The default computer 
        {
            CurrentPlayersInGame.Add(new ComputerPlayer(playerNumber));
        }
        public void AddComputerPlayer(string name, int playerNumber) // This one lets you add the name of the computer player
        {
            CurrentPlayersInGame.Add(new ComputerPlayer(name, playerNumber));
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
                // Diagonal lines top left to bottom right , There are six possible lines that could hold a winner in this direction               
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
                                    Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
                                    Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
                                    Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
                                    Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
                                Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
                                Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
        }// End of CheckWinningDiagonal1
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
                                Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
                                Console.WriteLine($"{player.GetName()} Wins!!!!!!!!!!");
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
        }


    }// End of Game class

    internal class Program
    {
        static void Main(string[] args)
        {
            bool samePlayers = false;
            bool willPlayAgain = false;
            Game currentGame = new Game();

            do
            {                
                //Todo Display player name instead of player 1
                //Start new game                 

                currentGame.resetBoard();


                if (!samePlayers)
                {
                    // Input player 1
                    Console.WriteLine("Enter Player 1's name: ");
                    string player1Name = Console.ReadLine();
                    // Add Player 1
                    currentGame.AddHumanPlayer(player1Name, 1);

                    // Input player 2
                    Console.WriteLine("Will Player 2 be Human (Y/N)? ");
                    string willPlayer2BeHuman = Console.ReadLine();



                    //this is so is value is not assigned it does not crash
                    string player2Name = "";
                    if (willPlayer2BeHuman.ToUpper() == "Y")
                    {
                        Console.WriteLine("Enter Player 2's name: ");
                        player2Name = Console.ReadLine();
                        currentGame.AddHumanPlayer(player2Name, 2);// Add human player2
                    }
                    else// Add Computer player default or with name 
                    {
                        Console.WriteLine("Do you want to enter a name for Computer player 2 (Y/N)? ");
                        string willPlayer2HaveAName = Console.ReadLine();
                        if (willPlayer2HaveAName.ToUpper() == "Y")
                        {
                            Console.WriteLine("Enter Player 2's name: ");
                            player2Name = Console.ReadLine();
                            currentGame.AddComputerPlayer(player2Name, 2);
                        }
                        else
                        {
                            currentGame.AddComputerPlayer(2);
                        }
                    }
                }

                //Display Players for testing purposes
                foreach (Player p in currentGame.CurrentPlayersInGame)
                {
                    Console.WriteLine(p.ToString());
                }

                //Display Board b4 game starts
                Console.WriteLine("Hit enter to Continue");
                Console.ReadLine();
                Console.Clear();
                currentGame.DisplayBoard();

                //bool moveComplete = false;  // on its way out
                int columnNumber;
                int numberOfMoves = 0;
                bool gameOn = true;

                while (gameOn)
                {
                    for (int i = 0; i < currentGame.CurrentPlayersInGame.Count; i++)
                    {
                        Console.WriteLine(currentGame.CurrentPlayersInGame[i].GetName() + ", please enter a column number for your move : ");
                        columnNumber = int.Parse(Console.ReadLine());
                        if (i == 0) currentGame.MakeAMove(columnNumber, 'X');
                        else currentGame.MakeAMove(columnNumber, 'O');

                        numberOfMoves++;
                        if (numberOfMoves > 6)
                        {
                            if (currentGame.CheckForWinner())
                            {
                                gameOn = false;
                                //Console.Clear();
                                currentGame.DisplayBoard();
                                break;
                            }
                        }


                        // must be at the end of the loop for display Purposes 
                        Console.Clear();
                        currentGame.DisplayBoard();
                    }
                }
                    
                
                //Display Players for testing purposes
                foreach (Player p in currentGame.CurrentPlayersInGame)
                {
                    Console.WriteLine(p.ToString());// Todo maybe display winner first 
                }




                // Play again input 
                Console.WriteLine("Do you want to play again (Y/N)? ");
                string playAgian = Console.ReadLine();

                if (playAgian.ToUpper() == "Y")
                {
                    willPlayAgain = true;

                    Console.WriteLine("Do you want to use the same players (Y/N)? ");
                    string useTheSamePlayers = Console.ReadLine();
                    if (useTheSamePlayers.ToUpper() == "Y") samePlayers = true;
                    else
                    {
                        currentGame.CurrentPlayersInGame.Clear();
                        samePlayers = false;
                    }
                }

                else willPlayAgain = false;
                Console.Clear(); // clear questions after game is over






            } while (willPlayAgain);// End of do while loop

        }// End of Main
    }     
    
}