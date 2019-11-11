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
using MyCreditCalculator;
using MyCreditCalculator.CustomControls;
using MyCreditCalculator.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(MyEditor), typeof(MyEditorRenderer))]
namespace MyCreditCalculator.Droid.CustomRenderers
{
    public class MyEditorRenderer: EditorRenderer
    {
        public MyEditorRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                // set background
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                // settext size
                Control.SetTextSize( Android.Util.ComplexUnitType.Pt,8);
            }

        }
    }
}