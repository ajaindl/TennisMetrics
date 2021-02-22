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
using TennisMetrics.Droid.Activities.Helpers;
using TennisMetrics.Droid.Models;

namespace TennisMetrics.Droid.Activities
{
    static class ActivityChange
    {
        public static Intent Finalize(ScoreKeeper scoreKeeper, Match match, Activity activity)
        {
            scoreKeeper.PlayerAction(true);
            return ReturnHelper.ReturnToBase(scoreKeeper, match, activity);
        }
    }
}