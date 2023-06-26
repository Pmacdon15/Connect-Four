using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;

namespace ConnectFour
{
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
        /// <summary>
        /// Random number generator for computer player.
        /// </summary>
        public new Random R = new Random();   
        /// <summary>
        /// IsHuman is used to determine if the player is a human or a computer and will be set to false for computers. I belive this is unnessary as I can check the player type in the currentPlayers list. will probaly remove(which will take time and effect changes not just here.)
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
        /// <summary>
        /// this is a random move for a computer player that is easy mode or a basic computer. 
        /// </summary>
        /// <returns></returns>
        public int RandomMove()
        {
            return R.Next(1, 8);
            
        }        
        /// <summary>
        /// This is a work in progress for a computer player that is hard mode or a more advanced computer.
        /// </summary>
        /// <returns></returns>
        public int WinningMove()
        {
            return 0;
        }
    }// End of Computer Class

    internal class Program
    {
        static void Main(string[] args)
        {
            // Declare variables and Game object which is the controller class.
            bool samePlayers = false;
            bool willPlayAgain = false;
            Game currentGame = new Game();

            Console.WriteLine("Welcome to Connect Four!");
            Console.WriteLine("In This Game after the first round player one will be the player with the most wins.\nThe player to go first is randomly selected."); // This feature is so that I could include an object oriented premise.
            Console.WriteLine("Thank you for playing!\n\n\n\n");
            Console.WriteLine("Please Press Enter to Continue");
            Console.ReadLine();
            
            do
            {      
                //Start new game
                currentGame.ResetBoard();// Only necessary after the first time around, has no effect on the first game
                if (!samePlayers)
                {
                    // Input player 1
                    string player1Name = "";
                   
                    // This loop deals with the case where the user enters no name at all.
                    while (player1Name=="")
                    {
                        Console.WriteLine("Enter Player 1's name(at least one Character): ");
                        player1Name = Console.ReadLine();
                    }
                    player1Name = Player.CapitalizeName(player1Name);// capitalize the name for comparison later on.
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

                    // Then we deal with the Acceptable result.
                    string player2Name = "";
                   
                    if (willPlayer2BeHuman.ToUpper() == "Y")
                    {
                        // This loop deals with the case where the user enters the same name as player 1 or no name at all.
                        while (player2Name == player1Name || player2Name == "")
                        {
                            Console.WriteLine("Enter Player 2's name(at least one character and not the same as Player one): ");
                            player2Name = Console.ReadLine();
                            player2Name = Player.CapitalizeName(player2Name);
                        }
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

                        // Then we deal with the Acceptable result.
                        if (willPlayer2HaveAName.ToUpper() == "Y")
                        {
                            // This loop deals with the case where the user enters the same name as player 1 or no name at all.
                            while (player2Name == player1Name || player2Name == "")
                            {
                                Console.WriteLine("Enter Player 2's name(at least one character and not the same as Player one): ");
                                player2Name = Console.ReadLine();
                                player2Name = Player.CapitalizeName(player2Name);
                            }
                            currentGame.AddComputerPlayer(player2Name);// Assumes a default of 2 for computer players 
                        }
                        else //if (willPlayer2HaveAName.ToUpper() == "N")
                        {
                            currentGame.AddComputerPlayer(2); // Uses default name for computer players
                        }                                                                      
                    }   
                }
                // Display Players before game starts.
                Console.Clear();                
                foreach (Player p in currentGame.CurrentPlayersInGame) Console.WriteLine(p);
                
                // Display Board b4 game starts
                Console.WriteLine("Hit enter to Continue");
                Console.ReadLine();
                Console.Clear();
                currentGame.DisplayBoard();

                // Initialize variables for the game
                int columnNumber;
                int numberOfMoves = 0;
                bool gameOn = true;
                // For a random player start
                bool isFirstTime = true;
                Random ran = new Random();
                int ranPlayerStart = ran.Next(0, 2); //0 for player 1 and 1 for player 2
                
                // This loop is the Game 
                while (gameOn)
                {
                    if (!isFirstTime) ranPlayerStart = 0;  
                    for (int i = ranPlayerStart; i < currentGame.CurrentPlayersInGame.Count; i++)
                    {
                        isFirstTime = false;
                        if (currentGame.CurrentPlayersInGame[i] is HumanPlayer)
                        {
                            Console.Write(currentGame.CurrentPlayersInGame[i].Name + ", please enter a column number for your move: ");
                            while (true)
                            {
                                try
                                {   columnNumber = int.Parse(Console.ReadLine());
                                    if (columnNumber < 0 || columnNumber >= 8)
                                    {
                                        throw new Exception("Column number is out of range. Please enter a valid Column.");
                                    }
                                    Console.WriteLine("The column number you entered is: " + columnNumber);
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.Write("An error occurred: " + ex.Message+ "\nPlease Enter a valid number (1-7): ");
                                }
                            }                        
                        }   
                        else
                        {   // Here A computer is making a move.
                            // Down casting to access the RandomMove method.
                            ComputerPlayer computerPlayer = currentGame.CurrentPlayersInGame[i] as ComputerPlayer;
                            columnNumber = computerPlayer.RandomMove();     
                            Console.WriteLine(currentGame.CurrentPlayersInGame[i].Name + " has chosen column " + columnNumber);                                                       
                            // 2 Second delay here so the user can see the computer's move.
                            System.Threading.Thread.Sleep(2000);
                        }
                        // 0 is player 1 else player 2.
                        if (i == 0)  
                        {
                            // This char and reference are unnecessary and are there to check a box.// might re move at a later date.
                            char player1symbol = 'X';
                            if (!currentGame.MakeAMove(columnNumber, ref player1symbol))
                            {
                                Console.WriteLine("Invalid Move");
                                i--;// This makes it so that when there is an invalid move the palyer goes again.
                                continue;
                            }
                        }
                        // Is player 2.
                        else
                        {
                            // This aswell is unnecessary and is there to check a box. // might remove at a later date.
                            char player2symbol = 'O';
                            if (!currentGame.MakeAMove(columnNumber, ref player2symbol))
                            {
                                Console.WriteLine("Invalid Move");
                                i--;// This makes it so that when there is an invalid move the palyer goes again.
                                continue;
                            }
                        }
                        numberOfMoves++;                                       
                        // Check for winner if possible after 7 moves
                        if (numberOfMoves > 6)
                        {
                            if (currentGame.CheckForWinner(numberOfMoves))
                            {
                                gameOn = false;
                                //Console.Clear();   this is on its way out if nothing breaks
                                currentGame.DisplayBoard();
                                break;
                            }
                        }                         

                        // must be at the end of the loop for display Purposes 
                        Console.Clear();
                        currentGame.DisplayBoard();
                    }
                }// Game is on inside this loop and ends win or draw.                    
                
                // We are able to sort different types of players because we use the icomparable interface.
                currentGame.CurrentPlayersInGame.Sort();

                // This loop reassigns the player's number meaning the winning player is player one.
                for (int i = 0; i < currentGame.CurrentPlayersInGame.Count; i++)
                {
                    int playerNumber = currentGame.CurrentPlayersInGame.IndexOf(currentGame.CurrentPlayersInGame[i]) + 1;
                    currentGame.CurrentPlayersInGame[i].PlayerNumber = playerNumber;
                }
                // Display the players in order of most wins
                foreach (Player p in currentGame.CurrentPlayersInGame) Console.WriteLine(p);

                // Play again input 
                string playAgain = "";
                do
                {
                    Console.WriteLine("Do you want to play again (Y/N)? ");
                    playAgain = Console.ReadLine();
                } while (playAgain.ToUpper() != "Y" && playAgain.ToUpper() != "N");

                if (playAgain.ToUpper() == "Y")
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
                
                // Clear questions after game is over.
                Console.Clear();  
                
            } while (willPlayAgain);// End of do while loop.

        }// End of Main.
    }// End of Program class.
}// End of namespace.