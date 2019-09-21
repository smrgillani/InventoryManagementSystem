using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Data;
using G_Accounting_System.DAL.DataTables;

namespace G_Accounting_System.DAL
{
    public class ReportsDAL
    {
        #region INVENTORY
        public List<Reports> Inventory_ProductSalesReport(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Product_Sales_Report", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }

            return fetchPSREntries(cmd);
        }

        public List<Reports> Inventory_InventoryDetailsReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Inventory_Details", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (ItemName == "")
            {
                cmd.Parameters.AddWithValue("@pItemName", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pItemName", ItemName);
            }
            return fetchIDEntries(cmd);
        }

        public List<Reports> Inventory_InventoryValuationSummaryReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Inventory_Valuation_Summary", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (ItemName == "")
            {
                cmd.Parameters.AddWithValue("@pItemName", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pItemName", ItemName);
            }
            return fetchIVSREntries(cmd);
        }

        public List<Reports> Inventory_StockSummaryReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Stock_Summary", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (ItemName == "")
            {
                cmd.Parameters.AddWithValue("@pItemName", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pItemName", ItemName);
            }
            return fetchSSREntries(cmd);
        }

        private List<Reports> fetchPSREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> ProductSalesReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    ProductSalesReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.ItemName = Convert.ToString(dr["ItemName"]);
                        li.SKU = Convert.ToString(dr["SKU"]);
                        li.QuantitySold = Convert.ToInt32(dr["QuantitySold"]);
                        li.TotalSalePrice = Convert.ToSingle(dr["TotalSalePrice"]);
                        ProductSalesReport.Add(li);
                    }
                    ProductSalesReport.TrimExcess();
                }
            }
            return ProductSalesReport;
        }

        private List<Reports> fetchIDEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> InventoryDetails = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    InventoryDetails = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Item_id = Convert.ToInt32(dr["Item_Id"]);
                        li.ItemName = Convert.ToString(dr["ItemName"]);
                        li.SKU = Convert.ToString(dr["Item_Sku"]);
                        li.StockInHand = (dr["StockInHand"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["StockInHand"]);
                        li.CommittedStock = (dr["CommittedStock"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CommittedStock"]);
                        li.AvailableForSale = (dr["AvailableForSale"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["AvailableForSale"]);
                        li.QuantityOrdered = (dr["QuantityOrdered"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityOrdered"]);
                        li.QuantityIn = (dr["QuantityIn"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityIn"]);
                        li.QuantityOut = (dr["QuantityOut"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityOut"]);
                        InventoryDetails.Add(li);
                    }
                    InventoryDetails.TrimExcess();
                }
            }
            return InventoryDetails;
        }

        private List<Reports> fetchIVSREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> InventoryValuationSummary = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    InventoryValuationSummary = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Item_id = Convert.ToInt32(dr["Item_Id"]);
                        li.ItemName = Convert.ToString(dr["ItemName"]);
                        li.SKU = Convert.ToString(dr["SKU"]);
                        li.StockInHand = (dr["StockInHand"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["StockInHand"]);
                        li.InventoryAssetValue = Convert.ToSingle(dr["InventoryAssetValue"]);
                        InventoryValuationSummary.Add(li);
                    }
                    InventoryValuationSummary.TrimExcess();
                }
            }
            return InventoryValuationSummary;
        }

        private List<Reports> fetchSSREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> StockSummaryReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    StockSummaryReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.ItemName = Convert.ToString(dr["Item_Name"]);
                        li.SKU = Convert.ToString(dr["Item_Sku"]);
                        li.QuantityIn = (dr["QuantityIn"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityIn"]);
                        li.QuantityOut = (dr["QuantityOut"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityOut"]);
                        li.ClosingStock = (dr["ClosingStock"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ClosingStock"]);
                        StockSummaryReport.Add(li);
                    }
                    StockSummaryReport.TrimExcess();
                }
            }
            return StockSummaryReport;
        }
        #endregion

        #region SALESORDERS
        public List<Reports> Sales_SalesOrderHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Sales_Order_History", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (Status == "Confirm" || Status == "Closed" || Status == "Draft")
            {
                cmd.Parameters.AddWithValue("@pStatus", Status);
                cmd.Parameters.AddWithValue("@pInvoiceStatus", null);
                cmd.Parameters.AddWithValue("@pShipmentStatus", null);
            }
            else if (Status == "Invoiced")
            {
                cmd.Parameters.AddWithValue("@pStatus", null);
                cmd.Parameters.AddWithValue("@pInvoiceStatus", Status);
            }
            else if (Status == "Shipped")
            {
                cmd.Parameters.AddWithValue("@pStatus", null);
                cmd.Parameters.AddWithValue("@pShipmentStatus", true);
            }
            return fetchSOHREntries(cmd);
        }

        public List<Reports> Sales_OrderFulfillmentByItemReport(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Order_Fulfillment_By_Item", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            return fetchSOFBIREntries(cmd);
        }

        public List<Reports> Sales_InvoiceHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Invoice_History", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (Status == "All")
            {
                cmd.Parameters.AddWithValue("@pInvoice_Status", null);
                cmd.Parameters.AddWithValue("@pOverdue", null);
                cmd.Parameters.AddWithValue("@pPartiallyPaid", null);
            }
            else if (Status == "Overdue")
            {
                cmd.Parameters.AddWithValue("@pOverdue", Status);
                cmd.Parameters.AddWithValue("@pPartiallyPaid", null);
                cmd.Parameters.AddWithValue("@pInvoice_Status", null);
            }
            else if (Status == "Partially Paid")
            {
                cmd.Parameters.AddWithValue("@pPartiallyPaid", Status);
                cmd.Parameters.AddWithValue("@pOverdue", null);
                cmd.Parameters.AddWithValue("@pInvoice_Status", null);
            }
            else if (Status == "Draft" || Status == "Paid")
            {
                cmd.Parameters.AddWithValue("@pInvoice_Status", Status);
            }
            return fetchIHREntries(cmd);
        }

        public List<Reports> Sales_PaymentsReceivedReport(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Payments_Received", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            return fetchPRREntries(cmd);
        }

        public List<Reports> Sales_PackingHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Packing_History", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (Status == "All")
            {
                cmd.Parameters.AddWithValue("@pPackage_Status", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pPackage_Status", Status);
            }
            return fetchPHREntries(cmd);
        }

        public List<Reports> Sales_SalesByCustomerReport(string StartDate, string EndDate, string CustomerName, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Sales_By_Customer", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (CustomerName == "")
            {
                cmd.Parameters.AddWithValue("@pCustomerName", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pCustomerName", CustomerName);
            }
            return fetchSBCREntries(cmd);
        }

        public List<Reports> Sales_SalesByItemReport(string StartDate, string EndDate, string ItemName, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Sales_By_Item", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (ItemName == "")
            {
                cmd.Parameters.AddWithValue("@pItemName", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pItemName", ItemName);
            }
            return fetchSBIREntries(cmd);
        }

        public List<Reports> Sales_SalesBySalesPersonReport(string StartDate, string EndDate, string UserName, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Sales_By_SalesPerson", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (UserName == "")
            {
                cmd.Parameters.AddWithValue("@pUserName", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pUserName", UserName);
            }
            return fetchSBSPREntries(cmd);
        }

        private List<Reports> fetchSOHREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> SalesOrderHistoryReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    SalesOrderHistoryReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                        li.SaleOrderNo = (dr["SaleOrderNo"] == DBNull.Value) ? null : Convert.ToString(dr["SaleOrderNo"]);
                        li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                        li.Salutation = Convert.ToInt32(dr["Salutation"]);
                        li.CustomerName = (dr["CustomerName"] == DBNull.Value) ? null : Convert.ToString(dr["CustomerName"]);
                        li.SO_Total_Amount = (dr["SO_Total_Amount"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SO_Total_Amount"]);
                        li.SO_Status = (dr["SO_Status"] == DBNull.Value) ? null : Convert.ToString(dr["SO_Status"]);
                        li.Date_Of_Day = (dr["Date_Of_Day"] == DBNull.Value) ? null : Convert.ToString(dr["Date_Of_Day"]);
                        SalesOrderHistoryReport.Add(li);
                    }
                    SalesOrderHistoryReport.TrimExcess();
                }
            }
            return SalesOrderHistoryReport;
        }

        private List<Reports> fetchSOFBIREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> OrderFulfillmentByItemReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    OrderFulfillmentByItemReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.ItemName = (dr["Item_Name"] == DBNull.Value) ? null : Convert.ToString(dr["Item_Name"]);
                        li.SKU = Convert.ToString(dr["Item_Sku"]);
                        li.Ordered = (dr["Ordered"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Ordered"]);
                        li.DropShipped = (dr["DropShipped"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DropShipped"]);
                        li.Fulfilled = (dr["Fulfilled"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Fulfilled"]);
                        li.ToBePacked = (dr["ToBePacked"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ToBePacked"]);
                        li.ToBeShipped = (dr["ToBeShipped"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ToBeShipped"]);
                        li.ToBeDelivered = (dr["ToBeDelivered"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ToBeDelivered"]);
                        OrderFulfillmentByItemReport.Add(li);
                    }
                    OrderFulfillmentByItemReport.TrimExcess();
                }
            }
            return OrderFulfillmentByItemReport;
        }

        private List<Reports> fetchIHREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> InvoiceHistoryReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    InvoiceHistoryReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Invoice_Status = (dr["Invoice_Status"] == DBNull.Value) ? null : Convert.ToString(dr["Invoice_Status"]);
                        li.InvoiceDateTime = (dr["InvoiceDateTime"] == DBNull.Value) ? null : Convert.ToString(dr["InvoiceDateTime"]);
                        li.InvoiceDueDate = (dr["InvoiceDueDate"] == DBNull.Value) ? null : Convert.ToString(dr["InvoiceDueDate"]);
                        li.Invoice_No = (dr["Invoice_No"] == DBNull.Value) ? null : Convert.ToString(dr["Invoice_No"]);
                        li.SalesOrderNo = (dr["SalesOrderNo"] == DBNull.Value) ? null : Convert.ToString(dr["SalesOrderNo"]);
                        li.Customer_id = (dr["Customer_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Customer_id"]);
                        li.CustomerName = (dr["CustomerName"] == DBNull.Value) ? null : Convert.ToString(dr["CustomerName"]);
                        li.Invoice_Amount = (dr["Invoice_Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Invoice_Amount"]);
                        li.Balance_Amount = (dr["Balance_Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Balance_Amount"]);
                        InvoiceHistoryReport.Add(li);
                    }
                    InvoiceHistoryReport.TrimExcess();
                }
            }
            return InvoiceHistoryReport;
        }

        private List<Reports> fetchPRREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> PaymentsReceivedReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    PaymentsReceivedReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.PaymentNo = (dr["PaymentNo"] == DBNull.Value) ? null : Convert.ToString(dr["PaymentNo"]);
                        li.SO_Payment_Date = (dr["SO_Payment_Date"] == DBNull.Value) ? null : Convert.ToString(dr["SO_Payment_Date"]);
                        li.Invoice_No = (dr["Invoice_No"] == DBNull.Value) ? null : Convert.ToString(dr["Invoice_No"]);
                        li.CustomerName = (dr["CustomerName"] == DBNull.Value) ? null : Convert.ToString(dr["CustomerName"]);
                        li.Invoice_Amount = (dr["Invoice_Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Invoice_Amount"]);
                        li.Payment_Mode = (dr["Payment_Mode"] == DBNull.Value) ? null : Convert.ToString(dr["Payment_Mode"]);
                        PaymentsReceivedReport.Add(li);
                    }
                    PaymentsReceivedReport.TrimExcess();
                }
            }
            return PaymentsReceivedReport;
        }

        private List<Reports> fetchPHREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> PackingHistoryReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    PackingHistoryReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Date_Of_Day = (dr["Date_Of_Day"] == DBNull.Value) ? null : Convert.ToString(dr["Date_Of_Day"]);
                        li.Package_No = (dr["Package_No"] == DBNull.Value) ? null : Convert.ToString(dr["Package_No"]);
                        li.SaleOrderNo = (dr["SaleOrderNo"] == DBNull.Value) ? null : Convert.ToString(dr["SaleOrderNo"]);
                        li.Package_Status = (dr["Package_Status"] == DBNull.Value) ? null : Convert.ToString(dr["Package_Status"]);
                        li.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Quantity"]);
                        PackingHistoryReport.Add(li);
                    }
                    PackingHistoryReport.TrimExcess();
                }
            }
            return PackingHistoryReport;
        }

        private List<Reports> fetchSBCREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> SalesByCustomerReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    SalesByCustomerReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                        li.CustomerName = (dr["CustomerName"] == DBNull.Value) ? null : Convert.ToString(dr["CustomerName"]);
                        li.InvoiceCount = (dr["InvoiceCount"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["InvoiceCount"]);
                        li.Sales = (dr["Sales"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Sales"]);
                        SalesByCustomerReport.Add(li);
                    }
                    SalesByCustomerReport.TrimExcess();
                }
            }
            return SalesByCustomerReport;
        }

        private List<Reports> fetchSBIREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> SalesByItemReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    SalesByItemReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.ItemName = (dr["ItemName"] == DBNull.Value) ? null : Convert.ToString(dr["ItemName"]);
                        li.QuantitySold = (dr["QuantitySold"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantitySold"]);
                        li.Amount = (dr["Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Amount"]);
                        li.AveragePrice = (dr["AveragePrice"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["AveragePrice"]);
                        SalesByItemReport.Add(li);
                    }
                    SalesByItemReport.TrimExcess();
                }
            }
            return SalesByItemReport;
        }

        private List<Reports> fetchSBSPREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> SalesBySalesPersonReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    SalesBySalesPersonReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.AddedBy = Convert.ToInt32(dr["AddedBy"]);
                        li.AddedByName = (dr["AddedByName"] == DBNull.Value) ? null : Convert.ToString(dr["AddedByName"]);
                        li.InvoiceCount = (dr["InvoiceCount"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["InvoiceCount"]);
                        li.Sales = (dr["Sales"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Sales"]);
                        SalesBySalesPersonReport.Add(li);
                    }
                    SalesBySalesPersonReport.TrimExcess();
                }
            }
            return SalesBySalesPersonReport;
        }
        #endregion

        #region PURCHASES
        public List<Reports> Purchases_PurchaseOrderHistoryReport(string StartDate, string EndDate, string Status, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Purchase_Order_History", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            if (Status == "All")
            {
                cmd.Parameters.AddWithValue("@pRecieveStatus", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pRecieveStatus", Status);
            }
            return fetchPOHREntries(cmd);
        }

        public List<Reports> Purchases_PurchaseByVendorReport(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Purchase_By_Vendor", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            return fetchPBVREntries(cmd);
        }

        public List<Reports> Purchases_PurchaseByItemReport(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Purchase_By_Item", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            return fetchPBIREntries(cmd);
        }

        public List<Reports> Purchases_BillDetailsReport(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Bill_Details", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            return fetchBDREntries(cmd);
        }





        private List<Reports> fetchPOHREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> PurchaseOrderHistoryReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    PurchaseOrderHistoryReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Date_Of_Day = (dr["Date_Of_Day"] == DBNull.Value) ? null : Convert.ToString(dr["Date_Of_Day"]);
                        li.PurchaseOrderNo = (dr["PurchaseOrderNo"] == DBNull.Value) ? null : Convert.ToString(dr["PurchaseOrderNo"]);
                        li.Vendor_id = Convert.ToInt32(dr["Vendor_id"]);
                        li.VendorName = (dr["VendorName"] == DBNull.Value) ? null : Convert.ToString(dr["VendorName"]);
                        li.PurchaseOrderStatus = (dr["RecieveStatus"] == DBNull.Value) ? null : Convert.ToString(dr["RecieveStatus"]);
                        li.QuantityOrdered = (dr["QuantityOrdered"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityOrdered"]);
                        li.QuantityReceived = (dr["QuantityReceived"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityReceived"]);
                        li.Amount = (dr["Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Amount"]);
                        PurchaseOrderHistoryReport.Add(li);
                    }
                    PurchaseOrderHistoryReport.TrimExcess();
                }
            }
            return PurchaseOrderHistoryReport;
        }

        private List<Reports> fetchPBVREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> PurchaseByVendorReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    PurchaseByVendorReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Vendor_id = Convert.ToInt32(dr["VendorId"]);
                        li.VendorName = (dr["VendorName"] == DBNull.Value) ? null : Convert.ToString(dr["VendorName"]);
                        li.QuantityOrdered = (dr["QuantityOrdered"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityOrdered"]);
                        li.Amount = (dr["Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Amount"]);
                        PurchaseByVendorReport.Add(li);
                    }
                    PurchaseByVendorReport.TrimExcess();
                }
            }
            return PurchaseByVendorReport;
        }

        private List<Reports> fetchPBIREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> PurchaseByItemReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    PurchaseByItemReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.ItemName = (dr["ItemName"] == DBNull.Value) ? null : Convert.ToString(dr["ItemName"]);
                        li.QuantityPurchased = (dr["QuantityPurchased"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QuantityPurchased"]);
                        li.Amount = (dr["Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Amount"]);
                        li.AveragePrice = (dr["AveragePrice"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["AveragePrice"]);
                        PurchaseByItemReport.Add(li);
                    }
                    PurchaseByItemReport.TrimExcess();
                }
            }
            return PurchaseByItemReport;
        }

        private List<Reports> fetchBDREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> BillDetailsReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    BillDetailsReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Bill_Status = (dr["Bill_Status"] == DBNull.Value) ? null : Convert.ToString(dr["Bill_Status"]);
                        li.BillDateTime = (dr["BillDateTime"] == DBNull.Value) ? null : Convert.ToString(dr["BillDateTime"]);
                        li.BillDueDate = (dr["BillDueDate"] == DBNull.Value) ? null : Convert.ToString(dr["BillDueDate"]);
                        li.Bill_No = (dr["Bill_No"] == DBNull.Value) ? null : Convert.ToString(dr["Bill_No"]);
                        li.Vendor_id = Convert.ToInt32(dr["Vendor_id"]);
                        li.VendorName = (dr["VendorName"] == DBNull.Value) ? null : Convert.ToString(dr["VendorName"]);
                        li.Bill_Amount = (dr["Bill_Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Bill_Amount"]);
                        li.Balance_Amount = (dr["Balance_Amount"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["Balance_Amount"]);
                        BillDetailsReport.Add(li);
                    }
                    BillDetailsReport.TrimExcess();
                }
            }
            return BillDetailsReport;
        }

        #endregion

        public List<Reports> ActivityLogsReport(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Reports_Activity_Logs", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (StartDate == "")
            {
                cmd.Parameters.AddWithValue("@pFrom", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", StartDate);
            }
            if (EndDate == "")
            {
                cmd.Parameters.AddWithValue("@pTo", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pTo", EndDate);
            }
            return fetchALREntries(cmd);
        }

        private List<Reports> fetchALREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Reports> ActivityLogsReport = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    ActivityLogsReport = new List<Reports>();
                    while (dr.Read())
                    {
                        Reports li = new Reports();
                        li.Date = (dr["Date"] == DBNull.Value) ? null : Convert.ToString(dr["Date"]);
                        li.Time = (dr["Time"] == DBNull.Value) ? null : Convert.ToString(dr["Time"]);
                        li.ActivityType = (dr["ActivityType"] == DBNull.Value) ? null : Convert.ToString(dr["ActivityType"]);
                        li.ActivityName = (dr["ActivityName"] == DBNull.Value) ? null : Convert.ToString(dr["ActivityName"]);
                        li.Description = (dr["Description"] == DBNull.Value) ? null : Convert.ToString(dr["Description"]);
                        ActivityLogsReport.Add(li);
                    }
                    ActivityLogsReport.TrimExcess();
                }
            }
            return ActivityLogsReport;
        }
    }
}
