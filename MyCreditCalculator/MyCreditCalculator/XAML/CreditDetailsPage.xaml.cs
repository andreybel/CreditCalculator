using MyCreditCalculator.Models;
using System;
using System.Collections.Generic;
using Realms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Globalization;

namespace MyCreditCalculator.XAML
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreditDetailsPage : ContentPage
	{
        string _CreditName { get; set; }
        string _CreditDate { get; set; }
        decimal _CreditSumm { get; set; }
        int _CreditTerm { get; set; }
        double _CreditPercent { get; set; }
        string PaymentType { get; set; }
        double TotalSumm { get; set; }
        double OverPayment { get; set; }
        public CreditDetailsPage()
        {
            InitializeComponent();
        }
        public CreditDetailsPage(string creditName, string creditDate, decimal creditSumm, int creditTerm, double creditPercent,
            string paymentsType, double totalSumm, double overPayment)
        {
            this._CreditName = creditName;
            this._CreditDate = creditDate;
            this._CreditSumm = creditSumm;
            this._CreditTerm = creditTerm;
            this._CreditPercent = creditPercent;
            this.PaymentType = paymentsType;
            this.TotalSumm = totalSumm;
            this.OverPayment = overPayment;
            InitializeComponent();
        }
        /// <summary>
        /// Get a shedule of payments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetPaymentsShedule(object sender, EventArgs e)
        {
            CalculatorPage calculator = new CalculatorPage();

            List<Payment> payments = new List<Payment>();

            DateTime dateTime = Convert.ToDateTime(cDate.Text);
            var summ = Decimal.Parse(cSumm.Text);
            var term = Int32.Parse(cTerm.Text);
            var percent = Double.Parse(cPercent.Text, CultureInfo.InvariantCulture);
            if (cType.Text.Equals("Аннуитетный"))
            {
                payments = calculator.AnnuityMonthlyPayment(dateTime, summ, term, percent);
            }
            else
            {
                payments = calculator.DiffMonthlyPayment(dateTime, summ,term,percent);
            }
            await Navigation.PushAsync(new PaymentsTablePage(calculator.SumPaymentsCalculate(payments), payments, dateTime), true);
        }
    }
}