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
    public partial class mrp_forapproval : System.Web.UI.Page
    {
        private static int mrp_key = 0;
        private static string docnumber = "", entitycode = "";
        private static bool bindDM = true, bindOpex = true, bindManPower = true, bindCapex = true;

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

                DirectMaterialsRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] " + Constants.DM_string();
                OpexRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] " + Constants.OP_string();
                ManpowerRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] " + Constants.MAN_string();
                CapexRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] " + Constants.CA_string();

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

        private void BindDirectMaterials(string DOC_NUMBER)
        {
            DMGridApproval.DataSource = MRPClass.MRPApproval_Direct_Materials(DOC_NUMBER, entitycode);
            DMGridApproval.KeyFieldName = "PK";
            DMGridApproval.DataBind();
        }

        private void BindOpex(string DOC_NUMBER)
        {
            OpexGridApproval.DataSource = MRPClass.MRPApproval_OPEX(DOC_NUMBER, entitycode);
            OpexGridApproval.KeyFieldName = "PK";
            OpexGridApproval.DataBind();
        }

        private void BindManPower(string DOC_NUMBER)
        {
            ManPowerGridApproval.DataSource = MRPClass.MRPApproval_ManPower(DOC_NUMBER, entitycode);
            ManPowerGridApproval.KeyFieldName = "PK";
            ManPowerGridApproval.DataBind();
        }

        private void BindCapex(string DOC_NUMBER)
        {
            CapexGridApproval.DataSource = MRPClass.MRPApproval_CAPEX(DOC_NUMBER, entitycode);
            CapexGridApproval.KeyFieldName = "PK";
            CapexGridApproval.DataBind();
        }

        protected void DMGridApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void OpexGridApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void ManPowerGridApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void CapexGridApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void OpexGridApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindOpex = false;
        }

        protected void OpexGridApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedQty"], "ApprovedQtyOpex") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedCost"], "ApprovedCostOpex") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedTotalCost"], "ApprovedTotalCostOpex") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.OpexTable() + " SET [ApprovedQty] = @QTY, [ApprovedCost] = @COST, [ApprovedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty_float);
            cmd.Parameters.AddWithValue("@COST", cost_float);
            cmd.Parameters.AddWithValue("@TOTAL", total_float);
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                MRPClass.UpdateLastModified(conn, docnumber);
                string remarks = MRPClass.opex_logs + "-" + MRPClass.edit_logs;
                MRPClass.AddLogsMOPList(conn, mrp_key, remarks);
            }

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindOpex = true;
            BindOpex(docnumber);
        }

        protected void ManPowerGridApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindManPower = false;
        }

        protected void ManPowerGridApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedQty"], "ApprovedQtyManPower") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedCost"], "ApprovedCostManPower") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedTotalCost"], "ApprovedTotalCostManPower") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.ManPowerTable() + " SET [ApprovedQty] = @QTY, [ApprovedCost] = @COST, [ApprovedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty_float);
            cmd.Parameters.AddWithValue("@COST", cost_float);
            cmd.Parameters.AddWithValue("@TOTAL", total_float);
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                MRPClass.UpdateLastModified(conn, docnumber);
                string remarks = MRPClass.manpower_logs + "-" + MRPClass.edit_logs;
                MRPClass.AddLogsMOPList(conn, mrp_key, remarks);
            }

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindManPower = true;
            BindManPower(docnumber);
        }

        protected void CapexGridApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindCapex = false;
        }



        protected void CapexGridApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedQty"], "ApprovedQtyCapex") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedCost"], "ApprovedCostCapex") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedTotalCost"], "ApprovedTotalCostCapex") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.CapexTable() + " SET [ApprovedQty] = @QTY, [ApprovedCost] = @COST, [ApprovedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty_float);
            cmd.Parameters.AddWithValue("@COST", cost_float);
            cmd.Parameters.AddWithValue("@TOTAL", total_float);
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                MRPClass.UpdateLastModified(conn, docnumber);
                string remarks = MRPClass.capex_logs + "-" + MRPClass.edit_logs;
                MRPClass.AddLogsMOPList(conn, mrp_key, remarks);
            }

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindCapex = true;
            BindCapex(docnumber);
        }

        protected void DMGridApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedQty"], "ApprovedQtyDM") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedCost"], "ApprovedCostDM") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["ApprovedTotalCost"], "ApprovedTotalCostDM") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update = "UPDATE " + MRPClass.DirectMatTable() + " SET [ApprovedQty] = @QTY, [ApprovedCost] = @COST, [ApprovedTotalCost] = @TOTAL WHERE [PK] = @PK";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@QTY", qty_float);
            cmd.Parameters.AddWithValue("@COST", cost_float);
            cmd.Parameters.AddWithValue("@TOTAL", total_float);
            cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                MRPClass.UpdateLastModified(conn, docnumber);
                string remarks = MRPClass.directmaterials_logs + "-" + MRPClass.edit_logs;
                MRPClass.AddLogsMOPList(conn, mrp_key, remarks);
            }

            conn.Close();

            e.Cancel = true;
            grid.CancelEdit();
            bindDM = true;
            BindDirectMaterials(docnumber);
        }

        protected void DMGridApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindDM = false;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {

        }

        protected void MRPList_Click(object sender, EventArgs e)
        {

        }

        protected void Preview_Click(object sender, EventArgs e)
        {

        }

        protected void DMGridApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }

        protected void OpexGridApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }

        protected void ManPowerGridApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }

        protected void CapexGridApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }
    }
}