<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="HijoPortal.change_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/ChangePassword.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/HijoLogo.png" />
    <script type="text/javascript" src="jquery/changePW.js"></script>
    <title>Change Password</title>
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
            <div id="dvContentWrapper" runat="server" class="ContentWrapper">
                <div id="dvHeader">
                    <h1>Change Password</h1>
                </div>
                <div style="width: 100%; padding-top: 30px;">
                    <table id="tblChangePW" border="0">
                        <tr>
                            <td style="width: 130px; vertical-align: top;">
                                <table style="width: auto;">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Old Password" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td style="width: 250px;">
                                <dx:ASPxTextBox ID="oldPasswordCH" runat="server" ClientInstanceName="oldPasswordCH" Password="true" Width="100%" Theme="iOS">
                                    <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                        <ErrorFrameStyle Wrap="True" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="OnPassValidationChangePW" />
                                </dx:ASPxTextBox>

                            </td>
                            <td>
                                <div style="display: none;">
                                    <dx:ASPxTextBox ID="oldPasswordCHDB" runat="server" Width="170px" ClientInstanceName="oldPasswordCHDB" Theme="iOS">
                                    </dx:ASPxTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <table style="width: auto;">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="New Password" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td>
                                <dx:ASPxTextBox ID="newPasswordCH" runat="server" ClientInstanceName="newPasswordCH" Password="true" Width="100%" Theme="iOS">
                                    <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                        <ErrorFrameStyle Wrap="True" />
                                    </ValidationSettings>
                                    <ClientSideEvents Init="OnPasswordTextBoxInitChangePW" KeyUp="OnPassChangedChangePW" Validation="OnPassValidationChangePW" />
                                </dx:ASPxTextBox>
                                <div style="padding-top: 10px;">
                                    <dx:ASPxRatingControl ID="ratingControlChangePW" runat="server" ReadOnly="true" ItemCount="5" Value="0" ClientInstanceName="ratingControlChangePW" Theme="iOS" />
                                </div>
                                <div style="padding-top: 5px; padding-bottom: 10px">
                                    <dx:ASPxLabel ID="ratingLabelChangePW" runat="server" ClientInstanceName="ratingLabelChangePW" Text="Password safety" Theme="iOS" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top;">
                                <table style="width: auto;">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Confirm Password" Theme="iOS"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td>
                                <dx:ASPxTextBox ID="confirmPasswordCH" runat="server" ClientInstanceName="confirmPasswordCH" Password="true" Width="100%" Theme="iOS">
                                    <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                        <ErrorFrameStyle Wrap="True" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="OnPassValidationChangePW" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="padding-top: 10px; padding-bottom: 10px;">
                                <dx:ASPxCaptcha runat="server" ID="captcha" TextBox-Position="Bottom" TextBox-ShowLabel="false" TextBoxStyle-Width="100%" Width="200" Theme="iOS">
                                    <RefreshButtonStyle Font-Underline="false"></RefreshButtonStyle>
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                    </ValidationSettings>
                                </dx:ASPxCaptcha>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnChangePW" runat="server" Text="Change Password" Width="100%" Theme="iOS" OnClick="btnChangePW_Click"></dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="CancelBtn" runat="server" Text="Cancel" CausesValidation="false" Theme="iOS" OnClick="CancelBtn_Click">
                                            </dx:ASPxButton>
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
