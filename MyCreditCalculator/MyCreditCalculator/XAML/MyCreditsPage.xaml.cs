using MyCreditCalculator.Interfaces;
using MyCreditCalculator.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCreditCalculator.XAML
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyCreditsPage : ContentPage
	{
        public MyCreditsPage ()
		{
			InitializeComponent ();
		}
        // show all credits
        protected override async void OnAppearing()
        {
            ((App)App.Current).ResumeAtTodoId = -1;
            creditsList.ItemsSource = await App.Database.GetItemsAsync();
            base.OnAppearing();
        }
        /// <summary>
        /// to open details of selected credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnCreditSelected(object sender, ItemTappedEventArgs e)
        {
            MyCredit selectedCredit = (MyCredit)e.Item;
            CreditDetailsPage detailsPage = new CreditDetailsPage(selectedCredit.CreditName, selectedCredit.CreditDate,  (decimal)selectedCredit.CreditSumm,
                selectedCredit.CreditTerm,selectedCredit.CreditPercent, 
                selectedCredit.PaymentsType, selectedCredit.TotalSumm, selectedCredit.OverPayment);
            detailsPage.BindingContext = selectedCredit;
            await Navigation.PushAsync(detailsPage);
        }
        /// <summary>
        /// to remove selected credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDeleteCredit(object sender, EventArgs e)
        {
            //MyCredit selectedCredit = (MyCredit)e.SelectedItem;
            bool result = await DisplayAlert("","Точно удалить?","Да","Нет");
            if (result)
            {
                var removeCredit = (sender as BindableObject).BindingContext as MyCredit;
                await App.database.DeleteItemAsync(removeCredit);
                DependencyService.Get<IToastMessage>().ShowMesssage("Кредит удален");
                creditsList.ItemsSource = await App.database.GetItemsAsync();
            }
        }
    }
}