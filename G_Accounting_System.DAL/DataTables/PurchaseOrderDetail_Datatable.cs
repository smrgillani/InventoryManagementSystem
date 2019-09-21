using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL.DataTables
{
    class PurchaseOrderDetail_Datatable
    {
        public DataTable DataTable { get; private set; }
        public PurchaseOrderDetail_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("pd_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("PurchasingId", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("ItemId", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("VendorId", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Qty", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("PriceUnit", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("MsrmntUnit", typeof(string)) { AllowDBNull = true });

        }
        public void FillDataTable(List<Purchases> list, int purchasing_id)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["pd_id"] = currentObj.pdid;
                currentRow["PurchasingId"] = purchasing_id;
                currentRow["ItemId"] = currentObj.ItemId;
                currentRow["VendorId"] = currentObj.VendorId;
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
