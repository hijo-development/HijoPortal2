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
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
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
                Response.RedirectLocation = "mrp_finance.aspx?DocNum=" + doc_number.ToString();
            }
        }
    }
}