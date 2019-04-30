$(document).ready(function () {

    $("#LogOut").click(function (e) {
        PopupLogout.SetHeaderText("Alert");
        PopupLogout.Show();
    });

    changeWidth.resizeWidth();

    //AddEditDisplay.cookiesCondition();
});

$(window).resize(function () {
    changeWidth.resizeWidth();
});

var recentstatemenupane = "";
var collapsed_string = "collapsed";
var expanded_string = "expanded";
changeWidth = {
    resizeWidth: function () {
        var heightNoScrollBars = $(window).height();
        var widthNoScrollBars = $(window).width();
        //var x = Math.abs($('body').width() - document.documentElement.clientWidth);
        var fullwidthBrowser = $('body').width();
        //var fullheightBrowser = $('body').height();
        var leftPanel = (fullwidthBrowser * 0.15);
        var centerPanel = fullwidthBrowser * 0.70;
        var origCenterPanel = fullwidthBrowser * 0.85;
        var rightPanel = leftPanel;
        var leftCenter = rightPanel + origCenterPanel;
        var mrpwidth = fullwidthBrowser * 0.84;
        var mrpwidthWrapper = fullwidthBrowser * 0.85;
        var ContentWrapperWidth = fullwidthBrowser * 0.81;
        var containMenu = fullwidthBrowser * 0.15;

        //var contentHeight = 600;
        //$('#MRPPanel').width(mrpwidth);
        $('#MasterPanel').width(mrpwidth);
        $('#AddFormPanel').width(mrpwidth);
        $('#PanelLeft').width(leftPanel);

        var menupanel = MainSplitterClient.GetPaneByName('containMenu');
        menupanel.SetSize(containMenu);
        if (menupanel.IsCollapsed()) {
            $('.ContentWrapper').width(fullwidthBrowser);
            recentstatemenupane = collapsed_string;
        } else {
            $('.ContentWrapper').width(ContentWrapperWidth);
            recentstatemenupane = expanded_string;
        }

        var h = window.innerHeight
            || document.documentElement.clientHeight
            || document.body.clientHeight;

        var contentHeight = h - ($('#dvBanner').height() + $('#footer').height() + 10);
        var contentHeightInside = h - ($('#dvBanner').height() + $('#footer').height() + 35);
        //var mrpWrapperH = h - 130;
        var mrpWrapperH = h - 200;
        var mrpWrapperH_Details = h - 310;
        //var mrpWrapperH_Details = (h - $('#divHeaderMRP')) - 170;

        var HeaderH = $('#dvHeader').height();

        $('#dvChangePW').height(mrpWrapperH + 10);
        $('#MRP_Wrapper').height(mrpWrapperH);
        $('#MRP_Wrapper').width(mrpwidthWrapper);
        $('#MRP_Wrapper_Details').height(mrpWrapperH_Details);

        var DetailH = mrpWrapperH - (HeaderH + 15);
        //var DetailH = 600;

        $('#dvDetails').height(DetailH);

        //MainSplitterClient.Setheight(contentHeight);

        $('#dvSplitter').height(contentHeight);

        $('#dvMOPWorkflow').height(contentHeight - (HeaderH + 30));

        $('#dvSCMSetup').height(contentHeight - (HeaderH + 30));
        $('#dvFinanceSetup').height(contentHeight - (HeaderH + 30));
        $('#dvExecutiveSetup').height(contentHeight - (HeaderH + 30));
        $('#dvWorkflowSetup').height(contentHeight - (HeaderH + 30));
        $('#tblWelcome').height(contentHeight - (HeaderH + 30));

        $('#dvPOUploadingSetup').height(contentHeight - (HeaderH));


        //$('#divWelcome').height(contentHeight - (HeaderH + 100));

        //$('#trWelcome').height(contentHeight - (HeaderH));

        //$('#dvContentWrapper').height(contentHeightInside);

        //console.log("Center Height: " + centerPanelHeight + " form Height: " + formHeight + ":::: " + h1);

        //console.log("Body Height: " + h);
        //console.log("Header Height: " + $('#dvBanner').height());
        //console.log("Footer Height: " + $('#footer').height());
        //console.log("Content Height: " + contentHeight);

        //console.log($('#dvPOUploadingSetup').height());
    }
}

function devSplitterResize(s, e) {
    var h = window.innerHeight
        || document.documentElement.clientHeight
        || document.body.clientHeight;

    var contentHeight = h - ($('#dvBanner').height() + $('#footer').height() + 10);

    $('#MainSplitterClient').height(contentHeight);

    //MainSplitterClient.height(contentHeight);

    //console.log("Body Height: " + h);
    //console.log("Header Height: " + $('#dvBanner').height());
    //console.log("Footer Height: " + $('#footer').height());
    //console.log("Content Height: " + contentHeight);
}

function SplitterContentResize(s, e) {
    var fullwidthBrowser = $('body').width();
    var navWidth = $('#containMenu').width();
    var sidePanelwidth = $('#SideMenu').width();

    var contentWidth = fullwidthBrowser - (navWidth + sidePanelwidth);

    $('#MRP_Wrapper').width(contentWidth);

}

function openNav() {
    //var pane1 = MainSplitterClient.GetPaneByName("containMenu");
    //var pane1 = MainSplitterClient.GetPane(0);
    //pane1.Collapse(pane1);
    //document.getElementById("mySidenav").style.width = "350px";
}

function closeNav() {
    //var pane1 = MainSplitterClient.GetPaneByName("containMenu");
    //pane1.CollapseForward();
    //document.getElementById("mySidenav").style.width = "0";
}

//REUSABLE FUNCTION START HERE....
function FilterDigit(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    //KEY (TAB) keycode: 0
    //KEY (0 to 9) keycode: 48-57
    //Key (DEL)    keycode: 8
    //Key (.)    keycode: 46
    var textboxval = s.GetText().split(".");
    var length = textboxval.length;
    var text = s.GetText().substring(s.GetText().indexOf(".") + 1, s.GetText().length);
    //console.log(text);
    if (key == 46) {
        if (length > 1) {
            ASPxClientUtils.PreventEvent(e.htmlEvent);
        }
    } else {

    }

    if ((key >= 48 && key <= 57) || key == 8 || key == 46 || key == 0) {
        return true;
    } else {
        ASPxClientUtils.PreventEvent(e.htmlEvent);
    }
}

function OnValueChangeQty(s, e) {
    s.SetText(CorrectValue(s.GetText(), 2));
}

function OnValueChange(s, e) {
    s.SetText(CorrectValue(s.GetText(), 1));
}

function CorrectValue(str, type) {
    switch (type) {
        case 1:
            return accounting.formatMoney(str);
        case 2:
            return accounting.formatNumber(str);
    }
}
//END OF REUSABLE FUNCTION

// Master mrp_list.aspx
var MRPListButton;
function CustomButtonClick(s, e) {
    var button = e.buttonID;
    //var hidden_val = MRPHiddenVal.Get('hidden_value');
    //var hidden_val_Stat = MRPHiddenValStatus.Get('hidden_value');
    //console.log(hidden_val);
    MRPListButton = button;
    if (button == "Delete") {
        //var result = confirm("Delete this row?");
        //if (result)
        e.processOnServer = true;
    } else if (button == "Edit") {
        e.processOnServer = true;
    } else if (button == "Preview") {
        e.processOnServer = true;
    } else if (button == "Submit") {
        e.processOnServer = true;
    }
}

function MainTableEndCallback(s, e) {
    var hidden_val = MRPHiddenVal.Get('hidden_value');
    var hidden_val_Stat = MRPHiddenValStatus.Get('hidden_value');
    if (hidden_val === "InvalidCreator") {
        MRPNotificationMessage.SetText("You are not authorized to access this item!");
        MRPNotify.SetHeaderText("Alert");
        MRPNotify.Show();
        MRPHiddenVal.Set('hidden_value', ' ');
    } else if (hidden_val === "Creator") {
        if (hidden_val_Stat === "2") {
            MRPNotificationMessage.SetText("Already submitted to BU / SSU Lead for review!");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
            MRPHiddenValStatus.Set('hidden_value', ' ');
        } else if (hidden_val_Stat === "3") {
            MRPNotificationMessage.SetText("Already submitted for Approval!");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
            MRPHiddenValStatus.Set('hidden_value', ' ');
        } else if (hidden_val_Stat === "4") {
            MRPNotificationMessage.SetText("Already Approved!");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
            MRPHiddenValStatus.Set('hidden_value', ' ');
        } else {
            if (MRPListButton === "Delete") {
                PopupDeleteMRPList.SetHeaderText("Confirm");
                PopupDeleteMRPList.Show();
            } else if (MRPListButton === "Submit") {
                PopupSubmitMRPList.SetHeaderText("Confirm");
                PopupSubmitMRPList.Show();
            }
        }
    } else if (hidden_val === "submitted") {
        MRPNotificationMessage.SetText("Successfully Submitted");
        MRPNotify.SetHeaderText("Info");
        MRPNotify.Show();
        MRPHiddenVal.Set('hidden_value', ' ');
        MainTable.Refresh();//refresh Grid
    } else if (hidden_val === "deleted") {
        MRPNotificationMessage.SetText("Successfully Deleted");
        MRPNotify.SetHeaderText("Info");
        MRPNotify.Show();
        MRPHiddenVal.Set('hidden_value', ' ');
        MainTable.Refresh();//refresh Grid
    }
}


// MRPAddForm Script


//function OperatingUnitCA(s, e) {
//    var text = s.GetSelectedItem().text;
//    if (text.length == 0)
//        s.SetIsValid(false);
//    else
//        s.SetIsValid(true);
//}




