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

            var sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            var rh = new ReturnHelper();

            var fhuf = FindViewById<Button>(Resource.Id.fhuf);
            var bhuf = FindViewById<Button>(Resource.Id.bhuf);
            var fhf = FindViewById<Button>(Resource.Id.fhfe);
            var bhf = FindViewById<Button>(Resource.Id.bhfe);

            fhuf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FhUFErrors += 1;
                sh.PlayerAction(sh, false);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            bhuf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BhUFErrors += 1;
                sh.PlayerAction(sh, false);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            fhf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FHFError += 1;
                sh.PlayerAction(sh, false);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
            bhf.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.BHFError += 1;
                sh.PlayerAction(sh, false);
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            };
        }

    }
}