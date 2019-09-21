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
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class BillController : Controller
    {
        // GET: Bill
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add()
        {
            return View("Bill");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllBills(string Search)
        {
            try
            {
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(Search);
                int User_id = Convert.ToInt32(Session["UserId"]);
                List<Bills> bills = new Catalog().AllBills(search.Search, search.StartDate, search.EndDate, User_id);

                List<Bill> bill = new List<Bill>();

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

                        bill.Add(li);
                    }
                }
                bill.TrimExcess();
                var pbills = bill.Skip(search.PageStart).Take(search.PageLength);
                return Json(new { draw = search.Draw, recordsTotal = bill.Count, recordsFiltered = bill.Count, data = pbills }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult BillInvoice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult BillInvoice(int id)
        {

            Bill bill = null;
            Bills bills = new Catalog().SelectBillById(id);
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
            Purchases purchases = new Catalog().SelectPurchase(bill.Purchase_id);
            List<Purchases> itemspackg = new Catalog().SelectAllPItems(bill.Purchase_id);

            List<Purchase> items = new List<Purchase>();

            if (purchases != null)
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
                    items.Add(li);
                }
            }
            items.TrimExcess();
            return Json(new { InvoiceNum = purchases.id, OrderNo = purchases.TempOrderNum, PremisesId = purchases.PremisesId, PremisesName = purchases.Premises_Name, UserId = purchases.UserId, User = purchases.AVendor_Name != null ? purchases.AVendor_Name.Name : "", Approved = purchases.Approved, AUserId = purchases.AUserId, AUser = purchases.ABVendor_Name != null ? purchases.ABVendor_Name.Name : "", Time = purchases.Time, Date = purchases.Date, UnitPrice = purchases.PriceUnit, TotalItems = purchases.TotalItems, TotalPrice = purchases.TotalPrice, ItemQty = purchases.ItemQty, RecieveStatus = purchases.RecieveStatus, BillStatus = purchases.BillStatus, RecieveDate = purchases.RecieveDateTime, PItems = items, pBill = bill }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertBill(string BillData)
        {
            var js = new JavaScriptSerializer();
            Bill bill = js.Deserialize<Bill>(BillData);

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
            Addbill.AddedBy = Convert.ToInt32(Session["UserId"]);

            new Catalog().InsertBill(Addbill);
            bill.pFlag = Addbill.pFlag;
            bill.pDesc = Addbill.pDesc;
            bill.pBill_id_Output = Addbill.pBill_id_Output;

            var result = JsonConvert.SerializeObject(Addbill);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BillPayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult BillPayment(int id)
        {

            Bill bill = null;
            Bills bills = new Catalog().SelectBillById(id);
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
            return Json(new { pBill = bill }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertPayment(string Paymentdata)
        {
            var js = new JavaScriptSerializer();
            Payment payment = js.Deserialize<Payment>(Paymentdata);

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
            if(payment.pFlag == "1")
            {
                int purchase_id = new Catalog().SelectBillById(payment.Bill_id).Purchase_id;
                Activity activity = new Activity();
                activity.ActivityName = "Payment";
                activity.ActivityType_id = purchase_id;
                activity.ActivityType = "Purchase Order";

                activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                List<Activity> activities = new List<Activity>();
                activities.Add(activity);


                new ActivitiesClass().InsertActivity(activities);
            }

            //var result = JsonConvert.SerializeObject(item);

            return Json(AddPayment, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateBillStatus(string BillData)
        {
            string response = "";
            try
            {
                var js = new JavaScriptSerializer();
                Bills bills = new Bills();

                if (!string.IsNullOrEmpty(BillData))
                {
                    Bill bill = js.Deserialize<Bill>(BillData);

                    bills.id = bill.id;
                    bills.Bill_Status = bill.Bill_Status;

                    new Catalog().UpdateBillStatus(bills);
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
        public JsonResult SelectPaymentByBillId(int id)
        {
            List<Payments> payments = new Catalog().SelectPaymentByBillId(Convert.ToInt32(id));
            List<Payment> payment = new List<Payment>();

            if (payments != null)
            {
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
            return Json(payment, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewPayments()
        {
            return View("ViewPayments");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ViewPayments(string Search)
        //{
        //    var js = new JavaScriptSerializer();
        //    SearchParameters search = js.Deserialize<SearchParameters>(Search);
        //    List<Payments> payments = new Catalog().ViewPayments(search.Option, search.Search, search.StartDate, search.EndDate);
        //    List<Payment> payment = new List<Payment>();

        //    if (payments != null)
        //    {
        //        foreach (var dbr in payments)
        //        {
        //            Payment li = new Payment();
        //            li.Payment_id = dbr.Payment_id;
        //            li.Bill_id = dbr.Bill_id;
        //            li.Bill_No = dbr.Bill_No;
        //            li.Payment_Mode = dbr.Payment_Mode;
        //            li.Payment_Date = dbr.Payment_Date;
        //            li.Total_Amount = dbr.Total_Amount;
        //            li.Paid_Amount = dbr.Paid_Amount;
        //            li.Balance_Amount = dbr.Balance_Amount;
        //            payment.Add(li);
        //        }
        //        payment.TrimExcess();
        //    }
        //    var ppaymets = payment.Skip(search.PageStart).Take(search.PageLength);
        //    return Json(new { draw = search.Draw, recordsTotal = payment.Count, recordsFiltered = payment.Count, data = ppaymets }, JsonRequestBehavior.AllowGet);
        //}
    }
}