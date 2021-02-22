using Android.Content;
using TennisMetrics.Droid.Models;
using Newtonsoft.Json;
using TennisMetrics.Droid.Activities.Enums;

namespace TennisMetrics.Droid.Activities.Helpers
{
    public static class ReturnHelper
    {
        public static Intent ReturnToBase(ScoreKeeper scoreKeeper, Match match, Context context)
        {
            if (scoreKeeper.IsServing)
            {
                var intent = new Intent(context, typeof(ServeActivity));
                intent.PutExtra(ExtraType.Match.ToString(), JsonConvert.SerializeObject(match));
                intent.PutExtra(ExtraType.ScoreKeeper.ToString(), JsonConvert.SerializeObject(scoreKeeper));
                intent.PutExtra("Activity", "Data");
                return intent;
            }
            else
            {
                var intent = new Intent(context, typeof(ReturnActivity));
                intent.PutExtra(ExtraType.Match.ToString(), JsonConvert.SerializeObject(match));
                intent.PutExtra(ExtraType.ScoreKeeper.ToString(), JsonConvert.SerializeObject(scoreKeeper));
                intent.PutExtra("Activity", "Data");
                return intent;
            }
        }
    }
}