$(document).ready(function () {
    $.post("modules/{{_setup.id}}/backend.php", { logout: 'true' }, function (data) {
        if (data != '') {
            $("#div_{{_setup.id}}").html(data);
        }
    });
});