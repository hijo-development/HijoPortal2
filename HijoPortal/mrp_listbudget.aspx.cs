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
    public partial class mrp_listbudget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                bool isAllowed = false;
                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget", DateTime.Now);
                if (isAllowed == false)
                {
                    Response.Redirect("home.aspx");
                } else
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                }

                
            }

            BindListBudget();
        }

        private void BindListBudget()
        {
            ListBudgetGrid.DataSource = MRPClass.MRP_ListBudget();
            ListBudgetGrid.KeyFieldName = "PK";
            ListBudgetGrid.DataBind();

        }

        protected void ListBudgetGrid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if(e.ButtonID == "BudgetGridEdit")
            {
                string doc_number = ListBudgetGrid.GetRowValues(ListBudgetGrid.FocusedRowIndex, "DocNumber").ToString();
                string work_line = ListBudgetGrid.GetRowValues(ListBudgetGrid.FocusedRowIndex, "WorkLine").ToString();
                ASPxWebControl.RedirectOnCallback("mrp_preview.aspx?DocNum=" + doc_number.ToString() + "&WrkFlwLn=" + work_line.ToString());
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