using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;

namespace cTime.Droid.Adapter
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private readonly List<Fragment> _fragments = new List<Fragment>();
        private readonly List<string> _fragmentTitles = new List<string>();

        public ViewPagerAdapter(FragmentManager fm) : base(fm)
        {
        }

        public override int Count => this._fragments.Count;

        public void AddFragment(Fragment fragment, string title)
        {
            this._fragments.Add(fragment);
            this._fragmentTitles.Add(title);
        }

        public override Fragment GetItem(int position)
        {
            return this._fragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new String(this._fragmentTitles[position]);
        }
    }
}