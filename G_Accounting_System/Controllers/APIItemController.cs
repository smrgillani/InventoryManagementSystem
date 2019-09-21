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
    public class APIItemController : ApiController
    {
        [Route("api/APIItem/GetAllItems")]
        [HttpPost]
        public IEnumerable<Item> GetAllItems()
        {
            List<Item> items = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                //HttpRequest resolveRequest = HttpContext.Current.Request;
                //List<YourModel> model = new List<YourModel>();
                //resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
                //string Search = new StreamReader(resolveRequest.InputStream).ReadToEnd();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Items> item = new Catalog().AllItems(search.Option, search.Search, search.StartDate, search.EndDate);

                items = new List<Item>();

                if (item != null)
                {
                    foreach (var dbr in item)
                    {
                        Item li = new Item();
                        li.id = dbr.id;
                        li.Item_type = dbr.Item_type;
                        li.File_Name = dbr.File_Name;
                        li.Item_Name = dbr.Item_Name;
                        li.Item_Sku = dbr.Item_Sku;
                        li.Category_id = dbr.Category_id;
                        li.Item_Category = dbr.Item_Category;
                        li.Unit_id = dbr.Unit_id;
                        li.Item_Unit = dbr.Item_Unit;
                        li.Manufacturer_id = dbr.Manufacturer_id;
                        li.Item_Manufacturer = dbr.Item_Manufacturer;
                        li.Item_Upc = dbr.Item_Upc;
                        li.Brand_id = dbr.Brand_id;
                        li.Item_Brand = dbr.Item_Brand;
                        li.Item_Mpn = dbr.Item_Mpn;
                        li.Item_Ean = dbr.Item_Ean;
                        li.Item_Isbn = dbr.Item_Isbn;
                        li.Item_Sell_Price = dbr.Item_Sell_Price;
                        li.Item_Tax = dbr.Item_Tax;
                        li.Item_Purchase_Price = dbr.Item_Purchase_Price;
                        li.Vendor_id = dbr.Vendor_id;
                        li.Item_Preferred_Vendor = dbr.Item_Preferred_Vendor;
                        li.Delete_Request_By = dbr.Delete_Request_By;
                        li.Delete_Status = dbr.Delete_Status;
                        items.Add(li);
                    }
                }

                items.TrimExcess();

                return items;
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

        [Route("api/APIItem/ItemById")]
        [HttpPost]
        public Classes ItemById()
        {
            Item item = null;
            Stocks stock = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                item = js.Deserialize<Item>(strJson);



                Items items = new Catalog().SelectItem(Convert.ToInt32(item.id), null);
                Stocks stocks = new Catalog().SelectStockByItemid(item.id);

                Classes data = new Classes();
                data.Item = new Item();
                data.Stock = new Stock();

                data.Item.id = items.id;
                data.Item.File_Name = items.File_Name;
                data.Item.Item_type = items.Item_type;
                data.Item.File_Name = items.File_Name;
                data.Item.Item_Name = items.Item_Name;
                data.Item.Item_Sku = items.Item_Sku;
                data.Item.Category_id = items.Category_id;
                data.Item.Item_Category = items.Item_Category;
                data.Item.Item_Unit = items.Item_Unit;
                data.Item.Unit_id = items.Unit_id;
                data.Item.Item_Manufacturer = items.Item_Manufacturer;
                data.Item.Manufacturer_id = items.Manufacturer_id;
                data.Item.Item_Upc = items.Item_Upc;
                data.Item.Item_Brand = items.Item_Brand;
                data.Item.Brand_id = items.Brand_id;
                data.Item.Item_Mpn = items.Item_Mpn;
                data.Item.Item_Ean = items.Item_Ean;
                data.Item.Item_Isbn = items.Item_Isbn;
                data.Item.Item_Sell_Price = items.Item_Sell_Price.ToString();
                data.Item.Item_Tax = items.Item_Tax;
                data.Item.Item_Purchase_Price = items.Item_Purchase_Price;
                data.Item.Vendor_id = items.Vendor_id;
                data.Item.Item_Preferred_Vendor = items.Item_Preferred_Vendor;
                data.Stock.Physical_Quantity = stocks.Physical_Quantity;
                data.Stock.Physical_Avail_ForSale = stocks.Physical_Avail_ForSale;
                data.Stock.Physical_Committed = stocks.Physical_Committed;
                data.Stock.Accounting_Quantity = stocks.Accounting_Quantity;
                data.Stock.Acc_Avail_ForSale = stocks.Acc_Avail_ForSale;
                data.Stock.Acc_Commited = stocks.Acc_Commited;


                int ActivityType_id = item.id;
                string ActivityType = "Item";
                data.Activity = new ActivitiesClass().Activities(ActivityType_id, ActivityType);

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIItem/InsertUpdateItems")]
        [HttpPost]
        public Classes InsertUpdateItems()
        {
            string ActivityName = null;
            Classes data = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    data = new Classes();
                    var js = new JavaScriptSerializer();
                    data = js.Deserialize<Classes>(strJson);

                    byte[] imageBytes = Convert.FromBase64String(data.Item.base64);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image image = Image.FromStream(ms, true);

                    var filename = data.Item.Item_Name + "-" + DateTime.Now.ToString("yyyyMMdd") + ".jpg";
                    var filepath = "/Images/ItemImages/" + filename;
                    data.Item.File_Name = filepath;
                    Items AddItem = new Items();
                    AddItem.id = data.Item.id;
                    AddItem.Item_type = data.Item.Item_type;
                    AddItem.ImagePath = filepath;
                    AddItem.Item_Name = data.Item.Item_Name;
                    AddItem.Item_Sku = data.Item.Item_Sku;
                    AddItem.Item_Category = data.Item.Item_Category;
                    AddItem.Item_Unit = data.Item.Item_Unit;
                    AddItem.Item_Manufacturer = data.Item.Item_Manufacturer;
                    AddItem.Item_Upc = data.Item.Item_Upc;
                    AddItem.Item_Brand = data.Item.Item_Brand;
                    AddItem.Item_Mpn = data.Item.Item_Mpn;
                    AddItem.Item_Ean = data.Item.Item_Ean;
                    AddItem.Item_Isbn = data.Item.Item_Isbn;
                    AddItem.Item_Sell_Price = data.Item.Item_Sell_Price;
                    AddItem.Item_Tax = data.Item.Item_Tax;
                    AddItem.Item_Purchase_Price = data.Item.Item_Purchase_Price;
                    AddItem.Item_Preferred_Vendor = data.Item.Item_Preferred_Vendor;

                    var User_id = HttpContext.Current.User.Identity.Name;
                    if (data.Item.id == 0)
                    {
                        AddItem.AddedBy = Convert.ToInt32(User_id);
                        AddItem.UpdatedBy = 0;
                    }
                    else
                    {
                        AddItem.UpdatedBy = Convert.ToInt32(User_id);
                        AddItem.AddedBy = 0;
                    }

                    new Catalog().InsertUpdateItem(AddItem);
                    data.Item.pFlag = AddItem.pFlag;
                    data.Item.pDesc = AddItem.pDesc;
                    data.Item.pItem_id_Out = AddItem.pItem_id_Out;
                    if (data.Item.pFlag == "1")
                    {
                        //HttpContext.Current.Server.MapPath("~" + filepath);
                        var filePath = HttpContext.Current.Server.MapPath("~" + filepath);

                        image.Save(filePath);


                        foreach (var dbr in data.Stocks)
                        {
                            Stocks li = new Stocks();
                            List<Stocks> stocks = new List<Stocks>();
                            li.Stock_id = dbr.Stock_id;
                            li.Item_id = Convert.ToInt32(data.Item.pItem_id_Out);
                            li.Physical_Quantity = "0";
                            li.Physical_Avail_ForSale = "0";
                            li.Physical_Committed = "0";
                            li.Accounting_Quantity = "0";
                            li.Acc_Avail_ForSale = "0";
                            li.Acc_Commited = "0";
                            li.OpeningStock = dbr.OpeningStock;
                            li.ReorderLevel = dbr.ReorderLevel;
                            stocks.Add(li);

                            stocks.TrimExcess();

                            string Result = new Catalog().UpdateItemStock(stocks);
                            data.Stock = new Stock();
                            data.Stock.Item_id = Convert.ToInt32(data.Item.pItem_id_Out);

                        }

                        Activity activity = new Activity();
                        if (data.Item.id == 0)
                        {
                            ActivityName = "Created";
                            activity.ActivityType_id = Convert.ToInt32(data.Item.pItem_id_Out);
                        }
                        else
                        {
                            ActivityName = "Updated";
                            activity.ActivityType_id = data.Item.id;
                        }
                        activity.ActivityType = "Item";
                        activity.ActivityName = ActivityName;
                        activity.User_id = Convert.ToInt32(User_id);
                        activity.Icon = "fa fa-fw fa-floppy-o bg-blue";
                        List<Activity> activities = new List<Activity>();
                        activities.Add(activity);


                        new ActivitiesClass().InsertActivity(activities);
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Route("api/APIItem/ItemsTransactionSO")]
        [HttpGet]
        public IEnumerable<SalesOrder> ItemsTransactionSO()
        {
            Item item = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                item = js.Deserialize<Item>(strJson);



                List<SalesOrders> saleorders = new Catalog().ItemsTransactionSO(item.id, null);

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
                        li.SO_Status = dbr.SO_Status;
                        li.SO_DateTime = dbr.SO_DateTime;
                        saleorder.Add(li);
                    }
                }

                saleorder.TrimExcess();


                return saleorder;
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

        [Route("api/APIItem/ItemsTransactionINV")]
        [HttpGet]
        public IEnumerable<SO_Invoice> ItemsTransactionINV()
        {
            Item item = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                item = js.Deserialize<Item>(strJson);

                List<SO_Invoices> invoices = new Catalog().ItemsTransactionINV(item.id, null);

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
                        li.Invoice_Status = dbr.Invoice_Status;
                        li.InvoiceDateTime = dbr.InvoiceDateTime;
                        invoice.Add(li);
                    }
                }

                invoice.TrimExcess();

                return invoice;
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

        [Route("api/APIItem/ItemsTransactionPO")]
        [HttpGet]
        public IEnumerable<Purchase> ItemsTransactionPO()
        {
            Item item = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                item = js.Deserialize<Item>(strJson);

                List<Purchases> purchases = new Catalog().ItemsTransactionPO(item.id, null);

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
                        li.RecieveStatus = dbr.RecieveStatus;
                        li.RecieveDateTime = dbr.RecieveDateTime;
                        purchase.Add(li);
                    }
                }

                purchase.TrimExcess();

                return purchase;
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

        [Route("api/APIItem/ItemsTransactionBill")]
        [HttpGet]
        public IEnumerable<Bill> ItemsTransactionBill()
        {
            Item item = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                item = js.Deserialize<Item>(strJson);

                List<Bills> bills = new Catalog().ItemsTransactionBill(item.id, null);

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
                        li.Bill_Status = dbr.Bill_Status;
                        li.BillDateTime = dbr.BillDateTime;
                        bill.Add(li);
                    }
                }

                bill.TrimExcess();

                return bill;
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

        [Route("api/APIItem/SendRequestToDelItems")]
        [HttpPost]
        public object SendRequestToDelItems()
        {
            List<Items> items = null;
            List<Item> itemsNotDelete = null;
            List<Item> itemsrequestedtoDelete = null;

            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                var js = new JavaScriptSerializer();
                itemsrequestedtoDelete = js.Deserialize<List<Item>>(strJson);

                var User_id = HttpContext.Current.User.Identity.Name;
                if (strJson != null)
                {
                    items = new List<Items>();
                    itemsNotDelete = new List<Item>();
                    var RequestBy = HttpContext.Current.User.Identity.Name;

                    foreach (var dbr in itemsrequestedtoDelete)
                    {
                        Items li = new Items();
                        Items liChecked = new Items();
                        li.id = dbr.id;
                        li.Item_Name = dbr.Item_Name;
                        li.Enable = 0;
                        li.Delete_Request_By = Convert.ToInt32(RequestBy);
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
                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/CSV/Items/" + filename), fileContent.ToString());

                        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/CSV/Items/"));

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("salmanahmed635@gmail.com", "unforgetable");
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
                        smtpClient.Send(mailMessage);

                        string type = "Items";
                        new Catalog().DelItemRequest(items, type);
                    }

                }
                return new { items, itemsNotDelete };
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

