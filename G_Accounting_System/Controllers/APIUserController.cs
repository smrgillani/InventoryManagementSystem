using G_Accounting_System.APP;
using G_Accounting_System.Auth;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace G_Accounting_System.Controllers
{
    [CustomAuthorize]
    public class APIUserController : ApiController
    {
        [Route("api/APIUser/GetAllUsers")]
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Users> user = new Catalog().AllUsers(search.Option, search.Search, search.StartDate, search.EndDate, null);

                users = new List<User>();

                if (user != null)
                {
                    foreach (var dbr in user)
                    {
                        User li = new User();
                        li.id = dbr.id;
                        li.email = dbr.email;
                        li.Name = dbr.Name.Name;
                        li.Company = dbr.Company;
                        li.Designation = dbr.Designation;
                        li.Landline = dbr.Landline;
                        li.Mobile = dbr.Mobile;
                        li.ContactEmail = dbr.ContactEmail;
                        li.Address = dbr.Address;
                        li.AddressLandline = dbr.AddressLandline;
                        li.City = dbr.City;
                        li.CityName = dbr.CityName;
                        li.Country = dbr.Country;
                        li.CountryName = dbr.CountryName;
                        li.BankAccountNumber = dbr.BankAccountNumber;
                        li.status = dbr.status;
                        li.Premises_id = dbr.Premises_id;
                        li.PremisesPhone = dbr.PremisesPhone;
                        li.PremisesCity = dbr.PremisesCity;
                        li.PremisesCityName = dbr.PremisesCityName;
                        li.PremisesCountry = dbr.PremisesCountry;
                        li.PremisesCountryName = dbr.PremisesCountryName;
                        li.PremisesAddress = dbr.PremisesAddress;
                        li.pao = dbr.pao;
                        li.paf = dbr.paf;
                        li.pas = dbr.pas;
                        li.pas_ = dbr.pas_;
                        li.pav = dbr.pav;
                        li.pap = dbr.pap;
                        li.pac = dbr.pac;
                        li.pas__ = dbr.pas__;
                        li.pae = dbr.pae;
                        li.pap_ = dbr.pap_;
                        li.pai = dbr.pai;
                        li.pas___ = dbr.pas___;
                        li.pau = dbr.pau;
                        li.puo = dbr.puo;
                        li.puf = dbr.puf;
                        li.pus = dbr.pus;
                        li.pus_ = dbr.pus_;
                        li.puv = dbr.puv;
                        li.pup = dbr.pup;
                        li.puc = dbr.puc;
                        li.pus__ = dbr.pus__;
                        li.pue = dbr.pue;
                        li.pup_ = dbr.pup_;
                        li.pui = dbr.pui;
                        li.pus___ = dbr.pus___;
                        li.puu = dbr.puu;
                        li.pdo = dbr.pdo;
                        li.pdf = dbr.pdf;
                        li.pds = dbr.pds;
                        li.pds_ = dbr.pds_;
                        li.pdv = dbr.pdv;
                        li.pdp = dbr.pdp;
                        li.pdc = dbr.pdc;
                        li.pds__ = dbr.pds__;
                        li.pde = dbr.pde;
                        li.pdp_ = dbr.pdp_;
                        li.pdi = dbr.pdi;
                        li.pds___ = dbr.pds___;
                        li.pdu = dbr.pdu;
                        li.pvo = dbr.pvo;
                        li.pvf = dbr.pvf;
                        li.pvs = dbr.pvs;
                        li.pvs_ = dbr.pvs_;
                        li.pvv = dbr.pvv;
                        li.pvp = dbr.pvp;
                        li.pvc = dbr.pvc;
                        li.pvs__ = dbr.pvs__;
                        li.pve = dbr.pve;
                        li.pvp_ = dbr.pvp_;
                        li.pvi = dbr.pvi;
                        li.pvs___ = dbr.pvs___;
                        li.pvu = dbr.pvu;
                        li.pvol = dbr.pvol;
                        li.pvfl = dbr.pvfl;
                        li.pvsl = dbr.pvsl;
                        li.pvsl_ = dbr.pvsl_;
                        li.pvvl = dbr.pvvl;
                        li.pvpl = dbr.pvpl;
                        li.pvcl = dbr.pvcl;
                        li.pvsl__ = dbr.pvsl__;
                        li.pvel = dbr.pvel;
                        li.pvpl_ = dbr.pvpl_;
                        li.pvil = dbr.pvil;
                        li.pvsl___ = dbr.pvsl___;
                        li.pvul = dbr.pvul;
                        li.password = "".PadRight(dbr.password.Length, '*');
                        li.attached_profile = (dbr.Name != null) ? dbr.Name.Name : "";
                        li.PremisesName = dbr.PremisesName;
                        li.status = (dbr.status.Equals("1")) ? "Active" : "Inactive";
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        users.Add(li);
                    }
                }

                users.TrimExcess();

                return users;
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

        [Route("api/APIUser/UserById")]
        [HttpGet]
        public Classes UserById()
        {
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                User users = js.Deserialize<User>(strJson);

                List<Contacts> employee = new Catalog().AllEmployees(null, null, null, null);
                List<Contacts> customer = new Catalog().AllCustomers(null, null, null, null);
                List<Contacts> vendor = new Catalog().AllVendors(null, null, null, null);
                Users user = new Catalog().SelectUser(users.id);

                List<Premisess> stores = new Catalog().AllStores(null, null, null, null);
                List<Premisess> offices = new Catalog().AllOffices(null, null, null, null);
                List<Premisess> shops = new Catalog().AllShops(null, null, null, null);
                List<Premisess> factories = new Catalog().AllFactories(null, null, null, null);

                Classes data = new Classes();

                data.Vendors = new List<Contact>();
                data.Employees = new List<Contact>();
                data.Customers = new List<Contact>();

                data.Stores = new List<Premises>();
                data.Offices = new List<Premises>();
                data.Shops = new List<Premises>();
                data.Factories = new List<Premises>();

                if (employee != null)
                {
                    foreach (var dbr in employee)
                    {
                        Contact li = new Contact();
                        li.id = dbr.id;
                        li.Name = dbr.Salutation + " " + dbr.Name;
                        data.Employees.Add(li);
                    }
                    data.Employees.TrimExcess();
                }

                if (vendor != null)
                {
                    foreach (var dbr in vendor)
                    {
                        Contact li = new Contact();
                        li.id = dbr.id;
                        li.Name = dbr.Salutation + " " + dbr.Name;
                        data.Vendors.Add(li);
                    }
                    data.Vendors.TrimExcess();
                }

                if (customer != null)
                {
                    foreach (var dbr in customer)
                    {
                        Contact li = new Contact();
                        li.id = dbr.id;
                        li.Name = dbr.Salutation + " " + dbr.Name;
                        data.Customers.Add(li);
                    }
                    data.Customers.TrimExcess();
                }

                if (stores != null)
                {
                    foreach (var dbr in stores)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        data.Stores.Add(li);
                    }
                    data.Stores.TrimExcess();
                }

                if (offices != null)
                {
                    foreach (var dbr in offices)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        data.Offices.Add(li);
                    }
                    data.Offices.TrimExcess();
                }

                if (shops != null)
                {
                    foreach (var dbr in shops)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        data.Shops.Add(li);
                    }
                    data.Shops.TrimExcess();

                }

                if (factories != null)
                {
                    foreach (var dbr in factories)
                    {
                        Premises li = new Premises();
                        li.id = dbr.id;
                        li.Name = dbr.Name;
                        data.Factories.Add(li);
                    }
                    data.Factories.TrimExcess();

                }
                //var decryptedPassword = PasswordHelper.DecryptPassword(user.password);
                if (user != null)
                {
                    data.User = new User
                    {
                        id = user.id,
                        email = user.email,
                        attached_profile = user.attached_profile,
                        //password = "".PadRight(user.password.Length, '*'),
                        //password = "".PadRight(decryptedPassword.Count(),'*'),
                        Role_id = user.Role_id,
                        Role = user.Role,
                        AddedBy = user.AddedBy,
                        UpdatedBy = user.UpdatedBy,
                        Time = user.Time,
                        Date = user.Date,
                        Month = user.Month,
                        Year = user.Year,
                        Name = user.Name.Name,
                        CountP = user.CountP,
                        Company = user.Company,
                        Designation = user.Designation,
                        Landline = user.Landline,
                        Mobile = user.Mobile,
                        ContactEmail = user.ContactEmail,
                        Address = user.Address,
                        AddressLandline = user.AddressLandline,
                        City = user.City,
                        CityName = user.CityName,
                        Country = user.Country,
                        CountryName = user.CountryName,
                        BankAccountNumber = user.BankAccountNumber,
                        status = user.status,
                        Premises_id = user.Premises_id,
                        PremisesName = user.PremisesName,
                        PremisesPhone = user.PremisesPhone,
                        PremisesCity = user.PremisesCity,
                        PremisesCityName = user.PremisesCityName,
                        PremisesCountry = user.PremisesCountry,
                        PremisesCountryName = user.PremisesCountryName,
                        PremisesAddress = user.PremisesAddress,
                        pao = user.pao,
                        paf = user.paf,
                        pas = user.pas,
                        pas_ = user.pas_,
                        pav = user.pav,
                        pap = user.pap,
                        pac = user.pac,
                        pas__ = user.pas__,
                        pae = user.pae,
                        pap_ = user.pap_,
                        pai = user.pai,
                        pas___ = user.pas___,
                        pau = user.pau,
                        puo = user.puo,
                        puf = user.puf,
                        pus = user.pus,
                        pus_ = user.pus_,
                        puv = user.puv,
                        pup = user.pup,
                        puc = user.puc,
                        pus__ = user.pus__,
                        pue = user.pue,
                        pup_ = user.pup_,
                        pui = user.pui,
                        pus___ = user.pus___,
                        puu = user.puu,
                        pdo = user.pdo,
                        pdf = user.pdf,
                        pds = user.pds,
                        pds_ = user.pds_,
                        pdv = user.pdv,
                        pdp = user.pdp,
                        pdc = user.pdc,
                        pds__ = user.pds__,
                        pde = user.pde,
                        pdp_ = user.pdp_,
                        pdi = user.pdi,
                        pds___ = user.pds___,
                        pdu = user.pdu,
                        pvo = user.pvo,
                        pvf = user.pvf,
                        pvs = user.pvs,
                        pvs_ = user.pvs_,
                        pvv = user.pvv,
                        pvp = user.pvp,
                        pvc = user.pvc,
                        pvs__ = user.pvs__,
                        pve = user.pve,
                        pvp_ = user.pvp_,
                        pvi = user.pvi,
                        pvs___ = user.pvs___,
                        pvu = user.pvu,
                        pvol = user.pvol,
                        pvfl = user.pvfl,
                        pvsl = user.pvsl,
                        pvsl_ = user.pvsl_,
                        pvvl = user.pvvl,
                        pvpl = user.pvpl,
                        pvcl = user.pvcl,
                        pvsl__ = user.pvsl__,
                        pvel = user.pvel,
                        pvpl_ = user.pvpl_,
                        pvil = user.pvil,
                        pvsl___ = user.pvsl___,
                        pvul = user.pvul
                    };
                }


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

        [Route("api/APIUser/InsertUpdateUsers")]
        [HttpPost]
        public User InsertUpdateUsers()
        {
            User user = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    user = js.Deserialize<User>(strJson);

                    Users users = new Users();

                    Contacts employee = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 0, 1, 1);
                    Contacts customer = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 0, 1, 0, 1);
                    Contacts vendor = new Catalog().SelectContact(Convert.ToInt32(user.attached_profile), 1, 0, 0, 1);

                    users.id = user.id;
                    users.email = user.email;
                    users.attached_profile = user.attached_profile;
                    //var encryptedPassword = PasswordHelper.EncryptPassword(user.password);
                    users.password = user.password;
                    users.status = user.status.Equals("1") ? "1" : "0";
                    users.Premises_id = user.Premises_id;
                    users.Role_id = user.Role_id;
                    users.pao = user.pao;
                    users.paf = user.paf;
                    users.pas = user.pas;
                    users.pas_ = user.pas_;
                    users.pav = user.pav;
                    users.pap = user.pap;
                    users.pac = user.pac;
                    users.pas__ = user.pas__;
                    users.pae = user.pae;
                    users.pap_ = user.pap_;
                    users.pai = user.pai;
                    users.pas___ = user.pas___;
                    users.pau = user.pau;
                    users.puo = user.puo;
                    users.puf = user.puf;
                    users.pus = user.pus;
                    users.pus_ = user.pus_;
                    users.puv = user.puv;
                    users.pup = user.pup;
                    users.puc = user.puc;
                    users.pus__ = user.pus__;
                    users.pue = user.pue;
                    users.pup_ = user.pup_;
                    users.pui = user.pui;
                    users.pus___ = user.pus___;
                    users.puu = user.puu;
                    users.pdo = user.pdo;
                    users.pdf = user.pdf;
                    users.pds = user.pds;
                    users.pds_ = user.pds_;
                    users.pdv = user.pdv;
                    users.pdp = user.pdp;
                    users.pdc = user.pdc;
                    users.pds__ = user.pds__;
                    users.pde = user.pde;
                    users.pdp_ = user.pdp_;
                    users.pdi = user.pdi;
                    users.pds___ = user.pds___;
                    users.pdu = user.pdu;
                    users.pvo = user.pvo;
                    users.pvf = user.pvf;
                    users.pvs = user.pvs;
                    users.pvs_ = user.pvs_;
                    users.pvv = user.pvv;
                    users.pvp = user.pvp;
                    users.pvc = user.pvc;
                    users.pvs__ = user.pvs__;
                    users.pve = user.pve;
                    users.pvp_ = user.pvp_;
                    users.pvi = user.pvi;
                    users.pvs___ = user.pvs___;
                    users.pvu = user.pvu;
                    users.pvol = user.pvol;
                    users.pvfl = user.pvfl;
                    users.pvsl = user.pvsl;
                    users.pvsl_ = user.pvsl_;
                    users.pvvl = user.pvvl;
                    users.pvpl = user.pvpl;
                    users.pvcl = user.pvcl;
                    users.pvsl__ = user.pvsl__;
                    users.pvel = user.pvel;
                    users.pvpl_ = user.pvpl_;
                    users.pvil = user.pvil;
                    users.pvsl___ = user.pvsl___;
                    users.pvul = user.pvul;

                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (user.id == 0)
                    {
                        users.AddedBy = Convert.ToInt32(User_id);
                        users.UpdatedBy = 0;
                    }
                    else
                    {
                        users.UpdatedBy = Convert.ToInt32(User_id);
                        users.AddedBy = 0;
                    }
                    new Catalog().InsertUpdateUser(users);
                    user.pFlag = users.pFlag;
                    user.pDesc = users.pDesc;
                    user.pUserid_Out = users.pUserid_Out;
                }

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
