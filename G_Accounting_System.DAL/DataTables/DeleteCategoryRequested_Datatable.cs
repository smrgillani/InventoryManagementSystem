using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL.DataTables
{
    class DeleteCategoryRequested_Datatable
    {
        public DataTable DataTable { get; private set; }
        public DeleteCategoryRequested_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("Category_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Enable", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Delete_Request_By", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Delete_Status", typeof(string)) { AllowDBNull = true });

        }
        public void FillDataTable(List<Categories> list)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["Category_id"] = currentObj.id;
                currentRow["Enable"] = currentObj.Enable;
                currentRow["Delete_Request_By"] = currentObj.Delete_Request_By;
                currentRow["Delete_Status"] = currentObj.Delete_Status;

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
