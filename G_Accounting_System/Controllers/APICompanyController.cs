using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    public class APICompanyController : ApiController
    {
        [Route("api/APICompany/GetAllCompanies")]
        [HttpGet]
        public IEnumerable<Company> GetAllCompanies()
        {
            List<Company> companies = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                //HttpRequest resolveRequest = HttpContext.Current.Request;
                //List<YourModel> model = new List<YourModel>();
                //resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
                //string Search = new StreamReader(resolveRequest.InputStream).ReadToEnd();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Companies> company = new Catalog().SelectAllAICompanies(search.Option, search.Search, search.StartDate, search.EndDate, null);

                companies = new List<Company>();

                if (company != null)
                {
                    foreach (var dbr in company)
                    {
                        Company li = new Company();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Landline = dbr.Landline;
                        li.Mobile = dbr.Mobile;
                        li.Email = dbr.Email;
                        li.City = dbr.City;
                        li.BankAccountNumber = dbr.BankAccountNumber;
                        li.PaymentMethod = (dbr.PaymentMethod.Equals("1") ? "Cheque" : "") + (dbr.PaymentMethod.Equals("2") ? "Cash" : "") + (dbr.PaymentMethod.Equals("3") ? "Card" : "");
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        companies.Add(li);
                    }
                }

                companies.TrimExcess();

                return companies;
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

        [Route("api/APICompany/CompanyById")]
        [HttpGet]
        public Classes CompanyById()
        {
            Company company = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                company = js.Deserialize<Company>(strJson);

                Companies companies = new Catalog().SelectCompany(Convert.ToInt32(company.id), null);

                Classes data = new Classes();
                data.Company = new Company();
                data.Company.id = companies.id;
                data.Company.Name = companies.Name;
                data.Company.Landline = companies.Landline;
                data.Company.Mobile = companies.Mobile;
                data.Company.Email = companies.Email;
                data.Company.Website = companies.Website;
                data.Company.Address = companies.Address;
                data.Company.City = companies.City;
                data.Company.Country = companies.Country;
                data.Company.BankAccountNumber = companies.BankAccountNumber;
                data.Company.PaymentMethod = companies.PaymentMethod;
                data.Company.Payment_method_ = (companies.PaymentMethod.Equals("1") ? "Cheque" : "") + (companies.PaymentMethod.Equals("2") ? "Cash" : "") + (companies.PaymentMethod.Equals("3") ? "Card" : "");
                data.Company.IsEnabled = companies.Enable.ToString();
                data.Company.IsEnabled_ = (companies.Enable == 1) ? "Active" : "InActive";
                int ActivityType_id = company.id;
                string ActivityType = "Company";
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

        [Route("api/APICompany/InsertUpdateCompanies")]
        [HttpPost]
        public Company InsertUpdateCompanies()
        {
            Company company = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    company = js.Deserialize<Company>(strJson);

                    Companies AddCompany = new Companies();
                    AddCompany.id = company.id;
                    AddCompany.Name = company.Name;
                    AddCompany.Landline = company.Landline;
                    AddCompany.Mobile = company.Mobile;
                    AddCompany.Email = company.Email;
                    AddCompany.Website = company.Website;
                    AddCompany.Address = company.Address;
                    AddCompany.City = company.City;
                    AddCompany.Country = company.Country;
                    AddCompany.BankAccountNumber = company.BankAccountNumber;
                    AddCompany.PaymentMethod = (company.PaymentMethod.Equals("1") || company.PaymentMethod.Equals("2") || company.PaymentMethod.Equals("3")) ? company.PaymentMethod : "1";
                    AddCompany.Enable = company.IsEnabled.Equals("1") ? 1 : 0;

                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (company.id == 0)
                    {
                        AddCompany.AddedBy = Convert.ToInt32(User_id);
                        AddCompany.UpdatedBy = 0;
                        company.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddCompany.UpdatedBy = Convert.ToInt32(User_id);
                        AddCompany.AddedBy = 0;
                        company.UpdatedBy = Convert.ToInt32(User_id);
                    }
                    new Catalog().InsertUpdateCompanies(AddCompany);

                    company.pFlag = AddCompany.pFlag;
                    company.pDesc = AddCompany.pDesc;
                    company.pCompanyid_Out = AddCompany.pCompanyid_Out;
                    if (company.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (company.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(company.pCompanyid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = company.id;
                        }
                        activity.ActivityType = "Company";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return company;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APICompany/SendRequestToDelCompanies")]
        [HttpPost]
        public object SendRequestToDelCompanies()
        {
            List<Companies> companies = null;
            List<Company> companiesNotDelete = null;
            List<Company> companysrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                companysrequestedtoDelete = js.Deserialize<List<Company>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    companies = new List<Companies>();
                    companiesNotDelete = new List<Company>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in companysrequestedtoDelete)
                    {
                        Companies li = new Companies();
                        Companies liChecked = new Companies();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = dbr.Delete_Status;
                        liChecked = new Catalog().CheckCompanyForDelete(li.id);
                        if (liChecked != null)
                        {
                            Company co = new Company();
                            co.Name = liChecked.Name;
                            companiesNotDelete.Add(co);
                        }
                        else
                        {
                            companies.Add(li);
                        }

                    }
                    companies.TrimExcess();
                    companiesNotDelete.TrimExcess();
                    if (companies.Count != 0)
                    {
                        DataTable dt = ToDataTable.ListToDataTable(companies);

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
                        string filename = "Companies-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Companies/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Companies/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                        MailMessage mailMessage = new MailMessage();

                        mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                        mailMessage.To.Add("salmanahmed635@gmail.com");
                        mailMessage.Subject = "Delete Companies";
                        mailMessage.Body = "Following Companies are requested to be deleted";
                        foreach (FileInfo file in dir.GetFiles(filename))
                        {
                            if (file.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(file.FullName));
                            }
                        }
                        smtpClient.Send(mailMessage);

                        string type = "Companies";
                        new Catalog().DelCompanyRequest(companies, type);
                    }

                }
                return new { companies, companiesNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
