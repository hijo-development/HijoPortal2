using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class mrp_previewforapproval : System.Web.UI.Page
    {
        private static int mrp_key = 0, appflwln = 0, iStatusKey = 0;
        private static int
            PK_MAT = 0,
            PK_OPEX = 0,
            PK_MAN = 0,
            PK_CAPEX = 0,
            PK_REV = 0;
        private static string itemcommand = "", entitycode = "", buCode = "", docnum = "";
        private const string matstring = "Materials", opexstring = "Opex", manstring = "Manpower", capexstring = "Capex", revstring = "Revenue";
        private static DateTime dateCreated;

        protected void LogsBtn_Click(object sender, EventArgs e)
        {
            string tablename = "";
            int PK = 0;

            if (itemcommand == matstring)
            {
                tablename = MRPClass.MaterialsTableLogs();
                PK = PK_MAT;
            }
            else if (itemcommand == opexstring)
            {
                tablename = MRPClass.OpexTableLogs();
                PK = PK_OPEX;
            }
            else if (itemcommand == manstring)
            {
                tablename = MRPClass.ManpowerTableLogs();
                PK = PK_MAN;
            }
            else if (itemcommand == capexstring)
            {
                tablename = MRPClass.CapexTableLogs();
                PK = PK_CAPEX;
            }
            else if (itemcommand == revstring)
            {
                tablename = MRPClass.RevenueTableLogs();
                PK = PK_REV;
            }

            if (PK == 0)
                return;

            //Query if log exist
            string query = "SELECT COUNT(*) FROM " + tablename + " WHERE MasterKey = '" + PK + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand comm = new SqlCommand(query, conn);
            int count = Convert.ToInt32(comm.ExecuteScalar());
            if (count > 0)//edit
            {
                string update = "UPDATE " + tablename + " SET [Remarks] = @Remarks WHERE [MasterKey] = '" + PK + "' AND UserKey = '" + Session["CreatorKey"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Remarks", LogsMemo.Text);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            else//add
            {
                string insert = "INSERT INTO " + tablename + " ([MasterKey], [UserKey], [Remarks]) VALUES (@MasterKey, @UserKey, @Remarks)";

                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@MasterKey", PK);
                cmd.Parameters.AddWithValue("@UserKey", Convert.ToInt32(Session["CreatorKey"]));
                cmd.Parameters.AddWithValue("@Remarks", LogsMemo.Text);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            conn.Close();

            LogsPopup.ShowOnPageLoad = false;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            CheckCreatorKey();

            iStatusKey = MRPClass.MRP_ApprvLine_Status(mrp_key, appflwln);
            StatusHidden["hidden_preview_iStatusKey"] = iStatusKey;

            //MRPClass.PrintString(appflwln.ToString());

            if (iStatusKey == 0)
            {

                bool isAllowed = false;
                switch (appflwln)
                {
                    case 1:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPSCMLead", dateCreated);
                            break;
                        }
                    case 2:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPFinanceLead", dateCreated);
                            break;
                        }
                    case 3:
                        {
                            isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPExecutive", dateCreated);
                            break;
                        }
                }
                if (isAllowed == true)
                {
                    //MRPClass.PrintString("Approved");
                    PopupSubmitAppPreview.ShowOnPageLoad = false;
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                    MRPApproveClass.MRP_Approve(docnum.ToString(), mrp_key, dateCreated, appflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));
                    Submit.Enabled = false;
                    Load_MRP(docnum);

                    ModalPopupExtenderLoading.Hide();

                    MRPNotifyMsgPrevApp.Text = MRPClass.successfully_approved;
                    MRPNotifyMsgPrevApp.ForeColor = System.Drawing.Color.Black;
                    MRPNotifyPrevApp.HeaderText = "Info";
                    MRPNotifyPrevApp.ShowOnPageLoad = true;
                }
                else
                {
                    MRPNotifyMsgPrevApp.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
                    MRPNotifyMsgPrevApp.ForeColor = System.Drawing.Color.Red;
                    MRPNotifyPrevApp.HeaderText = "Info";
                    MRPNotifyPrevApp.ShowOnPageLoad = true;
                }

            }

            //else
            //{

            //    //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            //    //MRPNotificationMessage.Text = "Document already submitted to BU / SSU Lead for review.";
            //    //MRPNotify.HeaderText = "Alert";
            //    //MRPNotify.ShowOnPageLoad = true;

            //}


        }

        private void HideTableData(ListViewItemEventArgs e)
        {
            if (entitycode != MRPClass.train_entity)
            {
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tableDataRevDesc");
                td.Visible = false;

                HtmlTableCell pk_td = (HtmlTableCell)e.Item.FindControl("pk_td");
                pk_td.Visible = false;
            }
        }

        private void Load_MRP(string docnum)
        {
            //string query = "SELECT TOP (100) PERCENT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, " +
            //                  " dbo.tbl_MRP_List.DateCreated, dbo.tbl_MRP_List.EntityCode, dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, " +
            //                  " dbo.tbl_MRP_List.BUCode, dbo.vw_AXOperatingUnitTable.NAME AS BUCodeDesc, dbo.tbl_MRP_List.MRPMonth, " +
            //                  " dbo.tbl_MRP_List.MRPYear, dbo.tbl_MRP_List.StatusKey, dbo.tbl_MRP_Status.StatusName, " +
            //                  " dbo.tbl_MRP_List.CreatorKey, dbo.tbl_MRP_List.LastModified " +
            //                  " FROM  dbo.tbl_MRP_List LEFT OUTER JOIN " +
            //                  " dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
            //                  " dbo.tbl_MRP_Status ON dbo.tbl_MRP_List.StatusKey = dbo.tbl_MRP_Status.PK LEFT OUTER JOIN " +
            //                  " dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID " +
            //                  " WHERE(dbo.tbl_MRP_List.DocNumber = '" + DocNum.Text.ToString().Trim() + "') " +
            //                  " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";

            string query = "SELECT tbl_MRP_List.*, " +
                           " vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                           " vw_AXOperatingUnitTable.NAME AS BUCodeDesc, " +
                           " tbl_MRP_Status.StatusName, tbl_Users.Lastname, " +
                           " tbl_Users.Firstname, tbl_MRP_List.EntityCode, " +
                           " tbl_MRP_List.BUCode " +
                           " FROM tbl_MRP_List INNER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK " +
                           " LEFT OUTER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER " +
                           " LEFT OUTER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK " +
                           " LEFT OUTER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID " +
                           " WHERE dbo.tbl_MRP_List.DocNumber = '" + docnum + "' " +
                           " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DocNum.Text = reader["DocNumber"].ToString();
                //DateCreated.Text = reader["DateCreated"].ToString();
                dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                mrp_key = Convert.ToInt32(reader["PK"]);
                entitycode = reader["EntityCode"].ToString();
                EntityCode.Text = reader["EntityCodeDesc"].ToString();
                buCode = reader["BUCode"].ToString();
                BUCode.Text = reader["BUCodeDesc"].ToString();
                Month.Text = MRPClass.Month_Name(Int32.Parse(reader["MRPMonth"].ToString()));
                Year.Text = reader["MRPYear"].ToString();
                //Status.Text = reader["StatusName"].ToString();
                Creator.Text = EncryptionClass.Decrypt(reader["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(reader["Lastname"].ToString());
                Status.Text = reader["StatusName"].ToString();
            }
            reader.Close();
            conn.Close();

            iStatusKey = MRPClass.MRP_ApprvLine_Status(mrp_key, appflwln);
            StatusHidden["hidden_preview_iStatusKey"] = iStatusKey;
            WorkLineHidden["hidden_preview_wrkflwln"] = appflwln;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                //docnum = Request.Params["DocNum"].ToString();
                //appflwln = Convert.ToInt32(Request.Params["ApprvLn"].ToString());
                //DocNum.Text = Request.Params["DocNum"].ToString();

                //Session["mrp_docNum"] = value.ToString();
                //Session["mrp_appLine"] = wrklineval.ToString();
                docnum = Session["mrp_docNum"].ToString();
                appflwln = Convert.ToInt32(Session["mrp_appLine"]);
                DocNum.Text = Session["mrp_docNum"].ToString();

                if (MRPClass.PreviewApprovalRights(Convert.ToInt32(Session["CreatorKey"]), docnum) == false)
                {
                    MRPAccessRightsMsg.Text = "Acces Denied!";
                    MRPAccessRights.HeaderText = "Access Denied";
                    MRPAccessRights.ShowOnPageLoad = true;
                }

                Load_MRP(docnum);
            }

            BindAll();
        }

        private void BindAll()
        {
            string docnum = DocNum.Text.ToString();
            DMRoundPanel.HeaderText = Constants.DM_string();
            GridPreviewDM.DataSource = DM(docnum);
            GridPreviewDM.KeyFieldName = "PK";
            GridPreviewDM.DataBind();

            OPRoundPanel.HeaderText = Constants.OP_string();
            GridPreviewOP.DataSource = OP(docnum);
            GridPreviewOP.KeyFieldName = "PK";
            GridPreviewOP.DataBind();

            MANRoundPanel.HeaderText = Constants.MAN_string();
            GridPreviewMAN.DataSource = MAN(docnum);
            GridPreviewMAN.KeyFieldName = "PK";
            GridPreviewMAN.DataBind();

            CARoundPanel.HeaderText = Constants.CA_string();
            GridPreviewCA.DataSource = CA(docnum);
            GridPreviewCA.KeyFieldName = "PK";
            GridPreviewCA.DataBind();

            if (entitycode != Constants.HITS_CODE())
            {
                RevRoundPanel.HeaderText = Constants.REV_string();

                GridPreviewREV.DataSource = REV(docnum);
                GridPreviewREV.KeyFieldName = "PK";
                GridPreviewREV.DataBind();
            }
            else
            {
                RevRoundPanel.Visible = false;
                GridPreviewREV.Visible = false;
            }

            TotalRoundPanel.HeaderText = Constants.SUMMARY_string();
            GridPreviewSummary.DataSource = SUMMARY(docnum);
            GridPreviewSummary.KeyFieldName = "PK";
            GridPreviewSummary.DataBind();

        }

        public static DataTable DM(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Expense", typeof(string));
                dtTable.Columns.Add("Activity", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));
                dtTable.Columns.Add("RecQty", typeof(string));
                dtTable.Columns.Add("RecCost", typeof(string));
                dtTable.Columns.Add("RecTotalCost", typeof(string));
            }

            string farm_query = "[dbo].[DirectMaterialPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["OperatingUnit"] = row["OperUinit"].ToString();
                    dtRow["Activity"] = row["Activity"].ToString();
                    dtRow["Expense"] = row["ExpenseCode"].ToString();
                    dtRow["Descripiton"] = row["ItemDescription"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");

                    dtRow["RecQty"] = Convert.ToDouble(row["EdittedQty"].ToString()).ToString("N");
                    dtRow["RecCost"] = Convert.ToDouble(row["EdittedCost"].ToString()).ToString("N");
                    dtRow["RecTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        protected void RightsOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        public static DataTable OP(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Expense", typeof(string));
                dtTable.Columns.Add("ProcurementCategory", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));

                dtTable.Columns.Add("RecQty", typeof(string));
                dtTable.Columns.Add("RecCost", typeof(string));
                dtTable.Columns.Add("RecTotalCost", typeof(string));
            }

            string farm_query = "[dbo].[OPEXPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString(); ;
                    dtRow["OperatingUnit"] = row["OprUnit"].ToString();
                    dtRow["ProcurementCategory"] = row["ProcCat"].ToString();
                    dtRow["Expense"] = row["ExpenseCode"].ToString();
                    dtRow["Descripiton"] = row["OPEX_Description"].ToString();
                    dtRow["UOM"] = row["UOMDesc"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");

                    dtRow["RecQty"] = Convert.ToDouble(row["EdittedQty"].ToString()).ToString("N");
                    dtRow["RecCost"] = Convert.ToDouble(row["EdittedCost"].ToString()).ToString("N");
                    dtRow["RecTotalCost"] = Convert.ToDouble(row["EdittedTotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;
        }

        public static DataTable MAN(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Activity", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));

                dtTable.Columns.Add("RecQty", typeof(string));
                dtTable.Columns.Add("RecCost", typeof(string));
                dtTable.Columns.Add("RecTotalCost", typeof(string));
            }

            string farm_query = "[dbo].[ManPowerPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString(); ;
                    dtRow["OperatingUnit"] = row["OprUnitDesc"].ToString();
                    dtRow["Activity"] = row["ActivityName"].ToString();
                    dtRow["Descripiton"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOMDesc"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");

                    dtRow["RecQty"] = Convert.ToDouble(row["EdittedQty"].ToString()).ToString("N");
                    dtRow["RecCost"] = Convert.ToDouble(row["EdittedCost"].ToString()).ToString("N");
                    dtRow["RecTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        public static DataTable CA(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("Expense", typeof(string));
                dtTable.Columns.Add("ProcurementCategory", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("UOM", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("Cost", typeof(string));
                dtTable.Columns.Add("TotalCost", typeof(string));

                dtTable.Columns.Add("RecQty", typeof(string));
                dtTable.Columns.Add("RecCost", typeof(string));
                dtTable.Columns.Add("RecTotalCost", typeof(string));

            }

            string farm_query = "[dbo].[CAPEXPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["OperatingUnit"] = row["OprUnit"].ToString();
                    dtRow["ProcurementCategory"] = row["ProdCat"].ToString();
                    dtRow["Descripiton"] = row["Description"].ToString();
                    dtRow["UOM"] = row["UOM"].ToString();
                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["Cost"] = Convert.ToDouble(row["Cost"].ToString()).ToString("N");
                    dtRow["TotalCost"] = Convert.ToDouble(row["TotalCost"].ToString()).ToString("N");

                    dtRow["RecQty"] = Convert.ToDouble(row["EdittedQty"].ToString()).ToString("N");
                    dtRow["RecCost"] = Convert.ToDouble(row["EdittedCost"].ToString()).ToString("N");
                    dtRow["RecTotalCost"] = Convert.ToDouble(row["EdittiedTotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        public static DataTable REV(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("OperatingUnit", typeof(string));
                dtTable.Columns.Add("ProductName", typeof(string));
                dtTable.Columns.Add("FarmName", typeof(string));
                dtTable.Columns.Add("Price", typeof(string));
                dtTable.Columns.Add("Volume", typeof(string));
                dtTable.Columns.Add("TotalPrice", typeof(string));
            }

            string farm_query = "[dbo].[RevenueAssumptionPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);
            //cmd.ExecuteNonQuery();

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["OperatingUnit"] = row["OperUinit"].ToString();
                    dtRow["ProductName"] = row["ProductName"].ToString();
                    dtRow["FarmName"] = row["FarmName"].ToString();
                    dtRow["Price"] = Convert.ToDouble(row["Price"].ToString()).ToString("N");
                    dtRow["Volume"] = Convert.ToDouble(row["Volume"].ToString()).ToString("N");
                    dtRow["TotalPrice"] = Convert.ToDouble(row["TotalPrice"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        public static DataTable SUMMARY(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("Description", typeof(string));
                dtTable.Columns.Add("Total", typeof(string));
            }

            string farm_query = "[dbo].[MRPSummaryPreview]";
            cmd = new SqlCommand(farm_query, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@headerdocnum", docnumber);
            cmd.Parameters.AddWithValue("@entity", entitycode);

            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["Sort"].ToString();
                    dtRow["Description"] = row["MRPGroup"].ToString();
                    dtRow["Total"] = Convert.ToDouble(row["EdittedTotalCost"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();
            return dtTable;

        }

        private void ModifyGridColumns(ASPxGridView grid)
        {
            grid.Columns["PK"].Visible = false;
            grid.Columns["Qty"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["Cost"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["TotalCost"].CellStyle.HorizontalAlign = HorizontalAlign.Right;

            grid.Columns["RecQty"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["RecQty"].Caption = "Recommended Quantity For Purchase";

            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#66ffcc");
            grid.Columns["RecQty"].CellStyle.BackColor = col;
            grid.Columns["RecQty"].HeaderStyle.BackColor = col;

            grid.Columns["RecCost"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["RecCost"].Caption = "Cost";
            grid.Columns["RecTotalCost"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["RecTotalCost"].Caption = "Total Cost";

            if (entitycode != Constants.TRAIN_CODE())
                grid.Columns["OperatingUnit"].Visible = false;
        }

        protected void GridPreviewDM_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        protected void GridPreviewOP_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        protected void GridPreviewMAN_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        protected void GridPreviewCA_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            ModifyGridColumns(grid);
        }

        protected void GridPreviewREV_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            grid.Columns["PK"].Visible = false;
            grid.Columns["Price"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["Volume"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns["TotalPrice"].CellStyle.HorizontalAlign = HorizontalAlign.Right;

            if (entitycode != Constants.TRAIN_CODE())
                grid.Columns["OperatingUnit"].Visible = false;
        }

        protected void GridPreviewSummary_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            grid.Columns["PK"].Visible = false;
            grid.Columns["Total"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
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
    }
}