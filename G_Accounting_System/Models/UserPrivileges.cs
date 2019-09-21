using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class UserPrivileges
    {
        public int priv_ID { set; get; }
        public int id { get; set; }
        public int User_id { get; set; }
        public string PrivName { get; set; }
        public int Add { get; set; }
        public int Edit { get; set; }
        public int Delete { get; set; }
        public int View { get; set; }
        public int Profile { get; set; }
        public int Receive { get; set; }
        public int BillAdd { get; set; }
        public int BillView { get; set; }
        public int PaymentAdd { get; set; }
        public int PayementView { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}
