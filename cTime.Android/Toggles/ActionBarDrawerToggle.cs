using Android.App;
using Android.Support.V4.Widget;
using Android.Views;

namespace cTime.Android.Toggles
{
    public class ActionBarDrawerToggle : global::Android.Support.V7.App.ActionBarDrawerToggle
    {
        private readonly Activity _activity;

        public ActionBarDrawerToggle(Activity activity, DrawerLayout drawerLayout, int openedResource,
            int closedResource) : base(activity, drawerLayout, openedResource, closedResource)
        {
            this._activity = activity;
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}