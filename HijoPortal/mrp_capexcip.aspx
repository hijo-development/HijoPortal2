<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_capexcip.aspx.cs" Inherits="HijoPortal.mrp_capexcip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeaderPO">
            <h1>M O P  Details</h1>
            <table border="0" style="width: 100%;">
                <tr>
                    <td style="width: 2%">
                        <dx:ASPxLabel runat="server" Text="MRP MONTH YEAR" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%">:</td>
                    <td style="width: 20%">
                        <dx:ASPxComboBox ID="MRPmonthyear" runat="server" OnInit="MRPmonthyear_Init" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){CAPEXCIP.PerformCallback();}" />
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align: right">
                        <dx:ASPxButton ID="Submit" runat="server" Text="Submit" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                        &nbsp
                            <dx:ASPxButton ID="Preview" runat="server" Text="PREVIEW" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <dx:ASPxGridView ID="CAPEXCIP" ClientInstanceName="CAPEXCIP" runat="server" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
            OnStartRowEditing="CAPEXCIP_StartRowEditing"
            OnRowUpdating="CAPEXCIP_RowUpdating"
            OnBeforeGetCallbackResult="CAPEXCIP_BeforeGetCallbackResult"
            OnCustomCallback="CAPEXCIP_CustomCallback">
            <%--<ClientSideEvents RowClick="function(s,e){focused(s,e,'CAPEX');}" />--%>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="true"></dx:GridViewCommandColumn>
                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="CIPSIPNumber" Caption="Capex Number">
                    <EditItemTemplate>
                        <dx:ASPxTextBox ID="CIPSIPNumber" Text='<%#Eval("CIPSIPNumber")%>' runat="server" Width="170px" HorizontalAlign="Right" Theme="Office2010Blue">
                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                        </dx:ASPxTextBox>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="HeaderDocNum" Caption="Document Number">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("HeaderDocNum")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="CompanyName">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("CompanyName")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="BUName" Caption="Business Unit">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("BUName")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="RevDesc" Caption="Operating Unit">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("RevDesc")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Description">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("Description")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="UOM">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("UOM")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="ApprovedQty">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("ApprovedQty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="ApprovedCost">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("ApprovedCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="ApprovedTotalCost">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("ApprovedTotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
            <Styles>
                <Cell Wrap="False"></Cell>
            </Styles>
            <SettingsCommandButton>
                <EditButton Image-Url="images/Edit.ico" Image-AlternateText="Edit" Image-ToolTip="Edit Row" RenderMode="Image" Image-Width="15px"></EditButton>
                <UpdateButton Image-Url="images/Save.ico" Image-AlternateText="Update" Image-ToolTip="Update" RenderMode="Image" Image-Width="15px"></UpdateButton>
                <CancelButton Image-Url="images/Undo.ico" Image-AlternateText="Cancel" Image-ToolTip="Cancel" RenderMode="Image" Image-Width="15px"></CancelButton>
            </SettingsCommandButton>
            <SettingsEditing Mode="Inline"></SettingsEditing>

            <SettingsPager PageSize="10"></SettingsPager>
            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
        </dx:ASPxGridView>

        <%--<dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableHierarchyRecreation="true" Theme="Office2010Blue">
            <TabPages>
                <dx:TabPage>
                    <ContentCollection>
                        <dx:ContentControl>
                            <dx:ASPxRoundPanel ID="CapexRoundPanel" runat="server" Font-Bold="true" EnableAnimation="true" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Width="100%" Theme="Office2010Blue">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>--%>
    </div>
</asp:Content>
