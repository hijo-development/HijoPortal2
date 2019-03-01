<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="change_pw.aspx.cs" Inherits="HijoPortal.change_pw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div id="dvContentWrapper" runat="server" class ="ContentWrapper">--%>
        <div class="ChangePW-box">
            <%--<img src="images/avatar.png" class="avatar" />--%>
            <h1>Change Password</h1>
            <p>Old Password</p>
            <asp:TextBox ID="txtOldPassword" placeholder="Enter Old Password" runat="server" TabIndex="0" TextMode="Password"></asp:TextBox>
            <p>New Password</p>
            <asp:TextBox ID="txtNewPassword" placeholder="Enter New Password" runat="server" TabIndex="1" TextMode="Password"></asp:TextBox>
            <p>Confirm Password</p>
            <asp:TextBox ID="txtConfirmPassword" placeholder="Enter Confirm Password" runat="server" TabIndex="2" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnChangePW" runat="server" Text="Change Password" TabIndex="3" OnClick="btnChangePW_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" OnClick="btnCancel_Click" />
            <asp:Label ID="lblerror" runat="server" CssClass="text-danger" Text=""></asp:Label>
        </div>
    <%--</div>--%>
</asp:Content>
