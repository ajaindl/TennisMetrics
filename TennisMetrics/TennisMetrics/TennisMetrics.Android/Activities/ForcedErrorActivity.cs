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
        private Button fhwf;
        private Button bhwf;
        private Button fhvwf;
        private Button bhvwf;
        public ScoreHelper sh;
        private Match match;
        private ReturnHelper rh;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ForcedError);

            sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            rh = new ReturnHelper();

            fhwf = FindViewById<Button>(Resource.Id.fhwf);
            bhwf = FindViewById<Button>(Resource.Id.bhwf);
            fhvwf = FindViewById<Button>(Resource.Id.fhvwf);
            bhvwf = FindViewById<Button>(Resource.Id.bhvwf);

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
            // Create your application here
        }
    }
}