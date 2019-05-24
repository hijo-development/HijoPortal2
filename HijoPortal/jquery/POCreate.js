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
            SavePO.SetEnabled(false);
            break;
        case 'Update':
            //var tax_group = TaxGroupClient.GetValue() == null;
            //var tax_item_group = TaxItemGroupClient.GetValue() == null;
            var total = POTotalCost.GetValue() == null;
            var totalwVAT = TotalPOCostwVAT.GetValue() == null;
            //if (tax_group)
            //    TaxGroupClient.SetIsValid(false);
            //if (tax_item_group)
            //    TaxItemGroupClient.SetIsValid(false);
            if (total)
                POTotalCost.SetIsValid(false);

            if (totalwVAT)
                TotalPOCostwVAT.SetIsValid(false);

            if (!total && !totalwVAT)
                s.UpdateEdit();

            SavePO.SetEnabled(true);
            break;
        case 'Cancel':
            s.CancelEdit();
            SavePO.SetEnabled(true);
            break;
    }
}

function POCheck_Changed(s, e) {
    var checkVal = s.GetValue();
    var cost = parseFloat(accounting.unformat(POCost.GetText()));
    var qty = parseFloat(POQty.GetText()).toFixed(2);
    //var VAT = 1.12;
    var VAT = txtVATClient.GetText(); // 1.12;
    var costwVAT = 0;
    var totalwVAT = 0;
    //console.log(checkVal);
    if (checkVal == true) {
        if (cost > 0) {
            costwVAT = cost * VAT;
            POCostwVAT.SetText(accounting.formatMoney(costwVAT));
        } else {
            costwVAT = cost;
            POCostwVAT.SetText("");
        }
    }
    if (checkVal == false) {
        if (cost > 0) {
            costwVAT = cost;
            POCostwVAT.SetText(accounting.formatMoney(costwVAT));
        } else {
            costwVAT = cost;
            POCostwVAT.SetText("");
        }
    }

    totalwVAT = costwVAT * qty;
    TotalPOCostwVAT.SetText(accounting.formatMoney(totalwVAT));
}

//function POCostwVAT_Changed(s, e) {
//    var costwVAT = parseFloat(accounting.unformat(s.GetText()));
//    var qty = parseFloat(POQty.GetText()).toFixed(2);
//    var totalwVAT = 0;
//    //console.log("pass");
//    if (qty > 0) {
//        if (costwVAT > 0) {
//            totalwVAT = costwVAT * qty;
//            TotalPOCostwVAT.SetText(accounting.formatMoney(totalwVAT));
//        }
//    } else {
//        TotalPOCostwVAT.SetText("");
//    }
//}

function POCost_KeyUp(s, e) {//OnChange
    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var checkVal = CheckwVAT.GetValue();
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var costwVAT = 0;
    var qty = parseFloat(POQty.GetText()).toFixed(2);
    //var VAT = 1.12;
    var VAT = txtVATClient.GetText(); // 1.12;
    var total = 0;
    var totalwVAT = 0;

    if (checkVal == true) {
        costwVAT = cost * VAT;
        POCostwVAT.SetText(accounting.formatMoney(costwVAT));
    }
    if (checkVal == false) {
        costwVAT = cost;
        POCostwVAT.SetText(accounting.formatMoney(costwVAT));
    }

    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
        }
        if (costwVAT > 0) {
            totalwVAT = costwVAT * qty;
        }
    } 

    POTotalCost.SetText(accounting.formatMoney(total));
    TotalPOCostwVAT.SetText(accounting.formatMoney(totalwVAT));
}

//function POCostwVAT_KeyUp(s, e) {//OnChange
//    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
//    var cost = parseFloat(accounting.unformat(s.GetText()));
//    var qty = parseFloat(POQty.GetText()).toFixed(2);
//    var total = 0;
//    if (qty > 0) {
//        if (cost > 0) {
//            total = cost * qty;
//            TotalPOCostwVAT.SetText(accounting.formatMoney(total));
//        }
//    } else {
//        TotalPOCostwVAT.SetText("");
//    }
//}

function POQty_KeyUp(s, e) {//OnChange
    var avail_qty = accounting.unformat(ReqQtyClient.GetText());
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(s.GetText()).toFixed(2);
    var cost = parseFloat(accounting.unformat(POCost.GetText()));
    var costwVAT = parseFloat(accounting.unformat(POCostwVAT.GetText()));
    var total = 0;
    var totalwVAT = 0;
    if (parseFloat(s.GetText()) <= parseFloat(avail_qty)) {
        if (qty > 0) {
            if (cost > 0) {
                total = cost * qty;
                POTotalCost.SetIsValid(true);
                POTotalCost.SetText(parseFloat(total).toFixed(2));
            }
            if (costwVAT > 0) {
                totalwVAT = costwVAT * qty;
                TotalPOCostwVAT.SetIsValid(true);
                TotalPOCostwVAT.SetText(parseFloat(totalwVAT).toFixed(2));
            }
        } else {
            POTotalCost.SetIsValid(false);
            POTotalCost.SetText("");
            TotalPOCostwVAT.SetIsValid(false);
            TotalPOCostwVAT.SetText("");
        }
    } else {
        if (cost > 0) {
            s.SetText(avail_qty);
            total = cost * avail_qty;
            POTotalCost.SetIsValid(true);
            POTotalCost.SetText(parseFloat(total).toFixed(2));
        }
        if (costwVAT > 0) {
            totalwVAT = costwVAT * qty;
            TotalPOCostwVAT.SetIsValid(true);
            TotalPOCostwVAT.SetText(parseFloat(totalwVAT).toFixed(2));
        }
    }

}

function Save_Click(s, e) {
    var execute = VendorClient.GetIsValid() && SiteClient.GetIsValid() && ExpDelClient.GetIsValid() && TermsClient.GetIsValid() && WarehousePO.GetIsValid() && CurrencyClient.GetIsValid() && LocationClient.GetIsValid() && RemarksClient.GetIsValid();
    if (execute) {
        $find('ModalPopupExtenderLoading').show();
        e.processOnServer = true;
    }
}