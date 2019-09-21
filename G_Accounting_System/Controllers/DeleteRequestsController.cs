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
    public class DeleteRequestsController : Controller
    {
        // GET: DeleteRequests
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            List<DeleteRequests> req = new Catalog().SelectAllDeleteRequests();

            List<DeleteRequest> delreq = new List<DeleteRequest>();

            if (req != null)
            {
                foreach (var dbr in req)
                {
                    DeleteRequest li = new DeleteRequest();
                    li.id = dbr.id;
                    li.Type = dbr.Type;
                    li.Count = dbr.Count;
                    delreq.Add(li);
                    Session["delreq"] = delreq;
                    delreq.TrimExcess();
                }
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectAll(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search.ToString());
            string type = search.type.ToString();
            List<DeleteRequests> requests = new Catalog().AllItemsDelRequest(type);

            List<DeleteRequest> request = new List<DeleteRequest>();

            if (requests != null)
            {
                foreach (var dbr in requests)
                {
                    DeleteRequest li = new DeleteRequest();
                    li.id = dbr.id;
                    li.Name = dbr.Name;
                    li.DeleteRequestedBy = dbr.DeleteRequestedBy;

                    request.Add(li);
                }
            }
            request.TrimExcess();
            return Json(new { draw = search.Draw, recordsTotal = request.Count, recordsFiltered = request.Count, data = request }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int id, string type)
        {
            string response = "";
            try
            {
                new Catalog().Delete(id, type);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}