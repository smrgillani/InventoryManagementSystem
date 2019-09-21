using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class DeleteRequests
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
