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
    public class APIPremisesController : ApiController
    {
        #region OFFICES
        [Route("api/APIPremises/GetAllOffices")]
        [HttpGet]
        public IEnumerable<Premises> GetAllOffices()
        {
            List<Premises> office = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Premisess> offices = new Catalog().AllOffices(search.filtertype, search.Search, search.StartDate, search.EndDate);

                office = new List<Premises>();

                if (offices != null)
                {
                    foreach (var dbr in offices)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.pc_mac_Address = dbr.pc_mac_Address;
                        li.Phone = dbr.Phone;
                        li.City = dbr.City;
                        li.CityName = dbr.CityName;
                        li.Country = dbr.Country;
                        li.CountryName = dbr.CountryName;
                        li.Address = dbr.Address;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        li.AddedBy = dbr.AddedBy;
                        li.UpdatedBy = dbr.UpdatedBy;
                        office.Add(li);
                    }
                }

                office.TrimExcess();

                return office;
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

        [Route("api/APIPremises/OfficeById")]
        [HttpGet]
        public Classes OfficeById()
        {
            Premises office = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                office = js.Deserialize<Premises>(strJson);



                Premisess offices = new Catalog().SelectPremises(Convert.ToInt32(office.id));
                Classes data = new Classes();
                data.Premises = new Premises();
                data.Premises.id = offices.id;
                data.Premises.Name = offices.Name;
                data.Premises.pc_mac_Address = offices.pc_mac_Address;
                data.Premises.Phone = offices.Phone;
                data.Premises.Address = offices.Address;
                data.Premises.Country = offices.Country;
                data.Premises.CountryName = offices.CountryName;
                data.Premises.City = offices.City;
                data.Premises.CityName = offices.CityName;
                data.Premises.CountryName = offices.CountryName;
                data.Premises.CityName = offices.CityName;
                data.Premises.AddedBy = offices.AddedBy;
                data.Premises.UpdatedBy = offices.UpdatedBy;
                data.Premises.Delete_Status = offices.Delete_Status;
                data.Premises.Delete_Request_By = offices.Delete_Request_By;
                int ActivityType_id = office.id;
                string ActivityType = "Office";
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

        [Route("api/APIPremises/InsertUpdateOffice")]
        [HttpPost]
        public Premises InsertUpdateOffice()
        {
            Premises offices = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    offices = js.Deserialize<Premises>(strJson);

                    Premisess AddOffice = new Premisess();
                    AddOffice.id = offices.id;
                    AddOffice.Name = offices.Name;
                    AddOffice.pc_mac_Address = offices.pc_mac_Address;
                    AddOffice.Phone = offices.Phone;
                    AddOffice.City = offices.City;
                    AddOffice.Country = offices.Country;
                    AddOffice.Address = offices.Address;
                    AddOffice.Office = "1";
                    AddOffice.Factory = "0";
                    AddOffice.Store = "0";
                    AddOffice.Shop = "0";
                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (offices.id == 0)
                    {
                        AddOffice.AddedBy = Convert.ToInt32(User_id);
                        AddOffice.UpdatedBy = 0;
                    }
                    else
                    {
                        AddOffice.UpdatedBy = Convert.ToInt32(User_id);
                        AddOffice.AddedBy = 0;
                    }
                    new Catalog().AddPremises(AddOffice);

                    offices.pFlag = AddOffice.pFlag;
                    offices.pDesc = AddOffice.pDesc;
                    offices.pPremisesid_Out = AddOffice.pPremisesid_Out;
                    if (offices.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (offices.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(offices.pPremisesid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = offices.id;
                        }
                        activity.ActivityType = "Office";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return offices;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIPremises/SendRequestToDelOffices")]
        [HttpPost]
        public object SendRequestToDelOffices()
        {
            List<Premisess> premises = null;
            List<Premises> premisesNotDelete = null;
            List<Premises> premisessrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                premisessrequestedtoDelete = js.Deserialize<List<Premises>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    premises = new List<Premisess>();
                    premisesNotDelete = new List<Premises>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in premisessrequestedtoDelete)
                    {
                        Premisess li = new Premisess();
                        Premisess liChecked = new Premisess();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = dbr.Delete_Status;
                        liChecked = new Catalog().CheckPremisesForDelete(li.id);
                        if (liChecked != null)
                        {
                            Premises pr = new Premises();
                            pr.Name = liChecked.Name;
                            premisesNotDelete.Add(pr);
                        }
                        else
                        {
                            premises.Add(li);
                        }
                    }
                    premises.TrimExcess();
                    premisesNotDelete.TrimExcess();
                    if (premises.Count != 0)
                    {
                        DataTable dt = ToDataTable.ListToDataTable(premises);

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
                        string filename = "Offices-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Offices/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Offices/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                        MailMessage mailMessage = new MailMessage();

                        mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                        mailMessage.To.Add("salmanahmed635@gmail.com");
                        mailMessage.Subject = "Delete Offices";
                        mailMessage.Body = "Following Offices are requested to be deleted";
                        foreach (FileInfo file in dir.GetFiles(filename))
                        {
                            if (file.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(file.FullName));
                            }
                        }
                        smtpClient.Send(mailMessage);

                        string type = "Offices";
                        new Catalog().DelPremisesRequest(premises, type);
                    }

                }
                return new { premises, premisesNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region FACTORIES
        [Route("api/APIPremises/GetAllFactories")]
        [HttpGet]
        public IEnumerable<Premises> GetAllFactories()
        {
            List<Premises> factory = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Premisess> factories = new Catalog().AllFactories(search.Option, search.Search, search.StartDate, search.EndDate);

                factory = new List<Premises>();

                if (factories != null)
                {
                    foreach (var dbr in factories)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.pc_mac_Address = dbr.pc_mac_Address;
                        li.Phone = dbr.Phone;
                        li.City = dbr.City;
                        li.CityName = dbr.CityName;
                        li.Country = dbr.Country;
                        li.CountryName = dbr.CountryName;
                        li.Address = dbr.Address;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        li.AddedBy = dbr.AddedBy;
                        li.UpdatedBy = dbr.UpdatedBy;
                        factory.Add(li);
                    }
                }

                factory.TrimExcess();

                return factory;
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

        [Route("api/APIPremises/FactoryById")]
        [HttpGet]
        public Classes FactoryById()
        {
            Premises factory = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                factory = js.Deserialize<Premises>(strJson);



                Premisess factories = new Catalog().SelectPremises(Convert.ToInt32(factory.id));

                Classes data = new Classes();
                data.Premises = new Premises();
                data.Premises.id = factories.id;
                data.Premises.Name = factories.Name;
                data.Premises.pc_mac_Address = factories.pc_mac_Address;
                data.Premises.Phone = factories.Phone;
                data.Premises.Address = factories.Address;
                data.Premises.Country = factories.Country;
                data.Premises.CountryName = factories.CountryName;
                data.Premises.City = factories.City;
                data.Premises.CityName = factories.CityName;
                data.Premises.CountryName = factories.CountryName;
                data.Premises.CityName = factories.CityName;
                data.Premises.AddedBy = factories.AddedBy;
                data.Premises.UpdatedBy = factories.UpdatedBy;
                data.Premises.Delete_Status = factories.Delete_Status;
                data.Premises.Delete_Request_By = factories.Delete_Request_By;
                int ActivityType_id = factory.id;
                string ActivityType = "Factory";
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

        [Route("api/APIPremises/InsertUpdateFactory")]
        [HttpPost]
        public Premises InsertUpdateFactory()
        {
            Premises factory = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    factory = js.Deserialize<Premises>(strJson);

                    Premisess AddFactory = new Premisess();
                    AddFactory.id = factory.id;
                    AddFactory.Name = factory.Name;
                    AddFactory.pc_mac_Address = factory.pc_mac_Address;
                    AddFactory.Phone = factory.Phone;
                    AddFactory.City = factory.City;
                    AddFactory.Country = factory.Country;
                    AddFactory.Address = factory.Address;
                    AddFactory.Office = "0";
                    AddFactory.Factory = "1";
                    AddFactory.Store = "0";
                    AddFactory.Shop = "0";
                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (factory.id == 0)
                    {
                        AddFactory.AddedBy = Convert.ToInt32(User_id);
                        AddFactory.UpdatedBy = 0;
                        factory.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddFactory.UpdatedBy = Convert.ToInt32(User_id);
                        AddFactory.AddedBy = 0;
                        factory.UpdatedBy = Convert.ToInt32(User_id);
                    }
                    new Catalog().AddPremises(AddFactory);

                    factory.pFlag = AddFactory.pFlag;
                    factory.pDesc = AddFactory.pDesc;
                    factory.pPremisesid_Out = AddFactory.pPremisesid_Out;
                    if (factory.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (factory.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(factory.pPremisesid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = factory.id;
                        }
                        activity.ActivityType = "Factory";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return factory;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIPremises/SendRequestToDelFactories")]
        [HttpPost]
        public object SendRequestToDelFactories()
        {
            List<Premisess> premises = null;
            List<Premises> premisesNotDelete = null;
            List<Premises> premisessrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                premisessrequestedtoDelete = js.Deserialize<List<Premises>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    premises = new List<Premisess>();
                    premisesNotDelete = new List<Premises>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in premisessrequestedtoDelete)
                    {
                        Premisess li = new Premisess();
                        Premisess liChecked = new Premisess();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = dbr.Delete_Status;
                        liChecked = new Catalog().CheckPremisesForDelete(li.id);
                        if (liChecked != null)
                        {
                            Premises pr = new Premises();
                            pr.Name = liChecked.Name;
                            premisesNotDelete.Add(pr);
                        }
                        else
                        {
                            premises.Add(li);
                        }
                    }
                    premises.TrimExcess();
                    premisesNotDelete.TrimExcess();
                    if (premises.Count != 0)
                    {
                        DataTable dt = ToDataTable.ListToDataTable(premises);

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
                        string filename = "Factories-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Factories/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Factories/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                        MailMessage mailMessage = new MailMessage();

                        mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                        mailMessage.To.Add("salmanahmed635@gmail.com");
                        mailMessage.Subject = "Delete Factories";
                        mailMessage.Body = "Following Factories are requested to be deleted";
                        foreach (FileInfo file in dir.GetFiles(filename))
                        {
                            if (file.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(file.FullName));
                            }
                        }
                        smtpClient.Send(mailMessage);

                        string type = "Factories";
                        new Catalog().DelPremisesRequest(premises, type);
                    }

                }
                return new { premises, premisesNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region STORES
        [Route("api/APIPremises/GetAllStores")]
        [HttpGet]
        public IEnumerable<Premises> GetAllStores()
        {
            List<Premises> store = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Premisess> stores = new Catalog().AllStores(search.Option, search.Search, search.StartDate, search.EndDate);

                store = new List<Premises>();

                if (stores != null)
                {
                    foreach (var dbr in stores)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.pc_mac_Address = dbr.pc_mac_Address;
                        li.Phone = dbr.Phone;
                        li.City = dbr.City;
                        li.CityName = dbr.CityName;
                        li.Country = dbr.Country;
                        li.CountryName = dbr.CountryName;
                        li.Address = dbr.Address;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        li.AddedBy = dbr.AddedBy;
                        li.UpdatedBy = dbr.UpdatedBy;
                        store.Add(li);
                    }
                }

                store.TrimExcess();

                return store;
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

        [Route("api/APIPremises/StoreById")]
        [HttpGet]
        public Classes StoreById()
        {
            Premises store = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                store = js.Deserialize<Premises>(strJson);



                Premisess stores = new Catalog().SelectPremises(Convert.ToInt32(store.id));
                Classes data = new Classes();
                data.Premises = new Premises();
                data.Premises.id = stores.id;
                data.Premises.Name = stores.Name;
                data.Premises.pc_mac_Address = stores.pc_mac_Address;
                data.Premises.Phone = stores.Phone;
                data.Premises.Address = stores.Address;
                data.Premises.Country = stores.Country;
                data.Premises.CountryName = stores.CountryName;
                data.Premises.City = stores.City;
                data.Premises.CityName = stores.CityName;
                data.Premises.CountryName = stores.CountryName;
                data.Premises.CityName = stores.CityName;
                data.Premises.AddedBy = stores.AddedBy;
                data.Premises.UpdatedBy = stores.UpdatedBy;
                data.Premises.Delete_Status = stores.Delete_Status;
                data.Premises.Delete_Request_By = stores.Delete_Request_By;
                int ActivityType_id = store.id;
                string ActivityType = "Store";
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

        [Route("api/APIPremises/InsertUpdateStore")]
        [HttpPost]
        public Premises InsertUpdateStore()
        {
            Premises store = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    store = js.Deserialize<Premises>(strJson);

                    Premisess AddStore = new Premisess();
                    AddStore.id = store.id;
                    AddStore.Name = store.Name;
                    AddStore.pc_mac_Address = store.pc_mac_Address;
                    AddStore.Phone = store.Phone;
                    AddStore.City = store.City;
                    AddStore.Country = store.Country;
                    AddStore.Address = store.Address;
                    AddStore.Office = "0";
                    AddStore.Factory = "0";
                    AddStore.Store = "1";
                    AddStore.Shop = "0";
                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (store.id == 0)
                    {
                        AddStore.AddedBy = Convert.ToInt32(User_id);
                        AddStore.UpdatedBy = 0;
                        store.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddStore.UpdatedBy = Convert.ToInt32(User_id);
                        AddStore.AddedBy = 0;
                        store.UpdatedBy = Convert.ToInt32(User_id);
                    }
                    new Catalog().AddPremises(AddStore);

                    store.pFlag = AddStore.pFlag;
                    store.pDesc = AddStore.pDesc;
                    store.pPremisesid_Out = AddStore.pPremisesid_Out;
                    if (store.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (store.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(store.pPremisesid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = store.id;
                        }
                        activity.ActivityType = "Store";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return store;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIPremises/SendRequestToDelStores")]
        [HttpPost]
        public object SendRequestToDelStores()
        {
            List<Premisess> premises = null;
            List<Premises> premisesNotDelete = null;
            List<Premises> premisessrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                premisessrequestedtoDelete = js.Deserialize<List<Premises>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    premises = new List<Premisess>();
                    premisesNotDelete = new List<Premises>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in premisessrequestedtoDelete)
                    {
                        Premisess li = new Premisess();
                        Premisess liChecked = new Premisess();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = dbr.Delete_Status;
                        liChecked = new Catalog().CheckPremisesForDelete(li.id);
                        if (liChecked != null)
                        {
                            Premises pr = new Premises();
                            pr.Name = liChecked.Name;
                            premisesNotDelete.Add(pr);
                        }
                        else
                        {
                            premises.Add(li);
                        }
                    }
                    premises.TrimExcess();
                    premisesNotDelete.TrimExcess();
                    if (premises.Count != 0)
                    {
                        DataTable dt = ToDataTable.ListToDataTable(premises);

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
                        string filename = "Stores-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Stores/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Stores/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                        MailMessage mailMessage = new MailMessage();

                        mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                        mailMessage.To.Add("salmanahmed635@gmail.com");
                        mailMessage.Subject = "Delete Stores";
                        mailMessage.Body = "Following Stores are requested to be deleted";
                        foreach (FileInfo file in dir.GetFiles(filename))
                        {
                            if (file.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(file.FullName));
                            }
                        }
                        smtpClient.Send(mailMessage);

                        string type = "Stores";
                        new Catalog().DelPremisesRequest(premises, type);
                    }

                }
                return new { premises, premisesNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region SHOPS
        [Route("api/APIPremises/GetAllShops")]
        [HttpGet]
        public IEnumerable<Premises> GetAllShops()
        {
            List<Premises> shop = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Premisess> shops = new Catalog().AllShops(search.Option, search.Search, search.StartDate, search.EndDate);

                shop = new List<Premises>();

                if (shops != null)
                {
                    foreach (var dbr in shops)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.pc_mac_Address = dbr.pc_mac_Address;
                        li.Phone = dbr.Phone;
                        li.City = dbr.City;
                        li.CityName = dbr.CityName;
                        li.Country = dbr.Country;
                        li.CountryName = dbr.CountryName;
                        li.Address = dbr.Address;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        li.AddedBy = dbr.AddedBy;
                        li.UpdatedBy = dbr.UpdatedBy;
                        shop.Add(li);
                    }
                }

                shop.TrimExcess();

                return shop;
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

        [Route("api/APIPremises/ShopById")]
        [HttpGet]
        public Classes ShopById()
        {
            Premises shop = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                shop = js.Deserialize<Premises>(strJson);



                Premisess shops = new Catalog().SelectPremises(Convert.ToInt32(shop.id));
                Classes data = new Classes();
                data.Premises = new Premises();
                data.Premises.id = shops.id;
                data.Premises.Name = shops.Name;
                data.Premises.pc_mac_Address = shops.pc_mac_Address;
                data.Premises.Phone = shops.Phone;
                data.Premises.Address = shops.Address;
                data.Premises.Country = shops.Country;
                data.Premises.CountryName = shops.CountryName;
                data.Premises.City = shops.City;
                data.Premises.CityName = shops.CityName;
                data.Premises.CountryName = shops.CountryName;
                data.Premises.CityName = shops.CityName;
                data.Premises.AddedBy = shops.AddedBy;
                data.Premises.UpdatedBy = shops.UpdatedBy;
                data.Premises.Delete_Status = shops.Delete_Status;
                data.Premises.Delete_Request_By = shops.Delete_Request_By;
                int ActivityType_id = shop.id;
                string ActivityType = "Shop";
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

        [Route("api/APIPremises/InsertUpdateShop")]
        [HttpPost]
        public Premises InsertUpdateShop()
        {
            Premises shop = null;
            string ActivityName = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    shop = js.Deserialize<Premises>(strJson);

                    Premisess AddShop = new Premisess();
                    AddShop.id = shop.id;
                    AddShop.Name = shop.Name;
                    AddShop.pc_mac_Address = shop.pc_mac_Address;
                    AddShop.Phone = shop.Phone;
                    AddShop.City = shop.City;
                    AddShop.Country = shop.Country;
                    AddShop.Address = shop.Address;
                    AddShop.Office = "0";
                    AddShop.Factory = "0";
                    AddShop.Store = "0";
                    AddShop.Shop = "1";
                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (shop.id == 0)
                    {
                        AddShop.AddedBy = Convert.ToInt32(User_id);
                        AddShop.UpdatedBy = 0;
                        shop.AddedBy = Convert.ToInt32(User_id);
                    }
                    else
                    {
                        AddShop.UpdatedBy = Convert.ToInt32(User_id);
                        AddShop.AddedBy = 0;
                        shop.UpdatedBy = Convert.ToInt32(User_id);
                    }
                    new Catalog().AddPremises(AddShop);

                    shop.pFlag = AddShop.pFlag;
                    shop.pDesc = AddShop.pDesc;
                    shop.pPremisesid_Out = AddShop.pPremisesid_Out;
                    if (shop.pFlag == "1")
                    {
                        Activity activity = new Activity();
                        if (shop.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(shop.pPremisesid_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = shop.id;
                        }
                        activity.ActivityType = "Shop";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return shop;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIPremises/SendRequestToDelShops")]
        [HttpPost]
        public object SendRequestToDelShops()
        {
            List<Premisess> premises = null;
            List<Premises> premisesNotDelete = null;
            List<Premises> premisessrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                premisessrequestedtoDelete = js.Deserialize<List<Premises>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    premises = new List<Premisess>();
                    premisesNotDelete = new List<Premises>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in premisessrequestedtoDelete)
                    {
                        Premisess li = new Premisess();
                        Premisess liChecked = new Premisess();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
                        li.Delete_Status = dbr.Delete_Status;
                        liChecked = new Catalog().CheckPremisesForDelete(li.id);
                        if (liChecked != null)
                        {
                            Premises pr = new Premises();
                            pr.Name = liChecked.Name;
                            premisesNotDelete.Add(pr);
                        }
                        else
                        {
                            premises.Add(li);
                        }
                    }
                    premises.TrimExcess();
                    premisesNotDelete.TrimExcess();
                    if (premises.Count != 0)
                    {
                        DataTable dt = ToDataTable.ListToDataTable(premises);

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
                        string filename = "Shops-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Shops/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Shops/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                        MailMessage mailMessage = new MailMessage();

                        mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                        mailMessage.To.Add("salmanahmed635@gmail.com");
                        mailMessage.Subject = "Delete Shops";
                        mailMessage.Body = "Following Shops are requested to be deleted";
                        foreach (FileInfo file in dir.GetFiles(filename))
                        {
                            if (file.Exists)
                            {
                                mailMessage.Attachments.Add(new Attachment(file.FullName));
                            }
                        }
                        smtpClient.Send(mailMessage);

                        string type = "Shops";
                        new Catalog().DelPremisesRequest(premises, type);
                    }

                }
                return new { premises, premisesNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion


    }
}
