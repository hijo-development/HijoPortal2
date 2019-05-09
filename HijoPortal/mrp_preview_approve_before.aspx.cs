using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web;
using HijoPortal.classes;

namespace HijoPortal
{
    public partial class mrp_preview_approve_before : System.Web.UI.Page
    {
        private static int mrp_key = 0, wrkflwln = 0, iStatusKey = 0, iSource = 0;
        private static string itemcommand = "", entitycode = "", buCode = "", docnumber = "";

        protected void RevListView_DataBound(object sender, EventArgs e)
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

        protected void RevListView_ItemDataBound(object sender, ListViewItemEventArgs e)
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
            }
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

                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");

                HtmlTableCell act = (HtmlTableCell)cell.FindControl("act");
                HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                HtmlTableCell desc = (HtmlTableCell)cell.FindControl("sec");
                HtmlTableCell uom = (HtmlTableCell)cell.FindControl("third");
                HtmlTableCell qty = (HtmlTableCell)cell.FindControl("fourth");
                HtmlTableCell cost = (HtmlTableCell)cell.FindControl("fifth");
                HtmlTableCell total_one = (HtmlTableCell)cell.FindControl("six");
                HtmlTableCell qty_rec = (HtmlTableCell)cell.FindControl("sev");
                HtmlTableCell cost_two = (HtmlTableCell)cell.FindControl("eight");
                HtmlTableCell total_two = (HtmlTableCell)cell.FindControl("nine");
                HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");

                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode").ToString().ToString();
                if (entitycode == Constants.TRAIN_CODE())
                {
                    string farm = (string)DataBinder.Eval(dataitem.DataItem, "RevDesc").ToString();
                    if (!string.IsNullOrEmpty(farm))
                    {
                        cell.Attributes.Add("class", "no_border");

                        tableDataRevDesc.ColSpan = 10;
                        tableDataRevDesc.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");

                        act.ColSpan = 10;

                        act.Style.Add("font-weight", "bold");

                        //if (entitycode == Constants.TRAIN_CODE())
                        //tableDataRevDesc.Style.Add("display", "none");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
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
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }
                }
            }
        }

        protected void DMListView_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell actTH = (HtmlTableCell)listview.FindControl("actTH");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");
            HtmlTableCell recqty = (HtmlTableCell)listview.FindControl("recqty");
            HtmlTableCell cost_two = (HtmlTableCell)listview.FindControl("cost_two");
            HtmlTableCell total_two = (HtmlTableCell)listview.FindControl("total_two");

            if (entitycode != Constants.TRAIN_CODE())
            {
                if (revth != null)
                {
                    revth.Visible = false;
                    actTH.Width = "7%";
                    desc.Width = "35%";
                    uom.Width = "7%";
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
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
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
                }
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalDM.Style.Add("width", "64.5%");
            TotalDM.Style.Add("width", "10%");
            LabelTotalEDM.Style.Add("width", "15.5%");
            TotalEDM.Style.Add("width", "10%");
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

        //protected void OK_SUBMIT_Click(object sender, EventArgs e)
        //{

        //    CheckCreatorKey();

        //    if (MRPClass.MRP_Line_Status(mrp_key, wrkflwln) == 0)
        //    {
        //        bool isAllowed = false;
        //        switch (wrkflwln)

        //        {
        //            case 1:
        //                {
        //                    isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBULead", dateCreated, entitycode, buCode);
        //                    break;
        //                }
        //            case 2:
        //                {
        //                    isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
        //                    break;
        //                }
        //            //case 3:
        //            //    {
        //            //        isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget_PerEntBU", dateCreated, entitycode, buCode);
        //            //        break;
        //            //    }
        //            case 3:
        //                {
        //                    isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
        //                    break;
        //                }
        //        }

        //        if (isAllowed == true)
        //        {
        //            PopupSubmitPreviewAnal.ShowOnPageLoad = false;
        //            //MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

        //            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

        //            MRPSubmitClass.MRP_Submit(docnumber.ToString(), mrp_key, dateCreated, wrkflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

        //            Submit.Enabled = false;

        //            MRPNotificationMessage.Text = MRPClass.successfully_submitted;
        //            MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
        //            MRPNotify.HeaderText = "Info";
        //            MRPNotify.ShowOnPageLoad = true;
        //        }
        //        else
        //        {
        //            MRPNotificationMessage.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
        //            MRPNotificationMessage.ForeColor = System.Drawing.Color.Red;
        //            MRPNotify.HeaderText = "Info";
        //            MRPNotify.ShowOnPageLoad = true;
        //        }
        //    }
        //    else
        //    {
        //        //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

        //    }

        //    //}
        //}

        protected void OpexListView_ItemDataBound(object sender, ListViewItemEventArgs e)
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
                HtmlTableCell qty_rec = (HtmlTableCell)cell.FindControl("sev");
                HtmlTableCell cost_two = (HtmlTableCell)cell.FindControl("eight");
                HtmlTableCell total_two = (HtmlTableCell)cell.FindControl("nine");

                //Get the Name values
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
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");

                        act.ColSpan = 10;
                        act.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
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

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }

                }
            }
        }

        protected void OpexListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void OpexListView_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell expTH = (HtmlTableCell)listview.FindControl("expTH");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");
            HtmlTableCell recqty = (HtmlTableCell)listview.FindControl("recqty");
            HtmlTableCell cost_two = (HtmlTableCell)listview.FindControl("cost_two");
            HtmlTableCell total_two = (HtmlTableCell)listview.FindControl("total_two");

            if (entitycode != Constants.TRAIN_CODE())
            {
                if (revth != null)
                {
                    revth.Visible = false;
                    expTH.Width = "7%";
                    desc.Width = "35%";
                    uom.Width = "7%";
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
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
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
                }
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalOP.Style.Add("width", "64.5%");
            TotalOpex.Style.Add("width", "10%");
            LabelTotalEOP.Style.Add("width", "15.5%");
            TotalEOpex.Style.Add("width", "10%");
        }

        protected void ManListView_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell actTH = (HtmlTableCell)listview.FindControl("actTH");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");
            HtmlTableCell recqty = (HtmlTableCell)listview.FindControl("recqty");
            HtmlTableCell cost_two = (HtmlTableCell)listview.FindControl("cost_two");
            HtmlTableCell total_two = (HtmlTableCell)listview.FindControl("total_two");

            if (qty != null)
            {
                qty.InnerText = Constants.Prev_Head_Count();
                recqty.InnerText = Constants.Prev_Head_Count();
            }

            if (entitycode != Constants.TRAIN_CODE())
            {
                if (revth != null)
                {
                    revth.Visible = false;
                    actTH.Width = "7%";
                    desc.Width = "35%";
                    uom.Width = "7%";
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
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
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
                }
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalMan.Style.Add("width", "64.5%");
            TotalManpower.Style.Add("width", "10%");
            LabelTotalEMan.Style.Add("width", "15.5%");
            TotalEManpower.Style.Add("width", "10%");
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

                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");

                HtmlTableCell act = (HtmlTableCell)cell.FindControl("act");
                HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                HtmlTableCell desc = (HtmlTableCell)cell.FindControl("sec");
                HtmlTableCell uom = (HtmlTableCell)cell.FindControl("third");
                HtmlTableCell qty = (HtmlTableCell)cell.FindControl("fourth");
                HtmlTableCell cost = (HtmlTableCell)cell.FindControl("fifth");
                HtmlTableCell total_one = (HtmlTableCell)cell.FindControl("six");
                HtmlTableCell qty_rec = (HtmlTableCell)cell.FindControl("sev");
                HtmlTableCell cost_two = (HtmlTableCell)cell.FindControl("eight");
                HtmlTableCell total_two = (HtmlTableCell)cell.FindControl("nine");
                HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");

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
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }

                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");
                        act.ColSpan = 10;
                        act.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        cell.Attributes.Add("class", "no_border");

                        act.ColSpan = 9;
                        act.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }
                }
            }
        }

        protected void CapexListView_DataBound(object sender, EventArgs e)
        {
            ListView listview = sender as ListView;
            HtmlTableCell revth = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
            HtmlTableCell desc = (HtmlTableCell)listview.FindControl("desc");
            HtmlTableCell uom = (HtmlTableCell)listview.FindControl("uom");
            HtmlTableCell qty = (HtmlTableCell)listview.FindControl("qty");
            HtmlTableCell cost = (HtmlTableCell)listview.FindControl("cost");
            HtmlTableCell total = (HtmlTableCell)listview.FindControl("total");
            HtmlTableCell recqty = (HtmlTableCell)listview.FindControl("recqty");
            HtmlTableCell cost_two = (HtmlTableCell)listview.FindControl("cost_two");
            HtmlTableCell total_two = (HtmlTableCell)listview.FindControl("total_two");

            if (entitycode != Constants.TRAIN_CODE())
            {
                if (revth != null)
                {
                    revth.Visible = false;
                    desc.Width = "42%";
                    uom.Width = "7%";
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
                }
            }
            else
            {
                if (revth != null)
                {
                    desc.Width = "32%";
                    revth.Width = "10%";
                    uom.Width = "7%";
                    qty.Width = "8%";
                    cost.Width = "7.5%";
                    total.Width = "10%";
                    recqty.Width = "8%";
                    cost_two.Width = "7.5%";
                    total_two.Width = "10%";
                }
            }

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalCA.Style.Add("width", "64.5%");
            TotalCapex.Style.Add("width", "10%");
            LabelTotalECA.Style.Add("width", "15.5%");
            TotalECapex.Style.Add("width", "10%");
        }

        protected void CapexListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HideTableData(e);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;

                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");

                //HtmlTableCell act = (HtmlTableCell)cell.FindControl("act");
                HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                HtmlTableCell desc = (HtmlTableCell)cell.FindControl("sec");
                HtmlTableCell uom = (HtmlTableCell)cell.FindControl("third");
                HtmlTableCell qty = (HtmlTableCell)cell.FindControl("fourth");
                HtmlTableCell cost = (HtmlTableCell)cell.FindControl("fifth");
                HtmlTableCell total_one = (HtmlTableCell)cell.FindControl("six");
                HtmlTableCell qty_rec = (HtmlTableCell)cell.FindControl("sev");
                HtmlTableCell cost_two = (HtmlTableCell)cell.FindControl("eight");
                HtmlTableCell total_two = (HtmlTableCell)cell.FindControl("nine");

                if (entitycode == Constants.TRAIN_CODE())
                {
                    string farm = (string)DataBinder.Eval(dataitem.DataItem, "RevDesc").ToString();

                    if (!string.IsNullOrEmpty(farm))
                    {
                        cell.Attributes.Add("class", "no_border");

                        tableDataRevDesc.ColSpan = 9;
                        tableDataRevDesc.Style.Add("font-weight", "bold");

                        desc.Style.Add("display", "none");
                        uom.Style.Add("display", "none");
                        qty.Style.Add("display", "none");
                        cost.Style.Add("display", "none");
                        total_one.Style.Add("display", "none");
                        qty_rec.Style.Add("display", "none");
                        cost_two.Style.Add("display", "none");
                        total_two.Style.Add("display", "none");
                    }

                }
            }

        }

        protected void CapexListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //if (MRPClass.MRP_Line_Status(mrp_key, wrkflwln) == 0)
            //{
            //    bool isAllowed = false;
            //    switch (wrkflwln)
            //    {
            //        case 1:
            //            {
            //                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBULead", dateCreated, entitycode, buCode);
            //                break;
            //            }
            //        case 2:
            //            {
            //                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
            //                break;
            //            }
            //        //case 3:
            //        //    {
            //        //        isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget_PerEntBU", dateCreated, entitycode, buCode);
            //        //        break;
            //        //    }
            //        case 3:
            //            {
            //                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
            //                break;
            //            }
            //    }

            //    if (isAllowed == true)
            //    {
            //        PopupSubmitPreviewAnal.ShowOnPageLoad = false;
            //        //MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

            //        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            //        MRPSubmitClass.MRP_Submit(docnumber.ToString(), mrp_key, dateCreated, wrkflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

            //        Submit.Enabled = false;
            //        Load_MRP(docnumber);

            //        MRPNotificationMessage.Text = MRPClass.successfully_submitted;
            //        MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
            //        MRPNotify.HeaderText = "Info";
            //        MRPNotify.ShowOnPageLoad = true;
            //    }
            //    else
            //    {
            //        MRPNotificationMessage.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
            //        MRPNotificationMessage.ForeColor = System.Drawing.Color.Red;
            //        MRPNotify.HeaderText = "Info";
            //        MRPNotify.ShowOnPageLoad = true;
            //    }
            //}
            //else
            //{
            //    //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            //}


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

        protected void btAddEdit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("mrp_inventanalyst.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=" + wrkflwln.ToString());
        }

        protected void btMOPList_Click(object sender, EventArgs e)
        {
            if (iSource == 0)
            {
                Response.Redirect("mrp_list.aspx");
            }
            if (iSource == 1)
            {
                Session["mrp_docNum"] = docnumber.ToString();
                Session["mrp_wrkLine"] = "0";

                Response.Redirect("mrp_addedit.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=0");
                //Response.RedirectLocation = "mrp_addedit.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=0";
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
                           " FROM tbl_MRP_List LEFT OUTER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK " +
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
            string docnum = DocNum.Text.ToString();
            RevListView.DataSource = Preview.Preview_Revenue(docnum, entitycode);
            RevListView.DataBind();
            TARevenue.InnerText = Preview.preview_total_revenue(docnum);

            SummaryListView.DataSource = Preview.MRP_PrevTotalSummary(DocNum.Text.ToString(), entitycode);
            SummaryListView.DataBind();
            TotalSummary.InnerText = Preview.Prev_Summary_Total();

            DataTable tableMat = Preview.Preview_DM(DocNum.Text.ToString(), entitycode);
            DMListView.DataSource = tableMat;
            DMListView.DataBind();
            TotalDM.InnerText = Preview.preview_total_directmaterials(docnum);
            TotalEDM.InnerText = Preview.preview_requestedtotal_directmaterials(docnum);

            DataTable tableOpex = Preview.Preview_OP(DocNum.Text.ToString(), entitycode);
            OpexListView.DataSource = tableOpex;
            OpexListView.DataBind();
            TotalOpex.InnerText = Preview.preview_total_opex(docnum);
            TotalEOpex.InnerText = Preview.preview_requestedtotal_opex(docnum);

            DataTable tableManpower = Preview.Preview_MAN(DocNum.Text.ToString(), entitycode);
            ManListView.DataSource = tableManpower;
            ManListView.DataBind();
            TotalManpower.InnerText = Preview.preview_total_manpower(docnum);
            TotalEManpower.InnerText = Preview.preview_requestedtotal_manpower(docnum);

            CapexListView.DataSource = Preview.Preview_CA(docnum, entitycode);
            CapexListView.DataBind();
            TotalCapex.InnerText = Preview.preview_total_capex(docnum);
            TotalECapex.InnerText = Preview.preview_requestedtotal_capex(docnum);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            if (!Page.IsPostBack)
            {

                //DocNum.Text = Request.Params["DocNum"].ToString();
                //docnumber = Request.Params["DocNum"].ToString();
                ////wrkflwln = Convert.ToInt32(Request.Params["WrkFlwLn"].ToString());
                //iSource = Convert.ToInt32(Request.Params["Source"].ToString());

                //Session["mrp_docNum"] = docnumber.ToString();
                //Session["mrp_source"] = "1";
                DocNum.Text = Session["mrp_docNum"].ToString();
                docnumber = Session["mrp_docNum"].ToString();
                //wrkflwln = Convert.ToInt32(Request.Params["WrkFlwLn"].ToString());
                iSource = Convert.ToInt32(Session["mrp_source"]);

                mrpHead.InnerText = "M O P Preview";

                if (iSource == 0)
                {
                    btMOPList.Text = "M O P List";
                }
                if (iSource == 1)
                {
                    btMOPList.Text = "M O P Add/Edit";
                }

                Load_MRP(docnumber);

            }
        }
    }
}