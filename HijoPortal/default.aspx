<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HijoPortal._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <link rel="stylesheet" type="text/css" href="css/LogIn.css" />
    <link rel="stylesheet" type="text/css" href="css/LogInICON.css" />
    <script src="jquery/ContentPage.js" type="text/javascript"></script>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-box">
            <img src="images/HijoLogo.png" class="avatar" />
            <h1>Login Here</h1>
            <p>Username</p>
            <asp:TextBox ID="txtUserName" placeholder="Enter Username" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <p>Password</p>
            <asp:TextBox ID="txtPassword" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnLogIn" runat="server" Text="Log in" OnClick="btnLogIn_Click" />
            <%--<asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" OnClick="btnCreateAccount_Click" />--%>
            <%--<asp:HyperLink ID="HyperLink1" runat="server">Create Account?</asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" runat="server">Forgot password?</asp:HyperLink>--%>
            <div style="width: 100%; align-content: center;">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: center; align-content: center;">
                            <a href="createaccount.aspx">Create Account?</a>
                            &nbsp
                            <a href="#">Forgot password?</a>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%">
                <%--<asp:LinkButton ID="LinkButtonCreateAccount" href="create_account.aspx" runat="server" Width="100%">Create Account</asp:LinkButton>--%>
                <asp:Label ID="lblerror" runat="server" CssClass="text-danger" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
