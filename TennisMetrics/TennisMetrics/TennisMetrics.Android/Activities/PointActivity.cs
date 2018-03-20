using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TennisMetrics.Droid.Models;
using TennisMetrics.Droid.Fragments;
using TennisMetrics.Droid.Activities.Helpers;
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Point Outcome")]
    public class PointActivity : Activity
    {
        private string w = "Winner";
        private string err = "Error";
        private Button ace;
        private Button winner;
        private Button error;
        private Button unreturned;
        public ScoreHelper sh;
        private Match match;
        private ReturnHelper rh;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Point);

            sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            rh = new ReturnHelper();

            ace = FindViewById<Button>(Resource.Id.ace);
            winner = FindViewById<Button>(Resource.Id.winner);
            error = FindViewById<Button>(Resource.Id.error);
            unreturned = FindViewById<Button>(Resource.Id.forcedOppError);



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