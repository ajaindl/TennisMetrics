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
    [Activity(Label = "StatsActivity")]
    public class StatsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.StatsDisplay);

            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            var stats = match.Player.Stats;

            var mainMenuButton = FindViewById<Button>(Resource.Id.mainMenuStats);
            var score = FindViewById<TextView>(Resource.Id.matchScore);
            var fhW = FindViewById<TextView>(Resource.Id.fhW);
            var fhFE = FindViewById<TextView>(Resource.Id.fhFE);
            var fhUF = FindViewById<TextView>(Resource.Id.fhUF);
            var fhF = FindViewById<TextView>(Resource.Id.fhF);
            var bhW = FindViewById<TextView>(Resource.Id.bhW);
            var bhFE = FindViewById<TextView>(Resource.Id.bhFE);
            var bhUF = FindViewById<TextView>(Resource.Id.bhUF);
            var bhF = FindViewById<TextView>(Resource.Id.bhF);
            var fhvW = FindViewById<TextView>(Resource.Id.fhvW);
            var fhvFE = FindViewById<TextView>(Resource.Id.fhvFE);
            var fhvUF = FindViewById<TextView>(Resource.Id.fhvUF);
            var fhvF = FindViewById<TextView>(Resource.Id.fhvF);
            var bhvW = FindViewById<TextView>(Resource.Id.bhvW);
            var bhvFE = FindViewById<TextView>(Resource.Id.bhvFE);
            var bhvUF = FindViewById<TextView>(Resource.Id.bhvUF);
            var bhvF = FindViewById<TextView>(Resource.Id.bhvF);
            var fsP = FindViewById<TextView>(Resource.Id.fsP);
            var ssP = FindViewById<TextView>(Resource.Id.ssP);
            var aces = FindViewById<TextView>(Resource.Id.aces);
            var df = FindViewById<TextView>(Resource.Id.dfs);

            fhW.Text = stats.FhWinners.ToString();
            fhFE.Text = stats.FHFWinner.ToString();
            fhUF.Text = stats.FhUFErrors.ToString();
            fhF.Text = stats.FHFError.ToString();
            bhW.Text = stats.BhWinners.ToString();
            bhFE.Text = stats.BHFWinner.ToString();
            bhUF.Text = stats.BhUFErrors.ToString();
            bhF.Text = stats.BHFError.ToString();
            fhvW.Text = stats.FhVWinners.ToString();
            fhvFE.Text = stats.FHVFWinner.ToString();
            bhvW.Text = stats.BhVWinners.ToString();
            bhvFE.Text = stats.BHVFWinner.ToString();
            fsP.Text = GetServePercentage(stats.FSMade, stats.FSServed)+"%";
            ssP.Text = GetServePercentage(stats.SSMade, stats.SSMade+ stats.DoubleFaults)+"%";
            aces.Text = stats.Aces.ToString();
            df.Text = stats.DoubleFaults.ToString();

            mainMenuButton.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            string GetServePercentage(int made, int total)
            {
                if (total == 0)
                    return "0";
                var serves = Convert.ToDouble(made);

                var percentage = serves / total;
                percentage = Math.Round(percentage, 2) * 100;
                return percentage.ToString();


            }



        }
    }
}