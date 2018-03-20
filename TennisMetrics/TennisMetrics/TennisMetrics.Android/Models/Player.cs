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

namespace TennisMetrics.Droid.Models
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public Point Stats { get; set; }
        public bool IsServing { get; set; }
        public int GamesWon { get; set; }
        public int SetsWon { get; set; }
        public int PointsWon { get; set; }

    }
}