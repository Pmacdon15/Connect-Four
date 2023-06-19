using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace ConnectFour
{
    /// <summary>
    /// Player is an abstract class that is used to create a HumanPlayer and a ComputerPlayer.
    /// </summary>
    public abstract class Player : IComparable<Player>
    {
        /// <summary>
        /// Player Name is protected so that it can only be accessed by the player class and its subclasses.
        /// </summary>
        public  string Name { get; private set; }
        /// <summary>
        /// Player Number, 1 or 2, is protected so that it can only be accessed by the player class and its subclasses.
        /// </summary>
        protected int PlayerNumber { get; set; }
        /// <summary>
        /// Is a statistic That belongs to the player.
        /// </summary>
        protected int GamesWon { get; set; }
        /// <summary>
        /// Is a statistic That belongs to the player.
        /// </summary>
        protected int GamesLost { get; set; }
        /// <summary>
        /// Is a statistic That belongs to the player.
        /// </summary>
        protected int GamesDrawn { get; set; }
        /// <summary>
        /// Is a statistic That belongs to the player.
        /// </summary>
        protected int TotalGames { get; set; }
        /// <summary>
        /// This will be used when a computer player with no name is entered.
        /// </summary>
        /// <param name="playerNumber"></param>
        public Player(int playerNumber) 
        {
            Name = "Ken Knif";
            GamesWon = 10;
            GamesLost = 5;
            GamesDrawn = 1;
            TotalGames = 16;
            PlayerNumber = playerNumber;
        }
        /// <summary>
        /// This is a basic player constructor with zero values as default, This will be used most times when a player is created.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerNumber"></param>
        public Player(string name, int playerNumber)
        {
            Name = CapitalizeName(name);
            GamesWon = 0;
            GamesLost = 0;
            GamesDrawn = 0;
            TotalGames = 0;
            PlayerNumber = playerNumber;
        }  
        /// <summary>
       /// This constructor is used for entering custom player data, it is not used within the main program. It was created in case a need  arises for entering custom information.
       /// </summary>
       /// <param name="name"></param>
       /// <param name="playerNumber"></param>
       /// <param name="gamesWon"></param>
       /// <param name="gamesLost"></param>
       /// <param name="gamesDrawn"></param>
       /// <param name="totalGames"></param>
        public Player(string name, int playerNumber, int gamesWon=0, int gamesLost = 0, int gamesDrawn = 0)
        {
            Name = name;
            PlayerNumber = playerNumber;
            GamesWon = gamesWon;
            GamesLost = gamesLost;
            GamesDrawn = gamesDrawn;
            TotalGames = gamesWon+gamesLost+gamesDrawn;
        }
        /// <summary>
        /// This method capitalizes players names so that they are all uniform.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CapitalizeName(string name)
        {
            string[] nameParts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string capitalized = "";

            foreach (string part in nameParts)
            {
                capitalized += char.ToUpper(part[0]) + part.Substring(1) + " ";
            }
            return capitalized;        
        }
        /// <summary>
    /// Used to get the player's name as it a a protected.
    /// </summary>
    /// <returns></returns>
        public int GetNumber()
        {
            return PlayerNumber ;
        }       
        /// <summary>
        /// This method is abstract and must be overridden by computer and player classes, it will be used Four adding a win to the player statistics and incrementing total games. 
        /// </summary>
        public abstract void AddWin();
        /// <summary>
        /// This method is used to add a loss to the player's statistics and increment the total games played.
        /// </summary>
        public void AddLoss()
        {
            GamesLost++;
            TotalGames++;
        }
        /// <summary>
        /// This method is virtual and will be overridden by the human and computer player classes. It adds a draw to the player's statistics and increments the total games played.
        /// </summary>
        public virtual void AddDraw()
        {
            GamesDrawn++;
            TotalGames++;
        }
        /// <summary>
        /// This overrides the two string method for player subclasses human and computer. It will display a list of their statistics with a line above and below.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Name: {Name} \nGames Won: {GamesWon} \nGames Lost: {GamesLost} \nGames Drawn: {GamesDrawn} \nTotal Games: {TotalGames}\n------------------------";
        }
        /// <summary>
        /// Uses I compare in order to sort different types of players.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Player other)
        {
            if (GamesWon < other.GamesWon) return 1;           
            else if (GamesWon > other.GamesWon) return -1;            
            else return 0;
           
        }
    }// End of Player Class
    /// <summary>
    /// Human player class is a child of the player class
    /// </summary>
    public class HumanPlayer : Player
    {
        /// <summary>
        /// this is used to determine if the player is human, It will be set to true for human players.
        /// </summary>
        public bool IsHuman { get; set; }
        /// <summary>
        /// use one creating a human player for player one or player 2.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerNumber"></param>
        public HumanPlayer(string name, int playerNumber) : base(name, playerNumber)
        {
            IsHuman = true;
        }
        /// <summary>
        /// Use to add a win to the player's statistics. This is overridden from player class.
        /// </summary>
        public override void AddWin()
        {
            GamesWon++;
            TotalGames++;
        }
        /// <summary>
        /// Used to add a draw to the players statistics. This is overridden from player class.
        /// </summary>
        public override void AddDraw()
        {
            GamesDrawn++;
            TotalGames++;
        }

    }// End of Human Class
    /// <summary>
    /// The computer player class its a child of the player class
    /// </summary>
    public class ComputerPlayer : Player
    {
        //Todo add a random name generator for the computer player
        //Todo Add a random next move
        //Todo add a random next move that is a winning move
        /// <summary>
        /// IsHuman is used to determine if the player is a human or a computer and will be set to false for computers.
        /// </summary>
        public bool IsHuman { get; set; }
        /// <summary>
        /// Used when Entering a player name is not nessary.
        /// </summary>
        /// <param name="playerNumber"></param>
        public ComputerPlayer(int playerNumber) : base(playerNumber)
        {

        }
         /// <summary>
        /// For adding a computer player with a specific name and number. It also uses the base class player as a default constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerNumber"></param>
        public ComputerPlayer(string name, int playerNumber) : base(name, playerNumber)
        {
            IsHuman = false;
        }
        /// <summary>
        /// AddWin is Overridden from the Player class to allow for the computer player to add a win to its stats.
        /// </summary>
        public override void AddWin()
        {
            GamesWon++;
            TotalGames++;
        }
        
    }// End of Computer Class
    /// <summary>
    /// This game class and controls most parts of the Game.
    /// </summary>
    public class Game
    {
        //public bool Status { get; set; }  // This is on its way out.!!!!!!
        /// <summary>
        /// The GameBoard is a 2D array of char type that is used to display the game board and keep track of the game state.
        /// </summary>
        private char[,] GameBoard { get; set; } = new char[6, 7];
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
        // Add PLayers
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
        /// MakeAMove Allows the player to make a move on the game board.
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <param name="playerSymbol"></param>
        /// <returns></returns>
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

    internal class Program
    {
        static void Main(string[] args)
        {
            // Declare variables and Game object which is the controller class.
            bool samePlayers = false;
            bool willPlayAgain = false;
            Game currentGame = new Game();

            Console.WriteLine("Welcome to Connect Four!");
            Console.WriteLine("Please Press Enter to Continue");
            Console.ReadLine();
            
            do
            {      
                //Start new game
                currentGame.ResetBoard();// Only necessary after the first time around, has no effect on the first game
                if (!samePlayers)
                {
                    // Input player 1
                    Console.WriteLine("Enter Player 1's name: ");
                    string player1Name = Console.ReadLine();
                    // Add Player 1
                    currentGame.AddHumanPlayer(player1Name, 1);
                    string willPlayer2BeHuman = "";

                    // Input player 2
                    //This loop Excption handles if the user does not enter Y or N
                    do
                    {
                        Console.WriteLine("Will Player 2 be Human (Y/N)? ");
                        willPlayer2BeHuman = Console.ReadLine();
                        
                        if(willPlayer2BeHuman.ToUpper() != "Y" && willPlayer2BeHuman.ToUpper() != "N")
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter Y or N");
                        }

                    } while (willPlayer2BeHuman.ToUpper() != "Y" && willPlayer2BeHuman.ToUpper() != "N");

                    string player2Name = "";
                    if (willPlayer2BeHuman.ToUpper() == "Y")
                    {
                        Console.WriteLine("Enter Player 2's name: ");
                        player2Name = Console.ReadLine();
                        Console.Clear();
                        currentGame.AddHumanPlayer(player2Name, 2);// Add human player2
                    }
                    else if (willPlayer2BeHuman.ToUpper() == "N")      // Add Computer player default or with name 
                    {
                        string willPlayer2HaveAName = "";
                        //This loop Excption handles if the user does not enter Y or N
                        do
                        {
                            Console.WriteLine("Do you want to enter a name for Computer player 2 (Y/N)? ");
                            willPlayer2HaveAName = Console.ReadLine();


                            if (willPlayer2HaveAName.ToUpper() != "Y" && willPlayer2HaveAName.ToUpper() != "N")
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter Y or N");
                            }

                        } while (willPlayer2HaveAName.ToUpper() != "Y" && willPlayer2HaveAName.ToUpper() != "N");
                            

                        if (willPlayer2HaveAName.ToUpper() == "Y")
                        {
                            Console.WriteLine("Enter Player 2's name: ");
                            player2Name = Console.ReadLine();
                            currentGame.AddComputerPlayer(player2Name);// Assumes a default of 2 for computer players 
                        }
                        else //if (willPlayer2HaveAName.ToUpper() == "N")
                        {
                            currentGame.AddComputerPlayer(2);
                        }
                       
                       
                        
                    }   
                }

                //Display Players before game starts
                foreach (Player p in currentGame.CurrentPlayersInGame)
                {
                    Console.WriteLine(p);
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
                        Console.WriteLine(currentGame.CurrentPlayersInGame[i].Name + ", please enter a column number for your move : ");
                        columnNumber = int.Parse(Console.ReadLine());
                        if (i == 0)
                        {
                            if (!currentGame.MakeAMove(columnNumber, 'X'))
                            {
                                Console.WriteLine("Invalid Move");
                                i--;
                                continue;
                            }
                        }
                        else
                        {
                            if (!currentGame.MakeAMove(columnNumber, 'O'))
                            {
                                Console.WriteLine("Invalid Move");
                                i--;
                                continue;
                            }
                        }
                        numberOfMoves++;
                                                
                        if (numberOfMoves > 6)
                        {
                            if (currentGame.CheckForWinner(numberOfMoves))
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
                }// Game is on inside this loop and ends win or draw
                    
                
                //Display Players for testing purposes  // this will be changed to use interface and sort the winner with the most wins // maybe make this a static method
                currentGame.CurrentPlayersInGame.Sort();
                foreach (Player p in currentGame.CurrentPlayersInGame)
                {
                    Console.WriteLine(p);// Todo maybe display winner first // removed To string put back if issues
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