<%@ Page Title="Home" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="HijoPortal.home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Home.css" rel="stylesheet" />
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



    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <PanelCollection>
            <dx:PanelContent>
                <div id="divWelcome" runat="server">
                    <table id="tblWelcome" style="width: 100%; height: 625px;">
                        <tr style="width: 100%; height: 100%;">
                            <%--<td style="width: 300px; vertical-align: top; padding: 10px;">--%>
                            <td style="vertical-align: top; padding: 10px;">
                                <h4 style="color:darkgreen;">Vision: </h4>
                                <p><b>HIJO</b> nurtures nature today to benefit the generation of tomorrow.</p>
                                <br />
                                <h4 style="color:darkblue;">Mission:</h4>
                                <p>To build enterprises in <b>Agribusiness</b>, <b>Port & Logistics</b>, <b>Property Development</b>, <b>Leisure & Tourism</b> & <b>Renewable Energy</b> in line with the <b>FELICE</b> principle.</p>
                                <br />
                                <h4 style="color:darkgreen;">Core Values:</h4>
                                <p>We aim for <b>EXCELLENCE</b>. We work as a <b>TEAM</b>. We have <b>INTEGRITY</b>. We are <b>STEWARDS</b> of God's creation. We are <b>ENTREPRENEURS</b>. We will <b>GROW</b> while having <b>FUN</b>.</p>
                            </td>
                            <%--<td style="width: 100px"></td>--%>
                            <%--<td style="width: 700px; background-image: url('../images/banner.png'); background-size: 100%; background-repeat: no-repeat;"></td>--%>
                            <%--<td style="width: 700px;"></td>--%>
                        </tr>
                    </table>
                </div>

                <div id="divWorkAssigned" runat="server">
                    <div id="dvHeader">
                        <h1>Work Assigned to me . . .</h1>
                    </div>
                    <div>
                        <dx:ASPxGridView ID="HomeGrid" runat="server" Theme="Office2010Blue" Width="100%" 
                            EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                            Style="margin: 0 auto;" OnCustomButtonCallback ="HomeGrid_CustomButtonCallback">
                            <%--<ClientSideEvents CustomButtonClick="function(s,e){
                                                $find('ModalPopupExtenderLoading').show();
                                                e.processOnServer = true;
                                                }" />--%>
                            <%--<ClientSideEvents BeginCallback="function(s,e){
                                                $find('ModalPopupExtenderLoading').show();
                                                }" />--%>
                            <%--<ClientSideEvents EndCallback="function(s,e){
                                                $find('ModalPopupExtenderLoading').hide();
                                                }" />--%>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px">
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                                <%--<dx:GridViewDataHyperLinkColumn FieldName="DocNumber" Width="140px">
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink OnInit="DocNumBtn_Init" ID="DocNumBtn" runat="server" Text='<%#Eval("DocNumber")%>' Theme="Office2010Blue">
                                            <ClientSideEvents Click="function(s,e){
                                                $find('ModalPopupExtenderLoading').show();
                                                e.processOnServer = true;
                                                }" />
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:GridViewDataHyperLinkColumn>--%>
                                <dx:GridViewDataColumn FieldName="DocNumber" Caption="Document Number" Width="140px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="DateCreated" Caption="Date Created"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="BU/ Department"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="Month"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MRPYear" Caption="Year"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LevelLine" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LevelPosition" Caption="Workflow Level"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Status" Caption="Status"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="WorkflowType" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CreatorKey" Visible="false"></dx:GridViewDataColumn>
                            </Columns>
                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" />
                            <%--<SettingsLoadingPanel Mode="ShowAsPopup" />--%>
                            <SettingsBehavior AllowFocusedRow="true" ProcessSelectionChangedOnServer="true" AllowSort="true" AllowHeaderFilter="true" />
                        </dx:ASPxGridView>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
