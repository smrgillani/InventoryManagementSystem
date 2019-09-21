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
    public class UserDAL
    {
        public void InsertUpdateUser(Users U)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Users", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", U.id);
            cmd.Parameters.AddWithValue("@pemail", (U.email == null) ? Convert.DBNull : U.email);
            cmd.Parameters.AddWithValue("@pattached_profile", (U.attached_profile == null) ? Convert.DBNull : U.attached_profile);
            cmd.Parameters.AddWithValue("@ppassword", (U.password == null) ? Convert.DBNull : U.password);
            cmd.Parameters.AddWithValue("@pEnable", (U.status == null) ? Convert.DBNull : U.status);
            cmd.Parameters.AddWithValue("@pPremises_id", U.Premises_id);
            cmd.Parameters.AddWithValue("@pRole_id", U.Role_id);
            //cmd.Parameters.AddWithValue("@ppao", U.pao ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppaf", U.paf ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppas", U.pas ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppas_", U.pas_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppav", U.pav ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppap", U.pap ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppac", U.pac ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppas__", U.pas__ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppae", U.pae ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppap_", U.pap_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppai", U.pai ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppas___", U.pas___ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppau", U.pau ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppuo", U.puo ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppuf", U.puf ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppus", U.pus ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppus_", U.pus_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppuv", U.puv ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppup", U.pup ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppuc", U.puc ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppus__", U.pus__ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppue", U.pue ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppup_", U.pup_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppui", U.pui ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppus___", U.pus___ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppuu", U.puu ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdo", U.pdo ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdf", U.pdf ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppds", U.pds ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppds_", U.pds_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdv", U.pdv ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdp", U.pdp ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdc", U.pdc ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppds__", U.pds__ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppde", U.pde ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdp_", U.pdp_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdi", U.pdi ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppds___", U.pds___ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppdu", U.pdu ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvo", U.pvo ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvf", U.pvf ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvs", U.pvs ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvs_", U.pvs_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvv", U.pvv ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvp", U.pvp ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvc", U.pvc ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvs__", U.pvs__ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppve", U.pve ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvp_", U.pvp_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvi", U.pvi ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvs___", U.pvs___ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvu", U.pvu ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvol", U.pvol ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvfl", U.pvfl ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvsl", U.pvsl ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvsl_", U.pvsl_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvvl", U.pvvl ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvpl", U.pvpl ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvcl", U.pvcl ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvsl__", U.pvsl__ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvel", U.pvel ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvpl_", U.pvpl_ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvil", U.pvil ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvsl___", U.pvsl___ ? 1 : 0);
            //cmd.Parameters.AddWithValue("@ppvul", U.pvul ? 1 : 0);
            cmd.Parameters.AddWithValue("@pAddedBy", (U.AddedBy == 0) ? Convert.DBNull : U.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (U.UpdatedBy == 0) ? Convert.DBNull : U.UpdatedBy);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pUserid_Out = new SqlParameter("@pUserid_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pUserid_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Userid_Out = pUserid_Out.Value.ToString();

            U.pFlag = Flag;
            U.pDesc = Desc;
            U.pUserid_Out = Userid_Out;
        }


        //public void Update(Users U)
        //{
        //    SqlCommand cmd = new SqlCommand("update Users set email = @email , password = @password , attached_profile = @attached_profile , pao = @pao , paf = @paf , pas = @pas , pas_ = @pas_ , pav = @pav , pap = @pap , pac = @pac , pas__ = @pas__ , pae = @pae , pap_ = @pap_ , pai = @pai , pas___ = @pas___ , pau = @pau , puo = @puo , puf = @puf , pus = @pus , pus_ = @pus_ , puv = @puv , pup = @pup , puc = @puc , pus__ = @pus__ , pue = @pue , pup_ = @pup_ , pui = @pui , pus___ = @pus___ , puu = @puu , pdo = @pdo , pdf = @pdf , pds = @pds , pds_ = @pds_ , pdv = @pdv , pdp = @pdp , pdc = @pdc , pds__ = @pds__ , pde = @pde , pdp_ = @pdp_ , pdi = @pdi , pds___ = @pds___ , pdu = @pdu , pvo = @pvo , pvf = @pvf , pvs = @pvs , pvs_ = @pvs_ , pvv = @pvv , pvp = @pvp , pvc = @pvc , pvs__ = @pvs__ , pve = @pve , pvp_ = @pvp_ , pvi = @pvi , pvs___ = @pvs___, pvu = @pvu , pvol = @pvol , pvfl = @pvfl , pvsl = @pvsl , pvsl_  = @pvsl_, pvvl = @pvvl , pvpl = @pvpl , pvcl = @pvcl , pvsl__ = @pvsl__ , pvel = @pvel , pvpl_ = @pvpl_ , pvil = @pvil , pvsl___ = @pvsl___ , pvul = @pvul , Enable = @status where id = @id", DALUtil.getConnection());
        //    cmd.Parameters.AddWithValue("@email", (U.email == null) ? Convert.DBNull : U.email);
        //    cmd.Parameters.AddWithValue("@attached_profile", (U.attached_profile == null) ? Convert.DBNull : U.attached_profile);
        //    cmd.Parameters.AddWithValue("@password", (U.password == null) ? Convert.DBNull : U.password);
        //    cmd.Parameters.AddWithValue("@status", (U.status == null) ? Convert.DBNull : U.status);
        //    cmd.Parameters.AddWithValue("@pao", U.pao ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@paf", U.paf ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pas", U.pas ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pas_", U.pas_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pav", U.pav ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pap", U.pap ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pac", U.pac ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pas__", U.pas__ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pae", U.pae ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pap_", U.pap_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pai", U.pai ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pas___", U.pas___ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pau", U.pau ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@puo", U.puo ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@puf", U.puf ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pus", U.pus ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pus_", U.pus_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@puv", U.puv ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pup", U.pup ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@puc", U.puc ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pus__", U.pus__ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pue", U.pue ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pup_", U.pup_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pui", U.pui ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pus___", U.pus___ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@puu", U.puu ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdo", U.pdo ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdf", U.pdf ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pds", U.pds ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pds_", U.pds_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdv", U.pdv ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdp", U.pdp ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdc", U.pdc ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pds__", U.pds__ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pde", U.pde ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdp_", U.pdp_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdi", U.pdi ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pds___", U.pds___ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pdu", U.pdu ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvo", U.pvo ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvf", U.pvf ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvs", U.pvs ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvs_", U.pvs_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvv", U.pvv ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvp", U.pvp ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvc", U.pvc ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvs__", U.pvs__ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pve", U.pve ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvp_", U.pvp_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvi", U.pvi ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvs___", U.pvs___ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvu", U.pvu ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvol", U.pvol ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvfl", U.pvfl ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvsl", U.pvsl ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvsl_", U.pvsl_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvvl", U.pvvl ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvpl", U.pvpl ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvcl", U.pvcl ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvsl__", U.pvsl__ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvel", U.pvel ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvpl_", U.pvpl_ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvil", U.pvil ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvsl___", U.pvsl___ ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@pvul", U.pvul ? 1 : 0);
        //    cmd.Parameters.AddWithValue("@id", U.id);
        //    RunQuery(cmd);
        //}

        //public List<Users> SelectAll()
        //{
        //    SqlCommand cmd = new SqlCommand("Select * from users order by id desc ", DALUtil.getConnection());
        //    return fetchEntries(cmd);
        //}

        public List<Users> SelectAllIAOA(string Option, string search, string From, string To, int? id)
        {
            SqlCommand cmd;
            if (id != null)
            {
                cmd = new SqlCommand("proc_Select_Users_By_ID", DALUtil.getConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pid", id);
            }
            else
            {
                cmd = new SqlCommand("proc_Select_Users", DALUtil.getConnection());
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
                cmd.Parameters.AddWithValue("@pemail", search);
                cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
                cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            }
            return fetchEntries(cmd);
        }

        public Users SelectById(int id)
        {
            SqlCommand cmd = cmd = new SqlCommand("proc_Select_Users_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", id);
            List<Users> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from users Where id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepUser(Users U)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateUserVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", U.id);
            cmd.Parameters.AddWithValue("@pEnable", U.Enable);
            RunQuery(cmd);
        }

        public void DelUserRequest(List<Users> U, string type)
        {
            DeleteUsersRequested_Datatable deleteUsersRequested_Datatable = new DeleteUsersRequested_Datatable();
            deleteUsersRequested_Datatable.FillDataTable(U);
            var dt = deleteUsersRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Users_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
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
                        li.password = Convert.ToString(dr["password"]);
                        li.attached_profile = Convert.ToString(dr["attached_profile"]);

                        li.Name = (Convert.ToString(dr["attached_profile"]) != null) ? new ContactDAL().SelectById(Convert.ToInt32(dr["attached_profile"]), null) : null;

                        li.Company = Convert.ToString(dr["Company"]);
                        li.Designation = Convert.ToString(dr["Designation"]);
                        li.Landline = Convert.ToString(dr["Landline"]);
                        li.Mobile = Convert.ToString(dr["Mobile"]);
                        li.ContactEmail = Convert.ToString(dr["ContactEmail"]);
                        li.Address = Convert.ToString(dr["Address"]);
                        li.AddressLandline = Convert.ToString(dr["AddressLandline"]);
                        li.City = Convert.ToInt32(dr["City"]);
                        li.CityName = Convert.ToString(dr["CityName"]);
                        li.Country = Convert.ToInt32(dr["Country"]);
                        li.CountryName = Convert.ToString(dr["CountryName"]);
                        li.BankAccountNumber = Convert.ToString(dr["BankAccountNumber"]);
                        li.status = Convert.ToString(dr["Enable"]);

                        li.Premises_id = Convert.ToInt32(dr["Premises_id"]);
                        li.PremisesName = Convert.ToString(dr["PremisesName"]);
                        li.PremisesPhone = Convert.ToString(dr["PremisesPhone"]);
                        li.PremisesCity = (dr["PremisesCity"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PremisesCity"]);

                        li.PremisesCityName = (dr["PremisesCityName"] == DBNull.Value) ? null : Convert.ToString(dr["PremisesCityName"]);
                        li.PremisesCountry = (dr["PremisesCountry"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PremisesCountry"]);
                        li.PremisesCountryName = (dr["PremisesCountryName"] == DBNull.Value) ? null : Convert.ToString(dr["PremisesCountryName"]);
                        li.PremisesAddress = (dr["PremisesAddress"] == DBNull.Value) ? null : Convert.ToString(dr["PremisesAddress"]);

                        //li.CountP = Convert.ToInt32(dr["pao"]) + Convert.ToInt32(dr["paf"]) + Convert.ToInt32(dr["pas"]) + Convert.ToInt32(dr["pas_"]) + Convert.ToInt32(dr["pav"]) + Convert.ToInt32(dr["pap"]) + Convert.ToInt32(dr["pac"]) + Convert.ToInt32(dr["pas__"]) + Convert.ToInt32(dr["pae"]) + Convert.ToInt32(dr["pap_"]) + Convert.ToInt32(dr["pai"]) + Convert.ToInt32(dr["pas___"]) + Convert.ToInt32(dr["pau"]) + Convert.ToInt32(dr["puo"]) + Convert.ToInt32(dr["puf"]) + Convert.ToInt32(dr["pus"]) + Convert.ToInt32(dr["pus_"]) + Convert.ToInt32(dr["puv"]) + Convert.ToInt32(dr["pup"]) + Convert.ToInt32(dr["puc"]) + Convert.ToInt32(dr["pus__"]) + Convert.ToInt32(dr["pue"]) + Convert.ToInt32(dr["pup_"]) + Convert.ToInt32(dr["pui"]) + Convert.ToInt32(dr["pus___"]) + Convert.ToInt32(dr["puu"]) + Convert.ToInt32(dr["pdo"]) + Convert.ToInt32(dr["pdf"]) + Convert.ToInt32(dr["pds"]) + Convert.ToInt32(dr["pds_"]) + Convert.ToInt32(dr["pdv"]) + Convert.ToInt32(dr["pdp"]) + Convert.ToInt32(dr["pdc"]) + Convert.ToInt32(dr["pds__"]) + Convert.ToInt32(dr["pde"]) + Convert.ToInt32(dr["pdp_"]) + Convert.ToInt32(dr["pdi"]) + Convert.ToInt32(dr["pds___"]) + Convert.ToInt32(dr["pdu"]) + Convert.ToInt32(dr["pvo"]) + Convert.ToInt32(dr["pvf"]) + Convert.ToInt32(dr["pvs"]) + Convert.ToInt32(dr["pvs_"]) + Convert.ToInt32(dr["pvv"]) + Convert.ToInt32(dr["pvp"]) + Convert.ToInt32(dr["pvc"]) + Convert.ToInt32(dr["pvs__"]) + Convert.ToInt32(dr["pve"]) + Convert.ToInt32(dr["pvp_"]) + Convert.ToInt32(dr["pvi"]) + Convert.ToInt32(dr["pvs___"]) + Convert.ToInt32(dr["pvu"]) + Convert.ToInt32(dr["pvol"]) + Convert.ToInt32(dr["pvfl"]) + Convert.ToInt32(dr["pvsl"]) + Convert.ToInt32(dr["pvsl_"]) + Convert.ToInt32(dr["pvvl"]) + Convert.ToInt32(dr["pvpl"]) + Convert.ToInt32(dr["pvcl"]) + Convert.ToInt32(dr["pvsl__"]) + Convert.ToInt32(dr["pvel"]) + Convert.ToInt32(dr["pvpl_"]) + Convert.ToInt32(dr["pvil"]) + Convert.ToInt32(dr["pvsl___"]) + Convert.ToInt32(dr["pvul"]);
                        //li.pao = (Convert.ToString(dr["pao"]).Equals("1")) ? true : false;
                        //li.paf = (Convert.ToString(dr["paf"]).Equals("1")) ? true : false;
                        //li.pas = (Convert.ToString(dr["pas"]).Equals("1")) ? true : false;
                        //li.pas_ = (Convert.ToString(dr["pas_"]).Equals("1")) ? true : false;
                        //li.pav = (Convert.ToString(dr["pav"]).Equals("1")) ? true : false;
                        //li.pap = (Convert.ToString(dr["pap"]).Equals("1")) ? true : false;
                        //li.pac = (Convert.ToString(dr["pac"]).Equals("1")) ? true : false;
                        //li.pas__ = (Convert.ToString(dr["pas__"]).Equals("1")) ? true : false;
                        //li.pae = (Convert.ToString(dr["pae"]).Equals("1")) ? true : false;
                        //li.pap_ = (Convert.ToString(dr["pap_"]).Equals("1")) ? true : false;
                        //li.pai = (Convert.ToString(dr["pai"]).Equals("1")) ? true : false;
                        //li.pas___ = (Convert.ToString(dr["pas___"]).Equals("1")) ? true : false;
                        //li.pau = (Convert.ToString(dr["pau"]).Equals("1")) ? true : false;
                        //li.puo = (Convert.ToString(dr["puo"]).Equals("1")) ? true : false;
                        //li.puf = (Convert.ToString(dr["puf"]).Equals("1")) ? true : false;
                        //li.pus = (Convert.ToString(dr["pus"]).Equals("1")) ? true : false;
                        //li.pus_ = (Convert.ToString(dr["pus_"]).Equals("1")) ? true : false;
                        //li.puv = (Convert.ToString(dr["puv"]).Equals("1")) ? true : false;
                        //li.pup = (Convert.ToString(dr["pup"]).Equals("1")) ? true : false;
                        //li.puc = (Convert.ToString(dr["puc"]).Equals("1")) ? true : false;
                        //li.pus__ = (Convert.ToString(dr["pus__"]).Equals("1")) ? true : false;
                        //li.pue = (Convert.ToString(dr["pue"]).Equals("1")) ? true : false;
                        //li.pup_ = (Convert.ToString(dr["pup_"]).Equals("1")) ? true : false;
                        //li.pui = (Convert.ToString(dr["pui"]).Equals("1")) ? true : false;
                        //li.pus___ = (Convert.ToString(dr["pus___"]).Equals("1")) ? true : false;
                        //li.puu = (Convert.ToString(dr["puu"]).Equals("1")) ? true : false;
                        //li.pdo = (Convert.ToString(dr["pdo"]).Equals("1")) ? true : false;
                        //li.pdf = (Convert.ToString(dr["pdf"]).Equals("1")) ? true : false;
                        //li.pds = (Convert.ToString(dr["pds"]).Equals("1")) ? true : false;
                        //li.pds_ = (Convert.ToString(dr["pds_"]).Equals("1")) ? true : false;
                        //li.pdv = (Convert.ToString(dr["pdv"]).Equals("1")) ? true : false;
                        //li.pdp = (Convert.ToString(dr["pdp"]).Equals("1")) ? true : false;
                        //li.pdc = (Convert.ToString(dr["pdc"]).Equals("1")) ? true : false;
                        //li.pds__ = (Convert.ToString(dr["pds__"]).Equals("1")) ? true : false;
                        //li.pde = (Convert.ToString(dr["pde"]).Equals("1")) ? true : false;
                        //li.pdp_ = (Convert.ToString(dr["pdp_"]).Equals("1")) ? true : false;
                        //li.pdi = (Convert.ToString(dr["pdi"]).Equals("1")) ? true : false;
                        //li.pds___ = (Convert.ToString(dr["pds___"]).Equals("1")) ? true : false;
                        //li.pdu = (Convert.ToString(dr["pdu"]).Equals("1")) ? true : false;
                        //li.pvo = (Convert.ToString(dr["pvo"]).Equals("1")) ? true : false;
                        //li.pvf = (Convert.ToString(dr["pvf"]).Equals("1")) ? true : false;
                        //li.pvs = (Convert.ToString(dr["pvs"]).Equals("1")) ? true : false;
                        //li.pvs_ = (Convert.ToString(dr["pvs_"]).Equals("1")) ? true : false;
                        //li.pvv = (Convert.ToString(dr["pvv"]).Equals("1")) ? true : false;
                        //li.pvp = (Convert.ToString(dr["pvp"]).Equals("1")) ? true : false;
                        //li.pvc = (Convert.ToString(dr["pvc"]).Equals("1")) ? true : false;
                        //li.pvs__ = (Convert.ToString(dr["pvs__"]).Equals("1")) ? true : false;
                        //li.pve = (Convert.ToString(dr["pve"]).Equals("1")) ? true : false;
                        //li.pvp_ = (Convert.ToString(dr["pvp_"]).Equals("1")) ? true : false;
                        //li.pvi = (Convert.ToString(dr["pvi"]).Equals("1")) ? true : false;
                        //li.pvs___ = (Convert.ToString(dr["pvs___"]).Equals("1")) ? true : false;
                        //li.pvu = (Convert.ToString(dr["pvu"]).Equals("1")) ? true : false;
                        //li.pvol = (Convert.ToString(dr["pvol"]).Equals("1")) ? true : false;
                        //li.pvfl = (Convert.ToString(dr["pvfl"]).Equals("1")) ? true : false;
                        //li.pvsl = (Convert.ToString(dr["pvsl"]).Equals("1")) ? true : false;
                        //li.pvsl_ = (Convert.ToString(dr["pvsl_"]).Equals("1")) ? true : false;
                        //li.pvvl = (Convert.ToString(dr["pvvl"]).Equals("1")) ? true : false;
                        //li.pvpl = (Convert.ToString(dr["pvpl"]).Equals("1")) ? true : false;
                        //li.pvcl = (Convert.ToString(dr["pvcl"]).Equals("1")) ? true : false;
                        //li.pvsl__ = (Convert.ToString(dr["pvsl__"]).Equals("1")) ? true : false;
                        //li.pvel = (Convert.ToString(dr["pvel"]).Equals("1")) ? true : false;
                        //li.pvpl_ = (Convert.ToString(dr["pvpl_"]).Equals("1")) ? true : false;
                        //li.pvil = (Convert.ToString(dr["pvil"]).Equals("1")) ? true : false;
                        //li.pvsl___ = (Convert.ToString(dr["pvsl___"]).Equals("1")) ? true : false;
                        //li.pvul = (Convert.ToString(dr["pvul"]).Equals("1")) ? true : false;
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        users.Add(li);
                    }
                    users.TrimExcess();
                }
            }
            return users;
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
