using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Data;


namespace G_Accounting_System.DAL
{
    public class BillDAL
    {
        public void InsertBill(Bills B)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Bill", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPurchase_id", B.Purchase_id);
            cmd.Parameters.AddWithValue("@pBill_No", B.Bill_No);
            cmd.Parameters.AddWithValue("@pBill_Status", B.Bill_Status);
            cmd.Parameters.AddWithValue("@pBill_Amount", B.Bill_Amount);
            cmd.Parameters.AddWithValue("@pAddedBy", (B.AddedBy == 0) ? Convert.DBNull : B.AddedBy);
            cmd.Parameters.AddWithValue("@pEnable", "1");
            cmd.Parameters.AddWithValue("@pBillDateTime", (B.BillDateTime == null) ? Convert.DBNull : B.BillDateTime);
            cmd.Parameters.AddWithValue("@pBillDueDate", (B.BillDueDate == null) ? Convert.DBNull : B.BillDueDate);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pBill_id_Output = new SqlParameter("@pBill_id_Output", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(pBill_id_Output);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string Bill_id_Output = pBill_id_Output.Value.ToString();

            B.pFlag = Flag;
            B.pDesc = Desc;
            B.pBill_id_Output = Bill_id_Output;
        }

        public void UpdateBillStatus(Bills B)
        {
            SqlCommand cmd = new SqlCommand("Update Bills set Bill_Status = @pBillStatus where Bill_id=@pBill_id", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@pBill_id", B.id);
            cmd.Parameters.AddWithValue("@pBillStatus", B.Bill_Status);

            RunQuery(cmd);

        }

        public List<Bills> SelectAll(string search, string From, string To, int User_id)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Bills", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(search))
            {
                cmd.Parameters.AddWithValue("@pBill_No", search);
            }
            else
            {
                cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
                cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            }
            cmd.Parameters.AddWithValue("@pAddedBy", User_id);

            return fetchEntries(cmd);
        }

        public Bills SelectBById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Bills_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pBill_id", id);
            List<Bills> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Bills SelectBByPId(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Bills_By_PID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPurchase_id", id);
            List<Bills> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public string getLastBillNo()
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Last_BillNo", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            string temp = fetchBLEntries(cmd);
            return (temp != null) ? temp : null;
        }

        public List<Bills> VendorTransactions_Bills(int Vendor_id, string Search)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Bills_By_VendorId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pVendorId", Vendor_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);
            return fetchVTBEntries(cmd);
        }

        private List<Bills> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Bills> bills = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        bills = new List<Bills>();
                        while (dr.Read())
                        {
                            Bills li = new Bills();
                            li.id = Convert.ToInt32(dr["Bill_id"]);
                            li.Purchase_id = Convert.ToInt32(dr["Purchase_id"]);
                            li.Bill_No = Convert.ToString(dr["Bill_No"]);
                            li.Order_No = Convert.ToString(dr["OrderNo"]);
                            li.Bill_Status = Convert.ToString(dr["Bill_Status"]);
                            li.Bill_Amount = dr["Total_Amount"] != DBNull.Value ? (decimal)dr["Total_Amount"] : 0;
                            li.Amount_Paid = dr["Amount_Paid"] != DBNull.Value ? (decimal)dr["Amount_Paid"] : 0;
                            li.Balance_Amount = dr["Balance_Amount"] != DBNull.Value ? (decimal)dr["Balance_Amount"] : 0;
                            li.BillDateTime = Convert.ToString(dr["BillDateTime"]);
                            li.BillDueDate = Convert.ToString(dr["BillDueDate"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Month = Convert.ToString(dr["Month_Of_Day"]);
                            li.Year = Convert.ToString(dr["Year_Of_Day"]);
                            bills.Add(li);
                        }
                        bills.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                bills = null;
            }
            return bills;
        }

        private string fetchBLEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            string lastBillNo = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lastBillNo = Convert.ToString(dr["LastBillNo"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                lastBillNo = null;
            }
            return lastBillNo;
        }

        private List<Bills> fetchVTBEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Bills> bills = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        bills = new List<Bills>();
                        while (dr.Read())
                        {
                            Bills li = new Bills();
                            li.id = Convert.ToInt32(dr["Bill_id"]);
                            li.Purchase_id = Convert.ToInt32(dr["PurchasingId"]);
                            li.Bill_No = Convert.ToString(dr["Bill_No"]);
                            li.Bill_Status = Convert.ToString(dr["Bill_Status"]);
                            li.Bill_Amount = Convert.ToInt32(dr["Bill_Amount"]);
                            li.Balance_Amount = dr["Balance_Amount"] != DBNull.Value ? (decimal)dr["Balance_Amount"] : 0;
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            bills.Add(li);
                        }
                        bills.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                bills = null;
            }
            return bills;
        }

        internal void RunQuery(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            con.Open();
            using (con)
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
