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
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class CalenderController : Controller
    {
        // GET: Calender
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddEvents(string EventData)
        {
            var js = new JavaScriptSerializer();
            Calender calender = js.Deserialize<Calender>(EventData);

            Calenders AddEvent = new Calenders();
            AddEvent.title = calender.title;
            AddEvent.backgroundColor = calender.backgroundColor;
            AddEvent.borderColor = calender.borderColor;
            AddEvent.AddedBy = Convert.ToInt32(Session["UserId"]);

            new Catalog().AddEvents(AddEvent);
            calender.pFlag = AddEvent.pFlag;
            calender.pDesc = AddEvent.pDesc;
            calender.pEventid_Out = AddEvent.pEventid_Out;

            return Json(calender, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddCalenderEvents(string EventData)
        {
            var js = new JavaScriptSerializer();
            Calender calender = js.Deserialize<Calender>(EventData);

            Calenders AddEvent = new Calenders();
            AddEvent.id = calender.id;
            AddEvent.start = calender.start;
            AddEvent.AddedBy = Convert.ToInt32(Session["UserId"]);

            new Catalog().AddCalenderEvents(AddEvent);
            calender.pFlag = AddEvent.pFlag;
            calender.pDesc = AddEvent.pDesc;
            calender.pEventid_Out = AddEvent.pEventid_Out;

            return Json(calender, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetEventsName()
        {
            int User_id = Convert.ToInt32(Session["UserId"]);
            List<Calenders> eventsname = new Catalog().GetEventsName(Convert.ToInt32(User_id));
            List<Calender> eventname = new List<Calender>();

            if (eventsname != null)
            {
                foreach (var dbr in eventsname)
                {
                    Calender li = new Calender();
                    li.id = dbr.id;
                    li.title = dbr.title;
                    li.backgroundColor = dbr.backgroundColor;
                    li.backgroundColor = dbr.borderColor;
                    li.AddedBy = dbr.AddedBy;
                    li.Time_Of_Day = dbr.Time_Of_Day;
                    li.Date_Of_Day = dbr.Date_Of_Day;
                    li.Month_Of_Day = dbr.Month_Of_Day;
                    li.Year_Of_Day = dbr.Year_Of_Day;
                    eventname.Add(li);
                }
            }

            return Json(eventname, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetCalenderEvents()
        {
            int User_id = Convert.ToInt32(Session["UserId"]);
            List<Calenders> eventsname = new Catalog().GetCalenderEvents(Convert.ToInt32(User_id));
            List<Calender> eventname = new List<Calender>();

            if (eventsname != null)
            {
                foreach (var dbr in eventsname)
                {
                    Calender li = new Calender();
                    li.id = dbr.id;
                    li.title = dbr.title;
                    li.backgroundColor = dbr.backgroundColor;
                    li.backgroundColor = dbr.backgroundColor;
                    li.start = dbr.start;
                    li.AddedBy = dbr.AddedBy;
                    li.Time_Of_Day = dbr.Time_Of_Day;
                    li.Date_Of_Day = dbr.Date_Of_Day;
                    li.Month_Of_Day = dbr.Month_Of_Day;
                    li.Year_Of_Day = dbr.Year_Of_Day;
                    eventname.Add(li);
                }
            }

            return Json(eventname, JsonRequestBehavior.DenyGet);
        }

    }
}