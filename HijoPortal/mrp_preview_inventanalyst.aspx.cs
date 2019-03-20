﻿using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_preview_inventanalyst : System.Web.UI.Page
    {
        private static int mrp_key = 0, wrkflwln = 0, iStatusKey = 0;
        private static string itemcommand = "", entitycode = "", buCode = "", docnumber = "";

        protected void RevListView_DataBound(object sender, EventArgs e)
        {
            HideHeader(sender);
        }

        protected void RevListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
        }

        protected void DMListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void DMListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode");
                if (!string.IsNullOrEmpty(code))
                {
                    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                    td.ColSpan = 10;
                    td.Style.Add("font-weight", "bold");
                    td.Style.Add("font-style", "italic");
                    td.Style.Add("border-right-color", "transparent");

                    HtmlTableCell sec = (HtmlTableCell)cell.FindControl("sec");
                    sec.Style.Add("display", "none");

                    HtmlTableCell third = (HtmlTableCell)cell.FindControl("third");
                    third.Style.Add("display", "none");

                    HtmlTableCell fourth = (HtmlTableCell)cell.FindControl("fourth");
                    fourth.Style.Add("display", "none");

                    HtmlTableCell fifth = (HtmlTableCell)cell.FindControl("fifth");
                    fifth.Style.Add("display", "none");

                    HtmlTableCell six = (HtmlTableCell)cell.FindControl("six");
                    six.Style.Add("display", "none");

                    HtmlTableCell sev = (HtmlTableCell)cell.FindControl("sev");
                    sev.Style.Add("display", "none");

                    HtmlTableCell eight = (HtmlTableCell)cell.FindControl("eight");
                    eight.Style.Add("display", "none");

                    HtmlTableCell nine = (HtmlTableCell)cell.FindControl("nine");
                    nine.Style.Add("display", "none");

                    HtmlTableCell pin = (HtmlTableCell)cell.FindControl("pin");
                    pin.Style.Add("display", "none");

                    if (entitycode == Constants.TRAIN_CODE())
                    {
                        HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                        tableDataRevDesc.Style.Add("display", "none");
                    }

                    HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");
                    ImageButton pinImg = (ImageButton)td_last.FindControl("pinImg");
                    pinImg.Visible = false;
                    td_last.Style.Add("border-right-color", "transparent");

                }
                else
                {
                    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                }
            }
        }

        protected void DMListView_DataBound(object sender, EventArgs e)
        {
            HideHeader(sender);
        }

        protected void OK_SUBMIT_Click(object sender, EventArgs e)
        {

            if (MRPClass.MRP_Line_Status(mrp_key, wrkflwln) == 0)
            {
                bool isAllowed = false;
                switch (wrkflwln)

                {
                    case 1:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBULead", dateCreated, entitycode, buCode);
                            break;
                        }
                    case 2:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
                            break;
                        }
                    case 3:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget_PerEntBU", dateCreated, entitycode, buCode);
                            break;
                        }
                    case 4:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
                            break;
                        }
                }

                if (isAllowed == true)
                {
                    PopupSubmitPreviewAnal.ShowOnPageLoad = false;
                    //MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                    MRPSubmitClass.MRP_Submit(docnumber.ToString(), mrp_key, dateCreated, wrkflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    Submit.Enabled = false;

                    MRPNotificationMessage.Text = MRPClass.successfully_submitted;
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
                else
                {
                    MRPNotificationMessage.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Red;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            }

            //}
        }

        protected void OpexListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ExpenseCodeName");
                MRPClass.PrintString("code: " + code);
                if (!string.IsNullOrEmpty(code))
                {
                    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                    td.ColSpan = 10;
                    td.Style.Add("font-weight", "bold");
                    td.Style.Add("font-style", "italic");
                    td.Style.Add("border-right-color", "transparent");

                    HtmlTableCell sec = (HtmlTableCell)cell.FindControl("sec");
                    sec.Style.Add("display", "none");

                    HtmlTableCell third = (HtmlTableCell)cell.FindControl("third");
                    third.Style.Add("display", "none");

                    HtmlTableCell fourth = (HtmlTableCell)cell.FindControl("fourth");
                    fourth.Style.Add("display", "none");

                    HtmlTableCell fifth = (HtmlTableCell)cell.FindControl("fifth");
                    fifth.Style.Add("display", "none");

                    HtmlTableCell six = (HtmlTableCell)cell.FindControl("six");
                    six.Style.Add("display", "none");

                    HtmlTableCell sev = (HtmlTableCell)cell.FindControl("sev");
                    sev.Style.Add("display", "none");

                    HtmlTableCell eight = (HtmlTableCell)cell.FindControl("eight");
                    eight.Style.Add("display", "none");

                    HtmlTableCell nine = (HtmlTableCell)cell.FindControl("nine");
                    nine.Style.Add("display", "none");

                    HtmlTableCell pin = (HtmlTableCell)cell.FindControl("pin");
                    pin.Style.Add("display", "none");

                    if (entitycode == Constants.TRAIN_CODE())
                    {
                        HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                        tableDataRevDesc.Style.Add("display", "none");
                    }

                    HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");
                    ImageButton pinImg = (ImageButton)td_last.FindControl("pinImg");
                    pinImg.Visible = false;
                    td_last.Style.Add("border-right-color", "transparent");

                }
                else
                {
                    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                }
            }
        }

        protected void OpexListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void OpexListView_DataBound(object sender, EventArgs e)
        {
            HideHeader(sender);
        }

        protected void ManListView_DataBound(object sender, EventArgs e)
        {
            HideHeader(sender);
        }

        protected void ManListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void ManListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode");
                if (!string.IsNullOrEmpty(code))
                {
                    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                    td.ColSpan = 10;
                    td.Style.Add("font-weight", "bold");
                    td.Style.Add("font-style", "italic");
                    td.Style.Add("border-right-color", "transparent");

                    HtmlTableCell sec = (HtmlTableCell)cell.FindControl("sec");
                    sec.Style.Add("display", "none");

                    HtmlTableCell third = (HtmlTableCell)cell.FindControl("third");
                    third.Style.Add("display", "none");

                    HtmlTableCell fourth = (HtmlTableCell)cell.FindControl("fourth");
                    fourth.Style.Add("display", "none");

                    HtmlTableCell fifth = (HtmlTableCell)cell.FindControl("fifth");
                    fifth.Style.Add("display", "none");

                    HtmlTableCell six = (HtmlTableCell)cell.FindControl("six");
                    six.Style.Add("display", "none");

                    HtmlTableCell sev = (HtmlTableCell)cell.FindControl("sev");
                    sev.Style.Add("display", "none");

                    HtmlTableCell eight = (HtmlTableCell)cell.FindControl("eight");
                    eight.Style.Add("display", "none");

                    HtmlTableCell nine = (HtmlTableCell)cell.FindControl("nine");
                    nine.Style.Add("display", "none");

                    HtmlTableCell pin = (HtmlTableCell)cell.FindControl("pin");
                    pin.Style.Add("display", "none");

                    if (entitycode == Constants.TRAIN_CODE())
                    {
                        HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                        tableDataRevDesc.Style.Add("display", "none");
                    }

                    HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");
                    ImageButton pinImg = (ImageButton)td_last.FindControl("pinImg");
                    pinImg.Visible = false;
                    td_last.Style.Add("border-right-color", "transparent");

                }
                else
                {
                    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                }
            }
        }

        protected void CapexListView_DataBound(object sender, EventArgs e)
        {
            HideHeader(sender);
        }

        protected void CapexListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
        }

        protected void CapexListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (MRPClass.MRP_Line_Status(mrp_key, wrkflwln) == 0)
            {
                bool isAllowed = false;
                switch (wrkflwln)
                {
                    case 1:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBULead", dateCreated, entitycode, buCode);
                            break;
                        }
                    case 2:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
                            break;
                        }
                    case 3:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget_PerEntBU", dateCreated, entitycode, buCode);
                            break;
                        }
                    case 4:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
                            break;
                        }
                }

                if (isAllowed == true)
                {
                    PopupSubmitPreviewAnal.ShowOnPageLoad = false;
                    //MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                    MRPSubmitClass.MRP_Submit(docnumber.ToString(), mrp_key, dateCreated, wrkflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    Submit.Enabled = false;
                    Load_MRP(docnumber);

                    MRPNotificationMessage.Text = MRPClass.successfully_submitted;
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
                else
                {
                    MRPNotificationMessage.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Red;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            }


        }

        private void HideTableData(ListViewItemEventArgs e)
        {
            if (entitycode != MRPClass.train_entity)
            {
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tableDataRevDesc");
                td.Visible = false;
                //visibility:hidden

                HtmlTableCell pk_td = (HtmlTableCell)e.Item.FindControl("pk_td");
                pk_td.Visible = false;

                extraDMTD.Visible = false;
                extraOPTD.Visible = false;
                extraMANTD.Visible = false;
                extraCATD.Visible = false;
                extraRevTD.Visible = false;

                HtmlTableCell sec = (HtmlTableCell)e.Item.FindControl("sec");
                if (sec != null)
                    sec.Style.Add("width", "45%");
            }
        }

        private void HideHeader(object sender)
        {
            //MRPClass.PrintString("hideheader");
            if (entitycode != MRPClass.train_entity)
            {
                ListView listview = sender as ListView;
                HtmlTableCell th = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
                if (th != null)
                    th.Visible = false;

                HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
                if (th != null)
                    pk_th.Visible = false;
            }
        }

        protected void RevListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        private static DateTime dateCreated;

        private void Load_MRP(string docnumber)
        {
            string query = "SELECT tbl_MRP_List.*, " +
                           " vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                           " vw_AXOperatingUnitTable.NAME AS BUCodeDesc, " +
                           " tbl_MRP_Status.StatusName, tbl_Users.Lastname, " +
                           " tbl_Users.Firstname, tbl_MRP_List.EntityCode, " +
                           " tbl_MRP_List.BUCode " +
                           " FROM tbl_MRP_List INNER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK " +
                           " LEFT OUTER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER " +
                           " LEFT OUTER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK " +
                           " LEFT OUTER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID " +
                           " WHERE dbo.tbl_MRP_List.DocNumber = '" + docnumber + "' " +
                           " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //DocNum.Text = reader["DocNumber"].ToString();
                //DateCreated.Text = reader["DateCreated"].ToString();
                mrp_key = Convert.ToInt32(reader["PK"]);
                entitycode = reader["EntityCode"].ToString();
                dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                EntityCode.Text = reader["EntityCodeDesc"].ToString();
                buCode = reader["BUCode"].ToString();
                BUCode.Text = reader["BUCodeDesc"].ToString();
                Month.Text = MRPClass.Month_Name(Int32.Parse(reader["MRPMonth"].ToString()));
                Year.Text = reader["MRPYear"].ToString();
                Creator.Text = EncryptionClass.Decrypt(reader["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(reader["Lastname"].ToString());
                Status.Text = reader["StatusName"].ToString();
                //Status.Text = reader["StatusName"].ToString();
            }
            reader.Close();
            conn.Close();

            iStatusKey = MRPClass.MRP_Line_Status(mrp_key, wrkflwln);
            StatusHidden["hidden_preview_iStatusKey"] = iStatusKey;
            WrkFlowHidden["hidden_preview_wrkflwln"] = wrkflwln;

            //MRPClass.PrintString(entitycode);
            RevListView.DataSource = MRPClass.MRP_Revenue(DocNum.Text.ToString(), entitycode);
            RevListView.DataBind();
            TARevenue.InnerText = MRPClass.revenue_total().ToString("N");

            SummaryListView.DataSource = MRPClass.MRP_PrevTotalSummary(DocNum.Text.ToString(), entitycode);
            SummaryListView.DataBind();
            TotalSummary.InnerText = MRPClass.Prev_Summary_Total();

            DataTable tableMat = MRPClass.Preview_DM(DocNum.Text.ToString(), entitycode);
            DMListView.DataSource = tableMat;
            DMListView.DataBind();
            TotalDM.InnerText = MRPClass.materials_total().ToString("N");

            DataTable tableOpex = MRPClass.Preview_OP(DocNum.Text.ToString(), entitycode);
            OpexListView.DataSource = tableOpex;
            OpexListView.DataBind();
            TotalOpex.InnerText = MRPClass.opex_total().ToString("N");

            DataTable tableManpower = MRPClass.Preview_MAN(DocNum.Text.ToString(), entitycode);
            ManListView.DataSource = tableManpower;
            ManListView.DataBind();
            TotalManpower.InnerText = MRPClass.manpower_total().ToString("N");

            CapexListView.DataSource = MRPClass.Preview_CA(DocNum.Text.ToString(), entitycode);
            CapexListView.DataBind();
            TotalCapex.InnerText = MRPClass.capex_total().ToString("N");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            if (!Page.IsPostBack)
            {

                DocNum.Text = Request.Params["DocNum"].ToString();
                docnumber = Request.Params["DocNum"].ToString();
                wrkflwln = Convert.ToInt32(Request.Params["WrkFlwLn"].ToString());

                if (wrkflwln == 2)
                {
                    mrpHead.InnerText = "M O P Preview (Inventory Analyst)";
                }
                if (wrkflwln == 3)
                {
                    mrpHead.InnerText = "M O P Preview (Finance - Budget)";
                }
                if (wrkflwln == 4)
                {
                    mrpHead.InnerText = "M O P Preview (Deliberation)";
                }

                if (wrkflwln == 0)
                {
                    Submit.Text = "Submit";
                }
                else
                {
                    Submit.Text = "Submit & Approve";
                }

                Load_MRP(docnumber);

                //string query = "SELECT TOP (100) PERCENT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, " +
                //              " dbo.tbl_MRP_List.DateCreated, dbo.tbl_MRP_List.EntityCode, dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                //              " dbo.tbl_MRP_List.BUCode, dbo.vw_AXOperatingUnitTable.NAME AS BUCodeDesc, dbo.tbl_MRP_List.MRPMonth, " +
                //              " dbo.tbl_MRP_List.MRPYear, dbo.tbl_MRP_List.StatusKey, dbo.tbl_MRP_Status.StatusName, " +
                //              " dbo.tbl_MRP_List.CreatorKey, dbo.tbl_MRP_List.LastModified " +
                //              " FROM  dbo.tbl_MRP_List LEFT OUTER JOIN " +
                //              " dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                //              " dbo.tbl_MRP_Status ON dbo.tbl_MRP_List.StatusKey = dbo.tbl_MRP_Status.PK LEFT OUTER JOIN " +
                //              " dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID " +
                //              " WHERE(dbo.tbl_MRP_List.DocNumber = '" + DocNum.Text.ToString().Trim() + "') " +
                //              " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";



            }
        }
    }
}