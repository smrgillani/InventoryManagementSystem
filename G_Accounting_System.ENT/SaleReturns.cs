using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class SaleReturns
    {
        public int SaleReturn_id { get; set; }
        public int SaleOrder_id { get; set; }
        public string SaleReturnNo { get; set; }
        public int Package_id { get; set; }
        public int Item_id { get; set; }
        public string Item_Name { get; set; }
        public string PriceUnit { get; set; }
        public string MsrmntUnit { get; set; }
        public string ItemQty { get; set; }
        public string Return_Qty { get; set; }
        public string Received_Qty { get; set; }
        public string To_Received_Qty { get; set; }
        public string ReturnQty_Cost { get; set; }
        public string SaleReturn_Date { get; set; }
        public string SaleReturn_Status { get; set; }
        public string TotalReturn_Cost { get; set; }
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
