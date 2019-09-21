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
using System.Data;
using System.Text;
using G_Accounting_System.Code.Helpers;
using System.IO;
using System.Net.Mail;
using System.Net;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class CategoryController : Controller
    {
        // GET: Category
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
                return View("Category");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllCatgeories(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Categories> category = new Catalog().AllCategories(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Category> categories = new List<Category>();

            if (category != null)
            {
                foreach (var dbr in category)
                {
                    Category li = new Category();
                    li.id = dbr.id;
                    li.Category_Name = dbr.Category_Name;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    categories.Add(li);
                }
            }

            categories.TrimExcess();
            var pcategories = categories.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = categories.Count, recordsFiltered = categories.Count, data = pcategories }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateCategory(string CategoryData)
        {
            var js = new JavaScriptSerializer();
            Category category = null;
            string ActivityName = null;
            try
            {
                category = js.Deserialize<Category>(CategoryData);

                Categories AddCategory = new Categories();
                AddCategory.id = category.id;
                AddCategory.Category_Name = category.Category_Name;
                AddCategory.Enable = 1;
                if (category.id == 0)
                {
                    AddCategory.AddedBy = Convert.ToInt32(Session["UserId"]);
                    AddCategory.UpdatedBy = 0;
                }
                else
                {
                    AddCategory.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                    AddCategory.AddedBy = 0;
                }

                new Catalog().InsertUpdateCategory(AddCategory);
                category.pFlag = AddCategory.pFlag;
                category.pDesc = AddCategory.pDesc;
                category.pCategoryid_Out = AddCategory.pCategoryid_Out;
                if (category.pFlag == "1")
                {
                    Activity activity = new Activity();
                    if (category.id == 0)
                    {
                        ActivityName = "Created";
                        activity.ActivityType_id = Convert.ToInt32(category.pCategoryid_Out);
                    }
                    else
                    {
                        ActivityName = "Updated";
                        activity.ActivityType_id = category.id;
                    }
                    activity.ActivityType = "Category";
                    activity.ActivityName = ActivityName;
                    activity.User_id = Convert.ToInt32(Session["UserId"]);
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);


                    new ActivitiesClass().InsertActivity(activities);
                }
            }
            catch (Exception e)
            {
                category = null;
            }
            return Json(category, JsonRequestBehavior.AllowGet);
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
            Categories categories = new Catalog().SelectCategory(Convert.ToInt32(id));

            Category category = new Category();
            category.id = categories.id;
            category.Category_Name = categories.Category_Name;
            category.Enable = categories.Enable;
            category.IsEnabled_ = (categories.Enable == 1) ? "Active" : "InActive";
            category.Delete_Status = categories.Delete_Status;
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            Category categories = null;
            try
            {
                bool check = new PermissionsClass().CheckEditPermission();
                if (check != false)
                {
                    categories = new Category();
                    Categories category = new Catalog().SelectCategory(id);
                    categories.id = category.id;
                    categories.Category_Name = category.Category_Name;
                    categories.Enable = category.Enable;
                    categories.IsEnabled_ = (category.Enable == 1) ? "Active" : "InActive";
                    categories.AddedBy = category.AddedBy;
                    categories.Time = category.Time;
                    categories.Date = category.Date;
                    categories.Month = category.Month;
                    categories.Year = category.Year;
                }
                else
                {
                    return Json(check, JsonRequestBehavior.DenyGet);
                }
                return Json(categories, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(categories, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Category cat = null;
            try
            {
                Categories categories = new Catalog().SelectCategory(id);
                cat = new Category();
                if (categories == null)
                {
                    response = "Please Select A Valid Categroy";
                }
                else
                {
                    Categories category = new Categories();
                    category.id = id;

                    if (categories.Enable == 1)
                    {
                        category.Enable = 0;
                    }
                    else
                    {
                        category.Enable = 1;
                    }

                    new Catalog().UpdatepCategory(category);
                    cat.Enable = category.Enable;
                    Activity activity = new Activity();
                    if (cat.Enable == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (cat.Enable == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Category";
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
                response = "Internal Server Error.";
            }

            return Json(cat, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelCategories(string DeleteCategoryData)
        {
            string response = "";
            List<Categories> categories = null;
            List<Category> categoriesNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Category> catsrequestedtoDelete = js.Deserialize<List<Category>>(DeleteCategoryData);
                categories = new List<Categories>();
                activities = new List<Activity>();
                categoriesNotDelete = new List<Category>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in catsrequestedtoDelete)
                {
                    Categories li = new Categories();
                    Categories liChecked = new Categories();
                    li.id = dbr.id;
                    li.Category_Name = dbr.Category_Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;
                    liChecked = new Catalog().CheckCategoryForDelete(li.id);
                    if (liChecked != null)
                    {
                        Category cat = new Category();
                        cat.Category_Name = liChecked.Category_Name;
                        categoriesNotDelete.Add(cat);
                    }
                    else
                    {
                        categories.Add(li);
                    }
                }
                categories.TrimExcess();
                categoriesNotDelete.TrimExcess();
                if (categories.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(categories);

                    StringBuilder fileContent = new StringBuilder();

                    foreach (var col in dt.Columns)
                    {
                        fileContent.Append(col.ToString() + ",");
                    }
                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (var column in dr.ItemArray)
                        {
                            fileContent.Append("\"" + column.ToString() + "\",");
                        }
                        fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                    }
                    string filename = "Category-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Categories/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Categories/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Categories";
                    mailMessage.Body = "Following Categories are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Categories";
                    string Result = new Catalog().DelCategoryRequest(categories, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (categories != null)
                        {
                            foreach (var i in categories)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Category";
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

            return Json(new { Response = response, Categories = categories, CategoriesNotDelete = categoriesNotDelete }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelCategory(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}