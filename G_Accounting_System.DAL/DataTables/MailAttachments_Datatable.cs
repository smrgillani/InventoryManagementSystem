using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;

namespace G_Accounting_System.DAL.DataTables
{
    class MailAttachments_Datatable
    {
        public DataTable DataTable { get; private set; }
        public MailAttachments_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("Mailbox_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("FileName", typeof(string)) { AllowDBNull = true });

        }
        public void FillDataTable(List<MailAttachments> list)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["Mailbox_id"] = currentObj.Mailbox_id;
                currentRow["FileName"] = currentObj.FileName;

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
