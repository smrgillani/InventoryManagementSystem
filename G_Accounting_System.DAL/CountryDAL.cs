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
    public class CountryDAL
    {
        public void InsertUpdateCountry(Countries C)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Countries", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", C.id);
            cmd.Parameters.AddWithValue("@pName", (C.Name == null) ? Convert.DBNull : C.Name);
            cmd.Parameters.AddWithValue("@pEnable", C.Enable);
            cmd.Parameters.AddWithValue("@pAddedBy", (C.AddedBy == 0) ? Convert.DBNull : C.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (C.UpdatedBy == 0) ? Convert.DBNull : C.UpdatedBy);
            cmd.Parameters.AddWithValue("@pTimeOfDay", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDateOfDay", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonthOfDay", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYearOfDay", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pCountryid_Out = new SqlParameter("@pCountryid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pCountryid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Countryid_Out = pCountryid_Out.Value.ToString();

            C.pFlag = Flag;
            C.pDesc = Desc;
            C.pCountryid_Out = Countryid_Out;
        }

        //public void Update(Countries C)
        //{
        //    SqlCommand cmd = new SqlCommand("UPDATE Countries set Name = @Full_name , Enable = @Enable where id = @Id", DALUtil.getConnection());
        //    cmd.Parameters.AddWithValue("@Full_name", (C.Name == null) ? Convert.DBNull : C.Name);
        //    cmd.Parameters.AddWithValue("@Enable", C.Enable);
        //    cmd.Parameters.AddWithValue("@Id", C.id);
        //    RunQuery(cmd);
        //}

        public List<Countries> SelectAllCountries(string Option, string search, string From, string To)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_Countries", DALUtil.getConnection());
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
            cmd.Parameters.AddWithValue("@pName", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            return fetchEntries(cmd);
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Countries Where id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepCountry(Countries C)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateCountryVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", C.id);
            cmd.Parameters.AddWithValue("@pEnable", C.Enable);
            RunQuery(cmd);
        }

        public string DelCountryRequest(List<Countries> C, string type)
        {
            DeleteCountriesRequested_Datatable deleteCountriesRequested_Datatable = new DeleteCountriesRequested_Datatable();
            deleteCountriesRequested_Datatable.FillDataTable(C);
            var dt = deleteCountriesRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Countries_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Countries SelectById(int id, int? sop)
        {
            SqlCommand cmd;

            if (sop == null)
            {
                cmd = new SqlCommand("Select * from Countries where Id=@Id", DALUtil.getConnection());
            }
            else
            {
                cmd = new SqlCommand("Select * from Countries where Id=@Id and Enable = @Sop", DALUtil.getConnection());
                cmd.Parameters.AddWithValue("@Sop", sop);
            }

            cmd.Parameters.AddWithValue("@Id", id);

            List<Countries> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Countries CheckCountryForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Country_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCountry_id", id);
            List<Countries> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Countries> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Countries> countries = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    countries = new List<Countries>();
                    while (dr.Read())
                    {
                        Countries li = new Countries();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.Name = Convert.ToString(dr["Name"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.AddedBy = (dr["AddedBy"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["AddedBy"]);
                        li.UpdatedBy = (dr["UpdatedBy"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["UpdatedBy"]);
                        li.Time = Convert.ToString(dr["TimeOfDay"]);
                        li.Date = Convert.ToString(dr["DateOfDay"]);
                        li.Month = Convert.ToString(dr["MonthOfDay"]);
                        li.Year = Convert.ToString(dr["YearOfDay"]);
                        countries.Add(li);
                    }
                    countries.TrimExcess();
                }
            }
            return countries;
        }

        private List<Countries> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Countries> countries = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    countries = new List<Countries>();
                    while (dr.Read())
                    {
                        Countries li = new Countries();
                        li.Name = Convert.ToString(dr["Name"]);
                        countries.Add(li);
                    }
                    countries.TrimExcess();
                }
            }
            return countries;
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
