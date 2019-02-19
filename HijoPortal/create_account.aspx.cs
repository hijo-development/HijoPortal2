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
    public partial class create_account : System.Web.UI.Page
    {
        int iEmployeeKey = 0, iAccountType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TimerLogIn.Enabled = false;
                iAccountType = 0;
                iEmployeeKey = 0;
                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtMiddleName.Text = "";
                txtEmailAdd.Text = "";
                txtDomainAccount.Text = "";
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                lblStatus.Text = "";

                lblDomainAccount.Visible = false;
                txtDomainAccount.Visible = false;
                lblIDNum.Visible = false;
                txtIDNumber.Visible = false;

                DataTable dtUserType = AccountClass.UserTypeTable();
                AccountType.DataSource = dtUserType;
                AccountType.DataTextField = "UserType";
                AccountType.DataValueField = "PK";
                AccountType.DataBind();
            }
        }

        protected void AccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            iEmployeeKey = 0;
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtEmailAdd.Text = "";
            txtDomainAccount.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            lblStatus.Text = "";
            iAccountType = Convert.ToInt32(AccountType.SelectedValue);

            if (iAccountType == 1)
            {
                //lblDomainAccount.Visible = true;
                //txtDomainAccount.Visible = true;
                lblIDNum.Visible = true;
                txtIDNumber.Visible = true;
                txtIDNumber.Focus();
                txtLastName.Enabled = false;
                txtFirstName.Enabled = false;
                txtMiddleName.Enabled = false;
            }
            else
            {
                //lblDomainAccount.Visible = false;
                //txtDomainAccount.Visible = false;
                lblIDNum.Visible = false;
                txtIDNumber.Visible = false;
                txtLastName.Enabled = true;
                txtFirstName.Enabled = true;
                txtMiddleName.Enabled = true;
                txtLastName.Focus();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(AccountType.SelectedValue) == 1)
            {
                //if (int.Parse(txtEmployeeKey.Text.ToString()) == 0)
                if (int.Parse(txtEmployeeKey.Text.ToString()) == 0)
                {
                    lblStatus.Text = "Please supply ID Number!";
                    txtIDNumber.Focus();
                    return;
                }
            }
            if (txtLastName.Text.Trim() == "") { lblStatus.Text = "Please supply lastname!"; txtLastName.Focus(); return; }
            if (txtFirstName.Text.Trim() == "") { lblStatus.Text = "Please supply firstname!"; txtFirstName.Focus(); return; }
            if (txtMiddleName.Text.Trim() == "") { lblStatus.Text = "Please supply middlename!"; txtMiddleName.Focus(); return; }
            if (txtEmailAdd.Text.Trim() == "") { lblStatus.Text = "Please supply email address!"; txtEmailAdd.Focus(); return; }
            if (txtUserName.Text.Trim() == "") { lblStatus.Text = "Please supply username!"; txtUserName.Focus(); return; }
            if (txtPassword.Text.Trim() == "") { lblStatus.Text = "Please supply password!"; txtPassword.Focus(); return; }
            if (txtConfirmPassword.Text.Trim() == "") { lblStatus.Text = "Please supply confirm password!"; txtConfirmPassword.Focus(); return; }

            string NewPW = txtPassword.Text.ToString().Trim();
            string ConPW = txtConfirmPassword.Text.ToString().Trim();
            bool result = NewPW.Equals(ConPW, StringComparison.CurrentCulture);

            if (result == false)
            {
                lblStatus.Text = "Password not match";
                txtConfirmPassword.Focus();
                return;
            }

            //if (txtPassword.Text.ToString().Trim() != txtConfirmPassword.Text.ToString().Trim())
            //{
            //    lblStatus.Text = "Password not match!"; txtPassword.Focus(); return;
            //}

            AccountClass.SaveAccount(1, 0, txtLastName.Text.ToString().Trim(), txtFirstName.Text.ToString().Trim(),
                                     txtMiddleName.Text.ToString().Trim(), txtEmailAdd.Text.ToString().Trim(),
                                     Convert.ToInt32(AccountType.SelectedValue), txtUserName.Text.ToString().Trim(), txtPassword.Text.ToString().Trim(),
                                     int.Parse(txtEmployeeKey.Text.ToString()), txtDomainAccount.Text.ToString().Trim());

            //lblUserType.Text = Convert.ToInt32(AccountType.SelectedValue).ToString();
            lblStatus.Text = GlobalClass.QueryError.ToString();

            if (lblStatus.Text.ToString().Trim() == "Account saved!")
            {
                TimerLogIn.Enabled = true;
            }

        }

        protected void TimerLogIn_Tick(object sender, EventArgs e)
        {
            TimerLogIn.Enabled = false;
            Response.Redirect("default.aspx");
        }

        protected void txtIDNumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearchIDNum_Click(object sender, EventArgs e)
        {
            if (txtIDNumber.Text.ToString().Trim() != "")
            {
                DataTable dtEmployee = AccountClass.GetEmployeeInfo(txtIDNumber.Text.ToString());
                if (dtEmployee.Rows.Count > 0)
                {
                    foreach (DataRow row in dtEmployee.Rows)
                    {
                        txtEmployeeKey.Text = row["EmployeeKey"].ToString();
                        txtLastName.Text = row["LastName"].ToString();
                        txtFirstName.Text = row["FirstName"].ToString();
                        txtMiddleName.Text = row["MiddleName"].ToString();
                        txtEmailAdd.Text = row["Email"].ToString();
                        //txtDomainAccount.Text = row["DomainAccount"].ToString();
                    }
                }
                dtEmployee.Clear();
            }
        }
    }
}