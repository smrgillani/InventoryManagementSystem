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
    public class DeleteRequestDAL
    {
        public List<DeleteRequests> AllItemsDelRequest(string type)
        {
            SqlCommand cmd = null;
            switch (type)
            {
                case "Offices":
                    cmd = new SqlCommand("proc_Select_DelRequested_Offices", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Factories":
                    cmd = new SqlCommand("proc_Select_DelRequested_Factories", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Stores":
                    cmd = new SqlCommand("proc_Select_DelRequested_Stores", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Shops":
                    cmd = new SqlCommand("proc_Select_DelRequested_Shops", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Vendors":
                    cmd = new SqlCommand("proc_Select_DelRequested_Vendors", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Companies":
                    cmd = new SqlCommand("proc_Select_DelRequested_Companies", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Customers":
                    cmd = new SqlCommand("proc_Select_DelRequested_Customers", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Employees":
                    cmd = new SqlCommand("proc_Select_DelRequested_Employees", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Items":
                    cmd = new SqlCommand("proc_Select_DelRequested_Items", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Categories":
                    cmd = new SqlCommand("proc_Select_DelRequested_Categories", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Brands":
                    cmd = new SqlCommand("proc_Select_DelRequested_Brands", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Manufacturers":
                    cmd = new SqlCommand("proc_Select_DelRequested_Manufacturers", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Units":
                    cmd = new SqlCommand("proc_Select_DelRequested_Units", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Countries":
                    cmd = new SqlCommand("proc_Select_DelRequested_Countries", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Cities":
                    cmd = new SqlCommand("proc_Select_DelRequested_Cities", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Users":
                    cmd = new SqlCommand("proc_Select_DelRequested_Users", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Purchases":
                    cmd = new SqlCommand("proc_Select_DelRequested_Purchases", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;
                case "Sales":
                    cmd = new SqlCommand("proc_Select_DelRequested_Sales", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    break;

                default:

                    break;
            }
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public void Delete(int id, string type)
        {
            SqlCommand cmd = null;
            switch (type)
            {
                case "Offices":
                    cmd = new SqlCommand("proc_Delete_Premises", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Offices");
                    break;
                case "Factories":
                    cmd = new SqlCommand("proc_Delete_Premises", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Factories");
                    break;
                case "Stores":
                    cmd = new SqlCommand("proc_Delete_Premises", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Stores");
                    break;
                case "Shops":
                    cmd = new SqlCommand("proc_Delete_Premises", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Shops");
                    break;
                case "Vendors":
                    cmd = new SqlCommand("proc_Delete_Contacts", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Vendors");
                    break;
                case "Companies":
                    cmd = new SqlCommand("proc_Delete_Companies", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Companies");
                    break;
                case "Customers":
                    cmd = new SqlCommand("proc_Delete_Contacts", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Customers");
                    break;
                case "Employees":
                    cmd = new SqlCommand("proc_Delete_Contacts", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Employees");
                    break;
                case "Items":
                    cmd = new SqlCommand("proc_Delete_Items", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Items");
                    break;
                case "Categories":
                    cmd = new SqlCommand("proc_Delete_Categories", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Categories");
                    break;
                case "Brands":
                    cmd = new SqlCommand("proc_Delete_Brands", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Brands");
                    break;
                case "Manufacturers":
                    cmd = new SqlCommand("proc_Delete_Manufacturers", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Manufacturers");
                    break;
                case "Units":
                    cmd = new SqlCommand("proc_Delete_Units", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Units");
                    break;
                case "Countries":
                    cmd = new SqlCommand("proc_Delete_Countries", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Countries");
                    break;
                case "Cities":
                    cmd = new SqlCommand("proc_Delete_Cities", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Cities");
                    break;
                case "Users":
                    cmd = new SqlCommand("proc_Delete_Users", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Users");
                    break;
                case "Purchases":
                    cmd = new SqlCommand("proc_Delete_Purchasing", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Purchases");
                    break;
                case "Sales":
                    cmd = new SqlCommand("proc_Delete_Sales", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", id);
                    cmd.Parameters.AddWithValue("@ptype", "Sales");
                    break;


                default:

                    break;
            }
            
            RunQuery(cmd);
        }

        private List<DeleteRequests> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<DeleteRequests> categories = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    categories = new List<DeleteRequests>();
                    while (dr.Read())
                    {
                        DeleteRequests li = new DeleteRequests();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.Name = Convert.ToString(dr["name"]);
                        li.DeleteRequestedBy_id = Convert.ToInt32(dr["Delete_Request_By"]);
                        li.DeleteRequestedBy = Convert.ToString(dr["username"]);

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
