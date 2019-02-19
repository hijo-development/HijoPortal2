using System;
using System.Collections;
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
    public partial class user_list : System.Web.UI.Page
    {
        private static bool bindUserList = true;

        private void CheckCreatorKey()
        {
            if (Session["CreatorKey"] == null)
            {
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

            if (bindUserList)
            {
                BindUserList();
            }
            else
            {
                bindUserList = true;
            }
        }

        private void BindUserList()
        {
            //MRPClass.PrintString("MRP is bind");
            DataTable dtRecord = AccountClass.UserList();
            UserListGrid.DataSource = dtRecord;
            UserListGrid.KeyFieldName = "PK";
            UserListGrid.DataBind();

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
                combo.Value = DataBinder.Eval(container.DataItem, "EntCode").ToString();
            }
        }

        protected void UserListGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindUserList = false;
        }

        protected void UserListGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("UserPageControl") as ASPxPageControl;
            ASPxComboBox entCode = pageControl.FindControl("EntityCode") as ASPxComboBox;
            ASPxComboBox buCode = pageControl.FindControl("EntityCode") as ASPxComboBox;
            ASPxComboBox userLevel = pageControl.FindControl("UserLevel") as ASPxComboBox;
            ASPxComboBox userStatus = pageControl.FindControl("UserStatus") as ASPxComboBox;
            ASPxTextBox domainAcc = pageControl.FindControl("DomainAccount") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string sEntCode = entCode.Value.ToString();
            string sBUCode = buCode.Value.ToString();

            string sDomainAcc = "";
            if (domainAcc.Value != null)
            {
                sDomainAcc = EncryptionClass.Encrypt(domainAcc.Value.ToString());
            }

            int sUserLevel = Convert.ToInt32(userLevel.Value.ToString());
            int sUserStatus = Convert.ToInt32(userStatus.Value.ToString());

            string update_User = "UPDATE tbl_Users " +
                                 " SET [EntityCode] = @EntCode, " +
                                 " [BUCode] = @BUCode, " +
                                 " [DomainAccount] = @DomainAccount, " +
                                 " [UserLevelKey] = @UserLevelKey, " +
                                 " [Active] = @Active " +
                                 " WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_User, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@EntCode", sEntCode);
            cmd.Parameters.AddWithValue("@BUCode", sBUCode);
            cmd.Parameters.AddWithValue("@DomainAccount", sDomainAcc);
            cmd.Parameters.AddWithValue("@UserLevelKey", sUserLevel);
            cmd.Parameters.AddWithValue("@Active", sUserStatus);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindUserList();
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void UserListGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "Edit")
            {
                if (UserListGrid.FocusedRowIndex > -1)
                {
                    //Session["UserListKey"] = UserList.GetRowValues(UserList.FocusedRowIndex, "PK").ToString();
                    //Response.RedirectLocation = "mrp_addedit.aspx";
                }

            }
            else if (e.ButtonID == "Delete")
            {
                if (UserListGrid.FocusedRowIndex > -1)
                {
                    string PK = UserListGrid.GetRowValues(UserListGrid.FocusedRowIndex, "PK").ToString();
                    string delete = "DELETE FROM [dbo].[tbl_Users] WHERE [PK] ='" + PK + "'";
                    SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(delete, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //BindMRP();
                }
            }
        }

        protected void UserLevel_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = AccountClass.UserLevelTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 0;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            l_ValueField.Caption = "LEVEL";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            //MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "UserLevelKey").ToString();
            }
        }

        protected void UserStatus_Init(object sender, EventArgs e)
        {
            DataTable dtRecord = AccountClass.UserLevelTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "CODE";
            l_ValueField.Width = 0;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            l_ValueField.Caption = "LEVEL";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            //MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "StatusKey").ToString();
            }
        }

        protected void BUCallBackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            //BUCode.value = "";

        }
    }
}