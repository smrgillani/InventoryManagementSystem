using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G_Accounting_System.Models;
using System.Web.Script.Serialization;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class ReportsController : Controller
    {
        // GET: Reports
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        #region INVENTORY
        public ActionResult Inventory_ProductSalesReport()
        {
            return View("Inventory_ProductSalesReport");
        }

        public ActionResult Inventory_InventoryDetailsReport()
        {
            return View("Inventory_InventoryDetailsReport");
        }

        public ActionResult Inventory_InventoryValuationSummaryReport()
        {
            return View("Inventory_InventoryValuationSummaryReport");
        }

        public ActionResult Inventory_StockSummaryReport()
        {
            return View("Inventory_StockSummaryReport");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inventory_ProductSalesReport(string Search)
        {
            List<Report> productsalesreport = new List<Report>();
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            try
            {
                List<Reports> productsalesreports = new Catalog().Inventory_ProductSalesReport(search.StartDate, search.EndDate, search.Search);

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
                var pproductsalesreport = productsalesreport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = productsalesreport.Count, recordsFiltered = productsalesreport.Count, data = pproductsalesreport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inventory_InventoryDetailsReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> inventoryDetailsReport = new List<Report>();
            try
            {
                List<Reports> inventoryDetailsReports = new Catalog().Inventory_InventoryDetailsReport(search.StartDate, search.EndDate, search.ItemName, search.Search);



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
                var pinventoryDetailsReport = inventoryDetailsReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = inventoryDetailsReport.Count, recordsFiltered = inventoryDetailsReport.Count, data = pinventoryDetailsReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inventory_InventoryValuationSummaryReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> inventoryValuationSummaryReport = new List<Report>();
            try
            {
                List<Reports> inventoryValuationSummaryReports = new Catalog().Inventory_InventoryValuationSummaryReport(search.StartDate, search.EndDate, search.ItemName, search.Search);

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
                var pinventoryValuationSummaryReport = inventoryValuationSummaryReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = inventoryValuationSummaryReport.Count, recordsFiltered = inventoryValuationSummaryReport.Count, data = pinventoryValuationSummaryReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inventory_StockSummaryReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> stockSummaryReport = new List<Report>();
            try
            {
                List<Reports> stockSummaryReports = new Catalog().Inventory_StockSummaryReport(search.StartDate, search.EndDate, search.ItemName, search.Search);

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
                var pstockSummaryReport = stockSummaryReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = stockSummaryReport.Count, recordsFiltered = stockSummaryReport.Count, data = pstockSummaryReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
           
        }
        #endregion

        #region SALESORDER
        public ActionResult Sales_SalesOrderHistoryReport()
        {
            return View("Sales_SalesOrderHistoryReport");
        }

        public ActionResult Sales_OrderFulfillmentByItemReport()
        {
            return View("Sales_OrderFulfillmentByItemReport");
        }

        public ActionResult Sales_InvoiceHistoryReport()
        {
            return View("Sales_InvoiceHistoryReport");
        }

        public ActionResult Sales_PaymentsReceivedReport()
        {
            return View("Sales_PaymentsReceivedReport");
        }

        public ActionResult Sales_PackingHistoryReport()
        {
            return View("Sales_PackingHistoryReport");
        }

        public ActionResult Sales_SalesByCustomerReport()
        {
            return View("Sales_SalesByCustomerReport");
        }

        public ActionResult Sales_SalesByItemReport()
        {
            return View("Sales_SalesByItemReport");
        }

        public ActionResult Sales_SalesBySalesPersonReport()
        {
            return View("Sales_SalesBySalesPersonReport");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_SalesOrderHistoryReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> salesOrderHistoryReport = new List<Report>();
            try
            {
                List<Reports> salesOrderHistoryReports = new Catalog().Sales_SalesOrderHistoryReport(search.StartDate, search.EndDate, search.Status, search.Search);

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
                var psalesOrderHistoryReport = salesOrderHistoryReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = salesOrderHistoryReport.Count, recordsFiltered = salesOrderHistoryReport.Count, data = psalesOrderHistoryReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_OrderFulfillmentByItemReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> orderFulfillmentByItemReport = new List<Report>();
            try
            {
                List<Reports> orderFulfillmentByItemReports = new Catalog().Sales_OrderFulfillmentByItemReport(search.StartDate, search.EndDate, search.Search);

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
                var porderFulfillmentByItemReport = orderFulfillmentByItemReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = orderFulfillmentByItemReport.Count, recordsFiltered = orderFulfillmentByItemReport.Count, data = porderFulfillmentByItemReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_InvoiceHistoryReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> invoiceHistoryReport = new List<Report>();
            try
            {
                List<Reports> invoiceHistoryReports = new Catalog().Sales_InvoiceHistoryReport(search.StartDate, search.EndDate, search.Status, search.Search);

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
                var pinvoiceHistoryReport = invoiceHistoryReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = invoiceHistoryReport.Count, recordsFiltered = invoiceHistoryReport.Count, data = pinvoiceHistoryReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_PaymentsReceivedReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> paymentsReceivedReport = new List<Report>();
            try
            {
                List<Reports> paymentsReceivedReports = new Catalog().Sales_PaymentsReceivedReport(search.StartDate, search.EndDate, search.Search);

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
                var ppaymentsReceivedReport = paymentsReceivedReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = paymentsReceivedReport.Count, recordsFiltered = paymentsReceivedReport.Count, data = ppaymentsReceivedReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_PackingHistoryReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> packingHistoryReport = new List<Report>();
            try
            {
                List<Reports> packingHistoryReports = new Catalog().Sales_PackingHistoryReport(search.StartDate, search.EndDate, search.Status, search.Search);

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
                var ppackingHistoryReport = packingHistoryReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = packingHistoryReport.Count, recordsFiltered = packingHistoryReport.Count, data = ppackingHistoryReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_SalesByCustomerReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> salesByCustomerReport = new List<Report>();
            try
            {
                List<Reports> salesByCustomerReports = new Catalog().Sales_SalesByCustomerReport(search.StartDate, search.EndDate, search.CustomerName, search.Search);

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
                var psalesByCustomerReport = salesByCustomerReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = salesByCustomerReport.Count, recordsFiltered = salesByCustomerReport.Count, data = psalesByCustomerReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_SalesByItemReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> salesByItemReport = new List<Report>();
            try
            {
                List<Reports> salesByItemReportS = new Catalog().Sales_SalesByItemReport(search.StartDate, search.EndDate, search.ItemName, search.Search);

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
                var psalesByItemReport = salesByItemReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = salesByItemReport.Count, recordsFiltered = salesByItemReport.Count, data = psalesByItemReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sales_SalesBySalesPersonReport(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Report> salesBySalesPersonReport = new List<Report>();
            try
            {
                List<Reports> salesBySalesPersonRepors = new Catalog().Sales_SalesBySalesPersonReport(search.StartDate, search.EndDate, search.UserName, search.Search);

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
                var psalesBySalesPersonReport = salesBySalesPersonReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = salesBySalesPersonReport.Count, recordsFiltered = salesBySalesPersonReport.Count, data = psalesBySalesPersonReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
           
        }
        #endregion

        #region PURCHASES
        public ActionResult Purchases_PurchaseOrderHistoryReport()
        {
            return View("Purchases_PurchaseOrderHistoryReport");
        }

        public ActionResult Purchases_PurchaseByVendorReport()
        {
            return View("Purchases_PurchaseByVendorReport");
        }

        public ActionResult Purchases_PurchaseByItemReport()
        {
            return View("Purchases_PurchaseByItemReport");
        }

        public ActionResult Purchases_BillDetailsReport()
        {
            return View("Purchases_BillDetailsReport");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchases_PurchaseOrderHistoryReport(string Search)
        {
            List<Report> purchaseOrderHistory = new List<Report>();
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            try
            {
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
                var ppurchaseOrderHistory = purchaseOrderHistory.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = purchaseOrderHistory.Count, recordsFiltered = purchaseOrderHistory.Count, data = ppurchaseOrderHistory }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchases_PurchaseByVendorReport(string Search)
        {
            List<Report> purchaseByVendorReport = new List<Report>();
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            try
            {
                List<Reports> purchaseByVendorReports = new Catalog().Purchases_PurchaseByVendorReport(search.StartDate, search.EndDate, search.Search);

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
                var ppurchaseByVendorReport = purchaseByVendorReport.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = purchaseByVendorReport.Count, recordsFiltered = purchaseByVendorReport.Count, data = ppurchaseByVendorReport }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchases_PurchaseByItemReport(string Search)
        {
            List<Report> purchaseByItem = new List<Report>();
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            try
            {
                List<Reports> purchaseByItems = new Catalog().Purchases_PurchaseByItemReport(search.StartDate, search.EndDate, search.Search);

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
                var ppurchaseByItem = purchaseByItem.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = purchaseByItem.Count, recordsFiltered = purchaseByItem.Count, data = ppurchaseByItem }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchases_BillDetailsReport(string Search)
        {
            List<Report> billDetail = new List<Report>();
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            try
            {
                List<Reports> billDetails = new Catalog().Purchases_BillDetailsReport(search.StartDate, search.EndDate, search.Search);

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
                var pbillDetail = billDetail.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = billDetail.Count, recordsFiltered = billDetail.Count, data = pbillDetail }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        #endregion

        public ActionResult ActivityLogsReport()
        {
            return View("ActivityLogsReport");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivityLogsReport(string Search)
        {
            List<Report> activityLog = new List<Report>();
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            try
            {
                List<Reports> activityLogs = new Catalog().ActivityLogsReport(search.StartDate, search.EndDate, search.Search);

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
                var pactivityLog = activityLog.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = activityLog.Count, recordsFiltered = activityLog.Count, data = pactivityLog }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

    }
}