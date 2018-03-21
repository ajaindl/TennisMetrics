using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using TennisMetrics.Droid.Models;
using TennisMetrics.Droid.Activities.Helpers;
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Point Outcome")]
    public class PointActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Point);

            var sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            var rh = new ReturnHelper();

            var ace = FindViewById<Button>(Resource.Id.ace);
            var winner = FindViewById<Button>(Resource.Id.winner);
            var error = FindViewById<Button>(Resource.Id.error);
            var unreturned = FindViewById<Button>(Resource.Id.forcedOppError);



            ace.Click += (object sender, EventArgs args) =>
             {
                 match.Player.Stats.Aces += 1;
                 sh.PlayerAction(sh, true);
                 var intent = rh.ReturnToBase(sh, match, this);
                 StartActivity(intent);

             };
            winner.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(WinnerActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
                StartActivity(intent);

            };
            error.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(ErrorActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
                StartActivity(intent);
            };
            unreturned.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(ForcedErrorActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
                StartActivity(intent);
            };

            if (!sh.IsServing)
                ace.Enabled = false;


        }

    
    }
}