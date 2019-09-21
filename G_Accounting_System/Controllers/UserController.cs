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
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Net;
using System.Data;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using G_Accounting_System.Code.Helpers;
using System.Text;
using Activity = G_Accounting_System.Models.Activity;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class UserController : Controller
    {
        //
        // GET: /User/       
        [PermissionsAuthorize]
        public ActionResult Index(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? id)
        {
            try
            {
                bool check = new PermissionsClass().CheckAddPermission();
                if (check != false)
                {
                    List<Contacts> employee = new Catalog().AllEmployees(null, null, null, null);
                    List<Contacts> customer = new Catalog().AllCustomers(null, null, null, null);
                    List<Contacts> vendor = new Catalog().AllVendors(null, null, null, null);

                    List<Premisess> stores = new Catalog().AllStores(null, null, null, null);
                    List<Premisess> offices = new Catalog().AllOffices(null, null, null, null);
                    List<Premisess> shops = new Catalog().AllShops(null, null, null, null);
                    List<Premisess> factories = new Catalog().AllFactories(null, null, null, null);

                    Classes data = new Classes();

                    data.Vendors = new List<Contact>();
                    data.Employees = new List<Contact>();
                    data.Customers = new List<Contact>();

                    data.Stores = new List<Premises>();
                    data.Offices = new List<Premises>();
                    data.Shops = new List<Premises>();
                    data.Factories = new List<Premises>();

                    if (employee != null)
                    {
                        foreach (var dbr in employee)
                        {
                            Contact li = new Contact();
                            li.id = dbr.id;
                            li.Name = dbr.Salutation + " " + dbr.Name;
                            data.Employees.Add(li);
                        }
                        data.Employees.TrimExcess();
                    }

                    if (vendor != null)
                    {
                        foreach (var dbr in vendor)
                        {
                            Contact li = new Contact();
                            li.id = dbr.id;
                            li.Name = dbr.Salutation + " " + dbr.Name;
                            data.Vendors.Add(li);
                        }
                        data.Vendors.TrimExcess();
                    }

                    if (customer != null)
                    {
                        foreach (var dbr in customer)
                        {
                            Contact li = new Contact();
                            li.id = dbr.id;
                            li.Name = dbr.Salutation + " " + dbr.Name;
                            data.Customers.Add(li);
                        }
                        data.Customers.TrimExcess();

                    }

                    if (stores != null)
                    {
                        foreach (var dbr in stores)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Stores.Add(li);
                        }
                        data.Stores.TrimExcess();
                    }

                    if (offices != null)
                    {
                        foreach (var dbr in offices)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Offices.Add(li);
                        }
                        data.Offices.TrimExcess();
                    }

                    if (shops != null)
                    {
                        foreach (var dbr in shops)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Shops.Add(li);
                        }
                        data.Shops.TrimExcess();

                    }

                    if (factories != null)
                    {
                        foreach (var dbr in factories)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Factories.Add(li);
                        }
                        data.Factories.TrimExcess();

                    }

                    //Console.WriteLine("Vendors => " + data.Vendors.Count + " Emp => " + data.Employees.Count + "Cus => " + data.Customers.Count + "");

                    return View("User", data);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateUser(string UserData)
        {
            string response = "";
            User user = null;
            string ActivityName = null;
            try
            {
                var js = new JavaScriptSerializer();
                user = js.Deserialize<User>(UserData);
                Users users = new Users();

                Contacts employee = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 0, 1, 1);
                Contacts customer = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 1, 0, 1);
                Contacts vendor = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 1, 0, 0, 1);

                if (string.IsNullOrEmpty(user.email))
                {
                    response = "Enter An Email Address";
                }
                else if (string.IsNullOrEmpty(user.password))
                {
                    response = "Enter A Password.";
                }
                else if (string.IsNullOrEmpty(user.attached_profile))
                {
                    response = "Please Select Any Profile For Account. ";
                }
                else if (employee == null && customer == null && vendor == null)
                {
                    response = "Please Select A Valid Profile For Account. ";
                }
                else
                {
                    users.id = user.id;
                    users.email = user.email;
                    users.attached_profile = user.attached_profile;
                    //var encryptedPassword = PasswordHelper.EncryptPassword(user.password);
                    users.password = user.password;
                    users.status = user.status.Equals("1") ? "1" : "0";
                    users.Premises_id = user.Premises_id;
                    users.Role_id = user.Role_id;
                    users.Enable = 1;
                    //users.pao = user.pao;
                    //users.paf = user.paf;
                    //users.pas = user.pas;
                    //users.pas_ = user.pas_;
                    //users.pav = user.pav;
                    //users.pap = user.pap;
                    //users.pac = user.pac;
                    //users.pas__ = user.pas__;
                    //users.pae = user.pae;
                    //users.pap_ = user.pap_;
                    //users.pai = user.pai;
                    //users.pas___ = user.pas___;
                    //users.pau = user.pau;
                    //users.puo = user.puo;
                    //users.puf = user.puf;
                    //users.pus = user.pus;
                    //users.pus_ = user.pus_;
                    //users.puv = user.puv;
                    //users.pup = user.pup;
                    //users.puc = user.puc;
                    //users.pus__ = user.pus__;
                    //users.pue = user.pue;
                    //users.pup_ = user.pup_;
                    //users.pui = user.pui;
                    //users.pus___ = user.pus___;
                    //users.puu = user.puu;
                    //users.pdo = user.pdo;
                    //users.pdf = user.pdf;
                    //users.pds = user.pds;
                    //users.pds_ = user.pds_;
                    //users.pdv = user.pdv;
                    //users.pdp = user.pdp;
                    //users.pdc = user.pdc;
                    //users.pds__ = user.pds__;
                    //users.pde = user.pde;
                    //users.pdp_ = user.pdp_;
                    //users.pdi = user.pdi;
                    //users.pds___ = user.pds___;
                    //users.pdu = user.pdu;
                    //users.pvo = user.pvo;
                    //users.pvf = user.pvf;
                    //users.pvs = user.pvs;
                    //users.pvs_ = user.pvs_;
                    //users.pvv = user.pvv;
                    //users.pvp = user.pvp;
                    //users.pvc = user.pvc;
                    //users.pvs__ = user.pvs__;
                    //users.pve = user.pve;
                    //users.pvp_ = user.pvp_;
                    //users.pvi = user.pvi;
                    //users.pvs___ = user.pvs___;
                    //users.pvu = user.pvu;
                    //users.pvol = user.pvol;
                    //users.pvfl = user.pvfl;
                    //users.pvsl = user.pvsl;
                    //users.pvsl_ = user.pvsl_;
                    //users.pvvl = user.pvvl;
                    //users.pvpl = user.pvpl;
                    //users.pvcl = user.pvcl;
                    //users.pvsl__ = user.pvsl__;
                    //users.pvel = user.pvel;
                    //users.pvpl_ = user.pvpl_;
                    //users.pvil = user.pvil;
                    //users.pvsl___ = user.pvsl___;
                    //users.pvul = user.pvul;
                    if (user.id == 0)
                    {
                        users.AddedBy = Convert.ToInt32(Session["UserId"]);
                        users.UpdatedBy = 0;
                    }
                    else
                    {
                        users.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        users.AddedBy = 0;
                    }
                    new Catalog().InsertUpdateUser(users);
                    user.pFlag = users.pFlag;
                    user.pDesc = users.pDesc;
                    user.pUserid_Out = users.pUserid_Out;
                    if (user.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (user.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(user.pUserid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = user.id;
                        }
                        activity.ActivityType = "User";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(Session["UserId"]);
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

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllUsers(string Search, int? id)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Users> user = new Catalog().AllUsers(search.Option, search.Search, search.StartDate, search.EndDate, id);

            List<User> users = new List<User>();

            if (user != null)
            {
                foreach (var dbr in user)
                {
                    User li = new User();
                    li.id = dbr.id;
                    li.email = dbr.email;
                    li.password = "".PadRight(dbr.password.Count(), '*');
                    li.attached_profile = (dbr.Name != null) ? dbr.Name.Name : "";
                    li.PremisesName = dbr.PremisesName;
                    li.status = (dbr.status.Equals("1")) ? "Active" : "Inactive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    users.Add(li);
                }
            }

            users.TrimExcess();
            var pusers = users.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = users.Count, recordsFiltered = users.Count, data = pusers }, JsonRequestBehavior.AllowGet);
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
            Users users = new Catalog().SelectUser(id);

            User user = new User();
            user.id = users.id;
            user.email = users.email;
            user.attached_profile = (users.Name != null) ? users.Name.Salutation + " " + users.Name.Name : "";

            user.Company = users.Company;
            user.CompanyName = new Catalog().SelectCompany(Convert.ToInt32(users.Company), null).Name;
            user.Designation = users.Designation;
            user.Landline = users.Landline;
            user.Mobile = users.Mobile;
            user.ContactEmail = users.ContactEmail;
            user.Address = users.Address;
            user.AddressLandline = users.AddressLandline;
            user.City = users.City;
            user.CityName = users.CityName;
            user.Country = users.Country;
            user.CountryName = users.CountryName;
            user.BankAccountNumber = users.BankAccountNumber;
            user.Enable = users.Enable;
            user.status = (users.status.Equals("1")) ? "Active" : "Inactive";
            user.Delete_Status = users.Delete_Status;

            user.Premises_id = users.Premises_id;
            user.PremisesName = users.PremisesName;
            user.PremisesPhone = users.PremisesPhone;
            user.PremisesCity = users.PremisesCity;
            user.PremisesCityName = users.PremisesCityName;
            user.PremisesCountry = users.PremisesCountry;
            user.PremisesCountryName = users.PremisesCountryName;
            user.PremisesAddress = users.PremisesAddress;

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelUser(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            Classes data = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    List<Contacts> employee = new Catalog().AllEmployees(null, null, null, null);
                    List<Contacts> customer = new Catalog().AllCustomers(null, null, null, null);
                    List<Contacts> vendor = new Catalog().AllVendors(null, null, null, null);
                    Users user = new Catalog().SelectUser(id);

                    List<Premisess> stores = new Catalog().AllStores(null, null, null, null);
                    List<Premisess> offices = new Catalog().AllOffices(null, null, null, null);
                    List<Premisess> shops = new Catalog().AllShops(null, null, null, null);
                    List<Premisess> factories = new Catalog().AllFactories(null, null, null, null);

                    data = new Classes();

                    data.Vendors = new List<Contact>();
                    data.Employees = new List<Contact>();
                    data.Customers = new List<Contact>();

                    data.Stores = new List<Premises>();
                    data.Offices = new List<Premises>();
                    data.Shops = new List<Premises>();
                    data.Factories = new List<Premises>();

                    if (employee != null)
                    {
                        foreach (var dbr in employee)
                        {
                            Contact li = new Contact();
                            li.id = dbr.id;
                            li.Name = dbr.Salutation + " " + dbr.Name;
                            data.Employees.Add(li);
                        }
                        data.Employees.TrimExcess();
                    }

                    if (vendor != null)
                    {
                        foreach (var dbr in vendor)
                        {
                            Contact li = new Contact();
                            li.id = dbr.id;
                            li.Name = dbr.Salutation + " " + dbr.Name;
                            data.Vendors.Add(li);
                        }
                        data.Vendors.TrimExcess();
                    }

                    if (customer != null)
                    {
                        foreach (var dbr in customer)
                        {
                            Contact li = new Contact();
                            li.id = dbr.id;
                            li.Name = dbr.Salutation + " " + dbr.Name;
                            data.Customers.Add(li);
                        }
                        data.Customers.TrimExcess();
                    }

                    if (stores != null)
                    {
                        foreach (var dbr in stores)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Stores.Add(li);
                        }
                        data.Stores.TrimExcess();
                    }

                    if (offices != null)
                    {
                        foreach (var dbr in offices)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Offices.Add(li);
                        }
                        data.Offices.TrimExcess();
                    }

                    if (shops != null)
                    {
                        foreach (var dbr in shops)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Shops.Add(li);
                        }
                        data.Shops.TrimExcess();

                    }

                    if (factories != null)
                    {
                        foreach (var dbr in factories)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Factories.Add(li);
                        }
                        data.Factories.TrimExcess();

                    }
                    //var decryptedPassword = PasswordHelper.DecryptPassword(user.password);
                    if (user != null)
                    {
                        data.User = new User
                        {
                            id = user.id,
                            email = user.email,
                            attached_profile = user.attached_profile,
                            password = "".PadRight(user.password.Count(), '*'),
                            //password = "".PadRight(decryptedPassword.Count(),'*'),
                            CountP = user.CountP,
                            Premises_id = user.Premises_id,
                            status = user.status,
                            //pao = user.pao,
                            //paf = user.paf,
                            //pas = user.pas,
                            //pas_ = user.pas_,
                            //pav = user.pav,
                            //pap = user.pap,
                            //pac = user.pac,
                            //pas__ = user.pas__,
                            //pae = user.pae,
                            //pap_ = user.pap_,
                            //pai = user.pai,
                            //pas___ = user.pas___,
                            //pau = user.pau,
                            //puo = user.puo,
                            //puf = user.puf,
                            //pus = user.pus,
                            //pus_ = user.pus_,
                            //puv = user.puv,
                            //pup = user.pup,
                            //puc = user.puc,
                            //pus__ = user.pus__,
                            //pue = user.pue,
                            //pup_ = user.pup_,
                            //pui = user.pui,
                            //pus___ = user.pus___,
                            //puu = user.puu,
                            //pdo = user.pdo,
                            //pdf = user.pdf,
                            //pds = user.pds,
                            //pds_ = user.pds_,
                            //pdv = user.pdv,
                            //pdp = user.pdp,
                            //pdc = user.pdc,
                            //pds__ = user.pds__,
                            //pde = user.pde,
                            //pdp_ = user.pdp_,
                            //pdi = user.pdi,
                            //pds___ = user.pds___,
                            //pdu = user.pdu,
                            //pvo = user.pvo,
                            //pvf = user.pvf,
                            //pvs = user.pvs,
                            //pvs_ = user.pvs_,
                            //pvv = user.pvv,
                            //pvp = user.pvp,
                            //pvc = user.pvc,
                            //pvs__ = user.pvs__,
                            //pve = user.pve,
                            //pvp_ = user.pvp_,
                            //pvi = user.pvi,
                            //pvs___ = user.pvs___,
                            //pvu = user.pvu,
                            //pvol = user.pvol,
                            //pvfl = user.pvfl,
                            //pvsl = user.pvsl,
                            //pvsl_ = user.pvsl_,
                            //pvvl = user.pvvl,
                            //pvpl = user.pvpl,
                            //pvcl = user.pvcl,
                            //pvsl__ = user.pvsl__,
                            //pvel = user.pvel,
                            //pvpl_ = user.pvpl_,
                            //pvil = user.pvil,
                            //pvsl___ = user.pvsl___,
                            //pvul = user.pvul
                        };
                    }

                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return View("User", data);
            }
            catch (Exception e)
            {
                return View("User", data);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            User use = null;
            try
            {
                Users users = new Catalog().SelectUser(id);
                use = new User();
                if (users == null)
                {
                    response = "Please Select A Valid Vendor";
                }
                else
                {
                    Users user = new Users();
                    user.id = id;

                    if (users.status == "1")
                    {
                        user.Enable = 0;
                    }
                    else
                    {
                        user.Enable = 1;
                    }

                    new Catalog().UpdatepUser(user);
                    use.Enable = user.Enable;
                    Activity activity = new Activity();
                    if (use.Enable == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (use.Enable == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "User";
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

            return Json(use, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelUsers(string DeleteUserData)
        {
            string response = "";
            try
            {
                var js = new JavaScriptSerializer();
                List<User> usersrequestedtoDelete = js.Deserialize<List<User>>(DeleteUserData);
                List<Users> users = new List<Users>();

                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in usersrequestedtoDelete)
                {
                    Users li = new Users();
                    li.id = dbr.id;
                    li.email = dbr.email;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;
                    users.Add(li);
                }
                users.TrimExcess();
                DataTable dt = ToDataTable.ListToDataTable(users);

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
                string filename = "Users-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                System.IO.File.WriteAllText(Server.MapPath("~/CSV/Users/" + filename), fileContent.ToString());

                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Users/"));

                SmtpClient email = new SmtpClient();
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                mailMessage.To.Add("salmanahmed635@gmail.com");
                mailMessage.Subject = "Delete Users";
                mailMessage.Body = "Following Users are requested to be deleted";
                foreach (FileInfo file in dir.GetFiles(filename))
                {
                    if (file.Exists)
                    {
                        mailMessage.Attachments.Add(new Attachment(file.FullName));
                    }
                }
                email.Send(mailMessage);


                string type = "Users";
                new Catalog().DelUserRequest(users, type);

            }
            catch (Exception e)
            {
                response = e.ToString();
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RolesDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().RolesDropdown();


            var result = JsonConvert.SerializeObject(dropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult UpdateUser(string UserData)
        //{
        //    String response = "";

        //    try
        //    {

        //        var js = new JavaScriptSerializer();
        //        User user = js.Deserialize<User>(UserData);
        //        Users users = new Users();

        //        Contacts employee = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 0, 1, 1);
        //        Contacts customer = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 1, 0, 1);
        //        Contacts vendor = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 1, 0, 0, 1);

        //        if (string.IsNullOrEmpty(user.email))
        //        {
        //            response = "Enter An Email Address";
        //        }
        //        else if (string.IsNullOrEmpty(user.password))
        //        {
        //            response = "Enter A Password.";
        //        }
        //        else if (string.IsNullOrEmpty(user.attached_profile))
        //        {
        //            response = "Please Select Any Profile For Account. ";
        //        }
        //        else if (employee == null && customer == null && vendor == null)
        //        {
        //            response = "Please Select A Valid Profile For Account. ";
        //        }
        //        else
        //        {
        //            users.id = user.id;
        //            users.email = user.email;
        //            users.attached_profile = user.attached_profile;
        //            users.password = user.password;
        //            users.status = user.status.Equals("1") ? "1" : "0";
        //            users.pao = user.pao;
        //            users.paf = user.paf;
        //            users.pas = user.pas;
        //            users.pas_ = user.pas_;
        //            users.pav = user.pav;
        //            users.pap = user.pap;
        //            users.pac = user.pac;
        //            users.pas__ = user.pas__;
        //            users.pae = user.pae;
        //            users.pap_ = user.pap_;
        //            users.pai = user.pai;
        //            users.pas___ = user.pas___;
        //            users.pau = user.pau;
        //            users.puo = user.puo;
        //            users.puf = user.puf;
        //            users.pus = user.pus;
        //            users.pus_ = user.pus_;
        //            users.puv = user.puv;
        //            users.pup = user.pup;
        //            users.puc = user.puc;
        //            users.pus__ = user.pus__;
        //            users.pue = user.pue;
        //            users.pup_ = user.pup_;
        //            users.pui = user.pui;
        //            users.pus___ = user.pus___;
        //            users.puu = user.puu;
        //            users.pdo = user.pdo;
        //            users.pdf = user.pdf;
        //            users.pds = user.pds;
        //            users.pds_ = user.pds_;
        //            users.pdv = user.pdv;
        //            users.pdp = user.pdp;
        //            users.pdc = user.pdc;
        //            users.pds__ = user.pds__;
        //            users.pde = user.pde;
        //            users.pdp_ = user.pdp_;
        //            users.pdi = user.pdi;
        //            users.pds___ = user.pds___;
        //            users.pdu = user.pdu;
        //            users.pvo = user.pvo;
        //            users.pvf = user.pvf;
        //            users.pvs = user.pvs;
        //            users.pvs_ = user.pvs_;
        //            users.pvv = user.pvv;
        //            users.pvp = user.pvp;
        //            users.pvc = user.pvc;
        //            users.pvs__ = user.pvs__;
        //            users.pve = user.pve;
        //            users.pvp_ = user.pvp_;
        //            users.pvi = user.pvi;
        //            users.pvs___ = user.pvs___;
        //            users.pvu = user.pvu;
        //            users.pvol = user.pvol;
        //            users.pvfl = user.pvfl;
        //            users.pvsl = user.pvsl;
        //            users.pvsl_ = user.pvsl_;
        //            users.pvvl = user.pvvl;
        //            users.pvpl = user.pvpl;
        //            users.pvcl = user.pvcl;
        //            users.pvsl__ = user.pvsl__;
        //            users.pvel = user.pvel;
        //            users.pvpl_ = user.pvpl_;
        //            users.pvil = user.pvil;
        //            users.pvsl___ = user.pvsl___;
        //            users.pvul = user.pvul;

        //            new Catalog().UpdateUser(users);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        response = "Internal Server Error.";
        //    }

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}
    }
}
