<%@ Page Title="MOP Workflow" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_workflow.aspx.cs" Inherits="HijoPortal.mrp_workflow" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>M O P  Workflow</h1>
        </div>
        <div id="dvMOPWorkflow" runat="server">
            <dx:ASPxSplitter ID="MRPWorkflowSplitter" runat="server" ClientInstanceName="MRPWorkflowSplitterDirect" BackColor="#e6eff7" AllowResize="true" Border-BorderStyle="None" Width="100%" Height="100%">
                <Panes>
                    <dx:SplitterPane Size="50%" ScrollBars="Auto">
                        <ContentCollection>
                            <dx:SplitterContentControl runat="server">
                                <%--Worflow Master--%>
                                <dx:ASPxGridView ID="grdWorkflowMaster" runat="server"
                                    ClientInstanceName="grdWorkflowMasterDirect"
                                    EnableTheming="True"
                                    KeyboardSupport="true" Style="margin: 0 auto;"
                                    Width="100%"
                                    Theme="Office2010Blue"
                                    OnInitNewRow="grdWorkflowMaster_InitNewRow"
                                    OnRowInserting="grdWorkflowMaster_RowInserting"
                                    OnRowDeleting="grdWorkflowMaster_RowDeleting"
                                    OnStartRowEditing="grdWorkflowMaster_StartRowEditing"
                                    OnRowUpdating="grdWorkflowMaster_RowUpdating" 
                                    OnBeforeGetCallbackResult="grdWorkflowMaster_BeforeGetCallbackResult">

                                    <%--<ClientSideEvents RowClick="function(s,e){focusedWorkflowMaster(s,e,'WorkflowMaster');}" />--%>

                                    <SettingsBehavior AllowSort="true" SortMode="Value" />

                                    <Columns>
                                        <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="50px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Ctrl" Caption="Ctrl" VisibleIndex="2" SortOrder="Ascending"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="EffectDate" Caption="Effect Date" VisibleIndex="3"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="EntCode" Visible="false" VisibleIndex="4"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="EntCodeDesc" Caption="Entity" VisibleIndex="5"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="BUCode" Visible="false" VisibleIndex="6"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="BU / SSU" VisibleIndex="7"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="BUHead" Visible="false" VisibleIndex="8"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="BUHeadName" Caption="Department Head" VisibleIndex="9"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DateCreated" Caption="Date Created" VisibleIndex="10"></dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsCommandButton>
                                        <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                        <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                        <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                    </SettingsCommandButton>

                                    <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>

                                    <Templates>
                                        <EditForm>
                                            <div style="padding: 4px 3px 4px 3px">
                                                <dx:ASPxPageControl ID="WorkflowMasterPageControl" runat="server" Width="100%" Theme="Office2010Blue">
                                                    <TabPages>
                                                        <dx:TabPage Text="Workflow Master" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%; padding: 10px;">
                                                                        <tr>
                                                                            <td style="width: 200px;">
                                                                                <dx:ASPxLabel runat="server" Text="Effect Date" Theme="Office2010Blue" />
                                                                            </td>
                                                                            <td style="width: 500px; padding: 0px 0px 0px 3px;">
                                                                                <dx:ASPxDateEdit ID="EffectDate" ClientInstanceName="EffectDateDirect" runat="server" Value='<%#Eval("EffectDate")%>' Theme="Office2010Blue"></dx:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="width: 20px;"></td>
                                                                            <%--Spacer--%>
                                                                            <td style="width: 200px;">
                                                                                <dx:ASPxLabel runat="server" Text="BU / SSU Head" Theme="Office2010Blue" />
                                                                            </td>
                                                                            <td style="width: 500px;">
                                                                                <dx:ASPxComboBox ID="BUHead" runat="server" ClientInstanceName="BUHeadDirect" OnInit="BUHead_Init" AutoResizeWithContainer="true" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                    ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="" />
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue" />
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxComboBox ID="EntCode" runat="server" ClientInstanceName="EntCodeDirect" OnInit="EntCode_Init" AutoResizeWithContainer="true" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                    ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="" />
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                            <td></td>
                                                                            <td>
                                                                                <dx:ASPxLabel runat="server" Text="Date Created" Theme="Office2010Blue" />
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="txtDateCreated" runat="server" Text='<%#Eval("DateCreated")%>' Theme="Office2010Blue" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel runat="server" Text="BU / SSU" Theme="Office2010Blue" />
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxComboBox ID="BUCode" runat="server" ClientInstanceName="BUCodeDirect" OnInit="BUCode_Init" AutoResizeWithContainer="true" TextFormatString="{1}" ValueType="System.String" Theme="Office2010Blue"
                                                                                    ValidationSettings-ErrorDisplayMode="None" ValidationSettings-RequiredField-IsRequired="true" Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="" />
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                            <td></td>
                                                                            <td></td>
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
                                                    <ClientSideEvents Click="updateWorkflowMaster" />
                                                </dx:ASPxButton>
                                                <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){grdWorkflowMasterDirect.CancelEdit();}" />
                                                </dx:ASPxButton>
                                            </div>
                                        </EditForm>
                                    </Templates>

                                </dx:ASPxGridView>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane ScrollBars="Auto">
                        <ContentCollection>
                            <dx:SplitterContentControl runat="server">
                                <%--Worflow Details--%>
                                <div style="width: 100%">
                                    <h2 style="width: 100%; text-align: center;">Details</h2>
                                </div>
                                <dx:ASPxCallbackPanel ID="WorkflowDetCallbackPanel" ClientInstanceName="WorkflowDetCallbackPanelDirect" runat="server" Width="100%">
                                    <ClientSideEvents EndCallback="" />
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <dx:ASPxGridView ID="grdWorkflowMasterDetails" runat="server"
                                                ClientInstanceName="grdWorkflowMasterDetailsDirect"
                                                EnableTheming="True"
                                                KeyboardSupport="true" Style="margin: 0 auto;"
                                                Width="100%"
                                                Theme="Office2010Blue">
                                                <SettingsBehavior AllowSort="true" SortMode="Value" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" Width="50px" CellStyle-HorizontalAlign="Left"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="MasterKey" Visible="false" VisibleIndex="2"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Line" Caption="Line" VisibleIndex="2" SortOrder="Ascending"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Description" Caption="Effect Date" VisibleIndex="3"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="DesignatedKey" Visible="false" VisibleIndex="4"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Designated" Caption="Designated Person" VisibleIndex="5"></dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsCommandButton>
                                                    <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>
                                                    <EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px"></EditButton>
                                                    <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
                                                </SettingsCommandButton>
                                            </dx:ASPxGridView>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>
        </div>
    </div>
</asp:Content>
