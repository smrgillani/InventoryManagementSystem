﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Messages
    {
        public int id { get; set; }
        public int Sender_id { get; set; }
        public Contacts SenderName { get; set; }
        public int Receiver_id { get; set; }
        public Contacts ReceiverName { get; set; }
        public string strMessage { get; set; }
        public string Status { get; set; }
        public int Enable { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
    }
}
