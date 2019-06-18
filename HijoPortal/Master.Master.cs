using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using DevExpress.Web;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HijoPortal.classes;
using System.Collections;

namespace HijoPortal
{
    public partial class Master : System.Web.UI.MasterPage
    {
        private const string materialsIdentifier = "Materials", opexIdentifier = "OPEX", manpowerIdentifier = "Manpower", capexIdentifier = "CAPEX", revenueIdentifier = "Revenue";

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMOPSidePanel.Visible = false;
            dvItemSidePanel.Visible = false;

            TimeoutControl1.TimeOutUrl = "TimeOutPage.aspx";
            if (!IsPostBack)
            {
                txtVAT.Text = "1.12";

                if (Session["UserCompleteName"] != null)
                {

                    WelcomeLbl.Text = "Welcome " + Session["FirstName"].ToString();
                    string employeepic = "~/images/users/" + Session["EmployeeKey"].ToString() + ".jpg";

                    string EmpimgPath = GlobalClass.UserImagePath + Session["EmployeeKey"].ToString() + ".jpg";
                    if (!File.Exists(EmpimgPath)) { employeepic = "~/images/users/ID.jpg"; }

                    //MRPClass.PrintString(Session["EmployeeKey"].ToString());

                    ProfileImage.ImageUrl = employeepic;

                    //lblUser.InnerHtml = "User : " + Session["UserCompleteName"].ToString();
                    UserLbl.Text = Session["UserCompleteName"].ToString();

                    //ASPxLabelEnt.Text = "";
                    //if (Session["EntityCodeDesc"] != null)
                    //{
                    //    ASPxLabelEnt.Text = "Entity : " + Session["EntityCodeDesc"].ToString();
                    //}
                    if (Session["EntityCodeDesc"].ToString().Trim() != "")
                    {
                        //ASPxLabelEnt.Text = "Entity : " + Session["EntityCodeDesc"].ToString();
                        EntityLbl.Text = Session["EntityCodeDesc"].ToString();
                    }

                    //ASPxLabelBU.Text = "";
                    //if (Session["BUCodeDesc"] != null)
                    //{
                    //    ASPxLabelBU.Text = "BU / Dept : " + Session["BUCodeDesc"].ToString();
                    //}
                    if (Session["BUCodeDesc"].ToString().Trim() != "")
                    {
                        //ASPxLabelBU.Text = "BU / Dept : " + Session["BUCodeDesc"].ToString();


                    //ASPxSplitter1.Height = 661 - 10;


                        BULbl.Text = Session["BUCodeDesc"].ToString();
                    }

                    //ASPxSplitter1.Height = 661 - 10;

                    //ProfileImage.ImageUrl = "~/images/avatar.png";

                    Load_Menu(Convert.ToInt32(Session["CreatorKey"]));

                    //BindSideNavGrid(Convert.ToInt32(Session["CreatorKey"]));

                }
                else
                {
                    Response.Redirect("default.aspx");
                }


            }
        }

        protected void BtnSideNav_Init(object sender, EventArgs e)
        {
            //string page = "";

            //ASPxHyperLink link = sender as ASPxHyperLink;
            //GridViewDataItemTemplateContainer container = link.NamingContainer as GridViewDataItemTemplateContainer;
            //object page = container.Grid.GetRowValues(container.VisibleIndex, "formName");
            //link.NavigateUrl = page.ToString();
        }

