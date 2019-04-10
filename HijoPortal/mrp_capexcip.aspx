<%@ Page Title="CAPEX ID Assignment" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_capexcip.aspx.cs" Inherits="HijoPortal.mrp_capexcip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function MRPmonthyear_SelectedIndexChanged(s, e) {
            EntityComboClient.SetText("");
            BUComboClient.SetText("");

            EntityComboClient.SetEnabled(false);
            BUComboClient.SetEnabled(false);

            EntityCallbackClient.PerformCallback();
        }

        function EntityCombo_SelectedIndexChanged(s, e) {
            BUCallbackClient.PerformCallback();
        }

        function BUCombo_SelectedIndexChanged(s, e) {
            CAPEXCIP.PerformCallback();
        }

    </script>
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
                            <dx:ASPxButton ID="OK_SUBMIT" runat="server" Text="SUBMIT" Theme="Office2010Blue" AutoPostBack="false">
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
        <div id="dvHeaderPO">
            <h1>for CAPEX ID Assignment</h1>
            <table border="0" style="width: 100%;">
                <tr>
                    <td style="width: 10%">
                        <dx:ASPxLabel runat="server" Text="MRP MONTH YEAR" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%">:</td>
                    <td style="width: 20%">
                        <dx:ASPxComboBox ID="MRPmonthyear" runat="server" OnInit="MRPmonthyear_Init" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="MRPmonthyear_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <dx:ASPxLabel runat="server" Text="Entity" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%">:</td>
                    <td style="width: 20%">
                        <dx:ASPxCallbackPanel ID="EntityCallback" runat="server" ClientInstanceName="EntityCallbackClient" OnCallback="EntityCallback_Callback" Width="200px">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxComboBox ID="EntityCombo" runat="server" ClientInstanceName="EntityComboClient" OnInit="EntityCombo_Init" ValueType="System.String" Theme="Office2010Blue">
                                        <ClientSideEvents SelectedIndexChanged="EntityCombo_SelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                    </td>

                    <td>
                        <dx:ASPxLabel runat="server" Text="SSU/BU" CssClass="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxCallbackPanel ID="BUCallback" runat="server" ClientInstanceName="BUCallbackClient" OnCallback="BUCallback_Callback" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxComboBox ID="BUCombo" runat="server" ClientInstanceName="BUComboClient" ValueType="System.String" Theme="Office2010Blue">
                                        <ClientSideEvents SelectedIndexChanged="BUCombo_SelectedIndexChanged" />
                                    </dx:ASPxComboBox>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                    </td>
                </tr>

                <%--<tr>
                    <td colspan="7" style="text-align: right; padding-right: 5px;">
                        <dx:ASPxButton ID="Submit" runat="server" Text="Submit" AutoPostBack="false" Theme="Office2010Blue">
                            <ClientSideEvents Click="function(s,e){PopupSubmit.SetHeaderText('Confirm'); PopupSubmit.Show();}" />
                        </dx:ASPxButton>
                    </td>
                </tr>--%>
            </table>
        </div>

        <dx:ASPxGridView ID="CAPEXCIP" ClientInstanceName="CAPEXCIP" runat="server" EnableCallBacks="True" Width="100%" Theme="Office2010Blue"
            OnStartRowEditing="CAPEXCIP_StartRowEditing"
            OnRowUpdating="CAPEXCIP_RowUpdating"
            OnBeforeGetCallbackResult="CAPEXCIP_BeforeGetCallbackResult"
            OnCustomCallback="CAPEXCIP_CustomCallback">
            <%--<ClientSideEvents RowClick="function(s,e){focused(s,e,'CAPEX');}" />--%>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="true" Width="30" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
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
                <dx:GridViewDataColumn FieldName="CompanyName" Caption="Entity">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("CompanyName")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="BUName" Caption="BU/SSU">
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

                <dx:GridViewDataColumn FieldName="Qty" Caption="Requested Qty" CellStyle-HorizontalAlign="Right">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("Qty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Cost" Caption="Cost" CellStyle-HorizontalAlign="Right">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("Cost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="TotalCost" Caption="Total Cost" CellStyle-HorizontalAlign="Right">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("TotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>

                <dx:GridViewDataColumn FieldName="ApprovedQty" Caption="Recommended Qty" CellStyle-HorizontalAlign="Right">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("ApprovedQty")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="ApprovedCost" Caption="Cost" CellStyle-HorizontalAlign="Right">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("ApprovedCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="ApprovedTotalCost" Caption="Total Cost" CellStyle-HorizontalAlign="Right">
                    <EditItemTemplate>
                        <dx:ASPxLabel runat="server" Text='<%#Eval("ApprovedTotalCost")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
            <Styles>
                <Cell Wrap="false"></Cell>
                <InlineEditCell Wrap="False"></InlineEditCell>
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
