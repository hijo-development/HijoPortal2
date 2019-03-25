using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class POClass
    {

        public static DataTable PO_Created_List()
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
                dtTable.Columns.Add("PONumber", typeof(string));
                dtTable.Columns.Add("MOPNumber", typeof(string));
                dtTable.Columns.Add("VendorCode", typeof(string));
                dtTable.Columns.Add("VendorName", typeof(string));
                dtTable.Columns.Add("DateCreated", typeof(string));
                dtTable.Columns.Add("CreatorKey", typeof(string));
                dtTable.Columns.Add("Creator", typeof(string));
                dtTable.Columns.Add("ExpectedDate", typeof(string));
                dtTable.Columns.Add("TotalAmount", typeof(string));
                dtTable.Columns.Add("Status", typeof(string));
            }

            string qry = "SELECT dbo.tbl_POCreation.PK, dbo.tbl_POCreation.PONumber, dbo.tbl_POCreation.MRPNumber, " +
                         " dbo.tbl_POCreation.VendorCode, dbo.vw_AXVendTable.NAME AS VendorName, " +
                         " dbo.tbl_POCreation.DateCreated, dbo.tbl_POCreation.CreatorKey, " +
                         " dbo.tbl_Users.Firstname, dbo.tbl_Users.Lastname, dbo.tbl_POCreation.ExpectedDate " +
                         " FROM dbo.tbl_POCreation LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_POCreation.CreatorKey = dbo.tbl_Users.PK LEFT OUTER JOIN " +
                         " dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM " +
                         " ORDER BY dbo.tbl_POCreation.PONumber DESC";
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
                    dtRow["PONumber"] = row["PONumber"].ToString();
                    dtRow["MOPNumber"] = row["MRPNumber"].ToString();
                    dtRow["VendorCode"] = row["VendorCode"].ToString();
                    dtRow["VendorName"] = row["VendorName"].ToString();
                    dtRow["VendorCode"] = row["VendorCode"].ToString();
                    dtRow["DateCreated"] = Convert.ToDateTime(row["DateCreated"]).ToString("MM/dd/yyyy");
                    dtRow["CreatorKey"] = row["CreatorKey"].ToString();
                    dtRow["Creator"] = EncryptionClass.Decrypt(row["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    dtRow["ExpectedDate"] = Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy");
                    dtRow["TotalAmount"] = "0.00";
                    dtRow["Status"] = "Created";
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable SelectItemsForPO(int Month, int Year, string DocNumber)
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
                dtTable.Columns.Add("ItemPK", typeof(string));
                dtTable.Columns.Add("ItemIdentiflier", typeof(string));
                dtTable.Columns.Add("MOPNumber", typeof(string));
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("BUSSU", typeof(string));
                dtTable.Columns.Add("MOPType", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }


            cn.Close();

            return dtTable;
        }

        public static DataTable MRPMonthYearTable()
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
                dtTable.Columns.Add("MRPMonth", typeof(string));
                dtTable.Columns.Add("MRPYear", typeof(string));
                //dtTable.Columns.Add("EntityCode", typeof(string));
            }

            string qry = "SELECT [PK], [MRPMonth], [MRPYear] FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE PK IN(SELECT MAX(PK) FROM [hijo_portal].[dbo].[tbl_MRP_List] GROUP BY MRPMonth, MRPYear) AND StatusKey = '4' ORDER BY MRPMonth, MRPYear ASC";

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
                    dtRow["MRPMonth"] = Convertion.INDEX_TO_MONTH(Convert.ToInt32(row["MRPMonth"].ToString()));
                    dtRow["MRPYear"] = row["MRPYear"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable DocumentNumber_By_Month_Year(int month, string year)
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("DocumentNumber", typeof(string));
            }

            string month_string = "'" + month.ToString() + "'";
            string year_string = "'" + year + "'";
            if (month == 0)
            {
                month_string = "MRPMonth";
                year_string = "MRPYear";
            }

            string qry = "SELECT [PK], [DocNumber] FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE MRPMonth = " + month_string + " AND MRPYear = " + year_string + " AND StatusKey = '4' ORDER BY MRPMonth, MRPYear ASC";

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
                    dtRow["DocumentNumber"] = row["DocNumber"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

    }
}