using G_Accounting_System.APP;
using G_Accounting_System.ENT;
using G_Accounting_System.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G_Accounting_System.Controllers
{
    public class DropdownController : Controller
    {
        // GET: Dropdown
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CountriesDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().CountriesDropdown();
            List<Dropdown> citiesDropdown = new List<Dropdown>();
            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    citiesDropdown.Add(li);
                }
                citiesDropdown.TrimExcess();
            }


            //List<string> CultureList = new List<string>();
            //CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            //foreach(CultureInfo getCultures in getCultureInfo)
            //{
            //    RegionInfo getRegionInfo = new RegionInfo(getCultures.LCID);
            //    if (!(CultureList.Contains(getRegionInfo.EnglishName))){
            //        CultureList.Add(getRegionInfo.EnglishName);
            //    }
            //}
            //CultureList.Sort();
            var result = JsonConvert.SerializeObject(citiesDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CitiesDropdown(int Country_id)
        {
            List<Dropdowns> dropdown = new Catalog().CitiesDropdown(Country_id);
            List<Dropdown> citiesDropdown = new List<Dropdown>();
            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    citiesDropdown.Add(li);
                }
                citiesDropdown.TrimExcess();
            }
            var result = JsonConvert.SerializeObject(citiesDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CompaniesDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().CompaniesDropdown();
            List<Dropdown> CompanyDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    CompanyDropdown.Add(li);
                }
                CompanyDropdown.TrimExcess();
            }
            var result = JsonConvert.SerializeObject(CompanyDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [ValidateAntiForgeryToken]
        public JsonResult CategoriesDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().CategoriesDropdown();
            List<Dropdown> CategoriesDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    CategoriesDropdown.Add(li);
                }
                CategoriesDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(CategoriesDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult BrandsDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().BrandsDropdown();
            List<Dropdown> BrandsDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    BrandsDropdown.Add(li);
                }
                BrandsDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(BrandsDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManufacturersDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().ManufacturersDropdown();
            List<Dropdown> ManufacturersDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    ManufacturersDropdown.Add(li);
                }
                ManufacturersDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(ManufacturersDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UnitsDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().UnitsDropdown();
            List<Dropdown> UnitsDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    UnitsDropdown.Add(li);
                }
                UnitsDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(UnitsDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult VendorsDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().VendorsDropdown();
            List<Dropdown> VendorsDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    VendorsDropdown.Add(li);
                }
                VendorsDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(VendorsDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RolesDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().RolesDropdown();
            List<Dropdown> RolesDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    RolesDropdown.Add(li);
                }
                RolesDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(dropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ItemsDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().ItemsDropdown();
            List<Dropdown> ItemsDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    ItemsDropdown.Add(li);
                }
                ItemsDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(ItemsDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CustomersDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().CustomersDropdown();
            List<Dropdown> CustomersDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    CustomersDropdown.Add(li);
                }
                CustomersDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(CustomersDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PaymentModesDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().PaymentModesDropdown();
            List<Dropdown> PaymentModesDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    PaymentModesDropdown.Add(li);
                }
                PaymentModesDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(PaymentModesDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UsersDropdown()
        {
            List<Dropdowns> dropdown = new Catalog().UsersDropdown();
            List<Dropdown> UsersDropdown = new List<Dropdown>();

            if (dropdown != null)
            {
                foreach (var dbr in dropdown)
                {
                    Dropdown li = new Dropdown();
                    li.id = dbr.id;
                    li.name = dbr.name;
                    UsersDropdown.Add(li);
                }
                UsersDropdown.TrimExcess();
            }

            var result = JsonConvert.SerializeObject(UsersDropdown);

            return Json(new { Response = result }, JsonRequestBehavior.DenyGet);
        }
    }
}