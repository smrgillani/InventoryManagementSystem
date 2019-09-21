using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Data;
using G_Accounting_System.DAL.DataTables;

namespace G_Accounting_System.DAL
{
    public class DashboardDAL
    {
        public Dashboards SalesActivity()
        {
            SqlCommand cmd = new SqlCommand("proc_Dashboard_SalesActivity", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            List<Dashboards> temp = fetchSAEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Dashboards ProductDetails()
        {
            SqlCommand cmd = new SqlCommand("proc_Dashboard_ProductDetails", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            List<Dashboards> temp = fetchPDEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Dashboards PurchaseOrder()
        {
            SqlCommand cmd = new SqlCommand("proc_Dashboard_PurchaseOrder", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            List<Dashboards> temp = fetchPOEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Dashboards SalesOrder()
        {
            SqlCommand cmd = new SqlCommand("proc_Dashboard_SalesOrder", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            List<Dashboards> temp = fetchSOEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Dashboards InventorySummary()
        {
            SqlCommand cmd = new SqlCommand("proc_Dashboard_Inventory_Summary", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            List<Dashboards> temp = fetchISEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Dashboards SalesOrderDetail()
        {
            SqlCommand cmd = new SqlCommand("proc_Dashboard_SO_Summary", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            List<Dashboards> temp = fetchSODEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public List<Dashboards> TopSellingItems()
        {
            SqlCommand cmd = new SqlCommand("proc_Dashboard_TopSellingItems", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            return fetchTSIEntries(cmd);
        }

        private List<Dashboards> fetchSAEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dashboards> salesActivities = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesActivities = new List<Dashboards>();
                    while (dr.Read())
                    {
                        Dashboards li = new Dashboards();
                        li.ToBePacked = (dr["ToBePacked"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["ToBePacked"]);
                        li.ToBeShipped = (dr["ToBeShipped"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["ToBeShipped"]);
                        li.ToBeDelivered = (dr["ToBeDelivered"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["ToBeDelivered"]);
                        li.ToBeInvoiced = (dr["ToBeInvoiced"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["ToBeInvoiced"]);
                        salesActivities.Add(li);
                    }
                    salesActivities.TrimExcess();
                }
            }
            return salesActivities;
        }

        private List<Dashboards> fetchPDEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dashboards> salesActivities = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesActivities = new List<Dashboards>();
                    while (dr.Read())
                    {
                        Dashboards li = new Dashboards();
                        li.TotalItems = (dr["TotalItems"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TotalItems"]);
                        //li.LowStockItems = Convert.ToInt32(dr["LowStockItems"]);
                        salesActivities.Add(li);
                    }
                    salesActivities.TrimExcess();
                }
            }
            return salesActivities;
        }

        private List<Dashboards> fetchTSIEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dashboards> topSellingItems = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    topSellingItems = new List<Dashboards>();
                    while (dr.Read())
                    {
                        Dashboards li = new Dashboards();
                        li.ItemId = (dr["ItemId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ItemId"]);
                        li.ItemName = (dr["ItemName"] == DBNull.Value) ? null : Convert.ToString(dr["ItemName"]);
                        li.ItemImage = (dr["ItemImage"] == DBNull.Value) ? null : Convert.ToString(dr["ItemImage"]);
                        li.QuantitySold = (dr["QuantitySold"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["QuantitySold"]);
                        topSellingItems.Add(li);
                    }
                    topSellingItems.TrimExcess();
                }
            }
            return topSellingItems;
        }

        private List<Dashboards> fetchPOEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dashboards> purchaseOrder = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    purchaseOrder = new List<Dashboards>();
                    while (dr.Read())
                    {
                        Dashboards li = new Dashboards();
                        li.QuantityOrdered = (dr["QuantityOrdered"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["QuantityOrdered"]);
                        li.TotalCost = (dr["TotalCost"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["TotalCost"]);
                        purchaseOrder.Add(li);
                    }
                    purchaseOrder.TrimExcess();
                }
            }
            return purchaseOrder;
        }

        private List<Dashboards> fetchSOEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dashboards> purchaseOrder = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    purchaseOrder = new List<Dashboards>();
                    while (dr.Read())
                    {
                        Dashboards li = new Dashboards();
                        li.QuantitySold = (dr["QuantitySold"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["QuantitySold"]);
                        li.TotalCost = (dr["TotalCost"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["TotalCost"]);
                        purchaseOrder.Add(li);
                    }
                    purchaseOrder.TrimExcess();
                }
            }
            return purchaseOrder;
        }

        private List<Dashboards> fetchISEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dashboards> inventorySummary = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    inventorySummary = new List<Dashboards>();
                    while (dr.Read())
                    {
                        Dashboards li = new Dashboards();
                        li.QuantityInHand = (dr["QuantityInHand"] == DBNull.Value) ? 0 : Convert.ToSingle(dr["QuantityInHand"]);
                        //li.QuantityToBeReceived = Convert.ToSingle(dr["TotalCost"]);
                        inventorySummary.Add(li);
                    }
                    inventorySummary.TrimExcess();
                }
            }
            return inventorySummary;
        }

        private List<Dashboards> fetchSODEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Dashboards> salesOrderDetail = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesOrderDetail = new List<Dashboards>();
                    while (dr.Read())
                    {
                        Dashboards li = new Dashboards();
                        li.Draft = (dr["Draft"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Draft"]);
                        li.Confirmed = (dr["Confirmed"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Confirmed"]);
                        li.Packed = (dr["Packed"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Packed"]);
                        li.Shipped = (dr["Shipped"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Shipped"]);
                        li.Invoiced = (dr["Invoiced"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Invoiced"]);
                        salesOrderDetail.Add(li);
                    }
                    salesOrderDetail.TrimExcess();
                }
            }
            return salesOrderDetail;
        }
    }
}
