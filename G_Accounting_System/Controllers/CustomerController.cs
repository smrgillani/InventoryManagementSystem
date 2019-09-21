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
using System.Net;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        [PermissionsAuthorize]
        public ActionResult Index(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? id)
        {
            bool check = new PermissionsClass().CheckAddPermission();
            if (check != false)
            {
                return View("Customer");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InserUpdateCustomer(string Customer)
        {
            string response = "";
            Contact customer = null;
            string ActivityName = null;
            try
            {
                var js = new JavaScriptSerializer();
                customer = js.Deserialize<Contact>(Customer);

                if (string.IsNullOrEmpty(customer.Name))
                {
                    response = "Enter Customer Full Name In General Details.";
                }
                else if (string.IsNullOrEmpty(customer.Landline))
                {
                    response = "Enter Landline Number Of Customer In General Details.";
                }
                else if (string.IsNullOrEmpty(customer.Mobile))
                {
                    response = "Enter Mobile Number Of Customer In General Details.";
                }
                else if (string.IsNullOrEmpty(customer.Address))
                {
                    response = "Enter Office Location Address Of Customer In Address Details.";
                }
                else
                {

                    Contacts AddCustomer = new Contacts();
                    AddCustomer.id = customer.id;
                    AddCustomer.Salutation = (customer.Salutation.Equals("1") || customer.Salutation.Equals("2") || customer.Salutation.Equals("3") || customer.Salutation.Equals("4") || customer.Salutation.Equals("5")) ? customer.Salutation : "1";
                    AddCustomer.Name = customer.Name;
                    AddCustomer.CompanyId = customer.CompanyId;
                    AddCustomer.Designation = customer.Designation;
                    AddCustomer.Landline = customer.Landline;
                    AddCustomer.Mobile = customer.Mobile;
                    AddCustomer.Email = customer.Email;
                    AddCustomer.Website = customer.Website;
                    AddCustomer.Address = customer.Address;
                    AddCustomer.AddressLandline = customer.AddressLandline;
                    AddCustomer.City = customer.City;
                    AddCustomer.Country = customer.Country;
                    AddCustomer.BankAccountNumber = customer.BankAccountNumber;
                    AddCustomer.PaymentMethod = (customer.PaymentMethod.Equals("1") || customer.PaymentMethod.Equals("2") || customer.PaymentMethod.Equals("3")) ? customer.PaymentMethod : "1";
                    AddCustomer.Enable = customer.IsEnabled.Equals("1") ? 1 : 0;
                    AddCustomer.Vendor = 0;
                    AddCustomer.Customer = 1;
                    AddCustomer.Employee = 0;
                    AddCustomer.Premises_id = Convert.ToInt32(Session["Premises_id"]);

                    Countries countries = new Catalog().SelectCountry(Convert.ToInt32(customer.Country), null);
                    Cities citites = new Catalog().SelectCity(Convert.ToInt32(customer.City), null);
                    Companies companies = new Catalog().SelectCompany(customer.CompanyId, null);
                    if (countries != null && citites != null && companies != null)
                    {
                        if (customer.id == 0)
                        {
                            AddCustomer.AddedBy = Convert.ToInt32(Session["UserId"]);
                            AddCustomer.UpdatedBy = 0;
                        }
                        else
                        {
                            AddCustomer.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                            AddCustomer.AddedBy = 0;
                        }
                        new Catalog().InsertUpdateContacts(AddCustomer);

                        customer.pFlag = AddCustomer.pFlag;
                        customer.pDesc = AddCustomer.pDesc;
                        customer.pContactid_Out = AddCustomer.pContactid_Out;
                        if (customer.pFlag == "1")
                        {
                            Activity activity = new Activity();
                            if (customer.id == 0)
                            {
                                ActivityName = "Created";
                                activity.ActivityType_id = Convert.ToInt32(customer.pContactid_Out);
                            }
                            else
                            {
                                ActivityName = "Updated";
                                activity.ActivityType_id = customer.id;
                            }
                            activity.ActivityType = "Customer";
                            activity.ActivityName = ActivityName;
                            activity.User_id = Convert.ToInt32(Session["UserId"]);
                            activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                            List<Activity> activities = new List<Activity>();
                            activities.Add(activity);


                            new ActivitiesClass().InsertActivity(activities);
                        }
                        return Json(customer, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        customer.pFlag = "0";
                        customer.pDesc = "Provided Data is Incorrect";
                        return Json(customer, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                customer.pFlag = "0";
                customer.pDesc = e.Message;
                return Json(customer, JsonRequestBehavior.AllowGet);
            }

            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCustomers(int? id, string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Contacts> customer = new Catalog().AllCustomers(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Contact> customers = new List<Contact>();

            if (customer != null)
            {
                foreach (var dbr in customer)
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
                    customers.Add(li);
                }
            }

            customers.TrimExcess();
            var pcustomers = customers.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = customers.Count, recordsFiltered = customers.Count, data = pcustomers }, JsonRequestBehavior.AllowGet);
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
            Contacts customers = new Catalog().SelectContact(Convert.ToInt32(id), 0, 1, 0, null);

            Contact customer = new Contact();
            customer.id = customers.id;
            customer.Salutation = customers.Salutation;
            customer.Name = customers.Name;
            customer.Full_name_ = (customers.Salutation.Equals("1") ? "Mr." : "") + (customers.Salutation.Equals("2") ? "Mrs." : "") + (customers.Salutation.Equals("3") ? "Ms." : "") + (customers.Salutation.Equals("4") ? "Miss" : "") + (customers.Salutation.Equals("5") ? "Dr." : "") + " " + customers.Name;
            customer.CompanyId = customers.CompanyId;
            customer.Company = new Catalog().SelectCompany(customers.CompanyId, null).Name;
            customer.Designation = customers.Designation;
            customer.Landline = customers.Landline;
            customer.Mobile = customers.Mobile;
            customer.Email = customers.Email;
            customer.Website = customers.Website;
            customer.Address = customers.Address;
            customer.AddressLandline = customers.AddressLandline;
            customer.City = customers.City;
            customer.CityName = new Catalog().SelectCity(Convert.ToInt32(customers.City), null).Name;
            customer.Country = customers.Country;
            customer.CountryName = new Catalog().SelectCountry(Convert.ToInt32(customers.Country), null).Name;
            customer.BankAccountNumber = customers.BankAccountNumber;
            customer.PaymentMethod = customers.PaymentMethod;
            customer.Payment_method_ = (customers.PaymentMethod.Equals("1") ? "Cheque" : "") + (customers.PaymentMethod.Equals("2") ? "Cash" : "") + (customers.PaymentMethod.Equals("3") ? "Card" : "");
            customer.IsEnabled = customers.Enable.ToString();
            customer.IsEnabled_ = (customers.Enable == 1) ? "Active" : "InActive";
            customer.Delete_Status = customers.Delete_Status;

            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(int id)
        {
            Contact customer = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    Contacts customers = new Catalog().SelectContact(Convert.ToInt32(id), 0, 1, 0, null);

                    customer = new Contact();
                    customer.id = customers.id;
                    customer.Salutation = customers.Salutation;
                    customer.Name = customers.Name;
                    customer.CompanyId = customers.CompanyId;
                    customer.Designation = customers.Designation;
                    customer.Landline = customers.Landline;
                    customer.Mobile = customers.Mobile;
                    customer.Email = customers.Email;
                    customer.Website = customers.Website;
                    customer.Address = customers.Address;
                    customer.AddressLandline = customers.AddressLandline;
                    customer.City = customers.City;
                    customer.Country = customers.Country;
                    customer.BankAccountNumber = customers.BankAccountNumber;
                    customer.PaymentMethod = customers.PaymentMethod;
                    customer.IsEnabled = customers.Enable.ToString();
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(customer, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(customer, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string response = "";
            Contact con = null;
            string ActivityName = null;
            try
            {
                Contacts contacts = new Catalog().SelectContact(id, 0, 1, 0, null);
                con = new Contact();
                if (contacts == null)
                {
                    response = "Please Select A Customer Vendor";
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
                    con.IsEnabled = contact.Enable.ToString();
                    Activity activity = new Activity();
                    if (con.IsEnabled == "1")
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (con.IsEnabled == "0")
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Customer";
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

            return Json(con, JsonRequestBehavior.AllowGet);
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
        public ActionResult CustomerTransactions_Items(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = null;
            List<SalesOrder> item = new List<SalesOrder>();
            try
            {
                search = js.Deserialize<SearchParameters>(Search);
                List<SalesOrders> items = new Catalog().CustomerTransactions_Items(Convert.ToInt32(search.Option), search.Search);
                if (items != null)
                {
                    foreach (var dbr in items)
                    {
                        SalesOrder li = new SalesOrder();
                        li.sdid = dbr.sdid;
                        li.SalesOrder_id = dbr.SalesOrder_id;
                        li.SaleOrderNo = dbr.SaleOrderNo;
                        li.Customer_id = dbr.Customer_id;
                        li.ItemId = dbr.ItemId;
                        li.ItemName = dbr.ItemName;
                        li.ItemQty = dbr.ItemQty;
                        li.PriceUnit = dbr.PriceUnit;
                        li.SO_Total_Amount = Convert.ToString(Convert.ToDouble(dbr.ItemQty) * dbr.PriceUnit);
                        li.Date_Of_Day = dbr.Date_Of_Day;
                        li.Time_Of_Day = dbr.Time_Of_Day;
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
        public ActionResult CustomerTransactions_Invoices(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = null;
            List<SO_Invoice> invoice = new List<SO_Invoice>();
            try
            {
                search = js.Deserialize<SearchParameters>(Search);
                List<SO_Invoices> invoices = new Catalog().CustomerTransactions_Invoices(Convert.ToInt32(search.Option), search.Search);
                if (invoices != null)
                {
                    foreach (var dbr in invoices)
                    {
                        SO_Invoice li = new SO_Invoice();
                        li.id = dbr.id;
                        li.SalesOrder_id = dbr.SalesOrder_id;
                        li.Invoice_No = dbr.Invoice_No;
                        li.Invoice_Status = dbr.Invoice_Status;
                        li.Invoice_Amount = dbr.Invoice_Amount;
                        li.Balance_Amount = dbr.Balance_Amount;
                        li.Date = dbr.Date;
                        li.Time = dbr.Time;
                        invoice.Add(li);
                    }

                }
                invoice.TrimExcess();
            }
            catch (Exception e)
            {
                invoice = null;
            }
            var pinvoice = invoice.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = invoice.Count, recordsFiltered = invoice.Count, data = pinvoice }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerTransactions_Payments(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = null;
            List<SO_Payment> payment = new List<SO_Payment>();
            try
            {
                search = js.Deserialize<SearchParameters>(Search);
                List<SO_Payments> payments = new Catalog().CustomerTransactions_Payments(Convert.ToInt32(search.Option), search.Search);
                if (payments != null)
                {
                    foreach (var dbr in payments)
                    {
                        SO_Payment li = new SO_Payment();
                        li.SO_Payment_id = dbr.SO_Payment_id;
                        li.Invoice_id = dbr.Invoice_id;
                        li.Invoice_No = dbr.Invoice_No;
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
        public JsonResult SendRequestToDelCustomers(string DeleteCustomerData)
        {
            string response = "";
            string type = "";
            List<Contacts> contacts = null;
            List<Contact> contactsNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Contact> contactrequestedtoDelete = js.Deserialize<List<Contact>>(DeleteCustomerData);
                contacts = new List<Contacts>();
                contactsNotDelete = new List<Contact>();
                activities = new List<Activity>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);

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
                    string filename = "Customers-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Customers/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Customers/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Customers";
                    mailMessage.Body = "Following Customers are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    type = "Customers";
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
                                activity.ActivityType = "Customer";
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
