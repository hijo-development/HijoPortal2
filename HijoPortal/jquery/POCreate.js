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
    var btnID = e.buttonID;
    switch (btnID) {
        case 'Edit':
            s.StartEditRow(e.visibleIndex);
            break;
        case 'Update':
            var tax_group = TaxGroupClient.GetValue() == null;
            var tax_item_group = TaxItemGroupClient.GetValue() == null;
            var total = POTotalCost.GetValue() == null;
            if (tax_group)
                TaxGroupClient.SetIsValid(false);
            if (tax_item_group)
                TaxItemGroupClient.SetIsValid(false);
            if (total)
                POTotalCost.SetIsValid(false);

            if (!tax_group && !tax_item_group && !total)
                s.UpdateEdit();
            break;
        case 'Cancel':
            s.CancelEdit();
            break;
    }
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
    var avail_qty = accounting.unformat(ReqQtyClient.GetText());
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(POCost.GetText()));
    var total = 0;
    if (parseFloat(s.GetText()) <= parseFloat(avail_qty)) {
        if (qty > 0) {
            if (cost > 0) {
                total = cost * qty;
                POTotalCost.SetIsValid(true);
                POTotalCost.SetText(parseFloat(total).toFixed(2));
            }
        } else {
            POTotalCost.SetIsValid(false);
            POTotalCost.SetText("");
        }
    } else {
        if (cost > 0) {
            s.SetText(avail_qty);
            total = cost * avail_qty;
            POTotalCost.SetIsValid(true);
            POTotalCost.SetText(parseFloat(total).toFixed(2));
        }
    }

}