using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G_Accounting_System.Models;
using System.Web.Script.Serialization;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using G_Accounting_System.Code.Helpers;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class ActivityController : Controller
    {
        // GET: Activity
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Activities(int ActivityType_id, string ActivityType)
        {
            List<Activities> itemActivities = new Catalog().Activities(ActivityType_id, ActivityType);

            List<Activity> activity = new List<Activity>();

            if (itemActivities != null)
            {
                Session["Activities"] = null;
                foreach (var dbr in itemActivities)
                {
                    Activity li = new Activity();
                    li.id = dbr.id;
                    //li.Item_id = dbr.Item_id;
                    //li.ItemName = dbr.ItemName;
                    li.ActivityType_id = dbr.ActivityType_id;
                    li.ActivityType = dbr.ActivityType;
                    li.ActivityName = dbr.ActivityName;
                    li.Description = dbr.Description;
                    li.Date = dbr.Date;
                    li.Time = dbr.Time;
                    li.User_id = dbr.User_id;
                    li.UserName = dbr.UserName;
                    li.Icon = dbr.Icon;
                    activity.Add(li);
                    activity.TrimExcess();
                    Session["Activities"] = activity;
                }
            }
            else
            {
                Session["Activities"] = null;
            }
            return Json(activity, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertActivity(string ActivityData)
        {
            try
            {
                var js = new JavaScriptSerializer();
                List<Activity> activity = js.Deserialize<List<Activity>>(ActivityData);

                List<Activities> activities = new List<Activities>();
                foreach (var item in activity)
                {
                    Activities li = new Activities();
                    //li.Item_id = item.Item_id;
                    li.ActivityType_id = item.ActivityType_id;
                    li.ActivityType = item.ActivityType;
                    li.ActivityName = item.ActivityName;

                    li.Icon = item.Icon;
                    li.User_id = Convert.ToInt32(Session["UserId"]);

                    Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                    User user = new User();
                    var username = users.Name.Name;

                    if (item.ActivityName == "Created")
                    {
                        li.Description = "Created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Updated")
                    {
                        li.Description = "Updated by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Purchase Order")
                    {
                        li.Description = "Purchase Order created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Sales Order")
                    {
                        li.Description = "Sales Order created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Invoice")
                    {
                        li.Description = "Invoice created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Package")
                    {
                        li.Description = "Package created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Shipment")
                    {
                        li.Description = "Shipment created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Payment")
                    {
                        li.Description = "Payment created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Receiveing")
                    {
                        li.Description = "Receiveing created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Bill")
                    {
                        li.Description = "Bill created by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Marked as Active")
                    {
                        li.Description = "Marked as Active by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    else if (item.ActivityName == "Marked as Inactive")
                    {
                        li.Description = "Marked as Inactive by " + username + " on " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss tt");
                    }
                    li.Date = DateTime.Now.ToString("dd/MM/yyyy");
                    li.Time = DateTime.Now.ToString("HH:mm:ss tt");
                    activities.Add(li);
                }
                activities.TrimExcess();

                new Catalog().InsertActivity(activities);

                return Json(activities, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}