var typeCustomDelete = "";
const DM_string = "Direct Materials", OP_string = "OPEX", MAN_string = "Manpower", CA_string = "CAPEX", REV_string = "Revenue";
function DirectMaterialsGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    if (button == "DMEdit") {
        DirectMaterialsGrid_HandleCollapse();

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                s.StartEditRow(e.visibleIndex);
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                s.StartEditRow(e.visibleIndex);
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }

        //s.StartEditRow(e.visibleIndex);
    } else if (button == "DMDelete") {

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                typeCustomDelete = DM_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                typeCustomDelete = DM_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Delete! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }

        //typeCustomDelete = DM_string;
        //PopUpDelete.SetHeaderText("Alert");
        //PopUpDelete.Show();
    }
}

function DMRoundPanel_CollapsedChanging(s, e) {
    DirectMaterialsGrid_HandleCollapse();
}

function DirectMaterialsGrid_HandleCollapse() {
    if (OPEXGrid.IsEditing() || OPEXGrid.IsNewRowEditing())
        OPEXGrid.CancelEdit();

    if (ManPowerGrid.IsEditing() || ManPowerGrid.IsNewRowEditing())
        ManPowerGrid.CancelEdit();

    if (CAPEXGrid.IsEditing() || CAPEXGrid.IsNewRowEditing())
        CAPEXGrid.CancelEdit();

    if (RevenueGrid.IsEditing() || RevenueGrid.IsNewRowEditing())
        RevenueGrid.CancelEdit();

    if (!OpRoundPanel.GetCollapsed())
        OpRoundPanel.SetCollapsed(true);

    if (!ManRoundPanel.GetCollapsed())
        ManRoundPanel.SetCollapsed(true);

    if (!CaRoundPanel.GetCollapsed())
        CaRoundPanel.SetCollapsed(true);

    if (!RevRoundPanel.GetCollapsed())
        RevRoundPanel.SetCollapsed(true);
}

function OPEXGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    if (button == "OPEdit") {
        OPEXGrid_HandleCollapse();

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                s.StartEditRow(e.visibleIndex);
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                s.StartEditRow(e.visibleIndex);
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }
        //s.StartEditRow(e.visibleIndex);
    } else if (button == "OPDelete") {

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                typeCustomDelete = OP_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                typeCustomDelete = OP_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Delete! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }

        //typeCustomDelete = OP_string;
        //PopUpDelete.SetHeaderText("Alert");
        //PopUpDelete.Show();
    }
}

function OpRoundPanel_CollapsedChanging(s, e) {
    OPEXGrid_HandleCollapse();
}

function OPEXGrid_HandleCollapse() {
    if (DirectMaterialsGrid.IsEditing() || DirectMaterialsGrid.IsNewRowEditing())
        DirectMaterialsGrid.CancelEdit();

    if (ManPowerGrid.IsEditing() || ManPowerGrid.IsNewRowEditing())
        ManPowerGrid.CancelEdit();

    if (CAPEXGrid.IsEditing() || CAPEXGrid.IsNewRowEditing())
        CAPEXGrid.CancelEdit();

    if (RevenueGrid.IsEditing() || RevenueGrid.IsNewRowEditing())
        RevenueGrid.CancelEdit();

    if (!DMRoundPanel.GetCollapsed())
        DMRoundPanel.SetCollapsed(true);

    if (!ManRoundPanel.GetCollapsed())
        ManRoundPanel.SetCollapsed(true);

    if (!CaRoundPanel.GetCollapsed())
        CaRoundPanel.SetCollapsed(true);

    if (!RevRoundPanel.GetCollapsed())
        RevRoundPanel.SetCollapsed(true);
}

function ManPowerGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    if (button == "MANEdit") {
        ManPowerGrid_HandleCollapse();

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                s.StartEditRow(e.visibleIndex);
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                s.StartEditRow(e.visibleIndex);
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }

        }
        //s.StartEditRow(e.visibleIndex);
    } else if (button == "MANDelete") {

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                typeCustomDelete = MAN_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                typeCustomDelete = MAN_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Delete! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }

        //typeCustomDelete = MAN_string;
        //PopUpDelete.SetHeaderText("Alert");
        //PopUpDelete.Show();
    }
}

function ManRoundPanel_CollapsedChanging(s, e) {
    ManPowerGrid_HandleCollapse();
}

function ManPowerGrid_HandleCollapse() {
    if (DirectMaterialsGrid.IsEditing() || DirectMaterialsGrid.IsNewRowEditing())
        DirectMaterialsGrid.CancelEdit();

    if (OPEXGrid.IsEditing() || OPEXGrid.IsNewRowEditing())
        OPEXGrid.CancelEdit();

    if (CAPEXGrid.IsEditing() || CAPEXGrid.IsNewRowEditing())
        CAPEXGrid.CancelEdit();

    if (RevenueGrid.IsEditing() || RevenueGrid.IsNewRowEditing())
        RevenueGrid.CancelEdit();

    if (!DMRoundPanel.GetCollapsed())
        DMRoundPanel.SetCollapsed(true);

    if (!OpRoundPanel.GetCollapsed())
        OpRoundPanel.SetCollapsed(true);

    if (!CaRoundPanel.GetCollapsed())
        CaRoundPanel.SetCollapsed(true);

    if (!RevRoundPanel.GetCollapsed())
        RevRoundPanel.SetCollapsed(true);
}

function CAPEXGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    if (button == "CAEdit") {

        CAPEXGrid_HandleCollapse();

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                s.StartEditRow(e.visibleIndex);
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                s.StartEditRow(e.visibleIndex);
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }

        //s.StartEditRow(e.visibleIndex);
    } else if (button == "CADelete") {

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                typeCustomDelete = CA_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                typeCustomDelete = CA_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Delete! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }

        //typeCustomDelete = CA_string;
        //PopUpDelete.SetHeaderText("Alert");
        //PopUpDelete.Show();
    }
}

function CaRoundPanel_CollapsedChanging(s, e) {
    CAPEXGrid_HandleCollapse();
}

function CAPEXGrid_HandleCollapse() {
    if (DirectMaterialsGrid.IsEditing() || DirectMaterialsGrid.IsNewRowEditing())
        DirectMaterialsGrid.CancelEdit();

    if (OPEXGrid.IsEditing() || OPEXGrid.IsNewRowEditing())
        OPEXGrid.CancelEdit();

    if (ManPowerGrid.IsEditing() || ManPowerGrid.IsNewRowEditing())
        ManPowerGrid.CancelEdit();

    if (RevenueGrid.IsEditing() || RevenueGrid.IsNewRowEditing())
        RevenueGrid.CancelEdit();

    if (!DMRoundPanel.GetCollapsed())
        DMRoundPanel.SetCollapsed(true);

    if (!OpRoundPanel.GetCollapsed())
        OpRoundPanel.SetCollapsed(true);

    if (!ManRoundPanel.GetCollapsed())
        ManRoundPanel.SetCollapsed(true);

    if (!RevRoundPanel.GetCollapsed())
        RevRoundPanel.SetCollapsed(true);
}

function RevenueGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    if (button == "REVEdit") {
        RevenueGrid_HandleCollapse();

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                s.StartEditRow(e.visibleIndex);
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                s.StartEditRow(e.visibleIndex);
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }
        //s.StartEditRow(e.visibleIndex);
    } else if (button == "REVDelete") {

        if (wrkflowLine === "0") {
            if (statusKey === "1") {
                typeCustomDelete = REV_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else if (statusKey === "2") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "3") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            } else if (statusKey === "4") {
                Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        } else {
            if (wrklineStatus === "0") {
                typeCustomDelete = REV_string;
                PopUpDelete.SetHeaderText("Alert");
                PopUpDelete.Show();
            } else {
                Add_Edit_MRPNotificationMessage.SetText("Can't Delete! Document already submitted to Inventory Analyst for review.");
                Add_Edit_MRPNotify.SetHeaderText("Alert");
                Add_Edit_MRPNotify.Show();
            }
        }

        //typeCustomDelete = REV_string;
        //PopUpDelete.SetHeaderText("Alert");
        //PopUpDelete.Show();
    }
}

function RevRoundPanel_CollapsedChanging(s, e) {
    RevenueGrid_HandleCollapse();
}

function RevenueGrid_HandleCollapse() {
    if (DirectMaterialsGrid.IsEditing() || DirectMaterialsGrid.IsNewRowEditing())
        DirectMaterialsGrid.CancelEdit();

    if (OPEXGrid.IsEditing() || OPEXGrid.IsNewRowEditing())
        OPEXGrid.CancelEdit();

    if (ManPowerGrid.IsEditing() || ManPowerGrid.IsNewRowEditing())
        ManPowerGrid.CancelEdit();

    if (CAPEXGrid.IsEditing() || CAPEXGrid.IsNewRowEditing())
        CAPEXGrid.CancelEdit();

    if (!DMRoundPanel.GetCollapsed())
        DMRoundPanel.SetCollapsed(true);

    if (!OpRoundPanel.GetCollapsed())
        OpRoundPanel.SetCollapsed(true);

    if (!ManRoundPanel.GetCollapsed())
        ManRoundPanel.SetCollapsed(true);

    if (!CaRoundPanel.GetCollapsed())
        CaRoundPanel.SetCollapsed(true);
}

function OK_DELETE(s, e) {
    switch (typeCustomDelete) {
        case DM_string:
            //console.log(typeCustomDelete);
            DirectMaterialsGrid.DeleteRow(DirectMaterialsGrid.GetFocusedRowIndex());
            break;
        case OP_string:
            OPEXGrid.DeleteRow(OPEXGrid.GetFocusedRowIndex());
            break;
        case MAN_string:
            ManPowerGrid.DeleteRow(ManPowerGrid.GetFocusedRowIndex());
            break;
        case CA_string:
            CAPEXGrid.DeleteRow(CAPEXGrid.GetFocusedRowIndex());
            break;
        case REV_string:
            RevenueGrid.DeleteRow(RevenueGrid.GetFocusedRowIndex());
            break;
    }
    typeCustomDelete = "";
    PopUpDelete.Hide();
}

