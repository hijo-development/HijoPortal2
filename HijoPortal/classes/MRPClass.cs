using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using DevExpress.Web;
using System.Net.NetworkInformation;

namespace HijoPortal.classes
{
    public class MRPClass
    {
        public const string capex_logs = "CAPEX", opex_logs = "OPEX", directmaterials_logs = "DIRECTMATERIALS", manpower_logs = "MANPOWER", revenueassumption_logs = "REVENUEASSUMPTION", add_logs = "ADD", edit_logs = "EDIT", delete_logs = "DELETE";

        static double
            capex_total_amount = 0,
            opex_total_amount = 0,
            materials_total_amount = 0,
            manpower_total_amount = 0,
            revenue_total_amount = 0;

        public static string Month_Name(int iMonth)
        {
            DateTime now = DateTime.Now;
            string sMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(iMonth);
            return sMonth;
        }

        public static DataTable Master_MRP_List()
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("DocNumber", typeof(string));
                dtTable.Columns.Add("DateCreated", typeof(string));
                dtTable.Columns.Add("EntityCode", typeof(string));
                dtTable.Columns.Add("EntityCodeDesc", typeof(string));
                dtTable.Columns.Add("BUCode", typeof(string));
                dtTable.Columns.Add("BUCodeDesc", typeof(string));
                dtTable.Columns.Add("MRPMonth", typeof(Int32));
                dtTable.Columns.Add("MRPMonthDesc", typeof(string));
                dtTable.Columns.Add("MRPYear", typeof(Int32));
                dtTable.Columns.Add("Amount", typeof(Double));
                dtTable.Columns.Add("StatusKey", typeof(string));
                dtTable.Columns.Add("StatusKeyDesc", typeof(string));
                dtTable.Columns.Add("CreatorKey", typeof(Int32));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            //string query = "SELECT tbl_MRP_List.*, tbl_MRP_Status.StatusName FROM tbl_MRP_List INNER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK";
            string query = "SELECT TOP (100) PERCENT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, " +
                           " dbo.tbl_MRP_List.DateCreated, dbo.tbl_MRP_List.EntityCode, dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                           " dbo.tbl_MRP_List.BUCode, dbo.vw_AXOperatingUnitTable.NAME AS BUCodeDesc, dbo.tbl_MRP_List.MRPMonth, " +
                           " dbo.tbl_MRP_List.MRPYear, dbo.tbl_MRP_List.StatusKey, dbo.tbl_MRP_Status.StatusName, " +
                           " dbo.tbl_MRP_List.CreatorKey, dbo.tbl_MRP_List.LastModified " +
                           " FROM  dbo.tbl_MRP_List LEFT OUTER JOIN " +
                           " dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                           " dbo.tbl_MRP_Status ON dbo.tbl_MRP_List.StatusKey = dbo.tbl_MRP_Status.PK LEFT OUTER JOIN " +
                           " dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID " +
                           " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            double dummy = 12.2;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["DocNumber"] = row["DocNumber"].ToString();
                    dtRow["DateCreated"] = Convert.ToDateTime(row["DateCreated"]).ToString("MM/dd/yyyy");
                    dtRow["EntityCode"] = row["EntityCode"].ToString();
                    dtRow["EntityCodeDesc"] = row["EntityCodeDesc"].ToString();
                    dtRow["BUCode"] = row["BUCode"].ToString();
                    dtRow["BUCodeDesc"] = row["BUCodeDesc"].ToString();
                    dtRow["MRPMonth"] = Convert.ToInt32(row["MRPMonth"]);
                    dtRow["MRPMonthDesc"] = Month_Name(Convert.ToInt32(row["MRPMonth"]));
                    dtRow["MRPYear"] = Convert.ToInt32(row["MRPYear"]);
                    dtRow["Amount"] = Convert.ToDouble(dummy);
                    dtRow["StatusKey"] = row["StatusKey"].ToString();
                    dtRow["StatusKeyDesc"] = row["StatusName"].ToString();
                    dtRow["CreatorKey"] = Convert.ToInt32(row["CreatorKey"]);
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable Master_MRP_List_DOCNUM()
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("DocNumber", typeof(string));
                dtTable.Columns.Add("EntityCode", typeof(string));
                dtTable.Columns.Add("BUCode", typeof(string));
                dtTable.Columns.Add("MRPMonth", typeof(string));
            }

