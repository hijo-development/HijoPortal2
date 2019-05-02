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
                            <dx:ASPxButton ID="CancelPopup" runat="server" AutoPostBack="false" Text="Cancel" Theme="Office2010Blue">
                                <ClientSideEvents Click="function(s,e){DeletePopUpClient.Hide();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="DeletePopup2" runat="server" ClientInstanceName="DeletePopUp2Client" CloseAction="CloseButton" Theme="Office2010Blue" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table class="delete_table">
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="DeleteLbl2" runat="server" ClientInstanceName="DeleteLbl2Client" Text="Are you sure you want to delete?" Theme="Office2010Blue"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="OK2" runat="server" Text="OK" Theme="Office2010Blue">
                                <ClientSideEvents Click="OK2_Click" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Cancel" AutoPostBack="false" Theme="Office2010Blue">
                                <ClientSideEvents Click="function(s,e){DeletePopUp2Client.Hide();}" />
                            </dx:ASPxButton>
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

    <%--<div id="dvPOUploadingSetup" runat="server" class="scroll-container">--%>
    <div>
        <dx:ASPxPanel ID="ASPxPanelPOUp" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
            <PanelCollection>
                <dx:PanelContent>
                    <%-- <div>--%>
                    <dx:ASPxSplitter ID="ASPxSplitterPOUp" runat="server" Orientation="Vertical" AllowResize="true" Border-BorderStyle="None" Width="100%" Height="600px" Theme="Office2010Blue">
                        <Panes>
                            <dx:SplitterPane Size="50%" ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl>
                                        <div style="height: 30px;">
                                            <h3>PO Uploading Setup</h3>
                                        </div>
                                        <dx:ASPxGridView ID="POGrid" runat="server" ClientInstanceName="POGridClient" Theme="Office2010Blue" Width="100%"
                                            OnInitNewRow="POGrid_InitNewRow"
                                            OnRowInserting="POGrid_RowInserting"
                                            OnStartRowEditing="POGrid_StartRowEditing"
                                            OnRowUpdating="POGrid_RowUpdating"
                                            OnRowDeleting="POGrid_RowDeleting"
                                            OnBeforeGetCallbackResult="POGrid_BeforeGetCallbackResult"
                                            OnRowValidating="POGrid_RowValidating">
                                            <ClientSideEvents CustomButtonClick="POGrid_CustomButtonClick" />
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowEditButton="true" ShowNewButtonInHeader="true" ButtonRenderMode="Image">
                                                    <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="DeleteRow" Image-Url="images/Delete.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>
                                                        <%--<dx:GridViewCommandColumnCustomButton ID="EditGrid" Image-Url="images/Edit.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>--%>
                                                        <%--<dx:GridViewCommandColumnCustomButton ID="UpdateGrid" Image-Url="images/Save.ico" Image-Width="15px"></dx:GridViewCommandColumnCustomButton>--%>
                                                    </CustomButtons>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataColumn FieldName="PK" Visible="false"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EntityCode" Caption="Code"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Entity" Caption="Entity Name"></dx:GridViewDataColumn>
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
                                                    <dx:ASPxPageControl ID="GridPageControl" runat="server" Width="100%">
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
                                                                                    <dx:ASPxComboBox ID="EntityCode" runat="server" ClientInstanceName="EntityGridClient" OnInit="Entity_Init" Width="100%" ValueType="System.String" Theme="Office2010Blue">
                                                                                        <ClientSideEvents SelectedIndexChanged="Entity_SelectedIndexChanged" />
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                                                    </dx:ASPxComboBox>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                    <dx:ASPxTextBox ID="EntityName" runat="server" ClientInstanceName="EntityNameClient" Text='<%#Eval("Entity") %>' ReadOnly="true" Width="100%" Theme="Office2010Blue" Border-BorderColor="Transparent"></dx:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel runat="server" Text="Header Path" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td colspan="5">
                                                                                    <dx:ASPxTextBox ID="HeaderPath" runat="server" ClientInstanceName="HeaderPathClient" Text='<%#Eval("HeaderPath") %>' Width="100%" Theme="Office2010Blue">
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel runat="server" Text="Limit Path" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td colspan="5">
                                                                                    <dx:ASPxTextBox ID="LinePath" runat="server" ClientInstanceName="LinePathClient" Text='<%#Eval("LinePath") %>' Width="100%" Theme="Office2010Blue">
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel runat="server" Text="Domain" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxTextBox ID="Domain" runat="server" ClientInstanceName="DomainClient" Text='<%#Eval("Domain") %>' Width="100%" Theme="Office2010Blue">
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                </td>


                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel runat="server" Text="Username" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxTextBox ID="Uname" runat="server" ClientInstanceName="UnameClient" Text='<%#Eval("Name") %>' Width="100%" Theme="Office2010Blue">
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxCheckBox ID="AllowPassword" runat="server" ClientInstanceName="AllowPasswordClient" ClientVisible="false" Theme="Office2010Blue">
                                                                                        <ClientSideEvents CheckedChanged="AllowPassword_CheckedChanged" />
                                                                                    </dx:ASPxCheckBox>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="AllowLbl" runat="server" ClientInstanceName="AllowLblClient" ClientVisible="false" Text="Change Password" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="PasswordLbl" runat="server" ClientInstanceName="PasswordLblClient" Text="Password" Theme="Office2010Blue"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxTextBox ID="Pword" runat="server" ClientInstanceName="PWordClient" OnInit="Pword_Init" Theme="Office2010Blue" Width="100%">
                                                                                        <%--<ClientSideEvents Init="OnPasswordTextBoxInit" KeyUp="OnPassChanged" Validation="OnPassValidation" />--%>
                                                                                        <BorderLeft BorderColor="Transparent" />
                                                                                        <BorderRight BorderColor="Transparent" />
                                                                                        <BorderTop BorderColor="Transparent" />
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="Empty"></ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                    <%--<table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dx:ASPxRatingControl ID="ratingControl" runat="server" ReadOnly="true" ItemCount="5" Value="0" ClientInstanceName="ratingControl" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <dx:ASPxLabel ID="ratingLabel" runat="server" ClientInstanceName="ratingLabel" Text="Password safety" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>--%>
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
                                                <%--<DeleteButton Image-Url="images/Delete.ico" Image-Width="15px"></DeleteButton>--%>
                                                <NewButton Image-Url="images/Add.ico" Image-Width="15px"></NewButton>
                                            </SettingsCommandButton>
                                            <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                                            <SettingsBehavior AllowFocusedRow="true" AllowSelectSingleRowOnly="true" />
                                        </dx:ASPxGridView>
                                    </dx:SplitterContentControl>
                                </ContentCollection>
                            </dx:SplitterPane>
                            <dx:SplitterPane Size="50%" ScrollBars="Auto">
                                <ContentCollection>
                                    <dx:SplitterContentControl>
                                        <div style="height: 30px;">
                                            <h3>PO Number Setup</h3>
                                        </div>
                                        <dx:ASPxGridView ID="InfoGrid" runat="server" ClientInstanceName="InfoGridClient" Theme="Office2010Blue" Width="100%"
                                            OnStartRowEditing="InfoGrid_StartRowEditing"
                                            OnRowUpdating="InfoGrid_RowUpdating"
                                            OnRowDeleting="InfoGrid_RowDeleting"
                                            OnInitNewRow="InfoGrid_InitNewRow"
                                            OnRowInserting="InfoGrid_RowInserting"
                                            OnBeforeGetCallbackResult="InfoGrid_BeforeGetCallbackResult"
                                            OnRowValidating="InfoGrid_RowValidating">
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
                                                        <dx:ASPxHiddenField ID="ErrorHiddenValue" runat="server" ClientInstanceName="ErrorHiddenValueClient"></dx:ASPxHiddenField>
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

                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </div>

</asp:Content>
