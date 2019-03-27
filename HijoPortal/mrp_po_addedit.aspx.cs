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
    public partial class mrp_po_addedit : System.Web.UI.Page
    {
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
            }

            //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum=" + "PO-0000-000000001";
            //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum=PO-0000-000000001";
            string ponumber = Request.Params["PONum"].ToString();

            PONumberLbl.Text = ponumber;

            string query = "SELECT dbo.tbl_POCreation.*, dbo.vw_AXVendTable.NAME AS VendorName, dbo.vw_AXPaymTerm.DESCRIPTION AS TermsName, dbo.vw_AXCurrency.TXT, dbo.vw_AXInventSite.NAME AS SiteName, dbo.vw_AXInventSiteWarehouse.NAME AS WarehouseName FROM   dbo.tbl_POCreation INNER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM INNER JOIN dbo.vw_AXPaymTerm ON dbo.tbl_POCreation.PaymentTerms = dbo.vw_AXPaymTerm.PAYMTERMID INNER JOIN dbo.vw_AXCurrency ON dbo.tbl_POCreation.CurrencyCode = dbo.vw_AXCurrency.CURRENCYCODE INNER JOIN dbo.vw_AXInventSite ON dbo.tbl_POCreation.InventSite = dbo.vw_AXInventSite.SITEID INNER JOIN dbo.vw_AXInventSiteWarehouse ON dbo.tbl_POCreation.InventSiteWarehouse = dbo.vw_AXInventSiteWarehouse.warehouse WHERE [PONumber] = '" + ponumber + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                VendorCombo.Value = reader["VendorCode"].ToString();
                //VendorCombo.Text = reader["VendorCode"].ToString();
                VendorLbl.Text = reader["VendorName"].ToString();

                TermsCombo.Value = reader["PaymentTerms"].ToString();
                //TermsCombo.Text = reader["PaymentTerms"].ToString();
                TermsLbl.Text = reader["TermsName"].ToString();

                CurrencyCombo.Value = reader["CurrencyCode"].ToString();
                //CurrencyCombo.Text = reader["CurrencyCode"].ToString();
                CurrencyLbl.Text = reader["TXT"].ToString();

                SiteCombo.Value = reader["InventSite"].ToString();
                //SiteCombo.Text = reader["InventSite"].ToString();
                SiteLbl.Text = reader["SiteName"].ToString();

                WarehouseCombo.Value = reader["InventSiteWarehouse"].ToString();
                //WarehouseCombo.Text = reader["InventSiteWarehouse"].ToString();
                WarehouseLbl.Text = reader["WarehouseName"].ToString();

                LocationCombo.Value = reader["InventSiteWarehouseLocation"].ToString();
                //LocationCombo.Text = reader["InventSiteWarehouseLocation"].ToString();

                ExpDel.Value = reader["ExpectedDate"].ToString();
            }
            reader.Close();
            //VendorCombo.Value = 
        }
    }
}