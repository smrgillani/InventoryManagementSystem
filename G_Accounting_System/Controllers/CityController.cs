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
    public class CityController : Controller
    {
        //
        // GET: /City/
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
                List<Countries> country = new Catalog().SelectAllAICountries(null, null, null, null);

                Classes data = new Classes();

                data.Countries = new List<Country>();

                if (country != null)
                {
                    foreach (var dbr in country)
                    {
                        Country li = new Country();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        data.Countries.Add(li);
                    }
                }

                data.Countries.TrimExcess();

                return View("City", data);
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateCities(string City)
        {
            string response = "";
            City city = null;
            string ActivityName = null;
            try
            {
                var js = new JavaScriptSerializer();
                city = js.Deserialize<City>(City);

                Countries country = new Catalog().SelectCountry(Convert.ToInt32(city.Country), 1);

                if (string.IsNullOrEmpty(city.Name))
                {
                    response = "Enter City Full Name In General Details.";
                }
                else if (country == null)
                {
                    city.pFlag = "0";
                    city.pDesc = "Please Select A Valid Country For City";
                }
                else
                {

                    Cities AddCity = new Cities();
                    AddCity.id = city.id;
                    AddCity.Name = city.Name;
                    AddCity.Country = city.Country;
                    AddCity.Enable = city.IsEnabled.Equals("1") ? 1 : 0;
                    if (city.id == 0)
                    {
                        AddCity.AddedBy = Convert.ToInt32(Session["UserId"]);
                        AddCity.UpdatedBy = 0;
                    }
                    else
                    {
                        AddCity.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        AddCity.AddedBy = 0;
                    }
                    new Catalog().InsertUpdateCities(AddCity);

                    city.pFlag = AddCity.pFlag;
                    city.pDesc = AddCity.pDesc;
                    city.pCityid_Out = AddCity.pCityid_Out;
                    if (city.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (city.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(city.pCityid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = city.id;
                        }
                        activity.ActivityType = "City";
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

            return Json(city, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCities(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);

            List<Cities> city = new Catalog().SelectAllAICities(search.Option, search.Search, search.StartDate, search.EndDate);

            List<City> cities = new List<City>();

            if (city != null)
            {
                foreach (var dbr in city)
                {
                    City li = new City();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.Country = dbr.CountryName.id;
                    li.CountryName = dbr.CountryName.Name;
                    li.IsEnabled = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    cities.Add(li);
                }
            }

            cities.TrimExcess();

            var pcities = cities.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = cities.Count, recordsFiltered = cities.Count, data = pcities }, JsonRequestBehavior.AllowGet);
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
            Cities cities = new Catalog().SelectCity(id, null);

            City city = new City();
            city.id = cities.id;
            city.Name = cities.Name;
            city.Country = cities.Country;
            city.IsEnabled = cities.Enable.ToString();
            city.IsEnabled_ = (cities.Enable == 1) ? "Active" : "InActive";
            city.Delete_Status = cities.Delete_Status;
            return Json(city, JsonRequestBehavior.AllowGet);
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
                    List<Countries> country = new Catalog().SelectAllAICountries(null, null, null, null);

                    Cities cities = new Catalog().SelectCity(id, null);

                    data = new Classes();

                    data.Countries = new List<Country>();

                    data.City = new City()
                    {
                        id = cities.id,
                        Name = cities.Name,
                        Country = cities.Country,
                        IsEnabled = cities.Enable.ToString()
                    };

                    if (country != null)
                    {
                        foreach (var dbr in country)
                        {
                            Country li = new Country();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            data.Countries.Add(li);
                        }
                    }

                    data.Countries.TrimExcess();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }

            }
            catch (Exception e)
            {
                return View("City", data);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult UpdateCity(string City)
        //{
        //    string response = "";

        //    try
        //    {
        //        var js = new JavaScriptSerializer();
        //        City city = js.Deserialize<City>(City);

        //        Countries country = new Catalog().SelectCountry(Convert.ToInt32(city.Country), 1);

        //        if (string.IsNullOrEmpty(city.Name))
        //        {
        //            response = "Enter City Full Name In General Details.";
        //        }
        //        else if (country == null)
        //        {
        //            response = "Please Select A Valid Country For City";
        //        }
        //        else
        //        {
        //            Cities Cities = new Cities();
        //            Cities.id = city.id;
        //            Cities.Name = city.Name;
        //            Cities.Country = city.Country;
        //            Cities.Enable = city.IsEnabled.Equals("1") ? 1 : 0;
        //            new Catalog().UpdateCity(Cities);
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
                new Catalog().DelCity(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string response = "";
            City cit = null;
            string ActivityName = null;
            try
            {
                Cities cities = new Catalog().SelectCity(id, null);
                cit = new City();
                if (cities == null)
                {
                    response = "Please Select A Valid City";
                }
                else
                {
                    Cities Cities = new Cities();
                    Cities.id = id;

                    if (cities.Enable == 1)
                    {
                        Cities.Enable = 0;
                    }
                    else
                    {
                        Cities.Enable = 1;
                    }

                    new Catalog().UpdatepCity(Cities);
                    cit.IsEnabled = Cities.Enable.ToString();
                    Activity activity = new Activity();
                    if (cit.IsEnabled == "1")
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (cit.IsEnabled == "0")
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "City";
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

            return Json(cit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelCities(string DeleteCityData)
        {
            string response = "";
            List<Cities> cities = null;
            List<City> citiesNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<City> citiesrequestedtoDelete = js.Deserialize<List<City>>(DeleteCityData);
                cities = new List<Cities>();
                citiesNotDelete = new List<City>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);
                activities = new List<Activity>();
                foreach (var dbr in citiesrequestedtoDelete)
                {
                    Cities li = new Cities();
                    Cities liChecked = new Cities();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

                    liChecked = new Catalog().CheckCityForDelete(li.id);
                    if (liChecked != null)
                    {
                        City br = new City();
                        br.Name = liChecked.Name;
                        citiesNotDelete.Add(br);
                    }
                    else
                    {
                        cities.Add(li);
                    }
                }
                cities.TrimExcess();
                citiesNotDelete.TrimExcess();
                if (cities.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(cities);

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
                    string filename = "Cities-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Cities/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Cities/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Cities";
                    mailMessage.Body = "Following Cities are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Cities";
                    string Result = new Catalog().DelCityRequest(cities, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (cities != null)
                        {
                            foreach (var i in cities)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "City";
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

            return Json(new { Response = response, Cities = cities, CitiesNotDelete = citiesNotDelete }, JsonRequestBehavior.AllowGet);
        }

    }
}
