using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace G_Accounting_System.Controllers
{
    public class APIReportsController : ApiController
    {
        #region INVENTORY
        [Route("api/APIReports/Inventory_ProductSalesReport")]
        [HttpPost]
        public IEnumerable<Report> Inventory_ProductSalesReport()
        {
            List<Report> productsalesreport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> productsalesreports = new Catalog().Inventory_ProductSalesReport(search.StartDate, search.EndDate, search.Search);
                productsalesreport = new List<Report>();
                if (productsalesreports != null)
                {
                    foreach (var dbr in productsalesreports)
                    {
                        Report li = new Report();
                        li.Item_id = dbr.Item_id;
                        li.ItemName = dbr.ItemName;
                        li.SKU = dbr.SKU;
                        li.QuantitySold = dbr.QuantitySold;
                        li.TotalSalePrice = dbr.TotalSalePrice;
                        productsalesreport.Add(li);
                    }
                }

                productsalesreport.TrimExcess();

                return productsalesreport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Inventory_InventoryDetailsReport")]
        [HttpPost]
        public IEnumerable<Report> Inventory_InventoryDetailsReport()
        {
            List<Report> inventoryDetailsReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> inventoryDetailsReports = new Catalog().Inventory_InventoryDetailsReport(search.StartDate, search.EndDate, search.ItemName, search.Search);
                inventoryDetailsReport = new List<Report>();

                if (inventoryDetailsReports != null)
                {
                    foreach (var dbr in inventoryDetailsReports)
                    {
                        Report li = new Report();
                        li.Item_id = dbr.Item_id;
                        li.ItemName = dbr.ItemName;
                        li.SKU = dbr.SKU;
                        li.StockInHand = dbr.StockInHand;
                        li.CommittedStock = dbr.CommittedStock;
                        li.AvailableForSale = dbr.AvailableForSale;
                        li.QuantityOrdered = dbr.QuantityOrdered;
                        li.QuantityIn = dbr.QuantityIn;
                        li.QuantityOut = dbr.QuantityOut;
                        inventoryDetailsReport.Add(li);
                    }
                }

                inventoryDetailsReport.TrimExcess();

                return inventoryDetailsReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Inventory_InventoryValuationSummaryReport")]
        [HttpPost]
        public IEnumerable<Report> Inventory_InventoryValuationSummaryReport()
        {
            List<Report> inventoryValuationSummaryReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> inventoryValuationSummaryReports = new Catalog().Inventory_InventoryValuationSummaryReport(search.StartDate, search.EndDate, search.ItemName, search.Search);
                inventoryValuationSummaryReport = new List<Report>();

                if (inventoryValuationSummaryReports != null)
                {
                    foreach (var dbr in inventoryValuationSummaryReports)
                    {
                        Report li = new Report();
                        li.Item_id = dbr.Item_id;
                        li.ItemName = dbr.ItemName;
                        li.SKU = dbr.SKU;
                        li.StockInHand = dbr.StockInHand;
                        li.InventoryAssetValue = dbr.InventoryAssetValue;
                        inventoryValuationSummaryReport.Add(li);
                    }
                }

                inventoryValuationSummaryReport.TrimExcess();

                return inventoryValuationSummaryReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Inventory_StockSummaryReport")]
        [HttpPost]
        public IEnumerable<Report> Inventory_StockSummaryReport()
        {
            List<Report> stockSummaryReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> stockSummaryReports = new Catalog().Inventory_StockSummaryReport(search.StartDate, search.EndDate, search.ItemName, search.Search);
                stockSummaryReport = new List<Report>();

                if (stockSummaryReports != null)
                {
                    foreach (var dbr in stockSummaryReports)
                    {
                        Report li = new Report();
                        li.Item_id = dbr.Item_id;
                        li.ItemName = dbr.ItemName;
                        li.SKU = dbr.SKU;
                        li.QuantityIn = dbr.QuantityIn;
                        li.QuantityOut = dbr.QuantityOut;
                        li.ClosingStock = dbr.ClosingStock;
                        stockSummaryReport.Add(li);
                    }
                }

                stockSummaryReport.TrimExcess();

                return stockSummaryReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }
        #endregion

        #region SalesOrder
        [Route("api/APIReports/Sales_SalesOrderHistoryReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_SalesOrderHistoryReport()
        {
            List<Report> salesOrderHistoryReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> salesOrderHistoryReports = new Catalog().Sales_SalesOrderHistoryReport(search.StartDate, search.EndDate, search.Status, search.Search);
                salesOrderHistoryReport = new List<Report>();

                if (salesOrderHistoryReports != null)
                {
                    foreach (var dbr in salesOrderHistoryReports)
                    {
                        Report li = new Report();
                        li.SalesOrder_id = dbr.SalesOrder_id;
                        li.SaleOrderNo = dbr.SaleOrderNo;
                        li.Customer_id = dbr.Customer_id;
                        li.Salutation = dbr.Salutation;
                        li.Full_name_ = (dbr.Salutation.Equals("1") ? "Mr." : "") + (dbr.Salutation.Equals("2") ? "Mrs." : "") + (dbr.Salutation.Equals("3") ? "Ms." : "") + (dbr.Salutation.Equals("4") ? "Miss" : "") + (dbr.Salutation.Equals("5") ? "Dr." : "") + " " + dbr.CustomerName;
                        li.CustomerName = dbr.CustomerName;
                        li.SO_Status = dbr.SO_Status;
                        li.SO_Total_Amount = dbr.SO_Total_Amount;
                        li.Date_Of_Day = dbr.Date_Of_Day;
                        salesOrderHistoryReport.Add(li);
                    }
                }

                salesOrderHistoryReport.TrimExcess();

                return salesOrderHistoryReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Sales_OrderFulfillmentByItemReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_OrderFulfillmentByItemReport()
        {
            List<Report> orderFulfillmentByItemReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> orderFulfillmentByItemReports = new Catalog().Sales_OrderFulfillmentByItemReport(search.StartDate, search.EndDate, search.Search);
                orderFulfillmentByItemReport = new List<Report>();

                if (orderFulfillmentByItemReports != null)
                {
                    foreach (var dbr in orderFulfillmentByItemReports)
                    {
                        Report li = new Report();
                        li.ItemName = dbr.ItemName;
                        li.SKU = dbr.SKU;
                        li.Ordered = dbr.Ordered;
                        li.DropShipped = dbr.DropShipped;
                        li.Fulfilled = dbr.Fulfilled;
                        li.ToBePacked = dbr.ToBePacked;
                        li.ToBeShipped = dbr.ToBeShipped;
                        li.ToBeDelivered = dbr.ToBeDelivered;
                        orderFulfillmentByItemReport.Add(li);
                    }
                }

                orderFulfillmentByItemReport.TrimExcess();

                return orderFulfillmentByItemReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Sales_InvoiceHistoryReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_InvoiceHistoryReport()
        {
            List<Report> invoiceHistoryReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> invoiceHistoryReports = new Catalog().Sales_InvoiceHistoryReport(search.StartDate, search.EndDate, search.Status, search.Search);
                invoiceHistoryReport = new List<Report>();

                if (invoiceHistoryReports != null)
                {
                    foreach (var dbr in invoiceHistoryReports)
                    {
                        Report li = new Report();
                        li.Invoice_Status = dbr.Invoice_Status;
                        li.InvoiceDateTime = dbr.InvoiceDateTime;
                        li.InvoiceDueDate = dbr.InvoiceDueDate;
                        li.Invoice_No = dbr.Invoice_No;
                        li.SalesOrderNo = dbr.SalesOrderNo;
                        li.Customer_id = dbr.Customer_id;
                        li.CustomerName = dbr.CustomerName;
                        li.Invoice_Amount = dbr.Invoice_Amount;
                        li.Balance_Amount = dbr.Balance_Amount;
                        invoiceHistoryReport.Add(li);
                    }
                }

                invoiceHistoryReport.TrimExcess();

                return invoiceHistoryReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Sales_PaymentsReceivedReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_PaymentsReceivedReport()
        {
            List<Report> paymentsReceivedReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> paymentsReceivedReports = new Catalog().Sales_PaymentsReceivedReport(search.StartDate, search.EndDate, search.Search);
                paymentsReceivedReport = new List<Report>();

                if (paymentsReceivedReports != null)
                {
                    foreach (var dbr in paymentsReceivedReports)
                    {
                        Report li = new Report();
                        li.PaymentNo = dbr.PaymentNo;
                        li.SO_Payment_Date = dbr.SO_Payment_Date;
                        li.Invoice_No = dbr.Invoice_No;
                        li.CustomerName = dbr.CustomerName;
                        li.Invoice_Amount = dbr.Invoice_Amount;
                        li.Payment_Mode = dbr.Payment_Mode;
                        paymentsReceivedReport.Add(li);
                    }
                }

                paymentsReceivedReport.TrimExcess();

                return paymentsReceivedReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Sales_PackingHistoryReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_PackingHistoryReport()
        {
            List<Report> packingHistoryReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> packingHistoryReports = new Catalog().Sales_PackingHistoryReport(search.StartDate, search.EndDate, search.Status, search.Search);
                packingHistoryReport = new List<Report>();

                if (packingHistoryReports != null)
                {
                    foreach (var dbr in packingHistoryReports)
                    {
                        Report li = new Report();
                        li.Date_Of_Day = dbr.Date_Of_Day;
                        li.Package_No = dbr.Package_No;
                        li.SaleOrderNo = dbr.SaleOrderNo;
                        li.Package_Status = dbr.Package_Status;
                        li.Quantity = dbr.Quantity;
                        packingHistoryReport.Add(li);
                    }
                }

                packingHistoryReport.TrimExcess();

                return packingHistoryReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Sales_SalesByCustomerReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_SalesByCustomerReport()
        {
            List<Report> salesByCustomerReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> salesByCustomerReports = new Catalog().Sales_SalesByCustomerReport(search.StartDate, search.EndDate, search.CustomerName, search.Search);
                salesByCustomerReport = new List<Report>();

                if (salesByCustomerReports != null)
                {
                    foreach (var dbr in salesByCustomerReports)
                    {
                        Report li = new Report();
                        li.Customer_id = dbr.Customer_id;
                        li.CustomerName = dbr.CustomerName;
                        li.InvoiceCount = dbr.InvoiceCount;
                        li.Sales = dbr.Sales;
                        salesByCustomerReport.Add(li);
                    }
                }

                salesByCustomerReport.TrimExcess();

                return salesByCustomerReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Sales_SalesByItemReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_SalesByItemReport()
        {
            List<Report> salesByItemReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> salesByItemReportS = new Catalog().Sales_SalesByItemReport(search.StartDate, search.EndDate, search.ItemName, search.Search);
                salesByItemReport = new List<Report>();

                if (salesByItemReportS != null)
                {
                    foreach (var dbr in salesByItemReportS)
                    {
                        Report li = new Report();
                        li.Item_id = dbr.Item_id;
                        li.ItemName = dbr.ItemName;
                        li.QuantitySold = dbr.QuantitySold;
                        li.Amount = dbr.Amount;
                        li.AveragePrice = dbr.AveragePrice;
                        salesByItemReport.Add(li);
                    }
                }

                salesByItemReport.TrimExcess();

                return salesByItemReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Sales_SalesBySalesPersonReport")]
        [HttpPost]
        public IEnumerable<Report> Sales_SalesBySalesPersonReport()
        {
            List<Report> salesBySalesPersonReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> salesBySalesPersonRepors = new Catalog().Sales_SalesBySalesPersonReport(search.StartDate, search.EndDate, search.UserName, search.Search);
                salesBySalesPersonReport = new List<Report>();

                if (salesBySalesPersonRepors != null)
                {
                    foreach (var dbr in salesBySalesPersonRepors)
                    {
                        Report li = new Report();
                        li.AddedBy = dbr.AddedBy;
                        li.AddedByName = dbr.AddedByName;
                        li.InvoiceCount = dbr.InvoiceCount;
                        li.Sales = dbr.Sales;
                        salesBySalesPersonReport.Add(li);
                    }
                }

                salesBySalesPersonReport.TrimExcess();

                return salesBySalesPersonReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }
        #endregion

        #region PURCHASES
        [Route("api/APIReports/Purchases_PurchaseOrderHistoryReport")]
        [HttpPost]
        public IEnumerable<Report> Purchases_PurchaseOrderHistoryReport()
        {
            List<Report> purchaseOrderHistory = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> purchaseOrderHistorys = new Catalog().Purchases_PurchaseOrderHistoryReport(search.StartDate, search.EndDate, search.Status, search.Search);

                if (purchaseOrderHistorys != null)
                {
                    foreach (var dbr in purchaseOrderHistorys)
                    {
                        Report li = new Report();
                        li.Date_Of_Day = dbr.Date_Of_Day;
                        li.PurchaseOrderNo = dbr.PurchaseOrderNo;
                        li.Vendor_id = dbr.Vendor_id;
                        li.VendorName = dbr.VendorName;
                        li.PurchaseOrderStatus = dbr.PurchaseOrderStatus;
                        li.QuantityOrdered = dbr.QuantityOrdered;
                        li.QuantityReceived = dbr.QuantityReceived;
                        li.Amount = dbr.Amount;
                        purchaseOrderHistory.Add(li);
                    }
                }

                purchaseOrderHistory.TrimExcess();

                return purchaseOrderHistory;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Purchases_PurchaseByVendorReport")]
        [HttpPost]
        public IEnumerable<Report> Purchases_PurchaseByVendorReport()
        {
            List<Report> purchaseByVendorReport = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> purchaseByVendorReports = new Catalog().Purchases_PurchaseByVendorReport(search.StartDate, search.EndDate, search.Search);
                purchaseByVendorReport = new List<Report>();

                if (purchaseByVendorReports != null)
                {
                    foreach (var dbr in purchaseByVendorReports)
                    {
                        Report li = new Report();
                        li.Vendor_id = dbr.Vendor_id;
                        li.VendorName = dbr.VendorName;
                        li.QuantityOrdered = dbr.QuantityOrdered;
                        li.Amount = dbr.Amount;
                        purchaseByVendorReport.Add(li);
                    }
                }

                purchaseByVendorReport.TrimExcess();

                return purchaseByVendorReport;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Purchases_PurchaseByItemReport")]
        [HttpPost]
        public IEnumerable<Report> Purchases_PurchaseByItemReport()
        {
            List<Report> purchaseByItem = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> purchaseByItems = new Catalog().Purchases_PurchaseByItemReport(search.StartDate, search.EndDate, search.Search);
                purchaseByItem = new List<Report>();

                if (purchaseByItems != null)
                {
                    foreach (var dbr in purchaseByItems)
                    {
                        Report li = new Report();
                        li.Item_id = dbr.Item_id;
                        li.ItemName = dbr.ItemName;
                        li.QuantityPurchased = dbr.QuantityPurchased;
                        li.Amount = dbr.Amount;
                        li.AveragePrice = dbr.AveragePrice;
                        purchaseByItem.Add(li);
                    }
                }

                purchaseByItem.TrimExcess();

                return purchaseByItem;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APIReports/Purchases_BillDetailsReport")]
        [HttpPost]
        public IEnumerable<Report> Purchases_BillDetailsReport()
        {
            List<Report> billDetail = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> billDetails = new Catalog().Purchases_BillDetailsReport(search.StartDate, search.EndDate, search.Search);
                billDetail = new List<Report>();

                if (billDetails != null)
                {
                    foreach (var dbr in billDetails)
                    {
                        Report li = new Report();
                        li.Bill_Status = dbr.Bill_Status;
                        li.BillDateTime = dbr.BillDateTime;
                        li.BillDueDate = dbr.BillDueDate;
                        li.Bill_No = dbr.Bill_No;
                        li.Vendor_id = dbr.Vendor_id;
                        li.VendorName = dbr.VendorName;
                        li.Bill_Amount = dbr.Bill_Amount;
                        li.Balance_Amount = dbr.Balance_Amount;
                        billDetail.Add(li);
                    }
                }

                billDetail.TrimExcess();

                return billDetail;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }
        #endregion

        [Route("api/APIReports/ActivityLogsReport")]
        [HttpPost]
        public IEnumerable<Report> ActivityLogsReport()
        {
            List<Report> activityLog = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Reports> activityLogs = new Catalog().ActivityLogsReport(search.StartDate, search.EndDate, search.Search);
                activityLog = new List<Report>();

                if (activityLogs != null)
                {
                    foreach (var dbr in activityLogs)
                    {
                        Report li = new Report();
                        li.Date = dbr.Date;
                        li.Time = dbr.Time;
                        li.DateTime = dbr.Date + " " + dbr.Time;
                        li.ActivityType = dbr.ActivityType;
                        li.ActivityName = dbr.ActivityName;
                        li.Activity = dbr.ActivityType + " - " + dbr.ActivityName;
                        li.Description = dbr.Description;
                        activityLog.Add(li);
                    }
                }

                activityLog.TrimExcess();

                return activityLog;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }
    }
}
