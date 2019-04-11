using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class Preview
    {
        public static DataTable DirectMaterials(string docnumber)
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
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("POQty", typeof(string));
                dtTable.Columns.Add("RemainingQty", typeof(string));
            }

            string qry = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.ItemCode, dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.ItemDescriptionAddl, dbo.tbl_MRP_List_DirectMaterials.Qty, dbo.tbl_MRP_List_DirectMaterials.QtyPO, dbo.tbl_MRP_List_DirectMaterials.AvailForPO, dbo.tbl_POCreation_Details.ItemPK, dbo.tbl_POCreation_Details.PK FROM   dbo.tbl_MRP_List_DirectMaterials INNER JOIN dbo.tbl_POCreation_Details ON dbo.tbl_MRP_List_DirectMaterials.PK = dbo.tbl_POCreation_Details.ItemPK WHERE(dbo.tbl_POCreation_Details.MOPNumber = '" + docnumber + "') AND (dbo.tbl_POCreation_Details.Identifier = '1')";

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
                    dtRow["ItemCode"] = row["ItemCode"].ToString();

                    string desc = row["ItemDescriptionAddl"].ToString();
                    if (string.IsNullOrEmpty(desc))
                        dtRow["Descripiton"] = row["ItemDescription"].ToString();
                    else
                        dtRow["Descripiton"] = row["ItemDescription"].ToString() + "(" + desc + ")";

                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["POQty"] = Convert.ToDouble(row["QtyPO"].ToString()).ToString("N");
                    dtRow["RemainingQty"] = Convert.ToDouble(row["AvailForPO"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable OperatingExpense(string docnumber)
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
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("POQty", typeof(string));
                dtTable.Columns.Add("RemainingQty", typeof(string));
            }

            string qry = "SELECT DISTINCT dbo.tbl_POCreation_Details.PK, dbo.tbl_MRP_List_OPEX.ItemCode, dbo.tbl_MRP_List_OPEX.Description, dbo.tbl_MRP_List_OPEX.DescriptionAddl, dbo.tbl_MRP_List_OPEX.Qty, dbo.tbl_MRP_List_OPEX.QtyPO, dbo.tbl_MRP_List_OPEX.AvailForPO FROM dbo.tbl_POCreation_Details INNER JOIN dbo.tbl_MRP_List_OPEX ON dbo.tbl_POCreation_Details.ItemPK = dbo.tbl_MRP_List_OPEX.PK WHERE(dbo.tbl_POCreation_Details.MOPNumber = '" + docnumber + "') AND (dbo.tbl_POCreation_Details.Identifier = '2')";

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
                    dtRow["ItemCode"] = row["ItemCode"].ToString();

                    string desc = row["DescriptionAddl"].ToString();
                    if (string.IsNullOrEmpty(desc))
                        dtRow["Descripiton"] = row["Description"].ToString();
                    else
                        dtRow["Descripiton"] = row["Description"].ToString() + "(" + desc + ")";

                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["POQty"] = Convert.ToDouble(row["QtyPO"].ToString()).ToString("N");
                    dtRow["RemainingQty"] = Convert.ToDouble(row["AvailForPO"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable CapitalExpenditure(string docnumber)
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
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("POQty", typeof(string));
                dtTable.Columns.Add("RemainingQty", typeof(string));
            }

            string qry = "SELECT dbo.tbl_POCreation_Details.PK, dbo.tbl_MRP_List_CAPEX.Description, dbo.tbl_MRP_List_CAPEX.Qty, dbo.tbl_MRP_List_CAPEX.QtyPO, dbo.tbl_MRP_List_CAPEX.AvailForPO FROM dbo.tbl_POCreation_Details INNER JOIN dbo.tbl_MRP_List_CAPEX ON dbo.tbl_POCreation_Details.ItemPK = dbo.tbl_MRP_List_CAPEX.PK WHERE(dbo.tbl_POCreation_Details.MOPNumber = '" + docnumber + "') AND (dbo.tbl_POCreation_Details.Identifier = '4')";

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
                    dtRow["ItemCode"] = "";
                    dtRow["Descripiton"] = row["Description"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["POQty"] = Convert.ToDouble(row["QtyPO"].ToString()).ToString("N");
                    dtRow["RemainingQty"] = Convert.ToDouble(row["AvailForPO"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}
