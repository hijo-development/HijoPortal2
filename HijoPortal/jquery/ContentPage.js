$(document).ready(function () {

    changeWidth.resizeWidth();
});

$(window).resize(function () {
    changeWidth.resizeWidth();
});

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

        //var contentHeight = 600;
        //$('#MRPPanel').width(mrpwidth);
        $('#MasterPanel').width(mrpwidth);
        $('#AddFormPanel').width(mrpwidth);
        $('#PanelLeft').width(leftPanel);

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

        //$('#dvContentWrapper').height(contentHeightInside);

        //console.log("Center Height: " + centerPanelHeight + " form Height: " + formHeight + ":::: " + h1);

        //console.log("Body Height: " + h);
        //console.log("Header Height: " + $('#dvBanner').height());
        //console.log("Footer Height: " + $('#footer').height());
        //console.log("Content Height: " + contentHeight);
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
    document.getElementById("mySidenav").style.width = "350px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}


// MasterMRp
function CustomButtonClick(s, e) {
    console.log("custom button click" + e.buttonID);
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


// MRPAddForm Script
function updateDirectMat(s, e) {
    var actCode = ActivityCodeDirect.GetText();
    var itemCode = ItemCodeDirect.GetText();
    var itemDesc = ItemDescriptionDirect.GetText();
    var uom = UOMDirect.GetText();
    var cost = CostDirect.GetText();
    var qty = QtyDirect.GetText();
    var totalcost = TotalCostDirect.GetText();

    if (actCode.length > 0 && itemCode.length > 0 && itemDesc.length > 0 && uom.length > 0 && cost.length > 0 && qty.length > 0 && totalcost.length > 0) {
        DirectMaterialsGrid.UpdateEdit();
    }
}
function updateOpex(s, e) {
    var expense = ExpenseCodeOPEX.GetText();
    var itemCode = ItemCodeOPEX.GetText();
    var itemDesc = DescriptionOPEX.GetText();
    var uom = UOMOPEX.GetText();
    var cost = CostOPEX.GetText();
    var qty = QtyOPEX.GetText();
    var totalcost = TotalCostOPEX.GetText();

    if (expense.length > 0 && itemCode.length > 0 && itemDesc.length > 0 && uom.length > 0 && cost.length > 0 && qty.length > 0 && totalcost.length > 0) {
        OPEXGrid.UpdateEdit();
    }
}

function updateManpower(s, e) {
    var activity = ActivityCodeMAN.GetText();
    var type = ManPowerTypeKeyNameMAN.GetText();
    var itemDesc = DescriptionMAN.GetText();
    var uom = UOMMAN.GetText();
    var cost = CostMAN.GetText();
    var qty = QtyMAN.GetText();
    var totalcost = TotalCostMAN.GetText();

    if (activity.length > 0 && type.length > 0 && itemDesc.length > 0 && uom.length > 0 && cost.length > 0 && qty.length > 0 && totalcost.length > 0) {
        ManPowerGrid.UpdateEdit();
    }
}

function updateCAPEX(s, e) {
    var itemDesc = DescriptionCAPEX.GetText();
    var uom = UOMCAPEX.GetText();
    var cost = CostCAPEX.GetText();
    var qty = QtyCAPEX.GetText();
    var totalcost = TotalCostCAPEX.GetText();

    if (itemDesc.length > 0 && uom.length > 0 && cost.length > 0 && qty.length > 0 && totalcost.length > 0) {
        CAPEXGrid.UpdateEdit();
    }
}

function updateRevenue(s, e) {
    var product = ProductNameRev.GetText();
    var farm = FarmNameRev.GetText();
    var prize = PrizeRev.GetText();
    var volume = VolumeRev.GetText();
    var totalprize = TotalPrizeRev.GetText();

    if (product.length > 0 && farm.length > 0 && prize.length > 0 && volume.length > 0 && totalprize.length > 0) {
        RevenueGrid.UpdateEdit();
    }
}
function FilterDigit(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    console.log(key);
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

function OnKeyUpCostDirect(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(QtyDirect.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostDirect.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        TotalCostDirect.SetText("");
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
            TotalCostOPEX.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        TotalCostOPEX.SetText("");
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
            TotalCostMAN.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        TotalCostMAN.SetText("");
    }
} function OnKeyUpCostCapex(s, e) {//OnChange
    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(QtyCAPEX.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostCAPEX.SetText(parseFloat(accounting.formatMoney(total)));
        }
    } else {
        TotalCostCAPEX.SetText("");
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
            TotalCostDirect.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        TotalCostDirect.SetText("");
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
        }
    } else {
        TotalCostOPEX.SetText("");
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
        }
    } else {
        TotalCostMAN.SetText("");
    }
}
function OnKeyUpQtyCapex(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(CostCAPEX.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostCAPEX.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        TotalCostCAPEX.SetText("");
    }
}
//MRP Items
function ActivityCodeIndexChange(s, e) {
    //var selectedString = s.GetSelectedItem().text;
    //var indexDash = selectedString.indexOf('-');
    //var codeString = selectedString.substring(0, indexDash + 1);
    //var temp = selectedString.substring(indexDash + 1, selectedString.length);
    //var secondString = temp.substring(0, temp.indexOf('-'));

    //var selValue = s.GetSelectedItem().value;
    //var selText = s.GetSelectedItem().text;
    //ActivityCodeDirect.SetText((codeString + secondString).toString());
}

function ExpenseCodeIndexChangeOPEX(s, e) {
    //var selectedString = s.GetSelectedItem().text;
    //var indexDash = selectedString.indexOf('-');
    //var codeString = selectedString.substring(0, indexDash);
    //console.log(codeString);
    //ExpenseCodeOPEX.SetText(codeString);
}
function ActivityCodeIndexChangeMAN(s, e) {
    var selectedString = s.GetSelectedItem().text;
    var indexDash = selectedString.indexOf('-');
    var codeString = selectedString.substring(0, indexDash + 1);
    var temp = selectedString.substring(indexDash + 1, selectedString.length);
    var secondString = temp.substring(0, temp.indexOf('-'));
    ActivityCodeMAN.SetText((codeString + secondString).toString());
}

function EntityCodeIndexChange(s, e) {
    SetComboBoxEntityID(s);
    var selectedString = s.GetSelectedItem().text;
    EntityCodeDirect.SetText(selectedString.toString());
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

//Direct Materials
function listbox_selected(s, e) {
    var selValue = s.GetSelectedItem().value;
    var selText = s.GetSelectedItem().text;
    ItemCodeDirect.SetText(selValue);
    ItemDescriptionDirect.SetText(selText);
    listbox.SetVisible(false);
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
function focused(s, e, type) {
    var pk = s.GetRowKey(e.visibleIndex);;
    params = type + "-" + pk;

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


//FOR PO ADD/EDIT GRID SELECTION CHANGE(CHECKBOX CONTROL)

// POCreation
function POCustomButtonClick(s, e) {
    console.log("custom button click" + e.buttonID);
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

//Gridview PO
function OnKeyUpQtyPO(s, e) {//OnChange
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(POCost.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            POTotalCost.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        POTotalCost.SetText("");
    }
}

function OnKeyUpCostPO(s, e) {//OnChange
    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(POQty.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            POTotalCost.SetText(accounting.formatMoney(total));
        }
    } else {
        POTotalCost.SetText("");
    }
}



//END OF ADD FORM SCRIPT HERE.....