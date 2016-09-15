using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace cTime.Droid
{
    [Activity(Label = "cTime", MainLauncher = true, Theme = "@style/AppTheme")]
    public class LoginActivity : AppCompatActivity
    {
        #region fields

        #endregion

        #region properties

        private DrawerLayout Drawer { get; set; }

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


        }

        #endregion

        #region methods



        #endregion
    }
}