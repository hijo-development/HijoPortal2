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

            ASPxHyperLink link = sender as ASPxHyperLink;
            GridViewDataItemTemplateContainer container = link.NamingContainer as GridViewDataItemTemplateContainer;
            object value = container.Grid.GetRowValues(container.VisibleIndex, "DocNumber");
            object wrklineval = container.Grid.GetRowValues(container.VisibleIndex, "LevelLine");
            object wrkflowtypeval = container.Grid.GetRowValues(container.VisibleIndex, "WorkflowType");
            Session["mrp_creator"] = container.Grid.GetRowValues(container.VisibleIndex, "CreatorKey");

            //object value = HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "DocNumber");
            //object wrklineval = HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "LevelLine");
            //object wrkflowtypeval = HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "WorkflowType");
            //Session["mrp_creator"] = HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "CreatorKey");



            if (Convert.ToInt32(wrkflowtypeval) == 1)
            {
                if (Convert.ToInt32(wrklineval) == 1)
                {
                    page = "mrp_addedit.aspx";
                }
                if (Convert.ToInt32(wrklineval) == 2)
                {
                    page = "mrp_inventanalyst.aspx";
                }
                if (Convert.ToInt32(wrklineval) == 3)
                {
                    page = "mrp_preview_inventanalyst.aspx";
                }
                if (Convert.ToInt32(wrklineval) == 4)
                {
                    //page = "mrp_preview_inventanalyst.aspx";
                    page = "mrp_inventanalyst.aspx";
                }
                //Session["mrp_docNum"] = value.ToString();
                //Session["mrp_wrkLine"] = wrklineval.ToString();
                link.NavigateUrl = page + "?DocNum=" + value.ToString() + "&WrkFlwLn=" + wrklineval.ToString();
            }
            else if (Convert.ToInt32(wrkflowtypeval) == 2)
            {
                page = "mrp_previewforapproval.aspx";
                //Session["mrp_docNum"] = value.ToString();
                //Session["mrp_appLine"] = wrklineval.ToString();
                link.NavigateUrl = page + "?DocNum=" + value.ToString() + "&ApprvLn=" + wrklineval.ToString();
            }

            
        }

        protected void HomeGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            CheckSessionExpire();

            if (Session["CreatorKey"] == null)
                return;

            string docNum = HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "DocNumber").ToString();
            int wrklineval = Convert.ToInt32(HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "LevelLine"));
            int wrkflowtypeval = Convert.ToInt32(HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "WorkflowType"));
            int creatorKey = Convert.ToInt32(HomeGrid.GetRowValues(HomeGrid.FocusedRowIndex, "CreatorKey"));

            Session["mrp_creator"] = creatorKey.ToString();

            if (Convert.ToInt32(wrkflowtypeval) == 1)
            {
                Session["mrp_docNum"] = docNum.ToString();
                Session["mrp_wrkLine"] = wrklineval.ToString();
                switch (Convert.ToInt32(wrklineval))
                {
                    case 1:
                        {
                            Response.RedirectLocation = "mrp_addedit.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=" + wrklineval.ToString();
                            break;
                        }
                    case 2:
                        {
                            Response.RedirectLocation = "mrp_inventanalyst.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=" + wrklineval.ToString();
                            break;
                        }
                    case 3:
                        {
                            Response.RedirectLocation = "mrp_preview_inventanalyst.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=" + wrklineval.ToString();
                            break;
                        }
                    case 4:
                        {
                            Response.RedirectLocation = "mrp_inventanalyst.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=" + wrklineval.ToString();
                            break;
                        }
                }
            }
            if (Convert.ToInt32(wrkflowtypeval) == 2)
            {
                Session["mrp_docNum"] = docNum.ToString();
                Session["mrp_appLine"] = wrklineval.ToString();
                Response.RedirectLocation = "mrp_previewforapproval.aspx?DocNum=" + docNum.ToString() + "&ApprvLn=" + wrklineval.ToString();
            }


        }
    }
}