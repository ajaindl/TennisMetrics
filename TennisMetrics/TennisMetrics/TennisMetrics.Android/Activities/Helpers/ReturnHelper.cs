using Android.Content;
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