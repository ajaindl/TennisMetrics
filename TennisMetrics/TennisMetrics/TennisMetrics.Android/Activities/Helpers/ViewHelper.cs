using Android.Content;
using Android.Widget;

namespace TennisMetrics.Droid.Activities.Helpers
{
    public class ViewHelper
    {
        public TextView GetGameScoreView(Context context, int score)
        {
            var view = new TextView(context);
            var viewParams = new TableRow.LayoutParams(TableRow.LayoutParams.MatchParent, TableRow.LayoutParams.WrapContent, 1);
            view.LayoutParameters = viewParams;
            view.TextSize = 30;
            view.Text = score.ToString();
            return view;
        }


        
    }
}