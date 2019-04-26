<%@ Page Title="MOP Preview" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_preview_before.aspx.cs" Inherits="HijoPortal.mrp_preview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <dx:ASPxPopupControl ID="LogsPopup" runat="server" Modal="true" CloseAction="CloseButton"
        PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxMemo ID="LogsMemo" runat="server" Height="71px" Width="250px" Theme="Office2010Blue">
                                <DisabledStyle ForeColor="Black"></DisabledStyle>
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; padding-top: 10px;">
                            <dx:ASPxButton ID="LogsBtn" runat="server" Text="Save" Theme="Office2010Blue"
                                OnClick="LogsBtn_Click" AutoPostBack="false">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="MRPNotify" ClientInstanceName="MRPNotify" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxLabel ID="MRPNotificationMessage" ClientInstanceName="MRPNotificationMessage" runat="server" Text="" Theme="Office2010Blue" ForeColor="Red"></dx:ASPxLabel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="PopupSubmitPreview" ClientInstanceName="PopupSubmitPreview" runat="server" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="2" style="padding-right: 20px; padding-bottom: 20px;">
                            <dx:ASPxLabel runat="server" Text="Are you sure you want to submit this document?" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="OK_SUBMIT" runat="server" Text="SUBMIT" Theme="Office2010Blue" OnClick="Submit_Click" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){
                                    PopupSubmitPreview.Hide();
                                    $find('ModalPopupExtenderLoading').show();
                                    e.processOnServer = true;
                                    }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CANCEL_SUBMIT" runat="server" Text="CANCEL" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){PopupSubmitPreview.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 150px; background-color: #ffffff; padding: 5px 5px 0px 0px; border-radius: 2px;">
            <h1 id="mrpHead" runat="server"></h1>
            <table style="width: 100%; margin: auto;" border="0">
                <tr>
                    <td style="width: 12%">
                        <dx:ASPxLabel runat="server" Text="MRP Number" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td colspan="4">
                        <dx:ASPxLabel ID="DocNum" runat="server" Text="" Theme="Office2010Blue" Style="font-size: medium; font-weight: bold; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;"></dx:ASPxLabel>
                    </td>
                    <td rowspan="3" style="width: 40%; text-align: right; vertical-align: bottom;">
                        <div style="display: none;">
                            <dx:ASPxHiddenField ID="StatusHidden" runat="server" ClientInstanceName="StatusHidden"></dx:ASPxHiddenField>
                            <dx:ASPxHiddenField ID="WrkFlowHidden" runat="server" ClientInstanceName="WrkFlowHidden"></dx:ASPxHiddenField>
                        </div>
                        <dx:ASPxButton ID="btAddEdit" runat="server" Text="Add/Edit" AutoPostBack="false" Theme="Office2010Blue" OnClick="btAddEdit_Click"></dx:ASPxButton>
                        <%--OnClick="Submit_Click"--%>
                        <dx:ASPxButton ID="Submit" runat="server" Text="Submit" AutoPostBack="false" Theme="Office2010Blue">
                            <ClientSideEvents Click="Preview_Submit_Click" />
                        </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Month" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td style="width: 20%">
                        <dx:ASPxLabel ID="Month" runat="server" Text="ASPxLabel" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td style="width: 8%">
                        <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td style="width: 20%">
                        <dx:ASPxLabel ID="EntityCode" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Year" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel ID="Year" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Department" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel ID="BUCode" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Creator" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel ID="Creator" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel runat="server" Text="Status" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                    <td>:</td>
                    <td>
                        <dx:ASPxLabel ID="Status" runat="server" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>

        <div style="background-color: #ffffff; padding: 0px 0px 10px 0px; width: 100%;">

            <table class="rev_prev_table">
                <tr>
                    <td style="width: 50%;">
                        <table runat="server" class="main_prev_table" border="0">
                            <tr>
                                <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="4">REVENUE ASSUMPTIONS</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:ListView ID="RevListview" runat="server" OnItemCommand="RevListview_ItemCommand" OnDataBound="RevListview_DataBound" OnItemDataBound="RevListview_ItemDataBound">
                                        <LayoutTemplate>
                                            <table class="prev_table first_child_prev" runat="server" border="0" rule="cols">
                                                <tr class="headerRow">
                                                    <th id="pk_header" runat="server" style="width: 0px; display: none;"></th>
                                                    <th id="tableHeaderRevDesc" runat="server">Operating Unit</th>
                                                    <th id="prod" style="text-align: left; padding-left: 5px;">Product</th>
                                                    <th id="name">Farm Name</th>
                                                    <th id="volume">Volume</th>
                                                    <th id="prize">Prize</th>
                                                    <th id="total" style="padding: 2px;">Total Amount</th>
                                                </tr>
                                                <tr runat="server" id="itemPlaceholder" />
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="prev" runat="server">
                                                <td id="pk_td" runat="server" style="width: 0px; display: none;">
                                                    <asp:Label ID="RevID" runat="server"
                                                        Text='<%# Eval("PK") %>' Visible="false" />
                                                </td>
                                                <td id="tableDataRevDesc" runat="server" style="border-color: transparent; padding-left: 3%;">
                                                    <asp:Label ID="RevOpr" runat="server"
                                                        Text='<%# Eval("RevDesc") %>' />
                                                </td>
                                                <td id="sec" style="padding-left: 5px;">
                                                    <asp:Label ID="RevProduct" runat="server"
                                                        Text='<%# Eval("ProductName") %>' />
                                                </td>
                                                <td id="third" style="text-align: center;">
                                                    <asp:Label ID="RevFarm" runat="server"
                                                        Text='<%# Eval("FarmName") %>' />
                                                </td>

                                                <td id="fourth" style="text-align: right;">
                                                    <asp:Label ID="RevVolume" runat="server"
                                                        Text='<%# Eval("Volume") %>' />
                                                </td>
                                                <td id="fifth" style="text-align: right;">
                                                    <asp:Label ID="RevPrize" runat="server"
                                                        Text='<%# Eval("Prize") %>' />
                                                </td>
                                                <td id="six" style="text-align: right; padding-right: 5px; border-right-color: transparent;">
                                                    <asp:Label ID="RevTotalPrize" runat="server"
                                                        Text='<%# Eval("TotalPrize") %>' />
                                                </td>
                                                <%--<td style="text-align: right;border-color:transparent;">
                                        <asp:ImageButton ID="pinImg" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="15px" Height="15px" />
                                    </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>
                            <tr>
                                <td id="LabelTARev" runat="server" style="border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                                <td id="TARevenue" runat="server" class="prev_table_cell"></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%;">
                        <table id="tblSummCost" runat="server" class="main_prev_table" border="1">
                            <%--<table id="tblSummCost" runat="server">--%>
                            <tr>
                                <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="3">Summary of Cost and Expenses</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:ListView ID="PreviewListSummary" runat="server">
                                        <LayoutTemplate>
                                            <table class="prev_table" runat="server" border="0" rule="rows">
                                                <tr runat="server" id="itemPlaceholder" />
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="border-right-color: transparent;">
                                                    <asp:Label ID="SummaryDesc" Text='<%#Eval("Name") %>' runat="server" />
                                                </td>
                                                <td style="text-align: right; padding-right: 5px; border-left-color: transparent;">
                                                    <asp:Label ID="SummaryTotal" Text='<%#Eval("Total") %>' runat="server" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; border-right-width: 0px; padding-left: 5px; font-weight: bold;">Total</td>
                                <td id="TotalAmountSummary" runat="server" style="width: 85%; font-weight: bold; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px;"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table runat="server" class="main_prev_table" border="0">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="4">DIRECT MATERIALS</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ListView ID="MatListview" runat="server" OnItemCommand="MatListview_ItemCommand" OnDataBound="MatListview_DataBound" OnItemDataBound="MatListview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="prev_table" runat="server" border="0" rule="cols">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px; display: none;"></th>
                                        <th id="tableHeaderRevDesc" runat="server">Operating Unit</th>
                                        <th id="actTH" style="border-color: transparent;">Activity</th>
                                        <th id="desc" style="text-align: left; padding-left: 5px;">Description</th>
                                        <th id="uom">UOM</th>
                                        <th id="qty">Qty</th>
                                        <th id="cost">Est. Cost/Unit</th>
                                        <th id="total">Total</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="prev" runat="server">
                                    <td id="pk_td" runat="server" style="width: 0px; display: none;">
                                        <asp:Label ID="MatID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>
                                    <td id="tableDataRevDesc" runat="server" style="border-color: transparent; padding-left: 3%;">
                                        <asp:Label ID="MatOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />
                                    </td>
                                    <td id="act" runat="server" style="border-color: transparent;">
                                        <asp:Label runat="server" Text='<%#Eval("ActivityCode")%>'></asp:Label>
                                    </td>
                                    <td id="sec">
                                        <asp:Label ID="MatDescription" runat="server"
                                            Text='<%# Eval("ItemDescription") %>' />
                                    </td>
                                    <td id="third" style="text-align: center;">
                                        <asp:Label ID="MatUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>

                                    <td id="fourth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="MatQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td id="fifth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="MatCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td id="six" style="text-align: right; padding-right: 5px; border-right-color: transparent;">
                                        <asp:Label ID="MatTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>
                                    <%--<td id="pin" runat="server" style="text-align: right; border-color: transparent;">
                                        <asp:ImageButton ID="pinImg" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="15px" Height="15px" />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td id="LabelTotalDM" runat="server" style="border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="TAMat" runat="server" class="prev_table_cell"></td>
                </tr>
            </table>


            <table runat="server" class="main_prev_table" border="0">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="4">OPEX</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ListView ID="OpexListiview" runat="server" OnItemCommand="OpexListiview_ItemCommand" OnDataBound="OpexListiview_DataBound" OnItemDataBound="OpexListiview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="prev_table" runat="server" border="0" rule="cols">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px; display: none;"></th>
                                        <th id="tableHeaderRevDesc" runat="server">Operating Unit</th>
                                        <th id="expTH" style="border-color: transparent;">Expense</th>
                                        <th id="desc" style="text-align: left; padding-left: 5px;">Description</th>
                                        <th id="uom">UOM</th>
                                        <th id="qty">Qty</th>
                                        <th id="cost">Est. Cost/Unit</th>
                                        <th id="total">Total</th>

                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="prev" runat="server">
                                    <td id="pk_td" runat="server" style="width: 0%; display: none;">
                                        <asp:Label ID="OpexID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>

                                    <td id="tableDataRevDesc" runat="server" style="border-color: transparent; padding-left: 3%;">
                                        <asp:Label ID="OPOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />
                                    </td>
                                    <td id="act" runat="server" style="border-color: transparent;">
                                        <asp:Label runat="server" Text='<%#Eval("ExpenseCodeName") %>'></asp:Label>
                                    </td>
                                    <td id="sec" runat="server">
                                        <asp:Label ID="OpexDescription" runat="server"
                                            Text='<%# Eval("Description") %>' />
                                    </td>


                                    <td id="third" style="text-align: center;">
                                        <asp:Label ID="OpexUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>

                                    <td id="fourth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="OpexQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td id="fifth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="OpexCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td id="six" style="text-align: right; padding-right: 5px; border-right-color: transparent;">
                                        <asp:Label ID="OpexTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>

                                    <%--<td id="pin" runat="server" style="text-align: right; border-color: transparent">
                                        <asp:ImageButton ID="pinImg" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="15px" Height="15px" />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td id="LabelTotalOP" runat="server" style="border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="TAOpex" runat="server" class="prev_table_cell"></td>
                </tr>
            </table>

            <table runat="server" class="main_prev_table" border="0">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="4">MANPOWER</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ListView ID="ManListview" runat="server" OnItemCommand="ManListview_ItemCommand" OnDataBound="ManListview_DataBound" OnItemDataBound="ManListview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="prev_table" runat="server" border="0" rule="cols">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px; display: none"></th>
                                        <th id="tableHeaderRevDesc" runat="server">Operating Unit</th>
                                        <th id="actTH" style="border-color: transparent;">Activity</th>
                                        <th id="desc" style="text-align: left; padding-left: 5px;">Description</th>
                                        <th id="uom">UOM</th>
                                        <th id="qty">Qty</th>
                                        <th id="cost">Est. Cost/Unit</th>
                                        <th id="total">Total</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="prev" runat="server">
                                    <td id="pk_td" runat="server" style="width: 0px; display: none;">
                                        <asp:Label ID="ManID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>
                                    <td id="tableDataRevDesc" runat="server" style="border-color: transparent; padding-left: 3%;">
                                        <asp:Label ID="MANOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />
                                    </td>
                                    <td id="act" runat="server" style="border-color: transparent;">
                                        <asp:Label runat="server" Text='<%#Eval("ActivityCode")%>'></asp:Label>
                                    </td>
                                    <td id="sec" style="padding-left: 5px;">
                                        <asp:Label ID="ManDescription" runat="server"
                                            Text='<%# Eval("Description") %>' />
                                    </td>
                                    <td id="third" style="text-align: center;">
                                        <asp:Label ID="ManUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>

                                    <td id="fourth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="ManQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td id="fifth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="ManCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td id="six" style="text-align: right; padding-right: 5px; border-right-color: transparent;">
                                        <asp:Label ID="ManTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>
                                    <%--<td id="pin" runat="server" style="text-align: right; border-color:transparent;">
                                        <asp:ImageButton ID="pinImg" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="15px" Height="15px" />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td id="LabelTotalManpower" runat="server" style="width: 65%; border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="TAManpower" runat="server" class="prev_table_cell"></td>
                </tr>
            </table>


            <table runat="server" class="main_prev_table" border="0">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="4">CAPEX</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ListView ID="CapexListview" runat="server" OnItemCommand="CapexListview_ItemCommand" OnDataBound="CapexListview_DataBound" OnItemDataBound="CapexListview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="prev_table first_child_prev" runat="server" border="0" rule="cols">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px; display: none;"></th>
                                        <th id="tableHeaderRevDesc" runat="server">Operating Unit</th>
                                        <th id="desc" style="text-align: left; padding-left: 5px;">Description</th>
                                        <th id="uom">UOM</th>
                                        <th id="qty">Qty</th>
                                        <th id="cost">Est. Cost/Unit</th>
                                        <th id="total">Total</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="prev" runat="server">
                                    <td id="pk_td" runat="server" style="width: 0px; display: none;">
                                        <asp:Label ID="CapexID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>
                                    
                                    <td id="tableDataRevDesc" runat="server" style="border-color: transparent; padding-left: 3%;">
                                        <asp:Label ID="CAOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />
                                    </td>

                                    <%--<td id="prod_desc" runat="server">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("RevDesc") %>' />
                                    </td>--%>

                                    <td id="sec" style="padding-left: 5px;">
                                        <asp:Label ID="CapexDescription" runat="server"
                                            Text='<%# Eval("Description") %>' />
                                    </td>
                                    <td id="third" style="text-align: center;">
                                        <asp:Label ID="CapexUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>
                                    <td id="fourth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="CapexQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td id="fifth" style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="CapexCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td id="six" style="text-align: right; padding-right: 5px; border-right-color: transparent;">
                                        <asp:Label ID="CapexTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>
                                    <%--<td style="text-align: right;border-color:transparent;">
                                        <asp:ImageButton ID="ImageButton1" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="15px" Height="15px" />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td id="LabelTotalCapex" runat="server" style="border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="TotalAmountTD" runat="server" class="prev_table_cell"></td>
                </tr>
            </table>


        </div>
    </div>


</asp:Content>
