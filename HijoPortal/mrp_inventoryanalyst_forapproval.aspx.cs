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
    public partial class mrp_inventoryanalyst_forapproval : System.Web.UI.Page
    {
        private static int mrp_key = 0, wrkflwln = 0, iStatusKey = 0;
        private static string docnumber = "", entitycode = "", buCode = "", firstname = "", lastname = "";
        private static bool bindDM = true, bindOpex = true, bindManPower = true, bindCapex = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                //Rsize
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                docnumber = Request.Params["DocNum"].ToString();
                wrkflwln = Convert.ToInt32(Request.Params["WrkFlwLn"].ToString());

                Load_MRP(docnumber);

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

        private void Load_MRP(string docnumber)
        {
            string query = "SELECT TOP (100) PERCENT  tbl_MRP_List.*, vw_AXEntityTable.NAME AS EntityCodeDesc, vw_AXOperatingUnitTable.NAME AS BUCodeDesc, tbl_MRP_Status.StatusName, tbl_Users.Lastname, tbl_Users.Firstname FROM   tbl_MRP_List INNER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK LEFT OUTER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK LEFT OUTER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID WHERE dbo.tbl_MRP_List.DocNumber = '" + docnumber + "' ORDER BY dbo.tbl_MRP_List.DocNumber DESC";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

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
                buCode = reader["BUCode"].ToString();
                Month.Text = MRPClass.Month_Name(Int32.Parse(reader["MRPMonth"].ToString()));
                Year.Text = reader["MRPYear"].ToString();
                Status.Text = reader["StatusName"].ToString();
                firstname = reader["Firstname"].ToString();
                lastname = reader["Lastname"].ToString();

            }
            reader.Close();
            conn.Close();

            Creator.Text = EncryptionClass.Decrypt(firstname) + " " + EncryptionClass.Decrypt(lastname);
            iStatusKey = MRPClass.MRP_Line_Status(mrp_key, wrkflwln);
        }

        private void BindDirectMaterials(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_Direct_Materials(DOC_NUMBER, entitycode);
            DMGridInventApproval.DataSource = dtRecord;
            DMGridInventApproval.KeyFieldName = "PK";
            DMGridInventApproval.DataBind();
        }

        private void BindOpex(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_OPEX(DOC_NUMBER, entitycode);
            OPGridInventApproval.DataSource = dtRecord;
            OPGridInventApproval.KeyFieldName = "PK";
            OPGridInventApproval.DataBind();
        }

        private void BindManPower(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_ManPower(DOC_NUMBER, entitycode);
            MANGridInventApproval.DataSource = dtRecord;
            MANGridInventApproval.KeyFieldName = "PK";
            MANGridInventApproval.DataBind();
        }


        private void BindCapex(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRPInvent_CAPEX(DOC_NUMBER, entitycode);
            CAGridInventApproval.DataSource = dtRecord;
            CAGridInventApproval.KeyFieldName = "PK";
            CAGridInventApproval.DataBind();
        }

        protected void DMGridInventApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindDM = false;
        }

        protected void DMGridInventApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "EdittedQty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "EdittedCost") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittiedTotalCost"], "EdittiedTotalCost") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            string update = "UPDATE " + MRPClass.DirectMatTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittiedTotalCost] = @TOTAL WHERE [PK] = @PK";
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

        protected void DMGridInventApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void DMGridInventApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }

        protected void OPGridInventApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindOpex = false;
        }

        protected void OPGridInventApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "EdittedQty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "EdittedCost") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedTotalCost"], "EdittedTotalCost") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            string update = "UPDATE " + MRPClass.OpexTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittedTotalCost] = @TOTAL WHERE [PK] = @PK";
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

        protected void OPGridInventApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void OPGridInventApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }

        protected void MANGridInventApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindManPower = false;
        }

        protected void MRPList_Click(object sender, EventArgs e)
        {

        }

        protected void MANGridInventApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "EdittedQty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "EdittedCost") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittiedTotalCost"], "EdittiedTotalCost") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            string update = "UPDATE " + MRPClass.ManPowerTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittiedTotalCost] = @TOTAL WHERE [PK] = @PK";
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

        protected void MANGridInventApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            Submit.Enabled = false;

            Load_MRP(docnumber);

            BindDirectMaterials(docnumber);
            BindOpex(docnumber); 
            BindManPower(docnumber); 
            BindCapex(docnumber); 

            MRPNotificationMessage.Text = MRPClass.successfully_submitted;
            MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
            MRPNotify.HeaderText = "Info";
            MRPNotify.ShowOnPageLoad = true;
        }

        protected void MANGridInventApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            Response.Redirect("mrp_preview.aspx?DocNum=" + docnumber.ToString());
        }

        protected void CAGridInventApproval_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindCapex = false;
        }

        protected void CAGridInventApproval_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            ASPxTextBox qty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedQty"], "EdittedQty") as ASPxTextBox;
            ASPxTextBox cost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittedCost"], "EdittedCost") as ASPxTextBox;
            ASPxTextBox total = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["EdittiedTotalCost"], "EdittiedTotalCost") as ASPxTextBox;

            string PK = e.Keys[0].ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            Double qty_float = Convert.ToDouble(qty.Value.ToString());
            Double cost_float = Convert.ToDouble(cost.Value.ToString());
            Double total_float = Convert.ToDouble(total.Value.ToString());

            string update = "UPDATE " + MRPClass.CapexTable() + " SET [EdittedQty] = @QTY, [EdittedCost] = @COST, [EdittiedTotalCost] = @TOTAL WHERE [PK] = @PK";
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

        protected void CAGridInventApproval_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
        }

        protected void CAGridInventApproval_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.VisibilityRevDesc(grid, entitycode);
        }
    }
}