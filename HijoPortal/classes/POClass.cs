using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using DevExpress.Web;
using AjaxControlToolkit;
using System.Collections;

namespace HijoPortal.classes
{
    public class POClass
    {

        public static DataTable PO_Created_List()
        {
            DataTable dtTable = new DataTable();
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlDataReader reader = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("PONumber", typeof(string));
                dtTable.Columns.Add("MOPNumber", typeof(string));
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("BU", typeof(string));
                dtTable.Columns.Add("VendorCode", typeof(string));
                dtTable.Columns.Add("VendorName", typeof(string));
                dtTable.Columns.Add("DateCreated", typeof(string));
                dtTable.Columns.Add("CreatorKey", typeof(string));
                dtTable.Columns.Add("Creator", typeof(string));
                dtTable.Columns.Add("ExpectedDate", typeof(string));
                dtTable.Columns.Add("TotalAmount", typeof(string));
                dtTable.Columns.Add("POStatus", typeof(string));
                dtTable.Columns.Add("Status", typeof(string));
            }

            //string qry = "SELECT dbo.tbl_POCreation.PK, dbo.tbl_POCreation.PONumber, dbo.tbl_POCreation.MRPNumber, dbo.tbl_POCreation.VendorCode, dbo.vw_AXVendTable.NAME AS VendorName, dbo.tbl_POCreation.DateCreated, dbo.tbl_POCreation.CreatorKey, dbo.tbl_Users.Firstname, dbo.tbl_Users.Lastname, dbo.tbl_POCreation.ExpectedDate,  dbo.tbl_POCreation.POStatus FROM dbo.tbl_POCreation LEFT OUTER JOIN dbo.tbl_Users ON dbo.tbl_POCreation.CreatorKey = dbo.tbl_Users.PK LEFT OUTER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM ORDER BY dbo.tbl_POCreation.PONumber DESC";

            string qry = "SELECT DISTINCT TOP(100) PERCENT dbo.tbl_POCreation.PK, dbo.tbl_POCreation.PONumber, dbo.tbl_POCreation.MRPNumber, dbo.tbl_POCreation.VendorCode, dbo.vw_AXVendTable.NAME AS VendorName, dbo.tbl_POCreation.DateCreated, dbo.tbl_POCreation.CreatorKey, dbo.tbl_Users.Firstname, dbo.tbl_Users.Lastname, dbo.tbl_POCreation.ExpectedDate, dbo.tbl_POCreation.POStatus, dbo.vw_AXEntityTable.NAME as EntityName, dbo.vw_AXOperatingUnitTable.NAME AS BU FROM   dbo.tbl_POCreation INNER JOIN dbo.tbl_MRP_List ON dbo.tbl_POCreation.MRPNumber = dbo.tbl_MRP_List.DocNumber INNER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN dbo.tbl_Users ON dbo.tbl_POCreation.CreatorKey = dbo.tbl_Users.PK LEFT OUTER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM ORDER BY dbo.tbl_POCreation.PONumber DESC";

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
                    dtRow["PONumber"] = row["PONumber"].ToString();
                    dtRow["MOPNumber"] = row["MRPNumber"].ToString();
                    dtRow["Entity"] = row["EntityName"].ToString();
                    dtRow["BU"] = row["BU"].ToString();
                    dtRow["VendorName"] = row["VendorName"].ToString();
                    dtRow["VendorCode"] = row["VendorCode"].ToString();
                    dtRow["DateCreated"] = Convert.ToDateTime(row["DateCreated"]).ToString("MM/dd/yyyy");
                    dtRow["CreatorKey"] = row["CreatorKey"].ToString();
                    dtRow["Creator"] = EncryptionClass.Decrypt(row["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    dtRow["ExpectedDate"] = Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy");

                    string query = "SELECT SUM([TotalCost]) FROM [hijo_portal].[dbo].[tbl_POCreation_Details] WHERE [PONumber] = '" + row["PONumber"].ToString() + "' GROUP BY MOPNumber";
                    cmd = new SqlCommand(query, cn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dtRow["TotalAmount"] = Convert.ToDouble(reader[0].ToString()).ToString("N");
                    }
                    reader.Close();

                    dtRow["POStatus"] = row["POStatus"].ToString();

                    if (Convert.ToInt32(row["POStatus"]) == 0)
                    {
                        dtRow["Status"] = "Created";
                    }
                    else
                    {
                        dtRow["Status"] = "Submitted";
                    }
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable SelectItemsForPO(int Month, int Year, string DocNumber)
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
                dtTable.Columns.Add("ItemPK", typeof(string));
                dtTable.Columns.Add("ItemIdentiflier", typeof(string));
                dtTable.Columns.Add("MOPNumber", typeof(string));
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("BUSSU", typeof(string));
                dtTable.Columns.Add("MOPType", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }


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

            string qry = "SELECT [PK], [MRPMonth], [MRPYear] FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE PK IN(SELECT MAX(PK) FROM [hijo_portal].[dbo].[tbl_MRP_List] GROUP BY MRPMonth, MRPYear) AND StatusKey = '4' ORDER BY MRPMonth, MRPYear ASC";

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
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable DocumentNumber_By_Month_Year(int month, string year)
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
                dtTable.Columns.Add("DocumentNumber", typeof(string));
                dtTable.Columns.Add("EntityCode", typeof(string));
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("BU", typeof(string));
            }

            string month_string = "'" + month.ToString() + "'";
            string year_string = "'" + year + "'";
            if (month == 0)
            {
                month_string = "MRPMonth";
                year_string = "MRPYear";
            }

