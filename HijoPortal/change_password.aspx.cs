using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class change_password : System.Web.UI.Page
    {
        private void CheckSessionExpire()
        {
            if (Session["CreatorKey"] == null)
            {
                Response.Redirect("default.aspx");
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSessionExpire();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                DataTable dtUser = AccountClass.UserList();
                dtUser.CaseSensitive = true;
                string expression = "PK = '" + Session["CreatorKey"].ToString().Trim() + "'";
                string sortOrder = "PK ASC";
                DataRow[] foundRows;
                foundRows = dtUser.Select(expression, sortOrder);
                if (foundRows.Length > 0)
                {
                    oldPasswordCHDB.Text = foundRows[0]["Password"].ToString();
                }
            }
        }

        protected void btnChangePW_Click(object sender, EventArgs e)
        {
            if (captcha.IsValid && ASPxEdit.ValidateEditorsInContainer(this))
            {
                string qry = "", _sPassword = "";
                SqlCommand cmd = null;
                _sPassword = EncryptionClass.Encrypt(newPasswordCH.Text.ToString().Trim());

                SqlConnection con = new SqlConnection(GlobalClass.SQLConnString());
                con.Open();
                qry = "UPDATE tbl_Users " +
                      " SET Password = '" + _sPassword + "' " +
                      " WHERE (PK = " + Session["CreatorKey"] + ")";
                cmd = new SqlCommand(qry);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect("home.aspx");
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}