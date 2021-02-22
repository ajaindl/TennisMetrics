using System.Collections.Generic;

namespace TennisMetrics.Droid.Models
{
    public class Match
    {
        public Match(Settings settings, int games=6, int sets=3)
        {
            Settings = settings;
            TotalGames = games;
            TotalSets = sets;
            CurrentSet = 0;
            CurrentGame = 0;
            OpponentsGamesWon = 0;
            OpponentsSetsWon = 0;
            IdList = new List<string>();
       

        }
        public int StorageId { get; set; }
        public List<string> IdList { get; set; }
        public Player Player { get; set; }
        public Settings Settings { get; set; }
        public int TotalGames { get; set; }
        public int TotalSets { get; set; }
        public int CurrentSet { get; set; }
        public int CurrentGame { get; set; }
        public int OpponentsGamesWon { get; set; }
        public int OpponentsSetsWon { get; set; }
        public int OpponentsPointsWon { get; set; }
        public string CurrentGameScore { get; set; }
        public string CurrentSetScore { get; set; }
   



    }
}