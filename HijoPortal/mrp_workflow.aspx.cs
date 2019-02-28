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
    public partial class mrp_workflow : System.Web.UI.Page
    {
        private static bool bindWorkflowMaster = true, bindWorkflowDetails = true;

        private void CheckSessionExpire()
        {
            if (Session["CreatorKey"] == null)
            {
                Response.Redirect("default.aspx");
                return;
            }
        }

        protected void FocusThisRowGrid(ASPxGridView grid, int keyVal)
        {
            grid.FocusedRowIndex = grid.FindVisibleIndexByKeyValue(keyVal);
        }

        private bool CheckDetailsExist(string PK)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string query = "SELECT COUNT(*) FROM tbl_System_Workflow_Details Where MasterKey = " + Convert.ToInt32(PK) + "";
            SqlCommand cmd = new SqlCommand(query, conn);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            if (count > 0)//if the workflow has details
                return true;
            else
                return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSessionExpire();

            BindWorkflow();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }
        }

        private void BindWorkflow()
        {
            MRPClass.PrintString("Workflow is bind");
            DataTable dtRecord = WorkflowClass.WorkflowMasterTable();
            grdWorkflowMaster.DataSource = dtRecord;
            grdWorkflowMaster.KeyFieldName = "PK";
            grdWorkflowMaster.DataBind();

        }

        protected void EntCode_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = GlobalClass.EntityTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 30;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "EntCode").ToString();
            }
        }

        protected void BUCode_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = GlobalClass.BUSSUTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 30;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "BUCode").ToString();
            }
        }

        protected void BUHead_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = AccountClass.UserList();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "PK";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 0;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "CompleteName";
            l_TextField.Caption = "Employee Name";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "PK";
            combo.TextField = "CompleteName";
            combo.DataBind();

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "BUHead").ToString();
            }
        }

        protected void grdWorkflowMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindWorkflowMaster = false;
        }

        protected void grdWorkflowMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("WorkflowMasterPageControl") as ASPxPageControl;

            ASPxDateEdit effectDate = pageControl.FindControl("EffectDate") as ASPxDateEdit;
            ASPxComboBox entCode = pageControl.FindControl("EntCode") as ASPxComboBox;
            ASPxComboBox buCode = pageControl.FindControl("BUCode") as ASPxComboBox;
            ASPxComboBox buHead = pageControl.FindControl("BUHead") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string sCtrlNum = "", sLastModified = DateTime.Now.ToString();

            string insert = "INSERT INTO tbl_System_Workflow ([Ctrl], [EffectDate], [EntCode], [BUCode], [BUHead], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @EntCode, @BUCode, @BUHead, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@EntCode", entCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUCode", buCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", buHead.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindWorkflow();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_Workflow ORDER BY [PK] DESC";
            SqlCommand comm = new SqlCommand(query_pk, conn);
            SqlDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                pk_latest = Convert.ToInt32(r[0].ToString());
            }
            if (pk_latest > 0)
                FocusThisRowGrid(grid, pk_latest);

        }

        protected void grdWorkflowMaster_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            bool Exist = CheckDetailsExist(PK);
            if (Exist)//if the material has logs
            {
                conn.Close();
                e.Cancel = true;
            }
            else
            {
                string delete = "DELETE FROM tbl_System_Workflow WHERE [PK] ='" + PK + "'";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindWorkflow();
                e.Cancel = true;
            }
        }

        protected void grdWorkflowMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindWorkflowMaster = false;
        }

        protected void grdWorkflowMaster_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (grid.IsEditing || grid.IsNewRowEditing)
            {
                grid.SettingsBehavior.AllowSort = false;
                grid.SettingsBehavior.AllowAutoFilter = false;
                grid.SettingsBehavior.AllowHeaderFilter = false;
            }
            else
            {
                grid.SettingsBehavior.AllowSort = true;
                grid.SettingsBehavior.AllowAutoFilter = true;
                grid.SettingsBehavior.AllowHeaderFilter = true;
            }
        }

        protected void grdWorkflowMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("WorkflowMasterPageControl") as ASPxPageControl;

            ASPxDateEdit effectDate = pageControl.FindControl("EffectDate") as ASPxDateEdit;
            ASPxComboBox entCode = pageControl.FindControl("EntCode") as ASPxComboBox;
            ASPxComboBox buCode = pageControl.FindControl("BUCode") as ASPxComboBox;
            ASPxComboBox buHead = pageControl.FindControl("BUHead") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string entCodeDesc = GlobalClass.EntityCodeDescription(entCode.Value.ToString());
            string buCodeDesc = GlobalClass.BUCodeDescription(buCode.Value.ToString());
            string buHeadName = AccountClass.UserCompleteName(Convert.ToInt32(buHead.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            string update_MRP = "UPDATE tbl_System_Workflow " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [EntCode] = @EntCode, " +
                                " [BUCode] = @BUCode, " +
                                " [BUHead]= @BUHead, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@EntCode", entCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUCode", buCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", buHead.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindWorkflow();
            e.Cancel = true;
            grid.CancelEdit();
        }

        
    }
}