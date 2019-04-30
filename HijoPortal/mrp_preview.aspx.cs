using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections;
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
                if (revth != null)
                {
                    revth.Visible = false;
                    actTH.Width = "7%";
                    desc.Width = "35%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
            }
            else
            {
                if (revth != null)
                {
                    actTH.Width = "7%";
                    desc.Width = "25%";
                    revth.Width = "10%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
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
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode").ToString();
                if (entitycode == Constants.TRAIN_CODE())
                {
                    string farm = (string)DataBinder.Eval(dataitem.DataItem, "RevDesc").ToString();

                    if (!string.IsNullOrEmpty(farm))
                    {
                        cell.Attributes.Add("class", "no_border");

                        tableDataRevDesc.ColSpan = 6;
                        tableDataRevDesc.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");

                        act.ColSpan = 6;
                        act.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }
                }
                else
                {
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

        }

        protected void OpexListiview_DataBound(object sender, EventArgs e)
        {
            //Modify the width of the table
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
                if (revth != null)
                {
                    revth.Visible = false;
                    expTH.Width = "7%";
                    desc.Width = "35%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
            }
            else
            {
                if (revth != null)
                {
                    expTH.Width = "7%";
                    desc.Width = "25%";
                    revth.Width = "10%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
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

                string code = (string)DataBinder.Eval(dataitem.DataItem, "ExpenseCodeName").ToString();
                if (entitycode == Constants.TRAIN_CODE())
                {
                    string farm = (string)DataBinder.Eval(dataitem.DataItem, "RevDesc").ToString();

                    if (!string.IsNullOrEmpty(farm))
                    {
                        cell.Attributes.Add("class", "no_border");

                        tableDataRevDesc.ColSpan = 6;
                        tableDataRevDesc.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");

                        act.ColSpan = 6;
                        act.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");

                        act.ColSpan = 6;
                        act.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }
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

            if (qty != null)
                qty.InnerText = Constants.Prev_Head_Count();

            if (entitycode != Constants.TRAIN_CODE())
            {
                if (revth != null)
                {
                    revth.Visible = false;
                    actTH.Width = "7%";
                    desc.Width = "35%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
            }
            else
            {
                if (revth != null)
                {
                    actTH.Width = "7%";
                    desc.Width = "25%";
                    revth.Width = "10%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
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
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode").ToString();
                if (entitycode == Constants.TRAIN_CODE())
                {
                    string farm = (string)DataBinder.Eval(dataitem.DataItem, "RevDesc").ToString();

                    if (!string.IsNullOrEmpty(farm))
                    {
                        cell.Attributes.Add("class", "no_border");

                        tableDataRevDesc.ColSpan = 6;
                        tableDataRevDesc.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");

                        act.ColSpan = 6;
                        act.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }
                }
                else
                {
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
                if (revth != null)
                {
                    revth.Visible = false;
                    desc.Width = "42%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
            }
            else
            {
                if (revth != null)
                {
                    desc.Width = "32%";
                    revth.Width = "10%";
                    uom.Width = "7%";
                    qty.Width = "16%";
                    cost.Width = "15%";
                    total.Width = "20%";
                }
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
                if (iStatusKey == 0)
                {

                    //MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    PopupSubmitPreview.ShowOnPageLoad = false;

                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                    MRPSubmitClass.MRP_Submit(docnumber.ToString(), mrp_key, dateCreated, wrkflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    Submit.Enabled = false;
                    //btAddEdit.Enabled = false;
                    Load_MRP(docnumber);

                    ModalPopupExtenderLoading.Hide();

                    MRPNotificationMessage.Text = MRPClass.successfully_submitted;
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                //    MRPNotificationMessage.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
                //    MRPNotificationMessage.ForeColor = System.Drawing.Color.Red;
                //    MRPNotify.HeaderText = "Info";
                //    MRPNotify.ShowOnPageLoad = true;

                //}
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

                        ModalPopupExtenderLoading.Hide();

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
            Session["mrp_docNum"] = docnumber.ToString();
            Session["mrp_wrkLine"] = wrkflwln.ToString();

            Response.Redirect("mrp_addedit.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=" + wrkflwln.ToString());
            //} else
            //{
            //    Response.Redirect("mrp_inventanalyst.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=" + wrkflwln.ToString());
            //}



        }

        protected void CapexListview_ItemDataBound(object sender, ListViewItemEventArgs e)
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

                if (entitycode == Constants.TRAIN_CODE())
                {
                    string farm = (string)DataBinder.Eval(dataitem.DataItem, "RevDesc").ToString();

                    if (!string.IsNullOrEmpty(farm))
                    {
                        cell.Attributes.Add("class", "no_border");

                        tableDataRevDesc.ColSpan = 6;
                        tableDataRevDesc.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }

                }
            }
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
                if (revth != null)
                {
                    revth.Visible = false;
                    prod.Width = "40%";
                    name.Width = "15";
                    volume.Width = "15%";
                    prize.Width = "15%";
                    total.Width = "15%";
                }
            }
            else
            {
                if (revth != null)
                {
                    prod.Width = "30%";
                    revth.Width = "10%";
                    name.Width = "15%";
                    volume.Width = "15%";
                    prize.Width = "15%";
                    total.Width = "15%";
                }
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
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;

                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");

                HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                HtmlTableCell desc = (HtmlTableCell)cell.FindControl("sec");
                HtmlTableCell uom = (HtmlTableCell)cell.FindControl("third");
                HtmlTableCell qty = (HtmlTableCell)cell.FindControl("fourth");
                HtmlTableCell cost = (HtmlTableCell)cell.FindControl("fifth");
                HtmlTableCell total_one = (HtmlTableCell)cell.FindControl("six");

                if (entitycode == Constants.TRAIN_CODE())
                {
                    string farm = (string)DataBinder.Eval(dataitem.DataItem, "RevDesc").ToString();

                    if (!string.IsNullOrEmpty(farm))
                    {
                        cell.Attributes.Add("class", "no_border");

                        tableDataRevDesc.ColSpan = 6;
                        tableDataRevDesc.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                    }

                }
                //else
                //{

                //    cell.Attributes.Add("class", "no_border");

                //    if (entitycode != Constants.TRAIN_CODE())
                //        act.ColSpan = 9;
                //    else
                //        act.ColSpan = 10;

                //    act.Style.Add("font-weight", "bold");

                //    if (entitycode == Constants.TRAIN_CODE())
                //        tableDataRevDesc.Style.Add("display", "none");

                //    desc.Style.Add("display", "none");
                //    uom.Style.Add("display", "none");
                //    qty.Style.Add("display", "none");
                //    cost.Style.Add("display", "none");
                //    total_one.Style.Add("display", "none");
                //}
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

        private void HideTableData(ListViewItemEventArgs e)
        {
            if (entitycode != Constants.TRAIN_CODE())
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

            //DocumentReport obj_Rpt = new DocumentReport();

            //string docnum = DocNum.Text.ToString();
            //DataTable dt = DirectMaterials(docnum);
            //obj_Rpt.Report.DataSource = dt;
            ////obj_Rpt.Report.DataMember = DS.Tables[0].TableName;
            //obj_Rpt.Report.DataMember = dt.TableName;

            //obj_Rpt.Col1.DataBindings.Add("Text", null, "RevDesc");
            //obj_Rpt.Col2.DataBindings.Add("Text", null, "Expense");
            //obj_Rpt.Col3.DataBindings.Add("Text", null, "Activtiy");
            //obj_Rpt.Col4.DataBindings.Add("Text", null, "Description");
            //obj_Rpt.Col5.DataBindings.Add("Text", null, "UOM");
            //obj_Rpt.Col6.DataBindings.Add("Text", null, "Qty");
            //obj_Rpt.Col7.DataBindings.Add("Text", null, "Cost");
            //obj_Rpt.Col8.DataBindings.Add("Text", null, "TotalCost");

            //obj_Rpt.DisplayName = "DocumentReport " + DateTime.Now;

            //WebDocViewPrev.OpenReport(obj_Rpt);

            //PreviewReport.EventNames.

            //CapexListview.DataSource = Preview.Preview_CA(docnum, entitycode);
            //CapexListview.DataBind();
            //TotalAmountTD.InnerText = Preview.preview_total_capex(docnum);

            //DataTable tableMat = Preview.Preview_DM(docnum, entitycode);
            //MatListview.DataSource = tableMat;
            //MatListview.DataBind();
            //TAMat.InnerText = Preview.preview_total_directmaterials(DocNum.Text.ToString());

            //DataTable tableOpex = Preview.Preview_OP(docnum, entitycode);
            //OpexListiview.DataSource = tableOpex;
            //OpexListiview.DataBind();
            //TAOpex.InnerText = Preview.preview_total_opex(docnum);

            //DataTable tableManpower = Preview.Preview_MAN(docnum, entitycode);
            //ManListview.DataSource = tableManpower;
            //ManListview.DataBind();
            //TAManpower.InnerText = Preview.preview_total_manpower(docnum);

            //DataTable tableRevenue = Preview.Preview_Revenue(docnum, entitycode);
            //RevListview.DataSource = tableRevenue;
            //RevListview.DataBind();
            //TARevenue.InnerText = Preview.preview_total_revenue(docnum);

            //PreviewListSummary.DataSource = Preview.MRP_PrevTotalSummary(docnum, entitycode);
            //PreviewListSummary.DataBind();
            //TotalAmountSummary.InnerText = Preview.Prev_Summary_Total();

            //MRPClass.trial();
        }

        public static DataTable REV(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("ProductName", typeof(string));
                dtTable.Columns.Add("FarmName", typeof(string));
                dtTable.Columns.Add("Prize", typeof(string));
                dtTable.Columns.Add("Volume", typeof(string));
                dtTable.Columns.Add("TotalPrize", typeof(string));
            }

            string farm_query = "[dbo].[RevenueAssumptionPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["OperatingUnit"] = row["OperUinit"].ToString();
                    dtRow["ProductName"] = row["ProductName"].ToString();
                    dtRow["FarmName"] = row["FarmName"].ToString();
                    dtRow["Prize"] = Convert.ToDouble(row["Prize"].ToString()).ToString("N");
                    dtRow["Volume"] = Convert.ToDouble(row["Volume"].ToString()).ToString("N");
                    dtRow["TotalPrize"] = Convert.ToDouble(row["TotalPrize"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        public static DataTable SUMMARY(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("Total", typeof(string));
            }

            string farm_query = "[dbo].[MRPSummaryPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["Sort"].ToString();
                    dtRow["Description"] = row["MRPGroup"].ToString();
                    dtRow["Total"] = Convert.ToDouble(row["EdittedTotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }



        public static DataTable DM(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Expense", typeof(string));
                dtTable.Columns.Add("Activity", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string farm_query = "[dbo].[DirectMaterialPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["OperatingUnit"] = row["OperUinit"].ToString();
                    dtRow["Activity"] = row["Activity"].ToString();
                    dtRow["Expense"] = row["ExpenseCode"].ToString();
                    dtRow["Descripiton"] = row["ItemDescription"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        protected void GridPreviewDM_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        protected void GridPreviewOP_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        protected void GridPreviewMAN_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        protected void GridPreviewCA_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        private void ModifyGridColumns(ASPxGridView grid)
        {
            grid.Columns["PK"].Visible = false;
            grid.Columns["Qty"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["Cost"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["TotalCost"].CellStyle.HorizontalAlign = HorizontalAlign.Right;

            if(entitycode != Constants.TRAIN_CODE())
                grid.Columns["OperatingUnit"].Visible = false;
        }

        protected void GridPreviewREV_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            grid.Columns["PK"].Visible = false;
            grid.Columns["Prize"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["Volume"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["TotalPrize"].CellStyle.HorizontalAlign = HorizontalAlign.Right;

            if (entitycode != Constants.TRAIN_CODE())
                grid.Columns["OperatingUnit"].Visible = false;
        }

        protected void GridPreviewSummary_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            grid.Columns["PK"].Visible = false;
            grid.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
        }

        public static DataTable OP(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Expense", typeof(string));
                dtTable.Columns.Add("ProcurementCategory", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string farm_query = "[dbo].[OPEXPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString(); ;
                    dtRow["OperatingUnit"] = row["OprUnit"].ToString();
                    dtRow["ProcurementCategory"] = row["ProcCat"].ToString();
                    dtRow["Expense"] = row["ExpenseCode"].ToString();
                    dtRow["Descripiton"] = row["OPEX_Description"].ToString();
                    dtRow["UOM"] = row["UOMDesc"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MAN(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Activity", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string farm_query = "[dbo].[ManPowerPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString(); ;
                    dtRow["OperatingUnit"] = row["OprUnitDesc"].ToString();
                    dtRow["Activity"] = row["ActivityName"].ToString();
                    dtRow["Descripiton"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOMDesc"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        public static DataTable CA(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Expense", typeof(string));
                dtTable.Columns.Add("ProcurementCategory", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            string farm_query = "[dbo].[CAPEXPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString(); ;
                    dtRow["OperatingUnit"] = row["OprUnit"].ToString();
                    dtRow["ProcurementCategory"] = row["ProdCat"].ToString();
                    dtRow["Descripiton"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        public static DataTable DirectMaterials(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("RevDesc", typeof(string));
                dtTable.Columns.Add("Activtiy", typeof(string));
                dtTable.Columns.Add("Expense", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
            }

            //Query for TRAIN
            //string farm_query = "SELECT DISTINCT dbo.vw_AXFindimBananaRevenue.VALUE, dbo.vw_AXFindimBananaRevenue.DESCRIPTION FROM dbo.tbl_MRP_List_DirectMaterials LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_DirectMaterials.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE";

            //string farm_query = "EXEC [dbo].[DirectMaterialPreview] @headerdocnum = N'"+ docnumber + "'";
            string farm_query = "[dbo].[DirectMaterialPreview]";

            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            reader = cmd.ExecuteReader();
            ArrayList farm_arr = new ArrayList();
            while (reader.Read())
            {
                farm_arr.Add(reader["VALUE"].ToString());
            }
            reader.Close();

            for (int a = 0; a < farm_arr.Count; a++)
            {
                string farmStr = farm_arr[a].ToString();
                if (farmStr == null)
                    farmStr = "";

                DataRow dtRow = dtTable.NewRow();
                dtRow = dtTable.NewRow();
                //dtRow["RevDesc"] = row["RevDesc"].ToString();
                dtRow["RevDesc"] = farmStr;
                dtTable.Rows.Add(dtRow);

                MRPClass.PrintString("farm:" + farmStr);

                string expense_query = "SELECT DISTINCT dbo.vw_AXExpenseAccount.MAINACCOUNTID, dbo.vw_AXExpenseAccount.NAME FROM   dbo.tbl_MRP_List_DirectMaterials LEFT OUTER JOIN dbo.vw_AXExpenseAccount ON dbo.tbl_MRP_List_DirectMaterials.ExpenseCode = dbo.vw_AXExpenseAccount.MAINACCOUNTID LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_DirectMaterials.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE OprUnit = '" + farmStr + "'";

                cmd = new SqlCommand(expense_query, cn);
                reader = cmd.ExecuteReader();
                ArrayList expense_arr = new ArrayList();
                while (reader.Read())
                {
                    expense_arr.Add(reader["MAINACCOUNTID"].ToString());
                }
                reader.Close();

                for (int b = 0; b < expense_arr.Count; b++)
                {
                    string expStr = expense_arr[b].ToString();
                    if (expStr == null)
                        expStr = "";

                    dtRow = dtTable.NewRow();
                    dtRow["Expense"] = expStr;
                    dtTable.Rows.Add(dtRow);

                    string activity_query = "SELECT DISTINCT dbo.vw_AXFindimActivity.VALUE AS Activity, dbo.vw_AXFindimActivity.DESCRIPTION FROM   dbo.tbl_MRP_List_DirectMaterials LEFT OUTER JOIN dbo.vw_AXFindimActivity ON dbo.tbl_MRP_List_DirectMaterials.ActivityCode = dbo.vw_AXFindimActivity.VALUE LEFT OUTER JOIN dbo.vw_AXExpenseAccount ON dbo.tbl_MRP_List_DirectMaterials.ExpenseCode = dbo.vw_AXExpenseAccount.MAINACCOUNTID LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_DirectMaterials.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE WHERE OprUnit = '" + farmStr + "' AND ExpenseCode = '" + expStr + "'";

                    cmd = new SqlCommand(activity_query, cn);
                    reader = cmd.ExecuteReader();
                    ArrayList activity_arr = new ArrayList();
                    while (reader.Read())
                    {
                        activity_arr.Add(reader["Activity"].ToString());
                    }
                    reader.Close();

                    for (int c = 0; c < activity_arr.Count; c++)
                    {
                        string actStr = activity_arr[c].ToString();
                        if (actStr == null)
                            actStr = "";

                        dtRow = dtTable.NewRow();
                        //dtRow["RevDesc"] = row["RevDesc"].ToString();
                        dtRow["Activtiy"] = actStr;
                        dtTable.Rows.Add(dtRow);

                        string qry = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.*, dbo.vw_AXExpenseAccount.NAME AS Expense, dbo.vw_AXFindimActivity.DESCRIPTION AS Activity, dbo.vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc FROM dbo.tbl_MRP_List_DirectMaterials LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_DirectMaterials.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE LEFT OUTER JOIN dbo.vw_AXFindimActivity ON dbo.tbl_MRP_List_DirectMaterials.ActivityCode = dbo.vw_AXFindimActivity.VALUE LEFT OUTER JOIN dbo.vw_AXExpenseAccount ON dbo.tbl_MRP_List_DirectMaterials.ExpenseCode = dbo.vw_AXExpenseAccount.MAINACCOUNTID " +
                            "WHERE OprUnit = '" + farmStr + "' AND ExpenseCode = '" + expStr + "' AND ActivityCode = '" + actStr + "'";

                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row == dt.Rows[0])
                                {
                                    //MRPClass.PrintString(row["RevDesc"].ToString());
                                    //dtRow = dtTable.NewRow();
                                    ////dtRow["RevDesc"] = row["RevDesc"].ToString();
                                    //dtRow["RevDesc"] = farmStr;
                                    //dtTable.Rows.Add(dtRow);

                                    //dtRow = dtTable.NewRow();
                                    ////dtRow["Expense"] = row["Expense"].ToString();
                                    //dtRow["Expense"] = expStr;
                                    //dtTable.Rows.Add(dtRow);

                                    //dtRow = dtTable.NewRow();
                                    ////dtRow["Activtiy"] = row["Activity"].ToString();
                                    //dtRow["Activtiy"] = actStr;
                                    //dtTable.Rows.Add(dtRow);

                                    dtRow = dtTable.NewRow();
                                    dtRow["RevDesc"] = "";
                                    dtRow["Activtiy"] = "";
                                    dtRow["Expense"] = "";
                                    string desc = row["ItemDescriptionAddl"].ToString();
                                    if (string.IsNullOrEmpty(desc))
                                        dtRow["Descripiton"] = row["ItemDescription"].ToString();
                                    else
                                        dtRow["Descripiton"] = row["ItemDescription"].ToString() + "(" + desc + ")";
                                    dtRow["UOM"] = row["UOM"].ToString();
                                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                                    dtTable.Rows.Add(dtRow);
                                }
                                else
                                {
                                    dtRow = dtTable.NewRow();
                                    dtRow["RevDesc"] = "";
                                    dtRow["Activtiy"] = "";
                                    dtRow["Expense"] = "";
                                    string desc = row["ItemDescriptionAddl"].ToString();
                                    if (string.IsNullOrEmpty(desc))
                                        dtRow["Descripiton"] = row["ItemDescription"].ToString();
                                    else
                                        dtRow["Descripiton"] = row["ItemDescription"].ToString() + "(" + desc + ")";
                                    dtRow["UOM"] = row["UOM"].ToString();
                                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
                                    dtTable.Rows.Add(dtRow);
                                }
                            }
                        }
                        dt.Clear();
                    }

                }
            }


            //string qry = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.*, dbo.vw_AXExpenseAccount.NAME AS Expense, dbo.vw_AXFindimActivity.DESCRIPTION AS Activity, dbo.vw_AXFindimBananaRevenue.DESCRIPTION AS RevDesc FROM dbo.tbl_MRP_List_DirectMaterials LEFT OUTER JOIN dbo.vw_AXFindimBananaRevenue ON dbo.tbl_MRP_List_DirectMaterials.OprUnit = dbo.vw_AXFindimBananaRevenue.VALUE LEFT OUTER JOIN dbo.vw_AXFindimActivity ON dbo.tbl_MRP_List_DirectMaterials.ActivityCode = dbo.vw_AXFindimActivity.VALUE LEFT OUTER JOIN dbo.vw_AXExpenseAccount ON dbo.tbl_MRP_List_DirectMaterials.ExpenseCode = dbo.vw_AXExpenseAccount.MAINACCOUNTID";

            //cmd = new SqlCommand(qry);
            //cmd.Connection = cn;
            //adp = new SqlDataAdapter(cmd);
            //adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        DataRow dtRow = dtTable.NewRow();
            //        dtRow["RevDesc"] = row["RevDesc"].ToString();
            //        dtRow["Activtiy"] = row["Activity"].ToString();
            //        dtRow["Expense"] = row["Expense"].ToString();
            //        string desc = row["ItemDescriptionAddl"].ToString();
            //        if (string.IsNullOrEmpty(desc))
            //            dtRow["Descripiton"] = row["ItemDescription"].ToString();
            //        else
            //            dtRow["Descripiton"] = row["ItemDescription"].ToString() + "(" + desc + ")";
            //        dtRow["UOM"] = row["UOM"].ToString();
            //        dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
            //        dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
            //        dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");
            //        dtTable.Rows.Add(dtRow);
            //    }
            //}
            //dt.Clear();
            cn.Close();

            return dtTable;
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
                //DocNum.Text = Request.Params["DocNum"].ToString();
                //docnumber = Request.Params["DocNum"].ToString();
                //wrkflwln = Convert.ToInt32(Request.Params["WrkFlwLn"].ToString());

                //Session["mrp_docNum"] = docnumber.ToString();
                //Session["mrp_wrkLine"] = wrkflwln.ToString();

                DocNum.Text = Session["mrp_docNum"].ToString();
                docnumber = Session["mrp_docNum"].ToString();
                wrkflwln = Convert.ToInt32(Session["mrp_wrkLine"]);

                //MRPClass.PrintString("wrk:" + wrkflwln);

                if (wrkflwln == 0 || wrkflwln == 1)
                {
                    btAddEdit.Visible = true;
                }

                //if (wrkflwln == 0)
                //{
                //    Submit.Text = "Submit";
                //}
                //else
                //{
                //    Submit.Text = "Submit & Approve";
                //}

                if (wrkflwln == 0)
                {
                    Submit.Text = "Submit";
                }
                else
                {
                    Submit.Text = "Confirm & Submit";
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



            //string farm_query = "[dbo].[OPEXPreview]";
            //SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            //cn.Open();
            //SqlCommand cmd = new SqlCommand(farm_query, cn);

            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            //cmd.Parameters.AddWithValue("@entity", entitycode);
            //SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(dr);

            //GridPreviewOP.DataSource = dt;
            //GridPreviewOP.DataBind();
            //cn.Close();
            BindAll();
        }

        private void BindAll()
        {
            string docnum = DocNum.Text.ToString();
            GridPreviewDM.DataSource = DM(docnum);
            GridPreviewDM.KeyFieldName = "PK";
            GridPreviewDM.DataBind();

            GridPreviewOP.DataSource = OP(docnum);
            GridPreviewOP.KeyFieldName = "PK";
            GridPreviewOP.DataBind();

            GridPreviewMAN.DataSource = MAN(docnum);
            GridPreviewMAN.KeyFieldName = "PK";
            GridPreviewMAN.DataBind();

            GridPreviewCA.DataSource = CA(docnum);
            GridPreviewCA.KeyFieldName = "PK";
            GridPreviewCA.DataBind();

            GridPreviewREV.DataSource = REV(docnum);
            GridPreviewREV.KeyFieldName = "PK";
            GridPreviewREV.DataBind();

            GridPreviewSummary.DataSource = SUMMARY(docnum);
            GridPreviewSummary.KeyFieldName = "PK";
            GridPreviewSummary.DataBind();


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