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
    public class APICountryController : ApiController
    {
        [Route("api/APICountry/GetAllCountries")]
        [HttpGet]
        public IEnumerable<Country> GetAllCountries()
        {
            List<Country> countries = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Countries> country = new Catalog().SelectAllAICountries(null,null,null,null);

                countries = new List<Country>();

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
                        li.AddedBy = dbr.AddedBy;
                        li.UpdatedBy = dbr.UpdatedBy;
                        countries.Add(li);
                    }
                }

                return countries;
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

        [Route("api/APICountry/CountryById")]
        [HttpGet]
        public Classes CountryById()
        {
            Country country = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                country = js.Deserialize<Country>(strJson);

                Countries countries = new Catalog().SelectCountry(country.id, null);

                Classes data = new Classes();
                data.Country = new Country();
                data.Country.id = countries.id;
                data.Country.Name = countries.Name;
                data.Country.IsEnabled = countries.Enable;
                data.Country.IsEnabled_ = (countries.Enable == 1) ? "Active" : "InActive";
                data.Country.AddedBy = countries.AddedBy;
                data.Country.UpdatedBy = countries.UpdatedBy;
                int ActivityType_id = country.id;
                string ActivityType = "Country";
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

        [Route("api/APICountry/InsertUpdateCountries")]
        [HttpPost]
        public Country InsertUpdateCountries()
        {
            Country country = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    country = js.Deserialize<Country>(strJson);

                    Countries AddCountry = new Countries();
                    AddCountry.id = country.id;
                    AddCountry.Name = country.Name;
                    AddCountry.Enable = country.IsEnabled.Equals(Convert.ToInt32("1")) ? 1 : 0;

                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (country.id == 0)
                    {
                        AddCountry.AddedBy = Convert.ToInt32(User_id);
                        AddCountry.UpdatedBy = 0;
                        country.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddCountry.UpdatedBy = Convert.ToInt32(User_id);
                        AddCountry.AddedBy = 0;
                        country.UpdatedBy = Convert.ToInt32(User_id);
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
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return country;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APICountry/SendRequestToDelCountries")]
        [HttpPost]
        public object SendRequestToDelCountries()
        {
            List<Countries> countries = null;
            List<Country> countryNotDelete = null;
            List<Country> countriessrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                countriessrequestedtoDelete = js.Deserialize<List<Country>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    countries = new List<Countries>();
                    countryNotDelete = new List<Country>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in countriessrequestedtoDelete)
                    {
                        Countries li = new Countries();
                        Countries liChecked = new Countries();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
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
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Countries/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Countries/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Countries";
                        new Catalog().DelCountryRequest(countries, type);
                    }

                }
                return new { countries, countryNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
