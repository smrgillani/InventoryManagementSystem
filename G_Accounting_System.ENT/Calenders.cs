using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Calenders
    {
        public int id { get; set; }
        public string title { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string CalenderEventDate { get; set; }
        public string start { get; set; }
        public string Enable { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Delete_Request_By { get; set; }
        public string Delete_Status { get; set; }
        public string Time_Of_Day { get; set; }
        public string Date_Of_Day { get; set; }
        public string Month_Of_Day { get; set; }
        public string Year_Of_Day { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pEventid_Out { get; set; }
    }
}
