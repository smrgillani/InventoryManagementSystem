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
    public class BrandDAL
    {
        public void InsertUpdateBrands(Brands B)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Brands", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pBrand_id", B.id);
            cmd.Parameters.AddWithValue("@pBrand_Name", B.Brand_Name ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pEnable", "1");
            cmd.Parameters.AddWithValue("@pAddedBy", (B.AddedBy == 0) ? Convert.DBNull : B.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (B.UpdatedBy == 0) ? Convert.DBNull : B.UpdatedBy);
            cmd.Parameters.AddWithValue("@Time_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Date_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Month_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@Year_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pBrandid_Out = new SqlParameter("@pBrandid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pBrandid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Brandid_Out = pBrandid_Out.Value.ToString();

            B.pFlag = Flag;
            B.pDesc = Desc;
            B.pBrandid_Out = Brandid_Out;
        }

        public List<Brands> SelectAll(string Option, string search, string From, string To)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_Brands", DALUtil.getConnection());
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
            cmd.Parameters.AddWithValue("@pBrand_Name", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            return fetchEntries(cmd);


        }

        public Brands SelectById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Brands_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pBrand_id", id);
            List<Brands> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Brands BrandByName(string Brand_Name)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Brand_By_Name", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pBrand_Name", Brand_Name);
            List<Brands> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Brands Where Brand_id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepBrand(Brands B)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateBrandVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", B.id);
            cmd.Parameters.AddWithValue("@pEnable", B.Enable);
            RunQuery(cmd);
        }

        public string DelBrandRequest(List<Brands> B, string type)
        {
            DeleteBrandsRequested_Datatable deleteBrandsRequested_Datatable = new DeleteBrandsRequested_Datatable();
            deleteBrandsRequested_Datatable.FillDataTable(B);
            var dt = deleteBrandsRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Brand_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Brands CheckBrandForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Brands_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pBrand_id", id);
            List<Brands> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Brands> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Brands> brands = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    brands = new List<Brands>();
                    while (dr.Read())
                    {
                        Brands li = new Brands();
                        li.id = Convert.ToInt32(dr["Brand_id"]);
                        li.Brand_Name = Convert.ToString(dr["Brand_Name"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        brands.Add(li);
                    }
                    brands.TrimExcess();
                }
            }
            return brands;
        }

        private List<Brands> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Brands> brands = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    brands = new List<Brands>();
                    while (dr.Read())
                    {
                        Brands li = new Brands();
                        li.Brand_Name = Convert.ToString(dr["Brand_Name"]);
                        brands.Add(li);
                    }
                    brands.TrimExcess();
                }
            }
            return brands;
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
