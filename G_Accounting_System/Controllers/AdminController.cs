using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G_Accounting_System.Models;
using System.Web.Script.Serialization;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using Newtonsoft.Json;
using System.Web.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using G_Accounting_System.Code.Helpers;

namespace G_Accounting_System.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("api/Admin/Login")]
        [Authorize]
        public ActionResult Login(Login model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //string encryptedPassword = PasswordHelper.EncryptPassword(model.password);
                    Users loginuser = new Users();
                    loginuser.email = model.email;
                    //loginuser.password = encryptedPassword;
                    loginuser.password = model.password;
                    List<UserPrivilegess> myList = new List<UserPrivilegess>();
                    List<string> mystringList = new List<string>();
                    Users users = new Catalog().Login(loginuser);
                    if (users != null)
                    {
                        User user = new User();
                        user.id = users.id;
                        user.email = users.email;
                        user.password = loginuser.password;
                        user.attached_profile = users.Name.Name;
                        FormsAuthentication.SetAuthCookie(user.email, false);
                        var authTicket = new FormsAuthenticationTicket(1, user.email, DateTime.Now, DateTime.Now.AddMinutes(1440), false, user.email);
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Response.Cookies.Add(authCookie);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
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

                                List<UserPrivilegess> UserPrivileges = new Catalog().UserPrivilegess(user.id);
                                List<UserPrivileges> up = new List<UserPrivileges>();
                                foreach (var dbr in UserPrivileges)
                                {
                                    UserPrivileges li = new UserPrivileges();
                                    li.id = dbr.id;
                                    li.priv_ID = dbr.priv_ID;
                                    li.PrivName = dbr.PrivName;
                                    li.Add = dbr.Add;
                                    li.Edit = dbr.Edit;
                                    li.Delete = dbr.Delete;
                                    li.View = dbr.View;
                                    li.Profile = dbr.Profile;
                                    li.ControllerName = dbr.ControllerName;
                                    up.Add(li);
                                }
                                



                                Session["MenuList"] = up;
                                Session["rolebase"] = mystringList;
                                Session["Role"] = users.Role;
                                Session["UserId"] = users.id;
                                Session["Premises_id"] = users.Premises_id;
                                Session.Timeout = 1440;

                                //string token = createToken(model.email);
                                //string token = createToken(loginuser.email);
                                //int User_id = Convert.ToInt32(Session["UserId"]);
                                //List<Messages> messages = new Catalog().MessagesNotifications(User_id);
                                //List<Message> message = new List<Message>();

                                //if (messages != null)
                                //{
                                //    foreach (var dbr in messages)
                                //    {
                                //        Message li = new Message();
                                //        li.id = dbr.id;
                                //        li.Sender_id = dbr.Sender_id;
                                //        li.SenderName = dbr.SenderName.Name;
                                //        li.Receiver_id = dbr.Receiver_id;
                                //        li.ReceiverName = dbr.ReceiverName.Name;
                                //        li.strMessage = dbr.strMessage;
                                //        li.Date = dbr.Date;
                                //        li.Time = dbr.Time;
                                //        li.Month = dbr.Month;
                                //        li.Year = dbr.Year;

                                //        message.Add(li);
                                //        message.TrimExcess();
                                //        Session["MessagesNotifications"] = message;
                                //    }

                                //    //for (int i = 0; i < message.Count; i++)
                                //    //{
                                //    //    int z = message[i].priv_ID;
                                //    //    mystringList.Add(z.ToString());
                                //    //}
                                //}
                                //else
                                //{
                                //    //Session["Messages"] = null;
                                //}
                                return RedirectToAction("Index", "Dashboard");
                            }
                            else
                            {
                                Session["rolebase"] = null;
                                Session["UserId"] = users.id;
                                Session["Role_id"] = users.Role_id;
                                Session["Role"] = users.Role;
                                Session.Timeout = 1440;
                                return RedirectToAction("Index", "Dashboard");
                            }
                        }
                    }
                }

                return View(model);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public JsonResult getLoggedInUser_id()
        {
            try
            {
                User user = new User();
                user.id = Convert.ToInt32(Session["UserId"]);

                return Json(user, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session["UserId"] = null;

            return RedirectToAction("Login");
        }

        [SessionExpireFilterAttribute]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        [SessionExpireFilterAttribute]
        public JsonResult ChangePasswordUpdate(string ChangePasswordData)
        {
            try
            {
                var js = new JavaScriptSerializer();
                User user = js.Deserialize<User>(ChangePasswordData);

                Users users = new Users();
                users.id = Convert.ToInt32(Session["UserId"]);
                users.CurrentPassword = user.CurrentPassword;
                users.NewPassword = user.NewPassword;

                new Catalog().ChangePasswordUpdate(users);
                user.pFlag = users.pFlag;
                user.pDesc = users.pDesc;

                return Json(user, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public JsonResult MessagesNotifications()
        {
            int User_id = Convert.ToInt32(Session["UserId"]);
            List<Messages> messages = new Catalog().MessagesNotifications(User_id);
            List<Message> message = new List<Message>();

            if (messages != null)
            {
                foreach (var dbr in messages)
                {
                    Message li = new Message();
                    li.id = dbr.id;
                    li.Sender_id = dbr.Sender_id;
                    li.SenderName = dbr.SenderName.Name;
                    li.Receiver_id = dbr.Receiver_id;
                    if (dbr.Receiver_id != 0)
                    {
                        li.ReceiverName = dbr.ReceiverName.Name;
                    }
                    li.strMessage = dbr.strMessage;
                    li.Date = dbr.Date;
                    li.Time = dbr.Time;
                    li.Month = dbr.Month;
                    li.Year = dbr.Year;

                    message.Add(li);
                    message.TrimExcess();
                    Session["MessagesNotifications"] = message;
                }

                //for (int i = 0; i < message.Count; i++)
                //{
                //    int z = message[i].priv_ID;
                //    mystringList.Add(z.ToString());
                //}
            }
            else
            {
                //Session["Messages"] = null;
            }
            return Json(new { Messages = message }, JsonRequestBehavior.AllowGet);
        }


        private string createToken(string email)
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
                accesstokens.User_id = Convert.ToInt32(Session["UserId"].ToString());
                accesstokens.AccessToken = tokenString + accesstokens.User_id;
                accesstokens.IssueDate = issuedAt.ToString();
                accesstokens.ExpiryDate = expires.ToString();
                new Catalog().UpdateToken(accesstokens);
            }

            return tokenString;





        }

    }
}