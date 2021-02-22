using System.Collections.Generic;
using TennisMetrics.Droid.Models;

namespace TennisMetrics.Droid.Activities.Helpers
{
    public class ScoreKeeper
    {
        public ScoreKeeper()
        {
            MaxGames = 6;
            MaxSets = 3;
            PlayerScore = "0";
            OppScore = "0";
            PlayerPoints = 0;
            OppPoints = 0;
            PlayerGames = 0;
            OppGames = 0;
            PlayerSets = 0;
            OppSets = 0;
            TiebreakSet = true;
            InTiebreak = false;
            NoAd = false;
            Tb = new TiebreakHelper();
            IsSuper = false;
            PlayerTbPoints = 0;
            OppTbPoints = 0;
            IsServing = true;
            TbCounter = 0;
            GameFramesPlayer = new List<int>();
            GameFramesOpp = new List<int>();
            Finished = false;

        }
        public ScoreKeeper(Settings settings)
        {
            MaxGames = settings.Games==0? 6: settings.Games;
            MaxSets = settings.Sets==0? 3: settings.Sets;
            PlayerScore = "0";
            OppScore = "0";
            PlayerPoints = 0;
            OppPoints = 0;
            PlayerGames = 0;
            OppGames = 0;
            PlayerSets = 0;
            OppSets = 0;
            TiebreakSet = settings.TiebreakSet;
            InTiebreak = false;
            NoAd = settings.NoAd;
            Tb = new TiebreakHelper();
            IsSuper = false;
            PlayerTbPoints = 0;
            OppTbPoints = 0;
            TbCounter = 0;
            IsServing = true;
            CurrentMatchScoreO = "0";
            CurrentMatchScoreP = "0";
            GameFramesPlayer = new List<int>();
            GameFramesOpp = new List<int>();
            Finished = false;

        }
        public int MaxGames { get; set; }
        public int MaxSets { get; set; }
        public string PlayerScore { get; set; }
        public string OppScore { get; set; }
        public int PlayerPoints { get; set; }
        public int OppPoints { get; set; }
        public int PlayerGames { get; set; }
        public int OppGames { get; set; }
        public int PlayerSets { get; set; }
        public int OppSets { get; set; }
        public bool TiebreakSet { get; set; }
        public bool InTiebreak { get; set; }
        public bool NoAd { get; set; }
        public TiebreakHelper Tb { get; set; }
        public bool IsSuper { get; set; }
        public int PlayerTbPoints { get; set; }
        public int OppTbPoints { get; set; }
        public bool IsServing { get; set; }
        public string CurrentMatchScoreP { get; set; }
        public string CurrentMatchScoreO { get; set; }
        public int TbCounter { get; set; }
        public List<int> GameFramesPlayer { get; set; }
        public List<int> GameFramesOpp { get; set; }
        public bool Finished { get; set; }



        public void PlayerAction(bool playerWon)
        {
            IncrementPoint(playerWon);
        }

        public string GetPlayerScore()
        {
            return GetScore(PlayerPoints);
        }

        public string GetOppScore()
        {
            return GetScore(OppPoints);
        }

        private string GetScore(int points)
        {
            string score;
          
            switch (points)
            {
                case 1:
                    score = "15";
                    break;
                case 2:
                    score = "30";
                    break;
                case 3:
                    score = "40";
                    break;
                case 4:
                    score = "Ad";
                        break;
                default:
                    score = "0";
                    break;
            }

            return score;
        }

