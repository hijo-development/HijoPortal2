using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HijoPortal.classes;
using DevExpress.Web;
using System.Net.NetworkInformation;

namespace HijoPortal
{
    public partial class mrp_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                ASPxHiddenField entText = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "ASPxHiddenFieldEnt") as ASPxHiddenField;
                entText["hidden_value"] = Session["EntityCode"].ToString();
            }

            BindMRP();

            if (!Page.IsAsync)
            {
                //ASPxHiddenFieldEnt
                
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

        private void BindMRP()
        {
            //MRPClass.PrintString("MRP is bind");
            DataTable dtRecord = MRPClass.Master_MRP_List();
            MainTable.DataSource = dtRecord;
            MainTable.KeyFieldName = "PK";
            MainTable.DataBind();

        }

        protected void MainTable_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            CheckSessionExpire();

            ASPxHiddenField text = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "MRPHiddenVal") as ASPxHiddenField;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            string PK = MainTable.GetRowValues(MainTable.FocusedRowIndex, "PK").ToString();
            string query = "SELECT COUNT(*) FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE CreatorKey = '" + Session["CreatorKey"].ToString() + "' AND PK = '" + PK + "'";

            SqlCommand comm = new SqlCommand(query, conn);
            int count = Convert.ToInt32(comm.ExecuteScalar());

            if (count > 0)
            {
                if (e.ButtonID == "Edit")
                {
                    //msgTrans.Text = "Pass Edit";
                    if (MainTable.FocusedRowIndex > -1)
                    {
                        //Session["DocNumber"] = MainTable.GetRowValues(MainTable.FocusedRowIndex, "DocNumber").ToString();
                        string docNum = MainTable.GetRowValues(MainTable.FocusedRowIndex, "DocNumber").ToString();
                        string mrp_pk = MainTable.GetRowValues(MainTable.FocusedRowIndex, "PK").ToString();
                        Response.RedirectLocation = "mrp_addedit.aspx?DocNum=" + docNum.ToString();
                        //Response.RedirectLocation = "mrp_inventanalyst.aspx?DocNum=" + docNum.ToString();
                        //Response.RedirectLocation = "mrp_forapproval.aspx?DocNum=" + docNum.ToString();
                    }
                }

                if (e.ButtonID == "Delete")
                {
                    //msgTrans.Text = "Pass Delete";
                    if (MainTable.FocusedRowIndex > -1)
                    {
                        //string PK = MainTable.GetRowValues(MainTable.FocusedRowIndex, "PK").ToString();
                        string delete = "DELETE FROM [dbo].[tbl_MRP_List] WHERE [PK] ='" + PK + "'";
                        SqlCommand cmd = new SqlCommand(delete, conn);
                        cmd.ExecuteNonQuery();
                        BindMRP();
                    }
                }
            }
            else
            {
                if (e.ButtonID == "Edit")
                    text["hidden_value"] = "InvalidCreator";
            }

            if (e.ButtonID == "Preview")
            {
                //msgTrans.Text = "Pass Preview";
                string docNum = MainTable.GetRowValues(MainTable.FocusedRowIndex, "DocNumber").ToString();
                Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docNum.ToString();

                //Response.Redirect("mrp_preview.aspx?DocNum=" + docNum.ToString());
                //Response.Redirect("mrp_preview.aspx?DocNum=" + docNum.ToString());
            }

            conn.Close();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            CheckSessionExpire();

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            PopUpControl.HeaderText = "MRP";
            PopUpControl.ShowOnPageLoad = true;
        }

        protected void Month_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.Items.Add("January");
            combo.Items.Add("February");
            combo.Items.Add("March");
            combo.Items.Add("April");
            combo.Items.Add("May");
            combo.Items.Add("June");
            combo.Items.Add("July");
            combo.Items.Add("August");
            combo.Items.Add("September");
            combo.Items.Add("October");
            combo.Items.Add("November");
            combo.Items.Add("December");
        }

        protected void Year_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            int current_year = DateTime.Now.Year;
            int year = current_year + 10;
            for (int i = 0; i < 10; i++)
            {
                year = year - 1;
                combo.Items.Add(year.ToString());
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {

            CheckSessionExpire();

            if (Month.Value == null || Year.Value == null)
                return;

            string month = Month.Value.ToString();
            string year = Year.Value.ToString();
            int monthIndex = Convert.ToDateTime("01-" + month + "-2011").Month;
            int yearInteger = Convert.ToInt32(year);

            if (monthIndex <= Convert.ToInt32(DateTime.Now.Month.ToString("00")) && yearInteger <= Convert.ToInt32(DateTime.Now.Year.ToString()))
            {
                WarningPopUp.HeaderText = month + " " + year;
                WarningPopUp.ShowOnPageLoad = true;
                WarningText.Text = "Month and Year behind current date";
                return;
            }


            string query = "SELECT COUNT(*) FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE [MRPMonth] = @MONTH AND [MRPYear] = @YEAR AND [EntityCode] = @ENTITYCODE AND [BUCode] = @BUCODE";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MONTH", monthIndex);
            cmd.Parameters.AddWithValue("@YEAR", yearInteger);
            cmd.Parameters.AddWithValue("@ENTITYCODE", Session["EntityCode"].ToString());
            cmd.Parameters.AddWithValue("@BUCODE", Session["BUCode"].ToString());
            cmd.CommandType = CommandType.Text;
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
            {
                WarningPopUp.HeaderText = month + " " + year;
                WarningPopUp.ShowOnPageLoad = true;
                WarningText.Text = "Month and Year Already Exist";
            }
            else
            {
                string DocumentPrefix = "", DocumentNum = "", STATUS_NAME = "";
                int STATUS_KEY = 0;
                int MRP_MONTH = monthIndex;
                int MRP_YEAR = yearInteger;

                query = "SELECT [DocumentPrefix],[DocumentNum] FROM [hijo_portal].[dbo].[tbl_DocumentNumber] WHERE [DocumentPrefix] = 'MRP'";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DocumentPrefix = reader[0].ToString();
                    DocumentNum = reader[1].ToString();
                    MRPClass.PrintString("prefix:" + DocumentPrefix);
                }
                reader.Close();
                int doc_num = Int32.Parse(DocumentNum) + 1;
                string update_DocNumber = "UPDATE [dbo].[tbl_DocumentNumber] SET [DocumentNum] = '" + doc_num + "' WHERE [DocumentPrefix] = 'MRP'";
                command = new SqlCommand(update_DocNumber, conn);
                command.ExecuteNonQuery();

                string query_PK_STATUS = "SELECT [PK],[StatusName] FROM [hijo_portal].[dbo].[tbl_MRP_Status] WHERE [StatusName] = 'Draft'";
                command = new SqlCommand(query_PK_STATUS, conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    STATUS_KEY = Convert.ToInt32(reader[0].ToString());
                    STATUS_NAME = reader[1].ToString();

                }
                reader.Close();

                string monthStringTwoDigits = DateTime.Now.ToString("MM");
                string yearStringTwoDigits = DateTime.Now.ToString("yy");
                DateTime DATE_CREATED = DateTime.Now;
                string BU_CODE = Session["BUCode"].ToString();
                string CREATOR_KEY = Session["CreatorKey"].ToString();
                string ENTITY_CODE = Session["EntityCode"].ToString();
                string DOC_NUMBER = (doc_num).ToString("00000#");
                DOC_NUMBER = ENTITY_CODE + "-" + monthStringTwoDigits + yearStringTwoDigits + DocumentPrefix + "-" + DOC_NUMBER;

                string str = "INSERT INTO [dbo].[tbl_MRP_List] ([DocNumber], [DateCreated], [EntityCode], [BUCode], [MRPMonth], [MRPYear], [StatusKey], [CreatorKey], [LastModified]) VALUES (@DocNumber, @DateCreated, @EntityCode, @BUCode, @Month, @Year, @StatusKey, @CreatorKey, @LastModified)";

                cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@DocNumber", DOC_NUMBER);
                cmd.Parameters.AddWithValue("@DateCreated", DATE_CREATED);
                cmd.Parameters.AddWithValue("@EntityCode", ENTITY_CODE);
                cmd.Parameters.AddWithValue("@BUCode", BU_CODE);
                cmd.Parameters.AddWithValue("@Month", MRP_MONTH);
                cmd.Parameters.AddWithValue("@Year", MRP_YEAR);
                cmd.Parameters.AddWithValue("@StatusKey", STATUS_KEY);
                cmd.Parameters.AddWithValue("@CreatorKey", CREATOR_KEY);
                cmd.Parameters.AddWithValue("@LastModified", DATE_CREATED.ToString("MM/dd/yyyy hh:mm:ss tt"));
                cmd.CommandType = CommandType.Text;
                int result = cmd.ExecuteNonQuery();

                string remarks = "MRP-ADD";

                int pk_latest = 0;
                string query_pk = "SELECT TOP 1 [PK] FROM " + MRPClass.MOPTableName() + " ORDER BY [PK] DESC";
                SqlCommand comm = new SqlCommand(query_pk, conn);
                SqlDataReader r = comm.ExecuteReader();

                while (r.Read()) pk_latest = Convert.ToInt32(r[0].ToString());
                r.Close();

                if (result > 0) MRPClass.AddLogsMOPList(conn, pk_latest, remarks);

                //Session["DocNumber"] = DOC_NUMBER;
                string docNum = DOC_NUMBER;
                PopUpControl.ShowOnPageLoad = false;
                Response.Redirect("mrp_addedit.aspx?DocNum=" + docNum.ToString());
                //Response.RedirectLocation = "mrp_addedit.aspx?DocNum=" + docNum.ToString();
            }
            conn.Close();
        }

        private void CheckSessionExpire()
        {
            if (Session["CreatorKey"] == null)
            {
                Response.Redirect("default.aspx");
                return;
            }
        }

        protected void MainTable_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //MRPClass.PrintString("pass custom callback");
            //if (e.Parameters == "AddNew")
            //{
            //    CheckSessionExpire();
            //    //ASPxHiddenField entText = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "ASPxHiddenFieldEnt") as ASPxHiddenField;
            //    if (Session["EntityCode"].ToString().Trim() != "")
            //    {                    
            //        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            //        PopUpControl.HeaderText = "MRP";
            //        PopUpControl.ShowOnPageLoad = true;
            //    }
            //    else
            //    {
            //        MRPClass.PrintString("pass script");
            //        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "CheckEnt", "checkEntity();", true);
            //    }                
            //}
        }
    }
}