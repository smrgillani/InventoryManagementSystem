using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Stocks
    {
        public int Stock_id { get; set; }
        public int Item_id { get; set; }
        public string Item_Name { get; set; }
        public string Physical_Quantity { get; set; }
        public string Physical_Avail_ForSale { get; set; }
        public string Physical_Committed { get; set; }
        public string Accounting_Quantity { get; set; }
        public string Acc_Avail_ForSale { get; set; }
        public string Acc_Commited { get; set; }
        public string OpeningStock { get; set; }
        public string ReorderLevel { get; set; }
        public string Quantity_Sold { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
