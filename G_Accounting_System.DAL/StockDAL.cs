using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Data;


namespace G_Accounting_System.DAL
{
    public class StockDAL
    {
        public string UpdateItemStock(List<Stocks> S)
        {
            Stock_Datatable stock_Datatable = new Stock_Datatable();
            stock_Datatable.FillDataTable(S);
            var dt = stock_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Stock", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);

            RunQuery(cmd);
            return "";
        }

        public string InsertItemStock(List<Stocks> S)
        {
            Stock_Datatable stock_Datatable = new Stock_Datatable();
            stock_Datatable.FillDataTable(S);
            var dt = stock_Datatable.DataTable;
            foreach (var dbr in S)
            {
                SqlCommand cmd = new SqlCommand("proc_Insert_Stock", DALUtil.getConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pItem_id", dbr.Item_id);
                cmd.Parameters.AddWithValue("@pPhysical_Quantity", dbr.Physical_Quantity);
                cmd.Parameters.AddWithValue("@pAccounting_Quantity", dbr.Accounting_Quantity);
                cmd.Parameters.AddWithValue("@pOpeningStock", dbr.OpeningStock);
                cmd.Parameters.AddWithValue("@pReorderLevel", dbr.ReorderLevel);
                cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
                cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
                cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));

                RunQuery(cmd);
            }
            return "";
        }

        public List<Stocks> GetAllItemsStockList(string StartDate, string EndDate, string Search)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Stock", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pName", Search);
            cmd.Parameters.AddWithValue("@pFrom", StartDate == "" ? Convert.DBNull : StartDate);
            cmd.Parameters.AddWithValue("@pTo", EndDate == "" ? Convert.DBNull : EndDate);

            return fetchSEntries(cmd);
        }

        public Stocks SelectStockByItemid(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Stock_By_ItemID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", id);
            List<Stocks> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        private List<Stocks> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Stocks> stocks = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    stocks = new List<Stocks>();
                    while (dr.Read())
                    {
                        Stocks li = new Stocks();
                        li.Stock_id = Convert.ToInt32(dr["Stock_id"]);
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.Physical_Quantity = Convert.ToString(dr["Physical_Quantity"] != DBNull.Value ? (string)dr["Physical_Quantity"] : "0");
                        li.Physical_Avail_ForSale = Convert.ToString(dr["Physical_Avail_ForSale"] != DBNull.Value ? (string)dr["Physical_Avail_ForSale"] : "0");
                        li.Physical_Committed = Convert.ToString(dr["Physical_Committed"] != DBNull.Value ? (string)dr["Physical_Committed"] : "0");
                        li.Accounting_Quantity = Convert.ToString(dr["Accounting_Quantity"] != DBNull.Value ? (string)dr["Accounting_Quantity"] : "0");
                        li.Acc_Avail_ForSale = Convert.ToString(dr["Acc_Avail_ForSale"] != DBNull.Value ? (string)dr["Acc_Avail_ForSale"] : "0");
                        li.Acc_Commited = Convert.ToString(dr["Acc_Commited"] != DBNull.Value ? (string)dr["Acc_Commited"] : "0");
                        li.OpeningStock = Convert.ToString(dr["OpeningStock"] != DBNull.Value ? (string)dr["OpeningStock"] : "0");
                        li.ReorderLevel = Convert.ToString(dr["ReorderLevel"] != DBNull.Value ? (string)dr["ReorderLevel"] : "0");
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        stocks.Add(li);
                    }
                    stocks.TrimExcess();
                }
            }
            return stocks;
        }

        private List<Stocks> fetchSEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Stocks> stocks = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    stocks = new List<Stocks>();
                    while (dr.Read())
                    {
                        Stocks li = new Stocks();
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.Stock_id = Convert.ToInt32(dr["Stock_id"]);
                        li.Item_Name = Convert.ToString(dr["Item_Name"]);
                        li.Physical_Quantity= Convert.ToString(dr["In_Stock"] != DBNull.Value ? (string)dr["In_Stock"] : "0");
                        li.Quantity_Sold = Convert.ToString(dr["Sold"] != DBNull.Value ? (string)dr["Sold"] : "0");
                        stocks.Add(li);
                    }
                    stocks.TrimExcess();
                }
            }
            return stocks;
        }

        internal void RunQuery(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            con.Open();
            using (con)
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
