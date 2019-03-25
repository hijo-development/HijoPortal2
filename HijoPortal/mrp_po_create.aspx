<%@ Page Title="Create Purchase Order" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_create.aspx.cs" Inherits="HijoPortal.mrp_po_create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table_detail {
            margin-bottom: 10px;
        }

        .table_po_td_label {
            width: 5%;
            vertical-align: top;
        }

        .table_po_td_data {
            width: 25%;
            vertical-align: top;
        }

        .table_po_semi {
            width: 1%;
            vertical-align: top;
        }

        .data_width {
            width: 60%;
        }

        .small_data_width {
            width: 30%;
        }
    </style>
    <script type="text/javascript">
        function VendorPO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            VendorLblClient.SetText(str[1]);
            TermsCallbackPO.PerformCallback();

        }

        function TermsPO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            TermsLblClient.SetText(str[1]);
            CurrencyCallbackPO.PerformCallback();
        }

        function CurrencyPO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            CurrencyLblClient.SetText(str[1]);
        }

        function SitePO_SelectedIndexChanged(s, e) {

            WarehouseCallbackPO.PerformCallback();
        }

        function WarehousePO_SelectedIndexChanged(s, e) {
            LocationCallbackPO.PerformCallback();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>Select Items for Purchase Order</h1>
        </div>
        <div>
            <table class="table_detail" border="0">
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Vendor" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <dx:ASPxComboBox ID="Vendor" runat="server" OnInit="Vendor_Init" CssClass="small_data_width" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="VendorPO_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                        <dx:ASPxLabel ID="VendorLbl" runat="server" ClientInstanceName="VendorLblClient" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Site" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <dx:ASPxComboBox ID="Site" runat="server" OnInit="Site_Init" CssClass="data_width" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="SitePO_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Expected Delivery" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <dx:ASPxDateEdit ID="ExpDel" runat="server" CssClass="data_width" Theme="Office2010Blue"></dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Terms" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <dx:ASPxCallbackPanel ID="TermsCallback" runat="server" ClientInstanceName="TermsCallbackPO" OnCallback="TermsCallback_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="Terms" runat="server" CssClass="small_data_width" ClientEnabled="false" ValueType="System.String" Theme="Office2010Blue">
                                                    <ClientSideEvents SelectedIndexChanged="TermsPO_SelectedIndexChanged" />
                                                </dx:ASPxComboBox>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="TermsLbl" runat="server" ClientInstanceName="TermsLblClient" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Warehouse" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <dx:ASPxCallbackPanel ID="WarehouseCallback" runat="server" ClientInstanceName="WarehouseCallbackPO" OnCallback="WarehouseCallback_Callback" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxComboBox ID="Warehouse" runat="server" ClientInstanceName="WarehousePO" ClientEnabled="false" CssClass="data_width" ValueType="System.String" Theme="Office2010Blue">
                                        <ClientSideEvents SelectedIndexChanged="WarehousePO_SelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="MOP Reference" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data" rowspan="3">
                        <dx:ASPxListBox ID="MOPRef" runat="server" CssClass="data_width" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxListBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Currency" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <dx:ASPxCallbackPanel ID="CurrencyCallback" runat="server" ClientInstanceName="CurrencyCallbackPO" OnCallback="CurrencyCallback_Callback" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxComboBox ID="Currency" runat="server" ClientEnabled="false" CssClass="small_data_width" ValueType="System.String" Theme="Office2010Blue">
                                        <ClientSideEvents SelectedIndexChanged="CurrencyPO_SelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel ID="CurrencyLbl" runat="server" ClientInstanceName="CurrencyLblClient" Theme="Office2010Blue"></dx:ASPxLabel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Location" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <dx:ASPxCallbackPanel ID="LocationCallback" runat="server" ClientInstanceName="LocationCallbackPO" OnCallback="LocationCallback_Callback" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxComboBox ID="Location" runat="server" ClientEnabled="false" CssClass="data_width" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                    </td>
                </tr>
                <tr>
                    <td style="height: 50px;"></td>
                </tr>
            </table>
        </div>

        <div>
            <dx:ASPxGridView ID="POCreateGrid" runat="server" Width="100%" Theme="Office2010Blue">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" ShowEditButton="true" ShowApplyFilterButton="true" VisibleIndex="0">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="TableIdentifier" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ItemCode">
                        <EditItemTemplate>
                            <dx:ASPxLabel runat="server" Text='<%#Eval("ItemCode") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Description">
                        <EditItemTemplate>
                            <dx:ASPxLabel runat="server" Text='<%#Eval("Description") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="UOM">
                        <EditItemTemplate>
                            <dx:ASPxLabel runat="server" Text='<%#Eval("UOM") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="RequestedQty">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxLabel runat="server" Text='<%#Eval("Qty") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Cost">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxLabel runat="server" Text='<%#Eval("Cost") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="TotalCost">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="POQty">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="POQty" ClientInstanceName="POQty" Text='<%#Eval("POQty") %>' runat="server" Width="100px">
                                <ClientSideEvents ValueChanged="OnValueChangeQty" />
                                <ClientSideEvents KeyUp="OnKeyUpQtyPO" />
                                <ClientSideEvents KeyPress="FilterDigit" />
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="POCost">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="POCost" ClientInstanceName="POCost" Text='<%#Eval("POCost") %>' runat="server" Width="100px">
                                <ClientSideEvents ValueChanged="OnValueChange" />
                                <ClientSideEvents KeyUp="OnKeyUpCostPO" />
                                <ClientSideEvents KeyPress="FilterDigit" />
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="TotalPOCost">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="TotalPOCost" ClientInstanceName="POTotalCost" Text='<%#Eval("TotalPOCost") %>' ReadOnly="true" runat="server" Width="100px"></dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="TaxGroup">
                        <PropertiesComboBox ValueField="TaxGroup"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="TaxItemGroup">
                        <PropertiesComboBox ValueField="TaxItemGroup"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
        <div>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxButton ID="Save" runat="server" ClientInstanceName="SavePO" Text="SAVE" Theme="Office2010Blue"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
