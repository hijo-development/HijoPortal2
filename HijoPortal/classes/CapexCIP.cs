using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HijoPortal.classes
{
    public class CapexCIP
    {
        public static DataTable CAPEXCIP_Table(string month, string year, string entity, string bu)
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
                dtTable.Columns.Add("CIPSIPNumber", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("CompanyName", typeof(string));
                dtTable.Columns.Add("BUName", typeof(string));
                dtTable.Columns.Add("RevDesc", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(Double));
                dtTable.Columns.Add("ApprovedCost", typeof(string));
                dtTable.Columns.Add("ApprovedTotalCost", typeof(string));
                dtTable.Columns.Add("ApprovedQty", typeof(Double));
                dtTable.Columns.Add("EntCode", typeof(string));
                dtTable.Columns.Add("ProcCat", typeof(string));
            }
            string query_all = "SELECT dbo.vw_AXEntityTable.ID AS EntCode, dbo.vw_AXEntityTable.NAME AS CompanyName, ISNULL(dbo.vw_AXOperatingUnitTable.NAME, '') AS BUName, ISNULL(dbo.vw_AXFindimBananaRevenue.DESCRIPTION, '') AS RevDesc, dbo.tbl_MRP_List_CAPEX.* FROM dbo.tbl_MRP_List_CAPEX LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_CAPEX.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE LEFT OUTER JOIN dbo.tbl_MRP_List ON dbo.tbl_MRP_List_CAPEX.HeaderDocNum = dbo.tbl_MRP_List.DocNumber LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID WHERE (dbo.tbl_MRP_List.StatusKey = 4) AND (dbo.tbl_MRP_List_CAPEX.ProdCat <> 'CIP')";

            string businessUnit = "";
            if (string.IsNullOrEmpty(bu) && entity == "0101")
                businessUnit = "dbo.tbl_MRP_List.BUCode";
            else
                businessUnit = "'" + bu + "'";

            string query_sort = "SELECT dbo.vw_AXEntityTable.ID AS EntCode, dbo.vw_AXEntityTable.NAME AS CompanyName, ISNULL(dbo.vw_AXOperatingUnitTable.NAME, '') AS BUName, ISNULL(dbo.vw_AXFindimBananaRevenue.DESCRIPTION, '') AS RevDesc, dbo.tbl_MRP_List_CAPEX.* FROM dbo.tbl_MRP_List_CAPEX LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_CAPEX.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE LEFT OUTER JOIN dbo.tbl_MRP_List ON dbo.tbl_MRP_List_CAPEX.HeaderDocNum = dbo.tbl_MRP_List.DocNumber LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID WHERE (dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND (dbo.tbl_MRP_List.MRPYear = '" + year + "') AND (dbo.tbl_MRP_List.StatusKey = 4) AND (dbo.tbl_MRP_List.EntityCode = '" + entity + "') AND (dbo.tbl_MRP_List.BUCode = '" + businessUnit + "') AND (dbo.tbl_MRP_List_CAPEX.ProdCat <> 'CIP')";

            if (string.IsNullOrEmpty(month) && string.IsNullOrEmpty(year))
                cmd = new SqlCommand(query_all);
            else
                cmd = new SqlCommand(query_sort);

            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["CIPSIPNumber"] = row["CIPSIPNumber"].ToString();
                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                    dtRow["CompanyName"] = row["CompanyName"].ToString();
                    dtRow["BUName"] = row["BUName"].ToString();
                    dtRow["RevDesc"] = row["RevDesc"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();

                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");

                    dtRow["ApprovedCost"] = Convert.ToDouble(row["ApprovedCost"].ToString()).ToString("N");
                    dtRow["ApprovedTotalCost"] = Convert.ToDouble(row["ApprovedTotalCost"].ToString()).ToString("N");
                    dtRow["ApprovedQty"] = Convert.ToDouble(row["ApprovedQty"].ToString()).ToString("N");

                    dtRow["EntCode"] = row["EntCode"].ToString();
                    dtRow["ProcCat"] = row["ProdCat"].ToString();
                    

                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
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

            //string qry = "SELECT [PK], [MRPMonth], [MRPYear], [EntityCode] FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE PK IN(SELECT MAX(PK) FROM [hijo_portal].[dbo].[tbl_MRP_List] GROUP BY MRPMonth, MRPYear) ORDER BY MRPMonth, MRPYear ASC";

            string qry = "SELECT DISTINCT PK, MRPMonth, MRPYear, EntityCode FROM dbo.tbl_MRP_List WHERE(StatusKey = '4') AND (PK IN(SELECT MAX(PK) AS Expr1 FROM dbo.tbl_MRP_List AS tbl_MRP_List_1 GROUP BY MRPMonth, MRPYear)) ORDER BY MRPMonth, MRPYear";

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
                    //dtRow["EntityCode"] = row["EntityCode"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

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