using System;
using Android.App;
using Android.OS;
using Android.Views;

namespace cTime.Android.Activites
{
    public class LoginDialogFragment : DialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Login, container, false);

            return view;
        }
    }
}