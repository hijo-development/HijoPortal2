using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_po_create : System.Web.UI.Page
    {
        private static string entitycode = "";
        private static bool bind = true;
        private static ArrayList mop_ref_arr = new ArrayList();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            }

            ListofRef();

            if (Session["CreatorKey"] != null)
            {
                if (bind)
                    BindGrid(Session["CreatorKey"].ToString());
                else
                    bind = true;
            }
            else CheckCreatorKey();

        }

        private void ListofRef()
        {
            string query = "SELECT DISTINCT MOPNumber FROM dbo.tbl_POCreation_Tmp WHERE UserKey = '" + Session["CreatorKey"].ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            //mop_ref_arr = new ArrayList();
            while (reader.Read())
            {
                MOPReference.Text = reader[0].ToString();
            }
        }

        private void BindGrid(string creatorkey)
        {
            DataTable dtRecord = POClass.POCreate_TmpTable(creatorkey);
            POCreateGrid.DataSource = dtRecord;
            POCreateGrid.KeyFieldName = "PK";
            POCreateGrid.DataBind();

            //for row updating
            DataSet ds = new DataSet();
            ds.Tables.Add(dtRecord);
            Session["DataSet"] = ds;
        }

        protected void Vendor_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = POClass.VendTableTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "ACCOUNTNUM";
            l_value.Caption = "Vendor Code";
            l_value.Width = 100;
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Vendor Name";
            l_text.Width = 350;
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.ValueField = "ACCOUNTNUM";
            combo.TextField = "NAME";
            combo.DataBind();
            //combo.TextFormatString = "{0}{1}";
        }

        protected void TermsCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT PAYMTERMID FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + Vendor.Text.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Terms.Value = reader["PAYMTERMID"].ToString();
            }
            reader.Close();
            conn.Close();

            Terms.DataSource = POClass.PaymTermTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PAYMTERMID";
            l_value.Width = 100;
            Terms.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            l_text.Width = 250;
            Terms.Columns.Add(l_text);

            Terms.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Terms.ValueField = "PAYMTERMID";
            Terms.TextField = "DESCRIPTION";
            Terms.DataBind();
            //Terms.TextFormatString = "{0}";
            Terms.ClientEnabled = true;
        }

        protected void CurrencyCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT dbo.vw_AXVendTable.VENDGROUP, dbo.vw_AXVendTable.PAYMTERMID, dbo.vw_AXVendTable.CURRENCY, dbo.vw_AXCurrency.TXT FROM dbo.vw_AXVendTable INNER JOIN dbo.vw_AXCurrency ON dbo.vw_AXVendTable.CURRENCY = dbo.vw_AXCurrency.CURRENCYCODE WHERE[ACCOUNTNUM] = '" + Vendor.Text.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            string value = "", text = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                value = reader["CURRENCY"].ToString();
                text = reader["TXT"].ToString();
            }
            reader.Close();
            conn.Close();

            Currency.DataSource = POClass.CurrencyTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "CURRENCYCODE";
            l_value.Caption = "Currency Code";
            l_value.Width = 100;
            Currency.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "TXT";
            l_text.Width = 250;
            l_text.Caption = "Currency Name";
            Currency.Columns.Add(l_text);

            Currency.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Currency.ValueField = "CURRENCYCODE";
            Currency.TextField = "TXT";
            Currency.DataBind();
            Currency.ClientEnabled = true;

            Currency.Value = value;
            Currency.Text = value + ";" + text;
        }

        protected void Site_Init(object sender, EventArgs e)
        {

            string query = "SELECT DISTINCT dbo.tbl_MRP_List.EntityCode FROM dbo.tbl_POCreation_Tmp INNER JOIN dbo.tbl_MRP_List ON dbo.tbl_POCreation_Tmp.MOPNumber = dbo.tbl_MRP_List.DocNumber WHERE UserKey = '" + Session["CreatorKey"].ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                entitycode = reader[0].ToString();
            }

            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = POClass.InventSiteTable(entitycode);

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "SITEID";
            l_value.Caption = "Site ID";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Width = 200;
            l_text.Caption = "Name";
            combo.Columns.Add(l_text);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.TextField = "NAME";
            combo.ValueField = "SITEID";
            combo.DataBind();
        }

        protected void WarehouseCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            Warehouse.Value = "";
            Warehouse.DataSource = POClass.InventSiteWarehouseTable(Site.Text.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "warehouse";
            l_value.Caption = "Warehouse";
            Warehouse.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            Warehouse.Columns.Add(l_text);

            Warehouse.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Warehouse.TextField = "NAME";
            Warehouse.ValueField = "warehouse";
            Warehouse.DataBind();
            Warehouse.ClientEnabled = true;
        }

        protected void LocationCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            Location.Value = "";

            Location.DataSource = POClass.InventSiteLocationTable(Warehouse.Text.ToString());

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "LocationCode";
            Location.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //Location.Columns.Add(l_text);

            //Location.TextField = "NAME";
            Location.ValueField = "LocationCode";
            Location.DataBind();
            Location.TextFormatString = "{0}";
            Location.ClientEnabled = true;
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            CheckCreatorKey();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlDataReader reader = null;

            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            SqlDataAdapter adp;

            //Declare Variables
            string DocPref = "", strDocNum = "", PONumber = "", delete ="", update = "", query="", insert ="", insert_po_details = "";
            int POSeriesNum = 0;

            string mopnumber = MOPReference.Text;
            string entitycode = "", bucode = "";

            query = "SELECT [PK],[EntityCode],[BUCode] FROM[hijo_portal].[dbo].[tbl_MRP_List] WHERE DocNumber = '" + mopnumber + "'";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                entitycode = reader["EntityCode"].ToString();
                bucode = reader["BUCode"].ToString();
            }
            reader.Close();

            //query = "SELECT [DocumentPrefix],[DocumentNum] FROM " + MRPClass.DocNumberTableName() + " where DocumentPrefix = 'PO'";

            //cmd = new SqlCommand(query, conn);
            //reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    DocPref = reader[0].ToString();
            //    DocNum = Convert.ToInt32(reader[1].ToString());
            //}
            //reader.Close();

            //DocNum += 1;
            //strDocNum = DocNum.ToString("00000000#");
            //string PONumber = DocPref + "-" + Session["EntityCode"].ToString() + "-" + strDocNum;

            //string update = "UPDATE " + MRPClass.DocNumberTableName() + " SET [DocumentNum] = '" + DocNum + "' WHERE [DocumentPrefix] = 'PO'";
            //cmd = new SqlCommand(update, conn);
            //int result = cmd.ExecuteNonQuery();

            //if (result == 0)//Cant Make PO Number
            //    return;

            string vendor = Vendor.Text.ToString();
            string payment = Terms.Text.ToString();
            string currency = Currency.Text.ToString();
            string site = Site.Text.ToString();
            string warehouse = Warehouse.Text.ToString();
            string location = Location.Text.ToString();
            string remarks = Remarks.Text.ToString();

            query = "SELECT tbl_PONumber.* FROM tbl_PONumber WHERE (EntityCode = '" + entitycode + "')";
            cmd = new SqlCommand(query);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    POSeriesNum = Convert.ToInt32(row["LastNumber"]) + 1;
                    PONumber = row["Prefix"].ToString() + "-" + row["EntityCode"].ToString() + "-" + row["BeforeSeries"].ToString() + POSeriesNum.ToString("0000000#");

                    insert = "INSERT INTO [hijo_portal].[dbo].[tbl_POCreation] ([PONumber],[DateCreated],[CreatorKey],[MRPNumber], [ExpectedDate], [VendorCode], [PaymentTerms], [CurrencyCode], [InventSite], [InventSiteWarehouse], [InventSiteWarehouseLocation], [EntityCode], [BUSSUCode], [Remarks]) VALUES (@PONumber, @DateCreated, @CreatorKey, @MRPNumber, @ExpectedDate, @VendorCode, @PaymentTerms, @CurrencyCode, @InventSite, @InventSiteWarehouse, @InventSiteWarehouseLocation, @EntityCode, @BUSSUCode, @Remarks)";

                    cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("@PONumber", PONumber);
                    cmd.Parameters.AddWithValue("@CreatorKey", Session["CreatorKey"].ToString());
                    cmd.Parameters.AddWithValue("@MRPNumber", mopnumber);
                    cmd.Parameters.AddWithValue("@EntityCode", entitycode);
                    cmd.Parameters.AddWithValue("@BUSSUCode", bucode);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ExpectedDate", ExpDel.Value.ToString());
                    cmd.Parameters.AddWithValue("@VendorCode", vendor);
                    cmd.Parameters.AddWithValue("@PaymentTerms", payment);
                    cmd.Parameters.AddWithValue("@CurrencyCode", currency);
                    cmd.Parameters.AddWithValue("@InventSite", site);
                    cmd.Parameters.AddWithValue("@InventSiteWarehouse", warehouse);
                    cmd.Parameters.AddWithValue("@InventSiteWarehouseLocation", location);
                    cmd.Parameters.AddWithValue("@Remarks", remarks);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    ASPxGridView grid = POCreateGrid as ASPxGridView;
                    for (int i = 0; i < grid.VisibleRowCount; i++)
                    {
                        object PK = grid.GetRowValues(i, "PK");
                        object MOPNumber = grid.GetRowValues(i, "MOPNumber");
                        object ItemPK = grid.GetRowValues(i, "ItemPK");
                        object TableIdentifier = grid.GetRowValues(i, "TableIdentifier");
                        object ItemCode = grid.GetRowValues(i, "ItemCode");
                        object Description = grid.GetRowValues(i, "Description");
                        object RequestedQty = grid.GetRowValues(i, "RequestedQty");
                        object Cost = grid.GetRowValues(i, "Cost");
                        object TotalCost = grid.GetRowValues(i, "TotalCost");
                        object POUOM = grid.GetRowValues(i, "POUOM");
                        object POQty = grid.GetRowValues(i, "POQty");
                        object POCost = grid.GetRowValues(i, "POCost");
                        object TotalPOCost = grid.GetRowValues(i, "TotalPOCost");
                        object TaxGroup = grid.GetRowValues(i, "TaxGroup");
                        object TaxItemGroup = grid.GetRowValues(i, "TaxItemGroup");

                        insert_po_details = "INSERT INTO [hijo_portal].[dbo].[tbl_POCreation_Details] ([PONumber],[MOPNumber], [ItemPK], [Identifier], [ItemCode], [TaxGroup], [TaxItemGroup], [Qty], [Cost], [TotalCost], [POUOM]) VALUES (@PONumber,@MOPNumber, @ItemPK, @Identifier, @ItemCode, @TaxGroup, @TaxItemGroup, @Qty, @Cost, @TotalCost, @POUOM)";

                        cmd = new SqlCommand(insert_po_details, conn);
                        cmd.Parameters.AddWithValue("@PONumber", PONumber);
                        cmd.Parameters.AddWithValue("@MOPNumber", MOPNumber);
                        cmd.Parameters.AddWithValue("@ItemPK", ItemPK);
                        cmd.Parameters.AddWithValue("@Identifier", TableIdentifier);
                        cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                        cmd.Parameters.AddWithValue("@TaxGroup", TaxGroup);
                        cmd.Parameters.AddWithValue("@TaxItemGroup", TaxItemGroup);
                        cmd.Parameters.AddWithValue("@POUOM", POUOM);
                        cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble(POQty));
                        cmd.Parameters.AddWithValue("@Cost", Convert.ToDouble(POCost));
                        cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDouble(TotalPOCost));
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        switch (TableIdentifier.ToString())
                        {
                            case "1"://Direct Material
                                update = "UPDATE " + MRP_Constants.DirectMaterials_TableName() + " SET [QtyPO] = '" + Convert.ToDouble(POQty) + "' WHERE [PK] = '" + ItemPK + "'";
                                cmd = new SqlCommand(update, conn);
                                cmd.ExecuteNonQuery();
                                break;

                            case "2"://Opex
                                update = "UPDATE " + MRP_Constants.OperatingExpense_TableName() + " SET [QtyPO] = '" + Convert.ToDouble(POQty) + "' WHERE [PK] = '" + ItemPK + "'";
                                cmd = new SqlCommand(update, conn);
                                cmd.ExecuteNonQuery();
                                break;
                        }
                    }

                    delete = "DELETE FROM [dbo].[tbl_POCreation_Tmp] WHERE [UserKey] = '" + Session["CreatorKey"].ToString() + "'";
                    cmd = new SqlCommand(delete, conn);
                    cmd.ExecuteNonQuery();

                    update = "UPDATE [dbo].[tbl_PONumber] SET LastNumber = " + POSeriesNum + " WHERE ([EntityCode] = '" + entitycode + "')";
                    cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();

                    ModalPopupExtenderLoading.Hide();

                    Response.Redirect("mrp_po_addedit.aspx?PONum=" + PONumber);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                POCreateNotify.HeaderText = "Alert!";
                POCreateNotifyLbl.Text = "Please Call Administrator." + Environment.NewLine + Environment.NewLine + "No PO Number Setup.";
                POCreateNotifyLbl.ForeColor = System.Drawing.Color.Red;
                POCreateNotify.ShowOnPageLoad = true;
            }
            dt.Clear();

            conn.Close();
        }

        protected void TaxItemGroup_Init(object sender, EventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)sender;
            comboBox.DataSource = POClass.TaxItemGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TaxItemGroup";
            comboBox.Columns.Add(l_value);

            comboBox.ValueField = "TaxItemGroup";
            comboBox.TextField = "TaxItemGroup";
            comboBox.DataBind(); ;
        }

        protected void TaxGroup_Init(object sender, EventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)sender;
            comboBox.DataSource = POClass.TaxGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TaxGroup";
            comboBox.Columns.Add(l_value);

            comboBox.ValueField = "TaxGroup";
            comboBox.TextField = "TaxGroup";
            comboBox.DataBind();
        }

        protected void POCreateGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox POUOM = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["POUOM"], "POUOM") as ASPxComboBox;
            ASPxTextBox POQty = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["POQty"], "POQty") as ASPxTextBox;
            ASPxTextBox POCost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["POCost"], "POCost") as ASPxTextBox;
            ASPxTextBox TotalPOCost = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TotalPOCost"], "TotalPOCost") as ASPxTextBox;
            ASPxComboBox TaxGroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxGroup"], "TaxGroup") as ASPxComboBox;
            ASPxComboBox TaxItemGroup = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["TaxItemGroup"], "TaxItemGroup") as ASPxComboBox;

            string PK = e.Keys[0].ToString();

            string update = "UPDATE dbo.tbl_POCreation_Tmp SET [TaxGroup] = @TaxGroup, [TaxItemGroup] = @TaxItemGroup,[POUOM] = @POUOM, [POQty] = @POQty, [POCost] = @POCost WHERE [PK] = @PK";

            string pouom = POUOM.Value.ToString();
            string qty = POQty.Value.ToString();
            string cost = POCost.Value.ToString();
            //string total = TotalPOCost.Value.ToString();
            string tax_group = TaxGroup.Text.ToString();
            string tax_item_group = TaxItemGroup.Text.ToString();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@POQty", Convert.ToDouble(qty));
            cmd.Parameters.AddWithValue("@POUOM", pouom);
            cmd.Parameters.AddWithValue("@POCost", Convert.ToDouble(cost));
            //cmd.Parameters.AddWithValue("@POTotalCost", total);
            cmd.Parameters.AddWithValue("@TaxGroup", tax_group);
            cmd.Parameters.AddWithValue("@TaxItemGroup", tax_item_group);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            BindGrid(Session["CreatorKey"].ToString());

            grid.CancelEdit();
            e.Cancel = true;
        }

        protected void POCreateGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bind = false;
        }

        protected void POUOM_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.UOMTable();
            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "SYMBOL";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "description";
            combo.Columns.Add(l_text);

            combo.ValueField = "SYMBOL";
            combo.TextField = "description";
            combo.DataBind();
            combo.TextFormatString = "{0}";

            GridViewEditItemTemplateContainer container = ((ASPxComboBox)sender).NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "POUOM").ToString();
            }
        }

        protected void POCreateGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            DesignBehavior.SetBehaviorGrid(grid);
        }
    }
}