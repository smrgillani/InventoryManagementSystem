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
    public class APIPurchaseController : ApiController
    {
        #region PURCHASES
        [Route("api/APIPurchase/GetAllPurchases")]
        [HttpGet]
        public IEnumerable<Purchase> GetAllPurchases()
        {
            List<Purchase> purchase = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                List<Purchases> purchases = new Catalog().AllPurchases(search.Option, search.Search, search.StartDate, search.EndDate, Convert.ToInt32(User_id));

                purchase = new List<Purchase>();

                if (purchases != null)
                {
                    foreach (var dbr in purchases)
                    {
                        Purchase li = new Purchase();
                        li.id = dbr.id;
                        li.TempOrderNum = dbr.TempOrderNum;
                        li.TotalItems = dbr.TotalItems;
                        li.TotalPrice = dbr.TotalPrice;
                        li.Approved = dbr.Approved;
                        li.RecieveStatus = dbr.RecieveStatus;
                        li.BillStatus = dbr.BillStatus;
                        li.DateOfDay = dbr.Date;
                        if (dbr.Premises_Name.Store == "1")
                        {
                            li.StoreId = dbr.Premises_Name.id;
                            li.StoreName = dbr.Premises_Name.Name;
                        }
                        else if (dbr.Premises_Name.Shop == "1")
                        {
                            li.ShopId = dbr.Premises_Name.id;
                            li.ShopName = dbr.Premises_Name.Name;
                        }
                        li.AddedBy = dbr.AddedBy;
                        li.DateOfDay = dbr.Date;
                        li.TimeOfDay = dbr.Time;
                        li.MonthOfDay = dbr.Month;
                        li.YearOfDay = dbr.Year;
                        li.TotalItems = dbr.TotalItems;
                        li.TotalPrice = dbr.TotalPrice;

                        purchase.Add(li);
                    }
                }
                purchase.TrimExcess();

                return purchase;
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

        [Route("api/APIPurchase/AddPurchase")]
        [HttpPost]
        public Purchase AddPurchase()
        {
            List<Purchase> purchase = null;
            Purchase purchaseorder = null;
            Classes data = null;
            List<Activity> activities = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                activities = new List<Activity>();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    purchase = js.Deserialize<List<Purchase>>(strJson);

                    List<Purchases> purchases = new List<Purchases>();
                    purchaseorder = new Purchase();
                    var User_id = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in purchase)
                    {
                        Purchases li = new Purchases();

                        Items item = new Catalog().SelectItem(Convert.ToInt32(dbr.ItemId), 1);

                        Contacts contact = new Catalog().SelectContact(Convert.ToInt32(dbr.VendorId), 1, 0, 0, 1);
                        data = new Classes();
                        data.Activitys = new Activity();
                        li.ItemId = dbr.ItemId;
                        li.VendorId = dbr.VendorId;
                        li.Quantity = dbr.Quantity;
                        li.PriceUnit = dbr.PriceUnit;
                        li.MsrmntUnit = dbr.MsrmntUnit;

                        data.Activitys.ActivityType_id = dbr.ItemId;
                        data.Activitys.ActivityType = "Item";
                        data.Activitys.ActivityName = "Purchase Order";
                        data.Activitys.User_id = Convert.ToInt32(User_id);
                        data.Activitys.Icon = "fa fa-fw fa-floppy-o bg-blue";

                        activities.Add(data.Activitys);

                        purchases.Add(li);
                    }

                    purchases.TrimExcess();
                    Purchases po = new Purchases();

                    new Catalog().AddPurchase(purchases, po, 1, "1", Convert.ToInt32(User_id));
                    purchaseorder.pFlag = po.pFlag;
                    purchaseorder.pDesc = po.pDesc;
                    purchaseorder.pPO_Output = po.pPO_Output;
                    if (purchaseorder.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        activity.ActivityType_id = Convert.ToInt32(purchaseorder.pPO_Output);
                        activity.ActivityType = "Purchase Order";
                        activity.ActivityName = "Created";
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return purchaseorder;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIPurchase/PurchaseOrderById")]
        [HttpPost]
        public Classes PurchaseOrderById()
        {
            Purchase purchase = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                purchase = js.Deserialize<Purchase>(strJson);
                Classes data = new Classes();


                Purchases purchases = new Catalog().SelectPurchase(purchase.id);
                Bills bills = new Catalog().SelectBillByPId(purchase.id);

                data.Purchases = new Purchase();
                data.Purchases.id = purchases.id;
                data.Purchases.TempOrderNum = purchases.TempOrderNum;
                data.Purchases.DateOfDay = purchases.Date;
                data.Purchases.TimeOfDay = purchases.Time;
                data.Purchases.MonthOfDay = purchases.Month;
                data.Purchases.YearOfDay = purchases.Year;
                data.Purchases.TotalItems = purchases.TotalItems;
                data.Purchases.TotalPrice = purchases.TotalPrice;
                data.Purchases.RecieveStatus = purchases.RecieveStatus;
                data.Purchases.BillStatus = purchases.BillStatus;
                data.Purchases.RecieveDateTime = purchases.RecieveDateTime;



                if (bills != null)
                {
                    data.Payments = new List<Payment>();
                    data.Bill = new Bill();
                    data.Bill.id = bills.id;
                    data.Bill.Order_No = bills.Order_No;
                    data.Bill.Purchase_id = bills.Purchase_id;
                    data.Bill.Bill_No = bills.Bill_No;
                    data.Bill.Bill_Status = bills.Bill_Status;
                    data.Bill.Bill_Amount = bills.Bill_Amount;
                    data.Bill.Amount_Paid = bills.Amount_Paid;
                    data.Bill.Balance_Amount = bills.Balance_Amount;
                    data.Bill.BillDateTime = bills.BillDateTime;
                    data.Bill.BillDueDate = bills.BillDueDate;
                    data.Bill.Time = bills.Time;
                    data.Bill.Date = bills.Date;
                    data.Bill.Month = bills.Month;
                    data.Bill.Year = bills.Year;
                    List<Payments> payments = new Catalog().SelectPaymentByBillId(Convert.ToInt32(bills.id));
                    foreach (var dbr in payments)
                    {
                        Payment li = new Payment();
                        li.Payment_id = dbr.Payment_id;
                        li.Bill_id = dbr.Bill_id;
                        li.Bill_No = dbr.Bill_No;
                        li.Payment_Mode = dbr.Payment_Mode;
                        li.Payment_Date = dbr.Payment_Date;
                        li.Total_Amount = dbr.Total_Amount;
                        li.Paid_Amount = dbr.Paid_Amount;
                        li.Balance_Amount = dbr.Balance_Amount;
                        data.Payments.Add(li);
                    }
                    data.Payments.TrimExcess();
                }




                List<Purchases> itemspackg = new Catalog().SelectAllPItems(purchase.id);


                data.Items = new List<Purchase>();
                if (itemspackg != null)
                {
                    foreach (var dbr in itemspackg)
                    {
                        Purchase li = new Purchase();
                        li.ItemId = dbr.ItemId;
                        li.ItemName = dbr.Item_Name != null ? dbr.Item_Name.Item_Name : "";
                        li.VendorId = dbr.VendorId;
                        li.VendorName = dbr.Vendor_Name != null ? dbr.Vendor_Name.Name : "";
                        li.VendorAddress = dbr.Vendor_Address != null ? dbr.Vendor_Address.Address : "";
                        li.VendorLandline = dbr.Vendor_Landline != null ? dbr.Vendor_Landline.AddressLandline : "";
                        li.VendorMobile = dbr.Vendor_Mobile != null ? dbr.Vendor_Mobile.Mobile : "";
                        li.VendorEmail = dbr.Vendor_Email != null ? dbr.Vendor_Email.Email : "";
                        li.PriceUnit = dbr.PriceUnit;
                        li.ItemQty = dbr.ItemQty;
                        li.TotalItems = dbr.TotalItems;
                        data.Items.Add(li);
                    }
                    data.Items.TrimExcess();
                }

                int ActivityType_id = purchase.id;
                string ActivityType = "Purchase Order";
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

        [Route("api/APIPurchase/PurchaseReceiving")]
        [HttpPost]
        public Purchase PurchaseReceiving()
        {
            Purchase purchase = null;
            Purchases purchases = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                if (strJson != null)
                {
                    var User_id = HttpContext.Current.User.Identity.Name;
                    purchase = js.Deserialize<Purchase>(strJson);
                    purchases = new Purchases();
                    purchases.id = purchase.id;
                    purchases.RecieveStatus = "Issued";
                    purchases.RecieveDateTime = purchase.RecieveDateTime;

                    new Catalog().UpdatePurchaseStatus(purchases);
                    Activity activity = new Activity();
                    activity.ActivityType_id = Convert.ToInt32(purchase.id);
                    activity.ActivityType = "Purchase Order";
                    activity.ActivityName = "Receiveing";
                    activity.User_id = Convert.ToInt32(User_id);
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);

                }

                return purchase;
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

        #region BILLS
        [Route("api/APIPurchase/GetAllBills")]
        [HttpPost]
        public IEnumerable<Bill> GetAllBills()
        {
            List<Bill> bill = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var User_id = HttpContext.Current.User.Identity.Name;
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                List<Bills> bills = new Catalog().AllBills(search.Search, search.StartDate, search.EndDate, Convert.ToInt32(User_id));

                bill = new List<Bill>();

                if (bills != null)
                {
                    foreach (var dbr in bills)
                    {
                        Bill li = new Bill();
                        li.id = dbr.id;
                        li.Purchase_id = dbr.Purchase_id;
                        li.Order_No = dbr.Order_No;
                        li.Bill_No = dbr.Bill_No;
                        li.Bill_Status = dbr.Bill_Status;
                        li.Bill_Amount = dbr.Bill_Amount;
                        li.Amount_Paid = dbr.Amount_Paid;
                        li.Balance_Amount = dbr.Balance_Amount;
                        li.BillDateTime = dbr.BillDateTime;
                        li.BillDueDate = dbr.BillDueDate;
                        li.Date = dbr.Date;
                        li.Time = dbr.Time;
                        li.Month = dbr.Month;
                        li.Year = dbr.Year;

                        bill.Add(li);
                    }
                }
                bill.TrimExcess();

                return bill;
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

        [Route("api/APIPurchase/AddBill")]
        [HttpPost]
        public Bill AddBill()
        {
            Bill bill = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    bill = js.Deserialize<Bill>(strJson);

                    Bills Addbill = new Bills();
                    Addbill.id = bill.id;
                    Addbill.Purchase_id = bill.Purchase_id;
                    Addbill.Bill_No = bill.Bill_No;
                    Addbill.Bill_Status = bill.Bill_Status;
                    Addbill.Bill_Amount = bill.Bill_Amount;
                    Addbill.Amount_Paid = bill.Amount_Paid;
                    Addbill.Balance_Amount = bill.Balance_Amount;
                    Addbill.BillDateTime = bill.BillDateTime;
                    Addbill.BillDueDate = bill.BillDueDate;

                    new Catalog().InsertBill(Addbill);
                    var User_id = HttpContext.Current.User.Identity.Name;
                    bill.pFlag = Addbill.pFlag;
                    bill.pDesc = Addbill.pDesc;
                    bill.pBill_id_Output = Addbill.pBill_id_Output;
                    if (bill.pFlag == "1")
                    {
                        Activity activity = new Activity();

                        activity.ActivityType_id = bill.Purchase_id;
                        activity.ActivityType = "Purchase Order";
                        activity.ActivityName = "Bill";
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return bill;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIPurchase/BillPayment")]
        [HttpPost]
        public Bill BillPayment()
        {
            Bill bill = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                bill = js.Deserialize<Bill>(strJson);

                Bills bills = new Catalog().SelectBillById(bill.id);

                if (bills != null)
                {
                    bill = new Bill();
                    bill.id = bills.id;
                    bill.Order_No = bills.Order_No;
                    bill.Purchase_id = bills.Purchase_id;
                    bill.Bill_No = bills.Bill_No;
                    bill.Bill_Status = bills.Bill_Status;
                    bill.Bill_Amount = bills.Bill_Amount;
                    bill.Amount_Paid = bills.Amount_Paid;
                    bill.Balance_Amount = bills.Balance_Amount;
                    bill.BillDateTime = bills.BillDateTime;
                    bill.BillDueDate = bills.BillDueDate;
                    bill.Time = bills.Time;
                    bill.Date = bills.Date;
                    bill.Month = bills.Month;
                    bill.Year = bills.Year;
                }


                return bill;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region PAYMENTS
        [Route("api/APIPurchase/InsertPayment")]
        [HttpPost]
        public Payment InsertPayment()
        {
            Payment payment = null;
            decimal Total = 0;
            decimal Paid = 0;
            decimal Balance = 0;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    payment = js.Deserialize<Payment>(strJson);

                    Payments AddPayment = new Payments();
                    AddPayment.Bill_id = payment.Bill_id;
                    AddPayment.Payment_Mode = payment.Payment_Mode;
                    AddPayment.Payment_Date = payment.Payment_Date;
                    AddPayment.Total_Amount = payment.Total_Amount;
                    AddPayment.Paid_Amount = payment.Paid_Amount;
                    AddPayment.Balance_Amount = payment.Balance_Amount;

                    new Catalog().InsertPayments(AddPayment);
                    payment.pFlag = AddPayment.pFlag;
                    payment.pDesc = AddPayment.pDesc;

                    Total = payment.Total_Amount;
                    Paid = payment.Paid_Amount;
                    Balance = payment.Balance_Amount;

                    if (payment.pFlag == "1")
                    {
                        Bills bills = new Bills();
                        bills.id = payment.Bill_id;
                        if (Total - Paid > 0)
                        {
                            bills.Bill_Status = "Partially Paid";
                        }
                        else if (Total - Paid == 0)
                        {
                            bills.Bill_Status = "Closed";
                        }
                        else
                        {
                            bills.Bill_Status = "Open";
                        }
                        new Catalog().UpdateBillStatus(bills);
                    }

                }
                return payment;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIPurchase/SelectPaymentsByBillId")]
        [HttpPost]
        public IEnumerable<Payment> SelectPaymentsByBillId()
        {
            Bill bill = null;
            List<Payment> payment = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                bill = js.Deserialize<Bill>(strJson);

                List<Payments> payments = new Catalog().SelectPaymentByBillId(Convert.ToInt32(bill.id));

                if (payments != null)
                {
                    payment = new List<Payment>();
                    foreach (var dbr in payments)
                    {
                        Payment li = new Payment();
                        li.Payment_id = dbr.Payment_id;
                        li.Bill_id = dbr.Bill_id;
                        li.Bill_No = dbr.Bill_No;
                        li.Payment_Mode = dbr.Payment_Mode;
                        li.Payment_Date = dbr.Payment_Date;
                        li.Total_Amount = dbr.Total_Amount;
                        li.Paid_Amount = dbr.Paid_Amount;
                        li.Balance_Amount = dbr.Balance_Amount;
                        payment.Add(li);
                    }
                    payment.TrimExcess();
                }

                return payment;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
