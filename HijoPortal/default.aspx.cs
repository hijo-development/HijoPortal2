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
            if (Session["CreatorKey"] != null)
                Response.Redirect(Constants.HomePage());
            else
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
                Session["EmployeeKey"] = foundRows[0]["EmployeeKey"].ToString();
                Session["FirstName"] = foundRows[0]["Firstname"].ToString();
                if (Convert.ToInt32(foundRows[0]["UserType"]) == 1)
                {
                    Session["EntityCode"] = foundRows[0]["EntityCode"].ToString();
                    Session["EntityCodeDesc"] = foundRows[0]["EntityCodeDesc"].ToString();
                    Session["BUCode"] = foundRows[0]["BUCode"].ToString();
                    Session["BUCodeDesc"] = foundRows[0]["BUCodeDesc"].ToString();
                    Session["isAdmin"] = foundRows[0]["UserLevelKey"].ToString();

                    Session["viewAllMRP"] = "0";
                    //if (GlobalClass.IsSuperAdmin(Convert.ToInt32(foundRows[0]["PK"])))
                    //{
                    //    Session["viewAllMRP"] = "1";
                    //}
                    //else
                    //{
                        if (GlobalClass.IsAdmin(Convert.ToInt32(foundRows[0]["PK"])) || GlobalClass.IsSuperAdmin(Convert.ToInt32(foundRows[0]["PK"])))
                        {
                            Session["viewAllMRP"] = "1";
                        }
                        else
                        {
                            if (GlobalClass.IsAllowed(Convert.ToInt32(foundRows[0]["PK"]), "MOPInventoryAnalyst", DateTime.Now, foundRows[0]["EntityCode"].ToString(), foundRows[0]["BUCode"].ToString(), "") || GlobalClass.IsSuperAdmin(Convert.ToInt32(foundRows[0]["PK"])))
                            {
                                Session["viewAllMRP"] = "1";
                            }
                            else
                            {
                                if (GlobalClass.IsAllowed(Convert.ToInt32(foundRows[0]["PK"]), "MOPSCMLead", DateTime.Now, foundRows[0]["EntityCode"].ToString(), foundRows[0]["BUCode"].ToString(), "") || GlobalClass.IsSuperAdmin(Convert.ToInt32(foundRows[0]["PK"])))
                                {
                                    Session["viewAllMRP"] = "1";
                                }
                                else
                                {
                                    if (GlobalClass.IsAllowed(Convert.ToInt32(foundRows[0]["PK"]), "MOPFinanceLead", DateTime.Now, foundRows[0]["EntityCode"].ToString(), foundRows[0]["BUCode"].ToString(), "") || GlobalClass.IsSuperAdmin(Convert.ToInt32(foundRows[0]["PK"])))
                                    {
                                        Session["viewAllMRP"] = "1";
                                    }
                                    else
                                    {
                                        if (GlobalClass.IsAllowed(Convert.ToInt32(foundRows[0]["PK"]), "MOPExecutive", DateTime.Now, foundRows[0]["EntityCode"].ToString(), foundRows[0]["BUCode"].ToString(), "") || GlobalClass.IsSuperAdmin(Convert.ToInt32(foundRows[0]["PK"])))
                                        {
                                            Session["viewAllMRP"] = "1";
                                        }
                                    }
                                }
                            }
                        }
                    //}
                    

                    if (Convert.ToUInt32(foundRows[0]["StatusKey"]) == 1)
                    {
                        Response.Redirect("home.aspx");
                    }
                    else
                    {
                        lblerror.Text = "Your account is inactive, Please call administrator.";
                    }
                }
            }
            else
            {
                lblerror.Text = "Invalid Login Details. Try to enter Username/password Carefully";
            }

        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            string gencode = DateTime.Now.Ticks.ToString("x");
        }
    }
}