function CANCEL_DELETE(s, e) {
    PopUpDelete.Hide();
}

function DirectMaterialsGrid_Add(s, e) {

    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    DirectMaterialsGrid_HandleCollapse();

    if (wrkflowLine === "0") {
        if (statusKey === "1") {
            DirectMaterialsGrid.AddNewRow();
        } else if (statusKey === "2") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "3") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "4") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    } else {
        if (wrklineStatus === "0") {
            DirectMaterialsGrid.AddNewRow();
        } else if (statusKey === "2") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "3") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "4") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    }

}

function OPEXGrid_Add(s, e) {
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    OPEXGrid_HandleCollapse();

    if (wrkflowLine === "0") {
        if (statusKey === "1") {
            OPEXGrid.AddNewRow();
        } else if (statusKey === "2") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "3") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "4") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    } else {
        if (wrklineStatus === "0") {
            OPEXGrid.AddNewRow();
        } else {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to Inventory Analyst for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    }
}

function ManPowerGrid_Add(s, e) {
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    ManPowerGrid_HandleCollapse();

    if (wrkflowLine === "0") {
        if (statusKey === "1") {
            ManPowerGrid.AddNewRow();
        } else if (statusKey === "2") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "3") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "4") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    } else {
        if (wrklineStatus === "0") {
            ManPowerGrid.AddNewRow();
        } else {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to Inventory Analyst for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    }
    //ManPowerGrid.AddNewRow();
}

function CAPEXGrid_Add(s, e) {
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    CAPEXGrid_HandleCollapse();

    if (wrkflowLine === "0") {
        if (statusKey === "1") {
            CAPEXGrid.AddNewRow();
        } else if (statusKey === "2") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "3") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "4") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    } else {

        if (wrklineStatus === "0") {
            CAPEXGrid.AddNewRow();
        } else {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to Inventory Analyst for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }

        //if (wrklineStatus === "1") {
        //    CAPEXGrid.AddNewRow();
        //} else {
        //    Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to Inventory Analyst for review.");
        //    Add_Edit_MRPNotify.SetHeaderText("Alert");
        //    Add_Edit_MRPNotify.Show();
        //}
    }
    //CAPEXGrid.AddNewRow();
}

function RevenueGrid_Add(s, e) {
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();

    RevenueGrid_HandleCollapse();

    if (wrkflowLine === "0") {
        if (statusKey === "1") {
            RevenueGrid.AddNewRow();
        } else if (statusKey === "2") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to BU / SSU Lead for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "3") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted for Approval.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "4") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already Approved.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    } else {
        if (wrklineStatus === "0") {
            RevenueGrid.AddNewRow();
        } else {
            Add_Edit_MRPNotificationMessage.SetText("Can't Add! Document already submitted to Inventory Analyst for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    }
    //RevenueGrid.AddNewRow();
}




function updateManpower(s, e) {
    var entityval = entityhiddenMAN.Get('hidden_value');
    var bool = true;
    if (entityval == "display") {
        if (OperatingUnitMAN.GetText().length == 0) {
            OperatingUnitMAN.SetIsValid(false);
            bool = false;
        } else {
            OperatingUnitMAN.SetIsValid(true);
            bool = true;
        }
    }

    var activity = ActivityCodeMAN.GetText();
    var type = ManPowerTypeKeyNameMAN.GetText();
    var itemDesc = DescriptionMAN.GetText();
    var uom = UOMMAN.GetText();
    var cost = CostMAN.GetText();
    var qty = QtyMAN.GetText();
    var totalcost = TotalCostMAN.GetText();

    if (activity.length > 0 && type.length > 0 && itemDesc.length > 0 && uom.length > 0 && cost.length > 0 && qty.length > 0 && totalcost.length > 0 && bool) {
        ManPowerGrid.UpdateEdit();
    }
}

//function updateCAPEX(s, e) {
//    var entityval = entityhiddenCA.Get('hidden_value');
//    var bool = true;
//    if (entityval == "display") {
//        if (OperatingUnitCA.GetText().length == 0) {
//            OperatingUnitCA.SetIsValid(false);
//            bool = false;
//        } else {
//            OperatingUnitCA.SetIsValid(true);
//            bool = true;
//        }
//    }

//    var itemDesc = DescriptionCAPEX.GetText();
//    var uom = UOMCAPEX.GetText();
//    var cost = CostCAPEX.GetText();
//    var qty = QtyCAPEX.GetText();
//    var totalcost = TotalCostCAPEX.GetText();

//    if (itemDesc.length > 0 && uom.length > 0 && cost.length > 0 && qty.length > 0 && totalcost.length > 0 && bool) {
//        CAPEXGrid.UpdateEdit();
//    }
//}

function updateRevenue(s, e) {
    var entityval = entityhiddenREV.Get('hidden_value');
    var bool = true;
    if (entityval == "display") {
        if (OperatingUnitREV.GetText().length == 0) {
            OperatingUnitREV.SetIsValid(false);
            bool = false;
        } else {
            OperatingUnitREV.SetIsValid(true);
            bool = true;
        }
    }

    var product = ProductNameRev.GetText();
    //var farm = FarmNameRev.GetText();
    var prize = PrizeRev.GetText();
    var volume = VolumeRev.GetText();
    var totalprize = TotalPrizeRev.GetText();

    if (product.length > 0 && prize.length > 0 && volume.length > 0 && totalprize.length > 0 && bool) {
        RevenueGrid.UpdateEdit();
    }
}


function OnKeyUpCostDirect(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(QtyDirect.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostDirect.SetText(accounting.formatMoney(total));
            TotalCostDirect.SetIsValid(true);
        }
    } else {
        TotalCostDirect.SetText("");
        TotalCostDirect.SetIsValid(false);
    }
}

function OnKeyUpCostOpex(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(QtyOPEX.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostOPEX.SetText(accounting.formatMoney(total));
            TotalCostOPEX.SetIsValid(true);
        }
    } else {
        TotalCostOPEX.SetText("");
        TotalCostOPEX.SetIsValid(false);
    }
}
function OnKeyUpCostMan(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(QtyMAN.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostMAN.SetText(accounting.formatMoney(total));
            TotalCostMAN.SetIsValid(true);
        }
    } else {
        TotalCostMAN.SetText("");
        TotalCostMAN.SetIsValid(false);
    }
}

//function OnKeyUpCostCapex(s, e) {//OnChange
//    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
//    var cost = parseFloat(accounting.unformat(s.GetText()));
//    var qty = parseFloat(QtyCAPEX.GetText()).toFixed(2);
//    var total = 0;
//    if (qty > 0) {
//        if (cost > 0) {
//            total = cost * qty;
//            TotalCostCAPEX.SetText(accounting.formatMoney(total));
//            TotalCostCAPEX.SetIsValid(true);
//        }
//    } else {
//        TotalCostCAPEX.SetText("");
//        TotalCostCAPEX.SetIsValid(false);
//    }
//}

function OnKeyUpCostRev(s, e) {
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(VolumeRev.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalPrizeRev.SetText(accounting.formatMoney(total));
            TotalPrizeRev.SetIsValid(true);
        }
    } else {
        TotalPrizeRev.SetText("");
        TotalPrizeRev.SetIsValid(false);
    }
}


function OnKeyUpQtyDirect(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(CostDirect.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostDirect.SetText(accounting.formatMoney(total));
            TotalCostDirect.SetIsValid(true);
        }
    } else {
        TotalCostDirect.SetText("");
        TotalCostDirect.SetIsValid(false);
    }
}

function OnKeyUpQtyOpex(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(CostOPEX.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostOPEX.SetText(parseFloat(total).toFixed(2));
            TotalCostOPEX.SetIsValid(true);
        }
    } else {
        TotalCostOPEX.SetText("");
        TotalCostOPEX.SetIsValid(false);
    }
}
function OnKeyUpQtyMan(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(CostMAN.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostMAN.SetText(parseFloat(total).toFixed(2));
            TotalCostMAN.SetIsValid(true);
        }
    } else {
        TotalCostMAN.SetText("");
        TotalCostMAN.SetIsValid(false);
    }
}

//function OnKeyUpQtyCapex(s, e) {//OnChange
//    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
//    var qty = parseFloat(s.GetText()).toFixed(2);
//    var cost = parseFloat(accounting.unformat(CostCAPEX.GetText()));
//    var total = 0;
//    if (qty > 0) {
//        if (cost > 0) {
//            total = cost * qty;
//            TotalCostCAPEX.SetText(parseFloat(total).toFixed(2));
//            TotalCostCAPEX.SetIsValid(true);
//        }
//    } else {
//        TotalCostCAPEX.SetText("");
//        TotalCostCAPEX.SetIsValid(false);
//    }
//}

function OnKeyUpQtyRev(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(PrizeRev.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalPrizeRev.SetText(parseFloat(total).toFixed(2));
            TotalPrizeRev.SetIsValid(true);
        }
    } else {
        TotalPrizeRev.SetText("");
        TotalPrizeRev.SetIsValid(false);
    }
}

//MRP Items


var isItemBeginCallback = 3;
function OnBeginCallback(s, e) {
    if (e.command == "STARTEDIT") {
        var visibleIndex = OPEXGrid.GetFocusedRowIndex();
        OPEXGrid.GetRowValues(visibleIndex, "isItem", OnGetGridViewValues);
    }
}

function OnGetGridViewValues(values) {
    if (values[0] >= 0) isItemBeginCallback = values[0];
}



