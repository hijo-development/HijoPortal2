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
            width: 70%;
        }

        .innertable_width_hundred {
            width: 100%;
        }

        .innertable_width_thirty {
            width: 30%;
        }

        .innertable_width_seventy {
            width: 70%;
        }
        .all_label{
            padding-left:5%;
        }
    </style>
    <script type="text/javascript">
        function VendorPO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            VendorLblClient.SetText(str[1]);
            TermsCallbackPO.PerformCallback();
            CurrencyCallbackPO.PerformCallback();

        }

        function TermsPO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            TermsLblClient.SetText(str[1]);
        }

        function CurrencyPO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            CurrencyLblClient.SetText(str[1]);
        }

        function CurrencyCallback_EndCallback(s, e) {
            var str = CurrencyClient.GetText().split(";");
            CurrencyClient.SetText(str[0]);
            CurrencyLblClient.SetText(str[1]);
        }

        function SitePO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            SiteLblClient.SetText(str[1]);
            WarehouseCallbackPO.PerformCallback();
        }

        function WarehousePO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            WarehouseLblClient.SetText(str[1]);
            LocationCallbackPO.PerformCallback();
        }

        function LocationPO_SelectedIndexChanged(s, e) {
            var str = s.GetText().split("; ");
            s.SetText(str[0]);
            LocationLblClient.SetText(str[1]);
        }

        function POCreateGrid_CustomButtonClick(s, e) {

        }

        function Save_GotFocus(s, e) {

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
                        <table class="innertable_width_hundred">
                            <tr>
                                <td class="innertable_width_thirty">
                                    <dx:ASPxComboBox ID="Vendor" runat="server" OnInit="Vendor_Init" Width="100%" ValueType="System.String" Theme="Office2010Blue">
                                        <ClientSideEvents SelectedIndexChanged="VendorPO_SelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                </td>
                                <td class="innertable_width_seventy all_label">
                                    <dx:ASPxLabel ID="VendorLbl" runat="server" ClientInstanceName="VendorLblClient" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Site" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable_width_hundred">
                            <tr>
                                <td class="innertable_width_thirty">
                                    <dx:ASPxComboBox ID="Site" runat="server" OnInit="Site_Init" CssClass="innertable_width_hundred" ValueType="System.String" Theme="Office2010Blue">
                                        <ClientSideEvents SelectedIndexChanged="SitePO_SelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                </td>
                                <td class="innertable_width_seventy all_label">
                                    <dx:ASPxLabel ID="SiteLbl" runat="server" ClientInstanceName="SiteLblClient" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Expected Delivery" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data" style="width:10%;">
                        <dx:ASPxDateEdit ID="ExpDel" runat="server" CssClass="innertable_width_hundred" Theme="Office2010Blue" AllowUserInput="false">
                            <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                        </dx:ASPxDateEdit>
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
                                <td style="width: 30%;">
                                    <dx:ASPxCallbackPanel ID="TermsCallback" runat="server" ClientInstanceName="TermsCallbackPO" OnCallback="TermsCallback_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="Terms" runat="server" ClientEnabled="false" ValueType="System.String" Theme="Office2010Blue" Width="100%">
                                                    <ClientSideEvents SelectedIndexChanged="TermsPO_SelectedIndexChanged" />
                                                </dx:ASPxComboBox>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td class="small_data_width all_label">
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
                        <table class="innertable_width_hundred">
                            <tr>
                                <td class="innertable_width_thirty">
                                    <dx:ASPxCallbackPanel ID="WarehouseCallback" runat="server" ClientInstanceName="WarehouseCallbackPO" OnCallback="WarehouseCallback_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="Warehouse" runat="server" ClientInstanceName="WarehousePO" ClientEnabled="false" CssClass="innertable_width_hundred" ValueType="System.String" Theme="Office2010Blue">
                                                    <ClientSideEvents SelectedIndexChanged="WarehousePO_SelectedIndexChanged" />
                                                </dx:ASPxComboBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td class="innertable_width_seventy all_label">
                                    <dx:ASPxLabel ID="WarehouseLbl" runat="server" ClientInstanceName="WarehouseLblClient" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>


                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="MOP Reference" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td style="width:10%;" rowspan="3">
                        <dx:ASPxListBox ID="MOPRef" runat="server" CssClass="innertable_width_hundred" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxListBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Currency" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable_width_hundred" border="0">
                            <tr>
                                <td class="innertable_width_thirty">
                                    <dx:ASPxCallbackPanel ID="CurrencyCallback" runat="server" ClientInstanceName="CurrencyCallbackPO" OnCallback="CurrencyCallback_Callback" Width="100%">
                                        <ClientSideEvents EndCallback="CurrencyCallback_EndCallback" />
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="Currency" runat="server" ClientInstanceName="CurrencyClient" ClientEnabled="false" CssClass="innertable_width_hundred" ValueType="System.String" Theme="Office2010Blue">
                                                    <ClientSideEvents SelectedIndexChanged="CurrencyPO_SelectedIndexChanged" />
                                                </dx:ASPxComboBox>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td class="innertable_width_seventy all_label">
                                    <dx:ASPxLabel ID="CurrencyLbl" runat="server" ClientInstanceName="CurrencyLblClient" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>


                    </td>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Location" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable_width_hundred">
                            <tr>
                                <td class="innertable_width_thirty">
                                    <dx:ASPxCallbackPanel ID="LocationCallback" runat="server" ClientInstanceName="LocationCallbackPO" OnCallback="LocationCallback_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="Location" runat="server" ClientEnabled="false" CssClass="innertable_width_hundred" ValueType="System.String" Theme="Office2010Blue">
                                                    <ClientSideEvents SelectedIndexChanged="LocationPO_SelectedIndexChanged" />
                                                </dx:ASPxComboBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td class="innertable_width_seventy all_label">
                                    <dx:ASPxLabel ID="LocationLbl" runat="server" ClientInstanceName="LocationLblClient" Text="" theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>


                    </td>
                </tr>
                <tr>
                    <td style="height: 50px;"></td>
                </tr>
            </table>
        </div>

        <div>
            <dx:ASPxGridView ID="POCreateGrid" runat="server" Width="100%" Theme="Office2010Blue">
                <ClientSideEvents CustomButtonClick="POCreateGrid_CustomButtonClick" />
                <Columns>
                    <dx:GridViewCommandColumn ButtonRenderMode="Image">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="Edit" Image-Url="images/Edit.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Delete" Image-Url="images/Delete.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Update" Image-Url="images/Save.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Cancel" Image-Url="images/Undo.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <%--<dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" ShowEditButton="true" ShowApplyFilterButton="true" VisibleIndex="0">
                    </dx:GridViewCommandColumn>--%>
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
                <SettingsBehavior AllowHeaderFilter="true" AllowAutoFilter="true" />
            </dx:ASPxGridView>
        </div>
        <div>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxButton ID="Save" runat="server" ClientInstanceName="SavePO" OnClick="Save_Click" Text="SAVE" Theme="Office2010Blue">
                            <ClientSideEvents GotFocus="Save_GotFocus" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
