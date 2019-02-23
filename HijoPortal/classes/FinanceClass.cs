﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HijoPortal.classes;

namespace HijoPortal.classes
{
    public class FinanceClass
    {

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
                dtTable.Columns.Add("UserKey", typeof(string));
                dtTable.Columns.Add("UserCompleteName", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_Approval.PK, dbo.tbl_System_Approval.Ctrl, " +
                         " dbo.tbl_System_Approval.EffectDate, dbo.tbl_System_Approval.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname, dbo.tbl_System_Approval.LastModified " +
                         " FROM dbo.tbl_System_Approval LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_Approval.UserKey = dbo.tbl_Users.PK";
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
                    dtRow["UserKey"] = row["UserKey"].ToString();
                    dtRow["UserCompleteName"] = EncryptionClass.Decrypt(row["Lastname"].ToString()) + ",  " + EncryptionClass.Decrypt(row["Firstname"].ToString());
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable FinanceHeadTable()
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
                dtTable.Columns.Add("UserKey", typeof(string));
                dtTable.Columns.Add("UserCompleteName", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_FinanceHead.PK, dbo.tbl_System_FinanceHead.Ctrl, " +
                         " dbo.tbl_System_FinanceHead.EffectDate, dbo.tbl_System_FinanceHead.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname, dbo.tbl_System_FinanceHead.LastModified " +
                         " FROM dbo.tbl_System_FinanceHead LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_FinanceHead.UserKey = dbo.tbl_Users.PK";
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
                    dtRow["UserKey"] = row["UserKey"].ToString();
                    dtRow["UserCompleteName"] = EncryptionClass.Decrypt(row["Lastname"].ToString()) + ",  " + EncryptionClass.Decrypt(row["Firstname"].ToString());
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable FinanceBudgetTable()
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
                dtTable.Columns.Add("UserKey", typeof(string));
                dtTable.Columns.Add("UserCompleteName", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_FinanceBudget.PK, dbo.tbl_System_FinanceBudget.Ctrl, " +
                         " dbo.tbl_System_FinanceBudget.EffectDate, dbo.tbl_System_FinanceBudget.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname, dbo.tbl_System_FinanceBudget.LastModified " +
                         " FROM dbo.tbl_System_FinanceBudget LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_FinanceBudget.UserKey = dbo.tbl_Users.PK";
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
                    dtRow["UserKey"] = row["UserKey"].ToString();
                    dtRow["UserCompleteName"] = EncryptionClass.Decrypt(row["Lastname"].ToString()) + ",  " + EncryptionClass.Decrypt(row["Firstname"].ToString());
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable FinanceBudget_DetailsTable(int MasterKey)
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
                dtTable.Columns.Add("EntityCode", typeof(string));
                dtTable.Columns.Add("EntityCodeDesc", typeof(string));
                dtTable.Columns.Add("BUSSUCode", typeof(string));
                dtTable.Columns.Add("BUSSUCodeDesc", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_FinanceBudget_Details.PK, " +
                         " dbo.tbl_System_FinanceBudget_Details.EntityCode, " +
                         " dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                         " dbo.tbl_System_FinanceBudget_Details.BUSSUCode, " +
                         " dbo.vw_AXOperatingUnitTable.NAME AS BUSSUCodeDesc, " +
                         " dbo.tbl_System_FinanceBudget_Details.MasterKey " +
                         " FROM dbo.tbl_System_FinanceBudget_Details LEFT OUTER JOIN " +
                         " dbo.vw_AXOperatingUnitTable ON dbo.tbl_System_FinanceBudget_Details.BUSSUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                         " dbo.vw_AXEntityTable ON dbo.tbl_System_FinanceBudget_Details.EntityCode = dbo.vw_AXEntityTable.ID " +
                         " WHERE(dbo.tbl_System_FinanceBudget_Details.MasterKey = "+ MasterKey + ")";
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
                    dtRow["EntityCode"] = row["EntityCode"].ToString();
                    dtRow["EntityCodeDesc"] = row["EntityCodeDesc"].ToString();
                    dtRow["BUSSUCode"] = row["BUSSUCode"].ToString();
                    dtRow["BUSSUCodeDesc"] = row["BUSSUCodeDesc"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}