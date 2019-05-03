<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeoutControl.ascx.cs" Inherits="HijoPortal.TimeoutControl" %>

<script type="text/javascript">
    window.SessionTimeout = (function () {
        var _timeLeft, _popupTimer, _countDownTimer;
        var stopTimers = function () {
            window.clearTimeout(_popupTimer);
            window.clearTimeout(_countDownTimer);
        };
        var updateCountDown = function () {
            var min = Math.floor(_timeLeft / 60);
            var sec = _timeLeft % 60;
            if (sec < 10)
                sec = "0" + sec;
            document.getElementById("CountDownHolder").innerHTML = min + ":" + sec;
            if (_timeLeft > 0) {
                _timeLeft--;
                _countDownTimer = window.setTimeout(updateCountDown, 1000);
            } else {
                window.location = <%= QuotedTimeOutUrl %>;
            }
        };
        var showPopup = function () {
            _timeLeft = 60;
            updateCountDown();
            ClientTimeoutPopup.Show();
        };
        var schedulePopup = function () {
            stopTimers();
            _popupTimer = window.setTimeout(showPopup, <%= PopupShowDelay %>);
        };
        var sendKeepAlive = function () {
            stopTimers();
            ClientTimeoutPopup.Hide();
            ClientKeepAliveHelper.PerformCallback();
        };
        return {
            schedulePopup: schedulePopup,
            sendKeepAlive: sendKeepAlive
        };
    })();    
</script>

<dx:ASPxPopupControl ID="TimeoutPopup" runat="server" ClientInstanceName="ClientTimeoutPopup"
    CloseAction="None" HeaderText="Session Expiring" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="300px" Height="30%" ShowFooter="true"
    AllowDragging="true" Theme="Moderno">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            Your session is about to expire!
            <br />
            <br />
            <span id="CountDownHolder"></span>
            <br />
            <br />
            Click OK to continue your session.
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterTemplate>
        <dx:ASPxButton ID="OkButton" runat="server" Text="OK" AutoPostBack="false" Theme="Moderno">
            <ClientSideEvents Click="SessionTimeout.sendKeepAlive" />
        </dx:ASPxButton>
    </FooterTemplate>
    <FooterStyle Paddings-Padding="5" />
</dx:ASPxPopupControl>

<dx:ASPxGlobalEvents ID="GlobalEvents" runat="server">
    <ClientSideEvents ControlsInitialized="SessionTimeout.schedulePopup" />
</dx:ASPxGlobalEvents>

<dx:ASPxCallback ID="KeepAliveHelper" runat="server" ClientInstanceName="ClientKeepAliveHelper"></dx:ASPxCallback>
