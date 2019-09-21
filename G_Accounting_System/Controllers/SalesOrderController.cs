using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G_Accounting_System.Models;
using System.Web.Script.Serialization;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using Newtonsoft.Json;
using G_Accounting_System.Code.Helpers;
using System.Data;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class SalesOrderController : Controller
    {
        // GET: SalesOrder
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add()
        {
            bool check = new PermissionsClass().CheckAddPermission();
            if (check != false)
            {
                return View("SalesOrder");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllSalesOrders(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);

            int User_id = Convert.ToInt32(Session["UserId"]);
            List<SalesOrders> salesorders = new Catalog().AllSalesOrders(search.Option, search.Search, search.StartDate, search.EndDate, User_id);


            List<SalesOrder> salesorder = new List<SalesOrder>();

            if (salesorders != null)
            {
                foreach (var dbr in salesorders)
                {
                    SalesOrder li = new SalesOrder();
                    li.SalesOrder_id = dbr.SalesOrder_id;
                    li.SaleOrderNo = dbr.SaleOrderNo;
                    li.Customer_Name = dbr.Customer_Name;
                    li.TotalItems = dbr.TotalItems;
                    li.SO_Total_Amount = dbr.SO_Total_Amount;
                    li.SO_Status = dbr.SO_Status;
                    li.TotalItems = dbr.TotalItems;
                    li.SO_Invoice_Status = dbr.SO_Invoice_Status;
                    li.SO_Package_Status = dbr.SO_Package_Status;
                    li.SO_Shipment_Status = dbr.SO_Shipment_Status;
                    li.SO_DateTime = dbr.SO_DateTime;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Time_Of_Day = dbr.Time_Of_Day;
                    li.Date_Of_Day = dbr.Date_Of_Day;
                    li.Month_Of_Day = dbr.Month_Of_Day;
                    li.Year_Of_Day = dbr.Year_Of_Day;
                    salesorder.Add(li);
                }
            }
            salesorder.TrimExcess();
            var psalesorder = salesorder.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = salesorder.Count, recordsFiltered = salesorder.Count, data = psalesorder }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSalesOrder(string SalesOrderData, int SalesOrder_id)
        {
            var js = new JavaScriptSerializer();
            Activity activity = null;
            string response = "";
            List<Activity> activities = null;
            List<SalesOrders> salesorders = new List<SalesOrders>();
            SalesOrder saleorder = new SalesOrder();
            try
            {
                activity = new Activity();
                activities = new List<Activity>();
                List<SalesOrder> salesorder = js.Deserialize<List<SalesOrder>>(SalesOrderData);
                if (salesorder.Count() < 0)
                {
                    response = "Please Add Products for Sales Order.";
                }
                else
                {


                    int i = 1;
                    int iv = 0;
                    int jv = 0;
                    int ii = 0;
                    int customer_id = salesorder[0].Customer_id;
                    SalesOrders so = new SalesOrders();
                    so.SalesOrder_id = SalesOrder_id;
                    foreach (var dbr in salesorder)
                    {
                        if (dbr.Customer_id != customer_id)
                        {
                            iv += 1;
                        }

                    }
                    if (iv == 0)
                    {
                        foreach (var dbr in salesorder)
                        {
                            Contacts contact = new Catalog().SelectContact(Convert.ToInt32(dbr.Customer_id), 0, 1, 0, 1);
                            if (contact == null)
                            {
                                jv += 1;
                            }
                        }
                    }
                    if (iv == 0 && jv == 0)
                    {
                        foreach (var dbr in salesorder)
                        {
                            Items item = new Catalog().SelectItem(Convert.ToInt32(dbr.ItemId), 1);
                            if (item == null)
                            {
                                ii += 1;
                            }
                        }
                    }
                    if (iv == 0 & jv == 0 && ii == 0)
                    {
                        foreach (var dbr in salesorder)
                        {
                            SalesOrders li = new SalesOrders();

                            Items item = new Catalog().SelectItem(Convert.ToInt32(dbr.ItemId), 1);

                            Contacts contact = new Catalog().SelectContact(Convert.ToInt32(dbr.Customer_id), 0, 1, 0, 1);

                            if (item == null)
                            {
                                response = "Item Number " + i + " Is Not A Valid Item, Please Add A Valid Item";
                            }
                            else if (contact == null)
                            {
                                response = response.Count() > 0 ? response + " And " : "" + "Please Add A Valid Customer For Item Number " + i + ".";
                            }
                            li.sdid = dbr.sdid;
                            li.ItemId = dbr.ItemId;
                            li.Customer_id = dbr.Customer_id;
                            li.Quantity = dbr.Quantity;
                            li.PriceUnit = dbr.PriceUnit;
                            li.MsrmntUnit = dbr.MsrmntUnit;
                            salesorders.Add(li);
                            if (dbr.sdid == 0)
                            {
                                activity = new Activity();
                                activity.ActivityType_id = dbr.ItemId;
                                activity.ActivityType = "Item";
                                activity.ActivityName = "Sales Order";
                                activity.User_id = Convert.ToInt32(Session["UserId"]);
                                activity.Icon = "fa fa-fw fa-floppy-o bg-blue";

                                activities.Add(activity);
                            }
                        }

                        salesorders.TrimExcess();


                        foreach (var listitem in salesorder)
                        {
                            so.SO_Total_Amount = listitem.SO_Total_Amount;
                            so.SO_Status = listitem.SO_Status;
                            so.SO_Invoice_Status = listitem.SO_Invoice_Status;
                            so.SO_Shipment_Status = listitem.SO_Shipment_Status;
                            so.SO_Package_Status = listitem.SO_Package_Status;
                            so.SO_Package_Status = listitem.SO_Package_Status;
                            so.Enable = 1;
                            so.AddedBy = Convert.ToInt32(Session["UserId"]);
                        }

                        if (response.Length <= 0)
                        {
                            if (so.SalesOrder_id == 0)
                            {
                                so.AddedBy = Convert.ToInt32(Session["UserId"]);
                                so.UpdatedBy = 0;
                            }
                            else
                            {
                                so.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                                so.AddedBy = 0;
                            }
                            int Premises_id = Convert.ToInt32(Session["Premises_id"]);
                            string Result = new Catalog().AddSalesOrder(salesorders, so, Premises_id, "1");
                            saleorder.pFlag = so.pFlag;
                            saleorder.pDesc = so.pDesc;
                            saleorder.pSO_Output = so.pSO_Output;
                            if (so.pFlag == "1")
                            {
                                activity = new Activity();
                                if (so.SalesOrder_id == 0)
                                {
                                    activity.ActivityName = "Created";
                                    activity.ActivityType_id = Convert.ToInt32(saleorder.pSO_Output);
                                }
                                else
                                {
                                    activity.ActivityName = "Updated";
                                    activity.ActivityType_id = so.SalesOrder_id;
                                }
                                activity.ActivityType = "Sales Order";
                                activity.User_id = Convert.ToInt32(Session["UserId"]);
                                activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                                activities.Add(activity);


                                new ActivitiesClass().InsertActivity(activities);
                            }
                        }
                    }
                    else
                    {
                        saleorder.pFlag = "0";
                        saleorder.pDesc = "Invalid Data Povided";
                    }
                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(saleorder, JsonRequestBehavior.AllowGet);
        }

        [PermissionsAuthorize]
        public ActionResult SOInvoice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SOInvoice(int id)
        {
            SalesOrders salesorders = null;
            List<SalesOrder> items = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    salesorders = new Catalog().SelectSOById(id);
                    List<SalesOrders> itemspackg = new Catalog().SelectAllSOItems(id);

                    items = new List<SalesOrder>();

                    if (salesorders != null)
                    {
                        foreach (var dbr in itemspackg)
                        {
                            SalesOrder li = new SalesOrder();
                            li.sdid = dbr.sdid;
                            li.ItemId = dbr.ItemId;
                            li.ItemName = dbr.ItemName != null ? dbr.ItemName : "";
                            li.Customer_id = dbr.Customer_id;
                            li.Customer_Name = dbr.Customer_Name != null ? dbr.Customer_Name : "";
                            li.Customer_Address = dbr.Customer_Address != null ? dbr.Customer_Address : "";
                            li.Customer_Landline = dbr.Customer_Landline != null ? dbr.Customer_Landline : "";
                            li.Customer_Mobile = dbr.Customer_Mobile != null ? dbr.Customer_Mobile : "";
                            li.Customer_Email = dbr.Customer_Email != null ? dbr.Customer_Email : "";
                            li.MsrmntUnit = dbr.MsrmntUnit;
                            li.PriceUnit = dbr.PriceUnit;
                            li.ItemQty = dbr.ItemQty;
                            li.TotalItems = dbr.TotalItems;
                            li.Packed_Qty = dbr.Packed_Qty;
                            items.Add(li);
                        }
                    }
                    items.TrimExcess();
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(new { SalesOrder_id = salesorders.SalesOrder_id, SaleOrderNo = salesorders.SaleOrderNo, TotalPrice = salesorders.SO_Total_Amount, PremisesId = salesorders.PremisesId, Premises_Name = salesorders.Premises_Name, UserId = salesorders.UserId, User_Name = salesorders.User_Name != null ? salesorders.User_Name : "", SO_Invoice_id = salesorders.SO_Invoice_id, Time_Of_Day = salesorders.Time_Of_Day, Date_Of_Day = salesorders.Date_Of_Day, PriceUnit = salesorders.PriceUnit, TotalItems = salesorders.TotalItems, SO_Total_Amount = salesorders.SO_Total_Amount, ItemQty = salesorders.ItemQty, SO_Status = salesorders.SO_Status, SO_Invoice_Status = salesorders.SO_Invoice_Status, SO_Shipment_Status = salesorders.SO_Shipment_Status, SO_Package_Status = salesorders.SO_Package_Status, SO_DateTime = salesorders.SO_DateTime, SOItems = items, TotalPackageCost = salesorders.PackageCost, TotalShipmentCost = salesorders.SO_Shipping_Charges }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bill()
        {

            return PartialView("SOBill_partialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertInvoice(string InvoiceData, string SOData)
        {
            SO_Invoices AddSO_Invoice = null;
            SalesOrders AddSO = null;
            try
            {
                var js = new JavaScriptSerializer();
                SO_Invoice so_invoice = js.Deserialize<SO_Invoice>(InvoiceData);
                SalesOrder salesorder = js.Deserialize<SalesOrder>(SOData);

                AddSO_Invoice = new SO_Invoices();
                AddSO_Invoice.id = so_invoice.id;
                AddSO_Invoice.SalesOrder_id = so_invoice.SalesOrder_id;
                AddSO_Invoice.Customer_id = so_invoice.Customer_id;
                AddSO_Invoice.Invoice_No = so_invoice.Invoice_No;
                AddSO_Invoice.Invoice_Status = so_invoice.Invoice_Status;
                AddSO_Invoice.Invoice_Amount = so_invoice.Invoice_Amount;
                AddSO_Invoice.Amount_Paid = so_invoice.Amount_Paid;
                AddSO_Invoice.Balance_Amount = so_invoice.Balance_Amount;
                AddSO_Invoice.InvoiceDateTime = so_invoice.InvoiceDateTime;
                AddSO_Invoice.InvoiceDueDate = so_invoice.InvoiceDueDate;
                AddSO_Invoice.AddedBy = Convert.ToInt32(Session["UserId"]);

                AddSO = new SalesOrders();
                AddSO.SalesOrder_id = salesorder.SalesOrder_id;
                AddSO.SO_Invoice_Status = salesorder.SO_Invoice_Status;
                AddSO.SO_Status = salesorder.SO_Status;

                new Catalog().InsertSOInvoice(AddSO_Invoice);


                so_invoice.pFlag = AddSO_Invoice.pFlag;
                so_invoice.pDesc = AddSO_Invoice.pDesc;
                so_invoice.pInvoice_id_Output = AddSO_Invoice.pInvoice_id_Output;
                if (so_invoice.pFlag == "1")
                {
                    new Catalog().UpdateSO_InvoiceStatus(AddSO);

                    Activity activity = new Activity();

                    activity.ActivityType_id = so_invoice.SalesOrder_id;
                    activity.ActivityType = "Sales Order";
                    activity.ActivityName = "Invoice";
                    activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
                else
                {
                    AddSO = null;
                }
                return Json(new { Invoice = AddSO_Invoice, So = AddSO }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                AddSO_Invoice = null;
                AddSO = null;
                return Json(new { Invoice = AddSO_Invoice, So = AddSO }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePurchaseStatus(string PurchaseData)
        {

            string response = "";
            try
            {
                var js = new JavaScriptSerializer();
                Purchases purchases = new Purchases();

                if (!string.IsNullOrEmpty(PurchaseData))
                {
                    Purchase purchase = js.Deserialize<Purchase>(PurchaseData);

                    purchases.id = purchase.id;
                    purchases.RecieveStatus = purchase.RecieveStatus;
                    purchases.RecieveDateTime = purchase.RecieveDateTime;


                    new Catalog().UpdatePurchaseStatus(purchases);
                }
            }
            catch (Exception e)
            {
                response = "Internal Error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SOInvoicePayment(int id)
        {
            SO_Invoices soinvoices = new Catalog().SelectSOInvoicePayment(id);

            SO_Invoice soinvoice = new SO_Invoice();

            if (soinvoices != null)
            {
                soinvoice.id = soinvoices.id;
                soinvoice.SalesOrder_id = soinvoices.SalesOrder_id;
                soinvoice.Invoice_No = soinvoices.Invoice_No;
                soinvoice.Invoice_Status = soinvoices.Invoice_Status;
                soinvoice.Invoice_Amount = soinvoices.Invoice_Amount;
                soinvoice.Amount_Paid = soinvoices.Amount_Paid;
                soinvoice.Balance_Amount = soinvoices.Balance_Amount;
                soinvoice.InvoiceDateTime = soinvoices.InvoiceDateTime;
                soinvoice.InvoiceDueDate = soinvoices.InvoiceDueDate;
            }
            return Json(new { Invoice = soinvoice }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertSO_Payments(string InvoicePaymentData, string InvoiceStatus)
        {
            var js = new JavaScriptSerializer();
            SO_Payments AddSO_Payment = null;
            try
            {
                SO_Payment so_payment = js.Deserialize<SO_Payment>(InvoicePaymentData);
                SO_Invoice so_invoice = js.Deserialize<SO_Invoice>(InvoiceStatus);

                AddSO_Payment = new SO_Payments();
                AddSO_Payment.Invoice_id = so_payment.Invoice_id;
                AddSO_Payment.Payment_Mode = so_payment.Payment_Mode;
                AddSO_Payment.Payment_Date = so_payment.Payment_Date;
                AddSO_Payment.Total_Amount = so_payment.Total_Amount;
                AddSO_Payment.Paid_Amount = so_payment.Paid_Amount;
                AddSO_Payment.Balance_Amount = so_payment.Balance_Amount;
                AddSO_Payment.AddedBy = Convert.ToInt32(Session["UserId"]);

                new Catalog().InsertSO_Payments(AddSO_Payment);

                so_payment.pFlag = AddSO_Payment.pFlag;
                so_payment.pDesc = AddSO_Payment.pDesc;

                SO_Invoices AddSO_Invoices = new SO_Invoices();
                AddSO_Invoices.id = so_invoice.id;
                AddSO_Invoices.Invoice_Status = so_invoice.Invoice_Status;
                if (so_invoice.Invoice_Status == "Paid")
                {
                    new Catalog().UpdateinvoiceStatus(AddSO_Invoices);

                }
                if (so_payment.pFlag == "1")
                {
                    Activity activity = new Activity();

                    activity.ActivityType_id = so_invoice.SalesOrder_id;
                    activity.ActivityType = "Sales Order";
                    activity.ActivityName = "Payment";
                    activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
                //var result = JsonConvert.SerializeObject(item);

                return Json(AddSO_Payment, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                AddSO_Payment = null;
                return Json(AddSO_Payment, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectSO_IvnvoicePaymentHistory(int id)
        {
            List<SO_Payments> payments = new Catalog().SelectSO_IvnvoicePayment(Convert.ToInt32(id));
            List<SO_Payment> payment = new List<SO_Payment>();

            if (payments != null)
            {
                foreach (var dbr in payments)
                {
                    SO_Payment li = new SO_Payment();
                    li.SO_Payment_id = dbr.SO_Payment_id;
                    li.Invoice_id = dbr.Invoice_id;
                    li.Invoice_No = dbr.Invoice_No;
                    li.Payment_Mode = dbr.Payment_Mode;
                    li.Payment_Date = dbr.Payment_Date;
                    li.Total_Amount = dbr.Total_Amount;
                    li.Paid_Amount = dbr.Paid_Amount;
                    li.Balance_Amount = dbr.Balance_Amount;
                    payment.Add(li);
                }
                payment.TrimExcess();
            }
            return Json(payment, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Package()
        {
            return PartialView("SOPackage_partialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertSOPackage(string Packagedata, string SOData)
        {
            string response = "";
            var js = new JavaScriptSerializer();
            List<Package> so_package = js.Deserialize<List<Package>>(Packagedata);
            SalesOrder salesorder = js.Deserialize<SalesOrder>(SOData);

            SalesOrders AddSO = null;
            List<Packages> so_packages = new List<Packages>();
            try
            {
                if (so_package.Count() < 0)
                {
                    response = "Please Add Products to Packing.";
                }
                else
                {
                    AddSO = new SalesOrders();
                    AddSO.SalesOrder_id = salesorder.SalesOrder_id;
                    //AddSO.SO_Package_Status = salesorder.SO_Package_Status;
                    //AddSO.SO_Status = salesorder.SO_Status;

                    int i = 1;
                    foreach (var dbr in so_package)
                    {
                        Packages li = new Packages();
                        li.SalesOrder_id = dbr.SalesOrder_id;
                        li.Package_Date = dbr.Package_Date;
                        li.PackageStatus = dbr.PackageStatus;
                        li.PackageCost = dbr.PackageCost;
                        li.Item_id = dbr.Item_id;
                        li.unitprice = dbr.unitprice;
                        li.PackageCost = dbr.PackageCost;
                        li.Packed_Qty = dbr.Packed_Qty;
                        so_packages.Add(li);

                        i++;
                    }


                    so_packages.TrimExcess();
                    int AddedBy = Convert.ToInt32(Session["UserId"]);
                    Packages package = new Packages();
                    Package pkg = new Package();
                    new Catalog().InsertSOPackage(so_packages, package, AddSO.SalesOrder_id, AddedBy);
                    pkg.pFlag = package.pFlag;
                    pkg.pDesc = package.pDesc;
                    if (package.pFlag == "1")
                    {
                        Activity activity = new Activity();

                        activity.ActivityType_id = AddSO.SalesOrder_id;
                        activity.ActivityType = "Sales Order";
                        activity.ActivityName = "Package";
                        activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }


                }
            }
            catch (Exception e)
            {
                so_packages = null;
                so_package = null;
                salesorder = null;
                Packagedata = null;
                SOData = null;
            }

            return Json(new { Invoice = so_packages, So = AddSO }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectPackagesForSO(int id)
        {
            List<Packages> packages = new Catalog().SelectPackagesForSO(Convert.ToInt32(id));
            List<Package> package = new List<Package>();

            if (packages != null)
            {
                foreach (var dbr in packages)
                {
                    Package li = new Package();
                    li.Package_id = dbr.Package_id;
                    li.SalesOrder_id = dbr.SalesOrder_id;
                    li.Package_No = dbr.Package_No;
                    li.Package_Date = dbr.Package_Date;
                    li.PackageStatus = dbr.PackageStatus;
                    li.PackageCost = dbr.PackageCost;
                    package.Add(li);
                }
                package.TrimExcess();
            }
            return Json(package, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectPackagedItemsBySOid(int id)
        {
            List<SalesOrders> AllItems = new Catalog().SelectAllSOItems(Convert.ToInt32(id));

            List<SalesOrder> packageditem = new List<SalesOrder>();
            Package li = null;
            foreach (var item in AllItems)
            {
                SalesOrder so = new SalesOrder();
                so.SalesOrder_id = item.SalesOrder_id;
                so.ItemId = item.ItemId;
                so.ItemName = item.ItemName;
                so.PriceUnit = item.PriceUnit;
                so.Packed_Qty = item.Packed_Qty;
                so.InvoicedQty = item.InvoicedQty;
                so.ItemQty = item.ItemQty;
                so.TotalItems = item.TotalItems;
                so.PackageCost = item.PackageCost;
                packageditem.Add(so);
            }
            packageditem.TrimExcess();
            return Json(new { packageditem = packageditem }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectSObyID(int id)
        {
            SalesOrders salesorders = new Catalog().SelectSOById(id);
            SalesOrder salesorder = new SalesOrder();

            if (salesorders != null)
            {
                salesorder = new SalesOrder();
                salesorder.SalesOrder_id = salesorders.SalesOrder_id;
                salesorder.SaleOrderNo = salesorders.SaleOrderNo;
                salesorder.Customer_id = salesorders.Customer_id;
                salesorder.Customer_Name = salesorders.Customer_Name;
                salesorder.PremisesId = salesorders.PremisesId;
                salesorder.Premises_Name = salesorders.Premises_Name;
                salesorder.UserId = salesorders.UserId;
                salesorder.User_Name = salesorders.User_Name;
                salesorder.SO_Total_Amount = salesorders.SO_Total_Amount;
                salesorder.SO_Shipment_Status = salesorders.SO_Shipment_Status;
                salesorder.SO_Status = salesorders.SO_Status;
                salesorder.SO_Invoice_Status = salesorders.SO_Invoice_Status;
                salesorder.SO_Package_Status = salesorders.SO_Package_Status;
                salesorder.SO_DateTime = salesorders.SO_DateTime;
                salesorder.SO_Expected_Shipment_Date = salesorders.SO_Expected_Shipment_Date;
                salesorder.TotalItems = salesorders.TotalItems;
                salesorder.SO_Total_Amount = salesorders.SO_Total_Amount;

            }
            return Json(salesorder, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Shipment()
        {
            return PartialView("SOShipment_partialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectPackagesForShipment(int id)
        {
            List<Packages> packages = new Catalog().SelectPackagesForShipment(Convert.ToInt32(id));
            List<Package> package = new List<Package>();

            if (packages != null)
            {
                foreach (var dbr in packages)
                {
                    Package li = new Package();
                    li.Package_id = dbr.Package_id;
                    li.SalesOrder_id = dbr.SalesOrder_id;
                    li.Package_No = dbr.Package_No;
                    li.Package_Date = dbr.Package_Date;
                    li.PackageStatus = dbr.PackageStatus;
                    package.Add(li);
                }
                package.TrimExcess();
            }
            return Json(package, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertSOShipment(string ShipmentData, string SOData)
        {
            string response = "";
            var js = new JavaScriptSerializer();
            List<Shipment> so_shipment = js.Deserialize<List<Shipment>>(ShipmentData);
            SalesOrder salesorder = js.Deserialize<SalesOrder>(SOData);
            SalesOrders AddSO = null;
            List<Shipments> so_shipments = new List<Shipments>();
            try
            {
                if (so_shipment.Count() < 0)
                {
                    response = "Please Add Products to Packing.";
                }
                else
                {
                    AddSO = new SalesOrders();
                    AddSO.SalesOrder_id = salesorder.SalesOrder_id;
                    //AddSO.SO_Package_Status = salesorder.SO_Package_Status;
                    //AddSO.SO_Status = salesorder.SO_Status;

                    int i = 1;
                    foreach (var dbr in so_shipment)
                    {
                        Shipments li = new Shipments();
                        li.SaleOrder_id = dbr.SaleOrder_id;
                        li.Shipment_No = dbr.Shipment_No;
                        li.Package_id = dbr.Package_id;
                        li.Shipment_Date = dbr.Shipment_Date;
                        li.Shipment_Cost = dbr.Shipment_Cost;
                        li.Shipment_Status = dbr.Shipment_Status;
                        so_shipments.Add(li);

                        i++;
                    }


                    so_shipments.TrimExcess();
                    int AddedBy = Convert.ToInt32(Session["UserId"]);
                    Shipments shipment = new Shipments();
                    Shipment shi = new Shipment();
                    new Catalog().InsertSOShipment(so_shipments, shipment, AddSO.SalesOrder_id, AddedBy);
                    shi.pFlag = shipment.pFlag;
                    shi.pDesc = shipment.pDesc;
                    if (shi.pFlag == "1")
                    {
                        Activity activity = new Activity();

                        activity.ActivityType_id = AddSO.SalesOrder_id;
                        activity.ActivityType = "Sales Order";
                        activity.ActivityName = "Shipment";
                        activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
            }
            catch (Exception e)
            {
                so_shipments = null;
                so_shipment = null;
                salesorder = null;
                ShipmentData = null;
                SOData = null;
            }

            return Json(new { Shipments = so_shipments, So = AddSO }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateShipmentDeliver(string Shipmentdata)
        {

            string response = "";
            try
            {
                var js = new JavaScriptSerializer();
                Shipments shipments = new Shipments();

                if (!string.IsNullOrEmpty(Shipmentdata))
                {
                    Shipment shipment = js.Deserialize<Shipment>(Shipmentdata);

                    shipments.Package_id = shipment.Package_id;
                    shipments.Shipment_Status = shipment.Shipment_Status;

                    new Catalog().UpdateShipmentDeliver(shipments);
                }
            }
            catch (Exception e)
            {
                response = "Internal Error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaleReturn()
        {
            return PartialView("SO_SaleReturn_partialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetItemsForSaleRetrurn(int id)
        {
            List<SalesOrders> itemspackg = new Catalog().SelectItemsForSaleRetrurn(id);

            List<SalesOrders> items = new List<SalesOrders>();

            foreach (var dbr in itemspackg)
            {
                SalesOrders li = new SalesOrders();
                li.ItemId = dbr.ItemId;
                li.ItemId = dbr.ItemId;
                li.ItemName = dbr.ItemName != null ? dbr.ItemName : "";
                li.Customer_id = dbr.Customer_id;
                li.Customer_Name = dbr.Customer_Name != null ? dbr.Customer_Name : "";
                li.Customer_Address = dbr.Customer_Address != null ? dbr.Customer_Address : "";
                li.Customer_Landline = dbr.Customer_Landline != null ? dbr.Customer_Landline : "";
                li.Customer_Mobile = dbr.Customer_Mobile != null ? dbr.Customer_Mobile : "";
                li.Customer_Email = dbr.Customer_Email != null ? dbr.Customer_Email : "";
                li.PriceUnit = dbr.PriceUnit;
                li.ItemQty = dbr.ItemQty;
                li.TotalItems = dbr.TotalItems;
                li.Packed_Qty = dbr.Packed_Qty;
                li.Package_id = dbr.Package_id;
                li.ReturnQty = dbr.ReturnQty;
                items.Add(li);
            }
            items.TrimExcess();
            return Json(new { SOItems = items }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertSaleReturn(string SaleReturnItems, string SaleReturndata)
        {
            string response = "";
            var js = new JavaScriptSerializer();
            List<SaleReturn> salereturn_item = js.Deserialize<List<SaleReturn>>(SaleReturnItems);
            List<SaleReturns> salereturn_items = new List<SaleReturns>();

            SaleReturn salereturn = js.Deserialize<SaleReturn>(SaleReturndata);
            SaleReturns salereturns = null;


            if (salereturn != null)
            {
                salereturns = new SaleReturns();
                salereturns.SaleOrder_id = salereturn.SaleOrder_id;
                salereturns.SaleReturn_Date = salereturn.SaleReturn_Date;
                salereturns.SaleReturn_Status = salereturn.SaleReturn_Status;
                salereturns.AddedBy = Convert.ToInt32(Session["UserId"]);
            }


            try
            {
                if (salereturn_item.Count() < 0)
                {
                    response = "Please Select Items For Sale Return.";
                }
                else
                {
                    int i = 1;
                    foreach (var dbr in salereturn_item)
                    {
                        SaleReturns li = new SaleReturns();
                        li.Package_id = dbr.Package_id;
                        li.Item_id = dbr.Item_id;
                        li.Return_Qty = dbr.Return_Qty;
                        li.ReturnQty_Cost = dbr.ReturnQty_Cost;
                        salereturn_items.Add(li);

                        i++;
                    }


                    salereturn_items.TrimExcess();

                    new Catalog().InsertSaleReturn(salereturn_items, salereturns);
                }
            }
            catch (Exception e)
            {

            }

            return Json(new { SaleReturnsItems = salereturn_item, SaleReturn = salereturn }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectSaleReturnsForSO(int id)
        {
            List<SaleReturns> salereturns = new Catalog().SelectSaleReturnsForSO(Convert.ToInt32(id));
            List<SaleReturn> salereturn = new List<SaleReturn>();

            if (salereturns != null)
            {
                foreach (var dbr in salereturns)
                {
                    SaleReturn li = new SaleReturn();
                    li.SaleReturn_id = dbr.SaleReturn_id;
                    li.SaleOrder_id = dbr.SaleOrder_id;
                    li.SaleReturnNo = dbr.SaleReturnNo;
                    li.SaleReturn_Date = dbr.SaleReturn_Date;
                    li.SaleReturn_Status = dbr.SaleReturn_Status;
                    li.TotalReturn_Cost = dbr.TotalReturn_Cost;
                    salereturn.Add(li);
                }
                salereturn.TrimExcess();
            }
            return Json(salereturn, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectAllSRItemsSO_id(int id)
        {
            List<SaleReturns> itemspackg = new Catalog().SelectAllSRItemsSO_id(id);

            List<SaleReturn> items = new List<SaleReturn>();

            foreach (var dbr in itemspackg)
            {
                SaleReturn li = new SaleReturn();
                li.SaleOrder_id = dbr.SaleOrder_id;
                li.Package_id = dbr.Package_id;
                li.SaleReturn_id = dbr.SaleReturn_id;
                li.Item_id = dbr.Item_id;
                li.Item_Name = dbr.Item_Name;
                li.PriceUnit = dbr.PriceUnit;
                li.ItemQty = dbr.ItemQty;
                li.Return_Qty = dbr.Return_Qty;
                li.Received_Qty = dbr.Received_Qty;
                items.Add(li);
            }
            items.TrimExcess();
            return Json(new { SOItems = items }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectSOItemsSOid(int id)
        {
            List<SalesOrder> items = null;
            try
            {
                List<SalesOrders> itemspackg = new Catalog().SelectAllSOItems(id);

                items = new List<SalesOrder>();

                foreach (var dbr in itemspackg)
                {
                    SalesOrder li = new SalesOrder();
                    li.ItemId = dbr.ItemId;
                    li.ItemQty = dbr.ItemQty;
                    items.Add(li);
                }
                items.TrimExcess();
            }
            catch (Exception e)
            {

            }
            return Json(new { SOItems = items }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnReceive()
        {
            return PartialView("ReceiveReturnedItems_partialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ReturnReceivingQty(string ReturnReceivingdata)
        {
            string response = "";
            var js = new JavaScriptSerializer();
            List<SaleReturn> salereturn_item = js.Deserialize<List<SaleReturn>>(ReturnReceivingdata);
            List<SaleReturns> salereturn_items = new List<SaleReturns>();
            try
            {
                if (salereturn_item.Count() < 0)
                {
                    response = "Please Select Items For Sale Return.";
                }
                else
                {
                    int i = 1;
                    foreach (var dbr in salereturn_item)
                    {
                        SaleReturns li = new SaleReturns();
                        li.SaleReturn_id = dbr.SaleReturn_id;
                        li.Package_id = dbr.Package_id;
                        li.Item_id = dbr.Item_id;
                        li.Return_Qty = dbr.Return_Qty;
                        li.Received_Qty = dbr.Received_Qty;
                        salereturn_items.Add(li);

                        i++;
                    }


                    salereturn_items.TrimExcess();

                    new Catalog().ReturnReceivingQty(salereturn_items);
                }
            }
            catch (Exception e)
            {

            }

            return Json(new { SaleReturnsItems = salereturn_item }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteItemFromSaleOrder(int sdid)
        {

            string response = "";
            try
            {
                new Catalog().DeleteItemFromSaleOrder(sdid);
            }
            catch (Exception e)
            {
                response = "Internal Error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            SalesOrder so = null;
            try
            {
                SalesOrders salesOrders = new Catalog().SelectSOById(id);
                so = new SalesOrder();
                if (salesOrders == null)
                {
                    response = "Please Select A Valid Sales Order";
                }
                else
                {
                    if (salesOrders.SO_Invoice_Status == "Invoiced" || salesOrders.SO_Shipment_Status == "True" || salesOrders.SO_Shipment_Status == "1" || salesOrders.SO_Package_Status == "True" || salesOrders.SO_Package_Status == "True" || salesOrders.Delete_Status == "Requested")
                    {
                        response = "Invalid Request";
                    }
                    else
                    {
                        SalesOrders salesOrder = new SalesOrders();
                        salesOrder.SalesOrder_id = id;

                        if (salesOrders.Enable == 1)
                        {
                            salesOrder.Enable = 0;
                        }
                        else
                        {
                            salesOrder.Enable = 1;
                        }

                        new Catalog().UpdatepSaleOrder(salesOrder);
                        so.Enable = salesOrder.Enable;
                        Activity activity = new Activity();
                        if (so.Enable == 1)
                        {
                            ActivityName = "Marked as Active";
                        }
                        else if (so.Enable == 0)
                        {
                            ActivityName = "Marked as Inactive";
                        }
                        activity.ActivityType_id = id;
                        activity.ActivityType = "Sales Order";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelSales(string DeleteSalesData)
        {
            string response = "";
            List<SalesOrders> salesOrders = null;
            List<SalesOrder> salesNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<SalesOrder> salesrequestedtoDelete = js.Deserialize<List<SalesOrder>>(DeleteSalesData);
                salesOrders = new List<SalesOrders>();
                activities = new List<Activity>();
                salesNotDelete = new List<SalesOrder>();

                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in salesrequestedtoDelete)
                {
                    SalesOrders li = new SalesOrders();
                    li.SalesOrder_id = dbr.SalesOrder_id;
                    li.SaleOrderNo = dbr.SaleOrderNo;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

                    salesOrders.Add(li);
                }
                salesOrders.TrimExcess();
                if (salesOrders.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(salesOrders);

                    StringBuilder fileContent = new StringBuilder();

                    foreach (var col in dt.Columns)
                    {
                        if (col.ToString() == "SaleOrderNo")
                        {
                            fileContent.Append(col.ToString() + ",");
                        }
                    }
                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

                    foreach (DataRow dr in dt.Rows)
                    {
                        //foreach (var column in dr.ItemArray.ElementAtOrDefault(1))
                        //{

                        fileContent.Append("\"" + dr.ItemArray.ElementAtOrDefault(2).ToString() + "\",");

                        //}
                        fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                    }
                    string filename = "Sales_Orders-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Sales_Orders/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Sales_Orders/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";


                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");



                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Sales Orders";
                    mailMessage.Body = "Following SAles Orders are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Sales";
                    string Result = new Catalog().DelSalesRequest(salesOrders, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (salesOrders != null)
                        {
                            foreach (var i in salesOrders)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.SalesOrder_id;
                                activity.ActivityType = "Sales Order";
                                activity.ActivityName = "Delete Requested";
                                activity.User_id = Convert.ToInt32(Session["UserId"]);
                                activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                                activities.Add(activity);
                            }

                            new ActivitiesClass().InsertActivity(activities);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response = e.ToString();
            }
            return Json(new { Response = response, Sales = salesOrders }, JsonRequestBehavior.AllowGet);
        }

    }
}