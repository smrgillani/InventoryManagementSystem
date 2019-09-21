using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_Accounting_System.ENT;
using System.Data.SqlClient;
using System.Transactions;
using System.Data;
using G_Accounting_System.DAL.DataTables;

namespace G_Accounting_System.DAL
{
    public class PurchaseDAL
    {
        public string Add(List<Purchases> P, Purchases po, int PremisesId, string UserId, int AddedBy)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("proc_InsertUpdate_PurchaseOrder", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    string TempOrderNum = DateTime.Now.ToString("ddMMyyyyHHmmss") + PremisesId + UserId;
                    cmd.Parameters.AddWithValue("@pid", po.id);
                    cmd.Parameters.AddWithValue("@pTempOrderNum", TempOrderNum);
                    cmd.Parameters.AddWithValue("@pPremisesId", PremisesId);
                    cmd.Parameters.AddWithValue("@pUserId", UserId);
                    cmd.Parameters.AddWithValue("@pTotalItems", P.Count);
                    cmd.Parameters.AddWithValue("@pApproved", "0");
                    cmd.Parameters.AddWithValue("@pEnable", 1);
                    cmd.Parameters.AddWithValue("@pApprovedByUI", "0");
                    cmd.Parameters.AddWithValue("@pRecieveStatus", "Draft");
                    cmd.Parameters.AddWithValue("@pAddedBy", (po.AddedBy == 0) ? Convert.DBNull : po.AddedBy);
                    cmd.Parameters.AddWithValue("@pUpdatedBy", (po.UpdatedBy == 0) ? Convert.DBNull : po.UpdatedBy);
                    cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
                    cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
                    SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pPO_Output = new SqlParameter("@pPO_Output", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(pFlag);
                    cmd.Parameters.Add(pDesc);
                    cmd.Parameters.Add(pPO_Output);

                    RunQuery(cmd);

                    string Flag = pFlag.Value.ToString();
                    string Desc = pDesc.Value.ToString();
                    string PO_Output = pPO_Output.Value.ToString();

                    po.pFlag = Flag;
                    po.pDesc = Desc;
                    po.pPO_Output = PO_Output;
                    Purchases GetPurchasingId = null;
                    if (po.id == 0)
                    {
                        GetPurchasingId = SelectByTempOrderNum(TempOrderNum);
                    }
                    else
                    {
                        GetPurchasingId = po;
                    }

                    PurchaseOrderDetail_Datatable purchaseOrderDetail_Datatable = new PurchaseOrderDetail_Datatable();

                    purchaseOrderDetail_Datatable.FillDataTable(P, GetPurchasingId.id);
                    var dt = purchaseOrderDetail_Datatable.DataTable;
                    cmd = new SqlCommand("proc_InsertUpdate_PurchaseOrderDetail", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dt", dt);

                    RunQuery(cmd);

                    scope.Complete();
                    scope.Dispose();

                    return "";
                }
            }
            catch (TransactionAbortedException e)
            {
                return e.Message.Equals("0") ? "" : "";
            }
        }

        public void UpdatePurchaseStatus(Purchases P)
        {
            SqlCommand cmd = null;
            if (P.Bill_Stat == "True")
            {
                cmd = new SqlCommand("Update Purchasing set RecieveStatus=@RecieveStatus, Recieve_DateTime=@Recieve_DateTime, BillStatus=@Bill_Stat where id=@Id", DALUtil.getConnection());
                cmd.Parameters.AddWithValue("@Bill_Stat", P.Bill_Stat);
            }
            if (P.Rec_Stat == "True")
            {
                cmd = new SqlCommand("Update Purchasing set RecieveStatus=@RecieveStatus, Recieve_DateTime=@Recieve_DateTime, Rec_Stat=@Rec_Stat where id=@Id", DALUtil.getConnection());
                cmd.Parameters.AddWithValue("@Rec_Stat", P.Rec_Stat);
            }

            cmd.Parameters.AddWithValue("@Id", P.id);
            cmd.Parameters.AddWithValue("@RecieveStatus", P.RecieveStatus);
            cmd.Parameters.AddWithValue("@Recieve_DateTime", P.RecieveDateTime);


            RunQuery(cmd);

        }
        //public void UpdatePBillStatus(Bills B)
        //{
        //    SqlCommand cmd = new SqlCommand("proc_InsertUpdate_Bill", DALUtil.getConnection());
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@pBill_id", B.id);
        //    cmd.Parameters.AddWithValue("@pPurchase_id", B.Purchase_id);
        //    cmd.Parameters.AddWithValue("@pBill_No", B.Bill_No);
        //    cmd.Parameters.AddWithValue("@pBill_Status", B.Bill_Status);
        //    cmd.Parameters.AddWithValue("@pBill_Amount", B.Bill_Amount);
        //    cmd.Parameters.AddWithValue("@pEnable", "1");
        //    cmd.Parameters.AddWithValue("@pBillDateTime", (B.BillDateTime == null) ? Convert.DBNull : B.BillDateTime);
        //    cmd.Parameters.AddWithValue("@pBillDueDate", (B.BillDueDate == null) ? Convert.DBNull : B.BillDueDate);
        //    cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
        //    cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
        //    cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
        //    cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
        //    SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
        //    SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
        //    SqlParameter pBill_id_Output = new SqlParameter("@pBill_id_Output", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
        //    cmd.Parameters.Add(pFlag);
        //    cmd.Parameters.Add(pDesc);
        //    cmd.Parameters.Add(pBill_id_Output);

