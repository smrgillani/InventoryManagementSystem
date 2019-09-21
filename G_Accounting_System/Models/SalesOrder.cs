using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class SalesOrder
    {
        public int SalesOrder_id { get; set; }
        public int sdid { get; set; }
        public string SaleOrderNo { get; set; }
        public int Customer_id { get; set; }
        public int SO_Invoice_id { get; set; }
        public int Package_id { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_Landline { get; set; }
        public string Customer_Mobile { get; set; }
        public string Customer_Email { get; set; }
        public int PremisesId { get; set; }
        public string Premises_Name { get; set; }
        public int UserId { get; set; }
        public string User_Name { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string TotalItems { get; set; }
        public int Quantity { get; set; }
        public double PriceUnit { get; set; }
        public string MsrmntUnit { get; set; }
        public string ItemQty { get; set; }
        public string SO_Total_Amount { get; set; }
        public string SO_Shipping_Charges { get; set; }
        public string SO_Status { get; set; }
        public string SO_Invoice_Status { get; set; }
        public string SO_Shipment_Status { get; set; }
        public string SO_Package_Status { get; set; }
        public string SO_DateTime { get; set; }
        public string SO_Expected_Shipment_Date { get; set; }
        public string Time_Of_Day { get; set; }
        public string Date_Of_Day { get; set; }
        public string Month_Of_Day { get; set; }
        public string Year_Of_Day { get; set; }
        public string Packed_Qty { get; set; }
        public string InvoicedQty { get; set; }
        public string PackageCost { get; set; }
        public string ReturnQty { get; set; }
        public string Received_Qty { get; set; }
        public string To_Received_Qty { get; set; }
        public int Delete_Request_By { get; set; }
        public string Delete_Status { get; set; }
        public int Enable { get; set; }
        public string IsEnabled_ { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pSO_Output { get; set; }
    }
}