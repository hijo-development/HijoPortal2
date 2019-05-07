function Checkbox_CheckedChanged(s, e) {
    var getchecked = s.GetChecked();
    MonthYearComboClient.SetEnabled(getchecked);
    if (getchecked) {
        var monthValLength = MonthYearComboClient.GetText().length;
        if (monthValLength > 0)
            BtnAddClient.SetEnabled(true);
        else
            BtnAddClient.SetEnabled(false);
    } else {
        MonthYearComboClient.SetValue("");
        MonthYearComboClient.SetText("");
        if (MonthClient.GetText().length > 0 && YearClient.GetText().length > 0) {
            BtnAddClient.SetEnabled(true);
        }
    }
}

function MonthYearCombo_SelectedIndexChanged(s, e) {
    BtnAddClient.SetEnabled(false);
    if (s.GetText().length > 0 && MonthClient.GetText().length > 0 && YearClient.GetText().length > 0)
        BtnAddClient.SetEnabled(true);
}

function Month_SelectedIndexChanged(s, e) {
    if (CheckboxClient.GetChecked()) {
        if (MonthYearComboClient.GetText().length > 0 && YearClient.GetText().length > 0)
            BtnAddClient.SetEnabled(true);
        else
            BtnAddClient.SetEnabled(false);
    } else {
        if (YearClient.GetText().length > 0)
            BtnAddClient.SetEnabled(true);
        else
            BtnAddClient.SetEnabled(false);
    }
}

function Year_SelectedIndexChanged(s, e) {
    if (CheckboxClient.GetChecked()) {
        if (MonthYearComboClient.GetText().length > 0 && MonthClient.GetText().length > 0)
            BtnAddClient.SetEnabled(true);
        else
            BtnAddClient.SetEnabled(false);
    } else {
        if (MonthClient.GetText().length > 0)
            BtnAddClient.SetEnabled(true);
        else
            BtnAddClient.SetEnabled(false);
    }
}

function AddNewMOP(s, e) {
    PopUpControl.Hide();
    $find('ModalPopupExtenderLoading').show();
    e.processOnServer = true;
}