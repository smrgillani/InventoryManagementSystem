using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G_Accounting_System.Models
{
    public class Email
    {
        public int id { get; set; }
        public List<MailAttachment> MailAttachments { get; set; }
        public UniqueId mail_id { get; set; }
        public ulong Id { get; set; }
        public bool IsValid { get; set; }
        public ulong Validity { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string Subject { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string Status { get; set; }
        public int User_id { get; set; }
        public HttpPostedFileWrapper AttachFile { get; set; }
        public MailAttachment base64 { get; set; }
        public int Read { get; set; }
        public int Unread { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }   
    }
}