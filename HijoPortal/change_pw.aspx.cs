using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HijoPortal.classes;

namespace HijoPortal
{
    public partial class change_pw : System.Web.UI.Page
    {
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
                txtOldPassword.Focus();
            }
        }

        protected void btnChangePW_Click(object sender, EventArgs e)
        {
            if (txtOldPassword.Text.ToString().Trim() == "")
            {
                lblerror.Text = "Please supply old password";
                txtOldPassword.Focus();
                return;
            }
            if (txtNewPassword.Text.ToString().Trim() == "")
            {
                lblerror.Text = "Please supply new password";
                txtOldPassword.Focus();
                return;
            }
            if (txtConfirmPassword.Text.ToString().Trim() == "")
            {
                lblerror.Text = "Please supply confirm password";
                txtOldPassword.Focus();
                return;
            }

            //if (txtNewPassword.Text.ToString().Trim() != txtConfirmPassword.Text.ToString().Trim())
            //{
            //    lblerror.Text = "Password not match";
            //    txtConfirmPassword.Focus();
            //    return;
            //}

            string NewPW = txtNewPassword.Text.ToString().Trim();
            string ConPW = txtConfirmPassword.Text.ToString().Trim();
            bool result = NewPW.Equals(ConPW, StringComparison.CurrentCulture);

            if (result == false)
            {
                lblerror.Text = "Password not match";
                txtOldPassword.Focus();
                return;
            }

            DataTable dtUser = AccountClass.UserList();
            dtUser.CaseSensitive = true;
            string expression = "UserName = '" + Session["UserName"].ToString().Trim() + "' AND Password = '" + txtOldPassword.Text.ToString().Trim() + "'";
            string sortOrder = "PK ASC";
            DataRow[] foundRows;
            foundRows = dtUser.Select(expression, sortOrder);
            if (foundRows.Length > 0)
            {
                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                string update_User = "UPDATE tbl_Users " +
                                     " SET [Password] = @Password " +
                                     " WHERE [PK] = @PK";

                SqlCommand cmd = new SqlCommand(update_User, conn);
                cmd.Parameters.AddWithValue("@PK", Session["CreatorKey"].ToString());
                cmd.Parameters.AddWithValue("@Password", EncryptionClass.Encrypt(txtNewPassword.Text.ToString().Trim()));
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("home.aspx");
            }
            else
            {
                lblerror.Text = "Incorrect old password";
                txtOldPassword.Focus();
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}