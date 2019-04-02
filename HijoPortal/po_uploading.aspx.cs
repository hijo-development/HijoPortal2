using System;
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
    public partial class po_uploading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            BindGrid();
        }

        private void BindGrid()
        {
            POGrid.DataSource = POClass.PO_Uploading_Table();
            POGrid.KeyFieldName = "PK";
            POGrid.DataBind();
        }
    }
}