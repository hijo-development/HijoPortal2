function updateCAPEX(s, e) {
    var entityval = entityhiddenCA.Get('hidden_value');
    var bool = true;
    if (entityval == "display") {
        if (OperatingUnitCA.GetText().length == 0) {
            OperatingUnitCA.SetIsValid(false);
            bool = false;
        } else {
            OperatingUnitCA.SetIsValid(true);
            bool = true;
        }
    }

    var prodcat = ProdCatCAPEX.GetText();
    var itemDesc = DescriptionCAPEX.GetText();
    var uom = UOMCAPEX.GetText();
    var cost = CostCAPEX.GetText();
    var qty = QtyCAPEX.GetText();
    var totalcost = TotalCostCAPEX.GetText();

    if (prodcat.length > 0 && itemDesc.length > 0 && uom.length > 0 && cost.length > 0 && qty.length > 0 && totalcost.length > 0 && bool) {
        CAPEXGrid.UpdateEdit();
    }
}

function OperatingUnitCA(s, e) {
    var text = s.GetSelectedItem().text;
    if (text.length == 0)
        s.SetIsValid(false);
    else
        s.SetIsValid(true);
}

function OnKeyUpCostCapex(s, e) {//OnChange
    //var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    var cost = parseFloat(accounting.unformat(s.GetText()));
    var qty = parseFloat(QtyCAPEX.GetText()).toFixed(2);
    var total = 0;
    if (qty > 0) {
        if (cost > 0) {
            total = cost * qty;
            TotalCostCAPEX.SetText(accounting.formatMoney(total));
            TotalCostCAPEX.SetIsValid(true);
        }
    } else {
        TotalCostCAPEX.SetText("");
        TotalCostCAPEX.SetIsValid(false);
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
            TotalCostCAPEX.SetIsValid(true);
        }
    } else {
        TotalCostCAPEX.SetText("");
        TotalCostCAPEX.SetIsValid(false);
    }
}

