<%@ Page Title="PO Uploading Setup" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="po_uploading.aspx.cs" Inherits="HijoPortal.po_uploading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/POUploading.css" rel="stylesheet" />
    <script type="text/javascript" src="jquery/POUploading.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>PO Uploading Setup</h1>
        </div>
        <div id="dvPOUploadingSetup" runat="server" class="scroll-container">
            <dx:ASPxGridView ID="POGrid" runat="server" ClientInstanceName="POGridClient" Theme="Office2010Blue" Width="100%">
                <Columns>
                    <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" ShowNewButtonInHeader="true" ButtonRenderMode="Image">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntityCode" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Entity"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="HeaderPath"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="LinePath"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Domain"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="User" Caption="Username"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PW" Visible="false"></dx:GridViewDataColumn>
                </Columns>

                <Templates>
                    <EditForm>
                        <%--<div style="width: 500px; background-color: aquamarine;"></div>--%>
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
                            <TabPages>
                                <dx:TabPage Text="Edit">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <table class="edit_table" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="Entity" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="Entity" runat="server" Text='<%#Eval("EntityCode") %>' Width="100%" ValueType="System.String" Theme="Office2010Blue">
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td colspan="4">
                                                        <dx:ASPxTextBox ID="EntityName" runat="server" Text='<%#Eval("Entity") %>' ReadOnly="true" Width="100%" Theme="Office2010Blue" Border-BorderColor="Transparent"></dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="Header Path" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td colspan="5">
                                                        <dx:ASPxTextBox ID="HeaderPath" runat="server" Text='<%#Eval("HeaderPath") %>' Width="100%" Theme="Office2010Blue">
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="Limit Path" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td colspan="5">
                                                        <dx:ASPxTextBox ID="LinePath" runat="server" Text='<%#Eval("LinePath") %>' Width="100%" Theme="Office2010Blue">
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="Domain" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="Domain" runat="server" Text='<%#Eval("Domain") %>' Width="100%" Theme="Office2010Blue">
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="Username" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="Uname" runat="server" Text='<%#Eval("User") %>' Width="100%" Theme="Office2010Blue">
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="Password" Theme="Office2010Blue"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="Pword" runat="server" Text='<%#Eval("PW") %>' AutoResizeWithContainer="false" Password="true" Width="100%" Theme="Office2010Blue">

                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                        <%--<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" RequiredMarkDisplayMode="Auto">

                            <Paddings PaddingBottom="30" PaddingTop="10" />
                            <Items>
                                <dx:LayoutItem>
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Text='<%#Eval("PW") %>' AutoResizeWithContainer="true" Password="true" Width="100%" Theme="Office2010Blue">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>--%>
                        <div style="text-align: right; margin-top: 2px;">
                            <dx:ASPxButton runat="server" Text="Save" Theme="Office2010Blue" AutoPostBack="false">
                                <%--<ClientSideEvents Click="updateDirectMat" />--%>
                            </dx:ASPxButton>
                            <dx:ASPxButton runat="server" Text="Cancel" Theme="Office2010Blue" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){POGridClient.CancelEdit();}" />
                            </dx:ASPxButton>
                        </div>
                    </EditForm>
                </Templates>
                <SettingsCommandButton>
                    <EditButton Image-Url="images/Edit.ico" Image-Width="15px"></EditButton>
                    <DeleteButton Image-Url="images/Delete.ico" Image-Width="15px"></DeleteButton>
                    <NewButton Image-Url="images/Add.ico" Image-Width="15px"></NewButton>
                </SettingsCommandButton>
                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
            </dx:ASPxGridView>

        </div>
    </div>
</asp:Content>
