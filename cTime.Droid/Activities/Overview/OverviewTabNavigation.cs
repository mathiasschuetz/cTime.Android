using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using cTime.Droid.Adapter;

namespace cTime.Droid.Activities.Overview
{
    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class OverviewTabNavigation : AppCompatActivity
    {
        #region fields

        #endregion

        #region properties

        private DrawerLayout Drawer { get; set; }

        #endregion

        #region lifecycle

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Views
            this.SetContentView(Resource.Layout.OverviewTabNavigation);
            this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            //ActionBar
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.Toolbar);
            this.SetSupportActionBar(toolbar);
            this.SupportActionBar.Title = "cTime";

            //Drawer
            this.Drawer = this.FindViewById<DrawerLayout>(Resource.Id.DrawerLayout);

            //ViewPager  
            var viewpager = this.FindViewById<ViewPager>(Resource.Id.ViewPager);
            this.SetupViewPager(viewpager);
            viewpager.SetCurrentItem(1, true);

            //TabLayout  
            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.Tabs);
            tabLayout.SetupWithViewPager(viewpager);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Layout.ToolbarSettings, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        #endregion

        #region methodsprivate void SetupViewPager(ViewPager viewPager)

        private void SetupViewPager(ViewPager viewPager)
        {
            var adapter = new ViewPagerAdapter(this.SupportFragmentManager);
            adapter.AddFragment(new PresentListFragment(), "Anwesend");
            adapter.AddFragment(new StampFragment(), "Stempeln");
            adapter.AddFragment(new TimeListFragment(), "Zeiten");
            viewPager.Adapter = adapter;
            viewPager.Adapter.NotifyDataSetChanged();
        }

        #endregion
    }
}