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
    public class ManufacturerController : Controller
    {
        // GET: Manufacturer
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
                return View("Manufacturer");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllManufacturers(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Manufacturers> manufacturer = new Catalog().AllManufaturers(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Manufacturer> manufacturers = new List<Manufacturer>();

            if (manufacturer != null)
            {
                foreach (var dbr in manufacturer)
                {
                    Manufacturer li = new Manufacturer();
                    li.id = dbr.id;
                    li.Manufacturer_Name = dbr.Manufacturer_Name;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    manufacturers.Add(li);
                }
            }

            manufacturers.TrimExcess();
            var pmanufacturers = manufacturers.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = manufacturers.Count, recordsFiltered = manufacturers.Count, data = pmanufacturers }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateManufacturer(string ManufacturerData)
        {
            var js = new JavaScriptSerializer();
            string ActivityName = null;
            Manufacturer manufacturer = null;
            try
            {
                manufacturer = js.Deserialize<Manufacturer>(ManufacturerData);

                Manufacturers AddManufacturer = new Manufacturers();
                AddManufacturer.id = manufacturer.id;
                AddManufacturer.Manufacturer_Name = manufacturer.Manufacturer_Name;
                AddManufacturer.Enable = 1;
                if (manufacturer.id == 0)
                {
                    AddManufacturer.AddedBy = Convert.ToInt32(Session["UserId"]);
                    AddManufacturer.UpdatedBy = 0;
                }
                else
                {
                    AddManufacturer.UpdatedBy = Convert.ToInt32(Session["UserId"]);
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
                    activity.User_id = Convert.ToInt32(Session["UserId"]);
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
            }
            catch (Exception e)
            {
                manufacturer = null;
            }
            return Json(manufacturer, JsonRequestBehavior.AllowGet);
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
            Manufacturers manufacturers = new Catalog().SelectManufacture(Convert.ToInt32(id));

            Manufacturer manufacturer = new Manufacturer();
            manufacturer.id = manufacturers.id;
            manufacturer.Manufacturer_Name = manufacturers.Manufacturer_Name;
            manufacturer.Enable = manufacturers.Enable;
            manufacturer.IsEnabled_ = (manufacturer.Enable == 1) ? "Active" : "InActive";
            manufacturer.Delete_Status = manufacturer.Delete_Status;
            return Json(manufacturer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            Manufacturer manufaturers = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    manufaturers = new Manufacturer();
                    Manufacturers manufaturer = new Catalog().SelectManufacture(id);
                    manufaturers.id = manufaturer.id;
                    manufaturers.Manufacturer_Name = manufaturer.Manufacturer_Name;
                    manufaturers.Enable = manufaturer.Enable;
                    manufaturers.IsEnabled_ = (manufaturer.Enable == 1) ? "Active" : "InActive";
                    manufaturers.AddedBy = manufaturer.AddedBy;
                    manufaturers.Time = manufaturer.Time;
                    manufaturers.Date = manufaturer.Date;
                    manufaturers.Month = manufaturer.Month;
                    manufaturers.Year = manufaturer.Year;
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(manufaturers, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(manufaturers, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Manufacturer man = null;
            try
            {
                Manufacturers manufacturers = new Catalog().SelectManufacture(id);
                man = new Manufacturer();
                if (manufacturers == null)
                {
                    response = "Please Select A Valid Manufacturer";
                }
                else
                {
                    Manufacturers manufacturer = new Manufacturers();
                    manufacturer.id = id;

                    if (manufacturers.Enable == 1)
                    {
                        manufacturer.Enable = 0;
                    }
                    else
                    {
                        manufacturer.Enable = 1;
                    }

                    new Catalog().UpdatepManufacturer(manufacturer);
                    man.Enable = manufacturer.Enable;
                    Activity activity = new Activity();
                    if (man.Enable == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (man.Enable == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Manufacturer";
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

            return Json(man, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelManufacture(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelManufacturers(string DeleteManufacturerData)
        {
            string response = "";
            List<Manufacturers> manufacturers = null;
            List<Manufacturer> manufacturersNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Manufacturer> manufacturerrequestedtoDelete = js.Deserialize<List<Manufacturer>>(DeleteManufacturerData);
                manufacturers = new List<Manufacturers>();
                activities = new List<Activity>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);
                manufacturersNotDelete = new List<Manufacturer>();

                foreach (var dbr in manufacturerrequestedtoDelete)
                {
                    Manufacturers li = new Manufacturers();
                    Manufacturers liChecked = new Manufacturers();
                    li.id = dbr.id;
                    li.Manufacturer_Name = dbr.Manufacturer_Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;
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
                    string filename = "Manufacturer-" + DateTime.Now.ToString("dd-MM-yyyy hh mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Manufacturer/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Manufacturer/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                    email.Send(mailMessage);

                    string type = "Manufacturers";
                    string Result = new Catalog().DelManufacturerRequest(manufacturers, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (manufacturers != null)
                        {
                            foreach (var i in manufacturers)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Manufacturer";
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

            return Json(new { Response = response, Manufacturers = manufacturers, ManufacturersNotDelete = manufacturersNotDelete }, JsonRequestBehavior.AllowGet);
        }
    }
}