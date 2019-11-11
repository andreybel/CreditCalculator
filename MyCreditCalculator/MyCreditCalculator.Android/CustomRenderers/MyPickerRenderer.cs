using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyCreditCalculator.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(MyCreditCalculator.MyPicker), typeof(MyPickerRenderer))]
namespace MyCreditCalculator.Droid
{
    public class MyPickerRenderer: PickerRenderer
    {
        public MyPickerRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
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