        protected void ChangePW_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = sender as ASPxHyperLink;
            link.NavigateUrl = "change_password.aspx";
        }

        protected void ImageProfile_Click(object sender, ImageClickEventArgs e)
        {

        }

        private void BindSideNavGrid(int usrKey)
        {
            //SideNavGrid.DataSource = GlobalClass.SideNavigation(usrKey);
            //SideNavGrid.KeyFieldName = "PK";
            //SideNavGrid.SortBy(SideNavGrid.Columns[1], DevExpress.Data.ColumnSortOrder.Ascending);
            ////SideNavGrid.Columns[1].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending;
            //SideNavGrid.DataBind();
        }

        private void Load_Menu(int usrKey)
        {
            string qry = "";
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            DataTable dtable1 = new DataTable();

            using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnString()))
            {
                con.Open();
                qry = "SELECT tbl_System_SideNav.* FROM tbl_System_SideNav WHERE (ViewinNAV = 1)  ORDER BY Sort";
                cmd = new SqlCommand(qry);
                cmd.Connection = con;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    //var sb = new StringBuilder();
                    //sb.Append("<ul>");
                    MenuBar.Items.Clear();
                    foreach (DataRow row in dtable.Rows)
                    {
                        //if (Convert.ToInt32(Session["isAdmin"]) == 1)
                        if (GlobalClass.IsAdmin(usrKey) == true || GlobalClass.IsSuperAdmin(usrKey) == true)
                        {
                            if (GlobalClass.IsAllowed(usrKey, row["ModuleName"].ToString().Trim(), DateTime.Now))
                            {
                                MenuBar.Items.Add(new DevExpress.Web.MenuItem() { NavigateUrl = row["formURL"].ToString(), Text = row["formDescription"].ToString() });
                                //MenuBar.Items.Add(new DevExpress.Web.MenuItem() { NavigateUrl = "Default.aspx", Text = "Default" });
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(row["forAdminOnly"]) != 1)
                            {
                                if (GlobalClass.IsAllowed(usrKey, row["ModuleName"].ToString().Trim(), DateTime.Now))
                                {
                                    MenuBar.Items.Add(new DevExpress.Web.MenuItem() { NavigateUrl = row["formURL"].ToString(), Text = row["formDescription"].ToString() });
                                }
                            }
                        }
                    }
                    //sb.Append("</ul>");
                    //dvSideNav.InnerHtml = sb.ToString();
                }
                dtable.Clear();
                con.Close();
            }
        }

        protected void FloatCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            //string type = e.Parameter.Substring(0, e.Parameter.IndexOf("-"));
            //int PK = Convert.ToInt32(e.Parameter.Substring(e.Parameter.IndexOf("-") + 1, e.Parameter.Length - (e.Parameter.IndexOf("-") + 1)));

            dvMOPSidePanel.Visible = false;
            dvItemSidePanel.Visible = false;

            string param = e.Parameter;

            string[] arrParam = param.Split('^');
            string type = arrParam[0].ToString();

            //MRPClass.PrintString(type + " - " + PK.ToString() + " - " + sEntCode + " - " + sBuCode + " - " + arrParam[4].ToString()) ;

            string description = "Description";
            string descdata = "", sItemCode = "";

            //SqlCommand cmd = new SqlCommand();


            string query_1 = "";
            string query_2 = "";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            if (type == "MOPList")
            {
                dvMOPSidePanel.Visible = true;
                int mopKey = Convert.ToInt32(arrParam[1].ToString());
                string mopNum = "";
                query_1 = "SELECT tbl_MRP_List.* FROM tbl_MRP_List WHERE (PK = "+ mopKey + ")";
                cmd = new SqlCommand(query_1);
                cmd.Connection = conn;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        mopNum = row["DocNumber"].ToString();
                        MOPDucNumber.Text = row["DocNumber"].ToString();

                        DataTable dtRecord = MRPClass.POListDetails(mopNum);
                        grdMOPPOList.DataSource = dtRecord;
                        grdMOPPOList.DataBind();
                    }
                }
                dt.Clear();
                
            }
            else
            {
                dvItemSidePanel.Visible = true;

                int PK = Convert.ToInt32(arrParam[1]);
                string sEntCode = arrParam[2].ToString();
                string sBuCode = arrParam[3].ToString();

                List<object> fieldValues = new List<object>();

                

                

                //this is for comment section
                ArrayList loggersFirstName = new ArrayList();
                ArrayList loggersLastName = new ArrayList();
                ArrayList logsArr = new ArrayList();

                switch (type)
                {
                    case materialsIdentifier:
                        //ASPxGridView grid = DirectMaterialsGrid as ASPxGridView;
                        //ASPxTextBox txtid = (ASPxTextBox)ContentPlaceHolder1.FindControl("txtTest");

                        //ASPxGridView grid = (ASPxGridView)ContentPlaceHolder1.FindControl("DirectMaterialsGrid");

                        if (PK == 0)
                        {
                            sItemCode = arrParam[4].ToString();
                            query_1 = "SELECT ITEMID, NAMEALIAS " +
                                      " FROM dbo.vw_AXInventTable " +
                                      " WHERE(DATAAREAID = '" + sEntCode + "') " +
                                      " AND(ITEMID = '" + sItemCode + "')";
                            cmd = new SqlCommand(query_1);
                            cmd.Connection = conn;
                            adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    lblItemCode.Text = row["ITEMID"].ToString();
                                    lblDescription.Text = row["NAMEALIAS"].ToString();

                                    DataTable dtRecord = ItemInfoClass.Item_Invent_OnHand(sEntCode, lblItemCode.Text);
                                    grdOnHand.DataSource = dtRecord;
                                    grdOnHand.DataBind();

                                    DataTable dtRecord1 = ItemInfoClass.Item_Pending_PO(sEntCode, sBuCode, lblItemCode.Text);
                                    grdPendingPO.DataSource = dtRecord1;
                                    grdPendingPO.DataBind();

                                    DataTable dtRecord2 = ItemInfoClass.Item_Inventory_Movement(sEntCode, sBuCode, lblItemCode.Text);
                                    grdInventMovement.DataSource = dtRecord2;
                                    grdInventMovement.DataBind();
                                }
                            }
                            dt.Clear();

                        }
                        else
                        {
                            query_1 = "SELECT [ItemDescription], [ItemCode] FROM [dbo].[tbl_MRP_List_DirectMaterials] where [PK] = '" + PK + "'";
                            cmd = new SqlCommand(query_1);
                            cmd.Connection = conn;
                            adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    descdata = row["ItemDescription"].ToString();
                                    lblItemCode.Text = row["ItemCode"].ToString();
                                    lblDescription.Text = row["ItemDescription"].ToString();

                                    DataTable dtRecord = ItemInfoClass.Item_Invent_OnHand(sEntCode, lblItemCode.Text);
                                    grdOnHand.DataSource = dtRecord;
                                    grdOnHand.DataBind();

                                    DataTable dtRecord1 = ItemInfoClass.Item_Pending_PO(sEntCode, sBuCode, lblItemCode.Text);
                                    grdPendingPO.DataSource = dtRecord1;
                                    grdPendingPO.DataBind();

                                    DataTable dtRecord2 = ItemInfoClass.Item_Inventory_Movement(sEntCode, sBuCode, lblItemCode.Text);
                                    grdInventMovement.DataSource = dtRecord2;
                                    grdInventMovement.DataBind();
                                }
                            }
                            dt.Clear();
                            //cmd = new SqlCommand(query_1, conn);
                            //SqlDataReader reader = cmd.ExecuteReader();
                            //while (reader.Read())
                            //{
                            //    descdata = reader[0].ToString();
                            //    lblItemCode.Text = reader[1].ToString();
                            //    lblDescription.Text = reader[0].ToString();

                            //    DataTable dtRecord = ItemInfoClass.Item_Invent_OnHand(sEntCode, lblItemCode.Text);
                            //    grdOnHand.DataSource = dtRecord;
                            //    grdOnHand.DataBind();
                            //}
                            //reader.Close();
                        }

                        query_2 = "SELECT tbl_MRP_List_DirectMaterials_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM   tbl_MRP_List_DirectMaterials_Logs LEFT OUTER JOIN tbl_Users ON tbl_MRP_List_DirectMaterials_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";
                        cmd = new SqlCommand(query_2);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                logsArr.Add(row["Remarks"].ToString());
                                loggersFirstName.Add(row["Firstname"].ToString());
                                loggersLastName.Add(row["Lastname"].ToString());
                            }
                        }
                        dt.Clear();
                        //cmd = new SqlCommand(query_2, conn);
                        //SqlDataReader reader = cmd.ExecuteReader();
                        //reader = cmd.ExecuteReader();
                        //while (reader.Read())
                        //{
                        //    logsArr.Add(reader[0].ToString());
                        //    loggersFirstName.Add(reader[1].ToString());
                        //    loggersLastName.Add(reader[2].ToString());
                        //}
                        //reader.Close();

                        break;
                    case opexIdentifier:

                        //ASPxGridView grid1 = (ASPxGridView)ContentPlaceHolder1.FindControl("DirectMaterialsGrid");

                        if (PK == 0)
                        {
                            sItemCode = arrParam[4].ToString();
                            query_1 = "SELECT ITEMID, NAMEALIAS " +
                                      " FROM dbo.vw_AXInventTable " +
                                      " WHERE(DATAAREAID = '" + sEntCode + "') " +
                                      " AND(ITEMID = '" + sItemCode + "')";
                            cmd = new SqlCommand(query_1);
                            cmd.Connection = conn;
                            adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    lblItemCode.Text = row["ITEMID"].ToString();
                                    lblDescription.Text = row["NAMEALIAS"].ToString();

                                    DataTable dtRecord = ItemInfoClass.Item_Invent_OnHand(sEntCode, lblItemCode.Text);
                                    grdOnHand.DataSource = dtRecord;
                                    grdOnHand.DataBind();

                                    DataTable dtRecord1 = ItemInfoClass.Item_Pending_PO(sEntCode, sBuCode, lblItemCode.Text);
                                    grdPendingPO.DataSource = dtRecord1;
                                    grdPendingPO.DataBind();

                                    DataTable dtRecord2 = ItemInfoClass.Item_Inventory_Movement(sEntCode, sBuCode, lblItemCode.Text);
                                    grdInventMovement.DataSource = dtRecord2;
                                    grdInventMovement.DataBind();
                                }
                            }
                            dt.Clear();

                        }
                        else
                        {
                            query_1 = "SELECT [Description], [ItemCode] FROM [dbo].[tbl_MRP_List_OPEX] where [PK] = '" + PK + "'";
                            cmd = new SqlCommand(query_1);
                            cmd.Connection = conn;
                            adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    descdata = row["Description"].ToString();
                                    lblItemCode.Text = row["ItemCode"].ToString();
                                    lblDescription.Text = row["Description"].ToString();

                                    DataTable dtRecord = ItemInfoClass.Item_Invent_OnHand(sEntCode, lblItemCode.Text);
                                    grdOnHand.DataSource = dtRecord;
                                    grdOnHand.DataBind();

                                    DataTable dtRecord1 = ItemInfoClass.Item_Pending_PO(sEntCode, sBuCode, lblItemCode.Text);
                                    grdPendingPO.DataSource = dtRecord1;
                                    grdPendingPO.DataBind();

                                    DataTable dtRecord2 = ItemInfoClass.Item_Inventory_Movement(sEntCode, sBuCode, lblItemCode.Text);
                                    grdInventMovement.DataSource = dtRecord2;
                                    grdInventMovement.DataBind();
                                }
                            }
                            dt.Clear();
                        }

                        query_2 = "SELECT tbl_MRP_List_OPEX_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_MRP_List_OPEX_Logs LEFT OUTER JOIN tbl_Users ON tbl_MRP_List_OPEX_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";
                        cmd = new SqlCommand(query_2);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                logsArr.Add(row["Remarks"].ToString());
                                loggersFirstName.Add(row["Firstname"].ToString());
                                loggersLastName.Add(row["Lastname"].ToString());
                            }
                        }
                        dt.Clear();

                        break;
                    case manpowerIdentifier:
                        query_1 = "SELECT [Description] FROM [dbo].[tbl_MRP_List_ManPower] where [PK] = '" + PK + "'";
                        cmd = new SqlCommand(query_1);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                descdata = row["Description"].ToString();
                                lblItemCode.Text = ""; //reader[1].ToString();
                                lblDescription.Text = row["Description"].ToString();
                            }
                        }
                        dt.Clear();

                        query_2 = "SELECT tbl_MRP_List_ManPower_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_MRP_List_ManPower_Logs LEFT OUTER JOIN tbl_Users ON tbl_MRP_List_ManPower_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";
                        cmd = new SqlCommand(query_2);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                logsArr.Add(row["Remarks"].ToString());
                                loggersFirstName.Add(row["Firstname"].ToString());
                                loggersLastName.Add(row["Lastname"].ToString());
                            }
                        }
                        dt.Clear();

                        break;
                    case capexIdentifier:
                        query_1 = "SELECT [Description] FROM [dbo].[tbl_MRP_List_CAPEX] where [PK] = '" + PK + "'";
                        cmd = new SqlCommand(query_1);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                descdata = row["Description"].ToString();
                                lblItemCode.Text = ""; //reader[1].ToString();
                                lblDescription.Text = row["Description"].ToString();
                            }
                        }
                        dt.Clear();

                        query_2 = "SELECT tbl_MRP_List_CAPEX_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_MRP_List_CAPEX_Logs LEFT OUTER JOIN tbl_Users ON tbl_MRP_List_CAPEX_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";
                        cmd = new SqlCommand(query_2);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                logsArr.Add(row["Remarks"].ToString());
                                loggersFirstName.Add(row["Firstname"].ToString());
                                loggersLastName.Add(row["Lastname"].ToString());
                            }
                        }
                        dt.Clear();

                        break;
                    case revenueIdentifier:
                        query_1 = "SELECT [FarmName] FROM [dbo].[tbl_MRP_List_RevenueAssumptions] where [PK] = '" + PK + "'";
                        cmd = new SqlCommand(query_1);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                descdata = row["FarmName"].ToString();
                                lblItemCode.Text = ""; //reader[1].ToString();
                                lblDescription.Text = row["FarmName"].ToString();
                            }
                        }
                        dt.Clear();

                        query_2 = "SELECT tbl_MRP_List_RevenueAssumptions_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_Users LEFT OUTER JOIN tbl_MRP_List_RevenueAssumptions_Logs ON tbl_Users.PK = tbl_MRP_List_RevenueAssumptions_Logs.UserKey WHERE MasterKey = '" + PK + "'";
                        cmd = new SqlCommand(query_2);
                        cmd.Connection = conn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                logsArr.Add(row["Remarks"].ToString());
                                loggersFirstName.Add(row["Firstname"].ToString());
                                loggersLastName.Add(row["Lastname"].ToString());
                            }
                        }
                        dt.Clear();

                        break;
                }

                for (int i = 0; i < loggersFirstName.Count; i++)
                {
                    string fname = EncryptionClass.Decrypt(loggersFirstName[i].ToString());
                    loggersFirstName[i] = fname;
                    string lname = EncryptionClass.Decrypt(loggersLastName[i].ToString());
                    loggersLastName[i] = lname;
                }                
            }
            conn.Close();

        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session["CreatorKey"] = null;
            Response.Redirect("default.aspx");
        }
    }
}