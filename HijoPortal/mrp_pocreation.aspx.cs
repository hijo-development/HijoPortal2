using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HijoPortal.classes;
using System.Text;

namespace HijoPortal
{
    public partial class mrp_pocreation : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCreatorKey();
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            if (!Page.IsPostBack)
            {
            }
            BindPO();
        }

        private void BindPO()
        {
            DataTable dtRecord = MRPClass.PO_Table();
            POTable.DataSource = dtRecord;
            POTable.KeyFieldName = "PK";
            POTable.DataBind();
        }

        protected void POTable_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (Session["CreatorKey"] == null)
            {
                Response.RedirectLocation = "default.aspx";
                return;
            }
            ASPxHiddenField text = POTable.FindHeaderTemplateControl(POTable.Columns[0], "HiddenVal") as ASPxHiddenField;
            text["hidden_value"] = "";

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            string PK = POTable.GetRowValues(POTable.FocusedRowIndex, "PK").ToString();
            string query = "SELECT COUNT(*) FROM " + MRPClass.POTableName() + " WHERE CreatorKey = '" + Session["CreatorKey"].ToString() + "' AND PK = '" + PK + "'";

            SqlCommand comm = new SqlCommand(query, conn);
            int result = Convert.ToInt32(comm.ExecuteScalar());

            if (result > 0)
            {
                if (e.ButtonID == "Edit")
                {
                    if (POTable.FocusedRowIndex > -1)
                    {
                        Session["PO_PK"] = PK;
                        string docNum = POTable.GetRowValues(POTable.FocusedRowIndex, "MRPNumber").ToString();
                        string poNumber = POTable.GetRowValues(POTable.FocusedRowIndex, "PONumber").ToString();
                        Session["MRP_Number"] = docNum;

                        //query if po number already PO

                        //string query_po = "SELECT COUNT(*) FROM[hijo_portal].[dbo].[tbl_POCreation_Details] WHERE PONumber = '" + poNumber + "'";
                        //SqlCommand cmd = new SqlCommand(query_po, conn);
                        //int result_po = Convert.ToInt32(cmd.ExecuteScalar());
                        //MRPClass.PrintString(":" + result_po);
                        //if (result_po > 0)
                        //    text["hidden_value"] = "AlreadyPO";
                        //else
                        Response.RedirectLocation = "mrp_pocreatededit.aspx?PONum=" + poNumber.ToString();

//                         string query_po = "SELECT COUNT(*) FROM[hijo_portal].[dbo].[tbl_POCreation_Details] WHERE PONumber = '" + poNumber + "'";
//                         SqlCommand cmd = new SqlCommand(query_po, conn);
//                         int result_po = Convert.ToInt32(cmd.ExecuteScalar());
//                         //MRPClass.PrintString(":" + result_po);
//                         if (result_po > 0)
//                             text["hidden_value"] = "AlreadyPO";
//                         else
//                             Response.RedirectLocation = "mrp_poaddedit.aspx?DocNum=" + docNum.ToString();
                    }
                }

                if (e.ButtonID == "Delete")
                {
                    if (POTable.FocusedRowIndex > -1)
                    {
                        string delete = "DELETE FROM " + MRPClass.POTableName() + " WHERE [PK] ='" + PK + "'";
                        SqlCommand cmd = new SqlCommand(delete, conn);
                        cmd.ExecuteNonQuery();
                        BindPO();
                    }
                }
            }
            else
            {
                text["hidden_value"] = "InvalidCreator";
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
            //PopUpControl.ShowOnPageLoad = true;
            //MRPClass.PrintString("triall...");
            //CheckCreatorKey();
            Session["MRP_Number"] = null;
            Response.Redirect("mrp_poaddedit.aspx");

        }

        protected void MRPNumber_Init(object sender, EventArgs e)
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
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            //CheckCreatorKey();
            //SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            //conn.Open();

            ////Declare Variables
            //string DocPref = "", strDocNum = "";
            //int DocNum = 0;

            //string query = "SELECT [DocumentPrefix],[DocumentNum] FROM " + MRPClass.DocNumberTableName() + " where DocumentPrefix = 'PO'";

            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    DocPref = reader[0].ToString();
            //    DocNum = Convert.ToInt32(reader[1].ToString());
            //}
            //reader.Close();

            //DocNum += 1;
            //strDocNum = DocNum.ToString("00000000#");
            //string PONumber = DocPref + "-" + Session["EntityCode"].ToString() + "-" + strDocNum;
            //string MRPnumber = MRPNumber.Value.ToString();

            //string insert = "INSERT INTO " + MRPClass.POTableName() + " ([MRPNumber],[PONumber],[CreatorKey]) VALUES (@MRPNumber, @PONumber, @CreatorKey)";
            //cmd = new SqlCommand(insert, conn);
            //cmd.Parameters.AddWithValue("MRPNumber", MRPnumber);
            //cmd.Parameters.AddWithValue("PONumber", PONumber);
            //cmd.Parameters.AddWithValue("CreatorKey", Session["CreatorKey"].ToString());
            //cmd.CommandType = CommandType.Text;
            //int result = cmd.ExecuteNonQuery();

            //if (result > 0)
            //{
            //    string update = "UPDATE " + MRPClass.DocNumberTableName() + " SET [DocumentNum] = '" + DocNum + "' WHERE [DocumentPrefix] = 'PO'";

            //    cmd = new SqlCommand(update, conn);
            //    cmd.ExecuteNonQuery();
            //}

            //PopUpControl.ShowOnPageLoad = false;
            //Response.RedirectLocation = "mrp_poaddedit.aspx?DocNum=" + MRPnumber;

            //conn.Close();
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