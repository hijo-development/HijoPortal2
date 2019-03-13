<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_forapproval.aspx.cs" Inherits="HijoPortal.mrp_forapproval" %>

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
                            <dx:ASPxButton ID="MRPList" runat="server" OnClick="MRPList_Click" Text="MOP LIST" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                    &nbsp
                            <dx:ASPxButton ID="Preview" runat="server" OnClick="Preview_Click" Text="PREVIEW" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableHierarchyRecreation="true" Theme="Office2010Blue">
        <TabPages>
            <dx:TabPage Text="MRP">
                <ContentCollection>
                    <dx:ContentControl>
                        <dx:ASPxRoundPanel ID="DirectMaterialsRoundPanel" runat="server" ClientInstanceName="DMGridAppRoundPanel" HeaderText="DIRECT MATERIALS" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                            <ClientSideEvents CollapsedChanging="DMGridAppRoundPanel_CollapsedChanging" />
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="DMGridApproval" runat="server" ClientInstanceName="DMGridApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                        OnStartRowEditing="DMGridApproval_StartRowEditing"
                                        OnRowUpdating="DMGridApproval_RowUpdating"
                                        OnBeforeGetCallbackResult="DMGridApproval_BeforeGetCallbackResult"
                                        OnDataBound="DMGridApproval_DataBound">
                                        <ClientSideEvents RowClick="function(s,e){focused(s,e,'Materials');}" />
                                        <ClientSideEvents CustomButtonClick="DMGridApproval_CustomButtonClick" />

                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="DMAppEdit" Image-AlternateText="Edit" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px">
                                                        <Image AlternateText="Edit" ToolTip="Edit Row" Width="15px" Url="Images/Edit.ico"></Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
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

                                            <dx:GridViewDataColumn FieldName="ApprovedQty" VisibleIndex="14">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedQtyDM" ClientInstanceName="ApprovedQtyDM" runat="server" Text='<%#Eval("ApprovedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpApprovedQtyDirect" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedCost" VisibleIndex="15">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedCostDM" ClientInstanceName="ApprovedCostDM" runat="server" Text='<%#Eval("ApprovedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpApprovedCostDirect" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedTotalCost" VisibleIndex="16">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedTotalCostDM" ClientInstanceName="ApprovedTotalCostDM" runat="server" Text='<%#Eval("ApprovedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
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
                                            AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                        <dx:ASPxRoundPanel ID="OpexRoundPanel" runat="server" ClientInstanceName="OPGridAppRoundPanel" HeaderText="OPEX" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                            <ClientSideEvents CollapsedChanging="OPGridAppRoundPanel_CollapsedChanging" />
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="OpexGridApproval" runat="server" ClientInstanceName="OpexGridApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                        OnStartRowEditing="OpexGridApproval_StartRowEditing"
                                        OnRowUpdating="OpexGridApproval_RowUpdating"
                                        OnBeforeGetCallbackResult="OpexGridApproval_BeforeGetCallbackResult"
                                        OnDataBound="OpexGridApproval_DataBound">
                                        <ClientSideEvents RowClick="function(s,e){focused(s,e,'OPEX');}" />
                                        <ClientSideEvents CustomButtonClick="OpexGridApproval_CustomButtonClick" />

                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="OPAppEdit" Image-AlternateText="Edit" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px">
                                                        <Image AlternateText="Edit" ToolTip="Edit Row" Width="15px" Url="Images/Edit.ico"></Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
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

                                            <dx:GridViewDataColumn FieldName="ApprovedQty" VisibleIndex="14">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedQtyOpex" ClientInstanceName="ApprovedQtyOpex" runat="server" Text='<%#Eval("ApprovedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpQtyApprovedQtyOpex" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedCost" VisibleIndex="15">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedCostOpex" ClientInstanceName="ApprovedCostOpex" runat="server" Text='<%#Eval("ApprovedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpCostApprovedCostOpex" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedTotalCost" VisibleIndex="16">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedTotalCostOpex" ClientInstanceName="ApprovedTotalCostOpex" runat="server" Text='<%#Eval("ApprovedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
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
                                            AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                        <dx:ASPxRoundPanel ID="ManpowerRoundPanel" runat="server" ClientInstanceName="MANGridAppRoundPanel" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                            <ClientSideEvents CollapsedChanging="MANGridAppRoundPanel_CollapsedChanging" />
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="ManPowerGridApproval" runat="server" ClientInstanceName="ManPowerGridApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                        OnStartRowEditing="ManPowerGridApproval_StartRowEditing"
                                        OnRowUpdating="ManPowerGridApproval_RowUpdating"
                                        OnBeforeGetCallbackResult="ManPowerGridApproval_BeforeGetCallbackResult"
                                        OnDataBound="ManPowerGridApproval_DataBound">
                                        <ClientSideEvents RowClick="function(s,e){focused(s,e,'Manpower');}" />
                                        <ClientSideEvents CustomButtonClick="ManPowerGridApproval_CustomButtonClick" />

                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="MANAppEdit" Image-AlternateText="Edit" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px">
                                                        <Image AlternateText="Edit" ToolTip="Edit Row" Width="15px" Url="Images/Edit.ico"></Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
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
                                            <dx:GridViewDataColumn FieldName="ApprovedQty" VisibleIndex="14">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedQtyManPower" ClientInstanceName="ApprovedQtyManPower" runat="server" Text='<%#Eval("ApprovedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpApprovedQtyManPower" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedCost" VisibleIndex="15">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedCostManPower" ClientInstanceName="ApprovedCostManPower" runat="server" Text='<%#Eval("ApprovedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpApprovedCostManPower" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedTotalCost" VisibleIndex="16">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedTotalCostManPower" ClientInstanceName="ApprovedTotalCostManPower" runat="server" Text='<%#Eval("ApprovedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
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
                                            AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>
                        <dx:ASPxRoundPanel ID="CapexRoundPanel" runat="server" ClientInstanceName="CAGridAppRoundPanel" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                            <ClientSideEvents CollapsedChanging="CAGridAppRoundPanel_CollapsedChanging" />
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxGridView ID="CapexGridApproval" runat="server" ClientInstanceName="CapexGridApproval" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
                                        OnStartRowEditing="CapexGridApproval_StartRowEditing"
                                        OnRowUpdating="CapexGridApproval_RowUpdating"
                                        OnBeforeGetCallbackResult="CapexGridApproval_BeforeGetCallbackResult"
                                        OnDataBound="CapexGridApproval_DataBound">
                                        <ClientSideEvents RowClick="function(s,e){focused(s,e,'CAPEX');}" />
                                        <ClientSideEvents CustomButtonClick="CapexGridApproval_CustomButtonClick" />

                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="CAAppEdit" Image-AlternateText="Edit" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px">
                                                        <Image AlternateText="Edit" ToolTip="Edit Row" Width="15px" Url="Images/Edit.ico"></Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
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
                                            <dx:GridViewDataColumn FieldName="ApprovedQty" VisibleIndex="13">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedQtyCapex" ClientInstanceName="ApprovedQtyCapex" runat="server" Text='<%#Eval("ApprovedQty") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChangeQty" KeyPress="FilterDigit" KeyUp="OnKeyUpApprovedQtyCapex" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedCost" VisibleIndex="14">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedCostCapex" ClientInstanceName="ApprovedCostCapex" runat="server" Text='<%#Eval("ApprovedCost") %>' Width="120px" Theme="Office2010Blue" HorizontalAlign="Right">
                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="OnValueChange" KeyPress="FilterDigit" KeyUp="OnKeyUpApprovedCostCapex" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ApprovedTotalCost" VisibleIndex="15">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="ApprovedTotalCostCapex" ClientInstanceName="ApprovedTotalCostCapex" runat="server" Text='<%#Eval("ApprovedTotalCost") %>' Width="120px" Border-BorderColor="Transparent" Theme="Office2010Blue">
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
</asp:Content>
