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
    public class ItemDAL
    {
        public void InsertUpdateItem(Items I)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Items", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", I.id);
            cmd.Parameters.AddWithValue("@pItem_type", (I.Item_type == null) ? Convert.DBNull : I.Item_type);
            cmd.Parameters.AddWithValue("@pItem_file", (I.ImagePath == null) ? Convert.DBNull : I.ImagePath);
            cmd.Parameters.AddWithValue("@pItem_Name", (I.Item_Name == null) ? Convert.DBNull : I.Item_Name);
            cmd.Parameters.AddWithValue("@pItem_Sku", (I.Item_Sku == null) ? Convert.DBNull : I.Item_Sku);
            cmd.Parameters.AddWithValue("@pItem_Category", (I.Item_Category == null) ? Convert.DBNull : I.Item_Category);
            cmd.Parameters.AddWithValue("@pItem_Unit", (I.Item_Unit == null) ? Convert.DBNull : I.Item_Unit);
            cmd.Parameters.AddWithValue("@pItem_Manufacturer", (I.Item_Manufacturer == null) ? Convert.DBNull : I.Item_Manufacturer);
            cmd.Parameters.AddWithValue("@pItem_Upc", (I.Item_Upc == null) ? Convert.DBNull : I.Item_Upc);
            cmd.Parameters.AddWithValue("@pItem_Brand", (I.Item_Brand == null) ? Convert.DBNull : I.Item_Brand);
            cmd.Parameters.AddWithValue("@pItem_Mpn", (I.Item_Mpn == null) ? Convert.DBNull : I.Item_Mpn);
            cmd.Parameters.AddWithValue("@pItem_Ean", (I.Item_Ean == null) ? Convert.DBNull : I.Item_Ean);
            cmd.Parameters.AddWithValue("@pItem_Isbn", (I.Item_Isbn == null) ? Convert.DBNull : I.Item_Isbn);
            cmd.Parameters.AddWithValue("@pItem_Sell_Price", (I.Item_Sell_Price == null) ? Convert.DBNull : I.Item_Sell_Price);
            cmd.Parameters.AddWithValue("@pItem_Tax", (I.Item_Tax == null) ? Convert.DBNull : I.Item_Tax);
            cmd.Parameters.AddWithValue("@pItem_Purchase_Price", (I.Item_Purchase_Price == null) ? Convert.DBNull : I.Item_Purchase_Price);
            cmd.Parameters.AddWithValue("@pItem_Preferred_Vendor", (I.Item_Preferred_Vendor == null) ? Convert.DBNull : I.Item_Preferred_Vendor);
            cmd.Parameters.AddWithValue("@pEnable", "1");
            cmd.Parameters.AddWithValue("@pAddedBy", (I.AddedBy == 0) ? Convert.DBNull : I.AddedBy);
            cmd.Parameters.AddWithValue("@pUpdatedBy", (I.UpdatedBy == 0) ? Convert.DBNull : I.UpdatedBy);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pItem_id_Out = new SqlParameter("@pItem_id_Out", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pItem_id_Out);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Item_id_Out = pItem_id_Out.Value.ToString();

            I.pFlag = Flag;
            I.pDesc = Desc;
            I.pItem_id_Out = Item_id_Out;
        }

        public List<Items> SelectAll(string Option, string search, string From, string To)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Items", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (Option == "All")
            {
                cmd.Parameters.AddWithValue("@pEnable", null);
            }
            else if (Option == "Active" || Option == null)
            {
                cmd.Parameters.AddWithValue("@pEnable", 1);
            }
            else if (Option == "Inactive")
            {
                cmd.Parameters.AddWithValue("@pEnable", 0);
            }
            cmd.Parameters.AddWithValue("@pItem_Name", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            return fetchEntries(cmd);
        }

        public Items SelectById(int id, int? eop)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Items_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", id);
            //cmd.Parameters.AddWithValue("@pEnable", eop);

            List<Items> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public void Del(int id)
        {
            SqlCommand cmd = new SqlCommand("Delete from Items Where Item_id=@Id ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            RunQuery(cmd);
        }

        public void UpdatepItem(Items I)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateItemVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", I.id);
            cmd.Parameters.AddWithValue("@pEnable", I.Enable);
            RunQuery(cmd);
        }

        public string DelItemRequest(List<Items> I, string type)
        {
            DeleteItemRequested_Datatable deleteItemRequested_Datatable = new DeleteItemRequested_Datatable();
            deleteItemRequested_Datatable.FillDataTable(I);
            var dt = deleteItemRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Item_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public List<Items> AllItemsDelRequest()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("Select * from Items where Delete_Status = 'Requested'", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            return fetchEntries(cmd);
        }

        public Items CheckItemForDelete(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Check_Item_For_Delete", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", id);
            List<Items> temp = fetchStatus(cmd);

            return (temp != null) ? temp[0] : null;
        }

        private List<Items> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Items> items = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        items = new List<Items>();
                        while (dr.Read())
                        {
                            Items li = new Items();
                            li.id = Convert.ToInt32(dr["Item_id"]);
                            li.Stock_id = Convert.ToInt32(dr["Stock_id"]);
                            li.Item_type = Convert.ToString(dr["Item_type"]);
                            li.File_Name = Convert.ToString(dr["Item_file"]);
                            li.Item_Name = Convert.ToString(dr["Item_Name"]);
                            li.Item_Sku = Convert.ToString(dr["Item_Sku"]);
                            li.Item_Category = Convert.ToString(dr["Category"]);
                            li.Category_id = Convert.ToInt32(dr["Catgeory_id"]);
                            li.Item_Unit = Convert.ToString(dr["Unit"]);
                            li.Unit_id = Convert.ToInt32(dr["Unit_id"]);
                            li.Item_Manufacturer = Convert.ToString(dr["Manufacturer"]);
                            li.Manufacturer_id = Convert.ToInt32(dr["Manufacturer_id"]);
                            li.Item_Upc = Convert.ToString(dr["Item_Upc"]);
                            li.Item_Brand = Convert.ToString(dr["Brand"]);
                            li.Brand_id = Convert.ToInt32(dr["Brand_id"]);
                            li.Item_Mpn = Convert.ToString(dr["Item_Mpn"]);
                            li.Item_Ean = Convert.ToString(dr["Item_Ean"]);
                            li.Item_Isbn = Convert.ToString(dr["Item_Isbn"]);
                            li.Item_Sell_Price = Convert.ToString(dr["Item_Sell_Price"]);
                            li.Item_Tax = Convert.ToString(dr["Item_Tax"]);
                            li.Item_Purchase_Price = Convert.ToString(dr["Item_Purchase_Price"]);
                            li.Item_Preferred_Vendor = Convert.ToString(dr["Vendor"]);
                            li.OpeningStock = Convert.ToString(dr["OpeningStock"]);
                            li.ReorderLevel = Convert.ToString(dr["ReorderLevel"]);
                            li.Vendor_id = Convert.ToInt32(dr["Vendor_id"]);
                            li.Enable = Convert.ToInt32(dr["Enable"]);
                            li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                            li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Month = Convert.ToString(dr["Month_Of_Day"]);
                            li.Year = Convert.ToString(dr["Year_Of_Day"]);
                            items.Add(li);
                        }
                        items.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                items = null;
            }
            return items;
        }

        private List<Items> fetchStatus(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Items> items = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    items = new List<Items>();
                    while (dr.Read())
                    {
                        Items li = new Items();
                        li.Item_Name = Convert.ToString(dr["Item_Name"]);
                        items.Add(li);
                    }
                    items.TrimExcess();
                }
            }
            return items;
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
