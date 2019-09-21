using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G_Accounting_System.Models;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json;

namespace G_Accounting_System
{
    public class MyWSHandler : WebSocketHandler
    {
        private static WebSocketCollection _wsClients = new WebSocketCollection();
        public override void OnOpen()
        {
            _wsClients.Add(this);



            base.OnOpen();

        }

        public override void OnMessage(string message)
        {
            string msgBack = string.Format(
                "inbox hello");
            this.Send(msgBack);
        }

        public override void OnClose()
        {
            _wsClients.Remove(this);

            base.OnClose();
        }

        public override void OnError()
        {
            base.OnError();
        }

        public void SendMessage(Email message)
        {
            if (string.IsNullOrEmpty(message.User_id.ToString()))
            {
                SendBroadcastMessage(message);
            }
            else
            {
                SendMessage(message, message.User_id);
            }
        }

        public void SendMessage(Email message, int User_id)
        {
            var webSockets = _wsClients.Where(s =>
            {
                var httpCookie = s.WebSocketContext.Cookies["SessionId"];

                return httpCookie != null && httpCookie.Value == User_id.ToString();
            });
            foreach (var socket in webSockets)
            {
                socket.Send(JsonConvert.SerializeObject(message));
            }
        }

        public void SendBroadcastMessage(Email msg)
        {
            _wsClients.Broadcast(JsonConvert.SerializeObject(msg));
        }
    }
}