//var isItem = 1;
//var isProdCat = 1;
//var postponedCallbackOPEXProCat = false;
//function ExpenseCodeIndexChangeOPEX(s, e) {
//    //document.getElementById("itemTD").style.display = "none";
//    //document.getElementById("itemTD2").style.display = "none";
//    //ItemCodeOPEX.SetVisible(false);
//    isItem = parseInt(s.GetSelectedItem().GetColumnText('isItem'));
//    isProdCat = parseInt(s.GetSelectedItem().GetColumnText('isProdCategory'));
//    switch (isItem) {
//        case 0://Non PO
//            document.getElementById("div1").style.display = "none";
//            document.getElementById("div2").style.display = "none";
//            DescriptionOPEX.SetText("");
//            DescriptionOPEX.GetInputElement().readOnly = false;
//            ItemCodeOPEX.SetText("");
//            break;
//        case 1://PO
//            document.getElementById("div1").style.display = "block";
//            document.getElementById("div2").style.display = "block";
//            DescriptionOPEX.SetText("");
//            DescriptionOPEX.GetInputElement().readOnly = true;
//            ItemCodeOPEX.SetText("");
//            break;
//    }

//    switch (isProdCat) {
//        case 0://hide product category combobox
//            document.getElementById("CA_prodcombo_div").style.display = "none";
//            document.getElementById("CA_prodlabel_div").style.display = "none";
//            break;
//        case 1://show product category combobox
//            document.getElementById("CA_prodcombo_div").style.display = "block";
//            document.getElementById("CA_prodlabel_div").style.display = "block";
//            break;
//    }

//    if (ProcCatOPEXCallbackClient.InCallback()) {
//        postponedCallbackOPEXProCat = true;
//    }
//    else {
//        ProcCatOPEXCallbackClient.PerformCallback();
//    }

//}

function Hide() {
    document.getElementById("div1").style.display = "none";
    document.getElementById("div2").style.display = "none";
    DescriptionOPEX.SetText("");
    DescriptionOPEX.GetInputElement().readOnly = false;
    ItemCodeOPEX.SetText("");
}



function listbox_selectedOPEX(s, e) {
    var selValue = s.GetSelectedItem().value;
    var selText = s.GetSelectedItem().text;
    var arrTextItem = selText.split(';');
    ItemCodeOPEX.SetText(selValue);
    //DescriptionOPEX.SetText(selText);
    DescriptionOPEX.SetText(arrTextItem[1].trim());
    DescriptionOPEX.SetIsValid(true);
    UOMOPEX.SetText(arrTextItem[2].trim());
    CostOPEX.SetText(arrTextItem[3].trim());
    listboxOPEX.SetVisible(false);

    //SideBarlblItemCode.SetText(selValue);
    //SideBarlblDescription.SetText(DescriptionOPEX.GetText());
    var entCode = EntityCodeAddEditDirect.GetText();
    var buCode = BUCodeAddEditDirect.GetText();
    params = "OPEX^0^" + entCode + "^" + buCode + "^" + selValue;
    if (FloatCallbackPanel.InCallback())
        postponedCallbackRequired = true;
    else
        FloatCallbackPanel.PerformCallback(params);

    QtyOPEX.Focus();

    var paneSide = MainSplitterClient.GetPaneByName("SideMenu");
    paneSide.Expand();

    var paneMenu = MainSplitterClient.GetPaneByName("containMenu");
    if (paneMenu.IsCollapsed() === false) {
        paneMenu.Collapse(true);
    }


}

//function ActivityCodeIndexChangeMAN(s, e) {
//    var selectedString = s.GetSelectedItem().text;
//    var indexDash = selectedString.indexOf('-');
//    var codeString = selectedString.substring(0, indexDash + 1);
//    var temp = selectedString.substring(indexDash + 1, selectedString.length);
//    var secondString = temp.substring(0, temp.indexOf('-'));
//    ActivityCodeMAN.SetText((codeString + secondString).toString());
//}

//Entity to BU Callback
var postponedCallbackEntitytoBU = false;
function EntityCodeIndexChange(s, e) {
    SetComboBoxEntityID(s);
    var selectedString = s.GetSelectedItem().text;
    EntityCodeDirect.SetText(selectedString.toString());

    //console.log("pass callback");

    if (BUCodeCallbackPanelDirect.InCallback()) {
        postponedCallbackEntitytoBU = true;
    }
    else {
        BUCodeCallbackPanelDirect.PerformCallback();
    }
}

//END CALL BU
function BU_EndCallBack(s, e) {
    if (postponedCallbackEntitytoBU) {
        BUCodeCallbackPanelDirect.PerformCallback();
        postponedCallbackEntitytoBU = false;
    }
}

function BUCodeIndexChange(s, e) {
    SetComboBoxBUID(s);
    var selectedString = s.GetSelectedItem().text;
    BUCodeDirect.SetText(selectedString.toString());
}

function UserLevelDescIndexChange(s, e) {
    SetComboBoxUserLevelID(s);
    var selectedString = s.GetSelectedItem().text;
    EmployeeLevelDirect.SetText(selectedString.toString());
}

function UserStatuscIndexChange(s, e) {
    SetComboBoxUserStatusID(s);
    var selectedString = s.GetSelectedItem().text;
    UserStatusDirect.SetText(selectedString.toString());
}

function SetComboBoxEntityID(s) {
    var EntityID = s.GetValue();
    EntityValueClient.SetText(EntityID);
}

function SetComboBoxBUID(s) {
    var BUID = s.GetValue();
    BUValueClient.SetText(BUID);
}

function SetComboBoxUserLevelID(s) {
    var UserLevelID = s.GetValue();
    UserLevelValueClient.SetText(UserLevelID);
}

function SetComboBoxUserStatusID(s) {
    var UserStatusID = s.GetValue();
    UserStatusValueClient.SetText(UserStatusID);
}

function updateUserList(s, e) {
    var endCode = EntityValueClient.GetText();
    var buCode = BUValueClient.GetText();

    if (endCode.length > 0 && buCode.length > 0) {
        UserListGrid.UpdateEdit();
    }
}


//Direct Materials ITem Code list box
function listbox_selected(s, e) {
    var selValue = s.GetSelectedItem().value;
    var selText = s.GetSelectedItem().text;
    var arrSelText = selText.split(';');

    ItemCodeDirect.SetText(selValue);
    ItemDescriptionDirect.SetText(arrSelText[1].trim());
    ItemDescriptionDirect.SetIsValid(true);
    UOMDirect.SetText(arrSelText[2].trim());
    CostDirect.SetText(arrSelText[3].trim());

    //SideBarlblItemCode.SetText(selValue);
    //SideBarDescription.SetText(arrSelText[1].trim());

    listbox.SetVisible(false);

    var entCode = EntityCodeAddEditDirect.GetText();
    var buCode = BUCodeAddEditDirect.GetText();
    params = "Materials^0^" + entCode + "^" + buCode + "^" + selValue;
    if (FloatCallbackPanel.InCallback())
        postponedCallbackRequired = true;
    else
        FloatCallbackPanel.PerformCallback(params);

    QtyDirect.Focus();

    var paneSide = MainSplitterClient.GetPaneByName("SideMenu");
    paneSide.Expand();

    var paneMenu = MainSplitterClient.GetPaneByName("containMenu");
    paneMenu.Collapse(true);

}

function ItemCodeDirect_KeyPress(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    //KEY (ENTER) keycode: 13
    if (key == 13) {
        ASPxClientUtils.PreventEvent(e.htmlEvent);
        listbox.SetVisible(true);
        listbox.PerformCallback(ItemCodeDirect.GetInputElement().value);
    }
}

//FOR SIDEBAR
var postponedCallbackRequired = false;
var params = "";

function MOPListFocused(s,e) {
    //var visibleIndex = e.GetFocusedRowIndex();
    //var MOPNum = MainTable.GetRowValues(visibleIndex, 'DocNumber');
    //MainTable.GetRowValues(visibleIndex, 'DocNumber', MOPNum);
    var MOPKey = s.GetRowKey(e.visibleIndex);
    params = "MOPList^" + MOPKey + "^^";
    //console.log("params:" + params);
    if (FloatCallbackPanel.InCallback())
        postponedCallbackRequired = true;
    else
        FloatCallbackPanel.PerformCallback(params);
}

function focused(s, e, type) {
    var pk = s.GetRowKey(e.visibleIndex);
    var entCode = EntityCodeAddEditDirect.GetText();
    var buCode = BUCodeAddEditDirect.GetText();
    params = type + "^" + pk + "^" + entCode + "^" + buCode;
    if (FloatCallbackPanel.InCallback())
        postponedCallbackRequired = true;
    else
        FloatCallbackPanel.PerformCallback(params);
}

function focusedInventAnal(s, e, type) {
    var pk = s.GetRowKey(e.visibleIndex);
    var entCode = EntityCodeInventDirect.GetText();
    var buCode = BUCodeInventDirect.GetText();
    params = type + "^" + pk + "^" + entCode + "^" + buCode;
    if (FloatCallbackPanel.InCallback())
        postponedCallbackRequired = true;
    else
        FloatCallbackPanel.PerformCallback(params);
}

//Floating SideBar
function FloatEndCallback(s, e) {
    if (postponedCallbackRequired) {
        FloatCallbackPanel.PerformCallback(params);
        postponedCallbackRequired = false;
    }
}

//Main Splitter Resize
function OnMainSplitterPaneResized(s, e) {
    //var ele = MainSplitterClient.GetPaneByName('containForm').GetElement();
    ////ele.SetHeight(e.pane.GetClientHeight());
    //ele.SetWidth(e.pane.GetClientHeight());
    var name = e.pane.name;
    ResizeContentSplitter(e.pane);

}

function ResizeContentSplitter(control) {
    var ele = MainSplitterClient.GetPaneByName('containForm').GetElement();
    ele.SetWidth(control.GetClientHeight());
    console.log("Panel Height: ");
}

//FOR WORFLOW
function updateWorkflowMaster(s, e) {
    var effectDate = EffectDateDirect.GetText();
    var entCode = EntCodeDirect.GetText();
    var buCode = BUCodeDirect.GetText();
    var buHead = BUHeadDirect.GetText();

    if (effectDate.length > 0 && entCode.length > 0 && buCode.length > 0 && buHead.length > 0) {
        grdWorkflowMasterDirect.UpdateEdit();
    }
}

