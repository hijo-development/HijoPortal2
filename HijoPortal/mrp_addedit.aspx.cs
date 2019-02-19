using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HijoPortal.classes;
using System.Web.UI.HtmlControls;

namespace HijoPortal
{
    public partial class mrp_addedit : System.Web.UI.Page
    {
        ArrayList ArrDirectMat = new ArrayList();
        ArrayList ArrOpex = new ArrayList();
        ArrayList ArrManPower = new ArrayList();
        ArrayList ArrCapex = new ArrayList();
        ArrayList ArrRevenue = new ArrayList();
        private static string docnumber = "";
        private static bool bindDM = true, bindOpex = true, bindManPower = true, bindCapex = true, bindRevenue = true;
        private const string materialsIdentifier = "Materials", opexIdentifier = "OPEX", manpowerIdentifier = "Manpower", capexIdentifier = "CAPEX", revenueIdentifier = "Revenue";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CreatorKey"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            
            if (!Page.IsPostBack)
            {

                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                ArrDirectMat.Clear();
                ArrOpex.Clear();
                ArrManPower.Clear();
                ArrCapex.Clear();
                ArrRevenue.Clear();

                docnumber = Request.Params["DocNum"].ToString();  //Session["DocNumber"].ToString();
                string query = "SELECT TOP (100) PERCENT  tbl_MRP_List.*, vw_AXEntityTable.NAME AS EntityCodeDesc, vw_AXOperatingUnitTable.NAME AS BUCodeDesc, tbl_MRP_Status.StatusName, tbl_Users.Lastname, tbl_Users.Firstname FROM   tbl_MRP_List INNER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK LEFT OUTER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK LEFT OUTER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID WHERE dbo.tbl_MRP_List.DocNumber = '" + docnumber + "' ORDER BY dbo.tbl_MRP_List.DocNumber DESC";
                //string query = "SELECT TOP (100) PERCENT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, " +
                //                          " dbo.tbl_MRP_List.DateCreated, dbo.tbl_MRP_List.EntityCode, dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                //                          " dbo.tbl_MRP_List.BUCode, dbo.vw_AXOperatingUnitTable.NAME AS BUCodeDesc, dbo.tbl_MRP_List.MRPMonth, " +
                //                          " dbo.tbl_MRP_List.MRPYear, dbo.tbl_MRP_List.StatusKey, dbo.tbl_MRP_Status.StatusName, " +
                //                          " dbo.tbl_MRP_List.CreatorKey, dbo.tbl_MRP_List.LastModified " +
                //                          " FROM  dbo.tbl_MRP_List LEFT OUTER JOIN " +
                //                          " dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                //                          " dbo.tbl_MRP_Status ON dbo.tbl_MRP_List.StatusKey = dbo.tbl_MRP_Status.PK LEFT OUTER JOIN " +
                //                          " dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID " +
                //                          " WHERE(dbo.tbl_MRP_List.DocNumber = '" + docnumber + "') " +
                //                          " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";
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
                RevenueRoundPanel.HeaderText = "[" + DocNum.Text.ToString().Trim() + "] Revenue & Assumptions";

                DirectMaterialsRoundPanel.Font.Bold = true;
                OpexRoundPanel.Font.Bold = true;
                ManpowerRoundPanel.Font.Bold = true;
                CapexRoundPanel.Font.Bold = true;
                RevenueRoundPanel.Font.Bold = true;

                DirectMaterialsRoundPanel.Collapsed = true;
                OpexRoundPanel.Collapsed = true;
                ManpowerRoundPanel.Collapsed = true;
                CapexRoundPanel.Collapsed = true;
                RevenueRoundPanel.Collapsed = true;

                ASPxPageControl1.Font.Bold = true;
                ASPxPageControl1.Font.Size = 12;
            }

            if (bindDM)
                BindDirectMaterials(docnumber);
            else
                bindDM = true;

