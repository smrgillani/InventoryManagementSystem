using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL.DataTables
{
    class UserPrivDataTable
    {
        public DataTable DataTable { get; private set; }
        public UserPrivDataTable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Priv_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("User_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Add", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Edit", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("View", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Profile", typeof(int)) { AllowDBNull = true });    

        }
        public void FillDataTable(List<UserPrivilegess> list)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["id"] = currentObj.id;
                currentRow["Priv_id"] = currentObj.priv_ID;
                currentRow["User_id"] = currentObj.User_id;
                currentRow["Add"] = currentObj.Add;
                currentRow["Edit"] = currentObj.Edit;
                //currentRow["Update"] = currentObj.Edit;
                //currentRow["Delete"] = currentObj.Delete;
                currentRow["View"] = currentObj.View;
                currentRow["Profile"] = currentObj.Profile;

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
