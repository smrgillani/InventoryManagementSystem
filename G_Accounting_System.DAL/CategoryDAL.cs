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
    public class CategoryDAL
    {
        public void InsertUpdateCategory(Categories C)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Category", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCategory_id", C.id);
            cmd.Parameters.AddWithValue("@pCategory_Name", C.Category_Name ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pEnable", "1");
            cmd.Parameters.AddWithValue("@pAddedBy", (C.AddedBy == 0) ? Convert.DBNull : C.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (C.UpdatedBy == 0) ? Convert.DBNull : C.UpdatedBy);
            cmd.Parameters.AddWithValue("@Time_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Date_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Month_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@Year_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pCategoryid_Out = new SqlParameter("@pCategoryid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pCategoryid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Categoryid_Out = pCategoryid_Out.Value.ToString();

            C.pFlag = Flag;
            C.pDesc = Desc;
            C.pCategoryid_Out = Categoryid_Out;
        }

        public List<Categories> SelectAll(string Option, string search, string From, string To)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_Categories", DALUtil.getConnection());
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
            cmd.Parameters.AddWithValue("@pCategory_Name", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            return fetchEntries(cmd);
        }

        public Categories SelectById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Categories_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCategory_id", id);
            List<Categories> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Categories CategoryByName(string Category_Name)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Category_By_Name", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCategory_Name", Category_Name);
            List<Categories> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Categories Where Category_id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepCategory(Categories C)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateCategoryVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", C.id);
            cmd.Parameters.AddWithValue("@pEnable", C.Enable);
            RunQuery(cmd);
        }

        public string DelCategoryRequest(List<Categories> C, string type)
        {
            DeleteCategoryRequested_Datatable deleteCategoryRequested_Datatable = new DeleteCategoryRequested_Datatable();
            deleteCategoryRequested_Datatable.FillDataTable(C);
            var dt = deleteCategoryRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Category_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Categories CheckCategoryForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Category_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCategory_id", id);
            List<Categories> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Categories> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Categories> categories = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    categories = new List<Categories>();
                    while (dr.Read())
                    {
                        Categories li = new Categories();
                        li.id = Convert.ToInt32(dr["Category_id"]);
                        li.Category_Name = Convert.ToString(dr["Category_Name"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        categories.Add(li);
                    }
                    categories.TrimExcess();
                }
            }
            return categories;
        }

        private List<Categories> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Categories> categories = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    categories = new List<Categories>();
                    while (dr.Read())
                    {
                        Categories li = new Categories();
                        li.Category_Name = Convert.ToString(dr["Category_Name"]);
                        categories.Add(li);
                    }
                    categories.TrimExcess();
                }
            }
            return categories;
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