            string qry = "SELECT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.ID AS EntityCode, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU FROM dbo.tbl_MRP_List INNER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER WHERE MRPMonth = " + month_string + " AND MRPYear = " + year_string + " AND StatusKey = '4' ORDER BY MRPMonth, MRPYear ASC";

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
                    dtRow["DocumentNumber"] = row["DocNumber"].ToString();
                    dtRow["EntityCode"] = row["EntityCode"].ToString();
                    dtRow["Entity"] = row["Entity"].ToString();
                    dtRow["BU"] = row["BU"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable POSelecetedItemTable(string docnumber, int month, string year, ArrayList groupID)
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
                dtTable.Columns.Add("TableIdentifier", typeof(string));
                dtTable.Columns.Add("DocumentNumber", typeof(string));
                dtTable.Columns.Add("CapexCIP", typeof(string));
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("BU", typeof(string));
                dtTable.Columns.Add("ItemCatCode", typeof(string));
                dtTable.Columns.Add("ItemCat", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("ItemDescription", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
            }

            //string groupid_string = "";
            //if (string.IsNullOrEmpty(groupID) || groupID == "ALL") groupid_string = "dbo.vw_AXInventTable.ITEMGROUPID";
            //else groupid_string = "'" + groupID + "'";

            MRPClass.PrintString("year:" + year);
            MRPClass.PrintString("month:" + month);
            MRPClass.PrintString("doc:" + docnumber);
            MRPClass.PrintString("count:" + groupID.Count);

            if (groupID.Count == 0)
                return dtTable;

            string groupid_string = "", qry = "";
            int i = -1;

            for (i = 0; i < groupID.Count; i++)
            {
                groupid_string = groupID[i].ToString();
                //MRPClass.PrintString("count:" + groupID.Count.ToString());

                //Direct Materials
                //qry = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.PK, dbo.tbl_MRP_List_DirectMaterials.TableIdentifier, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List_DirectMaterials.ItemCode, dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.ItemDescriptionAddl,dbo.tbl_MRP_List_DirectMaterials.UOM, dbo.tbl_MRP_List_DirectMaterials.Qty, dbo.tbl_MRP_List_DirectMaterials.Cost, dbo.tbl_MRP_List_DirectMaterials.TotalCost, dbo.vw_AXInventTable.ITEMGROUPID AS ItemCatCode, dbo.vw_AXProdCategory.DESCRIPTION AS ItemCat FROM  dbo.vw_AXProdCategory RIGHT OUTER JOIN dbo.vw_AXInventTable ON dbo.vw_AXProdCategory.NAME = dbo.vw_AXInventTable.ITEMGROUPID RIGHT OUTER JOIN dbo.tbl_MRP_List LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN dbo.tbl_MRP_List_DirectMaterials ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_DirectMaterials.HeaderDocNum ON dbo.vw_AXInventTable.ITEMID = dbo.tbl_MRP_List_DirectMaterials.ItemCode WHERE(dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = '" + year + "') AND(dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') AND(dbo.vw_AXInventTable.ITEMGROUPID = '" + groupid_string + "') AND (dbo.tbl_MRP_List_DirectMaterials.AvailForPO > 0)";
                qry = "SELECT DISTINCT dbo.tbl_MRP_List_OPEX.PK, dbo.tbl_MRP_List_OPEX.TableIdentifier, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List_OPEX.ItemCode, dbo.tbl_MRP_List_OPEX.Description, dbo.tbl_MRP_List_OPEX.DescriptionAddl, dbo.tbl_MRP_List_OPEX.UOM, dbo.tbl_MRP_List_OPEX.Qty, dbo.tbl_MRP_List_OPEX.Cost, dbo.tbl_MRP_List_OPEX.TotalCost, (CASE dbo.tbl_MRP_List_OPEX.ItemCode WHEN '' THEN dbo.tbl_MRP_List_OPEX.ProcCat ELSE dbo.vw_AXInventTable.ITEMGROUPID END) AS ItemCatCode, (SELECT DESCRIPTION FROM  dbo.vw_AXProdCategory  WHERE(NAME = (CASE dbo.tbl_MRP_List_OPEX.ItemCode WHEN '' THEN dbo.tbl_MRP_List_OPEX.ProcCat ELSE dbo.vw_AXInventTable.ITEMGROUPID END)) AND(dataareaid = dbo.tbl_MRP_List.EntityCode)) AS ItemCat FROM dbo.vw_AXInventTable RIGHT OUTER JOIN dbo.tbl_MRP_List LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN    dbo.tbl_MRP_List_OPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_OPEX.HeaderDocNum ON dbo.vw_AXInventTable.ITEMID = dbo.tbl_MRP_List_OPEX.ItemCode WHERE(dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = '" + year + "') AND(dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') AND(dbo.tbl_MRP_List_OPEX.AvailForPO > 0) AND ((CASE dbo.tbl_MRP_List_OPEX.ItemCode WHEN '' THEN dbo.tbl_MRP_List_OPEX.ProcCat ELSE dbo.vw_AXInventTable.ITEMGROUPID END) = '" + groupid_string + "')";
                cmd = new SqlCommand(qry);
                cmd.Connection = cn;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow dtRow = dtTable.NewRow();
                        dtRow["PK"] = row["PK"].ToString() + "-" + row["TableIdentifier"].ToString();
                        dtRow["TableIdentifier"] = row["TableIdentifier"].ToString();
                        dtRow["DocumentNumber"] = row["DocNumber"].ToString();
                        dtRow["CapexCIP"] = "";
                        dtRow["Entity"] = row["Entity"].ToString();
                        dtRow["BU"] = row["BU"].ToString();
                        dtRow["ItemCatCode"] = row["ItemCatCode"].ToString();
                        dtRow["ItemCat"] = row["ItemCat"].ToString();
                        dtRow["ItemCode"] = row["ItemCode"].ToString();
                        if (row["DescriptionAddl"].ToString().Trim() != "")
                        {
                            dtRow["ItemDescription"] = row["Description"].ToString() + " (" + row["DescriptionAddl"].ToString() + ")";
                        }
                        else
                        {
                            dtRow["ItemDescription"] = row["Description"].ToString();
                        }
                        dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                        dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                        dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                        dtRow["UOM"] = row["UOM"].ToString();
                        dtTable.Rows.Add(dtRow);
                    }
                }
                dt.Clear();

                //Operating Expense
                qry = "SELECT DISTINCT dbo.tbl_MRP_List_OPEX.PK, dbo.tbl_MRP_List_OPEX.TableIdentifier, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List_OPEX.ItemCode, dbo.tbl_MRP_List_OPEX.Description, dbo.tbl_MRP_List_OPEX.DescriptionAddl, dbo.tbl_MRP_List_OPEX.UOM,dbo.tbl_MRP_List_OPEX.Qty, dbo.tbl_MRP_List_OPEX.Cost, dbo.tbl_MRP_List_OPEX.TotalCost, dbo.vw_AXInventTable.ITEMGROUPID AS ItemCatCode, dbo.vw_AXProdCategory.DESCRIPTION AS ItemCat FROM  dbo.vw_AXProdCategory RIGHT OUTER JOIN dbo.vw_AXInventTable ON dbo.vw_AXProdCategory.NAME = dbo.vw_AXInventTable.ITEMGROUPID RIGHT OUTER JOIN dbo.tbl_MRP_List LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN         dbo.tbl_MRP_List_OPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_OPEX.HeaderDocNum ON dbo.vw_AXInventTable.ITEMID = dbo.tbl_MRP_List_OPEX.ItemCode WHERE(dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = '" + year + "') AND(dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') AND(dbo.vw_AXInventTable.ITEMGROUPID = '" + groupid_string + "') AND (dbo.tbl_MRP_List_OPEX.AvailForPO > 0)";

                cmd = new SqlCommand(qry);
                cmd.Connection = cn;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow dtRow = dtTable.NewRow();
                        dtRow["PK"] = row["PK"].ToString() + "-" + row["TableIdentifier"].ToString();
                        dtRow["TableIdentifier"] = row["TableIdentifier"].ToString();
                        dtRow["DocumentNumber"] = row["DocNumber"].ToString();
                        dtRow["CapexCIP"] = "";
                        dtRow["Entity"] = row["Entity"].ToString();
                        dtRow["BU"] = row["BU"].ToString();
                        dtRow["ItemCatCode"] = row["ItemCatCode"].ToString();
                        dtRow["ItemCat"] = row["ItemCat"].ToString();
                        dtRow["ItemCode"] = row["ItemCode"].ToString();
                        if (row["DescriptionAddl"].ToString().Trim() != "")
                        {
                            dtRow["ItemDescription"] = row["Description"].ToString() + " (" + row["DescriptionAddl"].ToString() + ")";
                        }
                        else
                        {
                            dtRow["ItemDescription"] = row["Description"].ToString();
                        }
                        dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                        dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                        dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                        dtRow["UOM"] = row["UOM"].ToString();
                        dtTable.Rows.Add(dtRow);
                    }
                }
                dt.Clear();

                //Capital Expenditure
                //qry = "SELECT dbo.tbl_MRP_List_CAPEX.CIPSIPNumber, dbo.tbl_MRP_List_CAPEX.PK, dbo.tbl_MRP_List_CAPEX.TableIdentifier, dbo.tbl_MRP_List_CAPEX.ProdCat, dbo.tbl_MRP_List_CAPEX.Description, dbo.tbl_MRP_List_CAPEX.UOM, dbo.tbl_MRP_List_CAPEX.Cost, dbo.tbl_MRP_List_CAPEX.Qty, dbo.tbl_MRP_List_CAPEX.TotalCost,  dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List.DocNumber FROM   dbo.vw_AXEntityTable INNER JOIN dbo.tbl_MRP_List ON dbo.vw_AXEntityTable.ID = dbo.tbl_MRP_List.EntityCode INNER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER INNER JOIN dbo.tbl_MRP_List_CAPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_CAPEX.HeaderDocNum WHERE ((dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = '" + year + "') AND (dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') AND(dbo.tbl_MRP_List_CAPEX.ProdCat = '" + groupid_string + "') AND (dbo.tbl_MRP_List_CAPEX.AvailForPO > 0) AND (RTRIM(LTRIM(dbo.tbl_MRP_List_CAPEX.CIPSIPNumber)) <> '')) OR ((dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = '" + year + "') AND (dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') AND(dbo.tbl_MRP_List_CAPEX.ProdCat = '" + groupid_string + "') AND (dbo.tbl_MRP_List_CAPEX.AvailForPO > 0) AND (RTRIM(LTRIM(dbo.tbl_MRP_List_CAPEX.ProdCat)) = 'CIP'))";

