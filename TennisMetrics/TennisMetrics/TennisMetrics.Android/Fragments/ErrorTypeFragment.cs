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
    public class ErrorTypeFragment : Fragment
    {
        Button f;
        Button uf;
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

            f = Activity.FindViewById<Button>(Resource.Id.forced);
            uf = Activity.FindViewById<Button>(Resource.Id.unforced);

            f.Click += (object sender, EventArgs args) =>
            {
                sh.PlayerAction(sh, false);
                matchStats.Player.Stats.FHFError += 1;
                ShString = JsonConvert.SerializeObject(sh);
                HideFragments();
            };
            uf.Click += (object sender, EventArgs args) =>
            {
                sh.PlayerAction(sh, false);
                ShString = JsonConvert.SerializeObject(sh);
                HideFragments();
            };

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {


            View view = inflater.Inflate(Resource.Layout.ErrorTypeFragment, container, false);

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