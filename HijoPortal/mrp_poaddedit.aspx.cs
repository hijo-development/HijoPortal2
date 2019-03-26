using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
            CheckCreatorKey();
            
            if (!Page.IsPostBack)
            {
                bool isAllowed = false;
                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPProcurementOfficer", DateTime.Now);
                if (isAllowed == false)
                {
                    Response.Redirect("home.aspx");
                } else
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                    ExpDelivery.ClientEnabled = false;
                    Vendor.ClientEnabled = false;
                    Currency.ClientEnabled = false;
                    Site.ClientEnabled = false;
                    Terms.ClientEnabled = false;
                    WareHouse.ClientEnabled = false;
                    Location.ClientEnabled = false;
                    ProCategory.ClientEnabled = false;
                }
                
            }
            
            if (DocNumber.Value != null)
            {
                string s = ProCategory.Value.ToString();
                string docnum = DocNumber.Value.ToString();

                if (s == "ALL") s = "ITEMGROUPID";

                BindPOAddEdit(docnum, s);
            }
            

            //if (!Page.IsPostBack)
            //{
            //    string query = "SELECT * FROM " + MRPClass.POTableName() + " WHERE [PK] = '" + Session["PO_PK"].ToString() + "'";
            //    SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        POnumber.Text = reader["PONumber"].ToString();
            //        POdate.Text = Convert.ToDateTime(reader["DateCreated"]).ToString("MM/dd/yyyy");
            //        ExpDelivery.Value = reader["ExpectedDate"].ToString();
            //        Vendor.Value = reader["VendorCode"].ToString();
            //        Currency.Value = reader["CurrencyCode"].ToString();
            //        Site.Value = reader["InventSite"].ToString();
            //        Terms.Value = reader["PaymentTerms"].ToString();
            //        WareHouse.Value = reader["InventSiteWarehouse"].ToString();
            //        Location.Value = reader["InventSiteWarehouseLocation"].ToString();
            //    }
            //    reader.Close();

            //    //BindGridViewDataComboBoxColumn();
            //    BindPOAddEdit(Session["MRP_Number"].ToString(), "ITEMGROUPID");

            //    string query_ponumber = "SELECT Count(*) FROM [hijo_portal].[dbo].[tbl_POCreation] where PONumber = '" + POnumber.Text + "' AND ExpectedDate IS NOT NULL";
            //    cmd = new SqlCommand(query_ponumber, conn);
            //    int count = Convert.ToInt32(cmd.ExecuteScalar());
            //    if (count > 0) Create.Text = "Update";
            //    else Send.Enabled = false;

            //    conn.Close();

            //}
            //else
            //{
            //    if (Session["DataSet"] != null)
            //    {
            //        DataSet ds = (DataSet)Session["DataSet"];
            //        DataTable dataTable = ds.Tables[0];
            //        dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["PK"] };
            //        POAddEditGrid.DataSource = dataTable;
            //        POAddEditGrid.KeyFieldName = "PK";
            //        POAddEditGrid.DataBind();
            //    }
            //}
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
            ASPxGridView grid = sender as ASPxGridView;
            MRPClass.SetBehaviorGrid(grid);
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
                //MRPClass.PrintString(enumerator.Key.ToString());
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
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            string s = ProCategory.Value.ToString();
            string docnum = DocNumber.Value.ToString();

            //MOPMonthYear.Text = "";
            //MOPEntity.Text = "";
            //MOPBUSSU.Text = "";

            MRPClass.PrintString(docnum);

            string qry = "SELECT dbo.vw_AXEntityTable.NAME AS Entity, " +
                         " dbo.vw_AXOperatingUnitTable.NAME AS BUSSU, " +
                         " dbo.tbl_MRP_List.MRPMonth, dbo.tbl_MRP_List.MRPYear, " +
                         " dbo.tbl_MRP_List.DocNumber " +
                         " FROM dbo.tbl_MRP_List LEFT OUTER JOIN " +
                         " dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN " +
                         " dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER " +
                         " WHERE(dbo.tbl_MRP_List.DocNumber = '"+ docnum + "')";
            cn.Open();
            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            MRPClass.PrintString(dt.Rows.Count.ToString());
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MRPClass.PrintString("pass foreach");
                    //MOPMonthYear.Text = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(row["MRPMonth"])) + "-" + row["MRPYear"].ToString(); ;
                    //MOPEntity.Text = row["Entity"].ToString();
                    //MOPBUSSU.Text = row["BUSSU"].ToString();
                }
            }
            dt.Clear();
            cn.Close();

            if (s == "ALL") s = "ITEMGROUPID";

            if (Session["MRP_Number"] == null)
                BindPOAddEdit(docnum, s);
            else
                BindPOAddEdit(Session["MRP_Number"].ToString(), s);

            //selList.Items.Clear();
        }

        protected void Site_Init(object sender, EventArgs e)
        {
            //ASPxComboBox combo = sender as ASPxComboBox;
            //combo.DataSource = MRPClass.InventSiteTable();

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "SITEID";
            //l_value.Caption = "Site ID";
            //combo.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //l_text.Caption = "Name";
            //combo.Columns.Add(l_text);

            //combo.TextField = "NAME";
            //combo.ValueField = "SITEID";
            //combo.DataBind();
            //combo.TextFormatString = "{0}";
        }

        protected void WarehouseCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            //WareHouse.Value = "";
            //DataTable dtRecord = MRPClass.InventSiteWarehouseTable(Site.Value.ToString());
            //WareHouse.DataSource = dtRecord;

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "warehouse";
            //l_value.Caption = "Warehouse";
            //WareHouse.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //l_text.Caption = "Name";
            //WareHouse.Columns.Add(l_text);

            //WareHouse.TextField = "NAME";
            //WareHouse.ValueField = "warehouse";
            //WareHouse.TextFormatString = "{0}";
            //WareHouse.DataBind();
            //WareHouse.ClientEnabled = true;
        }

        protected void LocationCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            //Location.Value = "";
            //if (WareHouse.Value == null)
            //    return;

            //Location.DataSource = MRPClass.InventSiteLocationTable(WareHouse.Value.ToString());

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "LocationCode";
            //Location.Columns.Add(l_value);

            ////ListBoxColumn l_text = new ListBoxColumn();
            ////l_text.FieldName = "NAME";
            ////Location.Columns.Add(l_text);

            ////Location.TextField = "NAME";
            //Location.ValueField = "LocationCode";
            //Location.DataBind();
            //Location.TextFormatString = "{0}";
            //Location.ClientEnabled = true;
        }

        protected void Vendor_Init(object sender, EventArgs e)
        {
            //ASPxComboBox combo = sender as ASPxComboBox;
            //combo.DataSource = MRPClass.VendTableTable(); ;

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "ACCOUNTNUM";
            //combo.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "NAME";
            //combo.Columns.Add(l_text);

            //combo.ValueField = "ACCOUNTNUM";
            //combo.TextField = "NAME";
            //combo.DataBind();
            //combo.TextFormatString = "{0}";
        }

        protected void CurrencyCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            //string query = "SELECT VENDGROUP, PAYMTERMID, CURRENCY FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE[ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

            //SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            //conn.Open();
            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Currency.Value = reader["CURRENCY"].ToString();
            //}
            //reader.Close();
            //conn.Close();

            //Currency.DataSource = MRPClass.CurrencyTable(); ;

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "CURRENCYCODE";
            //Currency.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "TXT";
            //Currency.Columns.Add(l_text);

            //Currency.ValueField = "CURRENCYCODE";
            //Currency.TextField = "TXT";
            //Currency.DataBind();
            //Currency.TextFormatString = "{0}";
            //Currency.ClientEnabled = true;
        }

        protected void TermsCallback_Callback(object sender, CallbackEventArgsBase e)
        {
            //string query = "SELECT PAYMTERMID FROM[hijo_portal].[dbo].[vw_AXVendTable] WHERE [ACCOUNTNUM] = '" + Vendor.Value.ToString() + "'";

            //SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            //conn.Open();
            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Terms.Value = reader["PAYMTERMID"].ToString();
            //}
            //reader.Close();
            //conn.Close();

            //Terms.DataSource = MRPClass.PaymTermTable(); ;

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "PAYMTERMID";
            //Terms.Columns.Add(l_value);

            //ListBoxColumn l_text = new ListBoxColumn();
            //l_text.FieldName = "DESCRIPTION";
            //Terms.Columns.Add(l_text);

            //Terms.ValueField = "PAYMTERMID";
            //Terms.TextField = "DESCRIPTION";
            //Terms.DataBind();
            //Terms.TextFormatString = "{0}";
            //Terms.ClientEnabled = true;
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            CheckCreatorKey();
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            if (DocNumber.Value != null)
            {
                ExpDelivery.ClientEnabled = true;
                Vendor.ClientEnabled = true;
                Currency.ClientEnabled = true;
                Site.ClientEnabled = true;
                Terms.ClientEnabled = true;
                WareHouse.ClientEnabled = true;
                Location.ClientEnabled = true;
                ProCategory.ClientEnabled = true;
            }

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
                    //CREATE PO NUMBER

                    //Declare Variables
                    string DocPref = "", strDocNum = "";
                    int DocNum = 0;

                    //QUERY PO NUMBER TO START TO...
                    string query = "SELECT [DocumentPrefix],[DocumentNum] FROM " + MRPClass.DocNumberTableName() + " where DocumentPrefix = 'PO'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //GET DATA...
                        DocPref = reader[0].ToString();
                        DocNum = Convert.ToInt32(reader[1].ToString());
                    }
                    reader.Close();

                    //INCREASE IN DOCNUMBER INTEGER (SEE SQL DATABASE FOR CLARIFICATION)
                    DocNum += 1;
                    strDocNum = DocNum.ToString("00000000#");
                    //CREATED PO NUMBER
                    string PONumber = DocPref + "-" + Session["EntityCode"].ToString() + "-" + strDocNum;
                    string MRPnumber = DocNumber.Value.ToString();

                    //UPDATE PO NUMBER IS INCREASE (SEE SQL DATABASE FOR CLARIFICATION)
                    string update = "UPDATE " + MRPClass.DocNumberTableName() + " SET [DocumentNum] = '" + DocNum + "' WHERE [DocumentPrefix] = 'PO'";
                    cmd = new SqlCommand(update, conn);
                    int result = cmd.ExecuteNonQuery();

                    if (result == 0)
                        return;

                    string terms = "";
                    if (Terms.Value != null)//IF TERMS COMBOBOX NULL/EMPTY VALUE
                        terms = Terms.Value.ToString();

                    //INSERT INFO IN PO CREATION TABLE
                    string insert = "INSERT INTO " + MRPClass.POTableName() + " ([MRPNumber],[PONumber],[CreatorKey], [ExpectedDate] ,[VendorCode], [PaymentTerms], [CurrencyCode], [InventSite], [InventSiteWarehouse], [InventSiteWarehouseLocation]) VALUES (@MRPNumber, @PONumber, @CreatorKey, @expdate, @vendor, @terms, @currency, @site, @warehouse, @location)";
                    cmd = new SqlCommand(insert, conn);
                    cmd.Parameters.AddWithValue("MRPNumber", MRPnumber);
                    cmd.Parameters.AddWithValue("PONumber", PONumber);
                    cmd.Parameters.AddWithValue("CreatorKey", Session["CreatorKey"].ToString());
                    cmd.Parameters.AddWithValue("@expdate", ExpDelivery.Value.ToString());
                    cmd.Parameters.AddWithValue("@vendor", Vendor.Value.ToString());
                    cmd.Parameters.AddWithValue("@terms", terms);
                    cmd.Parameters.AddWithValue("@currency", Currency.Value.ToString());
                    cmd.Parameters.AddWithValue("@site", Site.Value.ToString());
                    cmd.Parameters.AddWithValue("@warehouse", WareHouse.Value.ToString());
                    cmd.Parameters.AddWithValue("@location", Location.Value.ToString());
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    switch (identifier)
                    {
                        case "1"://Direct Materials
                            string update_materials = "UPDATE " + MRPClass.DirectMatTable() + " SET [QtyPO] = '" + po_qty + "' WHERE [PK] = '" + pk + "'";
                            SqlCommand cmd_mat = new SqlCommand(update_materials, conn);
                            cmd_mat.ExecuteNonQuery();

                            string insert_mat_po = "INSERT " + MRPClass.POCreationTableName() + " ([PONumber],[ItemCode],[TaxGroup],[TaxItemGroup],[Qty],[Cost],[TotalCost], [ItemPK], [Identifier]) VALUES (@ponumber, @code, @taxgroup, @taxitem, @poqty, @pocost, @pototal, @itempk, @identifier)";

                            SqlCommand cmd_mat_po = new SqlCommand(insert_mat_po, conn);
                            cmd_mat_po.Parameters.AddWithValue("@ponumber", PONumber);
                            cmd_mat_po.Parameters.AddWithValue("@code", item_code);
                            cmd_mat_po.Parameters.AddWithValue("@taxgroup", taxgroup);
                            cmd_mat_po.Parameters.AddWithValue("@taxitem", taxitem);
                            cmd_mat_po.Parameters.AddWithValue("@poqty", Convert.ToDouble(po_qty));
                            cmd_mat_po.Parameters.AddWithValue("@pocost", Convert.ToDouble(po_cost));
                            cmd_mat_po.Parameters.AddWithValue("@pototal", Convert.ToDouble(po_total));
                            cmd_mat_po.Parameters.AddWithValue("@itempk", pk);
                            cmd_mat_po.Parameters.AddWithValue("@identifier", identifier);
                            cmd_mat_po.CommandType = CommandType.Text;
                            cmd_mat_po.ExecuteNonQuery();
                            break;

                        case "2"://Opex
                            string update_opex = "UPDATE " + MRPClass.OpexTable() + " SET [QtyPO] = '" + po_qty + "' WHERE [PK] = '" + pk + "'";
                            SqlCommand cmd_opex = new SqlCommand(update_opex, conn);
                            cmd_opex.ExecuteNonQuery();

                            string insert_opex_po = "INSERT " + MRPClass.POCreationTableName() + " ([PONumber],[ItemCode],[TaxGroup],[TaxItemGroup],[Qty],[Cost],[TotalCost], [ItemPK], [Identifier]) VALUES (@ponumber, @code, @taxgroup, @taxitem, @poqty, @pocost, @pototal, @itempk, @identifier)";

                            SqlCommand cmd_opex_po = new SqlCommand(insert_opex_po, conn);
                            cmd_opex_po.Parameters.AddWithValue("@ponumber", PONumber);
                            cmd_opex_po.Parameters.AddWithValue("@code", item_code);
                            cmd_opex_po.Parameters.AddWithValue("@taxgroup", taxgroup);
                            cmd_opex_po.Parameters.AddWithValue("@taxitem", taxitem);
                            cmd_opex_po.Parameters.AddWithValue("@poqty", Convert.ToDouble(po_qty));
                            cmd_opex_po.Parameters.AddWithValue("@pocost", Convert.ToDouble(po_cost));
                            cmd_opex_po.Parameters.AddWithValue("@pototal", Convert.ToDouble(po_total));
                            cmd_opex_po.Parameters.AddWithValue("@itempk", pk);
                            cmd_opex_po.Parameters.AddWithValue("@identifier", identifier);
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
                    //MRPClass.PrintString(dataColumn.FieldName);
                    if (dataColumn.FieldName == "TaxGroup" || dataColumn.FieldName == "TaxItemGroup")
                        e.Errors[dataColumn] = "Value cannot be null.";
                }
            }

            //Displays the error row if there is at least one error. 
            if (e.Errors.Count > 0) e.RowError = "Please, fill all fields.";
        }
        protected void ItemsRequestedByFilterCondition_1(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            //ASPxComboBox comboBox = (ASPxComboBox)source;
            //comboBox.DataSource = MRPClass.TaxGroupTable();

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "TaxGroup";
            //comboBox.Columns.Add(l_value);

            //comboBox.ValueField = "TaxGroup";
            //comboBox.TextField = "TaxGroup";
            //comboBox.DataBind();
        }
        protected void ItemsRequestedByFilterCondition_2(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            //ASPxComboBox comboBox = (ASPxComboBox)source;
            //comboBox.DataSource = MRPClass.TaxItemGroupTable();

            //ListBoxColumn l_value = new ListBoxColumn();
            //l_value.FieldName = "TaxItemGroup";
            //comboBox.Columns.Add(l_value);

            //comboBox.ValueField = "TaxItemGroup";
            //comboBox.TextField = "TaxItemGroup";
            //comboBox.DataBind(); ;
        }

        protected void Send_Click(object sender, EventArgs e)
        {

        }

        protected void DocNumber_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            DataTable dtRecord = MRPClass.Master_MRP_List_DOCNUM();
            combo.DataSource = dtRecord;

            ListBoxColumn l_ValueField = new ListBoxColumn();
            l_ValueField.FieldName = "DocNumber";
            l_ValueField.Caption = "MRP#";
            l_ValueField.Width = 150;
            combo.Columns.Add(l_ValueField);

            ListBoxColumn l_TextField = new ListBoxColumn();
            l_TextField.FieldName = "MRPMonth";
            l_TextField.Caption = "Month/Year";
            l_TextField.Width = 75;
            combo.Columns.Add(l_TextField);

            ListBoxColumn l_TextField2 = new ListBoxColumn();
            l_TextField2.FieldName = "EntityCode";
            l_TextField2.Caption = "Entity";
            l_TextField2.Width = 150;
            combo.Columns.Add(l_TextField2);

            ListBoxColumn l_TextField3 = new ListBoxColumn();
            l_TextField3.FieldName = "BUCode";
            l_TextField3.Caption = "BU";
            l_TextField3.Width = 250;
            combo.Columns.Add(l_TextField3);

            combo.ValueField = "DocNumber";
            combo.TextField = "DocNumber";
            combo.DataBind();
            combo.TextFormatString = "{0}";
        }
    }
}