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
    public partial class po_uploading : System.Web.UI.Page
    {
        private static bool bindGrid = true, bindInfo = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            BindGrid();

            if (bindInfo)
                BindInfo();
            else
                bindInfo = true;
        }

        private void BindGrid()
        {
            POGrid.DataSource = POClass.PO_Uploading_Table();
            POGrid.KeyFieldName = "PK";
            POGrid.DataBind();
        }

        private void BindInfo()
        {
            InfoGrid.DataSource = POClass.PO_Info_Table();
            InfoGrid.KeyFieldName = "PK";
            InfoGrid.DataBind();
        }

        protected void Pword_Init(object sender, EventArgs e)
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.Password = true;
            text.Height = 10;
        }

        protected void Code_Init(object sender, EventArgs e)
        {
            ASPxComboBox combo = sender as ASPxComboBox;
            combo.DataSource = POClass.PO_EntityCode_Table();

            ListBoxColumn lv = new ListBoxColumn();
            lv.FieldName = "ID";
            lv.Caption = "Code";
            lv.Width = 50;
            combo.Columns.Add(lv);

            ListBoxColumn lt = new ListBoxColumn();
            lt.FieldName = "NAME";
            lt.Caption = "Entity";
            lt.Width = 250;
            combo.Columns.Add(lt);

            combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            combo.ValueField = "ID";
            combo.TextField = "NAME";
            combo.DataBind();

            GridViewEditItemTemplateContainer container = ((ASPxComboBox)sender).NamingContainer as GridViewEditItemTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "Code").ToString();
                combo.Text = DataBinder.Eval(container.DataItem, "Code").ToString();
            }
        }

        protected void InfoGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindInfo = false;
        }

        protected void InfoGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox code = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Code"], "Code") as ASPxComboBox;
            ASPxTextBox prefix = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Prefix"], "Prefix") as ASPxTextBox;
            ASPxTextBox series = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["BeforeSeries"], "BeforeSeries") as ASPxTextBox;
            ASPxTextBox max = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["MaxNumber"], "MaxNumber") as ASPxTextBox;
            ASPxTextBox last = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["LastNumber"], "LastNumber") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string update = "UPDATE [dbo].[tbl_PONumber] SET [EntityCode] = @EntityCode, [Prefix] = @Prefix, [BeforeSeries] = @BeforeSeries, [MaxNumber] = @MaxNumber, [LastNumber] = @LastNumber WHERE [PK] = @PK";

            double max_number = Convert.ToDouble(max.Text);
            double last_number = Convert.ToDouble(last.Text);

            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@EntityCode", code.Text);
            cmd.Parameters.AddWithValue("@Prefix", prefix.Text);
            cmd.Parameters.AddWithValue("@BeforeSeries", series.Text);
            cmd.Parameters.AddWithValue("@MaxNumber", max_number);
            cmd.Parameters.AddWithValue("@LastNumber", last_number);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
            grid.CancelEdit();
            e.Cancel = true;
            BindInfo();
        }

        protected void InfoGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();

            string delete = "DELETE FROM [dbo].[tbl_PONumber] WHERE PK = '" + PK + "'";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            grid.CancelEdit();
            e.Cancel = true;
            BindInfo();
        }

        protected void InfoGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindInfo = false;
        }

        protected void InfoGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox code = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Code"], "Code") as ASPxComboBox;
            ASPxTextBox prefix = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Prefix"], "Prefix") as ASPxTextBox;
            ASPxTextBox series = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["BeforeSeries"], "BeforeSeries") as ASPxTextBox;
            ASPxTextBox max = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["MaxNumber"], "MaxNumber") as ASPxTextBox;
            ASPxTextBox last = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["LastNumber"], "LastNumber") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string insert = "INSERT INTO [dbo].[tbl_PONumber] ([EntityCode], [Prefix], [BeforeSeries], [MaxNumber], [LastNumber]) VALUES (@EntityCode, @Prefix, @BeforeSeries, @MaxNumber, @LastNumber)";

            double max_number = Convert.ToDouble(max.Text);
            double last_number = Convert.ToDouble(last.Text);

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@EntityCode", code.Text);
            cmd.Parameters.AddWithValue("@Prefix", prefix.Text);
            cmd.Parameters.AddWithValue("@BeforeSeries", series.Text);
            cmd.Parameters.AddWithValue("@MaxNumber", max_number);
            cmd.Parameters.AddWithValue("@LastNumber", last_number);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
            grid.CancelEdit();
            e.Cancel = true;
            BindInfo();
        }

        protected void InfoGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            DesignBehavior.SetBehaviorGrid(grid);
        }
    }
}