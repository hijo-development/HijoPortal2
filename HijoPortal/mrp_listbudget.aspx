<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_listbudget.aspx.cs" Inherits="HijoPortal.mrp_listbudget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <dx:ASPxGridView ID="ListBudgetGrid" runat="server" Theme="Office2010Blue" Width="100%"
             OnCustomButtonCallback="ListBudgetGrid_CustomButtonCallback">
            <ClientSideEvents CustomButtonClick="ListBudgetGrid_CustomButtonClick" />
            <Columns>
                <dx:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="BudgetGridEdit" Image-Url="images/Edit.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="DocNumber"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="DateCreated"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="Business Unit"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="MRP Month"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="MRPYear" Caption="MRP Year"></dx:GridViewDataColumn>
            </Columns>
            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
            <SettingsBehavior AllowFocusedRow="true" AllowSort="true" />
        </dx:ASPxGridView>
    </div>
</asp:Content>
