using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace G_Accounting_System.Controllers
{
    [CustomAuthorize]
    public class APIManufacturerController : ApiController
    {
        [Route("api/APIManufacturer/GetAllManufacturers")]
        [HttpPost]
        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                List<Manufacturers> manufacturer = new Catalog().AllManufaturers(search.Option, search.Search, search.StartDate, search.EndDate);

                List<Manufacturer> manufacturers = new List<Manufacturer>();

                if (manufacturer != null)
                {
                    foreach (var dbr in manufacturer)
                    {
                        Manufacturer li = new Manufacturer();
                        li.id = dbr.id;
                        li.Manufacturer_Name = dbr.Manufacturer_Name;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        manufacturers.Add(li);
                    }
                }

                manufacturers.TrimExcess();

                return manufacturers;
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

        [Route("api/APIManufacturer/ManufacturerById")]
        [HttpPost]
        public Classes ManufacturerById()
        {
            Manufacturer manufacturer = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                manufacturer = js.Deserialize<Manufacturer>(strJson);


                Manufacturers manufacturers = new Catalog().SelectManufacture(Convert.ToInt32(manufacturer.id));
                Classes data = new Classes();
                data.Manufacturer = new Manufacturer();
                data.Manufacturer.id = manufacturers.id;
                data.Manufacturer.Manufacturer_Name = manufacturers.Manufacturer_Name;
                data.Manufacturer.Enable = manufacturers.Enable;
                int ActivityType_id = manufacturer.id;
                string ActivityType = "Manufacturer";
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

        [Route("api/APIManufacturer/InsertUpdateManufacturers")]
        [HttpPost]
        public Manufacturer InsertUpdateManufacturers()
        {
            Manufacturer manufacturer = null;
            string ActivityName = null;
            try
            {
                HttpRequest resolveRequest = HttpContext.Current.Request;
                //List<YourModel> model = new List<YourModel>();
                resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
                string ManufacturerData = new StreamReader(resolveRequest.InputStream).ReadToEnd();
                if (ManufacturerData != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    manufacturer = js.Deserialize<Manufacturer>(ManufacturerData);

                    Manufacturers AddManufacturer = new Manufacturers();
                    AddManufacturer.id = manufacturer.id;
                    AddManufacturer.Manufacturer_Name = manufacturer.Manufacturer_Name;
                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (manufacturer.id == 0)
                    {
                        AddManufacturer.AddedBy = Convert.ToInt32(User_id);
                        AddManufacturer.UpdatedBy = 0;
                    }
                    else
                    {
                        AddManufacturer.UpdatedBy = Convert.ToInt32(User_id);
                        AddManufacturer.AddedBy = 0;
                    }

                    new Catalog().InsertUpdateManufacturer(AddManufacturer);
                    manufacturer.pFlag = AddManufacturer.pFlag;
                    manufacturer.pDesc = AddManufacturer.pDesc;
                    manufacturer.pManufacturerid_Out = AddManufacturer.pManufacturerid_Out;
                    if (manufacturer.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (manufacturer.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(manufacturer.pManufacturerid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = manufacturer.id;
                        }
                        activity.ActivityType = "Manufacturer";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return manufacturer;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIManufacturer/SendRequestToDelManufacturers")]
        [HttpPost]
        public object SendRequestToDelManufacturers()
        {
            List<Manufacturers> manufacturers = null;
            List<Manufacturer> manufacturersNotDelete = null;
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                data = js.Deserialize<Classes>(strJson);


                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    manufacturers = new List<Manufacturers>();
                    manufacturersNotDelete = new List<Manufacturer>();

                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in data.Manufacturers)
                    {
                        Manufacturers li = new Manufacturers();
                        Manufacturers liChecked = new Manufacturers();
                        li.id = dbr.id;
                        li.Manufacturer_Name = dbr.Manufacturer_Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = "Requested";
                        liChecked = new Catalog().CheckManufacturerForDelete(li.id);
                        if (liChecked != null)
                        {
                            Manufacturer man = new Manufacturer();
                            man.Manufacturer_Name = liChecked.Manufacturer_Name;
                            manufacturersNotDelete.Add(man);
                        }
                        else
                        {
                            manufacturers.Add(li);
                        }
                    }
                    manufacturers.TrimExcess();
                    manufacturersNotDelete.TrimExcess();
                    if (manufacturers.Count != 0)
                    {
                        DataTable dt = ToDataTable.ListToDataTable(manufacturers);

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
                        string filename = "Manufacturer-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Manufacturer/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Manufacturer/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                        MailMessage mailMessage = new MailMessage();

                        mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                        mailMessage.To.Add("salmanahmed635@gmail.com");
                        mailMessage.Subject = "Delete Manufacturers";
                        mailMessage.Body = "Following Manufacturers are requested to be deleted";
                        foreach (FileInfo file in dir.GetFiles(filename))
                        {
                            if (file.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(file.FullName));
                            }
                        }
                        smtpClient.Send(mailMessage);

                        string type = "Manufacturers";
                        new Catalog().DelManufacturerRequest(manufacturers, type);
                    }

                }
                return new { manufacturers, manufacturersNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
