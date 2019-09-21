using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL
{
    class SaleReturn_Datatable
    {
        public DataTable DataTable { get; private set; }
        public SaleReturn_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("SaleReturn_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Package_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Item_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Return_Qty", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("ReturnQty_Cost", typeof(string)) { AllowDBNull = true });
        }
        public void FillDataTable(List<SaleReturns> list)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["SaleReturn_id"] = currentObj.SaleReturn_id;
                currentRow["Package_id"] = currentObj.Package_id;
                currentRow["Item_id"] = currentObj.Item_id;
                currentRow["Return_Qty"] = currentObj.Return_Qty;
                currentRow["ReturnQty_Cost"] = currentObj.ReturnQty_Cost;
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
