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
    public partial class mop_workflow : System.Web.UI.Page
    {
        private static bool bindDataFlowList = true;
        private static bool bindDataFlowDetailsList = true;
        private static bool bindApprovalList = true;
        private static bool bindApprovalDetailsList = true;
        private static string sPositionNameDataKey = "";
        private static string sPositionNameAppKey = "";

        private static int iDataFlowKey = 0;
        private static int iApprovalKey = 0;


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


        private void BindDataFlow()
        {
            DataTable dtRecord = DataFlowClass.DataFlowTable();
            grdDataFlow.DataSource = dtRecord;
            grdDataFlow.KeyFieldName = "PK";
            grdDataFlow.DataBind();

        }

        private void BindDataFlowDetails(int MasterKey)
        {
            DataTable dtRecord = DataFlowClass.DataFlowDetailsTable(MasterKey);
            grdDataFlowDetail.DataSource = dtRecord;
            grdDataFlowDetail.KeyFieldName = "PK";
            grdDataFlowDetail.DataBind();

        }

        private void BindApproval()
        {
            DataTable dtRecord = DataFlowClass.ApprovalTable();
            grdApproval.DataSource = dtRecord;
            grdApproval.KeyFieldName = "PK";
            grdApproval.DataBind();

        }

        private void BindApprovalDetails(int MasterKey)
        {
            //MRPClass.PrintString("pass approval det");
            DataTable dtRecord = DataFlowClass.ApprovalDetailsTable(MasterKey);
            grdApprovalDetail.DataSource = dtRecord;
            grdApprovalDetail.KeyFieldName = "PK";
            grdApprovalDetail.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }

            //BindDataFlow();

            if (bindDataFlowList)
            {
                BindDataFlow();
            }
            else
            {
                bindDataFlowList = true;
            }

            if (bindDataFlowDetailsList)
            {
                int iDataFlowKey = 0;
                if (grdDataFlow.VisibleRowCount > 0)
                {
                    iDataFlowKey = Convert.ToInt32(grdDataFlow.GetRowValues(grdDataFlow.FocusedRowIndex, "PK").ToString());
                }
                BindDataFlowDetails(iDataFlowKey);
            }
            else
            {
                bindDataFlowDetailsList = true;
            }

            if (bindApprovalList)
            {
                BindApproval();
            }
            else
            {
                bindApprovalList = true;
            }

            if (bindApprovalDetailsList)
            {
                int iApprovalKey = 0;
                if (grdApproval.VisibleRowCount > 0)
                {
                    iApprovalKey = Convert.ToInt32(grdApproval.GetRowValues(grdApproval.FocusedRowIndex, "PK").ToString());
                }
                //MRPClass.PrintString(iApprovalKey.ToString());
                BindApprovalDetails(iApprovalKey);
            }
            else
            {
                bindApprovalDetailsList = true;
            }

        }

        protected void grdDataFlow_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindDataFlowList = false;

            ASPxLabel ctrlNum = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["Ctrl"], "ASPxCtrlTextBox") as ASPxLabel;
            ASPxDateEdit effectDate = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxLabel lastModified = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["LastModified"], "ASPxLastModifiedTextBox") as ASPxLabel;
            ASPxTextBox Remarks = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["Remarks"], "ASPxRemarksTextBox") as ASPxTextBox;

            //BindDataFlowDetails(0);

            ctrlNum.Text = GlobalClass.GetControl_DocNum("DataFlow", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
            Remarks.Text = "";
            Remarks.Focus();
        }

        protected void grdDataFlow_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxTextBox Remarks = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["Remarks"], "ASPxRemarksTextBox") as ASPxTextBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("DataFlow", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_MOP_DataFlow ([Ctrl], [EffectDate], [Remarks], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @Remarks, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@Remarks", Remarks.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindDataFlow();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_MOP_DataFlow ORDER BY [PK] DESC";
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

        protected void grdDataFlow_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindDataFlowList = false;
        }

        protected void grdDataFlow_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxTextBox Remarks = grdDataFlow.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlow.Columns["Remarks"], "ASPxRemarksTextBox") as ASPxTextBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_MOP_DataFlow " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [Remarks]= @Remarks, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@Remarks", Remarks.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindDataFlow();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdDataFlow_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_MOP_DataFlow WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindDataFlow();
            e.Cancel = true;
            conn.Close();
        }

        protected void grdApproval_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindApprovalList = false;

            ASPxLabel ctrlNum = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["Ctrl"], "ASPxCtrlTextBox") as ASPxLabel;
            ASPxDateEdit effectDate = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxLabel lastModified = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["LastModified"], "ASPxLastModifiedTextBox") as ASPxLabel;
            ASPxTextBox Remarks = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["Remarks"], "ASPxRemarksTextBox") as ASPxTextBox;

            ctrlNum.Text = GlobalClass.GetControl_DocNum("Approval", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
            Remarks.Text = "";
            Remarks.Focus();
        }

        protected void grdApproval_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxTextBox Remarks = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["Remarks"], "ASPxRemarksTextBox") as ASPxTextBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("Approval", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_Approval ([Ctrl], [EffectDate], [Remarks], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @Remarks, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@Remarks", Remarks.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindApproval();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_Approval ORDER BY [PK] DESC";
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

        protected void grdApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindApprovalList = false;
        }

        protected void grdApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxTextBox Remarks = grdApproval.FindEditRowCellTemplateControl((GridViewDataColumn)grdApproval.Columns["Remarks"], "ASPxRemarksTextBox") as ASPxTextBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_Approval " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [Remarks]= @Remarks, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@Remarks", Remarks.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindApproval();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdApproval_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_Approval WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindApproval();
            e.Cancel = true;
            conn.Close();
        }

        protected void PositionName_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = GlobalClass.PositionNameTable();
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

            combo.Value = sPositionNameDataKey.ToString();
        }

        protected void grdDataFlowDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.Parameters == "AddNew")
            {
                if (grdDataFlow.VisibleRowCount == 0) { return; }
                bool parseInt = int.TryParse(grdDataFlow.GetRowValues(grdDataFlow.FocusedRowIndex, "PK").ToString(), out iDataFlowKey);
                if (parseInt == false) { return; }
                if (iDataFlowKey == 0) { return; }

                bindDataFlowDetailsList = false;
                sPositionNameDataKey = "";

                grdDataFlowDetail.AddNewRow();

            }
            if (e.Parameters == "DataFlow")
            {
                iDataFlowKey = 0;
                if (grdDataFlow.VisibleRowCount > 0)
                {
                    iDataFlowKey = Convert.ToInt32(grdDataFlow.GetRowValues(grdDataFlow.FocusedRowIndex, "PK").ToString());
                }
                grid.CancelEdit();
                BindDataFlowDetails(iDataFlowKey);
            }
        }

        protected void grdDataFlowDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox Line = grdDataFlowDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlowDetail.Columns["Line"], "ASPxLineTextBox") as ASPxTextBox;
            Line.Text = Convert.ToString(Convert.ToInt32(grid.VisibleRowCount.ToString()) + 1);
        }

        protected void grdDataFlowDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox Line = grdDataFlowDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlowDetail.Columns["Line"], "ASPxLineTextBox") as ASPxTextBox;
            ASPxComboBox positionName = grdDataFlowDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlowDetail.Columns["PositionName"], "PositionName") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_MOP_DataFlow_Details ([MasterKey], [Line], [PositionNameKey]) " +
                            " VALUES (@MasterKey, @Line, @PositionNameKey)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@MasterKey", iDataFlowKey.ToString());
            cmd.Parameters.AddWithValue("@Line", Line.Value.ToString());
            cmd.Parameters.AddWithValue("@PositionNameKey", positionName.Value.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_MOP_DataFlow " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";
            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iDataFlowKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindDataFlowDetails(iDataFlowKey);

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_MOP_DataFlow_Details ORDER BY [PK] DESC";
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

        protected void grdDataFlowDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            bool parseInt = int.TryParse(grdDataFlow.GetRowValues(grdDataFlow.FocusedRowIndex, "PK").ToString(), out iDataFlowKey);
            if (parseInt == false) { grid.CancelEdit(); }
            if (iDataFlowKey == 0) { grid.CancelEdit(); }

            bindDataFlowDetailsList = false;
            sPositionNameDataKey = grdDataFlowDetail.GetRowValues(grdDataFlowDetail.FocusedRowIndex, "PositionNameKey").ToString();
        }

        protected void grdDataFlowDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox Line = grdDataFlowDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlowDetail.Columns["Line"], "ASPxLineTextBox") as ASPxTextBox;
            ASPxComboBox positionName = grdDataFlowDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlowDetail.Columns["PositionName"], "PositionName") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE tbl_System_MOP_DataFlow_Details " +
                            " SET [Line] = @Line, " +
                            " [PositionNameKey] = @PositionNameKey " +
                            " WHERE ([PK] = @PK)";

            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@Line", Line.Value.ToString());
            cmd.Parameters.AddWithValue("@PositionNameKey", positionName.Value.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_MOP_DataFlow " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iDataFlowKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            e.Cancel = true;
            grid.CancelEdit();
            BindDataFlowDetails(iDataFlowKey);
        }

        protected void grdDataFlowDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            bool parseInt = int.TryParse(grdDataFlow.GetRowValues(grdDataFlow.FocusedRowIndex, "PK").ToString(), out iDataFlowKey);
            if (parseInt == false) { return; }
            if (iDataFlowKey == 0) { return; }

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string sLastModified = DateTime.Now.ToString();

            string delete = "DELETE FROM tbl_System_MOP_DataFlow_Details WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_MOP_DataFlow " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iDataFlowKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            BindDataFlowDetails(iDataFlowKey);
            e.Cancel = true;
            conn.Close();
        }

        protected void PositionName_Init1(object sender, EventArgs e)
        {
            DataTable dtRecord = GlobalClass.PositionNameTable();
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

            combo.Value = sPositionNameAppKey.ToString();
        }

        protected void grdApprovalDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.Parameters == "AddNew")
            {
                if (grdApproval.VisibleRowCount == 0) { return; }
                bool parseInt = int.TryParse(grdApproval.GetRowValues(grdApproval.FocusedRowIndex, "PK").ToString(), out iApprovalKey);
                if (parseInt == false) { return; }
                if (iApprovalKey == 0) { return; }

                bindApprovalDetailsList = false;
                sPositionNameAppKey = "";

                grdApprovalDetail.AddNewRow();

            }
            if (e.Parameters == "Approval")
            {
                iApprovalKey = 0;
                if (grdApproval.VisibleRowCount > 0)
                {
                    iApprovalKey = Convert.ToInt32(grdApproval.GetRowValues(grdApproval.FocusedRowIndex, "PK").ToString());
                }
                grid.CancelEdit();
                BindApprovalDetails(iApprovalKey);
            }
        }

        protected void grdApprovalDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox Line = grdApprovalDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdApprovalDetail.Columns["Line"], "ASPxLineTextBox") as ASPxTextBox;
            Line.Text = Convert.ToString(Convert.ToInt32(grid.VisibleRowCount.ToString()) + 1);
        }

        protected void grdApprovalDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox Line = grdApprovalDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdApprovalDetail.Columns["Line"], "ASPxLineTextBox") as ASPxTextBox;
            ASPxComboBox positionName = grdApprovalDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdApprovalDetail.Columns["PositionName"], "PositionName") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_Approval_Details ([MasterKey], [Line], [PositionNameKey]) " +
                            " VALUES (@MasterKey, @Line, @PositionNameKey)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@MasterKey", iApprovalKey.ToString());
            cmd.Parameters.AddWithValue("@Line", Line.Value.ToString());
            cmd.Parameters.AddWithValue("@PositionNameKey", positionName.Value.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_Approval " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";
            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iApprovalKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindApprovalDetails(iApprovalKey);

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_Approval_Details ORDER BY [PK] DESC";
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

        protected void grdApprovalDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            bool parseInt = int.TryParse(grdApproval.GetRowValues(grdDataFlow.FocusedRowIndex, "PK").ToString(), out iApprovalKey);
            if (parseInt == false) { grid.CancelEdit(); }
            if (iApprovalKey == 0) { grid.CancelEdit(); }

            bindApprovalDetailsList = false;
            sPositionNameAppKey = grdApprovalDetail.GetRowValues(grdApprovalDetail.FocusedRowIndex, "PositionNameKey").ToString();
        }

        protected void grdApprovalDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox Line = grdDataFlowDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlowDetail.Columns["Line"], "ASPxLineTextBox") as ASPxTextBox;
            ASPxComboBox positionName = grdDataFlowDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdDataFlowDetail.Columns["PositionName"], "PositionName") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE tbl_System_Approval_Details " +
                            " SET [Line] = @Line, " +
                            " [PositionNameKey] = @PositionNameKey " +
                            " WHERE ([PK] = @PK)";

            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@Line", Line.Value.ToString());
            cmd.Parameters.AddWithValue("@PositionNameKey", positionName.Value.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_Approval " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iApprovalKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            e.Cancel = true;
            grid.CancelEdit();
            BindApprovalDetails(iApprovalKey);
        }

        protected void grdApprovalDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            bool parseInt = int.TryParse(grdApproval.GetRowValues(grdDataFlow.FocusedRowIndex, "PK").ToString(), out iApprovalKey);
            if (parseInt == false) { return; }
            if (iApprovalKey == 0) { return; }

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string sLastModified = DateTime.Now.ToString();

            string delete = "DELETE FROM tbl_System_Approval_Details WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_Approval " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iApprovalKey.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            BindApprovalDetails(iApprovalKey);
            e.Cancel = true;
            conn.Close();
        }

        protected void grdDataFlow_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.Parameters == "AddNew")
            {
                grdDataFlow.AddNewRow();
                bindDataFlowDetailsList = false;
                BindDataFlowDetails(0);
            }
        }

        protected void grdApproval_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.Parameters == "AddNew")
            {
                grdApproval.AddNewRow();
                bindApprovalDetailsList = false;
                BindApprovalDetails(0);
            }
        }

        protected void grdDataFlow_BeforeGetCallbackResult(object sender, EventArgs e)
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

        protected void grdDataFlowDetail_BeforeGetCallbackResult(object sender, EventArgs e)
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

        protected void grdApproval_BeforeGetCallbackResult(object sender, EventArgs e)
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

        protected void grdApprovalDetail_BeforeGetCallbackResult(object sender, EventArgs e)
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
    }
}