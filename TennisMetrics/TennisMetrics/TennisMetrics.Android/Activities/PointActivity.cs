using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using TennisMetrics.Droid.Models;
using TennisMetrics.Droid.Activities.Helpers;
using Newtonsoft.Json;
using TennisMetrics.Droid.Activities.Enums;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Point Outcome")]
    public class PointActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Point);

            var scoreKeeper = JsonConvert.DeserializeObject<ScoreKeeper>(Intent.GetStringExtra(ExtraType.ScoreKeeper.ToString()));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra(ExtraType.Match.ToString()));

            var ace = FindViewById<Button>(Resource.Id.ace);
            var winner = FindViewById<Button>(Resource.Id.winner);
            var error = FindViewById<Button>(Resource.Id.error);
            var unreturned = FindViewById<Button>(Resource.Id.forcedOppError);

            if (!scoreKeeper.IsServing)
            {
                ace.Enabled = false;
            }


            ace.Click += (object sender, EventArgs args) =>
             {
                 match.Player.Stats.Aces += 1;
                 StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));

             };
            winner.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(WinnerActivity));
                FinalizeActivitySwap(intent);

            };
            error.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(ErrorActivity));
                FinalizeActivitySwap(intent);
            };
            unreturned.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(ForcedErrorActivity));
                FinalizeActivitySwap(intent);
            };

 

            void FinalizeActivitySwap(Intent intent)
            {
                intent.PutExtra(ExtraType.Match.ToString(), JsonConvert.SerializeObject(match));
                intent.PutExtra(ExtraType.ScoreKeeper.ToString(), JsonConvert.SerializeObject(scoreKeeper));
                StartActivity(intent);
            }


        }

    
    }
}