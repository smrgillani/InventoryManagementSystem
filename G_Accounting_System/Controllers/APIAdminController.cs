using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace G_Accounting_System.Controllers
{
    public class APIAdminController : ApiController
    {
        [Route("api/APIAdmin/Login")]
        [HttpPost]
        public object Login()
        {
            User login = null;
            User user = null;
            List<UserPrivilegess> myList = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                    {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    login = js.Deserialize<User>(strJson);

                    Users loginuser = new Users();
                    loginuser.email = login.email;
                    loginuser.password = login.password;

                    myList = new List<UserPrivilegess>();
                    List<string> mystringList = new List<string>();
                    Users users = new Catalog().Login(loginuser);

                    if (users != null)
                    {
                        user = new User();
                        user.id = users.id;
                        user.email = users.email;
                        user.password = loginuser.password;
                        user.attached_profile = users.Name.Name;

                        //myList = new Catalog().UserPrivileges(loginuser.email, encryptedPassword);
                        myList = new Catalog().UserPrivileges(loginuser.email, loginuser.password);
                        if (myList != null)
                        {
                            int ListTotal = myList.Count;

                            for (int i = 0; i < ListTotal; i++)
                            {
                                int z = myList[i].priv_ID;
                                mystringList.Add(z.ToString());
                            }
                        }
                        var User_id = HttpContext.Current.User.Identity.Name;

                        string token = createToken(user.email, user.id);

                        user.Token = token+ user.id;
                        user.pFlag = "1";
                        user.pDesc = "Login Successfull";
                    }
                    else
                    {
                        user = new User();
                        user.pFlag = "0";
                        user.pDesc = "Invalid Credentials";
                    }

                }
                return new { user, myList };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/APIAdmin/ChangePassword")]
        [HttpPost]
        public User ChangePassword()
        {
            User user = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    user = js.Deserialize<User>(strJson);
                    Users users = new Users();
                    var User_id = HttpContext.Current.User.Identity.Name;
                    users.id = Convert.ToInt32(User_id); ;
                    users.CurrentPassword = user.CurrentPassword;
                    users.NewPassword = user.NewPassword;

                    new Catalog().ChangePasswordUpdate(users);
                    user.pFlag = users.pFlag;
                    user.pDesc = users.pDesc;
                }
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIAdmin/UserSignup")]
        [HttpPost]
        public User UserSignup()
        {
            User user = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    user = js.Deserialize<User>(strJson);

                    Users users = new Users();

                    Contacts employee = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 0, 1, 1);
                    Contacts customer = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 1, 0, 1);
                    Contacts vendor = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 1, 0, 0, 1);

                    users.id = user.id;
                    users.email = user.email;
                    users.attached_profile = user.attached_profile;
                    //var encryptedPassword = PasswordHelper.EncryptPassword(user.password);
                    users.password = user.password;
                    users.status = user.status.Equals("1") ? "1" : "0";
                    users.Premises_id = user.Premises_id;
                    users.Role_id = new Catalog().GetRoleByRoleName("Admin").id;
                    users.pao = true;
                    users.paf = true;
                    users.pas = true;
                    users.pas_ = true;
                    users.pav = true;
                    users.pap = true;
                    users.pac = true;
                    users.pas__ = true;
                    users.pae = true;
                    users.pap_ = true;
                    users.pai = true;
                    users.pas___ = true;
                    users.pau = true;
                    users.puo = true;
                    users.puf = true;
                    users.pus = true;
                    users.pus_ = true;
                    users.puv = true;
                    users.pup = true;
                    users.puc = true;
                    users.pus__ = true;
                    users.pue = true;
                    users.pup_ = true;
                    users.pui = true;
                    users.pus___ = true;
                    users.puu = true;
                    users.pdo = true;
                    users.pdf = true;
                    users.pds = true;
                    users.pds_ = true;
                    users.pdv = true;
                    users.pdp = true;
                    users.pdc = true;
                    users.pds__ = true;
                    users.pde = true;
                    users.pdp_ = true;
                    users.pdi = true;
                    users.pds___ = true;
                    users.pdu = true;
                    users.pvo = true;
                    users.pvf = true;
                    users.pvs = true;
                    users.pvs_ = true;
                    users.pvv = true;
                    users.pvp = true;
                    users.pvc = true;
                    users.pvs__ = true;
                    users.pve = true;
                    users.pvp_ = true;
                    users.pvi = true;
                    users.pvs___ = true;
                    users.pvu = true;
                    users.pvol = true;
                    users.pvfl = true;
                    users.pvsl = true;
                    users.pvsl_ = true;
                    users.pvvl = true;
                    users.pvpl = true;
                    users.pvcl = true;
                    users.pvsl__ = true;
                    users.pvel = true;
                    users.pvpl_ = true;
                    users.pvil = true;
                    users.pvsl___ = true;
                    users.pvul = true;

                    new Catalog().InsertUpdateUser(users);
                    user.pFlag = users.pFlag;
                    user.pDesc = users.pDesc;
                    user.pUserid_Out = users.pUserid_Out;
                }

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string createToken(string email, int User_id)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:50191", audience: "http://localhost:50191",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            if (token != null)
            {
                Tokens accesstokens = new Tokens();
                accesstokens.User_id = Convert.ToInt32(User_id);
                
                accesstokens.AccessToken = tokenString + accesstokens.User_id;
                accesstokens.IssueDate = issuedAt.ToString();
                accesstokens.ExpiryDate = expires.ToString();
                new Catalog().UpdateToken(accesstokens);
            }

            return tokenString;





        }
    }
}
