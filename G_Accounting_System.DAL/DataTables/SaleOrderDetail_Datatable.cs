using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;


namespace G_Accounting_System.DAL.DataTables
{
    class SaleOrderDetail_Datatable
    {
        public DataTable DataTable { get; private set; }
        public SaleOrderDetail_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("SalesOrder_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("ItemId", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Customer_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Qty", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("PriceUnit", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("MsrmntUnit", typeof(string)) { AllowDBNull = true });

        }
        public void FillDataTable(List<SalesOrders> list, int saleorder_id)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["id"] = currentObj.sdid;
                currentRow["SalesOrder_id"] = saleorder_id;
                currentRow["ItemId"] = currentObj.ItemId;
                currentRow["Customer_id"] = currentObj.Customer_id;
                currentRow["Qty"] = currentObj.Quantity;
                currentRow["PriceUnit"] = currentObj.PriceUnit;
                currentRow["MsrmntUnit"] = currentObj.MsrmntUnit;

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
