using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace G_Accounting_System.DAL
{
    public class DALUtil
    {
        internal static SqlConnection getConnection()
        {
            string constr = ConfigurationManager.ConnectionStrings["G_Accounting_Systems"].ConnectionString;
            return new SqlConnection(constr);
        }
    }
}
