using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            if (!Page.IsPostBack)
            {
                if (Session["UserCompleteName"] != null)
                {
                    lblUser.InnerHtml = "User : " + Session["UserCompleteName"].ToString();

                    ASPxLabelEnt.Text = "";
                    //if (Session["EntityCodeDesc"] != null)
                    //{
                    //    ASPxLabelEnt.Text = "Entity : " + Session["EntityCodeDesc"].ToString();
                    //}
                    if (Session["EntityCodeDesc"].ToString().Trim() != "")
                    {
                        ASPxLabelEnt.Text = "Entity : " + Session["EntityCodeDesc"].ToString();
                    }

                    ASPxLabelBU.Text = "";
                    //if (Session["BUCodeDesc"] != null)
                    //{
                    //    ASPxLabelBU.Text = "BU / Dept : " + Session["BUCodeDesc"].ToString();
                    //}
                    if (Session["BUCodeDesc"].ToString().Trim() != "")
                    {
                        ASPxLabelBU.Text = "BU / Dept : " + Session["BUCodeDesc"].ToString();
                    }

                    //ASPxSplitter1.Height = 661 - 10;

                    SqlCommand cmd = null;
                    SqlDataAdapter adp;
                    DataTable dtable = new DataTable();

                    using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnString()))
                    {
                        con.Open();
                        string qry = "SELECT tbl_System_SideNav.* " +
                                     " FROM tbl_System_SideNav " +
                                     " ORDER BY Sort";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = con;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count>0)
                        {
                            var sb = new StringBuilder();
                            sb.Append("<ul>");
                            foreach (DataRow row in dtable.Rows)
                            {
                                if (Convert.ToInt32(Session["isAdmin"]) == 1)
                                {
                                    sb.Append("<li>");
                                    sb.Append(row["MenuScript"].ToString());
                                    sb.Append("</li>");
                                } else
                                {
                                    if (Convert.ToInt32(row["forAdminOnly"]) != 1)
                                    {
                                        sb.Append("<li>");
                                        sb.Append(row["MenuScript"].ToString());
                                        sb.Append("</li>");
                                        
                                    }
                                }
                            }
                            sb.Append("</ul>");
                            System.Diagnostics.Debug.Write(sb.ToString());
                            dvSideNav.InnerHtml = sb.ToString();
                        }
                        dtable.Clear();
                        con.Close();
                    }

                }
                else
                {
                    Response.Redirect("default.aspx");
                }

            }
        }

        protected void FloatCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            string type = e.Parameter.Substring(0, e.Parameter.IndexOf("-"));
            int PK = Convert.ToInt32(e.Parameter.Substring(e.Parameter.IndexOf("-") + 1, e.Parameter.Length - (e.Parameter.IndexOf("-") + 1)));
            List<object> fieldValues = new List<object>();

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string description = "Description";
            string descdata = "";

            SqlCommand cmd = new SqlCommand();

            string query_1 = "";
            string query_2 = "";

            //this is for comment section
            ArrayList loggersFirstName = new ArrayList();
            ArrayList loggersLastName = new ArrayList();
            ArrayList logsArr = new ArrayList();

            switch (type)
            {
                case materialsIdentifier:
                    //ASPxGridView grid = DirectMaterialsGrid as ASPxGridView;
                    //ASPxTextBox txtid = (ASPxTextBox)ContentPlaceHolder1.FindControl("txtTest");

                    ASPxGridView grid = (ASPxGridView)ContentPlaceHolder1.FindControl("DirectMaterialsGrid");

                    query_1 = "SELECT [ItemDescription] FROM [hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials] where [PK] = '" + PK + "'";
                    cmd = new SqlCommand(query_1, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        descdata = reader[0].ToString();
                    }

                    reader.Close();


                    query_2 = "SELECT tbl_MRP_List_DirectMaterials_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM   tbl_MRP_List_DirectMaterials_Logs INNER JOIN tbl_Users ON tbl_MRP_List_DirectMaterials_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";

                    cmd = new SqlCommand(query_2, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        logsArr.Add(reader[0].ToString());
                        loggersFirstName.Add(reader[1].ToString());
                        loggersLastName.Add(reader[2].ToString());
                    }
                    reader.Close();

                    break;
                case opexIdentifier:
                    query_1 = "SELECT [Description] FROM [hijo_portal].[dbo].[tbl_MRP_List_OPEX] where [PK] = '" + PK + "'";
                    cmd = new SqlCommand(query_1, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        descdata = reader[0].ToString();
                    }

                    reader.Close();

                    query_2 = "SELECT tbl_MRP_List_OPEX_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_MRP_List_OPEX_Logs INNER JOIN tbl_Users ON tbl_MRP_List_OPEX_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";

                    cmd = new SqlCommand(query_2, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        logsArr.Add(reader[0].ToString());
                        loggersFirstName.Add(reader[1].ToString());
                        loggersLastName.Add(reader[2].ToString());
                    }
                    reader.Close();

                    break;
                case manpowerIdentifier:
                    query_1 = "SELECT [Description] FROM [hijo_portal].[dbo].[tbl_MRP_List_ManPower] where [PK] = '" + PK + "'";
                    cmd = new SqlCommand(query_1, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        descdata = reader[0].ToString();
                    }

                    reader.Close();

                    query_2 = "SELECT tbl_MRP_List_ManPower_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_MRP_List_ManPower_Logs INNER JOIN tbl_Users ON tbl_MRP_List_ManPower_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";

                    cmd = new SqlCommand(query_2, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        logsArr.Add(reader[0].ToString());
                        loggersFirstName.Add(reader[1].ToString());
                        loggersLastName.Add(reader[2].ToString());
                    }
                    reader.Close();

                    break;
                case capexIdentifier:
                    query_1 = "SELECT [Description] FROM [hijo_portal].[dbo].[tbl_MRP_List_CAPEX] where [PK] = '" + PK + "'";
                    cmd = new SqlCommand(query_1, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        descdata = reader[0].ToString();
                    }

                    reader.Close();

                    query_2 = "SELECT tbl_MRP_List_CAPEX_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_MRP_List_CAPEX_Logs INNER JOIN tbl_Users ON tbl_MRP_List_CAPEX_Logs.UserKey = tbl_Users.PK WHERE MasterKey = '" + PK + "'";

                    cmd = new SqlCommand(query_2, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        logsArr.Add(reader[0].ToString());
                        loggersFirstName.Add(reader[1].ToString());
                        loggersLastName.Add(reader[2].ToString());
                    }
                    reader.Close();

                    break;
                case revenueIdentifier:
                    query_1 = "SELECT [FarmName] FROM [hijo_portal].[dbo].[tbl_MRP_List_RevenueAssumptions] where [PK] = '" + PK + "'";
                    cmd = new SqlCommand(query_1, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        descdata = reader[0].ToString();
                    }

                    reader.Close();

                    query_2 = "SELECT tbl_MRP_List_RevenueAssumptions_Logs.Remarks, tbl_Users.Firstname, tbl_Users.Lastname FROM tbl_Users INNER JOIN tbl_MRP_List_RevenueAssumptions_Logs ON tbl_Users.PK = tbl_MRP_List_RevenueAssumptions_Logs.UserKey WHERE MasterKey = '" + PK + "'";

                    cmd = new SqlCommand(query_2, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        logsArr.Add(reader[0].ToString());
                        loggersFirstName.Add(reader[1].ToString());
                        loggersLastName.Add(reader[2].ToString());
                    }
                    reader.Close();

                    break;
            }

            for (int i = 0; i < loggersFirstName.Count; i++)
            {
                string fname = EncryptionClass.Decrypt(loggersFirstName[i].ToString());
                loggersFirstName[i] = fname;
                string lname = EncryptionClass.Decrypt(loggersLastName[i].ToString());
                loggersLastName[i] = lname;
            }

            for (int i = 0; i < 1; i++)//table row forloop
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell td = new HtmlTableCell();
                for (int i2 = 0; i2 < 2; i2++)//table data forloop
                {
                    td = new HtmlTableCell();
                    if (i2 == 0)//first column
                    {
                        td.InnerText = description;
                        td.Attributes.Add("style", "width:25%");
                        tr.Cells.Add(td);
                    }
                    else//second column
                    {
                        td.InnerText = descdata;
                        td.Attributes.Add("style", "width:75%");
                        td.Attributes.Add("style", "padding:0px 5px 0px 0px");
                        tr.Cells.Add(td);
                    }
                }
                warehousetable.Controls.Add(tr);
            }


            for (int i = 0; i < logsArr.Count; i++)//table row forloop
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell td = new HtmlTableCell();
                for (int i2 = 0; i2 < 2; i2++)//table data forloop
                {
                    td = new HtmlTableCell();
                    if (i2 == 0)//first column
                    {

                        td.InnerText = loggersFirstName[i].ToString() + " " + loggersLastName[i].ToString();
                        td.Attributes.Add("style", "width:25%;");
                        td.ColSpan = 2;
                        tr.Cells.Add(td);
                        commentstable.Controls.Add(tr);
                    }
                    else
                    {
                        tr = new HtmlTableRow();
                        td = new HtmlTableCell();
                        td.Style.Add("width", "5%");
                        tr.Cells.Add(td);

                        td = new HtmlTableCell();
                        td.InnerText = logsArr[i].ToString();
                        td.Attributes.Add("style", "width:75%");
                        td.Attributes.Add("style", "padding:0px 5px 0px 0px");
                        tr.Cells.Add(td);

                    }
                }
                commentstable.Controls.Add(tr);
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