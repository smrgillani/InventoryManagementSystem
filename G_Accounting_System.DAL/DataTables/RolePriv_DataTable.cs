using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL
{
    class RolePriv_DataTable
    {
        public DataTable DataTable { get; private set; }
        public RolePriv_DataTable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("Role_Priv_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Role_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Priv_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Check_Status", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Enable", typeof(string)) { AllowDBNull = true });

        }
        public void FillDataTable(List<RolePrivileges> list)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["Role_Priv_id"] = currentObj.Role_Priv_id;
                currentRow["Role_id"] = currentObj.Role_id;
                currentRow["Priv_id"] = currentObj.Priv_id;
                currentRow["Check_Status"] = currentObj.Check_Status;
                currentRow["Enable"] = currentObj.Enable;

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
