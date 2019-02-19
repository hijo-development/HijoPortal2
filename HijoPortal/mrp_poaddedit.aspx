<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_poaddedit.aspx.cs" Inherits="HijoPortal.mrp_poaddedit" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <%--<div id="dvHeader">--%>
        <div style="background-color: #fff">
            <div id="dvHeaderPO" style="height: auto; background-color: #ffffff; padding: 5px 5px 0px 0px; border-radius: 2px;">
                <h1>List of AUTO PO</h1>
                <table border="0" style="width: 100%;">
                    <tr>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="PO #" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td style="width: 20%">
                            <dx:ASPxTextBox ID="POnumber" runat="server" Text="" Width="180px" Theme="Office2010Blue" Border-BorderStyle="None" ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true"
                                Style="font-size: medium; font-weight: bold; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="PO Date" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td style="width: 20%">
                            <dx:ASPxTextBox ID="POdate" runat="server" Text="" Theme="Office2010Blue" Border-BorderStyle="None"
                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="Expected Delivery" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td style="width: 20%">
                            <dx:ASPxDateEdit ID="ExpDelivery" runat="server" Theme="Office2010Blue" AllowUserInput="false"
                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="Vendor" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxComboBox ID="Vendor" runat="server" ValueType="System.String" Theme="Office2010Blue"
                                OnInit="Vendor_Init" ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                <ClientSideEvents SelectedIndexChanged="vendor_indexchanged" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="Currency" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxCallbackPanel ID="CurrencyCallback" ClientInstanceName="CurrencyCallback" runat="server" Width="200px" OnCallback="CurrencyCallback_Callback">
                                <ClientSideEvents EndCallback="currency_endcallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="Currency" runat="server" ValueType="System.String" Theme="Office2010Blue" ValidationSettings-ErrorDisplayMode="None"
                                            ValidationSettings-RequiredField-IsRequired="true">
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="Site" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxComboBox ID="Site" runat="server" ValueType="System.String" Theme="Office2010Blue" OnInit="Site_Init"
                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                <ClientSideEvents SelectedIndexChanged="site_indexchanged" />
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 12%; height: 20px">
                            <dx:ASPxLabel runat="server" Text="Terms" Theme="Office2010Blue"></dx:ASPxLabel>

                        </td>
                        <td>:</td>
                        <td style="width: 20%" colspan="4">
                            <dx:ASPxCallbackPanel ID="TermsCallback" ClientInstanceName="TermsCallback" runat="server" Width="200px" OnCallback="TermsCallback_Callback">
                                <ClientSideEvents EndCallback="terms_endcallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="Terms" runat="server" ValueType="System.String" Theme="Office2010Blue"
                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="Warehouse" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxCallbackPanel ID="WarehouseCallback" ClientInstanceName="WarehouseCallback" runat="server" Width="200px" OnCallback="WarehouseCallback_Callback">
                                <ClientSideEvents EndCallback="warehouse_endcallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="WareHouse" ClientInstanceName="WareHouse" runat="server" ValueType="System.String" Theme="Office2010Blue"
                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                            <ClientSideEvents SelectedIndexChanged="warehouse_indexchanged" />
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Procurement Category" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td colspan="4">
                            <dx:ASPxComboBox ID="ProCategory" runat="server" ValueType="System.String" Width="170px" Theme="Office2010Blue"
                                TextFormatString="{0}" OnInit="ProCategory_Init" ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                <ClientSideEvents SelectedIndexChanged="procategory_indexchange" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 12%">
                            <dx:ASPxLabel runat="server" Text="Location" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxCallbackPanel ID="LocationCallback" ClientInstanceName="LocationCallback" runat="server" Width="200px" OnCallback="LocationCallback_Callback">
                                <ClientSideEvents EndCallback="location_endcallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="Location" ClientInstanceName="Location" runat="server" ValueType="System.String" Theme="Office2010Blue"
                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 70%; vertical-align: top">
                            <dx:ASPxGridView ID="POAddEditGrid" ClientInstanceName="POAddEditGrid" runat="server" Theme="Office2010Blue" Width="100%" AutoGenerateColumns="false"
                                OnRowValidating="POAddEditGrid_RowValidating"
                                OnRowUpdating="POAddEditGrid_RowUpdating"
                                OnCustomCallback="POAddEditGrid_CustomCallback"
                                EnableCallBacks="true" KeyFieldName="PK;TableIdentifier">
                                <%--<ClientSideEvents SelectionChanged="POgrid_selectionChanged" />--%>
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" ShowEditButton="true" VisibleIndex="0">
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="TableIdentifier" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="MRPCategory" VisibleIndex="2">
                                        <EditItemTemplate>
                                            <dx:ASPxLabel runat="server" Text='<%#Eval("MRPCategory") %>'></dx:ASPxLabel>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Item" VisibleIndex="3">
                                        <EditItemTemplate>
                                            <dx:ASPxLabel runat="server" Text='<%#Eval("Item") %>'></dx:ASPxLabel>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="4">
                                        <EditItemTemplate>
                                            <dx:ASPxLabel runat="server" Text='<%#Eval("UOM") %>'></dx:ASPxLabel>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="5">
                                        <EditItemTemplate>
                                            <dx:ASPxLabel runat="server" Text='<%#Eval("Qty") %>'></dx:ASPxLabel>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="6">
                                        <EditItemTemplate>
                                            <dx:ASPxLabel runat="server" Text='<%#Eval("Cost") %>'></dx:ASPxLabel>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="7">
                                        <EditItemTemplate>
                                            <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost") %>'></dx:ASPxLabel>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="POQty" VisibleIndex="8">
                                        <EditItemTemplate>
                                            <dx:ASPxTextBox ID="POQty" ClientInstanceName="POQty" Text='<%#Eval("POQty") %>' runat="server" Width="100px">
                                                <ClientSideEvents ValueChanged="OnValueChangeQty" />
                                                <ClientSideEvents KeyUp="OnKeyUpQtyPO" />
                                                <ClientSideEvents KeyPress="FilterDigit" />
                                            </dx:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="POCost" VisibleIndex="9">
                                        <EditItemTemplate>
                                            <dx:ASPxTextBox ID="POCost" ClientInstanceName="POCost" Text='<%#Eval("POCost") %>' runat="server" Width="100px">
                                                <ClientSideEvents ValueChanged="OnValueChange" />
                                                <ClientSideEvents KeyUp="OnKeyUpCostPO" />
                                                <ClientSideEvents KeyPress="FilterDigit" />
                                            </dx:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="POTotalCost" VisibleIndex="10">
                                        <EditItemTemplate>
                                            <dx:ASPxTextBox ID="POTotalCost" ClientInstanceName="POTotalCost" Text='<%#Eval("POTotalCost") %>' ReadOnly="true" runat="server" Width="100px"></dx:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>

                                    <%--<dx:GridViewDataColumn FieldName="TaxGroup" VisibleIndex="11"></dx:GridViewDataColumn>--%>
                                    <%--<dx:GridViewDataColumn FieldName="TaxItemGroup" VisibleIndex="12"></dx:GridViewDataColumn>--%>

                                    <dx:GridViewDataComboBoxColumn FieldName="TaxGroup" VisibleIndex="11">
                                        <PropertiesComboBox ValueField="TaxGroup" OnItemsRequestedByFilterCondition="ItemsRequestedByFilterCondition_1"></PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn FieldName="TaxItemGroup" VisibleIndex="12">
                                        <PropertiesComboBox ValueField="TaxItemGroup" OnItemsRequestedByFilterCondition="ItemsRequestedByFilterCondition_2"></PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                </Columns>
                                <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" />
                                <SettingsBehavior AllowFocusedRow="True" AllowSort="true" AllowDragDrop="false" />
                                <SettingsEditing Mode="Inline"></SettingsEditing>
                            </dx:ASPxGridView>
                        </td>
                        <td style="width: 1%;"></td>
                        <%--<td style="width: 29%;">
                                <div style="width: 400px; overflow-x: auto">
                                    <dx:ASPxListBox ID="selList" ClientInstanceName="selList" runat="server" Width="400px" Theme="Office2010Blue"
                                        EnableSynchronization="True">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="MRPCategory"></dx:ListBoxColumn>
                                            <dx:ListBoxColumn FieldName="Item"></dx:ListBoxColumn>
                                            <dx:ListBoxColumn FieldName="UOM"></dx:ListBoxColumn>
                                            <dx:ListBoxColumn FieldName="Cost"></dx:ListBoxColumn>
                                            <dx:ListBoxColumn FieldName="POQty"></dx:ListBoxColumn>
                                        </Columns>
                                        <ItemStyle Wrap="True" />
                                    </dx:ASPxListBox>
                                </div>

                                <div class="TopPadding">
                                    Selected count: <span id="selCount" style="font-weight: bold">0</span>
                                </div>
                            </div>
                        </td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <dx:ASPxButton ID="Create" runat="server" Text="Create" Theme="Office2010Blue" OnClick="Create_Click"></dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
