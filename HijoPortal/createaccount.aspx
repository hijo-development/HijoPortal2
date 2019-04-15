<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createaccount.aspx.cs" Inherits="HijoPortal.createaccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Account</title>
    <link rel="stylesheet" type="text/css" href="css/Account.css" />
    <link href="css/ChangePassword.css" rel="stylesheet" />
    <script src="jquery/createAccount.js" type="text/javascript"></script>
    <style type="text/css">
        .createaccount_div {
            width: 1360px;
            padding: 5px;
            background-color: rgb(230,239,247);
            -moz-box-shadow: 1px 1px 3px 2px #87cefa;
            -webkit-box-shadow: 1px 1px 3px 2px #87cefa;
            box-shadow: 1px 1px 3px 2px #87cefa;
            overflow-x: auto;
            height:500px;
            /*overflow-y: auto;*/
        }
    </style>
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

        <dx:ASPxPopupControl ID="CreateAccntNotify" ClientInstanceName="CreateAccntNotifyClient" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue" ContentStyle-Paddings-Padding="20">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <dx:ASPxLabel ID="CreateAccntNotifyLbl" ClientInstanceName="CreateAccntNotifyLblClient" runat="server" Text="" ForeColor="Red" Theme="Office2010Blue"></dx:ASPxLabel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxGlobalEvents runat="server" ID="GlobalEvents">
            <ClientSideEvents ControlsInitialized="onControlsInitialized" />
        </dx:ASPxGlobalEvents>

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
        <div class="createaccount_div11" id="dvContentWrapper11">
            <div style="height: 50px;"></div>
            <div id="dvHeader" style="height: 50px;">
                <h1>Account Registration</h1>
            </div>
            <div style="width: 800px; height: 500px; margin: auto;">
                <table style="width: 100%;">
                    <tr>
                        <td style="vertical-align: top; padding: 10px; width: 50%;">
                            <dx:ASPxFormLayout ID="FormLayoutReg"
                                ClientInstanceName="FormLayoutRegDirect"
                                runat="server"
                                RequiredMarkDisplayMode="Auto"
                                UseDefaultPaddings="false"
                                AlignItemCaptionsInAllGroups="true"
                                Width="100%" Theme="Office2010Blue">
                                <Paddings PaddingBottom="30" PaddingTop="10" />
                                <Items>
                                    <dx:LayoutGroup Caption="Registration Data" GroupBoxDecoration="Box">
                                        <Items>
                                            <dx:LayoutItem Caption="ID Number" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="IDNumTextBox" runat="server" ClientInstanceName="idnumTextboxDirect" NullText="ID Number" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true" ValidateOnLeave="true">
                                                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                                <ErrorFrameStyle Wrap="True" />
                                                            </ValidationSettings>
                                                            <ClientSideEvents LostFocus="onIDNumberLostFocus" Validation="OnIDNumPassValidation" />
                                                        </dx:ASPxTextBox>
                                                        <dx:ASPxCallbackPanel ID="CallbackPanelIDNum" runat="server" ClientInstanceName="CallbackPanelIDNumDirect" Width="100%" OnCallback="CallbackPanelIDNum_Callback">
                                                            <ClientSideEvents EndCallback="IDNumberEndCallback" />
                                                            <PanelCollection>
                                                                <dx:PanelContent>
                                                                    <dx:ASPxTextBox ID="IDNumTextBoxMatch" runat="server" ClientInstanceName="idnumTextboxMatchDirect" Width="170px" Visible="false" Theme="iOS">
                                                                        <%--<ClientSideEvents TextChanged="onIDNumMatchChanged" />--%>
                                                                    </dx:ASPxTextBox>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Last Name" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="lastNameTextBox" runat="server" ClientInstanceName="lnameTextboxDirect" NullText="Last Name" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorDisplayMode="Text" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorFrameStyle-Wrap="true">
                                                                <ErrorFrameStyle Wrap="True"></ErrorFrameStyle>
                                                                <RequiredField IsRequired="True" ErrorText="The value is required"></RequiredField>
                                                            </ValidationSettings>
                                                            <ClientSideEvents />
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="First Name" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="firstNameTextBox" runat="server" ClientInstanceName="fnameTextboxDirect" NullText="First Name" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorDisplayMode="Text" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorFrameStyle-Wrap="true">
                                                                <ErrorFrameStyle Wrap="True"></ErrorFrameStyle>

                                                                <RequiredField IsRequired="True" ErrorText="The value is required"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Gender" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cmbGender" ClientInstanceName="cmbGenderDirect" runat="server" ValueType="System.String" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <Items>
                                                                <dx:ListEditItem Text="Male" Value="1" />
                                                                <dx:ListEditItem Text="Female" Value="2" />
                                                            </Items>
                                                            <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorDisplayMode="Text" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorFrameStyle-Wrap="true">
                                                                <ErrorFrameStyle Wrap="True"></ErrorFrameStyle>
                                                                <RequiredField IsRequired="True" ErrorText="The value is required"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="E-mail" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox runat="server" ID="eMailTextBox" ClientInstanceName="eMailTextBoxDirect" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom" SetFocusOnError="true">
                                                                <ErrorFrameStyle Wrap="True" />
                                                                <RegularExpression ErrorText="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </td>
                        <td style="vertical-align: top; padding: 10px; width: 50%;">
                            <dx:ASPxFormLayout ID="FormLayoutAut"
                                ClientInstanceName="FormLayoutAutDirect"
                                runat="server"
                                RequiredMarkDisplayMode="Auto"
                                UseDefaultPaddings="false"
                                AlignItemCaptionsInAllGroups="true"
                                Width="100%" Theme="Office2010Blue">
                                <Paddings PaddingBottom="30" PaddingTop="10" />
                                <Items>
                                    <dx:LayoutGroup Caption="Authorization Data" GroupBoxDecoration="Box">
                                        <Items>
                                            <dx:LayoutItem Caption="User Name" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="userNameTextBox" runat="server" ClientInstanceName="userNameTextBoxDirect" NullText="User Name" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <%--<ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorDisplayMode="Text" SetFocusOnError="true" ErrorTextPosition="Bottom" ErrorFrameStyle-Wrap="true" />--%>
                                                            <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                                <ErrorFrameStyle Wrap="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Password" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="passwordTextBox" runat="server" ClientInstanceName="passwordTextBox" Password="true" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                                <ErrorFrameStyle Wrap="True" />
                                                            </ValidationSettings>
                                                            <ClientSideEvents Init="OnPasswordTextBoxInit" KeyUp="OnPassChanged" Validation="OnPassValidation" />
                                                        </dx:ASPxTextBox>
                                                        <div style="padding-top: 10px;">
                                                            <dx:ASPxRatingControl ID="ratingControl" runat="server" ReadOnly="true" ItemCount="5" Value="0" ClientInstanceName="ratingControl" Theme="iOS" />
                                                        </div>
                                                        <div style="padding-top: 5px; padding-bottom: 10px">
                                                            <dx:ASPxLabel ID="ratingLabel" runat="server" ClientInstanceName="ratingLabel" Text="Password safety" />
                                                        </div>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Confirm password" CaptionStyle-ForeColor="Black">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="confirmPasswordTextBox" runat="server" ClientInstanceName="confirmPasswordTextBox" Password="true" Width="100%" CssClass="maxWidth" Theme="iOS">
                                                            <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                                <ErrorFrameStyle Wrap="True" />
                                                            </ValidationSettings>
                                                            <ClientSideEvents Validation="OnPassValidation" />
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top; padding: 10px; width: 50%; align-content: center;">
                            <dx:ASPxFormLayout ID="FormLayoutMed"
                                ClientInstanceName="FormLayoutMedDirect"
                                runat="server"
                                RequiredMarkDisplayMode="Auto"
                                UseDefaultPaddings="false"
                                AlignItemCaptionsInAllGroups="true"
                                Width="100%" Theme="Office2010Blue">
                                <Paddings PaddingBottom="30" PaddingTop="10" />
                                <Items>
                                    <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="Box" HorizontalAlign="Center" Width="100%">
                                        <ParentContainerStyle CssClass="mobileGroupIndent"></ParentContainerStyle>
                                        <Items>
                                            <dx:LayoutItem ShowCaption="False" CssClass="mobileAlign" HorizontalAlign="Center">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCaptcha runat="server" ID="captcha" ClientInstanceName="captchaDirect" TextBox-Position="Bottom" TextBox-ShowLabel="false" TextBoxStyle-Width="100%" Width="200" Theme="iOS">
                                                            <RefreshButtonStyle Font-Underline="false"></RefreshButtonStyle>
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                            </ValidationSettings>
                                                        </dx:ASPxCaptcha>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="False" CssClass="mobileAlign" HorizontalAlign="Center">
                                                <NestedControlCellStyle CssClass="maxWidth"></NestedControlCellStyle>
                                                <Paddings PaddingTop="12" />
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        By clicking "Sign Up", you agree to the <a href="javascript:;">privacy policy</a> and the <a href="javascript:;">terms of use</a>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" CssClass="btn">
                                                <Paddings PaddingTop="20" />
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <div>
                                                            <dx:ASPxButton runat="server" ID="signUp" Text="Sign Up" Width="200px" CssClass="btn" OnClick="signUp_Click" AutoPostBack="false" Theme="iOS">
                                                                <ClientSideEvents Click="SignUp" />
                                                            </dx:ASPxButton>
                                                            <%--<dx:ASPxButton runat="server" ID="signCancel" Text="Cancel" Width="100px" CssClass="btn" ClientSideEvents-Click="alert('Default Button Clicked')" />--%>
                                                        </div>
                                                        <%--<dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton"></dx:ASPxButton>--%>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
