using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Accounting_System.ENT
{
    public class Dashboards
    {
        #region SalesActivity
        public float ToBePacked { get; set; }
        public float ToBeShipped { get; set; }
        public float ToBeDelivered { get; set; }
        public float ToBeInvoiced { get; set; }
        #endregion

        #region ProductDetails
        public int TotalItems { get; set; }
        public int LowStockItems { get; set; }
        #endregion

        #region TopSellingItems/SO
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public float QuantitySold { get; set; }
        #endregion

        #region PurchaseOrder/SalesOrder
        public float QuantityOrdered { get; set; }
        public float TotalCost { get; set; }
        #endregion

        #region InventorySummary
        public float QuantityInHand { get; set; }
        public float QuantityToBeReceived { get; set; }
        #endregion

        #region SalesOrderDetails
        public int Draft { get; set; }
        public int Confirmed { get; set; }
        public int Packed { get; set; }
        public int Shipped { get; set; }
        public int Invoiced { get; set; }
        #endregion
    }
}
