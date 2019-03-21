using DevExpress.Web;
using HijoPortal.classes;
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
    public partial class mrp_preview : System.Web.UI.Page
    {

        private static int mrp_key = 0, wrkflwln = 0, iStatusKey = 0;
        private static int
            PK_MAT = 0,
            PK_OPEX = 0,
            PK_MAN = 0,
            PK_CAPEX = 0,
            PK_REV = 0;
        private static string itemcommand = "", entitycode = "", buCode = "", docnumber = "";
        private const string matstring = "Materials", opexstring = "Opex", manstring = "Manpower", capexstring = "Capex", revstring = "Revenue";
        private static DateTime dateCreated;

        protected void MatListview_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell actTH = (HtmlTableCell)listview.FindControl("actTH");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");

            if (entitycode != Constants.TRAIN_CODE())
            {
                revth.Visible = false;
                actTH.Width = "7%";
                desc.Width = "35%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }
            else
            {
                actTH.Width = "7%";
                desc.Width = "25%";
                revth.Width = "10%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalDM.Style.Add("width", "80%");
            TAMat.Style.Add("width", "20%");
        }

        protected void MatListview_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;

                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");

                HtmlTableCell act = (HtmlTableCell)cell.FindControl("act");
                HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                HtmlTableCell desc = (HtmlTableCell)cell.FindControl("sec");
                HtmlTableCell uom = (HtmlTableCell)cell.FindControl("third");
                HtmlTableCell qty = (HtmlTableCell)cell.FindControl("fourth");
                HtmlTableCell cost = (HtmlTableCell)cell.FindControl("fifth");
                HtmlTableCell total_one = (HtmlTableCell)cell.FindControl("six");

                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode");
                if (!string.IsNullOrEmpty(code))
                {
                    cell.Attributes.Add("class", "no_border");

                    if (entitycode != Constants.TRAIN_CODE())
                        act.ColSpan = 6;
                    else
                        act.ColSpan = 7;

                    act.Style.Add("font-weight", "bold");

                    if (entitycode == Constants.TRAIN_CODE())
                        tableDataRevDesc.Style.Add("display", "none");

                    desc.Style.Add("display", "none");
                    uom.Style.Add("display", "none");
                    qty.Style.Add("display", "none");
                    cost.Style.Add("display", "none");
                    total_one.Style.Add("display", "none");
                }
            }

        }

        protected void OpexListiview_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell expTH = (HtmlTableCell)listview.FindControl("expTH");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");

            if (entitycode != Constants.TRAIN_CODE())
            {
                revth.Visible = false;
                expTH.Width = "7%";
                desc.Width = "35%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }
            else
            {
                expTH.Width = "7%";
                desc.Width = "25%";
                revth.Width = "10%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalOP.Style.Add("width", "64.5%");
            TAOpex.Style.Add("width", "10%");
        }

        protected void OpexListiview_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;

                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");

                HtmlTableCell act = (HtmlTableCell)cell.FindControl("act");
                HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                HtmlTableCell desc = (HtmlTableCell)cell.FindControl("sec");
                HtmlTableCell uom = (HtmlTableCell)cell.FindControl("third");
                HtmlTableCell qty = (HtmlTableCell)cell.FindControl("fourth");
                HtmlTableCell cost = (HtmlTableCell)cell.FindControl("fifth");
                HtmlTableCell total_one = (HtmlTableCell)cell.FindControl("six");

                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ExpenseCodeName");
                if (!string.IsNullOrEmpty(code))
                {
                    cell.Attributes.Add("class", "no_border");

                    if (entitycode != Constants.TRAIN_CODE())
                        act.ColSpan = 6;
                    else
                        act.ColSpan = 7;

                    act.Style.Add("font-weight", "bold");

                    if (entitycode == Constants.TRAIN_CODE())
                        tableDataRevDesc.Style.Add("display", "none");

                    desc.Style.Add("display", "none");
                    uom.Style.Add("display", "none");
                    qty.Style.Add("display", "none");
                    cost.Style.Add("display", "none");
                    total_one.Style.Add("display", "none");
                }
            }
        }

        protected void ManListview_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell actTH = (HtmlTableCell)listview.FindControl("actTH");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");

            if (entitycode != Constants.TRAIN_CODE())
            {
                revth.Visible = false;
                actTH.Width = "7%";
                desc.Width = "35%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }
            else
            {
                actTH.Width = "7%";
                desc.Width = "25%";
                revth.Width = "10%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalManpower.Style.Add("width", "64.5%");
            TAManpower.Style.Add("width", "10%");

        }

        protected void ManListview_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                ListViewDataItem dataitem = (ListViewDataItem)e.Item;

                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");

                HtmlTableCell act = (HtmlTableCell)cell.FindControl("act");
                HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                HtmlTableCell desc = (HtmlTableCell)cell.FindControl("sec");
                HtmlTableCell uom = (HtmlTableCell)cell.FindControl("third");
                HtmlTableCell qty = (HtmlTableCell)cell.FindControl("fourth");
                HtmlTableCell cost = (HtmlTableCell)cell.FindControl("fifth");
                HtmlTableCell total_one = (HtmlTableCell)cell.FindControl("six");

                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode");
                if (!string.IsNullOrEmpty(code))
                {
                    cell.Attributes.Add("class", "no_border");

                    if (entitycode != Constants.TRAIN_CODE())
                        act.ColSpan = 9;
                    else
                        act.ColSpan = 10;

                    act.Style.Add("font-weight", "bold");

                    if (entitycode == Constants.TRAIN_CODE())
                        tableDataRevDesc.Style.Add("display", "none");

                    desc.Style.Add("display", "none");
                    uom.Style.Add("display", "none");
                    qty.Style.Add("display", "none");
                    cost.Style.Add("display", "none");
                    total_one.Style.Add("display", "none");
                }
            }
        }

        protected void CapexListview_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");

            if (entitycode != Constants.TRAIN_CODE())
            {
                revth.Visible = false;
                desc.Width = "42%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }
            else
            {
                desc.Width = "32%";
                revth.Width = "10%";
                uom.Width = "7%";
                qty.Width = "16%";
                cost.Width = "15%";
                total.Width = "20%";
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalCapex.Style.Add("width", "64.5%");
            TotalAmountTD.Style.Add("width", "10%");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (wrkflwln == 0)
            {
                if (iStatusKey == 1)
                {

                    //MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    PopupSubmitPreview.ShowOnPageLoad = false;

                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                    MRPSubmitClass.MRP_Submit(docnumber.ToString(), mrp_key, dateCreated, wrkflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    Submit.Enabled = false;
                    //btAddEdit.Enabled = false;
                    Load_MRP(docnumber);

                    MRPNotificationMessage.Text = MRPClass.successfully_submitted;
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                }
            }
            else
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
                    }

                    if (isAllowed == true)
                    {
                        PopupSubmitPreview.ShowOnPageLoad = false;
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
        }

        protected void btAddEdit_Click(object sender, EventArgs e)
        {
            //if (wrkflwln == 0 || wrkflwln == 1)
            //{
                Response.Redirect("mrp_addedit.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=" + wrkflwln.ToString());
            //} else
            //{
            //    Response.Redirect("mrp_inventanalyst.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=" + wrkflwln.ToString());
            //}
            


        }

        protected void CapexListview_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
        }

        protected void RevListview_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell prod = (HtmlTableCell)listview.FindControl("prod");
            HtmlTableCell name = (HtmlTableCell)listview.FindControl("name");
            HtmlTableCell volume = (HtmlTableCell)listview.FindControl("volume");
            HtmlTableCell prize = (HtmlTableCell)listview.FindControl("prize");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");

            if (entitycode != Constants.TRAIN_CODE())
            {
                revth.Visible = false;
                prod.Width = "40%";
                name.Width = "15";
                volume.Width = "15%";
                prize.Width = "15%";
                total.Width = "15%";
            }
            else
            {
                prod.Width = "30%";
                revth.Width = "10%";
                name.Width = "15%";
                volume.Width = "15%";
                prize.Width = "15%";
                total.Width = "15%";
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            if (entitycode != Constants.TRAIN_CODE())
            {
                LabelTARev.Style.Add("width", "40.%");
                TARevenue.Style.Add("width", "60%");
            }
            else
            {
                LabelTARev.Style.Add("width", "30.%");
                TARevenue.Style.Add("width", "70%");
            }
        }

        protected void RevListview_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
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

        private void HideTableData(ListViewItemEventArgs e)
        {
            if (entitycode != MRPClass.train_entity)
            {
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tableDataRevDesc");
                td.Visible = false;

                HtmlTableCell pk_td = (HtmlTableCell)e.Item.FindControl("pk_td");
                pk_td.Visible = false;
            }
        }


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

            //MRPClass.PrintString("ispostback");
            DataTable table = MRPClass.MRP_CAPEX(DocNum.Text.ToString(), entitycode);
            CapexListview.DataSource = table;
            CapexListview.DataBind();
            TotalAmountTD.InnerText = MRPClass.capex_total().ToString("N");

            //System.Web.UI.HtmlControls.HtmlTableRow r = new System.Web.UI.HtmlControls.HtmlTableRow();
            //System.Web.UI.HtmlControls.HtmlTableCell c = new System.Web.UI.HtmlControls.HtmlTableCell();
            //c.InnerText = "New Cell";
            //r.Cells.Add(c);

            //HtmlTable T = (HtmlTable)MatListview.Items[0].FindControl("prev");
            //T.Controls.Add(r);



            //DataTable tableMat = MRPClass.MRP_Direct_Materials(DocNum.Text.ToString(), entitycode);
            DataTable tableMat = MRPClass.Preview_DM(DocNum.Text.ToString(), entitycode);
            MatListview.DataSource = tableMat;
            MatListview.DataBind();
            TAMat.InnerText = MRPClass.materials_total().ToString("N");

            DataTable tableOpex = MRPClass.Preview_OP(DocNum.Text.ToString(), entitycode);
            OpexListiview.DataSource = tableOpex;
            OpexListiview.DataBind();
            TAOpex.InnerText = MRPClass.opex_total().ToString("N");

            DataTable tableManpower = MRPClass.Preview_MAN(DocNum.Text.ToString(), entitycode);
            ManListview.DataSource = tableManpower;
            ManListview.DataBind();
            TAManpower.InnerText = MRPClass.manpower_total().ToString("N");

            DataTable tableRevenue = MRPClass.MRP_Revenue(DocNum.Text.ToString(), entitycode);
            RevListview.DataSource = tableRevenue;
            RevListview.DataBind();
            TARevenue.InnerText = MRPClass.revenue_total().ToString("N");

            PreviewListSummary.DataSource = MRPClass.MRP_PrevTotalSummary(DocNum.Text.ToString(), entitycode);
            PreviewListSummary.DataBind();
            TotalAmountSummary.InnerText = MRPClass.Prev_Summary_Total();

            MRPClass.trial();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            CheckCreatorKey();

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            if (!IsPostBack)
            {
                //lblMRPDocNum.Text = Request.Params["DocNum"].ToString();

                mrpHead.InnerText = "M O P  Preview";

                btAddEdit.Visible = false;
                DocNum.Text = Request.Params["DocNum"].ToString();
                docnumber = Request.Params["DocNum"].ToString();
                wrkflwln = Convert.ToInt32(Request.Params["WrkFlwLn"].ToString());
                //MRPClass.PrintString("wrk:" + wrkflwln);

                if (wrkflwln == 0 || wrkflwln == 1)
                {
                    btAddEdit.Visible = true;
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

        protected void LogsBtn_Click(object sender, EventArgs e)
        {
            string tablename = "";
            int PK = 0;

            if (itemcommand == matstring)
            {
                tablename = MRPClass.MaterialsTableLogs();
                PK = PK_MAT;
            }
            else if (itemcommand == opexstring)
            {
                tablename = MRPClass.OpexTableLogs();
                PK = PK_OPEX;
            }
            else if (itemcommand == manstring)
            {
                tablename = MRPClass.ManpowerTableLogs();
                PK = PK_MAN;
            }
            else if (itemcommand == capexstring)
            {
                tablename = MRPClass.CapexTableLogs();
                PK = PK_CAPEX;
            }
            else if (itemcommand == revstring)
            {
                tablename = MRPClass.RevenueTableLogs();
                PK = PK_REV;
            }

            if (PK == 0)
                return;

            //Query if log exist
            string query = "SELECT COUNT(*) FROM " + tablename + " WHERE MasterKey = '" + PK + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand comm = new SqlCommand(query, conn);
            int count = Convert.ToInt32(comm.ExecuteScalar());
            //MRPClass.PrintString(tablename + PK + count + LogsMemo.Text);
            if (count > 0)//edit
            {
                string update = "UPDATE " + tablename + " SET [Remarks] = @Remarks WHERE [MasterKey] = '" + PK + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Remarks", LogsMemo.Text);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            else//add
            {
                string insert = "INSERT INTO " + tablename + " ([MasterKey], [UserKey], [Remarks]) VALUES (@MasterKey, @UserKey, @Remarks)";

                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@MasterKey", PK);
                cmd.Parameters.AddWithValue("@UserKey", Convert.ToInt32(Session["CreatorKey"]));
                cmd.Parameters.AddWithValue("@Remarks", LogsMemo.Text);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            conn.Close();

            LogsPopup.ShowOnPageLoad = false;
        }

        protected void CapexListview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            // save button Clicked
            if (e.CommandName == "Link")
            {
                itemcommand = capexstring;
                ListViewItem itemClicked = e.Item;
                // Find Controls/Retrieve values from the item  here
                Label c = (Label)itemClicked.FindControl("CapexID");
                PK_CAPEX = Convert.ToInt32(c.Text);

                string query = "SELECT [Remarks] FROM " + MRPClass.CapexTableLogs() + " WHERE MasterKey = '" + PK_CAPEX + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";
                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader reader = comm.ExecuteReader();
                bool empty = true;
                while (reader.Read())
                {
                    LogsMemo.Text = reader[0].ToString();
                    LogsMemo.Focus();
                    empty = false;
                }

                if (empty)
                {
                    LogsMemo.Enabled = true;
                    LogsMemo.Text = "";
                    LogsMemo.Focus();
                }
                conn.Close();

                LogsPopup.HeaderText = "Comment";
                LogsPopup.ShowOnPageLoad = true;
            }
        }

        protected void MatListview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Link")
            {
                itemcommand = matstring;
                ListViewItem itemClicked = e.Item;
                // Find Controls/Retrieve values from the item  here
                Label c = (Label)itemClicked.FindControl("MatID");
                PK_MAT = Convert.ToInt32(c.Text);

                string query = "SELECT [Remarks] FROM " + MRPClass.MaterialsTableLogs() + " WHERE MasterKey = '" + PK_MAT + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";

                //MRPClass.PrintString("CreatorKey: " + Session["CreatorKey"].ToString());
                //MRPClass.PrintString("PK_MAT: " + PK_MAT.ToString());

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader reader = comm.ExecuteReader();
                bool empty = true;
                while (reader.Read())
                {
                    LogsMemo.Text = reader[0].ToString();
                    LogsMemo.Focus();
                    empty = false;
                }

                if (empty)
                {
                    LogsMemo.Enabled = true;
                    LogsMemo.Text = "";
                    LogsMemo.Focus();
                }
                conn.Close();

                LogsPopup.HeaderText = "Comment";
                LogsPopup.ShowOnPageLoad = true;
            }
        }

        protected void OpexListiview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Link")
            {
                itemcommand = opexstring;
                ListViewItem itemClicked = e.Item;
                // Find Controls/Retrieve values from the item  here
                Label c = (Label)itemClicked.FindControl("OpexID");
                PK_OPEX = Convert.ToInt32(c.Text);

                string query = "SELECT [Remarks] FROM " + MRPClass.OpexTableLogs() + " WHERE MasterKey = '" + PK_OPEX + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader reader = comm.ExecuteReader();
                bool empty = true;
                while (reader.Read())
                {
                    LogsMemo.Text = reader[0].ToString();
                    LogsMemo.Focus();
                    empty = false;
                }

                if (empty)
                {
                    LogsMemo.Enabled = true;
                    LogsMemo.Text = "";
                    LogsMemo.Focus();
                }
                conn.Close();

                LogsPopup.HeaderText = "Comment";
                LogsPopup.ShowOnPageLoad = true;
            }
        }

        protected void ManListview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Link")
            {
                itemcommand = manstring;
                ListViewItem itemClicked = e.Item;
                // Find Controls/Retrieve values from the item  here
                Label c = (Label)itemClicked.FindControl("ManID");
                PK_MAN = Convert.ToInt32(c.Text);

                string query = "SELECT [Remarks] FROM " + MRPClass.ManpowerTableLogs() + " WHERE MasterKey = '" + PK_MAN + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader reader = comm.ExecuteReader();
                bool empty = true;
                while (reader.Read())
                {
                    LogsMemo.Text = reader[0].ToString();
                    LogsMemo.Focus();
                    empty = false;
                }

                if (empty)
                {
                    LogsMemo.Text = "";
                    LogsMemo.Focus();
                }
                conn.Close();

                LogsPopup.HeaderText = "Comment";
                LogsPopup.ShowOnPageLoad = true;
            }
        }
        protected void RevListview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Link")
            {
                itemcommand = revstring;
                ListViewItem itemClicked = e.Item;
                // Find Controls/Retrieve values from the item  here
                Label c = (Label)itemClicked.FindControl("RevID");
                PK_REV = Convert.ToInt32(c.Text);

                string query = "SELECT [Remarks] FROM " + MRPClass.RevenueTableLogs() + " WHERE MasterKey = '" + PK_REV + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader reader = comm.ExecuteReader();
                bool empty = true;
                while (reader.Read())
                {
                    LogsMemo.Text = reader[0].ToString();
                    LogsMemo.Focus();
                    empty = false;
                }

                if (empty)
                {
                    LogsMemo.Text = "";
                    LogsMemo.Focus();
                }
                conn.Close();

                LogsPopup.HeaderText = "Comment";
                LogsPopup.ShowOnPageLoad = true;
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