var postponedCallbackRequiredWorkflow = false;
var paramsWorkflow = "";
function focusedWorkflowMaster(s, e, type) {
    var pk = s.GetRowKey(e.visibleIndex);;
    paramsWorkflow = type + "-" + pk;

    //if (FloatCallbackPanel.InCallback())
    //    postponedCallbackRequiredWorkflow = true;
    //else
    //    postponedCallbackRequiredWorkflow.PerformCallback(paramsWorkflow);
}


var creatorkey = -1;
var hidcreatorkey = -1;
// POCreation
function POCustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "Delete") {
        var result = confirm("Delete this row?");
        if (result)
            e.processOnServer = true;
    } else if (button == "Edit") {
        e.processOnServer = true;
    } else if (button == "Preview") {
        e.processOnServer = true;
    }

}

function POEndCallback(s, e) {
    var hidden_val = HiddenVal.Get('hidden_value');
    if (hidden_val == "AlreadyPO") {
        NotificationMessage.SetText("Already PO");
        Notify.SetHeaderText("Alert");
        Notify.Show();
        HiddenVal.Set('hidden_value', ' ');
    } else if (hidden_val == "InvalidCreator") {
        NotificationMessage.SetText("You are not authorized to access this item");
        Notify.SetHeaderText("Alert");
        Notify.Show();
        HiddenVal.Set('hidden_value', ' ');
    }
}

function POgrid_selectionChanged(s, e) {
    //selList.PerformCallback();
    //s.GetSelectedFieldValues('MRPCategory;Item', GetSelectedFieldValuesCallback);
    s.GetSelectedFieldValues('MRPCategory;Item;UOM;Cost;POQty', GetSelectedFieldValuesCallback);
}

function GetSelectedFieldValuesCallback(values) {
    selList.BeginUpdate();
    //try {
    selList.ClearItems();
    console.log(values.length);
    for (var i = 0; i < values.length; i++) {
        s = "";
        selList.AddItem(values[i], i);
        //for (j = 0; j < values[i].length; j++) {
        //    //s = s + values[i][j] + " ";
        //    //selList.InsertItem(i, values[i][j]);

        //}

        //selList.AddItem(s); 
    }
    //} finally {
    //    selList.EndUpdate();
    //}

    selList.EndUpdate();
    document.getElementById("selCount").innerHTML = POAddEditGrid.GetSelectedRowCount();
}

//FOR PO ADD/EDIT PROD CATEGORY DROPDOWN
function procategory_indexchange(s, e) {
    POAddEditGrid.PerformCallback();
}



//FOR WAREHOUSE IN PO ADD/EDIT
var postponedCallbackRequiredWarehouse = false;
function site_indexchanged(s, e) {
    if (WarehouseCallback.InCallback()) {
        postponedCallbackRequiredWarehouse = true;
        postponedCallbackRequiredLocation = true;
    }
    else {
        WarehouseCallback.PerformCallback();
        LocationCallback.PerformCallback();
    }
}

function warehouse_endcallback(s, e) {
    if (postponedCallbackRequiredWarehouse) {
        WarehouseCallback.PerformCallback();
        postponedCallbackRequiredWarehouse = false;
    }
}

var postponedCallbackRequiredLocation = false;
function warehouse_indexchanged(s, e) {
    Location.SetText("");
    if (LocationCallback.InCallback())
        postponedCallbackRequiredLocation = true;
    else
        LocationCallback.PerformCallback();
}

function location_endcallback(s, e) {
    if (postponedCallbackRequiredLocation) {
        LocationCallback.PerformCallback();
        postponedCallbackRequiredLocation = false;
    }
}

//FOR VENDOR
var postponedCallbackRequiredCurrency = false;
var postponedCallbackRequiredTerms = false;
function vendor_indexchanged(s, e) {
    if (CurrencyCallback.InCallback()) {
        postponedCallbackRequiredCurrency = true;
        postponedCallbackRequiredTerms = true;
    }
    else {
        CurrencyCallback.PerformCallback();
        TermsCallback.PerformCallback();
    }
}

//END CALL BACK CURRENCY
function currency_endcallback(s, e) {
    if (postponedCallbackRequiredCurrency) {
        CurrencyCallback.PerformCallback();
        postponedCallbackRequiredCurrency = false;
    }
}

//END CALL BACK TERMS
function terms_endcallback(s, e) {
    if (postponedCallbackRequiredTerms) {
        TermsCallback.PerformCallback();
        postponedCallbackRequiredTerms = false;
    }
}

//FOR USERLIST
var postponedCallbackuserBU = false;
function UserEntity_IndexChanged(s, e) {
    if (BUCallBackPanelDirect.InCallback()) {
        postponedCallbackuserBU = true;
    }
    else {
        BUCallBackPanelDirect.PerformCallback();
    }
}

function UserBU_EndCallback(s, e) {
    if (postponedCallbackuserBU) {
        BUCallBackPanelDirect.PerformCallback();
        postponedCallbackuserBU = false;
    }
}

function updateUserListNew(s, e) {
    var endCode = EntityCodeDirect.GetText();
    var levelCode = UserLevelDirect.GetText();
    var statusCode = UserStatusDirect.GetText();

    if (endCode.length > 0 && levelCode.length > 0 && statusCode.length > 0) {
        UserListGrid.UpdateEdit();
    }
}

//FOR BU / Dept Head
var postponedCallbackHeadBU = false;
function HeadEntity_IndexChanged(s, e) {
    if (BUCallBackPanelHeadDirect.InCallback()) {
        postponedCallbackHeadBU = true;
    }
    else {
        BUCallBackPanelHeadDirect.PerformCallback();
    }
}

function HeadBU_EndCallback(s, e) {
    if (postponedCallbackHeadBU) {
        BUCallBackPanelHeadDirect.PerformCallback();
        postponedCallbackHeadBU = false;
    }
}

function updateBUDeptHeadList(s, e) {
    var endCode = EntityCodeHeadDirect.GetText();
    var headCode = BUHeadDirect.GetText();
    var effectDate = EffectDateHeadDirect.GetText();
    var Status = BUHeadStatusDirect.GetText();

    if (endCode.length > 0 && headCode.length > 0 && effectDate.length > 0 && Status.length > 0) {
        BUDeptListGridDirect.UpdateEdit();
    }
}


//FOR SCM
var postponedCallbackSCMProcCat = false;
function OnGridFocusedRowChangedSCMProcOff(s, e) {
    grdSCMProcurementOffDetailsDirect.PerformCallback();
}

function OnGridFocusedRowChangedSCMProcOff_EndCallback(s, e) {
    grdSCMProcurementOffDetailsDirect.Refresh();
}
//END OF ADD FORM SCRIPT HERE.....

//FOR Finance
var postponedCallbackFinBudBU = false;
function FinBudEntity_IndexChanged(s, e) {
    if (FinBUCallbackPanelDirect.InCallback()) {
        postponedCallbackFinBudBU = true;
    }
    else {
        FinBUCallbackPanelDirect.PerformCallback();
    }
}

function FinBudBU_EndCallback(s, e) {
    if (postponedCallbackFinBudBU) {
        FinBUCallbackPanelDirect.PerformCallback();
        postponedCallbackFinBudBU = false;
    }
}

function OnGridFocusedRowChangedFinBud(s, e) {
    grdFinanceBudgetDetDirect.PerformCallback();
}

function OnGridFocusedRowChangedFinBud_EndCallback(s, e) {
    grdFinanceBudgetDetDirect.Refresh();

}


//mrp_inventanalyst
function MRPanalystfocused(s, e, type) {
    //var pk = s.GetRowKey(e.visibleIndex);;
    //params = type + "-" + pk;

    //if (FloatCallbackPanel.InCallback())
    //    postponedCallbackRequired = true;
    //else
    //    FloatCallbackPanel.PerformCallback(params);
}



















//mrp_forapproval.aspx
//for Approval Qty Direct Materials
function OnKeyUpApprovedQtyDirect(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(ApprovedCostDM.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostDM.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostDM.SetText("");
    }
}

//for Approval Cost Direct Materials
function OnKeyUpApprovedCostDirect(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(ApprovedQtyDM.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostDM.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostDM.SetText("");
    }
}

//for Approval Qty Opex
function OnKeyUpQtyApprovedQtyOpex(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(ApprovedCostOpex.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostOpex.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostOpex.SetText("");
    }
}


//for Approval Cost Opex
function OnKeyUpCostApprovedCostOpex(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(ApprovedQtyOpex.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostOpex.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostOpex.SetText("");
    }
}


//for Approval Qty Manpower
function OnKeyUpApprovedQtyManPower(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(ApprovedCostManPower.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostManPower.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostManPower.SetText("");
    }
}

//for Approval Cost Manpower
function OnKeyUpApprovedCostManPower(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(ApprovedQtyManPower.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostManPower.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostManPower.SetText("");
    }
}

//for Approval Qty Capex
function OnKeyUpApprovedQtyCapex(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(ApprovedCostCapex.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostCapex.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostCapex.SetText("");
    }
}


//for Approval Cost Capex
function OnKeyUpApprovedCostCapex(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(ApprovedQtyCapex.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            ApprovedTotalCostCapex.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        ApprovedTotalCostCapex.SetText("");
    }
}

//function (s, e) {
//    var entity = lblEntity.getvalue();
//    MRPNotificationMessage.SetText('You are not authorized to access this item');
//    MRPNotify.SetHeaderText('Alert');
//    MRPNotify.Show();
//    MRPHiddenVal.Set('hidden_value', ' ');
//}


