using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using TennisMetrics.Droid.Models;
using TennisMetrics.Droid.Activities.Helpers;
using TennisMetrics.Droid.Activities.Enums;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Return")]
    public class ReturnActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Returned);

            var view = new ViewHelper();
            var scoreKeeper = JsonConvert.DeserializeObject<ScoreKeeper>(Intent.GetStringExtra(ExtraType.ScoreKeeper.ToString()));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra(ExtraType.Match.ToString()));


            var mainMenuButton = FindViewById<Button>(Resource.Id.mainMenuR);
            var r = FindViewById<Button>(Resource.Id.returned);
            var ur = FindViewById<Button>(Resource.Id.unreturned);
            var playerName = FindViewById<TextView>(Resource.Id.playerRName);
            var oppName = FindViewById<TextView>(Resource.Id.oppRName);
            var playerRow = FindViewById<TableRow>(Resource.Id.playerRRow);
            var oppRow = FindViewById<TableRow>(Resource.Id.oppRRow);
            var playerScore = FindViewById<TextView>(Resource.Id.playerRScore);
            var oppScore = FindViewById<TextView>(Resource.Id.oppRScore);



            if (scoreKeeper.IsServing)
            {
                var intent = ReturnHelper.ReturnToBase(scoreKeeper, match, this);
                StartActivity(intent);
            }
            playerName.Text = match.Player.Name;
            oppName.Text = "Opponent";
            foreach (var game in scoreKeeper.GameFramesPlayer)
            {
                playerRow.AddView(view.GetGameScoreView(this, game));
            }
            foreach (var game in scoreKeeper.GameFramesOpp)
            {
                oppRow.AddView(view.GetGameScoreView(this, game));
            }
            playerRow.AddView(view.GetGameScoreView(this, scoreKeeper.PlayerGames));
            oppRow.AddView(view.GetGameScoreView(this, scoreKeeper.OppGames));
            playerScore.Text = scoreKeeper.GetPlayerScore();
            oppScore.Text = scoreKeeper.GetOppScore();

            if (scoreKeeper.InTiebreak)
            {
                    playerScore.Text = scoreKeeper.PlayerTbPoints.ToString();
                    oppScore.Text = scoreKeeper.OppTbPoints.ToString();
        
            }

            if (scoreKeeper.Finished)
            {
                StoreMatchStatsHelper.StoreMatchStats(match);
                var intent = new Intent(this, typeof(StatsActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                StartActivity(intent);
            }



            r.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.SReturned += 1;
                var intent = ActivityChange.Finalize(scoreKeeper, match, this);
                StartActivity(intent);
            };

            ur.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.SUnreturned += 1;
                scoreKeeper.PlayerAction(false);
                var intent = ReturnHelper.ReturnToBase(scoreKeeper, match, this);
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
                    ActivityChange.Finalize(scoreKeeper, match, this);
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