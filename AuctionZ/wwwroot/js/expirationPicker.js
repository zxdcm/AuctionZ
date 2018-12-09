$(function () {
    $('#expirationPicker').datetimepicker({
        showTodayButton: true,
        format: 'DD-MM-YYYY HH:mm',
        showClose: true,
        showClear: true,
        toolbarPlacement: 'top',
        stepping: 15,
        widgetPositioning: {
            horizontal: 'right',
            vertical: 'bottom'
        }
    });
});