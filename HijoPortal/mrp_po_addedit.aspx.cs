using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_po_addedit : System.Web.UI.Page
    {
        private static string vendorCode = "", vendorName = "", termsCode = "", termsName = "", currencyCode = "", currencyName = "", siteName = "", siteCode = "", warehouseCode = "", warehouseName = "", locationCode = "";
        private static string ponumber = "";
        private static bool bind = true;
        private void CheckCreatorKey()
        {
            if (Session["CreatorKey"] == null)
            {
                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback(Constants.DefaultPage());
                else
                    Response.Redirect("default.aspx");

                return;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                BindData();
                ListofRef();
            }

            if (bind)
                BindGrid();
            else
                bind = true;

        }

        private void BindData()
        {
            //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);


            //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum=" + "PO-0000-000000001";
            //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum=PO-0000-000000001";
            ponumber = Request.Params["PONum"].ToString();

            PONumberLbl.Text = ponumber;

            string query = "SELECT dbo.tbl_POCreation.*, dbo.vw_AXVendTable.NAME AS VendorName, dbo.vw_AXPaymTerm.DESCRIPTION AS TermsName, dbo.vw_AXCurrency.TXT, dbo.vw_AXInventSite.NAME AS SiteName, dbo.vw_AXInventSiteWarehouse.NAME AS WarehouseName FROM   dbo.tbl_POCreation INNER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM INNER JOIN dbo.vw_AXPaymTerm ON dbo.tbl_POCreation.PaymentTerms = dbo.vw_AXPaymTerm.PAYMTERMID INNER JOIN dbo.vw_AXCurrency ON dbo.tbl_POCreation.CurrencyCode = dbo.vw_AXCurrency.CURRENCYCODE INNER JOIN dbo.vw_AXInventSite ON dbo.tbl_POCreation.InventSite = dbo.vw_AXInventSite.SITEID INNER JOIN dbo.vw_AXInventSiteWarehouse ON dbo.tbl_POCreation.InventSiteWarehouse = dbo.vw_AXInventSiteWarehouse.warehouse WHERE [PONumber] = '" + ponumber + "'";




            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                vendorCode = reader["VendorCode"].ToString();
                vendorName = reader["VendorName"].ToString();

                termsCode = reader["PaymentTerms"].ToString();
                termsName = reader["TermsName"].ToString();

                currencyCode = reader["CurrencyCode"].ToString();
                currencyName = reader["TXT"].ToString();

                siteCode = reader["InventSite"].ToString();
                siteName = reader["SiteName"].ToString();

                warehouseCode = reader["InventSiteWarehouse"].ToString();
                warehouseName = reader["WarehouseName"].ToString();

                locationCode = reader["InventSiteWarehouseLocation"].ToString();

                ExpDel.Value = reader["ExpectedDate"].ToString();
                //ExpDel.Text = reader["ExpectedDate"].ToString();
                txtStatus.Text = reader["POStatus"].ToString();


            }
            reader.Close();
            //VendorCombo.Value = 

            VendorCombo_Data();
            VendorCombo.Value = vendorCode;
            VendorCombo.Text = vendorCode;
            VendorLbl.Text = vendorName;
            if (!string.IsNullOrEmpty(vendorCode))
                VendorCombo.IsValid = true;

            TermsCombo_Data();
            TermsCombo.Value = termsCode;
            TermsCombo.Text = termsCode;
            TermsLbl.Text = termsName;
            if (!string.IsNullOrEmpty(termsCode))
                TermsCombo.IsValid = true;

            CurrencyCombo_Data();
            CurrencyCombo.Value = currencyCode;
            CurrencyCombo.Text = currencyCode;
            CurrencyLbl.Text = currencyName;
            if (!string.IsNullOrEmpty(currencyCode))
                CurrencyCombo.IsValid = true;

            SiteCombo_Data();
            SiteCombo.Value = siteCode;
            SiteCombo.Text = siteCode;
            SiteLbl.Text = siteName;
            if (!string.IsNullOrEmpty(siteCode))
                SiteCombo.IsValid = true;


            WarehouseCombo_Data();
            WarehouseCombo.Value = warehouseCode;
            WarehouseCombo.Text = warehouseCode;
            WarehouseLbl.Text = warehouseName;
            if (!string.IsNullOrEmpty(warehouseCode))
                WarehouseCombo.IsValid = true;

            LocationCombo_Data();
            LocationCombo.Value = locationCode;
            LocationCombo.Text = locationCode;
        }


        private void ListofRef()
        {
            string query = "SELECT DISTINCT MRPNumber FROM [hijo_portal].[dbo].[tbl_POCreation] WHERE CreatorKey = '" + Session["CreatorKey"].ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MOPReference.Text = reader[0].ToString();
            }
        }

        private void BindGrid()
        {
            POAddEditGrid.DataSource = POClass.PO_AddEdit_Table(ponumber);
            POAddEditGrid.KeyFieldName = "PK";
            POAddEditGrid.DataBind();
        }

        private void VendorCombo_Data()
        {
            ASPxComboBox combo = VendorCombo as ASPxComboBox;
            combo.Columns.Clear();
            combo.DataSource = POClass.VendTableTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "ACCOUNTNUM";
            l_value.Caption = "Vendor Code";
            l_value.Width = 100;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Vendor Name";
            l_text.Width = 350;
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.ValueField = "ACCOUNTNUM";
            combo.TextField = "NAME";
            combo.DataBind();
        }

        private void TermsCombo_Data()
        {
            string query = "SELECT PAYMTERMID FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + VendorCombo.Text.ToString() + "'";

            ASPxComboBox combo = TermsCombo as ASPxComboBox;
            combo.Columns.Clear();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                combo.Value = reader["PAYMTERMID"].ToString();
            }
            reader.Close();
            conn.Close();

            combo.DataSource = null;
            combo.DataSource = POClass.PaymTermTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PAYMTERMID";
            l_value.Width = 100;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            l_text.Width = 250;
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.ValueField = "PAYMTERMID";
            combo.TextField = "DESCRIPTION";
            combo.DataBind();
            combo.ClientEnabled = true;

            TermsLbl.Text = "";
        }

        private void CurrencyCombo_Data()
        {
            ASPxComboBox combo = CurrencyCombo as ASPxComboBox;
            combo.Columns.Clear();

            string query = "SELECT dbo.vw_AXVendTable.VENDGROUP, dbo.vw_AXVendTable.PAYMTERMID, dbo.vw_AXVendTable.CURRENCY, dbo.vw_AXCurrency.TXT FROM dbo.vw_AXVendTable INNER JOIN dbo.vw_AXCurrency ON dbo.vw_AXVendTable.CURRENCY = dbo.vw_AXCurrency.CURRENCYCODE WHERE[ACCOUNTNUM] = '" + VendorCombo.Text.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            string value = "", text = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                value = reader["CURRENCY"].ToString();
                text = reader["TXT"].ToString();
            }
            reader.Close();
            conn.Close();

            combo.DataSource = POClass.CurrencyTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "CURRENCYCODE";
            l_value.Caption = "Currency Code";
            l_value.Width = 100;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "TXT";
            l_text.Width = 250;
            l_text.Caption = "Currency Name";
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.ValueField = "CURRENCYCODE";
            combo.TextField = "TXT";
            combo.DataBind();
            combo.ClientEnabled = true;

            combo.Value = value;
            combo.Text = value + ";" + text;
        }

        private void SiteCombo_Data()
        {
            ASPxComboBox combo = SiteCombo as ASPxComboBox;
            combo.Columns.Clear();
            combo.DataSource = POClass.InventSiteTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "SITEID";
            l_value.Caption = "Site ID";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Width = 200;
            l_text.Caption = "Name";
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.TextField = "NAME";
            combo.ValueField = "SITEID";
            combo.DataBind();
        }

        private void WarehouseCombo_Data()
        {
            ASPxComboBox combo = WarehouseCombo as ASPxComboBox;
            combo.Columns.Clear();

            combo.Value = "";
            combo.DataSource = POClass.InventSiteWarehouseTable(SiteCombo.Text.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "warehouse";
            l_value.Caption = "Warehouse";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.TextField = "NAME";
            combo.ValueField = "warehouse";
            combo.DataBind();
            combo.ClientEnabled = true;
        }

        private void LocationCombo_Data()
        {
            ASPxComboBox combo = LocationCombo as ASPxComboBox;
            combo.Columns.Clear();

            combo.Value = "";

            combo.DataSource = POClass.InventSiteLocationTable(WarehouseCombo.Text.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "LocationCode";
            combo.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //Location.Columns.Add(l_text);

            //Location.TextField = "NAME";
            combo.ValueField = "LocationCode";
            combo.DataBind();
            combo.TextFormatString = "{0}";
            combo.ClientEnabled = true;
        }

        protected void TermsCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            TermsCombo_Data();
        }

        protected void CurrencyCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            CurrencyCombo_Data();
        }

        protected void WarehouseCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            WarehouseCombo_Data();
        }

        protected void LocationCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            LocationCombo_Data();
        }

        protected void TaxGroup_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = (ASPxComboBox)sender;
            combo.DataSource = POClass.TaxGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TaxGroup";
            combo.Columns.Add(l_value);

            combo.ValueField = "TaxGroup";
            combo.TextField = "TaxGroup";
            combo.DataBind();

            GridViewEditItemTemplateContainer container = combo.NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "TaxGroup").ToString();
            }
        }

        protected void TaxItemGroup_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = (ASPxComboBox)sender;
            combo.DataSource = POClass.TaxItemGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TaxItemGroup";
            combo.Columns.Add(l_value);

            combo.ValueField = "TaxItemGroup";
            combo.TextField = "TaxItemGroup";
            combo.DataBind();

            GridViewEditItemTemplateContainer container = combo.NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "TaxItemGroup").ToString();
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string update = "UPDATE [dbo].[tbl_POCreation] SET [ExpectedDate] = @ExpectedDate, [VendorCode] = @VendorCode, [PaymentTerms] = @PaymentTerms, [CurrencyCode] = @CurrencyCode, [InventSite] = @InventSite, [InventSiteWarehouse] = @InventSiteWarehouse, [InventSiteWarehouseLocation] = @InventSiteWarehouseLocation WHERE [PONumber] = @PONumber";

            string location = "";
            if (LocationCombo.Value != null)
                location = LocationCombo.Value.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@ExpectedDate", ExpDel.Text.ToString());
            cmd.Parameters.AddWithValue("@VendorCode", VendorCombo.Text.ToString());
            cmd.Parameters.AddWithValue("@PaymentTerms", TermsCombo.Text.ToString());
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCombo.Text.ToString());
            cmd.Parameters.AddWithValue("@InventSite", SiteCombo.Text.ToString());
            cmd.Parameters.AddWithValue("@InventSiteWarehouse", WarehouseCombo.Text.ToString());
            cmd.Parameters.AddWithValue("@InventSiteWarehouseLocation", location);
            cmd.Parameters.AddWithValue("@PONumber", ponumber);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            BindData();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            //ponumber;
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dt1 = new DataTable();
            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            string qry = "";

            string sServerDir = HttpContext.Current.Server.MapPath("~");
            string sDir = sServerDir + @"\po_file";
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }

            conn.Open();
            qry = "SELECT dbo.tbl_POCreation.PK, dbo.tbl_POCreation.PONumber, dbo.tbl_POCreation.MRPNumber, dbo.tbl_POCreation.DateCreated, dbo.tbl_POCreation.CreatorKey, dbo.tbl_POCreation.ExpectedDate, dbo.tbl_POCreation.VendorCode, dbo.tbl_POCreation.PaymentTerms, dbo.tbl_POCreation.CurrencyCode,dbo.tbl_POCreation.InventSite, dbo.tbl_POCreation.InventSiteWarehouse, dbo.tbl_POCreation.InventSiteWarehouseLocation, dbo.vw_AXVendTable.NAME, dbo.vw_AXVendTable.VENDGROUP, dbo.vw_AXVendTable.INCLTAX, dbo.vw_AXVendTable.PAYMMODE, dbo.vw_AXVendTable.TAXGROUP, dbo.tbl_POCreation.EntityCode, dbo.tbl_POCreation.BUSSUCode FROM  dbo.tbl_POCreation LEFT OUTER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM WHERE(dbo.tbl_POCreation.PONumber = '" + ponumber + "')";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string sPORemarks = "MOP Number " + row["MRPNumber"].ToString();
                    string sIncTax = "";
                    if (Convert.ToInt32(row["INCLTAX"]) == 0)
                    {
                        sIncTax = "No";
                    } else
                    {
                        sIncTax = "Yes";
                    }

                    string sDefaultDimension = "";
                    if (row["EntityCode"].ToString().Trim() == "0000")
                    {
                        sDefaultDimension = row["BUSSUCode"].ToString() + "__";
                    }
                    else if (row["EntityCode"].ToString().Trim() == "0303")
                    {
                        sDefaultDimension = "_" + row["BUSSUCode"].ToString() + "_";
                    }
                    else 
                    {
                        sDefaultDimension = "";
                    }

                    string sFile = sDir + @"\" + row["EntityCode"].ToString() + "_" + row["PONumber"].ToString() + "_H.txt";
                    if (!File.Exists(sFile))
                    {
                        File.Create(sFile);
                    }

                    if (File.Exists(sFile))
                    {
                        using (StreamWriter w = File.AppendText(sFile))
                        {
                            w.WriteLine("PurchId|AccountingDate|DeliveryDate|CurrencyCode|OrderAccount|InvoiceAccount|DeliveryName|PurchName|Payment|InclTax|PaymMode|PORemarks|DocumentState|DocumentStatus|InventSiteId|Remarks|VendGroup|TaxGroup|LanguageId|PostingProfile|PurchaseType|PurchPoolId|PurchStatus|DefaultDimension");
                            w.WriteLine(row["PONumber"].ToString() + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|" + row["CurrencyCode"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row["NAME"].ToString() + "|" + row["NAME"].ToString() + "|" + row["PaymentTerms"].ToString() + "|" + sIncTax + "|" + row["PAYMMODE"].ToString() + "|"+ sPORemarks .ToString() + "|Draft|None|" + row["InventSite"].ToString() + "|" + sPORemarks.ToString() + "|" + row["VENDGROUP"].ToString() + "|" + row["TAXGROUP"].ToString() + "|en-us|Gen|Purchase order||Open order|" + sDefaultDimension.ToString());
                            w.Close();
                        }
                    }

                    int iLineNumber = 0;
                    string sDefaultDimensionLine = "";
                    qry = "SELECT ItemCode, TaxGroup, TaxItemGroup, Qty, Cost, TotalCost, POUOM, (CASE Identifier WHEN 1 THEN (SELECT OprUnit FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE (SELECT OprUnit FROM  dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) END) AS OprUnit, (CASE Identifier WHEN 1 THEN         (SELECT ItemDescription + (CASE LTRIM(RTRIM(ItemDescriptionAddl)) WHEN '' THEN '' ELSE ' (' + ItemDescriptionAddl + ')' END) AS ItemDesc FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE (SELECT Description + (CASE LTRIM(RTRIM(DescriptionAddl)) WHEN '' THEN '' ELSE ' (' + DescriptionAddl + ')' END) AS ItemDesc FROM dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) END) AS ItemDesc FROM dbo.tbl_POCreation_Details WHERE(PONumber = '" + ponumber + "')";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        string sFileD = sDir + @"\" + row["EntityCode"].ToString() + "_" + row["PONumber"].ToString() + "_L.txt";
                        if (!File.Exists(sFileD))
                        {
                            File.Create(sFileD);
                        }

                        if (File.Exists(sFileD))
                        {
                            using (StreamWriter w = File.AppendText(sFileD))
                            {
                                w.WriteLine("PurchId|VendAccount|DeliveryName|Name|VendGroup|InventSiteID|InventLocationID|wMSLocationId|Complete|CreateFixedAsset|CurrencyCode|DeliveryDate|IsFinalized|IsPwp|ItemId|PurchQty|PurchUnit|PurchPrice|LineAmount|LineNumber|PriceUnit|MatchingPolicy|OverDeliveryPct|PurchaseType|PurchStatus|TaxGroup|TaxItemGroup|UnderDeliveryPct|VariantId|DefaultDimension");
                                w.Close();
                            }
                        }

                        foreach (DataRow row1 in dt1.Rows)
                        {
                            iLineNumber = iLineNumber + 1;
                            if (File.Exists(sFileD))
                            {

                                if (row["EntityCode"].ToString().Trim() == "0000")
                                {
                                    sDefaultDimensionLine = row["BUSSUCode"].ToString() + "__";
                                }
                                else if (row["EntityCode"].ToString().Trim() == "0303")
                                {
                                    sDefaultDimensionLine = "_" + row["BUSSUCode"].ToString() + "_";
                                }
                                else if (row["EntityCode"].ToString().Trim() == "0101")
                                {
                                    sDefaultDimensionLine = "__" + row1["OprUnit"].ToString();
                                }
                                else
                                {
                                    sDefaultDimensionLine = "";
                                }

                                using (StreamWriter w = File.AppendText(sFileD))
                                {
                                    w.WriteLine(row["PONumber"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row1["ItemDesc"].ToString() + "|" + row1["ItemDesc"].ToString() + "|" + row["VENDGROUP"].ToString() + "|" + row["InventSite"].ToString() + "|" + row["InventSiteWarehouse"].ToString() + "|" + row["InventSiteWarehouseLocation"].ToString() + "|0|0|" + row["CurrencyCode"].ToString() + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|0|0|" + row1["ItemCode"].ToString() + "|" + Convert.ToDouble(row1["Qty"]).ToString("#0.0000") + "|" + row1["POUOM"].ToString() + "|" + Convert.ToDouble(row1["Cost"]).ToString("#0.0000") + "|" + Convert.ToDouble(row1["TotalCost"]).ToString("#0.0000") + "|" + iLineNumber.ToString() + "|1|Three-way matching|0|Purchase order|Open order|" + row1["TaxGroup"].ToString() + "|" + row1["TaxItemGroup"].ToString() + "|100||" + sDefaultDimensionLine.ToString());
                                    w.Close();
                                }
                            }
                        }
                    }
                    dt1.Clear();
                }
            }
            dt.Clear();

            conn.Close();
        }

        protected void POAddEditGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bind = false;
        }

        protected void POAddEditGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox POUOM = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["POUOM"], "POUOM") as ASPxComboBox;
            ASPxTextBox POQty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["POQty"], "POQty") as ASPxTextBox;
            ASPxTextBox POCost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["POCost"], "POCost") as ASPxTextBox;
            ASPxTextBox TotalPOCost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TotalPOCost"], "TotalPOCost") as ASPxTextBox;
            ASPxComboBox TaxGroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxGroup"], "TaxGroup") as ASPxComboBox;
            ASPxComboBox TaxItemGroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxItemGroup"], "TaxItemGroup") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = null;

            string PK = e.Keys[0].ToString();

            string ItemPK = "", Identifier = ""; Double old_qty = 0;
            string query = "SELECT ItemPK, Identifier, Qty FROM [hijo_portal].[dbo].[tbl_POCreation_Details] WHERE [PK] = '" + PK + "'";
            cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ItemPK = reader["ItemPK"].ToString();
                Identifier = reader["Identifier"].ToString();
                old_qty = Convert.ToDouble(reader["Qty"].ToString());
            }
            reader.Close();

            string update = "UPDATE [hijo_portal].[dbo].[tbl_POCreation_Details] SET [TaxGroup] = @TaxGroup, [TaxItemGroup] = @TaxItemGroup, [Qty] = @Qty, [Cost] = @Cost, [TotalCost] = @TotalCost, [POUOM] = @POUOM WHERE [PK] = @PK";

            string pouom = POUOM.Value.ToString();
            string qty = POQty.Value.ToString();
            string cost = POCost.Value.ToString();
            string total = TotalPOCost.Value.ToString();
            string tax_group = TaxGroup.Value.ToString();
            string tax_item_group = TaxItemGroup.Value.ToString();

            cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@POUOM", pouom);
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty));
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(total));
            cmd.Parameters.AddWithValue("@TaxGroup", tax_group);
            cmd.Parameters.AddWithValue("@TaxItemGroup", tax_item_group);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();



            Double original_qty_po = 0, remaining = 0;
            switch (Identifier)
            {
                case "1"://DM
                    query = "SELECT [QtyPO] FROM [dbo].[tbl_MRP_List_DirectMaterials] WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        original_qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = original_qty_po - old_qty + Convert.ToDouble(qty);

                    update = "UPDATE [dbo].[tbl_MRP_List_DirectMaterials] SET [QtyPO] = '" + remaining + "' WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    break;

                case "2"://OP
                    query = "SELECT [QtyPO] FROM [dbo].[tbl_MRP_List_OPEX] WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        original_qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = original_qty_po - old_qty + Convert.ToDouble(qty);

                    update = "UPDATE [dbo].[tbl_MRP_List_OPEX] SET [QtyPO] = '" + Convert.ToDouble(qty) + "' WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    break;
            }
            conn.Close();
            BindGrid();

            grid.CancelEdit();
            e.Cancel = true;
        }

        protected void POAddEditGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string PK = e.Keys[0].ToString();
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = null;

            string ItemPK = "", Identifier = "", qty = "";
            string query = "SELECT ItemPK, Identifier, Qty FROM [hijo_portal].[dbo].[tbl_POCreation_Details] WHERE [PK] = '" + PK + "'";
            cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ItemPK = reader["ItemPK"].ToString();
                Identifier = reader["Identifier"].ToString();
                qty = reader["Qty"].ToString();
            }
            reader.Close();

            string delete = "DELETE FROM [dbo].[tbl_POCreation_Details] WHERE [PK] = '" + PK + "'";
            cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

            string update = ""; Double original_qty_po = 0, remaining = 0;
            switch (Identifier)
            {
                case "1"://DM

                    query = "SELECT [QtyPO] FROM [dbo].[tbl_MRP_List_DirectMaterials] WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        original_qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = original_qty_po - Convert.ToDouble(qty);
                    MRPClass.PrintString(remaining.ToString());
                    MRPClass.PrintString(original_qty_po.ToString());
                    MRPClass.PrintString(qty.ToString());

                    update = "UPDATE [dbo].[tbl_MRP_List_DirectMaterials] SET [QtyPO] = '" + remaining + "' WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    break;

                case "2"://OP
                    query = "SELECT [QtyPO] FROM [dbo].[tbl_MRP_List_OPEX] WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        original_qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = original_qty_po - Convert.ToDouble(qty);

                    update = "UPDATE [dbo].[tbl_MRP_List_OPEX] SET [QtyPO] = '" + remaining + "' WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    break;
            }

            //query = "SELECT COUNT(*) FROM [hijo_portal].[dbo].[tbl_POCreation_Details] WHERE "

            conn.Close();

            e.Cancel = true;

            //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            //BindGrid();

        }

        protected void POAddEditGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            DesignBehavior.SetBehaviorGrid(grid);
        }

        protected void POUOM_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.UOMTable();
            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "SYMBOL";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "description";
            combo.Columns.Add(l_text);

            combo.ValueField = "SYMBOL";
            combo.TextField = "description";
            combo.DataBind();
            combo.TextFormatString = "{0}";

            GridViewEditItemTemplateContainer container = ((ASPxComboBox)sender).NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "POUOM").ToString();
            }
        }

        protected void VendorCombo_Init(object sender, EventArgs e)
        {
            //VendorCombo_Data();
            //VendorCombo.Value = vendorCode;
            //VendorCombo.Text = vendorCode;
            //VendorLbl.Text = vendorName;
        }

        protected void TermsCombo_Init(object sender, EventArgs e)
        {
            //TermsCombo_Data();
            //TermsCombo.Value = termsCode;
            //TermsCombo.Text = termsCode;
            //TermsLbl.Text = termsName;
        }

        protected void CurrencyCombo_Init(object sender, EventArgs e)
        {
            //CurrencyCombo_Data();
            //CurrencyCombo.Value = currencyCode;
            //CurrencyCombo.Text = currencyCode;
            //CurrencyLbl.Text = currencyName;
        }

        protected void SiteCombo_Init(object sender, EventArgs e)
        {
            //SiteCombo_Data();
            //SiteCombo.Value = siteCode;
            //SiteCombo.Text = siteCode;
            //SiteLbl.Text = siteName;
        }

        protected void WarehouseCombo_Init(object sender, EventArgs e)
        {
            //WarehouseCombo_Data();
            //WarehouseCombo.Value = warehouseCode;
            //WarehouseCombo.Text = warehouseCode;
            //WarehouseLbl.Text = warehouseName;
        }

        protected void LocationCombo_Init(object sender, EventArgs e)
        {
            //LocationCombo_Data();
            //LocationCombo.Value = locationCode;
            //LocationCombo.Text = locationCode;
        }
    }
}