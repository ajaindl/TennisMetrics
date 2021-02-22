using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using TennisMetrics.Droid.Activities.Helpers;
using TennisMetrics.Droid.Models;
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Error Type")]
    public class ErrorActivity : Activity
    {
  
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Error);

            var scoreKeeper = JsonConvert.DeserializeObject<ScoreKeeper>(Intent.GetStringExtra("ScoreHelper"));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));

            var fhuf = FindViewById<Button>(Resource.Id.fhUF);
            var bhuf = FindViewById<Button>(Resource.Id.bhuf);
            var fhf = FindViewById<Button>(Resource.Id.fhFE);
            var bhf = FindViewById<Button>(Resource.Id.bhfe);

            fhuf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FhUFErrors += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };
            bhuf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BhUFErrors += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };
            fhf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FHFError += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };
            bhf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BHFError += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };
        }

    }
}