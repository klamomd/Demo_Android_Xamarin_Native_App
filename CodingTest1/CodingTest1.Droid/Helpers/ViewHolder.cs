using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CodingTest1.Droid.Helpers
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView Username { get; set; }
        public TextView Password { get; set; }
    }
}