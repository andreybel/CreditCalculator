using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyCreditCalculator.Models
{
    
    public class MyCredit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CreditDate { get; set; }
        public string CreditName { get; set; }
        public double CreditSumm { get; set; }
        public int CreditTerm { get; set; }
        public double CreditPercent { get; set; }
        public string PaymentsType { get; set; }
        public double TotalSumm { get; set; }
        public double OverPayment { get; set; }
    }
}
