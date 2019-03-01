<%@ Page Title="S C M" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="scm.aspx.cs" Inherits="HijoPortal.scm" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>S C M  Setup</h1>
        </div>
        <div id="dvSCMSetup" runat="server" class="scroll-container">
            <dx:ASPxPageControl ID="SCMPageControl" runat="server" Width="100%" Height="100%" ActiveTabIndex="0" EnableHierarchyRecreation="true" Theme="Office2010Blue">
                <TabPages>
                    <dx:TabPage Text="Head & Inventory Analyst" Visible="true" ActiveTabStyle-Font-Bold="true">
                        <ContentCollection>
                            <dx:ContentControl ID="tabHeadInventoryAnal" runat="server">
                                <dx:ASPxSplitter ID="HeadInventoryAnalSplitter" runat="server" ClientInstanceName="MRPWorkflowSplitterDirect" BackColor="#e6eff7" AllowResize="true" Border-BorderStyle="None" Width="100%" Height="100%">
                                    <Panes>
                                        <dx:SplitterPane Size="50%" ScrollBars="Auto">
                                            <ContentCollection>
                                                <dx:SplitterContentControl runat="server">
                                                    <%--Head--%>
                                                    <h3 style="text-align: center; width: 100%; margin-top: 2px;">SCM Lead</h3>
                                                    <div style="width: 100%;">
                                                        <dx:ASPxGridView ID="grdSCMHead" runat="server"
                                                            ClientInstanceName="grdSCMHeadDirect"
                                                            EnableTheming="True"
                                                            KeyboardSupport="true" Style="margin: 0 auto;"
                                                            Width="100%"
                                                            EnableCallBacks="true"
                                                            KeyFieldName="PK"
                                                            Theme="Office2010Blue"
                                                            OnInitNewRow="grdSCMHead_InitNewRow"
                                                            OnRowInserting="grdSCMHead_RowInserting"
                                                            OnStartRowEditing="grdSCMHead_StartRowEditing"
                                                            OnRowUpdating="grdSCMHead_RowUpdating"
                                                            OnRowDeleting="grdSCMHead_RowDeleting" 
                                                            OnBeforeGetCallbackResult="grdSCMHead_BeforeGetCallbackResult">

                                                            <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                                                    <EditItemTemplate>
                                                                        <%--<dx:ASPxTextBox ID="ASPxCtrlTextBox1" runat="server" Width="100%" Text='<%#Eval("EffectDate")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                                        <dx:ASPxLabel ID="ASPxCtrlTextBox" runat="server" Width="100%" Text='<%#Eval("Ctrl")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="EffectDate" Caption="Effect Date" VisibleIndex="3">
                                                                    <EditItemTemplate>
                                                                        <dx:ASPxDateEdit ID="EffectDate" ClientInstanceName="EffectDateHeadDirect" runat="server" Value='<%#Eval("EffectDate")%>' Theme="Office2010Blue" AllowUserInput="false"
                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                            <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                                                                        </dx:ASPxDateEdit>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UserKey" Visible="false" VisibleIndex="5"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Head" VisibleIndex="6">
                                                                    <EditItemTemplate>
                                                                        <dx:ASPxComboBox ID="SCMHead" runat="server" ClientInstanceName="SCMHeadDirect" OnInit="SCMHead_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
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
                                        <dx:SplitterPane ScrollBars="Auto">
                                            <ContentCollection>
                                                <dx:SplitterContentControl runat="server">
                                                    <%--Inventory Analyst--%>
                                                    <h3 style="text-align: center; width: 100%; margin-top: 2px;">Inventory Analyst</h3>
                                                    <div style="width: 100%;">
                                                        <dx:ASPxGridView ID="grdSCMInventoryAnal" runat="server"
                                                            ClientInstanceName="grdSCMInventoryAnalDirect"
                                                            EnableTheming="True"
                                                            KeyboardSupport="true" Style="margin: 0 auto;"
                                                            Width="100%"
                                                            EnableCallBacks="true"
                                                            KeyFieldName="PK"
                                                            Theme="Office2010Blue"
                                                            OnInitNewRow="grdSCMInventoryAnal_InitNewRow"
                                                            OnRowInserting="grdSCMInventoryAnal_RowInserting"
                                                            OnStartRowEditing="grdSCMInventoryAnal_StartRowEditing"
                                                            OnRowUpdating="grdSCMInventoryAnal_RowUpdating"
                                                            OnRowDeleting="grdSCMInventoryAnal_RowDeleting" 
                                                            OnBeforeGetCallbackResult="grdSCMInventoryAnal_BeforeGetCallbackResult">

                                                            <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                                                    <EditItemTemplate>
                                                                        <%--<dx:ASPxTextBox ID="ASPxCtrlTextBoxAnal" runat="server" Width="100%" Text='<%#Eval("EffectDate")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                                        <dx:ASPxLabel ID="ASPxCtrlTextBoxAnal" runat="server" Width="100%" Text='<%#Eval("Ctrl")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="EffectDate" Caption="Effect Date" VisibleIndex="3">
                                                                    <EditItemTemplate>
                                                                        <dx:ASPxDateEdit ID="EffectDateAnal" ClientInstanceName="EffectDateAnalDirect" runat="server" Value='<%#Eval("EffectDate")%>' Theme="Office2010Blue" AllowUserInput="false"
                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                            <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                                                                        </dx:ASPxDateEdit>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UserKey" Visible="false" VisibleIndex="5"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Inventory Analyst" VisibleIndex="6">
                                                                    <EditItemTemplate>
                                                                        <dx:ASPxComboBox ID="InventoryAnal" runat="server" ClientInstanceName="InventoryAnalDirect" OnInit="InventoryAnal_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="" />
                                                                        </dx:ASPxComboBox>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="7">
                                                                    <EditItemTemplate>
                                                                        <%--<dx:ASPxTextBox ID="ASPxLastModifiedTextBoxAnal" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                                        <dx:ASPxLabel ID="ASPxLastModifiedTextBoxAnal" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                                                            <SettingsText ConfirmDelete="Delete This Inventory Analyst?" />
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
                                </dx:ASPxSplitter>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Procurement Officer" Visible="true" ActiveTabStyle-Font-Bold="true">
                        <ContentCollection>
                            <dx:ContentControl ID="tabProcurementOff" runat="server">
                                <dx:ASPxSplitter ID="ProcurementOfficerSplitter" runat="server" ClientInstanceName="ProcurementOfficerSplitterDirect" BackColor="#e6eff7" AllowResize="true" Border-BorderStyle="None" Width="100%" Height="100%">
                                    <Panes>
                                        <dx:SplitterPane Size="70%" ScrollBars="Auto">
                                            <ContentCollection>
                                                <dx:SplitterContentControl ID="splitProcOff" runat="server">
                                                    <%--Procurement Officer--%>
                                                    <h3 style="text-align: center; width: 100%; margin-top: 2px;">Procurement Officer</h3>
                                                    <div id="dvSMCProcOff" runat="server" style="width: 100%;">
                                                        <dx:ASPxGridView ID="grdSCMProcurementOff" runat="server"
                                                            ClientInstanceName="grdSCMProcurementOffDirect"
                                                            EnableTheming="True"
                                                            KeyboardSupport="true" Style="margin: 0 auto;"
                                                            Width="100%"
                                                            EnableCallBacks="true"
                                                            KeyFieldName="PK"
                                                            Theme="Office2010Blue"
                                                            OnInitNewRow="grdSCMProcurementOff_InitNewRow"
                                                            OnRowInserting="grdSCMProcurementOff_RowInserting"
                                                            OnStartRowEditing="grdSCMProcurementOff_StartRowEditing"
                                                            OnRowUpdating="grdSCMProcurementOff_RowUpdating"
                                                            OnRowDeleting="grdSCMProcurementOff_RowDeleting" 
                                                            OnBeforeGetCallbackResult="grdSCMProcurementOff_BeforeGetCallbackResult">

                                                            <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                            <%--FocusedRowChanged="OnGridFocusedRowChangedSCMProcOff"--%>

                                                            <%--function (s, e) {grdSCMProcurementOffDetailsDirect.PerformCallback('AddNew');}"--%>
                                                            <%--RowClick="OnGridFocusedRowChangedSCMProcOff"
                                                            EndCallback="OnGridFocusedRowChangedSCMProcOff_EndCallback"--%>

                                                            <ClientSideEvents
                                                                RowClick="function (s, e) {grdSCMProcurementOffDetailsDirect.PerformCallback('ProcOff');}"
                                                                EndCallback="function (s,e) {grdSCMProcurementOffDetailsDirect.Refresh();}" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                                                    <EditItemTemplate>
                                                                        <%--<dx:ASPxTextBox ID="ASPxCtrlTextBoxProcOff" runat="server" Width="100%" Text='<%#Eval("EffectDate")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                                        <dx:ASPxLabel ID="ASPxCtrlTextBoxProcOff" runat="server" Width="100%" Text='<%#Eval("Ctrl")%>' Theme="Office2010Blue"></dx:ASPxLabel>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="EffectDate" Caption="Effect Date" VisibleIndex="3">
                                                                    <EditItemTemplate>
                                                                        <dx:ASPxDateEdit ID="EffectDateProcOff" ClientInstanceName="EffectDateProcOffDirect" runat="server" Value='<%#Eval("EffectDate")%>' Theme="Office2010Blue" AllowUserInput="false" Width="100%"
                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                                            <ClientSideEvents GotFocus="function(s, e) { s.ShowDropDown(); }" />
                                                                        </dx:ASPxDateEdit>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UserKey" Visible="false" VisibleIndex="5"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="UserCompleteName" Caption="Procurement Officer" VisibleIndex="6">
                                                                    <EditItemTemplate>
                                                                        <dx:ASPxComboBox ID="ProcurementOff" runat="server" ClientInstanceName="ProcurementOffDirect" OnInit="ProcurementOff_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="" />
                                                                        </dx:ASPxComboBox>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="7">
                                                                    <EditItemTemplate>
                                                                        <%--<dx:ASPxTextBox ID="ASPxLastModifiedTextBoxProcOff" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                                        <dx:ASPxLabel ID="ASPxLastModifiedTextBoxProcOff" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                                                            <SettingsText ConfirmDelete="Delete This Procurement Officer?" />
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
                                        <dx:SplitterPane ScrollBars="Auto">
                                            <ContentCollection>
                                                <dx:SplitterContentControl runat="server">
                                                    <%--Procurement Category--%>
                                                    <h3 style="text-align: center; width: 100%; margin-top: 2px;">Assigned Procurement Category</h3>
                                                    <div style="width: 100%;">

                                                        <dx:ASPxGridView ID="grdSCMProcurementOffDetails" runat="server"
                                                            ClientInstanceName="grdSCMProcurementOffDetailsDirect"
                                                            EnableTheming="True"
                                                            KeyboardSupport="true" Style="margin: 0 auto;"
                                                            Width="100%"
                                                            EnableCallBacks="true"
                                                            KeyFieldName="PK"
                                                            Theme="Office2010Blue"
                                                            OnCustomCallback="grdSCMProcurementOffDetails_CustomCallback"
                                                            OnInitNewRow="grdSCMProcurementOffDetails_InitNewRow"
                                                            OnRowInserting="grdSCMProcurementOffDetails_RowInserting"
                                                            OnStartRowEditing="grdSCMProcurementOffDetails_StartRowEditing"
                                                            OnRowUpdating="grdSCMProcurementOffDetails_RowUpdating"
                                                            OnRowDeleting="grdSCMProcurementOffDetails_RowDeleting" 
                                                            OnBeforeGetCallbackResult="grdSCMProcurementOffDetails_BeforeGetCallbackResult">

                                                            <ClientSideEvents
                                                                CustomButtonClick=""
                                                                EndCallback="function (s,e) {grdSCMProcurementOffDetailsDirect.InCallback();}" />

                                                            <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="false" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <div style="text-align: left">
                                                                            <dx:ASPxButton ID="AddProcCat" ClientInstanceName="AddProcCatDirect" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                <ClientSideEvents Click="function (s, e) {grdSCMProcurementOffDetailsDirect.PerformCallback('AddNew');}" />
                                                                            </dx:ASPxButton>
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="MasterKey" Visible="false" VisibleIndex="2">
                                                                    <EditItemTemplate>
                                                                        <%--<dx:ASPxTextBox ID="ASPxMasterKeyTextBox" runat="server" Width="100%" Text='<%#Eval("MasterKey")%>' Theme="Office2010Blue" Enabled="false"></dx:ASPxTextBox>--%>
                                                                        <dx:ASPxHiddenField ID="ASPxMasterKeyHiddenField" runat="server"></dx:ASPxHiddenField>
                                                                    </EditItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ProcCat" Visible="false" VisibleIndex="3"></dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ProcCatDesc" Caption="Procurement Category" VisibleIndex="4" SortOrder="Ascending">
                                                                    <EditItemTemplate>
                                                                        <dx:ASPxComboBox ID="ProcurementCat" runat="server" ClientInstanceName="ProcurementCatDirect" OnInit="ProcurementCat_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                            ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="" />
                                                                        </dx:ASPxComboBox>
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
                                                            <SettingsText ConfirmDelete="Delete This Procurement Category?" />
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
                                </dx:ASPxSplitter>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
    </div>
</asp:Content>
