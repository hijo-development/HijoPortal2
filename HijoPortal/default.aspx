<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HijoPortal._default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <link rel="stylesheet" type="text/css" href="css/LogIn.css" />
    <link rel="stylesheet" type="text/css" href="css/LogInICON.css" />
    <script src="jquery/ContentPage.js" type="text/javascript"></script>
    <script type="text/javascript">
        function FilterDigit_NumberOnly_KeyPress(s, e) {
            var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            //KEY (TAB) keycode: 0
            //KEY (0 to 9) keycode: 48-57
            //Key (DEL)    keycode: 8
            if ((key >= 48 && key <= 57) || key == 8 || key == 0) {
                return true;
            } else {
                ASPxClientUtils.PreventEvent(e.htmlEvent);
            }
        }
        function EnableButton_KeyUp(s, e) {
            ResetBtnClient.SetEnabled(IdNumberClient.GetText().length > 0);
        }
        function forgot_password_click() {
            ResetPopUpClient.Show();
        }

        function enterEvent(e) {
            if (e.keyCode == 13) {
                document.getElementById("btnLogIn").click();
            }
        }

        //$(function () {
        //    $("#btnLogIn").on("click", function () {
        //        $find('ModalPopupExtenderLoading').show();
        //    });
        //});

    </script>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:TextBox ID="TextBoxLoading" runat="server" Visible="true" Style="display: none;"></asp:TextBox>
        <ajaxToolkit:ModalPopupExtender runat="server"
            ID="ModalPopupExtenderLoading"
            BackgroundCssClass="modalBackground"
            PopupControlID="PanelLoading"
            TargetControlID="TextBoxLoading"
            CancelControlID="ButtonErrorOK1"
            ClientIDMode="Static">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelLoading" runat="server"
            CssClass="modalPopupLoading"
            Height="200px"
            Width="200px"
            align="center"
            Style="display: none;">
            <img src="images/Loading.gif" style="height: 200px; width: 200px;" />
            <asp:Button ID="ButtonErrorOK1" runat="server" CssClass="buttons" Width="30%" Text="OK" Style="display: none;" />
        </asp:Panel>

        <dx:ASPxPopupControl ID="ResetPopUp" runat="server" ClientInstanceName="ResetPopUpClient" Width="450px" Height="80px" Theme="Office2010Silver" Modal="true" HeaderText="Reset Password" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <table id="tblChangePW" border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 100px; vertical-align: middle;">
                                <dx:ASPxLabel runat="server" Text="ID No." Theme="iOS"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="IdNumber" runat="server" ClientInstanceName="IdNumberClient" Width="100%" Theme="iOS" HorizontalAlign="Right">
                                    <ClientSideEvents KeyPress="FilterDigit_NumberOnly_KeyPress" />
                                    <ClientSideEvents KeyUp="EnableButton_KeyUp" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="padding-top: 5px;">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="ResetBtn" runat="server" OnClick="ResetBtn_Click" ClientInstanceName="ResetBtnClient" ClientEnabled="false" Text="Reset Password" Theme="Glass"></dx:ASPxButton>
                                            <dx:ASPxButton ID="CancelBtn" runat="server" Text="Cancel" AutoPostBack="false" Theme="Glass">
                                                <ClientSideEvents Click="function(s,e){ResetPopUpClient.Hide();}" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <div class="login-box">
            <img src="images/HijoLogo.png" class="avatar" />
            <h1>Login Here</h1>
            <p>Username</p>
            <asp:TextBox ID="txtUserName" placeholder="Enter Username" runat="server" AutoCompleteType="Disabled" onkeydown="javascript:enterEvent(event);"></asp:TextBox>
            <p>Password</p>
            <asp:TextBox ID="txtPassword" placeholder="Enter Password" runat="server" TextMode="Password" onkeydown="javascript:enterEvent(event);"></asp:TextBox>
            <asp:Button ID="btnLogIn" runat="server" Text="Log in" OnClientClick="function () { $find('ModalPopupExtenderLoading').show(); }" OnClick="btnLogIn_Click" />
            <%--<asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" OnClick="btnCreateAccount_Click" />--%>
            <%--<asp:HyperLink ID="HyperLink1" runat="server">Create Account?</asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" runat="server">Forgot password?</asp:HyperLink>--%>
            <div style="width: 100%; align-content: center;">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: center; align-content: center;">
                            <a href="createaccount.aspx">Create Account?</a>
                            &nbsp
                            <a href="#" onclick="forgot_password_click();">Forgot password?</a>
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
