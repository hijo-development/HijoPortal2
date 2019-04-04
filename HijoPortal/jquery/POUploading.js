function SaveChanges(s, e) {
    var code = EntityGridClient.GetText();
    console.log(code);
    var header = HeaderPathClient.GetText();
    var line = LinePathClient.GetText();
    var domain = DomainClient.GetText();
    var username = UnameClient.GetText();
    var password = PWordClient.GetText();

    if (AllowPasswordClient.GetChecked()) {
        if (code.length > 0 && header.length > 0 && line.length > 0 && domain.length > 0 && username.length > 0 && password.length > 0)
            POGridClient.UpdateEdit();
    } else {
        if (code.length > 0 && header.length > 0 && line.length > 0 && domain.length > 0 && username.length > 0)
            POGridClient.UpdateEdit();
    }
}

//INFO GRID
function Code_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    EntityClient.SetText(str[1]);
}

function Entity_SelectedIndexChanged(s, e) {
    var str = s.GetText().split("; ");
    s.SetText(str[0]);
    EntityNameClient.SetText(str[1]);
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
                s.UpdateEdit();
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

function POGrid_CustomButtonClick(s, e) {
    var btnID = e.buttonID;
    switch (btnID) {
        case 'DeleteRow':
            DeletePopUp2Client.SetHeaderText("Alert");
            DeletePopUp2Client.Show();
            break;
    }
}

function OnGetCellValues(values) {
    var code = values;
    console.log(code);

}

function InfoGrid_EndCallback(s, e) {
    if (ErrorHiddenValueClient.Get("error_text") != null) {
        ErrorCatchLblClient.SetText(ErrorHiddenValueClient.Get("error_text"));
        ErrorCatcher.SetHeaderText("Error");
        ErrorCatcher.Show();
    }

    console.log(ErrorHiddenValueClient.Get('error_text'));
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
    } else if (key >= 97 && key <= 122) {
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

function OK2_Click(s, e) {
    DeletePopUp2Client.Hide();
    POGridClient.DeleteRow(POGridClient.GetFocusedRowIndex());
}

//For PASSWORD
function AllowPassword_CheckedChanged(s, e) {
    var value = s.GetChecked();
    if (value) {
        PWordClient.SetEnabled(true);
        PasswordLblClient.SetEnabled(true);
        AllowLblClient.SetEnabled(true);
    } else {
        PWordClient.SetEnabled(false);
        PWordClient.SetText("");
        PasswordLblClient.SetEnabled(false);
        AllowLblClient.SetEnabled(false);
    }
}

var passwordMinLength = 6;
function GetPasswordRating(password) {
    var result = 0;
    if (password) {
        result++;
        if (password.length >= passwordMinLength) {
            if (/[a-z]/.test(password))
                result++;
            if (/[A-Z]/.test(password))
                result++;
            if (/\d/.test(password))
                result++;
            if (!(/^[a-z0-9]+$/i.test(password)))
                result++;
        }
    }
    return result;
}
function OnPasswordTextBoxInit(s, e) {
    ApplyCurrentPasswordRating();
}
function OnPassChanged(s, e) {
    ApplyCurrentPasswordRating();
}
function ApplyCurrentPasswordRating() {
    var password = PWordClient.GetText();
    var passwordRating = GetPasswordRating(password);
    ApplyPasswordRating(passwordRating);
}
function ApplyPasswordRating(value) {
    ratingControl.SetValue(value);
    switch (value) {
        case 0:
            ratingLabel.SetValue("Password safety");
            break;
        case 1:
            ratingLabel.SetValue("Too simple");
            break;
        case 2:
            ratingLabel.SetValue("Not safe");
            break;
        case 3:
            ratingLabel.SetValue("Normal");
            break;
        case 4:
            ratingLabel.SetValue("Safe");
            break;
        case 5:
            ratingLabel.SetValue("Very safe");
            break;
        default:
            ratingLabel.SetValue("Password safety");
    }
}
function GetErrorText(editor) {
    if (editor === PWordClient) {
        if (ratingControl.GetValue() === 1)

            return "The password is too simple";
    }
    // else if (editor === confirmPasswordTextBox) {
    //    if (PWordClient.GetText() !== confirmPasswordTextBox.GetText())
    //        return "The password you entered do not match";
    //}

    return "";
}
function OnPassValidation(s, e) {
    var errorText = GetErrorText(s);
    if (errorText) {
        e.isValid = false;
        e.errorText = errorText;
    }
}
