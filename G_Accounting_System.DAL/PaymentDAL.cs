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
    public class PaymentDAL
    {
        public void InsertPayments(Payments P)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Payments", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pBill_id",P.Bill_id);
            cmd.Parameters.AddWithValue("@pPayment_Mode", (P.Payment_Mode == 0) ? Convert.DBNull : P.Payment_Mode);
            cmd.Parameters.AddWithValue("@pPayment_Date", (P.Payment_Date == null) ? Convert.DBNull : P.Payment_Date);
            cmd.Parameters.AddWithValue("@pTotal_Amount", (P.Total_Amount == 0) ? Convert.DBNull : P.Total_Amount);
            cmd.Parameters.AddWithValue("@pPaid_Amount", (P.Paid_Amount == 0) ? Convert.DBNull : P.Paid_Amount);
            cmd.Parameters.AddWithValue("@pBalance_Amount", P.Balance_Amount);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            P.pFlag = Flag;
            P.pDesc = Desc;
        }

        public List<Payments> SelectPaymentByBillId(int id)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Payments_By_BillID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pBill_id", id);

            return fetchEntries(cmd);
        }

        public List<Payments> ViewPayments(string Option, string search, string From, string To)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Payments_By_BillID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pBill_id", Option);
            cmd.Parameters.AddWithValue("@pBillNo", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);

            return fetchEntries(cmd);
        }

        public List<Payments> VendorTransactions_Payments(int Vendor_id, string Search)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Payments_By_VendorId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pVendorId", Vendor_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);
            return fetchVTPEntries(cmd);
        }

        private List<Payments> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Payments> payments = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        payments = new List<Payments>();
                        while (dr.Read())
                        {
                            Payments li = new Payments();
                            li.Payment_id = Convert.ToInt32(dr["Payment_id"]);
                            li.Bill_id = Convert.ToInt32(dr["Bill_id"]);
                            li.Bill_No = Convert.ToString(dr["Bill_No"]);
                            li.Payment_Mode = Convert.ToInt32(dr["Payment_Mode"]);
                            li.Payment_Date = Convert.ToString(dr["Payment_Date"]);
                            li.Total_Amount = Convert.ToDecimal(dr["Total_Amount"]);
                            li.Paid_Amount = Convert.ToDecimal(dr["Paid_Amount"]);
                            li.Balance_Amount = Convert.ToDecimal(dr["Balance_Amount"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Month = Convert.ToString(dr["Month_Of_Day"]);
                            li.Year = Convert.ToString(dr["Year_Of_Day"]);
                            payments.Add(li);
                        }
                        payments.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                payments = null;
            }
            return payments;
        }

        private List<Payments> fetchVTPEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Payments> payments = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        payments = new List<Payments>();
                        while (dr.Read())
                        {
                            Payments li = new Payments();
                            li.Payment_id = Convert.ToInt32(dr["Payment_id"]);
                            li.Bill_id = Convert.ToInt32(dr["Bill_id"]);
                            li.Bill_No = Convert.ToString(dr["Bill_No"]);
                            li.PaymentMode = Convert.ToString(dr["Payment_Mode"]);
                            li.Total_Amount = dr["Total_Amount"] != DBNull.Value ? (decimal)dr["Total_Amount"] : 0;
                            li.Paid_Amount = dr["Paid_Amount"] != DBNull.Value ? (decimal)dr["Paid_Amount"] : 0;
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            payments.Add(li);
                        }
                        payments.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                payments = null;
            }
            return payments;
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
