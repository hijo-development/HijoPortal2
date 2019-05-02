<%@ Page Title="for Deliberation" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_listforapproval.aspx.cs" Inherits="HijoPortal.mrp_listforapproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>M O P  List (For Approval)</h1>
        </div>
        <div>
            <dx:ASPxGridView ID="ListForApprovalGrid" runat="server" Theme="Office2010Blue" Width="100%"
                OnCustomButtonCallback="ListForApprovalGrid_CustomButtonCallback">
                <ClientSideEvents CustomButtonClick="ListForApprovalGrid_CustomButtonClick" />
                <ClientSideEvents BeginCallback="function(s,e){loadingPanel.Show();}" />
                <%--<ClientSideEvents EndCallback="function(s,e){loadingPanel.Hide();}" />--%>
                <Columns>
                    <dx:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="ForApprovalGridEdit" Image-Url="images/Edit.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DocNumber" Caption="Document #" Width="140px" SortOrder="Descending"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DateCreated" Caption="Date Created"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="BU / Department"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="Month"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPYear" Caption="Year"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="WorkLine" Visible="false"></dx:GridViewDataColumn>
                </Columns>
                <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                <SettingsBehavior AllowFocusedRow="true" AllowSort="true" />
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
