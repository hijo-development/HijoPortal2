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

        private static string docNum = "", PK = "0", entCode = "", buCode = "";
        private static int StatusKey = 0, CurrentWorkFlow = 0;
        private static DateTime dteCreated;

        protected void Page_Load(object sender, EventArgs e)
        {

            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                ASPxHiddenField entText = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "ASPxHiddenFieldEnt") as ASPxHiddenField;
                entText["hidden_value"] = Session["EntityCode"].ToString();
            }

            BindMRP(Convert.ToInt32(Session["viewAllMRP"]), Session["EntityCode"].ToString(), Session["BUCode"].ToString());

            if (!Page.IsAsync)
            {
                //ASPxHiddenFieldEnt

            }
        }

        private void CheckCreatorKey()
        {
            if (Session["CreatorKey"] == null)
            {
                //MRPClass.PrintString(Page.IsCallback.ToString());
                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback(MRPClass.DefaultPage());
                else
                    Response.Redirect("default.aspx");

                return;
            }
        }

        private void BindMRP(int ViewAll, string EntityCode, string BUCode)
        {
            //MRPClass.PrintString("MRP is bind");
            DataTable dtRecord = MRPClass.Master_MRP_List(ViewAll, EntityCode, BUCode);
            MainTable.DataSource = dtRecord;
            MainTable.KeyFieldName = "PK";
            MainTable.DataBind();

        }

        protected void MainTable_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            CheckCreatorKey();

            if (Session["CreatorKey"] == null)
                return;

            ASPxHiddenField text = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "MRPHiddenVal") as ASPxHiddenField;
            ASPxHiddenField Status = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "MRPHiddenValStatus") as ASPxHiddenField;
            ASPxHiddenField StatusLine = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "MRPHiddenValStatusLine") as ASPxHiddenField;
            //if (e.ButtonID == "Add")
            //{

            //}

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            docNum = MainTable.GetRowValues(MainTable.FocusedRowIndex, "DocNumber").ToString();
            PK = MainTable.GetRowValues(MainTable.FocusedRowIndex, "PK").ToString();
            entCode = MainTable.GetRowValues(MainTable.FocusedRowIndex, "EntityCode").ToString();
            buCode = MainTable.GetRowValues(MainTable.FocusedRowIndex, "BUCode").ToString();
            StatusKey = Convert.ToInt32(MainTable.GetRowValues(MainTable.FocusedRowIndex, "StatusKey").ToString());
            dteCreated = Convert.ToDateTime(MainTable.GetRowValues(MainTable.FocusedRowIndex, "DateCreated").ToString());
            CurrentWorkFlow = Convert.ToInt32(MainTable.GetRowValues(MainTable.FocusedRowIndex, "WorkflowStatusLine").ToString());

            string query = "SELECT COUNT(*) FROM [hijo_portal].[dbo].[tbl_MRP_List] WHERE CreatorKey = '" + Session["CreatorKey"].ToString() + "' AND PK = '" + PK + "'";

            SqlCommand comm = new SqlCommand(query, conn);
            int count = Convert.ToInt32(comm.ExecuteScalar());
            if (count > 0)
            {
                text["hidden_value"] = "Creator";
                if (e.ButtonID == "Edit")
                {
                    if (MainTable.FocusedRowIndex > -1)
                    {
                        string mrp_pk = MainTable.GetRowValues(MainTable.FocusedRowIndex, "PK").ToString();
                        string mrp_creator = MainTable.GetRowValues(MainTable.FocusedRowIndex, "CreatorKey").ToString();

                        Session["mrp_creator"] = mrp_creator;

                        Session["mrp_docNum"] = docNum.ToString();
                        Session["mrp_wrkLine"] = "0";

                        //DEBUGGING ONLY
                        Response.RedirectLocation = "mrp_addedit.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=0";

                    }
                }

                if (e.ButtonID == "Delete")
                {
                    //msgTrans.Text = "Pass Delete";
                    if (MainTable.FocusedRowIndex > -1)
                    {
                        Status["hidden_value"] = MainTable.GetRowValues(MainTable.FocusedRowIndex, "StatusKey").ToString();

                    }
                }

                if (e.ButtonID == "Submit")
                {
                    if (MainTable.FocusedRowIndex > -1)
                    {
                        //MRPClass.PrintString(StatusKey.ToString());

                        Status["hidden_value"] = MainTable.GetRowValues(MainTable.FocusedRowIndex, "StatusKey").ToString();


                    }
                }

                if (e.ButtonID == "Preview")
                {
                    string mrp_creator = MainTable.GetRowValues(MainTable.FocusedRowIndex, "CreatorKey").ToString();
                    Session["mrp_creator"] = mrp_creator;
                    if (StatusKey == 4)
                    {
                        Session["mrp_docNum"] = docNum.ToString();
                        Session["source"] = "0";
                        Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                    }
                    else if (StatusKey == 3)
                    {
                        Session["mrp_docNum"] = docNum.ToString();
                        Session["mrp_source"] = "0";
                        Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                    }
                    else if (StatusKey == 2)
                    {
                        if (CurrentWorkFlow == 0 || CurrentWorkFlow == 1)
                        {
                            Session["mrp_docNum"] = docNum.ToString();
                            Session["mrp_wrkLine"] = "0";
                            Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=0";
                        }
                        else
                        {
                            Session["mrp_docNum"] = docNum.ToString();
                            Session["mrp_source"] = "0";
                            Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                        }
                    }
                    else
                    {
                        Session["mrp_docNum"] = docNum.ToString();
                        Session["mrp_wrkLine"] = "0";
                        Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=0";
                    }
                }
            }
            else
            {
                if (e.ButtonID == "Edit" || e.ButtonID == "Submit" || e.ButtonID == "Delete")
                {
                    if (e.ButtonID == "Edit")
                    {
                        if (StatusKey == 2 )
                        {
                            if (GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBULead", DateTime.Now, entCode, buCode))
                            {
                                string mrp_creator = MainTable.GetRowValues(MainTable.FocusedRowIndex, "CreatorKey").ToString();
                                Session["mrp_creator"] = mrp_creator;

                                Session["mrp_docNum"] = docNum.ToString();
                                Session["mrp_wrkLine"] = CurrentWorkFlow;
                                Response.RedirectLocation = "mrp_addedit.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=" + CurrentWorkFlow.ToString();
                            } else
                            {
                                text["hidden_value"] = "InvalidCreator";
                            }
                        } else
                        {
                            text["hidden_value"] = "InvalidCreator";
                        }
                        
                    } else
                    {
                        text["hidden_value"] = "InvalidCreator";
                    }
                        
                } else if (e.ButtonID == "Preview")
                {
                    string mrp_creator = MainTable.GetRowValues(MainTable.FocusedRowIndex, "CreatorKey").ToString();
                    Session["mrp_creator"] = mrp_creator;
                    if (GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBULead", dteCreated, entCode, buCode) == true)
                    {
                        if (StatusKey == 4)
                        {
                            Session["mrp_docNum"] = docNum.ToString();
                            Session["mrp_source"] = "0";
                            Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                        }
                        else if (StatusKey == 3)
                        {
                            Session["mrp_docNum"] = docNum.ToString();
                            Session["mrp_source"] = "0";
                            Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                        }
                        else if (StatusKey == 2)
                        {
                            if (CurrentWorkFlow == 0 || CurrentWorkFlow == 1)
                            {
                                Session["mrp_docNum"] = docNum.ToString();
                                Session["mrp_wrkLine"] = CurrentWorkFlow.ToString();
                                Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=" + CurrentWorkFlow.ToString();
                            }
                            else
                            {
                                Session["mrp_docNum"] = docNum.ToString();
                                Session["mrp_source"] = "0";
                                Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                            }
                        }
                        else
                        {
                            Session["mrp_docNum"] = docNum.ToString();
                            Session["mrp_wrkLine"] = "0";
                            Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=0";
                        }
                    }
                    else if (GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPProcurementOfficer", dteCreated, entCode, buCode) == true)
                    {
                        if (StatusKey == 4)
                        {
                            Session["mrp_docNum"] = docNum.ToString();
                            Session["mrp_source"] = "0";
                            Response.RedirectLocation = "mrp_preview_procoff.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                        }
                        else
                        {
                            text["hidden_value"] = "InvalidCreator";
                        }
                    }
                    else
                    {
                        if (Session["CreatorKey"] == Session["mrp_creator"])
                        {
                            if (StatusKey == 4)
                            {
                                Session["mrp_docNum"] = docNum.ToString();
                                Session["mrp_source"] = "0";
                                Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                            }
                            else if (StatusKey == 3)
                            {
                                Session["mrp_docNum"] = docNum.ToString();
                                Session["mrp_source"] = "0";
                                Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                            }
                            else if (StatusKey == 2)
                            {
                                if (CurrentWorkFlow == 0 || CurrentWorkFlow == 1)
                                {
                                    Session["mrp_docNum"] = docNum.ToString();
                                    Session["mrp_wrkLine"] = "0";
                                    Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=0";
                                }
                                else
                                {
                                    Session["mrp_docNum"] = docNum.ToString();
                                    Session["mrp_source"] = "0";
                                    Response.RedirectLocation = "mrp_preview_approve.aspx?DocNum=" + docNum.ToString() + "&Source=0";
                                }
                            }
                            else
                            {
                                Session["mrp_docNum"] = docNum.ToString();
                                Session["mrp_wrkLine"] = "0";
                                Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docNum.ToString() + "&WrkFlwLn=0";
                            }
                        }
                        else
                        {
                            text["hidden_value"] = "InvalidCreator";
                        }
                    }
                }

            }
            conn.Close();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            CheckCreatorKey();

            if (GlobalClass.CheckWorkFlowSetup(DateTime.Now, Session["EntityCode"].ToString(), Session["BUCode"].ToString()) == true)
            {
                //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                Checkbox.CheckState = CheckState.Unchecked;
                MonthYearCombo.Text = "";
                Year.Text = "";
                Month.Text = "";
                PopUpControl.HeaderText = "MRP";
                PopUpControl.ShowOnPageLoad = true;

            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                WarningText.Text = GlobalClass.WorkFlowSetupMsg;
                WarningText.ForeColor = System.Drawing.Color.Red;
                WarningPopUp.HeaderText = "Alert";
                WarningPopUp.ShowOnPageLoad = true;
            }
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

        protected void MonthYearCombo_Init(object sender, EventArgs e)
        {
            if (Session["EntityCode"] != null && Session["BUCode"] != null)
            {
                string entitycode = Session["EntityCode"].ToString();
                string bucode = Session["BUCode"].ToString();
                ASPxComboBox combo = sender as ASPxComboBox;
                combo.DataSource = MRPClass.MonthYear(entitycode, bucode);

                ListBoxColumn lv = new ListBoxColumn();
                lv.Width = 0;
                lv.FieldName = "PK";
                combo.Columns.Add(lv);

                ListBoxColumn lt1 = new ListBoxColumn();
                lt1.Width = 0;
                lt1.FieldName = "DocNumber";
                combo.Columns.Add(lt1);

                ListBoxColumn lt2 = new ListBoxColumn();
                lt2.FieldName = "MRPMonth";
                lt2.Caption = "Month";
                combo.Columns.Add(lt2);

                ListBoxColumn lt3 = new ListBoxColumn();
                lt3.FieldName = "MRPYear";
                lt3.Caption = "Year";
                combo.Columns.Add(lt3);

                combo.ValueField = "PK";
                combo.TextField = "MRPMonth";
                combo.DataBind();
                combo.TextFormatString = "{2}-{3}";
                combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {

            CheckCreatorKey();

            if (Month.Value == null || Year.Value == null)
                return;

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            int CopyMOP = 0, PreMonth = 0, PreYear = 0;
            if (Checkbox.Checked)
            {
                CopyMOP = 1;
                string[] arr = MonthYearCombo.Text.ToString().Split('-');
                string arr_month = arr[0];
                //MRPClass.PrintString(MonthYearCombo.Text.ToString());
                string arr_year = arr[1];
                PreMonth = Convertion.MONTH_TO_INDEX(arr_month);
                PreYear = Convert.ToInt32(arr_year);
            }

            string month = Month.Value.ToString();
            string year = Year.Value.ToString();

            string sResult = MRPClass.Insert_MRP(month, year, Convert.ToInt32(Session["CreatorKey"].ToString()), Session["EntityCode"].ToString(), Session["BUCode"].ToString(), CopyMOP, PreMonth, PreYear, WarningPopUp, WarningText, PopUpControl);

            string[] txtSplit = sResult.Split('|');
            int iRes = Convert.ToInt32(txtSplit[0]);
            string docNumber = txtSplit[1].ToString();
            int mrpKey = Convert.ToInt32(txtSplit[2]);

            if (iRes == 1)
            {
                Session["mrp_creator"] = Session["CreatorKey"].ToString();
                PopUpControl.ShowOnPageLoad = false;
                Session["mrp_docNum"] = docNumber.ToString();
                Session["mrp_wrkLine"] = "0";
                Response.Redirect("mrp_addedit.aspx?DocNum=" + docNumber.ToString() + "&WrkFlwLn=0");
            }            
        }

        protected void MainTable_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            CheckCreatorKey();
            if (e.Parameters == "AddNew")
            {
                //ASPxHiddenField entText = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "ASPxHiddenFieldEnt") as ASPxHiddenField;
                if (Session["EntityCode"].ToString().Trim() != "")
                {
                    //MRPClass.PrintString("pass with entity");
                    //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                    PopUpControl.HeaderText = "MRP";
                    PopUpControl.ShowOnPageLoad = true;
                }
                else
                {
                    //MRPClass.PrintString("pass script");
                    //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "CheckEnt", "AddMOPCheckEntity();", true);
                }
            }
        }

        protected void OK_SUBMIT_Click(object sender, EventArgs e)
        {
            PopupSubmitMRPList.ShowOnPageLoad = false;
            //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            //MRPClass.Submit_MRP(docNum.ToString(), Convert.ToInt32(PK), 1, entCode, buCode, Convert.ToInt32(Session["CreatorKey"]));

            MRPSubmitClass.MRP_Submit(docNum.ToString(), Convert.ToInt32(PK), dteCreated, 0, entCode, buCode, Convert.ToInt32(Session["CreatorKey"]));


            BindMRP(Convert.ToInt32(Session["viewAllMRP"]), Session["EntityCode"].ToString(), Session["BUCode"].ToString());

            MRPNotify.HeaderText = "Info";
            MRPNotificationMessage.Text = "Successfully Submitted!";
            MRPNotify.ShowOnPageLoad = true;

            //ASPxHiddenField text = MainTable.FindHeaderTemplateControl(MainTable.Columns[0], "MRPHiddenVal") as ASPxHiddenField;
            //text["hidden_value"] = "submitted";
        }

        protected void OK_DELETE_Click(object sender, EventArgs e)
        {
            PopupDeleteMRPList.ShowOnPageLoad = false;
            //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            string PK = MainTable.GetRowValues(MainTable.FocusedRowIndex, "PK").ToString();
            string delete = "DELETE FROM [dbo].[tbl_MRP_List] WHERE [PK] ='" + PK + "'";

            try
            {
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                BindMRP(Convert.ToInt32(Session["viewAllMRP"]), Session["EntityCode"].ToString(), Session["BUCode"].ToString());

                MRPNotify.HeaderText = "Info";
                MRPNotificationMessage.Text = "Successfully Deleted!";
                MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
                MRPNotify.ShowOnPageLoad = true;

            }
            catch (SqlException ex)
            {
                conn.Close();

                MRPNotify.HeaderText = "Error";
                MRPNotificationMessage.Text = ex.ToString();
                MRPNotificationMessage.ForeColor = System.Drawing.Color.Red;
                MRPNotify.ShowOnPageLoad = true;
            }
        }
    }
}