using System;
using System.Collections;
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


        //Classes for mrp_preview page
        public static DataTable Preview_OP(string DOC_NUMBER, string entity)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            //opex_total_amount = 0;
            //op_edited_total = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ExpenseCodeName", typeof(string));
                dtTable.Columns.Add("ExpenseCode", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("ACost", typeof(string));
                dtTable.Columns.Add("AQty", typeof(string));
                dtTable.Columns.Add("ATotalCost", typeof(string));
                dtTable.Columns.Add("VALUE", typeof(string));
                dtTable.Columns.Add("RevDesc", typeof(string));
            }


            string query_arraycode = "SELECT DISTINCT ExpenseCode FROM [hijo_portal].[dbo].[tbl_MRP_List_OPEX] WHERE HeaderDocNum = '" + DOC_NUMBER + "' ORDER BY ExpenseCode ASC";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            cmd = new SqlCommand(query_arraycode, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                list.Add(reader[0].ToString());
            }
            reader.Close();

            string query_farmunit = "SELECT DISTINCT dbo.vw_AXFindimBananaRevenue.VALUE FROM dbo.tbl_MRP_List_OPEX INNER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_OPEX.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE HeaderDocNum = '" + DOC_NUMBER + "'";

            cmd = new SqlCommand(query_farmunit, conn);
            reader = cmd.ExecuteReader();
            ArrayList farm_list = new ArrayList();
            while (reader.Read())
            {
                farm_list.Add(reader[0].ToString());
            }
            reader.Close();

            for (int c = 0; c < farm_list.Count; c++)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //string query_1 = "SELECT tbl_MRP_List_OPEX.*, vw_AXExpenseAccount.NAME, vw_AXExpenseAccount.isItem, vw_AXFindimBananaRevenue.VALUE, vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc FROM tbl_MRP_List_OPEX INNER JOIN vw_AXExpenseAccount ON tbl_MRP_List_OPEX.ExpenseCode = vw_AXExpenseAccount.MAINACCOUNTID INNER JOIN vw_AXFindimBananaRevenue ON tbl_MRP_List_OPEX.OprUnit = vw_AXFindimBananaRevenue.VALUE INNER JOIN tbl_MRP_List ON tbl_MRP_List_OPEX.HeaderDocNum = tbl_MRP_List.DocNumber WHERE [HeaderDocNum] = '" + DOC_NUMBER + "' AND tbl_MRP_List_OPEX.ExpenseCode = '" + list[i] + "' ";

                    string query_1 = "SELECT dbo.tbl_MRP_List_OPEX.PK, dbo.tbl_MRP_List_OPEX.HeaderDocNum, dbo.tbl_MRP_List_OPEX.TableIdentifier, dbo.tbl_MRP_List_OPEX.ExpenseCode, dbo.tbl_MRP_List_OPEX.OprUnit, dbo.tbl_MRP_List_OPEX.ProcCat, dbo.tbl_MRP_List_OPEX.ItemCode, dbo.tbl_MRP_List_OPEX.Description, dbo.tbl_MRP_List_OPEX.DescriptionAddl, dbo.tbl_MRP_List_OPEX.UOM, dbo.tbl_MRP_List_OPEX.Cost, dbo.tbl_MRP_List_OPEX.Qty, dbo.tbl_MRP_List_OPEX.TotalCost, dbo.tbl_MRP_List_OPEX.EdittedQty, dbo.tbl_MRP_List_OPEX.EdittedCost, dbo.tbl_MRP_List_OPEX.EdittedTotalCost, dbo.tbl_MRP_List_OPEX.ApprovedQty, dbo.tbl_MRP_List_OPEX.ApprovedCost, dbo.tbl_MRP_List_OPEX.ApprovedTotalCost, dbo.tbl_MRP_List_OPEX.QtyPO, dbo.tbl_MRP_List_OPEX.AvailForPO, dbo.vw_AXExpenseAccount.NAME, dbo.vw_AXExpenseAccount.isItem, dbo.vw_AXFindimBananaRevenue.VALUE, dbo.vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc FROM dbo.tbl_MRP_List_OPEX INNER JOIN dbo.vw_AXExpenseAccount ON dbo.tbl_MRP_List_OPEX.ExpenseCode = dbo.vw_AXExpenseAccount.MAINACCOUNTID INNER JOIN dbo.tbl_MRP_List ON dbo.tbl_MRP_List_OPEX.HeaderDocNum = dbo.tbl_MRP_List.DocNumber LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_OPEX.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "' AND tbl_MRP_List_OPEX.ExpenseCode = '" + list[i] + "' AND (dbo.tbl_MRP_List_OPEX.OprUnit = '" + farm_list[c] + "') ";

                    //for NON TRAIN  TODO: Explain this
                    string query_2 = "SELECT tbl_MRP_List_OPEX.*, vw_AXExpenseAccount.NAME, vw_AXExpenseAccount.isItem FROM   tbl_MRP_List_OPEX INNER JOIN vw_AXExpenseAccount ON tbl_MRP_List_OPEX.ExpenseCode = vw_AXExpenseAccount.MAINACCOUNTID WHERE [HeaderDocNum] = '" + DOC_NUMBER + "' AND tbl_MRP_List_OPEX.ExpenseCode = '" + list[i] + "' ";

                    if (entity == Constants.TRAIN_CODE())
                        cmd = new SqlCommand(query_1);
                    else
                        cmd = new SqlCommand(query_2);

                    cmd.Connection = cn;
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row == dt.Rows[0])
                            {
                                DataRow dtRow = dtTable.NewRow();
                                if (entity == Constants.TRAIN_CODE())
                                {
                                    if (i == 0)
                                    {
                                        dtRow["VALUE"] = row["VALUE"].ToString();
                                        dtRow["RevDesc"] = row["RevDesc"].ToString();
                                        dtTable.Rows.Add(dtRow);
                                    }
                                }

                                dtRow = dtTable.NewRow();
                                dtRow["ExpenseCodeName"] = row["NAME"].ToString();
                                dtTable.Rows.Add(dtRow);

                                dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ExpenseCodeName"] = "";

                                string desc = row["DescriptionAddl"].ToString();
                                if (!string.IsNullOrEmpty(desc))
                                    dtRow["Description"] = row["Description"].ToString() + " (" + desc + ")";
                                else
                                    dtRow["Description"] = row["Description"].ToString();

                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittedTotalCost"]).ToString("N");

                                if (entity == Constants.TRAIN_CODE())
                                {
                                    dtRow["VALUE"] = "";
                                    dtRow["RevDesc"] = "";
                                }
                                else
                                {
                                    dtRow["VALUE"] = "";
                                    dtRow["RevDesc"] = "";
                                }
                                dtTable.Rows.Add(dtRow);

                            }
                            else
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ExpenseCodeName"] = "";
                                dtRow["Description"] = row["Description"].ToString();
                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittedTotalCost"]).ToString("N");

                                //opex_total_amount += Convert.ToDouble(row["TotalCost"]);
                                //op_edited_total += Convert.ToDouble(row["EdittedTotalCost"]);

                                if (entity == Constants.TRAIN_CODE())
                                {
                                    dtRow["VALUE"] = "";
                                    dtRow["RevDesc"] = "";
                                }
                                else
                                {
                                    dtRow["VALUE"] = "";
                                    dtRow["RevDesc"] = "";
                                }
                                dtTable.Rows.Add(dtRow);
                            }
                        }
                    }
                    dt.Clear();
                }
            }
            cn.Close();
            return dtTable;
        }

        public static DataTable Preview_DM(string DOC_NUMBER, string entity)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            //materials_total_amount = 0;
            //mat_edited_total = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("ItemDescription", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("ACost", typeof(string));
                dtTable.Columns.Add("AQty", typeof(string));
                dtTable.Columns.Add("ATotalCost", typeof(string));
                dtTable.Columns.Add("VALUE", typeof(string));
                dtTable.Columns.Add("RevDesc", typeof(string));
            }


            string query_arraycode = "SELECT DISTINCT ActivityCode FROM [hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials] WHERE HeaderDocNum = '" + DOC_NUMBER + "' ORDER BY ActivityCode ASC";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            cmd = new SqlCommand(query_arraycode, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                list.Add(reader[0].ToString());
            }
            reader.Close();

            string query_farmunit = "SELECT DISTINCT dbo.vw_AXFindimBananaRevenue.VALUE FROM dbo.tbl_MRP_List_DirectMaterials INNER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_DirectMaterials.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE HeaderDocNum = '" + DOC_NUMBER + "'";

            cmd = new SqlCommand(query_farmunit, conn);
            reader = cmd.ExecuteReader();
            ArrayList farm_list = new ArrayList();
            while (reader.Read())
            {
                farm_list.Add(reader[0].ToString());
            }
            reader.Close();
            bool TRAIN = entity == Constants.TRAIN_CODE();
            if (TRAIN)
            {
                for (int c = 0; c < farm_list.Count; c++)
                {
                    for (int i = 0; i < list.Count; i++)
                    {

                        string query_1 = "SELECT DISTINCT tbl_MRP_List_DirectMaterials.*, vw_AXFindimActivity.DESCRIPTION, vw_AXFindimBananaRevenue.VALUE, vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc, tbl_MRP_List.EntityCode FROM   tbl_MRP_List_DirectMaterials INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_DirectMaterials.ActivityCode = vw_AXFindimActivity.VALUE INNER JOIN tbl_MRP_List ON tbl_MRP_List_DirectMaterials.HeaderDocNum = tbl_MRP_List.DocNumber INNER JOIN vw_AXFindimBananaRevenue ON tbl_MRP_List.EntityCode = vw_AXFindimBananaRevenue.Entity AND tbl_MRP_List_DirectMaterials.OprUnit = vw_AXFindimBananaRevenue.VALUE WHERE tbl_MRP_List_DirectMaterials.HeaderDocNum = '" + DOC_NUMBER + "' AND tbl_MRP_List_DirectMaterials.ActivityCode = '" + list[i] + "' AND (dbo.tbl_MRP_List_DirectMaterials.OprUnit = '" + farm_list[c] + "')";


                        string query_2 = "SELECT DISTINCT tbl_MRP_List_DirectMaterials.*, vw_AXFindimActivity.DESCRIPTION FROM   tbl_MRP_List_DirectMaterials INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_DirectMaterials.ActivityCode = vw_AXFindimActivity.VALUE WHERE tbl_MRP_List_DirectMaterials.HeaderDocNum = '" + DOC_NUMBER + "' AND tbl_MRP_List_DirectMaterials.ActivityCode = '" + list[i] + "' ";

                        if (entity == Constants.TRAIN_CODE()) cmd = new SqlCommand(query_1);
                        else cmd = new SqlCommand(query_2);


                        //cmd = new SqlCommand(query);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row == dt.Rows[0])
                                {
                                    DataRow dtRow = dtTable.NewRow();
                                    if (entity == Constants.TRAIN_CODE())
                                    {
                                        if (i == 0)
                                        {
                                            dtRow["VALUE"] = row["VALUE"].ToString();
                                            dtRow["RevDesc"] = row["RevDesc"].ToString();
                                            dtTable.Rows.Add(dtRow);
                                        }
                                    }

                                    dtRow = dtTable.NewRow();
                                    dtRow["ActivityCode"] = row["DESCRIPTION"].ToString();
                                    dtTable.Rows.Add(dtRow);

                                    dtRow = dtTable.NewRow();
                                    dtRow["PK"] = row["PK"].ToString();
                                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                    dtRow["ActivityCode"] = "";
                                    dtRow["ItemCode"] = row["ItemCode"].ToString();

                                    string desc = row["ItemDescriptionAddl"].ToString();
                                    if (!string.IsNullOrEmpty(desc))
                                        dtRow["ItemDescription"] = row["ItemDescription"].ToString() + " (" + desc + ")";
                                    else
                                        dtRow["ItemDescription"] = row["ItemDescription"].ToString();

                                    dtRow["UOM"] = row["UOM"].ToString();
                                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                    dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                    dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                    dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                    if (entity == Constants.TRAIN_CODE())
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    else
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    dtTable.Rows.Add(dtRow);
                                }
                                else
                                {
                                    DataRow dtRow = dtTable.NewRow();
                                    dtRow["PK"] = row["PK"].ToString();
                                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                    dtRow["ActivityCode"] = "";
                                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                                    dtRow["ItemDescription"] = row["ItemDescription"].ToString();
                                    dtRow["UOM"] = row["UOM"].ToString();
                                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                                    dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                    dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                    dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                    //materials_total_amount += Convert.ToDouble(row["TotalCost"]);
                                    //mat_edited_total += Convert.ToDouble(row["EdittiedTotalCost"]);


                                    if (entity == Constants.TRAIN_CODE())
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    else
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    dtTable.Rows.Add(dtRow);
                                }
                            }
                        }
                        dt.Clear();
                    }
                }
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string query_2 = "SELECT DISTINCT tbl_MRP_List_DirectMaterials.*, vw_AXFindimActivity.DESCRIPTION FROM   tbl_MRP_List_DirectMaterials INNER JOIN vw_AXFindimActivity ON tbl_MRP_List_DirectMaterials.ActivityCode = vw_AXFindimActivity.VALUE WHERE tbl_MRP_List_DirectMaterials.HeaderDocNum = '" + DOC_NUMBER + "' AND tbl_MRP_List_DirectMaterials.ActivityCode = '" + list[i] + "' ";

                    cmd = new SqlCommand(query_2);
                    cmd.Connection = cn;
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row == dt.Rows[0])
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["ActivityCode"] = row["DESCRIPTION"].ToString();
                                dtTable.Rows.Add(dtRow);

                                dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ActivityCode"] = "";
                                dtRow["ItemCode"] = row["ItemCode"].ToString();

                                string desc = row["ItemDescriptionAddl"].ToString();
                                if (!string.IsNullOrEmpty(desc))
                                    dtRow["ItemDescription"] = row["ItemDescription"].ToString() + " (" + desc + ")";
                                else
                                    dtRow["ItemDescription"] = row["ItemDescription"].ToString();

                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";
                                dtTable.Rows.Add(dtRow);
                            }
                            else
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ActivityCode"] = "";
                                dtRow["ItemCode"] = row["ItemCode"].ToString();
                                dtRow["ItemDescription"] = row["ItemDescription"].ToString();
                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");
                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";
                                dtTable.Rows.Add(dtRow);
                            }
                        }
                    }
                }
            }
            cn.Close();
            return dtTable;
        }

        public static DataTable Preview_CA(string DOC_NUMBER, string entitycode)
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
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("ProdCode", typeof(string));
                dtTable.Columns.Add("ProdCat", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("VALUE", typeof(string));
                dtTable.Columns.Add("RevDesc", typeof(string));
                dtTable.Columns.Add("ACost", typeof(string));
                dtTable.Columns.Add("AQty", typeof(string));
                dtTable.Columns.Add("ATotalCost", typeof(string));
            }

            string query_farmunit = "SELECT DISTINCT dbo.vw_AXFindimBananaRevenue.VALUE FROM dbo.tbl_MRP_List_DirectMaterials INNER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_DirectMaterials.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE HeaderDocNum = '" + DOC_NUMBER + "'";

            cmd = new SqlCommand(query_farmunit, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList farm_list = new ArrayList();
            while (reader.Read())
            {
                farm_list.Add(reader[0].ToString());
            }
            reader.Close();

            bool TRAIN = entitycode == Constants.TRAIN_CODE();

            if (TRAIN)
            {
                for (int c = 0; c < farm_list.Count; c++)
                {
                    string query_1 = "SELECT DISTINCT dbo.tbl_MRP_List_CAPEX.*, dbo.vw_AXFindimBananaRevenue.VALUE, dbo.vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc, dbo.vw_AXProdCategory.DESCRIPTION AS ProdDesc FROM dbo.tbl_MRP_List_CAPEX LEFT OUTER JOIN dbo.vw_AXProdCategory ON dbo.tbl_MRP_List_CAPEX.ProdCat = dbo.vw_AXProdCategory.NAME LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_CAPEX.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "' AND dbo.tbl_MRP_List_CAPEX.OprUnit = '" + farm_list[c] + "'";

                    cmd = new SqlCommand(query_1);

                    cmd.Connection = cn;
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row == dt.Rows[0])
                            {
                                DataRow dtRow = dtTable.NewRow();
                                if (c == 0)
                                {
                                    dtRow["VALUE"] = row["VALUE"].ToString();
                                    dtRow["RevDesc"] = row["RevDesc"].ToString();
                                    dtTable.Rows.Add(dtRow);
                                }

                                dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["Description"] = row["Description"].ToString();
                                dtRow["ProdCode"] = row["ProdCat"].ToString();
                                dtRow["ProdCat"] = row["ProdDesc"].ToString();
                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";

                                dtTable.Rows.Add(dtRow);
                            }
                            else
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["Description"] = row["Description"].ToString();
                                dtRow["ProdCode"] = row["ProdCat"].ToString();
                                dtRow["ProdCat"] = row["ProdDesc"].ToString();
                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";

                                dtTable.Rows.Add(dtRow);
                            }
                        }
                    }
                }
            }
            else
            {
                MRPClass.PrintString("IM HERE");
                string query_2 = "SELECT DISTINCT dbo.tbl_MRP_List_CAPEX.* FROM dbo.tbl_MRP_List_CAPEX WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

                //string query_2 = "SELECT DISTINCT dbo.tbl_MRP_List_CAPEX.*, dbo.vw_AXProdCategory.DESCRIPTION AS ProdDesc FROM dbo.tbl_MRP_List_CAPEX LEFT OUTER JOIN dbo.vw_AXProdCategory ON dbo.tbl_MRP_List_CAPEX.ProdCat = dbo.vw_AXProdCategory.NAME WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

                cmd = new SqlCommand(query_2);
                cmd.Connection = cn;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow dtRow = dtTable.NewRow();
                        dtRow["PK"] = row["PK"].ToString();
                        dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                        dtRow["Description"] = row["Description"].ToString();
                        //dtRow["ProdCode"] = row["ProdCat"].ToString();
                        //dtRow["ProdCat"] = row["ProdDesc"].ToString();
                        dtRow["UOM"] = row["UOM"].ToString();
                        dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                        dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                        dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                        dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                        dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                        dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");
                        dtRow["VALUE"] = "";
                        dtRow["RevDesc"] = "";

                        dtTable.Rows.Add(dtRow);
                        MRPClass.PrintString("IM HERE" + dt.Rows);
                    }
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable Preview_MAN(string DOC_NUMBER, string entity)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            //manpower_total_amount = 0;
            //man_edited_total = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ActivityCode", typeof(string));
                dtTable.Columns.Add("ManPowerTypeKey", typeof(Int32));
                dtTable.Columns.Add("ManPowerTypeKeyName", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("ACost", typeof(string));
                dtTable.Columns.Add("AQty", typeof(string));
                dtTable.Columns.Add("ATotalCost", typeof(string));
                dtTable.Columns.Add("VALUE", typeof(string));
                dtTable.Columns.Add("RevDesc", typeof(string));
            }


            string query_arraycode = "SELECT DISTINCT ActivityCode FROM [hijo_portal].[dbo].[tbl_MRP_List_ManPower] WHERE HeaderDocNum = '" + DOC_NUMBER + "' ORDER BY ActivityCode ASC";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            cmd = new SqlCommand(query_arraycode, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                list.Add(reader[0].ToString());
            }
            reader.Close();

            string query_farmunit = "SELECT DISTINCT dbo.vw_AXFindimBananaRevenue.VALUE FROM dbo.vw_AXFindimBananaRevenue INNER JOIN dbo.tbl_MRP_List_ManPower ON dbo.vw_AXFindimBananaRevenue.VALUE = dbo.tbl_MRP_List_ManPower.OprUnit WHERE HeaderDocNum = '" + DOC_NUMBER + "'";

            cmd = new SqlCommand(query_farmunit, conn);
            reader = cmd.ExecuteReader();
            ArrayList farm_list = new ArrayList();
            while (reader.Read())
            {
                farm_list.Add(reader[0].ToString());
            }
            reader.Close();
            bool TRAIN = entity == Constants.TRAIN_CODE();
            if (TRAIN)
            {
                for (int c = 0; c < farm_list.Count; c++)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string query_2 = "SELECT DISTINCT dbo.tbl_MRP_List_ManPower.PK, dbo.tbl_MRP_List_ManPower.HeaderDocNum, dbo.tbl_MRP_List_ManPower.TableIdentifier, dbo.tbl_MRP_List_ManPower.ActivityCode, dbo.tbl_MRP_List_ManPower.ManPowerTypeKey, dbo.tbl_MRP_List_ManPower.OprUnit, dbo.tbl_MRP_List_ManPower.Description, dbo.tbl_MRP_List_ManPower.UOM, dbo.tbl_MRP_List_ManPower.Cost, dbo.tbl_MRP_List_ManPower.Qty, dbo.tbl_MRP_List_ManPower.TotalCost, dbo.tbl_MRP_List_ManPower.EdittedQty, dbo.tbl_MRP_List_ManPower.EdittedCost, dbo.tbl_MRP_List_ManPower.EdittiedTotalCost, dbo.tbl_MRP_List_ManPower.ApprovedQty, dbo.tbl_MRP_List_ManPower.ApprovedCost, dbo.tbl_MRP_List_ManPower.ApprovedTotalCost, dbo.tbl_System_ManPowerType.ManPowerTypeDesc, dbo.vw_AXFindimActivity.DESCRIPTION AS AC_Desc, dbo.vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc FROM dbo.tbl_MRP_List_ManPower LEFT OUTER JOIN dbo.tbl_System_ManPowerType ON dbo.tbl_MRP_List_ManPower.ManPowerTypeKey = dbo.tbl_System_ManPowerType.PK LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_ManPower.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE LEFT OUTER JOIN dbo.vw_AXFindimActivity ON dbo.tbl_MRP_List_ManPower.ActivityCode = dbo.vw_AXFindimActivity.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "' AND tbl_MRP_List_ManPower.ActivityCode = '" + list[i] + "'  AND (dbo.tbl_MRP_List_ManPower.OprUnit = '" + farm_list[c] + "')";

                        cmd = new SqlCommand(query_2);

                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row == dt.Rows[0])
                                {
                                    DataRow dtRow = dtTable.NewRow();
                                    if (entity == Constants.TRAIN_CODE())
                                    {
                                        if (i == 0)
                                        {
                                            dtRow["VALUE"] = "";
                                            dtRow["RevDesc"] = row["RevDesc"].ToString();
                                            dtTable.Rows.Add(dtRow);
                                        }
                                    }

                                    dtRow = dtTable.NewRow();
                                    dtRow["ActivityCode"] = row["AC_Desc"].ToString();
                                    dtTable.Rows.Add(dtRow);

                                    dtRow = dtTable.NewRow();
                                    dtRow["PK"] = row["PK"].ToString();
                                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                    dtRow["ActivityCode"] = "";
                                    dtRow["ManPowerTypeKey"] = Convert.ToInt32(row["ManPowerTypeKey"]);
                                    dtRow["ManPowerTypeKeyName"] = row["ManPowerTypeDesc"].ToString();
                                    dtRow["Description"] = row["Description"].ToString();
                                    dtRow["UOM"] = row["UOM"].ToString();
                                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                    dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                    dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                    dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                    //manpower_total_amount += Convert.ToDouble(row["TotalCost"]);
                                    //man_edited_total += Convert.ToDouble(row["EdittiedTotalCost"]);

                                    if (entity == Constants.TRAIN_CODE())
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    else
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    dtTable.Rows.Add(dtRow);
                                }
                                else
                                {
                                    DataRow dtRow = dtTable.NewRow();
                                    dtRow["PK"] = row["PK"].ToString();
                                    dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                    dtRow["ActivityCode"] = "";
                                    dtRow["ManPowerTypeKey"] = Convert.ToInt32(row["ManPowerTypeKey"]);
                                    dtRow["ManPowerTypeKeyName"] = row["ManPowerTypeDesc"].ToString();
                                    dtRow["Description"] = row["Description"].ToString();
                                    dtRow["UOM"] = row["UOM"].ToString();
                                    dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                    dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                    dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                    dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                    dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                    //manpower_total_amount += Convert.ToDouble(row["TotalCost"]);
                                    //man_edited_total += Convert.ToDouble(row["EdittiedTotalCost"]);

                                    if (entity == Constants.TRAIN_CODE())
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    else
                                    {
                                        dtRow["VALUE"] = "";
                                        dtRow["RevDesc"] = "";
                                    }
                                    dtTable.Rows.Add(dtRow);
                                }
                            }
                        }
                        dt.Clear();

                    }
                }
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string query_1 = "SELECT DISTINCT dbo.tbl_MRP_List_ManPower.PK, dbo.tbl_MRP_List_ManPower.HeaderDocNum, dbo.tbl_MRP_List_ManPower.TableIdentifier, dbo.tbl_MRP_List_ManPower.ActivityCode, dbo.tbl_MRP_List_ManPower.ManPowerTypeKey, dbo.tbl_MRP_List_ManPower.OprUnit, dbo.tbl_MRP_List_ManPower.Description, dbo.tbl_MRP_List_ManPower.UOM, dbo.tbl_MRP_List_ManPower.Cost, dbo.tbl_MRP_List_ManPower.Qty, dbo.tbl_MRP_List_ManPower.TotalCost, dbo.tbl_MRP_List_ManPower.EdittedQty, dbo.tbl_MRP_List_ManPower.EdittedCost, dbo.tbl_MRP_List_ManPower.EdittiedTotalCost, dbo.tbl_MRP_List_ManPower.ApprovedQty, dbo.tbl_MRP_List_ManPower.ApprovedCost, dbo.tbl_MRP_List_ManPower.ApprovedTotalCost, dbo.tbl_System_ManPowerType.ManPowerTypeDesc, dbo.vw_AXFindimActivity.DESCRIPTION AS AC_Desc, dbo.vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc FROM dbo.tbl_MRP_List_ManPower LEFT OUTER JOIN dbo.tbl_System_ManPowerType ON dbo.tbl_MRP_List_ManPower.ManPowerTypeKey = dbo.tbl_System_ManPowerType.PK LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_ManPower.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE LEFT OUTER JOIN dbo.vw_AXFindimActivity ON dbo.tbl_MRP_List_ManPower.ActivityCode = dbo.vw_AXFindimActivity.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "' AND tbl_MRP_List_ManPower.ActivityCode = '" + list[i] + "' ";

                    cmd = new SqlCommand(query_1);

                    cmd.Connection = cn;
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row == dt.Rows[0])
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["ActivityCode"] = row["AC_Desc"].ToString();
                                dtTable.Rows.Add(dtRow);

                                dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ActivityCode"] = "";
                                dtRow["ManPowerTypeKey"] = Convert.ToInt32(row["ManPowerTypeKey"]);
                                dtRow["ManPowerTypeKeyName"] = row["ManPowerTypeDesc"].ToString();
                                dtRow["Description"] = row["Description"].ToString();
                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";
                                dtTable.Rows.Add(dtRow);
                            }
                            else
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ActivityCode"] = "";
                                dtRow["ManPowerTypeKey"] = Convert.ToInt32(row["ManPowerTypeKey"]);
                                dtRow["ManPowerTypeKeyName"] = row["ManPowerTypeDesc"].ToString();
                                dtRow["Description"] = row["Description"].ToString();
                                dtRow["UOM"] = row["UOM"].ToString();
                                dtRow["Cost"] = Convert.ToDouble(row["Cost"]).ToString("N");
                                dtRow["Qty"] = Convert.ToDouble(row["Qty"]).ToString("N");
                                dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"]).ToString("N");

                                dtRow["ACost"] = Convert.ToDouble(row["EdittedCost"]).ToString("N");
                                dtRow["AQty"] = Convert.ToDouble(row["EdittedQty"]).ToString("N");
                                dtRow["ATotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";
                                dtTable.Rows.Add(dtRow);
                            }
                        }
                    }
                    dt.Clear();
                }

            }
            cn.Close();
            return dtTable;
        }

        public static DataTable Preview_Revenue(string DOC_NUMBER, string entitycode)
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
                dtTable.Columns.Add("HeaderDocNum", typeof(string));
                dtTable.Columns.Add("ProductName", typeof(string));
                dtTable.Columns.Add("FarmName", typeof(string));
                dtTable.Columns.Add("Prize", typeof(string));
                dtTable.Columns.Add("Volume", typeof(string));
                dtTable.Columns.Add("TotalPrize", typeof(string));
                dtTable.Columns.Add("VALUE", typeof(string));
                dtTable.Columns.Add("RevDesc", typeof(string));
            }

            string query_farmunit = "SELECT DISTINCT dbo.vw_AXFindimBananaRevenue.VALUE FROM dbo.tbl_MRP_List_RevenueAssumptions LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_RevenueAssumptions.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE HeaderDocNum = '" + DOC_NUMBER + "'";

            cmd = new SqlCommand(query_farmunit, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList farm_list = new ArrayList();
            while (reader.Read())
            {
                farm_list.Add(reader[0].ToString());
            }
            reader.Close();

            bool TRAIN = entitycode == Constants.TRAIN_CODE();
            if (TRAIN)
            {
                for (int i = 0; i < farm_list.Count; i++)
                {
                    string query_1 = "SELECT DISTINCT dbo.tbl_MRP_List_RevenueAssumptions.*, dbo.vw_AXFindimBananaRevenue.VALUE, dbo.vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc FROM dbo.tbl_MRP_List_RevenueAssumptions LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_RevenueAssumptions.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE [HeaderDocNum] = '" + DOC_NUMBER + "' AND dbo.tbl_MRP_List_RevenueAssumptions.OprUnit = '" + farm_list[i] + "'";

                    MRPClass.PrintString(query_1);

                    cmd = new SqlCommand(query_1);
                    cmd.Connection = cn;
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            MRPClass.PrintString(farm_list[i].ToString());
                            if (row == dt.Rows[0])
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["VALUE"] = row["VALUE"].ToString();
                                dtRow["RevDesc"] = row["RevDesc"].ToString();
                                dtTable.Rows.Add(dtRow);

                                dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ProductName"] = row["ProductName"].ToString();
                                dtRow["FarmName"] = row["FarmName"].ToString();
                                dtRow["Prize"] = Convert.ToDouble(row["Prize"]).ToString("N");
                                dtRow["Volume"] = Convert.ToDouble(row["Volume"]).ToString("N");
                                dtRow["TotalPrize"] = Convert.ToDouble(row["TotalPrize"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";
                                dtTable.Rows.Add(dtRow);

                            }
                            else
                            {
                                DataRow dtRow = dtTable.NewRow();
                                dtRow["PK"] = row["PK"].ToString();
                                dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                                dtRow["ProductName"] = row["ProductName"].ToString();
                                dtRow["FarmName"] = row["FarmName"].ToString();
                                dtRow["Prize"] = Convert.ToDouble(row["Prize"]).ToString("N");
                                dtRow["Volume"] = Convert.ToDouble(row["Volume"]).ToString("N");
                                dtRow["TotalPrize"] = Convert.ToDouble(row["TotalPrize"]).ToString("N");

                                dtRow["VALUE"] = "";
                                dtRow["RevDesc"] = "";
                                dtTable.Rows.Add(dtRow);
                            }



                        }
                    }
                    dt.Clear();
                }
            }
            else
            {
                string query_2 = "SELECT * FROM [hijo_portal].[dbo].[tbl_MRP_List_RevenueAssumptions] WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

                cmd = new SqlCommand(query_2);
                cmd.Connection = cn;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow dtRow = dtTable.NewRow();
                        dtRow["PK"] = row["PK"].ToString();
                        dtRow["HeaderDocNum"] = row["HeaderDocNum"].ToString();
                        dtRow["ProductName"] = row["ProductName"].ToString();
                        dtRow["FarmName"] = row["FarmName"].ToString();
                        dtRow["Prize"] = Convert.ToDouble(row["Prize"]).ToString("N");
                        dtRow["Volume"] = Convert.ToDouble(row["Volume"]).ToString("N");
                        dtRow["TotalPrize"] = Convert.ToDouble(row["TotalPrize"]).ToString("N");

                        dtRow["VALUE"] = "";
                        dtRow["RevDesc"] = "";
                        dtTable.Rows.Add(dtRow);

                    }
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static string preview_total_opex(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_OPEX.TotalCost) AS TotalSum FROM dbo.tbl_MRP_List INNER JOIN dbo.tbl_MRP_List_OPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_OPEX.HeaderDocNum WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_requestedtotal_opex(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_OPEX.EdittedTotalCost) AS TotalSum FROM dbo.tbl_MRP_List_OPEX  WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_total_directmaterials(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_DirectMaterials.TotalCost) AS TotalSum FROM dbo.tbl_MRP_List INNER JOIN dbo.tbl_MRP_List_DirectMaterials ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_DirectMaterials.HeaderDocNum WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_requestedtotal_directmaterials(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_DirectMaterials.EdittiedTotalCost) AS TotalSum FROM dbo.tbl_MRP_List_DirectMaterials WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_total_capex(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_CAPEX.TotalCost) AS TotalSum FROM dbo.tbl_MRP_List INNER JOIN dbo.tbl_MRP_List_CAPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_CAPEX.HeaderDocNum WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_requestedtotal_capex(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(EdittiedTotalCost) AS TotalSum FROM dbo.tbl_MRP_List_CAPEX WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_total_manpower(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_ManPower.TotalCost) AS TotalSum FROM dbo.tbl_MRP_List INNER JOIN dbo.tbl_MRP_List_ManPower ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_ManPower.HeaderDocNum WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_requestedtotal_manpower(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_ManPower.EdittiedTotalCost) AS TotalSum FROM dbo.tbl_MRP_List_ManPower WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        public static string preview_total_revenue(string DOC_NUMBER)
        {
            string total = "";
            string query = "SELECT SUM(dbo.tbl_MRP_List_RevenueAssumptions.TotalPrize) AS TotalSum FROM dbo.tbl_MRP_List INNER JOIN dbo.tbl_MRP_List_RevenueAssumptions ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_RevenueAssumptions.HeaderDocNum WHERE [HeaderDocNum] = '" + DOC_NUMBER + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                total = reader["TotalSum"].ToString();
            }

            conn.Close();

            if (!string.IsNullOrEmpty(total))
                total = Convert.ToDouble(total).ToString("N");

            return total;
        }

        private static Double prev_summary = 0;
        public static string Prev_Summary_Total()
        {
            return prev_summary.ToString("N");
        }
        public static DataTable MRP_PrevTotalSummary(string DOC_NUMBER, string entitycode)
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlDataAdapter adp;
            prev_summary = 0;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("Name", typeof(string));
                dtTable.Columns.Add("Total", typeof(string));

            }
            string name = "";
            SqlCommand com = null;

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        name = Constants.DM_string();
                        string query_1 = "SELECT SUM(TotalCost) AS Total FROM [hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials] WHERE(HeaderDocNum = '" + DOC_NUMBER + "')GROUP BY HeaderDocNum";
                        com = new SqlCommand(query_1, cn);
                        break;

                    case 1:
                        name = Constants.OP_string();
                        string query_2 = "SELECT SUM(TotalCost) AS Total FROM [hijo_portal].[dbo].[tbl_MRP_List_OPEX] WHERE(HeaderDocNum = '" + DOC_NUMBER + "')GROUP BY HeaderDocNum";
                        com = new SqlCommand(query_2, cn);
                        break;

                    case 2:
                        name = Constants.MAN_string();
                        string query_3 = "SELECT SUM(TotalCost) AS Total FROM [hijo_portal].[dbo].[tbl_MRP_List_ManPower] WHERE(HeaderDocNum = '" + DOC_NUMBER + "')GROUP BY HeaderDocNum";
                        com = new SqlCommand(query_3, cn);
                        break;

                    case 3:
                        name = Constants.CA_string();
                        string query_4 = "SELECT SUM(TotalCost) AS Total FROM [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] WHERE(HeaderDocNum = '" + DOC_NUMBER + "')GROUP BY HeaderDocNum";
                        com = new SqlCommand(query_4, cn);
                        break;
                }

                dt.Clear();
                com.Connection = cn;
                adp = new SqlDataAdapter(com);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow dtRow = dtTable.NewRow();
                        dtRow["Name"] = name;
                        Double total = Convert.ToDouble(row[0].ToString());
                        dtRow["Total"] = total.ToString("N");
                        prev_summary += total;
                        dtTable.Rows.Add(dtRow);
                    }
                }
            }


            dt.Clear();
            cn.Close();
            return dtTable;
        }
    }
}
