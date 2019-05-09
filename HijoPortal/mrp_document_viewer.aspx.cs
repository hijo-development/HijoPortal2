using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using HijoPortal.classes;

namespace HijoPortal
{
    public partial class mrp_document_viewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);
                BindXtraReport();
            }
        }

        private void BindXtraReport()
        {
            DocumentReport obj_Rpt = new DocumentReport();
            string connectionString = "SERVER=hijo-axdb;DATABASE=hijo_portal;UID=it_dev;PASSWORD=itdev@2019;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.ItemCode, dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.ItemDescriptionAddl, dbo.tbl_MRP_List_DirectMaterials.Qty, dbo.tbl_MRP_List_DirectMaterials.QtyPO, dbo.tbl_MRP_List_DirectMaterials.AvailForPO, dbo.tbl_POCreation_Details.ItemPK, dbo.tbl_POCreation_Details.PK FROM   dbo.tbl_MRP_List_DirectMaterials LEFT OUTER JOIN dbo.tbl_POCreation_Details ON dbo.tbl_MRP_List_DirectMaterials.PK = dbo.tbl_POCreation_Details.ItemPK WHERE(dbo.tbl_POCreation_Details.MOPNumber = '" + "0000-0319MRP-000064" + "') AND (dbo.tbl_POCreation_Details.Identifier = '1')";

            //SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            //DataSet DS = new DataSet();
            //adapter.Fill(DS);
            //adapter.Dispose();
            //obj_Rpt.Report.DataSource = DS;

            DataTable dt = DirectMaterials("0000-0319MRP-000064");
            obj_Rpt.Report.DataSource = dt;
            //obj_Rpt.Report.DataMember = DS.Tables[0].TableName;
            obj_Rpt.Report.DataMember = dt.TableName;

            obj_Rpt.Col1.DataBindings.Add("Text", null, "ItemCode");
            obj_Rpt.Col2.DataBindings.Add("Text", null, "Descripiton");
            obj_Rpt.Col3.DataBindings.Add("Text", null, "Qty");
            obj_Rpt.Col4.DataBindings.Add("Text", null, "POQty");
            obj_Rpt.Col5.DataBindings.Add("Text", null, "RemainingQty");

            obj_Rpt.DisplayName = "DocumentReport " + DateTime.Now;
            
            ASPxWebDocumentViewer1.OpenReport(obj_Rpt);
        }

        public static DataTable DirectMaterials(string docnumber)
        {
            DataTable dtTable = new DataTable();
            dtTable.TableName = "Table";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("ItemCode", typeof(string));
                dtTable.Columns.Add("Descripiton", typeof(string));
                dtTable.Columns.Add("Qty", typeof(string));
                dtTable.Columns.Add("POQty", typeof(string));
                dtTable.Columns.Add("RemainingQty", typeof(string));
            }

            string qry = "SELECT DISTINCT dbo.tbl_MRP_List_DirectMaterials.ItemCode, dbo.tbl_MRP_List_DirectMaterials.ItemDescription, dbo.tbl_MRP_List_DirectMaterials.ItemDescriptionAddl, dbo.tbl_MRP_List_DirectMaterials.Qty, dbo.tbl_MRP_List_DirectMaterials.QtyPO, dbo.tbl_MRP_List_DirectMaterials.AvailForPO, dbo.tbl_POCreation_Details.ItemPK, dbo.tbl_POCreation_Details.PK FROM   dbo.tbl_MRP_List_DirectMaterials LEFT OUTER JOIN dbo.tbl_POCreation_Details ON dbo.tbl_MRP_List_DirectMaterials.PK = dbo.tbl_POCreation_Details.ItemPK WHERE(dbo.tbl_POCreation_Details.MOPNumber = '" + docnumber + "') AND (dbo.tbl_POCreation_Details.Identifier = '1')";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["ItemCode"] = row["ItemCode"].ToString();

                    string desc = row["ItemDescriptionAddl"].ToString();
                    if (string.IsNullOrEmpty(desc))
                        dtRow["Descripiton"] = row["ItemDescription"].ToString();
                    else
                        dtRow["Descripiton"] = row["ItemDescription"].ToString() + "(" + desc + ")";

                    dtRow["Qty"] = Convert.ToDouble(row["Qty"].ToString()).ToString("N");
                    dtRow["POQty"] = Convert.ToDouble(row["QtyPO"].ToString()).ToString("N");
                    dtRow["RemainingQty"] = Convert.ToDouble(row["AvailForPO"].ToString()).ToString("N");
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }
    }
}