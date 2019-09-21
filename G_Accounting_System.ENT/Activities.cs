using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Activities
    {
        public int id { get; set; }

        public int ActivityType_id { get; set; }
        public string ActivityType { get; set; }

        public int Item_id { get; set; }
        public string ItemName { get; set; }
        public Items Item_Name { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int User_id { get; set; }
        public string UserName { get; set; }
        public Contacts User_Name { get; set; }
        public string Icon { get; set; }
    }
}
