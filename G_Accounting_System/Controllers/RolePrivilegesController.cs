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
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class RolePrivilegesController : Controller
    {
        // GET: RolePrivileges
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult getAllRolePrivileges(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<RolePrivileges> rp = new Catalog().RolePrivileges(search.Option.ToString());

            List<RolePrivilege> rolepriv = new List<RolePrivilege>();

            if (rp != null)
            {
                foreach (var dbr in rp)
                {
                    RolePrivilege li = new RolePrivilege();
                    li.Role_Priv_id = dbr.Role_Priv_id;
                    li.Priv_id = dbr.Priv_id;
                    li.Priv_Name = dbr.Priv_Name;
                    li.Check_Status = dbr.Check_Status;
                    rolepriv.Add(li);
                }
            }

            rolepriv.TrimExcess();

            return Json(new { draw = search.Draw, recordsTotal = rolepriv.Count, recordsFiltered = rolepriv.Count, data = rolepriv }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateRolePrivileges(string RolePrivilegesData)
        {
            var js = new JavaScriptSerializer();
            List<RolePrivilege> rolepriv = js.Deserialize<List<RolePrivilege>>(RolePrivilegesData);

            List<RolePrivileges> rp = new List<RolePrivileges>();

            string response = "";

            try
            {
                if (rolepriv.Count() < 0)
                {
                    response = "Please Select Privilege.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in rolepriv)
                    {
                        RolePrivileges li = new RolePrivileges();

                        li.Role_id = dbr.Role_id;
                        li.Role_Priv_id = dbr.Role_Priv_id;
                        li.Priv_id = dbr.Priv_id;
                        li.Priv_Name = dbr.Priv_Name;
                        li.Enable = dbr.Enable;
                        li.Check_Status = dbr.Check_Status;
                        rp.Add(li);

                        i++;
                    }

                    rp.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateRolePrivileges(rp);

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