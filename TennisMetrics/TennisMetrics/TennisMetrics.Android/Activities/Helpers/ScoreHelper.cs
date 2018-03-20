using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TennisMetrics.Droid.Models;

namespace TennisMetrics.Droid.Activities.Helpers
{
    public class ScoreHelper
    {
        public ScoreHelper()
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
        public ScoreHelper(Settings settings)
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



        public ScoreHelper PlayerAction(ScoreHelper stats, bool playerWon)
        {
            IncrementPoint(stats, playerWon);
            return stats;
        }
        public string GetScore(int sPoints)
        {
            string sScore;
          
            switch (sPoints)
            {
                case 0:
                    sScore = "0";
                    break;
                case 1:
                    sScore = "15";
                    break;
                case 2:
                    sScore = "30";
                    break;
                case 3:
                    sScore = "40";
                    break;
                case 4:
                    sScore = "Ad";
                        break;
                default:
                    sScore = "0";
                    break;
            }

            return sScore;
        }
        public static void IncrementPoint(ScoreHelper stats, bool playerWon)
        {
            if (stats.InTiebreak)
            {
                IsServingTieBreak(stats);
                stats.Tb.IncrementPoint(stats.Tb, stats, playerWon, stats.IsSuper);

            }
            else
            {
                if (playerWon)
                {
                    if (stats.PlayerPoints >= 3)
                    {
                        switch (stats.OppPoints)
                        {
                            case 0:
                                ResetPoints(stats, playerWon);
                                IncrementGames(stats, playerWon);
                                break;
                            case 1:
                                ResetPoints(stats, playerWon);
                                IncrementGames(stats, playerWon);
                                break;
                            case 2:
                                ResetPoints(stats, playerWon);
                                IncrementGames(stats, playerWon);
                                break;
                            case 3:
                                if (stats.PlayerPoints == 3)
                                {
                                    if(stats.NoAd)
                                    {
                                        ResetPoints(stats, playerWon);
                                        IncrementGames(stats, playerWon);
                                    }
                                    else
                                        stats.PlayerPoints += 1;
                                }
                                else
                                {
                                    ResetPoints(stats, playerWon);
                                    IncrementGames(stats, playerWon);
                                }
                                break;
                            case 4: stats.OppPoints -= 1; break;
                        }
                    }
                    else
                        stats.PlayerPoints += 1;

                }
                else
                {
                    if (stats.OppPoints >= 3)
                    {
                        switch (stats.PlayerPoints)
                        {
                            case 0:
                                ResetPoints(stats, playerWon);
                                IncrementGames(stats, playerWon);
                                break;
                            case 1:
                                ResetPoints(stats, playerWon);
                                IncrementGames(stats, playerWon);
                                break;
                            case 2:
                                ResetPoints(stats, playerWon);
                                IncrementGames(stats, playerWon);
                                break;
                            case 3:
                                if (stats.PlayerPoints == 3)
                                {
                                    if (stats.NoAd)
                                    {
                                        ResetPoints(stats, playerWon);
                                        IncrementGames(stats, playerWon);
                                    }
                                    else
                                         stats.OppPoints += 1;
                                }
                                else
                                {
                                    ResetPoints(stats, playerWon);
                                    IncrementGames(stats, playerWon);
                                }
                                break;
                            case 4: stats.PlayerPoints -= 1; break;
                        }
                    }
                    else
                        stats.OppPoints += 1;

                }
            }
        }
        public static void IncrementGames(ScoreHelper stats, bool playerWon)
        {
           
                if (stats.PlayerGames >= stats.MaxGames - 1 && stats.OppGames < stats.PlayerGames - 1)
                {
                        if (playerWon)
                        {
                            stats.PlayerGames += 1;
                            IncrementSets(stats, playerWon);
                            stats.ResetGames(stats, playerWon);
                            ResetPoints(stats, playerWon);
                        }
                        else
                        {
                            stats.OppGames += 1;
                        }
                }
               else if(stats.PlayerGames >= stats.MaxGames-1 && stats.OppGames >= stats.MaxGames-1)
                {
                        if (stats.OppGames == stats.MaxGames - 1 && stats.PlayerGames == stats.MaxGames - 1)
                        {
                            stats.PlayerGames += 1;

                        }
                        else if (stats.OppGames == stats.MaxGames && stats.PlayerGames == stats.MaxGames - 1)
                        {
                            if (playerWon)
                            {
                                stats.PlayerGames += 1;
                                stats.InTiebreak = true;
                            }
                            else
                            {
                                IncrementSets(stats, playerWon);
                            }

                        }
                        else if (stats.OppGames == stats.MaxGames - 1 && stats.PlayerGames == stats.MaxGames)
                        {
                            if (playerWon)
                            {
                                stats.PlayerGames += 1;
                                IncrementSets(stats, playerWon);
                           
                            }

                            else
                            {
                                stats.OppGames += 1;
                                stats.InTiebreak = true;
                            }
                        }
   
                }
            else if (stats.OppGames >= stats.MaxGames - 1 && stats.PlayerGames < stats.OppGames - 1)
            {
                if (!playerWon)
                {
                    stats.OppGames += 1;
                    IncrementSets(stats, !playerWon);
                    stats.ResetGames(stats, !playerWon);
                    ResetPoints(stats, !playerWon);
                }
                else
                {
                    stats.PlayerGames += 1;

                }
            }
            else
                {
                    if (playerWon)
                    {   
                        stats.PlayerGames += 1;
                        stats.CurrentMatchScoreP = stats.PlayerGames.ToString();
                    }
                    else
                    {
                        stats.OppGames += 1;
                        stats.CurrentMatchScoreO = stats.OppGames.ToString();
                    }
                
                }
        
        }
        public static void IncrementSets(ScoreHelper stats, bool playerWon)
        {

            if (stats.PlayerSets == stats.MaxSets - 2 && stats.OppSets < stats.MaxSets - 2)
            {
                if (playerWon)
                {
                    stats.PlayerSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    ConcludeMatch(stats, true);
                }
                else
                {
                    stats.OppSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    if (stats.PlayerSets == stats.OppSets)
                    {
                        if (!stats.TiebreakSet)
                        {
                            stats.InTiebreak = true;
                            stats.IsSuper = true;
                        }
                    }
                }
            }
            else if (stats.PlayerSets < stats.MaxSets - 2 && stats.OppSets == stats.MaxSets - 2)
            {
                if (playerWon)
                {
                    
                    stats.PlayerSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    if (stats.PlayerSets== stats.OppSets)
                    {
                        if (!stats.TiebreakSet)
                        {
                            stats.InTiebreak = true;
                            stats.IsSuper = true;
                        }
                    }
                }
                else
                {
                    stats.OppSets += 1;
                    ConcludeMatch(stats, false);
                }
            }
            else
            {
                if (playerWon)
                {
                    stats.PlayerSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                }
                else
                {
                    stats.OppSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                }
            }
        }
        public ScoreHelper IncrementSets(ScoreHelper stats, bool playerWon, bool InTiebreak)
        {
            if (stats.PlayerSets == stats.MaxSets - 2 && stats.OppSets < stats.MaxSets - 2)
            {
                if (playerWon)
                {
                    stats.PlayerSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    stats.InTiebreak = false;
                    ResetGames(stats, playerWon);
                    ConcludeMatch(stats, true);
                }
                else
                {
                    stats.OppSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    ResetGames(stats, playerWon);
                    if (stats.PlayerSets == stats.OppSets)
                    {
                        if (!stats.TiebreakSet)
                        {
                            stats.InTiebreak = true;
                            stats.IsSuper = true;
                        }
                    }
                }
            }
            else if (stats.PlayerSets < stats.MaxSets - 2 && stats.OppSets == stats.MaxSets - 2)
            {
                if (playerWon)
                {
                    stats.PlayerSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    ResetGames(stats, playerWon);
                    if (stats.PlayerSets == stats.OppSets)
                    {
                        if (!stats.TiebreakSet)
                        {
                            stats.InTiebreak = true;
                            stats.IsSuper = true;
                        }
                    }
                }
                else
                {
                    stats.OppSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    ResetGames(stats, playerWon);
                    stats.InTiebreak = false;
                    ConcludeMatch(stats, false);
                }
            }
            else
            {
                if (playerWon)
                {
                    stats.PlayerSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    ResetGames(stats, playerWon);
                }
                else
                {
                    stats.OppSets += 1;
                    stats.GameFramesPlayer.Add(stats.PlayerGames);
                    stats.GameFramesOpp.Add(stats.OppGames);
                    ResetGames(stats, playerWon);
                }
            }

            return stats;
        }
        public static void ConcludeMatch(ScoreHelper stats, bool playerWon)
        {


        }
        public static void ResetPoints(ScoreHelper stats, bool playerWon)
        {
            stats.IsServing = !stats.IsServing;
            stats.OppPoints = 0;
            stats.PlayerPoints = 0;
      
                
                
       
        }
        public void ResetGames(ScoreHelper stats, bool playerWon)
        {
            stats.OppGames = 0;
            stats.PlayerGames = 0;
          
        }
        public static ScoreHelper IsServingTieBreak(ScoreHelper sh)
        {
            if (sh.TbCounter == 0)
            {
                sh.TbCounter += 1;
                sh.IsServing = !sh.IsServing;
                return sh;
            }
            else if (sh.TbCounter == 1)
            {
                sh.TbCounter += 1;
                return sh;
            }
            else
            {
                sh.TbCounter = 0;
                IsServingTieBreak(sh);
                return sh;
            }
        }
    }
}