
using Android.App;
using Android.Widget;
using MyCreditCalculator.Droid;
using MyCreditCalculator.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessageDroid))]
namespace MyCreditCalculator.Droid
{
    public class ToastMessageDroid : IToastMessage
    {
        public void ShowMesssage(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}