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
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Activities.Helpers
{
    public class ReturnHelper
    {
        public Intent ReturnToBase(ScoreHelper sh, Match match, Context context)
        {
            if (sh.IsServing)
            {
                var intent = new Intent(context, typeof(ServeActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
                intent.PutExtra("Activity", "Data");
                return intent;
            }
            else
            {
                var intent = new Intent(context, typeof(ReturnActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
                intent.PutExtra("Activity", "Data");
                return intent;
            }
        }
    }
}