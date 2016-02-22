using System.Net.Cache;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using cTime.Android.Activites;
using Org.Apache.Http.Client.Methods;
using Org.Apache.Http.Protocol;
using ActionBarDrawerToggle = cTime.Android.Toggles.ActionBarDrawerToggle;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace cTime.Android
{
    [Activity(Label = "cTime", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/cTime")]
    public class MainActivity : AppCompatActivity
    {
        private SupportToolbar _toolbar;
        private ActionBarDrawerToggle _actionBarDrawerToggle;
        private DrawerLayout _drawerLayout;
        private ListView _leftDrawer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Toolbar);

            this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            //Main
            this._toolbar = this.FindViewById<SupportToolbar>(Resource.Id.toolbar);
            this._drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            this._leftDrawer = this.FindViewById<ListView>(Resource.Id.left_drawer);

            this.SetSupportActionBar(this._toolbar);
            
            this._actionBarDrawerToggle = new ActionBarDrawerToggle(this, this._drawerLayout, Resource.String.openDrawer,
                Resource.String.closeDrawer);

            this._drawerLayout.SetDrawerListener(this._actionBarDrawerToggle);
            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayShowTitleEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            this._actionBarDrawerToggle.SyncState();
            
            var fragmentTransaction = this.FragmentManager.BeginTransaction();
            var login = new LoginDialogFragment();
            login.Show(fragmentTransaction, "Login Dialog");
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            this._actionBarDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
    }
}