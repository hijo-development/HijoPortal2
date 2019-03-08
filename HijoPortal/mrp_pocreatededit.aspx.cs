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

            BindGrid();
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
    }
}