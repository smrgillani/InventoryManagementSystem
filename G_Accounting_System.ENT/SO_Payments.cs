using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class SO_Payments
    {
        public int SO_Payment_id { get; set; }
        public int Invoice_id { get; set; }
        public string Invoice_No { get; set; }
        public int Payment_Mode { get; set; }
        public string PaymentMode { get; set; }
        public string Payment_Date { get; set; }
        public decimal Total_Amount { get; set; }
        public decimal Paid_Amount { get; set; }
        public decimal Balance_Amount { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
    }
}
