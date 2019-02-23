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
    public partial class mrp_inventanalyst : System.Web.UI.Page
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

            if (bindDM) BindDirectMaterials(docnumber); else bindDM = true;
            if (bindOpex) BindOpex(docnumber); else bindOpex = true;
            if (bindManPower) BindManPower(docnumber); else bindManPower = true;
            if (bindCapex) BindCapex(docnumber); else bindCapex = true;
        }

        private void BindDirectMaterials(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_Direct_Materials(DOC_NUMBER);
            DMGrid.DataSource = dtRecord;
            DMGrid.KeyFieldName = "PK";
            DMGrid.DataBind();
        }

        private void BindOpex(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_OPEX(DOC_NUMBER);
            OpGrid.DataSource = dtRecord;
            OpGrid.KeyFieldName = "PK";
            OpGrid.DataBind();
        }

        private void BindManPower(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_ManPower(DOC_NUMBER);
            ManPoGrid.DataSource = dtRecord;
            ManPoGrid.KeyFieldName = "PK";
            ManPoGrid.DataBind();
        }


        private void BindCapex(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_CAPEX(DOC_NUMBER);
            CapGrid.DataSource = dtRecord;
            CapGrid.KeyFieldName = "PK";
            CapGrid.DataBind();
        }



        protected void DMGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindDM = false;
        }

        protected void DMGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "InvEdittedQty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "InvEdittedCost") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittiedTotalCost"], "InvEdittiedTotalCost") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.DirectMatTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittiedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty.Value.ToString());
            cmd.Parameters.AddWithValue("@COST", cost.Value.ToString());
            cmd.Parameters.AddWithValue("@TOTAL", total.Value.ToString());
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0) MRPClass.UpdateLastModified(conn, docnumber);

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindDM = true;
            BindDirectMaterials(docnumber);
        }



        protected void OpGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindOpex = false;
        }

        protected void OpGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "InvEdittedQtyOp") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "InvEdittedCostOp") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedTotalCost"], "InvEdittiedTotalCostOp") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.OpexTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty.Value.ToString());
            cmd.Parameters.AddWithValue("@COST", cost.Value.ToString());
            cmd.Parameters.AddWithValue("@TOTAL", total.Value.ToString());
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0) MRPClass.UpdateLastModified(conn, docnumber);

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindOpex = true;
            BindOpex(docnumber);

        }

        protected void ManPoGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindManPower = false;
        }

        protected void ManPoGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "InvEdittedQtyManPo") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "InvEdittedCostManPo") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittiedTotalCost"], "InvEdittiedTotalCostManPo") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.ManPowerTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittiedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty.Value.ToString());
            cmd.Parameters.AddWithValue("@COST", cost.Value.ToString());
            cmd.Parameters.AddWithValue("@TOTAL", total.Value.ToString());
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0) MRPClass.UpdateLastModified(conn, docnumber);

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindManPower = true;
            BindManPower(docnumber);
        }

        protected void CapGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindCapex = true;
        }

        protected void CapGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "InvEdittedQtyCapex") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "InvEdittedCostCapex") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittiedTotalCost"], "InvEdittiedTotalCostCapex") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.CapexTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittiedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty.Value.ToString());
            cmd.Parameters.AddWithValue("@COST", cost.Value.ToString());
            cmd.Parameters.AddWithValue("@TOTAL", total.Value.ToString());
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0) MRPClass.UpdateLastModified(conn, docnumber);

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindCapex = true;
            BindCapex(docnumber);

        }
    }
}