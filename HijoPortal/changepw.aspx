<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="changepw.aspx.cs" Inherits="HijoPortal.changepw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="jquery/changePW.js" type="text/javascript"></script>

    <dx:ASPxGlobalEvents runat="server" ID="GlobalEvents">
        <ClientSideEvents ControlsInitialized="onControlsInitializedChangePW" />
    </dx:ASPxGlobalEvents>

    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader">
            <h1>Change Password</h1>
        </div>
        <div style="width: 100%;">
            <table style="margin: auto;">
                <tr>
                    <td>
                        <dx:ASPxFormLayout ID="FormLayoutChangePW"
                            ClientInstanceName="FormLayoutChangePWDirect"
                            runat="server"
                            RequiredMarkDisplayMode="Auto"
                            UseDefaultPaddings="false"
                            AlignItemCaptionsInAllGroups="true"
                            Width="100%" Theme="iOS">
                            <Paddings PaddingBottom="30" PaddingTop="10" />
                            <Items>
                                <dx:LayoutItem Caption="Old Password">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="oldPassword" runat="server" ClientInstanceName="oldPassword" Password="true" Width="100%" CssClass="maxWidth">
                                                <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                    <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                    <ErrorFrameStyle Wrap="True" />
                                                </ValidationSettings>
                                                <%--<ClientSideEvents Init="OnPasswordTextBoxInit" KeyUp="OnPassChanged" Validation="OnPassValidation" />--%>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="New Password">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="newPassword" runat="server" ClientInstanceName="newPassword" Password="true" Width="100%" CssClass="maxWidth">
                                                <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                    <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                    <ErrorFrameStyle Wrap="True" />
                                                </ValidationSettings>
                                                <%--<ClientSideEvents Init="OnPasswordTextBoxInit" KeyUp="OnPassChanged" Validation="OnPassValidation" />--%>
                                                <ClientSideEvents Init="OnPasswordTextBoxInitChangePW" KeyUp="OnPassChangedChangePW" Validation="OnPassValidationChangePW" />
                                            </dx:ASPxTextBox>
                                            <div style="padding-top: 10px;">
                                                <dx:ASPxRatingControl ID="ratingControlChangePW" runat="server" ReadOnly="true" ItemCount="5" Value="0" ClientInstanceName="ratingControlChangePW" />
                                            </div>
                                            <div style="padding-top: 5px; padding-bottom: 10px">
                                                <dx:ASPxLabel ID="ratingLabelChangePW" runat="server" ClientInstanceName="ratingLabelChangePW" Text="Password safety" />
                                            </div>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Confirm Password">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="confirmPassword" runat="server" ClientInstanceName="confirmPassword" Password="true" Width="100%" CssClass="maxWidth">
                                                <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                    <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                    <ErrorFrameStyle Wrap="True" />
                                                </ValidationSettings>
                                                <%--<ClientSideEvents Init="OnPasswordTextBoxInit" KeyUp="OnPassChanged" Validation="OnPassValidation" />--%>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" CssClass="mobileAlign" HorizontalAlign="Center">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxCaptcha runat="server" ID="captcha" TextBox-Position="Bottom" TextBox-ShowLabel="false" TextBoxStyle-Width="100%" Width="200">
                                                <RefreshButtonStyle Font-Underline="true"></RefreshButtonStyle>
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                                    <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                </ValidationSettings>
                                            </dx:ASPxCaptcha>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" CssClass="btn">
                                    <Paddings PaddingTop="20" />
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <div>
                                                <dx:ASPxButton runat="server" ID="btnChangePW" Text="Change Password" Width="200px" />
                                                <%--<dx:ASPxButton runat="server" ID="signCancel" Text="Cancel" Width="100px" CssClass="btn" ClientSideEvents-Click="alert('Default Button Clicked')" />--%>
                                            </div>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