        //    RunQuery(cmd);

        //    string Flag = pFlag.Value.ToString();
        //    string Desc = pDesc.Value.ToString();
        //    int Bill_id_Output = Convert.ToInt32(pBill_id_Output.Value);

        //    B.pFlag = Flag;
        //    B.pDesc = Desc;
        //    B.pBill_id_Output = Bill_id_Output;
        //}

        public Purchases SelectByTempOrderNum(string TempOrderNum)
        {
            SqlCommand cmd = new SqlCommand("Select * from Purchasing where TempOrderNum=@TempOrderNum ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@TempOrderNum", TempOrderNum);
            List<Purchases> temp = fetchPIEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public List<Purchases> SelectAll(string Option, string search, string From, string To, int User_id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Purchasing", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            if (Option == "All")
            {
                cmd.Parameters.AddWithValue("@pEnable", null);
            }
            else if (Option == "Active" || Option == null)
            {
                cmd.Parameters.AddWithValue("@pEnable", 1);
            }
            else if (Option == "Inactive")
            {
                cmd.Parameters.AddWithValue("@pEnable", 0);
            }
            cmd.Parameters.AddWithValue("@pTempOrderNum", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            cmd.Parameters.AddWithValue("@pAddedBy", User_id);





            return fetchEntries(cmd);
        }

        public Purchases SelectPById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Purchasing_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPurchasing_id", id);
            List<Purchases> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public List<Purchases> SelectAllPItems(int id)
        {
            SqlCommand cmd = new SqlCommand("Select pd_id,PurchasingId as id,ItemId,VendorId,Qty,PriceUnit,MsrmntUnit from PurchasingDetails where PurchasingId = @Id order by pd_id desc ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            return fetchiEntries(cmd);
        }

        public Purchases SelectIById(int id)
        {
            SqlCommand cmd = new SqlCommand("Select pd_id,PurchasingId as id,ItemId,VendorId,Qty,PriceUnit,MsrmntUnit from PurchasingDetails where id = @Id order by id desc", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            List<Purchases> temp = fetchiEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public int SelectITP(int id)
        {
            SqlCommand cmd = new SqlCommand("Select pd_id,PurchasingId as id,ItemId,VendorId,Qty,PriceUnit,MsrmntUnit from PurchasingDetails where PurchasingId = @Id order by id desc ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", id);
            return fetchtpEntries(cmd);
        }

        public List<Purchases> SelectItemsForBillByPOid(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_BillItems_By_PO_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPurchasingId", id);
            return fetchiEntries(cmd);
        }

        public List<Purchases> ItemsTransactionPO(int Item_id, string Search)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_PO_By_ItemID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", Item_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);

            return fetchTPOEntries(cmd);
        }

        public List<Purchases> PreviousOrders(int Item_id, int VendorId)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_PreviousPurchaseOrders", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", Item_id);
            cmd.Parameters.AddWithValue("@pVendor_id", VendorId);

            return fetchTPOEntries(cmd);
        }

        public List<Bills> ItemsTransactionBill(int Item_id, string Search)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Bills_By_ItemID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", Item_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);

            return fetchTBILLEntries(cmd);
        }


        public string DelPurchasingRequest(List<Purchases> P, string type)
        {
            DeletePurchasingsRequested_Datatable deletePurchasingsRequested_Datatable = new DeletePurchasingsRequested_Datatable();
            deletePurchasingsRequested_Datatable.FillDataTable(P);
            var dt = deletePurchasingsRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Purchasings_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public void UpdatepPurchase(Purchases P)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdatePurchaseVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", P.id);
            cmd.Parameters.AddWithValue("@pEnable", P.Enable);
            RunQuery(cmd);
        }

        public void DeleteItemFromPurchase(int pdid)
        {
            SqlCommand cmd = new SqlCommand("proc_DeleteItemFromPurchase", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ppd_id", pdid);
            RunQuery(cmd);
        }

        public List<Purchases> SelectVendorTransactions_Items(int Vendor_id, string Search)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Purchased_Items_By_VendorId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pVendorId", Vendor_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);
            return fetchVTIEntries(cmd);
        }


        private List<Purchases> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Purchases> purchases = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    purchases = new List<Purchases>();
                    while (dr.Read())
                    {
                        Purchases li = new Purchases();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.TempOrderNum = Convert.ToString(dr["TempOrderNum"]);
                        li.PremisesId = Convert.ToInt32(dr["PremisesId"]);
                        li.Enable = Convert.ToInt32(dr["Enable"]);
                        li.Premises_Name = new PremisesDAL().SelectById(Convert.ToInt32(dr["PremisesId"]));
                        li.UserId = Convert.ToInt32(dr["UserId"]);
                        li.User_Name = new UserDAL().SelectById(Convert.ToInt32(dr["UserId"]));
                        li.AVendor_Name = new ContactDAL().SelectById(li.User_Name != null ? li.User_Name.id : 0, null);
                        li.User_Name = null;
                        li.AUserId = Convert.ToInt32(dr["ApprovedByUI"]);
                        li.AUser_Name = new UserDAL().SelectById(Convert.ToInt32(dr["ApprovedByUI"]));
                        li.ABVendor_Name = new ContactDAL().SelectById(li.AUser_Name != null ? li.AUser_Name.id : 0, null);
                        li.AUser_Name = null;
                        li.Approved = Convert.ToString(dr["Approved"]).Equals("1") ? "Yes" : "No";
                        li.RecieveDateTime = Convert.ToString(dr["Recieve_DateTime"]);
                        li.RecieveStatus = Convert.ToString(dr["RecieveStatus"]);
                        li.Bill_Stat = Convert.ToString(dr["Bill_Stat"]);
                        li.Rec_Stat = Convert.ToString(dr["Rec_Stat"]);
                        li.BillStatus = dr["Bill_Status"] != DBNull.Value ? (string)dr["Bill_Status"] : null;
                        li.TotalItems = Convert.ToString(dr["TotalItems"]);
                        li.Delete_Request_By = (dr["Delete_Request_By"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Delete_Request_By"]);
                        li.Delete_Status = Convert.ToString(dr["Delete_Status"] ?? Convert.DBNull);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        li.TotalPrice = SelectITP(Convert.ToInt32(dr["id"]));
                        purchases.Add(li);
                    }
                    purchases.TrimExcess();
                }
            }
            return purchases;
        }

