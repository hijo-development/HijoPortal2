using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using HijoPortal.classes;

namespace HijoPortal
{
    public partial class mrp_po_list : System.Web.UI.Page
    {

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
            string ponumber = grid.GetRowValues(grid.FocusedRowIndex, "PONumber").ToString();
            switch (btnID)
            {
                case "Edit":
                    ASPxWebControl.RedirectOnCallback("mrp_po_addedit.aspx?PONum=" + ponumber);
                    //Response.RedirectLocation = "mrp_po_addedit.aspx?PONum="+ponumber;
                    break;

                case "Delete":

                    break;
            }
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            MRPClass.PrintString("ok click");
            string ponumber = gridCreatedPO.GetRowValues(gridCreatedPO.FocusedRowIndex, "PONumber").ToString();

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
    }
}