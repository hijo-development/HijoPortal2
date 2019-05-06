<%@ Page Title="List of Created Purchase Order" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_list.aspx.cs" Inherits="HijoPortal.mrp_po_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="jquery/POList.js"></script>
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

    <dx:ASPxPopupControl ID="PopupSubmit" ClientInstanceName="POListPopupSubmit" runat="server" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="2" style="padding-right: 20px; padding-bottom: 20px;">
                            <dx:ASPxLabel runat="server" Text="Are you sure you want to submit this Purchase Order document to AX?" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="OK_SUBMIT" runat="server" Text="SUBMIT" OnClick="OK_SUBMIT_Click" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){
                                    POListPopupSubmit.Hide();
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CANCEL_SUBMIT" runat="server" Text="CANCEL" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){POListPopupSubmit.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="POListNotify" ClientInstanceName="POList_MRPNotify" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue" ContentStyle-Paddings-Padding="20">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxLabel ID="POListNotifyLbl" ClientInstanceName="POList_MRPNotificationMessage" runat="server" Text="" ForeColor="Red" Theme="Office2010Blue"></dx:ASPxLabel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="POListPopup" runat="server" ClientInstanceName="POListPopupClient" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2010Blue" Modal="true">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="DeleteLbl" runat="server" ClientInstanceNam="DeleteLbl" Text="Are you sure you want to delete?" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="OK" runat="server" OnClick="OK_Click" Text="OK" Theme="Office2010Blue">
                                <ClientSideEvents Click="OK_Click" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="Cancel" runat="server" Text="Cancel" Theme="Office2010Blue">
                                <ClientSideEvents Click="function(s,e){POListPopupClient.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="PopupNotAllowed" runat="server" ClientInstanceName="PopupNotAllowed" CloseAction="CloseButton" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="PopupNotAllowedLabel" ClientInstanceName="PopupNotAllowedLabel" runat="server" Text="Not Allowed to Delete"></dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <PanelCollection>
            <dx:PanelContent>
                <%--<div id="dvContentWrapper" runat="server" class="ContentWrapper">--%>
                <div id="dvHeader" style="height: 30px;">
                    <h1>List of Created Purchase Order</h1>
                </div>
                <div>
                    <dx:ASPxGridView ID="gridCreatedPO" runat="server" ClientInstanceName="gridCreatedPO"
                        EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                        Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                        OnCustomButtonCallback="gridCreatedPO_CustomButtonCallback">
                        <ClientSideEvents CustomButtonClick="gridCreatedPO_CustomButtonClick" />

                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="50">
                                <HeaderTemplate>
                                    <div style="text-align: left;">
                                        <dx:ASPxButton ID="Add" OnClick="Add_Click" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle"></dx:ASPxButton>
                                        <dx:ASPxHiddenField ID="HiddenVal" ClientInstanceName="HiddenVal" runat="server"></dx:ASPxHiddenField>
                                    </div>
                                </HeaderTemplate>
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="Delete" Text="" Image-Url="Images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="Submit" Text="" Image-Url="Images/Submit.ico" Image-ToolTip="Submit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    <%--<dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>--%>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PONumber" Caption="PO Number" Width="130px"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="MOPNumber" Caption="MOP Number" Width="140px"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Entity" Caption="Entity"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="BU" Caption="SSU/BU"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="VendorName" Caption="Supplier"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="DateCreated" Caption="Date Created"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="CreatorKey" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Creator" Caption="Creator"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="ExpectedDate" Caption="Expected Delivery Date"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="TotalAmount" Caption="Total Amount">
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Status" Caption="Status"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="POStatus" Visible="false"></dx:GridViewDataColumn>
                        </Columns>
                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                        <SettingsPopup>
                            <EditForm Width="900">
                                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                            </EditForm>
                        </SettingsPopup>

                        <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="false">
                        </SettingsPager>

                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            AllowSort="true" ProcessFocusedRowChangedOnServer="False" ProcessSelectionChangedOnServer="False" AllowDragDrop="false" />
                        <SettingsText ConfirmDelete="Delete This Item?" />
                        <Styles>
                            <Cell Wrap="true"></Cell>
                            <InlineEditCell Wrap="true"></InlineEditCell>
                            <SelectedRow Font-Bold="False" Font-Italic="False">
                            </SelectedRow>
                            <FocusedRow Font-Bold="False" Font-Italic="False">
                            </FocusedRow>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
                <%--</div>--%>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>


</asp:Content>
