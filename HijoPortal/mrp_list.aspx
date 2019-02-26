<%@ Page Title="MOP List" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_list.aspx.cs" Inherits="HijoPortal.mrp_list" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPopupControl ID="WarningPopUp" runat="server" Modal="true" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <div style="padding: 5px;">
                    <dx:ASPxLabel runat="server" ID="WarningText" Text=""></dx:ASPxLabel>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="PopUpControl" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <div style="padding: 20px 10px;">
                    <table class="popup-modal">
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="Month" runat="server" ValueType="System.String" NullText="Month" Theme="Office2010Blue" OnInit="Month_Init"></dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="Year" runat="server" ValueType="System.String" NullText="Year" Theme="Office2010Blue" OnInit="Year_Init"></dx:ASPxComboBox>
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

    <dx:ASPxPopupControl ID="MRPNotify" ClientInstanceName="MRPNotify" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxLabel ID="MRPNotificationMessage" ClientInstanceName="MRPNotificationMessage" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>M O P  List</h1>
            <asp:Label ID="msgTrans" runat="server" Visible="false"></asp:Label>
        </div>
        <div>
            <dx:ASPxGridView ID="MainTable" runat="server" ClientInstanceName="MainTable"
                EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue"
                OnCustomButtonCallback="MainTable_CustomButtonCallback">
                <ClientSideEvents CustomButtonClick="CustomButtonClick" />
                <ClientSideEvents EndCallback="MainTableEndCallback" />
                <SettingsBehavior AllowSort ="true" SortMode="Value" />

                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="50">
                        <HeaderTemplate>
                            
                            <div style="text-align: center">
                                <dx:ASPxButton ID="Add" OnClick="Add_Click" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle"></dx:ASPxButton>
                                <dx:ASPxHiddenField ID="MRPHiddenVal" ClientInstanceName="MRPHiddenVal" runat="server"></dx:ASPxHiddenField>
                            </div>
                        </HeaderTemplate>
                        
                        <CustomButtons>
                            
                            <dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Delete" Text="" Image-Url="Images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DocNumber" Caption="MRP Number" VisibleIndex="2" SortOrder="Descending"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity" VisibleIndex="3"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="Department" VisibleIndex="4"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="Month" VisibleIndex="5"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MRPYear" Caption="Year" VisibleIndex="6"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Amount" VisibleIndex="7"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="StatusKeyDesc" Caption="Status" VisibleIndex="8"></dx:GridViewDataColumn>
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
