using System;
using System.Linq;

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
        /// Player Number, Is public so it can be easily reassigned after the game is over.
        /// </summary>
        public int PlayerNumber { get; set; }
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
                capitalized += char.ToUpper(part[0]) + part.Substring(1);
                if (part != nameParts.Last())
                    capitalized += " ";
            }
            return capitalized;
        }
        /// <summary>
        /// Originally used to get the player's number while it was a protected property. It still functions as intended and instead of changing the code I will leave it in place and use it.
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
    }   // End of Player Class
}// End of namespace.