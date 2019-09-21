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
    public class CompanyDAL
    {
        public void InsertUpdateCompanies(Companies C)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Company", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", C.id);
            cmd.Parameters.AddWithValue("@pName", C.Name ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pLandline", C.Landline ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pMobile", C.Mobile ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pEmail", C.Email ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pWebsite", C.Website ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pAddress", (C.Address == null) ? Convert.DBNull : C.Address);
            cmd.Parameters.AddWithValue("@pCity", C.City ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pCountry", (C.Country == null) ? Convert.DBNull : C.Country);
            cmd.Parameters.AddWithValue("@pBankAccountNumber", C.BankAccountNumber ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pPaymentMethod", C.PaymentMethod ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pEnable", C.Enable);
            cmd.Parameters.AddWithValue("@pAddedBy", (C.AddedBy == 0) ? Convert.DBNull : C.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (C.UpdatedBy == 0) ? Convert.DBNull : C.UpdatedBy);
            cmd.Parameters.AddWithValue("@pTimeOfDay", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDateOfDay", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonthOfDay", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYearOfDay", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pCompanyid_Out = new SqlParameter("@pCompanyid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pCompanyid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Companyid_Out = pCompanyid_Out.Value.ToString();

            C.pFlag = Flag;
            C.pDesc = Desc;
            C.pCompanyid_Out = Companyid_Out;
        }

        //public void Update(Companies C)
        //{
        //    SqlCommand cmd = new SqlCommand("UPDATE Companies set Name = @Full_name , Landline = @Contact_phone_landline , Mobile = @Contact_phone_mobile , Email = @Contact_email , Website = @Website , Address = @Address , City = @Address_city , Country = @Address_country , BankAccountNumber = @Bank_account_number , PaymentMethod = @Payment_method , Enable = @Enable where id = @Id", DALUtil.getConnection());
        //    cmd.Parameters.AddWithValue("@Full_name", (C.Name == null) ? Convert.DBNull : C.Name);
        //    cmd.Parameters.AddWithValue("@Contact_phone_landline", (C.Landline == null) ? Convert.DBNull : C.Landline);
        //    cmd.Parameters.AddWithValue("@Contact_phone_mobile", (C.Mobile == null) ? Convert.DBNull : C.Mobile);
        //    cmd.Parameters.AddWithValue("@Contact_email", (C.Email == null) ? Convert.DBNull : C.Email);
        //    cmd.Parameters.AddWithValue("@Website", (C.Website == null) ? Convert.DBNull : C.Website);
        //    cmd.Parameters.AddWithValue("@Address", (C.Address == null) ? Convert.DBNull : C.Address);
        //    cmd.Parameters.AddWithValue("@Address_city", (C.City == null) ? Convert.DBNull : C.City);
        //    cmd.Parameters.AddWithValue("@Address_country", (C.Country == null) ? Convert.DBNull : C.Country);
        //    cmd.Parameters.AddWithValue("@Bank_account_number", (C.BankAccountNumber == null) ? Convert.DBNull : C.BankAccountNumber);
        //    cmd.Parameters.AddWithValue("@Payment_method", (C.PaymentMethod == null) ? Convert.DBNull : C.PaymentMethod);
        //    cmd.Parameters.AddWithValue("@Enable", C.Enable);
        //    cmd.Parameters.AddWithValue("@Id", C.id);
        //    RunQuery(cmd);
        //}

        public List<Companies> SelectAllCompanies(string Option, string search, string From, string To,int? id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Companies", DALUtil.getConnection());
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
            cmd.Parameters.AddWithValue("@pid", id);
            return fetchEntries(cmd);
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Companies Where id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepCompany(Companies C)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateCompanyVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", C.id);
            cmd.Parameters.AddWithValue("@pEnable", C.Enable);
            RunQuery(cmd);
        }

        public string DelCompanyRequest(List<Companies> C, string type)
        {
            DeleteCompaniesRequested_Datatable deleteCompaniesRequested_Datatable = new DeleteCompaniesRequested_Datatable();
            deleteCompaniesRequested_Datatable.FillDataTable(C);
            var dt = deleteCompaniesRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Company_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Companies SelectById(int id, int? sop)
        {
            SqlCommand cmd;

            if (sop == null)
            {
                cmd = new SqlCommand("Select * from Companies where Id=@Id ", DALUtil.getConnection());
            }
            else
            {
                cmd = new SqlCommand("Select * from Companies where Id=@Id and Enable = @Sop", DALUtil.getConnection());
                cmd.Parameters.AddWithValue("@Sop", sop);
            }

            cmd.Parameters.AddWithValue("@Id", id);

            List<Companies> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Companies CheckCompanyForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Company_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCompany_id", id);
            List<Companies> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Companies> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Companies> companies = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    companies = new List<Companies>();
                    while (dr.Read())
                    {
                        Companies li = new Companies();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.Name = Convert.ToString(dr["Name"]);
                        li.Landline = Convert.ToString(dr["Landline"]);
                        li.Mobile = Convert.ToString(dr["Mobile"]);
                        li.Email = Convert.ToString(dr["Email"]);
                        li.Website = Convert.ToString(dr["Website"]);
                        li.Address = Convert.ToString(dr["Address"]);
                        li.City = Convert.ToString(dr["City"]);
                        li.Country = Convert.ToString(dr["Country"]);
                        li.BankAccountNumber = Convert.ToString(dr["BankAccountNumber"]);
                        li.PaymentMethod = Convert.ToString(dr["PaymentMethod"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Time = Convert.ToString(dr["TimeOfDay"]);
                        li.Date = Convert.ToString(dr["DateOfDay"]);
                        li.Month = Convert.ToString(dr["MonthOfDay"]);
                        li.Year = Convert.ToString(dr["YearOfDay"]);
                        companies.Add(li);
                    }
                    companies.TrimExcess();
                }
            }
            return companies;
        }

        private List<Companies> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Companies> companies = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    companies = new List<Companies>();
                    while (dr.Read())
                    {
                        Companies li = new Companies();
                        li.Name = Convert.ToString(dr["Name"]);
                        companies.Add(li);
                    }
                    companies.TrimExcess();
                }
            }
            return companies;
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
