$(document).ready(function () {

    $("#logout_{{_setup.id}}").hide();

    $("#logout_{{_setup.id}}").click(function () {
        $.post("modules/{{_setup.id}}/login.php", { logout: 'true' }, function (data) {
            if (data != '') {
                $("#form_{{_setup.id}}").show();
                $("#answer_{{_setup.id}}").html('logged out');
                $("#logout_{{_setup.id}}").hide();
            }
        });
    });

    $.post("modules/{{_setup.id}}/login.php", { getuser: 'true' }, function (data) {
        if (data != '') {
            $("#form_{{_setup.id}}").hide();
            $("#answer_{{_setup.id}}").html(data);
            $("#logout_{{_setup.id}}").show();
        }
    });

    $("#form_{{_setup.id}}").submit(function () {

        $.post("modules/{{_setup.id}}/login.php", { username: $('#username_{{_setup.id}}').val(), password: $('#password_{{_setup.id}}').val() }, function (data) {
            $("#form_{{_setup.id}}").fadeOut(function () {
                $("#answer_{{_setup.id}}").html(data);
                $("#logout_{{_setup.id}}").show();
            });
        });
        return false;
    });
});