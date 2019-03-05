using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class home : System.Web.UI.Page
    {

        DataTable wrkTable = new DataTable();

        private void CheckSessionExpire()
        {
            if (Session["CreatorKey"] == null)
            {
                Response.Redirect("default.aspx");
                return;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            divWelcome.Visible = false;
            divWorkAssigned.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSessionExpire();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                wrkTable = MRPClass.MRP_Work_Assigned_To_Me(Convert.ToInt32(Session["CreatorKey"]));

                divWelcome.Visible = false;
                divWorkAssigned.Visible = false;

                if (wrkTable.Rows.Count > 0)
                {
                    divWorkAssigned.Visible = true;
                    BindHomeGrid(Convert.ToInt32(Session["CreatorKey"]));

                } else
                {
                    divWelcome.Visible = true;
                }

            }

            
        }

        private void BindHomeGrid(int usrKey)
        {
            HomeGrid.DataSource = MRPClass.MRP_Work_Assigned_To_Me(usrKey);
            HomeGrid.KeyFieldName = "PK";
            HomeGrid.DataBind();
        }

        protected void DocNumBtn_Init(object sender, EventArgs e)
        {

            string page = "";
            int wrkline = Convert.ToInt32(HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "LevelLine").ToString());
            if (wrkline == 1)
            {
                page = "mrp_addedit.aspx";
            }
            if (wrkline == 2)
            {
                page = "mrp_inventanalyst.aspx";
            }

            ASPxHyperLink link = sender as ASPxHyperLink;
            GridViewDataItemTemplateContainer container = link.NamingContainer as GridViewDataItemTemplateContainer;
            object value = container.Grid.GetRowValues(container.VisibleIndex, "DocNumber");
            link.NavigateUrl = page+ "?DocNum=" + value.ToString() + "&WrkFlwLn="+ wrkline.ToString();
        }

    }
}