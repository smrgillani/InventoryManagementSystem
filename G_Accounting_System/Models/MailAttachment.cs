using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class MailAttachment
    {
        public int id { get; set; }
        public HttpPostedFileWrapper AttachFile { get; set; }
        public string FileName { get; set; }
        public string base64 { get; set; }
    }
}