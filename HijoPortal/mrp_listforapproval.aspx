<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_listforapproval.aspx.cs" Inherits="HijoPortal.mrp_listforapproval" %>
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
                <Columns>
                    <dx:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="ForApprovalGridEdit" Image-Url="images/Refresh.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DocNumber"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DateCreated"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="Business Unit"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="MRP Month"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPYear" Caption="MRP Year"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="WorkLine" Visible="true"></dx:GridViewDataColumn>
                </Columns>
                <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                <SettingsBehavior AllowFocusedRow="true" AllowSort="true" />
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