function AddMOPCheckEntity(s, e) {
    console.log("pass script");
    var entity = ASPxLabelEntDirect.GetText();
    if (entity === "") {
        MRPNotificationMessage.SetText("No assigned entity!");
        MRPNotify.SetHeaderText("Alert");
        MRPNotify.Show();
    }
}

//mrp_inventoryanalyst_forapproval
function OnKeyUpInvAppEditQtyDM(s, e) {//OnChange
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(InvAppEditCostDM.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalDM.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalDM.SetText("");
    }
}

function OnKeyUpInvAppEditCostDM(s, e) {//OnChange
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(InvAppEditQtyDM.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalDM.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalDM.SetText("");
    }
}

function OnKeyUpInvAppEditQtyOP(s, e) {//OnChange
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(InvAppEditCostOP.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalOP.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalOP.SetText("");
    }
}

function OnKeyUpInvAppEditCostOP(s, e) {//OnChange
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(InvAppEditQtyOP.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalOP.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalOP.SetText("");
    }
}

function OnKeyUpInvAppEditQtyMAN(s, e) {//OnChange
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(InvAppEditCostMAN.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalMAN.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalMAN.SetText("");
    }
}

function OnKeyUpInvAppEditCostMAN(s, e) {//OnChange
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(InvAppEditQtyMAN.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalMAN.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalMAN.SetText("");
    }
}

function OnKeyUpInvAppEditQtyCA(s, e) {//OnChange
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(InvAppEditCostCA.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalCA.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalCA.SetText("");
    }
}

function OnKeyUpInvAppEditCostCA(s, e) {//OnChange
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(InvAppEditQtyCA.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvAppEditTotalCA.SetText(accounting.formatMoney(total));
        }
    } else {
        InvAppEditTotalCA.SetText("");
    }
}

function DMGridInventApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "DMInvEdit") {
        DMGridInventApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function DMGridInvAppRoundPanel_CollapsedChanging(s, e) {
    DMGridInventApproval_HandleCollapse();
}

function DMGridInventApproval_HandleCollapse() {
    if (OPGridInventApproval.IsEditing() || OPGridInventApproval.IsNewRowEditing())
        OPGridInventApproval.CancelEdit();

    if (!OPGridInvAppRoundPanel.GetCollapsed())
        OPGridInvAppRoundPanel.SetCollapsed(true);

    if (MANGridInventApproval.IsEditing() || MANGridInventApproval.IsNewRowEditing())
        MANGridInventApproval.CancelEdit();

    if (!MANGridInvAppRoundPanel.GetCollapsed())
        MANGridInvAppRoundPanel.SetCollapsed(true);

    if (CAGridInventApproval.IsEditing() || CAGridInventApproval.IsNewRowEditing())
        CAGridInventApproval.CancelEdit();

    if (!CAGridInvAppRoundPanel.GetCollapsed())
        CAGridInvAppRoundPanel.SetCollapsed(true);
}

function OPGridInventApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "OPInvEdit") {
        OPGridInventApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function OPGridInvAppRoundPanel_CollapsedChanging(s, e) {
    OPGridInventApproval_HandleCollapse();
}

function OPGridInventApproval_HandleCollapse() {
    if (DMGridInventApproval.IsEditing() || DMGridInventApproval.IsNewRowEditing())
        DMGridInventApproval.CancelEdit();

    if (!DMGridInvAppRoundPanel.GetCollapsed())
        DMGridInvAppRoundPanel.SetCollapsed(true);

    if (MANGridInventApproval.IsEditing() || MANGridInventApproval.IsNewRowEditing())
        MANGridInventApproval.CancelEdit();

    if (!MANGridInvAppRoundPanel.GetCollapsed())
        MANGridInvAppRoundPanel.SetCollapsed(true);

    if (CAGridInventApproval.IsEditing() || CAGridInventApproval.IsNewRowEditing())
        CAGridInventApproval.CancelEdit();

    if (!CAGridInvAppRoundPanel.GetCollapsed())
        CAGridInvAppRoundPanel.SetCollapsed(true);
}

function MANGridInventApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "MANInvEdit") {
        MANGridInventApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function MANGridInvAppRoundPanel_CollapsedChanging(s, e) {
    MANGridInventApproval_HandleCollapse();
}

function MANGridInventApproval_HandleCollapse() {
    if (DMGridInventApproval.IsEditing() || DMGridInventApproval.IsNewRowEditing())
        DMGridInventApproval.CancelEdit();

    if (!DMGridInvAppRoundPanel.GetCollapsed())
        DMGridInvAppRoundPanel.SetCollapsed(true);

    if (OPGridInventApproval.IsEditing() || OPGridInventApproval.IsNewRowEditing())
        OPGridInventApproval.CancelEdit();

    if (!OPGridInvAppRoundPanel.GetCollapsed())
        OPGridInvAppRoundPanel.SetCollapsed(true);

    if (CAGridInventApproval.IsEditing() || CAGridInventApproval.IsNewRowEditing())
        CAGridInventApproval.CancelEdit();

    if (!CAGridInvAppRoundPanel.GetCollapsed())
        CAGridInvAppRoundPanel.SetCollapsed(true);
}

function CAGridInventApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "CAInvEdit") {
        CAGridInventApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function CAGridInvAppRoundPanel_CollapsedChanging(s, e) {
    CAGridInventApproval_HandleCollapse();
}

function CAGridInventApproval_HandleCollapse() {
    if (DMGridInventApproval.IsEditing() || DMGridInventApproval.IsNewRowEditing())
        DMGridInventApproval.CancelEdit();

    if (!DMGridInvAppRoundPanel.GetCollapsed())
        DMGridInvAppRoundPanel.SetCollapsed(true);

    if (OPGridInventApproval.IsEditing() || OPGridInventApproval.IsNewRowEditing())
        OPGridInventApproval.CancelEdit();

    if (!OPGridInvAppRoundPanel.GetCollapsed())
        OPGridInvAppRoundPanel.SetCollapsed(true);

    if (MANGridInventApproval.IsEditing() || MANGridInventApproval.IsNewRowEditing())
        MANGridInventApproval.CancelEdit();

    if (!MANGridInvAppRoundPanel.GetCollapsed())
        MANGridInvAppRoundPanel.SetCollapsed(true);
}
//...END OF...mrp_inventoryanalyst_forapproval

//mrp_inventanalyst
function DMGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;

    var wrkflowLine = WorkFlowLineTxtInventAnal.GetText();
    var statusKey = StatusKeyTxtInventAnal.GetText();

    if (button == "DMGridEdit") {
        DMGrid_HandleCollapse();
        if (statusKey === "0") {
            s.StartEditRow(e.visibleIndex);
        } else {
            Add_Edit_MRPNotificationMessage_InventAnal.SetText("Can't Edit! Document already submitted to Budget for review.");
            Add_Edit_MRPNotify_InventAnal.SetHeaderText("Alert");
            Add_Edit_MRPNotify_InventAnal.Show();
        }


        //s.StartEditRow(e.visibleIndex);
    }
}

function DMGridRoundPanel_CollapsedChanging(s, e) {
    DMGrid_HandleCollapse();
}

function DMGrid_HandleCollapse() {
    if (OpGrid.IsEditing() || OpGrid.IsNewRowEditing())
        OpGrid.CancelEdit();

    if (!OPGridRoundPanel.GetCollapsed())
        OPGridRoundPanel.SetCollapsed(true);

    if (ManPoGrid.IsEditing() || ManPoGrid.IsNewRowEditing())
        ManPoGrid.CancelEdit();

    if (!MANGridRoundPanel.GetCollapsed())
        MANGridRoundPanel.SetCollapsed(true);

    if (CapGrid.IsEditing() || CapGrid.IsNewRowEditing())
        CapGrid.CancelEdit();

    if (!CAGridRoundPanel.GetCollapsed())
        CAGridRoundPanel.SetCollapsed(true);
}

function OpGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var statusKey = StatusKeyTxtInventAnal.GetText();
    if (button == "OPGridEdit") {
        OpGrid_HandleCollapse();
        if (statusKey === "0") {
            s.StartEditRow(e.visibleIndex);
        } else {
            Add_Edit_MRPNotificationMessage_InventAnal.SetText("Can't Edit! Document already submitted to Budget for review.");
            Add_Edit_MRPNotify_InventAnal.SetHeaderText("Alert");
            Add_Edit_MRPNotify_InventAnal.Show();
        }

        //s.StartEditRow(e.visibleIndex);
    }
}

function OPGridRoundPanel_CollapsedChanging(s, e) {
    OpGrid_HandleCollapse();
}

function OpGrid_HandleCollapse() {
    if (DMGrid.IsEditing() || DMGrid.IsNewRowEditing())
        DMGrid.CancelEdit();

    if (!DMGridRoundPanel.GetCollapsed())
        DMGridRoundPanel.SetCollapsed(true);

    if (ManPoGrid.IsEditing() || ManPoGrid.IsNewRowEditing())
        ManPoGrid.CancelEdit();

    if (!MANGridRoundPanel.GetCollapsed())
        MANGridRoundPanel.SetCollapsed(true);

    if (CapGrid.IsEditing() || CapGrid.IsNewRowEditing())
        CapGrid.CancelEdit();

    if (!CAGridRoundPanel.GetCollapsed())
        CAGridRoundPanel.SetCollapsed(true);
}

function ManPoGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var statusKey = StatusKeyTxtInventAnal.GetText();
    if (button == "MANGridEdit") {
        ManPoGrid_HandleCollapse();
        if (statusKey === "0") {
            s.StartEditRow(e.visibleIndex);
        } else {
            Add_Edit_MRPNotificationMessage_InventAnal.SetText("Can't Edit! Document already submitted to Budget for review.");
            Add_Edit_MRPNotify_InventAnal.SetHeaderText("Alert");
            Add_Edit_MRPNotify_InventAnal.Show();
        }

        //s.StartEditRow(e.visibleIndex);
    }
}

