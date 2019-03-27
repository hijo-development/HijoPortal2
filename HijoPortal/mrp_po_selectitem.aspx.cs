﻿using System;
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
        private static string doc_static = "", year_static = "", prod_static = "";
        private static int month_static = -1;
        private bool executebindpageload = false;
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


            BindTable(doc_static, month_static, year_static, prod_static);
        }

        private void BindTable(string docnumber, int month, string year, string prod)
        {
            //if (month == -1) return;

            MainGrid_PO.DataSource = POClass.POSelecetedItemTable(docnumber, month, year, prod);
            MainGrid_PO.KeyFieldName = "PK";
            MainGrid_PO.DataBind();
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

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DocumentNumber";
            l_text.Caption = "Document Number";
            combo.Columns.Add(l_text);

            combo.ValueField = "DocumentNumber";
            combo.TextField = "DocumentNumber";
            combo.DataBind();
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = MainGrid_PO.GetSelectedFieldValues(new string[] { "PK", "TableIdentifier", "DocumentNumber", "ItemCode", "ItemDescription", "Qty", "Cost", "TotalCost" }) as List<object>;

            MRPClass.PrintString("--->>>" + fieldValues.Count.ToString());
            if (fieldValues.Count == 0)
                return;
            else
            {
                string userkey = Session["CreatorKey"].ToString();
                string delete = "DELETE FROM [dbo].[tbl_POCreation_Tmp] WHERE [UserKey] = '" + userkey + "'";

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();

                foreach (object[] obj in fieldValues)
                {
                    string[] arr = obj[0].ToString().Split('-');
                    string pk = arr[0];
                    string identifier = obj[1].ToString();
                    string docnum = obj[2].ToString();
                    string itemcode = obj[3].ToString();
                    string itemdesc = obj[4].ToString();
                    string qty = obj[5].ToString();
                    string cost = obj[6].ToString();
                    string totalcost = obj[7].ToString();

                    //MRPClass.PrintString(identifier);
                    //MRPClass.PrintString(docnum);
                    //MRPClass.PrintString(itemdesc);

                    string insert = "INSERT INTO [dbo].[tbl_POCreation_Tmp] ([UserKey], [MOPNumber], [ItemPK], [ItemIdentifier]) VALUES (@userkey, @mopnumber, @itempk, @itemidentifier)";

                    cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@userkey", userkey);
                    cmd.Parameters.AddWithValue("@mopnumber", docnum);
                    cmd.Parameters.AddWithValue("@itempk", pk);
                    cmd.Parameters.AddWithValue("@itemidentifier", identifier);
                    cmd.ExecuteNonQuery();
                }
                Response.Redirect("mrp_po_create.aspx");
            }
        }

        protected void ProdCategory_Combo_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.ProCategoryTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "NAME";
            l_value.Caption = "CODE";
            l_value.Width = 100;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            l_text.Width = 350;
            combo.Columns.Add(l_text);

            combo.ValueField = "NAME";
            combo.TextField = "DESCRIPTION";
            combo.DataBind();
            combo.TextFormatString = "{1}";
        }

        protected void MainGridCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            string month = "";
            year_static = "";
            month_static = 0;
            if (MonthYear_Combo.Value != null)
            {
                string monthyear = MonthYear_Combo.Text;
                string[] arr = monthyear.Split(' ');
                month = arr[0];
                year_static = arr[1];
                month_static = Convertion.MONTH_TO_INDEX(month);
            }

            if (MOPNum_Combo.Value != null)
                doc_static = MOPNum_Combo.Value.ToString();
            else
                doc_static = "";

            if (ProdCategory_Combo.Value != null)
                prod_static = ProdCategory_Combo.Value.ToString();
            else
                prod_static = "";

            executebindpageload = true;
            BindTable(doc_static, month_static, year_static, prod_static);

        }
    }
}