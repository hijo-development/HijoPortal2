function gridCreatedPO_CustomButtonClick(s, e) {
    var btnID = e.buttonID;
    switch (btnID) {
        case 'Edit':
            e.processOnServer = true;
            break;
        case 'Delete':
            break;
    }
}