            if (bindOpex)
                BindOPEX(docnumber);
            else
                bindOpex = true;

            if (bindManPower)
                BindManPower(docnumber);
            else
                bindManPower = true;

            if (bindCapex)
                BindCAPEX(docnumber);
            else
                bindCapex = true;

            if (bindRevenue)
                BindRevenue(docnumber);
            else
                bindRevenue = true;
        }

        private void BindDirectMaterials(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRP_Direct_Materials(DOC_NUMBER);
            DirectMaterialsGrid.DataSource = dtRecord;
            DirectMaterialsGrid.KeyFieldName = "PK";
            DirectMaterialsGrid.DataBind();
        }

        private void BindOPEX(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRP_OPEX(DOC_NUMBER);
            OPEXGrid.DataSource = dtRecord;
            OPEXGrid.KeyFieldName = "PK";
            OPEXGrid.DataBind();
        }
        private void BindManPower(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRP_ManPower(DOC_NUMBER);
            ManPowerGrid.DataSource = dtRecord;
            ManPowerGrid.KeyFieldName = "PK";
            ManPowerGrid.DataBind();
        }

        private void BindCAPEX(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRP_CAPEX(DOC_NUMBER);
            CAPEXGrid.DataSource = dtRecord;
            CAPEXGrid.KeyFieldName = "PK";
            CAPEXGrid.DataBind();
        }

        private void BindRevenue(string DOC_NUMBER)
        {
            DataTable dtRecord = MRPClass.MRP_Revenue(DOC_NUMBER);
            RevenueGrid.DataSource = dtRecord;
            RevenueGrid.KeyFieldName = "PK";
            RevenueGrid.DataBind();
        }


        protected void FocusThisRowGrid(ASPxGridView grid, int keyVal)
        {
            grid.FocusedRowIndex = grid.FindVisibleIndexByKeyValue(keyVal);
        }


