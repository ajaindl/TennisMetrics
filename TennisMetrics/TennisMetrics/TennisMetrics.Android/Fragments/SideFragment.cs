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
    public class SideFragment : Fragment
    {
        private string w = "Winner";
        private string err = "Error";
        Button fh;
        Button bh;
        ScoreKeeper sh;
        Match matchStats;
        Fragment currentFragment;
        Fragment strokeFragment;

        public string ShString { get; set; }
        public string MsString { get; set; }
        public string TagType { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            sh = JsonConvert.DeserializeObject<ScoreKeeper>(ShString);
            matchStats = JsonConvert.DeserializeObject<Match>(MsString);
            strokeFragment = new StrokeFragment();

            fh = Activity.FindViewById<Button>(Resource.Id.fh);
            bh = Activity.FindViewById<Button>(Resource.Id.bh);

            fh.Click += (object sender, EventArgs args) =>
            {
                ShowFragment(strokeFragment, TagType);
            };
            bh.Click += (object sender, EventArgs args) =>
            {
                ShowFragment(strokeFragment, TagType);
            };

        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {


            View view = inflater.Inflate(Resource.Layout.SideFragment, container, false);
            return view;
        }
        private void ShowFragment(Fragment fragment, string fragType)
        {
            var transaction = FragmentManager.BeginTransaction();
            if (fragType == w)
            {
                var rFrag = (StrokeFragment)FragmentManager.FindFragmentById(fragment.Id);
                rFrag.MsString = JsonConvert.SerializeObject(matchStats);
                rFrag.ShString = JsonConvert.SerializeObject(sh);
                if (currentFragment == null)
                {

                    transaction.Add(Resource.Id.fragment, rFrag, fragType);
                    currentFragment = rFrag;
                    rFrag.CurrentFragment = rFrag;
                    transaction.Commit();
                }
                else
                {
                    transaction.Replace(Resource.Id.fragment, rFrag, fragType);
                    currentFragment = rFrag;
                    rFrag.CurrentFragment = rFrag;
                    transaction.Commit();
                }
            }
            else
            {
                var rFrag = (ErrorTypeFragment)FragmentManager.FindFragmentById(fragment.Id);
                rFrag.MatchStatsString = JsonConvert.SerializeObject(matchStats);
                rFrag.ScoreHelperString = JsonConvert.SerializeObject(sh);
                if (currentFragment == null)
                {

                    transaction.Add(Resource.Id.fragment, rFrag, fragType);
                    currentFragment = rFrag;
                    rFrag.CurrentFragment = rFrag;
                    transaction.Commit();
                }
                else
                {
                    transaction.Replace(Resource.Id.fragment, rFrag, fragType);
                    currentFragment = rFrag;
                    rFrag.CurrentFragment = rFrag;
                    transaction.Commit();
                }
            }




        }



    }
}