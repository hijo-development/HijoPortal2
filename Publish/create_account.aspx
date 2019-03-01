<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="create_account.aspx.cs" Inherits="HijoPortal.create_account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Account</title>
    <link rel="stylesheet" type="text/css" href="css/LogIn.css" />
    <link rel="stylesheet" type="text/css" href="css/LogInICON.css" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="create-account-box">
            <img src="images/HijoLogo.png" class="avatar" />
            <h1>Sign Up Here</h1>
            <asp:UpdatePanel ID="UpdatePanelAccount" runat="server">
                <ContentTemplate>
                    <table class="nav-justified">
                        <tr>
                            <td style="width: 300px;">Account Type
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <asp:DropDownList ID="AccountType" CssClass="mydropdownlist1" runat="server" AutoPostBack="True" TabIndex="8" AutoCompleteType="Disabled" OnSelectedIndexChanged="AccountType_SelectedIndexChanged"></asp:DropDownList>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </td>
                            <td style="width: 50px;"></td>
                            <td style="width: 300px;">
                                <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>--%>
                                <asp:Label ID="lblIDNum" runat="server" Text="ID Number"></asp:Label>
                                <asp:Panel ID="panelIDNumber" runat="server" DefaultButton="btnSearchIDNum">
                                    <asp:TextBox ID="txtIDNumber" placeholder="Enter ID Number" runat="server" TabIndex="0" AutoCompleteType="Disabled" OnTextChanged="txtIDNumber_TextChanged"></asp:TextBox>
                                    <asp:Button ID="btnSearchIDNum" runat="server" Text="ID" Style="display: none;" OnClick="btnSearchIDNum_Click" />
                                    <asp:TextBox ID="txtEmployeeKey" runat="server" Style="display: none;"></asp:TextBox>
                                </asp:Panel>
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 300px;">Last Name
                        <asp:TextBox ID="txtLastName" placeholder="Enter Last Name" runat="server" TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style="width: 50px;"></td>
                            <td style="width: 300px;">User Name
                        <asp:TextBox ID="txtUserName" placeholder="Enter User Name" runat="server" TabIndex="5" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style="width: 50px;"></td>
                            <td rowspan="3">
                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>--%>
                                <img id="AccountPicture" runat="server" src="images/ID.jpg" width="200" height="200" />
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>First Name
                        <asp:TextBox ID="txtFirstName" placeholder="Enter First Name" runat="server" TabIndex="2" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Password
                        <asp:TextBox ID="txtPassword" placeholder="Enter Confirm Password" runat="server" TabIndex="6" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Middle Name
                        <asp:TextBox ID="txtMiddleName" placeholder="Enter Middle Name" runat="server" TabIndex="3" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Confirm Password
                        <asp:TextBox ID="txtConfirmPassword" placeholder="Enter Confirm Password" runat="server" TabIndex="7" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email Address
                        <asp:TextBox ID="txtEmailAdd" placeholder="Enter Email" runat="server" TabIndex="4" AutoCompleteType="Disabled" TextMode="Email"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td style="padding-top: 30px;">
                                <asp:Button ID="btnRegister" runat="server" Text="Register" Width="50%" BorderColor="Blue" OnClick="btnRegister_Click" /><asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="50%" BorderColor="Blue" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr style="height: 80px;">
                            <td>
                                <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>--%>
                                <asp:Label ID="lblDomainAccount" runat="server" Text="Domain Account"></asp:Label>
                                <asp:TextBox ID="txtDomainAccount" placeholder="Enter Domain Account" runat="server" TabIndex="9" AutoCompleteType="Disabled"></asp:TextBox>
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </td>
                            <td></td>
                            <td colspan="3">
                                <asp:Label ID="lblUserType" runat="server"></asp:Label>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                <asp:Timer ID="TimerLogIn" runat="server" Interval="1000" OnTick="TimerLogIn_Tick"></asp:Timer>
                            </td>
                            <%--<td></td>
                    <td></td>--%>
                        </tr>
                        <%--<tr>
                    <td colspan="5">
                    </td>
                </tr>--%>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
