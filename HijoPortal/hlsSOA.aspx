<%@ Page Title="HLS - Statement Of Account" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="hlsSOA.aspx.cs" Inherits="HijoPortal.hlsSOA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ComboBoxYear_SelectedIndexChanged(s, e) {
            var text = s.GetSelectedItem().text;
            TextBoxCustCodeClient.SetText("");
            ComboBoxWeekNumClient.PerformCallback();
            ComboBoxCustomerClient.PerformCallback();
            CallbackPanelWaybill.PerformCallback();
            ComboBoxWeekNumClient.SetEnabled(true);
            ComboBoxCustomerClient.SetEnabled(false);
            //ListBoxWaybillClient.SetEnabled(false);
        }

        function ComboBoxWeekNum_SelectedIndexChanged(s, e) {
            var text = s.GetSelectedItem().text;
            //var arr = s.GetText().split("; ");
            //s.SetText(arr[1]);
            TextBoxCustCodeClient.SetText("");
            ComboBoxCustomerClient.PerformCallback();
            CallbackPanelWaybill.PerformCallback();
            ComboBoxCustomerClient.SetEnabled(true);
            //ListBoxWaybillClient.SetEnabled(false);
        }

        function ComboBoxCustomer_SelectedIndexChanged(s, e) {
            var text = s.GetSelectedItem().text;
            var arr = s.GetText().split("; ");
            s.SetText(arr[1]);
            TextBoxCustCodeClient.SetText(arr[0]);
            CallbackPanelWaybill.PerformCallback();
            //ListBoxWaybillClient.SetEnabled(true);
        }

        //function ListBoxWaybill_SelectedIndexChanged(s, e) {
        //    if (s.GetSelectedRowCount() > 0)
        //        BtnAddSOAClient.SetEnabled(true);
        //    else
        //        BtnAddSOAClient.SetEnabled(false);
        //    //var mop = MOPNum_Combo.GetText();
        //    //var monthyear = MonthYear_Combo.GetText();
        //    //MainGridCallbackPanel.PerformCallback();
        //    //if (monthyear.length > 0)

        //}

        //function ListBoxWaybill_EndCallback(s, e) {
        //    ListBoxWaybillClient.SetEnabled(true);
        //}

        function gridWayBill_SelectionChanged(s, e) {
            if (s.GetSelectedRowCount() > 0)
                BtnAddSOAClient.SetEnabled(true);
            else
                BtnAddSOAClient.SetEnabled(false);
        }

        function btnAddSOA_Clicked(s, e) {
            PopUpControlAddSOA.Hide();
            $find('ModalPopupExtenderLoading').show();
            e.processOnServer = true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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


    <dx:ASPxPopupControl ID="PopUpControlAddSOA" ClientInstanceName="PopUpControlAddSOA" runat="server"
        Modal="true" PopupAnimationType="Fade" CloseAnimationType="Fade" CloseAction="CloseButton"
        PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true"
        Width="650px" Theme="Moderno">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <div style="padding: 20px 10px;">
                    <table class="popup-modal" style="width: 100%;">
                        <tr>
                            <td style="width: 150px; vertical-align: top;">
                                <dx:ASPxLabel runat="server" Text="Year" Theme="Moderno"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="ComboBoxYear" runat="server" ClientInstanceName="ComboBoxYearClient"
                                    ClientEnabled="true" DropDownRows="10" ValueType="System.String" Theme="Moderno" Width="100%"
                                    OnInit="ComboBoxYear_Init">
                                    <ClientSideEvents SelectedIndexChanged="ComboBoxYear_SelectedIndexChanged" />
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <dx:ASPxLabel runat="server" Text="Week Number" Theme="Moderno"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="ComboBoxWeekNum" runat="server" ClientInstanceName="ComboBoxWeekNumClient"
                                    ClientEnabled="false" DropDownRows="10" ValueType="System.String" Theme="Moderno" Width="100%"
                                    OnInit="ComboBoxWeekNum_Init" OnCallback="ComboBoxWeekNum_Callback">
                                    <ClientSideEvents SelectedIndexChanged="ComboBoxWeekNum_SelectedIndexChanged" />
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <dx:ASPxLabel runat="server" Text="Customer" Theme="Moderno"></dx:ASPxLabel>
                                <div style="display: none;">
                                    <%--<dx:ASPxLabel ID="CustCode1" runat="server" ClientInstanceName="CustCodeClient" Text="" Theme="Moderno"></dx:ASPxLabel>--%>
                                    <%--<dx:ASPxTextBox ID="CustCode" runat="server" ClientInstanceName="CustCodeClient" Text=""></dx:ASPxTextBox>--%>
                                    <dx:ASPxTextBox ID="TextBoxCustCode" runat="server" ClientInstanceName="TextBoxCustCodeClient" Width="170px"></dx:ASPxTextBox>
                                </div>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="ComboBoxCustomer" runat="server" ClientInstanceName="ComboBoxCustomerClient"
                                    ClientEnabled="false" DropDownRows="10" ValueType="System.String" Theme="Moderno" Width="100%"
                                    OnInit="ComboBoxCustomer_Init" OnCallback="ComboBoxCustomer_Callback">
                                    <ClientSideEvents SelectedIndexChanged="ComboBoxCustomer_SelectedIndexChanged" />
                                </dx:ASPxComboBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="vertical-align: top;">
                                <dx:ASPxLabel runat="server" Text="Waybill/s" Theme="Moderno"></dx:ASPxLabel>
                            </td>
                            <td>
                                <%--<dx:ASPxListBox ID="ListBoxWaybill" runat="server" ClientInstanceName="ListBoxWaybillClient" 
                                    ClientEnabled="false" SelectionMode="CheckColumn" EnableSelectAll="true" ValueType="System.String" 
                                    Theme="Moderno" Width="100%" Height="200px" OnInit="ListBoxWaybill_Init" OnCallback="ListBoxWaybill_Callback" >
                                    <ClientSideEvents SelectedIndexChanged="ListBoxWaybill_SelectedIndexChanged" EndCallback="ListBoxWaybill_EndCallback" />
                                </dx:ASPxListBox>--%>
                                <dx:ASPxCallbackPanel ID="CallbackPanelWaybill" runat="server" ClientInstanceName="CallbackPanelWaybill"
                                    Width="100%" OnCallback="CallbackPanelWaybill_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <dx:ASPxGridView ID="gridWayBill" runat="server" ClientInstanceName="gridWayBillClient" Theme="Moderno"
                                                Width="100%" OnDataBound="gridWayBill_DataBound">
                                                <ClientSideEvents SelectionChanged="gridWayBill_SelectionChanged" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="40px"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="Waybill" Caption="Waybill"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Year" Visible="false"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="WeekNum" Visible="false"></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="CustCode" Visible="false"></dx:GridViewDataColumn>
                                                </Columns>
                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="200" />
                                                <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="false">
                                                </SettingsPager>
                                            </dx:ASPxGridView>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2" style="text-align: right; padding-top: 20px;">
                                <dx:ASPxButton ID="BtnAddSOA" runat="server" ClientInstanceName="BtnAddSOAClient"
                                    ClientEnabled="false" Text="Add" Theme="Moderno" Width="30%" OnClick="BtnAddSOA_Click">
                                    <ClientSideEvents Click="btnAddSOA_Clicked" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <PanelCollection>
            <dx:PanelContent>
                <div id="dvHeader" style="height: 30px;">
                    <h1>HLS - Statement Of Account</h1>
                </div>
                <div>
                    <dx:ASPxGridView ID="HLSSOAList" runat="server" ClientInstanceName="HLSSOAListClient" KeyFieldName="PK"
                        EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                        Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue" OnCustomButtonCallback="HLSSOAList_CustomButtonCallback">
                        <SettingsBehavior AllowSort="true" SortMode="Value" />
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="50">
                                <HeaderTemplate>
                                    <div style="text-align: left;">
                                        <dx:ASPxButton ID="AddSOA" OnClick="AddSOA_Click" runat="server" Image-Url="Images/Add.ico" Image-Width="15px" Image-ToolTip="New Row" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Center" VerticalAlign="Middle"></dx:ASPxButton>
                                    </div>
                                </HeaderTemplate>
                                <CustomButtons>
                                    <%--<dx:GridViewCommandColumnCustomButton ID="Edit" Text="" Image-Url="Images/Edit.ico" Image-ToolTip="Edit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>--%>
                                    <dx:GridViewCommandColumnCustomButton ID="Delete" Text="" Image-Url="Images/Delete.ico" Image-ToolTip="Delete Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="Preview" Text="" Image-Url="Images/Refresh.ico" Image-ToolTip="Preview Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="Submit" Text="" Image-Url="Images/Submit.ico" Image-ToolTip="Submit Row" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="PK" Visible="false" VisibleIndex="1"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SOANum" Caption="SOA Number" VisibleIndex="2" HeaderStyle-Font-Bold="true" SortOrder="Descending" Width="140px"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SOADate" Caption="SOA Date" VisibleIndex="3" HeaderStyle-Font-Bold="true"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="CustCode" Visible="false" VisibleIndex="4"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="CustName" Caption="Customer" VisibleIndex="5" HeaderStyle-Font-Bold="true"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Year" Caption="Year" VisibleIndex="6" Width="140px" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="WeekNum" Caption="Week Number" VisibleIndex="7" Width="100px" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="BillingInv" Caption="Billing Invoice" VisibleIndex="8" Width="100px" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Amount" Caption="Amount" VisibleIndex="9" Width="200px">
                                <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <EditFormLayoutProperties>
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                        </EditFormLayoutProperties>
                        <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" ShowFilterRow="true" />
                        <SettingsPopup>
                            <EditForm Width="900">
                                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="850" />
                            </EditForm>
                        </SettingsPopup>

                        <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="false">
                        </SettingsPager>

                        <SettingsLoadingPanel Mode="ShowAsPopup" />
                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            AllowSort="true" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowDragDrop="false" ConfirmDelete="true" />
                        <Styles>
                            <Cell Wrap="False"></Cell>
                            <SelectedRow Font-Bold="False" Font-Italic="False">
                            </SelectedRow>
                            <FocusedRow Font-Bold="False" Font-Italic="False">
                            </FocusedRow>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
