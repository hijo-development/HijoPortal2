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
    public partial class mrp_previewforapproval : System.Web.UI.Page
    {
        private static int mrp_key = 0, appflwln = 0, iStatusKey = 0;
        private static int
            PK_MAT = 0,
            PK_OPEX = 0,
            PK_MAN = 0,
            PK_CAPEX = 0,
            PK_REV = 0;
        private static string itemcommand = "", entitycode = "", buCode = "", docnum = "";
        private const string matstring = "Materials", opexstring = "Opex", manstring = "Manpower", capexstring = "Capex", revstring = "Revenue";
        private static DateTime dateCreated;

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
            HtmlTableCell recqty = (HtmlTableCell)listview.FindControl("recqty");
            HtmlTableCell cost_two = (HtmlTableCell)listview.FindControl("cost_two");
            HtmlTableCell total_two = (HtmlTableCell)listview.FindControl("total_two");

            if (qty != null)
            {
                qty.InnerText = Constants.Prev_Req_Qty();
                recqty.InnerText = Constants.Prev_Rec_Qty();
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

            LabelTotalDM.Style.Add("width", "64.5%");
            TAMat.Style.Add("width", "10%");
            LabelTotalEDM.Style.Add("width", "15.5%");
            ETAMat.Style.Add("width", "10%");
        }

        private void HideHeader(object sender)
        {

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
            HtmlTableCell recqty = (HtmlTableCell)listview.FindControl("recqty");
            HtmlTableCell cost_two = (HtmlTableCell)listview.FindControl("cost_two");
            HtmlTableCell total_two = (HtmlTableCell)listview.FindControl("total_two");

            if (qty != null)
            {
                qty.InnerText = Constants.Prev_Req_Qty();
                recqty.InnerText = Constants.Prev_Rec_Qty();
            }

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
            TAOpex.Style.Add("width", "10%");
            LabelTotalEOP.Style.Add("width", "15.5%");
            ETAOpex.Style.Add("width", "10%");
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
                HtmlTableCell qty_rec = (HtmlTableCell)cell.FindControl("sev");
                HtmlTableCell cost_two = (HtmlTableCell)cell.FindControl("eight");
                HtmlTableCell total_two = (HtmlTableCell)cell.FindControl("nine");

                //Get the Name values
                string code = (string)DataBinder.Eval(dataitem.DataItem, "ExpenseCodeName").ToString().ToString();
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
            TAManpower.Style.Add("width", "10%");
            LabelTotalEMan.Style.Add("width", "15.5%");
            ETAManpower.Style.Add("width", "10%");
        }

        protected void RevListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

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

        protected void CapexListview_DataBound(object sender, EventArgs e)
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

            if (qty != null)
            {
                qty.InnerText = Constants.Prev_Req_Qty();
                recqty.InnerText = Constants.Prev_Rec_Qty();
            }

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
            TACapex.Style.Add("width", "10%");
            LabelTotalECA.Style.Add("width", "15.5%");
            ETACapex.Style.Add("width", "10%");
        }
        protected void CapexListview_ItemDataBound(object sender, ListViewItemEventArgs e)
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

        protected void CapexListview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            CheckCreatorKey();

            iStatusKey = MRPClass.MRP_ApprvLine_Status(mrp_key, appflwln);
            StatusHidden["hidden_preview_iStatusKey"] = iStatusKey;

            //MRPClass.PrintString(appflwln.ToString());

            if (iStatusKey == 0)
            {

                bool isAllowed = false;
                switch (appflwln)
                {
                    case 1:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPSCMLead", dateCreated);
                            break;
                        }
                    case 2:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPFinanceLead", dateCreated);
                            break;
                        }
                    case 3:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPExecutive", dateCreated);
                            break;
                        }
                }
                if (isAllowed == true)
                {
                    //MRPClass.PrintString("Approved");
                    PopupSubmitAppPreview.ShowOnPageLoad = false;
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                    MRPApproveClass.MRP_Approve(docnum.ToString(), mrp_key, dateCreated, appflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));
                    Submit.Enabled = false;
                    Load_MRP(docnum);

                    ModalPopupExtenderLoading.Hide();

                    MRPNotifyMsgPrevApp.Text = MRPClass.successfully_approved;
                    MRPNotifyMsgPrevApp.ForeColor = System.Drawing.Color.Black;
                    MRPNotifyPrevApp.HeaderText = "Info";
                    MRPNotifyPrevApp.ShowOnPageLoad = true;
                }
                else
                {
                    MRPNotifyMsgPrevApp.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
                    MRPNotifyMsgPrevApp.ForeColor = System.Drawing.Color.Red;
                    MRPNotifyPrevApp.HeaderText = "Info";
                    MRPNotifyPrevApp.ShowOnPageLoad = true;
                }

            }

            //else
            //{

            //    //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            //    //MRPNotificationMessage.Text = "Document already submitted to BU / SSU Lead for review.";
            //    //MRPNotify.HeaderText = "Alert";
            //    //MRPNotify.ShowOnPageLoad = true;

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

        private void Load_MRP(string docnum)
        {
            //string query = "SELECT TOP (100) PERCENT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, " +
            //                  " dbo.tbl_MRP_List.DateCreated, dbo.tbl_MRP_List.EntityCode, dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, " +
            //                  " dbo.tbl_MRP_List.BUCode, dbo.vw_AXOperatingUnitTable.NAME AS BUCodeDesc, dbo.tbl_MRP_List.MRPMonth, " +
            //                  " dbo.tbl_MRP_List.MRPYear, dbo.tbl_MRP_List.StatusKey, dbo.tbl_MRP_Status.StatusName, " +
            //                  " dbo.tbl_MRP_List.CreatorKey, dbo.tbl_MRP_List.LastModified " +
            //                  " FROM  dbo.tbl_MRP_List LEFT OUTER JOIN " +
            //                  " dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
            //                  " dbo.tbl_MRP_Status ON dbo.tbl_MRP_List.StatusKey = dbo.tbl_MRP_Status.PK LEFT OUTER JOIN " +
            //                  " dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID " +
            //                  " WHERE(dbo.tbl_MRP_List.DocNumber = '" + DocNum.Text.ToString().Trim() + "') " +
            //                  " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";

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
                           " WHERE dbo.tbl_MRP_List.DocNumber = '" + docnum + "' " +
                           " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DocNum.Text = reader["DocNumber"].ToString();
                //DateCreated.Text = reader["DateCreated"].ToString();
                dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                mrp_key = Convert.ToInt32(reader["PK"]);
                entitycode = reader["EntityCode"].ToString();
                EntityCode.Text = reader["EntityCodeDesc"].ToString();
                buCode = reader["BUCode"].ToString();
                BUCode.Text = reader["BUCodeDesc"].ToString();
                Month.Text = MRPClass.Month_Name(Int32.Parse(reader["MRPMonth"].ToString()));
                Year.Text = reader["MRPYear"].ToString();
                //Status.Text = reader["StatusName"].ToString();
                Creator.Text = EncryptionClass.Decrypt(reader["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(reader["Lastname"].ToString());
                Status.Text = reader["StatusName"].ToString();
            }
            reader.Close();
            conn.Close();

            iStatusKey = MRPClass.MRP_ApprvLine_Status(mrp_key, appflwln);
            StatusHidden["hidden_preview_iStatusKey"] = iStatusKey;
            WorkLineHidden["hidden_preview_wrkflwln"] = appflwln;
            BindAll();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                //docnum = Request.Params["DocNum"].ToString();
                //appflwln = Convert.ToInt32(Request.Params["ApprvLn"].ToString());
                //DocNum.Text = Request.Params["DocNum"].ToString();

                //Session["mrp_docNum"] = value.ToString();
                //Session["mrp_appLine"] = wrklineval.ToString();
                docnum = Session["mrp_docNum"].ToString();
                appflwln = Convert.ToInt32(Session["mrp_appLine"]);
                DocNum.Text = Session["mrp_docNum"].ToString();

                Load_MRP(docnum);


            }


        }

        private void BindAll()
        {
            MatListview.DataSource = Preview.Preview_DM(docnum, entitycode);
            MatListview.DataBind();
            TAMat.InnerText = Preview.preview_total_directmaterials(docnum);
            ETAMat.InnerText = Preview.preview_requestedtotal_directmaterials(docnum);

            OpexListiview.DataSource = Preview.Preview_OP(docnum, entitycode);
            OpexListiview.DataBind();
            TAOpex.InnerText = Preview.preview_total_opex(docnum);
            ETAOpex.InnerText = Preview.preview_requestedtotal_opex(docnum);

            ManListview.DataSource = Preview.Preview_MAN(docnum, entitycode);
            ManListview.DataBind();
            TAManpower.InnerText = Preview.preview_total_manpower(docnum);
            ETAManpower.InnerText = Preview.preview_requestedtotal_manpower(docnum);
            //ATAManpower.InnerText = MRPClass.manpower_approved_total().ToString("N");

            CapexListview.DataSource = Preview.Preview_CA(docnum, entitycode);
            CapexListview.DataBind();
            TACapex.InnerText = Preview.preview_total_capex(docnum);
            ETACapex.InnerText = Preview.preview_requestedtotal_capex(docnum);

            RevListview.DataSource = Preview.Preview_Revenue(DocNum.Text.ToString(), entitycode);
            RevListview.DataBind();
            TARevenue.InnerText = MRPClass.revenue_total().ToString("N");

            PreviewListSummary.DataSource = Preview.MRP_PrevTotalSummary(DocNum.Text.ToString(), entitycode);
            PreviewListSummary.DataBind();
            TotalAmountSummary.InnerText = Preview.Prev_Summary_Total();
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