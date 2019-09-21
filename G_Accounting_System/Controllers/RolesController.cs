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
    public class RolesController : Controller
    {
        // GET: Roles
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllRoles(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Roles> roles = new Catalog().AllRoles();

            List<Role> role = new List<Role>();

            if (roles != null)
            {
                foreach (var dbr in roles)
                {
                    Role li = new Role();
                    li.id = dbr.id;
                    li.Role_Name = dbr.Role_Name;
                    li.Enable = dbr.Role_Name;
                    role.Add(li);
                }
            }

            role.TrimExcess();
            var prole = role.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = role.Count, recordsFiltered = role.Count, data = prole }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateRoles(string RoleData)
        {
            var js = new JavaScriptSerializer();
            Role role = js.Deserialize<Role>(RoleData);

            Roles AddRole = new Roles();
            AddRole.Role_Name = role.Role_Name;
            AddRole.id = role.id;
            if (role.id == 0)
            {
                AddRole.AddedBy = Convert.ToInt32(Session["UserId"]);
                AddRole.UpdatedBy = 0;
            }
            else
            {
                AddRole.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                AddRole.AddedBy = 0;
            }

            new Catalog().InsertUpdateRoles(AddRole);
            role.pFlag = AddRole.pFlag;
            role.pDesc = AddRole.pDesc;

            return Json(role, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            Roles roles = new Catalog().SelectRole(id);

            Role role = new Role();

            if (roles != null)
            {

                role.id = roles.id;
                role.Role_Name = roles.Role_Name;
            }

            return Json(role, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelRole(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}