                qry = "SELECT DISTINCT dbo.tbl_MRP_List_CAPEX.CIPSIPNumber, dbo.tbl_MRP_List_CAPEX.PK, dbo.tbl_MRP_List_CAPEX.TableIdentifier, dbo.tbl_MRP_List_CAPEX.ProdCat, dbo.tbl_MRP_List_CAPEX.Description, dbo.tbl_MRP_List_CAPEX.UOM, dbo.tbl_MRP_List_CAPEX.Cost, dbo.tbl_MRP_List_CAPEX.Qty, dbo.tbl_MRP_List_CAPEX.TotalCost, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXProdCategory.DESCRIPTION AS ProdCatDesc FROM   dbo.vw_AXEntityTable INNER JOIN dbo.tbl_MRP_List ON dbo.vw_AXEntityTable.ID = dbo.tbl_MRP_List.EntityCode INNER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER INNER JOIN dbo.tbl_MRP_List_CAPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_CAPEX.HeaderDocNum INNER JOIN dbo.vw_AXProdCategory ON dbo.tbl_MRP_List_CAPEX.ProdCat = dbo.vw_AXProdCategory.NAME WHERE ((dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = '" + year + "') AND (dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') AND(dbo.tbl_MRP_List_CAPEX.ProdCat = '" + groupid_string + "') AND (dbo.tbl_MRP_List_CAPEX.AvailForPO > 0) AND (RTRIM(LTRIM(dbo.tbl_MRP_List_CAPEX.CIPSIPNumber)) <> '')) OR ((dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = '" + year + "') AND (dbo.tbl_MRP_List.MRPMonth = '" + month + "') AND(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') AND(dbo.tbl_MRP_List_CAPEX.ProdCat = '" + groupid_string + "') AND (dbo.tbl_MRP_List_CAPEX.AvailForPO > 0) AND (RTRIM(LTRIM(dbo.tbl_MRP_List_CAPEX.ProdCat)) = 'CIP'))";
                cmd = new SqlCommand(qry);
                cmd.Connection = cn;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow dtRow = dtTable.NewRow();
                        dtRow["PK"] = row["PK"].ToString() + "-" + row["TableIdentifier"].ToString();
                        dtRow["TableIdentifier"] = row["TableIdentifier"].ToString();
                        dtRow["DocumentNumber"] = row["DocNumber"].ToString();
                        dtRow["CapexCIP"] = row["CIPSIPNumber"].ToString();
                        dtRow["Entity"] = row["Entity"].ToString();
                        dtRow["BU"] = row["BU"].ToString();
                        //dtRow["ItemCatCode"] = row["ItemCatCode"].ToString();
                        dtRow["ItemCatCode"] = "";
                        dtRow["ItemCat"] = row["ProdCatDesc"].ToString();
                        dtRow["ItemCode"] = "";
                        if (row["DescriptionAddl"].ToString().Trim() != "")
                        {
                            dtRow["ItemDescription"] = row["Description"].ToString() + " (" + row["DescriptionAddl"].ToString() + ")";
                        }
                        else
                        {
                            dtRow["ItemDescription"] = row["Description"].ToString();
                        }
                        dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                        dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                        dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                        dtRow["UOM"] = row["UOM"].ToString();
                        dtTable.Rows.Add(dtRow);
                    }
                }
                dt.Clear();
            }

            cn.Close();

            return dtTable;
        }

        public static DataTable VendTableTable()
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
                dtTable.Columns.Add("ACCOUNTNUM", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT DISTINCT [ACCOUNTNUM],[NAME] FROM [hijo_portal].[dbo].[vw_AXVendTable]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ACCOUNTNUM"] = row["ACCOUNTNUM"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString(); ;
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable PaymTermTable()
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
                dtTable.Columns.Add("PAYMTERMID", typeof(string));
                dtTable.Columns.Add("DESCRIPTION", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXPaymTerm]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PAYMTERMID"] = row["PAYMTERMID"].ToString();
                    dtRow["DESCRIPTION"] = row["DESCRIPTION"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable CurrencyTable()
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
                dtTable.Columns.Add("CURRENCYCODE", typeof(string));
                dtTable.Columns.Add("TXT", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXCurrency]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["CURRENCYCODE"] = row["CURRENCYCODE"].ToString();
                    dtRow["TXT"] = row["TXT"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable InventSiteTable(string entity)
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
                dtTable.Columns.Add("NAME", typeof(string));
                dtTable.Columns.Add("SITEID", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSite] WHERE (DATAAREAID = '" + entity + "') ORDER BY NAME ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtRow["SITEID"] = row["SITEID"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable InventSiteWarehouseTable(string ID)
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
                dtTable.Columns.Add("warehouse", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSiteWarehouse] WHERE ([INVENTSITEID] = '" + ID + "') AND (NOT (warehouse = 'TRANSIT'))  ORDER BY NAME ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["warehouse"] = row["warehouse"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable InventSiteLocationTable(string warehouse)
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
                dtTable.Columns.Add("LocationCode", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSiteLocation] WHERE ([WarehouseCode] = '" + warehouse + "') AND (NOT (WarehouseCode = 'TRANSIT'))  ORDER BY WarehouseCode ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["LocationCode"] = row["LocationCode"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable POCreate_TmpTable(string creatorkey)
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
                dtTable.Columns.Add("MOPNumber", typeof(string));
                dtTable.Columns.Add("ItemPK", typeof(string));
                dtTable.Columns.Add("TableIdentifier", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("CapexCIP", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("RequestedQty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("POUOM", typeof(string));
                dtTable.Columns.Add("POQty", typeof(string));
                dtTable.Columns.Add("POCost", typeof(string));
                dtTable.Columns.Add("TotalPOCost", typeof(string));
                dtTable.Columns.Add("TaxGroup", typeof(string));
                dtTable.Columns.Add("TaxItemGroup", typeof(string));
            }

            string qry = "SELECT dbo.tbl_POCreation_Tmp.PK, dbo.tbl_POCreation_Tmp.MOPNumber, dbo.tbl_POCreation_Tmp.ItemPK, dbo.tbl_POCreation_Tmp.ItemIdentifier, dbo.tbl_MRP_List_DirectMaterials.ItemCode, dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.ItemDescriptionAddl, dbo.tbl_MRP_List_DirectMaterials.Qty, dbo.tbl_MRP_List_DirectMaterials.TotalCost, dbo.tbl_MRP_List_DirectMaterials.Cost, dbo.tbl_POCreation_Tmp.TaxGroup, dbo.tbl_POCreation_Tmp.TaxItemGroup, dbo.tbl_POCreation_Tmp.POQty, dbo.tbl_POCreation_Tmp.POCost, dbo.tbl_POCreation_Tmp.POTotalCost, dbo.tbl_POCreation_Tmp.POUOM, dbo.tbl_MRP_List_DirectMaterials.UOM FROM   dbo.tbl_POCreation_Tmp INNER JOIN dbo.tbl_MRP_List_DirectMaterials ON dbo.tbl_POCreation_Tmp.ItemPK = dbo.tbl_MRP_List_DirectMaterials.PK WHERE UserKey = '" + creatorkey + "' AND ItemIdentifier = '1'";

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
                    dtRow["MOPNumber"] = row["MOPNumber"].ToString();
                    dtRow["ItemPK"] = row["ItemPK"].ToString();
                    dtRow["TableIdentifier"] = row["ItemIdentifier"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["CapexCIP"] = "";
                    string desc = row["ItemDescriptionAddl"].ToString();
                    if (string.IsNullOrEmpty(desc))
                        dtRow["Description"] = row["ItemDescription"].ToString();
                    else
                        dtRow["Description"] = row["ItemDescription"].ToString() + " (" + desc + ")";

                    dtRow["RequestedQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");

                    string pouom = row["POUOM"].ToString();
                    if (!string.IsNullOrEmpty(pouom))
                        dtRow["POUOM"] = row["POUOM"].ToString();
                    else
                        dtRow["POUOM"] = row["UOM"].ToString();

                    string poqty = row["POQty"].ToString();
                    string pocost = row["POCost"].ToString();
                    string pototalcost = row["POTotalCost"].ToString();


                    if (Convert.ToDouble(poqty) == 0 && Convert.ToDouble(pocost) == 0 && Convert.ToDouble(pototalcost) == 0)
                    {
                        dtRow["POQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                        dtRow["POCost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                        dtRow["TotalPOCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    }
                    else
                    {
                        dtRow["POQty"] = Convert.ToDouble(poqty).ToString("N");
                        dtRow["POCost"] = Convert.ToDouble(pocost).ToString("N");
                        dtRow["TotalPOCost"] = Convert.ToDouble(pototalcost).ToString("N");
                    }

                    dtRow["TaxGroup"] = row["TaxGroup"].ToString();
                    dtRow["TaxItemGroup"] = row["TaxItemGroup"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();

            qry = "SELECT dbo.tbl_POCreation_Tmp.PK, dbo.tbl_POCreation_Tmp.MOPNumber, dbo.tbl_POCreation_Tmp.ItemPK, dbo.tbl_POCreation_Tmp.ItemIdentifier, dbo.tbl_MRP_List_OPEX.ItemCode, dbo.tbl_MRP_List_OPEX.Description, dbo.tbl_MRP_List_OPEX.DescriptionAddl, dbo.tbl_MRP_List_OPEX.Cost, dbo.tbl_MRP_List_OPEX.Qty, dbo.tbl_MRP_List_OPEX.TotalCost, dbo.tbl_POCreation_Tmp.TaxGroup, dbo.tbl_POCreation_Tmp.TaxItemGroup, dbo.tbl_POCreation_Tmp.POQty, dbo.tbl_POCreation_Tmp.POCost, dbo.tbl_POCreation_Tmp.POTotalCost, dbo.tbl_POCreation_Tmp.POUOM, dbo.tbl_MRP_List_OPEX.UOM FROM   dbo.tbl_POCreation_Tmp INNER JOIN dbo.tbl_MRP_List_OPEX ON dbo.tbl_POCreation_Tmp.ItemPK = dbo.tbl_MRP_List_OPEX.PK WHERE UserKey = '" + creatorkey + "' AND ItemIdentifier = '2'";

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
                    dtRow["MOPNumber"] = row["MOPNumber"].ToString();
                    dtRow["ItemPK"] = row["ItemPK"].ToString();
                    dtRow["TableIdentifier"] = row["ItemIdentifier"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["CapexCIP"] = "";
                    string desc = row["DescriptionAddl"].ToString();
                    if (string.IsNullOrEmpty(desc))
                        dtRow["Description"] = row["Description"].ToString();
                    else
                        dtRow["Description"] = row["Description"].ToString() + " (" + desc + ")";

                    dtRow["RequestedQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");

                    string pouom = row["POUOM"].ToString();
                    if (!string.IsNullOrEmpty(pouom))
                        dtRow["POUOM"] = row["POUOM"].ToString();
                    else
                        dtRow["POUOM"] = row["UOM"].ToString();

                    string poqty = row["POQty"].ToString();
                    string pocost = row["POCost"].ToString();
                    string pototalcost = row["POTotalCost"].ToString();


                    if (Convert.ToDouble(poqty) == 0 && Convert.ToDouble(pocost) == 0 && Convert.ToDouble(pototalcost) == 0)
                    {
                        dtRow["POQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                        dtRow["POCost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                        dtRow["TotalPOCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    }
                    else
                    {
                        dtRow["POQty"] = Convert.ToDouble(poqty).ToString("N");
                        dtRow["POCost"] = Convert.ToDouble(pocost).ToString("N");
                        dtRow["TotalPOCost"] = Convert.ToDouble(pototalcost).ToString("N");
                    }

                    dtRow["TaxGroup"] = row["TaxGroup"].ToString();
                    dtRow["TaxItemGroup"] = row["TaxItemGroup"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();

            //CAPEX
            qry = "SELECT dbo.tbl_POCreation_Tmp.PK, dbo.tbl_POCreation_Tmp.MOPNumber, dbo.tbl_POCreation_Tmp.ItemPK, dbo.tbl_POCreation_Tmp.ItemIdentifier, dbo.tbl_POCreation_Tmp.TaxGroup, dbo.tbl_POCreation_Tmp.TaxItemGroup, dbo.tbl_POCreation_Tmp.POQty, dbo.tbl_POCreation_Tmp.POCost, dbo.tbl_POCreation_Tmp.POTotalCost, dbo.tbl_POCreation_Tmp.POUOM, dbo.tbl_MRP_List_CAPEX.Description, dbo.tbl_MRP_List_CAPEX.UOM, dbo.tbl_MRP_List_CAPEX.Cost, dbo.tbl_MRP_List_CAPEX.Qty,  dbo.tbl_MRP_List_CAPEX.TotalCost, dbo.tbl_MRP_List_CAPEX.CIPSIPNumber FROM dbo.tbl_POCreation_Tmp INNER JOIN dbo.tbl_MRP_List_CAPEX ON dbo.tbl_POCreation_Tmp.ItemPK = dbo.tbl_MRP_List_CAPEX.PK WHERE UserKey = '" + creatorkey + "' AND ItemIdentifier = '4'";

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
                    dtRow["MOPNumber"] = row["MOPNumber"].ToString();
                    dtRow["ItemPK"] = row["ItemPK"].ToString();
                    dtRow["TableIdentifier"] = row["ItemIdentifier"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    dtRow["CapexCIP"] = row["CIPSIPNumber"].ToString();
                    string desc = row["DescriptionAddl"].ToString();
                    if (string.IsNullOrEmpty(desc))
                        dtRow["Description"] = row["Description"].ToString();
                    else
                        dtRow["Description"] = row["Description"].ToString() + " (" + desc + ")";

                    dtRow["RequestedQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");

                    string pouom = row["POUOM"].ToString();
                    if (!string.IsNullOrEmpty(pouom))
                        dtRow["POUOM"] = row["POUOM"].ToString();
                    else
                        dtRow["POUOM"] = row["UOM"].ToString();

                    string poqty = row["POQty"].ToString();
                    string pocost = row["POCost"].ToString();
                    string pototalcost = row["POTotalCost"].ToString();


                    if (Convert.ToDouble(poqty) == 0 && Convert.ToDouble(pocost) == 0 && Convert.ToDouble(pototalcost) == 0)
                    {
                        dtRow["POQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                        dtRow["POCost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                        dtRow["TotalPOCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    }
                    else
                    {
                        dtRow["POQty"] = Convert.ToDouble(poqty).ToString("N");
                        dtRow["POCost"] = Convert.ToDouble(pocost).ToString("N");
                        dtRow["TotalPOCost"] = Convert.ToDouble(pototalcost).ToString("N");
                    }

                    dtRow["TaxGroup"] = row["TaxGroup"].ToString();
                    dtRow["TaxItemGroup"] = row["TaxItemGroup"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();

            cn.Close();

            return dtTable;
        }


        public static DataTable TaxGroupTable()
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
                dtTable.Columns.Add("TAXGROUP", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXTaxGroup]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["TAXGROUP"] = row["TAXGROUP"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable TaxItemGroupTable()
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
                dtTable.Columns.Add("TAXITEMGROUP", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXTaxItemGroup]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["TAXITEMGROUP"] = row["TAXITEMGROUP"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
        public static DataTable PO_MOP_Ref(string ponumber)
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
                dtTable.Columns.Add("MOPNumber", typeof(string));
            }

            string qry = "SELECT DISTINCT [MOPNumber] FROM [hijo_portal].[dbo].[tbl_POCreation_Details] WHERE [PONumber] = '" + ponumber + "'";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["MOPNumber"] = row["MOPNumber"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable PO_AddEdit_Table(string ponumber)
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
                dtTable.Columns.Add("ItemPK", typeof(string));
                dtTable.Columns.Add("Identifier", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("CapexCIP", typeof(string));
                dtTable.Columns.Add("RequestedQty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("POUOM", typeof(string));
                dtTable.Columns.Add("POQty", typeof(string));
                dtTable.Columns.Add("POCost", typeof(string));
                dtTable.Columns.Add("TotalPOCost", typeof(string));
                dtTable.Columns.Add("TaxGroup", typeof(string));
                dtTable.Columns.Add("TaxItemGroup", typeof(string));
                dtTable.Columns.Add("ProdCat", typeof(string));
            }

            string qry = "SELECT dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.ItemDescriptionAddl, dbo.tbl_MRP_List_DirectMaterials.Cost AS DMCost, dbo.tbl_MRP_List_DirectMaterials.Qty AS DMQty, dbo.tbl_MRP_List_DirectMaterials.TotalCost AS DMTotal, dbo.tbl_POCreation_Details.* FROM dbo.tbl_MRP_List_DirectMaterials INNER JOIN dbo.tbl_POCreation_Details ON dbo.tbl_MRP_List_DirectMaterials.PK = dbo.tbl_POCreation_Details.ItemPK WHERE (dbo.tbl_POCreation_Details.Identifier = '1') AND (dbo.tbl_POCreation_Details.PONumber = '" + ponumber + "')";

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
                    dtRow["ItemPK"] = row["ItemPK"].ToString();
                    dtRow["Identifier"] = row["Identifier"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();

                    string desc = row["ItemDescriptionAddl"].ToString();
                    if (!string.IsNullOrEmpty(desc))
                        dtRow["Description"] = row["ItemDescription"].ToString() + "(" + desc + ")";
                    else
                        dtRow["Description"] = row["ItemDescription"].ToString();

                    dtRow["ProdCat"] = "";
                    dtRow["CapexCIP"] = "";
                    dtRow["RequestedQty"] = Convert.ToDouble(row["DMQty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["DMCost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["DMTotal"].ToString()).ToString("N");
                    dtRow["POUOM"] = row["POUOM"].ToString();
                    dtRow["POQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["POCost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalPOCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtRow["TaxGroup"] = row["TaxGroup"].ToString();
                    dtRow["TaxItemGroup"] = row["TaxItemGroup"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();

            qry = "SELECT dbo.tbl_POCreation_Details.*, dbo.tbl_MRP_List_OPEX.Description, dbo.tbl_MRP_List_OPEX.DescriptionAddl, dbo.tbl_MRP_List_OPEX.Cost AS OPCost, dbo.tbl_MRP_List_OPEX.Qty AS OPQty, dbo.tbl_MRP_List_OPEX.TotalCost AS OPTotal FROM   dbo.tbl_POCreation_Details INNER JOIN dbo.tbl_MRP_List_OPEX ON dbo.tbl_POCreation_Details.ItemPK = dbo.tbl_MRP_List_OPEX.PK WHERE (dbo.tbl_POCreation_Details.Identifier = '2') AND (dbo.tbl_POCreation_Details.PONumber = '" + ponumber + "')";


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
                    dtRow["ItemPK"] = row["ItemPK"].ToString();
                    dtRow["Identifier"] = row["Identifier"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();

                    string desc = row["DescriptionAddl"].ToString();
                    if (!string.IsNullOrEmpty(desc))
                        dtRow["Description"] = row["Description"].ToString() + "(" + desc + ")";
                    else
                        dtRow["Description"] = row["Description"].ToString();

                    dtRow["ProdCat"] = "";
                    dtRow["CapexCIP"] = "";
                    dtRow["RequestedQty"] = Convert.ToDouble(row["OPQty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["OPCost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["OPTotal"].ToString()).ToString("N");
                    dtRow["POUOM"] = row["POUOM"].ToString();
                    dtRow["POQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["POCost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalPOCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtRow["TaxGroup"] = row["TaxGroup"].ToString();
                    dtRow["TaxItemGroup"] = row["TaxItemGroup"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();


            //CAPEX
            qry = "SELECT dbo.tbl_POCreation_Details.PK, dbo.tbl_POCreation_Details.PONumber, dbo.tbl_POCreation_Details.MOPNumber, dbo.tbl_POCreation_Details.ItemPK, dbo.tbl_POCreation_Details.Identifier, dbo.tbl_POCreation_Details.ItemCode, dbo.tbl_POCreation_Details.TaxGroup, dbo.tbl_POCreation_Details.TaxItemGroup, dbo.tbl_POCreation_Details.POUOM, dbo.tbl_POCreation_Details.Qty, dbo.tbl_POCreation_Details.Cost, dbo.tbl_POCreation_Details.TotalCost, dbo.tbl_MRP_List_CAPEX.Description, dbo.tbl_MRP_List_CAPEX.Cost AS CACost, dbo.tbl_MRP_List_CAPEX.Qty AS CAQty, dbo.tbl_MRP_List_CAPEX.TotalCost AS CATotal, dbo.tbl_MRP_List_CAPEX.CIPSIPNumber, dbo.tbl_MRP_List_CAPEX.ProdCat FROM dbo.tbl_POCreation_Details INNER JOIN dbo.tbl_MRP_List_CAPEX ON dbo.tbl_POCreation_Details.ItemPK = dbo.tbl_MRP_List_CAPEX.PK WHERE(dbo.tbl_POCreation_Details.Identifier = '4') AND (dbo.tbl_POCreation_Details.PONumber = '" + ponumber + "')";


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
                    dtRow["ItemPK"] = row["ItemPK"].ToString();
                    dtRow["Identifier"] = row["Identifier"].ToString();
                    dtRow["ItemCode"] = "";
                    dtRow["ProdCat"] = row["ProdCat"].ToString();
                    dtRow["CapexCIP"] = row["CIPSIPNumber"].ToString();
                    dtRow["Description"] = row["Description"].ToString();
                    dtRow["RequestedQty"] = Convert.ToDouble(row["CAQty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["CACost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["CATotal"].ToString()).ToString("N");
                    dtRow["POUOM"] = row["POUOM"].ToString();
                    dtRow["POQty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["POCost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalPOCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtRow["TaxGroup"] = row["TaxGroup"].ToString();
                    dtRow["TaxItemGroup"] = row["TaxItemGroup"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();



            cn.Close();

            return dtTable;
        }

        public static DataTable PO_Uploading_Table()
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
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("HeaderPath", typeof(string));
                dtTable.Columns.Add("LinePath", typeof(string));
                dtTable.Columns.Add("Domain", typeof(string));
                dtTable.Columns.Add("Name", typeof(string));
                dtTable.Columns.Add("PW", typeof(string));
            }

            string qry = "SELECT * FROM [hijo_portal].[dbo].[tbl_AXPOUploadingPath]";

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
                    dtRow["EntityCode"] = row["Entity"].ToString();
                    dtRow["Entity"] = row["Entity Name"].ToString();
                    dtRow["HeaderPath"] = row["POHeaderPath"].ToString();
                    dtRow["LinePath"] = row["POLinePath"].ToString();
                    dtRow["Domain"] = row["Domain"].ToString();
                    dtRow["Name"] = row["UserName"].ToString();
                    dtRow["PW"] = row["Password"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
        public static DataTable PO_Info_Table()
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
                dtTable.Columns.Add("Code", typeof(string));
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("Prefix", typeof(string));
                dtTable.Columns.Add("BeforeSeries", typeof(string));
                dtTable.Columns.Add("MaxNumber", typeof(string));
                dtTable.Columns.Add("LastNumber", typeof(string));
            }

            string qry = "SELECT TOP (1000) dbo.tbl_PONumber.*, dbo.vw_AXEntityTable.NAME AS EntityName FROM dbo.tbl_PONumber INNER JOIN dbo.vw_AXEntityTable ON dbo.tbl_PONumber.EntityCode = dbo.vw_AXEntityTable.ID";

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
                    dtRow["Code"] = row["EntityCode"].ToString();
                    dtRow["Entity"] = row["EntityName"].ToString();
                    dtRow["Prefix"] = row["Prefix"].ToString();
                    dtRow["BeforeSeries"] = row["BeforeSeries"].ToString();
                    dtRow["MaxNumber"] = row["MaxNumber"].ToString();
                    dtRow["LastNumber"] = row["LastNumber"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
        public static DataTable PO_EntityCode_Table()
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
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            //string qry = "SELECT [ID] ,[NAME] FROM [hijo_portal].[dbo].[vw_AXEntityTable] WHERE [ID] NOT IN(SELECT [EntityCode] FROM [hijo_portal].[dbo].[tbl_PONumber])";
            string qry = "SELECT [ID] ,[NAME] FROM [hijo_portal].[dbo].[vw_AXEntityTable]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["ID"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static string POHeaderColumnName()
        {
            string sHeader = "";
            string sWebRoot = HttpContext.Current.Server.MapPath("~");
            string sFilePath = sWebRoot + @"config\POHeaderColNameFromMOP.txt";

            try
            {
                if (File.Exists(sFilePath))
                {
                    using (StreamReader sr = new StreamReader(sFilePath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            sHeader = sr.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sHeader = "";
            }

            return sHeader;
        }

        public static string POHeaderColumnNameDefault()
        {
            string sHeader = "";
            int iRecCnt = 0;
            //string sWebRoot = HttpContext.Current.Server.MapPath("~");
            //string sFilePath = sWebRoot + @"config\POHeaderColNameDefault.txt";

            //try
            //{
            //    if (File.Exists(sFilePath))
            //    {
            //        using (StreamReader sr = new StreamReader(sFilePath))
            //        {
            //            while (sr.Peek() >= 0)
            //            {
            //                sHeader = sr.ReadLine();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    sHeader = "";
            //}

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            conn.Open();
            string qry = "SELECT tbl_System_POHeaderDefault.* FROM tbl_System_POHeaderDefault ORDER BY PK";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sHeader = sHeader + row["ColumnName"].ToString().Trim();
                    iRecCnt = iRecCnt + 1;
                    if (iRecCnt < dt.Rows.Count)
                    {
                        sHeader = sHeader + "|";
                    }
                }
            }
            dt.Clear();
            conn.Close();

            return sHeader;
        }

        public static string POHeaderDefaultValue()
        {
            string sHeaderDefault = "";
            int iRecCnt = 0;
            //string sWebRoot = HttpContext.Current.Server.MapPath("~");
            //string sFilePath = sWebRoot + @"config\POHeaderDefaultValue.txt";

            //try
            //{
            //    if (File.Exists(sFilePath))
            //    {
            //        using (StreamReader sr = new StreamReader(sFilePath))
            //        {
            //            while (sr.Peek() >= 0)
            //            {
            //                sHeaderDefault = sr.ReadLine();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    sHeaderDefault = "";
            //}

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            conn.Open();
            string qry = "SELECT tbl_System_POHeaderDefault.* FROM tbl_System_POHeaderDefault ORDER BY PK";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sHeaderDefault = sHeaderDefault + row["ColumnValue"].ToString().Trim();
                    iRecCnt = iRecCnt + 1;
                    if (iRecCnt < dt.Rows.Count)
                    {
                        sHeaderDefault = sHeaderDefault + "|";
                    }
                }
            }
            dt.Clear();
            conn.Close();

            return sHeaderDefault;
        }

        public static string POLineColumnName()
        {
            string sLine = "";
            string sWebRoot = HttpContext.Current.Server.MapPath("~");
            string sFilePath = sWebRoot + @"config\POLineColNameFromMOP.txt";

            try
            {
                if (File.Exists(sFilePath))
                {
                    using (StreamReader sr = new StreamReader(sFilePath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            sLine = sr.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sLine = "";
            }

            return sLine;
        }

        public static string POLineColumnNameDefault()
        {
            string sLineDefault = "";
            int iRecCnt = 0;
            //string sWebRoot = HttpContext.Current.Server.MapPath("~");
            //string sFilePath = sWebRoot + @"config\POLineColNameDefault.txt";

            //try
            //{
            //    if (File.Exists(sFilePath))
            //    {
            //        using (StreamReader sr = new StreamReader(sFilePath))
            //        {
            //            while (sr.Peek() >= 0)
            //            {
            //                sHeader = sr.ReadLine();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    sHeader = "";
            //}

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            conn.Open();
            string qry = "SELECT tbl_System_POLinesDefault.* FROM tbl_System_POLinesDefault ORDER BY PK";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sLineDefault = sLineDefault + row["ColumnName"].ToString().Trim();
                    iRecCnt = iRecCnt + 1;
                    if (iRecCnt < dt.Rows.Count)
                    {
                        sLineDefault = sLineDefault + "|";
                    }
                }
            }
            dt.Clear();
            conn.Close();

            return sLineDefault;
        }

        public static string POLineDefaultValue()
        {
            string sLineDefault = "";
            int iRecCnt = 0;
            //string sWebRoot = HttpContext.Current.Server.MapPath("~");
            //string sFilePath = sWebRoot + @"config\POLineDefaultValue.txt";

            //try
            //{
            //    if (File.Exists(sFilePath))
            //    {
            //        using (StreamReader sr = new StreamReader(sFilePath))
            //        {
            //            while (sr.Peek() >= 0)
            //            {
            //                sHeaderDefault = sr.ReadLine();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    sHeaderDefault = "";
            //}

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            conn.Open();
            string qry = "SELECT tbl_System_POLinesDefault.* FROM tbl_System_POLinesDefault ORDER BY PK";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sLineDefault = sLineDefault + row["ColumnValue"].ToString().Trim();
                    iRecCnt = iRecCnt + 1;
                    if (iRecCnt < dt.Rows.Count)
                    {
                        sLineDefault = sLineDefault + "|";
                    }
                }
            }
            dt.Clear();
            conn.Close();

            return sLineDefault;
        }

        public static void SubmitToAX(string poNum, ASPxPopupControl popNotify, ASPxLabel lblNotify, ModalPopupExtender modExtender)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dt1 = new DataTable();
            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            DataTable dt2 = new DataTable();
            SqlCommand cmd2 = null;
            SqlDataAdapter adp2;

            string qry = "", sFiLeName = "", sFileNameD = "";
            string sFile = "", sFileD = "", sFileDest = "", sFileDDest = "";

            string sHeader = "", sDetails = "", sPriceUnit = "", sItemCode = "", sProcCatID = "", sFixedAssetID = "", sAssetBookID = "";

            string sServerDir = HttpContext.Current.Server.MapPath("~");
            string sDir = sServerDir + @"\po_file";
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }

            conn.Open();
            qry = "SELECT dbo.tbl_POCreation.PK, dbo.tbl_POCreation.PONumber, dbo.tbl_POCreation.MRPNumber, dbo.tbl_POCreation.DateCreated, dbo.tbl_POCreation.CreatorKey, dbo.tbl_POCreation.ExpectedDate, dbo.tbl_POCreation.VendorCode, dbo.tbl_POCreation.PaymentTerms, dbo.tbl_POCreation.CurrencyCode,dbo.tbl_POCreation.InventSite, dbo.tbl_POCreation.InventSiteWarehouse, dbo.tbl_POCreation.InventSiteWarehouseLocation, dbo.vw_AXVendTable.NAME, dbo.vw_AXVendTable.VENDGROUP, dbo.vw_AXVendTable.INCLTAX, dbo.vw_AXVendTable.PAYMMODE, dbo.vw_AXVendTable.TAXGROUP, dbo.tbl_POCreation.EntityCode, dbo.tbl_POCreation.BUSSUCode, dbo.tbl_POCreation.Remarks FROM  dbo.tbl_POCreation LEFT OUTER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM WHERE(dbo.tbl_POCreation.PONumber = '" + poNum + "')";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string sPORemarks = row["MRPNumber"].ToString();
                    string sIncTax = "";
                    if (Convert.ToInt32(row["INCLTAX"]) == 0)
                    {
                        sIncTax = "No";
                    }
                    else
                    {
                        sIncTax = "Yes";
                    }

                    string sDefaultDimension = "";
                    //if (row["EntityCode"].ToString().Trim() == "0000")
                    //{
                    //    sDefaultDimension = row["BUSSUCode"].ToString() + "__";
                    //}
                    //else if (row["EntityCode"].ToString().Trim() == "0303")
                    //{
                    //    sDefaultDimension = "_" + row["BUSSUCode"].ToString() + "_";
                    //}
                    //else
                    //{
                    //    sDefaultDimension = "";
                    //}

                    if (row["EntityCode"].ToString().Trim() == "0000")
                    {
                        sDefaultDimension = row["BUSSUCode"].ToString() + "";
                    }
                    else if (row["EntityCode"].ToString().Trim() == "0303")
                    {
                        sDefaultDimension = "" + row["BUSSUCode"].ToString() + "";
                    }
                    else
                    {
                        sDefaultDimension = "";
                    }

                    sFiLeName = row["EntityCode"].ToString() + "_" + row["PONumber"].ToString() + "_H.txt";
                    sFile = sDir + @"\" + sFiLeName;

                    if (File.Exists(sFile))
                    {
                        File.Delete(sFile);
                    }
                    FileStream fs = File.Create(sFile);
                    fs.Dispose();

                    if (File.Exists(sFile))
                    {
                        using (StreamWriter w = File.AppendText(sFile))
                        {
                            w.WriteLine(POClass.POHeaderColumnName() + POClass.POHeaderColumnNameDefault());
                            w.Close();

                        }
                        using (StreamWriter w = File.AppendText(sFile))
                        {
                            sHeader = row["PONumber"].ToString() + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|" + row["CurrencyCode"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row["NAME"].ToString() + "|" + row["NAME"].ToString() + "|" + row["PaymentTerms"].ToString() + "|" + sIncTax + "|" + row["PAYMMODE"].ToString() + "|" + row["InventSite"].ToString() + "|" + row["Remarks"].ToString() + "|" + row["VENDGROUP"].ToString() + "|" + row["TAXGROUP"].ToString() + "|" + sDefaultDimension.ToString() + "|" + sPORemarks + "|";

                            w.WriteLine(sHeader + POClass.POHeaderDefaultValue());
                            w.Close();
                        }
                    }

                    int iLineNumber = 0;
                    string sDefaultDimensionLine = "";
                    //qry = "SELECT ItemCode, TaxGroup, TaxItemGroup, Qty, Cost, TotalCost, POUOM, (CASE Identifier WHEN 1 THEN (SELECT OprUnit FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE (SELECT OprUnit FROM  dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) END) AS OprUnit, (CASE Identifier WHEN 1 THEN (SELECT ItemDescription + (CASE LTRIM(RTRIM(ItemDescriptionAddl)) WHEN '' THEN '' ELSE ' (' + ItemDescriptionAddl + ')' END) AS ItemDesc FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE (SELECT Description + (CASE LTRIM(RTRIM(DescriptionAddl)) WHEN '' THEN '' ELSE ' (' + DescriptionAddl + ')' END) AS ItemDesc FROM dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) END) AS ItemDesc FROM dbo.tbl_POCreation_Details WHERE(PONumber = '" + poNum + "')";
                    qry = "SELECT ItemCode, TaxGroup, TaxItemGroup, Qty, Cost, TotalCost, POUOM, (CASE Identifier WHEN 1 THEN (SELECT OprUnit FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE(CASE Identifier WHEN 2 THEN (SELECT OprUnit FROM  dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) ELSE (SELECT OprUnit FROM  dbo.tbl_MRP_List_CAPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 4)) END) END) AS OprUnit, (CASE Identifier WHEN 1 THEN (SELECT ItemDescription + (CASE LTRIM(RTRIM(ItemDescriptionAddl)) WHEN '' THEN '' ELSE ' (' + ItemDescriptionAddl + ')' END) AS ItemDesc FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE(CASE Identifier WHEN 2 THEN (SELECT Description + (CASE LTRIM(RTRIM(DescriptionAddl)) WHEN '' THEN '' ELSE ' (' + DescriptionAddl + ')' END) AS ItemDesc FROM  dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) ELSE (SELECT Description AS ItemDesc FROM  dbo.tbl_MRP_List_CAPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 4)) END) END) AS ItemDesc, ItemPK, Identifier, ISNULL((SELECT CIPSIPNumber FROM dbo.tbl_MRP_List_CAPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK)),'') AS FixedAssetID FROM dbo.tbl_POCreation_Details WHERE(PONumber = '" + poNum + "')";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        sFileNameD = row["EntityCode"].ToString() + "_" + row["PONumber"].ToString() + "_L.txt";
                        sFileD = sDir + @"\" + sFileNameD;

                        if (File.Exists(sFileD))
                        {
                            File.Delete(sFileD);
                        }
                        FileStream fsd = File.Create(sFileD);
                        fsd.Dispose();

                        if (File.Exists(sFileD))
                        {
                            using (StreamWriter w = File.AppendText(sFileD))
                            {
                                w.WriteLine(POClass.POLineColumnName() + POClass.POLineColumnNameDefault());
                                w.Close();
                            }
                        }

                        foreach (DataRow row1 in dt1.Rows)
                        {
                            iLineNumber = iLineNumber + 1;
                            if (File.Exists(sFileD))
                            {

                                //if (row["EntityCode"].ToString().Trim() == "0000")
                                //{
                                //    sDefaultDimensionLine = row["BUSSUCode"].ToString() + "__";
                                //}
                                //else if (row["EntityCode"].ToString().Trim() == "0303")
                                //{
                                //    sDefaultDimensionLine = "_" + row["BUSSUCode"].ToString() + "_";
                                //}
                                //else if (row["EntityCode"].ToString().Trim() == "0101")
                                //{
                                //    sDefaultDimensionLine = "__" + row1["OprUnit"].ToString();
                                //}
                                //else
                                //{
                                //    sDefaultDimensionLine = "";
                                //}

                                if (row["EntityCode"].ToString().Trim() == "0000")
                                {
                                    sDefaultDimensionLine = row["BUSSUCode"].ToString() + "";
                                }
                                else if (row["EntityCode"].ToString().Trim() == "0303")
                                {
                                    sDefaultDimensionLine = "" + row["BUSSUCode"].ToString() + "";
                                }
                                else if (row["EntityCode"].ToString().Trim() == "0101")
                                {
                                    sDefaultDimensionLine = "" + row1["OprUnit"].ToString();
                                }
                                else
                                {
                                    sDefaultDimensionLine = "";
                                }


                                sPriceUnit = "1";
                                //if (row1["ItemCode"].ToString() != "")
                                //{
                                //    sFixedAsset = "No";
                                //}
                                //else
                                //{
                                //    sFixedAsset = "Yes";
                                //}

                                if (row["EntityCode"].ToString().Trim() == "0303")
                                {
                                    sItemCode = "";
                                }
                                else
                                {
                                    sItemCode = row1["ItemCode"].ToString();
                                }

                                if (sItemCode == "")
                                {
                                    switch (Convert.ToInt32(row1["Identifier"]))
                                    {
                                        case 1:
                                            {
                                                sProcCatID = "";
                                                sFixedAssetID = "";
                                                sAssetBookID = "";
                                                break;
                                            }
                                        case 2:
                                            {
                                                sFixedAssetID = "";
                                                sAssetBookID = "";
                                                qry = "SELECT ISNULL((SELECT recid FROM  dbo.vw_AXProdCategory WHERE(NAME = dbo.tbl_MRP_List_OPEX.ProcCat) AND(dataareaid = dbo.tbl_MRP_List.EntityCode) AND(LedgerType = 'Expense')), '') AS ProcCatID FROM dbo.tbl_MRP_List_OPEX LEFT OUTER JOIN dbo.tbl_MRP_List ON dbo.tbl_MRP_List_OPEX.HeaderDocNum = dbo.tbl_MRP_List.DocNumber WHERE(dbo.tbl_MRP_List_OPEX.PK = " + row1["ItemPK"] + ")";
                                                cmd2 = new SqlCommand(qry);
                                                cmd2.Connection = conn;
                                                adp2 = new SqlDataAdapter(cmd2);
                                                adp2.Fill(dt2);
                                                if (dt2.Rows.Count > 0)
                                                {
                                                    foreach (DataRow row2 in dt2.Rows)
                                                    {
                                                        if (Convert.ToDouble(row2["ProcCatID"]) == 0)
                                                        {
                                                            sProcCatID = "";
                                                        }
                                                        else
                                                        {
                                                            sProcCatID = row2["ProcCatID"].ToString();
                                                        }
                                                    }
                                                }
                                                dt2.Clear();
                                                break;
                                            }
                                        case 4:
                                            {
                                                sFixedAssetID = row1["FixedAssetID"].ToString();
                                                sAssetBookID = "DEP";
                                                qry = "SELECT ISNULL((SELECT recid FROM  dbo.vw_AXProdCategory WHERE(NAME = dbo.tbl_MRP_List_CAPEX.ProdCat) AND(dataareaid = dbo.tbl_MRP_List.EntityCode) AND(LedgerType = 'FixedAsset')), '') AS ProcCatID FROM dbo.tbl_MRP_List_CAPEX LEFT OUTER JOIN dbo.tbl_MRP_List ON dbo.tbl_MRP_List_CAPEX.HeaderDocNum = dbo.tbl_MRP_List.DocNumber WHERE(dbo.tbl_MRP_List_CAPEX.PK = " + row1["ItemPK"] + ")";
                                                cmd2 = new SqlCommand(qry);
                                                cmd2.Connection = conn;
                                                adp2 = new SqlDataAdapter(cmd2);
                                                adp2.Fill(dt2);
                                                if (dt2.Rows.Count > 0)
                                                {
                                                    foreach (DataRow row2 in dt2.Rows)
                                                    {
                                                        if (Convert.ToDouble(row2["ProcCatID"]) == 0)
                                                        {
                                                            sProcCatID = "";
                                                        }
                                                        else
                                                        {
                                                            sProcCatID = row2["ProcCatID"].ToString();
                                                        }
                                                    }
                                                }
                                                dt2.Clear();
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    sProcCatID = "";
                                }


                                using (StreamWriter w = File.AppendText(sFileD))
                                {
                                    sDetails = row["PONumber"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row1["ItemDesc"].ToString() + "|" + row1["ItemDesc"].ToString() + "|" + row["VENDGROUP"].ToString() + "|" + row["InventSite"].ToString() + "|" + row["InventSiteWarehouse"].ToString() + "|" + row["InventSiteWarehouseLocation"].ToString() + "|" + row["CurrencyCode"].ToString() + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|" + sItemCode + "|" + Convert.ToDouble(row1["Qty"]).ToString("#0.0000") + "|" + row1["POUOM"].ToString() + "|" + Convert.ToDouble(row1["Cost"]).ToString("#0.0000") + "|" + Convert.ToDouble(row1["TotalCost"]).ToString("#0.0000") + "|" + iLineNumber.ToString() + "|" + sPriceUnit + "|" + row1["TaxGroup"].ToString() + "|" + row1["TaxItemGroup"].ToString() + "|" + sDefaultDimensionLine.ToString() + "|" + sProcCatID + "|" + sFixedAssetID + "|" + sAssetBookID + "|";

                                    w.WriteLine(sDetails + POClass.POLineDefaultValue());
                                    w.Close();
                                }
                            }
                        }
                    }
                    dt1.Clear();

                    string sDomain = "", sUsername = "", sPassword = "";

                    qry = "SELECT tbl_AXPOUploadingPath.* FROM tbl_AXPOUploadingPath WHERE ([Entity] = '" + row["EntityCode"].ToString() + "')";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            sFileDest = row1["POHeaderPath"].ToString() + @"\" + sFiLeName;
                            sFileDDest = row1["POLinePath"].ToString() + @"\" + sFileNameD;
                            sDomain = row1["Domain"].ToString();
                            sUsername = row1["UserName"].ToString();
                            if (row1["Password"].ToString().Trim() != "")
                            {
                                sPassword = EncryptionClass.Decrypt(row1["Password"].ToString());
                            }
                            else
                            {
                                sPassword = row1["Password"].ToString();
                            }

                            //sDomain = "hijo"; sUsername = "hijoportal_client"; sPassword = "hPortal@2019";

                            try
                            {
                                using (var impersonation = new ImpersonatedUser(sUsername, sDomain, sPassword))
                                {
                                    try
                                    {
                                        if (sFileDest.Trim() != "")
                                        {
                                            if (File.Exists(sFile) == true)
                                            {
                                                File.Copy(sFile, sFileDest, true);
                                            }
                                        }
                                        if (sFileDDest.Trim() != "")
                                        {
                                            if (File.Exists(sFileD) == true)
                                            {
                                                File.Copy(sFileD, sFileDDest, true);
                                            }
                                        }
                                        try
                                        {
                                            qry = "UPDATE tbl_POCreation SET [POStatus] = 1 WHERE ([PONumber] = @PONumber)";
                                            cmd = new SqlCommand(qry, conn);
                                            cmd.Parameters.AddWithValue("@PONumber", poNum);
                                            cmd.CommandType = CommandType.Text;
                                            cmd.ExecuteNonQuery();

                                            modExtender.Hide();

                                            lblNotify.ForeColor = System.Drawing.Color.Black;
                                            lblNotify.Text = "Sucessfully submitted to AX";
                                            popNotify.HeaderText = "Info";
                                            popNotify.ShowOnPageLoad = true;

                                        }
                                        catch (SqlException exSQL)
                                        {
                                            modExtender.Hide();
                                            lblNotify.ForeColor = System.Drawing.Color.Red;
                                            lblNotify.Text = exSQL.ToString();
                                            popNotify.HeaderText = "Error";
                                            popNotify.ShowOnPageLoad = true;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        modExtender.Hide();
                                        lblNotify.ForeColor = System.Drawing.Color.Red;
                                        lblNotify.Text = ex.ToString();
                                        popNotify.HeaderText = "Error";
                                        popNotify.ShowOnPageLoad = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                modExtender.Hide();
                                lblNotify.ForeColor = System.Drawing.Color.Red;
                                lblNotify.Text = ex.ToString();
                                popNotify.HeaderText = "Error";
                                popNotify.ShowOnPageLoad = true;
                            }
                        }
                    }
                    else
                    {
                        modExtender.Hide();
                        lblNotify.ForeColor = System.Drawing.Color.Red;
                        lblNotify.Text = "Please Call Administrator." + Environment.NewLine + Environment.NewLine + "No PO Destination Path Setup.";
                        popNotify.HeaderText = "Error";
                        popNotify.ShowOnPageLoad = true;
                    }
                    dt1.Clear();
                }
            }
            dt.Clear();

            conn.Close();
        }
    }
}