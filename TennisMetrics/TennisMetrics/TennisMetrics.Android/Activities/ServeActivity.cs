using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using TennisMetrics.Droid.Models;
using TennisMetrics.Droid.Activities.Helpers;
using System.Collections.Generic;
using TennisMetrics.Droid.Activities.Enums;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Serve")]
    public class ServeActivity : Activity
    {
        private Settings settings;
        private Match match;
        private ScoreKeeper sh;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Serve);

            var vh = new ViewHelper();
            
            if (Intent.GetStringExtra("Activity") == "Data")
            {
                sh = JsonConvert.DeserializeObject<ScoreKeeper>(Intent.GetStringExtra(ExtraType.ScoreKeeper.ToString()));
                match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra(ExtraType.Match.ToString()));

            }
            else
            {
                settings = JsonConvert.DeserializeObject<Settings>(Intent.GetStringExtra("Settings"));
                sh = new ScoreKeeper(settings);
                match = new Match(settings);
                match.Player = new Player(Intent.GetStringExtra("P"));
                match.Player.Stats = new Point();
            }

            if (!sh.IsServing)
            {
                var intent = new Intent(this, typeof(ReturnActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
                StartActivity(intent);
            }

            var mainMenuButton = FindViewById<Button>(Resource.Id.mainMenu);
            var isReturnButton = FindViewById<Button>(Resource.Id.isReturn);
            var fServeButton = FindViewById<Button>(Resource.Id.fserve);
            var sServeButton = FindViewById<Button>(Resource.Id.sserve);
            var dfButton = FindViewById<Button>(Resource.Id.df);
            var playerName = FindViewById<TextView>(Resource.Id.playerName);
            var oppName = FindViewById<TextView>(Resource.Id.oppName);
            var playerRow = FindViewById<TableRow>(Resource.Id.playerSRow);
            var oppRow = FindViewById<TableRow>(Resource.Id.oppSRow);
            var playerScore = FindViewById<TextView>(Resource.Id.playerSScore);
            var oppScore = FindViewById<TextView>(Resource.Id.oppSScore);

            playerName.Text = match.Player.Name ;
            oppName.Text = "Opponent";
            foreach(var game in sh.GameFramesPlayer)
            {
                playerRow.AddView(vh.GetGameScoreView(this, game));
            }
            foreach (var game in sh.GameFramesOpp)
            {
                oppRow.AddView(vh.GetGameScoreView(this, game));
            }
            playerRow.AddView(vh.GetGameScoreView(this, sh.PlayerGames));
            oppRow.AddView(vh.GetGameScoreView(this, sh.OppGames));
            playerScore.Text = sh.GetPlayerScore();
            oppScore.Text = sh.GetOppScore();

            if (sh.InTiebreak)
            {
                playerScore.Text = sh.PlayerTbPoints.ToString();
                oppScore.Text = sh.OppTbPoints.ToString();

            }

            if (sh.Finished)
            {
                var intent = new Intent(this, typeof(StatsActivity));
                intent.PutExtra(ExtraType.Match.ToString(), JsonConvert.SerializeObject(match));
                StoreMatchStatsHelper.StoreMatchStats(match);
                StartActivity(intent);
            }


            fServeButton.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.FSMade += 1;
                match.Player.Stats.FSServed += 1;
                var intent = new Intent(this, typeof(PointActivity));
                intent.PutExtra(ExtraType.Match.ToString(), JsonConvert.SerializeObject(match));
                intent.PutExtra(ExtraType.ScoreKeeper.ToString(), JsonConvert.SerializeObject(sh));
                StartActivity(intent);

            };

            sServeButton.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.SSMade += 1;
                match.Player.Stats.FSServed += 1;
                var intent = new Intent(this, typeof(PointActivity));
                intent = ActivityChange.Finalize(sh, match, this);
                StartActivity(intent);

            };
            dfButton.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.DoubleFaults += 1;
                sh.PlayerAction(false);
                var intent = ReturnHelper.ReturnToBase(sh, match, this);
                StartActivity(intent);   

            };
            isReturnButton.Click += (object sender, EventArgs args) =>
            {
                sh.IsServing = false;
                var intent = new Intent(this, typeof(ReturnActivity));
                intent = ActivityChange.Finalize(sh, match, this);
                StartActivity(intent);
            };
            mainMenuButton.Click += (object sender, EventArgs args) =>
            {
                AlertDialog.Builder alertBuilder = new AlertDialog.Builder(this);
                alertBuilder.SetTitle("Exit");
                alertBuilder.SetMessage("Are you sure you want to exit the match?");
                alertBuilder.SetPositiveButton("Yes", (senderAlert, argsAlert) =>
                {
                    var intent = new Intent(this, typeof(StatsActivity));
                    intent.PutExtra(ExtraType.Match.ToString(), JsonConvert.SerializeObject(match));
                    StartActivity(intent);
                });
                alertBuilder.SetNegativeButton("No", (senderAlert, argsAlert) =>
                {
                    alertBuilder.Dispose();
                });


                Dialog dialog = alertBuilder.Create();
                dialog.Show();

            };



        }

    }
}