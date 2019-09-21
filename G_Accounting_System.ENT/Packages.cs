using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Packages
    {
        public int Package_id { get; set; }
        public int PackageDetail_id { get; set; }
        public int SalesOrder_id { get; set; }
        public string Package_No { get; set; }
        public int Item_id { get; set; }
        public string Item_Name { get; set; }
        public string unitprice { get; set; }
        public string price { get; set; }
        public string Qty { get; set; }
        public string Packed_Qty { get; set; }
        public string Package_Date { get; set; }
        public string PackageStatus { get; set; }
        public string Item_Status { get; set; }
        public string PackageCost { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pPackageID_Output { get; set; }
    }
}
