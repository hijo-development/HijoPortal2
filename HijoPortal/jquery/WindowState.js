if (window.history && history.pushState) { // check for history api support
    window.addEventListener('load', function () {
        // create history states
        history.pushState(-1, null); // back state
        history.pushState(0, null); // main state
        history.pushState(1, null); // forward state
        history.go(-1); // start in main state

        this.addEventListener('popstate', function (event, state) {
            // check history state and fire custom events
            if (state = event.state) {

                event = document.createEvent('Event');
                event.initEvent(state > 0 ? 'next' : 'previous', true, true);
                this.dispatchEvent(event);

                // reset state
                history.go(-state);
                PopupLogout.SetHeaderText('Log Out');
                PopupLogout.Show();

            }
        }, false);

    }, false);
}

//window.onload = function () {
//    document.onkeydown = function (e) {
//        //return (e.which || e.keyCode) != 116;
//        if (e.which != 116 || e.keyCode != 116) {
//            PopupLogout.SetHeaderText('Log Out');
//            PopupLogout.Show();
//            return e.preventDefault();
//        }

//    };
//}