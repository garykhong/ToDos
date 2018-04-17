
function SetDefaultButtonToBeClickedOnEnterPress(formID, buttonID) {
    var ButtonKeys = { "EnterKey": 13 };
    $(function () {
        $("#" + formID).keypress(function (e) {
            if (e.which == ButtonKeys.EnterKey) {
                $("#" + buttonID).click();
                return false;
            }
        });
    });
}
