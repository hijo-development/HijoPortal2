<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="changepw.aspx.cs" Inherits="HijoPortal.changepw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #tblChangePW {
            margin: auto;
        }

            #tblChangePW td {
                padding: 2px;
            }
    </style>
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader">
            <h1>Change Password</h1>
        </div>
        <div style="width: 100%; padding-top: 30px;">
            <table id="tblChangePW">
                <tr>
                    <td style="width: 100px;">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Old Password" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 250px;">
                        <dx:ASPxTextBox ID="oldPasswordCH" runat="server" ClientInstanceName="oldPasswordCH" Password="true" Width="100%">
                            <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                <ErrorFrameStyle Wrap="True" />
                            </ValidationSettings>
                            <ClientSideEvents Validation="OnPassValidationChangePW" />
                        </dx:ASPxTextBox>

                    </td>
                    <td>
                        <div style="display: none;">
                            <dx:ASPxTextBox ID="oldPasswordCHDB" runat="server" Width="170px" ClientInstanceName="oldPasswordCHDB" >
                            </dx:ASPxTextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="New Password" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="newPasswordCH" runat="server" ClientInstanceName="newPasswordCH" Password="true" Width="100%" Theme="Office2010Blue">
                            <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                                <ErrorFrameStyle Wrap="True" />
                            </ValidationSettings>
                            <ClientSideEvents Init="OnPasswordTextBoxInitChangePW" KeyUp="OnPassChangedChangePW" Validation="OnPassValidationChangePW" />
                        </dx:ASPxTextBox>
                        <div style="padding-top: 10px;">
                            <dx:ASPxRatingControl ID="ratingControlChangePW" runat="server" ReadOnly="true" ItemCount="5" Value="0" ClientInstanceName="ratingControlChangePW" />
                        </div>
                        <div style="padding-top: 5px; padding-bottom: 10px">
                            <dx:ASPxLabel ID="ratingLabelChangePW" runat="server" ClientInstanceName="ratingLabelChangePW" Text="Password safety" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Confirm Password" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="confirmPasswordCH" runat="server" ClientInstanceName="confirmPasswordCH" Password="true" Width="100%" Theme="Office2010Blue">
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
                        <dx:ASPxCaptcha runat="server" ID="captcha" TextBox-Position="Bottom" TextBox-ShowLabel="false" TextBoxStyle-Width="100%" Width="200">
                            <RefreshButtonStyle Font-Underline="true"></RefreshButtonStyle>
                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                <RequiredField IsRequired="True" ErrorText="The value is required" />
                            </ValidationSettings>
                        </dx:ASPxCaptcha>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="padding-top: 20px; padding-bottom:30px;">
                        <dx:ASPxButton ID="btnChangePW" runat="server" Text="Change Password" Width="100%" OnClick="btnChangePW_Click"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
