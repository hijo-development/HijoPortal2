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

function GetErrorText(editor) {
    if (editor === newPassword) {
        if (ratingControlChangePW.GetValue() === 1)
            return "The password is too simple";
    } else if (editor === confirmPassword) {
        if (newPassword.GetText() !== confirmPassword.GetText())
            return "The password you entered do not match";
    }
    return "";
}

function ApplyCurrentPasswordRating() {
    var password = newPassword.GetText();
    var passwordRating = GetPasswordRating(password);
    ApplyPasswordRating(passwordRating);
}

function ApplyPasswordRating(value) {
    ratingControlChangePW.SetValue(value);
    switch (value) {
        case 0:
            ratingLabelChangePW.SetValue("Password safety");
            break;
        case 1:
            ratingLabelChangePW.SetValue("Too simple");
            break;
        case 2:
            ratingLabelChangePW.SetValue("Not safe");
            break;
        case 3:
            ratingLabelChangePW.SetValue("Normal");
            break;
        case 4:
            ratingLabelChangePW.SetValue("Safe");
            break;
        case 5:
            ratingLabelChangePW.SetValue("Very safe");
            break;
        default:
            ratingLabelChangePW.SetValue("Password safety");
    }
}

function onControlsInitializedChangePW(s, e) {
    FormLayoutChangePWDirect.AdjustControl();
    var controlsReg = ASPxClientControl.GetControlCollection().GetControlsByPredicate(function (c) {
        return c.GetParentControl() === FormLayoutChangePWDirect;
    });
    for (var i = 0; i < controlsReg.length; i++) {
        var valEvt = controlsReg[i].Validation;
        if (valEvt)
            valEvt.AddHandler(onValidationReg);
    }
}

function OnPasswordTextBoxInitChangePW(s, e) {
    ApplyCurrentPasswordRating();
}

function OnPassChangedChangePW(s, e) {
    ApplyCurrentPasswordRating();
}

function OnPassValidationChangePW(s, e) {
    var errorText = GetErrorText(s);
    if (errorText) {
        e.isValid = false;
        e.errorText = errorText;
    }
}