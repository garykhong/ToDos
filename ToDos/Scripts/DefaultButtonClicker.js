
function SetDefaultButtonToBeClickedOnEnterPress(formID, buttonID) {
    var ButtonKeys = { "EnterKey": 13 };
    var HtmlTagsToNotButtonClick = { "TextArea": "TEXTAREA" };
    $(function () {
        $("#" + formID).keypress(function (e) {
            if (e.which == ButtonKeys.EnterKey &&
                e.target.tagName != HtmlTagsToNotButtonClick.TextArea) {
                $("#" + buttonID).click();
                return false;
            }
        });
    });
}
