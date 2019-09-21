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
    public class AdminDAL
    {
        public void ChangePasswordUpdate(Users U)
        {
            SqlCommand cmd = new SqlCommand("proc_ChangePassword", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", U.id);
            cmd.Parameters.AddWithValue("@pCurrentPassword", U.CurrentPassword ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pNewPassword", U.NewPassword ?? Convert.DBNull);
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            U.pFlag = Flag;
            U.pDesc = Desc;
        }

        public void InsertUpdateRoles(Roles R)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Roles", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pRole_id", R.id);
            cmd.Parameters.AddWithValue("@pRole_Name", R.Role_Name ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@pEnable", "1");
            cmd.Parameters.AddWithValue("@pAddedBy", (R.AddedBy == 0) ? Convert.DBNull : R.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (R.UpdatedBy == 0) ? Convert.DBNull : R.UpdatedBy);
            cmd.Parameters.AddWithValue("@Time_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Date_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Month_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@Year_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            R.pFlag = Flag;
            R.pDesc = Desc;
        }

        public void UpdateToken(Tokens T)
        {
            SqlCommand cmd = new SqlCommand("proc_Update_Tokens", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUser_id", T.User_id);
            cmd.Parameters.AddWithValue("@pToken", T.AccessToken);
            cmd.Parameters.AddWithValue("@pIssueDate", T.IssueDate);
            cmd.Parameters.AddWithValue("@pExpiryDate", T.ExpiryDate);
            SqlParameter pToken_Out = new SqlParameter("@pToken_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pToken_Out);

            RunQuery(cmd);

            string Token_Out = pToken_Out.Value.ToString();

            T.pToken_Out = Token_Out;
        }

        public Tokens GetToken(string authenticationToken)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Token", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pauthenticationToken", authenticationToken);
            List<Tokens> temp = fetchGTEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Users Login(Users U)
        {
            SqlCommand cmd = cmd = new SqlCommand("proc_User_Login", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserEmail", U.email);
            cmd.Parameters.AddWithValue("@UserPassword", U.password);
            List<Users> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Roles SelectById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Roles_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pRole_id", id);
            List<Roles> temp = fetchREntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Roles GetRoleByRoleName(string Role_Name)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_RoleId_By_RoleName", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pRole_Name", Role_Name);
            List<Roles> temp = fetchREntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Roles Where Role_id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public List<UserPrivilegess> UserPrivileges(string email, string userpassword)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_User_Privileges", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserEmail", email);
            cmd.Parameters.AddWithValue("@UserPassword", userpassword);

            return fetchPEntries(cmd);
        }

        public List<Roles> AllRoles()
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_Roles", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            return fetchREntries(cmd);
        }

        public List<RolePrivileges> RolePrivileges(string search)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Role_Privileges", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pRoleID", search);

            return fetchRPEntries(cmd);
        }

        public List<UserPrivilegess> UserPrivilegess(int User_id)
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Userss_Privileges", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserID", User_id);

            return fetchUPEntries(cmd);
        }

        public string UpdateRolePrivileges(List<RolePrivileges> RP)
        {
            RolePriv_DataTable rolePriv_DataTable = new RolePriv_DataTable();
            rolePriv_DataTable.FillDataTable(RP);
            var dt = rolePriv_DataTable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_RolePrivileges", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            RunQuery(cmd);

            return "";
        }

        public string UpdateUserPrivileges(List<UserPrivilegess> UP)
        {
            UserPrivDataTable userPrivDataTable = new UserPrivDataTable();
            userPrivDataTable.FillDataTable(UP);
            var dt = userPrivDataTable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_UserPrivileges", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            RunQuery(cmd);

            return "";
        }

        public List<DeleteRequests> SelectAllDeleteRequests()
        {
            SqlCommand cmd;

            cmd = new SqlCommand("proc_Select_DeleteRequests", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            return fetchDREntries(cmd);
        }

        private List<Users> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Users> users = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    users = new List<Users>();
                    while (dr.Read())
                    {
                        Users li = new Users();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.email = Convert.ToString(dr["email"]);
                        li.Name = (Convert.ToString(dr["attached_profile"]) != null) ? new ContactDAL().SelectById(Convert.ToInt32(dr["attached_profile"]), null) : null;
                        li.Role_id = Convert.ToInt32(dr["Role_id"]);
                        li.Premises_id = Convert.ToInt32(dr["Premises_id"]);
                        li.Role = Convert.ToString(dr["Role"]);

                        users.Add(li);
                    }
                    users.TrimExcess();
                }
            }
            return users;
        }

        private List<UserPrivilegess> fetchPEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<UserPrivilegess> privilegess = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    privilegess = new List<UserPrivilegess>();
                    while (dr.Read())
                    {
                        UserPrivilegess li = new UserPrivilegess();
                        li.priv_ID = Convert.ToInt32(dr["Priv_id"] != DBNull.Value ? dr["Priv_id"] : 0);

                        privilegess.Add(li);
                    }
                    privilegess.TrimExcess();
                }
            }
            return privilegess;
        }

        private List<Roles> fetchREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Roles> roles = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    roles = new List<Roles>();
                    while (dr.Read())
                    {
                        Roles li = new Roles();
                        li.id = Convert.ToInt32(dr["Role_id"]);
                        li.Role_Name = Convert.ToString(dr["Role_Name"]);
                        li.Enable = Convert.ToString(dr["Enable"]);
                        li.Time_Of_Day = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date_Of_Day = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month_Of_Day = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year_Of_Day = Convert.ToString(dr["Year_Of_Day"]);

                        roles.Add(li);
                    }
                    roles.TrimExcess();
                }
            }
            return roles;
        }

        private List<RolePrivileges> fetchRPEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<RolePrivileges> rp = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    rp = new List<RolePrivileges>();
                    while (dr.Read())
                    {
                        RolePrivileges li = new RolePrivileges();
                        li.Role_Priv_id = Convert.ToInt32(dr["Role_Priv_id"]);
                        li.Priv_id = Convert.ToInt32(dr["Priv_id"]);
                        li.Priv_Name = Convert.ToString(dr["Priv_Name"]);
                        li.Check_Status = Convert.ToString(dr["Check_Status"]);

                        rp.Add(li);
                    }
                    rp.TrimExcess();
                }
            }
            return rp;
        }

        private List<UserPrivilegess> fetchUPEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<UserPrivilegess> up = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    up = new List<UserPrivilegess>();
                    while (dr.Read())
                    {
                        UserPrivilegess li = new UserPrivilegess();
                        li.id = Convert.ToInt32(dr["id"] != DBNull.Value ? dr["id"] : 0);
                        li.priv_ID = Convert.ToInt32(dr["Priv_id"] != DBNull.Value ? dr["Priv_id"] : 0);
                        li.PrivName = Convert.ToString(dr["Priv_Name"] != DBNull.Value ? dr["Priv_Name"] : null);
                        li.Add = Convert.ToInt32(dr["Add"] != DBNull.Value ? dr["Add"] : 0);
                        li.Edit = Convert.ToInt32(dr["Edit"] != DBNull.Value ? dr["Edit"] : 0);
                        //li.Delete = Convert.ToInt32(dr["Delete"] != DBNull.Value ? dr["Delete"] : 0);
                        li.View = Convert.ToInt32(dr["View"] != DBNull.Value ? dr["View"] : 0);
                        li.Profile = Convert.ToInt32(dr["Profile"] != DBNull.Value ? dr["Profile"] : 0);
                        li.ControllerName = Convert.ToString(dr["ControllerName"] != DBNull.Value ? dr["ControllerName"] : null);


                        up.Add(li);
                    }
                    up.TrimExcess();
                }
            }
            return up;
        }

        private List<DeleteRequests> fetchDREntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<DeleteRequests> rp = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    rp = new List<DeleteRequests>();
                    while (dr.Read())
                    {
                        DeleteRequests li = new DeleteRequests();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.Type = Convert.ToString(dr["Type"]);
                        li.Count = (dr["Count"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Count"]);

                        rp.Add(li);
                    }
                    rp.TrimExcess();
                }
            }
            return rp;
        }

        private List<Tokens> fetchGTEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Tokens> getToken = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    getToken = new List<Tokens>();
                    while (dr.Read())
                    {
                        Tokens li = new Tokens();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.User_id = Convert.ToInt32(dr["User_id"]);
                        li.AccessToken = Convert.ToString(dr["Token"]);
                        li.IssueDate = Convert.ToString(dr["IssueDate"]);
                        li.ExpiryDate = Convert.ToString(dr["ExpiryDate"]);

                        getToken.Add(li);
                    }
                    getToken.TrimExcess();
                }
            }
            return getToken;
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
