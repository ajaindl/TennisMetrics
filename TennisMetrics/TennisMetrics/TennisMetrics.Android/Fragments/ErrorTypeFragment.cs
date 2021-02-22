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

        public string ScoreHelperString { get; set; }
        public string MatchStatsString { get; set; }
        public Fragment CurrentFragment { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            ScoreKeeper sh = JsonConvert.DeserializeObject<ScoreKeeper>(ScoreHelperString);
            Match matchStats = JsonConvert.DeserializeObject<Match>(MatchStatsString);

            Button forced = Activity.FindViewById<Button>(Resource.Id.forced);
            Button unForced = Activity.FindViewById<Button>(Resource.Id.unforced);

            forced.Click += (object sender, EventArgs args) =>
            {
                sh.PlayerAction(false);
                matchStats.Player.Stats.FHFError += 1;
                ScoreHelperString = JsonConvert.SerializeObject(sh);
                HideFragments();
            };

            unForced.Click += (object sender, EventArgs args) =>
            {
                sh.PlayerAction(false);
                matchStats.Player.Stats.FhUFErrors += 1;
                ScoreHelperString = JsonConvert.SerializeObject(sh);
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