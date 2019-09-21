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
    [CustomAuthorize]
    public class APISalesOrderController : ApiController
    {
        #region SALESORDER
        [Route("api/APISalesOrder/GetAllSalesOrders")]
        [HttpGet]
        public IEnumerable<SalesOrder> GetAllSalesOrders()
        {
            List<SalesOrder> salesorder = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<SalesOrders> salesorders = new Catalog().AllSalesOrders(search.Option,search.Search, search.StartDate, search.EndDate, Convert.ToInt32(User_id));

                salesorder = new List<SalesOrder>();

                if (salesorders != null)
                {
                    foreach (var dbr in salesorders)
                    {
                        SalesOrder li = new SalesOrder();
                        li.SalesOrder_id = dbr.SalesOrder_id;
                        li.SaleOrderNo = dbr.SaleOrderNo;
                        li.Customer_id = dbr.Customer_id;
                        li.Customer_Name = dbr.Customer_Name;
                        li.TotalItems = dbr.TotalItems;
                        li.SO_Total_Amount = dbr.SO_Total_Amount;
                        li.SO_Status = dbr.SO_Status;
                        li.TotalItems = dbr.TotalItems;
                        li.SO_Invoice_Status = dbr.SO_Invoice_Status;
                        li.SO_Package_Status = dbr.SO_Package_Status;
                        li.SO_Shipment_Status = dbr.SO_Shipment_Status;
                        li.SO_DateTime = dbr.SO_DateTime;
                        li.Time_Of_Day = dbr.Time_Of_Day;
                        li.Date_Of_Day = dbr.Date_Of_Day;
                        li.Month_Of_Day = dbr.Month_Of_Day;
                        li.Year_Of_Day = dbr.Year_Of_Day;
                        li.AddedBy = dbr.AddedBy;
                        salesorder.Add(li);
                    }
                }
                salesorder.TrimExcess();

                return salesorder;
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

        [Route("api/APISalesOrder/AddSalesOrder")]
        [HttpPost]
        public Classes AddSalesOrder()
        {
            SalesOrder saleorder = null;
            List<SalesOrders> salesorderItems = null;
            Classes data = null;
            List<Activity> activities = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                activities = new List<Activity>();
                data = new Classes();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    data = js.Deserialize<Classes>(strJson);

                    salesorderItems = new List<SalesOrders>();
                    saleorder = new SalesOrder();
                    var User_id = HttpContext.Current.User.Identity.Name;

                    SalesOrders so = new SalesOrders();
                    so.SO_Total_Amount = data.saleOrders.SO_Total_Amount;
                    so.SO_Status = data.saleOrders.SO_Status;
                    so.SO_Invoice_Status = data.saleOrders.SO_Invoice_Status;
                    so.SO_Shipment_Status = data.saleOrders.SO_Shipment_Status;
                    so.SO_Package_Status = data.saleOrders.SO_Package_Status;
                    so.AddedBy = Convert.ToInt32(User_id);

                    foreach (var dbr in data.SOItems)
                    {
                        SalesOrders li = new SalesOrders();

                        Items item = new Catalog().SelectItem(Convert.ToInt32(dbr.ItemId), 1);

                        Contacts contact = new Catalog().SelectContact(Convert.ToInt32(dbr.Customer_id), 1, 0, 0, 1);

                        li.ItemId = dbr.ItemId;
                        li.Customer_id = dbr.Customer_id;
                        li.Quantity = dbr.Quantity;
                        li.PriceUnit = dbr.PriceUnit;
                        li.MsrmntUnit = dbr.MsrmntUnit;
                        salesorderItems.Add(li);

                        data.Activitys = new Activity();
                        data.Activitys.ActivityType_id = dbr.ItemId;
                        data.Activitys.ActivityType = "Item";
                        data.Activitys.ActivityName = "Sales Order";
                        data.Activitys.User_id = Convert.ToInt32(User_id);
                        data.Activitys.Icon = "fa fa-fw fa-floppy-o bg-blue";

                        activities.Add(data.Activitys);
                    }

                    salesorderItems.TrimExcess();
                    Purchases po = new Purchases();


                    new Catalog().AddSalesOrder(salesorderItems, so, 1, "1");
                    data.saleOrders.pFlag = so.pFlag;
                    data.saleOrders.pDesc = so.pDesc;
                    data.saleOrders.pSO_Output = so.pSO_Output;
                    if (data.saleOrders.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        activity.ActivityType_id = Convert.ToInt32(data.saleOrders.pSO_Output);
                        activity.ActivityType = "Sales Order";
                        activity.ActivityName = "Created";
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APISalesOrder/SaleOrderById")]
        [HttpGet]
        public Classes SaleOrderById()
        {
            SalesOrder saleorder = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                saleorder = js.Deserialize<SalesOrder>(strJson);
                Classes data = new Classes();


                SalesOrders salesorders = new Catalog().SelectSOById(saleorder.SalesOrder_id);
                List<SalesOrders> itemspackg = new Catalog().SelectAllSOItems(saleorder.SalesOrder_id);

                data.saleOrders = new SalesOrder();
                data.saleOrders.SalesOrder_id = salesorders.SalesOrder_id;
                data.saleOrders.SaleOrderNo = salesorders.SaleOrderNo;
                data.saleOrders.Date_Of_Day = salesorders.Date_Of_Day;
                data.saleOrders.Time_Of_Day = salesorders.Time_Of_Day;
                data.saleOrders.Month_Of_Day = salesorders.Month_Of_Day;
                data.saleOrders.Year_Of_Day = salesorders.Year_Of_Day;
                data.saleOrders.TotalItems = salesorders.TotalItems;
                data.saleOrders.SO_Total_Amount = salesorders.SO_Total_Amount;
                data.saleOrders.SO_Invoice_Status = salesorders.SO_Invoice_Status;
                data.saleOrders.SO_Package_Status = salesorders.SO_Package_Status;
                data.saleOrders.SO_Shipment_Status = salesorders.SO_Shipment_Status;
                data.saleOrders.SO_Status = salesorders.SO_Status;
                data.saleOrders.Customer_Name = salesorders.Customer_Name;
                data.saleOrders.Customer_id = salesorders.Customer_id;
                data.saleOrders.AddedBy = salesorders.AddedBy;

                data.SOItems = new List<SalesOrder>();
                if (itemspackg != null)
                {
                    foreach (var dbr in itemspackg)
                    {
                        SalesOrder li = new SalesOrder();
                        li.ItemId = dbr.ItemId;
                        li.ItemName = dbr.ItemName != null ? dbr.ItemName : "";
                        li.Customer_id = dbr.Customer_id;
                        li.Customer_Name = dbr.Customer_Name != null ? dbr.Customer_Name : "";
                        li.Customer_Address = dbr.Customer_Address != null ? dbr.Customer_Address : "";
                        li.Customer_Landline = dbr.Customer_Landline;
                        li.Customer_Mobile = dbr.Customer_Mobile;
                        li.Customer_Email = dbr.Customer_Email;
                        li.PriceUnit = dbr.PriceUnit;
                        li.ItemQty = dbr.ItemQty;
                        li.TotalItems = dbr.TotalItems;
                        data.SOItems.Add(li);
                    }
                    data.SOItems.TrimExcess();
                }


                return data;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //response.Content = new StringContent(JsonResp, Encoding.UTF8, "application/json");
            //return response;
        }
        #endregion

        #region INVOICE
        [Route("api/APISalesOrder/GetInvoice")]
        [HttpGet]
        public Classes GetInvoice()
        {
            SalesOrder saleorder = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                saleorder = js.Deserialize<SalesOrder>(strJson);
                Classes data = new Classes();


                SalesOrders salesorders = new Catalog().SelectSOById(saleorder.SalesOrder_id);
                List<SalesOrders> itemspackg = new Catalog().SelectAllSOItems(saleorder.SalesOrder_id);

                data.saleOrders = new SalesOrder();
                data.saleOrders.SalesOrder_id = salesorders.SalesOrder_id;
                data.saleOrders.SaleOrderNo = salesorders.SaleOrderNo;
                data.saleOrders.Date_Of_Day = salesorders.Date_Of_Day;
                data.saleOrders.Time_Of_Day = salesorders.Time_Of_Day;
                data.saleOrders.Month_Of_Day = salesorders.Month_Of_Day;
                data.saleOrders.Year_Of_Day = salesorders.Year_Of_Day;
                data.saleOrders.TotalItems = salesorders.TotalItems;
                data.saleOrders.SO_Total_Amount = salesorders.SO_Total_Amount;
                data.saleOrders.SO_Invoice_Status = salesorders.SO_Invoice_Status;
                data.saleOrders.SO_Package_Status = salesorders.SO_Package_Status;
                data.saleOrders.SO_Shipment_Status = salesorders.SO_Shipment_Status;
                data.saleOrders.SO_Status = salesorders.SO_Status;
                data.saleOrders.Customer_Name = salesorders.Customer_Name;
                data.saleOrders.Customer_id = salesorders.Customer_id;
                data.saleOrders.Customer_Name = salesorders.Customer_Name != null ? salesorders.Customer_Name : "";
                data.saleOrders.Customer_Address = salesorders.Customer_Address != null ? salesorders.Customer_Address : "";
                data.saleOrders.Customer_Landline = salesorders.Customer_Landline != null ? salesorders.Customer_Landline : "";
                data.saleOrders.Customer_Mobile = salesorders.Customer_Mobile != null ? salesorders.Customer_Mobile : "";
                data.saleOrders.Customer_Email = salesorders.Customer_Email != null ? salesorders.Customer_Email : "";
                data.saleOrders.AddedBy = salesorders.AddedBy;

                data.SOItems = new List<SalesOrder>();
                if (itemspackg != null)
                {
                    foreach (var dbr in itemspackg)
                    {
                        SalesOrder li = new SalesOrder();
                        li.ItemId = dbr.ItemId;
                        li.ItemName = dbr.ItemName != null ? dbr.ItemName : "";
                        li.Customer_id = dbr.Customer_id;

                        li.PriceUnit = dbr.PriceUnit;
                        li.ItemQty = dbr.ItemQty;
                        li.TotalItems = dbr.TotalItems;
                        li.Packed_Qty = dbr.Packed_Qty;
                        data.SOItems.Add(li);
                    }
                    data.SOItems.TrimExcess();
                }

                int ActivityType_id = saleorder.SalesOrder_id;
                string ActivityType = "Sales Order";
                data.Activity = new ActivitiesClass().Activities(ActivityType_id, ActivityType);

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //response.Content = new StringContent(JsonResp, Encoding.UTF8, "application/json");
            //return response;
        }

        [Route("api/APISalesOrder/AddInvoice")]
        [HttpPost]
        public Classes AddInvoice()
        {
            SO_Invoice so_invoice = null;
            SalesOrder salesorder = null;
            Classes data = null;
            try
            {
                data = new Classes();
                string strJson = new ApiRequestToJson().ToJson();

                //if (strJson != null)
                //{
                //    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                var js = new JavaScriptSerializer();
                data = js.Deserialize<Classes>(strJson);


                var User_id = HttpContext.Current.User.Identity.Name;
                SO_Invoices AddSO_Invoice = new SO_Invoices();

                AddSO_Invoice.id = data.Invoice.id;
                AddSO_Invoice.SalesOrder_id = data.Invoice.SalesOrder_id;
                AddSO_Invoice.Customer_id = data.Invoice.Customer_id;
                AddSO_Invoice.Invoice_No = data.Invoice.Invoice_No;
                AddSO_Invoice.Invoice_Status = data.Invoice.Invoice_Status;
                AddSO_Invoice.Invoice_Amount = data.Invoice.Invoice_Amount;
                AddSO_Invoice.Amount_Paid = data.Invoice.Amount_Paid;
                AddSO_Invoice.Balance_Amount = data.Invoice.Balance_Amount;
                AddSO_Invoice.InvoiceDateTime = data.Invoice.InvoiceDateTime;
                AddSO_Invoice.InvoiceDueDate = data.Invoice.InvoiceDueDate;
                AddSO_Invoice.AddedBy = Convert.ToInt32(User_id);

                SalesOrders AddSO = new SalesOrders();
                AddSO.SalesOrder_id = data.saleOrders.SalesOrder_id;
                AddSO.SO_Invoice_Status = data.saleOrders.SO_Invoice_Status;
                AddSO.SO_Status = data.saleOrders.SO_Status;

                new Catalog().InsertSOInvoice(AddSO_Invoice);


                data.Invoice.pFlag = AddSO_Invoice.pFlag;
                data.Invoice.pDesc = AddSO_Invoice.pDesc;
                data.Invoice.pInvoice_id_Output = AddSO_Invoice.pInvoice_id_Output;
                if (data.Invoice.pFlag == "1")
                {
                    new Catalog().UpdateSO_InvoiceStatus(AddSO);

                    Activity activity = new Activity();
                    List<Activity> activities = new List<Activity>();
                    activity.ActivityType_id = Convert.ToInt32(data.Invoice.SalesOrder_id);
                    activity.ActivityType = "Sales Order";
                    activity.ActivityName = "Invoice";
                    activity.User_id = Convert.ToInt32(User_id);
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PAYMENTS
        [Route("api/APISalesOrder/InsertPayment")]
        [HttpPost]
        public Classes InsertPayment()
        {
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    data = js.Deserialize<Classes>(strJson);

                    var User_id = HttpContext.Current.User.Identity.Name;
                    decimal Total = 0;
                    decimal Paid = 0;
                    decimal Balance = 0;


                    SO_Payments AddSO_Payment = new SO_Payments();
                    AddSO_Payment.Invoice_id = data.SOPayment.Invoice_id;

                    SO_Payments paymet = new Catalog().LastPaymentInvoice(data.SOPayment.Invoice_id);
                    if (paymet == null)
                    {
                        SO_Invoices invoices = new Catalog().InvoiceByInvoiceId(data.SOPayment.Invoice_id);
                        AddSO_Payment.Total_Amount = invoices.Invoice_Amount;
                        AddSO_Payment.Paid_Amount = data.SOPayment.Paid_Amount;
                        AddSO_Payment.Balance_Amount = invoices.Invoice_Amount - data.SOPayment.Paid_Amount;
                    }
                    else
                    {
                        Total = paymet.Balance_Amount;
                        Paid = data.SOPayment.Paid_Amount;
                        Balance = Total - Paid;

                        AddSO_Payment.Total_Amount = Total;
                        AddSO_Payment.Paid_Amount = Paid;
                        AddSO_Payment.Balance_Amount = Balance;

                    }

                    AddSO_Payment.Payment_Mode = data.SOPayment.Payment_Mode;
                    AddSO_Payment.Payment_Date = data.SOPayment.Payment_Date;

                    AddSO_Payment.AddedBy = Convert.ToInt32(User_id);

                    new Catalog().InsertSO_Payments(AddSO_Payment);

                    data.SOPayment.pFlag = AddSO_Payment.pFlag;
                    data.SOPayment.pDesc = AddSO_Payment.pDesc;

                    SO_Invoices AddSO_Invoices = new SO_Invoices();
                    AddSO_Invoices.id = data.SOPayment.Invoice_id;

                    if (paymet.Balance_Amount - data.SOPayment.Paid_Amount == 0)
                    {
                        AddSO_Invoices.Invoice_Status = "Paid";
                        new Catalog().UpdateinvoiceStatus(AddSO_Invoices);
                    }

                    if (data.SOPayment.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        List<Activity> activities = new List<Activity>();
                        activity.ActivityType_id = Convert.ToInt32(data.saleOrders.SalesOrder_id);
                        activity.ActivityType = "Sales Order";
                        activity.ActivityName = "Payment";
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APISalesOrder/InvoicePaymentHistory")]
        [HttpGet]
        public IEnumerable<SO_Payment> InvoicePaymentHistory()
        {
            SO_Payment payment = null;
            List<SO_Payment> paymentHistory = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                payment = js.Deserialize<SO_Payment>(strJson);
                Classes data = new Classes();


                List<SO_Payments> payments = new Catalog().SelectSO_IvnvoicePayment(Convert.ToInt32(payment.Invoice_id));
                paymentHistory = new List<SO_Payment>();
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
                        paymentHistory.Add(li);
                    }
                    paymentHistory.TrimExcess();
                }

                return paymentHistory;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //response.Content = new StringContent(JsonResp, Encoding.UTF8, "application/json");
            //return response;
        }
        #endregion

        #region PACKAGES
        [Route("api/APISalesOrder/InsertPackages")]
        [HttpPost]
        public Classes InsertPackages()
        {
            Classes data = null;
            List<Packages> so_packages = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    data = js.Deserialize<Classes>(strJson);

                    var User_id = HttpContext.Current.User.Identity.Name;
                    so_packages = new List<Packages>();
                    foreach (var dbr in data.PackageItems)
                    {
                        Packages li = new Packages();
                        li.SalesOrder_id = dbr.SalesOrder_id;
                        li.Package_Date = dbr.Package_Date;
                        li.PackageStatus = "Not Shipped";
                        li.PackageCost = dbr.PackageCost;
                        li.Item_id = dbr.Item_id;
                        li.PackageCost = dbr.PackageCost;
                        li.Packed_Qty = dbr.Packed_Qty;
                        so_packages.Add(li);
                    }
                    so_packages.TrimExcess();

                    SalesOrders AddSO = new SalesOrders();
                    AddSO.SalesOrder_id = data.saleOrders.SalesOrder_id;
                    Packages package = new Packages();
                    new Catalog().InsertSOPackage(so_packages, package, AddSO.SalesOrder_id, Convert.ToInt32(User_id));
                    data.package = new Package();
                    data.package.pFlag = package.pFlag;
                    data.package.pDesc = package.pDesc;
                    if (data.package.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        List<Activity> activities = new List<Activity>();
                        activity.ActivityType_id = Convert.ToInt32(data.saleOrders.SalesOrder_id);
                        activity.ActivityType = "Sales Order";
                        activity.ActivityName = "Package";
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APISalesOrder/SelectPackagesForSO")]
        [HttpGet]
        public IEnumerable<Package> SelectPackagesForSO()
        {
            SalesOrder saleorder = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                saleorder = js.Deserialize<SalesOrder>(strJson);
                Classes data = new Classes();


                List<Packages> packages = new Catalog().SelectPackagesForSO(Convert.ToInt32(saleorder.SalesOrder_id));

                List<SalesOrders> AllItems = new Catalog().SelectAllSOItems(Convert.ToInt32(saleorder.SalesOrder_id));

                if (packages != null)
                {
                    data.Packages = new List<Package>();
                    foreach (var dbr in packages)
                    {
                        Package li = new Package();
                        li.Package_id = dbr.Package_id;
                        li.SalesOrder_id = dbr.SalesOrder_id;
                        li.Package_No = dbr.Package_No;
                        li.Package_Date = dbr.Package_Date;
                        li.PackageStatus = dbr.PackageStatus;
                        data.Packages.Add(li);
                    }
                    data.Packages.TrimExcess();
                }

                return data.Packages;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //response.Content = new StringContent(JsonResp, Encoding.UTF8, "application/json");
            //return response;
        }
        #endregion

        #region SHIPMENTS
        [Route("api/APISalesOrder/InsertShipment")]
        [HttpPost]
        public Classes InsertShipment()
        {
            Classes data = null;
            List<Shipments> so_shipments = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    data = js.Deserialize<Classes>(strJson);

                    var User_id = HttpContext.Current.User.Identity.Name;
                    so_shipments = new List<Shipments>();
                    foreach (var dbr in data.Shipments)
                    {
                        Shipments li = new Shipments();
                        li.SaleOrder_id = dbr.SaleOrder_id;
                        li.Shipment_No = dbr.Shipment_No;
                        li.Package_id = dbr.Package_id;
                        li.Shipment_Date = dbr.Shipment_Date;
                        li.Shipment_Cost = dbr.Shipment_Cost;
                        li.Shipment_Status = "Shipped";
                        so_shipments.Add(li);
                    }
                    so_shipments.TrimExcess();

                    SalesOrders AddSO = new SalesOrders();
                    AddSO.SalesOrder_id = data.saleOrders.SalesOrder_id;
                    Shipments shipment = new Shipments();
                    new Catalog().InsertSOShipment(so_shipments, shipment, AddSO.SalesOrder_id, Convert.ToInt32(User_id));
                    data.Shipment = new Shipment();
                    data.Shipment.pFlag = shipment.pFlag;
                    data.Shipment.pDesc = shipment.pDesc;
                    data.Shipment.pShipementIdout = shipment.pShipementIdout;
                    if (data.package.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        List<Activity> activities = new List<Activity>();
                        activity.ActivityType_id = Convert.ToInt32(data.saleOrders.SalesOrder_id);
                        activity.ActivityType = "Sales Order";
                        activity.ActivityName = "Shipment";
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APISalesOrder/DeliverShipment")]
        [HttpPost]
        public Shipment DeliverShipment()
        {
            Shipments shipments = null;
            Shipment shipment = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    shipment = js.Deserialize<Shipment>(strJson);
                    shipments = new Shipments();
                    shipments.Package_id = shipment.Package_id;
                    Packages pkgs = new Catalog().PackageByPackageId(shipment.Package_id);

                    if (pkgs.PackageStatus == "Shipped")
                    {
                        shipments.Shipment_Status = "Delivered";
                    }
                    if (pkgs.PackageStatus == "Delivered")
                    {
                        shipments.Shipment_Status = "Shipped";
                    }
                    new Catalog().UpdateShipmentDeliver(shipments);
                    shipment.pFlag = shipments.pFlag;
                    shipment.pDesc = shipments.pDesc;
                }

                return shipment;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region SALE RETURN
        [Route("api/APISalesOrder/GetItemsForSaleRetrurn")]
        [HttpPost]
        public IEnumerable<SalesOrder> GetItemsForSaleRetrurn()
        {
            SalesOrder saleorder = null;
            List<SalesOrder> items = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                saleorder = js.Deserialize<SalesOrder>(strJson);
                List<SalesOrders> itemspackg = new Catalog().SelectItemsForSaleRetrurn(saleorder.SalesOrder_id);
                items = new List<SalesOrder>();
                foreach (var dbr in itemspackg)
                {
                    SalesOrder li = new SalesOrder();
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

                return items;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //response.Content = new StringContent(JsonResp, Encoding.UTF8, "application/json");
            //return response;
        }

        [Route("api/APISalesOrder/InsertSaleReturn")]
        [HttpPost]
        public Classes InsertSaleReturn()
        {
            Classes data = null;
            SaleReturns salereturns = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    data = js.Deserialize<Classes>(strJson);

                    var User_id = HttpContext.Current.User.Identity.Name;
                    List<SaleReturns> salereturn_items = new List<SaleReturns>();

                    foreach (var dbr in data.SaleReturnItems)
                    {
                        SaleReturns li = new SaleReturns();
                        li.Package_id = dbr.Package_id;
                        li.Item_id = dbr.Item_id;
                        Packages packageitem = new Catalog().PackageItemByItemId(dbr.Package_id, dbr.Item_id);

                        li.Return_Qty = dbr.Return_Qty;
                        li.ReturnQty_Cost = Convert.ToString(Convert.ToSingle(packageitem.Packed_Qty) * Convert.ToSingle(packageitem.price));
                        salereturn_items.Add(li);
                    }
                    salereturn_items.TrimExcess();

                    if (data.SaleReturn != null)
                    {
                        salereturns = new SaleReturns();
                        salereturns.SaleOrder_id = data.SaleReturn.SaleOrder_id;
                        salereturns.SaleReturn_Date = data.SaleReturn.SaleReturn_Date;
                        salereturns.SaleReturn_Status = "Approved";
                        salereturns.AddedBy = Convert.ToInt32(User_id);
                    }

                    new Catalog().InsertSaleReturn(salereturn_items, salereturns);
                    data.SaleReturn = new SaleReturn();
                    data.SaleReturn.pFlag = salereturns.pFlag;
                    data.SaleReturn.pDesc = salereturns.pDesc;
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APISalesOrder/SelectSaleReturnsForSO")]
        [HttpPost]
        public Classes SelectSaleReturnsForSO()
        {
            List<SaleReturn> salereturn = null;
            SalesOrder saleorder = null;
            Classes data = null;
            try
            {
                data = new Classes();
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                saleorder = js.Deserialize<SalesOrder>(strJson);
                List<SaleReturns> salereturns = new Catalog().SelectSaleReturnsForSO(Convert.ToInt32(saleorder.SalesOrder_id));

                salereturn = new List<SaleReturn>();
                if (salereturns != null)
                {
                    data.SaleReturns = new List<SaleReturn>();
                    foreach (var dbr in salereturns)
                    {
                        SaleReturn li = new SaleReturn();
                        li.SaleReturn_id = dbr.SaleReturn_id;
                        li.SaleOrder_id = dbr.SaleOrder_id;
                        li.SaleReturnNo = dbr.SaleReturnNo;
                        li.SaleReturn_Date = dbr.SaleReturn_Date;
                        li.SaleReturn_Status = dbr.SaleReturn_Status;
                        li.TotalReturn_Cost = dbr.TotalReturn_Cost;
                        data.SaleReturns.Add(li);
                    }
                    data.SaleReturns.TrimExcess();
                }
                List<SaleReturns> itemspackg = new Catalog().SelectAllSRItemsSO_id(saleorder.SalesOrder_id);
                if (itemspackg != null)
                {
                    data.SaleReturnItems = new List<SaleReturn>();
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
                        data.SaleReturnItems.Add(li);
                    }
                    data.SaleReturnItems.TrimExcess();
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APISalesOrder/ReturnReceivingQty")]
        [HttpPost]
        public Classes ReturnReceivingQty()
        {
            //List<SaleReturn> salereturn_item = null;
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    data = js.Deserialize<Classes>(strJson);

                    var User_id = HttpContext.Current.User.Identity.Name;

                    List<SaleReturns> salereturn_items = new List<SaleReturns>();

                    foreach (var dbr in data.SaleReturnItems)
                    {
                        SaleReturns li = new SaleReturns();
                        li.SaleReturn_id = dbr.SaleReturn_id;
                        li.Package_id = dbr.Package_id;
                        li.Item_id = dbr.Item_id;
                        SaleReturns sr = new Catalog().SaleReturnedItemyItem_id(dbr.Package_id, dbr.Item_id);
                        li.Return_Qty = sr.Return_Qty;
                        li.Received_Qty = dbr.Received_Qty;
                        salereturn_items.Add(li);
                    }
                    salereturn_items.TrimExcess();
                    new Catalog().ReturnReceivingQty(salereturn_items);
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
