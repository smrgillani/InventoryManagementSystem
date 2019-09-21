﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Unit
    {
        public int id { get; set; }
        public string Unit_Name { get; set; }
        public string Unit_Code { get; set; }
        public int Enable { get; set; }
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
        public string pUnitid_Out { get; set; }
    }
}