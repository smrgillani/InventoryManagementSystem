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
    public class UnitDAL
    {
        public void InsertUpdateUnit(Units U)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Unit", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUnit_id", U.id);
            cmd.Parameters.AddWithValue("@pUnit_Name", (U.Unit_Name == null) ? Convert.DBNull : U.Unit_Name);
            cmd.Parameters.AddWithValue("@pEnable", "1");
            cmd.Parameters.AddWithValue("@pAddedBy", (U.AddedBy == 0) ? Convert.DBNull : U.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (U.UpdatedBy == 0) ? Convert.DBNull : U.UpdatedBy);
            cmd.Parameters.AddWithValue("@Time_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Date_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Month_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@Year_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pUnitid_Out = new SqlParameter("@pUnitid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pUnitid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Unitid_Out = pUnitid_Out.Value.ToString();

            U.pFlag = Flag;
            U.pDesc = Desc;
            U.pUnitid_Out = Unitid_Out;
        }

        public List<Units> SelectAll(string Option, string search, string From, string To)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Units", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (Option == "All")
            {
                cmd.Parameters.AddWithValue("@pEnable", null);
            }
            else if (Option == "Active" || Option == null)
            {
                cmd.Parameters.AddWithValue("@pEnable", 1);
            }
            else if (Option == "Inactive")
            {
                cmd.Parameters.AddWithValue("@pEnable", 0);
            }
            cmd.Parameters.AddWithValue("@pUnit_Name", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            return fetchEntries(cmd);
        }

        public Units SelectById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Units_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUnit_id", id);
            List<Units> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Units UnitByName(string Unit_Name)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Unit_By_Name", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUnit_Name", Unit_Name);
            List<Units> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Units Where Unit_id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepUnit(Units U)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateUnitVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", U.id);
            cmd.Parameters.AddWithValue("@pEnable", U.Enable);
            RunQuery(cmd);
        }

        public string DelUnitRequest(List<Units> U, string type)
        {
            DeleteUnitRequested_Datatable deleteUnitRequested_Datatable = new DeleteUnitRequested_Datatable();
            deleteUnitRequested_Datatable.FillDataTable(U);
            var dt = deleteUnitRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Unit_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Units CheckUnitForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Unit_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUnit_id", id);
            List<Units> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Units> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Units> units = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    units = new List<Units>();
                    while (dr.Read())
                    {
                        Units li = new Units();
                        li.id = Convert.ToInt32(dr["Unit_id"]);
                        li.Unit_Name = Convert.ToString(dr["Unit_Name"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        units.Add(li);
                    }
                    units.TrimExcess();
                }
            }
            return units;
        }

        private List<Units> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Units> units = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    units = new List<Units>();
                    while (dr.Read())
                    {
                        Units li = new Units();
                        li.Unit_Name = Convert.ToString(dr["Unit_Name"]);
                        units.Add(li);
                    }
                    units.TrimExcess();
                }
            }
            return units;
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
