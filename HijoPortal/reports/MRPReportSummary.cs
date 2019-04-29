using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HijoPortal.classes;

/// <summary>
/// Summary description for MRPReportSummary
/// </summary>
public class MRPReportSummary : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLine xrLine1;
    private XRLine xrLine2;
    private XRLine xrLine3;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel6;
    private ReportFooterBand ReportFooter;
    private XRShape xrShape1;
    private XRLine xrLine6;
    private XRLine xrLine5;
    private XRLine xrLine4;
    private XRLabel xrLabel7;
    private DevExpress.XtraReports.Parameters.Parameter DocNum;
    private DevExpress.XtraReports.Parameters.Parameter EntityCode;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public MRPReportSummary()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters msSqlConnectionParameters1 = new DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MRPReportSummary));
            DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle1 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            this.DocNum = new DevExpress.XtraReports.Parameters.Parameter();
            this.EntityCode = new DevExpress.XtraReports.Parameters.Parameter();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DocNum
            // 
            this.DocNum.Description = "DocNumber";
            this.DocNum.Name = "DocNum";
            this.DocNum.Visible = false;
            // 
            // EntityCode
            // 
            this.EntityCode.Description = "Entity";
            this.EntityCode.Name = "EntityCode";
            this.EntityCode.Visible = false;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine6,
            this.xrLine5,
            this.xrLine4,
            this.xrLine3,
            this.xrLine1,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
            this.Detail.HeightF = 26.25001F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLine6
            // 
            this.xrLine6.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrLine6.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(8F, 0F);
            this.xrLine6.Name = "xrLine6";
            this.xrLine6.SizeF = new System.Drawing.SizeF(5.208313F, 26.25001F);
            // 
            // xrLine5
            // 
            this.xrLine5.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrLine5.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(486.7917F, 0F);
            this.xrLine5.Name = "xrLine5";
            this.xrLine5.SizeF = new System.Drawing.SizeF(5.208313F, 26.25001F);
            // 
            // xrLine4
            // 
            this.xrLine4.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrLine4.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(355.6249F, 0F);
            this.xrLine4.Name = "xrLine4";
            this.xrLine4.SizeF = new System.Drawing.SizeF(5.208313F, 26.25001F);
            // 
            // xrLine3
            // 
            this.xrLine3.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrLine3.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(240.7499F, 0F);
            this.xrLine3.Name = "xrLine3";
            this.xrLine3.SizeF = new System.Drawing.SizeF(5.208313F, 26.25001F);
            // 
            // xrLine1
            // 
            this.xrLine1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(9.999939F, 22.37501F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(480.0001F, 2F);
            // 
            // xrLabel3
            // 
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EdittedTotalCost]")});
            this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(366.3749F, 3F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(118.75F, 18.12499F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "xrLabelTotalEdittedCost";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel3.TextFormatString = "{0:n2}";
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalCost]")});
            this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(250.6249F, 3F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 18.12499F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel2.Summary = xrSummary1;
            this.xrLabel2.Text = "xrLabelTotalCost";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel2.TextFormatString = "{0:n2}";
            // 
            // xrLabel1
            // 
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MRPGroup]")});
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(19.99995F, 3F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(218.75F, 18.12499F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "xrLabel1";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 16F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.TopMargin.Visible = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(366.25F, 9F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(118.75F, 29.87498F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Recommended Total Cost";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(251.5F, 20.75F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 12.99999F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Total Cost";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 42.125F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "summary_hijo_portal_Connection";
            msSqlConnectionParameters1.AuthorizationType = DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer;
            msSqlConnectionParameters1.DatabaseName = "hijo_portal";
            msSqlConnectionParameters1.Password = "itdev@2019";
            msSqlConnectionParameters1.ServerName = "192.168.100.11,1433";
            msSqlConnectionParameters1.UserName = "it_dev";
            this.sqlDataSource1.ConnectionParameters = msSqlConnectionParameters1;
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "MRPSummaryPreview";
            queryParameter1.Name = "@headerdocnum";
            queryParameter1.Type = typeof(string);
            queryParameter2.Name = "@entity";
            queryParameter2.Type = typeof(string);
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "MRPSummaryPreview";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // xrLine2
            // 
            this.xrLine2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(10.87499F, 38.87498F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(480.0001F, 2F);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrLabel4,
            this.xrLabel5,
            this.xrShape1});
            this.PageHeader.HeightF = 41.87498F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrShape1
            // 
            this.xrShape1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Tag", "[Parameters].[DocNumber]")});
            this.xrShape1.FillColor = System.Drawing.Color.LimeGreen;
            this.xrShape1.LineWidth = 0;
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(9.999943F, 2.499994F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeRectangle1;
            this.xrShape1.SizeF = new System.Drawing.SizeF(480.0001F, 38.375F);
            this.xrShape1.Stretch = true;
            // 
            // xrLabel6
            // 
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([TotalCost])")});
            this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(252.5F, 4F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 15F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel6.Summary = xrSummary2;
            this.xrLabel6.Text = "xrLabel6";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel6.TextFormatString = "{0:n2}";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6});
            this.ReportFooter.HeightF = 63.54167F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel10
            // 
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(9.999943F, 10.00001F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(49.99998F, 23F);
            this.xrLabel10.Text = "doc#";
            // 
            // xrLabel9
            // 
            this.xrLabel9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters].[EntityCode]")});
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(76.24996F, 32.99999F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(162.5F, 23F);
            this.xrLabel9.Text = "xrLabel9";
            // 
            // xrLabel8
            // 
            this.xrLabel8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters].[DocNum]")});
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(76.24996F, 10.00001F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(162.5F, 23F);
            this.xrLabel8.Text = "xrLabel8";
            // 
            // xrLabel7
            // 
            this.xrLabel7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([EdittedTotalCost])")});
            this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(366.375F, 3.999996F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(118.625F, 15F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel7.Summary = xrSummary3;
            this.xrLabel7.Text = "xrLabel6";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel7.TextFormatString = "{0:n2}";
            // 
            // MRPReportSummary
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "MRPSummaryPreview";
            this.DataSource = this.sqlDataSource1;
            this.FilterString = "[DocNumber] = ?DocNum And [Entity] = ?EntityCode";
            this.Margins = new System.Drawing.Printing.Margins(100, 251, 16, 42);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DocNum,
            this.EntityCode});
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
