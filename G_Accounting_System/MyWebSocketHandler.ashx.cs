using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.WebSockets;
using System.Web.SessionState;
using System.Threading.Tasks;

namespace G_Accounting_System.Code
{
    /// <summary>
    /// Summary description for MyWebSocketHandler
    /// </summary>
    public class MyWebSocketHandler : IHttpHandler, IRequiresSessionState
    {
        private System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {

                HttpSessionState session = context.Session;
                string userId = session["UserId"].ToString();

                //check authenication here...
                bool authenValid = true;

                if (authenValid)
                    context.AcceptWebSocketRequest(webSocketContext => ProcessWebsocketSession(webSocketContext, userId));
            }
            else
            {
                string jsonResponse = serializer.Serialize(new
                {
                    type = "warning",
                    userId = "",
                    message = "someone try to hack our website! I have to close all connection!"
                });

                //close all connections.
                MyWebSocket.clients.Broadcast(jsonResponse);
                List<MyWebSocket> websocketListTmp = new List<MyWebSocket>(MyWebSocket.clients.Select(tmp => (MyWebSocket)tmp));
                foreach (MyWebSocket websocket in websocketListTmp)
                {
                    websocket.Close();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private Task ProcessWebsocketSession(AspNetWebSocketContext context, string userId)
        {
            MyWebSocket handler = new MyWebSocket(userId);
            var processTask = handler.ProcessWebSocketRequestAsync(context);
            return processTask;
        }
    }
}