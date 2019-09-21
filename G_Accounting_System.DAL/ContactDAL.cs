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
    public class ContactDAL
    {
        public void InsertUpdateContacts(Contacts C)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Contacts", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", C.id);
            cmd.Parameters.AddWithValue("@pImage", (C.File_Name == null) ? Convert.DBNull : C.File_Name);
            cmd.Parameters.AddWithValue("@pSalutation", (C.Salutation == null) ? Convert.DBNull : C.Salutation);
            cmd.Parameters.AddWithValue("@pName", (C.Name == null) ? Convert.DBNull : C.Name);
            cmd.Parameters.AddWithValue("@pCompany", C.CompanyId);
            cmd.Parameters.AddWithValue("@pDesignation", (C.Designation == null) ? Convert.DBNull : C.Designation);
            cmd.Parameters.AddWithValue("@pLandline", (C.Landline == null) ? Convert.DBNull : C.Landline);
            cmd.Parameters.AddWithValue("@pMobile", (C.Mobile == null) ? Convert.DBNull : C.Mobile);
            cmd.Parameters.AddWithValue("@pEmail", (C.Email == null) ? Convert.DBNull : C.Email);
            cmd.Parameters.AddWithValue("@pWebsite", (C.Website == null) ? Convert.DBNull : C.Website);
            cmd.Parameters.AddWithValue("@pAddress", (C.Address == null) ? Convert.DBNull : C.Address);
            cmd.Parameters.AddWithValue("@pAddressLandline", (C.AddressLandline == null) ? Convert.DBNull : C.AddressLandline);
            cmd.Parameters.AddWithValue("@pCity", (C.City == null) ? Convert.DBNull : C.City);
            cmd.Parameters.AddWithValue("@pCountry", (C.Country == null) ? Convert.DBNull : C.Country);
            cmd.Parameters.AddWithValue("@pBankAccountNumber", (C.BankAccountNumber == null) ? Convert.DBNull : C.BankAccountNumber);
            cmd.Parameters.AddWithValue("@pPaymentMethod", (C.PaymentMethod == null) ? Convert.DBNull : C.PaymentMethod);
            cmd.Parameters.AddWithValue("@pEnable", C.Enable);
            cmd.Parameters.AddWithValue("@pAddedBy", (C.AddedBy == 0) ? Convert.DBNull : C.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (C.UpdatedBy == 0) ? Convert.DBNull : C.UpdatedBy);
            cmd.Parameters.AddWithValue("@pVendor", C.Vendor);
            cmd.Parameters.AddWithValue("@pCustomer", C.Customer);
            cmd.Parameters.AddWithValue("@pEmployee", C.Employee);
            cmd.Parameters.AddWithValue("@pTimeOfDay", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDateOfDay", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonthOfDay", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYearOfDay", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pContactid_Out = new SqlParameter("@pContactid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pContactid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Contactid_Out = pContactid_Out.Value.ToString();

            C.pFlag = Flag;
            C.pDesc = Desc;
            C.pContactid_Out = Contactid_Out;
        }

        //public void Update(Contacts C)
        //{
        //    SqlCommand cmd = new SqlCommand("UPDATE Contacts set Salutation = @Salutation , Name = @Full_name , Company = @Company_name , Designation = @Designation , Landline = @Contact_phone_landline , Mobile = @Contact_phone_mobile , Email = @Contact_email , Website = @Website , Address = @Address , AddressLandline = @Address_phone , City = @Address_city , Country = @Address_country , BankAccountNumber = @Bank_account_number , PaymentMethod = @Payment_method , Enable = @Enable where id = @Id", DALUtil.getConnection());
        //    cmd.Parameters.AddWithValue("@Salutation", (C.Salutation == null) ? Convert.DBNull : C.Salutation);
        //    cmd.Parameters.AddWithValue("@Full_name", (C.Name == null) ? Convert.DBNull : C.Name);
        //    cmd.Parameters.AddWithValue("@Company_name", (C.Company == null) ? Convert.DBNull : C.Company);
        //    cmd.Parameters.AddWithValue("@Designation", (C.Designation == null) ? Convert.DBNull : C.Designation);
        //    cmd.Parameters.AddWithValue("@Contact_phone_landline", (C.Landline == null) ? Convert.DBNull : C.Landline);
        //    cmd.Parameters.AddWithValue("@Contact_phone_mobile", (C.Mobile == null) ? Convert.DBNull : C.Mobile);
        //    cmd.Parameters.AddWithValue("@Contact_email", (C.Email == null) ? Convert.DBNull : C.Email);
        //    cmd.Parameters.AddWithValue("@Website", (C.Website == null) ? Convert.DBNull : C.Website);
        //    cmd.Parameters.AddWithValue("@Address", (C.Address == null) ? Convert.DBNull : C.Address);
        //    cmd.Parameters.AddWithValue("@Address_phone", (C.AddressLandline == null) ? Convert.DBNull : C.AddressLandline);
        //    cmd.Parameters.AddWithValue("@Address_city", (C.City == null) ? Convert.DBNull : C.City);
        //    cmd.Parameters.AddWithValue("@Address_country", (C.Country == null) ? Convert.DBNull : C.Country);
        //    cmd.Parameters.AddWithValue("@Bank_account_number", (C.BankAccountNumber == null) ? Convert.DBNull : C.BankAccountNumber);
        //    cmd.Parameters.AddWithValue("@Payment_method", (C.PaymentMethod == null) ? Convert.DBNull : C.PaymentMethod);
        //    cmd.Parameters.AddWithValue("@Enable", C.Enable);
        //    cmd.Parameters.AddWithValue("@Id", C.id);
        //    RunQuery(cmd);
        //}

        public List<Contacts> ContactByType(int Customer, int Vendor, int Employee)
        {

            SqlCommand cmd = new SqlCommand("proc_Contact_By_Type", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCustomer", Customer);
            cmd.Parameters.AddWithValue("@pVendor", Vendor);
            cmd.Parameters.AddWithValue("@pEmployee", Employee);
            return fetchEntries(cmd);
        }

        public Contacts VendorByName(string Name)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Vendor_By_Name", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pName", Name);
            List<Contacts> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }


        public List<Contacts> SelectAllVendors(string Option, string search, string From, string To)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Vendors", DALUtil.getConnection());
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

        public List<Contacts> SelectAllCustomers(string Option, string search, string From, string To)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Customers", DALUtil.getConnection());
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

        public List<Contacts> SelectAllEmployees(string Option, string search, string From, string To)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Employees", DALUtil.getConnection());
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

        public void UpdatepContact(Contacts C)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateContactVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", C.id);
            cmd.Parameters.AddWithValue("@pEnable", C.Enable);
            RunQuery(cmd);
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Contacts Where id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public string DelContactRequest(List<Contacts> C, string type)
        {
            DeleteContactsRequested_Datatable deleteContactsRequested_Datatable = new DeleteContactsRequested_Datatable();
            deleteContactsRequested_Datatable.FillDataTable(C);
            var dt = deleteContactsRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Contacts_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Contacts SelectById(int id, int vop, int cop, int eop, int? sop)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_ContactByID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pid", id);
            cmd.Parameters.AddWithValue("@pVendor", vop);
            cmd.Parameters.AddWithValue("@pCustomer", cop);
            cmd.Parameters.AddWithValue("@pEmployee", eop);

            List<Contacts> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Contacts SelectById(int id, int? eop)
        {
            SqlCommand cmd;

            if (eop == null)
            {
                cmd = new SqlCommand("Select * from Contacts where Id=@Id", DALUtil.getConnection());
                cmd.Parameters.AddWithValue("@Id", id);
            }
            else
            {
                cmd = new SqlCommand("Select * from Contacts where Id=@Id and Enable = @Eop", DALUtil.getConnection());
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Eop", eop);
            }

            List<Contacts> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Contacts CheckContactForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Contacts_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pContact_id", id);
            List<Contacts> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Contacts> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Contacts> contacts = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    contacts = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts li = new Contacts();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.Salutation = Convert.ToString(dr["Salutation"]);
                        li.File_Name = (dr["Image"] == DBNull.Value) ? null : Convert.ToString(dr["Image"]);
                        li.Name = Convert.ToString(dr["Name"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.CompanyId = Convert.ToInt32(dr["Company"]);
                        li.Designation = Convert.ToString(dr["Designation"]);
                        li.Landline = Convert.ToString(dr["Landline"]);
                        li.Mobile = Convert.ToString(dr["Mobile"]);
                        li.Email = Convert.ToString(dr["Email"]);
                        li.Website = Convert.ToString(dr["Website"]);
                        li.Address = Convert.ToString(dr["Address"]);
                        li.AddressLandline = Convert.ToString(dr["AddressLandline"]);
                        li.City = Convert.ToString(dr["City"]);
                        li.Country = Convert.ToString(dr["Country"]);
                        li.BankAccountNumber = Convert.ToString(dr["BankAccountNumber"]);
                        li.PaymentMethod = Convert.ToString(dr["PaymentMethod"]);
                        li.Vendor = Convert.ToInt32(dr["Vendor"]);
                        li.Customer = Convert.ToInt32(dr["Customer"]);
                        li.Employee = Convert.ToInt32(dr["Employee"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Enable = Convert.ToInt32(dr["Enable"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["TimeOfDay"]);
                        li.Date = Convert.ToString(dr["DateOfDay"]);
                        li.Month = Convert.ToString(dr["MonthOfDay"]);
                        li.Year = Convert.ToString(dr["YearOfDay"]);
                        contacts.Add(li);
                    }
                    contacts.TrimExcess();
                }
            }
            return contacts;
        }

        private List<Contacts> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Contacts> contacts = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    contacts = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts li = new Contacts();
                        li.Name = Convert.ToString(dr["Name"]);
                        contacts.Add(li);
                    }
                    contacts.TrimExcess();
                }
            }
            return contacts;
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
