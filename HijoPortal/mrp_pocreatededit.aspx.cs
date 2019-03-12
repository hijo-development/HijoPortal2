using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_pocreatededit : System.Web.UI.Page
    {
        private static string ponumber = "", docnumber = "";
        private static bool bind = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            if (!Page.IsPostBack)
            {
                ponumber = Request.Params["PONum"].ToString();

                if (string.IsNullOrEmpty(ponumber))
                    return;

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();

                //QUERY DETAILS
                string query = "SELECT * FROM " + MRPClass.POTableName() + " WHERE [PONumber] = '" + ponumber + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    docnumber = reader["MRPNumber"].ToString();
                    DocNumber.Value = docnumber;
                    DateCreated.Value = Convert.ToDateTime(reader["DateCreated"].ToString()).ToString("MM/dd/yyyy");
                    ExpectedDate.Value = reader["ExpectedDate"].ToString();
                    Vendor.Value = reader["VendorCode"].ToString();
                    Terms.Value = reader["PaymentTerms"].ToString();
                    Currency.Value = reader["CurrencyCode"].ToString();
                    Site.Value = reader["InventSite"].ToString();
                    Warehouse.Value = reader["InventSiteWarehouse"].ToString();
                    Location.Value = reader["InventSiteWarehouseLocation"].ToString();
                }

                PONumber.Value = ponumber;

            }
            if (bind) BindGrid();
            else bind = true;
        }

        private void BindGrid()
        {
            if (string.IsNullOrEmpty(ponumber))
                return;

            POCreatedGrid.DataSource = MRPClass.PO_Creation_Details(ponumber);
            POCreatedGrid.KeyFieldName = "PK";
            POCreatedGrid.DataBind();
        }

        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.PO_ItemCodes(docnumber);

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "ItemCode";
            l_value.Caption = "Item Code";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text_1 = new ListBoxColumn();
            l_text_1.FieldName = "Description";
            combo.Columns.Add(l_text_1);

            ListBoxColumn l_text_2 = new ListBoxColumn();
            l_text_2.FieldName = "UOM";
            combo.Columns.Add(l_text_2);

            ListBoxColumn l_text_3 = new ListBoxColumn();
            l_text_3.FieldName = "Cost";
            combo.Columns.Add(l_text_3);

            ListBoxColumn l_text_4 = new ListBoxColumn();
            l_text_4.FieldName = "Qty";
            combo.Columns.Add(l_text_4);

            ListBoxColumn l_text_5 = new ListBoxColumn();
            l_text_5.FieldName = "TotalCost";
            combo.Columns.Add(l_text_5);

            ListBoxColumn l_text_6 = new ListBoxColumn();
            l_text_6.FieldName = "Type";
            combo.Columns.Add(l_text_6);

            ListBoxColumn l_text_pk = new ListBoxColumn();
            l_text_pk.FieldName = "PK";
            l_text_pk.Width = 0;
            combo.Columns.Add(l_text_pk);

            ListBoxColumn l_text_identifier = new ListBoxColumn();
            l_text_identifier.FieldName = "Identifier";
            l_text_identifier.Width = 0;
            combo.Columns.Add(l_text_identifier);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.TextField = "ItemCode";
            combo.ValueField = "ItemCode";
            combo.DataBind();
            combo.TextFormatString = "{0}";

            GridViewEditItemTemplateContainer container = combo.NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "ItemCode").ToString();
            }
        }

        protected void taxgroup_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.TaxGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TAXGROUP";
            combo.Columns.Add(l_value);
            combo.TextField = "TAXGROUP";
            combo.ValueField = "TAXGROUP";
            combo.DataBind();
            combo.TextFormatString = "{0}";

            GridViewEditItemTemplateContainer container = combo.NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "TAXGROUP").ToString();
            }
        }

        protected void taxitemgroup_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.TaxItemGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TAXITEMGROUP";
            combo.Columns.Add(l_value);
            combo.TextField = "TAXITEMGROUP";
            combo.ValueField = "TAXITEMGROUP";
            combo.DataBind();
            combo.TextFormatString = "{0}";

            GridViewEditItemTemplateContainer container = combo.NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "TAXITEMGROUP").ToString();
            }
        }

        protected void POCreatedGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bind = false;
        }

        protected void POCreatedGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bind = false;
        }

        protected void POCreatedGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxHiddenField pk_identifier = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ItemCode"], "pk_identifier_ingrid") as ASPxHiddenField;
            ASPxComboBox itemcode = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ItemCode"], "ItemCode") as ASPxComboBox;
            ASPxComboBox taxgroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxGroup"], "TaxGroup") as ASPxComboBox;
            ASPxComboBox taxitemgroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxItemGroup"], "TaxItemGroup") as ASPxComboBox;
            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Qty"], "Qty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Cost"], "Cost") as ASPxTextBox;
            ASPxTextBox totalcost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TotalCost"], "TotalCost") as ASPxTextBox;

            string itempk = pk_identifier["hidden_pk"].ToString();
            string identifier = pk_identifier["hidden_identifier"].ToString();
            string code = itemcode.Value.ToString();
            string tax_group = taxgroup.Value.ToString();
            string tax_itemgroup = taxitemgroup.Value.ToString();
            string qtystr = qty.Value.ToString();
            string coststr = cost.Value.ToString();
            string totalstr = totalcost.Value.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            //INSERT NEW ROW IN PO CREATION DETAILS
            string insert = "INSERT INTO " + MRPClass.POCreationTableName() + " ([PONumber] ,[ItemPK], [Identifier] ,[ItemCode] ,[TaxGroup] ,[TaxItemGroup] ,[Qty],[Cost] ,[TotalCost]) VALUES (@PONumber, @ItemPK, @Identifier, @ItemCode, @TaxGroup, @TaxItemGroup, @Qty, @Cost, @TotalCost)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@PONumber", ponumber);
            cmd.Parameters.AddWithValue("@ItemPK", itempk);
            cmd.Parameters.AddWithValue("@Identifier", identifier);
            cmd.Parameters.AddWithValue("@ItemCode", code);
            cmd.Parameters.AddWithValue("@TaxGroup", tax_group);
            cmd.Parameters.AddWithValue("@TaxItemGroup", tax_itemgroup);
            cmd.Parameters.AddWithValue("@Qty", qtystr);
            cmd.Parameters.AddWithValue("@Cost", coststr);
            cmd.Parameters.AddWithValue("@TotalCost", totalstr);
            cmd.CommandType = System.Data.CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            MRPClass.PrintString("identifier:" + identifier);
            MRPClass.PrintString("identifier:" + itempk);
            if (result > 0)
            {
                switch (identifier)
                {
                    case "1"://Direct Materials
                        string update_DM = "UPDATE " + MRPClass.DirectMatTable() + " SET [QtyPO] = @qty WHERE [PK] = '" + itempk + "'";

                        SqlCommand cmd_DM = new SqlCommand(update_DM, conn);
                        cmd_DM.Parameters.AddWithValue("@qty", qtystr);
                        cmd_DM.CommandType = System.Data.CommandType.Text;
                        cmd_DM.ExecuteNonQuery();

                        break;

                    case "2"://Opex
                        string update_OP = "UPDATE " + MRPClass.OpexTable() + " SET [QtyPO] = @qty WHERE [PK] = '" + itempk + "'";

                        SqlCommand cmd_OP = new SqlCommand(update_OP, conn);
                        cmd_OP.Parameters.AddWithValue("@qty", qtystr);
                        cmd_OP.CommandType = System.Data.CommandType.Text;
                        int aa = cmd_OP.ExecuteNonQuery();
                        if (aa > 0)
                            MRPClass.PrintString("successfully.....");
                        break;
                }
            }

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            BindGrid();
        }

        protected void POCreatedGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            string itempk = "", identifier = "";
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string query = "SELECT * FROM " + MRPClass.POCreationTableName() + " WHERE [PK] = '" + PK + "'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                itempk = reader["ItemPK"].ToString();
                identifier = reader["Identifier"].ToString();
            }
            reader.Close();

            ASPxComboBox itemcode = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ItemCode"], "ItemCode") as ASPxComboBox;
            ASPxComboBox taxgroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxGroup"], "TaxGroup") as ASPxComboBox;
            ASPxComboBox taxitemgroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxItemGroup"], "TaxItemGroup") as ASPxComboBox;
            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Qty"], "Qty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Cost"], "Cost") as ASPxTextBox;
            ASPxTextBox totalcost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TotalCost"], "TotalCost") as ASPxTextBox;

            string code = itemcode.Value.ToString();
            string tax_group = taxgroup.Value.ToString();
            string tax_itemgroup = taxitemgroup.Value.ToString();
            string qtystr = qty.Value.ToString();
            string coststr = cost.Value.ToString();
            string totalstr = totalcost.Value.ToString();

            string update = "UPDATE " + MRPClass.POCreationTableName() + " SET [ItemCode] = @ItemCode, [TaxGroup] = @TaxGroup, [TaxItemGroup] = @TaxItemGroup, [Qty] = @Qty, [Cost] = @Cost, [TotalCost] = @TotalCost WHERE [PK] = '" + PK + "'";
            cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@ItemCode", code);
            cmd.Parameters.AddWithValue("@TaxGroup", tax_group);
            cmd.Parameters.AddWithValue("@TaxItemGroup", tax_itemgroup);
            cmd.Parameters.AddWithValue("@Qty", qtystr);
            cmd.Parameters.AddWithValue("@Cost", coststr);
            cmd.Parameters.AddWithValue("@TotalCost", totalstr);
            cmd.CommandType = System.Data.CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                switch (identifier)
                {
                    case "1"://Direct Materials
                        string update_DM = "UPDATE " + MRPClass.DirectMatTable() + " SET [QtyPO] = @qty WHERE [PK] = '" + itempk + "'";

                        SqlCommand cmd_DM = new SqlCommand(update_DM, conn);
                        cmd_DM.Parameters.AddWithValue("@qty", qtystr);
                        cmd_DM.CommandType = System.Data.CommandType.Text;
                        cmd_DM.ExecuteNonQuery();

                        break;

                    case "2"://Opex
                        string update_OP = "UPDATE " + MRPClass.OpexTable() + " SET [QtyPO] = @qty WHERE [PK] = '" + itempk + "'";

                        SqlCommand cmd_OP = new SqlCommand(update_OP, conn);
                        cmd_OP.Parameters.AddWithValue("@qty", qtystr);
                        cmd_OP.CommandType = System.Data.CommandType.Text;
                        int aa = cmd_OP.ExecuteNonQuery();
                        if (aa > 0)
                            MRPClass.PrintString("successfully.....");
                        break;
                }
            }

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            BindGrid();
        }

        protected void POCreatedGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            string itempk = "", identifier = "", qty = "", cost = "", total = "";
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string query = "SELECT * FROM " + MRPClass.POCreationTableName() + " WHERE [PK] = '" + PK + "'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                itempk = reader["ItemPK"].ToString();
                identifier = reader["Identifier"].ToString();
                qty = reader["Qty"].ToString();
                cost = reader["Cost"].ToString();
                total = reader["TotalCost"].ToString();
            }
            reader.Close();

            string delete = "DELETE FROM " + MRPClass.POCreationTableName() + " WHERE [PK] = '" + PK + "'";
            cmd = new SqlCommand(delete, conn);
            int res = cmd.ExecuteNonQuery();

            if (res > 0)
            {
                Double update_qtypo = 0;
                switch (identifier)
                {
                    case "1"://Direct Materials
                        Double dm_qtypo = 0;
                        string query_dm = "SELECT [QtyPO] FROM " + MRPClass.DirectMatTable() + " WHERE [PK] = '" + itempk + "'";
                        cmd = new SqlCommand(query_dm, conn);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dm_qtypo = Convert.ToDouble(reader[0].ToString());
                        }
                        reader.Close();

                        update_qtypo = dm_qtypo - Convert.ToDouble(qty);

                        string update_dm = "UPDATE " + MRPClass.DirectMatTable() + " SET [QtyPO] = '" + update_qtypo + "' WHERE [PK] = '" + itempk + "'";
                        cmd = new SqlCommand(update_dm, conn);
                        cmd.ExecuteNonQuery();

                        break;
                    case "2"://Opex
                        Double op_qtypo = 0;
                        string query_op = "SELECT [QtyPO] FROM " + MRPClass.OpexTable() + " WHERE [PK] = '" + itempk + "'";
                        cmd = new SqlCommand(query_op, conn);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            op_qtypo = Convert.ToDouble(reader[0].ToString());
                        }
                        reader.Close();

                        update_qtypo = op_qtypo - Convert.ToDouble(qty);

                        string update_op = "UPDATE " + MRPClass.OpexTable() + " SET [QtyPO] = '" + update_qtypo + "' WHERE [PK] = '" + itempk + "'";
                        cmd = new SqlCommand(update_op, conn);
                        cmd.ExecuteNonQuery();
                        break;
                }

            }

            conn.Close();

        }

        protected void POCreatedGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void Vendor_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.VendTableTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "ACCOUNTNUM";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            combo.Columns.Add(l_text);

            combo.ValueField = "ACCOUNTNUM";
            combo.TextField = "NAME";
            combo.DataBind();
            combo.TextFormatString = "{0}";
        }

        protected void Site_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.InventSiteTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "SITEID";
            l_value.Caption = "Site ID";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            combo.Columns.Add(l_text);

            combo.TextField = "NAME";
            combo.ValueField = "SITEID";
            combo.DataBind();
            combo.TextFormatString = "{0}";
        }



        protected void pocreatededit_currency_callback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT VENDGROUP, PAYMTERMID, CURRENCY FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE[ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Currency.Value = reader["CURRENCY"].ToString();
            }
            reader.Close();
            conn.Close();

            Currency.DataSource = MRPClass.CurrencyTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "CURRENCYCODE";
            Currency.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "TXT";
            Currency.Columns.Add(l_text);

            Currency.ValueField = "CURRENCYCODE";
            Currency.TextField = "TXT";
            Currency.DataBind();
            Currency.TextFormatString = "{0}";
            Currency.ClientEnabled = true;
        }

        protected void pocreatededit_terms_callback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT PAYMTERMID FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Terms.Value = reader["PAYMTERMID"].ToString();
            }
            reader.Close();
            conn.Close();

            Terms.DataSource = MRPClass.PaymTermTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PAYMTERMID";
            Terms.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            Terms.Columns.Add(l_text);

            Terms.ValueField = "PAYMTERMID";
            Terms.TextField = "DESCRIPTION";
            Terms.DataBind();
            Terms.TextFormatString = "{0}";
            Terms.ClientEnabled = true;
        }

        protected void pocreatededit_warehouse_callback_Callback(object sender, CallbackEventArgsBase e)
        {
            Warehouse.Value = "";
            Warehouse.DataSource = MRPClass.InventSiteWarehouseTable(Site.Value.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "warehouse";
            l_value.Caption = "Warehouse";
            Warehouse.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            Warehouse.Columns.Add(l_text);

            Warehouse.TextField = "NAME";
            Warehouse.ValueField = "warehouse";
            Warehouse.TextFormatString = "{0}";
            Warehouse.DataBind();
            Warehouse.ClientEnabled = true;

        }

        protected void pocreatededit_location_callback_Callback(object sender, CallbackEventArgsBase e)
        {
            Location.Value = "";
            if (Warehouse.Value == null)
                return;

            Location.DataSource = MRPClass.InventSiteLocationTable(Warehouse.Value.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "LocationCode";
            Location.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //Location.Columns.Add(l_text);

            //Location.TextField = "NAME";
            Location.ValueField = "LocationCode";
            Location.DataBind();
            Location.TextFormatString = "{0}";
            Location.ClientEnabled = true;
        }

        
        protected void Currency_Init(object sender, EventArgs e)
        {
            if (Vendor.Value == null)
                return;

            string query = "SELECT VENDGROUP, PAYMTERMID, CURRENCY FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE[ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Currency.Value = reader["CURRENCY"].ToString();
            }
            reader.Close();
            conn.Close();

            Currency.DataSource = MRPClass.CurrencyTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "CURRENCYCODE";
            Currency.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "TXT";
            Currency.Columns.Add(l_text);

            Currency.ValueField = "CURRENCYCODE";
            Currency.TextField = "TXT";
            Currency.DataBind();
            Currency.TextFormatString = "{0}";
            Currency.ClientEnabled = true;
        }
        protected void Terms_Init(object sender, EventArgs e)
        {
            if (Vendor.Value == null)
                return;

            string query = "SELECT PAYMTERMID FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Terms.Value = reader["PAYMTERMID"].ToString();
            }
            reader.Close();
            conn.Close();

            Terms.DataSource = MRPClass.PaymTermTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PAYMTERMID";
            Terms.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            Terms.Columns.Add(l_text);

            Terms.ValueField = "PAYMTERMID";
            Terms.TextField = "DESCRIPTION";
            Terms.DataBind();
            Terms.TextFormatString = "{0}";
            Terms.ClientEnabled = true;
        }

        protected void Warehouse_Init(object sender, EventArgs e)
        {
            if (Site.Value == null)
                return;

            Warehouse.Value = "";
            Warehouse.DataSource = MRPClass.InventSiteWarehouseTable(Site.Value.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "warehouse";
            l_value.Caption = "Warehouse";
            Warehouse.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            Warehouse.Columns.Add(l_text);

            Warehouse.TextField = "NAME";
            Warehouse.ValueField = "warehouse";
            Warehouse.TextFormatString = "{0}";
            Warehouse.DataBind();
            Warehouse.ClientEnabled = true;
        }

        protected void Location_Init(object sender, EventArgs e)
        {
            Location.Value = "";
            if (Warehouse.Value == null)
                return;

            Location.DataSource = MRPClass.InventSiteLocationTable(Warehouse.Value.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "LocationCode";
            Location.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //Location.Columns.Add(l_text);

            //Location.TextField = "NAME";
            Location.ValueField = "LocationCode";
            Location.DataBind();
            Location.TextFormatString = "{0}";
            Location.ClientEnabled = true;
        }
    }
}