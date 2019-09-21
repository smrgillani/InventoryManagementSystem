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
using System.Drawing;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
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
                return View("Employee");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateEmployee(string Employee)
        {
            string response = "";
            Contact employee = null;
            string ActivityName = null;
            var filepath = "";
            Image image = null;
            try
            {
                var js = new JavaScriptSerializer();
                employee = js.Deserialize<Contact>(Employee);

                if (string.IsNullOrEmpty(employee.Name))
                {
                    response = "Enter Employee Full Name In General Details.";
                }
                else if (string.IsNullOrEmpty(employee.Landline))
                {
                    response = "Enter Landline Number Of Employee In General Details.";
                }
                else if (string.IsNullOrEmpty(employee.Mobile))
                {
                    response = "Enter Mobile Number Of Employee In General Details.";
                }
                else if (string.IsNullOrEmpty(employee.Address))
                {
                    response = "Enter Office Location Address Of Employee In Address Details.";
                }
                else
                {
                    Contacts AddEmployee = new Contacts();
                    if (!String.IsNullOrEmpty(employee.base64))
                    {
                        //Convert Image From Base64 to Image
                        byte[] imageBytes = Convert.FromBase64String(employee.base64);
                        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                        ms.Write(imageBytes, 0, imageBytes.Length);
                        image = Image.FromStream(ms, true);

                        var filename = employee.Name + "-" + DateTime.Now.ToString("yyyyMMdd") + ".jpg";
                        filepath = "/Images/EmployeeImages/" + filename;
                    }
                    AddEmployee.id = employee.id;
                    AddEmployee.File_Name = filepath;
                    AddEmployee.Salutation = (employee.Salutation.Equals("1") || employee.Salutation.Equals("2") || employee.Salutation.Equals("3") || employee.Salutation.Equals("4") || employee.Salutation.Equals("5")) ? employee.Salutation : "1";
                    AddEmployee.Name = employee.Name;
                    AddEmployee.CompanyId = employee.CompanyId;
                    AddEmployee.Designation = employee.Designation;
                    AddEmployee.Landline = employee.Landline;
                    AddEmployee.Mobile = employee.Mobile;
                    AddEmployee.Email = employee.Email;
                    AddEmployee.Website = employee.Website;
                    AddEmployee.Address = employee.Address;
                    AddEmployee.AddressLandline = employee.AddressLandline;
                    AddEmployee.City = employee.City;
                    AddEmployee.Country = employee.Country;
                    AddEmployee.BankAccountNumber = employee.BankAccountNumber;
                    AddEmployee.PaymentMethod = (employee.PaymentMethod.Equals("1") || employee.PaymentMethod.Equals("2") || employee.PaymentMethod.Equals("3")) ? employee.PaymentMethod : "1";
                    AddEmployee.Enable = employee.IsEnabled.Equals("1") ? 1 : 0;
                    AddEmployee.Vendor = 0;
                    AddEmployee.Customer = 0;
                    AddEmployee.Employee = 1;

                    Countries countries = new Catalog().SelectCountry(Convert.ToInt32(employee.Country), null);
                    Cities citites = new Catalog().SelectCity(Convert.ToInt32(employee.City), null);
                    Companies companies = new Catalog().SelectCompany(employee.CompanyId, null);
                    if (countries != null && citites != null && companies != null)
                    {
                        if (employee.id == 0)
                        {
                            AddEmployee.AddedBy = Convert.ToInt32(Session["UserId"]);
                            AddEmployee.UpdatedBy = 0;
                        }
                        else
                        {
                            AddEmployee.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                            AddEmployee.AddedBy = 0;
                        }
                        new Catalog().InsertUpdateContacts(AddEmployee);

                        employee.pFlag = AddEmployee.pFlag;
                        employee.pDesc = AddEmployee.pDesc;
                        employee.pContactid_Out = AddEmployee.pContactid_Out;
                        if (employee.pFlag == "1")
                        {
                            if (filepath != "")
                            {
                                image.Save(Server.MapPath("~" + filepath));
                            }
                            Activity activity = new Activity();
                            if (employee.id == 0)
                            {
                                ActivityName = "Created";
                                activity.ActivityType_id = Convert.ToInt32(employee.pContactid_Out);
                            }
                            else
                            {
                                ActivityName = "Updated";
                                activity.ActivityType_id = employee.id;
                            }
                            activity.ActivityType = "Employee";
                            activity.ActivityName = ActivityName;
                            activity.User_id = Convert.ToInt32(Session["UserId"]);
                            activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                            List<Activity> activities = new List<Activity>();
                            activities.Add(activity);


                            new ActivitiesClass().InsertActivity(activities);
                        }
                        return Json(employee, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        employee.pFlag = "0";
                        employee.pDesc = "Provided Data is Incorrect";
                        return Json(employee, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                employee.pFlag = "0";
                employee.pDesc = e.Message;
                return Json(employee, JsonRequestBehavior.AllowGet);
            }

            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllEmployees(int? id, string Search)
        {
            List<Contact> employees = null;
            SearchParameters search = null;
            var js = new JavaScriptSerializer();
            try
            {
                search = js.Deserialize<SearchParameters>(Search);
                List<Contacts> employee = new Catalog().AllEmployees(search.Option, search.Search, search.StartDate, search.EndDate);

                employees = new List<Contact>();

                if (employee != null)
                {
                    foreach (var dbr in employee)
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
                        employees.Add(li);
                    }
                }

                employees.TrimExcess();
            }
            catch (Exception e)
            {

            }
            var pemployees = employees.Skip(search.PageStart).Take(search.PageLength);

            return Json(new { draw = search.Draw, recordsTotal = employees.Count, recordsFiltered = employees.Count, data = pemployees }, JsonRequestBehavior.AllowGet);
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
            Contacts employees = new Catalog().SelectContact(Convert.ToInt32(id), 0, 0, 1, null);

            Contact employee = new Contact();
            employee.id = employees.id;
            employee.Salutation = employees.Salutation;
            employee.Name = employees.Name;
            employee.Full_name_ = (employees.Salutation.Equals("1") ? "Mr." : "") + (employees.Salutation.Equals("2") ? "Mrs." : "") + (employees.Salutation.Equals("3") ? "Ms." : "") + (employees.Salutation.Equals("4") ? "Miss" : "") + (employees.Salutation.Equals("5") ? "Dr." : "") + " " + employees.Name;
            employee.CompanyId = employees.CompanyId;
            employee.filename = employees.File_Name;
            employee.Company = new Catalog().SelectCompany(employees.CompanyId, null).Name;
            employee.Designation = employees.Designation;
            employee.Landline = employees.Landline;
            employee.Mobile = employees.Mobile;
            employee.Email = employees.Email;
            employee.Website = employees.Website;
            employee.Address = employees.Address;
            employee.AddressLandline = employees.AddressLandline;
            employee.City = employees.City;
            employee.CityName = new Catalog().SelectCity(Convert.ToInt32(employees.City), null).Name;
            employee.Country = employees.Country;
            employee.CountryName = new Catalog().SelectCountry(Convert.ToInt32(employees.Country), null).Name;
            employee.BankAccountNumber = employees.BankAccountNumber;
            employee.PaymentMethod = employees.PaymentMethod;
            employee.Payment_method_ = (employees.PaymentMethod.Equals("1") ? "Cheque" : "") + (employees.PaymentMethod.Equals("2") ? "Cash" : "") + (employees.PaymentMethod.Equals("3") ? "Card" : "");
            employee.IsEnabled = employees.Enable.ToString();
            employee.IsEnabled_ = (employees.Enable == 1) ? "Active" : "InActive";
            employee.Delete_Status = employees.Delete_Status;

            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(int id)
        {
            Contact employee = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    Contacts employees = new Catalog().SelectContact(Convert.ToInt32(id), 0, 0, 1, null);

                    employee = new Contact();
                    employee.id = employees.id;
                    employee.Salutation = employees.Salutation;
                    employee.Name = employees.Name;
                    employee.CompanyId = employees.CompanyId;
                    employee.Designation = employees.Designation;
                    employee.Landline = employees.Landline;
                    employee.Mobile = employees.Mobile;
                    employee.Email = employees.Email;
                    employee.Website = employees.Website;
                    employee.Address = employees.Address;
                    employee.AddressLandline = employees.AddressLandline;
                    employee.City = employees.City;
                    employee.Country = employees.Country;
                    employee.BankAccountNumber = employees.BankAccountNumber;
                    employee.PaymentMethod = employees.PaymentMethod;
                    employee.IsEnabled = employees.Enable.ToString();
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(employee, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(employee, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Contact emp = null;
            try
            {
                Contacts contacts = new Catalog().SelectContact(id, 0, 0, 1, null);
                emp = new Contact();
                if (contacts == null)
                {
                    response = "Please Select A Valid Employee";
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
                    emp.IsEnabled = contact.Enable.ToString();
                    Activity activity = new Activity();
                    if (emp.IsEnabled == "1")
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (emp.IsEnabled == "0")
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Employee";
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

            return Json(emp, JsonRequestBehavior.AllowGet);
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
        public JsonResult SendRequestToDelEmployees(string DeleteEmployeeData)
        {
            string response = "";
            List<Contacts> contacts = null;
            List<Contact> contactsNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Contact> contactrequestedtoDelete = js.Deserialize<List<Contact>>(DeleteEmployeeData);
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
                    string filename = "Employee-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Employee/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Employee/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Employees";
                    mailMessage.Body = "Following Employees are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Employees";
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
                                activity.ActivityType = "Employee";
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
