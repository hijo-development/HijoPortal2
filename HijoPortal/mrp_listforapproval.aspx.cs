using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_listforapproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack) {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }
            BindListApproval();
        }

        private void BindListApproval()
        {
            ListForApprovalGrid.DataSource = MRPClass.MRP_ListforDeliberation();
            ListForApprovalGrid.KeyFieldName = "PK";
            ListForApprovalGrid.DataBind();
        }

        protected void ListForApprovalGrid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "ForApprovalGridEdit")
            {
                string doc_number = ListForApprovalGrid.GetRowValues(ListForApprovalGrid.FocusedRowIndex, "DocNumber").ToString();
                string work_line = ListForApprovalGrid.GetRowValues(ListForApprovalGrid.FocusedRowIndex, "WorkLine").ToString();
                ASPxWebControl.RedirectOnCallback("mrp_inventoryanalyst_forapproval.aspx?DocNum=" + doc_number.ToString() + "&WrkFlwLn=" + work_line.ToString());
            }
        }

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
    }
}