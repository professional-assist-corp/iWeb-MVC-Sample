$(document).ready(function () {
    window.iWebRoot = location.href.substring(0, location.href.toLowerCase().indexOf("iweb") + 5);

    $('#btnLookup').click(function () {
        var searchData = { recno: $('#recno').val()};
        $.ajax({
            type: "GET",
            url: window.iWebRoot + "forms/SampleCustom/LookupID/?params=" + JSON.stringify(searchData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                location.href = window.iWebRoot + 'forms/DynamicProfile.aspx?FormKey=' + data.formKey + '&Key=' + data.cstKey;
            },
            error: function () {
                alert('An error occurred while running your search. Please try again.');
            }
        });
    });
});