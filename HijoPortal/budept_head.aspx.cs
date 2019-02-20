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
    public partial class budept_head : System.Web.UI.Page
    {
        private static bool bindHeadList = true;
        private static string sEntCode = "";
        private static string sBUCode = "";

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

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }

            if (bindHeadList)
            {
                BindUserList();
            }
            else
            {
                bindHeadList = true;
            }
        }

        private void BindUserList()
        {
            //MRPClass.PrintString("MRP is bind");
            DataTable dtRecord = GlobalClass.BUDeptHeadTable();
            BUDeptListGrid.DataSource = dtRecord;
            BUDeptListGrid.KeyFieldName = "PK";
            BUDeptListGrid.DataBind();

        }

        protected void BUDeptListGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindHeadList = false;
            sEntCode = "";
            sBUCode = "";
        }

        protected void BUDeptListGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("BUHeadPageControl") as ASPxPageControl;
            ASPxDateEdit effectDate = pageControl.FindControl("EffectDate") as ASPxDateEdit;
            ASPxComboBox entCode = pageControl.FindControl("EntityCode") as ASPxComboBox;
            ASPxCallbackPanel callBackPanel = pageControl.FindControl("BUCallBackPanel") as ASPxCallbackPanel;
            ASPxComboBox buCode = callBackPanel.FindControl("BUCode") as ASPxComboBox;
            ASPxComboBox buHead = pageControl.FindControl("BUHead") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string sCtrlNum = GlobalClass.GetControl_DocNum("BU_Dept_Head", Convert.ToDateTime(effectDate.Value.ToString())) , sLastModified = DateTime.Now.ToString();

            string sbuCode = "";
            if (buCode.Value != null)
            {
                sbuCode = buCode.Value.ToString();
            }

            string insert = "INSERT INTO tbl_System_BUDeptHead ([Ctrl], [EffectDate], [EntityCode], [BUDeptCode], [UserKey], [LastModified]) " +
                            " VALUES (@Ctrl, @EffectDate, @EntityCode, @BUDeptCode, @UserKey, @LastModified)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Ctrl", sCtrlNum);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@EntityCode", entCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUDeptCode", sbuCode.ToString());
            cmd.Parameters.AddWithValue("@UserKey", buHead.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindUserList();

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM tbl_System_BUDeptHead ORDER BY [PK] DESC";
            SqlCommand comm = new SqlCommand(query_pk, conn);
            SqlDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                pk_latest = Convert.ToInt32(r[0].ToString());
            }
            if (pk_latest > 0)
                FocusThisRowGrid(grid, pk_latest);
        }

        protected void BUDeptListGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            //bool Exist = CheckDetailsExist(PK);
            //if (Exist)//if the material has logs
            //{
            //    conn.Close();
            //    e.Cancel = true;
            //}
            //else
            //{
                string delete = "DELETE FROM tbl_System_BUDeptHead WHERE [PK] ='" + PK + "'";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindUserList();
                e.Cancel = true;
            //}
            conn.Close();
        }

        protected void BUDeptListGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindHeadList = false;
            sEntCode = BUDeptListGrid.GetRowValues(BUDeptListGrid.FocusedRowIndex, "EntityCode").ToString();
            sBUCode = BUDeptListGrid.GetRowValues(BUDeptListGrid.FocusedRowIndex, "BUDeptCode").ToString();
        }

        protected void BUDeptListGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("BUHeadPageControl") as ASPxPageControl;
            ASPxDateEdit effectDate = pageControl.FindControl("EffectDate") as ASPxDateEdit;
            ASPxComboBox entCode = pageControl.FindControl("EntityCode") as ASPxComboBox;
            ASPxCallbackPanel callBackPanel = pageControl.FindControl("BUCallBackPanel") as ASPxCallbackPanel;
            ASPxComboBox buCode = callBackPanel.FindControl("BUCode") as ASPxComboBox;
            ASPxComboBox buHead = pageControl.FindControl("BUHead") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string sLastModified = DateTime.Now.ToString();
            string sbuCode = "";
            if (buCode.Value != null)
            {
                sbuCode = buCode.Value.ToString();
            }
            string update_MRP = "UPDATE tbl_System_BUDeptHead " +
                                " SET [EffectDate] = @EffectDate, " +
                                " [EntityCode] = @EntCode, " +
                                " [BUDeptCode] = @BUCode, " +
                                " [UserKey]= @BUHead, " +
                                " [LastModified] = @LastModified " +
                                " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EffectDate", effectDate.Value.ToString());
            cmd.Parameters.AddWithValue("@EntCode", entCode.Value.ToString());
            cmd.Parameters.AddWithValue("@BUCode", sbuCode.ToString());
            cmd.Parameters.AddWithValue("@BUHead", buHead.Value.ToString());
            cmd.Parameters.AddWithValue("@LastModified", sLastModified);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindUserList();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void BUCallBackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxPageControl pageControl = BUDeptListGrid.FindEditFormTemplateControl("BUHeadPageControl") as ASPxPageControl;
            ASPxComboBox entCode = pageControl.FindControl("EntityCode") as ASPxComboBox;

            ASPxCallbackPanel callBackPanel = pageControl.FindControl("BUCallBackPanel") as ASPxCallbackPanel;
            ASPxComboBox buCode = callBackPanel.FindControl("BUCode") as ASPxComboBox;

            buCode.Value = "";
            buCode.Text = "";

            DataTable dtRecord = GlobalClass.EntBUSSUTable(entCode.Value.ToString());
            buCode.DataSource = dtRecord;

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "ID";
            //l_value.Caption = "ID";
            //buCode.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //l_text.Caption = "Name";
            //buCode.Columns.Add(l_text);

            buCode.TextField = "NAME";
            buCode.ValueField = "ID";
            buCode.TextFormatString = "{1}";
            buCode.DataBind();
        }

        protected void EntityCode_Init(object sender, EventArgs e)
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
            //MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "EntityCode").ToString();
            }
        }

        protected void BUCode_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = GlobalClass.EntBUSSUTable(sEntCode);
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

            combo.Value = sBUCode;
        }

        protected void BUHead_Init(object sender, EventArgs e)
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

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            //MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "UserKey").ToString();
            }
        }
    }
}