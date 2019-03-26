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
            //combo.TextFormatString = "{0}{1}";
        }

        protected void TermsCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT PAYMTERMID FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + Vendor.Text.ToString() + "'";

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
            l_value.Width = 100;
            Terms.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            l_text.Width = 250;
            Terms.Columns.Add(l_text);

            Terms.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Terms.ValueField = "PAYMTERMID";
            Terms.TextField = "DESCRIPTION";
            Terms.DataBind();
            //Terms.TextFormatString = "{0}";
            Terms.ClientEnabled = true;
        }

        protected void CurrencyCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT dbo.vw_AXVendTable.VENDGROUP, dbo.vw_AXVendTable.PAYMTERMID, dbo.vw_AXVendTable.CURRENCY, dbo.vw_AXCurrency.TXT FROM dbo.vw_AXVendTable INNER JOIN dbo.vw_AXCurrency ON dbo.vw_AXVendTable.CURRENCY = dbo.vw_AXCurrency.CURRENCYCODE WHERE[ACCOUNTNUM] = '" + Vendor.Text.ToString() + "'";

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

            Currency.DataSource = POClass.CurrencyTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "CURRENCYCODE";
            l_value.Caption = "Currency Code";
            l_value.Width = 100;
            Currency.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "TXT";
            l_text.Width = 250;
            l_text.Caption = "Currency Name";
            Currency.Columns.Add(l_text);

            Currency.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Currency.ValueField = "CURRENCYCODE";
            Currency.TextField = "TXT";
            Currency.DataBind();
            Currency.ClientEnabled = true;

            Currency.Value = value;
            Currency.Text = value + ";" + text;
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
        }

        protected void WarehouseCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            Warehouse.Value = "";
            Warehouse.DataSource = POClass.InventSiteWarehouseTable(Site.Text.ToString());

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
            Warehouse.DataBind();
            Warehouse.ClientEnabled = true;
        }

        protected void LocationCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            Location.Value = "";

            Location.DataSource = POClass.InventSiteLocationTable(Warehouse.Text.ToString());

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

        protected void Save_Click(object sender, EventArgs e)
        {

        }
    }
}