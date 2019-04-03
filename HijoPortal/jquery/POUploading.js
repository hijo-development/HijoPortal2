function SaveChanges(s, e) {
    POGridClient.CancelEdit();
}

//INFO GRID
function Code_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    EntityClient.SetText(str[1]);
}

function InfoGrid_CustomButtonClick(s, e) {
    var btnID = e.buttonID;
    switch (btnID) {
        case 'Update':
            var code = CodeClient.GetValue();
            var prefix = PrefixClient.GetValue();
            var series = BeforeSeriesClient.GetValue();
            var max = MaxNumberClient.GetValue();
            var last = LastNumberClient.GetValue();
            var exec = false;

            if (code == null) CodeClient.SetIsValid(false); else CodeClient.SetIsValid(true);
            if (prefix == null) PrefixClient.SetIsValid(false); else PrefixClient.SetIsValid(true);
            if (series == null) BeforeSeriesClient.SetIsValid(false); else BeforeSeriesClient.SetIsValid(true);
            if (max == null) MaxNumberClient.SetIsValid(false); else MaxNumberClient.SetIsValid(true);
            if (last == null) LastNumberClient.SetIsValid(false); else LastNumberClient.SetIsValid(true);

            if (code != null && prefix != null && series != null && max != null && last != null) {
                exec = true;
            }

            if (exec) {
                s.GetPageRowValues('Code', OnGetCellValues);
                //s.UpdateEdit();
            }
            break;
        case 'Cancel':
            s.CancelEdit();
            break;
        case 'Delete':
            DeletePopUpClient.SetHeaderText("Alert");
            DeletePopUpClient.Show();
            break;
    }
}

function OnGetCellValues(values) {
    var code = values;
    console.log(code);

}

function InfoGrid_EndCallback(s, e) {
   
}

function FilterDigit_NumberOnly_KeyPress(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    //KEY (TAB) keycode: 0
    //KEY (0 to 9) keycode: 48-57
    //Key (DEL)    keycode: 8
    if ((key >= 48 && key <= 57) || key == 8 || key == 0) {
        return true;
    } else {
        ASPxClientUtils.PreventEvent(e.htmlEvent);
    }
}

function FilterDigit_AlphaOnly_KeyPress(s, e) {
    var key = ASPxClientUtils.GetKeyCode(e.htmlEvent);
    if ((key >= 65 && key <= 90) || key == 8 || key == 0) {
        return true;
    } else if (key >= 97 && key <= 122){
        //s.SetText(s.GetText().toUpperCase());
        return true;
    }
    else {
        ASPxClientUtils.PreventEvent(e.htmlEvent);
    }
}

function ToUpperCase_KeyUp(s, e) {
    s.SetText(s.GetText().toUpperCase());
}

function OK_Click(s, e) {
    DeletePopUpClient.Hide();
    InfoGridClient.DeleteRow(InfoGridClient.GetFocusedRowIndex());
}
