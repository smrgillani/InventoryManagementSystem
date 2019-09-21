using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Tokens
    {
        public int id { get; set; }
        public int User_id { get; set; }
        public int Premises_id { get; set; }
        public string AccessToken { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string pToken_Out { get; set; }
    }
}
