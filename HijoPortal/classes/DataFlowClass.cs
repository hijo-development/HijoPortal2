using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class DataFlowClass
    {
        public static DataTable DataFlowTable()
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
                dtTable.Columns.Add("Ctrl", typeof(string));
                dtTable.Columns.Add("EffectDate", typeof(string));
                dtTable.Columns.Add("Remarks", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT tbl_System_MOP_DataFlow.* " +
                         " FROM tbl_System_MOP_DataFlow";
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
                    dtRow["Ctrl"] = row["Ctrl"].ToString();
                    dtRow["EffectDate"] = Convert.ToDateTime(row["EffectDate"]).ToString("MM/dd/yyyy");
                    dtRow["Remarks"] = row["Remarks"].ToString();
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable DataFlowDetailsTable(int MasterKey)
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
                dtTable.Columns.Add("Line", typeof(string));
                dtTable.Columns.Add("PositionNameKey", typeof(string));
                dtTable.Columns.Add("PositionName", typeof(string));
            }

            string qry = "SELECT TOP (100) PERCENT dbo.tbl_System_MOP_DataFlow_Details.PK, " +
                         " dbo.tbl_System_MOP_DataFlow_Details.Line, " +
                         " dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey, " +
                         " dbo.tbl_System_Approval_Position.PositionName " +
                         " FROM dbo.tbl_System_MOP_DataFlow_Details LEFT OUTER JOIN " +
                         " dbo.tbl_System_Approval_Position ON dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                         " WHERE(dbo.tbl_System_MOP_DataFlow_Details.MasterKey = "+ MasterKey + ") " +
                         " ORDER BY dbo.tbl_System_MOP_DataFlow_Details.Line";
            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

            MRPClass.PrintString(dt.Rows.Count.ToString());

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["Line"] = row["Line"].ToString();
                    dtRow["PositionNameKey"] = row["PositionNameKey"].ToString(); 
                    dtRow["PositionName"] = row["PositionName"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable ApprovalTable()
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
                dtTable.Columns.Add("Ctrl", typeof(string));
                dtTable.Columns.Add("EffectDate", typeof(string));
                dtTable.Columns.Add("Remarks", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT tbl_System_Approval.* " +
                         " FROM tbl_System_Approval";
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
                    dtRow["Ctrl"] = row["Ctrl"].ToString();
                    dtRow["EffectDate"] = Convert.ToDateTime(row["EffectDate"]).ToString("MM/dd/yyyy");
                    dtRow["Remarks"] = row["Remarks"].ToString();
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable ApprovalDetailsTable(int MasterKey)
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
                dtTable.Columns.Add("Line", typeof(string));
                dtTable.Columns.Add("PositionNameKey", typeof(string));
                dtTable.Columns.Add("PositionName", typeof(string));
            }

            //MRPClass.PrintString(MasterKey.ToString());

            string qry = "SELECT TOP (100) PERCENT dbo.tbl_System_Approval_Details.PK, " +
                         " dbo.tbl_System_Approval_Details.Line, " +
                         " dbo.tbl_System_Approval_Details.PositionNameKey, " +
                         " dbo.tbl_System_Approval_Position.PositionName " +
                         " FROM dbo.tbl_System_Approval_Details LEFT OUTER JOIN " +
                         " dbo.tbl_System_Approval_Position ON dbo.tbl_System_Approval_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                         " WHERE(dbo.tbl_System_Approval_Details.MasterKey = " + MasterKey + ") " +
                         " ORDER BY dbo.tbl_System_Approval_Details.Line";
            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

            //MRPClass.PrintString(dt.Rows.Count.ToString());

            if (dt.Rows.Count > 0)
            {
                
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["Line"] = row["Line"].ToString();
                    dtRow["PositionNameKey"] = row["PositionNameKey"].ToString();
                    dtRow["PositionName"] = row["PositionName"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}