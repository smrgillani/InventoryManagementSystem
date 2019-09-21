using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class SO_Invoice
    {
        public int id { get; set; }
        public int SalesOrder_id { get; set; }
        public int Customer_id { get; set; }
        public string Customer_Name { get; set; }
        public string Order_No { get; set; }
        public string Invoice_No { get; set; }
        public double PriceUnit { get; set; }
        public string ItemQty { get; set; }
        public string Invoice_Status { get; set; }
        public decimal Invoice_Amount { get; set; }
        public decimal Amount_Paid { get; set; }
        public decimal Balance_Amount { get; set; }
        public string InvoiceDateTime { get; set; }
        public string InvoiceDueDate { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pInvoice_id_Output { get; set; }
    }
}