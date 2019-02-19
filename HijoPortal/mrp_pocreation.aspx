<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_pocreation.aspx.cs" Inherits="HijoPortal.mrp_pocreation" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPopupControl ID="PopUpControl" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <div style="padding: 20px 10px;">
                    <table class="popup-modal">
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="MRPNumber" runat="server" ValueType="System.String" NullText="" TextFormatString="{0}" Theme="Office2010Blue" OnInit="MRPNumber_Init"></dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="BtnAdd" runat="server" Text="Add" Theme="Office2010Blue" OnClick="BtnAdd_Click"></dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>List of AUTO PO</h1>
            <asp:Label ID="msgTrans" runat="server" Visible="false"></asp:Label>
        </div>
        <div>
            <dx:ASPxGridView ID="POTable" runat="server" ClientInstanceName="POTable"
                EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                OnCustomButtonCallback="POTable_CustomButtonCallback">
                <ClientSideEvents CustomButtonClick="POCustomButtonClick" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="50">
                        <HeaderTemplate>
                            <div style="text-align: center">
                                <dx:ASPxButton ID="Add" OnClick="Add_Click" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle"></dx:ASPxButton>
                            </div>
                        </HeaderTemplate>
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Delete" Text="" Image-Url="Images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PONumber" Caption="PO Number" VisibleIndex="2"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPNumber" Caption="MRP Number" VisibleIndex="3"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DateCreated" VisibleIndex="4"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CreatorKey" Caption="Creator" VisibleIndex="5"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ExpectedDate" VisibleIndex="6"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VendorCode" VisibleIndex="7"></dx:GridViewDataColumn>
                </Columns>

                <%--<Settings HorizontalScrollBarMode="Auto" />--%>
                <SettingsCommandButton>
                    <%--<EditButton ButtonType="Image" Image-Url="Images/Edit.ico" Image-Width="15px" Image-ToolTip="Edit Row">
            </EditButton>
            <DeleteButton ButtonType="Image" Image-Url="Images/Delete.ico" Image-Width="15px"></DeleteButton>
            <NewButton ButtonType="Image" Image-Url="Images/Add.ico" Image-Width="15px"></NewButton>--%>
                </SettingsCommandButton>
                <%--<SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />--%>
                <EditFormLayoutProperties>
                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                </EditFormLayoutProperties>
                <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                <SettingsPopup>
                    <EditForm Width="900">
                        <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                    </EditForm>
                </SettingsPopup>

                <%--<SettingsPager Mode="ShowPager" PageSize="5" AlwaysShowPager="true">
                    </SettingsPager>--%>

                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                    AllowSort="true" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowDragDrop="false" ConfirmDelete="true" />
                <SettingsText ConfirmDelete="Delete This Item?" />
                <Styles>
                    <SelectedRow Font-Bold="False" Font-Italic="False">
                    </SelectedRow>
                    <FocusedRow Font-Bold="False" Font-Italic="False">
                    </FocusedRow>
                </Styles>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
