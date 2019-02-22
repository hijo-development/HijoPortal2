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
    public partial class scm : System.Web.UI.Page
    {
        private static bool bindHeadList = true;
        private static bool bindInventAnalList = true;
        private static bool bindProcOffList = true;
        private static bool bindProcCatList = true;
        private static string sHeadKey = "";
        private static string sInventAnalKey = "";
        private static string sProcOffKey = "";
        private static string sProcCatMasterKey = "";
        private static string sProcCatCode = "";

        private static int iProcOffMaster = 0;

        private void CheckCreatorKey()
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

        private void BindSCMHead()
        {
            
            DataTable dtRecord = SCMClass.SCMHeadTable();
            grdSCMHead.DataSource = dtRecord;
            grdSCMHead.KeyFieldName = "PK";
            grdSCMHead.DataBind();

        }

        private void BindSCMInventAnal()
        {

            DataTable dtRecord = SCMClass.InventoryAnalTable();
            grdSCMInventoryAnal.DataSource = dtRecord;
            grdSCMInventoryAnal.KeyFieldName = "PK";
            grdSCMInventoryAnal.DataBind();

        }

        private void BindSCMProcOff()
        {

            DataTable dtRecord = SCMClass.ProcurementOffTable();
            grdSCMProcurementOff.DataSource = dtRecord;
            grdSCMProcurementOff.KeyFieldName = "PK";
            grdSCMProcurementOff.DataBind();

        }

        private void BindSCMProcOff_Details(int MasterKey)
        {

            DataTable dtRecord = SCMClass.ProcurementOff_DetailsTable(MasterKey);
            grdSCMProcurementOffDetails.DataSource = dtRecord;
            grdSCMProcurementOffDetails.KeyFieldName = "PK";
            grdSCMProcurementOffDetails.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }

            if (bindHeadList)
            {
                BindSCMHead();
            }
            else
            {
                bindHeadList = true;
            }

            if (bindInventAnalList)
            {
                BindSCMInventAnal();
            }
            else
            {
                bindInventAnalList = true;
            }

            if (bindProcOffList)
            {
                BindSCMProcOff();
            }
            else
            {
                bindProcOffList = true;
            }

            if (bindProcCatList)
            {
                int iProdOffKey = 0;
                if (grdSCMProcurementOff.VisibleRowCount > 0)
                {
                    iProdOffKey = Convert.ToInt32(grdSCMProcurementOff.GetRowValues(grdSCMProcurementOff.FocusedRowIndex, "PK").ToString());
                }
                BindSCMProcOff_Details(iProdOffKey);
            }
            else
            {
                bindProcCatList = true;
            }

            
        }

        protected void SCMHead_Init(object sender, EventArgs e)
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

            //GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            //MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            //if (!container.Grid.IsNewRowEditing)
            //{
            //    combo.Value = DataBinder.Eval(container.DataItem, "UserKey").ToString();
            //}
        }

        protected void grdSCMHead_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindHeadList = false;
            sHeadKey = "";
            //ASPxGridView grid = sender as ASPxGridView;
            //ASPxDateEdit effectDate = grid.FindEditFormTemplateControl("EffectDate") as ASPxDateEdit;
            //ASPxTextBox lastModified = grid.FindEditFormTemplateControl("ASPxLastModifiedTextBox") as ASPxTextBox;

            ASPxLabel ctrlNum = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["Ctrl"], "ASPxCtrlTextBox") as ASPxLabel;
            ASPxDateEdit effectDate = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxLabel lastModified = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["LastModified"], "ASPxLastModifiedTextBox") as ASPxLabel;
            //ASPxTextBox lastModified = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["LastModified"], "ASPxLastModifiedTextBox") as ASPxTextBox;

            ctrlNum.Text = GlobalClass.GetControl_DocNum("SCM_Head", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
        }

        protected void grdSCMHead_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox scmHead = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["UserCompleteName"], "SCMHead") as ASPxComboBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("SCM_Head", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_SCMHead ([Ctrl], [EffectDate], [UserKey], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @UserKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@UserKey", scmHead.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindSCMHead();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_SCMHead ORDER BY [PK] DESC";
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

        protected void grdSCMHead_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindHeadList = false;
            sHeadKey = grdSCMHead.GetRowValues(grdSCMHead.FocusedRowIndex, "UserKey").ToString();
        }

        protected void grdSCMHead_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["EffectDate"], "EffectDate") as ASPxDateEdit;
            ASPxComboBox scmHead = grdSCMHead.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMHead.Columns["UserCompleteName"], "SCMHead") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_SCMHead " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [UserKey]= @BUHead, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", scmHead.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindSCMHead();
            e.Cancel = true;
            grid.CancelEdit();

        }

        protected void grdSCMHead_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_SCMHead WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindSCMHead();
            e.Cancel = true;
            conn.Close();
        }

        protected void InventoryAnal_Init(object sender, EventArgs e)
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

            combo.Value = sInventAnalKey.ToString();
        }

        protected void grdSCMInventoryAnal_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindInventAnalList = false;
            sInventAnalKey = "";
            //ASPxGridView grid = sender as ASPxGridView;
            //ASPxDateEdit effectDate = grid.FindEditFormTemplateControl("EffectDate") as ASPxDateEdit;
            //ASPxTextBox lastModified = grid.FindEditFormTemplateControl("ASPxLastModifiedTextBox") as ASPxTextBox;
            ASPxLabel ctrlNum = grdSCMInventoryAnal.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMInventoryAnal.Columns["Ctrl"], "ASPxCtrlTextBoxAnal") as ASPxLabel;
            ASPxDateEdit effectDate = grdSCMInventoryAnal.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMInventoryAnal.Columns["EffectDate"], "EffectDateAnal") as ASPxDateEdit;
            ASPxLabel lastModified = grdSCMInventoryAnal.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMInventoryAnal.Columns["LastModified"], "ASPxLastModifiedTextBoxAnal") as ASPxLabel;

            ctrlNum.Text = GlobalClass.GetControl_DocNum("SCM_InventAnal", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
        }

        protected void grdSCMInventoryAnal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdSCMInventoryAnal.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMInventoryAnal.Columns["EffectDate"], "EffectDateAnal") as ASPxDateEdit;
            ASPxComboBox scmInventAnal = grdSCMInventoryAnal.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMInventoryAnal.Columns["UserCompleteName"], "InventoryAnal") as ASPxComboBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("SCM_InventAnal", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_SCMInventoryAnalyst ([Ctrl], [EffectDate], [UserKey], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @UserKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@UserKey", scmInventAnal.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindSCMInventAnal();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_SCMInventoryAnalyst ORDER BY [PK] DESC";
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

        protected void grdSCMInventoryAnal_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindInventAnalList = false;
            sInventAnalKey = grdSCMInventoryAnal.GetRowValues(grdSCMInventoryAnal.FocusedRowIndex, "UserKey").ToString();
        }

        protected void grdSCMInventoryAnal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdSCMInventoryAnal.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMInventoryAnal.Columns["EffectDate"], "EffectDateAnal") as ASPxDateEdit;
            ASPxComboBox scmInventAnal = grdSCMInventoryAnal.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMInventoryAnal.Columns["UserCompleteName"], "InventoryAnal") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_SCMInventoryAnalyst " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [UserKey]= @BUHead, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", scmInventAnal.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindSCMInventAnal();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdSCMInventoryAnal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_SCMInventoryAnalyst WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindSCMInventAnal();
            e.Cancel = true;
            conn.Close();
        }

        protected void ProcurementOff_Init(object sender, EventArgs e)
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

            combo.Value = sProcOffKey.ToString();
        }

        protected void grdSCMProcurementOff_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindProcOffList = false;
            sProcOffKey = "";

            ASPxLabel ctrlNum = grdSCMProcurementOff.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOff.Columns["Ctrl"], "ASPxCtrlTextBoxProcOff") as ASPxLabel;
            ASPxDateEdit effectDate = grdSCMProcurementOff.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOff.Columns["EffectDate"], "EffectDateProcOff") as ASPxDateEdit;
            ASPxLabel lastModified = grdSCMProcurementOff.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOff.Columns["LastModified"], "ASPxLastModifiedTextBoxProcOff") as ASPxLabel;

            ctrlNum.Text = GlobalClass.GetControl_DocNum("SCM_ProcOff", Convert.ToDateTime(DateTime.Now.ToString()));
            effectDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            lastModified.Text = DateTime.Now.ToString();
        }

        protected void grdSCMProcurementOff_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdSCMProcurementOff.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOff.Columns["EffectDate"], "EffectDateProcOff") as ASPxDateEdit;
            ASPxComboBox scmProfOff = grdSCMProcurementOff.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOff.Columns["UserCompleteName"], "ProcurementOff") as ASPxComboBox;

            string sCtrlNum = GlobalClass.GetControl_DocNum("SCM_ProcOff", Convert.ToDateTime(effectDate.Value.ToString()));
            string sLastModified = DateTime.Now.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO tbl_System_SCMProcurementOfficer ([Ctrl], [EffectDate], [UserKey], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @UserKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@UserKey", scmProfOff.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindSCMProcOff();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_SCMProcurementOfficer ORDER BY [PK] DESC";
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

        protected void grdSCMProcurementOff_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindProcOffList = false;
            sProcOffKey = grdSCMProcurementOff.GetRowValues(grdSCMProcurementOff.FocusedRowIndex, "UserKey").ToString();
        }

        protected void grdSCMProcurementOff_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxDateEdit effectDate = grdSCMProcurementOff.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOff.Columns["EffectDate"], "EffectDateProcOff") as ASPxDateEdit;
            ASPxComboBox scmProfOff = grdSCMProcurementOff.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOff.Columns["UserCompleteName"], "ProcurementOff") as ASPxComboBox;

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_MRP = "UPDATE tbl_System_SCMProcurementOfficer " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [UserKey]= @BUHead, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@BUHead", scmProfOff.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindSCMProcOff();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grdSCMProcurementOff_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM tbl_System_SCMProcurementOfficer WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindSCMProcOff();
            e.Cancel = true;
            conn.Close();
        }

        protected void ProcurementCat_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = GlobalClass.ProCategoryTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 20;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            combo.Value = sProcCatCode.ToString();
        }

        protected void grdSCMProcurementOffDetails_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            ASPxGridView grid = sender as ASPxGridView;
            int iProdOffKey = 0;
            if (grdSCMProcurementOff.VisibleRowCount > 0)
            {
                iProdOffKey = Convert.ToInt32(grdSCMProcurementOff.GetRowValues(grdSCMProcurementOff.FocusedRowIndex, "PK").ToString());
            }
            grid.CancelEdit();
            BindSCMProcOff_Details(iProdOffKey);
        }

        protected void grdSCMProcurementOffDetails_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            sProcCatMasterKey = "";
            sProcCatMasterKey = grdSCMProcurementOff.GetRowValues(grdSCMProcurementOff.FocusedRowIndex, "PK").ToString();
            if (sProcCatMasterKey == "") { return; }

            
            //grdSCMProcurementOff.Enabled = false;
            bindProcCatList = false;
            sProcCatCode = "";
            iProcOffMaster = Convert.ToInt32(sProcCatMasterKey);

        }

        protected void grdSCMProcurementOffDetails_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxHiddenField masterKey = grdSCMProcurementOffDetails.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOffDetails.Columns["MasterKey"], "ASPxMasterKeyHiddenField") as ASPxHiddenField;
            ASPxComboBox scmProfCat = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ProcCatDesc"], "ProcurementCat") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string sLastModified = DateTime.Now.ToString();

            string insert = "INSERT INTO tbl_System_SCMProcurementOfficer_Details ([MasterKey], [ProcCat]) " +
                            " VALUES (@MasterKey, @ProcCat)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            //cmd.Parameters.AddWithValue("@MasterKey", sMasterKey.Value.ToString());
            cmd.Parameters.AddWithValue("@MasterKey", iProcOffMaster.ToString());
            cmd.Parameters.AddWithValue("@ProcCat", scmProfCat.Value.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_SCMProcurementOfficer " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iProcOffMaster.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            //BindSCMProcOff_Details(Convert.ToInt32(sMasterKey.Value));
            BindSCMProcOff_Details(iProcOffMaster);

            //grdSCMProcurementOff.Enabled = true;

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_SCMProcurementOfficer_Details ORDER BY [PK] DESC";
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

        protected void grdSCMProcurementOffDetails_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            sProcCatMasterKey = "";
            sProcCatMasterKey = grdSCMProcurementOff.GetRowValues(grdSCMProcurementOff.FocusedRowIndex, "PK").ToString();
            if (sProcCatMasterKey == "") { return; }

            bindProcCatList = false;
            iProcOffMaster = Convert.ToInt32(sProcCatMasterKey);
            sProcCatCode = grdSCMProcurementOffDetails.GetRowValues(grdSCMProcurementOffDetails.FocusedRowIndex, "ProcCat").ToString();
        }

        protected void grdSCMProcurementOffDetails_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxHiddenField masterKey = grdSCMProcurementOffDetails.FindEditRowCellTemplateControl((GridViewDataColumn)grdSCMProcurementOffDetails.Columns["MasterKey"], "ASPxMasterKeyHiddenField") as ASPxHiddenField;
            ASPxComboBox scmProfCat = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ProcCatDesc"], "ProcurementCat") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string sLastModified = DateTime.Now.ToString();
            string PK = e.Keys[0].ToString();

            string update = "UPDATE tbl_System_SCMProcurementOfficer_Details " +
                            " SET [ProcCat] = @ProcCat " +
                            " WHERE ([PK] = @PK)";

            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@ProcCat", scmProfCat.Value.ToString());
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_SCMProcurementOfficer " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iProcOffMaster.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            e.Cancel = true;
            grid.CancelEdit();
            BindSCMProcOff_Details(iProcOffMaster);
        }

        protected void grdSCMProcurementOffDetails_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            sProcCatMasterKey = "";
            sProcCatMasterKey = grdSCMProcurementOff.GetRowValues(grdSCMProcurementOff.FocusedRowIndex, "PK").ToString();
            if (sProcCatMasterKey == "") { return; }

            iProcOffMaster = Convert.ToInt32(sProcCatMasterKey);

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string sLastModified = DateTime.Now.ToString();

            string delete = "DELETE FROM tbl_System_SCMProcurementOfficer_Details WHERE [PK] ='" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

            string updateMaster = "UPDATE tbl_System_SCMProcurementOfficer " +
                                  " SET [LastModified] = @LastModified " +
                                  " WHERE ([PK] = @PK)";

            SqlCommand cmd1 = new SqlCommand(updateMaster, conn);
            cmd1.Parameters.AddWithValue("@PK", iProcOffMaster.ToString());
            cmd1.Parameters.AddWithValue("@LastModified", sLastModified.ToString());
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            conn.Close();
            BindSCMProcOff_Details(iProcOffMaster);
            e.Cancel = true;
            conn.Close();
        }
    }
}