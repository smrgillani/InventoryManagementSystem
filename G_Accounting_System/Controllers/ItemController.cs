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
using G_Accounting_System.Code.Helpers;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        [PermissionsAuthorize]
        public ActionResult Items()
        {
            return View("Items");
        }

        public ActionResult Add()
        {
            bool check = new PermissionsClass().CheckAddPermission();
            if (check != false)
            {
                return View("Item");
            }
            else
            {
                return Json(check, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertUpdateItem(string ItemData)
        {
            Item item = null;
            var filepath = "";
            Image image = null;
            string ActivityName = null;
            try
            {
                var js = new JavaScriptSerializer();
                item = js.Deserialize<Item>(ItemData);

                if (!String.IsNullOrEmpty(item.base64))
                {
                    //Convert Image From Base64 to Image
                    byte[] imageBytes = Convert.FromBase64String(item.base64);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    image = Image.FromStream(ms, true);

                    var filename = item.Item_Name + "-" + DateTime.Now.ToString("yyyyMMdd") + ".jpg";
                    filepath = "/Images/ItemImages/" + filename;
                }

                Items AddItem = new Items();
                AddItem.id = item.id;
                AddItem.Item_type = item.Item_type;
                AddItem.ImagePath = filepath;
                AddItem.Item_Name = item.Item_Name;
                AddItem.Item_Sku = item.Item_Sku;
                AddItem.Item_Category = item.Item_Category;
                AddItem.Item_Unit = item.Item_Unit;
                AddItem.Item_Manufacturer = item.Item_Manufacturer;
                AddItem.Item_Upc = item.Item_Upc;
                AddItem.Item_Brand = item.Item_Brand;
                AddItem.Item_Mpn = item.Item_Mpn;
                AddItem.Item_Ean = item.Item_Ean;
                AddItem.Item_Isbn = item.Item_Isbn;
                AddItem.Item_Sell_Price = item.Item_Sell_Price;
                AddItem.Item_Tax = item.Item_Tax;
                AddItem.Item_Purchase_Price = item.Item_Purchase_Price;
                AddItem.Item_Preferred_Vendor = item.Item_Preferred_Vendor;
                AddItem.Enable = 1;

                Brands brand = new Catalog().SelectBrand(Convert.ToInt32(item.Item_Brand));
                Categories category = new Catalog().SelectCategory(Convert.ToInt32(item.Item_Category));
                Manufacturers manufacturer = new Catalog().SelectManufacture(Convert.ToInt32(item.Item_Manufacturer));
                Units unit = new Catalog().SelectUnit(Convert.ToInt32(item.Item_Unit));
                Contacts vendor = new Catalog().SelectContact(Convert.ToInt32(item.Item_Preferred_Vendor), 1, 0, 0, null);

                if (brand != null && category != null && manufacturer != null && unit != null && vendor != null)
                {
                    if (item.id == 0)
                    {
                        AddItem.AddedBy = Convert.ToInt32(Session["UserId"]);
                        AddItem.UpdatedBy = 0;
                    }
                    else
                    {
                        AddItem.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        AddItem.AddedBy = 0;
                    }

                    new Catalog().InsertUpdateItem(AddItem);
                    item.pFlag = AddItem.pFlag;
                    item.pDesc = AddItem.pDesc;
                    item.pItem_id_Out = AddItem.pItem_id_Out;
                    if (item.pFlag == "1")
                    {
                        if (filepath != "")
                        {
                            image.Save(Server.MapPath("~" + filepath));
                        }
                        Items check = new Catalog().CheckItemForDelete(Convert.ToInt32(item.id));
                        if (check == null)
                        {
                            List<Stocks> stocks = new List<Stocks>();
                            Stocks li = new Stocks();
                            Stocks stock = new Catalog().SelectStockByItemid(Convert.ToInt32(item.pItem_id_Out));
                            if (stock != null)
                            {
                                li.Stock_id = stock.Stock_id;
                            }
                            else
                            {
                                li.Stock_id = 0;
                            }
                            li.Item_id = Convert.ToInt32(item.pItem_id_Out);
                            li.Physical_Quantity = item.OpeningStock;
                            li.Physical_Avail_ForSale = item.OpeningStock;
                            li.Physical_Committed = "0";
                            li.Accounting_Quantity = item.OpeningStock;
                            li.Acc_Avail_ForSale = item.OpeningStock;
                            li.Acc_Commited = "0";
                            li.OpeningStock = item.OpeningStock;
                            li.ReorderLevel = item.ReorderLevel;
                            stocks.Add(li);
                            stocks.TrimExcess();

                            string Result = new Catalog().UpdateItemStock(stocks);
                        }
                        Activity activity = new Activity();
                        if (item.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(item.pItem_id_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = item.id;
                        }
                        activity.ActivityType = "Item";
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
                    item.pFlag = "0";
                    item.pDesc = "Provided Data is Incorrect";
                }
            }
            catch (Exception e)
            {
                item.pFlag = "0";
                item.pDesc = e.Message;
            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllItems(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Items> item = new Catalog().AllItems(search.Option, search.Search, search.StartDate, search.EndDate);

            List<Item> items = new List<Item>();

            if (item != null)
            {
                foreach (var dbr in item)
                {
                    Item li = new Item();
                    li.id = dbr.id;
                    li.Item_type = dbr.Item_type;
                    li.ImagePath = dbr.ImagePath;
                    li.Item_Name = dbr.Item_Name;
                    li.Item_Sku = dbr.Item_Sku;
                    li.Item_Category = dbr.Item_Category;
                    li.Item_Unit = dbr.Item_Unit;
                    li.Item_Manufacturer = dbr.Item_Manufacturer;
                    li.Item_Upc = dbr.Item_Upc;
                    li.Item_Brand = dbr.Item_Brand;
                    li.Item_Mpn = dbr.Item_Mpn;
                    li.Item_Ean = dbr.Item_Ean;
                    li.Item_Isbn = dbr.Item_Isbn;
                    li.Item_Sell_Price = dbr.Item_Sell_Price;
                    li.Item_Tax = dbr.Item_Tax;
                    li.Item_Purchase_Price = dbr.Item_Purchase_Price;
                    li.Item_Preferred_Vendor = dbr.Item_Preferred_Vendor;
                    li.IsEnabled_ = (dbr.Enable == 1) ? "Active" : "InActive";
                    li.Delete_Request_By = dbr.Delete_Request_By;
                    li.Delete_Status = dbr.Delete_Status;
                    items.Add(li);
                }
            }

            items.TrimExcess();
            var pitems = items.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = items.Count, recordsFiltered = items.Count, data = pitems }, JsonRequestBehavior.AllowGet);
        }

        [PermissionsAuthorize]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Profile(int id)
        {
            Items items = new Catalog().SelectItem(Convert.ToInt32(id), null);
            Item item = null;
            string Response = null;
            try
            {
                Stock stock = null;
                if (items != null)
                {
                    Stocks stocks = new Catalog().SelectStockByItemid(id);
                    stock = new Stock();
                    item = new Item();
                    item.id = items.id;
                    item.File_Name = items.File_Name;
                    item.Item_type = items.Item_type;
                    item.ImagePath = items.ImagePath;
                    item.Item_Name = items.Item_Name;
                    item.Item_Sku = items.Item_Sku;
                    item.Item_Category = items.Item_Category;
                    item.Category_id = items.Category_id;
                    item.Item_Unit = items.Item_Unit;
                    item.Unit_id = items.Unit_id;
                    item.Item_Manufacturer = items.Item_Manufacturer;
                    item.Manufacturer_id = items.Manufacturer_id;
                    item.Item_Upc = items.Item_Upc;
                    item.Item_Brand = items.Item_Brand;
                    item.Brand_id = items.Brand_id;
                    item.Item_Mpn = items.Item_Mpn;
                    item.Item_Ean = items.Item_Ean;
                    item.Item_Isbn = items.Item_Isbn;
                    item.Item_Sell_Price = items.Item_Sell_Price.ToString();
                    item.Item_Tax = items.Item_Tax;
                    item.Item_Purchase_Price = items.Item_Purchase_Price;
                    item.Item_Preferred_Vendor = items.Item_Preferred_Vendor;
                    item.Vendor_id = items.Vendor_id;
                    item.Enable = items.Enable;
                    item.IsEnabled_ = (items.Enable == 1) ? "Active" : "InActive";
                    item.Delete_Status = items.Delete_Status;

                    stock.Physical_Quantity = stocks.Physical_Quantity;
                    stock.Physical_Avail_ForSale = stocks.Physical_Avail_ForSale;
                    stock.Physical_Committed = stocks.Physical_Committed;
                    stock.Accounting_Quantity = stocks.Accounting_Quantity;
                    stock.Acc_Avail_ForSale = stocks.Acc_Avail_ForSale;
                    stock.Acc_Commited = stocks.Acc_Commited;
                    stock.ReorderLevel = stocks.ReorderLevel;
                    return Json(new { item, stock }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response = "Provided Data is Incorrect";
                    return Json(Response, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Response = e.Message;
                return Json(Response, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(int id)
        {
            Items check = null;
            Item item = null;
            try
            {
                bool checks = new PermissionsClass().CheckEditPermission();
                if (checks != false)
                {
                    Items items = new Catalog().SelectItem(Convert.ToInt32(id), null);
                    check = new Catalog().CheckItemForDelete(Convert.ToInt32(id));

                    item = new Item();
                    item.id = items.id;
                    item.Item_type = items.Item_type;
                    item.Item_Name = items.Item_Name;
                    item.Item_Sku = items.Item_Sku;
                    item.Item_Category = items.Item_Category;
                    item.Category_id = items.Category_id;
                    item.Item_Unit = items.Item_Unit;
                    item.Unit_id = items.Unit_id;
                    item.Item_Manufacturer = items.Item_Manufacturer;
                    item.Manufacturer_id = items.Manufacturer_id;
                    item.Item_Upc = items.Item_Upc;
                    item.Item_Brand = items.Item_Brand;
                    item.Brand_id = items.Brand_id;
                    item.Item_Mpn = items.Item_Mpn;
                    item.Item_Ean = items.Item_Ean;
                    item.Item_Isbn = items.Item_Isbn;
                    item.Item_Sell_Price = items.Item_Sell_Price.ToString();
                    item.Item_Tax = items.Item_Tax;
                    item.Item_Purchase_Price = items.Item_Purchase_Price;
                    item.Item_Preferred_Vendor = items.Item_Preferred_Vendor;
                    item.Vendor_id = items.Vendor_id;
                    item.Stock_id = items.Stock_id;
                    item.OpeningStock = items.OpeningStock;
                    item.ReorderLevel = items.ReorderLevel;
                }
                else
                {
                    return Json(checks, JsonRequestBehavior.DenyGet);
                }
                return Json(new { item, check }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { item, check }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Updatep(int id)
        {
            string ActivityName = null;
            string response = "";
            Item ite = null;
            try
            {
                Items items = new Catalog().SelectItem(id, null);
                ite = new Item();
                if (items == null)
                {
                    response = "Please Select A Valid Item";
                }
                else
                {
                    Items item = new Items();
                    item.id = id;

                    if (items.Enable == 1)
                    {
                        item.Enable = 0;
                    }
                    else
                    {
                        item.Enable = 1;
                    }

                    new Catalog().UpdatepItem(item);
                    ite.Enable = item.Enable;
                    Activity activity = new Activity();
                    if (ite.Enable == 1)
                    {
                        ActivityName = "Marked as Active";
                    }
                    else if (ite.Enable == 0)
                    {
                        ActivityName = "Marked as Inactive";
                    }
                    activity.ActivityType_id = id;
                    activity.ActivityType = "Item";
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

            return Json(ite, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Del(int id)
        {
            string response = "";
            try
            {
                new Catalog().DelItem(id);
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult GetItemWS(string Search)
        //{

        //    List<Items> item = new Catalog().AllItems(Search);

        //    List<Select2Model> items = new List<Select2Model>();

        //    if (item != null)
        //    {
        //        foreach (var dbr in item)
        //        {
        //            Select2Model li = new Select2Model();
        //            li.id = dbr.id;
        //            li.text = dbr.Item_Name;
        //            items.Add(li);
        //        }
        //    }

        //    items.TrimExcess();

        //    return Json(new { items = items }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ItemsTransactionSO(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<SalesOrders> saleorders = new Catalog().ItemsTransactionSO(Convert.ToInt32(search.Option), search.Search);

            List<SalesOrder> saleorder = new List<SalesOrder>();

            if (saleorders != null)
            {
                foreach (var dbr in saleorders)
                {
                    SalesOrder li = new SalesOrder();
                    li.SalesOrder_id = dbr.SalesOrder_id;
                    li.SaleOrderNo = dbr.SaleOrderNo;
                    li.Customer_id = dbr.Customer_id;
                    li.Customer_Name = dbr.Customer_Name;
                    li.PriceUnit = dbr.PriceUnit;
                    li.ItemQty = dbr.ItemQty;
                    li.SO_Total_Amount = (dbr.PriceUnit * Convert.ToDouble(dbr.ItemQty)).ToString();
                    li.SO_Status = dbr.SO_Status;
                    li.SO_DateTime = dbr.SO_DateTime;
                    saleorder.Add(li);
                }
            }

            saleorder.TrimExcess();
            var psaleorder = saleorder.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = saleorder.Count, recordsFiltered = saleorder.Count, data = psaleorder }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ItemsTransactionINV(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<SO_Invoices> invoices = new Catalog().ItemsTransactionINV(Convert.ToInt32(search.Option), search.Search);

            List<SO_Invoice> invoice = new List<SO_Invoice>();

            if (invoices != null)
            {
                foreach (var dbr in invoices)
                {
                    SO_Invoice li = new SO_Invoice();
                    li.id = dbr.SalesOrder_id;
                    li.Invoice_No = dbr.Invoice_No;
                    li.Customer_id = dbr.Customer_id;
                    li.Customer_Name = dbr.Customer_Name;
                    li.PriceUnit = dbr.PriceUnit;
                    li.ItemQty = dbr.ItemQty;
                    li.Invoice_Amount = Convert.ToDecimal(dbr.PriceUnit * Convert.ToDouble(dbr.ItemQty));
                    li.Invoice_Status = dbr.Invoice_Status;
                    li.InvoiceDateTime = dbr.InvoiceDateTime;
                    invoice.Add(li);
                }
            }

            invoice.TrimExcess();
            var pinvoice = invoice.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = invoice.Count, recordsFiltered = invoice.Count, data = pinvoice }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ItemsTransactionPO(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Purchases> purchases = new Catalog().ItemsTransactionPO(Convert.ToInt32(search.Option), search.Search);

            List<Purchase> purchase = new List<Purchase>();

            if (purchases != null)
            {
                foreach (var dbr in purchases)
                {
                    Purchase li = new Purchase();
                    li.id = dbr.id;
                    li.TempOrderNum = dbr.TempOrderNum;
                    li.VendorId = dbr.VendorId;
                    li.VendorName = dbr.VendorName;
                    li.PriceUnit = dbr.PriceUnit;
                    li.ItemQty = dbr.ItemQty;
                    li.TotalPrice = Convert.ToInt32(dbr.PriceUnit * Convert.ToDouble(dbr.ItemQty));
                    li.RecieveStatus = dbr.RecieveStatus;
                    li.RecieveDateTime = dbr.RecieveDateTime;
                    purchase.Add(li);
                }
            }

            purchase.TrimExcess();
            var ppurchase = purchase.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = purchase.Count, recordsFiltered = purchase.Count, data = ppurchase }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ItemsTransactionBill(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Bills> bills = new Catalog().ItemsTransactionBill(Convert.ToInt32(search.Option), search.Search);

            List<Bill> bill = new List<Bill>();

            if (bills != null)
            {
                foreach (var dbr in bills)
                {
                    Bill li = new Bill();
                    li.id = dbr.id;
                    li.Bill_No = dbr.Bill_No;
                    li.Vendor_id = dbr.Vendor_id;
                    li.Vendor_Name = dbr.Vendor;
                    li.PriceUnit = dbr.PriceUnit;
                    li.ItemQty = dbr.ItemQty;
                    li.Bill_Amount = Convert.ToDecimal(dbr.PriceUnit * Convert.ToDouble(dbr.ItemQty));
                    li.Bill_Status = dbr.Bill_Status;
                    li.BillDateTime = dbr.BillDateTime;
                    bill.Add(li);
                }
            }

            bill.TrimExcess();
            var pbill = bill.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = bill.Count, recordsFiltered = bill.Count, data = pbill }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Import()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult GetUploadedFileData(HttpPostedFileWrapper file)
        //{
        //    DataTable res = null;
        //    List<Item> items = null;
        //    try
        //    {
        //        if (file != null)
        //        {
        //            file.SaveAs(Server.MapPath("/Import/" + file.FileName));

        //            var path = Server.MapPath("/Import/" + file.FileName);
        //            res = ConvertCSVtoDataTable(path);
        //            items = new List<Item>();

        //            foreach (DataRow dr in res.Rows)
        //            {
        //                Item li = new Item();
        //                li.id = Convert.ToInt32(dr["Item_id"]);
        //                li.Item_type = Convert.ToString(dr["Item_type"]);
        //                li.File_Name = Convert.ToString(dr["Item_file"]);
        //                li.Item_Name = Convert.ToString(dr["Item_Name"]);
        //                li.Item_Sku = Convert.ToString(dr["Item_Sku"]);
        //                li.Item_Category = Convert.ToString(dr["Item_Category"]);
        //                li.Item_Unit = Convert.ToString(dr["Item_Unit"]);
        //                li.Item_Manufacturer = Convert.ToString(dr["Item_Manufacturer"]);
        //                li.Item_Upc = Convert.ToString(dr["Item_Upc"]);
        //                li.Item_Brand = Convert.ToString(dr["Item_Brand"]);
        //                li.Item_Mpn = Convert.ToString(dr["Item_Mpn"]);
        //                li.Item_Ean = Convert.ToString(dr["Item_Ean"]);
        //                li.Item_Isbn = Convert.ToString(dr["Item_Isbn"]);
        //                li.Item_Sell_Price = Convert.ToString(dr["Item_Sell_Price"]);
        //                li.Item_Tax = Convert.ToString(dr["Item_Tax"]);
        //                li.Item_Purchase_Price = Convert.ToString(dr["Item_Purchase_Price"]);
        //                li.Item_Preferred_Vendor = Convert.ToString(dr["Item_Preferred_Vendor"]);
        //                li.AddedBy = Convert.ToInt32(Session["UserId"]);
        //                li.Enable = Convert.ToInt32(dr["Enable"]);
        //                li.Time = Convert.ToString(dr["Time_Of_Day"]);
        //                li.Date = Convert.ToString(dr["Date_Of_Day"]);
        //                li.Month = Convert.ToString(dr["Month_Of_Day"]);
        //                li.Year = Convert.ToString(dr["Year_Of_Day"]);
        //                items.Add(li);
        //            }
        //            items.TrimExcess();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return Json(new { Items = items }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult ImportItems(string ImportItemsData)
        //{
        //    List<Item> itemlist = null;
        //    List<Item> ItemsNotAdded = null;
        //    bool flag = false;
        //    try
        //    {
        //        var js = new JavaScriptSerializer();
        //        itemlist = js.Deserialize<List<Item>>(ImportItemsData);
        //        ItemsNotAdded = new List<Item>();
        //        foreach (var dbr in itemlist)
        //        {
        //            Items li = new Items();
        //            if (!String.IsNullOrEmpty(dbr.Item_Category))
        //            {
        //                Categories categories = new Catalog().CategoryByName(dbr.Item_Category);
        //                if (categories == null)
        //                {
        //                    Categories AddCategory = new Categories();
        //                    AddCategory.id = 0;
        //                    AddCategory.Category_Name = dbr.Item_Category;
        //                    AddCategory.Enable = 1;
        //                    AddCategory.AddedBy = Convert.ToInt32(Session["UserId"]);
        //                    AddCategory.UpdatedBy = 0;
        //                    new Catalog().InsertUpdateCategory(AddCategory);
        //                    if (AddCategory.pFlag == "1")
        //                    {
        //                        Activity activity = new Activity();
        //                        activity.ActivityType_id = Convert.ToInt32(AddCategory.pCategoryid_Out);
        //                        activity.ActivityType = "Category";
        //                        activity.ActivityName = "Created";
        //                        activity.User_id = Convert.ToInt32(Session["UserId"]);
        //                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
        //                        List<Activity> activities = new List<Activity>();
        //                        activities.Add(activity);
        //                        new ActivitiesClass().InsertActivity(activities);
        //                    }
        //                }
        //                else
        //                {
        //                    int Category_id = categories.id;
        //                    li.Category_id = Category_id;
        //                }

        //            }
        //            if (!String.IsNullOrEmpty(dbr.Item_Unit))
        //            {
        //                Units units = new Catalog().UnitByName(dbr.Item_Unit);
        //                if (units == null)
        //                {
        //                    Units AddUnit = new Units();
        //                    AddUnit.id = 0;
        //                    AddUnit.Unit_Name = dbr.Item_Unit;
        //                    AddUnit.Enable = 1;
        //                    AddUnit.AddedBy = Convert.ToInt32(Session["UserId"]);
        //                    AddUnit.UpdatedBy = 0;
        //                    new Catalog().InsertUpdateUnit(AddUnit);
        //                    if (AddUnit.pFlag == "1")
        //                    {
        //                        Activity activity = new Activity();
        //                        activity.ActivityType_id = Convert.ToInt32(AddUnit.pUnitid_Out);
        //                        activity.ActivityType = "Unit";
        //                        activity.ActivityName = "Created";
        //                        activity.User_id = Convert.ToInt32(Session["UserId"]);
        //                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
        //                        List<Activity> activities = new List<Activity>();
        //                        activities.Add(activity);
        //                        new ActivitiesClass().InsertActivity(activities);
        //                    }
        //                }
        //                else
        //                {
        //                    int Unit_id = units.id;
        //                    li.Unit_id = Unit_id;
        //                }
        //            }
        //            if (!String.IsNullOrEmpty(dbr.Item_Manufacturer))
        //            {
        //                Manufacturers manufacturers = new Catalog().ManufacturerByName(dbr.Item_Manufacturer);
        //                if (manufacturers == null)
        //                {
        //                    Manufacturers AddManufacturer = new Manufacturers();
        //                    AddManufacturer.id = 0;
        //                    AddManufacturer.Manufacturer_Name = dbr.Item_Manufacturer;
        //                    AddManufacturer.Enable = 1;
        //                    AddManufacturer.AddedBy = Convert.ToInt32(Session["UserId"]);
        //                    AddManufacturer.UpdatedBy = 0;
        //                    new Catalog().InsertUpdateManufacturer(AddManufacturer);
        //                    if (AddManufacturer.pFlag == "1")
        //                    {
        //                        Activity activity = new Activity();
        //                        activity.ActivityType_id = Convert.ToInt32(AddManufacturer.pManufacturerid_Out);
        //                        activity.ActivityType = "Manufacturer";
        //                        activity.ActivityName = "Created";
        //                        activity.User_id = Convert.ToInt32(Session["UserId"]);
        //                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
        //                        List<Activity> activities = new List<Activity>();
        //                        activities.Add(activity);
        //                        new ActivitiesClass().InsertActivity(activities);
        //                    }
        //                }
        //                else
        //                {
        //                    int Manufacturer_id = manufacturers.id;
        //                    li.Manufacturer_id = Manufacturer_id;
        //                }
        //            }
        //            if (!String.IsNullOrEmpty(dbr.Item_Brand))
        //            {
        //                Brands brands = new Catalog().BrandByName(dbr.Item_Brand);
        //                if (brands == null)
        //                {
        //                    Brands AddBrand = new Brands();
        //                    AddBrand.id = 0;
        //                    AddBrand.Brand_Name = dbr.Item_Brand;
        //                    AddBrand.AddedBy = Convert.ToInt32(Session["UserId"]);
        //                    AddBrand.UpdatedBy = 0;
        //                    new Catalog().InsertUpdateBrands(AddBrand);
        //                    if (AddBrand.pFlag == "1")
        //                    {
        //                        Activity activity = new Activity();
        //                        activity.ActivityType_id = Convert.ToInt32(AddBrand.pBrandid_Out);
        //                        activity.ActivityType = "Brand";
        //                        activity.ActivityName = "Created";
        //                        activity.User_id = Convert.ToInt32(Session["UserId"]);
        //                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
        //                        List<Activity> activities = new List<Activity>();
        //                        activities.Add(activity);
        //                        new ActivitiesClass().InsertActivity(activities);
        //                    }
        //                }
        //                else
        //                {
        //                    int Brand_id = brands.id;
        //                    li.Brand_id = Brand_id;
        //                }
        //            }
        //            if (!String.IsNullOrEmpty(dbr.Item_Preferred_Vendor))
        //            {
        //                Contacts vendors = new Catalog().VendorByName(dbr.Item_Preferred_Vendor);
        //                if (vendors == null)
        //                {
        //                    flag = true;
        //                    Item notAdded = new Item();
        //                    notAdded.Item_type = dbr.Item_type;
        //                    notAdded.Item_Name = dbr.Item_Name;
        //                    notAdded.Item_Sku = dbr.Item_Sku;
        //                    notAdded.Item_Upc = dbr.Item_Upc;
        //                    notAdded.Item_Mpn = dbr.Item_Mpn;
        //                    notAdded.Item_Ean = dbr.Item_Ean;
        //                    notAdded.Item_Isbn = dbr.Item_Isbn;
        //                    notAdded.Item_Sell_Price = dbr.Item_Sell_Price;
        //                    notAdded.Item_Tax = dbr.Item_Tax;
        //                    notAdded.Item_Purchase_Price = dbr.Item_Purchase_Price;
        //                    notAdded.Item_Preferred_Vendor = dbr.Item_Preferred_Vendor;
        //                    ItemsNotAdded.Add(notAdded);
        //                }
        //                else
        //                {
        //                    int Vendor_id = vendors.id;
        //                    li.Vendor_id = Vendor_id;
        //                }
        //            }
        //            li.Item_type = dbr.Item_type;
        //            li.Item_Name = dbr.Item_Name;
        //            li.Item_Sku = dbr.Item_Sku;
        //            li.Item_Upc = dbr.Item_Upc;
        //            li.Item_Brand = dbr.Item_Brand;
        //            li.Item_Mpn = dbr.Item_Mpn;
        //            li.Item_Ean = dbr.Item_Ean;
        //            li.Item_Isbn = dbr.Item_Isbn;
        //            li.Item_Sell_Price = dbr.Item_Sell_Price;
        //            li.Item_Tax = dbr.Item_Tax;
        //            li.Item_Purchase_Price = dbr.Item_Purchase_Price;
        //            li.Item_Preferred_Vendor = dbr.Item_Preferred_Vendor;
        //            li.AddedBy = Convert.ToInt32(Session["UserId"]);
        //            li.Enable = 1;
        //            if (flag == false)
        //            {
        //                new Catalog().InsertUpdateItem(li);
        //                flag = false;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return Json(ItemsNotAdded, JsonRequestBehavior.AllowGet);
        //}



        //public static DataTable ConvertCSVtoDataTable(string strFilePath)
        //{
        //    StreamReader sr = new StreamReader(strFilePath);
        //    string[] headers = sr.ReadLine().Split(',');
        //    DataTable dt = new DataTable();
        //    foreach (string header in headers)
        //    {
        //        dt.Columns.Add(header);
        //    }
        //    while (!sr.EndOfStream)
        //    {
        //        string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
        //        DataRow dr = dt.NewRow();
        //        for (int i = 0; i < headers.Length; i++)
        //        {
        //            dr[i] = rows[i];
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendRequestToDelItems(string DeleteItemsData)
        {
            string response = "";
            List<Items> items = null;
            List<Item> itemsNotDelete = null;
            List<Activity> activities = null;
            try
            {
                var js = new JavaScriptSerializer();
                List<Item> itemsrequestedtoDelete = js.Deserialize<List<Item>>(DeleteItemsData);
                items = new List<Items>();
                activities = new List<Activity>();
                itemsNotDelete = new List<Item>();
                int RequestBy = Convert.ToInt32(Session["UserId"]);

                foreach (var dbr in itemsrequestedtoDelete)
                {
                    Items li = new Items();
                    Items liChecked = new Items();
                    li.id = dbr.id;
                    li.Item_Name = dbr.Item_Name;
                    li.Enable = 0;
                    li.Delete_Request_By = RequestBy;
                    li.Delete_Status = dbr.Delete_Status;

                    liChecked = new Catalog().CheckItemForDelete(li.id);
                    if (liChecked != null)
                    {
                        Item it = new Item();
                        it.Item_Name = liChecked.Item_Name;
                        itemsNotDelete.Add(it);
                    }
                    else
                    {
                        items.Add(li);
                    }
                }
                items.TrimExcess();
                itemsNotDelete.TrimExcess();
                if (items.Count != 0)
                {
                    DataTable dt = ToDataTable.ListToDataTable(items);

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
                    string filename = "Item-" + DateTime.Now.ToString("dd-MM-yyyy HH mm ss tt") + ".csv";
                    System.IO.File.WriteAllText(Server.MapPath("~/CSV/Items/" + filename), fileContent.ToString());

                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/CSV/Items/"));

                    SmtpClient email = new SmtpClient();
                    email.Host = "smtp.gmail.com";
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress("salmanahmed635@gmail.com");
                    mailMessage.To.Add("salmanahmed635@gmail.com");
                    mailMessage.Subject = "Delete Items";
                    mailMessage.Body = "Following Items are requested to be deleted";
                    foreach (FileInfo file in dir.GetFiles(filename))
                    {
                        if (file.Exists)
                        {
                            mailMessage.Attachments.Add(new Attachment(file.FullName));
                        }
                    }
                    email.Send(mailMessage);

                    string type = "Items";
                    string Result = new Catalog().DelItemRequest(items, type);
                    if (Result.Length > 0)
                    {
                        response = "Internal Server Error.";
                    }
                    else
                    {
                        if (items != null)
                        {
                            foreach (var i in items)
                            {
                                Activity activity = new Activity();
                                activity.ActivityType_id = i.id;
                                activity.ActivityType = "Item";
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

            return Json(new { Response = response, Items = items, ItemsNotDelete = itemsNotDelete }, JsonRequestBehavior.AllowGet);
        }
    }
}
