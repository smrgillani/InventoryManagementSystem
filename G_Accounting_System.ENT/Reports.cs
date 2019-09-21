using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Reports
    {
        public int Item_id { get; set; }
        public string ItemName { get; set; }
        public string SKU { get; set; }
        public string Date_Of_Day { get; set; }

        #region Inventory_ProductSalesReport
        public float Margin { get; set; }
        public int QuantitySold { get; set; }
        public float TotalSalePrice { get; set; }
        public float SalePriceWithTax { get; set; }
        #endregion

        #region Inventory_InventoryDetails
        public int StockInHand { get; set; }
        public int CommittedStock { get; set; }
        public int AvailableForSale { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }
        #endregion

        #region Inventory_InventoryValuationSummary
        public float InventoryAssetValue { get; set; }
        #endregion

        #region Inventory_StockSummary
        public float ClosingStock { get; set; }
        #endregion

        #region Sales_SalesOrderHistory
        public int SalesOrder_id { get; set; }
        public string SaleOrderNo { get; set; }
        public int Customer_id { get; set; }
        public int Salutation { get; set; }
        public string CustomerName { get; set; }
        public float SO_Total_Amount { get; set; }
        public string SO_Status { get; set; }
        #endregion

        #region Sales_OrderFulfillmentByItem
        public int Ordered { get; set; }
        public int DropShipped { get; set; }
        public int Fulfilled { get; set; }
        public int ToBePacked { get; set; }
        public int ToBeShipped { get; set; }
        public int ToBeDelivered { get; set; }
        #endregion

        #region Sales_Invoice_History
        public string Invoice_Status { get; set; }
        public string InvoiceDateTime { get; set; }
        public string InvoiceDueDate { get; set; }
        public string Invoice_No { get; set; }
        public string SalesOrderNo { get; set; }
        public float Invoice_Amount { get; set; }
        public float Balance_Amount { get; set; }
        #endregion

        #region Sales_Payments_Received
        public string PaymentNo { get; set; }
        public string SO_Payment_Date { get; set; }
        public string Payment_Mode { get; set; }
        #endregion  

        #region Sales_Packing_History
        public string Package_No { get; set; }
        public string Package_Status { get; set; }
        public int Quantity { get; set; }
        #endregion  

        #region Sales_SalesByCutomer
        public int InvoiceCount { get; set; }
        public float Sales { get; set; }
        #endregion

        #region Sales_SalesByItem
        public float Amount { get; set; }
        public float AveragePrice { get; set; }
        #endregion

        #region Sales_SalesBySalesPerson
        public int AddedBy { get; set; }
        public string AddedByName { get; set; }
        #endregion

        #region Purchases_PurchaseOrderHistory
        public string PurchaseOrderNo { get; set; }
        public int Vendor_id { get; set; }
        public string VendorName { get; set; }
        public string PurchaseOrderStatus { get; set; }
        public int QuantityReceived { get; set; }
        #endregion

        #region Purchases_PurchaseByItem
        public int QuantityPurchased { get; set; }
        #endregion

        #region Purchases_BillDetails
        public string Bill_Status { get; set; }
        public string BillDateTime { get; set; }
        public string BillDueDate { get; set; }
        public string Bill_No { get; set; }
        public float Bill_Amount { get; set; }
        #endregion

        #region ActivityLogs
        public string DateTime { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ActivityType { get; set; }
        public string ActivityName { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
