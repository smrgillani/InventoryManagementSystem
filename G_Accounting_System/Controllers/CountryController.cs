using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using G_Accounting_System.Models;
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
    public class CountryController : Controller
    {
        //
        // GET: /Country/
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int? id)
        {
            bool check = new PermissionsClass().CheckAddPermission();
            if (check != false)
            {
                return View("Country");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateCountry(string Country)
        {
            string response = "";
            Country country = null;
            string ActivityName = null;
            try
            {
                var js = new JavaScriptSerializer();
                country = js.Deserialize<Country>(Country);

                if (string.IsNullOrEmpty(country.Name))
                {
                    response = "Enter Country Full Name In General Details.";
                }
                else
                {

                    Countries AddCountry = new Countries();
                    AddCountry.id = country.id;
                    AddCountry.Name = country.Name;
                    AddCountry.Enable = country.IsEnabled.Equals(1) ? 1 : 0;
                    if (country.id == 0)
                    {
                        AddCountry.AddedBy = Convert.ToInt32(Session["UserId"]);
                        AddCountry.UpdatedBy = 0;
                    }
                    else
                    {
                        AddCountry.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        AddCountry.AddedBy = 0;
                    }
                    new Catalog().InsertUpdateCountry(AddCountry);
                    country.pFlag = AddCountry.pFlag;
                    country.pDesc = AddCountry.pDesc;
                    country.pCountryid_Out = AddCountry.pCountryid_Out;
                    if (country.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (country.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(country.pCountryid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = country.id;
                        }
                        activity.ActivityType = "Country";
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

            return Json(country, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCountries(int? id, string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Countries> country = new Catalog().SelectAllAICountries(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Country> countries = new List<Country>();

            if (country != null)
            {
                foreach (var dbr in country)
                {
                    Country li = new Country();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.IsEnabled = dbr.Enable;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    countries.Add(li);
                }
            }

            countries.TrimExcess();
            var pcountries = countries.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = countries.Count, recordsFiltered = countries.Count, data = pcountries }, JsonRequestBehavior.AllowGet);
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
            Countries countries = new Catalog().SelectCountry(id, null);

            Country country = new Country();
            country.id = countries.id;
            country.Name = countries.Name;
            country.IsEnabled = countries.Enable;
            country.IsEnabled_ = (countries.Enable == 1) ? "Active" : "InActive";
            country.Delete_Status = countries.Delete_Status;
            return Json(country, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            Country country = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    Countries countries = new Catalog().SelectCountry(id, null);

                    country = new Country();
                    country.id = countries.id;
                    country.Name = countries.Name;
                    country.IsEnabled = countries.Enable;
                    country.IsEnabled_ = (countries.Enable == 1) ? "Active" : "InActive";
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return View("Country", country);
            }
            catch (Exception e)
            {
                return View("Country", country);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Country cou = null;
            try
            {
                Countries countries = new Catalog().SelectCountry(id, null);
                cou = new Country();
                if (countries == null)
                {
                    response = "Please Select A Valid Country";
                }
                else
                {
                    Countries country = new Countries();
                    country.id = id;

                    if (countries.Enable == 1)
                    {
                        country.Enable = 0;
                    }
                    else
                    {
                        country.Enable = 1;
                    }

                    new Catalog().UpdatepCountry(country);
                    cou.IsEnabled = country.Enable;
                    Activity activity = new Activity();
                    if (cou.IsEnabled == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (cou.IsEnabled == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Country";
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

            return Json(cou, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult UpdateCountry(string Country)
        //{
        //    string response = "";

        //    try
        //    {

        //        var js = new JavaScriptSerializer();
        //        Country country = js.Deserialize<Country>(Country);

        //        if (string.IsNullOrEmpty(country.Name))
        //        {
        //            response = "Enter Country Full Name In General Details.";
        //        }
        //        else
        //        {

        //            Countries AddCountry = new Countries();
        //            AddCountry.id = country.id;
        //            AddCountry.Name = country.Name;
        //            AddCountry.Enable = country.IsEnabled.Equals("1") ? 1 : 0;
        //            new Catalog().UpdateCountry(AddCountry);
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
                new Catalog().DelCountry(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelCountries(string DeleteCountryData)
        {
            string response = "";
            List<Countries> countries = null;
            List<Country> countryNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Country> countriessrequestedtoDelete = js.Deserialize<List<Country>>(DeleteCountryData);
                countries = new List<Countries>();
                activities = new List<Activity>();
                countryNotDelete = new List<Country>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in countriessrequestedtoDelete)
                {
                    Countries li = new Countries();
                    Countries liChecked = new Countries();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

                    liChecked = new Catalog().CheckCountryForDelete(li.id);
                    if (liChecked != null)
                    {
                        Country co = new Country();
                        co.Name = liChecked.Name;
                        countryNotDelete.Add(co);
                    }
                    else
                    {
                        countries.Add(li);
                    }
                }
                countries.TrimExcess();
                countryNotDelete.TrimExcess();
                if (countries.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(countries);

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
                    string filename = "Countries-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Countries/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Countries/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Countries";
                    mailMessage.Body = "Following Countries are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Countries";
                    string Result = new Catalog().DelCountryRequest(countries, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (countries != null)
                        {
                            foreach (var i in countries)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Country";
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

            return Json(new { Response = response, Countries = countries, CountriesNotDelete = countryNotDelete }, JsonRequestBehavior.AllowGet);
        }

    }
}
