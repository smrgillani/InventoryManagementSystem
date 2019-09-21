using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G_Accounting_System.ENT;

namespace G_Accounting_System.Models
{
    public class Bill
    {
        public int id { get; set; }
        public int Purchase_id { get; set; }
        public int Vendor_id { get; set; }
        public string Vendor_Name { get; set; }
        public string Order_No { get; set; }
        public string Bill_No { get; set; }
        public string Bill_Status { get; set; }
        public decimal Bill_Amount { get; set; }
        public decimal Amount_Paid { get; set; }
        public decimal Balance_Amount { get; set; }
        public string BillDateTime { get; set; }
        public string BillDueDate { get; set; }
        public double PriceUnit { get; set; }
        public string ItemQty { get; set; }
        public int AddedBy { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pBill_id_Output { get; set; }
       
    }
}