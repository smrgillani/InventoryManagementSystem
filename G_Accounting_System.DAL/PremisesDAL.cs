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
    public class PremisesDAL
    {
        public void Add(Premisess P)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Premises", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", P.id);
            cmd.Parameters.AddWithValue("@pName", (P.Name == null) ? Convert.DBNull : P.Name);
            cmd.Parameters.AddWithValue("@pPc_Mac_Address", (P.pc_mac_Address == null) ? Convert.DBNull : P.pc_mac_Address);
            cmd.Parameters.AddWithValue("@pPhone", (P.Phone == null) ? Convert.DBNull : P.Phone);
            cmd.Parameters.AddWithValue("@pCity", (P.City == null) ? Convert.DBNull : P.City);
            cmd.Parameters.AddWithValue("@pCountry", (P.Country == null) ? Convert.DBNull : P.Country);
            cmd.Parameters.AddWithValue("@pAddress", (P.Address == null) ? Convert.DBNull : P.Address);
            cmd.Parameters.AddWithValue("@pOffice", (P.Office == null) ? Convert.DBNull : P.Office);
            cmd.Parameters.AddWithValue("@pFactory", (P.Factory == null) ? Convert.DBNull : P.Factory);
            cmd.Parameters.AddWithValue("@pStore", (P.Store == null) ? Convert.DBNull : P.Store);
            cmd.Parameters.AddWithValue("@pShop", (P.Shop == null) ? Convert.DBNull : P.Shop);
            cmd.Parameters.AddWithValue("@pEnable", P.Enable);
            cmd.Parameters.AddWithValue("@pAddedBy", (P.AddedBy == 0) ? Convert.DBNull : P.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (P.UpdatedBy == 0) ? Convert.DBNull : P.UpdatedBy);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day ", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pPremisesid_Out = new SqlParameter("@pPremisesid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pPremisesid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Premisesid_Out = pPremisesid_Out.Value.ToString();

            P.pFlag = Flag;
            P.pDesc = Desc;
            P.pPremisesid_Out = Premisesid_Out;
        }

        public List<Premisess> SelectAllStores(string Option, string search, string From, string To)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Stores", DALUtil.getConnection());
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

        public List<Premisess> SelectAllOffices(string Option, string search, string From, string To)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Offices", DALUtil.getConnection());
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

        public List<Premisess> SelectAllShops(string Option, string search, string From, string To)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Shops", DALUtil.getConnection());
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

        public List<Premisess> SelectAllFactories(string Option, string search, string From, string To)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Factories", DALUtil.getConnection());
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

        public List<Premisess> PremisesByType(string Office, string Factories, string Stores, string Shops)
        {
            SqlCommand cmd = new SqlCommand("proc_Premises_By_Type", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pOffice", Office);
            cmd.Parameters.AddWithValue("@pFactory", Factories);
            cmd.Parameters.AddWithValue("@pStore", Stores);
            cmd.Parameters.AddWithValue("@pShop", Shops);
            return fetchEntries(cmd);
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Premises Where id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepPremises(Premisess P)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdatePremisesVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", P.id);
            cmd.Parameters.AddWithValue("@pEnable", P.Enable);
            RunQuery(cmd);
        }

        public string DelPremisesRequest(List<Premisess> P, string type)
        {
            DeletePremisesRequested_Datatable deletePremisesRequested_Datatable = new DeletePremisesRequested_Datatable();
            deletePremisesRequested_Datatable.FillDataTable(P);
            var dt = deletePremisesRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Premises_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Premisess SelectById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Premises_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", id);
            List<Premisess> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Premisess CheckPremisesForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Premises_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPremises_id", id);
            List<Premisess> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Premisess> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Premisess> premisess = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    premisess = new List<Premisess>();
                    while (dr.Read())
                    {
                        Premisess li = new Premisess();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.Name = Convert.ToString(dr["Name"]);
                        li.pc_mac_Address = Convert.ToString(dr["Pc_Mac_Address"]);
                        li.Phone = Convert.ToString(dr["Phone"]);
                        li.City = Convert.ToInt32(dr["City"]);
                        li.Country = Convert.ToInt32(dr["Country"]);
                        li.CityName = Convert.ToString(dr["CityName"]);
                        li.CountryName = Convert.ToString(dr["CountryName"]);
                        li.Address = Convert.ToString(dr["Address"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Office = Convert.ToString(dr["Office"]);
                        li.Factory = Convert.ToString(dr["Factory"]);
                        li.Shop = Convert.ToString(dr["Shop"]);
                        li.Store = Convert.ToString(dr["Store"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.AddedBy = (dr["AddedBy"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["AddedBy"]);
                        li.UpdatedBy = (dr["UpdatedBy"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["UpdatedBy"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        premisess.Add(li);
                    }
                    premisess.TrimExcess();
                }
            }
            return premisess;
        }

        private List<Premisess> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Premisess> premisess = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    premisess = new List<Premisess>();
                    while (dr.Read())
                    {
                        Premisess li = new Premisess();
                        li.Name = Convert.ToString(dr["Name"]);
                        premisess.Add(li);
                    }
                    premisess.TrimExcess();
                }
            }
            return premisess;
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
