using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HijoPortal.classes;

namespace HijoPortal.classes
{
    public class WorkflowClass
    {
        public static DataTable WorkflowMasterTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("Ctrl", typeof(string));
                dtTable.Columns.Add("EffectDate", typeof(string));
                dtTable.Columns.Add("EntCode", typeof(string));
                dtTable.Columns.Add("EntCodeDesc", typeof(string));
                dtTable.Columns.Add("BUCode", typeof(string));
                dtTable.Columns.Add("BUCodeDesc", typeof(string));
                dtTable.Columns.Add("BUHead", typeof(string));
                dtTable.Columns.Add("BUHeadName", typeof(string));
                dtTable.Columns.Add("DateCreated", typeof(string));
            }

            cn.Open();
            string qry = "SELECT dbo.tbl_System_Workflow.PK, dbo.tbl_System_Workflow.Ctrl, dbo.tbl_System_Workflow.EffectDate, " +
                         " dbo.tbl_System_Workflow.EntCode, dbo.vw_AXEntityTable.NAME AS EntCodeDesc, dbo.tbl_System_Workflow.BUCode, " +
                         " dbo.vw_AXOperatingUnitTable.NAME AS BUCodeDesc, dbo.tbl_System_Workflow.BUHead, dbo.tbl_Users.Lastname, " +
                         " dbo.tbl_Users.Firstname, dbo.tbl_System_Workflow.LastModified, dbo.tbl_System_Workflow.DateCreated " +
                         " FROM  dbo.tbl_System_Workflow LEFT OUTER JOIN " +
                         " dbo.vw_AXOperatingUnitTable ON dbo.tbl_System_Workflow.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                         " dbo.vw_AXEntityTable ON dbo.tbl_System_Workflow.EntCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_Workflow.BUHead = dbo.tbl_Users.PK";
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
                    dtRow["EntCode"] = row["EntCode"].ToString();
                    dtRow["EntCodeDesc"] = row["EntCodeDesc"].ToString();
                    dtRow["BUCode"] = row["BUCode"].ToString();
                    dtRow["BUCodeDesc"] = row["BUCodeDesc"].ToString();
                    dtRow["BUHead"] = row["BUHead"].ToString();
                    dtRow["BUHeadName"] = EncryptionClass.Decrypt(row["Lastname"].ToString()) + ",  " + EncryptionClass.Decrypt(row["Firstname"].ToString());
                    dtRow["DateCreated"] = Convert.ToDateTime(row["DateCreated"]).ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}