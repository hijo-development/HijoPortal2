<%@ Page Title="Select Items" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_selectitem.aspx.cs" Inherits="HijoPortal.mrp_po_selectitem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function MonthYear_Combo_SelectedIndexChanged(s, e) {
            var text = s.GetSelectedItem().text;
            MOPNum_Combo.PerformCallback();
            MainGridCallbackPanel.PerformCallback();
            MOPNum_Combo.SetEnabled(true);

        }
        function MOPNum_Combo_SelectedIndexChanged(s, e) {
            var monthyear = MonthYear_Combo.GetValue();
            var arr = s.GetText().split("; ");
            s.SetText(arr[0]);
            EntityClient.SetText(arr[1]);
            BUClient.SetText(arr[2]);
            MainGridCallbackPanel.PerformCallback();
        }

        function ProdCategory_Combo_SelectedIndexChanged(s, e) {
            MainGridCallbackPanel.PerformCallback();
        }

        function MainGrid_PO_SelectionChanged(s, e) {
            console.log(s.GetSelectedRowCount());
            if (s.GetSelectedRowCount() > 0)
                CreatePO.SetEnabled(true);
            else
                CreatePO.SetEnabled(false);
        }

        function ProdCat_ListBox_SelectedIndexChanged(s, e) {
            MainGridCallbackPanel.PerformCallback();
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

    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>Select Items for Purchase Order</h1>
        </div>
        <div>
            <table style="width: 100%; margin-bottom: 10px;" border="0">
                <tr style="padding-bottom: 5px;">
                    <td style="width: 15%;">
                        <dx:ASPxLabel runat="server" Text="Month/Year" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%;">:</td>
                    <td style="width: 15%;">
                        <dx:ASPxComboBox ID="MonthYear_Combo" runat="server" ClientInstanceName="MonthYear_Combo" OnInit="MonthYear_Combo_Init" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="MonthYear_Combo_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5%;"></td>
                    <td style="width: 6%;"></td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="MOP Number" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%;">:</td>
                    <td>
                        <dx:ASPxComboBox ID="MOPNum_Combo" runat="server" ClientInstanceName="MOPNum_Combo" OnInit="MOPNum_Combo_Init" OnCallback="MOPNum_Combo_Callback" ValueType="System.String" ClientEnabled="false" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="MOPNum_Combo_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%;">:</td>
                    <td  style="width: 26%;">
                        <dx:ASPxTextBox ID="Entity" runat="server" ClientInstanceName="EntityClient" Width="100%" BackColor="Transparent" Border-BorderColor="Transparent" Theme="Office2010Blue" ReadOnly="true"></dx:ASPxTextBox>
                    </td>
                    <td style="width: 3%;"></td>
                    <td style="width: 6%;">
                        <dx:ASPxLabel runat="server" Text="SSU/BU" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 1%;">:</td>
                    <td style="width: 15%;">
                        <dx:ASPxTextBox ID="BU" runat="server" ClientInstanceName="BUClient" Width="170px" BackColor="Transparent" Border-BorderColor="Transparent" Theme="Office2010Blue" ReadOnly="true"></dx:ASPxTextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Procurement Category" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td colspan="5">
                        <%--<dx:ASPxComboBox ID="ProdCategory_Combo" runat="server" OnInit="ProdCategory_Combo_Init" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="ProdCategory_Combo_SelectedIndexChanged" />
                        </dx:ASPxComboBox>--%>
                        <dx:ASPxListBox ID="ProdCat_ListBox" runat="server" OnInit="ProdCat_ListBox_Init" SelectionMode="CheckColumn" EnableSelectAll="true" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="ProdCat_ListBox_SelectedIndexChanged" />
                        </dx:ASPxListBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%;">
            <dx:ASPxCallbackPanel ID="MainGridCallbackPanel" runat="server" ClientInstanceName="MainGridCallbackPanel" OnCallback="MainGridCallbackPanel_Callback" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <dx:ASPxGridView ID="MainGrid_PO" runat="server" Theme="Office2010Blue" Width="100%" KeyFieldName="PK;TableIdentifier">
                            <ClientSideEvents SelectionChanged="MainGrid_PO_SelectionChanged" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="Page"></dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="TableIdentifier" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="DocumentNumber" Width="150px"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Entity"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BU" Caption="BU/SSU"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemCatCode" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemCat" Caption="Item Category"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemCode"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CapexCIP" Caption="Capex ID"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="ItemDescription" Caption="Description"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Qty">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Cost">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="TotalCost">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="UOM" Visible="false"></dx:GridViewDataColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        </dx:ASPxGridView>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>

        </div>
        <div>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxButton ID="CancelPage" runat="server" OnClick="CancelPage_Click" CausesValidation="false" Text="CANCEL" Theme="Office2010Blue"></dx:ASPxButton>
                        <dx:ASPxButton ID="Create" runat="server" ClientInstanceName="CreatePO" OnClick="Create_Click" Text="CREATE PO" ClientEnabled="false" Theme="Office2010Blue">
                            <ClientSideEvents Click="function(s,e){
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