        private List<Purchases> fetchPIEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Purchases> purchases = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    purchases = new List<Purchases>();
                    while (dr.Read())
                    {
                        Purchases li = new Purchases();
                        li.id = Convert.ToInt32(dr["id"]);
                        purchases.Add(li);
                    }
                    purchases.TrimExcess();
                }
            }
            return purchases;
        }

        private List<Purchases> fetchiEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Purchases> purchases = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    purchases = new List<Purchases>();
                    while (dr.Read())
                    {
                        Purchases li = new Purchases();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.pdid = Convert.ToInt32(dr["pd_id"]);
                        li.ItemId = Convert.ToInt32(dr["ItemId"]);
                        li.VendorId = Convert.ToInt32(dr["VendorId"]);
                        li.Item_Name = new ItemDAL().SelectById(Convert.ToInt32(dr["ItemId"]), null);
                        li.Vendor_Name = new ContactDAL().SelectById(Convert.ToInt32(dr["VendorId"]), null);
                        li.Vendor_Address = new ContactDAL().SelectById(Convert.ToInt32(dr["VendorId"]), null);
                        li.Vendor_Landline = new ContactDAL().SelectById(Convert.ToInt32(dr["VendorId"]), null);
                        li.Vendor_Mobile = new ContactDAL().SelectById(Convert.ToInt32(dr["VendorId"]), null);
                        li.Vendor_Email = new ContactDAL().SelectById(Convert.ToInt32(dr["VendorId"]), null);
                        li.MsrmntUnit = Convert.ToInt32(dr["Qty"]) + Convert.ToString(dr["MsrmntUnit"]);
                        li.Unit = Convert.ToString(dr["MsrmntUnit"]);
                        li.PriceUnit = Convert.ToInt32(dr["PriceUnit"]);
                        li.ItemQty = Convert.ToString(dr["Qty"]);
                        li.TotalItems = Convert.ToString(Convert.ToInt32(dr["Qty"]) * Convert.ToInt32(dr["PriceUnit"]));
                        purchases.Add(li);
                    }
                    purchases.TrimExcess();
                }
            }
            return purchases;
        }

        private int fetchtpEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            int InvoiceTotalPrice = 0;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        InvoiceTotalPrice = InvoiceTotalPrice + (Convert.ToInt32(dr["Qty"]) * Convert.ToInt32(dr["PriceUnit"]));
                    }
                }
            }
            return InvoiceTotalPrice;
        }

        private List<Purchases> fetchTPOEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Purchases> so = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        so = new List<Purchases>();
                        while (dr.Read())
                        {
                            Purchases li = new Purchases();
                            li.id = Convert.ToInt32(dr["PurchaseOrder_id"]);
                            li.TempOrderNum = Convert.ToString(dr["TempOrderNum"]);
                            li.ItemId = Convert.ToInt32(dr["ItemId"]);
                            li.ItemName = Convert.ToString(dr["Item_Name"]);
                            li.VendorId = Convert.ToInt32(dr["Vendor_id"]);
                            li.VendorName = Convert.ToString(dr["VendorName"]);
                            li.PriceUnit = Convert.ToSingle(dr["Price"]);
                            li.ItemQty = Convert.ToString(dr["QtyOrdered"]);
                            li.RecieveStatus = Convert.ToString(dr["RecieveStatus"]);
                            li.RecieveDateTime = Convert.ToString(dr["Recieve_DateTime"]);
                            so.Add(li);
                        }
                        so.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                so = null;
            }
            return so;
        }

        private List<Bills> fetchTBILLEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Bills> bill = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        bill = new List<Bills>();
                        while (dr.Read())
                        {
                            Bills li = new Bills();
                            li.id = Convert.ToInt32(dr["Bill_id"]);
                            li.Bill_No = Convert.ToString(dr["Bill_No"]);
                            li.Vendor_id = Convert.ToInt32(dr["Vendor_id"]);
                            li.Vendor = Convert.ToString(dr["VendorName"]);
                            li.PriceUnit = Convert.ToSingle(dr["Price"]);
                            li.ItemQty = Convert.ToString(dr["QtyPurchased"]);
                            li.Bill_Status = Convert.ToString(dr["Bill_Status"]);
                            li.BillDateTime = Convert.ToString(dr["BillDateTime"]);
                            bill.Add(li);
                        }
                        bill.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                bill = null;
            }
            return bill;
        }

        private List<Purchases> fetchVTIEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Purchases> items = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        items = new List<Purchases>();
                        while (dr.Read())
                        {
                            Purchases li = new Purchases();
                            li.id = Convert.ToInt32(dr["PurchasingId"]);
                            li.TempOrderNum = Convert.ToString(dr["TempOrderNum"]);
                            li.VendorId = Convert.ToInt32(dr["VendorId"]);
                            li.PriceUnit = Convert.ToSingle(dr["PriceUnit"]);
                            li.ItemQty = Convert.ToString(dr["Qty"]);
                            li.ItemId = Convert.ToInt32(dr["ItemId"]);
                            li.ItemName = Convert.ToString(dr["Item_Name"]);
                            li.MsrmntUnit = Convert.ToString(dr["MsrmntUnit"]);
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            items.Add(li);
                        }
                        items.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                items = null;
            }
            return items;
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
