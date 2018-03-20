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

namespace TennisMetrics.Droid.Activities.Helpers
{
    public class ViewHelper
    {
        public TextView GetGameScoreView(Context context, int score)
        {
            var view = new TextView(context);
            var viewParamas = new TableRow.LayoutParams(TableRow.LayoutParams.MatchParent, TableRow.LayoutParams.WrapContent, 1);
            view.LayoutParameters = viewParamas;
            view.TextSize = 30;
            view.Text = score.ToString();
            return view;
        }
        //public TextView GetSCoreView(Context context, string score)
        //{
        //    var view = new TextView(context);
        //    var viewParamas = new TableRow.LayoutParams(TableRow.LayoutParams.MatchParent, TableRow.LayoutParams.WrapContent, 2);
        //    view.LayoutParameters = viewParamas;
        //    view.TextSize = 30;
        //    view.Text = score;
        //    return view;

        //}

        
    }
}