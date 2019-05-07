<%@ Page Title="MOP Preview" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_preview_inventanalyst.aspx.cs" Inherits="HijoPortal.mrp_preview_inventanalyst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Preview.css" rel="stylesheet" />
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

    <dx:ASPxPopupControl ID="MRPNotify" ClientInstanceName="MRPNotify" runat="server" Modal="true" CloseAction="CloseButton" PopupAnimationType="Fade" CloseAnimationType="Fade"  PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Moderno" Width="100%">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxLabel ID="MRPNotificationMessage" ClientInstanceName="MRPNotificationMessage" runat="server" Text="" Theme="Moderno" ForeColor="Red" Width="300"></dx:ASPxLabel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="PopupSubmitPreviewAnal" ClientInstanceName="PopupSubmitPreviewAnal" runat="server" Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade"  PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Moderno" Width="100%">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="2" style="padding-right: 20px; padding-bottom: 20px;">
                            <dx:ASPxLabel runat="server" Text="Are you sure you want to submit this document?" Theme="Moderno" Width="300"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="OK_SUBMIT" runat="server" Text="SUBMIT" Theme="Moderno" OnClick="OK_SUBMIT_Click" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){
                                    PopupSubmitPreviewAnal.Hide();
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CANCEL_SUBMIT" runat="server" Text="CANCEL" Theme="Moderno" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){PopupSubmitPreviewAnal.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" BackColor="White" ScrollBars="Auto" Height="100%" Width="100%" Paddings-Padding="5">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxPanel ID="ASPxPanel2" runat="server" Width="100%" Paddings-PaddingBottom="10">
                    <PanelCollection>
                        <dx:PanelContent>
                            <div id="dvHeader" style="height: 150px; background-color: #ffffff; padding: 5px 5px 0px 0px; border-radius: 2px;">
                                <%--<h1>M O P  Preview (Inventory Analyst)</h1>--%>
                                <h1 id="mrpHead" runat="server"></h1>
                                <table style="width: 100%; margin: auto;" border="0">
                                    <tr>
                                        <td style="width: 12%">
                                            <dx:ASPxLabel runat="server" Text="MRP Number" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>:</td>
                                        <td colspan="4">
                                            <dx:ASPxLabel ID="DocNum" runat="server" Text="" Theme="Office2010Blue" Style="font-size: medium; font-weight: bold; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></dx:ASPxLabel>
                                        </td>
                                        <td rowspan="3" style="width: 40%; text-align: right; vertical-align: bottom;">
                                            <div style="display: none;">
                                                <dx:ASPxHiddenField ID="WrkFlowHidden" runat="server" ClientInstanceName="WrkFlowHiddenAnal"></dx:ASPxHiddenField>
                                                <dx:ASPxHiddenField ID="StatusHidden" runat="server" ClientInstanceName="StatusHiddenAnal"></dx:ASPxHiddenField>
                                            </div>
                                            <dx:ASPxButton ID="btAddEdit" runat="server" Text="MOP Details" OnClick="btAddEdit_Click" AutoPostBack="false" Theme="Office2010Blue"></dx:ASPxButton>
                                            <dx:ASPxButton ID="Submit" runat="server" Text="Submit" AutoPostBack="false" Theme="Office2010Blue">
                                                <ClientSideEvents Click="Preview_Submit_Analyst_Click" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Month" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>:</td>
                                        <td style="width: 20%">
                                            <dx:ASPxLabel ID="Month" runat="server" Text="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 8%">
                                            <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>:</td>
                                        <td style="width: 20%">
                                            <dx:ASPxLabel ID="EntityCode" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Year" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <dx:ASPxLabel ID="Year" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Department" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <dx:ASPxLabel ID="BUCode" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Creator" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <dx:ASPxLabel ID="Creator" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Status" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <dx:ASPxLabel ID="Status" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>


                <table style="width: inherit; margin-bottom: 10px;">
                    <tr>
                        <td style="vertical-align: top; text-align: left; width: 49%">
                            <dx:ASPxRoundPanel ID="RevRoundPanel" runat="server" HeaderText="Revenue and Assumptions" ShowCollapseButton="false" Width="100%" Theme="Glass">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="GridPreviewREV" runat="server" OnDataBound="GridPreviewREV_DataBound" Theme="Office2010Silver" Width="100%">
                                            <Border BorderStyle="None" />
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <%--<SettingsBehavior AllowDragDrop="false" />--%>
                                            <Settings ShowGroupPanel="true" ShowFooter="true" />
                                            <GroupSummary>
                                                <dx:ASPxSummaryItem FieldName="TotalPrize" SummaryType="Sum" ShowInColumn="OperatingUnit" DisplayFormat="Total: {0:0,0.00}" />
                                            </GroupSummary>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="TotalPrize" ShowInColumn="TotalPrize" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                                <dx:ASPxSummaryItem FieldName="RecTotalCost" ShowInColumn="RecTotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                            </TotalSummary>
                                            <Styles>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                        </td>
                        <td style="width: 2%;"></td>
                        <td style="vertical-align: top; text-align: right; width: 49%">
                            <dx:ASPxRoundPanel ID="TotalRoundPanel" runat="server" HeaderText="Total Summary" ShowCollapseButton="false" Width="100%" Theme="Glass">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxGridView ID="GridPreviewSummary" runat="server" OnDataBound="GridPreviewSummary_DataBound" Theme="Office2010Silver" Width="100%">
                                            <Border BorderStyle="None" />
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <%--<SettingsBehavior AllowDragDrop="false" />--%>
                                            <Settings ShowFooter="true" />
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="Total" ShowInColumn="Total" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                                <dx:ASPxSummaryItem FieldName="RecTotalCost" ShowInColumn="RecTotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                            </TotalSummary>
                                            <Styles>
                                                <Cell HorizontalAlign="Left"></Cell>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                        </td>
                    </tr>
                </table>
                <div style="height: 10px;"></div>
                <dx:ASPxRoundPanel ID="DMRoundPanel" runat="server" HeaderText="Direct Material" ShowCollapseButton="false" Width="100%" Theme="Glass">
                    <PanelCollection>
                        <dx:PanelContent>
                            <dx:ASPxGridView ID="GridPreviewDM" runat="server" OnDataBound="GridPreviewDM_DataBound" Theme="Office2010Silver" Width="100%">
                                <Border BorderStyle="None" />
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                <%--<SettingsBehavior AllowDragDrop="false" />--%>
                                <SettingsBehavior AllowFocusedRow="true" />
                                <Settings ShowGroupPanel="true" ShowFooter="true" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="OperatingUnit" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="Expense" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="Activity" DisplayFormat="Total: {0:0,0.00}" />
                                </GroupSummary>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" ShowInColumn="TotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="RecTotalCost" ShowInColumn="RecTotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                </TotalSummary>
                                <Styles>
                                    <Header Wrap="True"></Header>
                                    <Footer HorizontalAlign="Right"></Footer>
                                </Styles>
                            </dx:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>


                <div style="height: 10px;"></div>
                <dx:ASPxRoundPanel ID="OPRoundPanel" runat="server" HeaderText="Operating Expenditure" ShowCollapseButton="false" Width="100%" Theme="Glass">
                    <PanelCollection>
                        <dx:PanelContent>
                            <dx:ASPxGridView ID="GridPreviewOP" runat="server" OnDataBound="GridPreviewOP_DataBound" Theme="Office2010Silver" Width="100%">
                                <Border BorderStyle="None" />
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                <%--<SettingsBehavior AllowDragDrop="false" />--%>
                                <SettingsBehavior AllowFocusedRow="true" />
                                <Settings ShowGroupPanel="true" ShowFooter="true" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="OperatingUnit" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="Expense" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="ProcurementCategory" DisplayFormat="Total: {0:0,0.00}" />
                                </GroupSummary>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" ShowInColumn="TotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="RecTotalCost" ShowInColumn="RecTotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                </TotalSummary>
                                <Styles>
                                    <Header Wrap="True"></Header>
                                    <Footer HorizontalAlign="Right"></Footer>
                                </Styles>
                            </dx:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>


                <div style="height: 10px;"></div>
                <dx:ASPxRoundPanel ID="MANRoundPanel" runat="server" HeaderText="Manpower" ShowCollapseButton="false" Width="100%" Theme="Glass">
                    <PanelCollection>
                        <dx:PanelContent>
                            <dx:ASPxGridView ID="GridPreviewMAN" runat="server" OnDataBound="GridPreviewMAN_DataBound" Theme="Office2010Silver" Width="100%">
                                <Border BorderStyle="None" />
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                <%--<SettingsBehavior AllowDragDrop="false" />--%>
                                <SettingsBehavior AllowFocusedRow="true" />
                                <Settings ShowGroupPanel="true" ShowFooter="true" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="OperatingUnit" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="Activity" DisplayFormat="Total: {0:0,0.00}" />
                                </GroupSummary>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" ShowInColumn="TotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="RecTotalCost" ShowInColumn="RecTotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                </TotalSummary>
                                <Styles>
                                    <Header Wrap="True"></Header>
                                    <Footer HorizontalAlign="Right"></Footer>
                                </Styles>
                            </dx:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>

                <div style="height: 10px;"></div>
                <dx:ASPxRoundPanel ID="CARoundPanel" runat="server" HeaderText="Capital Expenditure" ShowCollapseButton="false" Width="100%" Theme="Glass">
                    <PanelCollection>
                        <dx:PanelContent>
                            <dx:ASPxGridView ID="GridPreviewCA" runat="server" OnDataBound="GridPreviewCA_DataBound" Theme="Office2010Silver" Width="100%">
                                <Border BorderStyle="None" />
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                <%--<SettingsBehavior AllowDragDrop="false" />--%>
                                <SettingsBehavior AllowFocusedRow="true" />
                                <Settings ShowGroupPanel="true" ShowFooter="true" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="OperatingUnit" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="Expense" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" ShowInColumn="ProcurementCategory" DisplayFormat="Total: {0:0,0.00}" />
                                </GroupSummary>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="TotalCost" ShowInColumn="TotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                    <dx:ASPxSummaryItem FieldName="RecTotalCost" ShowInColumn="RecTotalCost" SummaryType="Sum" DisplayFormat="Total: {0:0,0.00}" />
                                </TotalSummary>
                                <Styles>
                                    <Header Wrap="True"></Header>
                                    <Footer HorizontalAlign="Right"></Footer>
                                </Styles>
                            </dx:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
