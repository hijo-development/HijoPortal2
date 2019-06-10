<%@ Page Title="BU / Department Head" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="budept_head.aspx.cs" Inherits="HijoPortal.budept_head" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <PanelCollection>
            <dx:PanelContent>
                <div id="dvHeader" style="height: 30px;">
                    <h1>Business Unit / Department Heads</h1>
                </div>
                <div>
                    <dx:ASPxGridView ID="BUDeptListGrid" runat="server" ClientInstanceName="BUDeptListGridDirect"
                        EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                        Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                        OnInitNewRow="BUDeptListGrid_InitNewRow"
                        OnRowInserting="BUDeptListGrid_RowInserting"
                        OnRowDeleting="BUDeptListGrid_RowDeleting"
                        OnStartRowEditing="BUDeptListGrid_StartRowEditing"
                        OnRowUpdating="BUDeptListGrid_RowUpdating"
                        OnBeforeGetCallbackResult="BUDeptListGrid_BeforeGetCallbackResult">
                        <SettingsBehavior AllowSort="true" SortMode="Value" />

                        <Columns>
                            <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="50px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl #" VisibleIndex="2" SortOrder="Ascending"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="EffectDate" Caption="Effect Date" VisibleIndex="3"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="EntityCode" Visible="false" VisibleIndex="4"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity" VisibleIndex="5"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="BUDeptCode" Visible="false" VisibleIndex="6"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="BUDeptCodeDesc" Caption="BU / Department" VisibleIndex="7"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserKey" Visible="false" VisibleIndex="8"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Head" VisibleIndex="9"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="StatusKey" Visible="false" VisibleIndex="10"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="StatusDesc" Caption="Status" VisibleIndex="11"></dx:GridViewDataColumn>
                        </Columns>

                        <SettingsCommandButton>
                            <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                            <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                            <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                        </SettingsCommandButton>

                        <%--Edit Form--%>
                        <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                        <Templates>
                            <EditForm>
                                <div style="padding: 4px 3px 4px">
                                    <dx:ASPxPageControl ID="BUHeadPageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                        <TabPages>
                                            <dx:TabPage Text="User Details" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="tabUserInfo" runat="server">
                                                        <table style="padding: 10px;">
                                                            <tr>
                                                                <td style="width: 10%; padding: 0px 0px 10px;">
                                                                    <dx:ASPxLabel runat="server" Text="Ctrl #" Theme="Office2010Blue" />
                                                                </td>
                                                                <td style="padding: 0px 0px 10px;">:</td>
                                                                <td colspan="4" style="padding: 3px 3px 10px;">
                                                                    <dx:ASPxLabel runat="server" Text='<%#Eval("Ctrl")%>' Theme="Office2010Blue" Font-Bold="true" />
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                    <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="width: 40%;">
                                                                    <dx:ASPxComboBox ID="EntityCode" runat="server" ClientInstanceName="EntityCodeHeadDirect" OnInit="EntityCode_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                        ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                        <ClientSideEvents SelectedIndexChanged="HeadEntity_IndexChanged" />
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 5%;">
                                                                    <div style="display: none;">
                                                                        <dx:ASPxTextBox ID="EntityValue" ClientInstanceName="EntityValueClient" runat="server" Text='<%#Eval("EntityCode")%>' Theme="Office2010Blue" Style="display: none;" />
                                                                    </div>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <dx:ASPxLabel runat="server" Text="User" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="width: 40%;">
                                                                    <dx:ASPxComboBox ID="BUHead" runat="server" ClientInstanceName="BUHeadDirect" OnInit="BUHead_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
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
                                                                    <dx:ASPxCallbackPanel ID="BUCallBackPanel" ClientInstanceName="BUCallBackPanelHeadDirect" runat="server" OnCallback="BUCallBackPanel_Callback">
                                                                        <ClientSideEvents EndCallback="HeadBU_EndCallback" />
                                                                        <PanelCollection>
                                                                            <dx:PanelContent>
                                                                                <dx:ASPxComboBox ID="BUCode" runat="server" ClientInstanceName="BUCodeHeadDirect" OnInit="BUCode_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                    ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="false" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="" />
                                                                                </dx:ASPxComboBox>
                                                                            </dx:PanelContent>
                                                                        </PanelCollection>
                                                                    </dx:ASPxCallbackPanel>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <dx:ASPxLabel runat="server" Text="Effect Date" Theme="Office2010Blue" />
                                                                </td>
                                                                <td>:</td>
                                                                <td style="padding: 3px 2px 3px;">
                                                                    <table style="width: 100%;" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 35%;">
                                                                                <dx:ASPxDateEdit ID="EffectDate" ClientInstanceName="EffectDateHeadDirect" runat="server" Value='<%#Eval("EffectDate")%>' Theme="Office2010Blue" AllowUserInput="false"
                                                                                    ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                                    <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                                                                                </dx:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="width: 20%; text-align: right;">
                                                                                <dx:ASPxLabel runat="server" Text="Status : " Theme="Office2010Blue" Width="100%" />
                                                                            </td>
                                                                            <td style="width: 35%;">
                                                                                <dx:ASPxComboBox ID="BUHeadStatus" runat="server" ClientInstanceName="BUHeadStatusDirect" OnInit="BUHeadStatus_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                    ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="" />
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
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
                                        <ClientSideEvents Click="updateBUDeptHeadList" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){BUDeptListGridDirect.CancelEdit();}" />
                                    </dx:ASPxButton>
                                </div>
                            </EditForm>
                        </Templates>
                        <EditFormLayoutProperties>
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                        </EditFormLayoutProperties>
                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                        <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="true"></SettingsPager>
                        <SettingsPopup>
                            <EditForm Width="900">
                                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                            </EditForm>
                        </SettingsPopup>
                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            AllowSort="true" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowDragDrop="false" ConfirmDelete="true" />
                        <SettingsText ConfirmDelete="Delete This Head?" />
                        <Styles>
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
