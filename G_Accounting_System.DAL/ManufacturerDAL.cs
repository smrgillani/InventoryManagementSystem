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
    public class ManufacturerDAL
    {
        public void InsertUpdateManufacturer(Manufacturers M)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Manufacturer", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pManufacturer_id", M.id);
            cmd.Parameters.AddWithValue("@pManufacturer_Name", M.Manufacturer_Name ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pEnable", "1");
            cmd.Parameters.AddWithValue("@pAddedBy", (M.AddedBy == 0) ? Convert.DBNull : M.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (M.UpdatedBy == 0) ? Convert.DBNull : M.UpdatedBy);
            cmd.Parameters.AddWithValue("@Time_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Date_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Month_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@Year_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pManufacturerid_Out = new SqlParameter("@pManufacturerid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pManufacturerid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Manufacturerid_Out = pManufacturerid_Out.Value.ToString();

            M.pFlag = Flag;
            M.pDesc = Desc;
            M.pManufacturerid_Out = Manufacturerid_Out;
        }

        public List<Manufacturers> SelectAll(string Option, string search, string From, string To)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_Manufacturers", DALUtil.getConnection());
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
            cmd.Parameters.AddWithValue("@pManufacturer_Name", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            return fetchEntries(cmd);
        }

        public Manufacturers SelectById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Manufacturers_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pManufacturer_id", id);
            List<Manufacturers> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Manufacturers ManufacturerByName(string Manufacturer_Name)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Manufacturer_By_Name", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pManufacturer_Name", Manufacturer_Name);
            List<Manufacturers> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Manufacturers Where Manufacturer_id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepManufacturer(Manufacturers M)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateManufacturerVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", M.id);
            cmd.Parameters.AddWithValue("@pEnable", M.Enable);
            RunQuery(cmd);
        }

        public string DelManufacturerRequest(List<Manufacturers> M, string type)
        {
            DeleteManufacturerRequested_Datatable deleteManufacturerRequested_Datatable = new DeleteManufacturerRequested_Datatable();
            deleteManufacturerRequested_Datatable.FillDataTable(M);
            var dt = deleteManufacturerRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Manufacturer_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Manufacturers CheckManufacturerForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Manufacturer_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pManufacturer_id", id);
            List<Manufacturers> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Manufacturers> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Manufacturers> manufacturers = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    manufacturers = new List<Manufacturers>();
                    while (dr.Read())
                    {
                        Manufacturers li = new Manufacturers();
                        li.id = Convert.ToInt32(dr["Manufacturer_id"]);
                        li.Manufacturer_Name = Convert.ToString(dr["Manufacturer_Name"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        manufacturers.Add(li);
                    }
                    manufacturers.TrimExcess();
                }
            }
            return manufacturers;
        }

        private List<Manufacturers> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Manufacturers> manufacturers = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    manufacturers = new List<Manufacturers>();
                    while (dr.Read())
                    {
                        Manufacturers li = new Manufacturers();
                        li.Manufacturer_Name = Convert.ToString(dr["Manufacturer_Name"]);
                        manufacturers.Add(li);
                    }
                    manufacturers.TrimExcess();
                }
            }
            return manufacturers;
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
