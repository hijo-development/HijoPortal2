<%@ Page Title="Workflow" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mop_workflow.aspx.cs" Inherits="HijoPortal.mop_workflow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class ="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>Dataflow  Setup</h1>
        </div>
        <div id="dvWorkflowSetup" runat="server" class="scroll-container">
            <dx:ASPxSplitter ID="FinanceSetupSplitter" runat="server" ClientInstanceName="FinanceSetupSplitterDirect" BackColor="#e6eff7" AllowResize="true" Border-BorderStyle="None" Width="100%" Height="100%">
                <Panes>
                    <dx:SplitterPane Size="50%" ScrollBars="Auto">
                        <Panes>
                            <dx:SplitterPane Size="50%" ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server">
                                        <%--MOP Dataflow--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Data Flow</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdDataFlow" runat="server"
                                                ClientInstanceName="grdDataFlowDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue" 
                                                OnCustomCallback="grdDataFlow_CustomCallback" 
                                                OnInitNewRow="grdDataFlow_InitNewRow" 
                                                OnRowInserting="grdDataFlow_RowInserting" 
                                                OnStartRowEditing="grdDataFlow_StartRowEditing" 
                                                OnRowUpdating="grdDataFlow_RowUpdating" 
                                                OnRowDeleting="grdDataFlow_RowDeleting">

                                                <ClientSideEvents 
                                                    RowClick="function (s,e) {grdDataFlowDetailDirect.PerformCallback('DataFlow');}"
                                                    EndCallback="function (s,e) {grdDataFlowDirect.InCallback(); grdDataFlowDetailDirect.Refresh();}" />

                                                <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="false" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <div style="text-align: left">
                                                                <dx:ASPxButton ID="Add" ClientInstanceName="Addirect" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <ClientSideEvents Click="function (s, e) {grdDataFlowDirect.PerformCallback('AddNew');}" />
                                                                </dx:ASPxButton>
                                                            </div>
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                                        <EditItemTemplate>
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
                                                    <dx:GridViewDataColumn FieldName="Remarks" Caption="Remarks" VisibleIndex="4">
                                                        <EditItemTemplate>
                                                            <dx:ASPxTextBox ID="ASPxRemarksTextBox" runat="server" Width="100%" Text='<%#Eval("Remarks")%>' Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                            </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="7">
                                                        <EditItemTemplate>
                                                            <dx:ASPxLabel ID="ASPxLastModifiedTextBox" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                                                <SettingsText ConfirmDelete="Delete This Dataflow?" />
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
                                        <%--MOP Dataflow Details--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Data Flow Details</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdDataFlowDetail" runat="server"
                                                ClientInstanceName="grdDataFlowDetailDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue" 
                                                OnCustomCallback="grdDataFlowDetail_CustomCallback" 
                                                OnInitNewRow="grdDataFlowDetail_InitNewRow"
                                                OnRowInserting="grdDataFlowDetail_RowInserting" 
                                                OnStartRowEditing="grdDataFlowDetail_StartRowEditing" 
                                                OnRowUpdating="grdDataFlowDetail_RowUpdating" 
                                                OnRowDeleting="grdDataFlowDetail_RowDeleting">

                                                <ClientSideEvents
                                                    CustomButtonClick=""
                                                    EndCallback="function (s,e) {grdDataFlowDetailDirect.InCallback();}" />

                                                <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="false" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <div style="text-align: left">
                                                                <dx:ASPxButton ID="Add" ClientInstanceName="Addirect" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <ClientSideEvents Click="function (s, e) {grdDataFlowDetailDirect.PerformCallback('AddNew');}" />
                                                                </dx:ASPxButton>
                                                            </div>
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Line" Caption="Line" VisibleIndex="2" Width="100px" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <dx:ASPxTextBox ID="ASPxLineTextBox" runat="server" Width="100%" Text='<%#Eval("Line")%>' Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                            </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="PositionNameKey" Visible="false" VisibleIndex="3"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="PositionName" Caption="Position Title" VisibleIndex="4" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <dx:ASPxComboBox ID="PositionName" runat="server" ClientInstanceName="PositionNameDirect" OnInit="PositionName_Init" AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                            </dx:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsCommandButton>
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
                                                <SettingsText ConfirmDelete="Delete This Line?" />
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
                    <dx:SplitterPane ScrollBars="Auto">
                        <Panes>
                            <dx:SplitterPane Size="50%" ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server">
                                        <%--MOP Approver--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Approver</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdApproval" runat="server"
                                                ClientInstanceName="grdApprovalDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue" 
                                                OnCustomCallback="grdApproval_CustomCallback" 
                                                OnInitNewRow="grdApproval_InitNewRow" 
                                                OnRowInserting="grdApproval_RowInserting" 
                                                OnStartRowEditing="grdApproval_StartRowEditing" 
                                                OnRowUpdating="grdApproval_RowUpdating" 
                                                OnRowDeleting="grdApproval_RowDeleting">

                                                <ClientSideEvents
                                                    RowClick="function (s,e) {grdApprovalDetailDirect.PerformCallback('Approval');}"
                                                    EndCallback="function (s,e) {grdApprovalDirect.InCallback(); grdApprovalDetailDirect.Refresh();}" />

                                                <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <div style="text-align: left">
                                                                <dx:ASPxButton ID="Add" ClientInstanceName="Addirect" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <ClientSideEvents Click="function (s, e) {grdApprovalDirect.PerformCallback('AddNew');}" />
                                                                </dx:ASPxButton>
                                                            </div>
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending">
                                                        <EditItemTemplate>
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
                                                    <dx:GridViewDataColumn FieldName="Remarks" Caption="Remarks" VisibleIndex="4">
                                                        <EditItemTemplate>
                                                            <dx:ASPxTextBox ID="ASPxRemarksTextBox" runat="server" Width="100%" Text='<%#Eval("Remarks")%>' Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                            </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="LastModified" Caption="Last Modified" VisibleIndex="7">
                                                        <EditItemTemplate>
                                                            <dx:ASPxLabel ID="ASPxLastModifiedTextBox" runat="server" Width="100%" Text='<%#Eval("LastModified")%>' Theme="Office2010Blue"></dx:ASPxLabel>
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
                            <dx:SplitterPane ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server">
                                        <%--MOP Approver Details--%>
                                        <h3 style="text-align: center; width: 100%; margin-top: 2px;">Approver Details</h3>
                                        <div style="width: 100%;">
                                            <dx:ASPxGridView ID="grdApprovalDetail" runat="server"
                                                ClientInstanceName="grdApprovalDetailDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                EnableCallBacks="true"
                                                KeyFieldName="PK"
                                                Theme="Office2010Blue" 
                                                OnCustomCallback="grdApprovalDetail_CustomCallback" 
                                                OnInitNewRow="grdApprovalDetail_InitNewRow" 
                                                OnRowInserting="grdApprovalDetail_RowInserting" 
                                                OnStartRowEditing="grdApprovalDetail_StartRowEditing" 
                                                OnRowUpdating="grdApprovalDetail_RowUpdating" 
                                                OnRowDeleting="grdApprovalDetail_RowDeleting">

                                                <ClientSideEvents
                                                    CustomButtonClick=""
                                                    EndCallback="function (s,e) {grdApprovalDetailDirect.InCallback();}" />

                                                <SettingsBehavior AllowSort="true" SortMode="Value" />

                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="false" VisibleIndex="0" Width="40px" CellStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <div style="text-align: left">
                                                                <dx:ASPxButton ID="Add" ClientInstanceName="Addirect" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <ClientSideEvents Click="function (s, e) {grdApprovalDetailDirect.PerformCallback('AddNew');}" />
                                                                </dx:ASPxButton>
                                                            </div>
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Line" Caption="Line" VisibleIndex="2" Width="100px" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <dx:ASPxTextBox ID="ASPxLineTextBox" runat="server" Width="100%" Text='<%#Eval("Line")%>' Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true">
                                                            </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="PositionNameKey" Visible="false" VisibleIndex="3"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="PositionName" Caption="Position Title" VisibleIndex="4" SortOrder="Ascending">
                                                        <EditItemTemplate>
                                                            <dx:ASPxComboBox ID="PositionName" runat="server" ClientInstanceName="PositionNameDirect" OnInit="PositionName_Init1"  AutoResizeWithContainer="false" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                            </dx:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsCommandButton>
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
                                                <SettingsText ConfirmDelete="Delete This Line?" />
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
