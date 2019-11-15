using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCreditCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            masterPage.listView.ItemSelected += OnItemSelected;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                if (item.Title.Equals("Выход"))
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                else
                {
                    await Task.Factory.StartNew(() =>
                    {
                        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    });
                    IsPresented = false;
                    masterPage.listView.SelectedItem = null;
                }
                   
            }
        }

    }
}
