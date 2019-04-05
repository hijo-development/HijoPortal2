using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.IO;
using HijoPortal.classes;

namespace HijoPortal
{
    public partial class mrp_po_list : System.Web.UI.Page
    {

        private static string ponumber = "";

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

        protected void Add_Click(object sender, EventArgs e)
        {
            Session["MRP_Number"] = null;
            Response.Redirect("mrp_po_selectitem.aspx");
        }

        private void Bind_PO_List()
        {
            DataTable dtTable = POClass.PO_Created_List();
            gridCreatedPO.DataSource = dtTable;
            gridCreatedPO.KeyFieldName = "PK";
            gridCreatedPO.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            if (!Page.IsPostBack)
            {
                Bind_PO_List();
            }
        }

        protected void gridCreatedPO_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            string btnID = e.ButtonID;
            ASPxGridView grid = sender as ASPxGridView;
            ponumber = grid.GetRowValues(grid.FocusedRowIndex, "PONumber").ToString();
            switch (btnID)
            {
                case "Edit":
                    {
                        ASPxWebControl.RedirectOnCallback("mrp_po_addedit.aspx?PONum=" + ponumber);
                        //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum="+ponumber;
                        break;
                    }                    
                case "Delete":
                    {
                        break;
                    }
                case "Submit":
                    {

                        break;
                    }

            }
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            MRPClass.PrintString("ok click");
            ponumber = gridCreatedPO.GetRowValues(gridCreatedPO.FocusedRowIndex, "PONumber").ToString();

            DataTable dt = new DataTable();

            dt.Columns.Add("PK", typeof(string));
            dt.Columns.Add("Identifier", typeof(string));
            dt.Columns.Add("ItemPK", typeof(string));
            dt.Columns.Add("Qty", typeof(Double));

            string update = "";
            string query = "SELECT [PK], [Identifier], [ItemPK], [Qty] FROM [hijo_portal].[dbo].[tbl_POCreation_Details] WHERE PONumber = '" + ponumber + "'";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string pk = reader["PK"].ToString();
                string identifier = reader["Identifier"].ToString();
                string itempk = reader["ItemPK"].ToString();
                Double qty = Convert.ToDouble(reader["Qty"].ToString());

                dt.Rows.Add(new object[] { pk, identifier, itempk, qty });

            }
            reader.Close();

            Double original_qty = 0, remaining_qty = 0;
            foreach (DataRow dr in dt.Rows)
            {
                MRPClass.PrintString(dr["PK"] + ", " + dr["Identifier"] + ", " + dr["ItemPK"] + ", " + dr["Qty"]);
                Double poqty = Convert.ToDouble(dr["Qty"]);

                switch (dr["Identifier"])
                {
                    case "1"://Direct Materials
                        query = "SELECT [QtyPO] FROM [hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials] WHERE [PK] = '" + dr["ItemPK"] + "'";
                        cmd = new SqlCommand(query, conn);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            original_qty = Convert.ToDouble(reader["QtyPO"].ToString());
                        }
                        reader.Close();
                        remaining_qty = original_qty - poqty;

                         update = "UPDATE [hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials] SET [QtyPO] = '" + remaining_qty + "' WHERE [PK] = '" + dr["ItemPK"] + "'";

                        cmd = new SqlCommand(update, conn);
                        cmd.ExecuteNonQuery();

                        break;
                    case "2"://Opex
                        query = "SELECT [QtyPO] FROM [hijo_portal].[dbo].[tbl_MRP_List_OPEX] WHERE [PK] = '" + dr["ItemPK"] + "'";
                        cmd = new SqlCommand(query, conn);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            original_qty = Convert.ToDouble(reader["QtyPO"].ToString());
                        }
                        reader.Close();
                        remaining_qty = original_qty - poqty;

                         update = "UPDATE [hijo_portal].[dbo].[tbl_MRP_List_OPEX] SET [QtyPO] = '" + remaining_qty + "' WHERE [PK] = '" + dr["ItemPK"] + "'";

