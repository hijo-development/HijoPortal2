function OnKeyUpQtytInvDirect(s, e) {
    var avail_qty = inv_qty_dm.GetText();

    //var avail_qty = parseFloat(localStorage.getItem('InventAnalystQty')).toFixed(2);
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(accounting.unformat(InvEdittedCost.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCost.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCost.SetText("");
    }
}

function OnKeyUpCosttInvDirect(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(accounting.unformat(InvEdittedQty.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCost.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCost.SetText("");
    }
}



function OnKeyUpQtytInvOpex(s, e) {
    var avail_qty = inv_qty_op.GetText();
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(accounting.unformat(InvEdittedCostOp.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCostOp.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCostOp.SetText("");
    }
}

function OnKeyUpCosttInvOpex(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(accounting.unformat(InvEdittedQtyOp.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCostOp.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCostOp.SetText("");
    }
}

function OnKeyUpQtytInvCapex(s, e) {
    var avail_qty = inv_qty_ca.GetText();
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(accounting.unformat(InvEdittedCostCapex.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCostCapex.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCostCapex.SetText("");
    }
}

function OnKeyUpCosttInvCapex(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(accounting.unformat(InvEdittedQtyCapex.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCostCapex.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCostCapex.SetText("");
    }
}

function OnKeyUpQtytInvManPower(s, e) {
    var avail_qty = inv_qty_man.GetText();
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var qty = parseFloat(accounting.unformat(s.GetText()));
    var cost = parseFloat(accounting.unformat(InvEdittedCostManPo.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCostManPo.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCostManPo.SetText("");
    }
}

function OnKeyUpCosttInvManPower(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(accounting.unformat(InvEdittedQtyManPo.GetText())).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            InvEdittiedTotalCostManPo.SetText(parseFloat(total).toFixed(2));
        }
    } else {
        InvEdittiedTotalCostManPo.SetText("");
    }
}

