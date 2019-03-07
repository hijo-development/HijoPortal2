﻿<%@ Page Title="For Approval" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="mrp_previewforapproval.aspx.cs" Inherits="HijoPortal.mrp_previewforapproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <dx:ASPxPopupControl ID="MRPNotifyPrevApp" ClientInstanceName="MRPNotifyPrevApp" runat="server" Modal="true" CloseAction="CloseButton" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxLabel ID="MRPNotifyMsgPrevApp" ClientInstanceName="MRPNotifyMsgPrevApp" runat="server" Text="" Theme="Office2010Blue" ForeColor="Red"></dx:ASPxLabel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 150px; background-color: #ffffff; padding: 5px 5px 0px 0px; border-radius: 2px;">
            <h1>M R P  Preview</h1>
            <table style="width: 95%; margin: auto;" border="0">
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
                            <dx:ASPxHiddenField ID="StatusHidden" runat="server" ClientInstanceName="StatusHiddenPrevApp"></dx:ASPxHiddenField>
                        </div>
                        <dx:ASPxButton ID="Submit" runat="server" Text="Approved" AutoPostBack="false" Theme="Office2010Blue" OnClick="Submit_Click">
                            <ClientSideEvents Click="PreviewForApproval_Submit_Click" />
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
            </table>
        </div>
        <div style="background-color: #ffffff; padding: 0px 0px 10px 0px;">
            <table id="tblSummCost" runat="server" style="width: 95%; margin: auto; margin-bottom: 10px;" border="1">
                <%--<table id="tblSummCost" runat="server">--%>
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;"
                        colspan="3">Summary of Cost and Expenses</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:ListView ID="PreviewListSummary" runat="server">
                            <LayoutTemplate>
                                <table class="table1" style="width: 100%" runat="server" border="1">
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="SummaryDesc" runat="server" />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="SummaryTotal" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75%; border-right-width: 0px; padding-left: 5px;">Total</td>
                    <td id="TotalAmountSummary" runat="server" style="width: 15%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px;"></td>
                    <td style="border-left-width: 0px;"></td>
                </tr>
            </table>

            <table runat="server" style="width: 95%; margin: auto; margin-bottom: 10px;" border="1">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="13">DIRECT MATERIALS</td>
                </tr>
                <tr>
                    <td colspan="13">
                        <asp:ListView ID="MatListview" runat="server" OnItemCommand="MatListview_ItemCommand" OnDataBound="MatListview_DataBound" OnItemDataBound="MatListview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table1" style="width: 100%" runat="server" border="0">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px;"></th>
                                        <th style="width: 20%; text-align: left; padding-left: 5px;">Description</th>
                                        <th id="tableHeaderRevDesc" runat="server" style="width: 10%;">Operating Unit</th>
                                        <th style="width: 7%;">UOM</th>
                                        <th style="width: 6%;">Qty</th>
                                        <th style="width: 7%;">Est. Cost/Unit</th>
                                        <th style="width: 7%;">Total</th>
                                        <th style="width: 5%;">Editted Qty</th>
                                        <th style="width: 7%;">Editted Cost</th>
                                        <th style="width: 7%;">Editted Total Cost</th>
                                        <th style="width: 5%;">Approved Qty</th>
                                        <th style="width: 7%;">Approved Cost</th>
                                        <th style="width: 7%;">Approved Total Cost</th>
                                        <th style="width: 5%;"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td id="pk_td" runat="server" style="width: 0px;">
                                        <asp:Label ID="MatID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>
                                    <td style="padding-left: 5px;">
                                        <asp:Label ID="MatDescription" runat="server"
                                            Text='<%# Eval("ItemDescription") %>' />
                                    </td>
                                    <td id="tableDataRevDesc" runat="server" style="text-align: center;">
                                        <asp:Label ID="MatOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="MatUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>

                                    <td style="text-align: right;">
                                        <asp:Label ID="MatQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="MatCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="MatTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label1" runat="server"
                                            Text='<%# Eval("EdittedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("EdittedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label3" runat="server"
                                            Text='<%# Eval("EdittiedTotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label4" runat="server"
                                            Text='<%# Eval("ApprovedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label5" runat="server"
                                            Text='<%# Eval("ApprovedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# Eval("ApprovedTotalCost") %>' />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="20px" Height="20px" />
                                    </td>
                                </tr>
                                        
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%; border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="extraDMTD" runat="server" style="width: 10%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="TAMat" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ETAMat" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ATAMat" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="border-left-width: 0px; width: 5%;"></td>
                </tr>
            </table>

            <table runat="server" style="width: 95%; margin: auto; margin-bottom: 10px;" border="1">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="13">OPEX</td>
                </tr>
                <tr>
                    <td colspan="13">
                        <asp:ListView ID="OpexListiview" runat="server" OnItemCommand="OpexListiview_ItemCommand" OnDataBound="OpexListiview_DataBound" OnItemDataBound="OpexListiview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table1" style="width: 100%" runat="server" border="0">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px"></th>
                                        <th style="width: 20%; text-align: left; padding-left: 5px;">Description</th>
                                        <th id="tableHeaderRevDesc" runat="server" style="width: 10%;">Operating Unit</th>
                                        <th style="width: 7%;">UOM</th>
                                        <th style="width: 6%;">Qty</th>
                                        <th style="width: 7%;">Est. Cost/Unit</th>
                                        <th style="width: 7%;">Total</th>
                                        <th style="width: 5%;">Editted Qty</th>
                                        <th style="width: 7%;">Editted Cost</th>
                                        <th style="width: 7%;">Editted Total Cost</th>
                                        <th style="width: 5%;">Approved Qty</th>
                                        <th style="width: 7%;">Approved Cost</th>
                                        <th style="width: 7%;">Approved Total Cost</th>
                                        <th style="width: 5%;"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td id="pk_td" runat="server" style="width: 0px">
                                        <asp:Label ID="OpexID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>

                                    <td style="padding-left: 5px;">
                                        <asp:Label ID="OpexDescription" runat="server"
                                            Text='<%# Eval("Description") %>' />
                                    </td>

                                    <td id="tableDataRevDesc" runat="server" style="text-align: center;">
                                        <asp:Label ID="OPOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />

                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="OpexUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>

                                    <td style="text-align: right;">
                                        <asp:Label ID="OpexQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="OpexCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="OpexTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label1" runat="server"
                                            Text='<%# Eval("EdittedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("EdittedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label3" runat="server"
                                            Text='<%# Eval("EdittedTotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label4" runat="server"
                                            Text='<%# Eval("ApprovedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label5" runat="server"
                                            Text='<%# Eval("ApprovedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# Eval("ApprovedTotalCost") %>' />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="20px" Height="20px" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%; border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="extraOPTD" runat="server" style="width: 10%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="TAOpex" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ETAOpex" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ATAOpex" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="border-left-width: 0px; width: 5%;"></td>
                </tr>
            </table>
            

            <table runat="server" style="width: 95%; margin: auto; margin-bottom: 10px;" border="1">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="13">MANPOWER</td>
                </tr>
                <tr>
                    <td colspan="13">
                        <asp:ListView ID="ManListview" runat="server" OnItemCommand="ManListview_ItemCommand" OnDataBound="ManListview_DataBound" OnItemDataBound="ManListview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table1" style="width: 100%" runat="server">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px"></th>
                                        <th style="width: 20%; text-align: left; padding-left: 5px;">Description</th>
                                        <th id="tableHeaderRevDesc" runat="server" style="width: 10%;">Operating Unit</th>
                                        <th style="width: 7%;">UOM</th>
                                        <th style="width: 6%;">Qty</th>
                                        <th style="width: 7%;">Est. Cost/Unit</th>
                                        <th style="width: 7%;">Total</th>
                                        <th style="width: 5%;">Editted Qty</th>
                                        <th style="width: 7%;">Editted Cost</th>
                                        <th style="width: 7%;">Editted Total Cost</th>
                                        <th style="width: 5%;">Approved Qty</th>
                                        <th style="width: 7%;">Approved Cost</th>
                                        <th style="width: 7%;">Approved Total Cost</th>
                                        <th style="width: 5%;"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td id="pk_td" runat="server" style="width: 0px">
                                        <asp:Label ID="ManID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>
                                    <td style="padding-left: 5px;">
                                        <asp:Label ID="ManDescription" runat="server"
                                            Text='<%# Eval("Description") %>' />
                                    </td>

                                    <td id="tableDataRevDesc" runat="server" style="text-align: center;">
                                        <asp:Label ID="MANOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />

                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="ManUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>

                                    <td style="text-align: right;">
                                        <asp:Label ID="ManQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="ManCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="ManTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label1" runat="server"
                                            Text='<%# Eval("EdittedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("EdittedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label3" runat="server"
                                            Text='<%# Eval("EdittiedTotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label4" runat="server"
                                            Text='<%# Eval("ApprovedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label5" runat="server"
                                            Text='<%# Eval("ApprovedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# Eval("ApprovedTotalCost") %>' />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton1" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="20px" Height="20px" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%; border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="extraMANTD" runat="server" style="width: 10%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="TAManpower" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ETAManpower" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ATAManpower" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="border-left-width: 0px; width: 5%;"></td>
                </tr>
            </table>


            <table runat="server" style="width: 95%; margin: auto; margin-bottom: 10px;" border="1">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="13">CAPEX</td>
                </tr>
                <tr>
                    <td colspan="13">
                        <asp:ListView ID="CapexListview" runat="server" OnItemCommand="CapexListview_ItemCommand" OnDataBound="CapexListview_DataBound" OnItemDataBound="CapexListview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table1" style="width: 100%" runat="server">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px"></th>
                                        <th style="width: 20%; text-align: left; padding-left: 5px;">Description</th>
                                        <th id="tableHeaderRevDesc" runat="server" style="width: 10%;">Operating Unit</th>
                                        <th style="width: 7%;">UOM</th>
                                        <th style="width: 6%;">Qty</th>
                                        <th style="width: 7%;">Est. Cost/Unit</th>
                                        <th style="width: 7%;">Total</th>
                                        <th style="width: 5%;">Editted Qty</th>
                                        <th style="width: 7%;">Editted Cost</th>
                                        <th style="width: 7%;">Editted Total Cost</th>
                                        <th style="width: 5%;">Approved Qty</th>
                                        <th style="width: 7%;">Approved Cost</th>
                                        <th style="width: 7%;">Approved Total Cost</th>
                                        <th style="width: 5%;"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td id="pk_td" runat="server" style="width: 0px">
                                        <asp:Label ID="CapexID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>
                                    <td style="padding-left: 5px;">
                                        <asp:Label ID="CapexDescription" runat="server"
                                            Text='<%# Eval("Description") %>' />
                                    </td>

                                    <td id="tableDataRevDesc" runat="server" style="text-align: center;">
                                        <asp:Label ID="CAOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />

                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="CapexUOM" runat="server"
                                            Text='<%# Eval("UOM") %>' />
                                    </td>

                                    <td style="text-align: right;">
                                        <asp:Label ID="CapexQty" runat="server"
                                            Text='<%# Eval("Qty") %>' />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="CapexCost" runat="server"
                                            Text='<%# Eval("Cost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="CapexTotalCost" runat="server"
                                            Text='<%# Eval("TotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label1" runat="server"
                                            Text='<%# Eval("EdittedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("EdittedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label3" runat="server"
                                            Text='<%# Eval("EdittiedTotalCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label4" runat="server"
                                            Text='<%# Eval("ApprovedQty") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label5" runat="server"
                                            Text='<%# Eval("ApprovedCost") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# Eval("ApprovedTotalCost") %>' />
                                    </td>
                                    <td>

                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Link" EnableViewState="false">LinkButton</asp:LinkButton>--%>
                                        <asp:ImageButton ID="ImageButton1" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/images/pin.png" Width="20px" Height="20px" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%; border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="extraCATD" runat="server" style="width: 10%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="TotalAmountTD" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ETotalAmountTD" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="width: 12%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="ATotalAmountTD" runat="server" style="width: 7%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="border-left-width: 0px; width: 5%;"></td>
                </tr>
            </table>

            <%--<table runat="server" style="width: 80%; margin: auto;" border="1">
                <tr>
                    <td style="background-color: mediumspringgreen; border-bottom-color: transparent; text-align: center; font-weight: bold;" colspan="4">REVENUE ASSUMPTIONS</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ListView ID="RevListview" runat="server" OnItemCommand="RevListview_ItemCommand" OnDataBound="RevListview_DataBound" OnItemDataBound="RevListview_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table1" style="width: 100%" runat="server">
                                    <tr class="headerRow">
                                        <th id="pk_header" runat="server" style="width: 0px"></th>
                                        <th style="width: 35%; text-align: left; padding-left: 5px;">Product</th>
                                        <th id="tableHeaderRevDesc" runat="server" style="width: 10%;">Operating Unit</th>
                                        <th style="width: 10%;">Farm Name</th>
                                        <th style="width: 5%;">Volume</th>
                                        <th style="width: 15%;">Prize</th>
                                        <th style="width: 15%;">Total Prize</th>
                                        <th style="width: 10%;"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td id="pk_td" runat="server" style="width: 0px">
                                        <asp:Label ID="RevID" runat="server"
                                            Text='<%# Eval("PK") %>' Visible="false" />
                                    </td>
                                    <td style="padding-left: 5px;">
                                        <asp:Label ID="RevProduct" runat="server"
                                            Text='<%# Eval("ProductName") %>' />
                                    </td>
                                    <td id="tableDataRevDesc" runat="server" style="text-align: center;">
                                        <asp:Label ID="RevOpr" runat="server"
                                            Text='<%# Eval("RevDesc") %>' />

                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="RevFarm" runat="server"
                                            Text='<%# Eval("FarmName") %>' />
                                    </td>

                                    <td style="text-align: right;">
                                        <asp:Label ID="RevVolume" runat="server"
                                            Text='<%# Eval("Volume") %>' />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="RevPrize" runat="server"
                                            Text='<%# Eval("Prize") %>' />
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="RevTotalPrize" runat="server"
                                            Text='<%# Eval("TotalPrize") %>' />
                                    </td>
                                    <td>

                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Link" EnableViewState="false">LinkButton</asp:LinkButton>
                                        <asp:ImageButton ID="ImageButton1" CssClass="link-btn" runat="server" CommandName="Link" ImageUrl="~/Images/comment-black.png" Width="20px" Height="20px" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 65%; border-right-width: 0px; padding-left: 5px; font-weight: bold">Total</td>
                    <td id="extraRevTD" runat="server" style="width: 10%; border-right-width: 0px; border-left-width: 0px;"></td>
                    <td id="TARevenue" runat="server" style="width: 15%; border-left-width: 0px; border-right-width: 0px; text-align: right; padding-right: 5px; font-weight: bold"></td>
                    <td style="border-left-width: 0px; width: 10%"></td>
                </tr>
            </table>--%>
        </div>
    </div>
</asp:Content>
