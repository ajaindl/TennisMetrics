using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using TennisMetrics.Droid.Models;
using TennisMetrics.Droid.Activities.Helpers;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Return")]
    public class ReturnActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Returned);

            var vh = new ViewHelper();
            var sh = JsonConvert.DeserializeObject<ScoreHelper>(Intent.GetStringExtra("ScoreHelper"));
            var match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("Match"));
            var rh = new ReturnHelper();


            var mainMenuButton = FindViewById<Button>(Resource.Id.mainMenuR);
            var r = FindViewById<Button>(Resource.Id.returned);
            var ur = FindViewById<Button>(Resource.Id.unreturned);
            var playerName = FindViewById<TextView>(Resource.Id.playerRName);
            var oppName = FindViewById<TextView>(Resource.Id.oppRName);
            var playerRow = FindViewById<TableRow>(Resource.Id.playerRRow);
            var oppRow = FindViewById<TableRow>(Resource.Id.oppRRow);
            var playerScore = FindViewById<TextView>(Resource.Id.playerRScore);
            var oppScore = FindViewById<TextView>(Resource.Id.oppRScore);



            if (sh.IsServing)
            {
                var intent = rh.ReturnToBase(sh, match, this);
                StartActivity(intent);
            }
            playerName.Text = match.Player.Name;
            oppName.Text = "Opponent";
            foreach (var game in sh.GameFramesPlayer)
            {
                playerRow.AddView(vh.GetGameScoreView(this, game));
            }
            foreach (var game in sh.GameFramesOpp)
            {
                oppRow.AddView(vh.GetGameScoreView(this, game));
            }
            playerRow.AddView(vh.GetGameScoreView(this, sh.PlayerGames));
            oppRow.AddView(vh.GetGameScoreView(this, sh.OppGames));
            playerScore.Text = sh.GetScore(sh.PlayerPoints);
            oppScore.Text = sh.GetScore(sh.OppPoints);

            if (sh.InTiebreak)
            {
                    playerScore.Text = sh.PlayerTbPoints.ToString();
                    oppScore.Text = sh.OppTbPoints.ToString();
        
            }

            if (sh.Finished)
            {
                var intent = new Intent(this, typeof(StatsActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                var localStore = Application.Context.GetSharedPreferences("Matches", FileCreationMode.Private);
                var storeEditor = localStore.Edit();

                if (localStore.GetString("idlist", null) == null)
                {
                    match.StorageId = match.IdList.Count;
                    match.IdList.Add(match.StorageId, DateTime.Now.ToString());
                    storeEditor.PutString("idlist", JsonConvert.SerializeObject(match.IdList));
                }
                else
                {
                    match.IdList = JsonConvert.DeserializeAnonymousType(localStore.GetString("idlist", null), match.IdList);
                    match.StorageId = match.IdList.Count;
                    match.IdList.Add(match.StorageId, DateTime.Now.ToString());
                    storeEditor.PutString("idlist", JsonConvert.SerializeObject(match.IdList));
                }

                storeEditor.PutString(match.StorageId.ToString(), JsonConvert.SerializeObject(match));
                storeEditor.Commit();
                StartActivity(intent);
            }



            r.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.SReturned += 1;
                var intent = new Intent(this, typeof(PointActivity));
                intent.PutExtra("Match", JsonConvert.SerializeObject(match));
                intent.PutExtra("ScoreHelper", JsonConvert.SerializeObject(sh));
                StartActivity(intent);
            };

            ur.Click += (object sender, EventArgs args) =>
            {
                match.Player.Stats.SUnreturned += 1;
                sh.PlayerAction(sh, false);
                var intent = rh.ReturnToBase(sh, match, this);
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
                    intent.PutExtra("Match", JsonConvert.SerializeObject(match));
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