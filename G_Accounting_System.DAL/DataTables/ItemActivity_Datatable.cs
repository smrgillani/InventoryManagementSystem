using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G_Accounting_System.ENT;


namespace G_Accounting_System.DAL.DataTables
{
    class ItemActivity_Datatable
    {
        public DataTable DataTable { get; private set; }
        public ItemActivity_Datatable()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add(new DataColumn("ActivityType_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("ActivityType", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("ActivityName", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Description", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Date", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Time", typeof(string)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("User_id", typeof(int)) { AllowDBNull = true });
            DataTable.Columns.Add(new DataColumn("Icon", typeof(string)) { AllowDBNull = true });

        }
        public void FillDataTable(List<Activities> list)
        {
            if (list == null || list.Count == 0)
                return;

            DataRow currentRow;
            foreach (var currentObj in list)
            {
                currentRow = DataTable.NewRow();
                currentRow["ActivityType_id"] = currentObj.ActivityType_id;
                currentRow["ActivityType"] = currentObj.ActivityType;
                currentRow["ActivityName"] = currentObj.ActivityName;
                currentRow["Description"] = currentObj.Description;
                currentRow["Date"] = currentObj.Date;
                currentRow["Time"] = currentObj.Time;
                currentRow["User_id"] = currentObj.User_id;
                currentRow["Icon"] = currentObj.Icon;

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
