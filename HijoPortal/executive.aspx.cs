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

namespace HijoPortal
{
    public partial class executive : System.Web.UI.Page
    {
        private static bool bindExecutiveList = true;
        private static string sExecutiveKey = "";
        private static string sExecutiveStatusKey = "";  

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

        protected void FocusThisRowGrid(ASPxGridView grid, int keyVal)
        {
            grid.FocusedRowIndex = grid.FindVisibleIndexByKeyValue(keyVal);
        }

        private void BindExecutive()
        {
            DataTable dtRecord = FinanceClass.ExecutiveTable();
            grdExecutive.DataSource = dtRecord;
            grdExecutive.KeyFieldName = "PK";
            grdExecutive.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                if (GlobalClass.IsAdmin(Convert.ToInt32(Session["CreatorKey"])) == false)
                {
                    Response.Redirect("home.aspx");
                } else
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                }
                
            }

            if (bindExecutiveList)
            {
                BindExecutive();
            }
            else
            {
                bindExecutiveList = true;
            }
        }

        protected void Executive_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = AccountClass.UserListTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 0;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            combo.Value = sExecutiveKey.ToString();
        }

        protected void grdExecutive_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindExecutiveList = false;
            sExecutiveKey = "";
            sExecutiveStatusKey = "";

            ASPxLabel ctrlNum = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["Ctrl"], "ASPxCtrlTextBox") as ASPxLabel;
            ASPxDateEdit effectDate = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxLabel lastModified = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["LastModified"], "ASPxLastModifiedTextBox") as ASPxLabel;


            ctrlNum.Text = GlobalClass.GetControl_DocNum("Finance_Head", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
        }

        protected void grdExecutive_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox financeHead = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["UserCompleteName"], "Executive") as ASPxComboBox;
            ASPxComboBox excutiveStat = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("Finance_Head", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_Executive ([Ctrl], [EffectDate], [UserKey], [StatusKey], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @UserKey, @StatusKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@UserKey", financeHead.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", excutiveStat.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindExecutive();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_Executive ORDER BY [PK] DESC";
            SqlCommand comm = new SqlCommand(query_pk, conn);
            SqlDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                pk_latest = Convert.ToInt32(r[0].ToString());
            }
            conn.Close();
            if (pk_latest > 0)
            {
                FocusThisRowGrid(grid, pk_latest);
            }
        }

        protected void grdExecutive_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindExecutiveList = false;
            sExecutiveKey = grdExecutive.GetRowValues(grdExecutive.FocusedRowIndex, "UserKey").ToString();
            sExecutiveStatusKey = grdExecutive.GetRowValues(grdExecutive.FocusedRowIndex, "StatusKey").ToString();
        }

        protected void grdExecutive_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox financeHead = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["UserCompleteName"], "Executive") as ASPxComboBox;
            ASPxComboBox excutiveStat = grdExecutive.FindEditRowCellTemplateControl((GridViewDataColumn)grdExecutive.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_Executive " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [UserKey]= @BUHead, " +
                                " [StatusKey] = @StatusKey, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", financeHead.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", excutiveStat.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindExecutive();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdExecutive_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_Executive WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindExecutive();
            e.Cancel = true;
            conn.Close();
        }

        protected void grdExecutive_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void ExecutiveStatus_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = AccountClass.UserStatusTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;

            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 0;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            l_TextField.Caption = "STATUS";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            combo.Value = sExecutiveStatusKey.ToString();
        }
    }
}