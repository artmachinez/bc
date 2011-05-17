(function ($) {
    $.fn.counter = function (params) {

        this.each(function () {
            var $currentVisBox = $(this).find('.currentVisBox');
            var $totalVisBox = $(this).find('.totalVisBox');
            var $IPBox = $(this).find('.IPBox');

            $.fn.counter.getDataLoop(5000, $currentVisBox, $totalVisBox, $IPBox);
        });
        return this;
    };

    $.fn.counter.getDataLoop = function (timeout, currentVisBox, totalVisBox, IPBox) {
        $.ajax({
            type: 'POST',
            url: 'modules/{{_setup.id}}/WebService.php?action=GetData',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                currentVisBox.text(data.currentVisitors);
                totalVisBox.text(data.totalVisitors);
                IPBox.text(data.requestIP);
                setTimeout(function () { $.fn.counter.getDataLoop(timeout, currentVisBox, totalVisBox, IPBox) }, timeout);
            }
        });
    };

})(jQuery);  