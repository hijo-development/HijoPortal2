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
    public partial class mrp_po_create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }
        }

        protected void Vendor_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = POClass.VendTableTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "ACCOUNTNUM";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Width = 200;
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.ValueField = "ACCOUNTNUM";
            combo.TextField = "NAME";
            combo.DataBind();
            combo.TextFormatString = "{0}";
        }

        protected void TermsCallback_Callback(object sender, CallbackEventArgsBase e)
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

            Terms.DataSource = POClass.PaymTermTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PAYMTERMID";
            Terms.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            Terms.Columns.Add(l_text);

            Terms.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Terms.ValueField = "PAYMTERMID";
            Terms.TextField = "DESCRIPTION";
            Terms.DataBind();
            Terms.TextFormatString = "{0}";
            Terms.ClientEnabled = true;
        }

        protected void CurrencyCallback_Callback(object sender, CallbackEventArgsBase e)
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

            Currency.DataSource = POClass.CurrencyTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "CURRENCYCODE";
            l_value.Caption = "Currency Code";
            Currency.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "TXT";
            l_text.Caption = "Currency Name";
            Currency.Columns.Add(l_text);

            Currency.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Currency.ValueField = "CURRENCYCODE";
            Currency.TextField = "TXT";
            Currency.DataBind();
            Currency.TextFormatString = "{0}";
            Currency.ClientEnabled = true;
        }

        protected void Site_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
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
            combo.TextFormatString = "{0}";
        }

        protected void WarehouseCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            Warehouse.Value = "";
            Warehouse.DataSource = POClass.InventSiteWarehouseTable(Site.Value.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "warehouse";
            l_value.Caption = "Warehouse";
            Warehouse.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            Warehouse.Columns.Add(l_text);

            Warehouse.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Warehouse.TextField = "NAME";
            Warehouse.ValueField = "warehouse";
            Warehouse.TextFormatString = "{0}";
            Warehouse.DataBind();
            Warehouse.ClientEnabled = true;
        }

        protected void LocationCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            Location.Value = "";
            if (Warehouse.Value == null)
                return;

            Location.DataSource = POClass.InventSiteLocationTable(Warehouse.Value.ToString());

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