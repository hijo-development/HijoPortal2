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
    public partial class createaccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void signUp_Click(object sender, EventArgs e)
        {
            int iEmployeeKey = 0;
            MRPClass.PrintString("pass clicked signup");
            if (captcha.IsValid && ASPxEdit.ValidateEditorsInContainer(this))
            {
                //MRPClass.PrintString("pass inside validation");
                DataTable dt = new DataTable();
                SqlCommand cmd = null;
                SqlDataAdapter adp;
                
                string qry = "";
                using (SqlConnection conHRIS = new SqlConnection(GlobalClass.SQLConnStringHRIS()))
                {
                    //MRPClass.PrintString("pass inside hris");
                    qry = "SELECT PK, IDNumber FROM dbo.tbl_EmployeeIDNumber WHERE(IDNumber = '" + IDNumTextBox.Text.ToString() + "')";
                    cmd = new SqlCommand(qry);
                    cmd.Connection = conHRIS;
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        //MRPClass.PrintString("pass inside hris with id");
                        foreach (DataRow row in dt.Rows)
                        {
                            iEmployeeKey = Convert.ToInt32(row["PK"]);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            @"<script type=""text/javascript"">setTimeout(()=>{alert('ID Number not found in Employee MasterList!')},0);</script>");
                        return;
                    }
                    dt.Clear();
                    conHRIS.Close();
                }

                DataTable dtUser = AccountClass.UserList();

                //dtUser.CaseSensitive = true;
                string expressionID = "EmployeeKey = '" + iEmployeeKey.ToString().Trim() + "'";
                string sortOrderID = "PK ASC";
                DataRow[] foundRowsID;
                foundRowsID = dtUser.Select(expressionID, sortOrderID);
                if (foundRowsID.Length > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            @"<script type=""text/javascript"">setTimeout(()=>{alert('Found Duplicate ID Number!')},0);</script>");
                    return;
                }

                dtUser.CaseSensitive = true;
                string expressionName = "Lastname = '" + lastNameTextBox.Text.ToString().Trim() + "' AND Firstname = '" + firstNameTextBox.Text.ToString().Trim() + "'";
                string sortOrderName = "PK ASC";
                DataRow[] foundRowsName;
                foundRowsName = dtUser.Select(expressionName, sortOrderName);
                if (foundRowsName.Length > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            @"<script type=""text/javascript"">setTimeout(()=>{alert('Found Duplicate Lastname and Firstname!')},0);</script>");
                    return;
                }

                string expressionEmail = "Email = '" + eMailTextBox.Text.ToString().Trim() + "'";
                string sortOrderEmail = "PK ASC";
                DataRow[] foundRowsEmail;
                foundRowsEmail = dtUser.Select(expressionEmail, sortOrderEmail);
                if (foundRowsEmail.Length > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            @"<script type=""text/javascript"">setTimeout(()=>{alert('Found Duplicate Email!')},0);</script>");
                    return;
                }

                string expressionUName = "Username = '" + userNameTextBox.Text.ToString().Trim() + "'";
                string sortOrderUName = "PK ASC";
                DataRow[] foundRowsUName;
                foundRowsUName = dtUser.Select(expressionUName, sortOrderUName);
                if (foundRowsUName.Length > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            @"<script type=""text/javascript"">setTimeout(()=>{alert('Found Duplicate Username!')},0);</script>");
                    return;
                }

                using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnString()))
                {
                    string _sLastName, _sFirstName, _sEmail, _sUserName, _sPassword, _sIDNum;

                    _sLastName = EncryptionClass.Encrypt(lastNameTextBox.Text.ToString().Trim());
                    _sFirstName = EncryptionClass.Encrypt(firstNameTextBox.Text.ToString().Trim());
                    _sEmail = EncryptionClass.Encrypt(eMailTextBox.Text.ToString().Trim());
                    _sUserName = EncryptionClass.Encrypt(userNameTextBox.Text.ToString().Trim());
                    _sPassword = EncryptionClass.Encrypt(passwordTextBox.Text.ToString().Trim());
                    _sIDNum = EncryptionClass.Encrypt(IDNumTextBox.Text.ToString().Trim());

                    con.Open();

                    qry = "INSERT INTO tbl_Users " +
                          " (Lastname, Firstname, Username, Password, Email, EmployeeKey) " +
                          " VALUES ('" + _sLastName + "', '" + _sFirstName + "', '" + _sUserName + "', " +
                          " '" + _sPassword + "', '" + _sEmail + "', " + iEmployeeKey + ")"; ;
                    try
                    {
                        cmd = new SqlCommand(qry);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                @"<script type=""text/javascript"">setTimeout(()=>{alert('You have successfully registered')},0);</script>");

                        //MRPClass.PrintString("pass saved");

                        Response.Redirect("default.aspx");

                    }
                    catch (SqlException ex)
                    {
                        //MRPClass.PrintString(ex.ToString());
                        con.Close();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                @"<script type=""text/javascript"">setTimeout(()=>{alert('" + ex.ToString() + "')},0);</script>");
                    }
                }
            }
        }

        protected void CallbackPanelIDNum_Callback(object sender, CallbackEventArgsBase e)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            string qry = "";
            IDNumTextBoxMatch.Text = "";
            //IDNumTextBoxMatch.Value = "";
            using (SqlConnection conHRIS = new SqlConnection(GlobalClass.SQLConnStringHRIS()))
            {
                qry = "SELECT PK, IDNumber FROM dbo.tbl_EmployeeIDNumber WHERE(IDNumber = '" + IDNumTextBox.Text.ToString() + "')";
                cmd = new SqlCommand(qry);
                cmd.Connection = conHRIS;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        IDNumTextBoxMatch.Text = row["IDNumber"].ToString();
                        //IDNumTextBoxMatch.Value = row["IDNumber"].ToString();
                    }
                }
                dt.Clear();
                conHRIS.Close();
            }

            //if (IDNumTextBoxMatch.Text.Trim() == "")
            //{
            //    IDNumTextBox.IsValid = false;
            //    IDNumTextBox.ErrorText = "ID Number not found in Employee MasterList!";
            //} else
            //{
            //    IDNumTextBox.IsValid = true;
            //    IDNumTextBox.ErrorText = "";
            //}
        }
    }
}