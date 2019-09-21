using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using G_Accounting_System.Models;
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
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        [PermissionsAuthorize]
        public ActionResult Index(int? id)
        {
            return View();
        }

        public ActionResult Add()
        {
            bool check = new PermissionsClass().CheckAddPermission();
            if (check != false)
            {
                return View("Company");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateCompanies(string Company)
        {
            string response = "";
            Company company = null;
            string ActivityName = null;
            try
            {
                var js = new JavaScriptSerializer();
                company = js.Deserialize<Company>(Company);

                if (string.IsNullOrEmpty(company.Name))
                {
                    response = "Enter Company Full Name In General Details.";
                    company.pFlag = "0";
                    company.pDesc = "Enter Company Full Name In General Details.";
                }
                else if (string.IsNullOrEmpty(company.Landline))
                {
                    response = "Enter Landline Number Of Company In General Details.";
                    company.pFlag = "0";
                    company.pDesc = "Enter Landline Number Of Company In General Details.";
                }
                else if (string.IsNullOrEmpty(company.Mobile))
                {
                    response = "Enter Mobile Number Of Company In General Details.";
                    company.pFlag = "0";
                    company.pDesc = "Enter Mobile Number Of Company In General Details.";
                }
                else if (string.IsNullOrEmpty(company.Address))
                {
                    response = "Enter Office Location Address Of Company In Address Details.";
                    company.pFlag = "0";
                    company.pDesc = "Enter Office Location Address Of Company In Address Details.";
                }
                else if (company.Country == "0" || company.Country == null)
                {
                    response = "Enter Office Location Address Of Company In Address Details.";
                    company.pFlag = "0";
                    company.pDesc = "Select Country";
                }
                else if (company.City == "0" || company.City == null)
                {
                    response = "Enter Office Location Address Of Company In Address Details.";
                    company.pFlag = "0";
                    company.pDesc = "Select City";
                }
                else
                {

                    Companies AddCompany = new Companies();
                    AddCompany.id = company.id;
                    AddCompany.Name = company.Name;
                    AddCompany.Landline = company.Landline;
                    AddCompany.Mobile = company.Mobile;
                    AddCompany.Email = company.Email;
                    AddCompany.Website = company.Website;
                    AddCompany.Address = company.Address;
                    AddCompany.City = company.City;
                    AddCompany.Country = company.Country;
                    AddCompany.BankAccountNumber = company.BankAccountNumber;
                    AddCompany.PaymentMethod = (company.PaymentMethod.Equals("1") || company.PaymentMethod.Equals("2") || company.PaymentMethod.Equals("3")) ? company.PaymentMethod : "1";
                    AddCompany.Enable = company.IsEnabled.Equals("1") ? 1 : 0;
                    Countries country = new Catalog().SelectCountry(Convert.ToInt32(company.Country), null);
                    Cities city = new Catalog().SelectCity(Convert.ToInt32(company.City), null);
                    if (country != null && city != null)
                    {
                        if (company.id == 0)
                        {
                            AddCompany.AddedBy = Convert.ToInt32(Session["UserId"]);
                            AddCompany.UpdatedBy = 0;
                        }
                        else
                        {
                            AddCompany.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                            AddCompany.AddedBy = 0;
                        }
                        new Catalog().InsertUpdateCompanies(AddCompany);

                        company.pFlag = AddCompany.pFlag;
                        company.pDesc = AddCompany.pDesc;
                        company.pCompanyid_Out = AddCompany.pCompanyid_Out;
                        if (company.pFlag == "1")
                        {
                            Activity activity = new Activity();
                            if (company.id == 0)
                            {
                                ActivityName = "Created";
                                activity.ActivityType_id = Convert.ToInt32(company.pCompanyid_Out);
                            }
                            else
                            {
                                ActivityName = "Updated";
                                activity.ActivityType_id = company.id;
                            }
                            activity.ActivityType = "Company";
                            activity.ActivityName = ActivityName;
                            activity.User_id = Convert.ToInt32(Session["UserId"]);
                            activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                            List<Activity> activities = new List<Activity>();
                            activities.Add(activity);


                            new ActivitiesClass().InsertActivity(activities);
                        }
                    }
                    else
                    {
                        company.pFlag = "0";
                        company.pDesc = "Provided data is incorrect";

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(company, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCompanies(int? id, string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Companies> company = new Catalog().SelectAllAICompanies(search.Option, search.Search, search.StartDate, search.EndDate, id);

            List<Company> companies = new List<Company>();

            if (company != null)
            {
                foreach (var dbr in company)
                {
                    Company li = new Company();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.Landline = dbr.Landline;
                    li.Mobile = dbr.Mobile;
                    li.Email = dbr.Email;
                    li.City = dbr.City;
                    li.CityName = new Catalog().SelectCity(Convert.ToInt32(dbr.City), null).Name;
                    li.Country = dbr.Country;
                    li.CountryName = new Catalog().SelectCountry(Convert.ToInt32(dbr.Country), null).Name;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.BankAccountNumber = dbr.BankAccountNumber;
                    li.PaymentMethod = (dbr.PaymentMethod.Equals("1") ? "Cheque" : "") + (dbr.PaymentMethod.Equals("2") ? "Cash" : "") + (dbr.PaymentMethod.Equals("3") ? "Card" : "");
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    companies.Add(li);
                }
            }

            companies.TrimExcess();
            var pcompanies = companies.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = companies.Count, recordsFiltered = companies.Count, data = pcompanies }, JsonRequestBehavior.AllowGet);
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
            Companies companies = new Catalog().SelectCompany(Convert.ToInt32(id), null);

            Company company = new Company();
            company.id = companies.id;
            company.Name = companies.Name;
            company.Landline = companies.Landline;
            company.Mobile = companies.Mobile;
            company.Email = companies.Email;
            company.Website = companies.Website;
            company.Address = companies.Address;
            company.City = companies.City;
            company.CityName = new Catalog().SelectCity(Convert.ToInt32(companies.City), null).Name;
            company.Country = companies.Country;
            company.CountryName = new Catalog().SelectCountry(Convert.ToInt32(companies.Country), null).Name;
            company.BankAccountNumber = companies.BankAccountNumber;
            company.PaymentMethod = companies.PaymentMethod;
            company.Payment_method_ = (companies.PaymentMethod.Equals("1") ? "Cheque" : "") + (companies.PaymentMethod.Equals("2") ? "Cash" : "") + (companies.PaymentMethod.Equals("3") ? "Card" : "");
            company.IsEnabled = companies.Enable.ToString();
            company.IsEnabled_ = (companies.Enable == 1) ? "Active" : "InActive";
            company.Delete_Status = companies.Delete_Status;
            return Json(company, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(int id)
        {
            Company company = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    Companies companies = new Catalog().SelectCompany(Convert.ToInt32(id), null);

                    company = new Company();
                    company.id = companies.id;
                    company.Name = companies.Name;
                    company.Landline = companies.Landline;
                    company.Mobile = companies.Mobile;
                    company.Email = companies.Email;
                    company.Website = companies.Website;
                    company.Address = companies.Address;
                    company.City = companies.City;
                    company.Country = companies.Country;
                    company.BankAccountNumber = companies.BankAccountNumber;
                    company.PaymentMethod = companies.PaymentMethod;
                    company.IsEnabled = companies.Enable.ToString();
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(company, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(company, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Company com = null;
            try
            {
                Companies companies = new Catalog().SelectCompany(id, null);
                com = new Company();
                if (companies == null)
                {
                    response = "Please Select A Valid Vendor";
                }
                else
                {
                    Companies company = new Companies();
                    company.id = id;

                    if (companies.Enable == 1)
                    {
                        company.Enable = 0;
                    }
                    else
                    {
                        company.Enable = 1;
                    }

                    new Catalog().UpdatepCompany(company);
                    com.IsEnabled = company.Enable.ToString();
                    Activity activity = new Activity();
                    if (com.IsEnabled == "1")
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (com.IsEnabled == "0")
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Company";
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

            return Json(com, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult UpdateCompany(string Company)
        //{
        //    string response = "";

        //    try
        //    {

        //        var js = new JavaScriptSerializer();
        //        Company company = js.Deserialize<Company>(Company);

        //        if (string.IsNullOrEmpty(company.Name))
        //        {
        //            response = "Enter Company Full Name In General Details.";
        //        }
        //        else if (string.IsNullOrEmpty(company.Landline))
        //        {
        //            response = "Enter Landline Number Of Company In General Details.";
        //        }
        //        else if (string.IsNullOrEmpty(company.Mobile))
        //        {
        //            response = "Enter Mobile Number Of Company In General Details.";
        //        }
        //        else if (string.IsNullOrEmpty(company.Address))
        //        {
        //            response = "Enter Office Location Address Of Company In Address Details.";
        //        }
        //        else
        //        {
        //            Companies Companies = new Companies();
        //            Companies.id = company.id;
        //            Companies.Name = company.Name;
        //            Companies.Landline = company.Landline;
        //            Companies.Mobile = company.Mobile;
        //            Companies.Email = company.Email;
        //            Companies.Website = company.Website;
        //            Companies.Address = company.Address;
        //            Companies.City = company.City;
        //            Companies.Country = company.Country;
        //            Companies.BankAccountNumber = company.BankAccountNumber;
        //            Companies.PaymentMethod = (company.PaymentMethod.Equals("1") || company.PaymentMethod.Equals("2") || company.PaymentMethod.Equals("3")) ? company.PaymentMethod : "1";
        //            Companies.Enable = company.IsEnabled.Equals("1") ? 1 : 0;
        //            new Catalog().UpdateCompany(Companies);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        response = "Internal Server Error.";
        //    }

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelCompany(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelCompanies(string DeleteCompanyData)
        {
            string response = "";
            List<Companies> companies = null;
            List<Company> companiesNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Company> companysrequestedtoDelete = js.Deserialize<List<Company>>(DeleteCompanyData);
                companies = new List<Companies>();
                companiesNotDelete = new List<Company>();
                activities = new List<Activity>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in companysrequestedtoDelete)
                {
                    Companies li = new Companies();
                    Companies liChecked = new Companies();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;
                    liChecked = new Catalog().CheckCompanyForDelete(li.id);
                    if (liChecked != null)
                    {
                        Company co = new Company();
                        co.Name = liChecked.Name;
                        companiesNotDelete.Add(co);
                    }
                    else
                    {
                        companies.Add(li);
                    }

                }
                companies.TrimExcess();
                companiesNotDelete.TrimExcess();
                if (companies.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(companies);

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
                    string filename = "Companies-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Companies/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Companies/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Companies";
                    mailMessage.Body = "Following Companies are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Companies";
                    string Result = new Catalog().DelCompanyRequest(companies, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (companies != null)
                        {
                            foreach (var i in companies)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Company";
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

            return Json(new { Response = response, Companies = companies, CompaniesNotDelete = companiesNotDelete }, JsonRequestBehavior.AllowGet);
        }
    }
}
