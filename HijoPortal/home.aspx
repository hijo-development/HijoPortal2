<%@ Page Title="Home" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="HijoPortal.home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        
        <div id="divWelcome" runat="server">
            <table id="tblWelcome" style="width: 100%; height: 625px;">
                <tr style="width: 100%; height: 100%; ">
                    <td style="width: 300px; vertical-align: top; padding: 10px;">
                        <h4>Vision: </h4>
                        <p class="VisionMission"><b>HIJO</b> nurtures nature today to benefit the generation of tomorrow.</p>
                        <h4>Mission:</h4>
                        <p class="VisionMission">To build enterprises in <b>Agribusines</b>, <b>Port & Logistics</b>, <b>Property Development</b>, <b>Leisure & Tourism</b> & <b>Renewable Energy</b> in line with the <b>FELICE</b> principle.</p>
                        <h4>Core Values:</h4>
                        <p class="VisionMission">We aim for <b>EXCELLENCE</b>. We work as a <b>TEAM</b>. We have <b>INTEGRITY</b>. We are <b>STEWARDS</b> of God's creation. We are <b>ENTREPRENEURS</b>. We will <b>GROW</b> while having <b>FUN</b>.</p>
                    </td>
                    <td style="width: 100px"></td>
                    <td style="width: 700px; background-image: url('../images/banner.png'); background-size: 100%; background-repeat: no-repeat;"></td>
                </tr>
            </table>
        </div>

        <div id="divWorkAssigned" runat="server">
            <div id="dvHeader">
                <h1>Work Assigned to me . . .</h1>
            </div>
            <div>
                <dx:ASPxGridView ID="HomeGrid" runat="server" Theme="Office2010Blue" Width="100%">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataHyperLinkColumn FieldName="DocNumber" Width="200px">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink OnInit="DocNumBtn_Init" ID="DocNumBtn" runat="server" Text='<%#Eval("DocNumber")%>' Theme="Office2010Blue">
                                </dx:ASPxHyperLink>
                                <%-- NavigateUrl='<%# string.Format("mrp_addedit.aspx?DocNum={0}&WrkFlwLn=1", Eval("DocNumber")) %>' --%>
                            </DataItemTemplate>
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataColumn FieldName="DateCreated" Caption="Date Created"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="EntityCodeDesc" Caption="Entity"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="BUCodeDesc" Caption="BU/ Department"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="MRPMonthDesc" Caption="Month"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="MRPYear" Caption="Year"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="LevelLine" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="LevelPosition" Caption="Workflow Level"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Status" Caption="Status"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WorkflowType" Visible="false"></dx:GridViewDataColumn>
                    </Columns>
                    <Settings ShowHeaderFilterButton="true" ShowFilterBar="Auto" />
                    <SettingsBehavior AllowFocusedRow="true" ProcessSelectionChangedOnServer="true" AllowSort="true" AllowHeaderFilter="true" />
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
</asp:Content>
