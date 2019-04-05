<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgotpassword.aspx.cs" Inherits="HijoPortal.forgotpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/ChangePassword.css" rel="stylesheet" />
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
            ResetPasswordClient.SetEnabled(IdNumberClient.GetText().length > 0);
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="dvBanner" runat="server" style="height: 100px;">
            <table style="width: 100%; height: 100%">
                <tr style="height: 100px;">
                    <td style="width: 80px; height: 80px; padding: 10px;">
                        <img src="images/HijoLogo.png" style="height: 60px; width: 60px;" />
                    </td>
                    <td class="Header-td">
                        <h1>HIJO Portal</h1>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div id="Div1" runat="server" class="ContentWrapper">
                <div id="dvHeader">
                    <h1>Reset Password</h1>
                </div>
                <div style="width: 100%; padding-top: 30px;">
                    <table id="tblChangePW" border="0">
                        <tr>
                            <td style="width: 100px; vertical-align: top;">
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
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="ResetPassword" runat="server" ClientInstanceName="ResetPasswordClient" OnClick="ResetPassword_Click" ClientEnabled="false" Text="Reset Password" Theme="iOS"></dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Cancel" runat="server" OnClick="Cancel_Click" Text="Cancel" Theme="iOS"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
