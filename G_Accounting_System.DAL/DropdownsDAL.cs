using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace G_Accounting_System.DAL
{
    public class DropdownsDAL
    {
        public List<Dropdowns> CategoriesDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Categories", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> BrandsDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Brands", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> ManufacturersDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Manufacturers", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> UnitsDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Units", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> VendorsDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Vendors", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> ItemsDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Items", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> PaymentModesDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_PaymentMode", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> CustomersDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Customer", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> CountriesDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Countries", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> CitiesDropdown(int Country_id)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Cities", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCountry_id", Country_id);
            return fetchEntries(cmd);
        }

        public List<Dropdowns> RolesDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Roles", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> CompaniesDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Companies", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        public List<Dropdowns> UsersDropdown()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Dropdown_Users", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return fetchEntries(cmd);
        }

        private List<Dropdowns> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dropdowns> dropdown = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dropdown = new List<Dropdowns>();
                    while (dr.Read())
                    {
                        Dropdowns li = new Dropdowns();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.name = Convert.ToString(dr["name"]);
                        dropdown.Add(li);
                    }
                    dropdown.TrimExcess();
                }
            }
            return dropdown;
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
