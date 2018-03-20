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

namespace TennisMetrics.Droid.Activities.Helpers
{
    public class TiebreakHelper
    {
        public TiebreakHelper()
        {
            Max = 7;
            SuperMax = 10;
            OPoints = 0;
            PPoints = 0;
            Finished = false;
   
        }
        public int Max { get; set; }
        public int SuperMax { get; set; }
        public int PPoints { get; set; }
        public int OPoints { get; set; }
        public bool IsSuper { get; set; }
        public bool Finished { get; set; }

        public ScoreHelper IncrementPoint(TiebreakHelper stats, ScoreHelper playerStats, bool playerWon, bool isSuper)
        {
            if (isSuper)
            {
                if (stats.PPoints >= stats.SuperMax - 1 && stats.OPoints < stats.PPoints)
                {
                    if (playerWon)
                    {
                        playerStats.IncrementSets(playerStats, playerWon, false);
                        playerStats.InTiebreak = false;
                        playerStats.PlayerGames += 1;
                        playerStats.TbCounter = 0;
                        return playerStats;
                    }
                    else
                    { 
                    stats.OPoints += 1;
                    playerStats.OppTbPoints += 1;
                    return playerStats;
                    }
                }
                else if(stats.OPoints >= stats.SuperMax - 1 && stats.OPoints > stats.PPoints)
                {
                    if (playerWon)
                    {
                        stats.PPoints += 1;
                        playerStats.PlayerTbPoints += 1;
                        return playerStats;
                    }
                    else
                    {
                        playerStats.IncrementSets(playerStats, playerWon, false);
                        playerStats.InTiebreak = false;
                        playerStats.OppGames += 1;
                        playerStats.TbCounter = 0;
                        return (playerStats);

                    }
                }
                else
                {
                    if (playerWon)
                    {
                        stats.PPoints += 1;
                        playerStats.PlayerTbPoints += 1;
                        return playerStats;
                    }
                    else
                    {
                        stats.OPoints += 1;
                        playerStats.OppTbPoints += 1;
                        return playerStats;
                    }
                }
            }
            else
            {
                if (stats.PPoints >= stats.Max - 1 && stats.OPoints < stats.PPoints)
                {
                    if (playerWon)
                    {
                        playerStats.PlayerGames += 1;
                        playerStats.IncrementSets(playerStats, playerWon, false);
                        playerStats.InTiebreak = false;
                        playerStats.TbCounter = 0;
                        return playerStats;
                    }
                    else
                    {
                        stats.OPoints += 1;
                        playerStats.OppTbPoints += 1;
                        return playerStats;
                    }
                }
                else if (stats.OPoints >= stats.Max - 1 && stats.OPoints > stats.PPoints)
                {
                    if (playerWon)
                    {
                        stats.PPoints += 1;
                        playerStats.PlayerTbPoints += 1;
                        return playerStats;
                    }
                    else
                    {
                        playerStats.OppGames += 1;
                        playerStats.IncrementSets(playerStats, playerWon, false);
                        playerStats.InTiebreak = false;
                        playerStats.TbCounter = 0;
                        return (playerStats);

                    }
                }
                else
                {
                    if (playerWon)
                    {
                        stats.PPoints += 1;
                        playerStats.PlayerTbPoints += 1;
                        return playerStats;
                    }
                    else
                    {
                        stats.OPoints += 1;
                        playerStats.OppTbPoints += 1;
                        return playerStats;
                    }
                }
            }
        }
    }
}