function VendorPO_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    VendorLblClient.SetText(str[1]);
    TermsCallbackPO.PerformCallback();
    CurrencyCallbackPO.PerformCallback();

}

function TermsPO_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    TermsLblClient.SetText(str[1]);
}

function CurrencyPO_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    CurrencyLblClient.SetText(str[1]);
}

function CurrencyCallback_EndCallback(s, e) {
    var str = CurrencyClient.GetText().split(";");
    CurrencyClient.SetText(str[0]);
    CurrencyLblClient.SetText(str[1]);
}

function SitePO_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    SiteLblClient.SetText(str[1]);
    WarehouseCallbackPO.PerformCallback();
}

function WarehousePO_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    WarehouseLblClient.SetText(str[1]);
    LocationCallbackPO.PerformCallback();
}

function LocationPO_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    LocationLblClient.SetText(str[1]);
}

function POCreateGrid_CustomButtonClick(s, e) {

}

function POCost_KeyUp(s, e) {//OnChange
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

function POQty_KeyUp(s, e) {//OnChange
    var avail_qty = ReqQtyClient.GetText();
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(POCost.GetText()));
    var total = 0;
    if (Math.round(s.GetText()) <= Math.round(avail_qty)) {
        if (qty > 0) {
            if (cost > 0) {
                total = cost * qty;
                POTotalCost.SetText(parseFloat(total).toFixed(2));
            }
        } else {
            POTotalCost.SetText("");
        }
    } else {
        if (cost > 0) {
            s.SetText(avail_qty);
            total = cost * avail_qty;
            POTotalCost.SetText(parseFloat(total).toFixed(2));
        }
    }

}