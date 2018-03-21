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
    [Activity(Label = "Forced Error Type")]
    public class ForcedErrorActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ForcedError);

            var sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            var rh = new ReturnHelper();

            var fhwf = FindViewById<Button>(Resource.Id.fhwf);
            var bhwf = FindViewById<Button>(Resource.Id.bhwf);
            var fhvwf = FindViewById<Button>(Resource.Id.fhvwf);
            var bhvwf = FindViewById<Button>(Resource.Id.bhvwf);

            fhwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FHFWinner += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            bhwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BHFWinner += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            fhvwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FHVFWinner += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            bhvwf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BHVFWinner += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };

        }
    }
}