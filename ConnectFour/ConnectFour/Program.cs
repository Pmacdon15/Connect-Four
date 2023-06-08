using System;
using System.Collections.Generic;

namespace ConnectFour
{
    internal class Program
    {
        public abstract class Player
        {

        }

        public class HumanPlayer : Player
        {
            private int GamessWon { get; set; }
            private int GamesLost { get; set; }
            private int GamesDrawn { get; set; }
            private int TotalGames { get; set; }
        }

        public class ComputerPlayer : Player
        {
            private int GamessWon { get; set; }
            private int GamesLost { get; set; }
            private int GamesDrawn { get; set; }
            private int TotalGames { get; set; }
            public bool IsComputer { get; set; }

        }

        public class Game
        {
            private char[,] GameBoard { get; set; } = new char[6, 7];
            public bool Status { get; set; }
            //public List<Players> CurrentPlayers { get; set; }
            public Game()
            {
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
        }




        public class Game
        {
            private Player Player1 { get; set; }
            private Player Player2 { get; set; }
            
            public List<Player> CurrrentPlayerList { get; set; }

            public Game(Player player1, Player player2)
            {
                
                //work needed here
                
            }

            public void Start()
            { }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
