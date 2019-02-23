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
    public partial class mrp_forapproval : System.Web.UI.Page
    {
        private static string docnumber = "";
        private static bool bindDM = true, bindOpex = true, bindManPower = true, bindCapex = true;
        protected void Page_Load(object sender, EventArgs e)
        {
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

                DirectMaterialsRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] Direct Materials";
                OpexRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] Operational Expense";
                ManpowerRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] Man Power";
                CapexRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] Capital Expenditure";

                DirectMaterialsRoundPanel.Font.Bold = true;
                OpexRoundPanel.Font.Bold = true;
                ManpowerRoundPanel.Font.Bold = true;
                CapexRoundPanel.Font.Bold = true;

                DirectMaterialsRoundPanel.Collapsed = true;
                OpexRoundPanel.Collapsed = true;
                ManpowerRoundPanel.Collapsed = true;
                CapexRoundPanel.Collapsed = true;

                ASPxPageControl1.Font.Bold = true;
                ASPxPageControl1.Font.Size = 12;
            }

            //if (bindDM) BindDirectMaterials(docnumber); else bindDM = true;
            //if (bindOpex) BindOpex(docnumber); else bindOpex = true;
            //if (bindManPower) BindManPower(docnumber); else bindManPower = true;
            //if (bindCapex) BindCapex(docnumber); else bindCapex = true;
        }
    }
}