        protected void ActivityCode_Init(object sender, EventArgs e)
        {
            //SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            //conn.Open();

            //string query = "SELECT * FROM [hijo_portal].[dbo].[vw_AXFindimActivity]";

            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //ASPxComboBox combo = sender as ASPxComboBox;
            //while (reader.Read())
            //{
            //    combo.Items.Add(reader[1].ToString() + "-" + reader[0].ToString());
            //}

            DataTable dtRecord = MRPClass.ActivityCodeTable();
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = dtRecord;
            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "VALUE";
            l_ValueField.Caption = "ID";
            l_ValueField.Width = 50;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "DESCRIPTION";
            combo.Columns.Add(l_TextField);

            combo.ValueField = "VALUE";
            combo.TextField = "DESCRIPTION";
            combo.DataBind();

            GridViewEditFormTemplateContainer container = combo.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            MRPClass.PrintString("exp:" + !container.Grid.IsNewRowEditing);
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "ActivityCode").ToString();
            }
        }



        protected void UOM_Init(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            string query = "SELECT [SYMBOL] FROM[hijo_portal].[dbo].[vw_AXUnitOfMeasure]";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ASPxComboBox combo = sender as ASPxComboBox;
            while (reader.Read())
            {
                combo.Items.Add(reader[0].ToString());
            }
            reader.Close();
            conn.Close();
        }

        protected void ExpenseCode_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string query = "SELECT [NAME],[DESCRIPTION] FROM [hijo_portal].[dbo].[vw_AXProdCategory]";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                combo.Items.Add(reader[0].ToString() + "-" + reader[1].ToString());
            }
            reader.Close();
            conn.Close();
        }

        protected void ManPowerTypeKey_Init(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            string query = "SELECT [ManPowerTypeDesc] FROM [hijo_portal].[dbo].[tbl_System_ManPowerType]";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ASPxComboBox combo = sender as ASPxComboBox;
            while (reader.Read())
            {
                combo.Items.Add(reader[0].ToString());
            }
            reader.Close();
            conn.Close();
        }


        protected void DirectMaterialsGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindDM = false;
        }



        protected void DirectMaterialsGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("DirectPageControl") as ASPxPageControl;

            ASPxComboBox actCode = pageControl.FindControl("ActivityCode") as ASPxComboBox;
            ASPxTextBox itemCode = pageControl.FindControl("ItemCode") as ASPxTextBox;
            ASPxTextBox itemDesc = pageControl.FindControl("ItemDescription") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO " + MRPClass.DirectMatTable() + " ([HeaderDocNum], [ActivityCode], [ItemCode], [ItemDescription], [UOM], [Cost], [Qty], [TotalCost]) VALUES (@HeaderDocNum, @ActivityCode, @ItemCode, @ItemDesc, @UOM, @Cost, @Qty, @TotalCost)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@HeaderDocNum", docnumber);
            cmd.Parameters.AddWithValue("@ActivityCode", actCode.Value.ToString());
            cmd.Parameters.AddWithValue("@ItemCode", itemCode.Value.ToString());
            cmd.Parameters.AddWithValue("@ItemDesc", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindDirectMaterials(docnumber);

            int pk_latest = 0;
            string query_pk = "SELECT TOP 1 [PK] FROM " + MRPClass.DirectMatTable() + " ORDER BY [PK] DESC";
            SqlCommand comm = new SqlCommand(query_pk, conn);
            SqlDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                pk_latest = Convert.ToInt32(r[0].ToString());
            }
            if (pk_latest > 0)
                FocusThisRowGrid(grid, pk_latest);

        }

        protected void DirectMaterialsGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindDM = false;
        }

        protected void DirectMaterialsGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("DirectPageControl") as ASPxPageControl;

            ASPxComboBox actCode = pageControl.FindControl("ActivityCode") as ASPxComboBox;
            ASPxTextBox itemCode = pageControl.FindControl("ItemCode") as ASPxTextBox;
            ASPxTextBox itemDesc = pageControl.FindControl("ItemDescription") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string actcodeVal = MRPClass.ActivityCodeDESCRIPTION(actCode.Value.ToString());

            string PK = e.Keys[0].ToString();

            string update_MRP = "UPDATE " + MRPClass.DirectMatTable() + " SET [ActivityCode] = @ActivityCode, [ItemCode] = @ItemCode ,[ItemDescription] = @ItemDescription, [UOM]= @UOM, [Cost] = @Cost, [Qty] = @Qty, [TotalCost] = @TotalCost WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@ActivityCode", actcodeVal);
            cmd.Parameters.AddWithValue("@ItemCode", itemCode.Value.ToString());
            cmd.Parameters.AddWithValue("@ItemDescription", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindDirectMaterials(docnumber);
            e.Cancel = true;
            grid.CancelEdit();
        }



        protected void DirectMaterialsGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            bool Exist = CheckLogsExist(MRPClass.MaterialsTableLogs(), PK);
            if (Exist)//if the material has logs
            {
                conn.Close();
                e.Cancel = true;
            }
            else
            {
                string delete = "DELETE FROM " + MRPClass.DirectMatTable() + " WHERE [PK] ='" + PK + "'";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindDirectMaterials(docnumber);
                e.Cancel = true;
            }
        }

        protected void OPEXGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindCapex = false;
        }



        protected void OPEXGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("OPEXPageControl") as ASPxPageControl;

            ASPxComboBox experseCode = pageControl.FindControl("ExpenseCode") as ASPxComboBox;
            ASPxTextBox itemCode = pageControl.FindControl("ItemCode") as ASPxTextBox;
            ASPxTextBox itemDesc = pageControl.FindControl("Description") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO " + MRPClass.OpexTable() + " ([HeaderDocNum], [ExpenseCode], [ItemCode], [Description], [UOM], [Cost], [Qty], [TotalCost]) VALUES (@HeaderDocNum, @ExpenseCode, @ItemCode, @Description, @UOM, @Cost, @Qty, @TotalCost)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@HeaderDocNum", docnumber);
            cmd.Parameters.AddWithValue("@ExpenseCode", experseCode.Text);
            cmd.Parameters.AddWithValue("@ItemCode", itemCode.Value.ToString());
            cmd.Parameters.AddWithValue("@Description", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindOPEX(docnumber);
        }

        protected void OPEXGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            bool Exist = CheckLogsExist(MRPClass.OpexTableLogs(), PK);
            if (Exist)//if the material has logs
            {
                conn.Close();
                e.Cancel = true;
            }
            else
            {
                string delete = "DELETE FROM " + MRPClass.OpexTable() + " WHERE [PK] ='" + PK + "'";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindDirectMaterials(docnumber);
                e.Cancel = true;
            }
        }

        protected void OPEXGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindCapex = false;
        }

        protected void OPEXGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("OPEXPageControl") as ASPxPageControl;

            ASPxComboBox experseCode = pageControl.FindControl("ExpenseCode") as ASPxComboBox;
            ASPxTextBox itemCode = pageControl.FindControl("ItemCode") as ASPxTextBox;
            ASPxTextBox itemDesc = pageControl.FindControl("Description") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;


            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string update_MRP = "UPDATE " + MRPClass.OpexTable() + " SET [ExpenseCode] = @ExpenseCode, [ItemCode] = @ItemCode ,[Description] = @Description, [UOM]= @UOM, [Cost] = @Cost, [Qty] = @Qty, [TotalCost] = @TotalCost WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@ExpenseCode", experseCode.Text);
            cmd.Parameters.AddWithValue("@ItemCode", itemCode.Value.ToString());
            cmd.Parameters.AddWithValue("@Description", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindOPEX(docnumber);
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void ManPowerGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindManPower = false;
        }

        protected void ManPowerGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("ManPowerPageControl") as ASPxPageControl;

            ASPxComboBox actCode = pageControl.FindControl("ActivityCode") as ASPxComboBox;
            ASPxComboBox type = pageControl.FindControl("ManPowerTypeKeyName") as ASPxComboBox;
            ASPxTextBox itemDesc = pageControl.FindControl("Description") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            int manpower_type_pk = 0;
            string query = "SELECT [PK] FROM [hijo_portal].[dbo].[tbl_System_ManPowerType] WHERE [ManPowerTypeDesc] = '" + type.Value.ToString() + "'";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                manpower_type_pk = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();

            string insert = "INSERT INTO " + MRPClass.ManPowerTable() + " ([HeaderDocNum], [ActivityCode], [ManPowerTypeKey], [Description], [UOM], [Cost], [Qty], [TotalCost]) VALUES (@HeaderDocNum, @ActivityCode, @ManPowerTypeKey, @Description, @UOM, @Cost, @Qty, @TotalCost)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@HeaderDocNum", docnumber);
            cmd.Parameters.AddWithValue("@ActivityCode", actCode.Value.ToString());
            cmd.Parameters.AddWithValue("@ManPowerTypeKey", manpower_type_pk);
            cmd.Parameters.AddWithValue("@Description", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindManPower(docnumber);
        }

        protected void ManPowerGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            bool Exist = CheckLogsExist(MRPClass.ManpowerTableLogs(), PK);
            if (Exist)//if the material has logs
            {
                conn.Close();
                e.Cancel = true;
            }
            else
            {
                string delete = "DELETE FROM " + MRPClass.ManPowerTable() + " WHERE [PK] ='" + PK + "'";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindDirectMaterials(docnumber);
                e.Cancel = true;
            }
        }

        protected void ManPowerGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindManPower = false;
        }

        protected void ManPowerGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("ManPowerPageControl") as ASPxPageControl;

            ASPxComboBox actCode = pageControl.FindControl("ActivityCode") as ASPxComboBox;
            ASPxComboBox type = pageControl.FindControl("ManPowerTypeKeyName") as ASPxComboBox;
            ASPxTextBox itemDesc = pageControl.FindControl("Description") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string actcodeVal = MRPClass.ActivityCodeDESCRIPTION(actCode.Value.ToString());
            string PK = e.Keys[0].ToString();

            int manpower_type_pk = 0;
            string query = "SELECT [PK] FROM [hijo_portal].[dbo].[tbl_System_ManPowerType] WHERE [ManPowerTypeDesc] = '" + type.Value.ToString() + "'";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                manpower_type_pk = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();

            string update_MRP = "UPDATE " + MRPClass.ManPowerTable() + " SET [ActivityCode] = @ActivityCode, [ManPowerTypeKey] = @ManPowerTypeKey ,[Description] = @Description, [UOM]= @UOM, [Cost] = @Cost, [Qty] = @Qty, [TotalCost] = @TotalCost WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@ActivityCode", actcodeVal);
            cmd.Parameters.AddWithValue("@ManPowerTypeKey", manpower_type_pk);
            cmd.Parameters.AddWithValue("@Description", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindManPower(docnumber);
            e.Cancel = true;
            grid.CancelEdit();
        }
        protected void CAPEXGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindCapex = false;
        }

        protected void CAPEXGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("CAPEXPageControl") as ASPxPageControl;

            ASPxTextBox itemDesc = pageControl.FindControl("Description") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO " + MRPClass.CapexTable() + " ([HeaderDocNum], [Description], [UOM], [Cost], [Qty], [TotalCost]) VALUES (@HeaderDocNum, @Description, @UOM, @Cost, @Qty, @TotalCost)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@HeaderDocNum", docnumber);
            cmd.Parameters.AddWithValue("@Description", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindCAPEX(docnumber);
        }

        protected void CAPEXGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            bool Exist = CheckLogsExist(MRPClass.CapexTableLogs(), PK);
            if (Exist)//if the material has logs
            {
                conn.Close();
                e.Cancel = true;
            }
            else
            {
                string delete = "DELETE FROM " + MRPClass.CapexTable() + " WHERE [PK] ='" + PK + "'";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindDirectMaterials(docnumber);
                e.Cancel = true;
            }
        }

        protected void CAPEXGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindCapex = false;
        }

        protected void CAPEXGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("CAPEXPageControl") as ASPxPageControl;

            ASPxTextBox itemDesc = pageControl.FindControl("Description") as ASPxTextBox;
            ASPxComboBox uom = pageControl.FindControl("UOM") as ASPxComboBox;
            ASPxTextBox cost = pageControl.FindControl("Cost") as ASPxTextBox;
            ASPxTextBox qty = pageControl.FindControl("Qty") as ASPxTextBox;
            ASPxTextBox totalcost = pageControl.FindControl("TotalCost") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string update_MRP = "UPDATE " + MRPClass.CapexTable() + " SET [Description] = @Description, [UOM]= @UOM, [Cost] = @Cost, [Qty] = @Qty, [TotalCost] = @TotalCost WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@Description", itemDesc.Value.ToString());
            cmd.Parameters.AddWithValue("@UOM", uom.Value.ToString());
            cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(cost.Value.ToString()));
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(qty.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(totalcost.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindCAPEX(docnumber);
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void listbox_Callback(object sender, CallbackEventArgsBase e)
        {
            DataTable dtRecord = MRPClass.AXInventTable(e.Parameter);
            ASPxPageControl pageControl = DirectMaterialsGrid.FindEditFormTemplateControl("DirectPageControl") as ASPxPageControl;
            ASPxListBox listbox = pageControl.FindControl("listbox") as ASPxListBox;
            listbox.Visible = true;
            listbox.DataSource = dtRecord;
            listbox.ValueField = "ITEMID";
            listbox.TextField = "NAMEALIAS";
            listbox.DataBind();
        }

        protected void RevenueGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindRevenue = false;
        }

        protected void MRPList_Click(object sender, EventArgs e)
        {
            Response.Redirect("mrp_list.aspx");
        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            Response.Redirect("mrp_preview.aspx?DocNum=" + docnumber.ToString());

            //Response.RedirectLocation = "mrp_preview.aspx?DocNum=" + docnumber.ToString();

        }

        

        protected void RevenueGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("RevenuePageControl") as ASPxPageControl;

            ASPxTextBox product = pageControl.FindControl("ProductName") as ASPxTextBox;
            ASPxTextBox farm = pageControl.FindControl("FarmName") as ASPxTextBox;
            ASPxTextBox prize = pageControl.FindControl("Prize") as ASPxTextBox;
            ASPxTextBox volume = pageControl.FindControl("Volume") as ASPxTextBox;
            ASPxTextBox totalprize = pageControl.FindControl("TotalPrize") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO " + MRPClass.RevenueTable() + " ([HeaderDocNum], [ProductName], [FarmName], [Prize], [Volume], [TotalPrize]) VALUES (@HeaderDocNum, @ProductName, @FarmName, @Prize, @Volume, @TotalPrize)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@HeaderDocNum", docnumber);
            cmd.Parameters.AddWithValue("@ProductName", product.Value.ToString());
            cmd.Parameters.AddWithValue("@FarmName", farm.Value.ToString());
            cmd.Parameters.AddWithValue("@Prize", Convert.ToDouble(prize.Value.ToString()));
            cmd.Parameters.AddWithValue("@Volume", Convert.ToDouble(volume.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalPrize", Convert.ToDouble(totalprize.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            e.Cancel = true;
            grid.CancelEdit();
            BindRevenue(docnumber);
        }

        protected void RevenueGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            bool Exist = CheckLogsExist(MRPClass.RevenueTableLogs(), PK);
            if (Exist)//if the material has logs
            {
                conn.Close();
                e.Cancel = true;
            }
            else
            {
                string delete = "DELETE FROM " + MRPClass.RevenueTable() + " WHERE [PK] ='" + PK + "'";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindDirectMaterials(docnumber);
                e.Cancel = true;
            }
        }

        protected void RevenueGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindRevenue = false;
        }

        protected void RevenueGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("RevenuePageControl") as ASPxPageControl;

            ASPxTextBox product = pageControl.FindControl("ProductName") as ASPxTextBox;
            ASPxTextBox farm = pageControl.FindControl("FarmName") as ASPxTextBox;
            ASPxTextBox prize = pageControl.FindControl("Prize") as ASPxTextBox;
            ASPxTextBox volume = pageControl.FindControl("Volume") as ASPxTextBox;
            ASPxTextBox totalprize = pageControl.FindControl("TotalPrize") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string update_MRP = "UPDATE " + MRPClass.RevenueTable() + " SET [ProductName] = @ProductName, [FarmName]= @FarmName, [Prize] = @Prize, [Volume] = @Volume, [TotalPrize] = @TotalPrize WHERE [PK] = @PK";

            SqlCommand cmd = new SqlCommand(update_MRP, conn);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@ProductName", product.Value.ToString());
            cmd.Parameters.AddWithValue("@FarmName", farm.Value.ToString());
            cmd.Parameters.AddWithValue("@Prize", Convert.ToDouble(prize.Value.ToString()));
            cmd.Parameters.AddWithValue("@Volume", Convert.ToDouble(volume.Value.ToString()));
            cmd.Parameters.AddWithValue("@TotalPrize", Convert.ToDouble(totalprize.Value.ToString()));
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();

            BindRevenue(docnumber);
            e.Cancel = true;
            grid.CancelEdit();
        }

        private bool CheckLogsExist(string table, string PK)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string query = "SELECT COUNT(*) FROM " + table + " where MasterKey = '" + PK + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            if (count > 0)//if the material has logs
                return true;
            else
                return false;
        }
    }
}