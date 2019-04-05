using HijoPortal.classes;
using System;
namespace HijoPortal
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            //System.Guid guid = System.Guid.NewGuid();
            string gencode = DateTime.Now.Ticks.ToString("x");
            
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}