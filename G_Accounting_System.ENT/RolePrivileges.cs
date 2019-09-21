using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class RolePrivileges
    {
        public int Role_id { set; get; }
        public int Role_Priv_id { set; get; }
        public int Priv_id { set; get; }
        public string Priv_Name { set; get; }
        public string Enable { set; get; }
        public string Check_Status { set; get; }
    }
}
