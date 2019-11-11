using System;
using System.Collections.Generic;
using System.Text;

namespace MyCreditCalculator
{
    public class Payment
    {
        public DateTime PaymentDate { get; set; }
        public decimal MainPayment { get; set; }
        public decimal PercentPayment { get; set; }
        public decimal TotalPayment { get; set; }

        public Payment() { }

        public Payment(DateTime paymentDate, decimal mainPayment, decimal percentPayment, decimal totalPayment)
        {
            this.PaymentDate = paymentDate;
            this.MainPayment = mainPayment;
            this.PercentPayment = percentPayment;
            this.TotalPayment = totalPayment;
        }
    }
}
