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
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CreatorKey"] == null)
            {
                Response.Redirect("default.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }

            BindHomeGrid();
        }

        private void BindHomeGrid()
        {
            HomeGrid.DataSource = MRPClass.Master_MRP_List();
            HomeGrid.KeyFieldName = "PK";
            HomeGrid.DataBind();
        }

        protected void DocNumBtn_Init(object sender, EventArgs e)
        {

            string page = "";
            int i = 0;
            if (i == 0)
                page = "mrp_addedit.aspx";

            ASPxHyperLink link = sender as ASPxHyperLink;
            GridViewDataItemTemplateContainer container = link.NamingContainer as GridViewDataItemTemplateContainer;
            object value = container.Grid.GetRowValues(container.VisibleIndex, "DocNumber");
            link.NavigateUrl = page+ "?DocNum=" + value.ToString() + "&WrkFlwLn=1";
        }
    }
}