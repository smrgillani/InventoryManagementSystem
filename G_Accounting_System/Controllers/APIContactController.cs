using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;


namespace G_Accounting_System.Controllers
{
    [CustomAuthorize]
    public class APIContactController : ApiController
    {
        #region VENDORS
        [Route("api/APIContact/GetAllVendors")]
        [HttpGet]
        public IEnumerable<Contact> GetAllVendors()
        {
            List<Contact> vendors = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Contacts> vendor = new Catalog().AllVendors(null, null, null, null);

                vendors = new List<Contact>();

                if (vendor != null)
                {
                    foreach (var dbr in vendor)
                    {
                        Contact li = new Contact();
                        li.id = dbr.id;
                        li.Name = (dbr.Salutation.Equals("1") ? "Mr." : "") + (dbr.Salutation.Equals("2") ? "Mrs." : "") + (dbr.Salutation.Equals("3") ? "Ms." : "") + (dbr.Salutation.Equals("4") ? "Miss" : "") + (dbr.Salutation.Equals("5") ? "Dr." : "") + " " + dbr.Name;
                        li.Company = dbr.Company;
                        li.Landline = dbr.Landline;
                        li.Mobile = dbr.Mobile;
                        li.Email = dbr.Email;
                        li.BankAccountNumber = dbr.BankAccountNumber;
                        li.PaymentMethod = (dbr.PaymentMethod.Equals("1") ? "Cheque" : "") + (dbr.PaymentMethod.Equals("2") ? "Cash" : "") + (dbr.PaymentMethod.Equals("3") ? "Card" : "");
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

                return vendors;
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

        [Route("api/APIContact/VendorById")]
        [HttpGet]
        public Classes VendorById()
        {
            Contact vendor = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                vendor = js.Deserialize<Contact>(strJson);

                Contacts vendors = new Catalog().SelectContact(Convert.ToInt32(vendor.id), 1, 0, 0, null);

                Classes data = new Classes();
                data.Contact = new Contact();
                data.Contact.id = vendors.id;
                data.Contact.Salutation = vendors.Salutation;
                data.Contact.Name = vendors.Name;
                data.Contact.Full_name_ = (vendors.Salutation.Equals("1") ? "Mr." : "") + (vendors.Salutation.Equals("2") ? "Mrs." : "") + (vendors.Salutation.Equals("3") ? "Ms." : "") + (vendors.Salutation.Equals("4") ? "Miss" : "") + (vendors.Salutation.Equals("5") ? "Dr." : "") + " " + vendors.Name;
                data.Contact.Company = vendors.Company;
                data.Contact.Designation = vendors.Designation;
                data.Contact.Landline = vendors.Landline;
                data.Contact.Mobile = vendors.Mobile;
                data.Contact.Email = vendors.Email;
                data.Contact.Website = vendors.Website;
                data.Contact.Address = vendors.Address;
                data.Contact.AddressLandline = vendors.AddressLandline;
                data.Contact.City = vendors.City;
                data.Contact.Country = vendors.Country;
                data.Contact.BankAccountNumber = vendors.BankAccountNumber;
                data.Contact.PaymentMethod = vendors.PaymentMethod;
                data.Contact.Payment_method_ = (vendors.PaymentMethod.Equals("1") ? "Cheque" : "") + (vendors.PaymentMethod.Equals("2") ? "Cash" : "") + (vendors.PaymentMethod.Equals("3") ? "Card" : "");
                data.Contact.IsEnabled = vendors.Enable.ToString();
                data.Contact.IsEnabled_ = (vendors.Enable == 1) ? "Active" : "InActive";
                int ActivityType_id = vendor.id;
                string ActivityType = "Vendor";
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

        [Route("api/APIContact/InsertUpdateVendors")]
        [HttpPost]
        public Contact InsertUpdateVendors()
        {
            Contact vendor = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    vendor = js.Deserialize<Contact>(strJson);

                    Contacts AddVendor = new Contacts();
                    AddVendor.Salutation = (vendor.Salutation.Equals("1") || vendor.Salutation.Equals("2") || vendor.Salutation.Equals("3") || vendor.Salutation.Equals("4") || vendor.Salutation.Equals("5")) ? vendor.Salutation : "1";
                    AddVendor.id = vendor.id;
                    AddVendor.Name = vendor.Name;
                    AddVendor.Company = vendor.Company;
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
                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (vendor.id == 0)
                    {
                        AddVendor.AddedBy = Convert.ToInt32(User_id);
                        AddVendor.UpdatedBy = 0;
                        vendor.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddVendor.UpdatedBy = Convert.ToInt32(User_id);
                        AddVendor.AddedBy = 0;
                        vendor.UpdatedBy = Convert.ToInt32(User_id);
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
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return vendor;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIContact/SendRequestToDelVendor")]
        [HttpPost]
        public object SendRequestToDelVendor()
        {
            List<Contacts> contacts = null;
            List<Contact> contactsNotDelete = null;
            List<Contact> contactrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                contactrequestedtoDelete = js.Deserialize<List<Contact>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    contacts = new List<Contacts>();
                    contactsNotDelete = new List<Contact>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in contactrequestedtoDelete)
                    {
                        Contacts li = new Contacts();
                        Contacts liChecked = new Contacts();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
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
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Vendors/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Vendors/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Vendors";
                        new Catalog().DelContactRequest(contacts, type);
                    }

                }
                return new { contacts, contactsNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region CUSTOMERS
        [Route("api/APIContact/GetAllCustomers")]
        [HttpGet]
        public IEnumerable<Contact> GetAllCustomers()
        {
            List<Contact> customers = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Contacts> customer = new Catalog().AllCustomers(null, null, null, null);

                customers = new List<Contact>();

                if (customer != null)
                {
                    foreach (var dbr in customer)
                    {
                        Contact li = new Contact();
                        li.id = dbr.id;
                        li.Salutation = dbr.Salutation;
                        li.Name = (dbr.Salutation.Equals("1") ? "Mr." : "") + (dbr.Salutation.Equals("2") ? "Mrs." : "") + (dbr.Salutation.Equals("3") ? "Ms." : "") + (dbr.Salutation.Equals("4") ? "Miss" : "") + (dbr.Salutation.Equals("5") ? "Dr." : "") + " " + dbr.Name;
                        li.Company = dbr.Company;
                        li.Landline = dbr.Landline;
                        li.Mobile = dbr.Mobile;
                        li.Email = dbr.Email;
                        li.BankAccountNumber = dbr.BankAccountNumber;
                        li.PaymentMethod = (dbr.PaymentMethod.Equals("1") ? "Cheque" : "") + (dbr.PaymentMethod.Equals("2") ? "Cash" : "") + (dbr.PaymentMethod.Equals("3") ? "Card" : "");
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

                return customers;
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

        [Route("api/APIContact/CustomerById")]
        [HttpGet]
        public Classes CustomerById()
        {
            Contact customer = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                customer = js.Deserialize<Contact>(strJson);

                Contacts customers = new Catalog().SelectContact(Convert.ToInt32(customer.id), 0, 1, 0, null);
                Classes data = new Classes();
                data.Contact = new Contact();
                data.Contact.id = customers.id;
                data.Contact.Salutation = customers.Salutation;
                data.Contact.Name = customers.Name;
                data.Contact.Full_name_ = (customers.Salutation.Equals("1") ? "Mr." : "") + (customers.Salutation.Equals("2") ? "Mrs." : "") + (customers.Salutation.Equals("3") ? "Ms." : "") + (customers.Salutation.Equals("4") ? "Miss" : "") + (customers.Salutation.Equals("5") ? "Dr." : "") + " " + customers.Name;
                data.Contact.Company = customers.Company;
                data.Contact.Designation = customers.Designation;
                data.Contact.Landline = customers.Landline;
                data.Contact.Mobile = customers.Mobile;
                data.Contact.Email = customers.Email;
                data.Contact.Website = customers.Website;
                data.Contact.Address = customers.Address;
                data.Contact.AddressLandline = customers.AddressLandline;
                data.Contact.City = customers.City;
                data.Contact.Country = customers.Country;
                data.Contact.BankAccountNumber = customers.BankAccountNumber;
                data.Contact.PaymentMethod = customers.PaymentMethod;
                data.Contact.Payment_method_ = (customers.PaymentMethod.Equals("1") ? "Cheque" : "") + (customers.PaymentMethod.Equals("2") ? "Cash" : "") + (customers.PaymentMethod.Equals("3") ? "Card" : "");
                data.Contact.IsEnabled = customers.Enable.ToString();
                data.Contact.IsEnabled_ = (customers.Enable == 1) ? "Active" : "InActive";

                int ActivityType_id = customer.id;
                string ActivityType = "Customer";
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

        [Route("api/APIContact/InsertUpdateCustomers")]
        [HttpPost]
        public Contact InsertUpdateCustomers()
        {
            Contact customer = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    customer = js.Deserialize<Contact>(strJson);

                    Contacts AddCustomer = new Contacts();
                    AddCustomer.id = customer.id;
                    AddCustomer.Salutation = (customer.Salutation.Equals("1") || customer.Salutation.Equals("2") || customer.Salutation.Equals("3") || customer.Salutation.Equals("4") || customer.Salutation.Equals("5")) ? customer.Salutation : "1";
                    AddCustomer.Name = customer.Name;
                    AddCustomer.Company = customer.Company;
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

                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (customer.id == 0)
                    {
                        AddCustomer.AddedBy = Convert.ToInt32(User_id);
                        AddCustomer.UpdatedBy = 0;
                        customer.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddCustomer.UpdatedBy = Convert.ToInt32(User_id);
                        AddCustomer.AddedBy = 0;
                        customer.UpdatedBy = Convert.ToInt32(User_id);
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
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return customer;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIContact/SendRequestToDelCustomers")]
        [HttpPost]
        public object SendRequestToDelCustomers()
        {
            List<Contacts> contacts = null;
            List<Contact> contactsNotDelete = null;
            List<Contact> contactrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                contactrequestedtoDelete = js.Deserialize<List<Contact>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    contacts = new List<Contacts>();
                    contactsNotDelete = new List<Contact>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in contactrequestedtoDelete)
                    {
                        Contacts li = new Contacts();
                        Contacts liChecked = new Contacts();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
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
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Customers/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Customers/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Customers";
                        new Catalog().DelContactRequest(contacts, type);
                    }

                }
                return new { contacts, contactsNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Employees
        [Route("api/APIContact/GetAllEmployees")]
        [HttpGet]
        public IEnumerable<Contact> GetAllEmployees()
        {
            List<Contact> employees = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Contacts> employee = new Catalog().AllEmployees(null, null, null, null);

                employees = new List<Contact>();

                if (employee != null)
                {
                    foreach (var dbr in employee)
                    {
                        Contact li = new Contact();
                        li.id = dbr.id;
                        li.Salutation = dbr.Salutation;
                        li.Name = (dbr.Salutation.Equals("1") ? "Mr." : "") + (dbr.Salutation.Equals("2") ? "Mrs." : "") + (dbr.Salutation.Equals("3") ? "Ms." : "") + (dbr.Salutation.Equals("4") ? "Miss" : "") + (dbr.Salutation.Equals("5") ? "Dr." : "") + " " + dbr.Name;
                        li.Company = dbr.Company;
                        li.Landline = dbr.Landline;
                        li.Mobile = dbr.Mobile;
                        li.Email = dbr.Email;
                        li.BankAccountNumber = dbr.BankAccountNumber;
                        li.PaymentMethod = (dbr.PaymentMethod.Equals("1") ? "Cheque" : "") + (dbr.PaymentMethod.Equals("2") ? "Cash" : "") + (dbr.PaymentMethod.Equals("3") ? "Card" : "");
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

                return employees;
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

        [Route("api/APIContact/EmployeeById")]
        [HttpGet]
        public Classes EmployeeById()
        {
            Contact employee = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                employee = js.Deserialize<Contact>(strJson);

                Contacts employees = new Catalog().SelectContact(Convert.ToInt32(employee.id), 0, 0, 1, null);
                Classes data = new Classes();
                data.Contact = new Contact();
                data.Contact.id = employees.id;
                data.Contact.Salutation = employees.Salutation;
                data.Contact.Name = employees.Name;
                data.Contact.Full_name_ = (employees.Salutation.Equals("1") ? "Mr." : "") + (employees.Salutation.Equals("2") ? "Mrs." : "") + (employees.Salutation.Equals("3") ? "Ms." : "") + (employees.Salutation.Equals("4") ? "Miss" : "") + (employees.Salutation.Equals("5") ? "Dr." : "") + " " + employees.Name;
                data.Contact.Company = employees.Company;
                data.Contact.Designation = employees.Designation;
                data.Contact.Landline = employees.Landline;
                data.Contact.Mobile = employees.Mobile;
                data.Contact.Email = employees.Email;
                data.Contact.Website = employees.Website;
                data.Contact.Address = employees.Address;
                data.Contact.AddressLandline = employees.AddressLandline;
                data.Contact.City = employees.City;
                data.Contact.Country = employees.Country;
                data.Contact.BankAccountNumber = employees.BankAccountNumber;
                data.Contact.PaymentMethod = employees.PaymentMethod;
                data.Contact.Payment_method_ = (employees.PaymentMethod.Equals("1") ? "Cheque" : "") + (employees.PaymentMethod.Equals("2") ? "Cash" : "") + (employees.PaymentMethod.Equals("3") ? "Card" : "");
                data.Contact.IsEnabled = employees.Enable.ToString();
                data.Contact.IsEnabled_ = (employees.Enable == 1) ? "Active" : "InActive";
                int ActivityType_id = employee.id;
                string ActivityType = "Employee";
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

        [Route("api/APIContact/InsertUpdateEmployees")]
        [HttpPost]
        public Contact InsertUpdateEmployees()
        {
            Contact employee = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    employee = js.Deserialize<Contact>(strJson);

                    Contacts AddEmployee = new Contacts();
                    AddEmployee.id = employee.id;
                    AddEmployee.Salutation = (employee.Salutation.Equals("1") || employee.Salutation.Equals("2") || employee.Salutation.Equals("3") || employee.Salutation.Equals("4") || employee.Salutation.Equals("5")) ? employee.Salutation : "1";
                    AddEmployee.Name = employee.Name;
                    AddEmployee.Company = employee.Company;
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

                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (employee.id == 0)
                    {
                        AddEmployee.AddedBy = Convert.ToInt32(User_id);
                        AddEmployee.UpdatedBy = 0;
                        employee.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddEmployee.UpdatedBy = Convert.ToInt32(User_id);
                        AddEmployee.AddedBy = 0;
                        employee.UpdatedBy = Convert.ToInt32(User_id);
                    }
                    new Catalog().InsertUpdateContacts(AddEmployee);

                    employee.pFlag = AddEmployee.pFlag;
                    employee.pDesc = AddEmployee.pDesc;
                    employee.pContactid_Out = AddEmployee.pContactid_Out;
                    if (employee.pFlag == "1")
                    {
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
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIContact/SendRequestToDelEmployees")]
        [HttpPost]
        public object SendRequestToDelEmployees()
        {
            List<Contacts> contacts = null;
            List<Contact> contactsNotDelete = null;
            List<Contact> contactrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                contactrequestedtoDelete = js.Deserialize<List<Contact>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    contacts = new List<Contacts>();
                    contactsNotDelete = new List<Contact>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in contactrequestedtoDelete)
                    {
                        Contacts li = new Contacts();
                        Contacts liChecked = new Contacts();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
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
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Employee/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Employee/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Employees";
                        new Catalog().DelContactRequest(contacts, type);
                    }

                }
                return new { contacts, contactsNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
