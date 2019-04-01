function OnGetRowValues(value) {
    var status = value[0];
    console.log("stat:" + status);
    switch (status) {
        case "0"://allow to delete
            POListPopupClient.SetHeaderText("Alert");
            POListPopupClient.Show();
            break;
        case "1"://not allowed to delete
            PopupNotAllowed.SetHeaderText("Alert");
            PopupNotAllowed.Show();
            break;

    }
}

function gridCreatedPO_CustomButtonClick(s, e) {
    var btnID = e.buttonID;
    switch (btnID) {
        case 'Edit':
            e.processOnServer = true;
            break;
        case 'Delete':
            var visibleIndex = gridCreatedPO.GetFocusedRowIndex();
            console.log("stat:....");
            gridCreatedPO.GetRowValues(visibleIndex, 'POStatus', OnGetRowValues);
            
            break;
    }
}

function OK_Click(s, e) {
    POListPopupClient.Hide();
    e.processOnServer = true;
}

