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
using System.Security.Principal;

namespace G_Accounting_System.Controllers
{
    [SessionExpireFilterAttribute]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            ViewBag.userID = Session["UserId"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalesActivity()
        {
            Dashboard salesActivity = null;
            Dashboards salesActivities = new Catalog().SalesActivity();
            if (salesActivities != null)
            {
                salesActivity = new Dashboard();
                salesActivity.ToBePacked = salesActivities.ToBePacked;
                salesActivity.ToBeShipped = salesActivities.ToBeShipped;
                salesActivity.ToBeDelivered = salesActivities.ToBeDelivered;
                salesActivity.ToBeInvoiced = salesActivities.ToBeInvoiced;
            }
            return Json(salesActivity, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ProductDetails()
        {
            Dashboard ProductDetails = null;
            Dashboards productDetails = new Catalog().ProductDetails();
            if (productDetails != null)
            {
                ProductDetails = new Dashboard();
                ProductDetails.TotalItems = productDetails.TotalItems;
                //ProductDetails.LowStockItems = productDetails.LowStockItems;
            }
            return Json(ProductDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult TopSellingItems()
        {
            List<Dashboards> topSellingItems = new Catalog().TopSellingItems();
            List<Dashboard> topSellingItem = new List<Dashboard>();

            if (topSellingItems != null)
            {
                foreach (var dbr in topSellingItems)
                {
                    Dashboard li = new Dashboard();
                    li.ItemId = dbr.ItemId;
                    li.ItemName = dbr.ItemName;
                    li.ItemImage = dbr.ItemImage;
                    li.QuantitySold = dbr.QuantitySold;
                    topSellingItem.Add(li);
                }
            }



            return Json(topSellingItem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PurchaseOrder()
        {
            Dashboard purchaseOrder = null;
            Dashboards purchaseOrders = new Catalog().PurchaseOrder();
            if (purchaseOrders != null)
            {
                purchaseOrder = new Dashboard();
                purchaseOrder.QuantityOrdered = purchaseOrders.QuantityOrdered;
                purchaseOrder.TotalCost = purchaseOrders.TotalCost;
            }
            return Json(purchaseOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalesOrder()
        {
            Dashboard salesOrder = null;
            Dashboards salesOrders = new Catalog().SalesOrder();
            if (salesOrders != null)
            {
                salesOrder = new Dashboard();
                salesOrder.QuantitySold = salesOrders.QuantitySold;
                salesOrder.TotalCost = salesOrders.TotalCost;
            }
            return Json(salesOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InventorySummary()
        {
            Dashboard inventorySummary = null;
            Dashboards inventorySummarys = new Catalog().InventorySummary();
            if (inventorySummarys != null)
            {
                inventorySummary = new Dashboard();
                inventorySummary.QuantityInHand = inventorySummarys.QuantityInHand;
                //inventorySummary.QuantityToBeReceived = inventorySummarys.QuantityToBeReceived;
            }
            return Json(inventorySummary, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalesOrderDetail()
        {
            Dashboard salesOrderDetail = null;
            Dashboards salesOrderDetails = new Catalog().SalesOrderDetail();
            if (salesOrderDetails != null)
            {
                salesOrderDetail = new Dashboard();
                salesOrderDetail.Draft = salesOrderDetails.Draft;
                salesOrderDetail.Confirmed = salesOrderDetails.Confirmed;
                salesOrderDetail.Packed = salesOrderDetails.Packed;
                salesOrderDetail.Shipped = salesOrderDetails.Shipped;
                salesOrderDetail.Invoiced = salesOrderDetails.Invoiced;
            }
            return Json(salesOrderDetail, JsonRequestBehavior.AllowGet);
        }
    }
}
