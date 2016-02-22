using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using cTime.Android.Core.Services.CTime;

namespace cTime.Android.Activites
{
    public class LoginDialogFragment : DialogFragment
    {
        private ViewGroup _container;
        private CTimeService _cTimeService;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            this.Dialog.SetCanceledOnTouchOutside(false);
            this.Dialog.SetCancelable(false);

            this._cTimeService = new CTimeService();

            var view = inflater.Inflate(Resource.Layout.Login, this._container, false);
            
            this.AnmeldenButton = view.FindViewById<Button>(Resource.Id.anmelden);
            this.FirmGUID = view.FindViewById<EditText>(Resource.Id.firmGUID);
            this.Mail = view.FindViewById<EditText>(Resource.Id.mail);
            this.Password = view.FindViewById<EditText>(Resource.Id.password);
            this.ProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            this.AnmeldenButton = view.FindViewById<Button>(Resource.Id.anmelden);

            this.ProgressBar.Visibility = ViewStates.Invisible;

            this.AnmeldenButton.Click += this.AnmeldenButtonOnClick;

            return view;
        }

        public Button AnmeldenButton { get; set; }
        public EditText FirmGUID { get; set; }
        public EditText Mail { get; set; }
        public EditText Password { get; set; }

        public ProgressBar ProgressBar { get; set; }
        
        private async void AnmeldenButtonOnClick(object sender, EventArgs eventArgs)
        {
            this.ProgressBar.Visibility = ViewStates.Visible;

            try
            {
                var user = await this._cTimeService.Login(this.FirmGUID.Text, this.Mail.Text, this.Password.Text);

                this.ProgressBar.Visibility = ViewStates.Invisible;

                if (user == null)
                {
                    return;
                }

                //this._sessionStateService.CompanyId = this.CompanyId;
                //this._sessionStateService.CurrentUser = user;

                //await this._sessionStateService.SaveStateAsync();

                //this._application.CurrentState = IoC.Get<LoggedInApplicationState>();
            }
            catch (Exception exception)
            {
                //await this._exceptionHandler.HandleAsync(exception);
            }
        }
    }
}