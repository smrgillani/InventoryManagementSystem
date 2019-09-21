using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G_Accounting_System.Models;
using System.Web.Script.Serialization;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using System.Data;
using System.IO;
using System.Net.Mail;
using G_Accounting_System.Code.Helpers;
using System.Text;
using System.Net;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class FactoryController : Controller
    {
        //
        // GET: /Factory/
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
                return View("Factory");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InserUpdatePremises(string PremisesData)
        {
            Premises premises = null;
            string ActivityName = null;
            try
            {
                var js = new JavaScriptSerializer();
                premises = js.Deserialize<Premises>(PremisesData);

                Premisess AddPremises = new Premisess();
                AddPremises.id = premises.id;
                AddPremises.Name = premises.Name;
                AddPremises.pc_mac_Address = premises.pc_mac_Address;
                AddPremises.Phone = premises.Phone;
                AddPremises.City = premises.City;
                AddPremises.Country = premises.Country;
                AddPremises.Address = premises.Address;
                AddPremises.Office = "0";
                AddPremises.Factory = "1";
                AddPremises.Store = "0";
                AddPremises.Shop = "0";
                AddPremises.Enable = 1;
                Countries countries = new Catalog().SelectCountry(premises.Country, null);
                Cities citites = new Catalog().SelectCity(premises.City, null);
                if (countries != null && citites != null)
                {
                    if (premises.id == 0)
                    {
                        AddPremises.AddedBy = Convert.ToInt32(Session["UserId"]);
                        AddPremises.UpdatedBy = 0;
                    }
                    else
                    {
                        AddPremises.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        AddPremises.AddedBy = 0;
                    }
                    new Catalog().AddPremises(AddPremises);

                    premises.pFlag = AddPremises.pFlag;
                    premises.pDesc = AddPremises.pDesc;
                    premises.pPremisesid_Out = AddPremises.pPremisesid_Out;
                    if (premises.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (premises.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(premises.pPremisesid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = premises.id;
                        }
                        activity.ActivityType = "Factory";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(Session["UserId"]);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                    return Json(premises, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    premises.pFlag = "0";
                    premises.pDesc = "Provided Data is Incorrect";
                    return Json(premises, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                premises.pFlag = "0";
                premises.pDesc = e.Message;
                return Json(premises, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllPremises(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Premisess> premises = new Catalog().AllFactories(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Premises> premisess = new List<Premises>();

            if (premises != null)
            {
                foreach (var dbr in premises)
                {
                    Premises li = new Premises();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.pc_mac_Address = dbr.pc_mac_Address;
                    li.Phone = dbr.Phone;
                    li.City = dbr.City;
                    li.CityName = dbr.CityName;
                    li.Country = dbr.Country;
                    li.CountryName = dbr.CountryName;
                    li.Address = dbr.Address;
                    li._Enable = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    premisess.Add(li);
                }
            }

            premisess.TrimExcess();
            var ppremisess = premisess.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = premisess.Count, recordsFiltered = premisess.Count, data = ppremisess }, JsonRequestBehavior.AllowGet);
        }

        [PermissionsAuthorize]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Profile(int id)
        {
            Premisess premisess = new Catalog().SelectPremises(Convert.ToInt32(id));
            Premises premises = new Premises();
            premises.id = premisess.id;
            premises.Name = premisess.Name;
            premises.pc_mac_Address = premisess.pc_mac_Address;
            premises.Phone = premisess.Phone;
            premises.Address = premisess.Address;
            premises.Country = premisess.Country;
            premises.City = premisess.City;
            premises.CountryName = premisess.CountryName;
            premises.CityName = premisess.CityName;
            premises.Enable = premisess.Enable;
            premises._Enable = (premisess.Enable == 1) ? "Active" : "InActive";
            premises.Delete_Status = premisess.Delete_Status;
            return Json(new { premises }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(int id)
        {
            Premises premises = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    Premisess premisess = new Catalog().SelectPremises(Convert.ToInt32(id));
                    premises = new Premises();
                    premises.id = premisess.id;
                    premises.Name = premisess.Name;
                    premises.pc_mac_Address = premisess.pc_mac_Address;
                    premises.Phone = premisess.Phone;
                    premises.Address = premisess.Address;
                    premises.Country = premisess.Country;
                    premises.City = premisess.City;
                    premises.CountryName = premisess.CountryName;
                    premises.CityName = premisess.CityName;
                    premises.Enable = premisess.Enable;
                    premises._Enable = (premisess.Enable == 1) ? "Active" : "InActive";
                    premises.Delete_Status = premisess.Delete_Status;
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(premises, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(premises, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string response = "";
            string ActivityName = null;
            Premises pre = null;
            try
            {
                Premisess premisess = new Catalog().SelectPremises(id);
                pre = new Premises();
                if (premisess == null)
                {
                    response = "Please Select A Valid Premises";
                }
                else
                {
                    Premisess premises = new Premisess();
                    premises.id = id;

                    if (premisess.Enable == 1)
                    {
                        premises.Enable = 0;
                    }
                    else
                    {
                        premises.Enable = 1;
                    }

                    new Catalog().UpdatepPremises(premises);
                    pre.Enable = premises.Enable;
                    Activity activity = new Activity();
                    if (pre.Enable == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (pre.Enable == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Factory";
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

            return Json(pre, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelOffice(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelPremises(string DeletePremisesData)
        {
            string response = "";
            List<Premisess> premises = null;
            string type = "";
            List<Premises> premisesNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Premises> premisessrequestedtoDelete = js.Deserialize<List<Premises>>(DeletePremisesData);
                premises = new List<Premisess>();
                activities = new List<Activity>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);
                premisesNotDelete = new List<Premises>();
                foreach (var dbr in premisessrequestedtoDelete)
                {
                    Premisess li = new Premisess();
                    Premisess liChecked = new Premisess();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;
                    liChecked = new Catalog().CheckPremisesForDelete(li.id);
                    if (liChecked != null)
                    {
                        Premises pr = new Premises();
                        pr.Name = liChecked.Name;
                        premisesNotDelete.Add(pr);
                    }
                    else
                    {
                        premises.Add(li);
                    }
                }
                premises.TrimExcess();
                premisesNotDelete.TrimExcess();
                if (premises.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(premises);

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
                    string filename = "Factories-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Factories/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Factories/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Factories";
                    mailMessage.Body = "Following Factories are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);


                    type = "Factories";
                    string Result = new Catalog().DelPremisesRequest(premises, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (premises != null)
                        {
                            foreach (var i in premises)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Factory";
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

            return Json(new { Response = response, Premises = premises, PremisesNotDelete = premisesNotDelete, type = type }, JsonRequestBehavior.AllowGet);
        }
    }
}
