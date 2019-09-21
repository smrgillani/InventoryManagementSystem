using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Data;
using G_Accounting_System.DAL.DataTables;

namespace G_Accounting_System.DAL
{
    public class MailboxDAL
    {
        public void InsertEmail(Emails E, List<MailAttachments> mailAttachments)
        {
            MailAttachments_Datatable mailAttachments_Datatable = new MailAttachments_Datatable();
            mailAttachments_Datatable.FillDataTable(mailAttachments);
            var dt = mailAttachments_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_InsertEmail", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pEmailTo", E.EmailTo);
            cmd.Parameters.AddWithValue("@pEmailFrom", E.EmailFrom);
            cmd.Parameters.AddWithValue("@pSubject", E.Subject);
            cmd.Parameters.AddWithValue("@pBody", E.Body);
            cmd.Parameters.AddWithValue("@pStatus", E.Status);
            cmd.Parameters.AddWithValue("@pUser_id", E.User_id);
            cmd.Parameters.AddWithValue("@pTimeOfDay", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDateOfDay", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonthOfDay", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYearOfDay", DateTime.Now.ToString("yyyy"));
            cmd.Parameters.AddWithValue("@dt", dt);
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            E.pFlag = Flag;
            E.pDesc = Desc;
        }

        internal void RunQuery(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            con.Open();
            using (con)
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
