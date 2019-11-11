using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using MyCreditCalculator;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XMyCreditCalculator.Droid;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace XMyCreditCalculator.Droid
{
    public class MyEntryRenderer: EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                // set background
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);

            }

        }
    }
}