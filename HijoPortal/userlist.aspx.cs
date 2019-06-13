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
    public partial class userlist : System.Web.UI.Page
    {
        private static bool bindUserList = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CreatorKey"] == null)
            {
                Response.Redirect("default.aspx");
                return;
            }

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

        protected void UserList_DataBound(object sender, EventArgs e)
        {
            //GridViewDataColumn colPK = MainTable.Columns["PK"] as GridViewDataColumn;
            //GridViewDataColumn colDateCreated = MainTable.Columns["DateCreated"] as GridViewDataColumn;
            //GridViewDataColumn colMRPMonth = MainTable.Columns["MRPMonth"] as GridViewDataColumn;
            //GridViewDataColumn colStatusKey = MainTable.Columns["StatusKey"] as GridViewDataColumn;
            //GridViewDataColumn colCreatorKey = MainTable.Columns["CreatorKey"] as GridViewDataColumn;
            //GridViewDataColumn colLastModified = MainTable.Columns["LastModified"] as GridViewDataColumn;

            //colPK.Visible = false;
            //colDateCreated.Visible = false;
            //colMRPMonth.Visible = false;
            //colStatusKey.Visible = false;
            //colCreatorKey.Visible = false;
            //colLastModified.Visible = false;


            //GridViewDataColumn colMonthDesc = MainTable.Columns["MRPMonthDesc"] as GridViewDataColumn;
            //GridViewDataColumn colStatusDesc = MainTable.Columns["StatusKeyDesc"] as GridViewDataColumn;

            //colMonthDesc.Caption = "Month";
            //colStatusDesc.Caption = "Status";
        }

        protected void UserList_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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


        protected void EntityCode_Init(object sender, EventArgs e)
        {

            DataTable dtRecord = GlobalClass.EntityTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "ID";
            l_ValueField.Caption = "NAME";
            l_ValueField.Width = 30;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "NAME";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "EntityCode").ToString();
            }

        }

        protected void BUCode_Init(object sender, EventArgs e)
        {
            string query = "";

            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("UserPageControl") as ASPxPageControl;
            ASPxTextBox entCode = pageControl.FindControl("EntityValue") as ASPxTextBox;
            
            if (entCode.Value.ToString().Trim() != "")
            {
                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();

                query = "SELECT [OMOPERATINGUNITNUMBER] AS ID, [NAME] AS BUCodeDesc " +
                        " FROM [dbo].[vw_AXOperatingUnitTable] " +
                        " WHERE (entity = '" + entCode.Value.ToString().Trim() + "') ";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                ASPxComboBox combo = sender as ASPxComboBox;
                combo.Items.Clear();
                while (reader.Read())
                {

                    ListEditItem item = new ListEditItem
                    {
                        Value = reader[0].ToString(),
                        Text = reader[1].ToString()
                    };
                    combo.Items.Add(item);
                }

                conn.Close();
            } else
            {
                ASPxComboBox combo = sender as ASPxComboBox;
                combo.Items.Clear();
            }
            
        }

        protected void EmployeeLevel_Init(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string query = "SELECT [PK] AS ID, [UserLevel] AS UserTypeDesc FROM [dbo].[tbl_UserLevel]";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ASPxComboBox combo = sender as ASPxComboBox;

            while (reader.Read())
            {

                ListEditItem item = new ListEditItem
                {
                    Value = reader[0].ToString(),
                    Text = reader[1].ToString()
                };
                combo.Items.Add(item);
            }

            conn.Close();
        }

        protected void UserStatus_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            ListEditItem item = new ListEditItem
            {
                Value = "1",
                Text = "Active"
            };
            combo.Items.Add(item);

            ListEditItem item1 = new ListEditItem
            {
                Value = "0",
                Text = "Inactive"
            };
            combo.Items.Add(item1);
        }

        protected void UserList_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindUserList = false;
        }

        protected void UserList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("UserPageControl") as ASPxPageControl;
            //ASPxTextBox entCode = pageControl.FindControl("EntityValue") as ASPxTextBox;
            ASPxComboBox entCode = pageControl.FindControl("EntityCode") as ASPxComboBox;
            ASPxTextBox buCode = pageControl.FindControl("BUValue") as ASPxTextBox;
            ASPxTextBox domainAcc = pageControl.FindControl("DomainAccount") as ASPxTextBox;
            ASPxTextBox userLevel = pageControl.FindControl("UserLevelValue") as ASPxTextBox;
            ASPxTextBox userStatus = pageControl.FindControl("UserStatusValue") as ASPxTextBox;

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

        protected void BUCodeCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "";

            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("UserPageControl") as ASPxPageControl;
            ASPxTextBox entCode = pageControl.FindControl("EntityValue") as ASPxTextBox;

            if (entCode.Value.ToString().Trim() != "")
            {
                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();

                query = "SELECT [OMOPERATINGUNITNUMBER] AS ID, [NAME] AS BUCodeDesc " +
                        " FROM [dbo].[vw_AXOperatingUnitTable] " +
                        " WHERE (entity = '" + entCode.Value.ToString().Trim() + "') ";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                ASPxComboBox combo = sender as ASPxComboBox;
                combo.Items.Clear();
                while (reader.Read())
                {

                    ListEditItem item = new ListEditItem
                    {
                        Value = reader[0].ToString(),
                        Text = reader[1].ToString()
                    };
                    combo.Items.Add(item);
                }

                conn.Close();
            }
            else
            {
                ASPxComboBox combo = sender as ASPxComboBox;
                combo.Items.Clear();
            }
        }
    }
}