using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL
{
    class PackageDetail_Datatable
    {
        public DataTable DataTable { get; private set; }
        public PackageDetail_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("SaleOrder_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Package_No", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Item_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("UnitPrice", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Packed_Item_Qty", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Package_Date", typeof(string)) { AllowDBNull = true });

        }
        public void FillDataTable(List<Packages> list, string PackageNo)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["SaleOrder_id"] = currentObj.SalesOrder_id;
                currentRow["Package_No"] = PackageNo;
                currentRow["Item_id"] = currentObj.Item_id;
                currentRow["UnitPrice"] = currentObj.unitprice;
                currentRow["Packed_Item_Qty"] = currentObj.Packed_Qty;
                currentRow["Package_Date"] = currentObj.Package_Date;

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
