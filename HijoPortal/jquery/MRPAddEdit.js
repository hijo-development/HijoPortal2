$(document).ready(function () {
    AddEditDisplay.cookiesCondition();
});

AddEditDisplay = {
    cookiesCondition: function () {
        if (OPEXGrid.IsNewRowEditing()) {
            var isItem = getCookie("opisItem");
            var isProdCat = getCookie("opisProdCat");

            console.log(isItem + "-" + isProdCat);

            switch (isItem) {
                case 0://Non PO
                    document.getElementById("div1").style.display = "none";
                    document.getElementById("div2").style.display = "none";
                    DescriptionOPEX.SetText("");
                    DescriptionOPEX.GetInputElement().readOnly = false;
                    ItemCodeOPEX.SetText("");
                    break;
                case 1://PO
                    document.getElementById("div1").style.display = "block";
                    document.getElementById("div2").style.display = "block";
                    DescriptionOPEX.SetText("");
                    DescriptionOPEX.GetInputElement().readOnly = true;
                    ItemCodeOPEX.SetText("");
                    break;
            }

            switch (isProdCat) {
                case 0://hide product category combobox
                    document.getElementById("CA_prodcombo_div").style.display = "none";
                    document.getElementById("CA_prodlabel_div").style.display = "none";
                    break;
                case 1://show product category combobox
                    document.getElementById("CA_prodcombo_div").style.display = "block";
                    document.getElementById("CA_prodlabel_div").style.display = "block";
                    break;
            }
        }
    }
}

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

function ActivityCodeIndexChange(s, e) {
    //var now = getNow();
    //document.cookie = 'dmvalue=' + s.GetValue() + '; expires=' + now.toUTCString() + '; path=/';
    //document.cookie = 'dmtext=' + s.GetText() + '; expires=' + now.toUTCString() + '; path=/';

    document.cookie = 'dmvalue=' + s.GetValue();
    document.cookie = 'dmtext=' + s.GetText();
}

function ActivityCodeIndexChangeMAN(s, e) {
    //var now = getNow();
    document.cookie = 'manvalue=' + s.GetValue();
    document.cookie = 'mantext=' + s.GetText();
}

function getNow() {
    var now = new Date();
    var time = now.getTime();
    time += 3600 * 1000;
    now.setTime(time);
    return now;
}

//this function for debugging
function getCookie(name) {
    var re = new RegExp(name + "=([^;]+)");
    var value = re.exec(document.cookie);
    return (value != null) ? unescape(value[1]) : null;
}

//Operating Expenditure
var isItem = 1;
var isProdCat = 1;
var postponedCallbackOPEXProCat = false;
function ExpenseCodeIndexChangeOPEX(s, e) {
    isItem = parseInt(s.GetSelectedItem().GetColumnText('isItem'));
    isProdCat = parseInt(s.GetSelectedItem().GetColumnText('isProdCategory'));
    console.log(isItem + " .... " + isProdCat);

    //Save cookies
    document.cookie = 'opvalue=' + s.GetValue();
    document.cookie = 'optext=' + s.GetText();
    document.cookie = 'opisItem=' + isItem;
    document.cookie = 'opisProdCat=' + isProdCat;

    switch (isItem) {
        case 0://Non PO
            document.getElementById("div1").style.display = "none";
            document.getElementById("div2").style.display = "none";
            DescriptionOPEX.SetText("");
            DescriptionOPEX.GetInputElement().readOnly = false;
            ItemCodeOPEX.SetText("");
            break;
        case 1://PO
            document.getElementById("div1").style.display = "block";
            document.getElementById("div2").style.display = "block";
            DescriptionOPEX.SetText("");
            DescriptionOPEX.GetInputElement().readOnly = true;
            ItemCodeOPEX.SetText("");
            break;
    }

    switch (isProdCat) {
        case 0://hide product category combobox
            document.getElementById("CA_prodcombo_div").style.display = "none";
            document.getElementById("CA_prodlabel_div").style.display = "none";
            break;
        case 1://show product category combobox
            document.getElementById("CA_prodcombo_div").style.display = "block";
            document.getElementById("CA_prodlabel_div").style.display = "block";
            break;
    }

    if (ProcCatOPEXCallbackClient.InCallback()) {
        postponedCallbackOPEXProCat = true;
    }
    else {
        ProcCatOPEXCallbackClient.PerformCallback();
    }
}

