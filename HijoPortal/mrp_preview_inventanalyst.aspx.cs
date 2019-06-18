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
using DevExpress.Web;

namespace HijoPortal
{
    public partial class mrp_preview_inventanalyst : System.Web.UI.Page
    {
        private static int mrp_key = 0, wrkflwln = 0, iStatusKey = 0;
        private static string itemcommand = "", entitycode = "", buCode = "", docnumber = "";

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

        protected void OK_SUBMIT_Click(object sender, EventArgs e)
        {

            CheckCreatorKey();

            if (MRPClass.MRP_Line_Status(mrp_key, wrkflwln) == 0)
            {
                bool isAllowed = false;
                if (GlobalClass.IsSuperAdmin(Convert.ToInt32(Session["CreatorKey"])))
                {
                    isAllowed = true;
                } else
                {
                    switch (wrkflwln)

                    {
                        case 1:
                            {
                                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBULead", dateCreated, entitycode, buCode);
                                break;
                            }
                        case 2:
                            {
                                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
                                break;
                            }
                        //case 3:
                        //    {
                        //        isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPBudget_PerEntBU", dateCreated, entitycode, buCode);
                        //        break;
                        //    }
                        case 3:
                            {
                                isAllowed = GlobalClass.IsAllowed(Convert.ToInt32(Session["CreatorKey"]), "MOPInventoryAnalyst", dateCreated);
                                break;
                            }
                    }
                }
                

                if (isAllowed == true)
                {
                    PopupSubmitPreviewAnal.ShowOnPageLoad = false;
                    //MRPClass.Submit_MRP(docnumber.ToString(), mrp_key, wrkflwln + 1, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

                    MRPSubmitClass.MRP_Submit(docnumber.ToString(), mrp_key, dateCreated, wrkflwln, entitycode, buCode, Convert.ToInt32(Session["CreatorKey"]));

                    Submit.Enabled = false;

                    ModalPopupExtenderLoading.Hide();

                    MRPNotificationMessage.Text = MRPClass.successfully_submitted;
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Black;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
                else
                {
                    MRPNotificationMessage.Text = "You have no permission to perform this command!" + Environment.NewLine + "Access Denied!";
                    MRPNotificationMessage.ForeColor = System.Drawing.Color.Red;
                    MRPNotify.HeaderText = "Info";
                    MRPNotify.ShowOnPageLoad = true;
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            }

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

        protected void btAddEdit_Click(object sender, EventArgs e)
        {
            //Session["mrp_docNum"] = docnumber.ToString();
            Session["mrp_wrkLine"] = wrkflwln.ToString();
            Response.Redirect("mrp_inventanalyst.aspx?DocNum=" + Session["mrp_docNum"].ToString() + "&WrkFlwLn=" + wrkflwln.ToString());
        }

        private void HideHeader(object sender)
        {
            //MRPClass.PrintString("hideheader");
            if (entitycode != MRPClass.train_entity)
            {
                ListView listview = sender as ListView;
                HtmlTableCell th = (HtmlTableCell)listview.FindControl("tableHeaderRevDesc");
                if (th != null)
                    th.Visible = false;

                HtmlTableCell pk_th = (HtmlTableCell)listview.FindControl("pk_header");
                if (th != null)
                    pk_th.Visible = false;
            }
        }

        private static DateTime dateCreated;

        private void Load_MRP(string docnumber)
        {
            string query = "SELECT tbl_MRP_List.*, " +
                           " vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                           " vw_AXOperatingUnitTable.NAME AS BUCodeDesc, " +
                           " tbl_MRP_Status.StatusName, tbl_Users.Lastname, " +
                           " tbl_Users.Firstname, tbl_MRP_List.EntityCode, " +
                           " tbl_MRP_List.BUCode " +
                           " FROM tbl_MRP_List LEFT OUTER JOIN tbl_Users ON tbl_MRP_List.CreatorKey = tbl_Users.PK " +
                           " LEFT OUTER JOIN vw_AXOperatingUnitTable ON tbl_MRP_List.BUCode = vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER " +
                           " LEFT OUTER JOIN tbl_MRP_Status ON tbl_MRP_List.StatusKey = tbl_MRP_Status.PK " +
                           " LEFT OUTER JOIN vw_AXEntityTable ON tbl_MRP_List.EntityCode = vw_AXEntityTable.ID " +
                           " WHERE dbo.tbl_MRP_List.DocNumber = '" + docnumber + "' " +
                           " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //DocNum.Text = reader["DocNumber"].ToString();
                //DateCreated.Text = reader["DateCreated"].ToString();
                mrp_key = Convert.ToInt32(reader["PK"]);
                entitycode = reader["EntityCode"].ToString();
                dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                EntityCode.Text = reader["EntityCodeDesc"].ToString();
                buCode = reader["BUCode"].ToString();
                BUCode.Text = reader["BUCodeDesc"].ToString();
                Month.Text = MRPClass.Month_Name(Int32.Parse(reader["MRPMonth"].ToString()));
                Year.Text = reader["MRPYear"].ToString();
                Creator.Text = EncryptionClass.Decrypt(reader["Firstname"].ToString()) + " " + EncryptionClass.Decrypt(reader["Lastname"].ToString());
                Status.Text = reader["StatusName"].ToString();
                //Status.Text = reader["StatusName"].ToString();
            }
            reader.Close();
            conn.Close();

            iStatusKey = MRPClass.MRP_Line_Status(mrp_key, wrkflwln);
            StatusHidden["hidden_preview_iStatusKey"] = iStatusKey;
            WrkFlowHidden["hidden_preview_wrkflwln"] = wrkflwln;

            //MRPClass.PrintString(entitycode);
            string docnum = DocNum.Text.ToString();



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();

            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            if (!Page.IsPostBack)
            {
                //Session["mrp_docNum"] = docnumber.ToString();
                //Session["mrp_wrkLine"] = wrkflwln.ToString();
                //DocNum.Text = Request.Params["DocNum"].ToString();
                //docnumber = Request.Params["DocNum"].ToString();
                //wrkflwln = Convert.ToInt32(Request.Params["WrkFlwLn"].ToString());

                DocNum.Text = Session["mrp_docNum"].ToString();
                docnumber = Session["mrp_docNum"].ToString();
                wrkflwln = Convert.ToInt32(Session["mrp_wrkLine"]);

                if (wrkflwln == 2)
                {
                    mrpHead.InnerText = "M O P Preview (Inventory Analyst)";
                }
                //if (wrkflwln == 3)
                //{
                //    mrpHead.InnerText = "M O P Preview (Finance - Budget)";
                //}
                if (wrkflwln == 3)
                {
                    mrpHead.InnerText = "M O P Preview (Deliberation)";
                }

                if (wrkflwln == 2)
                {
                    //Submit.Text = "Submit";
                    Submit.Text = "Submit for Deliberation";
                }
                else if (wrkflwln == 3)
                {
                    Submit.Text = "Submit for Approval";
                }

                Load_MRP(docnumber);

                //string query = "SELECT TOP (100) PERCENT dbo.tbl_MRP_List.PK, dbo.tbl_MRP_List.DocNumber, " +
                //              " dbo.tbl_MRP_List.DateCreated, dbo.tbl_MRP_List.EntityCode, dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, " +
                //              " dbo.tbl_MRP_List.BUCode, dbo.vw_AXOperatingUnitTable.NAME AS BUCodeDesc, dbo.tbl_MRP_List.MRPMonth, " +
                //              " dbo.tbl_MRP_List.MRPYear, dbo.tbl_MRP_List.StatusKey, dbo.tbl_MRP_Status.StatusName, " +
                //              " dbo.tbl_MRP_List.CreatorKey, dbo.tbl_MRP_List.LastModified " +
                //              " FROM  dbo.tbl_MRP_List LEFT OUTER JOIN " +
                //              " dbo.vw_AXOperatingUnitTable ON dbo.tbl_MRP_List.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                //              " dbo.tbl_MRP_Status ON dbo.tbl_MRP_List.StatusKey = dbo.tbl_MRP_Status.PK LEFT OUTER JOIN " +
                //              " dbo.vw_AXEntityTable ON dbo.tbl_MRP_List.EntityCode = dbo.vw_AXEntityTable.ID " +
                //              " WHERE(dbo.tbl_MRP_List.DocNumber = '" + DocNum.Text.ToString().Trim() + "') " +
                //              " ORDER BY dbo.tbl_MRP_List.DocNumber DESC";
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
    }
}