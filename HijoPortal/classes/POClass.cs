using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class POClass
    {

        public static DataTable PO_Created_List()
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
                dtTable.Columns.Add("PONumber", typeof(string));
                dtTable.Columns.Add("MOPNumber", typeof(string));
                dtTable.Columns.Add("VendorCode", typeof(string));
                dtTable.Columns.Add("VendorName", typeof(string));
                dtTable.Columns.Add("DateCreated", typeof(string));
                dtTable.Columns.Add("CreatorKey", typeof(string));
                dtTable.Columns.Add("Creator", typeof(string));
                dtTable.Columns.Add("ExpectedDate", typeof(string));
                dtTable.Columns.Add("TotalAmount", typeof(string));
                dtTable.Columns.Add("Status", typeof(string));
            }

            string qry = "SELECT dbo.tbl_POCreation.PK, dbo.tbl_POCreation.PONumber, dbo.tbl_POCreation.MRPNumber, " +
                         " dbo.tbl_POCreation.VendorCode, dbo.vw_AXVendTable.NAME AS VendorName, " +
                         " dbo.tbl_POCreation.DateCreated, dbo.tbl_POCreation.CreatorKey, " +
                         " dbo.tbl_Users.Firstname, dbo.tbl_Users.Lastname, dbo.tbl_POCreation.ExpectedDate " +
                         " FROM dbo.tbl_POCreation LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_POCreation.CreatorKey = dbo.tbl_Users.PK LEFT OUTER JOIN " +
                         " dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM " +
                         " ORDER BY dbo.tbl_POCreation.PONumber DESC";
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
                    dtRow["VendorCode"] = row["VendorCode"].ToString();
                    dtRow["VendorName"] = row["VendorName"].ToString();
                    dtRow["VendorCode"] = row["VendorCode"].ToString();
                    dtRow["DateCreated"] = Convert.ToDateTime(row["DateCreated"]).ToString("MM/dd/yyyy");
                    dtRow["CreatorKey"] = row["CreatorKey"].ToString();
                    dtRow["Creator"] = EncryptionClass.Decrypt(row["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    dtRow["ExpectedDate"] = Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy");
                    dtRow["TotalAmount"] = "0.00";
                    dtRow["Status"] = "Created";
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
            }

            string month_string = "'" + month.ToString() + "'";
            string year_string = "'" + year + "'";
            if (month == 0)
            {
                month_string = "MRPMonth";
                year_string = "MRPYear";
            }

            string qry = "SELECT [PK], [DocNumber] FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE MRPMonth = " + month_string + " AND MRPYear = " + year_string + " AND StatusKey = '4' ORDER BY MRPMonth, MRPYear ASC";

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
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable POSelecetedItemTable(string docnumber, int month, string year, string groupID)
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
                dtTable.Columns.Add("Entity", typeof(string));
                dtTable.Columns.Add("BU", typeof(string));
                dtTable.Columns.Add("ItemCatCode", typeof(string));
                dtTable.Columns.Add("ItemCat", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("ItemDescription", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string month_string = "", year_string = "", doc_string = "";
            if (month == 0)
            {
                month_string = "MRPMonth";
                year_string = "MRPYear";
            }
            else
            {
                month_string = "'" + month + "'";
                year_string = "'" + year + "'";
            }

            if (string.IsNullOrEmpty(docnumber)) doc_string = "DocNumber";
            else doc_string = "'" + docnumber + "'";

            string groupid_string = "";
            if (string.IsNullOrEmpty(groupID) || groupID == "ALL") groupid_string = "dbo.vw_AXInventTable.ITEMGROUPID";
            else groupid_string = "'" + groupID + "'";

            //string qry = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.PK, dbo.tbl_MRP_List_DirectMaterials.TableIdentifier, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List_DirectMaterials.ItemCode,  dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.Qty, dbo.tbl_MRP_List_DirectMaterials.Cost, dbo.tbl_MRP_List_DirectMaterials.TotalCost FROM dbo.tbl_MRP_List INNER JOIN dbo.tbl_MRP_List_DirectMaterials ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_DirectMaterials.HeaderDocNum INNER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID INNER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER INNER JOIN dbo.vw_AXInventTable ON dbo.tbl_MRP_List_DirectMaterials.ItemCode = dbo.vw_AXInventTable.ITEMID WHERE (dbo.tbl_MRP_List.MRPMonth = " + month_string + ") AND (dbo.tbl_MRP_List.MRPYear = " + year_string + ") AND (dbo.tbl_MRP_List.DocNumber = " + doc_string + ") AND (dbo.tbl_MRP_List.StatusKey = '4') AND (dbo.vw_AXInventTable.ITEMGROUPID = " + groupid_string + ")";

            string qry = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.PK, dbo.tbl_MRP_List_DirectMaterials.TableIdentifier, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List_DirectMaterials.ItemCode, dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.ItemDescriptionAddl, dbo.tbl_MRP_List_DirectMaterials.Qty, dbo.tbl_MRP_List_DirectMaterials.Cost, dbo.tbl_MRP_List_DirectMaterials.TotalCost, dbo.vw_AXInventTable.ITEMGROUPID AS ItemCatCode, dbo.vw_AXProdCategory.DESCRIPTION AS ItemCat FROM  dbo.vw_AXProdCategory RIGHT OUTER JOIN dbo.vw_AXInventTable ON dbo.vw_AXProdCategory.NAME = dbo.vw_AXInventTable.ITEMGROUPID RIGHT OUTER JOIN dbo.tbl_MRP_List LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN dbo.tbl_MRP_List_DirectMaterials ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_DirectMaterials.HeaderDocNum ON dbo.vw_AXInventTable.ITEMID = dbo.tbl_MRP_List_DirectMaterials.ItemCode WHERE(dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = " + year_string + ") AND(dbo.tbl_MRP_List.MRPMonth = " + month_string + ") AND(dbo.tbl_MRP_List.DocNumber = " + doc_string + ") AND(dbo.vw_AXInventTable.ITEMGROUPID = " + groupid_string + ")";
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
                    dtRow["TableIdentifier"] = row["TableIdentifier"].ToString();
                    dtRow["DocumentNumber"] = row["DocNumber"].ToString();
                    dtRow["Entity"] = row["Entity"].ToString();
                    dtRow["BU"] = row["BU"].ToString();
                    dtRow["ItemCatCode"] = row["ItemCatCode"].ToString();
                    dtRow["ItemCat"] = row["ItemCat"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    if (row["ItemDescriptionAddl"].ToString().Trim() !="")
                    {
                        dtRow["ItemDescription"] = row["ItemDescription"].ToString() + " (" + row["ItemDescriptionAddl"].ToString() + ")";
                    } else
                    {
                        dtRow["ItemDescription"] = row["ItemDescription"].ToString();
                    }                    
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();

            //qry = "SELECT DISTINCT dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List_OPEX.ItemCode, dbo.tbl_MRP_List_OPEX.Description,dbo.tbl_MRP_List_OPEX.PK, dbo.tbl_MRP_List_OPEX.TableIdentifier, dbo.tbl_MRP_List_OPEX.Cost, dbo.tbl_MRP_List_OPEX.Qty, dbo.tbl_MRP_List_OPEX.TotalCost FROM   dbo.tbl_MRP_List INNER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID INNER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER INNER JOIN dbo.tbl_MRP_List_OPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_OPEX.HeaderDocNum INNER JOIN dbo.vw_AXInventTable ON dbo.tbl_MRP_List_OPEX.ItemCode = dbo.vw_AXInventTable.ITEMID WHERE(dbo.tbl_MRP_List.MRPMonth = " + month_string + ") AND(dbo.tbl_MRP_List.MRPYear = " + year_string + ") AND(dbo.tbl_MRP_List.DocNumber = " + doc_string + ") AND(dbo.tbl_MRP_List.StatusKey = '4') AND (dbo.vw_AXInventTable.ITEMGROUPID = " + groupid_string + ")";

            qry = "SELECT DISTINCT dbo.tbl_MRP_List_OPEX.PK, dbo.tbl_MRP_List_OPEX.TableIdentifier, dbo.tbl_MRP_List.DocNumber, dbo.vw_AXEntityTable.NAME AS Entity, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_MRP_List_OPEX.ItemCode, dbo.tbl_MRP_List_OPEX.Description, dbo.tbl_MRP_List_OPEX.DescriptionAddl, dbo.tbl_MRP_List_OPEX.Qty, dbo.tbl_MRP_List_OPEX.Cost, dbo.tbl_MRP_List_OPEX.TotalCost, dbo.vw_AXInventTable.ITEMGROUPID AS ItemCatCode, dbo.vw_AXProdCategory.DESCRIPTION AS ItemCat FROM  dbo.vw_AXProdCategory RIGHT OUTER JOIN dbo.vw_AXInventTable ON dbo.vw_AXProdCategory.NAME = dbo.vw_AXInventTable.ITEMGROUPID RIGHT OUTER JOIN dbo.tbl_MRP_List LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN         dbo.tbl_MRP_List_OPEX ON dbo.tbl_MRP_List.DocNumber = dbo.tbl_MRP_List_OPEX.HeaderDocNum ON dbo.vw_AXInventTable.ITEMID = dbo.tbl_MRP_List_OPEX.ItemCode WHERE(dbo.tbl_MRP_List.StatusKey = 4) AND(dbo.tbl_MRP_List.MRPYear = " + year_string + ") AND(dbo.tbl_MRP_List.MRPMonth = " + month_string + ") AND(dbo.tbl_MRP_List.DocNumber = " + doc_string + ") AND(dbo.vw_AXInventTable.ITEMGROUPID = " + groupid_string + ")";
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
                    dtRow["TableIdentifier"] = row["TableIdentifier"].ToString();
                    dtRow["DocumentNumber"] = row["DocNumber"].ToString();
                    dtRow["Entity"] = row["Entity"].ToString();
                    dtRow["BU"] = row["BU"].ToString();
                    dtRow["ItemCatCode"] = row["ItemCatCode"].ToString();
                    dtRow["ItemCat"] = row["ItemCat"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();
                    if (row["DescriptionAddl"].ToString().Trim() !="")
                    {
                        dtRow["ItemDescription"] = row["Description"].ToString() +" (" + row["DescriptionAddl"].ToString() + ")";
                    } else
                    {
                        dtRow["ItemDescription"] = row["Description"].ToString();
                    }
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();

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

            string qry = "SELECT [ACCOUNTNUM],[NAME] FROM [hijo_portal].[dbo].[vw_AXVendTable]";

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

        public static DataTable InventSiteTable()
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

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSite] ORDER BY NAME ASC";

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

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSiteWarehouse] WHERE [INVENTSITEID] = '" + ID + "'  ORDER BY NAME ASC";

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

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXInventSiteLocation] WHERE [WarehouseCode] = '" + warehouse + "'  ORDER BY WarehouseCode ASC";

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
    }
}