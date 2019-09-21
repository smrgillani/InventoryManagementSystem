using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL
{
    class Stock_Datatable
    {
        public DataTable DataTable { get; private set; }
        public Stock_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("Stock_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Item_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Physical_Quantity", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Physical_Avail_ForSale", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Physical_Committed", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Accounting_Quantity", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Acc_Avail_ForSale", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Acc_Commited", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("OpeningStock", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("ReorderLevel", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Time_Of_Day", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Date_Of_Day", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Month_Of_Day", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Year_Of_Day", typeof(string)) { AllowDBNull = true });
        }
        public void FillDataTable(List<Stocks> list)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["Stock_id"] = currentObj.Stock_id;
                currentRow["Item_id"] = currentObj.Item_id;
                currentRow["Physical_Quantity"] = currentObj.Physical_Quantity;
                currentRow["Physical_Avail_ForSale"] = currentObj.Physical_Avail_ForSale;
                currentRow["Physical_Committed"] = currentObj.Physical_Committed;
                currentRow["Accounting_Quantity"] = currentObj.Accounting_Quantity;
                currentRow["Acc_Avail_ForSale"] = currentObj.Acc_Avail_ForSale;
                currentRow["Acc_Commited"] = currentObj.Acc_Commited;
                currentRow["OpeningStock"] = currentObj.OpeningStock;
                currentRow["ReorderLevel"] = currentObj.ReorderLevel;
                currentRow["Time_Of_Day"] = DateTime.Now.ToString("HH:mm:ss tt");
                currentRow["Date_Of_Day"] = DateTime.Now.ToString("dd/MM/yyyy");
                currentRow["Month_Of_Day"] = DateTime.Now.ToString("MMM");
                currentRow["Year_Of_Day"] = DateTime.Now.ToString("yyyy");
                DataTable.Rows.Add(currentRow);
            }
        }
        public void Dispose()
        {
            if (DataTable == null)
                return;

            DataTable.Dispose();
            DataTable = null;
        }
    }
}
