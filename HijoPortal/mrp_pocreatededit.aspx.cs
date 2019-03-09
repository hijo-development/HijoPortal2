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
        private static string ponumber = "";
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
                    DocNumber.Value = reader["MRPNumber"].ToString();
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
            combo.DataSource = MRPClass.PO_ItemCodes();

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
            ASPxGridView grid = sender as ASPxGridView;
            grid.DoRowValidation();
            if (!grid.IsNewRowEditing)
            {
                grid.DoRowValidation();
            }
        }

        protected void POCreatedGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxHiddenField pk_identifier = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ItemCode"], "pk_identifier") as ASPxHiddenField;
            ASPxComboBox itemcode = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ItemCode"], "ItemCode") as ASPxComboBox;
            ASPxComboBox taxgroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxGroup"], "TaxGroup") as ASPxComboBox;
            ASPxComboBox taxitemgroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxItemGroup"], "TaxItemGroup") as ASPxComboBox;
            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Qty"], "Qty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Cost"], "Cost") as ASPxTextBox;
            ASPxTextBox totalcost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TotalCost"], "TotalCost") as ASPxTextBox;

            string pk = pk_identifier["hidden_pk"].ToString();
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
            cmd.Parameters.AddWithValue("@ItemPK", pk);
            cmd.Parameters.AddWithValue("@Identifier", identifier);
            cmd.Parameters.AddWithValue("@ItemCode", code);
            cmd.Parameters.AddWithValue("@TaxGroup", tax_group);
            cmd.Parameters.AddWithValue("@TaxItemGroup", tax_itemgroup);
            cmd.Parameters.AddWithValue("@Qty", qtystr);
            cmd.Parameters.AddWithValue("@Cost", coststr);
            cmd.Parameters.AddWithValue("@TotalCost", totalstr);
            cmd.CommandType = System.Data.CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result>0)
            {
                switch (identifier)
                {
                    case "1"://Direct Materials
                        string update_DM = "UPDATE " + MRPClass.DirectMatTable() + " SET [ApprovedQty] = @qty, [ApprovedCost] = @cost, [ApprovedTotalCost] = @total WHERE [PK] = '" + pk + "'";

                        SqlCommand cmd_DM = new SqlCommand(update_DM, conn);
                        cmd_DM.Parameters.AddWithValue("@qty", qtystr);
                        cmd_DM.Parameters.AddWithValue("@cost", coststr);
                        cmd_DM.Parameters.AddWithValue("@total", totalstr);
                        cmd_DM.CommandType = System.Data.CommandType.Text;
                        cmd_DM.ExecuteNonQuery();

                        break;

                    case "2"://Opex
                        string update_OP = "UPDATE " + MRPClass.OpexTable() + " SET [ApprovedQty] = @qty, [ApprovedCost] = @cost, [ApprovedTotalCost] = @total WHERE [PK] = '" + pk + "'";

                        SqlCommand cmd_OP = new SqlCommand(update_OP, conn);
                        cmd_OP.Parameters.AddWithValue("@qty", qtystr);
                        cmd_OP.Parameters.AddWithValue("@cost", coststr);
                        cmd_OP.Parameters.AddWithValue("@total", totalstr);
                        cmd_OP.CommandType = System.Data.CommandType.Text;
                        cmd_OP.ExecuteNonQuery();
                        break;
                }
            }

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void POCreatedGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void POCreatedGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void POCreatedGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }
    }
}