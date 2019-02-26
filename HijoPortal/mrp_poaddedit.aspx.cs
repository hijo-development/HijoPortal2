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
    public partial class mrp_poaddedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                string query = "SELECT * FROM " + MRPClass.POTableName() + " WHERE [PK] = '" + Session["PO_PK"].ToString() + "'";
                SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    POnumber.Text = reader["PONumber"].ToString();
                    POdate.Text = Convert.ToDateTime(reader["DateCreated"]).ToString("MM/dd/yyyy");
                    ExpDelivery.Value = reader["ExpectedDate"].ToString();
                    Vendor.Value = reader["VendorCode"].ToString();
                    Currency.Value = reader["CurrencyCode"].ToString();
                    Site.Value = reader["InventSite"].ToString();
                    Terms.Value = reader["PaymentTerms"].ToString();
                    WareHouse.Value = reader["InventSiteWarehouse"].ToString();
                    Location.Value = reader["InventSiteWarehouseLocation"].ToString();
                }
                reader.Close();
                
                //BindGridViewDataComboBoxColumn();
                BindPOAddEdit(Session["MRP_Number"].ToString(), "ITEMGROUPID");

                string query_ponumber = "SELECT Count(*) FROM [hijo_portal].[dbo].[tbl_POCreation] where PONumber = '" + POnumber.Text + "' AND ExpectedDate IS NOT NULL";
                cmd = new SqlCommand(query_ponumber, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0) Create.Text = "Update";
                else Send.Enabled = false;

                conn.Close();

            }
            else
            {
                if (Session["DataSet"] != null)
                {
                    DataSet ds = (DataSet)Session["DataSet"];
                    DataTable dataTable = ds.Tables[0];
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["PK"] };
                    POAddEditGrid.DataSource = dataTable;
                    POAddEditGrid.KeyFieldName = "PK";
                    POAddEditGrid.DataBind();
                }
            }
        }

        private void BindPOAddEdit(string mrp_number, string type)
        {
            DataTable dtRecord = MRPClass.POAddEdit_Table(mrp_number, type);
            POAddEditGrid.DataSource = dtRecord;
            POAddEditGrid.KeyFieldName = "PK";
            POAddEditGrid.DataBind();

            //for row updating
            DataSet ds = new DataSet();
            ds.Tables.Add(dtRecord);
            Session["DataSet"] = ds;
        }

        protected void POAddEditGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            if (POAddEditGrid.IsEditing || POAddEditGrid.IsNewRowEditing)
            {
                POAddEditGrid.SettingsBehavior.AllowSort = false;
                POAddEditGrid.SettingsBehavior.AllowAutoFilter = false;
                POAddEditGrid.SettingsBehavior.AllowHeaderFilter = false;
            }
            else
            {
                POAddEditGrid.SettingsBehavior.AllowSort = true;
                POAddEditGrid.SettingsBehavior.AllowAutoFilter = true;
                POAddEditGrid.SettingsBehavior.AllowHeaderFilter = true;
            }
        }

        protected void selList_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //List<object> fieldValues = POAddEditGrid.GetSelectedFieldValues(new string[] { "MRPCategory", "Item", "Qty", "UOM", "Cost" });
            //if (fieldValues.Count > 0)
            //{
            //    ListBoxColumn l1 = new ListBoxColumn();
            //    l1.FieldName = "MRPCategory";
            //    selList.Columns.Add(l1);
            //    foreach (object item in fieldValues)
            //    {
            //        MRPClass.PrintString("truytruyt");
            //        selList.Items.Add("MRPCategory", item);
            //        //selList.Items.Add("trial callback");
            //        //cechkedPersons.Add(new Person { Name = Convert.ToString(item) });
            //    }
            //}
        }

        protected void POAddEditGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

            ASPxTextBox qty = POAddEditGrid.FindEditRowCellTemplateControl((GridViewDataColumn)POAddEditGrid.Columns["POQty"], "POQty") as ASPxTextBox;
            ASPxTextBox cost = POAddEditGrid.FindEditRowCellTemplateControl((GridViewDataColumn)POAddEditGrid.Columns["POCost"], "POCost") as ASPxTextBox;
            ASPxTextBox total = POAddEditGrid.FindEditRowCellTemplateControl((GridViewDataColumn)POAddEditGrid.Columns["POTotalCost"], "POTotalCost") as ASPxTextBox;
            //ASPxGridView grid = sender as ASPxGridView;
            //MRPClass.PrintString(e.NewValues["POQty"].ToString());
            //e.Cancel = true;
            //grid.CancelEdit();

            DataSet ds = (DataSet)Session["DataSet"];
            ASPxGridView gridView = (ASPxGridView)sender;
            DataTable dataTable = ds.Tables[0];
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["PK"] };
            DataRow row = dataTable.Rows.Find(e.Keys["PK"]);
            row["POQty"] = qty.Value.ToString();
            row["POCost"] = cost.Value.ToString();
            row["POTotalCost"] = total.Value.ToString();

        
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();

            while (enumerator.MoveNext())
            {
                MRPClass.PrintString(enumerator.Key.ToString());
                row[enumerator.Key.ToString()] = enumerator.Value.ToString();
            }
            gridView.CancelEdit();
            e.Cancel = true;


            POAddEditGrid.DataSource = dataTable;
            POAddEditGrid.KeyFieldName = "PK";
            POAddEditGrid.DataBind();

        }

        protected void ProCategory_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.ProCategoryTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "NAME";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            combo.Columns.Add(l_text);

            combo.ValueField = "NAME";
            combo.TextField = "DESCRIPTION";
            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        protected void POAddEditGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string s = ProCategory.Value.ToString();
            if (s == "ALL") s = "ITEMGROUPID";
            BindPOAddEdit(Session["MRP_Number"].ToString(), s);
            //selList.Items.Clear();
        }

        protected void Site_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            DataTable dtRecord = MRPClass.InventSiteTable();
            combo.DataSource = dtRecord;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "SITEID";
            l_value.Caption = "Site ID";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            combo.Columns.Add(l_text);

            combo.TextField = "NAME";
            combo.ValueField = "SITEID";
            combo.DataBind();
            combo.TextFormatString = "{0}";
        }

        protected void WarehouseCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            WareHouse.Value = "";
            DataTable dtRecord = MRPClass.InventSiteWarehouseTable(Site.Value.ToString());
            WareHouse.DataSource = dtRecord;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "warehouse";
            l_value.Caption = "Warehouse";
            WareHouse.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            l_text.Caption = "Name";
            WareHouse.Columns.Add(l_text);

            WareHouse.TextField = "NAME";
            WareHouse.ValueField = "warehouse";
            WareHouse.TextFormatString = "{0}";
            WareHouse.DataBind();
        }

        protected void LocationCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            Location.Value = "";
            if (WareHouse.Value == null)
                return;

            Location.DataSource = MRPClass.InventSiteLocationTable(WareHouse.Value.ToString());

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
        }

        protected void Vendor_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = MRPClass.VendTableTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "ACCOUNTNUM";
            combo.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "NAME";
            combo.Columns.Add(l_text);

            combo.ValueField = "ACCOUNTNUM";
            combo.TextField = "NAME";
            combo.DataBind();
            combo.TextFormatString = "{0}";
        }

        protected void CurrencyCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT VENDGROUP, PAYMTERMID, CURRENCY FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE[ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Currency.Value = reader["CURRENCY"].ToString();
            }
            reader.Close();
            conn.Close();

            Currency.DataSource = MRPClass.CurrencyTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "CURRENCYCODE";
            Currency.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "TXT";
            Currency.Columns.Add(l_text);

            Currency.ValueField = "CURRENCYCODE";
            Currency.TextField = "TXT";
            Currency.DataBind();
            Currency.TextFormatString = "{0}";
        }

        protected void TermsCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            string query = "SELECT PAYMTERMID FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

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

            Terms.DataSource = MRPClass.PaymTermTable(); ;

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "PAYMTERMID";
            Terms.Columns.Add(l_value);

            ListBoxColumn l_text = new ListBoxColumn();
            l_text.FieldName = "DESCRIPTION";
            Terms.Columns.Add(l_text);

            Terms.ValueField = "PAYMTERMID";
            Terms.TextField = "DESCRIPTION";
            Terms.DataBind();
            Terms.TextFormatString = "{0}";
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string update_po = "UPDATE [hijo_portal].[dbo].[tbl_POCreation] SET [ExpectedDate] = @expdate, [VendorCode] = @vendor, [PaymentTerms] = @terms, [CurrencyCode] = @currency, [InventSite] = @site, [InventSiteWarehouse] = @warehouse,[InventSiteWarehouseLocation] = @location WHERE [PONumber] = @ponumber";

            string terms = "";
            if (Terms.Value != null)
                terms = Terms.Value.ToString();

            SqlCommand cmd_po = new SqlCommand(update_po, conn);
            cmd_po.Parameters.AddWithValue("@expdate", ExpDelivery.Value.ToString());
            cmd_po.Parameters.AddWithValue("@vendor", Vendor.Value.ToString());
            cmd_po.Parameters.AddWithValue("@terms", terms);
            cmd_po.Parameters.AddWithValue("@currency", Currency.Value.ToString());
            cmd_po.Parameters.AddWithValue("@site", Site.Value.ToString());
            cmd_po.Parameters.AddWithValue("@warehouse", WareHouse.Value.ToString());
            cmd_po.Parameters.AddWithValue("@location", Location.Value.ToString());
            cmd_po.Parameters.AddWithValue("@ponumber", POnumber.Text);
            cmd_po.CommandType = CommandType.Text;
            int result = cmd_po.ExecuteNonQuery();
            if(result > 0)
            {
                Send.Enabled = true;
                Create.Text = "Update";
            }

            conn.Close();



        }

        protected void POAddEditGrid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            foreach (GridViewColumn column in POAddEditGrid.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if (e.NewValues[dataColumn.FieldName] == null)
                {
                    MRPClass.PrintString(dataColumn.FieldName);
                    if (dataColumn.FieldName == "TaxGroup" || dataColumn.FieldName == "TaxItemGroup")
                        e.Errors[dataColumn] = "Value cannot be null.";
                }
            }

            //Displays the error row if there is at least one error. 
            if (e.Errors.Count > 0) e.RowError = "Please, fill all fields.";
        }
        protected void ItemsRequestedByFilterCondition_1(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            comboBox.DataSource = MRPClass.TaxGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TaxGroup";
            comboBox.Columns.Add(l_value);

            comboBox.ValueField = "TaxGroup";
            comboBox.TextField = "TaxGroup";
            comboBox.DataBind();
        }
        protected void ItemsRequestedByFilterCondition_2(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            comboBox.DataSource = MRPClass.TaxItemGroupTable();

            ListBoxColumn l_value = new ListBoxColumn();
            l_value.FieldName = "TaxItemGroup";
            comboBox.Columns.Add(l_value);

            comboBox.ValueField = "TaxItemGroup";
            comboBox.TextField = "TaxItemGroup";
            comboBox.DataBind(); ;
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            List<object> selectedValues = POAddEditGrid.GetSelectedFieldValues(new string[] { "PK", "TableIdentifier", "MRPCategory", "Item", "Qty", "UOM", "Cost", "TotalCost", "POQty", "POCost", "POTotalCost", "TaxGroup", "TaxItemGroup" }) as List<object>;

            if (selectedValues.Count == 0)
            {
                ItemsEmpty.HeaderText = "Alert";
                ItemsEmptyLabel.Text = "No Selected Items";
                ItemsEmpty.ShowOnPageLoad = true;
            }

            foreach (object[] obj in selectedValues)
            {
                string pk = obj[0].ToString();
                string identifier = obj[1].ToString();
                string category = obj[2].ToString();
                string item = obj[3].ToString();
                int slashIndex = item.IndexOf("/");
                string item_code = item.Substring(0, slashIndex);
                string qty = obj[4].ToString();
                string uom = obj[5].ToString();
                string cost = obj[6].ToString();
                string total = obj[7].ToString();
                string po_qty = obj[8].ToString();
                string po_cost = obj[9].ToString();
                string po_total = obj[10].ToString();
                string taxgroup = obj[11].ToString();
                string taxitem = obj[12].ToString();

                if (!string.IsNullOrEmpty(pk) && !string.IsNullOrEmpty(identifier) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(item) && !string.IsNullOrEmpty(qty) && !string.IsNullOrEmpty(uom) && !string.IsNullOrEmpty(cost) && !string.IsNullOrEmpty(total) && !string.IsNullOrEmpty(po_qty) && !string.IsNullOrEmpty(po_cost) && !string.IsNullOrEmpty(po_total) && !string.IsNullOrEmpty(taxgroup) && !string.IsNullOrEmpty(taxitem))
                {
                    SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
                    conn.Open();

                    switch (identifier)
                    {
                        case "1"://Direct Materials
                            string update_materials = "UPDATE [hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials] SET [QtyPO] = '" + po_qty + "' WHERE [PK] = '" + pk + "'";
                            SqlCommand cmd_mat = new SqlCommand(update_materials, conn);
                            cmd_mat.ExecuteNonQuery();

                            string insert_mat_po = "INSERT [hijo_portal].[dbo].[tbl_POCreation_Details] ([PONumber],[ItemCode],[TaxGroup],[TaxItemGroup],[Qty],[Cost],[TotalCost]) VALUES (@ponumber, @code, @taxgroup, @taxitem, @poqty, @pocost, @pototal)";

                            SqlCommand cmd_mat_po = new SqlCommand(insert_mat_po, conn);
                            cmd_mat_po.Parameters.AddWithValue("@ponumber", POnumber.Text);
                            cmd_mat_po.Parameters.AddWithValue("@code", item_code);
                            cmd_mat_po.Parameters.AddWithValue("@taxgroup", taxgroup);
                            cmd_mat_po.Parameters.AddWithValue("@taxitem", taxitem);
                            cmd_mat_po.Parameters.AddWithValue("@poqty", Convert.ToDouble(po_qty));
                            cmd_mat_po.Parameters.AddWithValue("@pocost", Convert.ToDouble(po_cost));
                            cmd_mat_po.Parameters.AddWithValue("@pototal", Convert.ToDouble(po_total));
                            cmd_mat_po.CommandType = CommandType.Text;
                            cmd_mat_po.ExecuteNonQuery();
                            break;

                        case "2"://Opex
                            string update_opex = "UPDATE [hijo_portal].[dbo].[tbl_MRP_List_OPEX] SET [QtyPO] = '" + po_qty + "' WHERE [PK] = '" + pk + "'";
                            SqlCommand cmd_opex = new SqlCommand(update_opex, conn);
                            cmd_opex.ExecuteNonQuery();

                            string insert_opex_po = "INSERT [hijo_portal].[dbo].[tbl_POCreation_Details] ([PONumber],[ItemCode],[TaxGroup],[TaxItemGroup],[Qty],[Cost],[TotalCost]) VALUES (@ponumber, @code, @taxgroup, @taxitem, @poqty, @pocost, @pototal)";

                            SqlCommand cmd_opex_po = new SqlCommand(insert_opex_po, conn);
                            cmd_opex_po.Parameters.AddWithValue("@ponumber", POnumber.Text);
                            cmd_opex_po.Parameters.AddWithValue("@code", item_code);
                            cmd_opex_po.Parameters.AddWithValue("@taxgroup", taxgroup);
                            cmd_opex_po.Parameters.AddWithValue("@taxitem", taxitem);
                            cmd_opex_po.Parameters.AddWithValue("@poqty", Convert.ToDouble(po_qty));
                            cmd_opex_po.Parameters.AddWithValue("@pocost", Convert.ToDouble(po_cost));
                            cmd_opex_po.Parameters.AddWithValue("@pototal", Convert.ToDouble(po_total));
                            cmd_opex_po.CommandType = CommandType.Text;
                            cmd_opex_po.ExecuteNonQuery();
                            break;
                    }

                    conn.Close();
                    Server.Transfer("mrp_pocreation.aspx");
                }
                else
                {
                    ItemsEmpty.HeaderText = "Alert";
                    ItemsEmptyLabel.Text = "Some selected items are empty";
                    ItemsEmpty.ShowOnPageLoad = true;
                }
            }
        }

        
    }
}