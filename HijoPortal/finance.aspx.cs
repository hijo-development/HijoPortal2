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
    public partial class finance : System.Web.UI.Page
    {
        private static bool bindFinanceInventList = true;
        private static bool bindHeadList = true;
        private static bool bindBudgetList = true;
        private static bool bindBudgetDetList = true;
        private static string sApprovalKey = "";
        private static string sHeadKey = "";
        private static string sBudgetKey = "";
        private static string sBudgetDetEnt = "";
        private static string sBudgetDetBU = "";
        private static string sBudgetStatusKey = "";
        private static string sInventOffStatusKey = "";
        private static string sFinanceHeadStatusKey = "";

        private static int iMasterKey = 0;


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

        private void BindFinanceInventoryOfficer()
        {
            DataTable dtRecord = FinanceClass.FinanceInventoryOfficerTable();
            grdFinanceApproval.DataSource = dtRecord;
            grdFinanceApproval.KeyFieldName = "PK";
            grdFinanceApproval.DataBind();

        }

        private void BindFinanceHead()
        {

            DataTable dtRecord = FinanceClass.FinanceHeadTable();
            grdFinanceHead.DataSource = dtRecord;
            grdFinanceHead.KeyFieldName = "PK";
            grdFinanceHead.DataBind();

        }

        private void BindFinanceBudget()
        {

            DataTable dtRecord = FinanceClass.FinanceBudgetTable();
            grdFinanceBudget.DataSource = dtRecord;
            grdFinanceBudget.KeyFieldName = "PK";
            grdFinanceBudget.DataBind();
            //ASPxLabelBudgetOff.Text = grdFinanceBudget.VisibleRowCount.ToString();

        }

        private void BindFinanceBudgetDet(int Master)
        {

            DataTable dtRecord = FinanceClass.FinanceBudget_DetailsTable(Master);
            grdFinanceBudgetDet.DataSource = dtRecord;
            grdFinanceBudgetDet.KeyFieldName = "PK";
            grdFinanceBudgetDet.DataBind();

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

            if (bindFinanceInventList)
            {
                BindFinanceInventoryOfficer();
            }
            else
            {
                bindFinanceInventList = true;
            }

            if (bindHeadList)
            {
                BindFinanceHead();
            }
            else
            {
                bindHeadList = true;
            }

            if (bindBudgetList)
            {
                BindFinanceBudget();
            }
            else
            {
                bindBudgetList = true;
            }

            if (bindBudgetDetList)
            {
                int iFinBudKey = 0;
                if (grdFinanceBudget.VisibleRowCount > 0)
                {
                    iFinBudKey = Convert.ToInt32(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString());
                }
                BindFinanceBudgetDet(iFinBudKey);
            }
            else
            {
                bindBudgetDetList = true;
            }
        }

        protected void FinanceHead_Init(object sender, EventArgs e)
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

            combo.Value = sHeadKey.ToString();
        }

        protected void grdFinanceHead_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindHeadList = false;
            sHeadKey = "";
            sFinanceHeadStatusKey = "";

            ASPxLabel ctrlNum = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["Ctrl"], "ASPxCtrlTextBox") as ASPxLabel;
            ASPxDateEdit effectDate = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxLabel lastModified = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["LastModified"], "ASPxLastModifiedTextBox") as ASPxLabel;


            ctrlNum.Text = GlobalClass.GetControl_DocNum("Finance_Head", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
        }

        protected void grdFinanceHead_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox financeHead = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["UserCompleteName"], "FinanceHead") as ASPxComboBox;
            ASPxComboBox financeHeadStatus = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("Finance_Head", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_FinanceHead ([Ctrl], [EffectDate], [UserKey], [StatusKey], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @UserKey, @StatusKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@UserKey", financeHead.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", financeHeadStatus.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindFinanceHead();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_FinanceHead ORDER BY [PK] DESC";
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

        protected void grdFinanceHead_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindHeadList = false;
            sHeadKey = grdFinanceHead.GetRowValues(grdFinanceHead.FocusedRowIndex, "UserKey").ToString();
            sFinanceHeadStatusKey = grdFinanceHead.GetRowValues(grdFinanceHead.FocusedRowIndex, "StatusKey").ToString();
        }

        protected void grdFinanceHead_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox financeHead = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["UserCompleteName"], "FinanceHead") as ASPxComboBox;
            ASPxComboBox financeHeadStatus = grdFinanceHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceHead.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_FinanceHead " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [UserKey]= @BUHead, " +
                                " [StatusKey] = @StatusKey, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", financeHead.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", financeHeadStatus.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindFinanceHead();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdFinanceHead_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_FinanceHead WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindFinanceHead();
            e.Cancel = true;
            conn.Close();
        }

        protected void FinanceBudget_Init(object sender, EventArgs e)
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

            combo.Value = sBudgetKey.ToString();
        }

        protected void grdFinanceBudget_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindBudgetList = false;
            sBudgetKey = "";
            sBudgetStatusKey = "";

            ASPxLabel ctrlNum = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["Ctrl"], "ASPxCtrlTextBoxBud") as ASPxLabel;
            ASPxDateEdit effectDate = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxLabel lastModified = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["LastModified"], "ASPxLastModifiedTextBoxBud") as ASPxLabel;


            ctrlNum.Text = GlobalClass.GetControl_DocNum("Finance_Budget", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
        }

        protected void grdFinanceBudget_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox financeHead = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["UserCompleteName"], "FinanceBudget") as ASPxComboBox;
            ASPxComboBox financeBudStat = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("Finance_Budget", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_FinanceBudget ([Ctrl], [EffectDate], [UserKey], [StatusKey], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @UserKey, @StatusKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@UserKey", financeHead.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", financeBudStat.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindFinanceBudget();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_FinanceBudget ORDER BY [PK] DESC";
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

        protected void grdFinanceBudget_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindBudgetList = false;
            sBudgetKey = grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "UserKey").ToString();
            sBudgetStatusKey = grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "StatusKey").ToString();
        }

        protected void grdFinanceBudget_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox financeHead = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["UserCompleteName"], "FinanceBudget") as ASPxComboBox;
            ASPxComboBox financeBudStat = grdFinanceBudget.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudget.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_FinanceBudget " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [UserKey]= @BUHead, " +
                                " [StatusKey] = @StatusKey, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", financeHead.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", financeBudStat.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindFinanceBudget();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdFinanceBudget_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_FinanceBudget WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindFinanceBudget();
            e.Cancel = true;
            conn.Close();
        }

        protected void Entity_Init(object sender, EventArgs e)
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

            combo.Value = sBudgetDetEnt;
        }

        protected void BUSSU_Init(object sender, EventArgs e)
        {

            DataTable dtRecord = GlobalClass.EntBUSSUTable(sBudgetDetEnt);
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;

            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 30;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            l_TextField.Caption = "NAME";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            combo.Value = sBudgetDetBU;
        }

        protected void FinBUCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            
            ASPxComboBox entCode = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["EntityCodeDesc"], "Entity") as ASPxComboBox;
            //ASPxCallbackPanel callBackPanel = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["BUSSUCodeDesc"], "FinBUCallbackPanel") as ASPxCallbackPanel;
            //ASPxCallbackPanel callBackPanel = grdFinanceBudgetDet.FindEmptyDataRowTemplateControl("FinBUCallbackPanel") as ASPxCallbackPanel;
            ASPxCallbackPanel callBackPanel = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["BUSSUCodeDesc"], "FinBUCallbackPanel") as ASPxCallbackPanel;
            //ASPxCallbackPanel callBackPanel = sender as ASPxCallbackPanel;
            ASPxComboBox buCode = callBackPanel.FindControl("BUSSU") as ASPxComboBox;

            //ASPxCallbackPanel callBackPanel = pageControl.FindControl("BUCallBackPanel") as ASPxCallbackPanel;


            buCode.Value = "";
            buCode.Text = "";

            DataTable dtRecord = GlobalClass.EntBUSSUTable(entCode.Value.ToString());
            buCode.DataSource = dtRecord;

            buCode.TextField = "NAME";
            buCode.ValueField = "ID";
            buCode.TextFormatString = "{1}";
            buCode.DataBind();

        }

        protected void grdFinanceBudgetDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            
            if (grdFinanceBudget.VisibleRowCount == 0) { grid.CancelEdit(); }
            bool parseInt = int.TryParse(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString(), out iMasterKey);
            if (parseInt == false) { grid.CancelEdit();  }
            if (iMasterKey == 0) { grid.CancelEdit(); }

            bindBudgetDetList = false;
            sBudgetDetEnt = "";
            sBudgetDetBU = "";
        }

        protected void grdFinanceBudgetDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox entCode = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["EntityCodeDesc"], "Entity") as ASPxComboBox;
            ASPxCallbackPanel callBackPanel = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["BUSSUCodeDesc"], "FinBUCallbackPanel") as ASPxCallbackPanel;
            ASPxComboBox buCode = callBackPanel.FindControl("BUSSU") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string sBUCode = "";
            if (buCode.Value != null)
            {
                sBUCode = buCode.Value.ToString();
            }

            string insert = "INSERT INTO tbl_System_FinanceBudget_Details ([MasterKey], [EntityCode], [BUSSUCode]) " +
                            " VALUES (@MasterKey, @EntityCode, @BUSSUCode)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@MasterKey", iMasterKey.ToString());
            cmd.Parameters.AddWithValue("@EntityCode", entCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUSSUCode", sBUCode);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_FinanceBudget " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iMasterKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindFinanceBudgetDet(iMasterKey);

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_FinanceBudget_Details ORDER BY [PK] DESC";
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

        protected void grdFinanceBudgetDet_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            bool parseInt = int.TryParse(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString(), out iMasterKey);
            if (parseInt == false) { grid.CancelEdit(); }
            if (iMasterKey == 0) { grid.CancelEdit(); }

            bindBudgetDetList = false;
            sBudgetDetEnt = grdFinanceBudgetDet.GetRowValues(grdFinanceBudgetDet.FocusedRowIndex, "EntityCode").ToString(); 
            sBudgetDetBU = grdFinanceBudgetDet.GetRowValues(grdFinanceBudgetDet.FocusedRowIndex, "BUSSUCode").ToString();
        }

        protected void grdFinanceBudgetDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox entCode = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["EntityCodeDesc"], "Entity") as ASPxComboBox;
            ASPxCallbackPanel callBackPanel = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["BUSSUCodeDesc"], "FinBUCallbackPanel") as ASPxCallbackPanel;
            ASPxComboBox buCode = callBackPanel.FindControl("BUSSU") as ASPxComboBox;
            //ASPxComboBox buCode = grdFinanceBudgetDet.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceBudgetDet.Columns["BUSSUCodeDesc"], "BUSSU") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE tbl_System_FinanceBudget_Details " +
                            " SET [EntityCode] = @EntityCode, " +
                            " [BUSSUCode] = @BUSSUCode " +
                            " WHERE ([PK] = @PK)";

            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EntityCode", entCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUSSUCode", entCode.Value.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_FinanceBudget " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iMasterKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            e.Cancel = true;
            grid.CancelEdit();
            BindFinanceBudgetDet(iMasterKey);
        }

        protected void grdFinanceBudgetDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            bool parseInt = int.TryParse(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString(), out iMasterKey);
            if (parseInt == false) { return; }
            if (iMasterKey == 0) { return; }

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string sLastModified = DateTime.Now.ToString();

            string delete = "DELETE FROM tbl_System_FinanceBudget_Details WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_FinanceBudget " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iMasterKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            BindFinanceBudgetDet(iMasterKey);
            e.Cancel = true;
            conn.Close();
        }

        protected void grdFinanceBudgetDet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.Parameters == "AddNew")
            {
                //MRPClass.PrintString("pass add");
                if (grdFinanceBudget.VisibleRowCount == 0) { return; }
                bool parseInt = int.TryParse(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString(), out iMasterKey);
                if (parseInt == false) { return; }
                if (iMasterKey == 0) { return; }

                bindBudgetDetList = false;
                sBudgetDetEnt = "";
                sBudgetDetBU = "";

                grdFinanceBudgetDet.AddNewRow();

            } 
            if (e.Parameters == "BudOff")
            {
                iMasterKey = 0;
                if (grdFinanceBudget.VisibleRowCount > 0)
                {
                    iMasterKey = Convert.ToInt32(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString());
                }
                grid.CancelEdit();
                BindFinanceBudgetDet(iMasterKey);
            }
        }

        protected void grdFinanceBudgetDet_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            //MRPClass.PrintString("pass add");
            //if (grdFinanceBudget.VisibleRowCount == 0) { return; }
            //bool parseInt = int.TryParse(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString(), out iMasterKey);
            //if (parseInt == false) { return; }
            //if (iMasterKey == 0) { return; }

            //bindBudgetDetList = false;
            //sBudgetDetEnt = "";
            //sBudgetDetBU = "";

            ////ASPxLabelBudgetOff.Text = "pass add";

            //grdFinanceBudgetDet.AddNewRow();

        }

        protected void Add_FinBudEntBU_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            if (grdFinanceBudget.VisibleRowCount == 0) { return; }
            bool parseInt = int.TryParse(grdFinanceBudget.GetRowValues(grdFinanceBudget.FocusedRowIndex, "PK").ToString(), out iMasterKey);
            if (parseInt == false) { return; }
            if (iMasterKey == 0) { return; }

            bindBudgetDetList = false;
            sBudgetDetEnt = "";
            sBudgetDetBU = "";

            //ASPxLabelBudgetOff.Text = "pass add";

            grdFinanceBudgetDet.AddNewRow();
        }

        protected void Approval_Init(object sender, EventArgs e)
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

            combo.Value = sApprovalKey.ToString();
        }

        protected void grdFinanceApproval_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindFinanceInventList = false;
            sApprovalKey = "";
            sInventOffStatusKey = "";

            ASPxLabel ctrlNum = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["Ctrl"], "ASPxCtrlTextBoxApp") as ASPxLabel;
            ASPxDateEdit effectDate = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxLabel lastModified = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["LastModified"], "ASPxLastModifiedTextBoxApp") as ASPxLabel;


            ctrlNum.Text = GlobalClass.GetControl_DocNum("Finance_Inventory_Officer", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
        }

        protected void grdFinanceApproval_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox approval = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["UserCompleteName"], "Approval") as ASPxComboBox;
            ASPxComboBox InventOff = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("Finance_Inventory_Officer", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_FinanceInventoryOfficer ([Ctrl], [EffectDate], [UserKey], [StatusKey] [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @UserKey, @StatusKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@UserKey", approval.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", InventOff.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindFinanceInventoryOfficer();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_FinanceInventoryOfficer ORDER BY [PK] DESC";
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

        protected void grdFinanceApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindFinanceInventList = false;
            sApprovalKey = grdFinanceApproval.GetRowValues(grdFinanceApproval.FocusedRowIndex, "UserKey").ToString();
            sInventOffStatusKey = grdFinanceApproval.GetRowValues(grdFinanceApproval.FocusedRowIndex, "StatusKey").ToString();
        }

        protected void grdFinanceApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox approval = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["UserCompleteName"], "Approval") as ASPxComboBox;
            ASPxComboBox InventOff = grdFinanceApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdFinanceApproval.Columns["StatusDesc"], "Status") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_FinanceInventoryOfficer " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [UserKey]= @BUHead, " +
                                " [StatusKey] = @StatusKey, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", approval.Value.ToString());
            cmd.Parameters.AddWithValue("@StatusKey", InventOff.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindFinanceInventoryOfficer();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdFinanceApproval_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_FinanceInventoryOfficer WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindFinanceInventoryOfficer();
            e.Cancel = true;
            conn.Close();
        }

        protected void grdFinanceHead_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void grdFinanceApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void grdFinanceBudget_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void grdFinanceBudgetDet_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void Status_Init(object sender, EventArgs e)
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

            combo.Value = sBudgetStatusKey.ToString();
        }

        protected void InventOffStatus_Init(object sender, EventArgs e)
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

            combo.Value = sInventOffStatusKey.ToString();
        }

        protected void FinanceHeadStatus_Init(object sender, EventArgs e)
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

            combo.Value = sFinanceHeadStatusKey.ToString();
        }
    }
}