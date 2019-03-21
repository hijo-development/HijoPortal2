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
    public partial class mrp_preview_inventanalyst : System.Web.UI.Page
    {
        private static int mrp_key = 0, wrkflwln = 0, iStatusKey = 0;
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
                    qty_rec.Style.Add("display", "none");
                    cost_two.Style.Add("display", "none");
                    total_two.Style.Add("display", "none");
                }

                //ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                ////Get the Name values
                //string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode");
                //if (!string.IsNullOrEmpty(code))
                //{
                //    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                //    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                //    td.ColSpan = 10;
                //    td.Style.Add("font-weight", "bold");
                //    td.Style.Add("border-right-color", "transparent");

                //    HtmlTableCell sec = (HtmlTableCell)cell.FindControl("sec");
                //    sec.Style.Add("display", "none");

                //    HtmlTableCell third = (HtmlTableCell)cell.FindControl("third");
                //    third.Style.Add("display", "none");

                //    HtmlTableCell fourth = (HtmlTableCell)cell.FindControl("fourth");
                //    fourth.Style.Add("display", "none");

                //    HtmlTableCell fifth = (HtmlTableCell)cell.FindControl("fifth");
                //    fifth.Style.Add("display", "none");

                //    HtmlTableCell six = (HtmlTableCell)cell.FindControl("six");
                //    six.Style.Add("display", "none");

                //    HtmlTableCell sev = (HtmlTableCell)cell.FindControl("sev");
                //    sev.Style.Add("display", "none");

                //    HtmlTableCell eight = (HtmlTableCell)cell.FindControl("eight");
                //    eight.Style.Add("display", "none");

                //    HtmlTableCell nine = (HtmlTableCell)cell.FindControl("nine");
                //    nine.Style.Add("display", "none");

                //    HtmlTableCell pin = (HtmlTableCell)cell.FindControl("pin");
                //    pin.Style.Add("display", "none");

                //    if (entitycode == Constants.TRAIN_CODE())
                //    {
                //        HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                //        tableDataRevDesc.Style.Add("display", "none");
                //    }

                //    HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");
                //    ImageButton pinImg = (ImageButton)td_last.FindControl("pinImg");
                //    pinImg.Visible = false;
                //    td_last.Style.Add("border-right-color", "transparent");

                //}
                //else
                //{
                //    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                //    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                //}


            }

            //HideTableData(e);
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    ListViewDataItem dataitem = (ListViewDataItem)e.Item;
            //    //Get the Name values
            //    string code = (string)DataBinder.Eval(dataitem.DataItem, "ActivityCode");
            //    if (!string.IsNullOrEmpty(code))
            //    {
            //        HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
            //        HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
            //        td.ColSpan = 10;
            //        td.Style.Add("font-weight", "bold");
            //        td.Style.Add("font-style", "italic");
            //        td.Style.Add("border-right-color", "transparent");

            //        HtmlTableCell sec = (HtmlTableCell)cell.FindControl("sec");
            //        sec.Style.Add("display", "none");

            //        HtmlTableCell third = (HtmlTableCell)cell.FindControl("third");
            //        third.Style.Add("display", "none");

            //        HtmlTableCell fourth = (HtmlTableCell)cell.FindControl("fourth");
            //        fourth.Style.Add("display", "none");

            //        HtmlTableCell fifth = (HtmlTableCell)cell.FindControl("fifth");
            //        fifth.Style.Add("display", "none");

            //        HtmlTableCell six = (HtmlTableCell)cell.FindControl("six");
            //        six.Style.Add("display", "none");

            //        HtmlTableCell sev = (HtmlTableCell)cell.FindControl("sev");
            //        sev.Style.Add("display", "none");

            //        HtmlTableCell eight = (HtmlTableCell)cell.FindControl("eight");
            //        eight.Style.Add("display", "none");

            //        HtmlTableCell nine = (HtmlTableCell)cell.FindControl("nine");
            //        nine.Style.Add("display", "none");

            //        HtmlTableCell pin = (HtmlTableCell)cell.FindControl("pin");
            //        pin.Style.Add("display", "none");

            //        if (entitycode == Constants.TRAIN_CODE())
            //        {
            //            HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
            //            tableDataRevDesc.Style.Add("display", "none");
            //        }

            //        HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");
            //        ImageButton pinImg = (ImageButton)td_last.FindControl("pinImg");
            //        pinImg.Visible = false;
            //        td_last.Style.Add("border-right-color", "transparent");

            //    }
            //    else
            //    {
            //        HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
            //        HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
            //    }
            //}
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
            else
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

            HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
            if (pk_th != null)
                pk_th.Visible = false;

            LabelTotalDM.Style.Add("width", "64.5%");
            TotalDM.Style.Add("width", "10%");
            LabelTotalEDM.Style.Add("width", "15.5%");
            TotalEDM.Style.Add("width", "10%");
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
                    //case 3:
                    //    {
                    //        isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget_PerEntBU", dateCreated, entitycode, buCode);
                    //        break;
                    //    }
                    case 3:
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
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ExpenseCodeName");
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


                //ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                ////Get the Name values
                //string code = (string)DataBinder.Eval(dataitem.DataItem, "ExpenseCodeName");
                //if (!string.IsNullOrEmpty(code))
                //{
                //    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                //    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                //    td.ColSpan = 10;
                //    td.Style.Add("font-weight", "bold");
                //    td.Style.Add("border-right-color", "transparent");

                //    HtmlTableCell sec = (HtmlTableCell)cell.FindControl("sec");
                //    sec.Style.Add("display", "none");

                //    HtmlTableCell third = (HtmlTableCell)cell.FindControl("third");
                //    third.Style.Add("display", "none");

                //    HtmlTableCell fourth = (HtmlTableCell)cell.FindControl("fourth");
                //    fourth.Style.Add("display", "none");

                //    HtmlTableCell fifth = (HtmlTableCell)cell.FindControl("fifth");
                //    fifth.Style.Add("display", "none");

                //    HtmlTableCell six = (HtmlTableCell)cell.FindControl("six");
                //    six.Style.Add("display", "none");

                //    HtmlTableCell sev = (HtmlTableCell)cell.FindControl("sev");
                //    sev.Style.Add("display", "none");

                //    HtmlTableCell eight = (HtmlTableCell)cell.FindControl("eight");
                //    eight.Style.Add("display", "none");

                //    HtmlTableCell nine = (HtmlTableCell)cell.FindControl("nine");
                //    nine.Style.Add("display", "none");

                //    HtmlTableCell pin = (HtmlTableCell)cell.FindControl("pin");
                //    pin.Style.Add("display", "none");

                //    if (entitycode == Constants.TRAIN_CODE())
                //    {
                //        HtmlTableCell tableDataRevDesc = (HtmlTableCell)cell.FindControl("tableDataRevDesc");
                //        tableDataRevDesc.Style.Add("display", "none");
                //    }

                //    HtmlTableCell td_last = (HtmlTableCell)cell.FindControl("pin");
                //    ImageButton pinImg = (ImageButton)td_last.FindControl("pinImg");
                //    pinImg.Visible = false;
                //    td_last.Style.Add("border-right-color", "transparent");

                //}
                //else
                //{
                //    HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("prev");
                //    HtmlTableCell td = (HtmlTableCell)cell.FindControl("act");
                //}
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
            else
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

            if (entitycode != Constants.TRAIN_CODE())
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
            else
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
                    qty_rec.Style.Add("display", "none");
                    cost_two.Style.Add("display", "none");
                    total_two.Style.Add("display", "none");
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
            else
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
                    //case 3:
                    //    {
                    //        isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget_PerEntBU", dateCreated, entitycode, buCode);
                    //        break;
                    //    }
                    case 3:
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

                HtmlTableCell pk_td = (HtmlTableCell)e.Item.FindControl("pk_td");
                pk_td.Visible = false;

            }
        }

        protected void btAddEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("mrp_inventanalyst.aspx?DocNum=" + docnumber.ToString() + "&WrkFlwLn=" + wrkflwln.ToString());
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
            TotalEDM.InnerText = MRPClass.material_edited_total().ToString("N");

            DataTable tableOpex = MRPClass.Preview_OP(DocNum.Text.ToString(), entitycode);
            OpexListView.DataSource = tableOpex;
            OpexListView.DataBind();
            TotalOpex.InnerText = MRPClass.opex_total().ToString("N");
            TotalEOpex.InnerText = MRPClass.opex_edited_total().ToString("N");

            DataTable tableManpower = MRPClass.Preview_MAN(DocNum.Text.ToString(), entitycode);
            ManListView.DataSource = tableManpower;
            ManListView.DataBind();
            TotalManpower.InnerText = MRPClass.manpower_total().ToString("N");
            TotalEManpower.InnerText = MRPClass.manpower_edited_total().ToString("N");

            CapexListView.DataSource = MRPClass.Preview_CA(DocNum.Text.ToString(), entitycode);
            CapexListView.DataBind();
            TotalCapex.InnerText = MRPClass.capex_total().ToString("N");
            TotalECapex.InnerText = MRPClass.capex_edited_total().ToString("N");
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
                //if (wrkflwln == 3)
                //{
                //    mrpHead.InnerText = "M O P Preview (Finance - Budget)";
                //}
                if (wrkflwln == 3)
                {
                    mrpHead.InnerText = "M O P Preview (Deliberation)";
                }

                if (wrkflwln == 2)
                {
                    //Submit.Text = "Submit";
                    Submit.Text = "Submit for Deliberation";
                } else if (wrkflwln == 3)
                {
                    Submit.Text = "Submit for Approval";
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