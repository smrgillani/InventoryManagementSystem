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
using System.Drawing;
using System.Web.Helpers;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using MailKit.Search;
using System.Web.Services.Description;
using MimeKit;
using MimePart = MimeKit.MimePart;
using System.Threading;
using Microsoft.ServiceModel.WebSockets;
using Microsoft.Web.WebSockets;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class MailboxController : Controller
    {
        // GET: Mailbox
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MailboxMain()
        {
            return View("MailsBoxMain");
        }

        public ActionResult Compose()
        {
            return View("Compose");
        }

        public ActionResult Reply()
        {
            return View("Compose");
        }

        public ActionResult ReadMail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult SendMail(string EmailData, string AttachmentData)
        {
            string response = "";
            Email email = null;
            try
            {
                var rawComment = Request.Unvalidated().Form["body"];
                var js = new JavaScriptSerializer();
                email = js.Deserialize<Email>(EmailData);
                List<MailAttachment> attachment = js.Deserialize<List<MailAttachment>>(AttachmentData);
                List<MailAttachments> attachments = new List<MailAttachments>();
                MailAttachments AddAttachment = null;


                Emails AddEmail = new Emails();
                //AddEmail.id = email.id;
                AddEmail.EmailTo = email.EmailTo;
                AddEmail.Subject = email.Subject;
                AddEmail.Body = email.Body;
                AddEmail.Status = email.Status;
                AddEmail.User_id = Convert.ToInt32(Session["UserId"]);

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                AddEmail.EmailFrom = user.email;


                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(AddEmail.EmailFrom);
                mailMessage.To.Add(AddEmail.EmailTo);
                mailMessage.Subject = AddEmail.Subject;
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = AddEmail.Body;


                if (attachment != null)
                {
                    foreach (var file in attachment)
                    {
                        MailAttachment li = new MailAttachment();
                        byte[] bytes = Convert.FromBase64String(file.base64);
                        MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
                        ms.Write(bytes, 0, bytes.Length);

                        Byte[] bytess = Convert.FromBase64String(file.base64);

                        using (var fs = new FileStream(Server.MapPath("~/Attachments/" + file.FileName), FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(bytess, 0, bytess.Length);
                            AddAttachment = new MailAttachments();
                            AddAttachment.FileName = "/Attachments/" + file.FileName;
                            attachments.Add(AddAttachment);
                            //mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/Attachments/" + file.FileName)));
                        }

                        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Attachments/"));
                        foreach (FileInfo files in dir.GetFiles(file.FileName))
                        {
                            if (files.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(files.FullName));
                            }
                        }
                    }
                }

                //mailMessage.Attachments.Add(new Attachment(file.FullName));

                smtpClient.Send(mailMessage);


                new Catalog().InsertEmail(AddEmail, attachments);
                email.pFlag = AddEmail.pFlag;
                email.pDesc = AddEmail.pDesc;
            }
            catch (SmtpFailedRecipientException e)
            {
                response = e.Message;
            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllFolders()
        {
            List<Email> email = new List<Email>();

            using (var client = new ImapClient())
            {
                client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");

                var personal = client.GetFolder(client.PersonalNamespaces[0]);

                //var uids = client.Inbox.Search(SearchQuery.SentOn(DateTime.Today));

                foreach (var folder in personal.GetSubfolders(false))
                {
                    Email li = new Email();
                    var foldername = client.GetFolder(folder.FullName);
                }

                client.Disconnect(true);
            }

            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmailsCount()
        {
            List<UniqueId> Inbox = new List<UniqueId>();
            List<UniqueId> Sent = new List<UniqueId>();
            List<UniqueId> Drafts = new List<UniqueId>();
            List<UniqueId> Junk = new List<UniqueId>();
            List<UniqueId> Trash = new List<UniqueId>();

            int inboxCount = 0;
            int sentCount = 0;
            int draftsCount = 0;
            int junkCount = 0;
            int trashCount = 0;

            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                ////INBOX
                client.Inbox.Open(FolderAccess.ReadOnly);

                foreach (var i in client.Inbox.Search(SearchQuery.NotSeen))
                {
                    Inbox.Add(i);
                }
                inboxCount = Inbox.Count;

                ////SENT
                client.GetFolder("[Gmail]/Sent Mail").Open(FolderAccess.ReadOnly);
                foreach (var i in client.GetFolder("[Gmail]/Sent Mail").Search(SearchQuery.NotSeen))
                {
                    Sent.Add(i);
                }
                sentCount = Sent.Count;

                //////DRAFTS
                //client.GetFolder("[Gmail]/Draft Mail").Open(FolderAccess.ReadOnly);
                //foreach (var i in client.GetFolder("[Gmail]/Draft Mail").Search(SearchQuery.Draft))
                //{
                //    Drafts.Add(i);
                //}
                //draftsCount = Drafts.Count;

                //////JUNK
                //client.GetFolder("[Gmail]/Spam Mail").Open(FolderAccess.ReadOnly);
                //foreach (var i in client.GetFolder("[Gmail]/Spam Mail").Search(SearchQuery.NotSeen))
                //{
                //    Junk.Add(i);
                //}
                //junkCount = Junk.Count;

                ////TRASH
                //client.GetFolder("[Gmail]/Trash Mail").Open(FolderAccess.ReadOnly);
                //foreach (var i in client.GetFolder("[Gmail]/Trash Mail").Search(SearchQuery.NotSeen))
                //{
                //    Trash.Add(i);
                //}
                //trashCount = Trash.Count;

                client.Disconnect(true);
            }

            return Json(new { InboxCount = inboxCount, SentCount = sentCount, DraftsCount = draftsCount, JunkCount = junkCount, TrashCount = trashCount }, JsonRequestBehavior.AllowGet);
        }

        #region GetMails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Inbox()
        {
            List<Email> email = new List<Email>();
            List<MailAttachment> attactments = new List<MailAttachment>();
            MailAttachment att = new MailAttachment();

            Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
            User user = new User();
            try
            {
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");

                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");

                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                    }

                    client.Inbox.Open(FolderAccess.ReadOnly);

                    IList<UniqueId> uids = client.Inbox.Search(SearchQuery.SentSince(DateTime.Today));

                    uids.Reverse();
                    foreach (var uid in uids)
                    {
                        Email li = new Email();
                        var message = client.Inbox.GetMessage(uid);
                        li.mail_id = uid;
                        li.Id = uid.Id;
                        li.IsValid = uid.IsValid;
                        li.Validity = uid.Validity;
                        li.EmailFrom = message.From.ToString();
                        li.EmailTo = message.To.ToString();
                        li.Subject = message.Subject;
                        li.Date = message.Date.ToString();
                        li.Body = message.HtmlBody;

                        email.Add(li);
                        // write the message to a file
                        //System.Diagnostics.Debug.WriteLine(message);
                        //message.WriteTo(string.Format("{0}.eml", uid));
                    }
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Sent()
        {
            List<Email> email = new List<Email>();
            List<MailAttachment> attactments = new List<MailAttachment>();
            try
            {
                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();

                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Sent")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Sent).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Sent")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var uids = client.GetFolder(FolderName).Search(SearchQuery.SentOn(DateTime.Today));

                    foreach (var uid in uids)
                    {
                        Email li = new Email();
                        var message = client.GetFolder(FolderName).GetMessage(uid);

                        li.mail_id = uid;
                        li.Id = uid.Id;
                        li.IsValid = uid.IsValid;
                        li.Validity = uid.Validity;
                        li.EmailFrom = message.From.ToString();
                        li.EmailTo = message.To.ToString();
                        li.Subject = message.Subject;
                        li.Date = message.Date.ToString();
                        li.Body = message.HtmlBody;

                        email.Add(li);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Drafts()
        {
            List<Email> email = new List<Email>();
            List<MailAttachment> attactments = new List<MailAttachment>();
            try
            {
                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Draft")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Drafts).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Drafts")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var uids = client.GetFolder(FolderName).Search(SearchQuery.All);

                    foreach (var uid in uids)
                    {
                        Email li = new Email();
                        var message = client.GetFolder(FolderName).GetMessage(uid);

                        li.mail_id = uid;
                        li.Id = uid.Id;
                        li.IsValid = uid.IsValid;
                        li.Validity = uid.Validity;
                        li.EmailFrom = message.From.ToString();
                        li.EmailTo = message.To.ToString();
                        li.Subject = message.Subject;
                        li.Date = message.Date.ToString();
                        li.Body = message.HtmlBody;

                        email.Add(li);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Junk()
        {
            List<Email> email = new List<Email>();
            List<MailAttachment> attactments = new List<MailAttachment>();
            try
            {
                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Spam")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Junk).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Junk")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var uids = client.GetFolder(FolderName).Search(SearchQuery.All);

                    foreach (var uid in uids)
                    {
                        Email li = new Email();
                        var message = client.GetFolder(FolderName).GetMessage(uid);
                        li.mail_id = uid;
                        li.Id = uid.Id;
                        li.IsValid = uid.IsValid;
                        li.Validity = uid.Validity;
                        li.EmailFrom = message.From.ToString();
                        li.EmailTo = message.To.ToString();
                        li.Subject = message.Subject;
                        li.Date = message.Date.ToString();
                        li.Body = message.HtmlBody;

                        email.Add(li);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Trash()
        {
            List<Email> email = new List<Email>();
            List<MailAttachment> attactments = new List<MailAttachment>();
            try
            {
                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Trash")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Trash).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Deleted")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var uids = client.GetFolder(FolderName).Search(SearchQuery.All);

                    foreach (var uid in uids)
                    {
                        Email li = new Email();
                        var message = client.GetFolder(FolderName).GetMessage(uid);

                        li.mail_id = uid;
                        li.Id = uid.Id;
                        li.IsValid = uid.IsValid;
                        li.Validity = uid.Validity;
                        li.EmailFrom = message.From.ToString();
                        li.EmailTo = message.To.ToString();
                        li.Subject = message.Subject;
                        li.Date = message.Date.ToString();
                        li.Body = message.HtmlBody;

                        email.Add(li);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Important()
        //{
        //    List<Email> email = new List<Email>();
        //    List<MailAttachment> attactments = new List<MailAttachment>();

        //    using (var client = new ImapClient())
        //    {
        //        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

        //        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");

        //        client.GetFolder(SpecialFolder.Flagged).Open(FolderAccess.ReadOnly);

        //        var uids = client.GetFolder(SpecialFolder.Flagged).Search(SearchQuery.All);

        //        foreach (var uid in uids)
        //        {
        //            Email li = new Email();
        //            var message = client.GetFolder(SpecialFolder.Flagged).GetMessage(uid);

        //            li.mail_id = uid;
        //            li.Id = uid.Id;
        //            li.IsValid = uid.IsValid;
        //            li.Validity = uid.Validity;
        //            li.EmailFrom = message.From.ToString();
        //            li.EmailTo = message.To.ToString();
        //            li.Subject = message.Subject;
        //            li.Date = message.Date.ToString();
        //            li.Body = message.HtmlBody;

        //            email.Add(li);
        //        }

        //        client.Disconnect(true);
        //    }

        //    return Json(email, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region GetSingleMail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult getSingleInboxEmail(string mail_id)
        {
            Email email = new Email();
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));


                List<MailAttachment> attactments = new List<MailAttachment>();

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                    }

                    client.Inbox.Open(FolderAccess.ReadOnly);
                    //IEnumerable<UniqueId> uids = client.Inbox.Search(SearchQuery.FromContains("salmanahmed635@gmail.com"));

                    client.Inbox.Open(FolderAccess.ReadOnly);

                    var message = client.Inbox.GetMessage(_uid);

                    email.EmailFrom = message.From.ToString();
                    email.EmailTo = message.To.ToString();
                    email.Subject = message.Subject;
                    email.Date = message.Date.ToString();
                    email.Body = message.HtmlBody;

                    var attachments = message.BodyParts.OfType<MimePart>().Where(part => !string.IsNullOrEmpty(part.FileName));

                    foreach (MimePart atch in attachments)
                    {
                        using (var memory = new MemoryStream())
                        {
                            atch.Content.DecodeTo(memory);
                            var buffer = memory.ToArray();
                            var text = Encoding.UTF8.GetString(buffer);
                            using (var fs = new FileStream(Server.MapPath("~/ImapAttachments/" + atch.FileName), FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(buffer, 0, buffer.Length);
                                MailAttachment att = new MailAttachment();
                                att.FileName = atch.FileName;
                                att.base64 = Convert.ToBase64String(buffer);

                                attactments.Add(att);
                                email.MailAttachments = attactments;
                            }
                        }
                    }
                    //email.Add(li);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult getSingleSentEmail(string mail_id)
        {
            Email email = new Email();
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));


                List<MailAttachment> attactments = new List<MailAttachment>();

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Sent")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Sent).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Sent")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var message = client.GetFolder(FolderName).GetMessage(_uid);

                    email.EmailFrom = message.From.ToString();
                    email.EmailTo = message.To.ToString();
                    email.Subject = message.Subject;
                    email.Date = message.Date.ToString();
                    email.Body = message.HtmlBody;

                    var attachments = message.BodyParts.OfType<MimePart>().Where(part => !string.IsNullOrEmpty(part.FileName));

                    foreach (MimePart atch in attachments)
                    {
                        using (var memory = new MemoryStream())
                        {
                            atch.Content.DecodeTo(memory);
                            var buffer = memory.ToArray();
                            var text = Encoding.UTF8.GetString(buffer);
                            using (var fs = new FileStream(Server.MapPath("~/ImapAttachments/" + atch.FileName), FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(buffer, 0, buffer.Length);
                                MailAttachment att = new MailAttachment();
                                att.FileName = atch.FileName;
                                att.base64 = Convert.ToBase64String(buffer);

                                attactments.Add(att);
                                email.MailAttachments = attactments;
                            }
                        }
                    }
                    //email.Add(li);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult getSingleDraftEmail(string mail_id)
        {
            Email email = new Email();
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));


                List<MailAttachment> attactments = new List<MailAttachment>();

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Draft")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Drafts).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Drafts")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var message = client.GetFolder(FolderName).GetMessage(_uid);

                    email.EmailFrom = message.From.ToString();
                    email.EmailTo = message.To.ToString();
                    email.Subject = message.Subject;
                    email.Date = message.Date.ToString();
                    email.Body = message.HtmlBody;

                    var attachments = message.BodyParts.OfType<MimePart>().Where(part => !string.IsNullOrEmpty(part.FileName));

                    foreach (MimePart atch in attachments)
                    {
                        using (var memory = new MemoryStream())
                        {
                            atch.Content.DecodeTo(memory);
                            var buffer = memory.ToArray();
                            var text = Encoding.UTF8.GetString(buffer);
                            using (var fs = new FileStream(Server.MapPath("~/ImapAttachments/" + atch.FileName), FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(buffer, 0, buffer.Length);
                                MailAttachment att = new MailAttachment();
                                att.FileName = atch.FileName;
                                att.base64 = Convert.ToBase64String(buffer);

                                attactments.Add(att);
                                email.MailAttachments = attactments;
                            }
                        }
                    }
                    //email.Add(li);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult getSingleJunkEmail(string mail_id)
        {
            Email email = new Email();
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));


                List<MailAttachment> attactments = new List<MailAttachment>();

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Junk" || foldername == "Spam")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Junk).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Junk")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var message = client.GetFolder(FolderName).GetMessage(_uid);

                    email.EmailFrom = message.From.ToString();
                    email.EmailTo = message.To.ToString();
                    email.Subject = message.Subject;
                    email.Date = message.Date.ToString();
                    email.Body = message.HtmlBody;

                    var attachments = message.BodyParts.OfType<MimePart>().Where(part => !string.IsNullOrEmpty(part.FileName));

                    foreach (MimePart atch in attachments)
                    {
                        using (var memory = new MemoryStream())
                        {
                            atch.Content.DecodeTo(memory);
                            var buffer = memory.ToArray();
                            var text = Encoding.UTF8.GetString(buffer);
                            using (var fs = new FileStream(Server.MapPath("~/ImapAttachments/" + atch.FileName), FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(buffer, 0, buffer.Length);
                                MailAttachment att = new MailAttachment();
                                att.FileName = atch.FileName;
                                att.base64 = Convert.ToBase64String(buffer);

                                attactments.Add(att);
                                email.MailAttachments = attactments;
                            }
                        }
                    }
                    //email.Add(li);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult getSingleTrashEmail(string mail_id)
        {
            Email email = new Email();
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));

                List<MailAttachment> attactments = new List<MailAttachment>();

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Trash")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Trash).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Deleted")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadOnly);

                    var message = client.GetFolder(FolderName).GetMessage(_uid);

                    email.EmailFrom = message.From.ToString();
                    email.EmailTo = message.To.ToString();
                    email.Subject = message.Subject;
                    email.Date = message.Date.ToString();
                    email.Body = message.HtmlBody;

                    var attachments = message.BodyParts.OfType<MimePart>().Where(part => !string.IsNullOrEmpty(part.FileName));

                    foreach (MimePart atch in attachments)
                    {
                        using (var memory = new MemoryStream())
                        {
                            atch.Content.DecodeTo(memory);
                            var buffer = memory.ToArray();
                            var text = Encoding.UTF8.GetString(buffer);
                            using (var fs = new FileStream(Server.MapPath("~/ImapAttachments/" + atch.FileName), FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(buffer, 0, buffer.Length);
                                MailAttachment att = new MailAttachment();
                                att.FileName = atch.FileName;
                                att.base64 = Convert.ToBase64String(buffer);

                                attactments.Add(att);
                                email.MailAttachments = attactments;
                            }
                        }
                    }
                    //email.Add(li);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(email, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DeleteMail

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteInboxMail(string mail_id)
        {
            string Response = "";
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));
                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                var MovetoFolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Deleted Items")
                            {
                                MovetoFolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        MovetoFolderName = client.GetFolder(SpecialFolder.Trash).ToString();
                    }

                    client.Inbox.Open(FolderAccess.ReadWrite);


                    client.Inbox.MoveTo(_uid, client.GetFolder(MovetoFolderName));

                    Response = "Mail Deleted Successfully";

                    client.Disconnect(true);
                }
            }
            catch
            {
                Response = "Intrernal Server Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteSentMail(string mail_id)
        {
            string Response = "";
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                var MovetoFolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Sent")
                            {
                                FolderName = foldername;
                                MovetoFolderName = "Deleted Items";
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Sent).ToString();
                        MovetoFolderName = client.GetFolder(SpecialFolder.Trash).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Sent")
                            {
                                FolderName = foldername;
                                MovetoFolderName = "Deleted";
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadWrite);


                    client.GetFolder(FolderName).MoveTo(_uid, client.GetFolder(MovetoFolderName));

                    Response = "Mail Deleted Successfully";

                    client.Disconnect(true);
                }
            }
            catch
            {
                Response = "Intrernal Server Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteDraftsMail(string mail_id)
        {
            string Response = "";
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                var MovetoFolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Draft")
                            {
                                FolderName = foldername;
                                MovetoFolderName = "Deleted Items";
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Drafts).ToString();
                        MovetoFolderName = client.GetFolder(SpecialFolder.Trash).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Drafts")
                            {
                                FolderName = foldername;
                                MovetoFolderName = "Deleted";
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadWrite);


                    client.GetFolder(FolderName).MoveTo(_uid, client.GetFolder(MovetoFolderName));

                    Response = "Mail Deleted Successfully";

                    client.Disconnect(true);
                }
            }
            catch
            {
                Response = "Intrernal Server Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteJunkMail(string mail_id)
        {
            string Response = "";
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Junk" || foldername == "Spam")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Junk).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Junk")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadWrite);


                    client.GetFolder(FolderName).AddFlags(_uid, MessageFlags.Deleted, silent: true);

                    Response = "Mail Deleted Successfully";

                    client.Disconnect(true);
                }
            }
            catch
            {
                Response = "Intrernal Server Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteTrashMail(string mail_id)
        {
            string Response = "";
            try
            {
                var js = new JavaScriptSerializer();
                dynamic uid = js.Deserialize<dynamic>(mail_id);

                UniqueId _uid = new UniqueId(Convert.ToUInt32(uid["Validity"]), Convert.ToUInt32(uid["Id"]));

                Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
                User user = new User();
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }

                var FolderName = "";
                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Trash")
                            {
                                FolderName = foldername;
                            }
                        }
                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");
                        FolderName = client.GetFolder(SpecialFolder.Trash).ToString();
                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                        var personal = client.GetFolder(client.PersonalNamespaces[0]);

                        foreach (var folder in personal.GetSubfolders(false))
                        {
                            string foldername = client.GetFolder(folder.FullName).ToString();
                            if (foldername == "Deleted")
                            {
                                FolderName = foldername;
                            }
                        }
                    }

                    client.GetFolder(FolderName).Open(FolderAccess.ReadWrite);


                    client.GetFolder(FolderName).AddFlags(_uid, MessageFlags.Deleted, silent: true);

                    Response = "Mail Deleted Successfully";

                    client.Disconnect(true);
                }
            }
            catch
            {
                Response = "Intrernal Server Error";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Notification()
        {
            int response = 0;
            List<Email> email = new List<Email>();
            List<MailAttachment> attactments = new List<MailAttachment>();
            MailAttachment att = new MailAttachment();

            Users users = new Catalog().SelectUser(Convert.ToInt32(Session["UserId"]));
            User user = new User();
            try
            {
                if (users != null)
                {
                    user.email = users.email;
                    user.Name = users.Name.Name;
                    user.password = users.password;
                }


                using (var client = new ImapClient())
                {
                    if (user.email.Contains("@yahoo") || user.email.Contains("@ymail"))
                    {
                        client.Connect("imap.mail.yahoo.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@yahoo.com", "unforgetable");

                    }
                    else if (user.email.Contains("@gmail"))
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@gmail.com", "unforgetable");

                    }
                    else if (user.email.Contains("@hotmail") || user.email.Contains("@live") || user.email.Contains("@outlook"))
                    {
                        client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect);
                        client.Authenticate("salmanahmed635@outlook.com", "unforgetable");
                    }
                    client.Inbox.Open(FolderAccess.ReadOnly);
                    var messages = client.Inbox.Fetch(0, -1, MessageSummaryItems.Full | MessageSummaryItems.UniqueId).ToList();
                    client.Inbox.MessageExpunged += (sender, e) =>
                    {
                        var folder = (ImapFolder)sender;

                        if (e.Index < messages.Count)
                        {
                            var message = messages[e.Index];

                            Console.WriteLine("{0}: expunged message {1}: Subject: {2}", folder, e.Index, message.Envelope.Subject);

                            // Note: If you are keeping a local cache of message information
                            // (e.g. MessageSummary data) for the folder, then you'll need
                            // to remove the message at e.Index.
                            messages.RemoveAt(e.Index);
                        }
                        else
                        {
                            Console.WriteLine("{0}: expunged message {1}: Unknown message.", folder, e.Index);
                        }
                    };

                    // Keep track of changes to the number of messages in the folder (this is how we'll tell if new messages have arrived).
                    client.Inbox.CountChanged += (sender, e) =>
                    {
                        // Note: the CountChanged event will fire when new messages arrive in the folder and/or when messages are expunged.
                        var folder = (ImapFolder)sender;

                        Console.WriteLine("The number of messages in {0} has changed.", folder);

                        // Note: because we are keeping track of the MessageExpunged event and updating our
                        // 'messages' list, we know that if we get a CountChanged event and folder.Count is
                        // larger than messages.Count, then it means that new messages have arrived.
                        if (folder.Count > messages.Count)
                        {
                            Console.WriteLine("{0} new messages have arrived.", folder.Count - messages.Count);
                            WebSocketCollection c = new WebSocketCollection();
                            c.Broadcast(folder.Count - messages.Count + "new emails");
                            // Note: your first instict may be to fetch these new messages now, but you cannot do
                            // that in this event handler (the ImapFolder is not re-entrant).
                            // 
                            // If this code had access to the 'done' CancellationTokenSource (see below), it could
                            // cancel that to cause the IDLE loop to end.
                        }
                    };

                    // Keep track of flag changes.
                    client.Inbox.MessageFlagsChanged += (sender, e) =>
                    {
                        var folder = (ImapFolder)sender;


                        Console.WriteLine("{0}: flags for message {1} have changed to: {2}.", folder, e.Index, e.Flags);
                    };

                    Console.WriteLine("Hit any key to end the IDLE loop.");
                    using (var done = new CancellationTokenSource())
                    {
                        // Note: when the 'done' CancellationTokenSource is cancelled, it ends to IDLE loop.
                        var thread = new Thread(IdleLoop);

                        thread.Start(new IdleState(client, done.Token));

                        Console.ReadKey();
                        done.Cancel();
                        thread.Join();
                    }

                    if (client.Inbox.Count > messages.Count)
                    {
                        Console.WriteLine("The new messages that arrived during IDLE are:");
                        foreach (var message in client.Inbox.Fetch(messages.Count, -1, MessageSummaryItems.Full | MessageSummaryItems.UniqueId))
                            Console.WriteLine("Subject: {0}", message.Envelope.Subject);
                    }

                    client.Disconnect(true);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {

            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        static void IdleLoop(object state)
        {
            var idle = (IdleState)state;

            lock (idle.Client.SyncRoot)
            {
                // Note: since the IMAP server will drop the connection after 30 minutes, we must loop sending IDLE commands that
                // last ~29 minutes or until the user has requested that they do not want to IDLE anymore.
                // 
                // For GMail, we use a 9 minute interval because they do not seem to keep the connection alive for more than ~10 minutes.
                while (!idle.IsCancellationRequested)
                {
                    using (var timeout = new CancellationTokenSource(new TimeSpan(0, 9, 0)))
                    {
                        try
                        {
                            // We set the timeout source so that if the idle.DoneToken is cancelled, it can cancel the timeout
                            idle.SetTimeoutSource(timeout);

                            if (idle.Client.Capabilities.HasFlag(ImapCapabilities.Idle))
                            {
                                // The Idle() method will not return until the timeout has elapsed or idle.CancellationToken is cancelled
                                idle.Client.Idle(timeout.Token, idle.CancellationToken);
                            }
                            else
                            {
                                // The IMAP server does not support IDLE, so send a NOOP command instead
                                idle.Client.NoOp(idle.CancellationToken);

                                // Wait for the timeout to elapse or the cancellation token to be cancelled
                                WaitHandle.WaitAny(new[] { timeout.Token.WaitHandle, idle.CancellationToken.WaitHandle });
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            // This means that idle.CancellationToken was cancelled, not the DoneToken nor the timeout.
                            break;
                        }
                        catch (ImapProtocolException)
                        {
                            // The IMAP server sent garbage in a response and the ImapClient was unable to deal with it.
                            // This should never happen in practice, but it's probably still a good idea to handle it.
                            // 
                            // Note: an ImapProtocolException almost always results in the ImapClient getting disconnected.
                            break;
                        }
                        catch (ImapCommandException)
                        {
                            // The IMAP server responded with "NO" or "BAD" to either the IDLE command or the NOOP command.
                            // This should never happen... but again, we're catching it for the sake of completeness.
                            break;
                        }
                        finally
                        {
                            // We're about to Dispose() the timeout source, so set it to null.
                            idle.SetTimeoutSource(null);
                        }
                    }
                }
            }
        }
    }
}

class IdleState
{
    readonly object mutex = new object();
    CancellationTokenSource timeout;

    /// <summary>
    /// Get the cancellation token.
    /// </summary>
    /// <remarks>
    /// <para>The cancellation token is the brute-force approach to cancelling the IDLE and/or NOOP command.</para>
    /// <para>Using the cancellation token will typically drop the connection to the server and so should
    /// not be used unless the client is in the process of shutting down or otherwise needs to
    /// immediately abort communication with the server.</para>
    /// </remarks>
    /// <value>The cancellation token.</value>
    public CancellationToken CancellationToken { get; private set; }

    /// <summary>
    /// Get the done token.
    /// </summary>
    /// <remarks>
    /// <para>The done token tells the <see cref="Program.IdleLoop"/> that the user has requested to end the loop.</para>
    /// <para>When the done token is cancelled, the <see cref="Program.IdleLoop"/> will gracefully come to an end by
    /// cancelling the timeout and then breaking out of the loop.</para>
    /// </remarks>
    /// <value>The done token.</value>
    public CancellationToken DoneToken { get; private set; }

    /// <summary>
    /// Get the IMAP client.
    /// </summary>
    /// <value>The IMAP client.</value>
    public ImapClient Client { get; private set; }

    /// <summary>
    /// Check whether or not either of the CancellationToken's have been cancelled.
    /// </summary>
    /// <value><c>true</c> if cancellation was requested; otherwise, <c>false</c>.</value>
    public bool IsCancellationRequested
    {
        get
        {
            return CancellationToken.IsCancellationRequested || DoneToken.IsCancellationRequested;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdleState"/> class.
    /// </summary>
    /// <param name="client">The IMAP client.</param>
    /// <param name="doneToken">The user-controlled 'done' token.</param>
    /// <param name="cancellationToken">The brute-force cancellation token.</param>
    public IdleState(ImapClient client, CancellationToken doneToken, CancellationToken cancellationToken = default(CancellationToken))
    {
        CancellationToken = cancellationToken;
        DoneToken = doneToken;
        Client = client;

        // When the user hits a key, end the current timeout as well
        doneToken.Register(CancelTimeout);
    }

    /// <summary>
    /// Cancel the timeout token source, forcing ImapClient.Idle() to gracefully exit.
    /// </summary>
    void CancelTimeout()
    {
        lock (mutex)
        {
            if (timeout != null)
                timeout.Cancel();
        }
    }

    /// <summary>
    /// Set the timeout source.
    /// </summary>
    /// <param name="source">The timeout source.</param>
    public void SetTimeoutSource(CancellationTokenSource source)
    {
        lock (mutex)
        {
            timeout = source;

            if (timeout != null && IsCancellationRequested)
                timeout.Cancel();
        }
    }
}

