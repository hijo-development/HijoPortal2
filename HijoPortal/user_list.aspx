<%@ Page Title="User List" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="user_list.aspx.cs" Inherits="HijoPortal.user_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <dx:ASPxPopupControl ID="PopupDeleteUserList" ClientInstanceName="PopupDeleteUserList" runat="server" CloseAction="CloseButton" Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" Theme="Moderno" Width="400px">
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
                            <dx:ASPxButton ID="OK_DELETE" runat="server" Text="DELETE" Theme="Moderno" AutoPostBack="false">
                                <%--<ClientSideEvents Click="OK_DELETE" />--%>
                                <ClientSideEvents Click="function(s,e){
                                    PopupDeleteUserList.Hide();
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CANCEL_DELETE" runat="server" Text="CANCEL" Theme="Moderno" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){PopupDeleteUserList.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <PanelCollection>
            <dx:PanelContent>
                <div id="dvHeader" style="height: 30px;">
                    <h1>User  List</h1>
                </div>
                <div>
                    <dx:ASPxGridView ID="UserListGrid" runat="server" ClientInstanceName="UserListGrid"
                        EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                        Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                        OnStartRowEditing="UserListGrid_StartRowEditing"
                        OnRowUpdating="UserListGrid_RowUpdating"
                        OnRowDeleting="UserListGrid_RowDeleting"
                        OnCustomButtonCallback="UserListGrid_CustomButtonCallback"
                        OnBeforeGetCallbackResult="UserListGrid_BeforeGetCallbackResult">
                        <SettingsBehavior AllowSort="true" SortMode="Value" />

                        <Columns>
                            <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="false" VisibleIndex="0">
                                <%--<CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="Delete" Text="" Image-Url="Images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>--%>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="LastName" Caption="Last Name" VisibleIndex="2" SortOrder="Ascending"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="FirstName" Caption="First Name" VisibleIndex="3"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Gender" Caption="Gender" VisibleIndex="4"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserName" Caption="UserName" VisibleIndex="5"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Email" Caption="Email" VisibleIndex="6"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserType" Visible="false" VisibleIndex="7"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserTypeDesc" Caption="User Type" VisibleIndex="8"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserLevelKey" Visible="false" VisibleIndex="9"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserLevelDesc" Caption="User Level" VisibleIndex="10"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="DomainAccount" Caption="Domain Account" VisibleIndex="11"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="EntityCode" Visible="false" VisibleIndex="12"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity" VisibleIndex="13"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="BUCode" Visible="false" VisibleIndex="14"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="BU / SSU" VisibleIndex="15"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="StatusKey" Visible="false" VisibleIndex="16"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="StatusDesc" Caption="Status" VisibleIndex="17"></dx:GridViewDataColumn>
                        </Columns>


                         <SettingsCommandButton>
                            <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                            <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                        </SettingsCommandButton>

                        <%--Edit Form--%>
                        <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                        <Templates>
                            <EditForm>
                                <div style="padding: 4px 3px 4px">
                                    <dx:ASPxPageControl ID="UserPageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                        <TabPages>
                                            <dx:TabPage Text="User Details" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="tabUserInfo" runat="server">
                                                        <table style="padding: 10px;">
                                                            <tr>
                                                                <td style="width: 10%; padding: 0px 0px 10px;">
                                                                    <dx:ASPxLabel runat="server" Text="Complete Name " Theme="Office2010Blue" />
                                                                </td>
                                                                <td style="padding: 0px 0px 10px;">:</td>
                                                                <td style="padding: 3px 3px 10px;">
                                                                    <dx:ASPxLabel runat="server" Text='<%#Eval("CompleteName")%>' Theme="Office2010Blue" Font-Bold="true" />
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <dx:ASPxLabel runat="server" Text="UserName " Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="padding: 3px 3px 10px;">
                                                                    <dx:ASPxLabel runat="server" Text='<%#Eval("UserName")%>' Theme="Office2010Blue" Font-Bold="true" />
                                                                </td>
                                                                <td></td>
                                                                <td rowspan="4" style="width: 15%; text-align: right;">
                                                                    <dx:ASPxImage ID="UserImage" runat="server" ClientInstanceName="UserImageDirect" ImageUrl="~/images/ID.jpg" ShowLoadingImage="true" Height="100px" Width="100px">
                                                                        <Border BorderStyle="Solid" BorderColor="Black" BorderWidth="1" />
                                                                        <ClientSideEvents />
                                                                    </dx:ASPxImage>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                    <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="width: 40%;">
                                                                    <dx:ASPxComboBox ID="EntityCode" runat="server" ClientInstanceName="EntityCodeDirect" AutoResizeWithContainer="false" OnInit="EntityCode_Init" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                        <ClientSideEvents SelectedIndexChanged="UserEntity_IndexChanged" />
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 5%;">
                                                                    <div style="display: none;">
                                                                        <dx:ASPxTextBox ID="EntityValue" ClientInstanceName="EntityValueClient" runat="server" Text='<%#Eval("EntityCode")%>' Theme="Office2010Blue" Style="display: none;" />
                                                                    </div>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <dx:ASPxLabel runat="server" Text="Level" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="width: 20%;">
                                                                    <dx:ASPxComboBox ID="UserLevel" runat="server" ClientInstanceName="UserLevelDirect" AutoResizeWithContainer="false" OnInit="UserLevel_Init" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                        <ClientSideEvents SelectedIndexChanged="" />
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxLabel runat="server" Text="BU / Department" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="padding: 3px 3px 3px;">
                                                                    <dx:ASPxCallbackPanel ID="BUCallBackPanel" ClientInstanceName="BUCallBackPanelDirect" runat="server" OnCallback="BUCallBackPanel_Callback">
                                                                        <ClientSideEvents EndCallback="UserBU_EndCallback" />
                                                                        <PanelCollection>
                                                                            <dx:PanelContent>
                                                                                <dx:ASPxComboBox ID="BUCode" runat="server" ClientInstanceName="BUCodeDirect" OnInit="BUCode_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                    ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="false" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="" />
                                                                                </dx:ASPxComboBox>
                                                                            </dx:PanelContent>
                                                                        </PanelCollection>
                                                                    </dx:ASPxCallbackPanel>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <dx:ASPxLabel runat="server" Text="Status" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <dx:ASPxComboBox ID="UserStatus" runat="server" ClientInstanceName="UserStatusDirect" AutoResizeWithContainer="false" OnInit="UserStatus_Init" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                        <ClientSideEvents SelectedIndexChanged="" />
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxLabel runat="server" Text="Domain Account" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="padding: 3px 3px 3px;">
                                                                    <dx:ASPxTextBox ID="DomainAccount" ClientInstanceName="DomainAccountClient" runat="server" Text='<%#Eval("DomainAccount")%>' Theme="Office2010Blue" Width="100%" ValidationSettings-RequiredField-IsRequired="false" />
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
                                        <ClientSideEvents Click="updateUserListNew" />
                                    </dx:ASPxButton>
                                    <%--<dx:ASPxButton runat="server" Text="Reset Password" Theme="Office2010Blue" AutoPostBack="false">
                                        <ClientSideEvents Click="" />
                                    </dx:ASPxButton>--%>
                                    <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){UserListGrid.CancelEdit();}" />
                                    </dx:ASPxButton>
                                </div>
                            </EditForm>
                        </Templates>
                        <EditFormLayoutProperties>
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                        </EditFormLayoutProperties>
                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                        <SettingsPopup>
                            <EditForm Width="900">
                                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                            </EditForm>
                        </SettingsPopup>
                        <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="true">
                        </SettingsPager>
                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            AllowSort="true" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowDragDrop="false" ConfirmDelete="true" />
                        <%--<SettingsText ConfirmDelete="Delete This User?" />--%>
                        <Styles>
                            <Cell Wrap="False"></Cell>
                            <SelectedRow Font-Bold="False" Font-Italic="False">
                            </SelectedRow>
                            <FocusedRow Font-Bold="False" Font-Italic="False">
                            </FocusedRow>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
