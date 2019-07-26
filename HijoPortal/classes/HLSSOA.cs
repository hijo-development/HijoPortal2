using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DevExpress.Web;

namespace HijoPortal.classes
{
    public class HLSSOA
    {
        public static DataTable HLSSOAYear()
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("sYear", typeof(string));
            }

            string query = "SELECT Yr FROM dbo.vw_AXHLSSOA_Group GROUP BY Yr";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["sYear"] = row["Yr"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable HLSSOAWeekNum(int iYear)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("sWeekNum", typeof(string));
            }

            string query = "SELECT WEEK_NO FROM dbo.vw_AXHLSSOA_Group WHERE(Yr = "+ iYear + ") GROUP BY WEEK_NO HAVING(WEEK_NO > 0) ORDER BY WEEK_NO DESC";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["sWeekNum"] = row["WEEK_NO"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable HLSSOACustomer (int iYear, int iWeekNum)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("CustAccount", typeof(string));
                dtTable.Columns.Add("CustName", typeof(string));
            }

            string query = "SELECT CUSTACCOUNT, NAME FROM dbo.vw_AXHLSSOA_Group WHERE(Yr = "+ iYear + ") AND(WEEK_NO = "+ iWeekNum + ") GROUP BY CUSTACCOUNT, NAME ORDER BY NAME";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["CustAccount"] = row["CUSTACCOUNT"].ToString();
                    dtRow["CustName"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable HLSSOAWaybills (int iYear, int iWeekNum, string CustNum)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("Waybill", typeof(string));
                dtTable.Columns.Add("Year", typeof(string));
                dtTable.Columns.Add("WeekNum", typeof(string));
                dtTable.Columns.Add("CustCode", typeof(string));
            }

