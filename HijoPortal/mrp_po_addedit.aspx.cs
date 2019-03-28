using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_po_addedit : System.Web.UI.Page
    {
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


                //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum=" + "PO-0000-000000001";
                //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum=PO-0000-000000001";
                ponumber = Request.Params["PONum"].ToString();

                PONumberLbl.Text = ponumber;

                string query = "SELECT dbo.tbl_POCreation.*, dbo.vw_AXVendTable.NAME AS VendorName, dbo.vw_AXPaymTerm.DESCRIPTION AS TermsName, dbo.vw_AXCurrency.TXT, dbo.vw_AXInventSite.NAME AS SiteName, dbo.vw_AXInventSiteWarehouse.NAME AS WarehouseName FROM   dbo.tbl_POCreation INNER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM INNER JOIN dbo.vw_AXPaymTerm ON dbo.tbl_POCreation.PaymentTerms = dbo.vw_AXPaymTerm.PAYMTERMID INNER JOIN dbo.vw_AXCurrency ON dbo.tbl_POCreation.CurrencyCode = dbo.vw_AXCurrency.CURRENCYCODE INNER JOIN dbo.vw_AXInventSite ON dbo.tbl_POCreation.InventSite = dbo.vw_AXInventSite.SITEID INNER JOIN dbo.vw_AXInventSiteWarehouse ON dbo.tbl_POCreation.InventSiteWarehouse = dbo.vw_AXInventSiteWarehouse.warehouse WHERE [PONumber] = '" + ponumber + "'";


                string vendorCode = "", vendorName = "", termsCode = "", termsName = "", currencyCode = "", currencyName = "", siteName = "", siteCode = "", warehouseCode = "", warehouseName = "", locationCode = "";

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


                }
                reader.Close();
                //VendorCombo.Value = 

                VendorCombo_Data();
                VendorCombo.Value = vendorCode;
                VendorCombo.Text = vendorCode;
                VendorLbl.Text = vendorName;

                TermsCombo_Data();
                TermsCombo.Value = termsCode;
                TermsCombo.Text = termsCode;
                TermsLbl.Text = termsName;

                CurrencyCombo_Data();
                CurrencyCombo.Value = currencyCode;
                CurrencyCombo.Text = currencyCode;
                CurrencyLbl.Text = currencyName;

                SiteCombo_Data();
                SiteCombo.Value = siteCode;
                SiteCombo.Text = siteCode;
                SiteLbl.Text = siteName;


                WarehouseCombo_Data();
                WarehouseCombo.Value = warehouseCode;
                WarehouseCombo.Text = warehouseCode;
                WarehouseLbl.Text = warehouseName;

                LocationCombo_Data();
                LocationCombo.Value = locationCode;
                LocationCombo.Text = locationCode;

                ListofRef();
            }

            if (bind)
                BindGrid();
            else
                bind = true;

        }

        private void ListofRef()
        {
            MOPRef.DataSource = POClass.PO_MOP_Ref(ponumber);
            MOPRef.ValueField = "MOPNumber";
            MOPRef.TextField = "MOPNumber";
            MOPRef.DataBind();
            //string query = "SELECT DISTINCT [MOPNumber] FROM [hijo_portal].[dbo].[tbl_POCreation_Tmp] WHERE UserKey = '" + Session["CreatorKey"].ToString() + "'";

            //SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            //conn.Open();

            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //mop_ref_arr = new ArrayList();
            //while (reader.Read())
            //{
            //    mop_ref_arr.Add(reader[0].ToString());
            //    MOPRef.Items.Add(reader[0].ToString());
            //}
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
            cmd.Parameters.AddWithValue("@ExpectedDate", ExpDel.Value.ToString());
            cmd.Parameters.AddWithValue("@VendorCode", VendorCombo.Value.ToString());
            cmd.Parameters.AddWithValue("@PaymentTerms", TermsCombo.Value.ToString());
            cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCombo.Value.ToString());
            cmd.Parameters.AddWithValue("@InventSite", SiteCombo.Value.ToString());
            cmd.Parameters.AddWithValue("@InventSiteWarehouse", WarehouseCombo.Value.ToString());
            cmd.Parameters.AddWithValue("@InventSiteWarehouseLocation", location);
            cmd.Parameters.AddWithValue("@PONumber", ponumber);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {

        }

        protected void POAddEditGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bind = false;
        }

        protected void POAddEditGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
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

            string update = "UPDATE [hijo_portal].[dbo].[tbl_POCreation_Details] SET [TaxGroup] = @TaxGroup, [TaxItemGroup] = @TaxItemGroup, [Qty] = @Qty, [Cost] = @Cost, [TotalCost] = @TotalCost WHERE [PK] = @PK";

            string qty = POQty.Value.ToString();
            string cost = POCost.Value.ToString();
            string total = TotalPOCost.Value.ToString();
            string tax_group = TaxGroup.Value.ToString();
            string tax_item_group = TaxItemGroup.Value.ToString();

            cmd = new SqlCommand(update, conn);
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

                    MRPClass.PrintString(remaining.ToString());
                    MRPClass.PrintString(old_qty.ToString());
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
            string delete = "DELETE FROM [dbo].[tbl_POCreation_Details] WHERE [PK] = '" + PK + "'";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

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

            string update = ""; Double qty_po = 0, remaining = 0;
            switch (Identifier)
            {
                case "1"://DM

                    query = "SELECT [QtyPO] FROM [dbo].[tbl_MRP_List_DirectMaterials] WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = qty_po - Convert.ToDouble(qty);

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
                        qty_po = Convert.ToDouble(reader["QtyPO"].ToString());
                    }
                    reader.Close();
                    remaining = qty_po - Convert.ToDouble(qty);

                    update = "UPDATE [dbo].[tbl_MRP_List_OPEX] SET [QtyPO] = '" + remaining + "' WHERE [PK] = '" + ItemPK + "'";
                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    break;
            }
            conn.Close();

            BindGrid();


        }

        protected void POAddEditGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            DesignBehavior.SetBehaviorGrid(grid);
        }
    }
}