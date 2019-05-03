<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="HijoPortal.change_password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/ChangePassword.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/HijoLogo.png" />
    <script type="text/javascript">this.history.forward(-1);</script>
    <script type="text/javascript" src="jquery/changePW.js"></script>
    <title>Change Password</title>
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

        <dx:ASPxPopupControl ID="PopupChangePW" ClientInstanceName="PopupChangePW" runat="server" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Moderno">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <table style="width: 100%;" border="0">
                        <tr>
                            <td colspan="2" style="padding-right: 20px; padding-bottom: 20px;">
                                <dx:ASPxLabel runat="server" Text="Are you sure you want to change your password?" Theme="Moderno"></dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <dx:ASPxButton ID="OK_ChangePW" runat="server" Text="Change Password" OnClick="OK_ChangePW_Click" Theme="Moderno" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){
                                    PopupChangePW.Hide();
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="CANCEL_SUBMIT" runat="server" Text="CANCEL" Theme="Moderno" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){PopupChangePW.Hide();}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--<div id="dvBanner" runat="server" style="height: 100px;">
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
        </div>--%>

        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" FullscreenMode="true" BackColor="Transparent">
            <Panes>
                <dx:SplitterPane ScrollBars="Auto">
                    <ContentCollection>
                        <dx:SplitterContentControl>
                            <div>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="height: 50px;"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <%--id="dvContentWrapper" runat="server" class="ContentWrapper"--%>
                                            <div>
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
                                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Old Password" Theme="Moderno"></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                            <td style="width: 250px;">
                                                                <dx:ASPxTextBox ID="oldPasswordCH" runat="server" ClientInstanceName="oldPasswordCH" Password="true" Width="100%" Theme="Moderno">
                                                                    <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                                        <ErrorFrameStyle Wrap="True" />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents Validation="OnPassValidationChangePW" />
                                                                </dx:ASPxTextBox>

                                                            </td>
                                                            <td>
                                                                <div style="display: none;">
                                                                    <dx:ASPxTextBox ID="oldPasswordCHDB" runat="server" Width="170px" ClientInstanceName="oldPasswordCHDB" Theme="Moderno">
                                                                    </dx:ASPxTextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <table style="width: auto;">
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="New Password" Theme="Moderno"></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                            <td>
                                                                <dx:ASPxTextBox ID="newPasswordCH" runat="server" ClientInstanceName="newPasswordCH" Password="true" Width="100%" Theme="Moderno">
                                                                    <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                                        <ErrorFrameStyle Wrap="True" />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents Init="OnPasswordTextBoxInitChangePW" KeyUp="OnPassChangedChangePW" Validation="OnPassValidationChangePW" />
                                                                </dx:ASPxTextBox>
                                                                <div style="padding-top: 10px;">
                                                                    <dx:ASPxRatingControl ID="ratingControlChangePW" runat="server" ReadOnly="true" ItemCount="5" Value="0" ClientInstanceName="ratingControlChangePW" Theme="Moderno" />
                                                                </div>
                                                                <div style="padding-top: 5px; padding-bottom: 10px">
                                                                    <dx:ASPxLabel ID="ratingLabelChangePW" runat="server" ClientInstanceName="ratingLabelChangePW" Text="Password safety" Theme="Moderno" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <table style="width: auto;">
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Confirm Password" Theme="Moderno"></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxTextBox ID="confirmPasswordCH" runat="server" ClientInstanceName="confirmPasswordCH" Password="true" Width="100%" Theme="Moderno">
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
                                                                <dx:ASPxCaptcha runat="server" ID="captcha" ClientInstanceName="captchaChPWDirect" TextBox-Position="Bottom" TextBox-ShowLabel="false" TextBoxStyle-Width="100%" Width="200" Theme="Moderno">
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
                                                                            <dx:ASPxButton ID="btnChangePW" runat="server" Text="Change Password" Width="100%" Theme="Moderno" AutoPostBack="false" OnClick="btnChangePW_Click">
                                                                                <ClientSideEvents Click="ChangePW" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxButton ID="CancelBtn" runat="server" Text="Cancel" CausesValidation="false" Theme="Moderno" OnClick="CancelBtn_Click">
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter>


    </form>
</body>
</html>
