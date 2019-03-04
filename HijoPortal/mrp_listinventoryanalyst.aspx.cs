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
    public partial class mrp_listinventoryanalyst : System.Web.UI.Page
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

        private void Bind_MRP_InventAnalyst()
        {
            DataTable dtRecord = MRPClass.MRP_InventoryAnalyst_Edit();
            grdMRPListInventAnalyst.DataSource = dtRecord;
            grdMRPListInventAnalyst.KeyFieldName = "PK";
            grdMRPListInventAnalyst.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                //Rsize
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                Bind_MRP_InventAnalyst();
            }
        }

        protected void grdMRPListInventAnalyst_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "Edit")
            {
                string docNum = grdMRPListInventAnalyst.GetRowValues(grdMRPListInventAnalyst.FocusedRowIndex, "DocNumber").ToString();

                Response.RedirectLocation = "mrp_inventanalyst.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=3";
            }
        }
    }
}