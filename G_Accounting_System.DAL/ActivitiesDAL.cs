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
    public class ActivitiesDAL
    {
        public void InsertActivity(List<Activities> A)
        {
            ItemActivity_Datatable itemActivity_Datatable = new ItemActivity_Datatable();
            itemActivity_Datatable.FillDataTable(A);
            var dt = itemActivity_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Insert_ItemActivity", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);

            RunQuery(cmd);

        }

        public List<Activities> Activities(int ActivityType_id, string ActivityType)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_ItemActivity", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pActivityType_id", ActivityType_id);
            cmd.Parameters.AddWithValue("@pActivityType", ActivityType);
            return fetchEntries(cmd);
        }

        private List<Activities> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Activities> activities = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        activities = new List<Activities>();
                        while (dr.Read())
                        {
                            Activities li = new Activities();
                            li.id = Convert.ToInt32(dr["id"]);
                            //li.Item_id = Convert.ToInt32(dr["Item_id"]);
                            //li.ItemName = Convert.ToString(dr["ItemName"]);
                            li.ActivityType_id = Convert.ToInt32(dr["ActivityType_id"]);
                            li.ActivityType = Convert.ToString(dr["ActivityType"]);
                            li.ActivityName = Convert.ToString(dr["ActivityName"]);
                            li.Description = Convert.ToString(dr["Description"]);
                            li.Date = Convert.ToString(dr["Date"]);
                            li.Time = Convert.ToString(dr["Time"]);
                            li.User_id = Convert.ToInt32(dr["User_id"]);
                            li.UserName = Convert.ToString(dr["UserName"]);
                            li.Icon = Convert.ToString(dr["Icon"]);
                            activities.Add(li);
                        }
                        activities.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                activities = null;
            }
            return activities;
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
