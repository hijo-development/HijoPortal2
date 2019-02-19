<%@ Page Title="User List" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="userlist.aspx.cs" Inherits="HijoPortal.userlist" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>User  List</h1>
        </div>
        <div>
            <%--<asp:Panel ID="MasterPanel" runat="server">--%>
            <dx:ASPxGridView ID="UserListGrid" runat="server" ClientInstanceName="UserListGrid"
                EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                OnCustomButtonCallback="UserList_CustomButtonCallback"
                OnDataBound="UserList_DataBound"
                OnStartRowEditing="UserList_StartRowEditing"
                OnRowUpdating="UserList_RowUpdating">

                <SettingsBehavior AllowSort="true" SortMode="Value" />

                <%--<ClientSideEvents CustomButtonClick="CustomButtonClick" />--%>
                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="false" VisibleIndex="0"></dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="LastName" Caption="Last Name" VisibleIndex="2" SortOrder="Ascending"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FirstName" Caption="First Name" VisibleIndex="3"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="MiddleName" Caption="Middle Name" VisibleIndex="4"></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="Email" Caption="Email" VisibleIndex="5"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UserType" Visible="false" VisibleIndex="6"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UserTypeDesc" Caption="User Type" VisibleIndex="7"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UserLevelKey" Visible="false" VisibleIndex="8"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UserLevelDesc" Caption="User Level" VisibleIndex="9"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DomainAccount" Caption="Domain Account" VisibleIndex="10"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntityCode" Visible="false" VisibleIndex="11"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity" VisibleIndex="12"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BUCode" Visible="false" VisibleIndex="13"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="BU / SSU" VisibleIndex="14"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="StatusKey" Visible="false" VisibleIndex="15"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="StatusDesc" Caption="Status" VisibleIndex="16"></dx:GridViewDataColumn>
                </Columns>

                <SettingsCommandButton>
                    <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                    <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                    <%--<NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>--%>
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
                                            <dx:ContentControl runat="server">
                                                <table style="padding: 10px;">
                                                    <tr>
                                                        <td style="width: 10%; padding: 0px 0px 10px;">
                                                            <dx:ASPxLabel runat="server" Text="Complete Name " Theme="Office2010Blue" />
                                                        </td>
                                                        <td style="padding: 0px 0px 10px;">:</td>
                                                        <td colspan="4" style="padding: 3px 3px 10px;">
                                                            <dx:ASPxLabel runat="server" Text='<%#Eval("CompleteName")%>' Theme="Office2010Blue" Font-Bold="true" />
                                                        </td>
                                                        <td></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%;">
                                                            <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue" />
                                                        </td>
                                                        <td>:</td>
                                                        <td style="width: 40%;">
                                                            <%--DataSourceID="SQLEntityConn" TextField="EntCodeDesc" ValueField="ID"--%>
                                                            <%--<dx:ASPxComboBox ID="EntityCode" runat="server" ClientInstanceName="EntityCodeDirect" Text='<%#Eval("EntityCodeDesc")%>' ValueType="System.String" Theme="Office2010Blue"--%>
                                                            <dx:ASPxComboBox ID="EntityCode" runat="server" ClientInstanceName="EntityCodeDirect" OnInit="EntityCode_Init" AutoResizeWithContainer="true" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                <%--ValidationSettings-ErrorDisplayMode="None"--%>
                                                                <%--ValidationSettings-RequiredField-IsRequired="true" Width="100%" OnInit="EntityCode_Init">--%>
                                                                <%--<ClientSideEvents SelectedIndexChanged="EntityCodeIndexChange" />--%>
                                                                <ClientSideEvents SelectedIndexChanged="" />
                                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { SetComboBoxEntityID(s); }" />--%>
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
                                                        <td style="width: 35%;">
                                                            <%--DataSourceID="SQLEntityConn" TextField="EntCodeDesc" ValueField="ID"--%>
                                                            <dx:ASPxComboBox ID="EmployeeLevel" runat="server" ClientInstanceName="EmployeeLevelDirect" Text='<%#Eval("UserLevelDesc")%>' ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None"
                                                                ValidationSettings-RequiredField-IsRequired="true" Width="100%" OnInit="EmployeeLevel_Init">
                                                                <ClientSideEvents SelectedIndexChanged="UserLevelDescIndexChange" />
                                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { SetComboBoxEntityID(s); }" />--%>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td>
                                                            <div style="display: none;">
                                                                <dx:ASPxTextBox ID="UserLevelValue" ClientInstanceName="UserLevelValueClient" runat="server" Text='<%#Eval("UserLevelKey")%>' Theme="Office2010Blue" Style="display: none;" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel runat="server" Text="Department" Theme="Office2010Blue" />
                                                        </td>
                                                        <td>:</td>
                                                        <td>
                                                            <dx:ASPxCallbackPanel ID="BUCodeCallbackPanel" ClientInstanceName="BUCodeCallbackPanelDirect" runat="server" Width="100%" OnCallback="BUCodeCallbackPanel_Callback">
                                                                <ClientSideEvents EndCallback="BU_EndCallBack" />
                                                                <PanelCollection>
                                                                    <dx:PanelContent>
                                                                        <dx:ASPxComboBox ID="BUCode" runat="server" ClientInstanceName="BUCodeDirect" Text='<%#Eval("BUCodeDesc")%>' ValueType="System.String" Theme="Office2010Blue"
                                                                            ValidationSettings-ErrorDisplayMode="None"
                                                                            ValidationSettings-RequiredField-IsRequired="true" Width="100%" OnInit="BUCode_Init">
                                                                            <ClientSideEvents SelectedIndexChanged="BUCodeIndexChange" />
                                                                        </dx:ASPxComboBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </td>
                                                        <td>
                                                            <div style="display: none;">
                                                                <dx:ASPxTextBox ID="BUValue" ClientInstanceName="BUValueClient" runat="server" Text='<%#Eval("BUCode")%>' Theme="Office2010Blue" />
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel runat="server" Text="Level" Theme="Office2010Blue" />
                                                        </td>
                                                        <td>:</td>
                                                        <td>
                                                            <%--DataSourceID="SQLEntityConn" TextField="EntCodeDesc" ValueField="ID"--%>
                                                            <dx:ASPxComboBox ID="UserStatus" runat="server" ClientInstanceName="UserStatusDirect" Text='<%#Eval("StatusDesc")%>' ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None"
                                                                ValidationSettings-RequiredField-IsRequired="true" Width="100%" OnInit="UserStatus_Init">
                                                                <ClientSideEvents SelectedIndexChanged="UserStatuscIndexChange" />
                                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { SetComboBoxEntityID(s); }" />--%>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td>
                                                            <div style="display: none;">
                                                                <dx:ASPxTextBox ID="UserStatusValue" ClientInstanceName="UserStatusValueClient" runat="server" Text='<%#Eval("StatusKey")%>' Theme="Office2010Blue" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel runat="server" Text="Domain Account" Theme="Office2010Blue" />
                                                        </td>
                                                        <td>:</td>
                                                        <td style="padding: 3px 3px 3px;">
                                                            <dx:ASPxTextBox ID="DomainAccount" ClientInstanceName="DomainAccountClient" runat="server" Text='<%#Eval("DomainAccount")%>' Theme="Office2010Blue" Width="100%" />
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
                                <ClientSideEvents Click="updateUserList" />
                            </dx:ASPxButton>
                            <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){UserListGrid.CancelEdit();}" />
                            </dx:ASPxButton>
                        </div>
                    </EditForm>
                </Templates>


                <%--<SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />--%>
                <EditFormLayoutProperties>
                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                </EditFormLayoutProperties>
                <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                <SettingsPopup>
                    <EditForm Width="900">
                        <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                    </EditForm>
                </SettingsPopup>

                <%--<SettingsPager Mode="ShowPager" PageSize="5" AlwaysShowPager="true">
                    </SettingsPager>--%>

                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                    AllowSort="true" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowDragDrop="false" ConfirmDelete="true" />
                <SettingsText ConfirmDelete="Delete This Item?" />
                <Styles>
                    <SelectedRow Font-Bold="False" Font-Italic="False">
                    </SelectedRow>
                    <FocusedRow Font-Bold="False" Font-Italic="False">
                    </FocusedRow>
                </Styles>
            </dx:ASPxGridView>
            <%--</asp:Panel>--%>
        </div>
    </div>
</asp:Content>
