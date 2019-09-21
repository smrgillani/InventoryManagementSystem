using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using G_Accounting_System.ENT;
using G_Accounting_System.APP;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Security.Claims;
using System.Web.Mvc;
using G_Accounting_System.Models;

namespace G_Accounting_System.Auth
{
    public class PermissionsAuthorize : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            Users user = new Users();
            bool ValidPermission = false;
            user.id = user.id;
            user.email = user.email;
            List<UserPrivileges> _MenuModelList = new List<UserPrivileges>();
            _MenuModelList = (List<UserPrivileges>)System.Web.HttpContext.Current.Session["MenuList"];
            string a = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToString();
            if (_MenuModelList.Where(x => x.ControllerName.Equals(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName)).Count() > 0 && _MenuModelList.Where(x => x.ControllerName.Equals(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName)) != null)
            {
                var pagePermissions = _MenuModelList.Where(x => x.ControllerName.Equals(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName)).FirstOrDefault();
                UserPrivileges permissions = new UserPrivileges();
                permissions.Add = pagePermissions.Add;
                permissions.Edit = pagePermissions.Edit;
                permissions.Delete = pagePermissions.Delete;
                permissions.View = pagePermissions.View;
                permissions.Profile = pagePermissions.Profile;
                filterContext.HttpContext.Session.Add("UserPermissions", permissions);
                base.OnActionExecuting(filterContext);
                // if (filterContext.ActionDescriptor.ActionName == "Add" && permissions.Add == 1)
                //{
                //    ValidPermission = true;
                // }
                //if (filterContext.ActionDescriptor.ActionName == "Update" && permissions.Edit == 1)
                //{
                //    ValidPermission = true;
                //}
                //if (filterContext.ActionDescriptor.ActionName == "Delete" && permissions.Delete == 1)
                //{
                //    ValidPermission = true;
                //}
                if ((filterContext.ActionDescriptor.ActionName == "Index" || filterContext.ActionDescriptor.ActionName == "index") && permissions.View == 1)
                {
                    ValidPermission = true;
                }
                else if ((filterContext.ActionDescriptor.ActionName == "Items" || filterContext.ActionDescriptor.ActionName == "items") && permissions.View == 1)
                {
                    ValidPermission = true;
                }
                else if ((filterContext.ActionDescriptor.ActionName == "Profile" || filterContext.ActionDescriptor.ActionName == "profile") && permissions.Profile == 1)
                {
                    ValidPermission = true;
                }
                else if ((filterContext.ActionDescriptor.ActionName == "Invoice" || filterContext.ActionDescriptor.ActionName == "invoice") && permissions.Profile == 1)
                {
                    ValidPermission = true;
                }
                else if (filterContext.ActionDescriptor.ActionName == "SOInvoice" && permissions.Profile == 1)
                {
                    ValidPermission = true;
                }
                //else if (filterContext.ActionDescriptor.ActionName == "Profile" && permissions.View == 1)
                //{
                //    ValidPermission = true;
                //}
                else
                {
                    filterContext.Result = new RedirectResult("~/Dashboard/Index");
                    ValidPermission = false;
                }
            }
            else
            {
                if (filterContext.ActionDescriptor.ActionName != "Dashboard" || filterContext.ActionDescriptor.ActionName == null)
                {
                    ValidPermission = false;
                    filterContext.Result = new RedirectResult("~/Dashboard/Index");
                }
            }

        }
    }
}