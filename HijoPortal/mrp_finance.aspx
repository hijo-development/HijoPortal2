<%@ Page Title="Budget" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_finance.aspx.cs" Inherits="HijoPortal.mrp_finance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPopupControl ID="PopupSubmit" ClientInstanceName="PopupSubmit" runat="server" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="2" style="padding-right: 20px; padding-bottom: 20px;">
                            <dx:ASPxLabel runat="server" Text="Are you sure you want to submit this document?" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="OK_SUBMIT" runat="server" Text="SUBMIT" Theme="Office2010Blue" OnClick="Submit_Click" AutoPostBack="false">
                                <%--<ClientSideEvents Click="OK_DELETE" />--%>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CANCEL_SUBMIT" runat="server" Text="CANCEL" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){PopupSubmit.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <div id="dvContentWrapper" runat="server" class="ContentWrapper">

        <div id="dvHeader">
            <h1>M O P  Details</h1>
            <table border="0">
                <tr>
                    <td style="width: 12%">
                        <dx:ASPxLabel runat="server" Text="MRP Number" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td colspan="4">
                        <dx:ASPxLabel ID="DocNum" runat="server" Text="" Theme="Office2010Blue" Style="font-size: medium; font-weight: bold; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></dx:ASPxLabel>
                    </td>
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
                        <dx:ASPxLabel ID="Month" runat="server" Text="" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
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
                        <dx:ASPxLabel runat="server" ID="Status" Text="" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td></td>
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
                        <%--OnClick="Submit_Click"--%>
                        <dx:ASPxButton ID="Submit" runat="server" Text="Submit" AutoPostBack="false" Theme="Office2010Blue">
                            <ClientSideEvents Click="function(s,e){PopupSubmit.SetHeaderText('Confirm'); PopupSubmit.Show();}" />
                        </dx:ASPxButton>
                        &nbsp
                            <dx:ASPxButton ID="Preview" runat="server" Text="PREVIEW" OnClick="Preview_Click" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableHierarchyRecreation="true" Theme="Office2010Blue">
            <TabPages>
                <dx:TabPage Text="MRP">
                    <ContentCollection>
                        <dx:ContentControl>
                            <dx:ASPxRoundPanel ID="DirectMaterialsRoundPanel" runat="server" HeaderText="DIRECT MATERIALS" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="DMGridFinance" runat="server" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                            OnDataBound="DMGridFinance_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'Materials');}" />

                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ActivityCode" Caption="Activity" VisibleIndex="3">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("ActivityCode")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="RevDesc" Caption="Operating Unit" VisibleIndex="4">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("RevDesc")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ItemCode" VisibleIndex="5">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("ItemCode")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ItemDescription" VisibleIndex="6">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("ItemDescription")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="7">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("UOM")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="8">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Cost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="9">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Qty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="10">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedQty" VisibleIndex="11">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedQty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittiedTotalCost" VisibleIndex="13">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittiedTotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                            <dx:ASPxRoundPanel ID="OpexRoundPanel" runat="server" HeaderText="OPEX" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="OPGridFinance" runat="server" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                            OnDataBound="OPGridFinance_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'OPEX');}" />

                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ExpenseCodeName" Caption="Activity" VisibleIndex="3">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("ExpenseCodeName")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="RevDesc" Caption="Operating Unit" VisibleIndex="4">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("RevDesc")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ItemCode" VisibleIndex="5">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("ItemCode")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Description" VisibleIndex="6">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Description")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="7">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("UOM")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="8">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Cost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="9">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Qty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="10">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedQty" VisibleIndex="11">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedQty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedTotalCost" VisibleIndex="13">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedTotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                            <dx:ASPxRoundPanel ID="ManpowerRoundPanel" runat="server" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="ManPoGridFinance" runat="server" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                            OnDataBound="ManPoGridFinance_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'Manpower');}" />

                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ActivityCode" Caption="Activity" VisibleIndex="3">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("ActivityCode")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="RevDesc" Caption="Operating Unit" VisibleIndex="4">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("RevDesc")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ManPowerTypeKeyName" Caption="Type" VisibleIndex="5">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("ManPowerTypeKeyName")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Description" VisibleIndex="6">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Description")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="7">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("UOM")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="8">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Cost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="9">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Qty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="10">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedQty" VisibleIndex="11">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedQty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittiedTotalCost" VisibleIndex="13">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittiedTotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                            <dx:ASPxRoundPanel ID="CapexRoundPanel" runat="server" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="CAGridFinance" runat="server" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                            OnDataBound="CAGridFinance_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'CAPEX');}" />
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="HeaderDocNum" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Description" VisibleIndex="4">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Description")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="RevDesc" Caption="Operating Unit" VisibleIndex="5">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("RevDesc")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="UOM" VisibleIndex="6">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("UOM")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Cost" VisibleIndex="7">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Cost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="8">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("Qty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="TotalCost" VisibleIndex="9">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedQty" VisibleIndex="10">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedQty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="11">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittedCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittiedTotalCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxLabel runat="server" Text='<%#Eval("EdittiedTotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
</asp:Content>
