using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Purchases
    {
        public int id { get; set; }
        public int pdid { get; set; }
        public int PremisesId { get; set; }
        public string PremisesName { get; set; }
        public Premisess Premises_Name { get; set; }
        public int UserId { get; set; }
        public int AUserId { get; set; }
        public int UserName { get; set; }
        public Users User_Name { get; set; }
        public Users AUser_Name { get; set; }
        public string TempOrderNum { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Items Item_Name { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public Contacts Vendor_Name { get; set; }
        public Contacts Vendor_Address { get; set; }
        public Contacts Vendor_Landline { get; set; }
        public Contacts Vendor_Mobile { get; set; }
        public Contacts Vendor_Email { get; set; }
        public Contacts AVendor_Name { get; set; }
        public Contacts ABVendor_Name { get; set; }
        public string RecieveStatus { get; set; }
        public string BillStatus { get; set; }
        public string Bill_Stat { get; set; }
        public string Rec_Stat { get; set; }
        public int Quantity { get; set; }
        public double PriceUnit { get; set; }
        public string MsrmntUnit { get; set; }
        public string Unit { get; set; }
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
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pPO_Output { get; set; }
    }
}
