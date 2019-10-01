using G_Accounting_System.APP;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace G_Accounting_System.Code
{
    public class MyWebSocket : WebSocketHandler
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        public static WebSocketCollection clients = new WebSocketCollection();
        public static EmailNotification emailnotif = new EmailNotification();
        public string userId;
        ImapClient _imap;
        static CancellationTokenSource _done;
        public MyWebSocket(string userId)
        {
            this.userId = userId;
        }

        public override void OnOpen()
        {
            clients.Add(this);
           

            string jsonResponse = serializer.Serialize(new
            {
                type = "login",
                userId = userId
            });

            clients.Broadcast(jsonResponse);

            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(StartIdleProcess);

            //this.Send(string.Format("Welcome URL {0}, userId {1}", this.WebSocketContext.UserHostAddress, userId));
            //Send(jsonResponse);
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
            string jsonResponseEmailNotif = serializer.Serialize(new
            {
                Email = "salmanahmed635@gmail.com",
                type = "Email Notification"
            });
            clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId == this.userId).Send(jsonResponseEmailNotif);

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

        public override void OnMessage(string message)
        {
            try
            {
                Message mm = new JavaScriptSerializer().Deserialize<Message>(message);
                if (mm.Type == "Message")
                {
                    Contacts SenderName = new Catalog().SelectUser(mm.Sender_id).Name;
                    Contacts ReceiverName = new Catalog().SelectUser(mm.Receiver_id).Name;

                    string jsonResponseRcvr = serializer.Serialize(new
                    {
                        Sender_id = mm.Sender_id,
                        SenderName = SenderName.Name,
                        Receiver_id = mm.Receiver_id,
                        ReceiverName = ReceiverName.Name,
                        message = mm.strMessage,
                        Date = DateTime.Now.ToString("dd/MM/yyyy"),
                        Time = DateTime.Now.ToString("HH:mm:ss tt"),
                        type = "Message"
                        //TotalClient = AllWebSockClients(clients)

                    });
                    string jsonResponseNotf = serializer.Serialize(new
                    {
                        //Sender_id = mm.Sender_id,
                        //SenderName = SenderName.Name,
                        User_id = mm.Receiver_id,
                        ReceiverName = ReceiverName.Name,
                        message = mm.strMessage,
                        type = "Message Notification"
                        //TotalClient = AllWebSockClients(clients)
                    });
                    clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId != this.userId).Send(jsonResponseRcvr);
                    clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId != this.userId).Send(jsonResponseNotf);
                    clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId == this.userId).Send(jsonResponseRcvr);
                }
                else if (mm.Type == "Message Notification")
                {
                    //Contacts SenderName = new Catalog().SelectUser(mm.Sender_id).Name;
                    Contacts ReceiverName = new Catalog().SelectUser(mm.User_id).Name;
                    string jsonResponseRcvr = serializer.Serialize(new
                    {
                        //Sender_id = mm.Sender_id,
                        //SenderName = SenderName.Name,
                        User_id = mm.User_id,
                        ReceiverName = ReceiverName.Name,
                        message = mm.strMessage,
                        type = "Message Notification"
                        //TotalClient = AllWebSockClients(clients)

                    });
                    //clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId != this.userId).Send(jsonResponseRcvr);
                    clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId == this.userId).Send(jsonResponseRcvr);
                }
                else if (mm.Type == "Email Notification")
                {
                    string email = new Catalog().SelectUser(mm.User_id).email;
                


                    string jsonResponseEmailNotif = serializer.Serialize(new
                    {
                        Email = email,
                        type = "Email Notification"
                    });
                    clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId == this.userId).Send(jsonResponseEmailNotif);
                }
               


                //clients.Broadcast(jsonResponse);
                //you can also filter user!. 
                //But best practice is to send all notification and let client filter it.
                //clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId != this.userId);
                //clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId != this.userId);


                //clients.ElementAt(0).Send(jsonResponseRcvr);
                //clients.ElementAt(Convert.ToInt32(clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId.Equals(mm.RecvuserId)))).Send(jsonResponseRcvr);
                //clients.ElementAt(Convert.ToInt32(clients.SingleOrDefault(websocket => (websocket as MyWebSocket).userId.Equals(mm.SndruserId)))).Send(jsonResponseSndr);
            }
            catch (Exception e)
            {

            }
        }

        public WebSocketCollection AllWebSockClients()
        {
            return clients;
        }

        public override void OnClose()
        {
            base.OnClose();
            clients.Remove(this);

            string jsonResponse = serializer.Serialize(new
            {
                type = "logout",
                userId = userId
            });

            clients.Broadcast(jsonResponse);
            //Send(jsonResponse);
        }

        public override void OnError()
        {
            base.OnError();

            string jsonResponse = serializer.Serialize(new
            {
                type = "error",
                userId = userId
            });

            clients.Broadcast(jsonResponse);
            //Send(jsonResponse);
        }
    }
}