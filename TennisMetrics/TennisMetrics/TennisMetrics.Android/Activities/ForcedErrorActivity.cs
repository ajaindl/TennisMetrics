using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using TennisMetrics.Droid.Activities.Helpers;
using TennisMetrics.Droid.Models;
using Newtonsoft.Json;
using TennisMetrics.Droid.Activities.Enums;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Forced Error Type")]
    public class ForcedErrorActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ForcedError);

            var scoreKeeper = JsonConvert.DeserializeObject<ScoreKeeper>(Intent.GetStringExtra(ExtraType.ScoreKeeper.ToString()));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra(ExtraType.ScoreKeeper.ToString()));

            var fhwf = FindViewById<Button>(Resource.Id.fhwf);
            var bhwf = FindViewById<Button>(Resource.Id.bhwf);
            var fhvwf = FindViewById<Button>(Resource.Id.fhvwf);
            var bhvwf = FindViewById<Button>(Resource.Id.bhvwf);

            fhwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FHFWinner += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };
            bhwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BHFWinner += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };
            fhvwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FHVFWinner += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };
            bhvwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BHVFWinner += 1;
                StartActivity(ActivityChange.Finalize(scoreKeeper, match, this));
            };


        }
    }
}