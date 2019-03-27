<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_addedit.aspx.cs" Inherits="HijoPortal.mrp_po_addedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/POAddedit.css" rel="stylesheet" />
    <script type="text/javascript" src="jquery/POAddedit.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>Purchase Order</h1>
        </div>
        <div>
            <table class="table_detail" border="1">
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="PO Number" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <dx:ASPxLabel ID="PONumberLbl" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Vendor" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxComboBox ID="VendorCombo" runat="server" CssClass="innertable_width" ValueType="System.String" Theme="Office2010Blue">
                                        <%--<ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>--%>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="VendorLbl" runat="server" CssClass="innertable_width" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Site" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxComboBox ID="SiteCombo" runat="server" CssClass="innertable_width" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="SiteLbl" runat="server" CssClass="innertable_width" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Expected Delivery" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data" style="width:10%;">
                        <dx:ASPxDateEdit ID="ExpDel" runat="server" AllowUserInput="false" CssClass="innertable_width" Theme="Office2010Blue">
                            <ClientSideEvents GotFocus="function(s,e){ s.ShowDropDown(); }" />
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Terms" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable">
                            <tr>
                                <td>
                                    <dx:ASPxCallbackPanel ID="TermsCallback" runat="server" CssClass="innertable_width">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="TermsCombo" runat="server" CssClass="innertable_width" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="TermsLbl" runat="server" CssClass="innertable_width" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Warehouse" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable">
                            <tr>
                                <td>
                                    <dx:ASPxCallbackPanel ID="WarehouseCallback" runat="server">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="WarehouseCombo" runat="server" CssClass="innertable_width" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="WarehouseLbl" runat="server" CssClass="innertable_width" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>

                    </td>

                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="MOP Reference" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td style="width: 10%;" rowspan="3">
                        <dx:ASPxListBox ID="MOPRef" runat="server" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxListBox>
                    </td>
                </tr>
                <tr>
                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Currecny" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable">
                            <tr>
                                <td>
                                    <dx:ASPxCallbackPanel ID="CurrencyCallback" runat="server">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="CurrencyCombo" runat="server" CssClass="innertable_width" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="CurrencyLbl" runat="server" CssClass="innertable_width" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>

                    </td>

                    <td class="table_po_td_label">
                        <dx:ASPxLabel runat="server" Text="Location" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td class="table_po_semi">:</td>
                    <td class="table_po_td_data">
                        <table class="innertable">
                            <tr>
                                <td>
                                    <dx:ASPxCallbackPanel ID="LocationCallback" runat="server">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxComboBox ID="LocationCombo" runat="server" CssClass="innertable_width" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td></td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </div>
        <div>
            <dx:ASPxGridView ID="POAddEditGrid" runat="server" Width="100%" Theme="Office2010Blue">
                <Columns>
                    <dx:GridViewCommandColumn ButtonRenderMode="Image" ShowEditButton="true" ShowDeleteButton="true">
                        <CustomButtons>

                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                   <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="ItemPK" Visible="false"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="Identifier" Visible="false"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="ItemCode"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="Description"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="Requested Qty"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="Cost"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="TotalCost"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="POQty"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="TotalPOCost"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="TaxGroup"></dx:GridViewDataColumn>
                   <dx:GridViewDataColumn FieldName="TaxItemGroup"></dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
