using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G_Accounting_System.Models;
using System.Web.Script.Serialization;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using System.Data;
using G_Accounting_System.Code.Helpers;
using System.Text;
using System.IO;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Net;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class VendorController : Controller
    {
        //
        // GET: /Vendor/
        [PermissionsAuthorize]
        public ActionResult Index(int? id)
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
                return View("Vendor");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateVendor(string Vendor)
        {
            string response = "";
            Contact vendor = null;
            string ActivityName = null;
            try
            {

                var js = new JavaScriptSerializer();
                vendor = js.Deserialize<Contact>(Vendor);

                if (string.IsNullOrEmpty(vendor.Name))
                {
                    response = "Enter Vendor Full Name In General Details.";
                }
                else if (string.IsNullOrEmpty(vendor.Landline))
                {
                    response = "Enter Landline Number Of Vendor In General Details.";
                }
                else if (string.IsNullOrEmpty(vendor.Mobile))
                {
                    response = "Enter Mobile Number Of Vendor In General Details.";
                }
                else if (string.IsNullOrEmpty(vendor.Address))
                {
                    response = "Enter Office Location Address Of Vendor In Address Details.";
                }
                else
                {

                    Contacts AddVendor = new Contacts();
                    AddVendor.Salutation = (vendor.Salutation.Equals("1") || vendor.Salutation.Equals("2") || vendor.Salutation.Equals("3") || vendor.Salutation.Equals("4") || vendor.Salutation.Equals("5")) ? vendor.Salutation : "1";
                    AddVendor.id = vendor.id;
                    AddVendor.Name = vendor.Name;
                    AddVendor.CompanyId = vendor.CompanyId;
                    AddVendor.Designation = vendor.Designation;
                    AddVendor.Landline = vendor.Landline;
                    AddVendor.Mobile = vendor.Mobile;
                    AddVendor.Email = vendor.Email;
                    AddVendor.Website = vendor.Website;
                    AddVendor.Address = vendor.Address;
                    AddVendor.AddressLandline = vendor.AddressLandline;
                    AddVendor.City = vendor.City;
                    AddVendor.Country = vendor.Country;
                    AddVendor.BankAccountNumber = vendor.BankAccountNumber;
                    AddVendor.PaymentMethod = (vendor.PaymentMethod.Equals("1") || vendor.PaymentMethod.Equals("2") || vendor.PaymentMethod.Equals("3")) ? vendor.PaymentMethod : "1";
                    AddVendor.Enable = vendor.IsEnabled.Equals("1") ? 1 : 0;
                    AddVendor.Vendor = 1;
                    AddVendor.Customer = 0;
                    AddVendor.Employee = 0;

                    Countries countries = new Catalog().SelectCountry(Convert.ToInt32(vendor.Country), null);
                    Cities citites = new Catalog().SelectCity(Convert.ToInt32(vendor.City), null);
                    Companies companies = new Catalog().SelectCompany(vendor.CompanyId, null);
                    if (countries != null && citites != null && companies != null)
                    {
                        if (vendor.id == 0)
                        {
                            AddVendor.AddedBy = Convert.ToInt32(Session["UserId"]);
                            AddVendor.UpdatedBy = 0;
                        }
                        else
                        {
                            AddVendor.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                            AddVendor.AddedBy = 0;
                        }
                        new Catalog().InsertUpdateContacts(AddVendor);
                        vendor.pFlag = AddVendor.pFlag;
                        vendor.pDesc = AddVendor.pDesc;
                        vendor.pContactid_Out = AddVendor.pContactid_Out;
                        if (vendor.pFlag == "1")
                        {
                            Activity activity = new Activity();
                            if (vendor.id == 0)
                            {
                                ActivityName = "Created";
                                activity.ActivityType_id = Convert.ToInt32(vendor.pContactid_Out);
                            }
                            else
                            {
                                ActivityName = "Updated";
                                activity.ActivityType_id = vendor.id;
                            }
                            activity.ActivityType = "Vendor";
                            activity.ActivityName = ActivityName;
                            activity.User_id = Convert.ToInt32(Session["UserId"]);
                            activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                            List<Activity> activities = new List<Activity>();
                            activities.Add(activity);


                            new ActivitiesClass().InsertActivity(activities);
                        }
                        return Json(vendor, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        vendor.pFlag = "0";
                        vendor.pDesc = "Provided Data is Incorrect";
                        return Json(vendor, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                vendor.pFlag = "0";
                vendor.pDesc = e.Message;
                return Json(vendor, JsonRequestBehavior.AllowGet);
            }

            return Json(vendor, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelContact(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllVendors(int? id, string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Contacts> vendor = new Catalog().AllVendors(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Contact> vendors = new List<Contact>();

            if (vendor != null)
            {
                foreach (var dbr in vendor)
                {
                    Contact li = new Contact();
                    li.id = dbr.id;
                    li.Name = (dbr.Salutation.Equals("1") ? "Mr." : "") + (dbr.Salutation.Equals("2") ? "Mrs." : "") + (dbr.Salutation.Equals("3") ? "Ms." : "") + (dbr.Salutation.Equals("4") ? "Miss" : "") + (dbr.Salutation.Equals("5") ? "Dr." : "") + " " + dbr.Name;
                    li.CompanyId = dbr.CompanyId;
                    li.Company = new Catalog().SelectCompany(li.CompanyId, null).Name;
                    li.Landline = dbr.Landline;
                    li.Mobile = dbr.Mobile;
                    li.Email = dbr.Email;
                    li.BankAccountNumber = dbr.BankAccountNumber;
                    li.PaymentMethod = (dbr.PaymentMethod.Equals("1") ? "Cheque" : "") + (dbr.PaymentMethod.Equals("2") ? "Cash" : "") + (dbr.PaymentMethod.Equals("3") ? "Card" : "");
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Time = dbr.Time;
                    li.Date = dbr.Date;
                    li.Month = dbr.Month;
                    li.Year = dbr.Year;
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    vendors.Add(li);
                }
            }

            vendors.TrimExcess();
            var pvendors = vendors.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = vendors.Count, recordsFiltered = vendors.Count, data = pvendors }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllVendorsFS()
        {
            List<Contacts> vendor = new Catalog().AllVendors(null, null, null, null);

            List<Contact> vendors = new List<Contact>();

            if (vendor != null)
            {
                foreach (var dbr in vendor)
                {
                    Contact li = new Contact();
                    li.id = dbr.id;
                    li.Name = (dbr.Salutation.Equals("1") ? "Mr." : "") + (dbr.Salutation.Equals("2") ? "Mrs." : "") + (dbr.Salutation.Equals("3") ? "Ms." : "") + (dbr.Salutation.Equals("4") ? "Miss" : "") + (dbr.Salutation.Equals("5") ? "Dr." : "") + " " + dbr.Name;
                    vendors.Add(li);
                }
            }

            vendors.TrimExcess();

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        [PermissionsAuthorize]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(int id)
        {
            string Response = null;
            try
            {
                Contacts vendors = new Catalog().SelectContact(Convert.ToInt32(id), 1, 0, 0, null);
                if (vendors != null)
                {
                    Contact vendor = new Contact();
                    vendor.id = vendors.id;
                    vendor.Salutation = vendors.Salutation;
                    vendor.Name = vendors.Name;
                    vendor.Full_name_ = (vendors.Salutation.Equals("1") ? "Mr." : "") + (vendors.Salutation.Equals("2") ? "Mrs." : "") + (vendors.Salutation.Equals("3") ? "Ms." : "") + (vendors.Salutation.Equals("4") ? "Miss" : "") + (vendors.Salutation.Equals("5") ? "Dr." : "") + " " + vendors.Name;
                    vendor.CompanyId = vendors.CompanyId;
                    vendor.Company = new Catalog().SelectCompany(vendors.CompanyId, null).Name;
                    vendor.Designation = vendors.Designation;
                    vendor.Landline = vendors.Landline;
                    vendor.Mobile = vendors.Mobile;
                    vendor.Email = vendors.Email;
                    vendor.Website = vendors.Website;
                    vendor.Address = vendors.Address;
                    vendor.AddressLandline = vendors.AddressLandline;

                    vendor.City = vendors.City;
                    vendor.CityName = new Catalog().SelectCity(Convert.ToInt32(vendors.City), null).Name;
                    vendor.Country = vendors.Country;
                    vendor.CountryName = new Catalog().SelectCountry(Convert.ToInt32(vendors.Country), null).Name;
                    vendor.BankAccountNumber = vendors.BankAccountNumber;
                    vendor.PaymentMethod = vendors.PaymentMethod;
                    vendor.Payment_method_ = (vendors.PaymentMethod.Equals("1") ? "Cheque" : "") + (vendors.PaymentMethod.Equals("2") ? "Cash" : "") + (vendors.PaymentMethod.Equals("3") ? "Card" : "");
                    vendor.IsEnabled = vendors.Enable.ToString();
                    vendor.IsEnabled_ = (vendors.Enable == 1) ? "Active" : "InActive";
                    vendor.Delete_Status = vendors.Delete_Status;
                    return Json(vendor, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response = "Provided Data is Incorrect";
                    return Json(Response, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Response = e.Message;
                return Json(Response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(int id)
        {
            Contact vendor = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    Contacts vendors = new Catalog().SelectContact(Convert.ToInt32(id), 1, 0, 0, null);

                    vendor = new Contact();
                    vendor.id = vendors.id;
                    vendor.Salutation = vendors.Salutation;
                    vendor.Name = vendors.Name;
                    vendor.CompanyId = vendors.CompanyId;
                    vendor.Designation = vendors.Designation;
                    vendor.Landline = vendors.Landline;
                    vendor.Mobile = vendors.Mobile;
                    vendor.Email = vendors.Email;
                    vendor.Website = vendors.Website;
                    vendor.Address = vendors.Address;
                    vendor.AddressLandline = vendors.AddressLandline;
                    vendor.City = vendors.City;
                    vendor.Country = vendors.Country;
                    vendor.BankAccountNumber = vendors.BankAccountNumber;
                    vendor.PaymentMethod = vendors.PaymentMethod;
                    vendor.IsEnabled = vendors.Enable.ToString();
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(vendor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(vendor, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Contact ven = null;
            try
            {
                Contacts contacts = new Catalog().SelectContact(id, 1, 0, 0, null);
                ven = new Contact();
                if (contacts == null)
                {
                    response = "Please Select A Valid Vendor";
                }
                else
                {
                    Contacts contact = new Contacts();
                    contact.id = id;

                    if (contacts.Enable == 1)
                    {
                        contact.Enable = 0;
                    }
                    else
                    {
                        contact.Enable = 1;
                    }

                    new Catalog().UpdatepContact(contact);
                    ven.IsEnabled = contact.Enable.ToString();
                    Activity activity = new Activity();
                    if (ven.IsEnabled == "1")
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (ven.IsEnabled == "0")
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Vendor";
                    activity.ActivityName = ActivityName;
                    activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(ven, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VendorTransactions_Items(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = null;
            List<Purchase> item = new List<Purchase>();
            try
            {
                search = js.Deserialize<SearchParameters>(Search);
                List<Purchases> items = new Catalog().SelectVendorTransactions_Items(Convert.ToInt32(search.Option), search.Search);
                if (items != null)
                {
                    foreach (var dbr in items)
                    {
                        Purchase li = new Purchase();
                        li.id = dbr.id;
                        li.TempOrderNum = dbr.TempOrderNum;
                        li.VendorId = dbr.VendorId;
                        li.ItemId = dbr.ItemId;
                        li.ItemName = dbr.ItemName;
                        li.ItemQty = dbr.ItemQty;
                        li.PriceUnit = dbr.PriceUnit;
                        li.TotalPrice = Convert.ToInt32(Convert.ToDouble(dbr.ItemQty) * dbr.PriceUnit);
                        li.MsrmntUnit = dbr.MsrmntUnit;
                        li.DateOfDay = dbr.Date;
                        li.TimeOfDay = dbr.Time;
                        item.Add(li);
                    }

                }
                item.TrimExcess();
            }
            catch (Exception e)
            {
                item = null;
            }
            var pitem = item.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = item.Count, recordsFiltered = item.Count, data = pitem }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VendorTransactions_Bills(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = null;
            List<Bill> bill = new List<Bill>();
            try
            {
                search = js.Deserialize<SearchParameters>(Search);
                List<Bills> bills = new Catalog().VendorTransactions_Bills(Convert.ToInt32(search.Option), search.Search);
                if (bills != null)
                {
                    foreach (var dbr in bills)
                    {
                        Bill li = new Bill();
                        li.id = dbr.id;
                        li.Purchase_id = dbr.Purchase_id;
                        li.Bill_No = dbr.Bill_No;
                        li.Bill_Status = dbr.Bill_Status;
                        li.Bill_Amount = dbr.Bill_Amount;
                        li.Balance_Amount = dbr.Balance_Amount;
                        li.Date = dbr.Date;
                        li.Time = dbr.Time;
                        bill.Add(li);
                    }

                }
                bill.TrimExcess();
            }
            catch (Exception e)
            {
                bill = null;
            }
            var pbill = bill.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = bill.Count, recordsFiltered = bill.Count, data = pbill }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VendorTransactions_Payments(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = null;
            List<Payment> payment = new List<Payment>();
            try
            {
                search = js.Deserialize<SearchParameters>(Search);
                List<Payments> payments = new Catalog().VendorTransactions_Payments(Convert.ToInt32(search.Option), search.Search);
                if (payments != null)
                {
                    foreach (var dbr in payments)
                    {
                        Payment li = new Payment();
                        li.Payment_id = dbr.Payment_id;
                        li.Bill_id = dbr.Bill_id;
                        li.Bill_No = dbr.Bill_No;
                        li.PaymentMode = dbr.PaymentMode;
                        li.Total_Amount = dbr.Total_Amount;
                        li.Paid_Amount = dbr.Paid_Amount;
                        li.Date = dbr.Date;
                        li.Time = dbr.Time;
                        payment.Add(li);
                    }

                }
                payment.TrimExcess();
            }
            catch (Exception e)
            {
                payment = null;
            }
            var ppayment = payment.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = payment.Count, recordsFiltered = payment.Count, data = ppayment }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetVendorWS(string Search)
        {
            List<Contacts> vendor = new Catalog().AllVendors(null, null, null, null);

            List<Select2Model> items = new List<Select2Model>();

            if (vendor != null)
            {
                foreach (var dbr in vendor)
                {
                    Select2Model li = new Select2Model();
                    li.id = dbr.id;
                    li.text = dbr.Name;
                    items.Add(li);
                }
            }

            items.TrimExcess();

            return Json(new { items = items }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelVendor(string DeleteVendorData)
        {
            string response = "";
            List<Contacts> contacts = null;
            List<Contact> contactsNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Contact> contactrequestedtoDelete = js.Deserialize<List<Contact>>(DeleteVendorData);
                contacts = new List<Contacts>();
                contactsNotDelete = new List<Contact>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);
                activities = new List<Activity>();
                foreach (var dbr in contactrequestedtoDelete)
                {
                    Contacts li = new Contacts();
                    Contacts liChecked = new Contacts();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

                    liChecked = new Catalog().CheckContactForDelete(li.id);
                    if (liChecked != null)
                    {
                        Contact co = new Contact();
                        co.Name = liChecked.Name;
                        contactsNotDelete.Add(co);
                    }
                    else
                    {
                        contacts.Add(li);
                    }
                }
                contacts.TrimExcess();
                contactsNotDelete.TrimExcess();
                if (contacts.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(contacts);

                    StringBuilder fileContent = new StringBuilder();

                    foreach (var col in dt.Columns)
                    {
                        fileContent.Append(col.ToString() + ",");
                    }
                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (var column in dr.ItemArray)
                        {
                            fileContent.Append("\"" + column.ToString() + "\",");
                        }
                        fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                    }
                    string filename = "Vendors-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Vendors/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Vendors/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Vendors";
                    mailMessage.Body = "Following Vendors are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Vendors";
                    string Result = new Catalog().DelContactRequest(contacts, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (contacts != null)
                        {
                            foreach (var i in contacts)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Vendor";
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

            return Json(new
            {
                Response = response,
                Contacts = contacts,
                ContactsNotDelete = contactsNotDelete
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
