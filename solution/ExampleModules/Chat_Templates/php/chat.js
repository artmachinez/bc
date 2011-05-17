(function ($) {
    $.fn.chat = function (params) {
        // merge default and user parameters  
        params = $.extend({ username: 'phpchatter' + Math.floor(Math.random() * 100 + 1), timeout: 5000, chatroom: 'default' }, params);

        this.each(function () {

            var $window = $(this).find('.chatWindow');
            var $input = $(this).find('.chatInput');

            $input.keyup(function (e) {
                if (e.which == 13) {

                    $.fn.chat.postMessage($input.val().substring(0, $input.val().length - 1), params.chatroom, new function () {
                        $window.append($('<div></div>').text(params.username + ': ' + $input.val()));
                        $input.val(null);
                        $window.scrollTop(1000);
                    });
                }
            });

            $.fn.chat.authUser(params, $window);
        });

        return this;
    };

    $.fn.chat.wsLoop = function (timeout, displayBox, chatroom) {
        $.ajax({
            type: 'POST',
            url: 'modules/{{_setup.id}}/WebService.php?action=GetChat',
            data: "{ 'chatroom': '" + chatroom + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data != "") {
                    $.each(data, function (i, item) {
                        msgBox = $('<div></div>').text(item.Username + ': ' + item.Msg);
                        displayBox.append(msgBox);
                        displayBox.scrollTop(1000);
                    });
                }
                setTimeout(function () { $.fn.chat.wsLoop(timeout, displayBox, chatroom) }, timeout);
            }
        });
    };

    $.fn.chat.authUser = function (params, window) {
        $.ajax({
            type: 'POST',
            url: 'modules/{{_setup.id}}/WebService.php?action=AuthUser',
            data: '{ "username": "' + params.username + '"}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data == true) {
                    $.fn.chat.wsLoop(params.timeout, window, params.chatroom);
                }
            }
        });
    },

    $.fn.chat.postMessage = function (message, chatroom, callback) {
        $.ajax({
            type: 'POST',
            url: 'modules/{{_setup.id}}/WebService.php?action=PostMessage',
            data: '{ "message": "' + message + '"}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data == true) {
                    callback();
                }
            }
        });
    }
})(jQuery);  