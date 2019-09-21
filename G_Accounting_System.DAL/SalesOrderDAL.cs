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
    public class SalesOrderDAL
    {
        public string Add(List<SalesOrders> S, SalesOrders so, int PremisesId, string UserId)
        {
            try
            {
                string SalesOrderNum = "SO-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + PremisesId + UserId;
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("proc_InsertUpdate_SalesOrder", DAL.DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pid", so.SalesOrder_id);
                    cmd.Parameters.AddWithValue("@pSaleOrderNo", SalesOrderNum);
                    cmd.Parameters.AddWithValue("@pPremisesId", PremisesId);
                    cmd.Parameters.AddWithValue("@pUserId", UserId);
                    cmd.Parameters.AddWithValue("@pTotalItems", S.Count);
                    cmd.Parameters.AddWithValue("@pSO_Total_Amount", so.SO_Total_Amount);
                    cmd.Parameters.AddWithValue("@pSO_Status", so.SO_Status);
                    cmd.Parameters.AddWithValue("@pSO_Invoice_Status", so.SO_Invoice_Status);
                    cmd.Parameters.AddWithValue("@pSO_Shipment_Status", so.SO_Shipment_Status);
                    cmd.Parameters.AddWithValue("@pSO_Package_Status", so.SO_Package_Status);
                    cmd.Parameters.AddWithValue("@pSO_DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@pEnable", so.Enable);
                    cmd.Parameters.AddWithValue("@pAddedBy", (so.AddedBy == 0) ? Convert.DBNull : so.AddedBy);
                    cmd.Parameters.AddWithValue("@pUpdatedBy", (so.UpdatedBy == 0) ? Convert.DBNull : so.UpdatedBy);
                    cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
                    cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
                    SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                    SqlParameter pSO_Output = new SqlParameter("@pSO_Output", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pFlag);
                    cmd.Parameters.Add(pDesc);
                    cmd.Parameters.Add(pSO_Output);

                    RunQuery(cmd);

                    string Flag = pFlag.Value.ToString();
                    string Desc = pDesc.Value.ToString();
                    string SO_Output = pSO_Output.Value.ToString();
                    so.pSO_Output = SO_Output;
                    so.pFlag = Flag;
                    so.pDesc = Desc;
                    SalesOrders GetSalesOrderId = null;
                    if (so.SalesOrder_id == 0)
                    {
                        GetSalesOrderId = SelectBySalesOrderNum(SalesOrderNum);
                    }
                    else
                    {
                        GetSalesOrderId = so;
                    }

                    SaleOrderDetail_Datatable saleOrderDetail_Datatable = new SaleOrderDetail_Datatable();

                    saleOrderDetail_Datatable.FillDataTable(S, GetSalesOrderId.SalesOrder_id);
                    var dt = saleOrderDetail_Datatable.DataTable;
                    cmd = new SqlCommand("proc_InsertUpdate_SaleOrderOrderDetail", DALUtil.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dt", dt);
                    RunQuery(cmd);

                    //foreach (var dbr in S)
                    //{
                    //    cmd = new SqlCommand("INSERT into SalesOrder_Details (SalesOrder_id , ItemId , Customer_id , Qty , PriceUnit , MsrmntUnit) VALUES (@SalesOrder_id , @ItemId , @CustomerID , @Qty , @PriceUnit , @MsrmntUnit)", DALUtil.getConnection());
                    //    cmd.Parameters.AddWithValue("@SalesOrder_id", GetSalesOrderId.SalesOrder_id);
                    //    cmd.Parameters.AddWithValue("@ItemId", dbr.ItemId);
                    //    cmd.Parameters.AddWithValue("@CustomerID", dbr.Customer_id);
                    //    cmd.Parameters.AddWithValue("@Qty", dbr.Quantity);
                    //    cmd.Parameters.AddWithValue("@PriceUnit", dbr.PriceUnit);
                    //    cmd.Parameters.AddWithValue("@MsrmntUnit", dbr.MsrmntUnit);

                    //    RunQuery(cmd);
                    //}

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

        public List<SalesOrders> SelectAll(string Option, string search, string From, string To, int User_id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SalesOrders", DALUtil.getConnection());
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
            cmd.Parameters.AddWithValue("@pSaleOrderNo", search);
            cmd.Parameters.AddWithValue("@pFrom", From == "" ? Convert.DBNull : From);
            cmd.Parameters.AddWithValue("@pTo", To == "" ? Convert.DBNull : To);
            cmd.Parameters.AddWithValue("@pAddedBy", User_id);
            return fetchEntries(cmd);
        }

        public void InsertSOInvoice(SO_Invoices SO)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_SO_Invoice", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", SO.SalesOrder_id);
            cmd.Parameters.AddWithValue("@pCustomer_id", SO.Customer_id);
            cmd.Parameters.AddWithValue("@pInvoice_No", SO.Invoice_No);
            cmd.Parameters.AddWithValue("@pInvoice_Status", SO.Invoice_Status);
            cmd.Parameters.AddWithValue("@pInvoice_Amount", SO.Invoice_Amount);
            cmd.Parameters.AddWithValue("@pAmount_Paid", SO.Amount_Paid);
            cmd.Parameters.AddWithValue("@pBalance_Amount", SO.Balance_Amount);
            cmd.Parameters.AddWithValue("@pInvoiceDateTime", (SO.InvoiceDateTime == null) ? Convert.DBNull : SO.InvoiceDateTime);
            cmd.Parameters.AddWithValue("@pInvoiceDueDate", (SO.InvoiceDueDate == null) ? Convert.DBNull : SO.InvoiceDueDate);
            cmd.Parameters.AddWithValue("@pAddedBy", (SO.AddedBy == 0) ? Convert.DBNull : SO.AddedBy);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter invoice_id_ouput = new SqlParameter("@pinvoice_id_ouput", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);
            cmd.Parameters.Add(invoice_id_ouput);

            RunQuery(cmd);
            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            string inv_id_ouput = invoice_id_ouput.Value.ToString();
            SO.pInvoice_id_Output = inv_id_ouput;
            SO.pFlag = Flag;
            SO.pDesc = Desc;
        }

        public string InsertSOPackage(List<Packages> P, Packages PG, int SalesOrder_id, int AddedBy)
        {
            string PackageNo = "PKG - " + DateTime.Now.ToString("ddMMyyyyHHmmss");
            PackageDetail_Datatable packageDetail_Datatable = new PackageDetail_Datatable();
            packageDetail_Datatable.FillDataTable(P, PackageNo);
            var dt = packageDetail_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Insert_Packaged_Items", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@pSaleOrder_Id", SalesOrder_id);
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();

            if (Flag == "1")
            {
                cmd = new SqlCommand("proc_InsertUpdate_SO_packages", DALUtil.getConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pSaleOrder_id", SalesOrder_id);
                cmd.Parameters.AddWithValue("@pPackage_No", PackageNo);
                cmd.Parameters.AddWithValue("@pPackage_Cost", P[0].PackageCost);
                cmd.Parameters.AddWithValue("@pPackage_Status", P[0].PackageStatus);
                cmd.Parameters.AddWithValue("@pPackage_Date", P[0].Package_Date);
                cmd.Parameters.AddWithValue("@pAddedBy", (AddedBy == 0) ? Convert.DBNull : AddedBy);
                cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
                cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
                cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));

                RunQuery(cmd);
            }
            PG.pFlag = Flag;
            PG.pDesc = Desc;
            return "";
        }

        public string InsertSOShipment(List<Shipments> S, Shipments SH, int SalesOrder_id, int AddedBy)
        {
            string ShipmentNo = "SHP - " + DateTime.Now.ToString("ddMMyyyyHHmmss");
            ShipmentPackages_Datatable shipmentPackages_Datatable = new ShipmentPackages_Datatable();
            shipmentPackages_Datatable.FillDataTable(S, ShipmentNo);
            var dt = shipmentPackages_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Insert_Shipment_Packages", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@pSaleOrder_Id", SalesOrder_id);
            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

            cmd.Parameters.Add(pFlag);
            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();

            string SFlag = "";
            string Desc = "";
            string ShipementIdout = "";
            if (Flag == "1")
            {
                cmd = new SqlCommand("proc_InsertUpdate_SO_Shipment", DALUtil.getConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pSaleOrder_id", SalesOrder_id);
                cmd.Parameters.AddWithValue("@pShipment_No", ShipmentNo);
                cmd.Parameters.AddWithValue("@pShipment_Cost", (S[0].Shipment_Cost == null) ? "0" : S[0].Shipment_Cost);
                cmd.Parameters.AddWithValue("@pShipment_Status", S[0].Shipment_Status);
                cmd.Parameters.AddWithValue("@pShipment_Date", S[0].Shipment_Date);
                cmd.Parameters.AddWithValue("@pAddedBy", (AddedBy == 0) ? Convert.DBNull : AddedBy);
                cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
                cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
                cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
                SqlParameter pSFlag = new SqlParameter("@pFlag", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                SqlParameter pShipementIdout = new SqlParameter("@pShipementIdout", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(pSFlag);
                cmd.Parameters.Add(pDesc);
                cmd.Parameters.Add(pShipementIdout);
                RunQuery(cmd);
                SFlag = pSFlag.Value.ToString();
                Desc = pDesc.Value.ToString();
                ShipementIdout = pShipementIdout.Value.ToString();

                foreach (var i in S)
                {
                    cmd = new SqlCommand("UPDATE SO_Packages SET Package_Status='Shipped' where package_id = @pPackage_id", DALUtil.getConnection());
                    cmd.Parameters.AddWithValue("@pPackage_id", i.Package_id);
                    RunQuery(cmd);
                }


                cmd = new SqlCommand("UPDATE SalesOrder set SO_Status='Confirm', SO_Shipment_Status='1' where id=@pSaleOrder_Id", DALUtil.getConnection());
                cmd.Parameters.AddWithValue("@pSaleOrder_Id", SalesOrder_id);
                RunQuery(cmd);
            }
            SH.pFlag = SFlag;
            SH.pDesc = Desc;
            SH.pShipementIdout = ShipementIdout;
            return "";
        }

        public string InsertSaleReturn(List<SaleReturns> S, SaleReturns SR)
        {
            string SaleReturnNo = "SR - " + DateTime.Now.ToString("ddMMyyyyHHmmss");
            SaleReturn_Datatable saleReturn_Datatable = new SaleReturn_Datatable();
            saleReturn_Datatable.FillDataTable(S);
            var dt = saleReturn_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Insert_SaleReturn", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSaleOrder_Id", SR.SaleOrder_id);
            cmd.Parameters.AddWithValue("@pSaleReturnNo", SaleReturnNo);
            cmd.Parameters.AddWithValue("@pSaleReturn_Date", SR.SaleReturn_Date);
            cmd.Parameters.AddWithValue("@pSaleReturn_Status", SR.SaleReturn_Status);
            cmd.Parameters.AddWithValue("@pAddedBy", (SR.AddedBy == 0) ? Convert.DBNull : SR.AddedBy);
            cmd.Parameters.AddWithValue("@pTime_Of_Day", DateTime.Now.ToString("HH:mm:ss tt"));
            cmd.Parameters.AddWithValue("@pDate_Of_Day", DateTime.Now.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@pMonth_Of_Day", DateTime.Now.ToString("MMM"));
            cmd.Parameters.AddWithValue("@pYear_Of_Day", DateTime.Now.ToString("yyyy"));
            cmd.Parameters.AddWithValue("@dt", dt);

            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            SR.pFlag = Flag;
            SR.pDesc = Desc;

            return "";
        }

        public string ReturnReceivingQty(List<SaleReturns> S)
        {
            ReturnReceivingDatatable returnReceivingDatatable = new ReturnReceivingDatatable();
            returnReceivingDatatable.FillDataTable(S);
            var dt = returnReceivingDatatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Insert_SaleReturn_Receiving", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);

            RunQuery(cmd);

            return "";
        }

        public void UpdateShipmentDeliver(Shipments S)
        {
            SqlCommand cmd = new SqlCommand("proc_Update_Shipment_Deliver", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPackage_id", S.Package_id);
            cmd.Parameters.AddWithValue("@pPackage_Status", S.Shipment_Status);

            SqlParameter pFlag = new SqlParameter("@pFlag", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
            SqlParameter pDesc = new SqlParameter("@pDesc", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pFlag);
            cmd.Parameters.Add(pDesc);

            RunQuery(cmd);

            string Flag = pFlag.Value.ToString();
            string Desc = pDesc.Value.ToString();
            S.pFlag = Flag;
            S.pDesc = Desc;
        }

        public void UpdateSO_InvoiceStatus(SalesOrders SO)
        {
            SqlCommand cmd = new SqlCommand("Update SalesOrder set SO_Status=@SO_Status, SO_Invoice_Status=@SO_Invoice_Status where id=@Id", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", SO.SalesOrder_id);
            cmd.Parameters.AddWithValue("@SO_Invoice_Status", SO.SO_Invoice_Status);
            cmd.Parameters.AddWithValue("@SO_Status", SO.SO_Status);

            RunQuery(cmd);

        }

        public void UpdateinvoiceStatus(SO_Invoices I)
        {
            SqlCommand cmd = new SqlCommand("Update SalesOrder_Invoices set Invoice_Status=@Invoice_Status where id=@Id", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", I.id);
            cmd.Parameters.AddWithValue("@Invoice_Status", I.Invoice_Status);

            RunQuery(cmd);

        }

        public void UpdateSO_PackageStatus(SalesOrders SO)
        {
            SqlCommand cmd = new SqlCommand("Update SalesOrder set SO_Status=@SO_Status, SO_Package_Status=@SO_Package_Status where id=@Id", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@Id", SO.SalesOrder_id);
            cmd.Parameters.AddWithValue("@SO_Package_Status", SO.SO_Package_Status);
            cmd.Parameters.AddWithValue("@SO_Status", SO.SO_Status);

            RunQuery(cmd);

        }

        public void InsertSO_Payments(SO_Payments P)
        {
            SqlCommand cmd = new SqlCommand("proc_InsertUpdate_SOInvoice_Payments", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSO_Invoice_id", P.Invoice_id);
            cmd.Parameters.AddWithValue("@pSO_Payment_Mode", P.Payment_Mode);
            cmd.Parameters.AddWithValue("@pSO_Payment_Date", P.Payment_Date);
            cmd.Parameters.AddWithValue("@pSO_Total_Amount", P.Total_Amount);
            cmd.Parameters.AddWithValue("@pSO_Paid_Amount", P.Paid_Amount);
            cmd.Parameters.AddWithValue("@pSO_Balance_Amount", P.Balance_Amount);
            cmd.Parameters.AddWithValue("@pAddedBy", (P.AddedBy == 0) ? Convert.DBNull : P.AddedBy);
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

        public Packages SlecetPKGBySO_ID(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Packages_By_SO_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);
            List<Packages> temp = fetchPackagesForSO(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public SO_Payments LastPaymentInvoice(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Last_Payment_Invoice", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pInvoiceId", id);
            List<SO_Payments> temp = fetchINVPEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public SO_Invoices InvoiceByInvoiceId(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SO_Invoice_By_InvoiceID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pInvoiceId", id);
            List<SO_Invoices> temp = fetchINVEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public string DelSalesRequest(List<SalesOrders> S, string type)
        {
            DeleteSalesRequested_Datatable deleteSalesRequested_Datatable = new DeleteSalesRequested_Datatable();
            deleteSalesRequested_Datatable.FillDataTable(S);
            var dt = deleteSalesRequested_Datatable.DataTable;

            SqlCommand cmd = new SqlCommand("proc_Update_Sales_Delete_Request", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@ptype", type);
            RunQuery(cmd);
            return "";
        }

        public Packages PackageByPackageId(int Package_id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Package_By_PackageId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPackage_id", Package_id);
            List<Packages> temp = fetchPKGEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public Packages PackageItemByItemId(int Package_id, int Item_id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_PackageItem_By_ItemId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPackage_id", Package_id);
            cmd.Parameters.AddWithValue("@pItem_id", Item_id);
            List<Packages> temp = fetchPKGItemEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public SaleReturns SaleReturnedItemyItem_id(int Package_id, int Item_id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SaleReturnedItems_By_ItemId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pPackage_id", Package_id);
            cmd.Parameters.AddWithValue("@pItem_id", Item_id);
            List<SaleReturns> temp = fetchSRItemEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public SalesOrders SelectBySalesOrderNum(string SalesOrderNum)
        {
            SqlCommand cmd = new SqlCommand("Select * from SalesOrder where SaleOrderNo=@SaleOrderNo ", DALUtil.getConnection());
            cmd.Parameters.AddWithValue("@SaleOrderNo", SalesOrderNum);
            List<SalesOrders> temp = fetchPIEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public List<SO_Payments> SelectSO_IvnvoicePayment(int id)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_SO_Payments_By_invoiceID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSO_Invoice_id", id);

            return fetchSOPaymentHistoryEntries(cmd);
        }

        private List<SO_Payments> fetchSOPaymentHistoryEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SO_Payments> payments = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        payments = new List<SO_Payments>();
                        while (dr.Read())
                        {
                            SO_Payments li = new SO_Payments();
                            li.SO_Payment_id = Convert.ToInt32(dr["SO_Payment_id"]);
                            li.Invoice_id = Convert.ToInt32(dr["SO_Invoice_id"]);
                            li.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                            li.Payment_Mode = Convert.ToInt32(dr["SO_Payment_Mode"]);
                            li.Payment_Date = Convert.ToString(dr["SO_Payment_Date"]);
                            li.Total_Amount = Convert.ToDecimal(dr["SO_Total_Amount"]);
                            li.Paid_Amount = Convert.ToDecimal(dr["SO_Paid_Amount"]);
                            li.Balance_Amount = Convert.ToDecimal(dr["SO_Balance_Amount"]);
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

        private List<SalesOrders> fetchPIEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SalesOrders> salesorders = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesorders = new List<SalesOrders>();
                    while (dr.Read())
                    {
                        SalesOrders li = new SalesOrders();
                        li.SalesOrder_id = Convert.ToInt32(dr["id"]);
                        salesorders.Add(li);
                    }
                    salesorders.TrimExcess();
                }
            }
            return salesorders;
        }

        public SalesOrders SelectSOById(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SalesOrder_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);
            List<SalesOrders> temp = fetchEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public SO_Invoices SelectSOInvoicePayment(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SO_Invoice_By_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);
            List<SO_Invoices> temp = fetchSOinvoiceEntries(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public List<SalesOrders> SelectAllSOItems(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SalesOrder_Item", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);
            return fetchiEntries(cmd);
        }

        public List<SaleReturns> SelectAllSRItemsSO_id(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SaleReturnedItems_By_SO_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);
            return fetchSRItemSO_IDEntries(cmd);
        }

        public List<SalesOrders> SelectItemsForSaleRetrurn(int id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Items_SaleReturn", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);
            return fetchSRiEntries(cmd);
        }

        public SalesOrders SelectSOdetailItems(int item_id, int SO_id)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_SalesOrderDetail_BY_ItemID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItemId", item_id);
            cmd.Parameters.AddWithValue("@SalesOrder_id", SO_id);
            List<SalesOrders> temp = SelectSOdetailItems(cmd);
            return (temp != null) ? temp[0] : null;
        }

        public List<Packages> SelectPackagesForSO(int id)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Packages_By_SO_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);

            return fetchPackagesForSO(cmd);
        }

        public List<SaleReturns> SelectSaleReturnsForSO(int id)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_SaleReturns_By_SO_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);

            return fetchSaleReturnsForSO(cmd);
        }

        public List<Packages> SelectPackagesForShipment(int id)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_PackagesForShipment_By_SO_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pSalesOrder_id", id);

            return fetchPackagesForSO(cmd);
        }

        public List<Packages> SelectPackagedItemsBySOid(int id, int SO_ID)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_PackagesDetail_By_SO_ID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_Id", id);
            cmd.Parameters.AddWithValue("@pSalesOrder_id", SO_ID);

            return fetchPackagedItemsBySOid(cmd);
        }

        public void UpdatepSaleOrder(SalesOrders S)
        {
            SqlCommand cmd = new SqlCommand("proc_UpdateSaleOrderVisibility", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pid", S.SalesOrder_id);
            cmd.Parameters.AddWithValue("@pEnable", S.Enable);
            RunQuery(cmd);
        }

        public void DeleteItemFromSaleOrder(int sdid)
        {
            SqlCommand cmd = new SqlCommand("proc_DeleteItemFromSales", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@psd_id", sdid);
            RunQuery(cmd);
        }

        public List<SalesOrders> CustomerTransactions_Items(int Customer_id, string Search)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Sold_Items_By_CustomerId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCustomer_id", Customer_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);
            return fetchCTIEntries(cmd);
        }

        public List<SO_Invoices> CustomerTransactions_Invoices(int Customer_id, string Search)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Bills_By_CustomerId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCustomer_id", Customer_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);
            return fetchCTINEntries(cmd);
        }

        public List<SO_Payments> CustomerTransactions_Payments(int Customer_id, string Search)
        {
            SqlCommand cmd = new SqlCommand("proc_Select_Payments_By_CustomerId", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pCustomer_id", Customer_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);
            return fetchCTPEntries(cmd);
        }





        private List<SalesOrders> fetchiEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SalesOrders> salesorders = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesorders = new List<SalesOrders>();
                    while (dr.Read())
                    {
                        SalesOrders li = new SalesOrders();
                        li.sdid = Convert.ToInt32(dr["id"]);
                        li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                        li.ItemId = Convert.ToInt32(dr["ItemId"]);
                        li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                        li.ItemName = Convert.ToString(dr["ItemName"]);
                        li.Customer_Name = Convert.ToString(dr["CustomerName"]);
                        li.Customer_Address = Convert.ToString(dr["CustomerAddress"]);
                        li.Customer_Landline = Convert.ToString(dr["CustomerLandline"]);
                        li.Customer_Mobile = Convert.ToString(dr["CustomerMobile"]);
                        li.Customer_Email = Convert.ToString(dr["CustomerEmail"]);
                        li.MsrmntUnit = Convert.ToString(dr["MsrmntUnit"]);
                        li.PriceUnit = Convert.ToInt32(dr["PriceUnit"]);
                        li.Packed_Qty = Convert.ToString(dr["PackgingQty"] != DBNull.Value ? (string)dr["PackgingQty"] : "0");
                        li.InvoicedQty = Convert.ToString(dr["InvoicedQty"] != DBNull.Value ? (string)dr["InvoicedQty"] : "0");
                        li.PackageCost = Convert.ToString(dr["Package_Cost"]);
                        li.ItemQty = Convert.ToString(dr["Qty"]);
                        li.TotalItems = Convert.ToString(Convert.ToInt32(dr["Qty"]) * Convert.ToInt32(dr["PriceUnit"]));
                        salesorders.Add(li);
                    }
                    salesorders.TrimExcess();
                }
            }
            return salesorders;
        }

        private List<SalesOrders> fetchSRiEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SalesOrders> salesorders = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesorders = new List<SalesOrders>();
                    while (dr.Read())
                    {
                        SalesOrders li = new SalesOrders();
                        li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                        li.ItemId = Convert.ToInt32(dr["ItemId"]);
                        li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                        li.ItemName = Convert.ToString(dr["ItemName"]);
                        li.Customer_Name = Convert.ToString(dr["CustomerName"]);
                        li.Customer_Address = Convert.ToString(dr["CustomerAddress"]);
                        li.Customer_Landline = Convert.ToString(dr["CustomerLandline"]);
                        li.Customer_Mobile = Convert.ToString(dr["CustomerMobile"]);
                        li.Customer_Email = Convert.ToString(dr["CustomerEmail"]);
                        li.MsrmntUnit = Convert.ToInt32(dr["Qty"]) + Convert.ToString(dr["MsrmntUnit"]);
                        li.PriceUnit = Convert.ToInt32(dr["PriceUnit"]);
                        li.Packed_Qty = Convert.ToString(dr["PackgingQty"] != DBNull.Value ? (string)dr["PackgingQty"] : "0");
                        li.Package_id = Convert.ToInt32(dr["Package_id"]);
                        li.PackageCost = Convert.ToString(dr["Package_Cost"]);
                        li.ItemQty = Convert.ToString(dr["Qty"]);
                        li.ReturnQty = Convert.ToString(dr["ReturnQty"] != DBNull.Value ? (string)dr["ReturnQty"] : "0");
                        li.TotalItems = Convert.ToString(Convert.ToInt32(dr["Qty"]) * Convert.ToInt32(dr["PriceUnit"]));
                        salesorders.Add(li);
                    }
                    salesorders.TrimExcess();
                }
            }
            return salesorders;
        }

        private List<SaleReturns> fetchSRItemSO_IDEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SaleReturns> salesorders = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesorders = new List<SaleReturns>();
                    while (dr.Read())
                    {
                        SaleReturns li = new SaleReturns();
                        li.SaleOrder_id = Convert.ToInt32(dr["SaleOrder_id"]);
                        li.SaleReturn_id = Convert.ToInt32(dr["SaleReturn_id"]);
                        li.Package_id = Convert.ToInt32(dr["Package_id"]);
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        li.Item_Name = Convert.ToString(dr["Item_Name"]);
                        li.PriceUnit = Convert.ToString(dr["PriceUnit"]);
                        li.ItemQty = Convert.ToString(dr["Qty"]);
                        li.Return_Qty = Convert.ToString(dr["Return_Qty"] != DBNull.Value ? (string)dr["Return_Qty"] : "0");
                        li.Received_Qty = Convert.ToString(dr["Received_Qty"] != DBNull.Value ? (string)dr["Received_Qty"] : "0");
                        salesorders.Add(li);
                    }
                    salesorders.TrimExcess();
                }
            }
            return salesorders;
        }

        private List<SalesOrders> fetchEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SalesOrders> salesorders = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    salesorders = new List<SalesOrders>();
                    while (dr.Read())
                    {
                        SalesOrders li = new SalesOrders();
                        li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                        li.SaleOrderNo = Convert.ToString(dr["SaleOrderNo"]);
                        li.PremisesId = Convert.ToInt32(dr["PremisesId"]);
                        li.Premises_Name = Convert.ToString(dr["PremisesName"]);
                        li.UserId = Convert.ToInt32(dr["UserId"]);
                        li.User_Name = Convert.ToString(dr["UserName"]);
                        li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                        li.Customer_Name = Convert.ToString(dr["CustomerName"]);
                        li.Customer_Address = Convert.ToString(dr["CustomerAddress"]);
                        li.Customer_Landline = Convert.ToString(dr["CustomerLandline"]);
                        li.Customer_Mobile = Convert.ToString(dr["CustomerMobile"]);
                        li.Customer_Email = Convert.ToString(dr["CustomerEmail"]);
                        li.SO_Invoice_id = dr["SO_Invoice_id"] != DBNull.Value ? (int)dr["SO_Invoice_id"] : 0;
                        li.TotalItems = Convert.ToString(dr["TotalItems"]);
                        li.SO_Total_Amount = Convert.ToString(dr["SO_Total_Amount"]);
                        li.SO_Status = Convert.ToString(dr["SO_Status"]);
                        li.SO_Invoice_Status = Convert.ToString(dr["SO_Invoice_Status"]);
                        li.SO_Package_Status = Convert.ToString(dr["SO_Package_Status"]);
                        li.SO_Shipment_Status = Convert.ToString(dr["SO_Shipment_Status"]);
                        //li.DeliveryStatus = Convert.ToString(dr["DeliveryStatus"]);
                        li.PackageCost = (dr["Package_Cost"] == DBNull.Value) ? "0" : Convert.ToString(dr["Package_Cost"]);
                        li.SO_Shipping_Charges = (dr["Shipment_Cost"] == DBNull.Value) ? "0" : Convert.ToString(dr["Shipment_Cost"]);
                        li.SO_DateTime = Convert.ToString(dr["SO_DateTime"]);
                        li.Enable = (dr["Enable"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Enable"]);
                        li.Time_Of_Day = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date_Of_Day = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month_Of_Day = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year_Of_Day = Convert.ToString(dr["Year_Of_Day"]);
                        li.AddedBy = Convert.ToInt32(dr["AddedBy"]);
                        salesorders.Add(li);
                    }
                    salesorders.TrimExcess();
                }
            }
            return salesorders;
        }

        private List<SO_Invoices> fetchSOinvoiceEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SO_Invoices> soinvoices = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    soinvoices = new List<SO_Invoices>();
                    while (dr.Read())
                    {
                        SO_Invoices li = new SO_Invoices();
                        li.id = Convert.ToInt32(dr["id"]);
                        li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                        li.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                        li.Invoice_Status = Convert.ToString(dr["Invoice_Status"]);
                        li.Invoice_Amount = Convert.ToDecimal(dr["Invoice_Amount"]);
                        li.Amount_Paid = Convert.ToDecimal(dr["Amount_Paid"]);
                        li.Balance_Amount = Convert.ToDecimal(dr["Balance_Amount"]);
                        li.InvoiceDateTime = Convert.ToString(dr["InvoiceDateTime"]);
                        li.InvoiceDueDate = Convert.ToString(dr["InvoiceDueDate"]);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        soinvoices.Add(li);
                    }
                    soinvoices.TrimExcess();
                }
            }
            return soinvoices;
        }

        private List<Packages> fetchPackagesForSO(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Packages> sopackages = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    sopackages = new List<Packages>();
                    while (dr.Read())
                    {
                        Packages li = new Packages();
                        li.Package_id = Convert.ToInt32(dr["Package_id"]);
                        li.SalesOrder_id = Convert.ToInt32(dr["SaleOrder_id"]);
                        li.Package_No = Convert.ToString(dr["Package_No"]);
                        li.PackageCost = Convert.ToString(dr["Package_Cost"]);
                        li.Package_Date = Convert.ToString(dr["Package_Date"]);
                        li.PackageStatus = Convert.ToString(dr["Package_Status"]);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        sopackages.Add(li);
                    }
                    sopackages.TrimExcess();
                }
            }
            return sopackages;
        }

        private List<Packages> fetchPackagedItemsBySOid(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Packages> sopackageitems = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    sopackageitems = new List<Packages>();
                    while (dr.Read())
                    {
                        Packages li = new Packages();
                        //li.Package_id = Convert.ToInt32((dr["Package_id"] == null) ? 0 : dr["Package_id"]);
                        //li.PackageDetail_id = Convert.ToInt32(dr["SO_PackageDetail_id"]);
                        //li.SalesOrder_id = Convert.ToInt32(dr["SaleOrder_id"]);
                        //li.Package_No = Convert.ToString(dr["Package_No"]);
                        li.Item_id = Convert.ToInt32(dr["Item_Id"]);
                        li.Item_Name = Convert.ToString(dr["ItemName"]);
                        //li.PackageStatus = Convert.ToString(dr["Package_Status"]);
                        li.Qty = Convert.ToString(dr["Total_Qty"]);
                        li.price = Convert.ToString(dr["price"]);
                        li.PackageCost = Convert.ToString(dr["PackageCost"]);
                        li.Packed_Qty = Convert.ToString((dr["Packed_Item_Qty"] == null) ? 0 : dr["Packed_Item_Qty"]);
                        sopackageitems.Add(li);
                    }
                    sopackageitems.TrimExcess();
                }
            }
            return sopackageitems;
        }

        private List<SalesOrders> SelectSOdetailItems(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SalesOrders> soitemdetail = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    soitemdetail = new List<SalesOrders>();
                    while (dr.Read())
                    {
                        SalesOrders li = new SalesOrders(); ;
                        li.PriceUnit = Convert.ToInt32(dr["PriceUnit"]);
                        soitemdetail.Add(li);
                    }
                    soitemdetail.TrimExcess();
                }
            }
            return soitemdetail;
        }

        public List<SalesOrders> ItemsTransactionSO(int Item_id, string Search)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_SO_By_ItemID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", Item_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);

            return fetchTSOEntries(cmd);
        }

        public List<SO_Invoices> ItemsTransactionINV(int Item_id, string Search)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("proc_Select_Invoices_By_ItemID", DALUtil.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pItem_id", Item_id);
            cmd.Parameters.AddWithValue("@pSearch", Search);

            return fetchTINVEntries(cmd);
        }

        private List<Packages> fetchItemID(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Packages> itemid = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    itemid = new List<Packages>();
                    while (dr.Read())
                    {
                        Packages li = new Packages();
                        li.Item_id = Convert.ToInt32(dr["Item_id"]);
                        itemid.Add(li);
                    }
                    itemid.TrimExcess();
                }
            }
            return itemid;
        }

        private List<SaleReturns> fetchSaleReturnsForSO(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SaleReturns> sosalereturns = null;
            con.Open();
            using (con)
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    sosalereturns = new List<SaleReturns>();
                    while (dr.Read())
                    {
                        SaleReturns li = new SaleReturns();
                        li.SaleReturn_id = Convert.ToInt32(dr["SaleReturn_id"]);
                        li.SaleOrder_id = Convert.ToInt32(dr["SaleOrder_id"]);
                        li.SaleReturnNo = Convert.ToString(dr["SaleReturnNo"]);
                        li.SaleReturn_Date = Convert.ToString(dr["SaleReturn_Date"]);
                        li.SaleReturn_Status = Convert.ToString(dr["SaleReturn_Status"]);
                        li.TotalReturn_Cost = Convert.ToString(dr["TotalReturn_Cost"]);
                        li.Time = Convert.ToString(dr["Time_Of_Day"]);
                        li.Date = Convert.ToString(dr["Date_Of_Day"]);
                        li.Month = Convert.ToString(dr["Month_Of_Day"]);
                        li.Year = Convert.ToString(dr["Year_Of_Day"]);
                        sosalereturns.Add(li);
                    }
                    sosalereturns.TrimExcess();
                }
            }
            return sosalereturns;
        }

        private List<SalesOrders> fetchTSOEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SalesOrders> so = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        so = new List<SalesOrders>();
                        while (dr.Read())
                        {
                            SalesOrders li = new SalesOrders();
                            li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                            li.SaleOrderNo = Convert.ToString(dr["SaleOrderNo"]);
                            li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                            li.Customer_Name = Convert.ToString(dr["CustomerName"]);
                            li.PriceUnit = Convert.ToSingle(dr["Price"]);
                            li.ItemQty = Convert.ToString(dr["QtyOrdered"]);
                            li.SO_Status = Convert.ToString(dr["SO_Status"]);
                            li.SO_DateTime = Convert.ToString(dr["SO_DateTime"]);
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

        private List<SO_Invoices> fetchTINVEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SO_Invoices> invoices = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        invoices = new List<SO_Invoices>();
                        while (dr.Read())
                        {
                            SO_Invoices li = new SO_Invoices();
                            li.id = Convert.ToInt32(dr["Invoice_id"]);
                            li.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                            li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                            li.Customer_Name = Convert.ToString(dr["CustomerName"]);
                            li.PriceUnit = Convert.ToSingle(dr["Price"]);
                            li.ItemQty = Convert.ToString(dr["QtyOrdered"]);
                            li.Invoice_Status = Convert.ToString(dr["Invoice_Status"]);
                            li.InvoiceDateTime = Convert.ToString(dr["InvoiceDateTime"]);
                            invoices.Add(li);
                        }
                        invoices.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                invoices = null;
            }
            return invoices;
        }

        private List<SO_Payments> fetchINVPEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SO_Payments> payment = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        payment = new List<SO_Payments>();
                        while (dr.Read())
                        {
                            SO_Payments li = new SO_Payments();
                            li.SO_Payment_id = Convert.ToInt32(dr["SO_Payment_id"]);
                            li.Invoice_id = Convert.ToInt32(dr["SO_Invoice_id"]);
                            li.Payment_Mode = Convert.ToInt32(dr["SO_Payment_Mode"]);
                            li.Payment_Date = Convert.ToString(dr["SO_Payment_Date"]);
                            li.Total_Amount = Convert.ToDecimal(dr["SO_Total_Amount"]);
                            li.Paid_Amount = Convert.ToDecimal(dr["SO_Paid_Amount"]);
                            li.Balance_Amount = Convert.ToDecimal(dr["SO_Balance_Amount"]);
                            li.AddedBy = Convert.ToInt32(dr["AddedBy"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Month = Convert.ToString(dr["Month_Of_Day"]);
                            li.Year = Convert.ToString(dr["Year_Of_Day"]);
                            payment.Add(li);
                        }
                        payment.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                payment = null;
            }
            return payment;
        }

        private List<SO_Invoices> fetchINVEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SO_Invoices> invoice = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        invoice = new List<SO_Invoices>();
                        while (dr.Read())
                        {
                            SO_Invoices li = new SO_Invoices();
                            li.id = Convert.ToInt32(dr["id"]);
                            li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                            li.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                            li.Invoice_Status = Convert.ToString(dr["Invoice_Status"]);
                            li.Invoice_Amount = Convert.ToDecimal(dr["Invoice_Amount"]);
                            li.Amount_Paid = Convert.ToDecimal(dr["Amount_Paid"]);
                            li.Balance_Amount = Convert.ToDecimal(dr["Balance_Amount"]);
                            li.InvoiceDateTime = Convert.ToString(dr["InvoiceDateTime"]);
                            li.InvoiceDateTime = Convert.ToString(dr["InvoiceDueDate"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Month = Convert.ToString(dr["Month_Of_Day"]);
                            li.Year = Convert.ToString(dr["Year_Of_Day"]);
                            invoice.Add(li);
                        }
                        invoice.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                invoice = null;
            }
            return invoice;
        }

        private List<Packages> fetchPKGEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Packages> package = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        package = new List<Packages>();
                        while (dr.Read())
                        {
                            Packages li = new Packages();
                            li.Package_id = Convert.ToInt32(dr["Package_id"]);
                            li.SalesOrder_id = Convert.ToInt32(dr["SaleOrder_id"]);
                            li.Package_No = Convert.ToString(dr["Package_No"]);
                            li.PackageCost = Convert.ToString(dr["Package_Cost"]);
                            li.PackageStatus = Convert.ToString(dr["Package_Status"]);
                            li.Package_Date = Convert.ToString(dr["Package_Date"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Month = Convert.ToString(dr["Month_Of_Day"]);
                            li.Year = Convert.ToString(dr["Year_Of_Day"]);
                            package.Add(li);
                        }
                        package.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                package = null;
            }
            return package;
        }

        private List<Packages> fetchPKGItemEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<Packages> package = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        package = new List<Packages>();
                        while (dr.Read())
                        {
                            Packages li = new Packages();
                            li.PackageDetail_id = Convert.ToInt32(dr["SO_PackageDetail_id"]);
                            li.SalesOrder_id = Convert.ToInt32(dr["SaleOrder_id"]);
                            li.Package_id = Convert.ToInt32(dr["Package_id"]);
                            li.Item_id = Convert.ToInt32(dr["Item_id"]);
                            li.price = Convert.ToString(dr["PriceUnit"]);
                            li.Packed_Qty = Convert.ToString(dr["Packed_Item_Qty"]);
                            li.Package_Date = Convert.ToString(dr["Package_Date"]);
                            package.Add(li);
                        }
                        package.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                package = null;
            }
            return package;
        }

        private List<SaleReturns> fetchSRItemEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SaleReturns> saleReturns = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        saleReturns = new List<SaleReturns>();
                        while (dr.Read())
                        {
                            SaleReturns li = new SaleReturns();
                            li.SaleOrder_id = Convert.ToInt32(dr["SaleOrder_id"]);
                            li.SaleReturn_id = Convert.ToInt32(dr["SaleReturn_id"]);
                            li.Package_id = Convert.ToInt32(dr["Package_id"]);
                            li.Item_id = Convert.ToInt32(dr["Item_id"]);
                            li.Item_Name = Convert.ToString(dr["Item_Name"]);
                            li.Return_Qty = Convert.ToString(dr["Return_Qty"]);
                            li.Received_Qty = Convert.ToString(dr["Received_Qty"]);
                            li.ReturnQty_Cost = Convert.ToString(dr["ReturnQty_Cost"]);

                            saleReturns.Add(li);
                        }
                        saleReturns.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                saleReturns = null;
            }
            return saleReturns;
        }

        private List<SalesOrders> fetchCTIEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SalesOrders> items = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        items = new List<SalesOrders>();
                        while (dr.Read())
                        {
                            SalesOrders li = new SalesOrders();
                            li.sdid = Convert.ToInt32(dr["id"]);
                            li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                            li.SaleOrderNo = Convert.ToString(dr["SaleOrderNo"]);
                            li.ItemId = Convert.ToInt32(dr["ItemId"]);
                            li.ItemName = Convert.ToString(dr["Item_Name"]);
                            li.Customer_id = Convert.ToInt32(dr["Customer_id"]);
                            li.ItemQty = Convert.ToString(dr["Qty"]);
                            li.PriceUnit = Convert.ToDouble(dr["PriceUnit"]);
                            li.MsrmntUnit = Convert.ToString(dr["MsrmntUnit"]);
                            li.Date_Of_Day = Convert.ToString(dr["Date_Of_Day"]);
                            li.Time_Of_Day = Convert.ToString(dr["Time_Of_Day"]);
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

        private List<SO_Invoices> fetchCTINEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SO_Invoices> invoices = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        invoices = new List<SO_Invoices>();
                        while (dr.Read())
                        {
                            SO_Invoices li = new SO_Invoices();
                            li.id = Convert.ToInt32(dr["Invoice_id"]);
                            li.SalesOrder_id = Convert.ToInt32(dr["SalesOrder_id"]);
                            li.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                            li.Invoice_Status = Convert.ToString(dr["Invoice_Status"]);
                            li.Invoice_Amount = Convert.ToInt32(dr["Invoice_Amount"]);
                            li.Balance_Amount = dr["Balance_Amount"] != DBNull.Value ? (decimal)dr["Balance_Amount"] : 0;
                            li.Date = Convert.ToString(dr["Date_Of_Day"]);
                            li.Time = Convert.ToString(dr["Time_Of_Day"]);
                            invoices.Add(li);
                        }
                        invoices.TrimExcess();
                    }
                }
            }
            catch (Exception e)
            {
                invoices = null;
            }
            return invoices;
        }

        private List<SO_Payments> fetchCTPEntries(SqlCommand cmd)
        {
            SqlConnection con = cmd.Connection;
            List<SO_Payments> payments = null;
            con.Open();
            try
            {
                using (con)
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        payments = new List<SO_Payments>();
                        while (dr.Read())
                        {
                            SO_Payments li = new SO_Payments();
                            li.SO_Payment_id = Convert.ToInt32(dr["SO_Payment_id"]);
                            li.Invoice_id = Convert.ToInt32(dr["SO_Invoice_id"]);
                            li.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                            li.PaymentMode = Convert.ToString(dr["SO_Payment_Mode"]);
                            li.Total_Amount = Convert.ToDecimal(dr["SO_Total_Amount"]);
                            li.Paid_Amount = Convert.ToDecimal(dr["SO_Paid_Amount"]);
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