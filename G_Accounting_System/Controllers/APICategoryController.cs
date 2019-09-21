using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;


namespace G_Accounting_System.Controllers
{
    [CustomAuthorize]
    public class APICategoryController : ApiController
    {
        [Route("api/APICategory/GetAllCategories")]
        [HttpPost]
        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                List<Categories> category = new Catalog().AllCategories(search.Option, search.Search, search.StartDate, search.EndDate);

                List<Category> categories = new List<Category>();

                if (category != null)
                {
                    foreach (var dbr in category)
                    {
                        Category li = new Category();
                        li.id = dbr.id;
                        li.Category_Name = dbr.Category_Name;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        categories.Add(li);
                    }
                }

                categories.TrimExcess();

                return categories;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //return response;
        }

        [Route("api/APICategory/CategoryById")]
        [HttpPost]
        public Classes CategoryById()
        {
            Category category = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                category = js.Deserialize<Category>(strJson);


                Categories categories = new Catalog().SelectCategory(Convert.ToInt32(category.id));
                Classes data = new Classes();
                data.Category = new Category();
                data.Category.id = categories.id;
                data.Category.Category_Name = categories.Category_Name;
                data.Category.Enable = categories.Enable;
                int ActivityType_id = category.id;
                string ActivityType = "Category";
                data.Activity = new ActivitiesClass().Activities(ActivityType_id, ActivityType);

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
            //var JsonResp = brands.ToJson();
            //HttpResponseMessage response;
            //response = Request.CreateResponse(HttpStatusCode.OK, brands);
            //response.Content = new StringContent(JsonResp, Encoding.UTF8, "application/json");
            //return response;
        }

        [Route("api/APICategory/InsertUpdateCategories")]
        [HttpPost]
        public Category InsertUpdateCategories()
        {
            Category category = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    category = js.Deserialize<Category>(strJson);

                    Categories AddCategory = new Categories();
                    AddCategory.id = category.id;
                    AddCategory.Category_Name = category.Category_Name;

                    var User_id = HttpContext.Current.User.Identity.Name;


                    if (category.id == 0)
                    {
                        AddCategory.AddedBy = Convert.ToInt32(User_id);
                        AddCategory.UpdatedBy = 0;
                    }
                    else
                    {
                        AddCategory.UpdatedBy = Convert.ToInt32(User_id);
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
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return category;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APICategory/ChangeCategoryStatus")]
        [HttpPost]
        public string ChangeCategoryStatus()
        {

            string ActivityName = null;
            string response = "";
            Category cat = null;
            Category catss = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                catss = js.Deserialize<Category>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                Categories categories = new Catalog().SelectCategory(catss.id);
                cat = new Category();
                if (categories == null)
                {
                    response = "Please Select A Valid Categroy";
                }
                else
                {
                    Categories category = new Categories();
                    category.id = catss.id;

                    if (categories.Enable == 1)
                    {
                        category.Enable = 0;
                    }
                    else
                    {
                        category.Enable = 1;
                    }

                    new Catalog().UpdatepCategory(category);
                    response = "Category Profile Visibility Updated Successfully.";
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
                    activity.ActivityType_id = catss.id;
                    activity.ActivityType = "Category";
                    activity.ActivityName = ActivityName;
                    activity.User_id = Convert.ToInt32(Convert.ToInt32(User_id));
                    activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                    List<Activity> activities = new List<Activity>();
                    activities.Add(activity);

                    new ActivitiesClass().InsertActivity(activities);
                }
                return response;
            }
            catch (Exception e)
            {
                response = e.Message;
                return response;
            }

        }

        [Route("api/APICategory/SendRequestToDelCategories")]
        [HttpPost]
        public object SendRequestToDelCategories()
        {
            List<Categories> categories = null;
            List<Category> categoriesNotDelete = null;
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                data = js.Deserialize<Classes>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    categories = new List<Categories>();
                    categoriesNotDelete = new List<Category>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in data.Categories)
                    {
                        Categories li = new Categories();
                        Categories liChecked = new Categories();
                        li.id = dbr.id;
                        li.Category_Name = dbr.Category_Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = "Requested";
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
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Categories/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Categories/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Categories";
                        new Catalog().DelCategoryRequest(categories, type);
                    }

                }
                return new { DeletedCategories = categories, NotDeletedCategories = categoriesNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
