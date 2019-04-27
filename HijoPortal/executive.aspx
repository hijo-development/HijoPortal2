<%@ Page Title="Executive Setup" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="executive.aspx.cs" Inherits="HijoPortal.executive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%">
        <PanelCollection>
            <dx:PanelContent>
                <div id="dvHeader" style="height: 30px;">
                    <h1>Executive  Setup</h1>
                </div>
                <div id="dvExecutiveSetup" runat="server" class="scroll-container">
                    <div style="width: 100%;">
                        <dx:ASPxGridView ID="grdExecutive" runat="server"
                            ClientInstanceName="grdExecutiveDirect"
                            EnableTheming="True"
                            KeyboardSupport="true" Style="margin: 0 auto;"
                            Width="100%"
                            EnableCallBacks="true"
                            KeyFieldName="PK"
                            Theme="Office2010Blue"
                            OnInitNewRow="grdExecutive_InitNewRow"
                            OnRowInserting="grdExecutive_RowInserting"
                            OnStartRowEditing="grdExecutive_StartRowEditing"
                            OnRowUpdating="grdExecutive_RowUpdating"
                            OnRowDeleting="grdExecutive_RowDeleting"
                            OnBeforeGetCallbackResult="grdExecutive_BeforeGetCallbackResult">

                            <SettingsBehavior AllowSort="true" SortMode="Value" />

                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                    <EditItemTemplate>
                                        <%--<dx:ASPxTextBox ID="ASPxCtrlTextBox" runat="server" Width="100%" Text='<%#Eval("EffectDate")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                        <dx:ASPxLabel ID="ASPxCtrlTextBox" runat="server" Width="100%" Text='<%#Eval("Ctrl")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="EffectDate" Caption="Effect Date" VisibleIndex="3">
                                    <EditItemTemplate>
                                        <dx:ASPxDateEdit ID="EffectDate" ClientInstanceName="EffectDateHeadDirect" runat="server" Value='<%#Eval("EffectDate")%>' Theme="Office2010Blue" Width="100%" AllowUserInput="false"
                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                            <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                                        </dx:ASPxDateEdit>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="UserKey" Visible="false" VisibleIndex="5"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Name" VisibleIndex="6">
                                    <EditItemTemplate>
                                        <dx:ASPxComboBox ID="Executive" runat="server" ClientInstanceName="ExecutiveDirect" OnInit="Executive_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="" />
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="StatusKey" Visible="false" VisibleIndex="7"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="StatusDesc" Caption="Status" VisibleIndex="8">
                                    <EditItemTemplate>
                                        <dx:ASPxComboBox ID="Status" runat="server" ClientInstanceName="StatusDirect" OnInit="ExecutiveStatus_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                            <ClientSideEvents SelectedIndexChanged="" />
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="9">
                                    <EditItemTemplate>
                                        <%--<dx:ASPxTextBox ID="ASPxLastModifiedTextBox" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                        <dx:ASPxLabel ID="ASPxLastModifiedTextBox" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                            </Columns>
                            <SettingsCommandButton>
                                <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                <UpdateButton ButtonType="Image" Image-Url="Images/Save.ico" Image-Width="15px"></UpdateButton>
                                <CancelButton ButtonType="Image" Image-Url="Images/Undo.ico" Image-Width="15px"></CancelButton>
                            </SettingsCommandButton>

                            <SettingsEditing Mode="Inline"></SettingsEditing>

                            <EditFormLayoutProperties>
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            </EditFormLayoutProperties>
                            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                            <SettingsPopup>
                                <EditForm Width="900">
                                    <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                                </EditForm>
                            </SettingsPopup>
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                AllowSort="true" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowDragDrop="false" ConfirmDelete="true" />
                            <SettingsText ConfirmDelete="Delete This Executive?" />
                            <Styles>
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
