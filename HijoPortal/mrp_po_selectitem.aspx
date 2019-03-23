<%@ Page Title="Select Items" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_po_selectitem.aspx.cs" Inherits="HijoPortal.mrp_po_selectitem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function MonthYear_Combo_SelectedIndexChanged(s, e) {
            var text = s.GetSelectedItem().text;
            console.log(text);
            MOPNum_Combo.PerformCallback();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>Select Items for Purchase Order</h1>
        </div>
        <div>
            <table style="width:100%; margin-bottom:10px;" border="0">
                <tr style="padding-bottom:5px;">
                    <td style="width:15%;">
                        <dx:ASPxLabel runat="server" Text="Month/Year" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width:1%;">:</td>
                    <td style="width:15%;">
                        <dx:ASPxComboBox ID="MonthYear_Combo" runat="server" OnInit="MonthYear_Combo_Init" ValueType="System.String" Theme="Office2010Blue">
                            <ClientSideEvents SelectedIndexChanged="MonthYear_Combo_SelectedIndexChanged" />
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width:69%;"></td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="MOP Number" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxComboBox ID="MOPNum_Combo" runat="server" ClientInstanceName="MOPNum_Combo" OnInit="MOPNum_Combo_Init" OnCallback="MOPNum_Combo_Callback" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Procurement Category" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxComboBox ID="ProdCategory_Combo" runat="server" ValueType="System.String" Theme="Office2010Blue"></dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width:100%;">
            <dx:ASPxGridView ID="MainGrid" runat="server" Theme="Office2010Blue" Width="100%">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectAllCheckboxMode="AllPages"></dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="ItemPK" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ItemIdentifier" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DocumentNumber"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Entity"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BU" Caption="BU/SSU"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ItemCode"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Description"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Qty"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Cost"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Total Cost"></dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