function MANGridRoundPanel_CollapsedChanging(s, e) {
    ManPoGrid_HandleCollapse();
}

function ManPoGrid_HandleCollapse() {
    if (DMGrid.IsEditing() || DMGrid.IsNewRowEditing())
        DMGrid.CancelEdit();

    if (!DMGridRoundPanel.GetCollapsed())
        DMGridRoundPanel.SetCollapsed(true);

    if (OpGrid.IsEditing() || OpGrid.IsNewRowEditing())
        OpGrid.CancelEdit();

    if (!OPGridRoundPanel.GetCollapsed())
        OPGridRoundPanel.SetCollapsed(true);

    if (CapGrid.IsEditing() || CapGrid.IsNewRowEditing())
        CapGrid.CancelEdit();

    if (!CAGridRoundPanel.GetCollapsed())
        CAGridRoundPanel.SetCollapsed(true);
}

function CapGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    var statusKey = StatusKeyTxtInventAnal.GetText();
    if (button == "CAGridEdit") {
        CapGrid_HandleCollapse();
        if (statusKey === "0") {
            s.StartEditRow(e.visibleIndex);
        } else {
            Add_Edit_MRPNotificationMessage_InventAnal.SetText("Can't Edit! Document already submitted to Budget for review.");
            Add_Edit_MRPNotify_InventAnal.SetHeaderText("Alert");
            Add_Edit_MRPNotify_InventAnal.Show();
        }

        //s.StartEditRow(e.visibleIndex);
    }
}

function CAGridRoundPanel_CollapsedChanging(s, e) {
    CapGrid_HandleCollapse();
}

function CapGrid_HandleCollapse() {
    if (DMGrid.IsEditing() || DMGrid.IsNewRowEditing())
        DMGrid.CancelEdit();

    if (!DMGridRoundPanel.GetCollapsed())
        DMGridRoundPanel.SetCollapsed(true);

    if (ManPoGrid.IsEditing() || ManPoGrid.IsNewRowEditing())
        ManPoGrid.CancelEdit();

    if (!MANGridRoundPanel.GetCollapsed())
        MANGridRoundPanel.SetCollapsed(true);

    if (OpGrid.IsEditing() || OpGrid.IsNewRowEditing())
        OpGrid.CancelEdit();

    if (!OPGridRoundPanel.GetCollapsed())
        OPGridRoundPanel.SetCollapsed(true);
}
//...END OF...mrp_inventanalyst

//mrp_forapproval
function DMGridApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "DMAppEdit") {
        DMGridApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function DMGridAppRoundPanel_CollapsedChanging(s, e) {
    DMGridApproval_HandleCollapse();
}

function DMGridApproval_HandleCollapse() {
    if (OpexGridApproval.IsEditing() || OpexGridApproval.IsNewRowEditing())
        OpexGridApproval.CancelEdit();

    if (!OPGridAppRoundPanel.GetCollapsed())
        OPGridAppRoundPanel.SetCollapsed(true);

    if (ManPowerGridApproval.IsEditing() || ManPowerGridApproval.IsNewRowEditing())
        ManPowerGridApproval.CancelEdit();

    if (!MANGridAppRoundPanel.GetCollapsed())
        MANGridAppRoundPanel.SetCollapsed(true);

    if (CapexGridApproval.IsEditing() || CapexGridApproval.IsNewRowEditing())
        CapexGridApproval.CancelEdit();

    if (!CAGridAppRoundPanel.GetCollapsed())
        CAGridAppRoundPanel.SetCollapsed(true);
}

function OpexGridApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "OPAppEdit") {
        OpexGridApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function OPGridAppRoundPanel_CollapsedChanging(s, e) {
    OpexGridApproval_HandleCollapse();
}

function OpexGridApproval_HandleCollapse() {
    if (DMGridApproval.IsEditing() || DMGridApproval.IsNewRowEditing())
        DMGridApproval.CancelEdit();

    if (!DMGridAppRoundPanel.GetCollapsed())
        DMGridAppRoundPanel.SetCollapsed(true);

    if (ManPowerGridApproval.IsEditing() || ManPowerGridApproval.IsNewRowEditing())
        ManPowerGridApproval.CancelEdit();

    if (!MANGridAppRoundPanel.GetCollapsed())
        MANGridAppRoundPanel.SetCollapsed(true);

    if (CapexGridApproval.IsEditing() || CapexGridApproval.IsNewRowEditing())
        CapexGridApproval.CancelEdit();

    if (!CAGridAppRoundPanel.GetCollapsed())
        CAGridAppRoundPanel.SetCollapsed(true);
}

function ManPowerGridApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "MANAppEdit") {
        ManPowerGridApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function MANGridAppRoundPanel_CollapsedChanging(s, e) {
    ManPowerGridApproval_HandleCollapse();
}

function ManPowerGridApproval_HandleCollapse() {
    if (DMGridApproval.IsEditing() || DMGridApproval.IsNewRowEditing())
        DMGridApproval.CancelEdit();

    if (!DMGridAppRoundPanel.GetCollapsed())
        DMGridAppRoundPanel.SetCollapsed(true);

    if (OpexGridApproval.IsEditing() || OpexGridApproval.IsNewRowEditing())
        OpexGridApproval.CancelEdit();

    if (!OPGridAppRoundPanel.GetCollapsed())
        OPGridAppRoundPanel.SetCollapsed(true);

    if (CapexGridApproval.IsEditing() || CapexGridApproval.IsNewRowEditing())
        CapexGridApproval.CancelEdit();

    if (!CAGridAppRoundPanel.GetCollapsed())
        CAGridAppRoundPanel.SetCollapsed(true);
}

function CapexGridApproval_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "CAAppEdit") {
        CapexGridApproval_HandleCollapse();
        s.StartEditRow(e.visibleIndex);
    }
}

function CAGridAppRoundPanel_CollapsedChanging(s, e) {
    CapexGridApproval_HandleCollapse();
}

function CapexGridApproval_HandleCollapse() {
    if (OpexGridApproval.IsEditing() || OpexGridApproval.IsNewRowEditing())
        OpexGridApproval.CancelEdit();

    if (!OPGridAppRoundPanel.GetCollapsed())
        OPGridAppRoundPanel.SetCollapsed(true);

    if (ManPowerGridApproval.IsEditing() || ManPowerGridApproval.IsNewRowEditing())
        ManPowerGridApproval.CancelEdit();

    if (!MANGridAppRoundPanel.GetCollapsed())
        MANGridAppRoundPanel.SetCollapsed(true);

    if (DMGridApproval.IsEditing() || DMGridApproval.IsNewRowEditing())
        DMGridApproval.CancelEdit();

    if (!DMGridAppRoundPanel.GetCollapsed())
        DMGridAppRoundPanel.SetCollapsed(true);
}


//...END OF...mrp_forapproval

function ListBudgetGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "BudgetGridEdit") {
        e.processOnServer = true;
    }
}

//mrp_preview
function Preview_Submit_Click(s, e) {
    var stat = StatusHidden.Get("hidden_preview_iStatusKey");
    var workline = WrkFlowHidden.Get("hidden_preview_wrkflwln");
    if (stat == "0")//0 
    {
        PopupSubmitPreview.SetHeaderText('Confirm');
        PopupSubmitPreview.Show();
        //e.processOnServer = true;
    }
    else {//1 submitted
        if (workline == "0") {
            MRPNotificationMessage.SetText("Document already submitted to BU / SSU Lead for review.");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
        } else if (workline == "1") {
            MRPNotificationMessage.SetText("Document already submitted to Inventory Analyst for review.");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
        } else if (workline == "2") {
            MRPNotificationMessage.SetText("Document already submitted to Budget for review.");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
        }

        //e.processOnServer = false;
    }
}

//mrp_preview_analyst
function Preview_Submit_Analyst_Click(s, e) {
    var stat = StatusHiddenAnal.Get("hidden_preview_iStatusKey");
    var workline = WrkFlowHiddenAnal.Get("hidden_preview_wrkflwln");
    if (stat == "0")//0 
    {
        PopupSubmitPreviewAnal.SetHeaderText('Confirm');
        PopupSubmitPreviewAnal.Show();
        //e.processOnServer = true;
    } else {
        if (workline == "3") {
            MRPNotificationMessage.SetText("Document already submitted to Finance - Budget for review.");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
        } else if (workline == "4") {
            MRPNotificationMessage.SetText("Document already submitted for Deliberation.");
            MRPNotify.SetHeaderText("Alert");
            MRPNotify.Show();
        }
    }
}

//Master.Master Splitter Collapse
function MainSplitterClient_PaneCollapsed(s, e) {
    SplitterBehavior(s, e);
}

function MainSplitterClient_PaneExpanded(s, e) {
    SplitterBehavior(s, e);
}

function SplitterBehavior(s, e) {
    var fullwidthBrowser = $('body').width();
    var partfullwidthBrowser = fullwidthBrowser * 0.96;
    var ContentWrapperWidth = fullwidthBrowser * 0.81;

    var menupanel = s.GetPaneByName('containMenu');

    switch (recentstatemenupane) {
        case collapsed_string:
            if (!menupanel.IsCollapsed()) {
                $('.ContentWrapper').width(ContentWrapperWidth);
                recentstatemenupane = expanded_string;
            }
            break;
        case expanded_string:
            if (menupanel.IsCollapsed()) {
                $('.ContentWrapper').width(partfullwidthBrowser);
                recentstatemenupane = collapsed_string;
            }
            break;
    }
}

function MainSplitterClient_PaneResized(s, e) {
    var menupanel = s.GetPaneByName('containMenu');
    var fullwidthBrowser = $('body').width();
    var size_str = menupanel.GetSize();
    var size_px = size_str.replace("px", "");
    var whatsleft = (fullwidthBrowser * 0.96) - parseInt(size_px);
    $('.ContentWrapper').width(whatsleft);
}

