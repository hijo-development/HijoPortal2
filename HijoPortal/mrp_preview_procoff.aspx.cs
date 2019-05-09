using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_preview_procoff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                //Session["mrp_docNum"] = docnumber.ToString();
                //Session["mrp_wrkLine"] = wrkflwln.ToString();
                //string docnumber = Request.Params["DocNum"].ToString();
                string docnumber = Session["mrp_docNum"].ToString();
                int source = Convert.ToInt32(Session["mrp_source"]);
                string query = "SELECT dbo.tbl_MRP_List.DocNumber, dbo.tbl_MRP_List.MRPMonth, dbo.tbl_MRP_List.MRPYear, dbo.tbl_MRP_List.DateCreated, dbo.vw_AXEntityTable.NAME AS EntityName, dbo.vw_AXOperatingUnitTable.NAME AS BU, dbo.tbl_Users.Firstname, dbo.tbl_Users.Lastname, dbo.tbl_MRP_Status.StatusName FROM   dbo.tbl_MRP_List LEFT OUTER JOIN dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN dbo.tbl_Users ON dbo.tbl_MRP_List.CreatorKey = dbo.tbl_Users.PK LEFT OUTER JOIN dbo.tbl_MRP_Status ON dbo.tbl_MRP_List.StatusKey = dbo.tbl_MRP_Status.PK WHERE DocNumber = '" + docnumber + "'";

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DocNum.Text = reader["DocNumber"].ToString();
                    Month.Text = Convertion.INDEX_TO_MONTH(Convert.ToInt32(reader["MRPMonth"].ToString()));
                    Year.Text = reader["MRPYear"].ToString();
                    Entity.Text = reader["EntityName"].ToString();
                    Department.Text = reader["BU"].ToString();
                    Status.Text = reader["StatusName"].ToString();
                    Creator.Text = EncryptionClass.Decrypt(reader["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(reader["Lastname"].ToString());
                    DateCreated.Text = reader["DateCreated"].ToString();

                }

                BindGrid(docnumber);
            }
        }

        private void BindGrid(string docnum)
        {
            DM_Grid.DataSource = Preview.DirectMaterials(docnum);
            DM_Grid.KeyFieldName = "PK";
            DM_Grid.DataBind();

            OP_Grid.DataSource = Preview.OperatingExpense(docnum);
            OP_Grid.KeyFieldName = "PK";
            OP_Grid.DataBind();

            CA_Grid.DataSource = Preview.CapitalExpenditure(docnum);
            CA_Grid.KeyFieldName = "PK";
            CA_Grid.DataBind();
        }
    }
}