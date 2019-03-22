<%@ Page Title="List of Created Purchase Order" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_list.aspx.cs" Inherits="HijoPortal.mrp_po_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>List of Created Purchase Order</h1>
        </div>
        <div>
            <dx:ASPxGridView ID="gridCreatedPO" runat="server" ClientInstanceName="gridCreatedPO"
                EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue">
                
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="50">
                        <HeaderTemplate>
                            <div style="text-align: left;">
                                <dx:ASPxButton ID="Add" OnClick="Add_Click" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle"></dx:ASPxButton>
                                <dx:ASPxHiddenField ID="HiddenVal" ClientInstanceName="HiddenVal" runat="server"></dx:ASPxHiddenField>
                            </div>
                        </HeaderTemplate>
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <dx:GridViewCommandColumnCustomButton ID="Delete" Text="" Image-Url="Images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                            <%--<dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>--%>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PONumber" Caption="PO Number" VisibleIndex="2"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="MOPNumber" Caption="MOP Number" VisibleIndex="3"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VendorName" Caption="Vendor Name" VisibleIndex="4"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DateCreated" Caption="Date Created" VisibleIndex="5"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CreatorKey" VisibleIndex="6" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Creator" Caption ="Creator" VisibleIndex="7"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ExpectedDate" Caption ="Expected Date" VisibleIndex="8"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="TotalAmount" Caption ="Total Amount" VisibleIndex="9">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Status" Caption ="Status" VisibleIndex="10"></dx:GridViewDataColumn>
                </Columns>
                <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                <SettingsPopup>
                    <EditForm Width="900">
                        <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                    </EditForm>
                </SettingsPopup>

                <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="false">
                </SettingsPager>
                
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
