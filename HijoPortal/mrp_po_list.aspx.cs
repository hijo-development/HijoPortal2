using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using HijoPortal.classes;

namespace HijoPortal
{
    public partial class mrp_po_list : System.Web.UI.Page
    {

        private void CheckCreatorKey()
        {
            if (Session["CreatorKey"] == null)
            {
                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback(MRPClass.DefaultPage());
                else
                    Response.Redirect("default.aspx");

                return;
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Session["MRP_Number"] = null;
            Response.Redirect("mrp_po_selectitem.aspx");
        }

        private void Bind_PO_List()
        {
            DataTable dtTable = POClass.PO_Created_List();
            gridCreatedPO.DataSource = dtTable;
            gridCreatedPO.KeyFieldName = "PK";
            gridCreatedPO.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            if (!Page.IsPostBack)
            {
                Bind_PO_List();
            }
        }

        protected void gridCreatedPO_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            string btnID = e.ButtonID;
            ASPxGridView grid = sender as ASPxGridView;
            string ponumber = grid.GetRowValues(grid.FocusedRowIndex, "PONumber").ToString();
            switch (btnID)
            {
                case "Edit":
                    ASPxWebControl.RedirectOnCallback("mrp_po_addedit.aspx?PONum=" + ponumber);
                    //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum="+ponumber;
                    break;
            }
        }
    }
}