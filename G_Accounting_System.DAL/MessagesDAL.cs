using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Data;

namespace G_Accounting_System.DAL
{
    public class MessagesDAL
    {
        public List<Messages> Messages(int Sender_id, int Receiver_id)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_Messages", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSender_id", Sender_id);
            cmd.Parameters.AddWithValue("@pReceiver_id", Receiver_id);

            return fetchEntries(cmd);
        }

        public void SendMessage(Messages M)
        {
            SqlCommand cmd = new SqlCommand("proc_SendMessage", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pSender_id", M.Sender_id);
            cmd.Parameters.AddWithValue("@pReceiver_id", M.Receiver_id);
            cmd.Parameters.AddWithValue("@pMessage", M.strMessage);
            cmd.Parameters.AddWithValue("@pStatus", M.Status);
            cmd.Parameters.AddWithValue("@pEnable", M.Enable);
            cmd.Parameters.AddWithValue("@pDate", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pTime", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pMonth", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            M.pFlag = Flag;
            M.pDesc = Desc;
        }

        public void UpdateStatus(Messages M)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateMessageStatus", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pReceiver_id", M.Receiver_id);
            cmd.Parameters.AddWithValue("@pStatus", M.Status);

            RunQuery(cmd);
        }

        public void DeleteChat(Messages M)
        {
            SqlCommand cmd = new SqlCommand("proc_DeleteChat", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pSender_id", M.Sender_id);
            cmd.Parameters.AddWithValue("@pReceiver_id", M.Receiver_id);
            cmd.Parameters.AddWithValue("@pEnable", M.Enable);
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            M.pFlag = Flag;
            M.pDesc = Desc;
        }

        public List<Messages> MessagesNotifications(int User_id)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_MessagesNotifications", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pReceiver_id", User_id);

            return fetchEntries(cmd);
        }

        private List<Messages> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Messages> messages = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    messages = new List<Messages>();
                    while (dr.Read())
                    {
                        Messages li = new Messages();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.Sender_id = (dr["Sender_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Sender_id"]);
                        li.SenderName = (Convert.ToString(dr["Sender_attachedprofile"]) != null) ? new ContactDAL().SelectById(Convert.ToInt32(dr["Sender_attachedprofile"]), null) : null;
                        li.Receiver_id = (dr["Receiver_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Receiver_id"]);
                        if (li.Receiver_id != 0)
                        {
                            li.ReceiverName = (Convert.ToString(dr["Receiver_attachedprofile"]) != null) ? new ContactDAL().SelectById(Convert.ToInt32(dr["Receiver_attachedprofile"]), null) : null;
                        }
                        li.strMessage = Convert.ToString(dr["Message"] ?? Convert.DBNull);
                        li.Date = Convert.ToString(dr["Date"]);
                        li.Time = Convert.ToString(dr["Time"]);
                        li.Month = Convert.ToString(dr["Month"]);
                        li.Year = Convert.ToString(dr["Year"]);
                        messages.Add(li);
                    }
                    messages.TrimExcess();
                }
            }
            return messages;
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
