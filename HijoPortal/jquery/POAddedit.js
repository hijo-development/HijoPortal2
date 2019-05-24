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
            var po_uom = POUOMClient.GetValue() == null;
            var tax_group = TaxGroupClient.GetValue() == null;
            var tax_item_group = TaxItemGroupClient.GetValue() == null;
            var total = TotalPOCostClient.GetValue() == null;
            if (po_uom)
                POUOMClient.SetIsValid(false);
            if (tax_group)
                TaxGroupClient.SetIsValid(false);
            if (tax_item_group)
                TaxItemGroupClient.SetIsValid(false);
            if (total)
                TotalPOCostClient.SetIsValid(false);

            if (!po_uom && !tax_group && !tax_item_group && !total)
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

function POAddEditGrid_EndCallback(s, e) {
    var rowCount = POAddEditGridClient.GetVisibleRowsOnPage();
    //if (rowCount == 0) {
    //    SubmitClient.SetEnabled(false);
    //} else {
    //    SubmitClient.SetEnabled(true);
    //}
}

function DeleteItem(s, e) {
    DeletePopupClient.Hide();
    
    var index = POAddEditGridClient.GetFocusedRowIndex();
    console.log(index);
    POAddEditGridClient.DeleteRow(index);
    
    //OPEXGrid.DeleteRow(OPEXGrid.GetFocusedRowIndex());
    
}

function POQty_KeyUp(s, e) {
    //var avail_qty = accounting.unformat(ReqQtyClient.GetText());
    ////var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    //var qty = parseFloat(s.GetText()).toFixed(2);
    //var cost = parseFloat(accounting.unformat(POCostClient.GetText()));
    //var total = 0;
    //if (parseFloat(s.GetText()) <= parseFloat(avail_qty)) {
    //    if (qty > 0) {
    //        if (cost > 0) {
    //            total = cost * qty;
    //            TotalPOCostClient.SetIsValid(true);
    //            TotalPOCostClient.SetText(parseFloat(total).toFixed(2));
    //        }
    //    } else {
    //        TotalPOCostClient.SetIsValid(false);
    //        TotalPOCostClient.SetText("");
    //    }
    //} else {
    //    if (cost > 0) {
    //        s.SetText(avail_qty);
    //        total = cost * avail_qty;
    //        TotalPOCostClient.SetIsValid(true);
    //        TotalPOCostClient.SetText(parseFloat(total).toFixed(2));
    //    }
    //}
    var avail_qty = accounting.unformat(ReqQtyClient.GetText());
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(POCostClient.GetText()));
    var costwVAT = parseFloat(accounting.unformat(POCostwVATClient.GetText()));
    var total = 0;
    var totalwVAT = 0;
    if (parseFloat(s.GetText()) <= parseFloat(avail_qty)) {
        if (qty > 0) {
            if (cost > 0) {
                total = cost * qty;
                TotalPOCostClient.SetIsValid(true);
                TotalPOCostClient.SetText(parseFloat(total).toFixed(2));
            }
            if (costwVAT > 0) {
                totalwVAT = costwVAT * qty;
                TotalPOCostwVATClient.SetIsValid(true);
                TotalPOCostwVATClient.SetText(parseFloat(totalwVAT).toFixed(2));
            }
        } else {
            TotalPOCostClient.SetIsValid(false);
            TotalPOCostClient.SetText("");
            TotalPOCostwVATClient.SetIsValid(false);
            TotalPOCostwVATClient.SetText("");
        }
    } else {
        if (cost > 0) {
            s.SetText(avail_qty);
            total = cost * avail_qty;
            TotalPOCostClient.SetIsValid(true);
            TotalPOCostClient.SetText(parseFloat(total).toFixed(2));
        }
        if (costwVAT > 0) {
            totalwVAT = costwVAT * qty;
            TotalPOCostwVATClient.SetIsValid(true);
            TotalPOCostwVATClient.SetText(parseFloat(totalwVAT).toFixed(2));
        }
    }
}

function POCost_KeyUp(s, e) {
    //var cost = parseFloat(accounting.unformat(s.GetText()));
    //var qty = parseFloat(accounting.unformat(POQtyClient.GetText()));
    //var total = 0;
    //if (qty > 0) {
    //    if (cost > 0) {
    //        total = cost * qty;
    //        TotalPOCostClient.SetText(accounting.formatMoney(total));
    //        TotalPOCostClient.SetIsValid(true);
    //    }
    //    else {
    //        TotalPOCostClient.SetIsValid(false);
    //        TotalPOCostClient.SetText("");
    //    }
    //} else {
    //    TotalPOCostClient.SetText("");
    //    TotalPOCostClient.SetIsValid(false);
    //}
    var checkVal = CheckwVATClient.GetValue();
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var costwVAT = 0;
    var qty = parseFloat(POQtyClient.GetText()).toFixed(2);
    //var VAT = 1.12;
    var VAT = txtVATClient.GetText(); // 1.12;
    var total = 0;
    var totalwVAT = 0;

    if (checkVal == true) {
        costwVAT = cost * VAT;
        POCostwVATClient.SetText(accounting.formatMoney(costwVAT));
    }
    if (checkVal == false) {
        costwVAT = cost;
        POCostwVATClient.SetText(accounting.formatMoney(costwVAT));
    }

    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
        }
        if (costwVAT > 0) {
            totalwVAT = costwVAT * qty;
        }
    }

    TotalPOCostClient.SetText(accounting.formatMoney(total));
    TotalPOCostwVATClient.SetText(accounting.formatMoney(totalwVAT));
}

function POCheck_Changed(s, e) {
    var checkVal = s.GetValue();
    var cost = parseFloat(accounting.unformat(POCostClient.GetText()));
    var qty = parseFloat(POQtyClient.GetText()).toFixed(2);
    var VAT = txtVATClient.GetText(); // 1.12;
    var costwVAT = 0;
    var totalwVAT = 0;
    //console.log(checkVal);
    if (checkVal == true) {
        if (cost > 0) {
            costwVAT = cost * VAT;
            POCostwVATClient.SetText(accounting.formatMoney(costwVAT));
        } else {
            costwVAT = cost;
            POCostwVATClient.SetText("");
        }
    }
    if (checkVal == false) {
        if (cost > 0) {
            costwVAT = cost;
            POCostwVATClient.SetText(accounting.formatMoney(costwVAT));
        } else {
            costwVAT = cost;
            POCostwVATClient.SetText("");
        }
    }

    totalwVAT = costwVAT * qty;
    TotalPOCostwVATClient.SetText(accounting.formatMoney(totalwVAT));
}

function POAddEditSubmitBtn_Click(s, e) {
    var POStatus = txtStatus.GetText();
    if (POStatus === "1") {
        POAddEdit_MRPNotify.SetHeaderText("Alert");
        POAddEdit_MRPNotify.Show();
    } else {
        POAddEditPopupSubmit.SetHeaderText("Confirm");
        POAddEditPopupSubmit.Show();
    }
}