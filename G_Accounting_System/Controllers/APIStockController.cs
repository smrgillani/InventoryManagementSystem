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
    public class APIStockController : ApiController
    {
        [Route("api/APIStock/AllItemsStock")]
        [HttpGet]
        public IEnumerable<Stock> GetAllItemsStockList()
        {
            List<Stock> stock = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();
                //HttpRequest resolveRequest = HttpContext.Current.Request;
                //List<YourModel> model = new List<YourModel>();
                //resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
                //string Search = new StreamReader(resolveRequest.InputStream).ReadToEnd();

                var js = new JavaScriptSerializer();
                SearchParameters search = js.Deserialize<SearchParameters>(strJson);

                List<Stocks> stocks = new Catalog().GetAllItemsStockList(search.StartDate, search.EndDate, search.Search);

                stock = new List<Stock>();

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

                return stock;
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


        [Route("api/APIStock/InsertItemStock")]
        [HttpPost]
        public string InsertItemStock()
        {
            string response = null;
            try
            {
                string strJson = new ApiRequestToJson().ToJson();

                if (strJson != null)
                {
                    //model = (List<YourModel>)serializer.Deserialize(jsonString, typeof(List<YourModel>);
                    var js = new JavaScriptSerializer();
                    List<Stock> stock = js.Deserialize<List<Stock>>(strJson);

                    List<Stocks> stocks = new List<Stocks>();

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

                        stocks.TrimExcess();

                        string Result = new Catalog().UpdateItemStock(stocks);
                        response = "Stock Updated Successfully";
                    }

                }
                return response;
            }
            catch (Exception e)
            {
                response = e.Message;
                return response;
            }
        }
    }
}