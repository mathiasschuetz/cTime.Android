using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace cTime.Droid
{
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class LoginActivity : AppCompatActivity
    {
        #region fields

        #endregion

        #region properties

        private DrawerLayout Drawer { get; set; }


        private EditText CompanyKey { get; set; }

        private EditText Username { get; set; }

        private EditText Password { get; set; }

        private Button Connect { get; set; }

        #endregion

        #region lifecycle

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Views
            this.SetContentView(Resource.Layout.Login);
            this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            //ActionBar
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.Toolbar);
            this.SetSupportActionBar(toolbar);
            this.SupportActionBar.Title = "Login";

            //Drawer
            this.Drawer = this.FindViewById<DrawerLayout>(Resource.Id.DrawerLayout);

            //Items
            this.CompanyKey = this.FindViewById<EditText>(Resource.Id.CompanyKey);
            this.Username = this.FindViewById<EditText>(Resource.Id.Username);
            this.Password = this.FindViewById<EditText>(Resource.Id.Password);
            this.Connect = this.FindViewById<Button>(Resource.Id.Connect);

            this.Connect.Click += (sender, args) => { };
        }

        #endregion

        #region methods

        #endregion
    }
}