            string query = "SELECT WAYBILLNO FROM dbo.vw_AXHLSSOA WHERE(CUSTACCOUNT = '"+ CustNum + "') AND(WEEK_NO = "+ iWeekNum + ") AND(Yr = "+ iYear + ") GROUP BY WAYBILLNO ORDER BY WAYBILLNO";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["Waybill"] = row["WAYBILLNO"].ToString();
                    dtRow["Year"] = iYear.ToString();
                    dtRow["WeekNum"] = iWeekNum.ToString();
                    dtRow["CustCode"] = CustNum.ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static void SaveUpdateCustomerAttention(string sCustCode, string sAttName, string sAttNum)
        {
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmdIns = null;
            SqlCommand cmdUp = null;

            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            string query, insert, update;

            query = "SELECT NAME, ISNULL(ADDRESS,'') AS ADDRESS FROM dbo.vw_AXCustomerTable WHERE(ACCOUNTNUM = '" + sCustCode + "')";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                insert = "INSERT INTO tbl_HLS_CustomerAttention (CustCode, AttentionName, AttentionNumber) VALUES (@CustCode, @AttentionName, @AttentionNumber)";
                cmdIns = new SqlCommand(insert, cn);
                cmdIns.Parameters.AddWithValue("@CustCode", sCustCode);
                cmdIns.Parameters.AddWithValue("@AttentionName", sAttName);
                cmdIns.Parameters.AddWithValue("@AttentionNumber", sAttNum);
                cmdIns.ExecuteNonQuery();
            } else
            {
                update = "UPDATE tbl_HLS_CustomerAttention SET AttentionName = @AttentionName, AttentionNumber = @AttentionNumber WHERE (CustCode = @CustCode)";
                cmdUp = new SqlCommand(update, cn);
                cmdUp.Parameters.AddWithValue("@CustCode", sCustCode);
                cmdUp.Parameters.AddWithValue("@AttentionName", sAttName);
                cmdUp.Parameters.AddWithValue("@AttentionNumber", sAttNum);
                cmdUp.ExecuteNonQuery();
            }
            dt.Clear();           
            cn.Close();
        }

        public static DataTable Customer_Info(string sCustCode)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            DataTable dt1 = new DataTable();
            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            string query, query1;
            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("CustCode", typeof(string));
                dtTable.Columns.Add("CustName", typeof(string));
                dtTable.Columns.Add("CustAdd", typeof(string));
                dtTable.Columns.Add("CustAtt", typeof(string));
                dtTable.Columns.Add("CustAttNum", typeof(string));
            }

            query = "SELECT NAME, ISNULL(ADDRESS,'') AS ADDRESS FROM dbo.vw_AXCustomerTable WHERE(ACCOUNTNUM = '"+ sCustCode + "')";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["CustCode"] = sCustCode.ToString();
                    dtRow["CustName"] = row["NAME"].ToString();
                    dtRow["CustAdd"] = row["ADDRESS"].ToString();

                    query1 = "SELECT AttentionName, AttentionNumber FROM tbl_HLS_CustomerAttention WHERE (CustCode  = '" + sCustCode + "')";

                    cmd1 = new SqlCommand(query1);
                    cmd1.Connection = cn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            dtRow["CustAtt"] = row1["AttentionName"].ToString();
                            dtRow["CustAttNum"] = row1["AttentionNumber"].ToString();
                        }
                    }
                    dt1.Clear();
                    
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable SOA_Footer(string sSOANum, string sTrans)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            string query = "";

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("SOANum", typeof(string));
                dtTable.Columns.Add("SOADate", typeof(string));
                dtTable.Columns.Add("Year", typeof(string));
                dtTable.Columns.Add("WeekNum", typeof(string));
                dtTable.Columns.Add("Remarks", typeof(string));
                dtTable.Columns.Add("PreparedBy", typeof(string));
                dtTable.Columns.Add("PreparedByPost", typeof(string));
                dtTable.Columns.Add("CheckedBy", typeof(string));
                dtTable.Columns.Add("CheckedByPost", typeof(string));
                dtTable.Columns.Add("ApprovedBy", typeof(string));
                dtTable.Columns.Add("ApprovedByPost", typeof(string));
            }

            switch (sTrans)
            {
                case "Add":
                    {
                        query = "SELECT TOP (1) '' AS SOANum, '' AS SOADate, '' AS YearNo, '' AS WeekNo, '' AS Remarks, PreparedBy, PreparedByPost, CheckedBy, CheckedByPost, ApprovedBy, ApprovedByPost FROM tbl_HLS_Footer ORDER BY EffectDate DESC";
                        break;
                    }
                case "View":
                    {
                        query = "SELECT SOANum, SOADate, YearNo, WeekNo, Remarks, PreparedBy, PreparedByPost, CheckedBy, CheckedByPost, ApprovedBy, ApprovedByPost FROM tbl_HLS_StatementOfAccount WHERE (SOANum = '" + sSOANum + "')";
                        break;
                    }
            }

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["SOANum"] = row["SOANum"].ToString();
                    if (row["SOADate"].ToString() == "")
                    {
                        dtRow["SOADate"] = row["SOADate"].ToString();
                    } else
                    {
                        dtRow["SOADate"] = Convert.ToDateTime(row["SOADate"]).ToString("MM/dd/yyyy");
                    }
                    dtRow["Year"] = row["YearNo"].ToString();
                    dtRow["WeekNum"] = row["WeekNo"].ToString();
                    dtRow["Remarks"] = row["Remarks"].ToString();
                    dtRow["PreparedBy"] = row["PreparedBy"].ToString();
                    dtRow["PreparedByPost"] = row["PreparedByPost"].ToString();
                    dtRow["CheckedBy"] = row["CheckedBy"].ToString();
                    dtRow["CheckedByPost"] = row["CheckedByPost"].ToString();
                    dtRow["ApprovedBy"] = row["ApprovedBy"].ToString();
                    dtRow["ApprovedByPost"] = row["ApprovedByPost"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable SOAWaybill_for_Save(string sUserKey)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("Waybill", typeof(string));
                dtTable.Columns.Add("CustCode", typeof(string));
                dtTable.Columns.Add("Year", typeof(string));
                dtTable.Columns.Add("WeekNum", typeof(string));
            }
            string query = "SELECT WaybillNum, CustCode, WeekYear, WeekNum FROM tbl_HLS_StatementOfAccount_Temp WHERE (UserKey = " + sUserKey + ")";
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["Waybill"] = row["WaybillNum"].ToString();
                    dtRow["CustCode"] = row["CustCode"].ToString();
                    dtRow["Year"] = row["WeekYear"].ToString();
                    dtRow["WeekNum"] = row["WeekNum"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable HLSSOA_Details (string sSOANum, string sUserKey, string sCustCode, string sYear, string sWeekNum)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            //DataTable dt1 = new DataTable();
            //SqlCommand cmd1 = null;
            //SqlDataAdapter adp1;
            string query = "", sWaybill = "";
            int iNum = 0;
            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("Num", typeof(string));
                dtTable.Columns.Add("Date", typeof(string));
                dtTable.Columns.Add("PlateNum", typeof(string));
                dtTable.Columns.Add("Particulars", typeof(string));
                dtTable.Columns.Add("ContainerNum", typeof(string));
                dtTable.Columns.Add("Waybill", typeof(string));
                dtTable.Columns.Add("From", typeof(string));
                dtTable.Columns.Add("To", typeof(string));
                dtTable.Columns.Add("Amount", typeof(string));
            }
            if (sSOANum == "")
            {
                query = "SELECT WaybillNum FROM tbl_HLS_StatementOfAccount_Temp WHERE (UserKey = " + sUserKey + ")";
            } else
            {
                query = "SELECT dbo.tbl_HLS_StatementOfAccount_Details.WaybillNum FROM dbo.tbl_HLS_StatementOfAccount_Details LEFT OUTER JOIN dbo.tbl_HLS_StatementOfAccount ON dbo.tbl_HLS_StatementOfAccount_Details.MasterKey = dbo.tbl_HLS_StatementOfAccount.PK WHERE (dbo.tbl_HLS_StatementOfAccount.SOANum = '" + sSOANum + "')";
            }
            
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sWaybill = sWaybill + "," + row["WaybillNum"].ToString();
                }
            }
            dt.Clear();

            query = "AX_HLS_SODetails '" + sCustCode + "', '" + sWeekNum + "', " + sYear + ", '" + sWaybill + "'";
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    DataRow dtRow = dtTable.NewRow();
                    if (row["ITEMID"].ToString() == "SH-00001")
                    {
                        iNum = iNum + 1;
                        dtRow["Num"] = iNum.ToString("0#");
                        dtRow["Date"] = Convert.ToDateTime(row["ShippingDateRequested"]).ToString("MM/dd/yyyy");
                        dtRow["PlateNum"] = row["PLATENUM"].ToString();
                        dtRow["Particulars"] = row["DRIVER"].ToString();
                        dtRow["ContainerNum"] = row["CONTAINER_NO"].ToString();
                        dtRow["Waybill"] = row["WAYBILLNO"].ToString();
                        dtRow["From"] = row["DISTINATION_FROM"].ToString();
                        dtRow["To"] = row["DISTINCATION_TO"].ToString();
                        dtRow["Amount"] = Convert.ToDouble(row["LINEAMOUNT"]).ToString("#,##0.00");
                    } else
                    {
                        dtRow["Num"] = "";
                        dtRow["Date"] = "";
                        dtRow["PlateNum"] = "";
                        if (row["ITEMID"].ToString() == "SH-00002")
                        {
                            dtRow["Particulars"] = row["ITEMDesc"].ToString() + " (" + Convert.ToDouble(row["SALESPRICE"]).ToString("#,##0.00") + " PER " + row["SALESUNIT"].ToString() + " X " + Convert.ToDouble(row["SALESQTY"]).ToString("#,##0") + " " + row["SALESUNIT"].ToString() + ")";
                        } else
                        {
                            double dSalesPrice = Convert.ToDouble(row["SALESPRICE"]) * (1 + (Convert.ToDouble(row["OVAT"])/100));
                            dtRow["Particulars"] = row["ITEMDesc"].ToString() + " (" + Convert.ToDouble(row["SALESQTY"]).ToString("#,##0") + " " + row["SALESUNIT"].ToString() + " X " + dSalesPrice.ToString("#0") + "/" + row["SALESUNIT"].ToString() + ")";
                        }
                            
                        dtRow["ContainerNum"] = "";
                        dtRow["Waybill"] = "";
                        dtRow["From"] = "";
                        dtRow["To"] = "";
                        if (row["ITEMID"].ToString() == "SH-00002")
                        {
                            dtRow["Amount"] = Convert.ToDouble(row["LINEAMOUNT"]).ToString("#,##0.00");
                        } else
                        {
                            dtRow["Amount"] = Convert.ToDouble(row["LineAmount_VAT"]).ToString("#,##0.00");
                        }
                    }
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();

            cn.Close();
            return dtTable;
        }

        public static void Save_HLSSOA(string sSOANum, DateTime dSOADate, int iYear, int iWeek, string sCustCode, string sRemarks, string sPreparedBy, string sPreparedByPost, string sCheckedBy, string sCheckedByPost, string sApprovedBy, string sApprovedByPost, ASPxGridView grdWaybills, string sAttention, string sAttentionNum)
        {
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmdIns = null;
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            int iMasterKey = 0;
            cn.Open();
            string sInsert = "INSERT INTO tbl_HLS_StatementOfAccount (SOANum, SOADate, YearNo, WeekNo, CustomerCode, Remarks, PreparedBy, PreparedByPost, CheckedBy, CheckedByPost, ApprovedBy, ApprovedByPost) VALUES (@SOANum, @SOADate, @YearNo, @WeekNo, @CustomerCode, @Remarks, @PreparedBy, @PreparedByPost, @CheckedBy, @CheckedByPost, @ApprovedBy, @ApprovedByPost)";
            cmdIns = new SqlCommand(sInsert, cn);
            cmdIns.Parameters.AddWithValue("@SOANum", sSOANum);
            cmdIns.Parameters.AddWithValue("@SOADate", dSOADate);
            cmdIns.Parameters.AddWithValue("@YearNo", iYear);
            cmdIns.Parameters.AddWithValue("@WeekNo", iWeek);
            cmdIns.Parameters.AddWithValue("@CustomerCode", sCustCode);
            cmdIns.Parameters.AddWithValue("@Remarks", sRemarks);
            cmdIns.Parameters.AddWithValue("@PreparedBy", sPreparedBy);
            cmdIns.Parameters.AddWithValue("@PreparedByPost", sPreparedByPost);
            cmdIns.Parameters.AddWithValue("@CheckedBy", sCheckedBy);
            cmdIns.Parameters.AddWithValue("@CheckedByPost", sCheckedByPost);
            cmdIns.Parameters.AddWithValue("@ApprovedBy", sApprovedBy);
            cmdIns.Parameters.AddWithValue("@ApprovedByPost", sApprovedByPost);
            cmdIns.ExecuteNonQuery();

            string query = "SELECT PK FROM tbl_HLS_StatementOfAccount WHERE (SOANum = '" + sSOANum + "')";
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count> 0 )
            {
                foreach (DataRow row in dt.Rows)
                {
                    iMasterKey = Convert.ToInt32(row["PK"]);
                }
            }
            dt.Clear();

            if (iMasterKey > 0)
            {
                for (int i = 0; i <= (grdWaybills.VisibleRowCount - 1); i++)
                {
                    object keyValue = grdWaybills.GetRowValues(i, "Waybill");
                    sInsert = "INSERT INTO tbl_HLS_StatementOfAccount_Details (MasterKey, Line, WaybillNum) VALUES (@MasterKey, @Line, @WaybillNum)";
                    cmdIns = new SqlCommand(sInsert, cn);
                    cmdIns.Parameters.AddWithValue("@MasterKey", iMasterKey);
                    cmdIns.Parameters.AddWithValue("@Line", i + 1);
                    cmdIns.Parameters.AddWithValue("@WaybillNum", keyValue.ToString());
                    cmdIns.ExecuteNonQuery();
                }
            }

            query = "SELECT tbl_HLS_CustomerAttention.* FROM tbl_HLS_CustomerAttention WHERE (CustCode = '" + sCustCode + "')";
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                sInsert = "UPDATE tbl_HLS_CustomerAttention SET AttentionName = @AttentionName, AttentionNumber = @AttentionNumber) WHERE (CustCode = @CustCode)";
                cmdIns = new SqlCommand(sInsert, cn);
                cmdIns.Parameters.AddWithValue("@CustCode", sCustCode);
                cmdIns.Parameters.AddWithValue("@AttentionName", sAttention);
                cmdIns.Parameters.AddWithValue("@AttentionNumber", sAttentionNum);
                cmdIns.ExecuteNonQuery();
            } else
            {
                sInsert = "INSERT INTO tbl_HLS_CustomerAttention (CustCode, AttentionName, AttentionNumber) VALUES (@CustCode, @AttentionName, @AttentionNumber)";
                cmdIns = new SqlCommand(sInsert, cn);
                cmdIns.Parameters.AddWithValue("@CustCode", sCustCode);
                cmdIns.Parameters.AddWithValue("@AttentionName", sAttention);
                cmdIns.Parameters.AddWithValue("@AttentionNumber", sAttentionNum);
                cmdIns.ExecuteNonQuery();
            }
            dt.Clear();

            cn.Close();
        }

        public static DataTable HLSSOA_List()
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dt1 = new DataTable();
            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            string query = "", query1 = "", sWaybill = "";
            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("SOANum", typeof(string));
                dtTable.Columns.Add("SOADate", typeof(string));
                dtTable.Columns.Add("CustCode", typeof(string));
                dtTable.Columns.Add("CustName", typeof(string));
                dtTable.Columns.Add("Year", typeof(string));
                dtTable.Columns.Add("WeekNum", typeof(string));
                dtTable.Columns.Add("BillingInv", typeof(string));
                dtTable.Columns.Add("Amount", typeof(string));
            }
            query = "SELECT dbo.tbl_HLS_StatementOfAccount.PK, dbo.tbl_HLS_StatementOfAccount.SOANum, dbo.tbl_HLS_StatementOfAccount.SOADate, dbo.tbl_HLS_StatementOfAccount.CustomerCode, dbo.vw_AXCustomerTable.NAME, dbo.tbl_HLS_StatementOfAccount.YearNo, dbo.tbl_HLS_StatementOfAccount.WeekNo, dbo.tbl_HLS_StatementOfAccount.BillingInvoiceNum FROM dbo.tbl_HLS_StatementOfAccount LEFT OUTER JOIN dbo.vw_AXCustomerTable ON dbo.tbl_HLS_StatementOfAccount.CustomerCode = dbo.vw_AXCustomerTable.ACCOUNTNUM ORDER BY dbo.tbl_HLS_StatementOfAccount.SOANum DESC";
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["SOANum"] = row["SOANum"].ToString();
                    dtRow["SOADate"] = Convert.ToDateTime(row["SOADate"]).ToString("MM/dd/yyyy");
                    dtRow["CustCode"] = row["CustomerCode"].ToString();
                    dtRow["CustName"] = row["NAME"].ToString();
                    dtRow["Year"] = row["YearNo"].ToString();
                    dtRow["WeekNum"] = row["WeekNo"].ToString();
                    dtRow["BillingInv"] = row["BillingInvoiceNum"].ToString();

                    query1 = "SELECT dbo.tbl_HLS_StatementOfAccount_Details.WaybillNum FROM dbo.tbl_HLS_StatementOfAccount_Details LEFT OUTER JOIN dbo.tbl_HLS_StatementOfAccount ON dbo.tbl_HLS_StatementOfAccount_Details.MasterKey = dbo.tbl_HLS_StatementOfAccount.PK WHERE (dbo.tbl_HLS_StatementOfAccount.SOANum = '" + row["SOANum"].ToString() + "')";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Connection = cn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            sWaybill = sWaybill + "," + row1["WaybillNum"].ToString();
                        }
                        
                    }
                    dt1.Clear();

                    double dAmount = 0;
                    query1 = "AX_HLS_SODetails '" + row["CustomerCode"].ToString() + "', '" + row["WeekNo"].ToString() + "', " + row["YearNo"].ToString() + ", '" + sWaybill + "'"; ;
                    cmd1 = new SqlCommand(query1);
                    cmd1.Connection = cn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            if (row1["ITEMID"].ToString() == "SH-00001" || row1["ITEMID"].ToString() == "SH-00002")
                            {
                                dAmount = dAmount + Convert.ToDouble(row1["LINEAMOUNT"]);
                            } else
                            {
                                dAmount = dAmount + Convert.ToDouble(row1["LineAmount_VAT"]);
                            }
                        }
                    }
                    dt1.Clear();

                    dtRow["Amount"] = dAmount.ToString("#,##0.00");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }
    }
}