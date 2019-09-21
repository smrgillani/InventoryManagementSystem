using System.Collections.Generic;
using System.Linq;
using G_Accounting_System.ENT;
using System.Web.Mvc;
using G_Accounting_System.Models;
using System.Web;

namespace G_Accounting_System.Auth
{
    public class PermissionsClass
    {
        public bool CheckAddPermission()
        {
            Users user = new Users();
            bool ValidPermission = false;
            user.id = user.id;
            user.email = user.email;
            List<UserPrivileges> _MenuModelList = new List<UserPrivileges>();

            _MenuModelList = (List<UserPrivileges>)System.Web.HttpContext.Current.Session["MenuList"];
            if (_MenuModelList.Where(x => x.ControllerName.Equals(HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString())).Count() > 0)
            {
                var pagePermissions = _MenuModelList.Where(x => x.ControllerName.Equals(HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString())).FirstOrDefault();
                UserPrivileges permissions = new UserPrivileges();
                permissions.Add = pagePermissions.Add;
                if (permissions.Add == 1)
                {
                    ValidPermission = true;
                }
                else
                {
                    ValidPermission = false;
                }
            }
            else
            {
                if (HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString() != "Dashboard")
                {
                    ValidPermission = false;
                }
            }
            return ValidPermission;
        }

        public bool CheckEditPermission()
        {
            Users user = new Users();
            bool ValidPermission = false;
            user.id = user.id;
            user.email = user.email;
            List<UserPrivileges> _MenuModelList = new List<UserPrivileges>();

            _MenuModelList = (List<UserPrivileges>)System.Web.HttpContext.Current.Session["MenuList"];
            if (_MenuModelList.Where(x => x.ControllerName.Equals(HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString())).Count() > 0)
            {
                var pagePermissions = _MenuModelList.Where(x => x.ControllerName.Equals(HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString())).FirstOrDefault();
                UserPrivileges permissions = new UserPrivileges();
                permissions.Edit = pagePermissions.Edit;
                if (permissions.Edit == 1)
                {
                    ValidPermission = true;
                }
                else
                {
                    ValidPermission = false;
                }
            }
            else
            {
                if (HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString() != "Dashboard")
                {
                    ValidPermission = false;
                }
            }
            return ValidPermission;
        }
    }
}