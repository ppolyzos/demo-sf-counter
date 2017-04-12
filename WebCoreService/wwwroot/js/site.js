// Write your Javascript code.
(function ($) {

    // Chart Functionality
    $.fn.setChart = function () {
        return this.each(function () {
            // Variables
            var chart = $(this),
                path = $('.chart__foreground path', chart),
                dashoffset = path.get(0).getTotalLength(),
                goal = chart.attr('data-goal'),
                consumed = chart.attr('data-count');

            $('.chart__foreground', chart).css({
                'stroke-dashoffset': Math.round(dashoffset - ((dashoffset / goal) * consumed))
            });
        });
    }; // setChart()

    // Count
    $.fn.count = function () {
        return this.each(function () {
            $(this).prop('Counter', 0).animate({
                Counter: $(this).attr('data-count')
            }, {
                    duration: 1000,
                    easing: 'swing',
                    step: function (now) {
                        $(this).text(Math.ceil(now));
                    }
                });
        });
    }; // count()

    $(window).load(function () {
        $('.js-chart').setChart();
        $('.js-count').count();
    });

    window.updateCounter = function (container, data) {
        var $container = $('#' + container),
            $chart = $container.find('.js-chart'),
            $counter = $container.find('.js-count:eq(0)'),
            $total = $container.find('.total'),
            $node = $container.find('.node');

        $chart.attr('data-count', data.counter);
        $chart.setChart();
        $counter.text(data.counter % 300);
        $node.text(data.node);
        $total.text(data.counter);
    }

})(jQuery);