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
    public class APICityController : ApiController
    {
        [Route("api/APICity/GetAllCities")]
        [HttpGet]
        public IEnumerable<City> GetAllCities()
        {
            List<City> cities = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Cities> city = new Catalog().SelectAllAICities(search.Option, search.Search, search.StartDate, search.EndDate);

                cities = new List<City>();

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
                        li.AddedBy = dbr.AddedBy;
                        li.UpdatedBy = dbr.UpdatedBy;
                        cities.Add(li);
                    }
                }

                cities.TrimExcess();

                return cities;
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

        [Route("api/APICity/CityById")]
        [HttpGet]
        public Classes CityById()
        {
            City city = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                city = js.Deserialize<City>(strJson);

                Cities cities = new Catalog().SelectCity(city.id, null);

                Classes data = new Classes();
                data.City = new City();
                data.City.id = cities.id;
                data.City.Name = cities.Name;
                data.City.Country = cities.Country;
                data.City.CountryName = cities.CountryName.Name;
                data.City.IsEnabled = cities.Enable.ToString();
                data.City.IsEnabled_ = (cities.Enable == 1) ? "Active" : "InActive";
                data.City.AddedBy = cities.AddedBy;
                data.City.UpdatedBy = cities.UpdatedBy;
                int ActivityType_id = city.id;
                string ActivityType = "City";
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

        [Route("api/APICity/InsertUpdateCities")]
        [HttpPost]
        public City InsertUpdateCities()
        {
            City city = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    city = js.Deserialize<City>(strJson);

                    Cities AddCity = new Cities();
                    AddCity.id = city.id;
                    AddCity.Name = city.Name;
                    AddCity.Country = city.Country;
                    AddCity.Enable = city.IsEnabled.Equals("1") ? 1 : 0;

                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (city.id == 0)
                    {
                        AddCity.AddedBy = Convert.ToInt32(User_id);
                        AddCity.UpdatedBy = 0;
                        city.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddCity.UpdatedBy = Convert.ToInt32(User_id);
                        AddCity.AddedBy = 0;
                        city.UpdatedBy = Convert.ToInt32(User_id);
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
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return city;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APICity/SendRequestToDelCities")]
        [HttpPost]
        public object SendRequestToDelCities()
        {
            List<Cities> cities = null;
            List<City> citiesNotDelete = null;
            List<City> citiesrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                citiesrequestedtoDelete = js.Deserialize<List<City>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    cities = new List<Cities>();
                    citiesNotDelete = new List<City>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in citiesrequestedtoDelete)
                    {
                        Cities li = new Cities();
                        Cities liChecked = new Cities();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
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
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Cities/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Cities/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Cities";
                        new Catalog().DelCityRequest(cities, type);
                    }

                }
                return new { cities, citiesNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
