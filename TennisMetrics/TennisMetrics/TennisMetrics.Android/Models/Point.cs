namespace TennisMetrics.Droid.Models
{
    public class Point
    {
        public Point()
        {
            FhWinners = 0;
            BhWinners = 0;
            Aces = 0;
            DoubleFaults = 0;
            FSMade = 0;
            SSMade = 0;
            FhVWinners = 0;
            BhVWinners = 0;
            FhUFErrors = 0;
            BhUFErrors = 0;
            FHFError = 0;
            BHFError = 0;
            FHFWinner = 0;
            BHFWinner = 0;
            FHVFWinner = 0;
            BHVFWinner = 0;
        

        }
        public int FhWinners { get; set; }
        public int BhWinners { get; set; }
        public int Aces { get; set; }
        public int DoubleFaults { get; set; }
        public int FSMade { get; set; }
        public int SSMade { get; set; }
        public int FhVWinners { get; set; }
        public int BhVWinners { get; set; }
        public int FhUFErrors { get; set; }
        public int BhUFErrors { get; set; }
        public int FHFError { get; set; }
        public int BHFError { get; set; }
        public int FHFWinner { get; set; }
        public int BHFWinner { get; set; }
        public int FHVFWinner { get; set; }
        public int BHVFWinner { get; set; }
        public int SReturned { get; set; }
        public int SUnreturned { get; set; }
    


    }
}