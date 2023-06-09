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
        public Player(string name , int playerNumber, int gamesWon, int gamesLost, int gamesDrawn, int totalGames )// for entering custum stats
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

        public HumanPlayer(string name, int playerNumber) :base(name, playerNumber)
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
        public bool IsHuman { get; set; }
        public ComputerPlayer(int playerNumber):base(playerNumber)// used when entering playername is not nessary
        {
            /*
            Name = "Ken Knif";// change this b4 you hand it in // I think this is not needed becuase it is using the base constructor , I will double check b4 i hand it in
            GamesWon = 10;
            GamesLost = 5;
            GamesDrawn = 1;
            TotalGames = 16;
            */
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
        public void AddHumanPlayer(string name,int playerNumber)
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

    }// End of Game Class



    internal class Program
    {      
        static void Main(string[] args)
        {
            //Start new game 
            Game currentGame = new Game();
            
            // Input player 1
            Console.WriteLine("Will Player 1 be Human (Y/N)? ");
            string willPlayer1BeHuman = Console.ReadLine();
            Console.WriteLine("Enter Player 1's name: ");
            string player1Name = Console.ReadLine();
                        
            // Add player 1
            if (willPlayer1BeHuman.ToUpper()== "Y") currentGame.AddHumanPlayer(player1Name, 1);
            else currentGame.AddComputerPlayer(1);
            
            /*
            // Input player 2
            Console.WriteLine("Will Player 2 be Human (Y/N)? ");
            string willPlayer2BeHuman = Console.ReadLine();
            Console.WriteLine("Enter Player 2's name: ");
            string player2Name = Console.ReadLine();

            // Add player 2  
            if (willPlayer2BeHuman.ToUpper()== "Y") currentGame.AddHumanPlayer(player2Name, 2);
            else*/

            currentGame.AddComputerPlayer(2);// This is selected to be a computer player for testing purposes, no need to pick a name one is provided
              
            //Display Players for testing purposes
            foreach (Player p in currentGame.CurrentPlayersInGame)
            {
                Console.WriteLine(p.ToString());
            }
            
            //Display Board b4 game starts
            currentGame.DisplayBoard();
            
            bool moveComplete = false;
            int columnNumber;
            // Loop until there is a winner or a draw
            do
            {//------------------------------------------------------------------------
                
                // Next two loops are for obtaininga valid move from each player
                do// Loop until a valid move is made then display the board
                {//--------------------------------------------------------------------
                    // Prompt player 1 to enter a column number for thier move
                    Console.WriteLine("Player 1 please enter a column number for your move : ");
                    columnNumber = int.Parse(Console.ReadLine());
                    moveComplete = currentGame.MakeAMove(columnNumber, 'X');

                } while (!moveComplete);
                moveComplete = false;
                Console.Clear();
                currentGame.DisplayBoard();

                do// Loop until a valid move is made then display the board
                {//---------------------------------------------------------------------
                    Console.WriteLine("Player 2 please enter a column number for your move : ");
                    columnNumber = int.Parse(Console.ReadLine());
                    moveComplete = currentGame.MakeAMove(columnNumber, 'Y');

                } while (!moveComplete);
                moveComplete= false;               
                currentGame.DisplayBoard();
               


            } while (currentGame.Status == true);




            



        }// End of Main
    }
}
