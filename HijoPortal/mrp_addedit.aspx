﻿<%@ Page Title="MOP Add/Edit" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_addedit.aspx.cs" Inherits="HijoPortal.mrp_addedit" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader">
            <h1>M O P  Details</h1>
            <%--<table class="mrp-add-form-table" style="width: 100%; padding: 25px; margin-bottom: 5px;" border="0">--%>
            <table border="0">
                <tr>
                    <td style="width: 12%">
                        <dx:ASPxLabel runat="server" Text="MRP Number" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td colspan="4">
                        <dx:ASPxLabel ID="DocNum" runat="server" Text=""  Theme="Office2010Blue" Style="font-size: medium; font-weight: bold; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></dx:ASPxLabel>
                    </td>
                    <%--<td style="width: 8%"></td>
                        <td></td>
                        <td style="width: 12%">
                        </td>--%>
                    <td style="width: 40%; text-align: right;" rowspan="2">
                        <span style="font-size: 30px; cursor: pointer" onclick="openNav()">&#9776;</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="MONTH" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td style="width: 20%">
                        <dx:ASPxLabel ID="Month" runat="server" Text="ASPxLabel" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 8%">
                        <dx:ASPxLabel runat="server" Text="DATE CREATED" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td style="width: 20%">
                        <dx:ASPxLabel ID="DateCreated" runat="server" Text="" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="YEAR" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel ID="Year" runat="server" Text="" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="ENTITY" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td colspan="2">
                        <dx:ASPxLabel ID="EntityCode" runat="server" Text="" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>

                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="DEPARTMENT" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel ID="BUCode" runat="server" Text="" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="STATUS" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel runat="server" ID="Status" Text="STATUS" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="CREATOR" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel ID="Creator" runat="server" Text="" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align: right">
                        <dx:ASPxButton ID="Submit" runat="server" Text="Submit" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                        &nbsp
                            <dx:ASPxButton ID="MRPList" runat="server" Text="MOP LIST" AutoPostBack="false" Theme="Office2010Blue" OnClick="MRPList_Click"></dx:ASPxButton>
                        &nbsp
                            <dx:ASPxButton ID="Preview" runat="server" Text="PREVIEW" AutoPostBack="false" Theme="Office2010Blue" OnClick="Preview_Click"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableHierarchyRecreation="true" Theme="Office2010Blue">
            <TabPages>
                <dx:TabPage Text="MRP">
                    <ContentCollection>
                        <dx:ContentControl>
                            <%--<div id="dvDetails" class="scroll">--%>
                                <table style="width: 100%; padding: 25px;" border="0">
                                    <tr>
                                        <td colspan="5">
                                            <dx:ASPxRoundPanel ID="DirectMaterialsRoundPanel" runat="server" HeaderText="DIRECT MATERIALS" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxGridView ID="DirectMaterialsGrid" runat="server" ClientInstanceName="DirectMaterialsGrid" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                                            OnInitNewRow="DirectMaterialsGrid_InitNewRow"
                                                            OnRowInserting="DirectMaterialsGrid_RowInserting"
                                                            OnRowDeleting="DirectMaterialsGrid_RowDeleting"
                                                            OnStartRowEditing="DirectMaterialsGrid_StartRowEditing"
                                                            OnRowUpdating="DirectMaterialsGrid_RowUpdating">
                                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'Materials');}" />

                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ActivityCode" Caption="Activity" VisibleIndex="3"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ItemCode" VisibleIndex="4"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ItemDescription" VisibleIndex="5"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="6"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="7" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="8"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="9" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                            </Columns>

                                                            <SettingsCommandButton>
                                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                                <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                                                <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                                            </SettingsCommandButton>
                                                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                                                            <Templates>
                                                                <EditForm>
                                                                    <div style="padding: 4px 3px 4px">
                                                                        <dx:ASPxPageControl ID="DirectPageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                                                            <TabPages>
                                                                                <dx:TabPage Text="Direct Materials" Visible="true">
                                                                                    <ContentCollection>
                                                                                        <dx:ContentControl runat="server">
                                                                                            <table style="width: 100%; padding: 10px;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Activity" Theme="Office2010Blue" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="ActivityCode" runat="server" ClientInstanceName="ActivityCodeDirect" OnInit="ActivityCode_Init" AutoResizeWithContainer="true" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents SelectedIndexChanged="ActivityCodeIndexChange" />
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Item Code" Theme="Office2010Blue" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="ItemCode" ClientInstanceName="ItemCodeDirect" runat="server" Text='<%#Eval("ItemCode")%>' AutoResizeWithContainer="true" Width="170px" Theme="Office2010Blue" ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents KeyPress="ItemCodeDirect_KeyPress" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                        <dx:ASPxListBox ID="listbox" ClientInstanceName="listbox" runat="server" ValueType="System.String" OnCallback="listbox_Callback" ClientVisible="false" CssClass="list-box" Theme="Office2010Blue" ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents SelectedIndexChanged="listbox_selected" />
                                                                                                        </dx:ASPxListBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Item Description" Theme="Office2010Blue" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="ItemDescription" runat="server" ClientInstanceName="ItemDescriptionDirect" ReadOnly="true" Text='<%#Eval("ItemDescription")%>' Width="170px" Theme="Office2010Blue" ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="UOM" Theme="Office2010Blue" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="UOM" runat="server" ClientInstanceName="UOMDirect" Text='<%#Eval("UOM")%>' OnInit="UOM_Init" ValueType="System.String" Theme="Office2010Blue" ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Cost" Theme="Office2010Blue" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Cost" runat="server" ClientInstanceName="CostDirect" Text='<%#Eval("Cost")%>' Width="170px" Theme="Office2010Blue" HorizontalAlign="Right"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChange" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpCostDirect" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Qty" Theme="Office2010Blue" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Qty" runat="server" ClientInstanceName="QtyDirect" Text='<%#Eval("Qty")%>' Width="170px" Theme="Office2010Blue" HorizontalAlign="Right"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpQtyDirect" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Total Cost" Theme="Office2010Blue" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="TotalCost" runat="server" ClientInstanceName="TotalCostDirect" Text='<%#Eval("TotalCost")%>' ReadOnly="true" Width="170px" Theme="Office2010Blue" HorizontalAlign="Right"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </dx:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dx:TabPage>
                                                                            </TabPages>
                                                                        </dx:ASPxPageControl>
                                                                    </div>
                                                                    <div style="text-align: right; padding: 2px">
                                                                        <dx:ASPxButton runat="server" Text="Save" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="updateDirectMat" />
                                                                        </dx:ASPxButton>
                                                                        <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e){DirectMaterialsGrid.CancelEdit();}" />
                                                                        </dx:ASPxButton>
                                                                    </div>
                                                                </EditForm>
                                                            </Templates>
                                                            <SettingsPager PageSize="10"></SettingsPager>
                                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" ConfirmDelete="true" />
                                                            <SettingsText ConfirmDelete="Delete This Item?" />
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <dx:ASPxRoundPanel ID="OpexRoundPanel" runat="server" HeaderText="OPEX" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                                <%--<HeaderTemplate>
                                    <dx:ASPxLabel runat="server" Text="OPEX" Theme="Office2010Blue"></dx:ASPxLabel>
                                </HeaderTemplate>--%>
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxGridView ID="OPEXGrid" runat="server" ClientInstanceName="OPEXGrid" Width="100%" Theme="Office2010Blue"
                                                            OnInitNewRow="OPEXGrid_InitNewRow"
                                                            OnRowInserting="OPEXGrid_RowInserting"
                                                            OnRowDeleting="OPEXGrid_RowDeleting"
                                                            OnStartRowEditing="OPEXGrid_StartRowEditing"
                                                            OnRowUpdating="OPEXGrid_RowUpdating">
                                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'OPEX');}" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ExpenseCode" VisibleIndex="3"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ItemCode" VisibleIndex="4"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Description" VisibleIndex="5"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="6" ></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="7" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="8" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="9" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                            </Columns>

                                                            <SettingsCommandButton>
                                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                                <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                                                <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                                            </SettingsCommandButton>
                                                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>

                                                            <Templates>
                                                                <EditForm>
                                                                    <div style="padding: 4px 3px 4px">
                                                                        <dx:ASPxPageControl ID="OPEXPageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                                                            <TabPages>
                                                                                <dx:TabPage Text="OPEX" Visible="true">
                                                                                    <ContentCollection>
                                                                                        <dx:ContentControl runat="server">
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Expense Code" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="ExpenseCode" runat="server" ClientInstanceName="ExpenseCodeOPEX" Text='<%#Eval("ExpenseCode")%>' OnInit="ExpenseCode_Init" ValueType="System.String" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents SelectedIndexChanged="ExpenseCodeIndexChangeOPEX" />
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Item Code" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="ItemCode" runat="server" ClientInstanceName="ItemCodeOPEX" Text='<%#Eval("ItemCode")%>' Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Item Description" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Description" runat="server" ClientInstanceName="DescriptionOPEX" Text='<%#Eval("Description")%>' Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="UOM" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="UOM" runat="server" ClientInstanceName="UOMOPEX" Text='<%#Eval("UOM")%>' OnInit="UOM_Init" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Cost" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Cost" runat="server" ClientInstanceName="CostOPEX" Text='<%#Eval("Cost")%>' HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChange" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpCostOpex" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Qty" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Qty" runat="server" ClientInstanceName="QtyOPEX" Text='<%#Eval("Qty")%>' HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpQtyOpex" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Total Cost" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="TotalCost" runat="server" ClientInstanceName="TotalCostOPEX" Text='<%#Eval("TotalCost")%>' ReadOnly="true" HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </dx:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dx:TabPage>
                                                                            </TabPages>
                                                                        </dx:ASPxPageControl>
                                                                    </div>
                                                                    <div style="text-align: right; padding: 2px">
                                                                        <dx:ASPxButton runat="server" Text="Save" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="updateOpex" />
                                                                        </dx:ASPxButton>
                                                                        <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e){OPEXGrid.CancelEdit();}" />
                                                                        </dx:ASPxButton>
                                                                    </div>
                                                                </EditForm>
                                                            </Templates>
                                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" ConfirmDelete="true" />
                                                            <SettingsText ConfirmDelete="Delete This Item?" />
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <dx:ASPxRoundPanel ID="ManpowerRoundPanel" runat="server" HeaderText="MANPOWER" EnableAnimation="true" ClientInstanceNam="ManpowerRoundPanel" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxGridView ID="ManPowerGrid" runat="server" ClientInstanceName="ManPowerGrid" Width="100%" Theme="Office2010Blue"
                                                            OnInitNewRow="ManPowerGrid_InitNewRow"
                                                            OnRowInserting="ManPowerGrid_RowInserting"
                                                            OnRowDeleting="ManPowerGrid_RowDeleting"
                                                            OnStartRowEditing="ManPowerGrid_StartRowEditing"
                                                            OnRowUpdating="ManPowerGrid_RowUpdating">
                                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'Manpower');}" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ActivityCode" Caption="Activity" VisibleIndex="3"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ManPowerTypeKey" Visible="false" VisibleIndex="4"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ManPowerTypeKeyName" Caption="Type" VisibleIndex="5"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Description" VisibleIndex="6"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="7"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="8" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="9" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="10" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                            </Columns>

                                                            <SettingsCommandButton>
                                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                                <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                                                <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                                            </SettingsCommandButton>
                                                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>

                                                            <Templates>
                                                                <EditForm>
                                                                    <div style="padding: 4px 3px 4px">
                                                                        <dx:ASPxPageControl ID="ManPowerPageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                                                            <TabPages>
                                                                                <dx:TabPage Text="ManPower" Visible="true">
                                                                                    <ContentCollection>
                                                                                        <dx:ContentControl runat="server">
                                                                                            <table style="width: 100%" border="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Activity Code" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="ActivityCode" runat="server" ClientInstanceName="ActivityCodeMAN" OnInit="ActivityCode_Init" AutoResizeWithContainer="true" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <%--<ClientSideEvents SelectedIndexChanged="ActivityCodeIndexChangeMAN" />--%>
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Type" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="ManPowerTypeKeyName" runat="server" ClientInstanceName="ManPowerTypeKeyNameMAN" Text='<%#Eval("ManPowerTypeKeyName")%>' OnInit="ManPowerTypeKey_Init" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Description" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Description" runat="server" ClientInstanceName="DescriptionMAN" Text='<%#Eval("Description")%>' Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="UOM" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="UOM" runat="server" ClientInstanceName="UOMMAN" Text='<%#Eval("UOM")%>' OnInit="UOM_Init" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Cost" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Cost" runat="server" ClientInstanceName="CostMAN" Text='<%#Eval("Cost")%>' HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChange" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpCostMan" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Qty" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Qty" runat="server" ClientInstanceName="QtyMAN" Text='<%#Eval("Qty")%>' Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpQtyMan" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Total Cost" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="TotalCost" runat="server" ClientInstanceName="TotalCostMAN" Text='<%#Eval("TotalCost")%>' ReadOnly="true" HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </dx:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dx:TabPage>
                                                                            </TabPages>
                                                                        </dx:ASPxPageControl>
                                                                    </div>
                                                                    <div style="text-align: right; padding: 2px">
                                                                        <dx:ASPxButton runat="server" Text="Save" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="updateManpower" />
                                                                        </dx:ASPxButton>
                                                                        <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e){ManPowerGrid.CancelEdit();}" />
                                                                        </dx:ASPxButton>
                                                                    </div>
                                                                </EditForm>
                                                            </Templates>
                                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" ConfirmDelete="true" />
                                                            <SettingsText ConfirmDelete="Delete This Item?" />
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <dx:ASPxRoundPanel ID="CapexRoundPanel" runat="server" HeaderText="CAPEX" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxGridView ID="CAPEXGrid" runat="server" ClientInstanceName="CAPEXGrid" Width="100%" Theme="Office2010Blue"
                                                            OnInitNewRow="CAPEXGrid_InitNewRow"
                                                            OnRowInserting="CAPEXGrid_RowInserting"
                                                            OnRowDeleting="CAPEXGrid_RowDeleting"
                                                            OnStartRowEditing="CAPEXGrid_StartRowEditing"
                                                            OnRowUpdating="CAPEXGrid_RowUpdating">
                                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'CAPEX');}" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Description" VisibleIndex="5"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="6"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="7" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="8" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="9" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                            </Columns>

                                                            <SettingsCommandButton>
                                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                                <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                                                <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                                            </SettingsCommandButton>
                                                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>

                                                            <Templates>
                                                                <EditForm>
                                                                    <div style="padding: 4px 3px 4px">
                                                                        <dx:ASPxPageControl ID="CAPEXPageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                                                            <TabPages>
                                                                                <dx:TabPage Text="CAPEX" Visible="true">
                                                                                    <ContentCollection>
                                                                                        <dx:ContentControl runat="server">
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Description" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Description" runat="server" ClientInstanceName="DescriptionCAPEX" Text='<%#Eval("Description")%>' Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="UOM" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxComboBox ID="UOM" runat="server" ClientInstanceName="UOMCAPEX" Text='<%#Eval("UOM")%>' OnInit="UOM_Init" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Cost" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Cost" runat="server" ClientInstanceName="CostCAPEX" Text='<%#Eval("Cost")%>' HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChange" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpCostCapex" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Qty" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="Qty" runat="server" ClientInstanceName="QtyCAPEX" Text='<%#Eval("Qty")%>' Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" />
                                                                                                            <ClientSideEvents KeyUp="OnKeyUpQtyCapex" />
                                                                                                            <ClientSideEvents KeyPress="FilterDigit" />
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel runat="server" Text="Total Cost" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxTextBox ID="TotalCost" runat="server" ClientInstanceName="TotalCostCAPEX" Text='<%#Eval("TotalCost")%>' ReadOnly="true" HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </dx:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dx:TabPage>
                                                                            </TabPages>
                                                                        </dx:ASPxPageControl>
                                                                    </div>
                                                                    <div style="text-align: right; padding: 2px">
                                                                        <dx:ASPxButton runat="server" Text="Save" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="updateCAPEX" />
                                                                        </dx:ASPxButton>
                                                                        <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e){CAPEXGrid.CancelEdit();}" />
                                                                        </dx:ASPxButton>
                                                                    </div>
                                                                </EditForm>
                                                            </Templates>
                                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" ConfirmDelete="true" />
                                                            <SettingsText ConfirmDelete="Delete This Item?" />
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5"></td>
                                    </tr>
                                </table>
                            <%--</div>--%>
                        </dx:ContentControl>
                    </ContentCollection>

                </dx:TabPage>
                <dx:TabPage Text="Revenue and Assumptions">
                    <ContentCollection>
                        <dx:ContentControl>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <dx:ASPxRoundPanel ID="RevenueRoundPanel" runat="server" HeaderText="REVENUE & ASSUMPTIONS" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                    <dx:ASPxGridView ID="RevenueGrid" runat="server" ClientInstanceName="RevenueGrid" Width="100%" Theme="Office2010Blue"
                                                        OnInitNewRow="RevenueGrid_InitNewRow"
                                                        OnRowInserting="RevenueGrid_RowInserting"
                                                        OnRowDeleting="RevenueGrid_RowDeleting"
                                                        OnStartRowEditing="RevenueGrid_StartRowEditing"
                                                        OnRowUpdating="RevenueGrid_RowUpdating">
                                                        <ClientSideEvents RowClick="function(s,e){focused(s,e,'Revenue');}" />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0"></dx:GridViewCommandColumn>
                                                            <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="3"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="FarmName" VisibleIndex="4"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Prize" VisibleIndex="5" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Volume" VisibleIndex="6" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="TotalPrize" VisibleIndex="7" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                                        </Columns>

                                                        <SettingsCommandButton>
                                                            <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                            <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                                            <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                                        </SettingsCommandButton>
                                                        <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>

                                                        <Templates>
                                                            <EditForm>
                                                                <div style="padding: 4px 3px 4px">
                                                                    <dx:ASPxPageControl ID="RevenuePageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                                                        <TabPages>
                                                                            <dx:TabPage Text="Revenue & Assumptions" Visible="true">
                                                                                <ContentCollection>
                                                                                    <dx:ContentControl runat="server">
                                                                                        <table style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dx:ASPxLabel runat="server" Text="Product Name" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxTextBox ID="ProductName" runat="server" ClientInstanceName="ProductNameRev" Text='<%#Eval("ProductName")%>' Width="170px" Theme="Office2010Blue"
                                                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                    </dx:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxLabel runat="server" Text="Farm Name" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxTextBox ID="FarmName" runat="server" ClientInstanceName="FarmNameRev" Text='<%#Eval("FarmName")%>' Width="170px" Theme="Office2010Blue"
                                                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                    </dx:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxLabel runat="server" Text="Prize" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxTextBox ID="Prize" runat="server" ClientInstanceName="PrizeRev" Text='<%#Eval("Prize")%>' HorizontalAlign="Right" Width="170px" Theme="Office2010Blue"
                                                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                    </dx:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dx:ASPxLabel runat="server" Text="Volume" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxTextBox ID="Volume" runat="server" ClientInstanceName="VolumeRev" Text='<%#Eval("Volume")%>' Width="170px" Theme="Office2010Blue"
                                                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                    </dx:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxLabel runat="server" Text="Total Prize" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dx:ASPxTextBox ID="TotalPrize" runat="server" ClientInstanceName="TotalPrizeRev" Text='<%#Eval("TotalPrize")%>' Width="170px" HorizontalAlign="Right" Theme="Office2010Blue"
                                                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                                                    </dx:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </dx:ContentControl>
                                                                                </ContentCollection>
                                                                            </dx:TabPage>
                                                                        </TabPages>
                                                                    </dx:ASPxPageControl>
                                                                </div>
                                                                <div style="text-align: right; padding: 2px">
                                                                    <dx:ASPxButton runat="server" Text="Save" Theme="Office2010Blue" AutoPostBack="false">
                                                                        <ClientSideEvents Click="updateRevenue" />
                                                                    </dx:ASPxButton>
                                                                    <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                                                        <ClientSideEvents Click="function(s,e){RevenueGrid.CancelEdit();}" />
                                                                    </dx:ASPxButton>
                                                                </div>
                                                            </EditForm>
                                                        </Templates>
                                                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                            AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" ConfirmDelete="true" />
                                                        <SettingsText ConfirmDelete="Delete This Item?" />
                                                    </dx:ASPxGridView>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxRoundPanel>
                                    </td>
                                </tr>
                            </table>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
</asp:Content>