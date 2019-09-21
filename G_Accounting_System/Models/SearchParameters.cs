using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class SearchParameters
    {
        public string Option { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Draw { get; set; }
        public int PageStart { get; set; }
        public int PageLength { get; set; }
        public string Search { get; set; }
        public string filtertype { get; set; }
        public string type { get; set; }
        public string ItemName { get; set; }
        public string CustomerName { get; set; }
        public string StockAvailability { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }
}