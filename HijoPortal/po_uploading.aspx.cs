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
using System.Configuration.Provider;

namespace HijoPortal
{
    public partial class po_uploading : System.Web.UI.Page
    {
        private static bool bindGrid = true, bindInfo = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "Resize", "changeWidth.resizeWidth();", true);

            if (bindGrid)
                BindGrid();
            else
                bindGrid = true;

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

            //GridViewEditFormTemplateContainer container = text.NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            //if (!container.Grid.IsNewRowEditing)
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
            string[] arr = code.Text.ToString().Split(';');
            string entity_string = arr[0];

            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@EntityCode", entity_string);
            cmd.Parameters.AddWithValue("@Prefix", prefix.Text);
            cmd.Parameters.AddWithValue("@BeforeSeries", series.Text);
            cmd.Parameters.AddWithValue("@MaxNumber", max_number);
            cmd.Parameters.AddWithValue("@LastNumber", last_number);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.CommandType = CommandType.Text;
            MRPClass.PrintString(code.Text);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException exc)
            {
                MRPClass.PrintString(exc.Message);
            }


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
            ASPxHiddenField errorvalue = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Code"], "ErrorHiddenValue") as ASPxHiddenField;
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

        protected void Entity_Init(object sender, EventArgs e)
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

            GridViewEditFormTemplateContainer container = ((ASPxComboBox)sender).NamingContainer.NamingContainer as GridViewEditFormTemplateContainer;
            if (!container.Grid.IsNewRowEditing)
            {
                combo.Value = DataBinder.Eval(container.DataItem, "EntityCode").ToString();
                combo.Text = DataBinder.Eval(container.DataItem, "EntityCode").ToString();
            }
        }

        protected void POGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            bindGrid = false;
        }

        protected void POGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("GridPageControl") as ASPxPageControl;

            ASPxComboBox entity = pageControl.FindControl("EntityCode") as ASPxComboBox;
            ASPxTextBox entityname = pageControl.FindControl("EntityName") as ASPxTextBox;
            ASPxTextBox header = pageControl.FindControl("HeaderPath") as ASPxTextBox;
            ASPxTextBox line = pageControl.FindControl("LinePath") as ASPxTextBox;
            ASPxTextBox domain = pageControl.FindControl("Domain") as ASPxTextBox;
            ASPxTextBox uname = pageControl.FindControl("Uname") as ASPxTextBox;
            ASPxTextBox Pword = pageControl.FindControl("Pword") as ASPxTextBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string encrypted_password = EncryptionClass.Encrypt(Pword.Text);

            string insert = "INSERT INTO [dbo].[tbl_AXPOUploadingPath] ([Entity], [Entity Name], [POHeaderPath], [POLinePath], [Domain], [UserName], [Password]) VALUES (@Entity, @EntityName, @POHeaderPath, @POLinePath, @Domain, @UserName, @Password)";

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@Entity", entity.Text);
            cmd.Parameters.AddWithValue("@EntityName", entityname.Text);
            cmd.Parameters.AddWithValue("@POHeaderPath", header.Text);
            cmd.Parameters.AddWithValue("@POLinePath", line.Text);
            cmd.Parameters.AddWithValue("@Domain", domain.Text);
            cmd.Parameters.AddWithValue("@UserName", uname.Text);
            cmd.Parameters.AddWithValue("@Password", encrypted_password);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            conn.Close();
            grid.CancelEdit();
            e.Cancel = true;
            BindGrid();
        }

        protected void POGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bindGrid = false;

        }

        protected void POGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("GridPageControl") as ASPxPageControl;

            ASPxComboBox entity = pageControl.FindControl("EntityCode") as ASPxComboBox;
            ASPxTextBox entityname = pageControl.FindControl("EntityName") as ASPxTextBox;
            ASPxTextBox header = pageControl.FindControl("HeaderPath") as ASPxTextBox;
            ASPxTextBox line = pageControl.FindControl("LinePath") as ASPxTextBox;
            ASPxTextBox domain = pageControl.FindControl("Domain") as ASPxTextBox;
            ASPxTextBox uname = pageControl.FindControl("Uname") as ASPxTextBox;
            ASPxTextBox Pword = pageControl.FindControl("Pword") as ASPxTextBox;
            ASPxCheckBox checkbox = pageControl.FindControl("AllowPassword") as ASPxCheckBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = null;

            string PK = e.Keys[0].ToString();
            string update = "";

            string[] arr = entity.Text.ToString().Split(';');
            string entity_string = arr[0];

            if (checkbox.Checked)
            {
                string encrypted_password = EncryptionClass.Encrypt(Pword.Text);

                update = "UPDATE [dbo].[tbl_AXPOUploadingPath] SET [Entity] = @Entity, [Entity Name] = @EntityName, [POHeaderPath] = @POHeaderPath, [POLinePath] = @POLinePath, [Domain] = @Domain, [UserName] = @UserName, [Password] = @Password WHERE [PK] = @PK";
                cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Entity", entity_string);
                cmd.Parameters.AddWithValue("@EntityName", entityname.Text);
                cmd.Parameters.AddWithValue("@POHeaderPath", header.Text);
                cmd.Parameters.AddWithValue("@POLinePath", line.Text);
                cmd.Parameters.AddWithValue("@Domain", domain.Text);
                cmd.Parameters.AddWithValue("@UserName", uname.Text);
                cmd.Parameters.AddWithValue("@Password", encrypted_password);
                cmd.Parameters.AddWithValue("@PK", PK);
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                update = "UPDATE [dbo].[tbl_AXPOUploadingPath] SET [Entity] = @Entity, [Entity Name] = @EntityName, [POHeaderPath] = @POHeaderPath, [POLinePath] = @POLinePath, [Domain] = @Domain, [UserName] = @UserName WHERE [PK] = @PK";
                cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Entity", entity_string);
                cmd.Parameters.AddWithValue("@EntityName", entityname.Text);
                cmd.Parameters.AddWithValue("@POHeaderPath", header.Text);
                cmd.Parameters.AddWithValue("@POLinePath", line.Text);
                cmd.Parameters.AddWithValue("@Domain", domain.Text);
                cmd.Parameters.AddWithValue("@UserName", uname.Text);
                cmd.Parameters.AddWithValue("@PK", PK);
                cmd.CommandType = CommandType.Text;
            }
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MRPClass.PrintString(exc.Message);
            }
            conn.Close();
            grid.CancelEdit();
            e.Cancel = true;
            BindGrid();
        }

        protected void POGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();

            string PK = e.Keys[0].ToString();
            string delete = "DELETE FROM [dbo].[tbl_AXPOUploadingPath] WHERE [PK] = '" + PK + "'";

            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            grid.CancelEdit();
            e.Cancel = true;
            BindGrid();
        }

        protected void POGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            DesignBehavior.SetBehaviorGrid(grid);

            if (grid.IsEditing)
            {
                if (!grid.IsNewRowEditing)
                {
                    ASPxPageControl pageControl = POGrid.FindEditFormTemplateControl("GridPageControl") as ASPxPageControl;
                    ASPxCheckBox checkbox = pageControl.FindControl("AllowPassword") as ASPxCheckBox;
                    ASPxLabel lbl = pageControl.FindControl("AllowLbl") as ASPxLabel;
                    //ASPxTextBox pword = pageControl.FindControl("Pword") as ASPxTextBox;

                    checkbox.Checked = true;
                    checkbox.ClientVisible = true;
                    lbl.ClientVisible = true;

                }
            }
        }

        protected void InfoGrid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxComboBox code = grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns["Code"], "Code") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            string[] arr = code.Text.ToString().Split(';');
            string entity_string = arr[0];

            if (grid.IsNewRowEditing)
            {

                string check = "SELECT COUNT(*) FROM [dbo].[tbl_PONumber] WHERE [EntityCode] = '" + entity_string + "'";

                cmd = new SqlCommand(check, conn);
                Int32 result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result > 0)
                {
                    e.RowError = "Entity Already Exist";
                }

            }
            else
            {
                string PK = e.Keys[0].ToString();

                string check = "SELECT [EntityCode] FROM [dbo].[tbl_PONumber] WHERE [EntityCode] = '" + entity_string + "' EXCEPT(SELECT[EntityCode] FROM[dbo].[tbl_PONumber] WHERE[PK] = '" + PK + "')";

                cmd = new SqlCommand(check, conn);
                reader = cmd.ExecuteReader();
                bool result = reader.Read();
                if (result)
                {
                    e.RowError = "Entity Already Exist";
                }
            }
            conn.Close();
        }

        protected void POGrid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("GridPageControl") as ASPxPageControl;
            ASPxComboBox entity = pageControl.FindControl("EntityCode") as ASPxComboBox;

            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            SqlDataReader reader = null;

            string[] arr = entity.Text.ToString().Split(';');
            string entity_string = arr[0];

            if (grid.IsNewRowEditing)
            {

                string check = "SELECT COUNT(*) FROM [dbo].[tbl_AXPOUploadingPath] WHERE [Entity] = '" + entity_string + "'";

                SqlCommand cmd = new SqlCommand(check, conn);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result > 0)
                {
                    e.RowError = "Entity Already Exist";
                }

            }
            else
            {
                string PK = e.Keys[0].ToString();

                string check1 = "SELECT [Entity] FROM [dbo].[tbl_AXPOUploadingPath] WHERE [Entity] = '" + entity_string + "' EXCEPT(SELECT [Entity] FROM [dbo].[tbl_AXPOUploadingPath] WHERE [PK] = '" + PK + "')";

                SqlCommand cmd = new SqlCommand(check1, conn);
                cmd.CommandType = CommandType.Text;
                reader = cmd.ExecuteReader();
                bool result = reader.Read();
                if (result)
                {
                    e.RowError = "Entity Already Exist";
                }
            }
            conn.Close();
        }

        protected void InfoGrid_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            DesignBehavior.SetBehaviorGrid(grid);
        }
    }
}