            string query = "SELECT tbl_MRP_List.DocNumber, tbl_MRP_List.MRPMonth, tbl_MRP_List.MRPYear, vw_AXOperatingUnitTable.NAME, vw_AXEntityTable.NAME AS BU FROM tbl_MRP_List INNER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER INNER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID ORDER BY DocNumber DESC";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["DocNumber"] = row["DocNumber"].ToString();
                    dtRow["EntityCode"] = row["NAME"].ToString();
                    dtRow["BUCode"] = row["BU"].ToString();
                    dtRow["MRPMonth"] = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(row["MRPMonth"])) + "-" + row["MRPYear"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRP_Direct_Materials(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            materials_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("ItemDescription", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string query = "SELECT DISTINCT tbl_MRP_List_DirectMaterials.*, vw_AXFindimActivity.DESCRIPTION FROM   tbl_MRP_List_DirectMaterials INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_DirectMaterials.ActivityCode = vw_AXFindimActivity.VALUE WHERE tbl_MRP_List_DirectMaterials.HeaderDocNum = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ActivityCode"] = row["DESCRIPTION"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["ItemDescription"] = row["ItemDescription"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    PrintString(row["Cost"].ToString());
                    materials_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRPInvent_Direct_Materials(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            materials_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("ItemDescription", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittiedTotalCost", typeof(string));
            }

            string query = "SELECT DISTINCT tbl_MRP_List_DirectMaterials.*, vw_AXFindimActivity.DESCRIPTION FROM   tbl_MRP_List_DirectMaterials INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_DirectMaterials.ActivityCode = vw_AXFindimActivity.VALUE WHERE tbl_MRP_List_DirectMaterials.HeaderDocNum = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ActivityCode"] = row["DESCRIPTION"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["ItemDescription"] = row["ItemDescription"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittiedTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRPApproval_Direct_Materials(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            materials_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("ItemDescription", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittiedTotalCost", typeof(string));
                dtTable.Columns.Add("ApprovedCost", typeof(string));
                dtTable.Columns.Add("ApprovedQty", typeof(Double));
                dtTable.Columns.Add("ApprovedTotalCost", typeof(string));
            }

            string query = "SELECT DISTINCT tbl_MRP_List_DirectMaterials.*, vw_AXFindimActivity.DESCRIPTION FROM   tbl_MRP_List_DirectMaterials INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_DirectMaterials.ActivityCode = vw_AXFindimActivity.VALUE WHERE tbl_MRP_List_DirectMaterials.HeaderDocNum = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ActivityCode"] = row["DESCRIPTION"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["ItemDescription"] = row["ItemDescription"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittiedTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");
                    dtRow["ApprovedQty"] = Convert.ToDouble(row["ApprovedQty"]);
                    dtRow["ApprovedCost"] = Convert.ToDouble(row["ApprovedCost"]).ToString("N");
                    dtRow["ApprovedTotalCost"] = Convert.ToDouble(row["ApprovedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRP_OPEX(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            opex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ExpenseCodeName", typeof(string));
                dtTable.Columns.Add("ExpenseCode", typeof(string));
                dtTable.Columns.Add("isItem", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string query = "SELECT tbl_MRP_List_OPEX.PK, tbl_MRP_List_OPEX.HeaderDocNum, tbl_MRP_List_OPEX.ExpenseCode, tbl_MRP_List_OPEX.ItemCode, tbl_MRP_List_OPEX.Description, tbl_MRP_List_OPEX.UOM, tbl_MRP_List_OPEX.Cost, tbl_MRP_List_OPEX.Qty, tbl_MRP_List_OPEX.TotalCost, vw_AXExpenseAccount.NAME, vw_AXExpenseAccount.isItem FROM   tbl_MRP_List_OPEX INNER JOIN vw_AXExpenseAccount ON tbl_MRP_List_OPEX.ExpenseCode = vw_AXExpenseAccount.MAINACCOUNTID WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ExpenseCode"] = row["ExpenseCode"].ToString();
                    dtRow["ExpenseCodeName"] = row["NAME"].ToString();
                    dtRow["isItem"] = row["isItem"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    opex_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRPInvent_OPEX(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            opex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ExpenseCodeName", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittedTotalCost", typeof(string));
            }

            string query = "SELECT tbl_MRP_List_OPEX.*, vw_AXExpenseAccount.NAME FROM   tbl_MRP_List_OPEX INNER JOIN vw_AXExpenseAccount ON tbl_MRP_List_OPEX.ExpenseCode = vw_AXExpenseAccount.MAINACCOUNTID WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ExpenseCodeName"] = row["NAME"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittedTotalCost"] = Convert.ToDouble(row["EdittedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    opex_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRPApproval_OPEX(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            opex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ExpenseCodeName", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittedTotalCost", typeof(string));
                dtTable.Columns.Add("ApprovedCost", typeof(string));
                dtTable.Columns.Add("ApprovedQty", typeof(Double));
                dtTable.Columns.Add("ApprovedTotalCost", typeof(string));
            }

            string query = "SELECT tbl_MRP_List_OPEX.*, vw_AXExpenseAccount.NAME FROM   tbl_MRP_List_OPEX INNER JOIN vw_AXExpenseAccount ON tbl_MRP_List_OPEX.ExpenseCode = vw_AXExpenseAccount.MAINACCOUNTID WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ExpenseCodeName"] = row["NAME"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittedTotalCost"] = Convert.ToDouble(row["EdittedTotalCost"]).ToString("N");
                    dtRow["ApprovedQty"] = Convert.ToDouble(row["ApprovedQty"]);
                    dtRow["ApprovedCost"] = Convert.ToDouble(row["ApprovedCost"]).ToString("N");
                    dtRow["ApprovedTotalCost"] = Convert.ToDouble(row["ApprovedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    opex_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRP_ManPower(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            manpower_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ManPowerTypeKey", typeof(Int32));
                dtTable.Columns.Add("ManPowerTypeKeyName", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string query = "SELECT tbl_MRP_List_ManPower.*, tbl_System_ManPowerType.ManPowerTypeDesc, vw_AXFindimActivity.DESCRIPTION as AC_Desc FROM tbl_MRP_List_ManPower INNER JOIN tbl_System_ManPowerType ON tbl_MRP_List_ManPower.ManPowerTypeKey = tbl_System_ManPowerType.PK INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_ManPower.ActivityCode = vw_AXFindimActivity.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";
            //string query = "SELECT tbl_MRP_List_ManPower.*, tbl_System_ManPowerType.ManPowerTypeDesc FROM tbl_MRP_List_ManPower INNER JOIN tbl_System_ManPowerType ON tbl_MRP_List_ManPower.ManPowerTypeKey = tbl_System_ManPowerType.PK WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ActivityCode"] = row["AC_Desc"].ToString();
                    dtRow["ManPowerTypeKey"] = Convert.ToInt32(row["ManPowerTypeKey"]);
                    dtRow["ManPowerTypeKeyName"] = row["ManPowerTypeDesc"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    manpower_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }


        public static DataTable MRPInvent_ManPower(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            manpower_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ManPowerTypeKey", typeof(Int32));
                dtTable.Columns.Add("ManPowerTypeKeyName", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittiedTotalCost", typeof(string));
            }

            string query = "SELECT tbl_MRP_List_ManPower.*, tbl_System_ManPowerType.ManPowerTypeDesc, vw_AXFindimActivity.DESCRIPTION as AC_Desc FROM tbl_MRP_List_ManPower INNER JOIN tbl_System_ManPowerType ON tbl_MRP_List_ManPower.ManPowerTypeKey = tbl_System_ManPowerType.PK INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_ManPower.ActivityCode = vw_AXFindimActivity.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";
            //string query = "SELECT tbl_MRP_List_ManPower.*, tbl_System_ManPowerType.ManPowerTypeDesc FROM tbl_MRP_List_ManPower INNER JOIN tbl_System_ManPowerType ON tbl_MRP_List_ManPower.ManPowerTypeKey = tbl_System_ManPowerType.PK WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ActivityCode"] = row["AC_Desc"].ToString();
                    dtRow["ManPowerTypeKey"] = Convert.ToInt32(row["ManPowerTypeKey"]);
                    dtRow["ManPowerTypeKeyName"] = row["ManPowerTypeDesc"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittiedTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    manpower_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRPApproval_ManPower(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            manpower_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ManPowerTypeKey", typeof(Int32));
                dtTable.Columns.Add("ManPowerTypeKeyName", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittiedTotalCost", typeof(string));
                dtTable.Columns.Add("ApprovedCost", typeof(string));
                dtTable.Columns.Add("ApprovedQty", typeof(Double));
                dtTable.Columns.Add("ApprovedTotalCost", typeof(string));
            }

            string query = "SELECT tbl_MRP_List_ManPower.*, tbl_System_ManPowerType.ManPowerTypeDesc, vw_AXFindimActivity.DESCRIPTION as AC_Desc FROM tbl_MRP_List_ManPower INNER JOIN tbl_System_ManPowerType ON tbl_MRP_List_ManPower.ManPowerTypeKey = tbl_System_ManPowerType.PK INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_ManPower.ActivityCode = vw_AXFindimActivity.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";
            //string query = "SELECT tbl_MRP_List_ManPower.*, tbl_System_ManPowerType.ManPowerTypeDesc FROM tbl_MRP_List_ManPower INNER JOIN tbl_System_ManPowerType ON tbl_MRP_List_ManPower.ManPowerTypeKey = tbl_System_ManPowerType.PK WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ActivityCode"] = row["AC_Desc"].ToString();
                    dtRow["ManPowerTypeKey"] = Convert.ToInt32(row["ManPowerTypeKey"]);
                    dtRow["ManPowerTypeKeyName"] = row["ManPowerTypeDesc"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittiedTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");
                    dtRow["ApprovedQty"] = Convert.ToDouble(row["ApprovedQty"]);
                    dtRow["ApprovedCost"] = Convert.ToDouble(row["ApprovedCost"]).ToString("N");
                    dtRow["ApprovedTotalCost"] = Convert.ToDouble(row["ApprovedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    manpower_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRP_Revenue(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            revenue_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ProductName", typeof(string));
                dtTable.Columns.Add("FarmName", typeof(string));
                dtTable.Columns.Add("Prize", typeof(string));
                dtTable.Columns.Add("Volume", typeof(Double));
                dtTable.Columns.Add("TotalPrize", typeof(string));
            }

            string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_List_RevenueAssumptions] WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["ProductName"] = row["ProductName"].ToString();
                    dtRow["FarmName"] = row["FarmName"].ToString();
                    dtRow["Prize"] = Convert.ToDouble(row["Prize"]).ToString("N");
                    dtRow["Volume"] = Convert.ToDouble(row["Volume"]);
                    dtRow["TotalPrize"] = Convert.ToDouble(row["TotalPrize"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    revenue_total_amount += Convert.ToDouble(row["TotalPrize"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRP_CAPEX(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            capex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    //for Preview Page
                    capex_total_amount += Convert.ToDouble(row["TotalCost"]);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRPInvent_CAPEX(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            capex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittiedTotalCost", typeof(string));

            }

            string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittiedTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    //for Preview Page
                    capex_total_amount += Convert.ToDouble(row["TotalCost"]);

                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MRPApproval_CAPEX(string DOC_NUMBER)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            capex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("EdittedCost", typeof(string));
                dtTable.Columns.Add("EdittedQty", typeof(Double));
                dtTable.Columns.Add("EdittiedTotalCost", typeof(string));
                dtTable.Columns.Add("ApprovedCost", typeof(string));
                dtTable.Columns.Add("ApprovedQty", typeof(Double));
                dtTable.Columns.Add("ApprovedTotalCost", typeof(string));
            }

            string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

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
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]);
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["EdittedCost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                    dtRow["EdittedQty"] = Convert.ToDouble(row["EdittedQty"]);
                    dtRow["EdittiedTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");
                    dtRow["ApprovedQty"] = Convert.ToDouble(row["ApprovedQty"]);
                    dtRow["ApprovedCost"] = Convert.ToDouble(row["ApprovedCost"]).ToString("N");
                    dtRow["ApprovedTotalCost"] = Convert.ToDouble(row["ApprovedTotalCost"]).ToString("N");
                    dtTable.Rows.Add(dtRow);

                    //for Preview Page
                    capex_total_amount += Convert.ToDouble(row["TotalCost"]);

                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }


        public static DataTable PO_Table()
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            capex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("PONumber", typeof(string));
                dtTable.Columns.Add("MRPNumber", typeof(string));
                dtTable.Columns.Add("DateCreated", typeof(string));
                dtTable.Columns.Add("CreatorKey", typeof(string));
                dtTable.Columns.Add("CreatorName", typeof(string));
                dtTable.Columns.Add("ExpectedDate", typeof(string));
                dtTable.Columns.Add("VendorCode", typeof(string));
            }

            //string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_POCreation]";
            string query = "SELECT tbl_POCreation.PK, tbl_POCreation.PONumber, tbl_POCreation.MRPNumber, tbl_POCreation.DateCreated, tbl_POCreation.CreatorKey, tbl_POCreation.ExpectedDate, tbl_POCreation.VendorCode, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_POCreation INNER JOIN tbl_Users ON tbl_POCreation.CreatorKey = tbl_Users.PK";

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
                    dtRow["PONumber"] = row["PONumber"].ToString();
                    dtRow["MRPNumber"] = row["MRPNumber"].ToString();
                    dtRow["DateCreated"] = Convert.ToDateTime(row["DateCreated"]).ToString("MM/dd/yyyy");
                    dtRow["CreatorKey"] = row["CreatorKey"].ToString();
                    dtRow["CreatorName"] = EncryptionClass.Decrypt(row["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    //dtRow["ExpectedDate"] = row["ExpectedDate"].ToString();

                    if (!DBNull.Value.Equals(row["ExpectedDate"]))
                    {
                        dtRow["ExpectedDate"] = Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        dtRow["ExpectedDate"] = "";
                    }
                    dtRow["VendorCode"] = row["VendorCode"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }


        public static DataTable MRP_List()
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("DocNumber", typeof(string));
                dtTable.Columns.Add("DateCreated", typeof(string));
                dtTable.Columns.Add("MRPType", typeof(string));
                dtTable.Columns.Add("MRPTypeDesc", typeof(string));
                dtTable.Columns.Add("MRPMonth", typeof(Int32));
                dtTable.Columns.Add("MRPMonthDesc", typeof(string));
                dtTable.Columns.Add("MRPYear", typeof(Int32));
                dtTable.Columns.Add("StatusKey", typeof(string));
                dtTable.Columns.Add("StatusKeyDesc", typeof(string));
                dtTable.Columns.Add("EntityCode", typeof(string));
                dtTable.Columns.Add("BUSSUCode", typeof(string));
                dtTable.Columns.Add("CreatorKey", typeof(Int32));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT dbo.tbl_MRP.PK, dbo.tbl_MRP.DocNumber, dbo.tbl_MRP.DateCreated, dbo.tbl_MRP.MRPType, " +
                         " dbo.tbl_MRP_Type.MRPType AS MRPTypeDesc, dbo.tbl_MRP.MRPMonth, dbo.tbl_MRP.MRPYear, " +
                         " dbo.tbl_MRP.StatusKey, " +
                         " dbo.tbl_MRP_Status.StatusName, dbo.tbl_MRP.LastModified, dbo.tbl_MRP.EntityCode, " +
                         " dbo.tbl_MRP.BUSSUCode, dbo.tbl_MRP.CreatorKey " +
                         " FROM   dbo.tbl_MRP LEFT OUTER JOIN " +
                         " dbo.tbl_MRP_Type ON dbo.tbl_MRP.MRPType = dbo.tbl_MRP_Type.PK LEFT OUTER JOIN " +
                         " dbo.tbl_MRP_Status ON dbo.tbl_MRP.StatusKey = dbo.tbl_MRP_Status.PK LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_MRP.CreatorKey = dbo.tbl_Users.PK";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["DocNumber"] = row["DocNumber"].ToString();
                    dtRow["DateCreated"] = Convert.ToDateTime(row["DateCreated"]).ToString("MM/dd/yyyy");
                    dtRow["MRPType"] = row["MRPType"].ToString();
                    dtRow["MRPTypeDesc"] = row["MRPTypeDesc"].ToString();
                    dtRow["MRPMonth"] = Convert.ToInt32(row["MRPMonth"]);
                    dtRow["MRPMonthDesc"] = Month_Name(Convert.ToInt32(row["MRPMonth"]));
                    dtRow["MRPYear"] = Convert.ToInt32(row["MRPYear"]);
                    dtRow["StatusKey"] = row["StatusKey"].ToString();
                    dtRow["StatusKeyDesc"] = row["StatusName"].ToString();
                    dtRow["EntityCode"] = row["EntityCode"].ToString();
                    dtRow["BUSSUCode"] = row["BUSSUCode"].ToString();
                    dtRow["CreatorKey"] = Convert.ToInt32(row["CreatorKey"]);
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            //DataTable table = new DataTable("Players");
            //table.Columns.Add(new DataColumn("Size", typeof(int)));
            //table.Columns.Add(new DataColumn("Sex", typeof(char)));

            return dtTable;
        }

        public static DataTable AXInventTable(string str)
        {

            DataTable dtTable = new DataTable();

            if (string.IsNullOrEmpty(str)) return dtTable;

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ITEMID", typeof(string));
                dtTable.Columns.Add("NAMEALIAS", typeof(string));
            }

            string qry = "SELECT [ITEMID],[NAMEALIAS] FROM [hijo_portal].[dbo].[vw_AXInventTable] WHERE [NAMEALIAS] LIKE '%" + str + "%'";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ITEMID"] = row["ITEMID"].ToString();
                    dtRow["NAMEALIAS"] = row["NAMEALIAS"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable ExpenseCodeTable()
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("MAINACCOUNTID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
                dtTable.Columns.Add("isItem", typeof(int));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXExpenseAccount]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["MAINACCOUNTID"] = row["MAINACCOUNTID"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtRow["isItem"] = Convert.ToInt32(row["isItem"].ToString());
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable ActivityCodeTable()
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("VALUE", typeof(string));
                dtTable.Columns.Add("DESCRIPTION", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXFindimActivity]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["VALUE"] = row["VALUE"].ToString();
                    dtRow["DESCRIPTION"] = row["DESCRIPTION"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable UOMTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("SYMBOL", typeof(string));
                dtTable.Columns.Add("description", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXUnitOfMeasure]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["SYMBOL"] = row["SYMBOL"].ToString();
                    dtRow["description"] = row["description"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static string ActivityCodeDESCRIPTION(string code)
        {
            string query = "SELECT VALUE FROM [hijo_portal].[dbo].[vw_AXFindimActivity] where DESCRIPTION = '" + code + "'";

            string actcodeVal = "";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand queryCMD = new SqlCommand(query, conn);
            SqlDataReader reader = queryCMD.ExecuteReader();
            while (reader.Read())
            {
                actcodeVal = reader[0].ToString();
            }
            reader.Close();
            conn.Close();
            return actcodeVal;
        }

        public static DataTable MRPItems(string DocNumber, string mrp_type)
        {
            DataTable dtTable = new DataTable();
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            conn.Open();

            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("PK", typeof(Int32));
                dtTable.Columns.Add("ExpenseCode", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("ManPowerTypeKey", typeof(string));
                dtTable.Columns.Add("ManPowerTypeDesc", typeof(string));
                dtTable.Columns.Add("ItemDescription", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("ItemQty", typeof(float));
                dtTable.Columns.Add("HeadCount", typeof(Int32));
                dtTable.Columns.Add("ItemCost", typeof(float));
                dtTable.Columns.Add("ItemTotalCost", typeof(float));
                dtTable.Columns.Add("Purpose", typeof(string));
                dtTable.Columns.Add("DateRequired", typeof(DateTime));
            }

            string query = "";

            if (mrp_type == "MANPOWER")
            {
                query = "SELECT tbl_MRP_Items.PK, tbl_MRP_Items.HeaderDocNum, tbl_MRP_Items.ExpenseCode, tbl_MRP_Items.ActivityCode, tbl_MRP_Items.ItemCode, tbl_MRP_Items.ItemDescription, tbl_MRP_Items.UOM, tbl_MRP_Items.ItemQty, tbl_MRP_Items.ItemCost, tbl_MRP_Items.ItemTotalCost, tbl_MRP_Items.DateRequired, tbl_MRP_Items.HeadCount, tbl_MRP_Items.Purpose, tbl_System_ManPowerType.ManPowerTypeDesc, tbl_System_ManPowerType.PK AS ManPowerTypeDesc, tbl_MRP_Items.ManPowerTypeKey " +
                              "FROM   tbl_MRP_Items INNER JOIN tbl_System_ManPowerType ON tbl_MRP_Items.ManPowerTypeKey = tbl_System_ManPowerType.PK WHERE [HeaderDocNum] = '" + DocNumber + "'";
            }
            else
            {
                query = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_Items] WHERE [HeaderDocNum] = '" + DocNumber + "'";
            }

            cmd = new SqlCommand(query);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["ExpenseCode"] = row["ExpenseCode"].ToString();
                    dtRow["ActivityCode"] = row["ActivityCode"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["ManPowerTypeKey"] = row["ManPowerTypeKey"].ToString();
                    if (mrp_type == "MANPOWER")
                        dtRow["ManPowerTypeDesc"] = row["ManPowerTypeDesc"].ToString();
                    dtRow["ItemDescription"] = row["ItemDescription"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["ItemQty"] = Convert.ToDouble(row["ItemQty"]);
                    dtRow["HeadCount"] = Convert.ToInt32(row["HeadCount"]);
                    dtRow["ItemCost"] = Convert.ToDouble(row["ItemCost"]);
                    dtRow["ItemTotalCost"] = Convert.ToDouble(row["ItemTotalCost"]);
                    dtRow["Purpose"] = row["Purpose"].ToString();
                    dtRow["DateRequired"] = Convert.ToDateTime(row["DateRequired"]);
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            conn.Close();

            return dtTable;
        }

        public static DataTable RevAssumpTable()
        {
            DataTable dtTable = new DataTable();
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            conn.Open();

            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("PK", typeof(Int32));
                dtTable.Columns.Add("Ctrl", typeof(string));
                dtTable.Columns.Add("MRPMonth", typeof(Int32));
                dtTable.Columns.Add("MRPMonthName", typeof(string));
                dtTable.Columns.Add("MRPYear", typeof(Int32));
                dtTable.Columns.Add("LastModified", typeof(DateTime));
            }

            string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_RevenueAssumptions]";
            cmd = new SqlCommand(query);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["Ctrl"] = row["Ctrl"].ToString();
                    dtRow["MRPMonth"] = Convert.ToInt32(row["MRPMonth"]);
                    dtRow["MRPMonthName"] = Month_Name(Convert.ToInt32(row["MRPMonth"]));
                    dtRow["MRPYear"] = Convert.ToInt32(row["MRPYear"]);
                    dtRow["LastModified"] = Convert.ToDateTime(row["LastModified"]).ToString("MM/dd/yyyy hh:mm:ss tt");
                    dtTable.Rows.Add(dtRow);

                }
            }
            dt.Clear();
            conn.Close();

            return dtTable;
        }

        public static DataTable RevDetailsTable(int pk)
        {
            DataTable dtTable = new DataTable();
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            conn.Open();

            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("MasterKey", typeof(Int32));
                dtTable.Columns.Add("Line", typeof(Int32));
                dtTable.Columns.Add("Product", typeof(string));
                dtTable.Columns.Add("Farm", typeof(string));
                dtTable.Columns.Add("Volume", typeof(Double));
                dtTable.Columns.Add("Price", typeof(Double));
                dtTable.Columns.Add("Amount", typeof(Double));
            }

            string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_RevenueAssumptions_Details] WHERE [MasterKey] = '" + pk + "'";
            cmd = new SqlCommand(query);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["MasterKey"] = row["MasterKey"].ToString();
                    dtRow["Line"] = Convert.ToInt32(row["Line"]);
                    dtRow["Product"] = row["Product"].ToString();
                    dtRow["Farm"] = row["Farm"].ToString();
                    dtRow["Volume"] = Convert.ToDouble(row["Volume"]);
                    dtRow["Price"] = Convert.ToDouble(row["Price"]);
                    dtRow["Amount"] = Convert.ToDouble(row["Amount"]);
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            conn.Close();

            return dtTable;
        }

        public static DataTable POAddEdit_Table(string mrp_number, string type)
        {

            if (type != "ITEMGROUPID")
            {
                type = "'" + type + "'";
            }
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            capex_total_amount = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("TableIdentifier", typeof(string));
                dtTable.Columns.Add("MRPCategory", typeof(string));
                dtTable.Columns.Add("Item", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("POQty", typeof(Double));
                dtTable.Columns.Add("POCost", typeof(string));
                dtTable.Columns.Add("POTotalCost", typeof(string));
                dtTable.Columns.Add("TaxGroup", typeof(string));
                dtTable.Columns.Add("TaxItemGroup", typeof(string));
            }

            //string query = "SELECT * FROM [hijo_portal].[dbo].[tbl_POCreation]";
            //string query = "SELECT tbl_POCreation.PK, tbl_POCreation.PONumber, tbl_POCreation.MRPNumber, tbl_POCreation.DateCreated, tbl_POCreation.CreatorKey, tbl_POCreation.ExpectedDate, tbl_POCreation.VendorCode, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_POCreation INNER JOIN tbl_Users ON tbl_POCreation.CreatorKey = tbl_Users.PK";

            string query = "SELECT tbl_MRP_List_DirectMaterials.PK, tbl_MRP_List_DirectMaterials.TableIdentifier, tbl_MRP_List_DirectMaterials.ItemCode, tbl_MRP_List_DirectMaterials.ItemDescription, tbl_MRP_List_DirectMaterials.Qty, tbl_MRP_List_DirectMaterials.UOM, tbl_MRP_List_DirectMaterials.Cost, tbl_MRP_List_DirectMaterials.TotalCost, vw_AXInventTable.ITEMGROUPID FROM tbl_MRP_List_DirectMaterials INNER JOIN vw_AXInventTable ON tbl_MRP_List_DirectMaterials.ItemCode = vw_AXInventTable.ITEMID " +
                "WHERE tbl_MRP_List_DirectMaterials.HeaderDocNum = '" + mrp_number + "' AND ITEMGROUPID = " + type + " AND tbl_MRP_List_DirectMaterials.QtyPO = '0' " +
                "UNION SELECT tbl_MRP_List_OPEX.PK, tbl_MRP_List_OPEX.TableIdentifier, tbl_MRP_List_OPEX.ItemCode, tbl_MRP_List_OPEX.Description, tbl_MRP_List_OPEX.Qty, tbl_MRP_List_OPEX.UOM, tbl_MRP_List_OPEX.Cost, tbl_MRP_List_OPEX.TotalCost, vw_AXInventTable.ITEMGROUPID FROM tbl_MRP_List_OPEX INNER JOIN vw_AXInventTable ON tbl_MRP_List_OPEX.ItemCode = vw_AXInventTable.ITEMID " +
                "WHERE tbl_MRP_List_OPEX.HeaderDocNum = '" + mrp_number + "' AND ITEMGROUPID = " + type + " AND tbl_MRP_List_OPEX.QtyPO = '0'";
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
                    dtRow["TableIdentifier"] = row["TableIdentifier"].ToString();
                    dtRow["MRPCategory"] = row["ITEMGROUPID"].ToString();
                    dtRow["Item"] = row["ItemCode"].ToString() + "/" + row["ItemDescription"].ToString();
                    dtRow["Qty"] = row["Qty"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["POQty"] = row["Qty"].ToString();
                    dtRow["POCost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                    dtRow["POTotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                    dtRow["TaxGroup"] = "";
                    dtRow["TaxItemGroup"] = "";
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable ProCategoryTable()
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("NAME", typeof(string));
                dtTable.Columns.Add("DESCRIPTION", typeof(string));
            }

            string qry = "SELECT [NAME],[DESCRIPTION] FROM [hijo_portal].[dbo].[vw_AXProdCategory] ORDER BY NAME ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dtTable.Rows.Add("ALL", "ALL");
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtRow["DESCRIPTION"] = row["DESCRIPTION"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable InventSiteTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("NAME", typeof(string));
                dtTable.Columns.Add("SITEID", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSite] ORDER BY NAME ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtRow["SITEID"] = row["SITEID"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable InventSiteWarehouseTable(string ID)
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("warehouse", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSiteWarehouse] WHERE [INVENTSITEID] = '" + ID + "'  ORDER BY NAME ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["warehouse"] = row["warehouse"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable InventSiteLocationTable(string warehouse)
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("LocationCode", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSiteLocation] WHERE [WarehouseCode] = '" + warehouse + "'  ORDER BY WarehouseCode ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["LocationCode"] = row["LocationCode"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable VendTableTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ACCOUNTNUM", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT [ACCOUNTNUM],[NAME] FROM [hijo_portal].[dbo].[vw_AXVendTable]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ACCOUNTNUM"] = row["ACCOUNTNUM"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString(); ;
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable CurrencyTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("CURRENCYCODE", typeof(string));
                dtTable.Columns.Add("TXT", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXCurrency]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["CURRENCYCODE"] = row["CURRENCYCODE"].ToString();
                    dtRow["TXT"] = row["TXT"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable PaymTermTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PAYMTERMID", typeof(string));
                dtTable.Columns.Add("DESCRIPTION", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXPaymTerm]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PAYMTERMID"] = row["PAYMTERMID"].ToString();
                    dtRow["DESCRIPTION"] = row["DESCRIPTION"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable TaxGroupTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("TAXGROUP", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXTaxGroup]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["TAXGROUP"] = row["TAXGROUP"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable TaxItemGroupTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("TAXITEMGROUP", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXTaxItemGroup]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["TAXITEMGROUP"] = row["TAXITEMGROUP"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static void SqlSuccess(int result)
        {
            if (result > 0)
                PrintString("Updated");
            else
                PrintString("Failed Update");
        }

        public static string MOPTableName()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List]";
        }

        public static string DirectMatTable()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials]";
        }

        public static string OpexTable()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_OPEX]";
        }

        public static string ManPowerTable()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_ManPower]";
        }

        public static string CapexTable()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_CAPEX]";
        }

        public static string RevenueTable()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_RevenueAssumptions]";
        }

        public static void PrintString(string str)
        {
            System.Diagnostics.Debug.WriteLine(">>>" + str + "<<<");
        }

        public static string MaterialsTableLogs()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials_Logs]";
        }

        public static string OpexTableLogs()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_OPEX_Logs]";
        }

        public static string ManpowerTableLogs()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_ManPower_Logs]";
        }

        public static string CapexTableLogs()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_CAPEX_Logs]";
        }

        public static string RevenueTableLogs()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_RevenueAssumptions_Logs]";
        }

        public static string POTableName()
        {
            return "[hijo_portal].[dbo].[tbl_POCreation]";
        }

        public static string DocNumberTableName()
        {
            return "[hijo_portal].[dbo].[tbl_DocumentNumber]";
        }

        public static string DefaultPage()
        {
            return "default.aspx";
        }

        public static double capex_total()
        {
            return capex_total_amount;
        }

        public static double opex_total()
        {
            return opex_total_amount;
        }
        public static double materials_total()
        {
            return materials_total_amount;
        }

        public static double manpower_total()
        {
            return manpower_total_amount;
        }

        public static double revenue_total()
        {
            return revenue_total_amount;
        }

        public static bool CheckLogsExist(string table, string PK)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string query = "SELECT COUNT(*) FROM " + table + " where MasterKey = '" + PK + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            if (count > 0)//if the material has logs
                return true;
            else
                return false;
        }

        public static void UpdateLastModified(SqlConnection conn, string docnumber)
        {
            string update_last_modified = "UPDATE [hijo_portal].[dbo].[tbl_MRP_List] SET [LastModified] = '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "' WHERE [DocNumber] = '" + docnumber + "'";
            SqlCommand comm = new SqlCommand(update_last_modified, conn);
            comm.ExecuteNonQuery();
        }

        public static void AddLogsMOPList(SqlConnection conn, int MRPKey, string Remarks)
        {
            String PhysicalAdd = NetworkInterface.GetAllNetworkInterfaces()
                            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                            .Select(nic => nic.GetPhysicalAddress().ToString())
                            .FirstOrDefault();

            string ComputerUN = Environment.UserName;

            string add_logs = "INSERT INTO [hijo_portal].[dbo].[tbl_MRP_List_ModifiedLogs] ([MRPKey], [DateModified],[PhysicalAdd], [ComputerUN], [Remarks]) VALUES (@MRPKey, @DateModified, @PhysicalAdd, @ComputerUN, @Remarks)";
            SqlCommand comm = new SqlCommand(add_logs, conn);
            comm.Parameters.AddWithValue("@MRPKey", MRPKey);
            comm.Parameters.AddWithValue("@DateModified", DateTime.Now.ToString());
            comm.Parameters.AddWithValue("@PhysicalAdd", PhysicalAdd);
            comm.Parameters.AddWithValue("@ComputerUN", ComputerUN);
            comm.Parameters.AddWithValue("@Remarks", Remarks);
            comm.ExecuteNonQuery();
        }

        

        public static void SetBehaviorGrid(ASPxGridView grid)
        {
            if (grid.IsEditing || grid.IsNewRowEditing)
            {
                grid.SettingsBehavior.AllowSort = false;
                grid.SettingsBehavior.AllowAutoFilter = false;
                grid.SettingsBehavior.AllowHeaderFilter = false;
            }
            else
            {
                grid.SettingsBehavior.AllowSort = true;
                grid.SettingsBehavior.AllowAutoFilter = true;
                grid.SettingsBehavior.AllowHeaderFilter = true;
            }
        }
    }
}