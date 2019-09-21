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
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Net;
using System.Data;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using G_Accounting_System.Code.Helpers;
using System.Text;
using G_Accounting_System.Auth;
using Activity = G_Accounting_System.Models.Activity;

namespace G_Accounting_System.Controllers
{
    //[RoutePrefix("api/Brand/")]
    [SessionExpireFilterAttribute]


    public class BrandController : Controller
    {
        // GET: Brand
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            bool check = new PermissionsClass().CheckAddPermission();
            if (check != false)
            {
                return View("Brand");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllBrands(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Brands> brand = new Catalog().AllBrands(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Brand> brands = new List<Brand>();

            if (brand != null)
            {
                foreach (var dbr in brand)
                {
                    Brand li = new Brand();
                    li.id = dbr.id;
                    li.Brand_Name = dbr.Brand_Name;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    brands.Add(li);
                }
            }

            brands.TrimExcess();
            var fbrands = brands.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = brands.Count, recordsFiltered = brands.Count, data = fbrands }, JsonRequestBehavior.AllowGet);
        }

        [PermissionsAuthorize]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(int id)
        {
            Brands brands = new Catalog().SelectBrand(Convert.ToInt32(id));

            Brand brand = new Brand();
            brand.id = brands.id;
            brand.Brand_Name = brands.Brand_Name;
            brand.Enable = brands.Enable;
            brand.IsEnabled_ = (brands.Enable == 1) ? "Active" : "InActive";
            brand.Delete_Status = brands.Delete_Status;

            return Json(brand, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateBrands(string BrandData)
        {
            var js = new JavaScriptSerializer();
            Brand brand = null;
            string ActivityName = null;
            string response = null;
            try
            {
                brand = js.Deserialize<Brand>(BrandData);

                Brands AddBrand = new Brands();
                AddBrand.id = brand.id;
                AddBrand.Brand_Name = brand.Brand_Name;
                if (brand.id == 0)
                {
                    AddBrand.AddedBy = Convert.ToInt32(Session["UserId"]);
                    AddBrand.UpdatedBy = 0;
                }
                else
                {
                    AddBrand.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                    AddBrand.AddedBy = 0;
                }
                if (Session["UserId"] != null)
                {
                    new Catalog().InsertUpdateBrands(AddBrand);
                    brand.pFlag = AddBrand.pFlag;
                    brand.pDesc = AddBrand.pDesc;
                    brand.pBrandid_Out = AddBrand.pBrandid_Out;
                    if (brand.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (brand.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(brand.pBrandid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = brand.id;
                        }
                        activity.ActivityType = "Brand";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(Session["UserId"]);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                else
                {
                    response = "Invalid Request";
                }
                return Json(brand, response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            Brand brand = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    Brands brands = new Catalog().SelectBrand(id);

                    brand = new Brand();

                    if (brands != null)
                    {

                        brand.id = brands.id;
                        brand.Brand_Name = brands.Brand_Name;
                    }
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(brand, JsonRequestBehavior.DenyGet);
            }
            catch (Exception e)
            {
                return Json(brand, JsonRequestBehavior.DenyGet);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Brand br = null;
            try
            {
                Brands brands = new Catalog().SelectBrand(id);
                br = new Brand();
                if (brands == null)
                {
                    response = "Please Select A Valid Brand";
                }
                else
                {
                    Brands brand = new Brands();
                    brand.id = id;

                    if (brands.Enable == 1)
                    {
                        brand.Enable = 0;
                    }
                    else
                    {
                        brand.Enable = 1;
                    }

                    new Catalog().UpdatepBrand(brand);
                    br.Enable = brand.Enable;
                    Activity activity = new Activity();
                    if (br.Enable == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (br.Enable == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Brand";
                    activity.ActivityName = ActivityName;
                    activity.User_id = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
            }
            catch (Exception e)
            {
                response = e.Message;
            }

            return Json(br, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Del(int id)
        //{
        //    string response = "";
        //    try
        //    {
        //        new Catalog().DelBrand(id);
        //    }
        //    catch (Exception e)
        //    {
        //        response = "Internal Server Error.";
        //    }

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelBrands(string DeleteBrandData)
        {
            string response = "";
            List<Brands> brands = null;
            List<Brand> brandsNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Brand> brandsrequestedtoDelete = js.Deserialize<List<Brand>>(DeleteBrandData);
                brands = new List<Brands>();
                activities = new List<Activity>();
                brandsNotDelete = new List<Brand>();

                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in brandsrequestedtoDelete)
                {
                    Brands li = new Brands();
                    Brands liChecked = new Brands();
                    li.id = dbr.id;

                    li.Brand_Name = dbr.Brand_Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

                    liChecked = new Catalog().CheckBrandForDelete(li.id);
                    if (liChecked != null)
                    {
                        Brand br = new Brand();
                        br.Brand_Name = liChecked.Brand_Name;
                        brandsNotDelete.Add(br);
                    }
                    else
                    {
                        brands.Add(li);
                    }
                }
                brands.TrimExcess();
                brandsNotDelete.TrimExcess();
                if (brands.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(brands);

                    StringBuilder fileContent = new StringBuilder();

                    foreach (var col in dt.Columns)
                    {
                        if (col.ToString() == "Brand_Name")
                        {
                            fileContent.Append(col.ToString() + ",");
                        }
                    }
                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

                    foreach (DataRow dr in dt.Rows)
                    {
                        //foreach (var column in dr.ItemArray.ElementAtOrDefault(1))
                        //{

                        fileContent.Append("\"" + dr.ItemArray.ElementAtOrDefault(1).ToString() + "\",");

                        //}
                        fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                    }
                    string filename = "Brand-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Brands/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Brands/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";


                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");



                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Brands";
                    mailMessage.Body = "Following Brands are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Brands";
                    string Result = new Catalog().DelBrandRequest(brands, type);

                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (brands != null)
                        {
                            foreach (var i in brands)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Brand";
                                activity.ActivityName = "Delete Requested";
                                activity.User_id = Convert.ToInt32(Session["UserId"]);
                                activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                                activities.Add(activity);
                            }

                            new ActivitiesClass().InsertActivity(activities);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response = e.ToString();
            }
            return Json(new { Response = response, Brands = brands, BrandsNotDelete = brandsNotDelete }, JsonRequestBehavior.AllowGet);
        }
    }
}