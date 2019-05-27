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
        private static string vendorCode = "", vendorName = "", termsCode = "", termsName = "", currencyCode = "", currencyName = "", siteName = "", siteCode = "", warehouseCode = "", warehouseName = "", locationCode = "", entity = "";
        private static string ponumber = "";
        private static int status = -1;
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
            LoadingPanel.ShowImage = true;
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                BindData();
            }

            if (bind)
                BindGrid();
            else
                bind = true;
        }

        private void BindData()
        {
            ponumber = Request.Params["PONum"].ToString();

            PONumberLbl.Text = ponumber;

            string query = "SELECT dbo.tbl_POCreation.*, dbo.vw_AXVendTable.NAME AS VendorName, dbo.vw_AXPaymTerm.DESCRIPTION AS TermsName, dbo.vw_AXCurrency.TXT, dbo.vw_AXInventSite.NAME AS SiteName, dbo.vw_AXInventSiteWarehouse.NAME AS WarehouseName FROM   dbo.tbl_POCreation LEFT OUTER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM LEFT OUTER JOIN dbo.vw_AXPaymTerm ON dbo.tbl_POCreation.PaymentTerms = dbo.vw_AXPaymTerm.PAYMTERMID LEFT OUTER JOIN dbo.vw_AXCurrency ON dbo.tbl_POCreation.CurrencyCode = dbo.vw_AXCurrency.CURRENCYCODE LEFT OUTER JOIN dbo.vw_AXInventSite ON dbo.tbl_POCreation.InventSite = dbo.vw_AXInventSite.SITEID LEFT OUTER JOIN dbo.vw_AXInventSiteWarehouse ON dbo.tbl_POCreation.InventSiteWarehouse = dbo.vw_AXInventSiteWarehouse.warehouse WHERE [PONumber] = '" + ponumber + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                entity = reader["EntityCode"].ToString();
                status = Convert.ToInt32(reader["POStatus"].ToString());
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

                MOPReference.Value = reader["MRPNumber"].ToString();

                Remarks.Text = reader["Remarks"].ToString();

                if (Convert.ToInt32(reader["POStatus"]) == 0)
                    StatusLbl.Text = "Created";
                else
                    StatusLbl.Text = "Submitted";

            }
            reader.Close();
            //VendorCombo.Value = 

            if (status == 0)
                VendorCombo_Data();

            VendorCombo.Value = vendorCode;
            VendorCombo.Text = vendorCode;
            VendorLbl.Text = vendorName;

            if (!string.IsNullOrEmpty(vendorCode))
                VendorCombo.IsValid = true;

            if (status == 0)
                TermsCombo_Data();

            TermsCombo.Value = termsCode;
            TermsCombo.Text = termsCode;
            TermsLbl.Text = termsName;

            if (!string.IsNullOrEmpty(termsCode))
                TermsCombo.IsValid = true;

            if (status == 0)
                CurrencyCombo_Data();


            CurrencyCombo.Value = currencyCode;
            CurrencyCombo.Text = currencyCode;
            CurrencyLbl.Text = currencyName;
            if (!string.IsNullOrEmpty(currencyCode))
                CurrencyCombo.IsValid = true;

            if (status == 0)
                SiteCombo_Data();

            SiteCombo.Value = siteCode;
            SiteCombo.Text = siteCode;
            SiteLbl.Text = siteName;
            if (!string.IsNullOrEmpty(siteCode))
                SiteCombo.IsValid = true;


            if (status == 0)
                WarehouseCombo_Data();

            WarehouseCombo.Value = warehouseCode;
            WarehouseCombo.Text = warehouseCode;
            WarehouseLbl.Text = warehouseName;
            if (!string.IsNullOrEmpty(warehouseCode))
                WarehouseCombo.IsValid = true;

            if (status == 0)
                LocationCombo_Data();


            LocationCombo.Value = locationCode;
            LocationCombo.Text = locationCode;


            if (status == 1)
            {
                VendorCombo.ReadOnly = true;
                TermsCombo.ReadOnly = true;
                CurrencyCombo.ReadOnly = true;
                SiteCombo.ReadOnly = true;
                WarehouseCombo.ReadOnly = true;
                LocationCombo.ReadOnly = true;
                ExpDel.ReadOnly = true;
                ExpDel.DropDownButton.ClientVisible = false;
                Save.ClientEnabled = false;
                Submit.ClientEnabled = false;
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
            //string query = "SELECT PAYMTERMID, DESCRIPTION FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + VendorCombo.Text.ToString() + "'";
            string query = "SELECT dbo.vw_AXVendTable.PAYMTERMID, ISNULL(dbo.vw_AXPaymTerm.DESCRIPTION, N'') AS DESCRIPTION, dbo.vw_AXVendTable.ACCOUNTNUM FROM dbo.vw_AXVendTable LEFT OUTER JOIN  dbo.vw_AXPaymTerm ON dbo.vw_AXVendTable.PAYMTERMID = dbo.vw_AXPaymTerm.PAYMTERMID WHERE(dbo.vw_AXVendTable.ACCOUNTNUM = '" + VendorCombo.Text.ToString() + "')";
            ASPxComboBox combo = TermsCombo as ASPxComboBox;
            combo.Columns.Clear();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    combo.Value = reader["PAYMTERMID"].ToString();
            //}
            //reader.Close();

            string value = "", text = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                value = reader["PAYMTERMID"].ToString();
                text = reader["DESCRIPTION"].ToString();
            }
            reader.Close();
            conn.Close();

            combo.DataSource = null;
            combo.DataSource = POClass.PaymTermTable();

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

            combo.Value = value;
            combo.Text = value;

            combo.IsValid = (!string.IsNullOrEmpty(value)) ? true : false;

            TermsLbl.Text = text;   //set text label of terms
        }

        private void CurrencyCombo_Data()
        {
            ASPxComboBox combo = CurrencyCombo as ASPxComboBox;
            combo.Columns.Clear();

            string query = "SELECT dbo.vw_AXVendTable.VENDGROUP, dbo.vw_AXVendTable.PAYMTERMID, dbo.vw_AXVendTable.CURRENCY, dbo.vw_AXCurrency.TXT FROM dbo.vw_AXVendTable LEFT OUTER JOIN dbo.vw_AXCurrency ON dbo.vw_AXVendTable.CURRENCY = dbo.vw_AXCurrency.CURRENCYCODE WHERE[ACCOUNTNUM] = '" + VendorCombo.Text.ToString() + "'";

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
            combo.DataSource = POClass.InventSiteTable(entity);

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

        protected void POAddEditGrid_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            BindGrid();
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
            string update = "UPDATE [dbo].[tbl_POCreation] SET [ExpectedDate] = @ExpectedDate, [VendorCode] = @VendorCode, [PaymentTerms] = @PaymentTerms, [CurrencyCode] = @CurrencyCode, [InventSite] = @InventSite, [InventSiteWarehouse] = @InventSiteWarehouse, [InventSiteWarehouseLocation] = @InventSiteWarehouseLocation, [Remarks] = @Remarks WHERE [PONumber] = @PONumber";

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
            cmd.Parameters.AddWithValue("@Remarks", Remarks.Text.ToString());
            cmd.Parameters.AddWithValue("@PONumber", ponumber);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            BindData();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            bool cancel = false;
            ASPxGridView grid = POAddEditGrid as ASPxGridView;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object taxgroup = grid.GetRowValues(i, "TaxGroup");
                object taxitemgroup = grid.GetRowValues(i, "TaxItemGroup");
                object identifier = grid.GetRowValues(i, "Identifier");
                object cip = grid.GetRowValues(i, "CapexCIP");
                object prodcat = grid.GetRowValues(i, "ProdCat");

                if (string.IsNullOrEmpty(taxgroup.ToString()) || string.IsNullOrEmpty(taxitemgroup.ToString()))
                {
                    cancel = true;
                    break;
                }

                if (identifier.ToString() == "4")
                {
                    MRPClass.PrintString(prodcat.ToString());
                    MRPClass.PrintString(string.IsNullOrEmpty(cip.ToString()).ToString());
                    if (prodcat.ToString() != "CIP")
                    {
                        if (string.IsNullOrEmpty(cip.ToString()))
                        {
                            cancel = true;
                            break;
                        }
                    }
                }
            }
            if (cancel)//if empty taxgroup this is true
            {
                ModalPopupExtenderLoading.Hide();
                PONotify.HeaderText = "Alert";
                PONotifyLbl.Text = "Some selected items are empty.";
                PONotify.ShowOnPageLoad = true;
            }
            else
            {
                if (grid.VisibleRowCount > 0)
                    //Submit_Method();
                    //POClass.SubmitToAX(ponumber, PONotify, PONotifyLbl, ModalPopupExtenderLoading);

                    POClass.SubmitToAXTable(ponumber, PONotify, PONotifyLbl, ModalPopupExtenderLoading);
                
                else
                {
                    ModalPopupExtenderLoading.Hide();

                    Submit.ClientEnabled = false;
                    PONotify.HeaderText = "Alert";
                    PONotifyLbl.Text = "No data to submit";
                    PONotify.ShowOnPageLoad = true;
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            BindData();
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
            ASPxTextBox POCostwVAT = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["POCostwVAT"], "POCostwVAT") as ASPxTextBox;
            ASPxTextBox TotalPOCostwVAT = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TotalPOCostwVAT"], "TotalPOCostwVAT") as ASPxTextBox;
            ASPxComboBox TaxGroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxGroup"], "TaxGroup") as ASPxComboBox;
            ASPxComboBox TaxItemGroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxItemGroup"], "TaxItemGroup") as ASPxComboBox;

            ASPxCheckBox wVAT = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["wVAT"], "CheckwVAT") as ASPxCheckBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = null;

            string PK = e.Keys[0].ToString();
            int iVAT = 0;
            if (Convert.ToBoolean(wVAT.Value))
            {
                iVAT = 1;
            }
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

            string update = "UPDATE [hijo_portal].[dbo].[tbl_POCreation_Details] SET [TaxGroup] = @TaxGroup, [TaxItemGroup] = @TaxItemGroup, [Qty] = @Qty, [Cost] = @Cost, [POUOM] = @POUOM, [wVAT] = @wVAT, [CostwVAT] = @CostwVAT WHERE [PK] = @PK";

            string pouom = POUOM.Value.ToString();
            string qty = POQty.Value.ToString();
            string cost = POCost.Value.ToString();
            string total = TotalPOCost.Value.ToString();
            string costwVAT = POCostwVAT.Value.ToString();
            string totalwVAT = TotalPOCostwVAT.Value.ToString();
            string tax_group = TaxGroup.Value.ToString();
            string tax_item_group = TaxItemGroup.Value.ToString();

            cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@POUOM", pouom);
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty));
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost));
            //cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(total));
            cmd.Parameters.AddWithValue("@TaxGroup", tax_group);
            cmd.Parameters.AddWithValue("@TaxItemGroup", tax_item_group);
            cmd.Parameters.AddWithValue("@wVAT", iVAT);
            cmd.Parameters.AddWithValue("@CostwVAT", Convert.ToDouble(costwVAT));
            //cmd.Parameters.AddWithValue("@TotalCostwVAT", Convert.ToDouble(totalwVAT));
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

                case "4"://CAPEX
                    query = "SELECT [QtyPO] FROM [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        original_qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = original_qty_po - old_qty + Convert.ToDouble(qty);

                    update = "UPDATE [dbo].[tbl_MRP_List_CAPEX] SET [QtyPO] = '" + Convert.ToDouble(qty) + "' WHERE [PK] = '" + ItemPK + "'";
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

                case "4"://CAPEX
                    query = "SELECT [QtyPO] FROM [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        original_qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = original_qty_po - Convert.ToDouble(qty);

                    update = "UPDATE [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] SET [QtyPO] = '" + remaining + "' WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    break;
            }


            conn.Close();
            e.Cancel = true;
            BindGrid();


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

        protected void POAddEditGrid_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (status == 1)
                POAddEditGrid.Columns[0].Visible = false;
            else
                POAddEditGrid.Columns[0].Visible = true;

            if (status == 0)//if not submitted
            {
                if (grid.VisibleRowCount == 0)
                    Submit.ClientEnabled = false;
                else
                    Submit.ClientEnabled = true;
            }
        }
    }
}