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
using System.Web.Security;
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class MessagingController : Controller
    {
        // GET: Messaging
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            ViewBag.userID = Session["UserId"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Messages(int Receiver_id)
        {
            int Sender_id = Convert.ToInt32(Session["UserId"]);

            List<Messages> messages = new Catalog().Messages(Sender_id, Receiver_id);
            List<Message> message = new List<Message>();
            Contacts ReceiverName = new Catalog().SelectUser(Receiver_id).Name;
            if (messages != null)
            {
                foreach (var dbr in messages)
                {
                    Message li = new Message();
                    li.id = dbr.id;
                    li.Sender_id = dbr.Sender_id;
                    li.SenderName = dbr.SenderName.Name;
                    li.Receiver_id = dbr.Receiver_id;
                    li.ReceiverName = dbr.ReceiverName.Name;
                    li.strMessage = dbr.strMessage;
                    li.Date = dbr.Date;
                    li.Time = dbr.Time;
                    li.Month = dbr.Month;
                    li.Year = dbr.Year;

                    message.Add(li);
                    message.TrimExcess();
                    //Session["Messages"] = message;
                }
            }

            
            else
            {
                //Session["Messages"] = null;
            }
            return Json(new { Messages = message, ReceiverName = ReceiverName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult getAllUsers()
        {
            List<Users> users = new Catalog().AllUsers(null, null, null, null, null);
            List<User> user = new List<User>();
            string CurrentUser = null;
            string CurrentUserEmail = null;
            if (users != null)
            {
                foreach (var dbr in users)
                {
                    if (dbr.id != Convert.ToInt32(Session["UserId"]))
                    {
                        User li = new User();
                        li.id = dbr.id;
                        li.email = dbr.email;
                        li.attached_profile = (dbr.Name != null) ? dbr.Name.Name : "";
                        user.Add(li);
                        user.TrimExcess();
                        //Session["UserList"] = user;
                    }
                    else
                    {
                        CurrentUser = (dbr.Name != null) ? dbr.Name.Name : "";
                        CurrentUserEmail = (dbr.email != null) ? dbr.Name.Email : "";
                    }
                }
            }
            else
            {
                Session["UserList"] = null;
            }
            return Json(new { Users = user, CurrentUser = CurrentUser, CurrentUserEmail = CurrentUserEmail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendMessage(string Message)
        {
            string response = "";
            Message message = null;
            try
            {
                var js = new JavaScriptSerializer();
                message = js.Deserialize<Message>(Message);

                Messages AddMessage = new Messages();
                AddMessage.Sender_id = Convert.ToInt32(Session["UserId"]);
                AddMessage.Receiver_id = message.Receiver_id;
                AddMessage.strMessage = message.strMessage;
                AddMessage.Enable = 1;
                AddMessage.Status = message.Status;

                new Catalog().SendMessage(AddMessage);
                message.pFlag = AddMessage.pFlag;
                message.pDesc = AddMessage.pDesc;
            }
            catch (Exception e)
            {

            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(string UpdateStatus)
        {
            string response = "";
            Message message = null;
            try
            {
                var js = new JavaScriptSerializer();
                message = js.Deserialize<Message>(UpdateStatus);

                Messages AddMessage = new Messages();
                AddMessage.Receiver_id = message.Receiver_id;
                AddMessage.Status = message.Status;

                new Catalog().UpdateStatus(AddMessage);
                message.pFlag = AddMessage.pFlag;
                message.pDesc = AddMessage.pDesc;
            }
            catch (Exception e)
            {

            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteChat(string ChatData)
        {
            string response = "";
            Message message = null;
            try
            {
                var js = new JavaScriptSerializer();
                message = js.Deserialize<Message>(ChatData);

                Messages messages = new Messages();
                messages.Receiver_id = message.Receiver_id;
                messages.Sender_id = Convert.ToInt32(Session["UserId"]);
                messages.Enable = 0;

                new Catalog().DeleteChat(messages);
                message.pFlag = messages.pFlag;
                message.pDesc = messages.pDesc;
            }
            catch (Exception e)
            {
                return null;
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}