                        cmd = new SqlCommand(update, conn);
                        cmd.ExecuteNonQuery();
                        break;
                }
            }

            string delete = "DELETE FROM [hijo_portal].[dbo].[tbl_POCreation] WHERE [PONumber] = '" + ponumber + "'";
            cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Bind_PO_List();
        }

        protected void OK_SUBMIT_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dt1 = new DataTable();
            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;

            ponumber = gridCreatedPO.GetRowValues(gridCreatedPO.FocusedRowIndex, "PONumber").ToString();

            string qry = "SELECT tbl_POCreation_Details.* FROM tbl_POCreation_Details WHERE (PONumber = '" + ponumber + "')";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["TaxGroup"].ToString().Trim() == "")
                    {
                        POListNotify.HeaderText = "Error...";
                        POListNotifyLbl.Text = "Details has no Tax Group.";
                        POListNotify.ShowOnPageLoad = true;
                        return;
                    }
                    if (row["TaxItemGroup"].ToString().Trim() == "")
                    {
                        POListNotify.HeaderText = "Error...";
                        POListNotifyLbl.Text = "Details has no Tax Item Group.";
                        POListNotify.ShowOnPageLoad = true;
                        return;
                    }
                }
            }
            dt.Clear();
            conn.Close();

            Submit_Method();

            Bind_PO_List();
        }

        private void Submit_Method()
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dt1 = new DataTable();
            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            string qry = "", sFiLeName = "", sFileNameD = "";
            string sFile = "", sFileD = "", sFileDest = "", sFileDDest = "";
            string sServerDir = HttpContext.Current.Server.MapPath("~");
            //string sServerDir = @"C:";
            string sDir = sServerDir + @"\po_file";
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }

            conn.Open();
            qry = "SELECT dbo.tbl_POCreation.PK, dbo.tbl_POCreation.PONumber, dbo.tbl_POCreation.MRPNumber, dbo.tbl_POCreation.DateCreated, dbo.tbl_POCreation.CreatorKey, dbo.tbl_POCreation.ExpectedDate, dbo.tbl_POCreation.VendorCode, dbo.tbl_POCreation.PaymentTerms, dbo.tbl_POCreation.CurrencyCode,dbo.tbl_POCreation.InventSite, dbo.tbl_POCreation.InventSiteWarehouse, dbo.tbl_POCreation.InventSiteWarehouseLocation, dbo.vw_AXVendTable.NAME, dbo.vw_AXVendTable.VENDGROUP, dbo.vw_AXVendTable.INCLTAX, dbo.vw_AXVendTable.PAYMMODE, dbo.vw_AXVendTable.TAXGROUP, dbo.tbl_POCreation.EntityCode, dbo.tbl_POCreation.BUSSUCode FROM  dbo.tbl_POCreation LEFT OUTER JOIN dbo.vw_AXVendTable ON dbo.tbl_POCreation.VendorCode = dbo.vw_AXVendTable.ACCOUNTNUM WHERE(dbo.tbl_POCreation.PONumber = '" + ponumber + "')";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string sPORemarks = "MOP Number " + row["MRPNumber"].ToString();
                    string sIncTax = "";
                    if (Convert.ToInt32(row["INCLTAX"]) == 0)
                    {
                        sIncTax = "No";
                    }
                    else
                    {
                        sIncTax = "Yes";
                    }

                    string sDefaultDimension = "";
                    if (row["EntityCode"].ToString().Trim() == "0000")
                    {
                        sDefaultDimension = row["BUSSUCode"].ToString() + "__";
                    }
                    else if (row["EntityCode"].ToString().Trim() == "0303")
                    {
                        sDefaultDimension = "_" + row["BUSSUCode"].ToString() + "_";
                    }
                    else
                    {
                        sDefaultDimension = "";
                    }

                    sFiLeName = row["EntityCode"].ToString() + "_" + row["PONumber"].ToString() + "_H.txt";
                    sFile = sDir + @"\" + sFiLeName;

                    if (File.Exists(sFile))
                    {
                        File.Delete(sFile);
                    }
                    FileStream fs = File.Create(sFile);
                    fs.Dispose();

                    if (File.Exists(sFile))
                    {
                        using (StreamWriter w = File.AppendText(sFile))
                        {
                            w.WriteLine("PurchId|AccountingDate|DeliveryDate|CurrencyCode|OrderAccount|InvoiceAccount|DeliveryName|PurchName|Payment|InclTax|PaymMode|PORemarks|DocumentState|DocumentStatus|InventSiteId|Remarks|VendGroup|TaxGroup|LanguageId|PostingProfile|PurchaseType|PurchPoolId|PurchStatus|DefaultDimension");
                            w.Close();

                        }
                        using (StreamWriter w = File.AppendText(sFile))
                        {
                            w.WriteLine(row["PONumber"].ToString() + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|" + row["CurrencyCode"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row["NAME"].ToString() + "|" + row["NAME"].ToString() + "|" + row["PaymentTerms"].ToString() + "|" + sIncTax + "|" + row["PAYMMODE"].ToString() + "|" + sPORemarks.ToString() + "|Draft|Open order|" + row["InventSite"].ToString() + "|" + sPORemarks.ToString() + "|" + row["VENDGROUP"].ToString() + "|" + row["TAXGROUP"].ToString() + "|en-us|Gen|Purchase order||Open order|" + sDefaultDimension.ToString());
                            w.Close();
                        }
                    }

                    int iLineNumber = 0;
                    string sDefaultDimensionLine = "";
                    qry = "SELECT ItemCode, TaxGroup, TaxItemGroup, Qty, Cost, TotalCost, POUOM, (CASE Identifier WHEN 1 THEN (SELECT OprUnit FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE (SELECT OprUnit FROM  dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) END) AS OprUnit, (CASE Identifier WHEN 1 THEN (SELECT ItemDescription + (CASE LTRIM(RTRIM(ItemDescriptionAddl)) WHEN '' THEN '' ELSE ' (' + ItemDescriptionAddl + ')' END) AS ItemDesc FROM  dbo.tbl_MRP_List_DirectMaterials WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 1)) ELSE (SELECT Description + (CASE LTRIM(RTRIM(DescriptionAddl)) WHEN '' THEN '' ELSE ' (' + DescriptionAddl + ')' END) AS ItemDesc FROM dbo.tbl_MRP_List_OPEX WHERE(PK = dbo.tbl_POCreation_Details.ItemPK) AND(TableIdentifier = 2)) END) AS ItemDesc FROM dbo.tbl_POCreation_Details WHERE(PONumber = '" + ponumber + "')";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        sFileNameD = row["EntityCode"].ToString() + "_" + row["PONumber"].ToString() + "_L.txt";
                        sFileD = sDir + @"\" + sFileNameD;

                        if (File.Exists(sFileD))
                        {
                            File.Delete(sFileD);
                        }
                        FileStream fsd = File.Create(sFileD);
                        fsd.Dispose();

                        if (File.Exists(sFileD))
                        {
                            using (StreamWriter w = File.AppendText(sFileD))
                            {
                                w.WriteLine("PurchId|VendAccount|DeliveryName|Name|VendGroup|InventSiteID|InventLocationID|wMSLocationId|Complete|CreateFixedAsset|CurrencyCode|DeliveryDate|IsFinalized|IsPwp|ItemId|PurchQty|PurchUnit|PurchPrice|LineAmount|LineNumber|PriceUnit|MatchingPolicy|OverDeliveryPct|PurchaseType|PurchStatus|TaxGroup|TaxItemGroup|UnderDeliveryPct|VariantId|DefaultDimension|WorkflowState");
                                w.Close();
                            }
                        }

                        foreach (DataRow row1 in dt1.Rows)
                        {
                            iLineNumber = iLineNumber + 1;
                            if (File.Exists(sFileD))
                            {

                                if (row["EntityCode"].ToString().Trim() == "0000")
                                {
                                    sDefaultDimensionLine = row["BUSSUCode"].ToString() + "__";
                                }
                                else if (row["EntityCode"].ToString().Trim() == "0303")
                                {
                                    sDefaultDimensionLine = "_" + row["BUSSUCode"].ToString() + "_";
                                }
                                else if (row["EntityCode"].ToString().Trim() == "0101")
                                {
                                    sDefaultDimensionLine = "__" + row1["OprUnit"].ToString();
                                }
                                else
                                {
                                    sDefaultDimensionLine = "";
                                }

                                using (StreamWriter w = File.AppendText(sFileD))
                                {
                                    w.WriteLine(row["PONumber"].ToString() + "|" + row["VendorCode"].ToString() + "|" + row1["ItemDesc"].ToString() + "|" + row1["ItemDesc"].ToString() + "|" + row["VENDGROUP"].ToString() + "|" + row["InventSite"].ToString() + "|" + row["InventSiteWarehouse"].ToString() + "|" + row["InventSiteWarehouseLocation"].ToString() + "|0|0|" + row["CurrencyCode"].ToString() + "|" + Convert.ToDateTime(row["ExpectedDate"]).ToString("MM/dd/yyyy") + "|0|0|" + row1["ItemCode"].ToString() + "|" + Convert.ToDouble(row1["Qty"]).ToString("#0.0000") + "|" + row1["POUOM"].ToString() + "|" + Convert.ToDouble(row1["Cost"]).ToString("#0.0000") + "|" + Convert.ToDouble(row1["TotalCost"]).ToString("#0.0000") + "|" + iLineNumber.ToString() + "|1|Three-way matching|0|Purchase order|Open order|" + row1["TaxGroup"].ToString() + "|" + row1["TaxItemGroup"].ToString() + "|100||" + sDefaultDimensionLine.ToString() + "|Not submitted");
                                    w.Close();
                                }
                            }
                        }
                    }
                    dt1.Clear();

                    string sDomain = "", sUsername = "", sPassword = "";

                    qry = "SELECT tbl_AXPOUploadingPath.* FROM tbl_AXPOUploadingPath WHERE ([Entity] = '" + row["EntityCode"].ToString() + "')";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dt1.Rows)
                        {
                            sFileDest = row1["POHeaderPath"].ToString() + @"\" + sFiLeName;
                            sFileDDest = row1["POLinePath"].ToString() + @"\" + sFileNameD;
                            sDomain = row1["Domain"].ToString();
                            sUsername = row1["UserName"].ToString();
                            if (row1["Password"].ToString().Trim() != "")
                            {
                                sPassword = EncryptionClass.Decrypt(row1["Password"].ToString());
                            }
                            else
                            {
                                sPassword = row1["Password"].ToString();
                            }

                            sDomain = "hijo"; sUsername = "hijoportal_client"; sPassword = "hPortal@2019";

                            try
                            {
                                using (var impersonation = new ImpersonatedUser(sUsername, sDomain, sPassword))
                                {
                                    try
                                    {
                                        if (sFileDest.Trim() != "")
                                        {
                                            if (File.Exists(sFile) == true)
                                            {
                                                File.Copy(sFile, sFileDest, true);
                                            }
                                        }
                                        if (sFileDDest.Trim() != "")
                                        {
                                            if (File.Exists(sFileD) == true)
                                            {
                                                File.Copy(sFileD, sFileDDest, true);
                                            }
                                        }
                                        try
                                        {
                                            qry = "UPDATE tbl_POCreation SET [POStatus] = 1 WHERE ([PONumber] = @PONumber)";
                                            cmd = new SqlCommand(qry, conn);
                                            cmd.Parameters.AddWithValue("@PONumber", ponumber);
                                            cmd.CommandType = CommandType.Text;
                                            cmd.ExecuteNonQuery();

                                            ModalPopupExtenderLoading.Hide();

                                            POListNotifyLbl.ForeColor = System.Drawing.Color.Black;
                                            POListNotifyLbl.Text = "Sucessfully submitted to AX";
                                            POListNotify.HeaderText = "Info";
                                            POListNotify.ShowOnPageLoad = true;

                                        }
                                        catch (SqlException exSQL)
                                        {
                                            ModalPopupExtenderLoading.Hide();
                                            POListNotifyLbl.ForeColor = System.Drawing.Color.Red;
                                            POListNotifyLbl.Text = exSQL.ToString();
                                            POListNotify.HeaderText = "Error";
                                            POListNotify.ShowOnPageLoad = true;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ModalPopupExtenderLoading.Hide();
                                        POListNotifyLbl.ForeColor = System.Drawing.Color.Red;
                                        POListNotifyLbl.Text = ex.ToString();
                                        POListNotify.HeaderText = "Error";
                                        POListNotify.ShowOnPageLoad = true;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                ModalPopupExtenderLoading.Hide();
                                POListNotifyLbl.ForeColor = System.Drawing.Color.Red;
                                POListNotifyLbl.Text = ex.ToString();
                                POListNotify.HeaderText = "Error";
                                POListNotify.ShowOnPageLoad = true;
                            }
                        }
                    }
                    else
                    {
                        ModalPopupExtenderLoading.Hide();
                        POListNotifyLbl.ForeColor = System.Drawing.Color.Red;
                        POListNotifyLbl.Text = "Please Call Administrator." + Environment.NewLine + Environment.NewLine + "No PO Destination Path Setup.";
                        POListNotify.HeaderText = "Error";
                        POListNotify.ShowOnPageLoad = true;
                    }
                    dt1.Clear();

                    //try
                    //{
                    //    try
                    //    {
                    //        if (sFileDest.Trim() != "")
                    //        {
                    //            if (File.Exists(sFile) == true)
                    //            {
                    //                File.Copy(sFile, sFileDest, true);
                    //            }
                    //        }

                    //        if (sFileDDest.Trim() != "")
                    //        {
                    //            if (File.Exists(sFileD) == true)
                    //            {
                    //                File.Copy(sFileD, sFileDDest, true);
                    //            }
                    //        }

                    //        try
                    //        {
                    //            qry = "UPDATE tbl_POCreation SET [POStatus] = 1 WHERE ([PONumber] = @PONumber)";
                    //            cmd = new SqlCommand(qry, conn);
                    //            cmd.Parameters.AddWithValue("@PONumber", ponumber);
                    //            cmd.CommandType = CommandType.Text;
                    //            cmd.ExecuteNonQuery();

                    //            ModalPopupExtenderLoading.Hide();

                    //            PONotifyLbl.ForeColor = System.Drawing.Color.Black;
                    //            PONotifyLbl.Text = "Sucessfully submitted to AX";
                    //            PONotify.HeaderText = "Info";
                    //            PONotify.ShowOnPageLoad = true;

                    //        }
                    //        catch (SqlException exSQL)
                    //        {
                    //            ModalPopupExtenderLoading.Hide();

                    //            PONotifyLbl.ForeColor = System.Drawing.Color.Red;
                    //            PONotifyLbl.Text = exSQL.ToString();
                    //            PONotify.HeaderText = "Error";
                    //            PONotify.ShowOnPageLoad = true;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ModalPopupExtenderLoading.Hide();

                    //        PONotifyLbl.ForeColor = System.Drawing.Color.Red;
                    //        PONotifyLbl.Text = ex.ToString();
                    //        PONotify.HeaderText = "Error";
                    //        PONotify.ShowOnPageLoad = true;
                    //    }
                    //} catch (Exception ex)
                    //{
                    //    ModalPopupExtenderLoading.Hide();

                    //    PONotifyLbl.ForeColor = System.Drawing.Color.Red;
                    //    PONotifyLbl.Text = ex.ToString();
                    //    PONotify.HeaderText = "Error";
                    //    PONotify.ShowOnPageLoad = true;
                    //}

                }
            }
            dt.Clear();

            conn.Close();
        }
    }
}