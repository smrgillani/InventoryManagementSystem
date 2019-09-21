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
using System.Data;
using System.Text;
using System.IO;
using System.Net.Mail;
using G_Accounting_System.Code.Helpers;
using System.Net;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class UnitController : Controller
    {
        // GET: Unit
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            bool check = new PermissionsClass().CheckAddPermission();
            if (check != false)
            {
                return View("Unit");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllUnits(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Units> brand = new Catalog().AllUnits(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Unit> units = new List<Unit>();

            if (brand != null)
            {
                foreach (var dbr in brand)
                {
                    Unit li = new Unit();
                    li.id = dbr.id;
                    li.Unit_Name = dbr.Unit_Name;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    units.Add(li);
                }
            }

            units.TrimExcess();
            var punits = units.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = units.Count, recordsFiltered = units.Count, data = punits }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateUnit(string UnitData)
        {
            var js = new JavaScriptSerializer();
            Unit unit = null;
            string ActivityName = null;
            try
            {
                unit = js.Deserialize<Unit>(UnitData);

                Units AddUnit = new Units();
                AddUnit.id = unit.id;
                AddUnit.Unit_Name = unit.Unit_Name;
                AddUnit.Enable = 1;
                if (unit.id == 0)
                {
                    AddUnit.AddedBy = Convert.ToInt32(Session["UserId"]);
                    AddUnit.UpdatedBy = 0;
                }
                else
                {
                    AddUnit.UpdatedBy = Convert.ToInt32(Session["UserId"]);
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
                    activity.User_id = Convert.ToInt32(Session["UserId"]);
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
            }
            catch (Exception e)
            {
                unit = null;
            }
            return Json(unit, JsonRequestBehavior.AllowGet);
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
            Units units = new Catalog().SelectUnit(Convert.ToInt32(id));

            Unit unit = new Unit();
            unit.id = units.id;
            unit.Unit_Name = units.Unit_Name;
            unit.Enable = units.Enable;
            unit.IsEnabled_ = (units.Enable == 1) ? "Active" : "InActive";
            unit.Delete_Status = units.Delete_Status;
            return Json(unit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            Unit units = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    units = new Unit();
                    Units unit = new Catalog().SelectUnit(id);
                    units.id = unit.id;
                    units.Unit_Name = unit.Unit_Name;
                    units.Enable = unit.Enable;
                    units.IsEnabled_ = (unit.Enable == 1) ? "Active" : "InActive";
                    units.AddedBy = unit.AddedBy;
                    units.Time = unit.Time;
                    units.Date = unit.Date;
                    units.Month = unit.Month;
                    units.Year = unit.Year;
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(units, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(units, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Unit uni = null;
            try
            {
                Units units = new Catalog().SelectUnit(id);
                uni = new Unit();
                if (units == null)
                {
                    response = "Please Select A Valid Unit";
                }
                else
                {
                    Units unit = new Units();
                    unit.id = id;

                    if (units.Enable == 1)
                    {
                        unit.Enable = 0;
                    }
                    else
                    {
                        unit.Enable = 1;
                    }

                    new Catalog().UpdatepUnit(unit);
                    uni.Enable = unit.Enable;
                    Activity activity = new Activity();
                    if (uni.Enable == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (uni.Enable == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Unit";
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

            return Json(uni, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelUnit(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelUnits(string DeleteUnitData)
        {
            string response = "";
            List<Units> units = null;
            List<Unit> unitsNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Unit> unitsrequestedtoDelete = js.Deserialize<List<Unit>>(DeleteUnitData);
                units = new List<Units>();
                activities = new List<Activity>();
                unitsNotDelete = new List<Unit>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in unitsrequestedtoDelete)
                {
                    Units li = new Units();
                    Units liChecked = new Units();
                    li.id = dbr.id;
                    li.Unit_Name = dbr.Unit_Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

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
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Unit/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Unit/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                    email.Send(mailMessage);

                    string type = "Units";
                    string Result = new Catalog().DelUnitRequest(units, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (units != null)
                        {
                            foreach (var i in units)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Unit";
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

            return Json(new { Response = response, Units = units, UnitsNotDelete = unitsNotDelete }, JsonRequestBehavior.AllowGet);
        }
    }
}