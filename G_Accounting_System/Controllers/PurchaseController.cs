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
using System.Data;
using G_Accounting_System.Code.Helpers;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class PurchaseController : Controller
    {
        //
        // GET: /Purchasing/
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
                return View("Purchase");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bill()
        {

            return PartialView("Bill_partialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecieveItems()
        {

            return PartialView("RecieveItems_partialView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddPurchase(string PurchaseData, int id)
        {
            var js = new JavaScriptSerializer();
            List<Purchase> purchase = null;
            List<Purchases> purchases = new List<Purchases>();
            Purchase purchaseorder = new Purchase();
            Activity activity = null;
            string response = "";
            List<Activity> activities = null;
            try
            {
                activity = new Activity();
                activities = new List<Activity>();
                purchase = js.Deserialize<List<Purchase>>(PurchaseData);
                if (purchase.Count() < 0)
                {
                    response = "Please Add Purchased Products.";
                }
                else
                {

                    int iv = 0;
                    int jv = 0;
                    int ii = 0;
                    int vendor_id = purchase[0].VendorId;
                    foreach (var dbr in purchase)
                    {
                        if (dbr.VendorId != vendor_id)
                        {
                            iv += 1;
                        }

                    }
                    if (iv == 0)
                    {
                        foreach (var dbr in purchase)
                        {
                            Contacts contact = new Catalog().SelectContact(Convert.ToInt32(dbr.VendorId), 1, 0, 0, 1);
                            if (contact == null)
                            {
                                jv += 1;
                            }
                        }
                    }
                    if (iv == 0 && jv == 0)
                    {
                        foreach (var dbr in purchase)
                        {
                            Items item = new Catalog().SelectItem(Convert.ToInt32(dbr.ItemId), 1);
                            if (item == null)
                            {
                                //Do Nothing Here
                                ii += 1;
                            }
                        }
                    }
                    if (iv == 0 & jv == 0 && ii == 0)
                    {
                        foreach (var dbr in purchase)
                        {
                            Purchases li = new Purchases();
                            li.pdid = dbr.pdid;
                            li.ItemId = dbr.ItemId;
                            li.VendorId = dbr.VendorId;
                            li.Quantity = dbr.Quantity;
                            li.PriceUnit = dbr.PriceUnit;
                            li.MsrmntUnit = dbr.MsrmntUnit;

                            purchases.Add(li);
                            if (dbr.pdid == 0)
                            {
                                activity.ActivityType_id = dbr.ItemId;
                                activity.ActivityType = "Item";
                                activity.ActivityName = "Purchase Order";
                                activity.User_id = Convert.ToInt32(Session["UserId"]);
                                activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                                activities.Add(activity);
                            }
                        }

                        purchases.TrimExcess();
                        Purchases po = new Purchases();
                        po.id = id;
                        if (response.Length <= 0)
                        {
                            if (po.id == 0)
                            {
                                po.AddedBy = Convert.ToInt32(Session["UserId"]);
                                po.UpdatedBy = 0;
                            }
                            else
                            {
                                po.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                                po.AddedBy = 0;
                            }

                            int AddedBy = Convert.ToInt32(Session["UserId"]);
                            int Premises_id = Convert.ToInt32(Session["Premises_id"]);
                            string Result = new Catalog().AddPurchase(purchases, po, Premises_id, "1", AddedBy);
                            purchaseorder.pFlag = po.pFlag;
                            purchaseorder.pDesc = po.pDesc;
                            purchaseorder.pPO_Output = po.pPO_Output;
                            if (purchaseorder.pFlag == "1")
                            {
                                activity = new Activity();
                                if (po.id == 0)
                                {
                                    activity.ActivityName = "Created";
                                    activity.ActivityType_id = Convert.ToInt32(purchaseorder.pPO_Output);
                                }
                                else
                                {
                                    activity.ActivityName = "Updated";
                                    activity.ActivityType_id = po.id;
                                }
                                activity.ActivityType = "Purchase Order";
                                activity.User_id = Convert.ToInt32(Session["UserId"]);
                                activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                                activities.Add(activity);


                                new ActivitiesClass().InsertActivity(activities);
                            }
                        }
                    }
                    else
                    {
                        purchaseorder.pFlag = "0";
                        purchaseorder.pDesc = "Invalid Data Povided";
                    }
                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(purchaseorder, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllPurchases(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            int User_id = Convert.ToInt32(Session["UserId"]);
            List<Purchases> purchases = new Catalog().AllPurchases(search.Option, search.Search, search.StartDate, search.EndDate, User_id);

            List<Purchase> purchase = new List<Purchase>();

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
                    li.Bill_Stat = dbr.Bill_Stat;
                    li.Rec_Stat = dbr.Rec_Stat;
                    li.DateOfDay = dbr.Date;
                    li._Enable = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    purchase.Add(li);
                }
            }
            purchase.TrimExcess();
            var ppurchase = purchase.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = purchase.Count, recordsFiltered = purchase.Count, data = ppurchase }, JsonRequestBehavior.AllowGet);
        }

        [PermissionsAuthorize]
        public ActionResult Invoice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Invoice(int id)
        {
            Purchases purchases = null;
            List<Purchase> items = null;
            Bill bill = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    purchases = new Catalog().SelectPurchase(id);
                    Bills bills = new Catalog().SelectBillByPId(id);
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

                    List<Purchases> itemspackg = new Catalog().SelectAllPItems(id);

                    items = new List<Purchase>();

                    if (purchases != null)
                    {
                        foreach (var dbr in itemspackg)
                        {
                            Purchase li = new Purchase();
                            li.pdid = dbr.pdid;
                            li.ItemId = dbr.ItemId;
                            li.ItemName = dbr.Item_Name != null ? dbr.Item_Name.Item_Name : "";
                            li.VendorId = dbr.VendorId;
                            li.VendorName = dbr.Vendor_Name != null ? dbr.Vendor_Name.Name : "";
                            li.VendorAddress = dbr.Vendor_Address != null ? dbr.Vendor_Address.Address : "";
                            li.VendorLandline = dbr.Vendor_Landline != null ? dbr.Vendor_Landline.AddressLandline : "";
                            li.VendorMobile = dbr.Vendor_Mobile != null ? dbr.Vendor_Mobile.Mobile : "";
                            li.VendorEmail = dbr.Vendor_Email != null ? dbr.Vendor_Email.Email : "";
                            li.PriceUnit = dbr.PriceUnit;
                            li.MsrmntUnit = dbr.Unit;
                            li.ItemQty = dbr.ItemQty;
                            li.TotalItems = dbr.TotalItems;
                            items.Add(li);
                        }
                    }
                    items.TrimExcess();
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(new { InvoiceNum = purchases.id, OrderNo = purchases.TempOrderNum, PremisesId = purchases.PremisesId, PremisesName = purchases.Premises_Name, UserId = purchases.UserId, User = purchases.AVendor_Name != null ? purchases.AVendor_Name.Name : "", Approved = purchases.Approved, AUserId = purchases.AUserId, AUser = purchases.ABVendor_Name != null ? purchases.ABVendor_Name.Name : "", Time = purchases.Time, Date = purchases.Date, UnitPrice = purchases.PriceUnit, TotalItems = purchases.TotalItems, TotalPrice = purchases.TotalPrice, ItemQty = purchases.ItemQty, RecieveStatus = purchases.RecieveStatus, BillStatus = purchases.BillStatus, Bill_Stat = purchases.Bill_Stat, Rec_Stat = purchases.Rec_Stat, RecieveDate = purchases.RecieveDateTime, PItems = items, pBill = bill }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { InvoiceNum = purchases.id, OrderNo = purchases.TempOrderNum, PremisesId = purchases.PremisesId, PremisesName = purchases.Premises_Name, UserId = purchases.UserId, User = purchases.AVendor_Name != null ? purchases.AVendor_Name.Name : "", Approved = purchases.Approved, AUserId = purchases.AUserId, AUser = purchases.ABVendor_Name != null ? purchases.ABVendor_Name.Name : "", Time = purchases.Time, Date = purchases.Date, UnitPrice = purchases.PriceUnit, TotalItems = purchases.TotalItems, TotalPrice = purchases.TotalPrice, ItemQty = purchases.ItemQty, RecieveStatus = purchases.RecieveStatus, BillStatus = purchases.BillStatus, Bill_Stat = purchases.Bill_Stat, Rec_Stat = purchases.Rec_Stat, RecieveDate = purchases.RecieveDateTime, PItems = items, pBill = bill }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string response = "";
            Purchase pur = null;
            string ActivityName = null;
            try
            {
                Purchases purchases = new Catalog().SelectPurchase(id);
                pur = new Purchase();
                if (purchases == null)
                {
                    response = "Please Select A Valid Purchase Order";
                }
                else
                {
                    if (purchases.Bill_Stat == "True" || purchases.Rec_Stat == "True" || purchases.Delete_Status == "Requested")
                    {
                        response = "Invalid Request";
                    }
                    else
                    {
                        Purchases Purchase = new Purchases();
                        Purchase.id = id;

                        if (purchases.Enable == 1)
                        {
                            Purchase.Enable = 0;
                        }
                        else
                        {
                            Purchase.Enable = 1;
                        }
                        new Catalog().UpdatepPurchase(Purchase);
                        pur.Enable = Purchase.Enable;
                        Activity activity = new Activity();
                        if (pur.Enable == 1)
                        {
                            ActivityName = "Marked as Active";
                        }
                        else if (pur.Enable == 0)
                        {
                            ActivityName = "Marked as Inactive";
                        }
                        activity.ActivityType_id = id;
                        activity.ActivityType = "Purchase Order";
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
        public JsonResult SelectItemsForBillByPOid(int id)
        {
            List<Purchases> AllItems = new Catalog().SelectItemsForBillByPOid(id);

            List<Purchase> billitem = new List<Purchase>();
            foreach (var item in AllItems)
            {
                Purchase li = new Purchase();
                li.id = item.id;
                li.ItemId = item.ItemId;
                li.ItemName = item.Item_Name != null ? item.Item_Name.Item_Name : "";
                li.VendorId = item.VendorId;
                li.VendorName = item.Vendor_Name != null ? item.Vendor_Name.Name : "";
                li.VendorAddress = item.Vendor_Address != null ? item.Vendor_Address.Address : "";
                li.VendorLandline = item.Vendor_Landline != null ? item.Vendor_Landline.AddressLandline : "";
                li.VendorMobile = item.Vendor_Mobile != null ? item.Vendor_Mobile.Mobile : "";
                li.VendorEmail = item.Vendor_Email != null ? item.Vendor_Email.Email : "";
                li.PriceUnit = item.PriceUnit;
                li.ItemQty = item.ItemQty;
                li.TotalItems = item.TotalItems;
                billitem.Add(li);
            }
            billitem.TrimExcess();
            getBillNo();
            return Json(new { BillItems = billitem, NewBillNo = getBillNo() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePurchaseStatus(string PurchaseData)
        {
            string ActivityName = null;
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
                    purchases.Bill_Stat = purchase.Bill_Stat;
                    purchases.Rec_Stat = purchase.Rec_Stat;


                    new Catalog().UpdatePurchaseStatus(purchases);

                    Activity activity = new Activity();
                    if (purchases.Rec_Stat == "True")
                    {
                        activity.ActivityName = "Receiveing";
                    }
                    if (purchases.Bill_Stat == "True")
                    {
                        activity.ActivityName = "Bill";
                    }
                    activity.ActivityType_id = purchases.id;
                    activity.ActivityType = "Purchase Order";

                    activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
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
        public JsonResult DeleteItemFromPurchase(int pdid)
        {

            string response = "";
            try
            {
                new Catalog().DeleteItemFromPurchase(pdid);
            }
            catch (Exception e)
            {
                response = "Internal Error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelPurchasing(string DeletePurchasingData)
        {
            string response = "";
            List<Purchases> purchases = null;
            List<Purchase> purchasesNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Purchase> purchaserequestedtoDelete = js.Deserialize<List<Purchase>>(DeletePurchasingData);
                purchases = new List<Purchases>();
                activities = new List<Activity>();
                purchasesNotDelete = new List<Purchase>();

                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in purchaserequestedtoDelete)
                {
                    Purchases li = new Purchases();
                    li.id = dbr.id;
                    li.TempOrderNum = dbr.TempOrderNum;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

                    purchases.Add(li);
                }
                purchases.TrimExcess();
                if (purchases.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(purchases);

                    StringBuilder fileContent = new StringBuilder();

                    foreach (var col in dt.Columns)
                    {
                        if (col.ToString() == "TempOrderNum")
                        {
                            fileContent.Append(col.ToString() + ",");
                        }
                    }
                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

                    foreach (DataRow dr in dt.Rows)
                    {
                        //foreach (var column in dr.ItemArray.ElementAtOrDefault(1))
                        //{

                        fileContent.Append("\"" + dr.ItemArray.ElementAtOrDefault(10).ToString() + "\",");

                        //}
                        fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                    }
                    string filename = "Purchase_Orders-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Purchase_Orders/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Purchase_Orders/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";


                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");



                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Purchase Orders";
                    mailMessage.Body = "Following Purchase Orders are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Purchases";
                    string Result = new Catalog().DelPurchasingRequest(purchases, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (purchases != null)
                        {
                            foreach (var i in purchases)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Purchase Order";
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
            return Json(new { Response = response, Purchases = purchases }, JsonRequestBehavior.AllowGet);
        }

        public string getBillNo()
        {
            string newBillNo = null;
            string lastbillNo = null;

            lastbillNo = new Catalog().getLastBillNo();
            if (lastbillNo == null)
            {
                newBillNo = "BN-1";
            }
            else
            {
                newBillNo = lastbillNo.Substring(lastbillNo.IndexOf('-') + 1);
                newBillNo = "BN-" + Convert.ToString(Convert.ToInt32(newBillNo) + 1);
            }
            return newBillNo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PreviousOrders(int ItemId, int VendorId)
        {
            string Response = null;
            List<Purchase> purchase = null;
            try
            {
                List<Purchases> purchases = new Catalog().PreviousOrders(ItemId, VendorId);
                purchase = new List<Purchase>();
                if (purchases != null)
                {
                    purchase = new List<Purchase>();
                    foreach (var dbr in purchases)
                    {
                        Purchase li = new Purchase();
                        li.id = dbr.id;
                        li.TempOrderNum = dbr.TempOrderNum;
                        li.ItemId = dbr.ItemId;
                        li.ItemName = dbr.ItemName;
                        li.VendorId = dbr.VendorId;
                        li.VendorName = dbr.VendorName;
                        li.PriceUnit = dbr.PriceUnit;
                        li.ItemQty = dbr.ItemQty;
                        purchase.Add(li);
                    }


                }
                purchase.TrimExcess();
            }
            catch (Exception e)
            {

            }
            return Json(purchase, JsonRequestBehavior.AllowGet);
        }

    }
}
