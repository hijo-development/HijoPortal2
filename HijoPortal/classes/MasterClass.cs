using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class MasterClass
    {
        public static DataTable SideNavBar()
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
                dtTable.Columns.Add("NAME", typeof(string));
                dtTable.Columns.Add("Na", typeof(string));
            }

            string qry = "SELECT [ID] ,[NAME] FROM [dbo].[vw_AXEntityTable]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["ID"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtRow["Na"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}