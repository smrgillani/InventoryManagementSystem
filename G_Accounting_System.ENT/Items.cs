using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace G_Accounting_System.ENT
{
    public class Items
    {
        public int id { get; set; }
        public int Stock_id { get; set; }
        public string Item_type { get; set; }
        public string File_Name { get; set; }
        public string Item_Name { get; set; }
        public string Item_Sku { get; set; }
        public string Item_Category { get; set; }
        public int Category_id { get; set; }
        public string Item_Unit { get; set; }
        public int Unit_id { get; set; }
        public string Item_Manufacturer { get; set; }
        public int Manufacturer_id { get; set; }
        public string Item_Upc { get; set; }
        public string Item_Brand { get; set; }
        public int Brand_id { get; set; }
        public string Item_Mpn { get; set; }
        public string Item_Ean { get; set; }
        public string Item_Isbn { get; set; }
        public string Item_Sell_Price { get; set; }
        public string Item_Tax { get; set; }
        public string Item_Purchase_Price { get; set; }
        public string Item_Preferred_Vendor { get; set; }
        public int Vendor_id { get; set; }
        public string OpeningStock { get; set; }
        public string ReorderLevel { get; set; }
        public int Enable { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int Delete_Request_By { get; set; }
        public string Delete_Status { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string ImagePath { get; set; }
        public string pFlag { get; set; }
        public string pDesc { get; set; }
        public string pItem_id_Out { get; set; }
    }
}
