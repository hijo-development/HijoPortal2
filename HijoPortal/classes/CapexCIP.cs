using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HijoPortal.classes
{
    public class CapexCIP
    {
        public static DataTable Entity(int month, string year)
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT DISTINCT dbo.vw_AXEntityTable.NAME, dbo.tbl_MRP_List.EntityCode FROM dbo.tbl_MRP_List_CAPEX INNER JOIN dbo.tbl_MRP_List ON dbo.tbl_MRP_List_CAPEX.HeaderDocNum = dbo.tbl_MRP_List.DocNumber INNER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID WHERE(dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.MRPYear = '" + year + "')";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["EntityCode"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable BusinessUnit(string entity)
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT DISTINCT dbo.vw_AXEntityTable.NAME, dbo.tbl_MRP_List.BUCode FROM dbo.tbl_MRP_List_CAPEX INNER JOIN dbo.tbl_MRP_List ON dbo.tbl_MRP_List_CAPEX.HeaderDocNum = dbo.tbl_MRP_List.DocNumber INNER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID WHERE(dbo.tbl_MRP_List.EntityCode = '" + entity + "')";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["BUCode"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}