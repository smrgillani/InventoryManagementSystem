using G_Accounting_System.APP;
using G_Accounting_System.Code;
using G_Accounting_System.ENT;
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

namespace G_Accounting_System
{
    public class EmailNotification
    {
        static CancellationTokenSource _done;
        ImapClient _imap;
        public static WebSocketCollection clients = new WebSocketCollection();
        
    }
}