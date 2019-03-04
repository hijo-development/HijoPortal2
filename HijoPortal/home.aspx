<%@ Page Title="Home" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="HijoPortal.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class ="ContentWrapper">
        <dx:ASPxGridView ID="HomeGrid" runat="server" Theme="Office2010Blue" Width="100%">
            <Columns>
                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                <dx:GridViewDataHyperLinkColumn FieldName="DocNumber" Width="200px">
                    <DataItemTemplate>
                        <dx:ASPxHyperLink OnInit="DocNumBtn_Init" ID="DocNumBtn" runat="server" Text='<%#Eval("DocNumber")%>' >
                        </dx:ASPxHyperLink>
                        <%-- NavigateUrl='<%# string.Format("mrp_addedit.aspx?DocNum={0}&WrkFlwLn=1", Eval("DocNumber")) %>' --%>
                    </DataItemTemplate>
                </dx:GridViewDataHyperLinkColumn>
                <dx:GridViewDataColumn FieldName="EntityCodeDesc" Width="400px"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="BUCodeDesc"></dx:GridViewDataColumn>
            </Columns>
            <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" />
            <SettingsBehavior AllowFocusedRow="true" ProcessSelectionChangedOnServer="true" AllowSort="true" AllowHeaderFilter="true" />
        </dx:ASPxGridView>
    </div>
</asp:Content>
