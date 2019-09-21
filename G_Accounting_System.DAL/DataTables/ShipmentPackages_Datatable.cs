using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL
{
    class ShipmentPackages_Datatable
    {
        public DataTable DataTable { get; private set; }
        public ShipmentPackages_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("SaleOrder_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Shipment_No", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Package_id", typeof(int)) { AllowDBNull = true });
        }
        public void FillDataTable(List<Shipments> list, string ShipmentNo)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["SaleOrder_id"] = currentObj.SaleOrder_id;
                currentRow["Shipment_No"] = ShipmentNo;
                currentRow["Package_id"] = currentObj.Package_id;
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
