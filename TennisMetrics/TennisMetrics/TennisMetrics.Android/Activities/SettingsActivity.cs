using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using TennisMetrics.Droid.Models;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Settings);
            var settings = new Settings();

            var gameSpinner = FindViewById<Spinner>(Resource.Id.gamesSpinner);
            var setsSpinner = FindViewById<Spinner>(Resource.Id.setsSpinner);
            var thirdSetRadio = FindViewById<CheckBox>(Resource.Id.tbCheck);
            var adRadio = FindViewById<CheckBox>(Resource.Id.adCheck);
            var p1NameText = FindViewById<EditText>(Resource.Id.playerName);
            var nextBtn = FindViewById<Button>(Resource.Id.settingsNext);

            var setSpinnerAdapater = ArrayAdapter.CreateFromResource(this, Resource.Array.sets_array, Android.Resource.Layout.SimpleSpinnerItem);
            var gameSpinnerAdapater = ArrayAdapter.CreateFromResource(this, Resource.Array.games_array, Android.Resource.Layout.SimpleSpinnerItem);
            setSpinnerAdapater.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            gameSpinnerAdapater.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            gameSpinner.Adapter = gameSpinnerAdapater;
            setsSpinner.Adapter = setSpinnerAdapater;


            nextBtn.Click += (object sender, EventArgs args) =>
             {

                 var intent = new Intent(this, typeof(ServeActivity));

                 //settings.Games = numGames.Value;
                 //settings.Sets =numSets.Value;
                 settings.TiebreakSet = !thirdSetRadio.Checked;
                 settings.NoAd = adRadio.Checked;
                 settings.Sets = Convert.ToInt32(setsSpinner.SelectedItem.ToString());
                 settings.Games = Convert.ToInt32(gameSpinner.SelectedItem.ToString());
                 intent.PutExtra("Settings", JsonConvert.SerializeObject(settings));
                 intent.PutExtra("P", p1NameText.Text);
                 intent.PutExtra("Activity", "Settings");
                 StartActivity(intent);
             };


        }
    }
}