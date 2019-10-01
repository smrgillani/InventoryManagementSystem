using G_Accounting_System.APP;
using G_Accounting_System.Code;
using G_Accounting_System.ENT;
using G_Accounting_System.Handlers;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebSockets;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel.Activation;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace G_Accounting_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static CancellationTokenSource _done;
        ImapClient _imap;
        static string user_id;
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);         
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(StartIdleProcess);

            if (worker.IsBusy)
                worker.CancelAsync();

            worker.RunWorkerAsync();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired && !string.IsNullOrWhiteSpace(authTicket.UserData))
                {
                    var data = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), data);
                }
            }
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session["UserIdd"] = HttpContext.Current.User;
            user_id = HttpContext.Current.User.Identity.Name;
        }

        private void StartIdleProcess(object sender, DoWorkEventArgs e)
        {
            _imap = new ImapClient();
            _imap.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            //_imap.Connect(ConfigurationManager.AppSettings["IncomingServerName"], Convert.ToInt32(ConfigurationManager.AppSettings["InPort"]), Convert.ToBoolean(ConfigurationManager.AppSettings["IncomingIsSSL"]));
            _imap.AuthenticationMechanisms.Remove("XOAUTH");
            //_imap.Authenticate(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["Password"]);
            _imap.Authenticate("salmanahmed635@gmail.com", "unforgetable");
            _imap.Inbox.Open(FolderAccess.ReadWrite);
            _imap.Inbox.CountChanged += Inbox_MessagesArrived;

            _done = new CancellationTokenSource();
            _imap.Idle(_done.Token);
        }

        public void Inbox_MessagesArrived(object sender, EventArgs e)
        {
            //sers User = new Catalog().SelectUser(mm.User_id);
            string From = "";
            string Subject = "";
            string Message = "";
            try
            {
                string jsonResponseEmailNotif = new JavaScriptSerializer().Serialize(new
                {
                    Email = "salmanahmed635@gmail.com",
                    type = "Email Notification"
                });
                //userId = HttpContext.Current.Session["UserIdd"].ToString();
                //if (HttpContext.Current != null && HttpContext.Current.Session != null)
                //{
                //    //string userId = HttpContext.Current.Session["UserIdd"].ToString();
                //}
                string userEmail = user_id;
            }
            catch (Exception ex)
            {

            }
            //WebSocketCollection clients = new WebSocketCollection();

            //clients = new MyWebSocket().AllWebSockClients();

            //new MyWebSocket().clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId == this.userId).Send(jsonResponseEmailNotif);

            //using (var client = new ImapClient())
            //{
            //    client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
            //    client.Authenticate("salmanahmed635@gmail.com", "unforgetable");


            //    client.Inbox.Open(FolderAccess.ReadWrite);
            //    var m = client.Inbox.Search(SearchQuery.New);
            //    foreach (var uid in client.Inbox.Search(SearchQuery.NotSeen))
            //    {
            //        try
            //        {
            //            var message = client.Inbox.GetMessage(uid);

            //            client.Inbox.SetFlags(uid, MessageFlags.Recent, true);
            //            From = message.From.ToString();
            //            Subject = message.Subject.ToString();
            //            Message = message.Body.ToString();
            //        }
            //        catch (Exception)
            //        {

            //        }
            //    }
            //    client.Disconnect(true);
            //}
        }
    }

}
