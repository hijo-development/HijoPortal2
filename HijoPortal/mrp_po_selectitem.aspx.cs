using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using HijoPortal.classes;

namespace HijoPortal
{
    public partial class mrp_po_selectitem : System.Web.UI.Page
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
            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }
        }

        protected void MonthYear_Combo_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = POClass.MRPMonthYearTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PK";
            l_value.Width = 0;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "MRPMonth";
            l_text.Caption = "Month";
            combo.Columns.Add(l_text);

            ListBoxColumn l_text2 = new ListBoxColumn();
            l_text2.FieldName = "MRPYear";
            l_text2.Caption = "Year";
            combo.Columns.Add(l_text2);

            combo.ValueField = "PK";
            combo.TextField = "MRPMonth";
            combo.DataBind();
            combo.TextFormatString = "{1} {2}";
        }

        protected void MOPNum_Combo_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;

            string monthyear = MonthYear_Combo.Text;
            if (!string.IsNullOrEmpty(monthyear))
            {
                string[] strarr = monthyear.Split(' ');
                string month = strarr[0].ToString();
                string year = strarr[1].ToString();

                int monthindex = Convertion.MONTH_TO_INDEX(month);

                MRPClass.PrintString(monthindex.ToString());

                combo.DataSource = POClass.DocumentNumber_By_Month_Year(monthindex, year);

            }
            else
            {
                combo.DataSource = POClass.DocumentNumber_By_Month_Year(0, "");
            }

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PK";
            l_value.Caption = "PK";
            l_value.Width = 0;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DocumentNumber";
            l_text.Caption = "Document Number";
            combo.Columns.Add(l_text);

            combo.Value = "PK";
            combo.Text = "DocumentNumber";
            combo.DataBind();
            combo.TextFormatString = "{1}";
        }

        protected void MOPNum_Combo_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = POClass.DocumentNumber_By_Month_Year(0, "");
            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PK";
            l_value.Caption = "PK";
            l_value.Width = 0;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DocumentNumber";
            l_text.Caption = "Document Number";
            combo.Columns.Add(l_text);

            combo.Value = "PK";
            combo.Text = "DocumentNumber";
            combo.DataBind();
            combo.TextFormatString = "{1}";
        }
    }
}