using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class MailAttachments
    {
        public int id { get; set; }
        public int Mailbox_id { get; set; }
        public string FileName { get; set; }
        public string base64 { get; set; }
    }
}
