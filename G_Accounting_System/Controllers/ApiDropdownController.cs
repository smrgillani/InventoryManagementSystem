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

    public class ApiDropdownController : ApiController
    {
        [CustomAuthorize]
        [Route("api/ApiDropdown/CategoriesDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> CategoriesDropdown()
        {
            List<Dropdown> categoriesDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().CategoriesDropdown();
                categoriesDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        categoriesDropdown.Add(li);
                    }
                }
                return categoriesDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/BrandsDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> BrandsDropdown()
        {
            List<Dropdown> brandsDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().BrandsDropdown();
                brandsDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        brandsDropdown.Add(li);
                    }
                }
                return brandsDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/ManufacturersDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> ManufacturersDropdown()
        {
            List<Dropdown> manufacturersDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().ManufacturersDropdown();
                manufacturersDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        manufacturersDropdown.Add(li);
                    }
                }
                return manufacturersDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/UnitsDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> UnitsDropdown()
        {
            List<Dropdown> unitsDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().UnitsDropdown();
                unitsDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        unitsDropdown.Add(li);
                    }
                }
                return unitsDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/VendorsDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> VendorsDropdown()
        {
            List<Dropdown> vendorsDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().VendorsDropdown();
                vendorsDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        vendorsDropdown.Add(li);
                    }
                }
                return vendorsDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/CountriesDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> CountriesDropdown()
        {
            List<Dropdown> countriesDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().CountriesDropdown();
                countriesDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        countriesDropdown.Add(li);
                    }
                }
                return countriesDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/CitiesDropdown")]
        [HttpPost]
        public IEnumerable<Dropdown> CitiesDropdown()
        {
            Country country = null;
            List<Dropdown> citiesDropdown = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                country = js.Deserialize<Country>(strJson);

                List<Dropdowns> dropdown = new Catalog().CitiesDropdown(country.id);
                citiesDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        citiesDropdown.Add(li);
                    }
                }
                return citiesDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/RolesDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> RolesDropdown()
        {
            List<Dropdown> rolesDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().RolesDropdown();
                rolesDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        rolesDropdown.Add(li);
                    }
                }
                return rolesDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/ItemsDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> ItemsDropdown()
        {
            List<Dropdown> itemsDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().ItemsDropdown();
                itemsDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        itemsDropdown.Add(li);
                    }
                }
                return itemsDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/PaymentModesDropdown")]
        [HttpGet]
        public IEnumerable<Dropdown> PaymentModesDropdown()
        {
            List<Dropdown> paymentModesDropdown = null;
            try
            {
                List<Dropdowns> dropdown = new Catalog().PaymentModesDropdown();
                paymentModesDropdown = new List<Dropdown>();

                if (dropdown != null)
                {
                    foreach (var dbr in dropdown)
                    {
                        Dropdown li = new Dropdown();
                        li.id = dbr.id;
                        li.name = dbr.name;
                        paymentModesDropdown.Add(li);
                    }
                }
                return paymentModesDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/ApiDropdown/Premises_AttachedProfileDropdown")]
        [HttpGet]
        public Classes PremisesDropdown()
        {
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);
                List<Contacts> employee = new Catalog().AllEmployees(null, null, null,null);
                List<Contacts> customer = new Catalog().AllCustomers(null, null, null, null);
                List<Contacts> vendor = new Catalog().AllVendors(null, null, null, null);

                List<Premisess> stores = new Catalog().AllStores(null,null, null, null);
                List<Premisess> offices = new Catalog().AllOffices(null,null, null, null);
                List<Premisess> shops = new Catalog().AllShops(null,null, null, null);
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

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //[CustomAuthorize]
        //[Route("api/ApiDropdown/CustomerDropdown")]
        //[HttpGet]
        //public IEnumerable<Contact> CustomerDropdown()
        //{
        //    List<Contact> customerDropdown = null;
        //    try
        //    {
        //        List<Contacts> dropdown = new Catalog().AllCustomers(null);
        //        customerDropdown = new List<Contact>();

        //        if (dropdown != null)
        //        {
        //            foreach (var dbr in dropdown)
        //            {
        //                Contact li = new Contact();
        //                li.id = dbr.id;
        //                li.Name = dbr.Name;
        //                customerDropdown.Add(li);
        //            }
        //        }
        //        return customerDropdown;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //[CustomAuthorize]
        //[Route("api/ApiDropdown/VendorDropdown")]
        //[HttpGet]
        //public IEnumerable<Contact> VendorDropdown()
        //{
        //    List<Contact> vendorDropdown = null;
        //    try
        //    {
        //        List<Contacts> dropdown = new Catalog().AllVendors(null, null);
        //        vendorDropdown = new List<Contact>();

        //        if (dropdown != null)
        //        {
        //            foreach (var dbr in dropdown)
        //            {
        //                Contact li = new Contact();
        //                li.id = dbr.id;
        //                li.Name = dbr.Name;
        //                vendorDropdown.Add(li);
        //            }
        //        }
        //        return vendorDropdown;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //[CustomAuthorize]
        //[Route("api/ApiDropdown/EmployeeDropdown")]
        //[HttpGet]
        //public IEnumerable<Contact> EmployeeDropdown()
        //{
        //    List<Contact> employeeDropdown = null;
        //    try
        //    {
        //        List<Contacts> dropdown = new Catalog().AllEmployees(null);
        //        employeeDropdown = new List<Contact>();

        //        if (dropdown != null)
        //        {
        //            foreach (var dbr in dropdown)
        //            {
        //                Contact li = new Contact();
        //                li.id = dbr.id;
        //                li.Name = dbr.Name;
        //                employeeDropdown.Add(li);
        //            }
        //        }
        //        return employeeDropdown;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //[CustomAuthorize]
        //[Route("api/ApiDropdown/OfficeDropdown")]
        //[HttpGet]
        //public IEnumerable<Premises> OfficeDropdown()
        //{
        //    List<Premises> officeDropdown = null;
        //    try
        //    {
        //        List<Premisess> dropdown = new Catalog().AllOffices();
        //        officeDropdown = new List<Premises>();

        //        if (dropdown != null)
        //        {
        //            foreach (var dbr in dropdown)
        //            {
        //                Premises li = new Premises();
        //                li.id = dbr.id;
        //                li.Name = dbr.Name;
        //                officeDropdown.Add(li);
        //            }
        //        }
        //        return officeDropdown;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //[CustomAuthorize]
        //[Route("api/ApiDropdown/FactoriesDropdown")]
        //[HttpGet]
        //public IEnumerable<Premises> FactoriesDropdown()
        //{
        //    List<Premises> factoriesDropdown = null;
        //    try
        //    {
        //        List<Premisess> dropdown = new Catalog().AllFactories();
        //        factoriesDropdown = new List<Premises>();

        //        if (dropdown != null)
        //        {
        //            foreach (var dbr in dropdown)
        //            {
        //                Premises li = new Premises();
        //                li.id = dbr.id;
        //                li.Name = dbr.Name;
        //                factoriesDropdown.Add(li);
        //            }
        //        }
        //        return factoriesDropdown;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //[CustomAuthorize]
        //[Route("api/ApiDropdown/StoresDropdown")]
        //[HttpGet]
        //public IEnumerable<Premises> StoresDropdown()
        //{
        //    List<Premises> storesDropdown = null;
        //    try
        //    {
        //        List<Premisess> dropdown = new Catalog().AllStores();
        //        storesDropdown = new List<Premises>();

        //        if (dropdown != null)
        //        {
        //            foreach (var dbr in dropdown)
        //            {
        //                Premises li = new Premises();
        //                li.id = dbr.id;
        //                li.Name = dbr.Name;
        //                storesDropdown.Add(li);
        //            }
        //        }
        //        return storesDropdown;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //[CustomAuthorize]
        //[Route("api/ApiDropdown/ShopsDropdown")]
        //[HttpGet]
        //public IEnumerable<Premises> ShopsDropdown()
        //{
        //    List<Premises> shopsDropdown = null;
        //    try
        //    {
        //        List<Premisess> dropdown = new Catalog().AllShops();
        //        shopsDropdown = new List<Premises>();

        //        if (dropdown != null)
        //        {
        //            foreach (var dbr in dropdown)
        //            {
        //                Premises li = new Premises();
        //                li.id = dbr.id;
        //                li.Name = dbr.Name;
        //                shopsDropdown.Add(li);
        //            }
        //        }
        //        return shopsDropdown;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        [Route("api/ApiDropdown/ContactByTypeDropdown")]
        [HttpPost]
        public IEnumerable<Contact> ContactByTypeDropdown()
        {
            Contact contact = null;
            List<Contact> contactDropdown = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    contact = js.Deserialize<Contact>(strJson);
                    List<Contacts> dropdown = new Catalog().ContactByType(contact.Customer, contact.Vendor, contact.Employee);
                    contactDropdown = new List<Contact>();

                    if (dropdown != null)
                    {
                        foreach (var dbr in dropdown)
                        {
                            Contact li = new Contact();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            contactDropdown.Add(li);
                        }
                    }
                }
                return contactDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/ApiDropdown/PremisesByTypeDropdown")]
        [HttpPost]
        public IEnumerable<Premises> PremisesByTypeDropdown()
        {
            Premises premises = null;
            List<Premises> premisesDropdown = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    premises = js.Deserialize<Premises>(strJson);
                    List<Premisess> dropdown = new Catalog().PremisesByType(premises.Office, premises.Factory, premises.Store, premises.Shop);
                    premisesDropdown = new List<Premises>();


                    if (dropdown != null)
                    {
                        foreach (var dbr in dropdown)
                        {
                            Premises li = new Premises();
                            li.id = dbr.id;
                            li.Name = dbr.Name;
                            premisesDropdown.Add(li);
                        }
                    }
                }
                return premisesDropdown;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [CustomAuthorize]
        [Route("api/ApiDropdown/ItemRelatedDropdown")]
        [HttpPost]
        public Classes ItemRelatedDropdown()
        {
            Premises premises = null;
            List<Premises> premisesDropdown = null;
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                if (strJson != null)
                {
                    var js = new JavaScriptSerializer();
                    premises = js.Deserialize<Premises>(strJson);
                    List<Dropdowns> vendordropdown = new Catalog().VendorsDropdown();
                    List<Dropdowns> brandsdropdown = new Catalog().BrandsDropdown();
                    List<Dropdowns> categoriesdropdown = new Catalog().CategoriesDropdown();
                    List<Dropdowns> manufacturersdropdown = new Catalog().ManufacturersDropdown();
                    List<Dropdowns> unitsdropdown = new Catalog().UnitsDropdown();
                    premisesDropdown = new List<Premises>();

                    data = new Classes();
                    if (vendordropdown != null)
                    {
                        data.VendorDropdown = new List<Dropdown>();
                        foreach (var dbr in vendordropdown)
                        {
                            Dropdown li = new Dropdown();
                            li.id = dbr.id;
                            li.name = dbr.name;
                            data.VendorDropdown.Add(li);
                        }
                    }
                    if (brandsdropdown != null)
                    {
                        data.BrandDropdown = new List<Dropdown>();
                        foreach (var dbr in brandsdropdown)
                        {
                            Dropdown li = new Dropdown();
                            li.id = dbr.id;
                            li.name = dbr.name;
                            data.BrandDropdown.Add(li);
                        }
                    }
                    if (categoriesdropdown != null)
                    {
                        data.CategoriesDropdown = new List<Dropdown>();
                        foreach (var dbr in categoriesdropdown)
                        {
                            Dropdown li = new Dropdown();
                            li.id = dbr.id;
                            li.name = dbr.name;
                            data.CategoriesDropdown.Add(li);
                        }
                    }
                    if (manufacturersdropdown != null)
                    {
                        data.ManufacturersDropdown = new List<Dropdown>();
                        foreach (var dbr in manufacturersdropdown)
                        {
                            Dropdown li = new Dropdown();
                            li.id = dbr.id;
                            li.name = dbr.name;
                            data.ManufacturersDropdown.Add(li);
                        }
                    }
                    if (unitsdropdown != null)
                    {
                        data.UnitsDropdown = new List<Dropdown>();
                        foreach (var dbr in unitsdropdown)
                        {
                            Dropdown li = new Dropdown();
                            li.id = dbr.id;
                            li.name = dbr.name;
                            data.UnitsDropdown.Add(li);
                        }
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
