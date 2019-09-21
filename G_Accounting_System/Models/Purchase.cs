using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Purchase
    {
        public int id { get; set; }
        public int pdid { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public int UserId { get; set; }
        public int UserName { get; set; }
        public string TempOrderNum { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorLandline { get; set; }
        public string VendorMobile { get; set; }
        public string VendorEmail { get; set; }
        public int Quantity { get; set; }
        public string RecieveStatus { get; set; }
        public string BillStatus { get; set; }
        public string Bill_Stat { get; set; }
        public string Rec_Stat { get; set; }
        public double PriceUnit { get; set; }
        public string MsrmntUnit { get; set; }
        public string ItemQty { get; set; }
        public string TotalItems { get; set; }
        public int TotalPrice { get; set; }
        public string Approved { get; set; }
        public string RecieveDateTime { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Delete_Request_By { get; set; }
        public string Delete_Status { get; set; }
        public int Enable { get; set; }
        public string _Enable { get; set; }
        public string TimeOfDay { get; set; }
        public string DateOfDay { get; set; }
        public string MonthOfDay { get; set; }
        public string YearOfDay { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pPO_Output { get; set; }
    }
}