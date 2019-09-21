using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace G_Accounting_System.Controllers
{
    public class UserPreviligesController : Controller
    {
        // GET: UserPreviliges
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult getAllUserPrivileges(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<UserPrivilegess> up = new Catalog().UserPrivilegess(Convert.ToInt32(search.Option));

            List<UserPrivileges> userpriv = new List<UserPrivileges>();

            if (up != null)
            {
                foreach (var dbr in up)
                {
                    UserPrivileges li = new UserPrivileges();
                    li.id = dbr.id;
                    li.priv_ID = dbr.priv_ID;
                    li.PrivName = dbr.PrivName;
                    li.Add = dbr.Add;
                    li.Edit = dbr.Edit;
                    //li.Delete = dbr.Delete;
                    li.View = dbr.View;
                    li.Profile = dbr.Profile;
                    userpriv.Add(li);
                }
            }

            userpriv.TrimExcess();

            return Json(new { draw = search.Draw, recordsTotal = userpriv.Count, recordsFiltered = userpriv.Count, data = userpriv }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateUserPrivileges(string UserPrivilegesData)
        {
            var js = new JavaScriptSerializer();
            List<UserPrivileges> userpriv = js.Deserialize<List<UserPrivileges>>(UserPrivilegesData);

            List<UserPrivilegess> up = new List<UserPrivilegess>();

            string response = "";

            try
            {
                if (userpriv.Count() < 0)
                {
                    response = "Please Select Privilege.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in userpriv)
                    {
                        UserPrivilegess li = new UserPrivilegess();
                        li.id = dbr.id;
                        li.priv_ID = dbr.priv_ID;
                        li.User_id = dbr.User_id;
                        li.Add = dbr.Add;
                        li.Edit = dbr.Edit;
                        //li.Delete = dbr.Delete;
                        li.View = dbr.View;
                        li.Profile = dbr.Profile;
                        up.Add(li);

                        i++;
                    }

                    up.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateUserPrivileges(up);

                        if (Result.Length > 0)
                        {
                            response = "Internal Server Error.";
                        }

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}