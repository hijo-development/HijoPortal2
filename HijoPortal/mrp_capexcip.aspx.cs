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
        private static string docnumber = "", entitycode = "", month = "", year = "", pk = "";
        private static bool bindCapexCIP = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {

                bool isAllowed = false;
                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryOfficer", DateTime.Now);
                if (isAllowed == false)
                {
                    Response.Redirect("home.aspx");
                } else
                {
                    //Rsize
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                }
                

                //docnumber = Request.Params["DocNum"].ToString();
                //string query = "SELECT TOP (100) PERCENT  tbl_MRP_List.*, vw_AXEntityTable.NAME AS EntityCodeDesc, vw_AXOperatingUnitTable.NAME AS BUCodeDesc, tbl_MRP_Status.StatusName, tbl_Users.Lastname, tbl_Users.Firstname FROM   tbl_MRP_List INNER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK LEFT OUTER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK LEFT OUTER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID WHERE dbo.tbl_MRP_List.DocNumber = '" + docnumber + "' ORDER BY dbo.tbl_MRP_List.DocNumber DESC";

                //SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                //conn.Open();

                //string firstname = "", lastname = "";

                //SqlCommand cmd = new SqlCommand(query, conn);
                //SqlDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    mrp_key = Convert.ToInt32(reader["PK"].ToString());
                //    entitycode = reader["EntityCode"].ToString();
                //    //DocNum.Text = reader["DocNumber"].ToString();
                //    //DateCreated.Text = reader["DateCreated"].ToString();
                //    //EntityCode.Text = reader["EntityCodeDesc"].ToString();
                //    //BUCode.Text = reader["BUCodeDesc"].ToString();
                //    //Month.Text = MRPClass.Month_Name(Int32.Parse(reader["MRPMonth"].ToString()));
                //    //Year.Text = reader["MRPYear"].ToString();
                //    //Status.Text = reader["StatusName"].ToString();
                //    //firstname = reader["Firstname"].ToString();
                //    //lastname = reader["Lastname"].ToString();

                //}
                //reader.Close();
                //conn.Close();

                //Creator.Text = EncryptionClass.Decrypt(firstname) + " " + EncryptionClass.Decrypt(lastname);

                //CapexRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] Capital Expenditure";
                //CapexRoundPanel.Font.Bold = true;
                //CapexRoundPanel.Collapsed = true;

                //ASPxPageControl1.Font.Bold = true;
                //ASPxPageControl1.Font.Size = 12;

                month = ""; year = ""; pk = "";
            }

            if (bindCapexCIP)
                BindCapex(month, year);
            else
                bindCapexCIP = true;
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

        protected void CAPEXCIP_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindCapexCIP = false;
        }

        protected void CAPEXCIP_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox CIP = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["CIPSIPNumber"], "CIPSIPNumber") as ASPxTextBox;
            string PK = e.Keys[0].ToString();
            string cip_text = CIP.Text.ToString();
           
            if (!string.IsNullOrEmpty(cip_text))
            {
                //MRPClass.PrintString(cip_text + " " + PK );
                string update = "UPDATE [dbo].[tbl_MRP_List_CAPEX] SET [CIPSIPNumber] = '" + cip_text + "' WHERE [PK] = '" + PK + "'";
                //MRPClass.PrintString(update);

                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();

                SqlCommand cmd = new SqlCommand(update, conn);
                int result = cmd.ExecuteNonQuery();


                //MRPClass.PrintString(result.ToString());
                conn.Close();
            }

            grid.CancelEdit();
            e.Cancel = true;
            BindCapex(month, year);
        }

        protected void CAPEXCIP_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void MRPmonthyear_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.MRPMonthYearTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PK";
            l_value.Width = 0;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "MRPMonth";
            combo.Columns.Add(l_text);

            ListBoxColumn l_text2 = new ListBoxColumn();
            l_text2.FieldName = "MRPYear";
            combo.Columns.Add(l_text2);

            combo.ValueField = "PK";
            combo.TextField = "MRPMonth";
            combo.DataBind();
            combo.TextFormatString = "{1} {2}";
        }

        protected void CAPEXCIP_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //MRPClass.PrintString("custom back back");
            //MRPClass.PrintString(MRPmonthyear.Text.ToString());

            string var = MRPmonthyear.Text.ToString();
            pk = MRPmonthyear.Value.ToString();
            int spaceindex = var.IndexOf(" ");
            int secondlength = var.Length - (spaceindex + 1);

            string monthvar = var.Substring(0, spaceindex);
            int monthIndex = Convert.ToDateTime("01-" + monthvar + "-2011").Month;
            month = monthIndex.ToString();
            year = var.Substring(spaceindex + 1, secondlength);

            BindCapex(month, year);
        }

        private void BindCapex(string month, string year)
        {
            if (string.IsNullOrEmpty(month) && string.IsNullOrEmpty(year))
                return;

            CAPEXCIP.DataSource = MRPClass.CAPEXCIP_Table(month, year);
            CAPEXCIP.KeyFieldName = "PK";
            CAPEXCIP.DataBind();
        }
    }
}