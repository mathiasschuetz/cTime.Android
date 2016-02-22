using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace cTime.Android.Activites
{
    public class LoginDialogFragment : DialogFragment
    {
        private ViewGroup _container;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            this.Dialog.SetCanceledOnTouchOutside(false);
            this.Dialog.SetCancelable(false);

            var view = inflater.Inflate(Resource.Layout.Login, this._container, false);

            return view;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.AnmeldenButton.Click += AnmeldenButtonOnClick;
        }

        public Button AnmeldenButton { get; set; }
        
        private void AnmeldenButtonOnClick(object sender, EventArgs eventArgs)
        {
            
        }
    }
}