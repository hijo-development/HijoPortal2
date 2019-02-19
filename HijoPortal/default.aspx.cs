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
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("create_account.aspx");
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.ToString().Trim() == "")
            {
                lblerror.Text = "Please supply username";
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text.ToString().Trim() == "")
            {
                lblerror.Text = "Please supply password";
                txtPassword.Focus();
                return;
            }

            DataTable dtUser = AccountClass.UserList();
            dtUser.CaseSensitive = true;
            string expression = "UserName = '" + txtUserName.Text.ToString().Trim() + "' AND Password = '" + txtPassword.Text.ToString().Trim() + "'";
            string sortOrder = "PK ASC";
            DataRow[] foundRows;
            foundRows = dtUser.Select(expression, sortOrder);
            if (foundRows.Length > 0)
            {
                Session["CreatorKey"] = foundRows[0]["PK"].ToString();
                Session["UserName"] = foundRows[0]["UserName"].ToString();
                Session["UserCompleteName"] = foundRows[0]["Lastname"].ToString() + ",  " + foundRows[0]["Firstname"].ToString();
                if (Convert.ToInt32(foundRows[0]["UserType"]) == 1)
                {
                    Session["EntityCode"] = foundRows[0]["EntityCode"].ToString();
                    Session["BUCode"] = foundRows[0]["BUCode"].ToString();
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                lblerror.Text = "Invalid Login Details. Try to enter Username/password Carefully";
            }
            
        }
    }
}