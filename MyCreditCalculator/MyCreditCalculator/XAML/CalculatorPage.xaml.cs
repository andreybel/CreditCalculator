using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyCreditCalculator.Interfaces;
using MyCreditCalculator.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCreditCalculator.XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorPage : ContentPage
	{
        private List<Payment> payments = new List<Payment>();
        private decimal crSumm;
        private double crPercent;
        private int crTerm;
        private decimal totalCreditSumm;
        private decimal overPaymentCredit;
		public CalculatorPage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// to calculate differential monthly payment
        /// </summary>
        /// <param name="summ"></param>
        /// <param name="term"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        internal List<Payment> DiffMonthlyPayment(DateTime dateTime, decimal summ, int term, double percent)
        {
            DateTime paymentDate; // for save date of payment
            decimal totalPayment; // for save total monthly payment
            decimal mainPayment; // for save main monthly payment to table
            decimal percentPayment; // for save percent monthly payment to table
            List<Payment> diffPayments = new List<Payment>();
            for (int i = 0; i < term; i++)
            {
                double mPercent = percent / 1200; // monthly percent
                totalPayment = summ / term + (summ * (term - i + 1) * (decimal)mPercent / term);
                mainPayment = summ / term;
                percentPayment = totalPayment - mainPayment;
                paymentDate = dateTime.AddMonths(i);
                diffPayments.Add(new Payment(paymentDate, mainPayment, percentPayment, totalPayment));
            }
            return diffPayments;
        }
        /// <summary>
        /// to calculate annutity monthly payment
        /// </summary>
        /// <param name="summ"></param>
        /// <param name="term"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        internal List<Payment> AnnuityMonthlyPayment(DateTime dateTime, decimal summ, int term, double percent)
        {
            DateTime paymentDate; // for save date of payment
            decimal totalPayment; // for save total monthly payment
            decimal mainPayment; // for save main monthly payment to table
            decimal percentPayment; // for save percent monthly payment to table
            decimal summRemainder = summ; // remainder of total credit summ
            List<Payment> annuityPayments = new List<Payment>();
            double mPercent = percent / 1200; // monthly percent
            double k = (mPercent * (Math.Pow((1 + mPercent), term))) / ((Math.Pow((1 + mPercent), term)) - 1);
            totalPayment = (decimal)k * summ;
            percentPayment = summRemainder * (decimal)mPercent; // first payment
            mainPayment = totalPayment - percentPayment; // first payment
            annuityPayments.Add(new Payment(dateTime, mainPayment, percentPayment, totalPayment));
            for (int i = 1; i < term; i++) // other payments
            {
                paymentDate = dateTime.AddMonths(i);
                summRemainder -= mainPayment;
                percentPayment = summRemainder * (decimal)mPercent;
                mainPayment = totalPayment - percentPayment;
                annuityPayments.Add(new Payment(paymentDate, mainPayment, percentPayment, totalPayment));
            }
            return annuityPayments;
        }
        /// <summary>
        /// to calculate a monthly payment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCalculateClicked(object sender, EventArgs e)
        {
            string percentSumPattern = @"^[1-9]\d*\,?\d*"; // to check percent and sum input
            string termPattern = @"^[1-9]\d*$"; // to check term input
            if (SumEditor.Text == null || TermEditor.Text == null || PercentEditor.Text == null)
            {
                monthlyPayment.TextColor = Color.Red;
                monthlyPayment.Text = "Заполните пустые поля!";
            }
            else if (!Regex.IsMatch(SumEditor.Text, percentSumPattern) || !Regex.IsMatch(TermEditor.Text, percentSumPattern)
                || !Regex.IsMatch(TermEditor.Text, termPattern))
            {
                monthlyPayment.TextColor = Color.Red;
                monthlyPayment.Text = "Недопустимые символы!";
                SumEditor.Text = null;
                TermEditor.Text = null;
                PercentEditor.Text = null;
            }
            else
            {
                monthlyPayment.Text = null;
                monthlyPayment.TextColor = Color.Black;
                overPayment.Text = null;
                DateTime dateTime = paymentDate.Date;
                crSumm = Decimal.Parse(SumEditor.Text);
                crTerm = Int32.Parse(TermEditor.Text);
                crPercent = Double.Parse(PercentEditor.Text);
                if (picker.Items[picker.SelectedIndex].Equals("Аннуитетный"))
                {
                    payments = AnnuityMonthlyPayment(dateTime, crSumm, crTerm, crPercent);
                    monthlyPayment.Text = "Ежемесячный платеж: " + payments.First().TotalPayment.ToString("0.00");
                }
                else
                {
                    payments = DiffMonthlyPayment(dateTime, crSumm, crTerm, crPercent);
                    monthlyPayment.Text = "Ежемесячный платеж: " + payments.First().TotalPayment.ToString("0.00") + " ... " +
                        payments.Last().TotalPayment.ToString("0.00"); // if payment type is "дифференцированный"
                                                                                                       // show overpayment of this credit                                                                 
                }
                // calculate totalsumm
                totalCreditSumm = SumPaymentsCalculate(payments);
                // calculate overpayment
                overPaymentCredit = OverPaymentCalculate(dateTime, crSumm, crTerm, crPercent);
                overPayment.Text = "Переплата: " + OverPaymentCalculate(dateTime, crSumm, crTerm, crPercent).ToString("0.00");
                saveButton.IsVisible = true;
                sheduleButton.IsVisible = true;
            }

        }     
        /// <summary>
        /// to get shedule of payments and open it in new Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnScheduleOfPaymentsClicked(object sender, EventArgs e)
        {
            string percentSumPattern = @"^[1-9]\d*\,?\d*"; // to check percent and sum input
            string termPattern = @"^[1-9]\d*$"; // to check term input
            if (SumEditor.Text == null || TermEditor.Text == null || PercentEditor.Text == null)
            {
                monthlyPayment.TextColor = Color.Red;
                monthlyPayment.Text = "Заполните пустые поля!";
            }
            else if (!Regex.IsMatch(SumEditor.Text, percentSumPattern) || !Regex.IsMatch(TermEditor.Text, percentSumPattern)
                || !Regex.IsMatch(TermEditor.Text, termPattern))
            {
                monthlyPayment.TextColor = Color.Red;
                monthlyPayment.Text = "Недопустимые символы!";
                SumEditor.Text = null;
                TermEditor.Text = null;
                PercentEditor.Text = null;
            }
            else
            {
                DateTime dateTime = paymentDate.Date;
                crSumm = Decimal.Parse(SumEditor.Text);
                crTerm = Int32.Parse(TermEditor.Text);
                crPercent = double.Parse(PercentEditor.Text);
                if (picker.Items[picker.SelectedIndex].Equals("Аннуитетный"))
                {
                    payments = AnnuityMonthlyPayment(dateTime, crSumm, crTerm, crPercent);
                }
                else
                {
                    payments = DiffMonthlyPayment(dateTime, crSumm, crTerm, crPercent);
                }
                await Navigation.PushAsync(new PaymentsTablePage(SumPaymentsCalculate(payments), payments, paymentDate.Date), true);
                
            }
        }
        /// <summary>
        /// calculate overpay of this credit
        /// </summary>
        /// <param name="summ"></param>
        /// <param name="term"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        private decimal OverPaymentCalculate(DateTime dateTime, decimal summ, int term, double percent)
        {
            decimal totalSumm = 0;
            decimal overPayment = 0;
            if (picker.Items[picker.SelectedIndex].Equals("Аннуитетный"))
            {
                foreach (var p in AnnuityMonthlyPayment(dateTime, summ, term, percent))
                {
                    totalSumm += p.TotalPayment;
                }
                overPayment = totalSumm - summ;
            }
            else
            {
                foreach (var p in DiffMonthlyPayment(dateTime, summ, term, percent))
                {
                    totalSumm += p.TotalPayment;
                }
                overPayment = totalSumm - summ;
            }
            return overPayment;
        }
        /// <summary>
        /// for calculate summ of all payments of credit line
        /// </summary>
        /// <param name="payments"></param>
        /// <returns></returns>
        internal decimal SumPaymentsCalculate(List<Payment> payments)
        {
            decimal total = 0;
            foreach (var p in payments)
            {
                total += p.TotalPayment;
            }
            return total;
        }
        /// <summary>
        /// help for picker with type of payment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHelpClicked(object sender, EventArgs e)
        {
            DisplayAlert("Тип платежа",
                "Аннуитетный - в этом случае ежемесячно списываются равные суммы в течение всего срока кредитования" + "\n" +
                "Диференцированный - в этом случае сумма ежемесячных платежей снижается от месяца к месяцу",
                "Понятно");
        }
        /// <summary>
        /// for opening picker by button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickerOpen(object sender, EventArgs e)
        {
            picker.Focus();
        }

        /// <summary>
        /// redirect to detail page to save a credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveCreditClicked(object sender, EventArgs e)
        {
            CreditNameFrame.IsVisible = true;
        }
        /// <summary>
        /// add new credit to data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddCreditClicked(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(myCreditName.Text))
                {
                    DependencyService.Get<IToastMessage>().ShowMesssage("Введите пожалуйста название кредита");
                }
                else
                {
                    MyCredit myCredit = new MyCredit
                    {
                        CreditName = myCreditName.Text,
                        CreditDate = paymentDate.Date.ToString(),
                        CreditSumm = (double)crSumm,
                        CreditTerm = crTerm,
                        CreditPercent = crPercent,
                        PaymentsType = picker.SelectedItem.ToString(),
                        TotalSumm = (double)totalCreditSumm,
                        OverPayment = (double)overPaymentCredit
                    };
                    await App.Database.SaveItemAsync(myCredit);
                    DependencyService.Get<IToastMessage>().ShowMesssage("Кредит добавлен!");
                    await Navigation.PushModalAsync(new MainPage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", ex.Message, "OK");
            }
            
        }
    }
}