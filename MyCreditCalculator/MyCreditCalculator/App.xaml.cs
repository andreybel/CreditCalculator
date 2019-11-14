using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MyCreditCalculator.Services;
using System.IO;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyCreditCalculator
{
    public partial class App : Application
    {
        public static CreditRepository database;
        public static CreditRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new CreditRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
                }
                return database;
            }
        }
        public int ResumeAtTodoId { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=b6720286-51d7-4077-86b6-28afcf3f8090;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()   
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
