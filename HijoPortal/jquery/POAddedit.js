function VendorCombo_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    VendorLblClient.SetText(str[1]);
    TermsLblClient.SetText("");
    CurrencyLblClient.SetText("");
    TermsCallbackClient.PerformCallback();
    CurrencyCallbackClient.PerformCallback();
}

function TermsCombo_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    TermsLblClient.SetText(str[1]);
}

function CurrencyCombo_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    CurrencyLblClient.SetText(str[1]);
}

function CurrencyCallback_EndCallback(s, e) {
    var str = CurrencyComboClient.GetText().split(";");
    CurrencyComboClient.SetText(str[0]);
    CurrencyLblClient.SetText(str[1]);
}

function SiteCombo_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    SiteLblClient.SetText(str[1]);
    WarehouseLblClient.SetText("");
    WarehouseCallbackClient.PerformCallback();
}

function WarehouseCombo_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    WarehouseLblClient.SetText(str[1]);
    LocationCallbackClient.PerformCallback();
}

function POAddEditGrid_CustomButtonClick(s, e) {
    var btnID = e.buttonID;
    switch (btnID) {
        case 'Edit':
            s.StartEditRow(e.visibleIndex);
            SaveClient.SetEnabled(false);
            SubmitClient.SetEnabled(false);
            break;
        case 'Delete':
            DeletePopupClient.SetHeaderText("Alert");
            DeletePopupClient.Show();
            break;
        case 'Update':
            var tax_group = TaxGroupClient.GetValue() == null;
            var tax_item_group = TaxItemGroupClient.GetValue() == null;
            var total = TotalPOCostClient.GetValue() == null;
            if (tax_group)
                TaxGroupClient.SetIsValid(false);
            if (tax_item_group)
                TaxItemGroupClient.SetIsValid(false);
            if (total)
                TotalPOCostClient.SetIsValid(false);

            if (!tax_group && !tax_item_group && !total)
                s.UpdateEdit();

            SaveClient.SetEnabled(true);
            SubmitClient.SetEnabled(true);
            break;
        case 'Cancel':
            s.CancelEdit();
            SaveClient.SetEnabled(true);
            SubmitClient.SetEnabled(true);
            break;
    }
}

function POQty_KeyUp(s, e) {
    var avail_qty = accounting.unformat(ReqQtyClient.GetText());
    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(POCostClient.GetText()));
    var total = 0;
    if (parseFloat(s.GetText()) <= parseFloat(avail_qty)) {
        if (qty > 0) {
            if (cost > 0) {
                total = cost * qty;
                TotalPOCostClient.SetIsValid(true);
                TotalPOCostClient.SetText(parseFloat(total).toFixed(2));
            }
        } else {
            TotalPOCostClient.SetIsValid(false);
            TotalPOCostClient.SetText("");
        }
    } else {
        if (cost > 0) {
            s.SetText(avail_qty);
            total = cost * avail_qty;
            TotalPOCostClient.SetIsValid(true);
            TotalPOCostClient.SetText(parseFloat(total).toFixed(2));
        }
    }
}

function POCost_KeyUp(s, e) {
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(accounting.unformat(POQtyClient.GetText()));
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalPOCostClient.SetText(accounting.formatMoney(total));
            TotalPOCostClient.SetIsValid(true);
        }
        else {
            TotalPOCostClient.SetIsValid(false);
            TotalPOCostClient.SetText("");
        }
    } else {
        TotalPOCostClient.SetText("");
        TotalPOCostClient.SetIsValid(false);
    }
}