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
    [Activity(Label = "Winner Type")]
    public class WinnerActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Winner);

            var sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            var rh = new ReturnHelper();


            var fhgs = FindViewById<Button>(Resource.Id.fhw);
            var bhgs = FindViewById<Button>(Resource.Id.bhw);
            var fhv = FindViewById<Button>(Resource.Id.fhvw);
            var bhv = FindViewById<Button>(Resource.Id.bhvw);

            fhgs.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FhWinners += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            bhgs.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BhWinners += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            fhv.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FhVWinners += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            bhv.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BhVWinners += 1;
                sh.PlayerAction(sh, true);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };






        }
    }
}