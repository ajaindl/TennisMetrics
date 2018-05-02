using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using TennisMetrics.Droid.Activities;
using System.Collections.Generic;
using Android.Content;

namespace TennisMetrics.Droid
{
    [Activity(Label = "Tennis Metrics", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var idList = new List<string>();
            //var matchListRetriever = Application.Context.GetSharedPreferences("Matches", Android.Content.FileCreationMode.Private);
            //idList = JsonConvert.DeserializeAnonymousType<List<string>>(matchListRetriever.GetString("idlist", new List<string>().ToString()), idList);

            // Get our button from the layout resource,
            // and attach an event to it
            var startMatchButton = FindViewById<Button>(Resource.Id.startMatch);
            var prevMatchButton = FindViewById<Button>(Resource.Id.prevMatch);

            startMatchButton.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(SettingsActivity));
                //intent.PutExtra("idlist", JsonConvert.SerializeObject(idList));
                StartActivity(intent);
            };
            prevMatchButton.Click += (object sender, EventArgs args) =>
            {
                var intent = new Intent(this, typeof(PreviousMatchActivity));

                StartActivity(intent);
            };
        }
    }
}

