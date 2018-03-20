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
    public class Settings
    {
        public int Sets { get; set; }
        public bool TiebreakSet { get; set; }
        public int Games { get; set; }
        public bool NoAd { get; set; }
    }
}