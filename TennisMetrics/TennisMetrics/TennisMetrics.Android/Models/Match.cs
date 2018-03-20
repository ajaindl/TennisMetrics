namespace TennisMetrics.Droid.Models
{
    public class Match
    {
        public Match(Settings settings, int games=6, int sets=3)
        {
            MySettings = settings;
            TotalGames = games;
            TotalSets = sets;
            CurrentSet = 0;
            CurrentGame = 0;
            OpponentsGamesWon = 0;
            OpponentsSetsWon = 0;
       

        }
        public Player Player { get; set; }
        public Settings MySettings { get; set; }
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