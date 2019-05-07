<%@ Page Title="MOP List" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_list.aspx.cs" Inherits="HijoPortal.mrp_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery/mrpList.js"></script>
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

    <dx:ASPxPopupControl ID="PopUpControl" ClientInstanceName="PopUpControl" runat="server" Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Moderno">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <div style="padding: 20px 10px;">
                    <table class="popup-modal">
                        <tr>
                            <td>
                                <dx:ASPxCheckBox ID="Checkbox" runat="server" ClientInstanceName="CheckboxClient" CheckState="Unchecked" Theme="Moderno">
                                    <ClientSideEvents CheckedChanged="Checkbox_CheckedChanged" />
                                </dx:ASPxCheckBox>
                                <dx:ASPxLabel runat="server" Text="Copy Previous MOP" Theme="Moderno"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="MonthYearCombo" runat="server" ClientInstanceName="MonthYearComboClient" ClientEnabled="false" DropDownRows="5" ValueType="System.String" Theme="Moderno" OnInit="MonthYearCombo_Init">
                                    <ClientSideEvents SelectedIndexChanged="MonthYearCombo_SelectedIndexChanged" />
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="Month" runat="server" ClientInstanceName="MonthClient" ValueType="System.String" NullText="Month" Theme="Moderno" OnInit="Month_Init">
                                    <ClientSideEvents SelectedIndexChanged="Month_SelectedIndexChanged" />
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="Year" runat="server" ClientInstanceName="YearClient" ValueType="System.String" NullText="Year" Theme="Moderno" OnInit="Year_Init">
                                    <ClientSideEvents SelectedIndexChanged="Year_SelectedIndexChanged" />
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="BtnAdd" runat="server" ClientInstanceName="BtnAddClient" ClientEnabled="false" Text="Add" Theme="Moderno" OnClick="BtnAdd_Click">
                                    <ClientSideEvents Click="AddNewMOP" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="WarningPopUp" runat="server" CloseAction="CloseButton" Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Moderno">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <div style="padding: 5px;">
                    <dx:ASPxLabel runat="server" ID="WarningText" Text="" Theme="Moderno"></dx:ASPxLabel>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="MRPNotify" ClientInstanceName="MRPNotify" runat="server" CloseAction="CloseButton" Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Moderno">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxLabel ID="MRPNotificationMessage" ClientInstanceName="MRPNotificationMessage" runat="server" Text="" Theme="Moderno"></dx:ASPxLabel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="PopupDeleteMRPList" ClientInstanceName="PopupDeleteMRPList" runat="server" CloseAction="CloseButton" Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Moderno" Width="100%">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="padding-right: 20px; padding-bottom: 20px;">
                            <dx:ASPxLabel runat="server" Text="Are you sure you want to delete this document?" Theme="Moderno" Width="300px"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="OK_DELETE" runat="server" Text="DELETE" Theme="Moderno" AutoPostBack="false" OnClick="OK_DELETE_Click">
                                <%--<ClientSideEvents Click="OK_DELETE" />--%>
                                <ClientSideEvents Click="function(s,e){
                                    PopupDeleteMRPList.Hide();
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CANCEL_DELETE" runat="server" Text="CANCEL" Theme="Moderno" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){PopupDeleteMRPList.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


    <dx:ASPxPopupControl ID="PopupSubmitMRPList" ClientInstanceName="PopupSubmitMRPList" runat="server" CloseAction="CloseButton" Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Office2010Blue">
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
                            <dx:ASPxButton ID="OK_SUBMIT" runat="server" Text="SUBMIT" Theme="Office2010Blue" AutoPostBack="false" OnClick="OK_SUBMIT_Click">
                                <ClientSideEvents Click="function(s,e){
                                    PopupSubmitMRPList.Hide();
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CANCEL_SUBMIT" runat="server" Text="CANCEL" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){PopupSubmitMRPList.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <%--<dx:ASPxPanel ID="ASPxPanel2" runat="server" Width="200px" Theme="Office2010Blue" HorizontalAlign="Center" VerticalAlign="Middle" ClientInstanceName="loadingPanel" Modal="true"></dx:ASPxPanel>--%>

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <PanelCollection>
            <dx:PanelContent>
                <div>
                    <%--<div id="dvContentWrapper" runat="server" class="ContentWrapper">--%>
                    <div id="dvHeader" style="height: 30px;">
                        <h1>M O P  List</h1>
                        <asp:Label ID="msgTrans" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div>

                        <%--OnCustomCallback="MainTable_CustomCallback"--%>

                        <dx:ASPxGridView ID="MainTable" runat="server" ClientInstanceName="MainTable" KeyFieldName="PK"
                            EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                            Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                            OnCustomButtonCallback="MainTable_CustomButtonCallback">
                            <ClientSideEvents CustomButtonClick="CustomButtonClick" />
                            <ClientSideEvents RowClick="MOPListFocused" />
                            <ClientSideEvents EndCallback="MainTableEndCallback" />
                            <SettingsBehavior AllowSort="true" SortMode="Value" />
                            <%--<ClientSideEvents BeginCallback="function(s,e){loadingPanel.Show();}" />--%>

                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="50">
                                    <HeaderTemplate>

                                        <div style="text-align: left;">
                                            <%--OnClick="Add_Click"--%>
                                            <dx:ASPxButton ID="Add" OnClick="Add_Click" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle">
                                                <%--<ClientSideEvents Click="function (s, e) {MainTable.PerformCallback('AddNew');}" />--%>
                                            </dx:ASPxButton>
                                            <dx:ASPxHiddenField ID="MRPHiddenVal" ClientInstanceName="MRPHiddenVal" runat="server"></dx:ASPxHiddenField>
                                            <dx:ASPxHiddenField ID="ASPxHiddenFieldEnt" ClientInstanceName="ASPxHiddenFieldEntDirect" runat="server"></dx:ASPxHiddenField>
                                            <dx:ASPxHiddenField ID="MRPHiddenValStatus" ClientInstanceName="MRPHiddenValStatus" runat="server"></dx:ASPxHiddenField>
                                            <dx:ASPxHiddenField ID="MRPHiddenValStatusLine" ClientInstanceName="MRPHiddenValStatusLine" runat="server"></dx:ASPxHiddenField>
                                        </div>
                                    </HeaderTemplate>

                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="Delete" Text="" Image-Url="Images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>

                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="DocNumber" Caption="MRP Number" VisibleIndex="2" SortOrder="Descending" Width="140px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="EntityCode" Visible="false" VisibleIndex="3"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity" VisibleIndex="4"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BUCode" Visible="false" VisibleIndex="5"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="Department" VisibleIndex="6"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="Month" VisibleIndex="7"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MRPYear" Caption="Year" VisibleIndex="8"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Amount" VisibleIndex="9" CellStyle-HorizontalAlign="Right"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="StatusKey" Visible="false" VisibleIndex="10"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="StatusKeyDesc" Caption="Status" VisibleIndex="11"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="WorkflowStatusLine" Visible="false" VisibleIndex="12"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="WorkflowStatus" Caption="Worflow Level" VisibleIndex="13"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="DateCreated" Visible="false" VisibleIndex="14"></dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn VisibleIndex="15" ButtonRenderMode="Image" Width="20">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="Submit" Text="" Image-Url="Images/Submit.ico" Image-ToolTip="Submit Row" Image-Width="15px">
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                            </Columns>

                            <%--<Settings HorizontalScrollBarMode="Auto" />--%>
                            <SettingsCommandButton>
                                <%--<EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px" Image-ToolTip="Edit Row">
            </EditButton>
            <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
            <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>--%>
                            </SettingsCommandButton>
                            <%--<SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />--%>
                            <EditFormLayoutProperties>
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            </EditFormLayoutProperties>
                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true"/>
                            <SettingsPopup>
                                <EditForm Width="900">
                                    <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                                </EditForm>
                            </SettingsPopup>

                            <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="false">
                            </SettingsPager>

                            <SettingsLoadingPanel Mode="ShowAsPopup" />
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                AllowSort="true" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowDragDrop="false" ConfirmDelete="true" />
                            <%--<SettingsText ConfirmDelete="Delete This Item?" />--%>
                            <Styles>
                                <Cell Wrap="False"></Cell>
                                <SelectedRow Font-Bold="False" Font-Italic="False">
                                </SelectedRow>
                                <FocusedRow Font-Bold="False" Font-Italic="False">
                                </FocusedRow>
                            </Styles>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

</asp:Content>
