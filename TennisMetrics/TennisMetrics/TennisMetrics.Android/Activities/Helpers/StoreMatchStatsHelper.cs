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
using TennisMetrics.Droid.Models;
using Newtonsoft.Json;

namespace TennisMetrics.Droid.Activities.Helpers
{
    public class StoreMatchStatsHelper
    {

        public static bool StoreMatchStats(Match match)
        {
            var id = DateTime.Now.ToString();
            var localStore = Application.Context.GetSharedPreferences("Matches", FileCreationMode.Private);
            var storeEditor = localStore.Edit();

            if (localStore.GetString("idlist", null) == null)
            {
                match.StorageId = match.IdList.Count;
                match.IdList.Add(id);
                storeEditor.PutString("idlist", JsonConvert.SerializeObject(match.IdList));
            }
            else
            {
                match.IdList = JsonConvert.DeserializeAnonymousType(localStore.GetString("idlist", null), match.IdList);
                match.StorageId = match.IdList.Count;
                match.IdList.Add(id);
                storeEditor.PutString("idlist", JsonConvert.SerializeObject(match.IdList));
            }

            storeEditor.PutString(match.StorageId.ToString(), JsonConvert.SerializeObject(match));
            storeEditor.Commit();
            return true;

        }
    }
}