using System;
using Android.App;
using Android.Widget;
using Android.OS;
using TennisMetrics.Droid.Activities;

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

            // Get our button from the layout resource,
            // and attach an event to it
            Button startMatchButton = FindViewById<Button>(Resource.Id.startMatch);

            startMatchButton.Click += (object sender, EventArgs args) =>
            {
                StartActivity(typeof(SettingsActivity));
            };
        }
    }
}

