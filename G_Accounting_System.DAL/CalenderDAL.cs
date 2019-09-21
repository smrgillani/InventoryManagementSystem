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
    public class CalenderDAL
    {
        public void AddEvents(Calenders C)
        {
            SqlCommand cmd = new SqlCommand("proc_AddEvents", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pEventName", (C.title == null) ? Convert.DBNull : C.title);
            cmd.Parameters.AddWithValue("@pBackgroundColour", (C.backgroundColor == null) ? Convert.DBNull : C.backgroundColor);
            cmd.Parameters.AddWithValue("@pBorderColour", (C.borderColor == null) ? Convert.DBNull : C.borderColor);
            cmd.Parameters.AddWithValue("@pAddedBy", (C.AddedBy == 0) ? Convert.DBNull : C.AddedBy);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pEventid_Out = new SqlParameter("@pEventid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pEventid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Eventid_Out = pEventid_Out.Value.ToString();

            C.pFlag = Flag;
            C.pDesc = Desc;
            C.pEventid_Out = Eventid_Out;
        }

        public void AddCalenderEvents(Calenders C)
        {
            SqlCommand cmd = new SqlCommand("proc_AddCalenderEvents", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pEvent_id", C.id);
            cmd.Parameters.AddWithValue("@pEvent_Start_Date", C.start);
            cmd.Parameters.AddWithValue("@pStartedBy", C.AddedBy);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            C.pFlag = Flag;
            C.pDesc = Desc;
        }

        public List<Calenders> GetCalenderEvents(int User_id)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_CalenderEvents_UserID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUser_id", User_id);

            return fetchCEntries(cmd);
        }

        public List<Calenders> GetEventsName(int User_id)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_EventsName_UserID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUser_id", User_id);

            return fetchEntries(cmd);
        }

        private List<Calenders> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Calenders> eventsname = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    eventsname = new List<Calenders>();
                    while (dr.Read())
                    {
                        Calenders li = new Calenders();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.title = Convert.ToString(dr["EventName"]);
                        li.backgroundColor = Convert.ToString(dr["BackgroundColour"]);
                        li.borderColor = Convert.ToString(dr["BorderColour"]);
                        li.AddedBy = Convert.ToInt32(dr["AddedBy"]);
                        li.Time_Of_Day = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date_Of_Day = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month_Of_Day = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year_Of_Day = Convert.ToString(dr["Year_Of_Day"]);
                        eventsname.Add(li);
                    }
                    eventsname.TrimExcess();
                }
            }
            return eventsname;
        }

        private List<Calenders> fetchCEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Calenders> eventsname = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    eventsname = new List<Calenders>();
                    while (dr.Read())
                    {
                        Calenders li = new Calenders();
                        li.id = Convert.ToInt32(dr["Event_id"]);
                        li.title = Convert.ToString(dr["title"]);
                        li.backgroundColor = Convert.ToString(dr["backgroundColor"]);
                        li.borderColor = Convert.ToString(dr["borderColor"]);
                        li.start = Convert.ToString(dr["start"]);
                        li.AddedBy = Convert.ToInt32(dr["StartedBy"]);
                        li.Time_Of_Day = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date_Of_Day = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month_Of_Day = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year_Of_Day = Convert.ToString(dr["Year_Of_Day"]);
                        eventsname.Add(li);
                    }
                    eventsname.TrimExcess();
                }
            }
            return eventsname;
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
