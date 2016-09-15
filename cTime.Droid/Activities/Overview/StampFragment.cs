using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace cTime.Droid.Activities.Overview
{
    public class StampFragment : Fragment
    {
        #region fields
        #endregion

        #region properties
        #endregion

        #region lifecycle

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.StampFragment, container, false);

            return view;
        }

        #endregion

        #region methods
        #endregion
    }
}