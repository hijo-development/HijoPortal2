using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Web;

namespace HijoPortal.classes
{
    public class MOPReportClass
    {

        

        public static DataTable MOPMaster(string DocNum)
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
                dtTable.Columns.Add("DocNumber", typeof(string));
                dtTable.Columns.Add("MonthYear", typeof(string));
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("BU", typeof(string));
            }
            cn.Open();
            string query = "SELECT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, dbo.tbl_MRP_List.DateCreated, dbo.tbl_MRP_List.EntityCode, dbo.vw_AXEntityTable.NAME AS EntityName, dbo.tbl_MRP_List.BUCode, ISNULL(dbo.vw_AXOperatingUnitTable.NAME, '') AS BUName, dbo.tbl_MRP_List.MRPMonth, dbo.tbl_MRP_List.MRPYear FROM dbo.tbl_MRP_List LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID WHERE(dbo.tbl_MRP_List.DocNumber = '" + DocNum + "')";
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
                    dtRow["DocNumber"] = row["DocNumber"].ToString();
                    dtRow["MonthYear"] = Convertion.INDEX_TO_MONTH(Convert.ToInt32(row["MRPMonth"].ToString())) + " " + row["MRPYear"].ToString(); ;
                    dtRow["Entity"] = row["EntityName"].ToString();
                    dtRow["BU"] = row["BUName"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}