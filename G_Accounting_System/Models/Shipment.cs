using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Shipment
    {
        public int Shipment_id { get; set; }
        public int SaleOrder_id { get; set; }
        public string Shipment_No { get; set; }
        public int Package_id { get; set; }
        public string Shipment_Date { get; set; }
        public string Shipment_Cost { get; set; }
        public string Shipment_Status { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pShipementIdout { get; set; }
    }
}