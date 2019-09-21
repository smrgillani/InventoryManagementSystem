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
    public class APIBrandController : ApiController
    {
        [Route("api/APIBrand/GetAllBrands")]
        [HttpPost]
        public IEnumerable<Brand> GetAllBrands()
        {
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
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
                int count = brands.Count;
                return brands;
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

        [Route("api/APIBrand/BrandById")]
        [HttpPost]
        public Classes BrandById()
        {

            Brand brand = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                brand = js.Deserialize<Brand>(strJson);

                Brands brands = new Catalog().SelectBrand(Convert.ToInt32(brand.id));
                Classes data = new Classes();
                data.Brand = new Brand();
                data.Brand.id = brands.id;
                data.Brand.Brand_Name = brands.Brand_Name;
                data.Brand.Enable = brands.Enable;
                int ActivityType_id = brand.id;
                string ActivityType = "Brand";
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

        [Route("api/APIBrand/InsertUpdateBrands")]
        [HttpPost]
        public Brand InsertUpdateBrands()
        {
            Brand brand = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    brand = js.Deserialize<Brand>(strJson);

                    Brands AddBrand = new Brands();
                    AddBrand.id = brand.id;
                    AddBrand.Brand_Name = brand.Brand_Name;

                    var User_id = HttpContext.Current.User.Identity.Name;


                    if (brand.id == 0)
                    {
                        AddBrand.AddedBy = Convert.ToInt32(User_id);
                        AddBrand.UpdatedBy = 0;
                    }
                    else
                    {
                        AddBrand.UpdatedBy = Convert.ToInt32(User_id);
                        AddBrand.AddedBy = 0;
                    }

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
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return brand;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIBrand/ChangeBrandStatus")]
        [HttpPost]
        public string ChangeBrandStatus()
        {

            string ActivityName = null;
            string response = "";
            Brand br = null;
            Brand brandss = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                brandss = js.Deserialize<Brand>(strJson);
                var User_id = HttpContext.Current.User.Identity.Name;
                Brands brands = new Catalog().SelectBrand(brandss.id);
                br = new Brand();
                if (brands == null)
                {
                    response = "Please Select A Valid Brand";
                }
                else
                {
                    Brands brand = new Brands();
                    brand.id = brandss.id;

                    if (brands.Enable == 1)
                    {
                        brand.Enable = 0;
                    }
                    else
                    {
                        brand.Enable = 1;
                    }

                    new Catalog().UpdatepBrand(brand);
                    response = "Brand Profile Visibility Updated Successfully.";
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
                    activity.ActivityType_id = brandss.id;
                    activity.ActivityType = "Brand";
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

        [Route("api/APIBrand/SendRequestToDelBrands")]
        [HttpPost]
        public object SendRequestToDelBrands()
        {
            List<Brands> brands = null;
            List<Brand> brandsNotDelete = null;
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                data = js.Deserialize<Classes>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    brands = new List<Brands>();
                    brandsNotDelete = new List<Brand>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in data.Brands)
                    {
                        Brands li = new Brands();
                        Brands liChecked = new Brands();
                        li.id = dbr.id;

                        li.Brand_Name = dbr.Brand_Name;
                        li.Delete_Status = "Requested";
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);

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
                        string filename = "Brand-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Brands/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Brands/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Brands";
                        new Catalog().DelBrandRequest(brands, type);
                    }

                }
                return new { DeletedBrands= brands, NotDeletedBrands = brandsNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
