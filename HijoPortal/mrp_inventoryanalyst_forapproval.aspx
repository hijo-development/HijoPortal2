<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_inventoryanalyst_forapproval.aspx.cs" Inherits="HijoPortal.mrp_inventoryanalyst_forapproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <dx:ASPxButton ID="Submit" runat="server" Text="Submit" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                        &nbsp
                            <dx:ASPxButton ID="MRPList" runat="server" Text="MOP LIST" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                        &nbsp
                            <dx:ASPxButton ID="Preview" runat="server" Text="PREVIEW" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
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
                                        <dx:ASPxGridView ID="DMGridInventApproval" runat="server" ClientInstanceName="DMGridInventApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue" 
                                            OnStartRowEditing="DMGridInventApproval_StartRowEditing"
                                            OnRowUpdating="DMGridInventApproval_RowUpdating" 
                                            OnBeforeGetCallbackResult="DMGridInventApproval_BeforeGetCallbackResult"
                                            OnDataBound="DMGridInventApproval_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'Materials');}" />

                                            <Columns>
                                                <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="0"></dx:GridViewCommandColumn>
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
                                                        <dx:ASPxTextBox ID="EdittedQty" ClientInstanceName="InvAppEditQtyDM" runat="server" Text='<%#Eval("EdittedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditQtyDM" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittedCost" ClientInstanceName="InvAppEditCostDM" runat="server" Text='<%#Eval("EdittedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditCostDM" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittiedTotalCost" VisibleIndex="13">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittiedTotalCost" ClientInstanceName="InvAppEditTotalDM" runat="server" Text='<%#Eval("EdittiedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsCommandButton>
                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                <UpdateButton ButtonType="Image" Image-Url="images/Save.ico" Image-Width="15px"></UpdateButton>
                                                <CancelButton ButtonType="Image" Image-Url="images/Undo.ico" Image-Width="15px"></CancelButton>
                                            </SettingsCommandButton>
                                            <SettingsEditing Mode="Inline"></SettingsEditing>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false"/>
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                            <dx:ASPxRoundPanel ID="OpexRoundPanel" runat="server" HeaderText="OPEX" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="OPGridInventApproval" runat="server" ClientInstanceName="OPGridInventApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue" 
                                            OnStartRowEditing="OPGridInventApproval_StartRowEditing" 
                                            OnRowUpdating="OPGridInventApproval_RowUpdating"
                                            OnBeforeGetCallbackResult="OPGridInventApproval_BeforeGetCallbackResult"
                                            OnDataBound="OPGridInventApproval_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'OPEX');}" />

                                            <Columns>
                                                <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="0"></dx:GridViewCommandColumn>
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
                                                        <dx:ASPxTextBox ID="EdittedQty" ClientInstanceName="InvAppEditQtyOP" runat="server" Text='<%#Eval("EdittedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditQtyOP" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittedCost" ClientInstanceName="InvAppEditCostOP" runat="server" Text='<%#Eval("EdittedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditCostOP" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedTotalCost" VisibleIndex="13">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittedTotalCost" ClientInstanceName="InvAppEditTotalOP" runat="server" Text='<%#Eval("EdittedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsCommandButton>
                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                <UpdateButton ButtonType="Image" Image-Url="images/Save.ico" Image-Width="15px"></UpdateButton>
                                                <CancelButton ButtonType="Image" Image-Url="images/Undo.ico" Image-Width="15px"></CancelButton>
                                            </SettingsCommandButton>
                                            <SettingsEditing Mode="Inline"></SettingsEditing>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false"/>
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                            <dx:ASPxRoundPanel ID="ManpowerRoundPanel" runat="server" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="MANGridInventApproval" runat="server" ClientInstanceName="MANGridInventApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue" 
                                            OnStartRowEditing="MANGridInventApproval_StartRowEditing"
                                            OnRowUpdating="MANGridInventApproval_RowUpdating"
                                            OnBeforeGetCallbackResult="MANGridInventApproval_BeforeGetCallbackResult"
                                            OnDataBound="MANGridInventApproval_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'Manpower');}" />

                                            <Columns>
                                                <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="0"></dx:GridViewCommandColumn>
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
                                                        <dx:ASPxTextBox ID="EdittedQty" ClientInstanceName="InvAppEditQtyMAN" runat="server" Text='<%#Eval("EdittedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditQtyMAN" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittedCost" ClientInstanceName="InvAppEditCostMAN" runat="server" Text='<%#Eval("EdittedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditCostMAN" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittiedTotalCost" VisibleIndex="13">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittiedTotalCost" ClientInstanceName="InvAppEditTotalMAN" runat="server" Text='<%#Eval("EdittiedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsCommandButton>
                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                <UpdateButton ButtonType="Image" Image-Url="images/Save.ico" Image-Width="15px"></UpdateButton>
                                                <CancelButton ButtonType="Image" Image-Url="images/Undo.ico" Image-Width="15px"></CancelButton>
                                            </SettingsCommandButton>
                                            <SettingsEditing Mode="Inline"></SettingsEditing>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false"/>
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                             <dx:ASPxRoundPanel ID="CapexRoundPanel" runat="server" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="CAGridInventApproval" runat="server" ClientInstanceName="CAGridInventApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue" 
                                            OnStartRowEditing="CAGridInventApproval_StartRowEditing" 
                                            OnRowUpdating="CAGridInventApproval_RowUpdating"
                                            OnBeforeGetCallbackResult="CAGridInventApproval_BeforeGetCallbackResult"
                                            OnDataBound="CAGridInventApproval_DataBound">
                                            <ClientSideEvents RowClick="function(s,e){focused(s,e,'CAPEX');}" />

                                            <Columns>
                                                <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="0"></dx:GridViewCommandColumn>
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
                                                        <dx:ASPxTextBox ID="EdittedQty" ClientInstanceName="InvAppEditQtyCA" runat="server" Text='<%#Eval("EdittedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditQtyCA" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittedCost" VisibleIndex="11">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittedCost" ClientInstanceName="InvAppEditCostCA" runat="server" Text='<%#Eval("EdittedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                            <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpInvAppEditCostCA" />
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EdittiedTotalCost" VisibleIndex="12">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="EdittiedTotalCost" ClientInstanceName="InvAppEditTotalCA" runat="server" Text='<%#Eval("EdittiedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>

                                            <SettingsCommandButton>
                                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                <UpdateButton ButtonType="Image" Image-Url="images/Save.ico" Image-Width="15px"></UpdateButton>
                                                <CancelButton ButtonType="Image" Image-Url="images/Undo.ico" Image-Width="15px"></CancelButton>
                                            </SettingsCommandButton>
                                            <SettingsEditing Mode="Inline"></SettingsEditing>

                                            <SettingsPager PageSize="10"></SettingsPager>
                                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false"/>
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
