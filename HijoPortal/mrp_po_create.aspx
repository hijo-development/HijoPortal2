<%@ Page Title="Create Purchase Order" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_create.aspx.cs" Inherits="HijoPortal.mrp_po_create" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/POCreate.css" rel="stylesheet" />
    <script type="text/javascript" src="jquery/POCreate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:TextBox ID="TextBoxLoading" runat="server" Visible="true" Style="display: none;"></asp:TextBox>
    <ajaxToolkit:ModalPopupExtender runat="server"
        ID="ModalPopupExtenderLoading"
        BackgroundCssClass="modalBackground"
        PopupControlID="PanelLoading"
        TargetControlID="TextBoxLoading"
        CancelControlID="ButtonErrorOK1"
        ClientIDMode="Static">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelLoading" runat="server"
        CssClass="modalPopupLoading"
        Height="200px"
        Width="200px"
        align="center"
        Style="display: none;">
        <img src="images/Loading.gif" style="height: 200px; width: 200px;" />
        <asp:Button ID="ButtonErrorOK1" runat="server" CssClass="buttons" Width="30%" Text="OK" Style="display: none;" />
    </asp:Panel>

    <dx:ASPxPopupControl ID="POCreateNotify" ClientInstanceName="POCreate_MRPNotify" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Moderno" ContentStyle-Paddings-Padding="20" Width="400px">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxLabel ID="POCreateNotifyLbl" ClientInstanceName="POCreate_MRPNotificationMessage" runat="server" Text="" ForeColor="Red" Theme="Moderno"></dx:ASPxLabel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <div id="dvHeader" style="height: 30px;">
        <h1>Create Purchase Order</h1>
    </div>
    <div>
        <table class="table_detail" border="0">
            <tr>
                <td class="table_po_td_label">
                    <dx:ASPxLabel runat="server" Text="Supplier" Theme="Office2010Blue"></dx:ASPxLabel>
                </td>
                <td class="table_po_semi">:</td>
                <td class="table_po_td_data">
                    <table class="innertable_width_hundred">
                        <tr>
                            <td class="innertable_width_thirty">
                                <dx:ASPxComboBox ID="Vendor" runat="server" ClientInstanceName="VendorClient" OnInit="Vendor_Init" Width="100%" ValueType="System.String" Theme="Office2010Blue">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
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
                                <dx:ASPxComboBox ID="Site" runat="server" ClientInstanceName="SiteClient" OnInit="Site_Init" CssClass="innertable_width_hundred" ValueType="System.String" Theme="Office2010Blue">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
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
                <td class="table_po_td_data" style="width: 10%;">
                    <dx:ASPxDateEdit ID="ExpDel" runat="server" ClientInstanceName="ExpDelClient" CssClass="innertable_width_hundred" Width="100%" Theme="Office2010Blue" AllowUserInput="false">
                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
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
                            <td class="innertable_width_thirty">
                                <dx:ASPxCallbackPanel ID="TermsCallback" runat="server" ClientInstanceName="TermsCallbackPO" OnCallback="TermsCallback_Callback" Width="100%">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <dx:ASPxComboBox ID="Terms" runat="server" ClientInstanceName="TermsClient" ClientEnabled="false" ValueType="System.String" Theme="Office2010Blue" Width="100%">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
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
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
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
                <td style="width: 10%; vertical-align: top;">
                    <dx:ASPxTextBox ID="MOPReference" runat="server" CssClass="innertable_width_hundred" Width="170px" ReadOnly="true" Border-BorderColor="Transparent" BackColor="Transparent" Theme="Office2010Blue">
                    </dx:ASPxTextBox>
                    <%--<dx:ASPxListBox ID="MOPRef" runat="server" CssClass="innertable_width_hundred" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxListBox>--%>
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
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
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
                    <table class="innertable">
                        <tr>
                            <td>
                                <dx:ASPxCallbackPanel ID="LocationCallback" runat="server" ClientInstanceName="LocationCallbackPO" OnCallback="LocationCallback_Callback" Width="100%">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <dx:ASPxComboBox ID="Location" runat="server" ClientInstanceName="LocationClient" ClientEnabled="false" Width="100%" ValueType="System.String" Theme="Office2010Blue">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
                                                <ClientSideEvents SelectedIndexChanged="LocationPO_SelectedIndexChanged" />
                                            </dx:ASPxComboBox>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="LocationLbl" runat="server" ClientInstanceName="LocationLblClient" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>


                </td>
            </tr>
            <tr>
                <td class="table_po_td_label" style="vertical-align: middle;">
                    <dx:ASPxLabel runat="server" Text="Remarks" Theme="Office2010Blue"></dx:ASPxLabel>
                </td>
                <td class="table_po_semi" style="vertical-align: middle;">:</td>
                <td class="table_po_td_data" colspan="7" style="padding-top: 5px;">
                    <dx:ASPxTextBox ID="Remarks" runat="server" ClientInstanceName="RemarksClient" Width="100%" Theme="Office2010Blue">
                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>

    <div class="grid_style">
        <dx:ASPxGridView ID="POCreateGrid" runat="server" Width="100%" Theme="Office2010Blue"
            OnStartRowEditing="POCreateGrid_StartRowEditing"
            OnRowUpdating="POCreateGrid_RowUpdating"
            OnBeforeGetCallbackResult="POCreateGrid_BeforeGetCallbackResult">
            <ClientSideEvents CustomButtonClick="POCreateGrid_CustomButtonClick" />
            <Columns>
                <%--<dx:GridViewCommandColumn ButtonRenderMode="Image">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="Edit" Image-Url="images/Edit.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Delete" Image-Url="images/Delete.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Update" Image-Url="images/Save.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Cancel" Image-Url="images/Undo.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>--%>
                <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="30px">
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="Edit" Image-Url="Images/Edit.ico" Image-Width="15px">
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="Update" Image-Url="images/Save.ico" Image-Width="15px" Visibility="EditableRow">
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="Cancel" Image-Url="images/Undo.ico" Image-Width="15px" Visibility="EditableRow">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="MOPNumber" Visible="false"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="ItemPK" Visible="false"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="TableIdentifier" Visible="false"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="ItemCode">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("ItemCode") %>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="CapexCIP" Caption="Capex ID">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("CapexCIP") %>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Description">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("Description") %>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                    <EditItemTemplate>
                        <dx:ASPxLabel ID="ReqQty" runat="server" ClientInstanceName="ReqQtyClient" Text='<%#Eval("RequestedQty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                        <%--<dx:ASPxLabel runat="server" Text='<%#Eval("RequestedQty") %>'></dx:ASPxLabel>--%>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Cost">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("Cost") %>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="TotalCost">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost") %>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="POUOM" Caption="PO UOM">
                    <HeaderStyle HorizontalAlign="Left" />
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                    <EditItemTemplate>
                        <dx:ASPxComboBox ID="POUOM" runat="server" ValueType="System.String" OnInit="POUOM_Init" Width="100px" Theme="Office2010Blue">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="POQty">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                    <EditItemTemplate>
                        <dx:ASPxTextBox ID="POQty" ClientInstanceName="POQty" Text='<%#Eval("POQty") %>' runat="server" Width="100px" HorizontalAlign="Right" Theme="Office2010Blue">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"></ValidationSettings>
                            <ClientSideEvents ValueChanged="OnValueChangeQty" />
                            <ClientSideEvents KeyUp="POQty_KeyUp" />
                            <ClientSideEvents KeyPress="FilterDigit" />
                        </dx:ASPxTextBox>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="POCost">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                    <EditItemTemplate>
                        <dx:ASPxTextBox ID="POCost" ClientInstanceName="POCost" Text='<%#Eval("POCost") %>' runat="server" Width="100px" HorizontalAlign="Right" Theme="Office2010Blue">
                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            <ClientSideEvents ValueChanged="OnValueChange" />
                            <ClientSideEvents KeyUp="POCost_KeyUp" />
                            <ClientSideEvents KeyPress="FilterDigit" />
                        </dx:ASPxTextBox>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="TotalPOCost">
                    <HeaderStyle HorizontalAlign="Right" />
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                    <EditItemTemplate>
                        <dx:ASPxTextBox ID="TotalPOCost" ClientInstanceName="POTotalCost" Text='<%#Eval("TotalPOCost") %>' runat="server" Width="100px" Border-BorderColor="Transparent" Theme="Office2010Blue">
                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                        </dx:ASPxTextBox>
                        <%--<dx:ASPxLabel ID="TotalPOCost" ClientInstanceName="POTotalCost" Text='<%#Eval("TotalPOCost") %>' runat="server" Width="100px" Theme="Office2010Blue">
                            </dx:ASPxLabel>--%>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataComboBoxColumn FieldName="TaxGroup">
                    <EditItemTemplate>
                        <dx:ASPxComboBox ID="TaxGroup" runat="server" ClientInstanceName="TaxGroupClient" OnInit="TaxGroup_Init" ValueType="System.String" Width="100px" Theme="Office2010Blue">
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                    <%--<PropertiesComboBox ValueField="TaxGroup"></PropertiesComboBox>--%>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="TaxItemGroup">
                    <EditItemTemplate>
                        <dx:ASPxComboBox ID="TaxItemGroup" runat="server" ClientInstanceName="TaxItemGroupClient" OnInit="TaxItemGroup_Init" ValueType="System.String" Width="100px" Theme="Office2010Blue">
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                    <%--<PropertiesComboBox ValueField="TaxItemGroup"></PropertiesComboBox>--%>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <Styles>
                <CommandColumnItem HorizontalAlign="Center" VerticalAlign="Middle"></CommandColumnItem>
                <CommandColumn HorizontalAlign="Center"></CommandColumn>
                <Cell Wrap="true"></Cell>
                <InlineEditCell Wrap="true"></InlineEditCell>
            </Styles>
            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" />
            <SettingsBehavior AllowFocusedRow="True" AllowSort="true" AllowDragDrop="false" />
            <SettingsEditing Mode="Inline"></SettingsEditing>
            <SettingsBehavior AllowHeaderFilter="true" AllowAutoFilter="true" />
        </dx:ASPxGridView>
    </div>
    <div>
        <table style="width: 100%; margin-top: 5px;">
            <tr>
                <td style="text-align: right;">
                    <dx:ASPxButton ID="CancelPage" runat="server" OnClick="CancelPage_Click" CausesValidation="false" Text="Cancel" Theme="Office2010Blue"></dx:ASPxButton>
                    <dx:ASPxButton ID="Save" runat="server" ClientInstanceName="SavePO" OnClick="Save_Click" Text="Create" Theme="Office2010Blue">
                        <ClientSideEvents Click="Save_Click" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
