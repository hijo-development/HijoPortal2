<%@ Page Title="MOP Inventory Analyst" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_listinventoryanalyst.aspx.cs" Inherits="HijoPortal.mrp_listinventoryanalyst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class ="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>M O P  List (Inventory Analyst)</h1>
            <asp:Label ID="msgTrans" runat="server" Visible="false"></asp:Label>
        </div>
        <div>
            <dx:ASPxGridView ID="grdMRPListInventAnalyst" ClientInstanceName="grdMRPListInventAnalystDirect" runat="server" 
                EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue" 
                OnCustomButtonCallback="grdMRPListInventAnalyst_CustomButtonCallback">
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="20">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DocNumber" Caption="MRP Number" VisibleIndex="2" SortOrder="Descending"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity" VisibleIndex="3"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="BU / Department" VisibleIndex="4"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="Month" VisibleIndex="5"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPYear" Caption="Year" VisibleIndex="6"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn  Caption="Status" VisibleIndex="9"></dx:GridViewDataColumn>
                </Columns>
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
                <Styles>
                    <SelectedRow Font-Bold="False" Font-Italic="False">
                    </SelectedRow>
                    <FocusedRow Font-Bold="False" Font-Italic="False">
                    </FocusedRow>
                </Styles>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
