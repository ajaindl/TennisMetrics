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
using TennisMetrics.Droid.Activities.Helpers;
using TennisMetrics.Droid.Models;
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Winner Type")]
    public class WinnerActivity : Activity
    {
        private Button fhgs;
        private Button bhgs;
        private Button fhv;
        private Button bhv;
        public ScoreHelper sh;
        private Match match;
        private ReturnHelper rh;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Winner);

            sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            rh = new ReturnHelper();


            fhgs = FindViewById<Button>(Resource.Id.fhw);
            bhgs = FindViewById<Button>(Resource.Id.bhw);
            fhv = FindViewById<Button>(Resource.Id.fhvw);
            bhv = FindViewById<Button>(Resource.Id.bhvw);

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
        //private void ReturnToBase(ScoreHelper sh, Match stats)
        //{
        //    if (sh.IsServing)
        //    {
        //        var intent = new Intent(this, typeof(ServeActivity));
        //        intent.PutExtra("Match", JsonConvert.SerializeObject(match));
        //        intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
        //        intent.PutExtra("Activity", "Data");
        //        StartActivity(intent);
        //    }
        //    else
        //    {
        //        var intent = new Intent(this, typeof(ReturnActivity));
        //        intent.PutExtra("Match", JsonConvert.SerializeObject(match));
        //        intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
        //        intent.PutExtra("Activity", "Data");
        //        StartActivity(intent);
        //    }
        //}
    }
}