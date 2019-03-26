<%@ Page Title="Select Items" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_selectitem.aspx.cs" Inherits="HijoPortal.mrp_po_selectitem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function MonthYear_Combo_SelectedIndexChanged(s, e) {
            var text = s.GetSelectedItem().text;
            MOPNum_Combo.PerformCallback();
            MainGridCallbackPanel.PerformCallback();

        }
        function MOPNum_Combo_SelectedIndexChanged(s, e) {
            var monthyear = MonthYear_Combo.GetValue();
            MainGridCallbackPanel.PerformCallback();
        }

        function ProdCategory_Combo_SelectedIndexChanged(s, e) {
            MainGridCallbackPanel.PerformCallback();
        }

        function MainGrid_PO_SelectionChanged(s, e) {
            console.log(s.GetSelectedRowCount());
            if (s.GetSelectedRowCount() > 0)
                CreatePO.SetEnabled(true);
            else
                CreatePO.SetEnabled(false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>Select Items for Purchase Order</h1>
        </div>
        <div>
            <table style="width: 100%; margin-bottom: 10px;" border="0">
                <tr style="padding-bottom: 5px;">
                    <td style="width: 15%;">
                        <dx:ASPxLabel runat="server" Text="Month/Year" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%;">:</td>
                    <td style="width: 15%;">
                        <dx:ASPxComboBox ID="MonthYear_Combo" runat="server" ClientInstanceName="MonthYear_Combo" OnInit="MonthYear_Combo_Init" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="MonthYear_Combo_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 69%;"></td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="MOP Number" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxComboBox ID="MOPNum_Combo" runat="server" ClientInstanceName="MOPNum_Combo" OnInit="MOPNum_Combo_Init" OnCallback="MOPNum_Combo_Callback" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="MOPNum_Combo_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Procurement Category" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxComboBox ID="ProdCategory_Combo" runat="server" OnInit="ProdCategory_Combo_Init" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="ProdCategory_Combo_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%;">
            <dx:ASPxCallbackPanel ID="MainGridCallbackPanel" runat="server" ClientInstanceName="MainGridCallbackPanel" OnCallback="MainGridCallbackPanel_Callback" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <dx:ASPxGridView ID="MainGrid_PO" runat="server" Theme="Office2010Blue" Width="100%">
                            <ClientSideEvents SelectionChanged="MainGrid_PO_SelectionChanged" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page"></dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="TableIdentifier" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="DocumentNumber" Width="150px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Entity"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BU" Caption="BU/SSU"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemCatCode" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName ="ItemCat" Caption="Item Category"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemCode"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemDescription" Caption="Description"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Qty">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Cost">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="TotalCost">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        </dx:ASPxGridView>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>

        </div>
        <div>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxButton ID="Create" runat="server" ClientInstanceName="CreatePO" OnClick="Create_Click" Text="CREATE PO" ClientEnabled="false" Theme="Office2010Blue"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
