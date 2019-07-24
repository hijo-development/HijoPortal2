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
    public partial class hlsSOA : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddSOA_Click(object sender, EventArgs e)
        {
            CheckCreatorKey();
            ComboBoxYear.Text = "";
            ComboBoxWeekNum.Text = "";
            ComboBoxCustomer.Text = "";
            PopUpControlAddSOA.HeaderText = "Statement Of Account - Add";
            PopUpControlAddSOA.ShowOnPageLoad = true;

        }

        protected void ComboBoxYear_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = HLSSOA.HLSSOAYear();
            ListBoxColumn lv = new ListBoxColumn();
            lv.Width = 0;
            lv.FieldName = "sYear";
            lv.Caption = "Select Year";
            combo.Columns.Add(lv);

            combo.ValueField = "sYear";
            combo.TextField = "sYear";
            combo.DataBind();
            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
        }

        protected void ComboBoxWeekNum_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;

            combo.DataSource = HLSSOA.HLSSOAWeekNum(0);
            ListBoxColumn lv = new ListBoxColumn();
            lv.Width = 0;
            lv.FieldName = "sWeekNum";
            lv.Caption = "Select Week Number";
            combo.Columns.Add(lv);

            combo.ValueField = "sWeekNum";
            combo.TextField = "sWeekNum";
            combo.DataBind();
            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
        }

        protected void ComboBoxWeekNum_Callback(object sender, CallbackEventArgsBase e)
        {
            int sYear = Convert.ToInt32(ComboBoxYear.Text);
            ASPxComboBox combo = sender as ASPxComboBox;

            combo.DataSource = HLSSOA.HLSSOAWeekNum(sYear);
            ListBoxColumn lv = new ListBoxColumn();
            lv.Width = 0;
            lv.FieldName = "sWeekNum";
            lv.Caption = "Select Week Number";
            combo.Columns.Add(lv);

            combo.ValueField = "sWeekNum";
            combo.TextField = "sWeekNum";
            combo.DataBind();
            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.TextFormatString = "{0}";
        }

        protected void ComboBoxCustomer_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;

            combo.DataSource = HLSSOA.HLSSOACustomer(0, 0);
            ListBoxColumn lv = new ListBoxColumn();
            lv.Width = 100;
            lv.FieldName = "CustAccount";
            lv.Caption = "Customer Code";
            combo.Columns.Add(lv);

            ListBoxColumn lt1 = new ListBoxColumn();
            lt1.FieldName = "CustName";
            lt1.Caption = "Customer Name";
            lt1.Width = 200;
            combo.Columns.Add(lt1);

            combo.ValueField = "CustAccount";
            combo.TextField = "CustName";
            combo.DataBind();
            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            
            //combo.ValueType = typeof(String);
        }

        protected void ComboBoxCustomer_Callback(object sender, CallbackEventArgsBase e)
        {
            string sYear = ComboBoxYear.Text;
            string sWeekNum = ComboBoxWeekNum.Text;
            ASPxComboBox combo = sender as ASPxComboBox;

            if (!string.IsNullOrEmpty(sWeekNum))
                combo.DataSource = HLSSOA.HLSSOACustomer(Convert.ToInt32(sYear), Convert.ToInt32(sWeekNum));
            else
            {
                combo.DataSource = HLSSOA.HLSSOACustomer(Convert.ToInt32(sYear), 0);
            }

            ListBoxColumn lv = new ListBoxColumn();
            lv.Width = 100;
            lv.FieldName = "CustAccount";
            lv.Caption = "Customer Code";
            combo.Columns.Add(lv);

            //ListBoxColumn lt1 = new ListBoxColumn();
            //lt1.FieldName = "CustName";
            //lt1.Caption = "Customer Name";
            //lt1.Width = 200;
            //combo.Columns.Add(lt1);

            combo.ValueField = "CustAccount";
            combo.TextField = "CustName";
            combo.DataBind();
            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.TextFormatString = "{1}";
        }

        protected void ListBoxWaybill_Init(object sender, EventArgs e)
        {
            string sYear = ComboBoxYear.Text;
            string sWeekNum = ComboBoxWeekNum.Text;
            string sCustCode = TextBoxCustCode.Text;

            ASPxListBox list = sender as ASPxListBox;
            list.Columns.Clear();
            list.Items.Clear();

            //if (!string.IsNullOrEmpty(sWeekNum) && !string.IsNullOrEmpty(sCustCode))
            //{
            //    MRPClass.PrintString("pass init ---- ");

            //    list.DataSource = HLSSOA.HLSSOAWaybills(Convert.ToInt32(sYear), Convert.ToInt32(sWeekNum), sCustCode);

            //    ListBoxColumn l_value = new ListBoxColumn();
            //    l_value.FieldName = "Waybill";
            //    l_value.Caption = "Select Waybill";
            //    l_value.Width = 300;
            //    list.Columns.Add(l_value);

            //    list.ValueField = "Waybill";
            //    list.TextField = "Waybill";
            //    list.DataBind();
            //    list.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            //    list.ClientEnabled = true;
            //}

            list.DataSource = HLSSOA.HLSSOAWaybills(0, 0, "");

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "Waybill";
            l_value.Caption = "Select Waybill";
            l_value.Width = 300;
            list.Columns.Add(l_value);

            list.ValueField = "Waybill";
            list.TextField = "Waybill";
            list.DataBind();
            list.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            list.ClientEnabled = true;

        }

        protected void ListBoxWaybill_Callback(object sender, CallbackEventArgsBase e)
        {

            string sYear = ComboBoxYear.Text;
            string sWeekNum = ComboBoxWeekNum.Text;
            string sCustCode = TextBoxCustCode.Text;

            MRPClass.PrintString("Year : " + sYear);
            MRPClass.PrintString("WeekNum : " + sWeekNum);
            MRPClass.PrintString("CustCode : " + sCustCode);

            ASPxListBox list = sender as ASPxListBox;
            list.Columns.Clear();
            list.Items.Clear();

            if (!string.IsNullOrEmpty(sWeekNum) && !string.IsNullOrEmpty(sCustCode))
            {
                //MRPClass.PrintString("pass callback ---- ");

                //MRPClass.PrintString("Year : " + sYear);
                //MRPClass.PrintString("WeekNum : " + sWeekNum);
                //MRPClass.PrintString("CustCode : " + sCustCode);

                list.DataSource = HLSSOA.HLSSOAWaybills(Convert.ToInt32(sYear), Convert.ToInt32(sWeekNum), sCustCode);

                ListBoxColumn l_value = new ListBoxColumn();
                l_value.FieldName = "Waybill";
                l_value.Caption = "Select Waybill";
                l_value.Width = 300;
                list.Columns.Add(l_value);

                list.ValueField = "Waybill";
                list.TextField = "Waybill";
                list.DataBind();
                list.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                list.ClientEnabled = true;
            } 
        }

        protected void gridWayBill_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ((GridViewDataColumn)grid.Columns["Waybill"]).SortAscending();
        }

        protected void CallbackPanelWaybill_Callback(object sender, CallbackEventArgsBase e)
        {
            //string sYear = ComboBoxYear.Text;
            //string sWeekNum = ComboBoxWeekNum.Text;
            string sCustCode = TextBoxCustCode.Text;

            int iYear = 0;
            int iWeekNum = 0;
            if (!string.IsNullOrEmpty(ComboBoxYear.Text))
            {
                iYear = Convert.ToInt32(ComboBoxYear.Text);
            }
            if (!string.IsNullOrEmpty(ComboBoxWeekNum.Text ))
            {
                iWeekNum = Convert.ToInt32(ComboBoxWeekNum.Text);
            }

            gridWayBill.DataSource = HLSSOA.HLSSOAWaybills(iYear, iWeekNum, sCustCode);
            gridWayBill.KeyFieldName = "Waybill";
            gridWayBill.DataBind();
        }

        protected void BtnAddSOA_Click(object sender, EventArgs e)
        {

        }
    }
}