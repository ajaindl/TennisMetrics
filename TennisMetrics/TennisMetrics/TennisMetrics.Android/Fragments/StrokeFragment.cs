using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TennisMetrics.Droid.Models;
using TennisMetrics.Droid.Activities.Helpers;
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Fragments
{

    public class StrokeFragment : Fragment
    {
        Button gs;
        Button vy;
        ScoreHelper sh;
        Match matchStats;

        public string ShString { get; set; }
        public string MsString { get; set; }
        public Fragment CurrentFragment { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            sh = JsonConvert.DeserializeObject<ScoreHelper>(ShString);
            matchStats = JsonConvert.DeserializeObject<Match>(MsString);

            gs = Activity.FindViewById<Button>(Resource.Id.groundstroke);
            vy = Activity.FindViewById<Button>(Resource.Id.volley);

            gs.Click += (object sender, EventArgs args) =>
            {
                sh.PlayerAction(sh, true);
                ShString = JsonConvert.SerializeObject(sh);
                HideFragments();
            };
            vy.Click += (object sender, EventArgs args) =>
            {
                sh.PlayerAction(sh, true);
                ShString = JsonConvert.SerializeObject(sh);
                HideFragments();
            };

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.StrokeFragment, container, false);

            return view;
        }
        private void HideFragments()
        {
            var transaction = FragmentManager.BeginTransaction();
            transaction.Remove(CurrentFragment);
            transaction.Commit();

        }
    }
}