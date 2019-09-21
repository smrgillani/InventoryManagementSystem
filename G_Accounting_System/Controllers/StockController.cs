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
using G_Accounting_System.Code.Helpers;
using G_Accounting_System.Auth;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class StockController : Controller
    {
        //
        // GET: /Stock/
        [PermissionsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAllItemsStockList(string Search)
        {
            var js = new JavaScriptSerializer();
            SearchParameters search = js.Deserialize<SearchParameters>(Search);
            List<Stocks> stocks = new Catalog().GetAllItemsStockList(search.StartDate, search.EndDate, search.Search);

            List<Stock> stock = new List<Stock>();

            if (stocks != null)
            {
                foreach (var dbr in stocks)
                {
                    Stock li = new Stock();
                    li.Stock_id = dbr.Stock_id;
                    li.Item_id = dbr.Item_id;
                    li.Item_Name = dbr.Item_Name;
                    li.Physical_Quantity = dbr.Physical_Quantity;
                    li.Quantity_Sold = dbr.Quantity_Sold;
                    stock.Add(li);
                }
            }

            stock.TrimExcess();
            var pstock = stock.Skip(search.PageStart).Take(search.PageLength);
            return Json(new { draw = search.Draw, recordsTotal = stock.Count, recordsFiltered = stock.Count, data = pstock }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SelectStockByItemid(int Item_id)
        {
            Stocks stocks = new Catalog().SelectStockByItemid(Item_id);

            Stock stock = new Stock();
            if (stocks != null)
            {
                stock.Item_id = stocks.Item_id;
                stock.Stock_id = stocks.Stock_id;
                stock.Physical_Quantity = stocks.Physical_Quantity;
                stock.Physical_Avail_ForSale = stocks.Physical_Avail_ForSale;
                stock.Physical_Committed = stocks.Physical_Committed;
                stock.Accounting_Quantity = stocks.Accounting_Quantity;
                stock.Acc_Avail_ForSale = stocks.Acc_Avail_ForSale;
                stock.Acc_Commited = stocks.Acc_Commited;
                stock.OpeningStock = stocks.OpeningStock;
                stock.ReorderLevel = stocks.ReorderLevel;
            }
            return Json(stock, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InsertItemStock(string StockData)
        {
            var js = new JavaScriptSerializer();
            List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

            List<Stocks> stocks = new List<Stocks>();

            string response = "";

            try
            {
                if (stock.Count() < 0)
                {
                    response = "Please Add Purchased Products.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in stock)
                    {
                        Stocks li = new Stocks();

                        li.Stock_id = dbr.Stock_id;
                        li.Item_id = dbr.Item_id;
                        Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
                        if (st != null)
                        {
                            li.Physical_Quantity = Convert.ToString((Convert.ToInt32(dbr.OpeningStock) - Convert.ToInt32(st.OpeningStock)) + Convert.ToInt32(st.Physical_Quantity));
                            li.Physical_Avail_ForSale = Convert.ToString((Convert.ToInt32(dbr.OpeningStock) - Convert.ToInt32(st.OpeningStock)) + Convert.ToInt32(st.Physical_Quantity));
                            li.Physical_Committed = dbr.Physical_Committed;
                            li.Accounting_Quantity = Convert.ToString((Convert.ToInt32(dbr.OpeningStock) - Convert.ToInt32(st.OpeningStock)) + Convert.ToInt32(st.Accounting_Quantity));
                            li.Acc_Avail_ForSale = Convert.ToString((Convert.ToInt32(dbr.OpeningStock) - Convert.ToInt32(st.OpeningStock)) + Convert.ToInt32(st.Accounting_Quantity));
                            li.Acc_Commited = dbr.Acc_Commited;
                        }
                        else
                        {
                            li.Physical_Quantity = dbr.OpeningStock;
                            li.Physical_Avail_ForSale = dbr.OpeningStock;
                            li.Physical_Committed = dbr.Physical_Committed;
                            li.Accounting_Quantity = dbr.OpeningStock;
                            li.Acc_Avail_ForSale = dbr.OpeningStock;
                            li.Acc_Commited = dbr.Acc_Commited;
                        }
                        li.OpeningStock = dbr.OpeningStock;
                        li.ReorderLevel = dbr.ReorderLevel;
                        stocks.Add(li);

                        i++;
                    }

                    stocks.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateItemStock(stocks);

                        if (Result.Length > 0)
                        {
                            response = "Internal Server Error.";
                        }

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult UpdatePOitemStock(string StockData)
        //{
        //    var js = new JavaScriptSerializer();
        //    List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

        //    List<Stocks> stocks = new List<Stocks>();

        //    string response = "";

        //    try
        //    {
        //        if (stock.Count() < 0)
        //        {
        //            response = "Please Add Purchased Products.";
        //        }
        //        else
        //        {

        //            int i = 1;
        //            foreach (var dbr in stock)
        //            {
        //                Stocks li = new Stocks();

        //                li.Item_id = dbr.Item_id;
        //                Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
        //                li.Stock_id = st.Stock_id;
        //                if (dbr.Physical_Quantity == "0")
        //                {
        //                    li.Physical_Quantity = st.Physical_Quantity;
        //                    li.Physical_Avail_ForSale = st.Physical_Avail_ForSale;
        //                    li.Physical_Committed = st.Physical_Committed;

        //                    li.Accounting_Quantity = Convert.ToString(Convert.ToInt32(st.Accounting_Quantity) + Convert.ToInt32(dbr.Accounting_Quantity));
        //                    li.Acc_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Acc_Avail_ForSale) + Convert.ToInt32(dbr.Accounting_Quantity));
        //                    li.Acc_Commited = st.Acc_Commited;
        //                }
        //                else if (dbr.Accounting_Quantity == "0")
        //                {
        //                    li.Physical_Quantity = Convert.ToString(Convert.ToInt32(st.Physical_Quantity) + Convert.ToInt32(dbr.Physical_Quantity));
        //                    li.Physical_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Physical_Avail_ForSale) + Convert.ToInt32(dbr.Physical_Quantity));
        //                    li.Physical_Committed = st.Physical_Committed;

        //                    li.Accounting_Quantity = st.Accounting_Quantity;
        //                    li.Acc_Avail_ForSale = st.Acc_Avail_ForSale;
        //                    li.Acc_Commited = st.Acc_Commited;

        //                }

        //                li.OpeningStock = st.OpeningStock;
        //                li.ReorderLevel = st.ReorderLevel;
        //                stocks.Add(li);
        //            }

        //            stocks.TrimExcess();

        //            if (response.Length <= 0)
        //            {
        //                string Result = new Catalog().UpdateItemStock(stocks);

        //                if (Result.Length > 0)
        //                {
        //                    response = "Internal Server Error.";
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        response = "Internal Server Error.";
        //    }

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePOitemStockOnReceiving(string StockData)
        {
            var js = new JavaScriptSerializer();
            List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

            List<Stocks> stocks = new List<Stocks>();

            string response = "";

            try
            {
                if (stock.Count() < 0)
                {
                    response = "Please Add Purchased Products.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in stock)
                    {
                        Stocks li = new Stocks();

                        li.Item_id = dbr.Item_id;
                        Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
                        li.Stock_id = st.Stock_id;

                        li.Physical_Quantity = Convert.ToString(Convert.ToInt32(st.Physical_Quantity) + Convert.ToInt32(dbr.Physical_Quantity));
                        li.Physical_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Physical_Avail_ForSale) + Convert.ToInt32(dbr.Physical_Quantity));
                        li.Physical_Committed = st.Physical_Committed;

                        li.Accounting_Quantity = st.Accounting_Quantity;
                        li.Acc_Avail_ForSale = st.Acc_Avail_ForSale;
                        li.Acc_Commited = st.Acc_Commited;

                        li.OpeningStock = st.OpeningStock;
                        li.ReorderLevel = st.ReorderLevel;
                        stocks.Add(li);
                    }

                    stocks.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateItemStock(stocks);

                        if (Result.Length > 0)
                        {
                            response = "Internal Server Error.";
                        }

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePOitemStockOnCreateBill(string StockData)
        {
            var js = new JavaScriptSerializer();
            List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

            List<Stocks> stocks = new List<Stocks>();

            string response = "";

            try
            {
                if (stock.Count() < 0)
                {
                    response = "Please Add Purchased Products.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in stock)
                    {
                        Stocks li = new Stocks();

                        li.Item_id = dbr.Item_id;
                        Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
                        li.Stock_id = st.Stock_id;

                        li.Physical_Quantity = Convert.ToString(Convert.ToInt32(st.Physical_Quantity) + Convert.ToInt32(dbr.Physical_Quantity));
                        li.Physical_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Physical_Avail_ForSale) + Convert.ToInt32(dbr.Physical_Quantity));
                        li.Physical_Committed = st.Physical_Committed;

                        li.Accounting_Quantity = Convert.ToString(Convert.ToInt32(st.Accounting_Quantity)+ Convert.ToInt32(dbr.Accounting_Quantity));
                        li.Acc_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Acc_Avail_ForSale)+ Convert.ToInt32(dbr.Accounting_Quantity));
                        li.Acc_Commited = st.Acc_Commited;

                        li.OpeningStock = st.OpeningStock;
                        li.ReorderLevel = st.ReorderLevel;
                        stocks.Add(li);
                    }

                    stocks.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateItemStock(stocks);

                        if (Result.Length > 0)
                        {
                            response = "Internal Server Error.";
                        }

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult UpdateSOitemStock(string StockData)
        //{
        //    var js = new JavaScriptSerializer();
        //    List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

        //    List<Stocks> stocks = new List<Stocks>();

        //    string response = "";

        //    try
        //    {
        //        if (stock.Count() < 0)
        //        {
        //            response = "Please Add Purchased Products.";
        //        }
        //        else
        //        {

        //            int i = 1;
        //            foreach (var dbr in stock)
        //            {
        //                Stocks li = new Stocks();

        //                li.Item_id = dbr.Item_id;
        //                Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
        //                li.Stock_id = st.Stock_id;
        //                if (dbr.Accounting_Quantity == "0")
        //                {
        //                    li.Physical_Quantity = Convert.ToString(Convert.ToInt32(st.Physical_Quantity) - Convert.ToInt32(dbr.Physical_Quantity));
        //                    li.Physical_Avail_ForSale = st.Physical_Avail_ForSale;
        //                    li.Physical_Committed = Convert.ToString(Convert.ToInt32(st.Physical_Committed) - Convert.ToInt32(dbr.Physical_Quantity));

        //                    li.Accounting_Quantity = st.Accounting_Quantity;
        //                    li.Acc_Avail_ForSale = st.Acc_Avail_ForSale;
        //                    li.Acc_Commited = Convert.ToString(Convert.ToInt32(st.Acc_Commited) + Convert.ToInt32(dbr.Acc_Commited));
        //                }
        //                else if (dbr.Physical_Quantity == "0")
        //                {
        //                    li.Physical_Quantity = st.Physical_Quantity;
        //                    li.Physical_Avail_ForSale = st.Physical_Avail_ForSale;
        //                    li.Physical_Committed = Convert.ToString(Convert.ToInt32(st.Physical_Committed) + Convert.ToInt32(dbr.Physical_Committed));

        //                    li.Accounting_Quantity = Convert.ToString(Convert.ToInt32(st.Accounting_Quantity) - Convert.ToInt32(dbr.Accounting_Quantity));
        //                    li.Acc_Avail_ForSale = st.Acc_Avail_ForSale;
        //                    li.Acc_Commited = Convert.ToString(Convert.ToInt32(st.Acc_Commited) + Convert.ToInt32(dbr.Accounting_Quantity));
        //                }
        //                else
        //                {
        //                    li.Physical_Quantity = st.Physical_Quantity;
        //                    li.Physical_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Physical_Avail_ForSale) - Convert.ToInt32(dbr.Physical_Quantity));
        //                    li.Physical_Committed = Convert.ToString(Convert.ToInt32(st.Physical_Committed) + Convert.ToInt32(dbr.Physical_Committed));
        //                    li.Accounting_Quantity = st.Accounting_Quantity;
        //                    li.Acc_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Acc_Avail_ForSale) - Convert.ToInt32(dbr.Accounting_Quantity));
        //                    li.Acc_Commited = Convert.ToString(Convert.ToInt32(st.Acc_Commited) + Convert.ToInt32(dbr.Acc_Commited));
        //                }
        //                li.OpeningStock = st.OpeningStock;
        //                li.ReorderLevel = st.ReorderLevel;
        //                stocks.Add(li);
        //            }

        //            stocks.TrimExcess();

        //            if (response.Length <= 0)
        //            {
        //                string Result = new Catalog().UpdateItemStock(stocks);

        //                if (Result.Length > 0)
        //                {
        //                    response = "Internal Server Error.";
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        response = "Internal Server Error.";
        //    }

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateSOitemStockOnInsert(string StockData)
        {
            var js = new JavaScriptSerializer();
            List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

            List<Stocks> stocks = new List<Stocks>();

            string response = "";

            try
            {
                if (stock.Count() < 0)
                {
                    response = "Please Add Purchased Products.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in stock)
                    {
                        Stocks li = new Stocks();

                        li.Item_id = dbr.Item_id;
                        Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
                        li.Stock_id = st.Stock_id;

                        li.Physical_Quantity = st.Physical_Quantity;
                        li.Physical_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Physical_Avail_ForSale) - Convert.ToInt32(dbr.Physical_Quantity));
                        li.Physical_Committed = Convert.ToString(Convert.ToInt32(st.Physical_Committed) + Convert.ToInt32(dbr.Physical_Quantity));

                        li.Accounting_Quantity = st.Accounting_Quantity;
                        li.Acc_Avail_ForSale = Convert.ToString(Convert.ToInt32(st.Acc_Avail_ForSale) - Convert.ToInt32(dbr.Accounting_Quantity));
                        li.Acc_Commited = Convert.ToString(Convert.ToInt32(st.Acc_Commited) + Convert.ToInt32(dbr.Accounting_Quantity));

                        li.OpeningStock = st.OpeningStock;
                        li.ReorderLevel = st.ReorderLevel;
                        stocks.Add(li);
                    }

                    stocks.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateItemStock(stocks);

                        if (Result.Length > 0)
                        {
                            response = "Internal Server Error.";
                        }

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateSOitemStockOnPayment(string StockData)
        {
            var js = new JavaScriptSerializer();
            List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

            List<Stocks> stocks = new List<Stocks>();

            string response = "";

            try
            {
                if (stock.Count() < 0)
                {
                    response = "Please Add Purchased Products.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in stock)
                    {
                        Stocks li = new Stocks();

                        li.Item_id = dbr.Item_id;
                        Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
                        li.Stock_id = st.Stock_id;

                        li.Physical_Quantity = st.Physical_Quantity;
                        li.Physical_Avail_ForSale = st.Physical_Avail_ForSale;
                        li.Physical_Committed = st.Physical_Committed;

                        li.Accounting_Quantity = Convert.ToString(Convert.ToInt32(st.Accounting_Quantity) - Convert.ToInt32(dbr.Accounting_Quantity));
                        li.Acc_Avail_ForSale = st.Acc_Avail_ForSale;
                        li.Acc_Commited = Convert.ToString(Convert.ToInt32(st.Acc_Commited) - Convert.ToInt32(dbr.Accounting_Quantity));

                        li.OpeningStock = st.OpeningStock;
                        li.ReorderLevel = st.ReorderLevel;
                        stocks.Add(li);
                    }

                    stocks.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateItemStock(stocks);

                        if (Result.Length > 0)
                        {
                            response = "Internal Server Error.";
                        }

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateSOitemStockOnPShipment(string StockData)
        {
            var js = new JavaScriptSerializer();
            List<Stock> stock = js.Deserialize<List<Stock>>(StockData);

            List<Stocks> stocks = new List<Stocks>();

            string response = "";

            try
            {
                if (stock.Count() < 0)
                {
                    response = "Please Add Purchased Products.";
                }
                else
                {

                    int i = 1;
                    foreach (var dbr in stock)
                    {
                        Stocks li = new Stocks();

                        li.Item_id = dbr.Item_id;
                        Stocks st = new Catalog().SelectStockByItemid(dbr.Item_id);
                        li.Stock_id = st.Stock_id;

                        li.Physical_Quantity = Convert.ToString(Convert.ToInt32(st.Physical_Quantity) - Convert.ToInt32(dbr.Physical_Quantity));
                        li.Physical_Avail_ForSale = st.Physical_Avail_ForSale;
                        li.Physical_Committed = Convert.ToString(Convert.ToInt32(st.Physical_Committed) - Convert.ToInt32(dbr.Physical_Quantity));

                        li.Accounting_Quantity = st.Accounting_Quantity;
                        li.Acc_Avail_ForSale = st.Acc_Avail_ForSale;
                        li.Acc_Commited = st.Acc_Commited;

                        li.OpeningStock = st.OpeningStock;
                        li.ReorderLevel = st.ReorderLevel;
                        stocks.Add(li);
                    }

                    stocks.TrimExcess();

                    if (response.Length <= 0)
                    {
                        string Result = new Catalog().UpdateItemStock(stocks);

                        if (Result.Length > 0)
                        {
                            response = "Internal Server Error.";
                        }

                    }

                }
            }
            catch (Exception e)
            {
                response = "Internal Server Error.";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
