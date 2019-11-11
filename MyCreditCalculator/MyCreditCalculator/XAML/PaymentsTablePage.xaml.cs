using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCreditCalculator.XAML
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentsTablePage : ContentPage
    {
        decimal SumPayments { get; set; }
        public List<Payment> Payments { get; set; }
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Add to payments table data from CalculatorPage
        /// </summary>
        /// <param name="_payments"></param>
        /// <param name="date"></param>
        public PaymentsTablePage(decimal sumPayments, List<Payment> _payments, DateTime date)
        {
            InitializeComponent();
            SumPayments = sumPayments;
            Payments = _payments;
            StartDate = date;
            this.BindingContext = Payments;
            // header of payment table
            FlexLayout tableHeader = new FlexLayout
            {
                Direction = FlexDirection.Row,
                JustifyContent = FlexJustify.SpaceBetween,
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.DarkSeaGreen
            };
            tableHeader.Children.Add(new Label { Text = "Дата платежа" });
            tableHeader.Children.Add(new Label { Text = "Осн.долг"});
            tableHeader.Children.Add(new Label { Text = "Проценты"});
            tableHeader.Children.Add(new Label { Text = "Платеж"});
            // total payment ant footer of page
            FlexLayout sumPayment = new FlexLayout
            {
                Direction = FlexDirection.Row,
                JustifyContent = FlexJustify.SpaceBetween,
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.FromHex("#ffc04c")
            };
            sumPayment.Children.Add(new Label {Text = "ИТОГО" , FontAttributes = FontAttributes.Bold });
            sumPayment.Children.Add(new Label {Text = sumPayments.ToString("0.00"), FontAttributes = FontAttributes.Bold });
            // payments table
            ListView paymentsTable = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = Payments,

                ItemTemplate = new DataTemplate(() => 
                {
                    Label dateLabel = new Label();
                    dateLabel.SetBinding(Label.TextProperty, "PaymentDate", BindingMode.Default, null, "{0:dd.MM.yyyy}");

                    Label mainLabel = new Label();
                    mainLabel.SetBinding(Label.TextProperty, "MainPayment", BindingMode.Default, null, "{0:0.00}");

                    Label percentLabel = new Label();
                    percentLabel.SetBinding(Label.TextProperty, "PercentPayment",BindingMode.Default, null, "{0:0.00}");

                    Label totalLabel = new Label();
                    totalLabel.SetBinding(Label.TextProperty, "TotalPayment", BindingMode.Default, null, "{0:0.00}");

                    return new ViewCell
                    {
                        View = new FlexLayout
                        {
                            Direction = FlexDirection.Row,
                            JustifyContent = FlexJustify.SpaceBetween,
                            Padding = new Thickness(10,10,10,10),
                            Children = { dateLabel, mainLabel, percentLabel, totalLabel }
                        }
                        
                    };
                })
            };
            this.Content = new StackLayout { Children = {tableHeader, paymentsTable, sumPayment} };
        }

    }
}