<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="user_list.aspx.cs" Inherits="HijoPortal.user_list" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>User  List</h1>
        </div>
        <div>
            <dx:ASPxGridView ID="UserListGrid" runat="server" ClientInstanceName="UserListGrid"
                EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                OnStartRowEditing="UserListGrid_StartRowEditing"
                OnRowUpdating="UserListGrid_RowUpdating"
                OnCustomButtonCallback="UserListGrid_CustomButtonCallback">
                <SettingsBehavior AllowSort="true" SortMode="Value" />

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
                                                            <dx:ASPxComboBox ID="EntityCode" runat="server" ClientInstanceName="EntityCodeDirect" AutoResizeWithContainer="false" OnInit="EntityCode_Init" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="" />
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 5%;"></td>
                                                        <td style="width: 10%;">
                                                            <dx:ASPxLabel runat="server" Text="Level" Theme="Office2010Blue" />
                                                        </td>
                                                        <td>:</td>
                                                        <td style="width: 35%;">
                                                            <dx:ASPxComboBox ID="UserLevel" runat="server" ClientInstanceName="UserLevelDirect" AutoResizeWithContainer="false" OnInit="UserLevelLevel_Init" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
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
                                                        <td>
                                                            <dx:ASPxCallbackPanel ID="BUCallBackPanel" ClientInstanceName="BUCallBackPanelDirect" runat="server" OnCallback="BUCallBackPanel_Callback">
                                                                <ClientSideEvents EndCallback="" />
                                                                <PanelCollection>
                                                                    <dx:PanelContent>
                                                                        <dx:ASPxComboBox ID="BUCode" ClientInstanceName="BUCodeDirect" runat="server" ValueType="System.String" Theme="Office2010Blue" ValidationSettings-ErrorDisplayMode="None"
                                                                            ValidationSettings-RequiredField-IsRequired="false" Width="100%">
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
                                <ClientSideEvents Click="updateUserListNew" />
                            </dx:ASPxButton>
                            <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){UserListGrid.CancelEdit();}" />
                            </dx:ASPxButton>
                        </div>
                    </EditForm>
                </Templates>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
