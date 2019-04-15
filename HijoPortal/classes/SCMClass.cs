using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HijoPortal.classes;

namespace HijoPortal.classes
{
    public class SCMClass
    {
        public static DataTable SCMHeadTable()
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
                dtTable.Columns.Add("StatusKey", typeof(string));
                dtTable.Columns.Add("StatusDesc", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_SCMHead.PK, dbo.tbl_System_SCMHead.Ctrl, " +
                         " dbo.tbl_System_SCMHead.EffectDate, dbo.tbl_System_SCMHead.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname, dbo.tbl_System_SCMHead.LastModified, " +
                         " dbo.tbl_System_SCMHead.StatusKey " +
                         " FROM dbo.tbl_System_SCMHead LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_SCMHead.UserKey = dbo.tbl_Users.PK";
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
                    dtRow["StatusKey"] = row["StatusKey"].ToString();
                    if (Convert.ToInt32(row["StatusKey"]) == 1)
                    {
                        dtRow["StatusDesc"] = "Active";
                    }
                    else
                    {
                        dtRow["StatusDesc"] = "Inactive";
                    }
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable InventoryAnalTable()
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
                dtTable.Columns.Add("StatusKey", typeof(string));
                dtTable.Columns.Add("StatusDesc", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_SCMInventoryAnalyst.PK, dbo.tbl_System_SCMInventoryAnalyst.Ctrl, " +
                         " dbo.tbl_System_SCMInventoryAnalyst.EffectDate, dbo.tbl_System_SCMInventoryAnalyst.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname, dbo.tbl_System_SCMInventoryAnalyst.LastModified, " +
                         " dbo.tbl_System_SCMInventoryAnalyst.StatusKey " +
                         " FROM dbo.tbl_System_SCMInventoryAnalyst LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_SCMInventoryAnalyst.UserKey = dbo.tbl_Users.PK";
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
                    dtRow["StatusKey"] = row["StatusKey"].ToString();
                    if (Convert.ToInt32(row["StatusKey"]) == 1)
                    {
                        dtRow["StatusDesc"] = "Active";
                    }
                    else
                    {
                        dtRow["StatusDesc"] = "Inactive";
                    }
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable ProcurementOffTable()
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
                dtTable.Columns.Add("StatusKey", typeof(string));
                dtTable.Columns.Add("StatusDesc", typeof(string));
                dtTable.Columns.Add("LastModified", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_SCMProcurementOfficer.PK, dbo.tbl_System_SCMProcurementOfficer.Ctrl, " +
                         " dbo.tbl_System_SCMProcurementOfficer.EffectDate, dbo.tbl_System_SCMProcurementOfficer.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname, dbo.tbl_System_SCMProcurementOfficer.LastModified, " +
                         " dbo.tbl_System_SCMProcurementOfficer.StatusKey " +
                         " FROM dbo.tbl_System_SCMProcurementOfficer LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_SCMProcurementOfficer.UserKey = dbo.tbl_Users.PK";
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
                    dtRow["StatusKey"] = row["StatusKey"].ToString();
                    if (Convert.ToInt32(row["StatusKey"]) == 1)
                    {
                        dtRow["StatusDesc"] = "Active";
                    }
                    else
                    {
                        dtRow["StatusDesc"] = "Inactive";
                    }
                    dtRow["LastModified"] = row["LastModified"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable ProcurementOff_DetailsTable(int MasterKey)
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
                dtTable.Columns.Add("MasterKey", typeof(string));
                dtTable.Columns.Add("ProcCat", typeof(string));
                dtTable.Columns.Add("ProcCatDesc", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_SCMProcurementOfficer_Details.MasterKey, " +
                         " dbo.tbl_System_SCMProcurementOfficer_Details.ProcCat, " +
                         " dbo.vw_AXProdCategory_Group.DESCRIPTION AS ProcCatDesc, " +
                         " dbo.tbl_System_SCMProcurementOfficer_Details.PK " +
                         " FROM dbo.tbl_System_SCMProcurementOfficer_Details LEFT OUTER JOIN " +
                         " dbo.vw_AXProdCategory_Group ON dbo.tbl_System_SCMProcurementOfficer_Details.ProcCat = dbo.vw_AXProdCategory_Group.NAME " +
                         " WHERE(dbo.tbl_System_SCMProcurementOfficer_Details.MasterKey = "+ MasterKey + ")";
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
                    dtRow["MasterKey"] = row["MasterKey"].ToString();
                    dtRow["ProcCat"] = row["ProcCat"].ToString();
                    dtRow["ProcCatDesc"] = row["ProcCatDesc"].ToString(); 
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}