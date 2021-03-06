﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TennisMetrics.Droid.Models;
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Activities
{
    [Activity(Label = "PreviousMatchActivity")]
    public class PreviousMatchActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PreviousMatches);

            var matchList = new List<Match>();
            var idList = new Dictionary<int, string>();
            var idDisplayList = new List<string>();
            var matchListView = FindViewById<ListView>(Resource.Id.matchList);
            var sharedPref= Application.Context.GetSharedPreferences("Matches", FileCreationMode.Private);
            var idString = sharedPref.GetString("idlist", null);
            if(idString!=null)
                idList = JsonConvert.DeserializeAnonymousType<Dictionary<int, string>>(idString, idList);

            if (idList.Count > 0)
            {
                foreach(var id in Enumerable.Range(0, idList.Count))
                {
                    idDisplayList.Add(idList[id]);
                    matchList.Add(JsonConvert.DeserializeObject<Match>(sharedPref.GetString(id.ToString(), null)));
                }

                var listAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, idDisplayList);
                matchListView.Adapter = listAdapter;
                matchListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                {
                    var intent = new Intent(this, typeof(StatsActivity));
                    intent.PutExtra("Match", JsonConvert.SerializeObject(matchList[e.Position]));
                    StartActivity(intent);
                };

            }



            
        }

      
    }
}