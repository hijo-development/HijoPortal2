<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_pocreatededit.aspx.cs" Inherits="HijoPortal.mrp_pocreatededit" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPopupControl ID="POCreatedGrid_DeletePopup" runat="server" ClientInstanceName="POCreatedGrid_DeletePopup" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Delete this row?" Theme="Office2010Blue"></dx:ASPxLabel>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Delete" Theme="Office2010Blue">
                                <ClientSideEvents Click="function(s,e){
                                    POCreatedGridCIN.DeleteRow(POCreatedGridCIN.GetFocusedRowIndex());
                                    POCreatedGrid_DeletePopup.Hide();}" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Cancel" Theme="Office2010Blue">
                                <ClientSideEvents Click="function(s,e){ POCreatedGrid_DeletePopup.Hide();}" />
                            </dx:ASPxButton>
                        </td>

                    </tr>
                </table>

            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <div id="dvContentWrapper" runat="server" class="ContentWrapper" style="background-color: #fff; padding: 5px;">
        <%--<div id="dvHeader">--%>
        <div style="background-color: #fff; width: auto;">
            <div id="dvHeaderPO" style="height: auto; background-color: #ffffff; padding: 5px 5px 0px 0px; border-radius: 2px;">
                <h1>List of PO</h1>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>
                            <dx:ASPxLabel runat="server" Text="PO Number #" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="PONumber" runat="server" ReadOnly="true" Border-BorderColor="Transparent" Width="170px">
                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <dx:ASPxLabel runat="server" Text="DocNumber #" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td style="width: 20%;">
                            <dx:ASPxTextBox ID="DocNumber" runat="server" ReadOnly="true" Border-BorderColor="Transparent" Width="170px">
                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Date Created" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="DateCreated" runat="server" ReadOnly="true" Border-BorderColor="Transparent" Width="170px">
                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Vendor" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxComboBox ID="Vendor" runat="server" ValueType="System.String" Theme="Office2010Blue" OnInit="Vendor_Init">
                                <ClientSideEvents SelectedIndexChanged="pocreatededit_Vendor_SelectedIndexChanged" />
                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>

                        <td>
                            <dx:ASPxLabel runat="server" Text="Site" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxComboBox ID="Site" runat="server" ValueType="System.String" Theme="Office2010Blue" OnInit="Site_Init">
                                <ClientSideEvents SelectedIndexChanged="pocreatededit_Site_SelectedIndexChanged" />
                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>

                        <td>
                            <dx:ASPxLabel runat="server" Text="Expected Date" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxDateEdit ID="ExpectedDate" runat="server" Theme="Office2010Blue" AllowUserInput="false">
                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Currency" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxCallbackPanel ID="pocreatededit_currency_callback" ClientInstanceName="pocreatededit_currency_callback" runat="server"
                                OnCallback="pocreatededit_currency_callback_Callback" Width="200px">
                                <ClientSideEvents EndCallback="pocreatededit_currency_EndCallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="Currency" runat="server" ValueType="System.String" Theme="Office2010Blue" OnInit="Currency_Init">
                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>

                        </td>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Warehouse" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxCallbackPanel ID="pocreatededit_warehouse_callback" ClientInstanceName="pocreatededit_warehouse_callback" runat="server"
                                OnCallback="pocreatededit_warehouse_callback_Callback" Width="200px">
                                <ClientSideEvents EndCallback="pocreatededit_warehouse_EndCallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="Warehouse" runat="server" ValueType="System.String" Theme="Office2010Blue" OnInit="Warehouse_Init">
                                            <ClientSideEvents SelectedIndexChanged="pocreatededit_Warehouse_SelectedIndexChanged" />
                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Terms" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxCallbackPanel ID="pocreatededit_terms_callback" ClientInstanceName="pocreatededit_terms_callback" runat="server"
                                OnCallback="pocreatededit_terms_callback_Callback" Width="200px">
                                <ClientSideEvents EndCallback="pocreatededit_terms_EndCallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="Terms" runat="server" ValueType="System.String" Theme="Office2010Blue" OnInit="Terms_Init">
                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>

                        </td>
                        <td>
                            <dx:ASPxLabel runat="server" Text="Location" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                        <td>:</td>
                        <td>
                            <dx:ASPxCallbackPanel ID="pocreatededit_location_callback" ClientInstanceName="pocreatededit_location_callback" runat="server"
                                OnCallback="pocreatededit_location_callback_Callback" Width="200px">
                                <ClientSideEvents EndCallback="pocreatededit_location_EndCallback" />
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxComboBox ID="Location" runat="server" ValueType="System.String" Theme="Office2010Blue" OnInit="Location_Init">
                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>

                        </td>

                    </tr>
                </table>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 100%; vertical-align: top;">
                            <dx:ASPxGridView ID="POCreatedGrid" runat="server" ClientInstanceName="POCreatedGridCIN" Theme="Office2010Blue" Width="100%" AutoGenerateColumns="false"
                                OnInitNewRow="POCreatedGrid_InitNewRow"
                                OnRowInserting="POCreatedGrid_RowInserting"
                                OnStartRowEditing="POCreatedGrid_StartRowEditing"
                                OnRowUpdating="POCreatedGrid_RowUpdating"
                                OnRowDeleting="POCreatedGrid_RowDeleting"
                                OnBeforeGetCallbackResult="POCreatedGrid_BeforeGetCallbackResult"
                                KeyFieldName="PK;Identifier">
                                <ClientSideEvents CustomButtonClick="POCreatedGrid_CustomButtonClick" />
                                <ClientSideEvents FocusedRowChanged="POCreatedGrid_FocusedRowChanged" />
                                <Columns>
                                    <dx:GridViewCommandColumn ButtonRenderMode="Image" ShowEditButton="true" ShowNewButtonInHeader="true" ShowApplyFilterButton="true" VisibleIndex="0" Width="40">
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="POCreatedGrid_DeleteBtn" Image-Url="images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                            <dx:GridViewCommandColumnCustomButton ID="POCreatedGrid_UpdateBtn" Visibility="EditableRow" Image-Url="images/Save.ico" Image-ToolTip="Update Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                            <dx:GridViewCommandColumnCustomButton ID="POCreatedGrid_Cancel" Visibility="EditableRow" Image-Url="images/Undo.ico" Image-ToolTip="Cancel Changes" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataColumn FieldName="PK" Visible="false">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ItemPK" Visible="false">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Identifier" Visible="false">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="AvailForPO" Visible="false"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ItemCode">
                                        <EditItemTemplate>
                                            <dx:ASPxHiddenField ID="pk_identifier_ingrid" ClientInstanceName="pk_identifier_ingrid" runat="server"></dx:ASPxHiddenField>
                                            <dx:ASPxComboBox ID="ItemCode" runat="server" ClientInstanceName="POCreatedGrid_ItemCode" ValueType="System.String" Theme="Office2010Blue" OnInit="itemcode_Init">
                                                <ClientSideEvents SelectedIndexChanged="itemcode_SelectedIndexChanged" />
                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" ErrorText="Invalid Value"></ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="TaxGroup">
                                        <EditItemTemplate>
                                            <dx:ASPxComboBox ID="TaxGroup" runat="server" ClientInstanceName="POCreatedGrid_TaxGroup" ValueType="System.String" Theme="Office2010Blue" OnInit="taxgroup_Init">
                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" ErrorText="Invalid Value"></ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataComboBoxColumn FieldName="TaxItemGroup">
                                        <EditItemTemplate>
                                            <dx:ASPxComboBox ID="TaxItemGroup" runat="server" ClientInstanceName="POCreatedGrid_TaxItemGroup" ValueType="System.String" Theme="Office2010Blue" OnInit="taxitemgroup_Init">
                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" ErrorText="Invalid Value"></ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataColumn FieldName="UOM">
                                        <EditItemTemplate>
                                            <dx:ASPxLabel runat="server" ClientInstanceName="POCreatedUOM" Text='<%#Eval("UOM") %>' Theme="Office2010Blue"></dx:ASPxLabel>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Qty">
                                        <EditItemTemplate>
                                            <dx:ASPxTextBox ID="Qty" runat="server" ClientInstanceName="POCreatedQty" Text='<%#Eval("Qty") %>' Theme="Office2010Blue" HorizontalAlign="Right">
                                                <ClientSideEvents ValueChanged="OnValueChangeQty" />
                                                <ClientSideEvents KeyUp="POCreatedQty_KeyUp" />
                                                <ClientSideEvents KeyPress="FilterDigit" />
                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" ErrorText="Invalid Value"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Cost">
                                        <EditItemTemplate>
                                            <dx:ASPxTextBox ID="Cost" runat="server" ClientInstanceName="POCreatedCost" Text='<%#Eval("Cost") %>' Theme="Office2010Blue" HorizontalAlign="Right">
                                                <ClientSideEvents ValueChanged="OnValueChange" />
                                                <ClientSideEvents KeyUp="POCreatedCost_KeyUp" />
                                                <ClientSideEvents KeyPress="FilterDigit" />
                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" ErrorText="Invalid Value"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="TotalCost">
                                        <EditItemTemplate>
                                            <dx:ASPxTextBox ID="TotalCost" runat="server" ClientInstanceName="POCreatedTotal" ReadOnly="true" Text='<%#Eval("TotalCost") %>' Theme="Office2010Blue" HorizontalAlign="Right">
                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" ErrorText="Invalid Value"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Styles>
                                    <Cell Wrap="True"></Cell>
                                </Styles>
                                <SettingsLoadingPanel Mode="ShowAsPopup" />
                                <SettingsEditing Mode="Inline"></SettingsEditing>
                                <SettingsCommandButton>
                                    <EditButton Image-Url="images/Edit.ico" Image-Width="15px"></EditButton>
                                    <DeleteButton Image-Url="images/Delete.ico" Image-Width="15px"></DeleteButton>
                                    <NewButton Image-Url="images/Add.ico" Image-Width="15px"></NewButton>
                                    <%--<UpdateButton Image-Url="images/Save.ico" Image-Width="15px"></UpdateButton>--%>
                                    <%--<CancelButton Image-Url="images/Undo.ico" Image-Width="15px"></CancelButton>--%>
                                </SettingsCommandButton>
                                <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                            </dx:ASPxGridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="Send" runat="server" Text="SEND" Theme="Office2010Blue"></dx:ASPxButton>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
</asp:Content>
