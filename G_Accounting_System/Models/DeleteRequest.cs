using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class DeleteRequest
    {
        public int id { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int DeleteRequestedBy_id { get; set; }
        public string DeleteRequestedBy { get; set; }
    }
}