$(document).ready(function () {
    changeWindow.resizeWidth();
});

$(window).resize(function () {
    changeWindow.resizeWidth();
});

changeWindow = {
    resizeWidth: function () {
        var h = window.innerHeight
            || document.documentElement.clientHeight
            || document.body.clientHeight;

        var minusfive = h - (h * 0.01);
        $('#dvContentWrapper11').height(minusfive);
        console.log("resize creare:" + minusfive);
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
    var password = passwordTextBox.GetText();
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
    if (editor === passwordTextBox) {
        if (ratingControl.GetValue() === 1)
            return "The password is too simple";
    } else if (editor === confirmPasswordTextBox) {
        var PW = passwordTextBox.GetText();
        var conPW = confirmPasswordTextBox.GetText();
        var strMatch = PW.match(conPW);
        if (strMatch === null) {
            return "The password you entered do not match.";
        }
        //if (passwordTextBox.GetText() !== confirmPasswordTextBox.GetText())
        //    return "The password you entered do not match";
    }
    return "";
}
function OnPassValidation(s, e) {
    var errorText = GetErrorText(s);
    if (errorText) {
        e.isValid = false;
        e.errorText = errorText;
    }
}
function onControlsInitialized(s, e) {
    FormLayoutRegDirect.AdjustControl();
    var controlsReg = ASPxClientControl.GetControlCollection().GetControlsByPredicate(function (c) {
        return c.GetParentControl() === FormLayoutRegDirect;
    });
    for (var i = 0; i < controlsReg.length; i++) {
        var valEvt = controlsReg[i].Validation;
        if (valEvt)
            valEvt.AddHandler(onValidationReg);
    }

    FormLayoutAutDirect.AdjustControl();
    var controlsAut = ASPxClientControl.GetControlCollection().GetControlsByPredicate(function (c) {
        return c.GetParentControl() === FormLayoutAutDirect;
    });
    for (var i = 0; i < controlsAut.length; i++) {
        var valEvt = controlsAut[i].Validation;
        if (valEvt)
            valEvt.AddHandler(onValidationAut);
    }

    FormLayoutMedDirect.AdjustControl();
    var controlsMed = ASPxClientControl.GetControlCollection().GetControlsByPredicate(function (c) {
        return c.GetParentControl() === FormLayoutMedDirect;
    });
    for (var i = 0; i < controlsMed.length; i++) {
        var valEvt = controlsMed[i].Validation;
        if (valEvt)
            valEvt.AddHandler(onValidationMed);
    }
}
function onValidationReg(s, e) {
    setTimeout(function () {
        FormLayoutRegDirect.AdjustControl();
    }, 0);
}

function onValidationAut(s, e) {
    setTimeout(function () {
        FormLayoutAutDirect.AdjustControl();
    }, 0);

}

function onValidationMed(s, e) {
    setTimeout(function () {
        FormLayoutMedDirect.AdjustControl();
    }, 0);

}

var PostBackIDNum = false;
function onIDNumberLostFocus(s, e) {

    //CallbackPanelIDNumDirect.PerformCallback();
    //console.log("Match ID : " + idnumTextboxMatchDirect.GetText());

    //idnumTextboxMatchDirect.SetValue("ID");
    //if (CallbackPanelIDNumDirect.InCallback()) {
    //    PostBackIDNum = true;
    //    //console.log("Match ID : " + idnumTextboxMatchDirect.GetText());
    //}
    //else {
    //    CallbackPanelIDNumDirect.PerformCallback();
    //    //console.log("Match ID : " + idnumTextboxMatchDirect.GetText());
    //}
    //console.log(idnumTextboxMatchDirect.GetText());
}

function IDNumberEndCallback(s, e) {
    if (PostBackIDNum) {
        CallbackPanelIDNumDirect.PerformCallback();
        PostBackIDNum = false;
        //console.log("Match ID : " + idnumTextboxMatchDirect.GetText());
    }
}

function OnIDNumberTextBoxInit(s, e) {
    //var errorText = GetErrorText(s);
    //if (errorText) {
    //    e.isValid = false;
    //    e.errorText = errorText;
    //}
}

function OnIDNumPassValidation(s, e) {
    //var inputIDNum = idnumTextboxDirect.GetText();
    //var IDNumMatch = idnumTextboxMatchDirect.GetText();
    //var IDNumMatch = document.getElementById("IDNumTextBoxMatch");
    //console.log("ID Num : " + IDNumMatch);
    //if (inputIDNum !== IDNumMatch) {
    //    e.isValid = false;
    //    e.errorText = "Invalid ID Number!";
    //} else {
    //    e.isValid = true;
    //    e.errorText = "";
    //}
}

function SignUp(s, e) {

    if (ASPxClientEdit.AreEditorsValid()) {
        $find('ModalPopupExtenderLoading').show();
        e.processOnServer = true;
    }

    //var idnum = idnumTextboxDirect.GetText();
    //var lname = lnameTextboxDirect.GetText();
    //var fname = fnameTextboxDirect.GetText();
    //var gender = cmbGenderDirect.GetValue();
    //var email = eMailTextBoxDirect.GetText();
    //var uname = userNameTextBoxDirect.GetText();

    //if (idnum.length > 0 && lname.length > 0 && fname.length > 0 && gender.length > 0 && email.length > 0 && uname.length > 0) {
    //    $find('ModalPopupExtenderLoading').show();
    //    e.processOnServer = true;
    //}
}

function AllCallback(s, e) {
    if (idnumTextboxDirect.GetText().length > 0) {
        if (e.htmlEvent.keyCode == 13) {
            lastnameCallbackClient.PerformCallback();
            firstnameCallbackClient.PerformCallback();
            genderCallbackClient.PerformCallback();
            emailCallbackClient.PerformCallback();
            EmployeeImageCallbackClient.PerformCallback();
        }
    }
}