using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
            }

            string query = "SELECT WAYBILLNO FROM dbo.vw_AXHLSSOA WHERE(CUSTACCOUNT = '"+ CustNum + "') AND(WEEK_NO = "+ iWeekNum + ") AND(Yr = "+ iYear + ") GROUP BY WAYBILLNO ORDER BY WAYBILLNO";

            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            //MRPClass.PrintString(dt.Rows.Count.ToString());
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["Waybill"] = row["WAYBILLNO"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}