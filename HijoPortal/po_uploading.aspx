<%@ Page Title="PO Uploading Setup" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="po_uploading.aspx.cs" Inherits="HijoPortal.po_uploading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/POUploading.css" rel="stylesheet" />
    <script type="text/javascript" src="jquery/POUploading.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="password" autocomplete="on" style="visibility: hidden; position: absolute; top: -10000px" />

    <dx:ASPxPopupControl ID="DeletePopUp" runat="server" ClientInstanceName="DeletePopUpClient" CloseAction="CloseButton" Theme="Office2010Blue" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table class="delete_table">
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="DeleteLbl" runat="server" ClientInstanceName="DeleteLblClient" Text="Are you sure you want to delete?" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="OK" runat="server" Text="OK" Theme="Office2010Blue">
                                <ClientSideEvents Click="OK_Click" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="CancelPopup" runat="server" Text="Cancel" Theme="Office2010Blue"></dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="ErrorCatcher" runat="server" ClientInstanceName="ErrorCatcher" CloseAction="CloseButton" Theme="Office2010Blue" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table class="delete_table">
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ErrorCatchLbl" runat="server" ClientInstanceName="ErrorCatchLblClient" Text="" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <div id="dvContentWrapper" runat="server" class="ContentWrapper">
        <div id="dvHeader" style="height: 30px;">
            <h1>PO Uploading Setup</h1>
        </div>
        <div id="dvPOUploadingSetup" runat="server" class="scroll-container">
            <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Orientation="Vertical" Width="100%" Height="500px" Theme="Office2010Blue">
                <Panes>
                    <dx:SplitterPane Size="50%" ScrollBars="Auto">
                        <ContentCollection>
                            <dx:SplitterContentControl>
                                <dx:ASPxGridView ID="POGrid" runat="server" ClientInstanceName="POGridClient" Theme="Office2010Blue" Width="100%">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" ShowNewButtonInHeader="true" ButtonRenderMode="Image">
                                            <%--<CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="Update" Image-Url="images/Save.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>--%>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="EntityCode" Visible="false"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Entity"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="HeaderPath"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="LinePath"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Domain"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Name" Caption="Username"></dx:GridViewDataColumn>
                                        <%--<dx:GridViewDataColumn FieldName="PW" Visible="false"></dx:GridViewDataColumn>--%>
                                        <dx:GridViewDataTextColumn FieldName="PW" Visible="false">
                                            <%--<PropertiesTextEdit Password="true" Width=""></PropertiesTextEdit>--%>
                                        </dx:GridViewDataTextColumn>
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


                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxLabel runat="server" Text="Username" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="Uname" runat="server" Text='<%#Eval("Name") %>' Width="100%" Theme="Office2010Blue">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxLabel runat="server" Text="Password" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="Pword" runat="server" Text='<%#Eval("PW") %>' OnInit="Pword_Init" Theme="Office2010Blue" Width="100%">
                                                                                <BorderLeft BorderColor="Transparent" />
                                                                                <BorderRight BorderColor="Transparent" />
                                                                                <BorderTop BorderColor="Transparent" />
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

                                            <div style="text-align: right; margin-top: 2px;">
                                                <dx:ASPxButton runat="server" Text="Save" Theme="Office2010Blue" AutoPostBack="false">
                                                    <ClientSideEvents Click="SaveChanges" />
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
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane Size="50%" ScrollBars="Auto">
                        <ContentCollection>
                            <dx:SplitterContentControl>
                                <dx:ASPxGridView ID="InfoGrid" runat="server" ClientInstanceName="InfoGridClient" Theme="Office2010Blue" Width="100%"
                                    OnStartRowEditing="InfoGrid_StartRowEditing"
                                    OnRowUpdating="InfoGrid_RowUpdating"
                                    OnRowDeleting="InfoGrid_RowDeleting"
                                    OnInitNewRow="InfoGrid_InitNewRow"
                                    OnRowInserting="InfoGrid_RowInserting"
                                    OnBeforeGetCallbackResult="InfoGrid_BeforeGetCallbackResult">
                                    <ClientSideEvents CustomButtonClick="InfoGrid_CustomButtonClick" />
                                    <ClientSideEvents EndCallback="InfoGrid_EndCallback" />
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" ShowNewButtonInHeader="true" ButtonRenderMode="Image">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="Delete" Image-Url="images/Delete.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="Update" Visibility="EditableRow" Image-Url="images/Save.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="Cancel" Visibility="EditableRow" Image-Url="images/Undo.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Code">
                                            <EditItemTemplate>
                                                <dx:ASPxComboBox ID="Code" runat="server" ClientInstanceName="CodeClient" OnInit="Code_Init" ValueType="System.String" Width="70px" Theme="Office2010Blue">
                                                    <ClientSideEvents SelectedIndexChanged="Code_SelectedIndexChanged" />
                                                    <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Entity">
                                            <EditItemTemplate>
                                                <dx:ASPxTextBox ID="Entity" runat="server" Text='<%#Eval("Entity") %>' ClientInstanceName="EntityClient" ReadOnly="true" Border-BorderColor="Transparent" Width="300px" Theme="Office2010Blue"></dx:ASPxTextBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Prefix">
                                            <EditItemTemplate>
                                                <dx:ASPxTextBox ID="Prefix" runat="server" ClientInstanceName="PrefixClient" MaxLength="2" Text='<%#Eval("Prefix") %>' Width="170px" Theme="Office2010Blue">
                                                    <ClientSideEvents KeyPress="FilterDigit_AlphaOnly_KeyPress" />
                                                    <ClientSideEvents KeyUp="ToUpperCase_KeyUp" />
                                                    <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="BeforeSeries">
                                            <EditItemTemplate>
                                                <dx:ASPxTextBox ID="BeforeSeries" runat="server" ClientInstanceName="BeforeSeriesClient" MaxLength="1" Text='<%#Eval("BeforeSeries") %>' Width="170px" Theme="Office2010Blue">
                                                    <ClientSideEvents KeyPress="FilterDigit_AlphaOnly_KeyPress" />
                                                    <ClientSideEvents KeyUp="ToUpperCase_KeyUp" />
                                                    <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="MaxNumber">
                                            <EditItemTemplate>
                                                <dx:ASPxTextBox ID="MaxNumber" runat="server" ClientInstanceName="MaxNumberClient" Text='<%#Eval("MaxNumber") %>' Width="170px" Theme="Office2010Blue">
                                                    <ClientSideEvents KeyPress="FilterDigit_NumberOnly_KeyPress" />
                                                    <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" RequiredField-ErrorText="Empty">
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="LastNumber">
                                            <EditItemTemplate>
                                                <dx:ASPxTextBox ID="LastNumber" runat="server" ClientInstanceName="LastNumberClient" Text='<%#Eval("LastNumber") %>' Width="170px" Theme="Office2010Blue">
                                                    <ClientSideEvents KeyPress="FilterDigit_NumberOnly_KeyPress" />
                                                    <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                    <SettingsCommandButton>
                                        <EditButton Image-Url="images/Edit.ico" Image-Width="15px"></EditButton>
                                        <%--<DeleteButton Image-Url="images/Delete.ico" Image-Width="15px"></DeleteButton>--%>
                                        <NewButton Image-Url="images/Add.ico" Image-Width="15px"></NewButton>
                                        <%--<UpdateButton Image-Url="images/Save.ico" Image-Width="15px"></UpdateButton>--%>
                                        <%--<CancelButton Image-Url="images/Undo.ico" Image-Width="15px"></CancelButton>--%>
                                    </SettingsCommandButton>
                                    <SettingsBehavior AllowFocusedRow="true" AllowSelectSingleRowOnly="true" />

                                </dx:ASPxGridView>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>


        </div>
    </div>
</asp:Content>