//mrp_listforapproval
function ListForApprovalGrid_CustomButtonClick(s, e) {
    var button = e.buttonID;
    if (button == "ForApprovalGridEdit") {
        e.processOnServer = true;
    }
}

//mrp_previewforapproval
function PreviewForApproval_Submit_Click(s, e) {
    var stat = StatusHiddenPrevApp.Get("hidden_preview_iStatusKey");
    var workline = StatusHiddenPrevAppWL.Get("hidden_preview_wrkflwln");
    //console.log(stat);
    //console.log(workline);
    if (stat == "0")//0 
    {
        PopupSubmitAppPreview.SetHeaderText('Confirm');
        PopupSubmitAppPreview.Show();
        //e.processOnServer = true;
    }
    else {//1 submitted
        if (workline == "1") {
            MRPNotifyMsgPrevApp.SetText("Document already approved by SCM Lead.");
            MRPNotifyPrevApp.SetHeaderText("Alert");
            MRPNotifyPrevApp.Show();
        } else if (workline == "2") {
            MRPNotifyMsgPrevApp.SetText("Document already approved by Finance Lead.");
            MRPNotifyPrevApp.SetHeaderText("Alert");
            MRPNotifyPrevApp.Show();
        } else if (workline == "3") {
            MRPNotifyMsgPrevApp.SetText("Document already approved by Executive.");
            MRPNotifyPrevApp.SetHeaderText("Alert");
            MRPNotifyPrevApp.Show();
        }

        //e.processOnServer = false;
    }
}

//mrp_poaddedit.aspx
function PODocNumber_SelectedIndexChanged(s, e) {
    if (PODocNumber.GetText().length > 0) {
        POExpDelivery.SetEnabled(true);
        POVendor.SetEnabled(true);
        POCurrency.SetEnabled(true);
        POSite.SetEnabled(true);
        Location.SetEnabled(true);
        POTerms.SetEnabled(true);
        WareHouse.SetEnabled(true);
        POProCategory.SetEnabled(true);
        POAddEditGrid.PerformCallback();
    }
}

//mrp_pocreatededit.aspx
function itemcode_SelectedIndexChanged(s, e) {

    var qty = s.GetSelectedItem().GetColumnText("Qty").toString();
    var cost = s.GetSelectedItem().GetColumnText("Cost").toString();
    var total = s.GetSelectedItem().GetColumnText("TotalCost").toString();
    var uom = s.GetSelectedItem().GetColumnText("UOM").toString();
    var pk = s.GetSelectedItem().GetColumnText("PK").toString();
    var identifier = s.GetSelectedItem().GetColumnText("Identifier").toString();

    POCreatedQty.SetText(qty);
    POCreatedCost.SetText(cost);
    POCreatedTotal.SetText(total);
    POCreatedUOM.SetText(uom);
    pk_identifier_ingrid.Set('hidden_pk', pk);
    pk_identifier_ingrid.Set('hidden_identifier', identifier);

    localStorage.setItem('AvailPO', qty);
}

function POCreatedQty_KeyUp(s, e) {
    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(POCreatedQty.GetText()).toFixed(2);
    var avail_qty = parseFloat(localStorage.getItem('AvailPO')).toFixed(2);
    var cost = parseFloat(accounting.unformat(POCreatedCost.GetText()));
    var total = 0;
    if (Math.round(POCreatedQty.GetText()) <= Math.round(localStorage.getItem('AvailPO'))) {
        if (qty > 0) {
            if (cost > 0) {
                total = cost * qty;
                POCreatedTotal.SetText(parseFloat(total).toFixed(2));
                POCreatedTotal.SetIsValid(true);
            }
        } else {
            POCreatedTotal.SetText("");
            POCreatedTotal.SetIsValid(false);
        }
    } else {
        s.SetText(avail_qty);
        if (cost > 0) {
            total = cost * avail_qty;
            POCreatedTotal.SetText(parseFloat(total).toFixed(2));
        }
    }
}

function POCreatedCost_KeyUp(s, e) {
    var cost = parseFloat(s.GetText()).toFixed(2);
    var qty = accounting.unformat(POCreatedQty.GetText());
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            POCreatedTotal.SetText(parseFloat(total).toFixed(2));
            POCreatedTotal.SetIsValid(true);
        }
    } else {
        POCreatedTotal.SetText("");
        POCreatedTotal.SetIsValid(false);
    }
}

function POCreatedGrid_CustomButtonClick(s, e) {
    console.log(e.buttonID);
    var button = e.buttonID;
    if (button == "POCreatedGrid_Cancel") {
        POCreatedGridCIN.CancelEdit();
    } else if (button == "POCreatedGrid_UpdateBtn") {
        var itemcode = POCreatedGrid_ItemCode.GetValue();
        var taxgroup = POCreatedGrid_TaxGroup.GetValue();
        var taxitem = POCreatedGrid_TaxItemGroup.GetValue();
        var qty = POCreatedQty.GetValue();
        var cost = POCreatedCost.GetValue();
        var total = POCreatedTotal.GetValue();
        if (itemcode != null && taxgroup != null && taxitem != null && qty != null && cost != null && total != null)
            POCreatedGridCIN.UpdateEdit();

        if (itemcode == null) POCreatedGrid_ItemCode.SetIsValid(false);
        if (taxgroup == null) POCreatedGrid_TaxGroup.SetIsValid(false);
        if (taxitem == null) POCreatedGrid_TaxItemGroup.SetIsValid(false);
        if (qty == null) POCreatedQty.SetIsValid(false);
        if (cost == null) POCreatedCost.SetIsValid(false);
        if (total == null) POCreatedTotal.SetIsValid(false);

    } else if (button == "POCreatedGrid_DeleteBtn") {
        POCreatedGrid_DeletePopup.SetHeaderText("Confirm");
        POCreatedGrid_DeletePopup.Show();

    }
}

function POCreatedGrid_FocusedRowChanged(s, e) {
    s.GetRowValues(s.GetFocusedRowIndex(), 'AvailForPO;Qty', POCreatedGrid_OnGetRowValues);
}

function POCreatedGrid_OnGetRowValues(values) {
    var availpo = values[0];
    var qty = values[1];
    var availpo_updated = Math.abs(Math.round(availpo) - Math.round(qty));
    localStorage.setItem('AvailPO', availpo_updated);
}


//POCREATEDEDIT Vendor
var pocreatededit_postponedCallbackRequiredCurrency = false;
var pocreatededit_postponedCallbackRequiredTerms = false;
function pocreatededit_Vendor_SelectedIndexChanged(s, e) {
    if (pocreatededit_currency_callback.InCallback()) {
        pocreatededit_postponedCallbackRequiredCurrency = true;
        pocreatededit_postponedCallbackRequiredTerms = true;
    }
    else {
        pocreatededit_currency_callback.PerformCallback();
        pocreatededit_terms_callback.PerformCallback();
    }
}

function pocreatededit_currency_EndCallback(s, e) {
    if (pocreatededit_postponedCallbackRequiredCurrency) {
        pocreatededit_currency_callback.PerformCallback();
        pocreatededit_postponedCallbackRequiredCurrency = false;
    }
}

function pocreatededit_terms_EndCallback(s, e) {
    if (pocreatededit_postponedCallbackRequiredTerms) {
        pocreatededit_terms_callback.PerformCallback();
        pocreatededit_postponedCallbackRequiredTerms = false;
    }
}

//Site
var pocreatededit_postponedCallbackRequiredWarehouse = false;
var pocreatededit_postponedCallbackRequiredLocation = false;
function pocreatededit_Site_SelectedIndexChanged(s, e) {
    if (pocreatededit_warehouse_callback.InCallback()) {
        pocreatededit_postponedCallbackRequiredWarehouse = true;
        pocreatededit_postponedCallbackRequiredLocation = true;
    }
    else {
        pocreatededit_warehouse_callback.PerformCallback();
        pocreatededit_location_callback.PerformCallback();
    }
}

function pocreatededit_warehouse_EndCallback(s, e) {
    if (pocreatededit_postponedCallbackRequiredWarehouse) {
        pocreatededit_warehouse_callback.PerformCallback();
        pocreatededit_postponedCallbackRequiredWarehouse = false;
    }
}

function pocreatededit_location_EndCallback(s, e) {
    if (pocreatededit_postponedCallbackRequiredLocation) {
        pocreatededit_location_callback.PerformCallback();
        pocreatededit_postponedCallbackRequiredLocation = false;
    }
}

//Location
function pocreatededit_Warehouse_SelectedIndexChanged(s, e) {
    //Location.SetText("");
    if (pocreatededit_location_callback.InCallback())
        pocreatededit_postponedCallbackRequiredLocation = true;
    else
        pocreatededit_location_callback.PerformCallback();
}

//mrp_addedit.aspx Submit Button
function mrp_addedit_submit(s, e) {
    var wrkflowLine = WorkFlowLineTxt.GetText();
    var statusKey = StatusKeyTxt.GetText();
    var wrklineStatus = WorkFlowLineStatusTxt.GetText();
    if (wrkflowLine === "0") {
        if (statusKey === "1") {
            PopupSubmit.SetHeaderText('Confirm');
            PopupSubmit.Show();
        } else if (statusKey === "2") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted to BU / SSU Lead for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "3") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted for Approval.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        } else if (statusKey === "4") {
            Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already Approved.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    } else {
        if (wrklineStatus === "0") {
            PopupSubmit.SetHeaderText('Confirm');
            PopupSubmit.Show();
        } else {
            Add_Edit_MRPNotificationMessage.SetText("Can't Edit! Document already submitted to Inventory Analyst for review.");
            Add_Edit_MRPNotify.SetHeaderText("Alert");
            Add_Edit_MRPNotify.Show();
        }
    }
}