function ProcCatOPEX_SelectedIndexChanged(s, e) {
    document.cookie = 'opproductvalue=' + s.GetValue();
    document.cookie = 'opproducttext=' + s.GetText();
}

function ProdCat_SelectedIndexChanged(s, e) {
    document.cookie = 'caproductvalue=' + s.GetValue();
    document.cookie = 'caproducttext=' + s.GetText();
}

//function pageinit(s, e) {
//    if (isItemBeginCallback == 0) {
//        document.getElementById("div1").style.display = "none";
//        document.getElementById("div2").style.display = "none";
//        ItemCodeOPEX.SetText("");
//    }

//    if (OPEXGrid.IsNewRowEditing()) {

//        var isItem = getCookie("opisItem");
//        var isProdCat = getCookie("opisProdCat");

//        console.log(isItem + "-" + isProdCat);

//        switch (isItem) {
//            case 0://Non PO
//                document.getElementById("div1").style.display = "none";
//                document.getElementById("div2").style.display = "none";
//                DescriptionOPEX.SetText("");
//                DescriptionOPEX.GetInputElement().readOnly = false;
//                ItemCodeOPEX.SetText("");
//                break;
//            case 1://PO
//                document.getElementById("div1").style.display = "block";
//                document.getElementById("div2").style.display = "block";
//                DescriptionOPEX.SetText("");
//                DescriptionOPEX.GetInputElement().readOnly = true;
//                ItemCodeOPEX.SetText("");
//                break;
//        }

//        //switch (isProdCat) {
//        //    case 0://hide product category combobox
//        //        document.getElementById("CA_prodcombo_div").style.display = "none";
//        //        document.getElementById("CA_prodlabel_div").style.display = "none";
//        //        break;
//        //    case 1://show product category combobox
//        //        document.getElementById("CA_prodcombo_div").style.display = "block";
//        //        document.getElementById("CA_prodlabel_div").style.display = "block";
//        //        break;
//        //}
//    }
//}

//Operating Unit in TRAIN Entity
function OperatingUnitDM_SelectedIndexChanged(s, e) {
    document.cookie = 'dm_operating_value=' + s.GetValue();
    document.cookie = 'dm_operating_text=' + s.GetText();

    var text = s.GetSelectedItem().text;
    if (text.length == 0)
        s.SetIsValid(false);
    else
        s.SetIsValid(true);
}

function OperatingUnitOP_SelectedIndexChanged(s, e) {
    document.cookie = 'op_operating_value=' + s.GetValue();
    document.cookie = 'op_operating_text=' + s.GetText();

    var text = s.GetSelectedItem().text;
    if (text.length == 0)
        s.SetIsValid(false);
    else
        s.SetIsValid(true);
}

function OperatingUnitMAN_SelectedIndexChanged(s, e) {
    document.cookie = 'man_operating_value=' + s.GetValue();
    document.cookie = 'man_operating_text=' + s.GetText();

    var text = s.GetSelectedItem().text;
    if (text.length == 0)
        s.SetIsValid(false);
    else
        s.SetIsValid(true);
}

function OperatingUnitCA_SelectedIndexChanged(s, e) {
    document.cookie = 'ca_operating_value=' + s.GetValue();
    document.cookie = 'ca_operating_text=' + s.GetText();

    var text = s.GetSelectedItem().text;
    if (text.length == 0)
        s.SetIsValid(false);
    else
        s.SetIsValid(true);
}

function OperatingUnitREV_SelectedIndexChanged(s, e) {
    document.cookie = 'rev_operating_value=' + s.GetValue();
    document.cookie = 'rev_operating_text=' + s.GetText();

    var text = s.GetSelectedItem().text;
    if (text.length == 0)
        s.SetIsValid(false);
    else
        s.SetIsValid(true);
}

//END CALLBACK
//function ProcCatOPEX_EndCallback(s, e) {
//    if (OPEXGrid.IsNewRowEditing()) {
//        var isProdCat = getCookie("opisProdCat");
//        switch (isProdCat) {
//            case 0://hide product category combobox
//                document.getElementById("CA_prodcombo_div").style.display = "none";
//                document.getElementById("CA_prodlabel_div").style.display = "none";
//                break;
//            case 1://show product category combobox
//                document.getElementById("CA_prodcombo_div").style.display = "block";
//                document.getElementById("CA_prodlabel_div").style.display = "block";
//                break;
//        }
//    }
//}

