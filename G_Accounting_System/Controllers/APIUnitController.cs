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
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace G_Accounting_System.Controllers
{
    [CustomAuthorize]
    public class APIUnitController : ApiController
    {
        [Route("api/APIUnit/GetAllUnits")]
        [HttpPost]
        public IEnumerable<Unit> GetAllUnits()
        {
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                List<Units> brand = new Catalog().AllUnits(search.Option, search.Search, search.StartDate, search.EndDate);

                List<Unit> units = new List<Unit>();

                if (brand != null)
                {
                    foreach (var dbr in brand)
                    {
                        Unit li = new Unit();
                        li.id = dbr.id;
                        li.Unit_Name = dbr.Unit_Name;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        units.Add(li);
                    }
                }

                units.TrimExcess();

                return units;
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

        [Route("api/APIUnit/UnitById")]
        [HttpPost]
        public Classes UnitById()
        {
            Unit unit = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                unit = js.Deserialize<Unit>(strJson);


                Units units = new Catalog().SelectUnit(Convert.ToInt32(unit.id));
                Classes data = new Classes();
                data.Unit = new Unit();
                data.Unit.id = units.id;
                data.Unit.Unit_Name = units.Unit_Name;
                data.Unit.Enable = units.Enable;
                int ActivityType_id = unit.id;
                string ActivityType = "Unit";
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

        [Route("api/APIUnit/InsertUpdateUnits")]
        [HttpPost]
        public Unit InsertUpdateUnits()
        {
            Unit unit = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    unit = js.Deserialize<Unit>(strJson);

                    Units AddUnit = new Units();
                    AddUnit.id = unit.id;
                    AddUnit.Unit_Name = unit.Unit_Name;
                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (unit.id == 0)
                    {
                        AddUnit.AddedBy = Convert.ToInt32(User_id);
                        AddUnit.UpdatedBy = 0;
                    }
                    else
                    {
                        AddUnit.UpdatedBy = Convert.ToInt32(User_id);
                        AddUnit.AddedBy = 0;
                    }

                    new Catalog().InsertUpdateUnit(AddUnit);
                    unit.pFlag = AddUnit.pFlag;
                    unit.pDesc = AddUnit.pDesc;
                    unit.pUnitid_Out = AddUnit.pUnitid_Out;
                    if (unit.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (unit.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(unit.pUnitid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = unit.id;
                        }
                        activity.ActivityType = "Unit";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return unit;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIUnit/SendRequestToDelUnits")]
        [HttpPost]
        public object SendRequestToDelUnits()
        {
            List<Units> units = null;
            List<Unit> unitsNotDelete = null;
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                data = js.Deserialize<Classes>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    units = new List<Units>();
                    unitsNotDelete = new List<Unit>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in data.Units)
                    {
                        Units li = new Units();
                        Units liChecked = new Units();
                        li.id = dbr.id;
                        li.Unit_Name = dbr.Unit_Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = "Requested";

                        liChecked = new Catalog().CheckUnitForDelete(li.id);
                        if (liChecked != null)
                        {
                            Unit un = new Unit();
                            un.Unit_Name = liChecked.Unit_Name;
                            unitsNotDelete.Add(un);
                        }
                        else
                        {
                            units.Add(li);
                        }

                    }
                    units.TrimExcess();
                    unitsNotDelete.TrimExcess();
                    if (units.Count != 0)
                    {
                        DataTable dt = ToDataTable.ListToDataTable(units);

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
                        string filename = "Unit-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Unit/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Unit/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                        MailMessage mailMessage = new MailMessage();

                        mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                        mailMessage.To.Add("salmanahmed635@gmail.com");
                        mailMessage.Subject = "Delete Units";
                        mailMessage.Body = "Following Units are requested to be deleted";
                        foreach (FileInfo file in dir.GetFiles(filename))
                        {
                            if (file.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(file.FullName));
                            }
                        }
                        smtpClient.Send(mailMessage);

                        string type = "Units";
                        new Catalog().DelUnitRequest(units, type);
                    }

                }
                return new { units, unitsNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
