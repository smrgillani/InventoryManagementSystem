using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class City
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Country { get; set; }
        public string CountryName { get; set; }
        public string  IsEnabled { get; set; }
        public string IsEnabled_ { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Delete_Request_By { get; set; }
        public string Delete_Status { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pCityid_Out { get; set; }
    }
}