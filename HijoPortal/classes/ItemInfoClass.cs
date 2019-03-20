using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class ItemInfoClass
    {
        public static DataTable Item_Invent_OnHand(string EntCode, string ItemCode)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("LocationID", typeof(string));
                dtTable.Columns.Add("InventLocationID", typeof(string));
                dtTable.Columns.Add("Quantity", typeof(string));
            }

            string query = "SELECT WMSLOCATIONID, INVENTLOCATIONID, Qty " +
                           " FROM dbo.vw_AXInventLevel " +
                           " WHERE(DATAAREAID = '"+ EntCode + "') " +
                           " AND(ITEMID = '"+ ItemCode + "')";
            cmd = new SqlCommand(query);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["LocationID"] = row["WMSLOCATIONID"].ToString();
                    dtRow["InventLocationID"] = row["INVENTLOCATIONID"].ToString();
                    dtRow["Quantity"] = Convert.ToDouble(row["Qty"]).ToString("#,##0.00");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }
    }
}