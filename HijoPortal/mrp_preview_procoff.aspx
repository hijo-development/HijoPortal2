<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_preview_procoff.aspx.cs" Inherits="HijoPortal.mrp_preview_procoff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #Header {
            width: 100%;
        }

            #Header h1 {
                width: 100%;
                text-align: center;
                font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
                font-size: medium;
                font-weight: bold;
                color: white;
                text-shadow: 1px 1px 2px black, 0 0 25px blue, 0 0 5px darkblue;
            }

            #Header table {
                font-family: Tahoma;
            }

            #Header td {
                font-family: Tahoma;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server">
        <div id="Header">
            <h1>Preview</h1>
            <table border="0" style="width: 100%">
                <tr>
                    <td style="width: 12%">
                        <dx:ASPxLabel runat="server" Text="MRP Number" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="DocNum" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="MONTH" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="Month" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>

                    <td style="width: 8%">
                        <dx:ASPxLabel runat="server" Text="DATE CREATED" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="DateCreated" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="YEAR" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="Year" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>

                    <td>
                        <dx:ASPxLabel runat="server" Text="ENTITY" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="Entity" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="DEPARTMENT" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="Department" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>

                    <td>
                        <dx:ASPxLabel runat="server" Text="STATUS" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="Status" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>


                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="CREATOR" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="Creator" runat="server" Width="170px" Theme="Office2010Blue" Border-BorderColor="Transparent" BackColor="Transparent"></dx:ASPxTextBox>
                    </td>

                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div>

            <table style="width: 100%; margin-top: 10px;">
                <tr>
                    <td>
                        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Direct Materials" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="DM_Grid" runat="server" Width="100%" Theme="Office2010Blue">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ItemCode"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Descripiton"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Qty" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="POQty" CellStyle-HorizontalAlign="Right">
                                                <FooterCellStyle HorizontalAlign="Right" Font-Bold="true"></FooterCellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="RemainingQty" CellStyle-HorizontalAlign="Right">
                                                <FooterCellStyle HorizontalAlign="Right" Font-Bold="true"></FooterCellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>

                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="POQty" SummaryType="Sum" ShowInColumn="POQty" DisplayFormat="Total: {0:0,0.00}" />
                                            <dx:ASPxSummaryItem FieldName="RemainingQty" SummaryType="Sum" ShowInColumn="RemainingQty" DisplayFormat="Total: {0:0,0.00}" />
                                        </TotalSummary>
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" ShowFooter="true" />
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>

                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="Operating Expense" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="OP_Grid" runat="server" Width="100%" Theme="Office2010Blue">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ItemCode"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Descripiton"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Qty" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="POQty" CellStyle-HorizontalAlign="Right">
                                                <FooterCellStyle HorizontalAlign="Right" Font-Bold="true"></FooterCellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="RemainingQty" CellStyle-HorizontalAlign="Right">
                                                <FooterCellStyle HorizontalAlign="Right" Font-Bold="true"></FooterCellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>

                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="POQty" SummaryType="Sum" ShowInColumn="POQty" DisplayFormat="Total: {0:0,0.00}" />
                                            <dx:ASPxSummaryItem FieldName="RemainingQty" SummaryType="Sum" ShowInColumn="RemainingQty" DisplayFormat="Total: {0:0,0.00}" />
                                        </TotalSummary>
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" ShowFooter="true" />
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="Capital Expenditure" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="CA_Grid" runat="server" Width="100%" Theme="Office2010Blue">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ItemCode"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Descripiton"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Qty" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="POQty" CellStyle-HorizontalAlign="Right">
                                                <FooterCellStyle HorizontalAlign="Right" Font-Bold="true"></FooterCellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="RemainingQty" CellStyle-HorizontalAlign="Right">
                                                <FooterCellStyle HorizontalAlign="Right" Font-Bold="true"></FooterCellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>

                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="POQty" SummaryType="Sum" ShowInColumn="POQty" DisplayFormat="Total: {0:0,0.00}" />
                                            <dx:ASPxSummaryItem FieldName="RemainingQty" SummaryType="Sum" ShowInColumn="RemainingQty" DisplayFormat="Total: {0:0,0.00}" />
                                        </TotalSummary>
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" ShowFooter="true" />
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
