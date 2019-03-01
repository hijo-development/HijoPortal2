using DevExpress.Web;
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
    public partial class mrp_capexcip : System.Web.UI.Page
    {
        private static int mrp_key = 0;
        private static string docnumber = "", entitycode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                //Rsize
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                docnumber = Request.Params["DocNum"].ToString();
                string query = "SELECT TOP (100) PERCENT  tbl_MRP_List.*, vw_AXEntityTable.NAME AS EntityCodeDesc, vw_AXOperatingUnitTable.NAME AS BUCodeDesc, tbl_MRP_Status.StatusName, tbl_Users.Lastname, tbl_Users.Firstname FROM   tbl_MRP_List INNER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK LEFT OUTER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK LEFT OUTER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID WHERE dbo.tbl_MRP_List.DocNumber = '" + docnumber + "' ORDER BY dbo.tbl_MRP_List.DocNumber DESC";

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();

                string firstname = "", lastname = "";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mrp_key = Convert.ToInt32(reader["PK"].ToString());
                    entitycode = reader["EntityCode"].ToString();
                    DocNum.Text = reader["DocNumber"].ToString();
                    DateCreated.Text = reader["DateCreated"].ToString();
                    EntityCode.Text = reader["EntityCodeDesc"].ToString();
                    BUCode.Text = reader["BUCodeDesc"].ToString();
                    Month.Text = MRPClass.Month_Name(Int32.Parse(reader["MRPMonth"].ToString()));
                    Year.Text = reader["MRPYear"].ToString();
                    Status.Text = reader["StatusName"].ToString();
                    firstname = reader["Firstname"].ToString();
                    lastname = reader["Lastname"].ToString();

                }
                reader.Close();
                conn.Close();

                Creator.Text = EncryptionClass.Decrypt(firstname) + " " + EncryptionClass.Decrypt(lastname);

                CapexRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] Capital Expenditure";
                CapexRoundPanel.Font.Bold = true;
                CapexRoundPanel.Collapsed = true;

                ASPxPageControl1.Font.Bold = true;
                ASPxPageControl1.Font.Size = 12;
            }

            BindCapex(docnumber);
        }
        private void CheckCreatorKey()
        {
            if (Session["CreatorKey"] == null)
            {
                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback(MRPClass.DefaultPage());
                else
                    Response.Redirect("default.aspx");

                return;
            }
        }

        private void BindCapex(string DOC_NUMBER)
        {
            //CAPEXCIP.DataSource = MRPClass.MRPInvent_CAPEX(DOC_NUMBER, entitycode);
            //CAPEXCIP.KeyFieldName = "PK";
            //CAPEXCIP.DataBind();
        }

        protected void CAPEXCIP_DataBound(object sender, EventArgs e)
        {

        }
    }
}