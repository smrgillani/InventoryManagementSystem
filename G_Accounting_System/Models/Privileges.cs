﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Privileges
    {
        public int Role_id { set; get; }
        public int Role_Priv_id { set; get; }
        public int Priv_id { set; get; }
        public string Priv_Name { set; get; }
        public bool check { set; get; }
    }
}