<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="hlsSOA_Add.aspx.cs" Inherits="HijoPortal.hlsSOA_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function BtnSaveSOAClient_Clicked(s, e) {
            var btnCap = BtnSaveSOAClient.GetText();
            if (btnCap == "S A V E") {
                $find('ModalPopupExtenderLoading').show();
            }
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

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <PanelCollection>
            <dx:PanelContent>
                <div id="dvHeader" style="height: 30px;">
                    <h1 id="HLSSOATitle" runat="server">HLS</h1>
                </div>
                <div>
                    <table style="width: 100%; margin-top: 20px;">
                        <tr>
                            <td style="padding-left: 10px; width: 50%;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <dx:ASPxLabel runat="server" Text="Date" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width:100px;">
                                                        <dx:ASPxTextBox ID="txtDate" runat="server" Width="100px" Theme="Office2010Blue"></dx:ASPxTextBox>
                                                        <div style="display:none;">
                                                            <dx:ASPxTextBox ID="txtCustCode" runat="server" Width="100px" Theme="Office2010Blue"></dx:ASPxTextBox>
                                                        </div>
                                                    </td>
                                                    <td></td>
                                                    <td style="width:50px;">
                                                        <dx:ASPxLabel runat="server" Text="Year" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td style="width:50px;">
                                                        <dx:ASPxTextBox ID="txtYear" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width:10px;"></td>
                                                    <td style="width:100px;">
                                                        <dx:ASPxLabel runat="server" Text="Week Number" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td style="width:50px;">
                                                        <dx:ASPxTextBox ID="txtWeekNum" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxLabel runat="server" Text="Customer" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtCustomer" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;"></td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtCustomerAdd" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td style="padding-right: 10px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <dx:ASPxLabel runat="server" Text="SOA Number" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtSOANumber" runat="server" Width="150px" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxLabel runat="server" Text="Attention" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtAttention" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;"></td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtAttentionNum" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-top: 5px; padding-left: 10px; padding-right: 10px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">
                                            <dx:ASPxLabel runat="server" Text="Remarks" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtRemarks" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                </div>
                <%--for viewing only--%>
                <div style="padding-top: 10px; padding-right: 10px;">
                    <dx:ASPxGridView ID="HLSWaybill" runat="server" ClientInstanceName="HLSWaybillClient" KeyFieldName="PK"
                        EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                        Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue">
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
                        <Columns>
                            <dx:GridViewDataColumn FieldName="Num" Caption="#" VisibleIndex="1" Width="30px">
                                <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Date" Caption="Date" VisibleIndex="2" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PlateNum" Caption="Plate No" VisibleIndex="3" Width="80px">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Particulars" Caption="Particulars" VisibleIndex="4" Width="350px">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="ContainerNum" Caption="Container No" VisibleIndex="5" Width="120px">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Waybill" Caption="Way Bill" VisibleIndex="6" Width="80px">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="From" Caption="From" VisibleIndex="7">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="To" Caption="To" VisibleIndex="8">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Left"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Amount" Caption="Amount" VisibleIndex="9" Width="120px">
                                <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="false"></SettingsPager>
                    </dx:ASPxGridView>
                </div>
                <%--footer--%>
                <div style="padding-top: 20px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="padding-left: 10px; padding-right: 10px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="padding-top: 5px; width: 100px;">
                                            <dx:ASPxLabel runat="server" Text="Prepared By" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtPreparedBy" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;"></td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtPreparedByPost" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="padding-right: 10px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="padding-top: 5px; width: 100px;">
                                            <dx:ASPxLabel runat="server" Text="Checked By" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtCheckedBy" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;"></td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtCheckedByPost" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="padding-right: 10px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="padding-top: 5px; width: 100px;">
                                            <dx:ASPxLabel runat="server" Text="Approved By" Theme="Office2010Blue"></dx:ASPxLabel>
                                        </td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtApprovedBy" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;"></td>
                                        <td style="padding-top: 5px;">
                                            <dx:ASPxTextBox ID="txtApprovedByPost" runat="server" Width="100%" Theme="Office2010Blue"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-top: 20px; padding-left: 10px; padding-right: 10px; text-align: right;">
                                <dx:ASPxButton ID="BtnBackSOA" runat="server" ClientInstanceName="BtnBackSOAClient"
                                    ClientEnabled="true" Text="<< Back to SOA List" Theme="Moderno" Width="150" OnClick="BtnBackSOA_Click">
                                    <ClientSideEvents Click="function(s,e){
                                        $find('ModalPopupExtenderLoading').show();
                                        e.processOnServer = true;
                                        }" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="BtnSaveSOA" runat="server" ClientInstanceName="BtnSaveSOAClient"
                                    ClientEnabled="true" Text="S A V E" Theme="Moderno" Width="150" OnClick="BtnSaveSOA_Click">
                                    <ClientSideEvents Click="BtnSaveSOAClient_Clicked" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--waybill list for save--%>
                <div style="display:none;">
                    <dx:ASPxGridView ID="HLSWaybillList" runat="server" ClientInstanceName="HLSWaybillListClient" KeyFieldName="PK"
                        EnableCallbackCompression="False" EnableCallBacks="True" EnableTheming="True" KeyboardSupport="true"
                        Style="margin: 0 auto;" Width="100%" Theme="Office2010Blue">

                        <Columns>
                            <%--<dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page" Width="40px">
                            </dx:GridViewCommandColumn>--%>
                            <dx:GridViewDataColumn FieldName="Waybill" Caption="Waybill" >
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="CustCode" Caption="CustCode">
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Year" Caption="Year">
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="WeekNum" Caption="WeekNum">
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right"></CellStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <SettingsPager Mode="ShowAllRecords" PageSize="5" AlwaysShowPager="false"></SettingsPager>
                    </dx:ASPxGridView>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>


</asp:Content>
