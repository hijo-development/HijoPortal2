<%@ Page Title="Approver / Finance Setup" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="finance.aspx.cs" Inherits="HijoPortal.finance" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>Approval / Finance  Setup</h1>
        </div>
        <div id="dvFinanceSetup" runat="server" class="scroll-container">
            <dx:ASPxSplitter ID="FinanceSetupSplitter" runat="server" ClientInstanceName="FinanceSetupSplitterDirect" BackColor="#e6eff7" AllowResize="true" Border-BorderStyle="None" Width="100%" Height="100%">
                <Panes>
                    <dx:SplitterPane Size="50%" ScrollBars="Auto">
                        <Panes>
                            <dx:SplitterPane Size="50%" ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server">
                                        <%--Approval--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Approver</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdFinanceApproval" runat="server"
                                                ClientInstanceName="grdFinanceApprovalDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue" 
                                                OnInitNewRow="grdFinanceApproval_InitNewRow" 
                                                OnRowInserting="grdFinanceApproval_RowInserting" 
                                                OnStartRowEditing="grdFinanceApproval_StartRowEditing" 
                                                OnRowUpdating="grdFinanceApproval_RowUpdating" 
                                                OnRowDeleting="grdFinanceApproval_RowDeleting">

                                                <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <%--<dx:ASPxTextBox ID="ASPxCtrlTextBox" runat="server" Width="100%" Text='<%#Eval("EffectDate")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                            <dx:ASPxLabel ID="ASPxCtrlTextBoxApp" runat="server" Width="100%" Text='<%#Eval("Ctrl")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                                                    <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Head" VisibleIndex="6">
                                                        <EditItemTemplate>
                                                            <dx:ASPxComboBox ID="Approval" runat="server" ClientInstanceName="ApprovalDirect" OnInit="Approval_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="" />
                                                            </dx:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="7">
                                                        <EditItemTemplate>
                                                            <%--<dx:ASPxTextBox ID="ASPxLastModifiedTextBox" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                            <dx:ASPxLabel ID="ASPxLastModifiedTextBoxApp" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                                                <SettingsText ConfirmDelete="Delete This Approval?" />
                                                <Styles>
                                                    <SelectedRow Font-Bold="False" Font-Italic="False">
                                                    </SelectedRow>
                                                    <FocusedRow Font-Bold="False" Font-Italic="False">
                                                    </FocusedRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </div>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                            <dx:SplitterPane  ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server">
                                        <%--Head--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Finance Head</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdFinanceHead" runat="server"
                                                ClientInstanceName="grdFinanceHeadDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue"
                                                OnInitNewRow="grdFinanceHead_InitNewRow"
                                                OnRowInserting="grdFinanceHead_RowInserting"
                                                OnStartRowEditing="grdFinanceHead_StartRowEditing"
                                                OnRowUpdating="grdFinanceHead_RowUpdating"
                                                OnRowDeleting="grdFinanceHead_RowDeleting">

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
                                                    <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Head" VisibleIndex="6">
                                                        <EditItemTemplate>
                                                            <dx:ASPxComboBox ID="FinanceHead" runat="server" ClientInstanceName="FinanceHeadDirect" OnInit="FinanceHead_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="" />
                                                            </dx:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="7">
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
                                                <SettingsText ConfirmDelete="Delete This Head?" />
                                                <Styles>
                                                    <SelectedRow Font-Bold="False" Font-Italic="False">
                                                    </SelectedRow>
                                                    <FocusedRow Font-Bold="False" Font-Italic="False">
                                                    </FocusedRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </div>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                        </Panes>
                    </dx:SplitterPane>
                    <dx:SplitterPane>
                        <Panes>
                            <dx:SplitterPane Size="50%" ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server">
                                        <%--Budget--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Finance (Budget)</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdFinanceBudget" runat="server"
                                                ClientInstanceName="grdFinanceBudgetDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue"
                                                OnInitNewRow="grdFinanceBudget_InitNewRow"
                                                OnRowInserting="grdFinanceBudget_RowInserting"
                                                OnStartRowEditing="grdFinanceBudget_StartRowEditing"
                                                OnRowUpdating="grdFinanceBudget_RowUpdating"
                                                OnRowDeleting="grdFinanceBudget_RowDeleting">

                                                <%--RowClick="OnGridFocusedRowChangedFinBud"--%>
                                                <%--EndCallback="OnGridFocusedRowChangedFinBud_EndCallback"--%>

                                                <ClientSideEvents
                                                    RowClick="function (s,e) {grdFinanceBudgetDetDirect.PerformCallback('BudOff');}"
                                                    EndCallback="function (s,e) {grdFinanceBudgetDetDirect.Refresh();}" />

                                                <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <%--<dx:ASPxTextBox ID="ASPxCtrlTextBox" runat="server" Width="100%" Text='<%#Eval("EffectDate")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                            <dx:ASPxLabel ID="ASPxCtrlTextBoxBud" runat="server" Width="100%" Text='<%#Eval("Ctrl")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                                                    <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Head" VisibleIndex="6">
                                                        <EditItemTemplate>
                                                            <dx:ASPxComboBox ID="FinanceBudget" runat="server" ClientInstanceName="FinanceBudgetDirect" OnInit="FinanceBudget_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="" />
                                                            </dx:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="7">
                                                        <EditItemTemplate>
                                                            <%--<dx:ASPxTextBox ID="ASPxLastModifiedTextBox" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                            <dx:ASPxLabel ID="ASPxLastModifiedTextBoxBud" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                                                <SettingsText ConfirmDelete="Delete This Budget Officer?" />
                                                <Styles>
                                                    <SelectedRow Font-Bold="False" Font-Italic="False">
                                                    </SelectedRow>
                                                    <FocusedRow Font-Bold="False" Font-Italic="False">
                                                    </FocusedRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                            <%--<dx:ASPxLabel ID="ASPxLabelBudgetOff" runat="server" Text="0"></dx:ASPxLabel>--%>
                                        </div>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                            <dx:SplitterPane ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server">
                                        <%--Budget--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Assigned Entity/BU or Dept</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdFinanceBudgetDet" runat="server"
                                                ClientInstanceName="grdFinanceBudgetDetDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue"
                                                OnCustomButtonCallback="grdFinanceBudgetDet_CustomButtonCallback"
                                                OnInitNewRow="grdFinanceBudgetDet_InitNewRow"
                                                OnRowInserting="grdFinanceBudgetDet_RowInserting"
                                                OnStartRowEditing="grdFinanceBudgetDet_StartRowEditing"
                                                OnRowUpdating="grdFinanceBudgetDet_RowUpdating"
                                                OnRowDeleting="grdFinanceBudgetDet_RowDeleting"
                                                OnCustomCallback="grdFinanceBudgetDet_CustomCallback">

                                                <ClientSideEvents
                                                    CustomButtonClick=""
                                                    EndCallback="function (s,e) {grdFinanceBudgetDetDirect.InCallback();}" />

                                                <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="false" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <div style="text-align: left">
                                                                <dx:ASPxButton ID="Add_FinBudEntBU" ClientInstanceName="Add_FinBudEntBUDirect" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <ClientSideEvents Click="function (s, e) {grdFinanceBudgetDetDirect.PerformCallback('AddNew');}" />
                                                                </dx:ASPxButton>
                                                            </div>
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="EntityCode" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity" VisibleIndex="3" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <dx:ASPxComboBox ID="Entity" runat="server" ClientInstanceName="EntityDirect" OnInit="Entity_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="FinBudEntity_IndexChanged" />
                                                            </dx:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="BUSSUCode" Visible="false" VisibleIndex="4"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="BUSSUCodeDesc" Caption="BU / Department" VisibleIndex="5" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <dx:ASPxCallbackPanel ID="FinBUCallbackPanel" ClientInstanceName="FinBUCallbackPanelDirect" OnCallback="FinBUCallbackPanel_Callback" runat="server" Width="100%">
                                                                <ClientSideEvents EndCallback="FinBudBU_EndCallback" />
                                                                <PanelCollection>
                                                                    <dx:PanelContent>
                                                                        <dx:ASPxComboBox ID="BUSSU" runat="server" ClientInstanceName="BUSSUDirect" OnInit="BUSSU_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="" />
                                                                        </dx:ASPxComboBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsCommandButton>
                                                    <%--<NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>--%>
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
                                                <SettingsText ConfirmDelete="Delete This Head?" />
                                                <Styles>
                                                    <SelectedRow Font-Bold="False" Font-Italic="False">
                                                    </SelectedRow>
                                                    <FocusedRow Font-Bold="False" Font-Italic="False">
                                                    </FocusedRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </div>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                        </Panes>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>
        </div>
    </div>
</asp:Content>
