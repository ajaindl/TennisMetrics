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
using Newtonsoft.Json;
using TennisMetrics.Droid.Models;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Settings);
            var settings = new Settings();

            var thirdSetRadio = FindViewById<CheckBox>(Resource.Id.tbCheck);
            var adRadio = FindViewById<CheckBox>(Resource.Id.adCheck);
            var p1NameText = FindViewById<EditText>(Resource.Id.playerName);
            var numSets = FindViewById<NumberPicker>(Resource.Id.numGames);
            var numGames = FindViewById<NumberPicker>(Resource.Id.numSets);
            var nextBtn = FindViewById<Button>(Resource.Id.settingsNext);

            numSets.MaxValue = 5;
            numSets.MinValue = 1;
            numGames.MaxValue = 10;
            numGames.MinValue = 4;
            numSets.Value = 3;
            numGames.Value = 6;


            nextBtn.Click += (object sender, EventArgs args) =>
             {

                 var intent = new Intent(this, typeof(ServeActivity));

                 settings.Games = numGames.Value;
                 settings.Sets =numSets.Value;
                 settings.TiebreakSet = !thirdSetRadio.Checked;
                 settings.NoAd = adRadio.Checked;

                 intent.PutExtra("Settings", JsonConvert.SerializeObject(settings));
                 intent.PutExtra("P", p1NameText.Text);
                 intent.PutExtra("Activity", "Settings");
                 StartActivity(intent);
             };

        }
    }
}