        private void IncrementPoint(bool playerWon)
        {
            if (InTiebreak)
            {
                IsServingTieBreak();
                Tb.IncrementPoint(Tb, this, playerWon, IsSuper);

            }
            else
            {
                if (playerWon)
                {
                    if (PlayerPoints >= 3)
                    {
                        switch (OppPoints)
                        {
                            case 0:
                                ResetPoints();
                                IncrementGames(playerWon);
                                break;
                            case 1:
                                ResetPoints();
                                IncrementGames(playerWon);
                                break;
                            case 2:
                                ResetPoints();
                                IncrementGames(playerWon);
                                break;
                            case 3:
                                if (PlayerPoints == 3)
                                {
                                    if(NoAd)
                                    {
                                        ResetPoints();
                                        IncrementGames(playerWon);
                                    }
                                    else
                                        PlayerPoints += 1;
                                }
                                else
                                {
                                    ResetPoints();
                                    IncrementGames(playerWon);
                                }
                                break;
                            case 4: OppPoints -= 1; break;
                        }
                    }
                    else
                        PlayerPoints += 1;

                }
                else
                {
                    if (OppPoints >= 3)
                    {
                        switch (PlayerPoints)
                        {
                            case 0:
                                ResetPoints();
                                IncrementGames(playerWon);
                                break;
                            case 1:
                                ResetPoints();
                                IncrementGames(playerWon);
                                break;
                            case 2:
                                ResetPoints();
                                IncrementGames(playerWon);
                                break;
                            case 3:
                                if (PlayerPoints == 3)
                                {
                                    if (NoAd)
                                    {
                                        ResetPoints();
                                        IncrementGames(playerWon);
                                    }
                                    else
                                         OppPoints += 1;
                                }
                                else
                                {
                                    ResetPoints();
                                    IncrementGames(playerWon);
                                }
                                break;
                            case 4: PlayerPoints -= 1; break;
                        }
                    }
                    else
                        OppPoints += 1;

                }
            }
        }
        private void IncrementGames(bool playerWon)
        {
           
                if (PlayerGames >= MaxGames - 1 && OppGames < PlayerGames - 1)
                {
                        if (playerWon)
                        {
                            PlayerGames += 1;
                            IncrementSets(playerWon);
                            ResetGames();
                            ResetPoints();
                        }
                        else
                        {
                            OppGames += 1;
                        }
                }
               else if(PlayerGames >= MaxGames-1 && OppGames >= MaxGames-1)
                {
                        if (OppGames == MaxGames - 1 && PlayerGames == MaxGames - 1)
                        {
                            PlayerGames += 1;

                        }
                        else if (OppGames == MaxGames && PlayerGames == MaxGames - 1)
                        {
                            if (playerWon)
                            {
                                PlayerGames += 1;
                                InTiebreak = true;
                            }
                            else
                            {
                                IncrementSets(playerWon);
                            }

                        }
                        else if (OppGames == MaxGames - 1 && PlayerGames == MaxGames)
                        {
                            if (playerWon)
                            {
                                PlayerGames += 1;
                                IncrementSets(playerWon);
                           
                            }

                            else
                            {
                                OppGames += 1;
                                InTiebreak = true;
                            }
                        }
   
                }
            else if (OppGames >= MaxGames - 1 && PlayerGames < OppGames - 1)
            {
                if (!playerWon)
                {
                    OppGames += 1;
                    IncrementSets(!playerWon);
                    ResetGames();
                    ResetPoints();
                }
                else
                {
                    PlayerGames += 1;

                }
            }
            else
                {
                    if (playerWon)
                    {   
                        PlayerGames += 1;
                        CurrentMatchScoreP = PlayerGames.ToString();
                    }
                    else
                    {
                        OppGames += 1;
                        CurrentMatchScoreO = OppGames.ToString();
                    }
                
                }
        
        }
        private void IncrementSets(bool playerWon)
        {

            if (PlayerSets == MaxSets - 2 && OppSets < MaxSets - 2)
            {
                if (playerWon)
                {
                    PlayerSets += 1;
                    GameFramesPlayer.Add(PlayerGames);
                    GameFramesOpp.Add(OppGames);
                    ConcludeMatch();
                }
                else
                {
                    OppSets += 1;
                    GameFramesPlayer.Add(PlayerGames);
                    GameFramesOpp.Add(OppGames);
                    if (PlayerSets == OppSets)
                    {
                        if (!TiebreakSet)
                        {
                            InTiebreak = true;
                            IsSuper = true;
                        }
                    }
                }
            }
            else if (PlayerSets < MaxSets - 2 && OppSets == MaxSets - 2)
            {
                if (playerWon)
                {
                    
                    PlayerSets += 1;
                    GameFramesPlayer.Add(PlayerGames);
                    GameFramesOpp.Add(OppGames);
                    if (PlayerSets== OppSets)
                    {
                        if (!TiebreakSet)
                        {
                            InTiebreak = true;
                            IsSuper = true;
                        }
                    }
                }
                else
                {
                    OppSets += 1;
                    ConcludeMatch();
                }
            }
            else
            {
                if (MaxSets == 1)
                {
                    ConcludeMatch();
                }
                else
                {
                    if (playerWon)
                    {
                        PlayerSets += 1;
                        GameFramesPlayer.Add(PlayerGames);
                        GameFramesOpp.Add(OppGames);
                    }
                    else
                    {
                        OppSets += 1;
                        GameFramesPlayer.Add(PlayerGames);
                        GameFramesOpp.Add(OppGames);
                    }
                }
            }
        }
        public ScoreKeeper IncrementSets(ScoreKeeper stats, bool playerWon, bool inTiebreak)
        {
            if (PlayerSets == MaxSets - 2 && OppSets < MaxSets - 2)
            {
                if (playerWon)
                {
                    PlayerSets += 1;
                    GameFramesPlayer.Add(PlayerGames);
                    GameFramesOpp.Add(OppGames);
                    InTiebreak = false;
                    ResetGames();
                    ConcludeMatch();
                }
                else
                {
                    OppSets += 1;
                    GameFramesPlayer.Add(PlayerGames);
                    GameFramesOpp.Add(OppGames);
                    ResetGames();
                    if (PlayerSets == OppSets)
                    {
                        if (!TiebreakSet)
                        {
                            InTiebreak = true;
                            IsSuper = true;
                        }
                    }
                }
            }
            else if (PlayerSets < MaxSets - 2 && OppSets == MaxSets - 2)
            {
                if (playerWon)
                {
                    PlayerSets += 1;
                    GameFramesPlayer.Add(PlayerGames);
                    GameFramesOpp.Add(OppGames);
                    ResetGames();
                    if (PlayerSets == OppSets)
                    {
                        if (!TiebreakSet)
                        {
                            InTiebreak = true;
                            IsSuper = true;
                        }
                    }
                }
                else
                {
                    OppSets += 1;
                    GameFramesPlayer.Add(PlayerGames);
                    GameFramesOpp.Add(OppGames);
                    ResetGames();
                    InTiebreak = false;
                    ConcludeMatch();
                }
            }
            else
            {
                if (MaxSets == 1)
                {
                    ConcludeMatch();
                }
                else
                {
                    if (playerWon)
                    {
                        PlayerSets += 1;
                        GameFramesPlayer.Add(PlayerGames);
                        GameFramesOpp.Add(OppGames);
                        ResetGames();
                    }
                    else
                    {
                        OppSets += 1;
                        GameFramesPlayer.Add(PlayerGames);
                        GameFramesOpp.Add(OppGames);
                        ResetGames();
                    }
                }
            }

            return stats;
        }

        private void ConcludeMatch()
        {
            Finished = true;
        }

        private void ResetPoints()
        {
            IsServing = !IsServing;
            OppPoints = 0;
            PlayerPoints = 0;         
        }

        private void ResetGames()
        {
            OppGames = 0;
            PlayerGames = 0;
          
        }
        private void IsServingTieBreak()
        {
            if (TbCounter == 0)
            {
                TbCounter += 1;
            }
            else if (TbCounter == 1)
            {
                TbCounter += 1;
            }
            else
            {
                TbCounter = 0;
                IsServingTieBreak();
            }
        }
    }
}