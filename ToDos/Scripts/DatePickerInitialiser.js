$(function () {
    $("#WhenItWasDone").datepicker({
        dateFormat: 'dd/mm/yy',
        defaultDate: 0,
        maxDate: 0
    });

    $("#FirstReminderDate").datepicker({
        dateFormat: 'dd/mm/yy',
        defaultDate: 0,
        minDate: 0
    });
});
