using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Premises
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string pc_mac_Address { get; set; }
        public string Phone { get; set; }
        public int City { get; set; }
        public int Country { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string Office { get; set; }
        public string Factory { get; set; }
        public string Store { get; set; }
        public string Shop { get; set; }
        public string Address { get; set; }
        public int Enable { get; set; }
        public string _Enable { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Delete_Request_By { get; set; }
        public string Delete_Status { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pPremisesid_Out { get; set; }
    }
}