/*
 * Author: Abdullah A Almsaeed
 * Date: 4 Jan 2014
 * Description:
 *      This is a demo file used only for the main dashboard (index.html)
 **/

$(function () {

    'use strict';

    // Make the dashboard widgets sortable Using jquery UI
    $('.connectedSortable').sortable({
        placeholder: 'sort-highlight',
        connectWith: '.connectedSortable',
        handle: '.box-header, .nav-tabs',
        forcePlaceholderSize: true,
        zIndex: 999999
    });
    $('.connectedSortable .box-header, .connectedSortable .nav-tabs-custom').css('cursor', 'move');

    // jQuery UI sortable for the todo list
    $('.todo-list').sortable({
        placeholder: 'sort-highlight',
        handle: '.handle',
        forcePlaceholderSize: true,
        zIndex: 999999
    });

    // bootstrap WYSIHTML5 - text editor
    $('.textarea').wysihtml5();

    $('.daterange').daterangepicker({
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        startDate: moment().subtract(29, 'days'),
        endDate: moment()
    }, function (start, end) {
        window.alert('You chose: ' + start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    });

    /* jQueryKnob */
    $('.knob').knob();

    // jvectormap data
    var visitorsData = {
        US: 398, // USA
        SA: 400, // Saudi Arabia
        CA: 1000, // Canada
        DE: 500, // Germany
        FR: 760, // France
        CN: 300, // China
        AU: 700, // Australia
        BR: 600, // Brazil
        IN: 800, // India
        GB: 320, // Great Britain
        RU: 3000 // Russia
    };
    // World map by jvectormap
    $('#world-map').vectorMap({
        map: 'world_mill_en',
        backgroundColor: 'transparent',
        regionStyle: {
            initial: {
                fill: '#e4e4e4',
                'fill-opacity': 1,
                stroke: 'none',
                'stroke-width': 0,
                'stroke-opacity': 1
            }
        },
        series: {
            regions: [
                {
                    values: visitorsData,
                    scale: ['#92c1dc', '#ebf4f9'],
                    normalizeFunction: 'polynomial'
                }
            ]
        },
        onRegionLabelShow: function (e, el, code) {
            if (typeof visitorsData[code] != 'undefined')
                el.html(el.html() + ': ' + visitorsData[code] + ' new visitors');
        }
    });

    // Sparkline charts
    var myvalues = [1000, 1200, 920, 927, 931, 1027, 819, 930, 1021];
    $('#sparkline-1').sparkline(myvalues, {
        type: 'line',
        lineColor: '#92c1dc',
        fillColor: '#ebf4f9',
        height: '50',
        width: '80'
    });
    myvalues = [515, 519, 520, 522, 652, 810, 370, 627, 319, 630, 921];
    $('#sparkline-2').sparkline(myvalues, {
        type: 'line',
        lineColor: '#92c1dc',
        fillColor: '#ebf4f9',
        height: '50',
        width: '80'
    });
    myvalues = [15, 19, 20, 22, 33, 27, 31, 27, 19, 30, 21];
    $('#sparkline-3').sparkline(myvalues, {
        type: 'line',
        lineColor: '#92c1dc',
        fillColor: '#ebf4f9',
        height: '50',
        width: '80'
    });

    // The Calender
    $('#calendar').datepicker();

    // SLIMSCROLL FOR CHAT WIDGET
    $('#chat-box').slimScroll({
        height: '250px'
    });

    /* Morris.js Charts */
    // Sales chart
    var area = new Morris.Area({
        element: 'revenue-chart',
        resize: true,
        data: [
            { y: '2011 Q1', item1: 2666, item2: 2666 },
            { y: '2011 Q2', item1: 2778, item2: 2294 },
            { y: '2011 Q3', item1: 4912, item2: 1969 },
            { y: '2011 Q4', item1: 3767, item2: 3597 },
            { y: '2012 Q1', item1: 6810, item2: 1914 },
            { y: '2012 Q2', item1: 5670, item2: 4293 },
            { y: '2012 Q3', item1: 4820, item2: 3795 },
            { y: '2012 Q4', item1: 15073, item2: 5967 },
            { y: '2013 Q1', item1: 10687, item2: 4460 },
            { y: '2013 Q2', item1: 8432, item2: 5713 }
        ],
        xkey: 'y',
        ykeys: ['item1', 'item2'],
        labels: ['Item 1', 'Item 2'],
        lineColors: ['#a0d0e0', '#3c8dbc'],
        hideHover: 'auto'
    });
    var line = new Morris.Line({
        element: 'line-chart',
        resize: true,
        data: [
            { y: '2011 Q1', item1: 2666 },
            { y: '2011 Q2', item1: 2778 },
            { y: '2011 Q3', item1: 4912 },
            { y: '2011 Q4', item1: 3767 },
            { y: '2012 Q1', item1: 6810 },
            { y: '2012 Q2', item1: 5670 },
            { y: '2012 Q3', item1: 4820 },
            { y: '2012 Q4', item1: 15073 },
            { y: '2013 Q1', item1: 10687 },
            { y: '2013 Q2', item1: 8432 }
        ],
        xkey: 'y',
        ykeys: ['item1'],
        labels: ['Item 1'],
        lineColors: ['#efefef'],
        lineWidth: 2,
        hideHover: 'auto',
        gridTextColor: '#fff',
        gridStrokeWidth: 0.4,
        pointSize: 4,
        pointStrokeColors: ['#efefef'],
        gridLineColor: '#efefef',
        gridTextFamily: 'Open Sans',
        gridTextSize: 10
    });

    // Donut Chart
    var donut = new Morris.Donut({
        element: 'sales-chart',
        resize: true,
        colors: ['#3c8dbc', '#f56954', '#00a65a'],
        data: [
            { label: 'Download Sales', value: 12 },
            { label: 'In-Store Sales', value: 30 },
            { label: 'Mail-Order Sales', value: 20 }
        ],
        hideHover: 'auto'
    });

    // Fix for charts under tabs
    $('.box ul.nav a').on('shown.bs.tab', function () {
        area.redraw();
        donut.redraw();
        line.redraw();
    });

    /* The todo list plugin */
    $('.todo-list').todoList({
        onCheck: function () {
            window.console.log($(this), 'The element has been checked');
        },
        onUnCheck: function () {
            window.console.log($(this), 'The element has been unchecked');
        }
    });



    SalesActivity();
    ProductDetails();
    TopSellingItems();
    PurchaseOrder();
    SalesOrder();
    InventorySummary();
    SalesOrderDetail();
});


function SalesActivity() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Dashboard/SalesActivity',
        async: true,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $('._To_Be_Packed_').html(resp.ToBePacked);
        $('._To_Be_Shipped_').text(resp.ToBeShipped);
        $('._To_Be_Delivered_').text(resp.ToBeDelivered);
        $('._To_Be_Invoices_').text(resp.ToBeInvoiced);


    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function ProductDetails() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Dashboard/ProductDetails',
        type: "POST",
        async: true,
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        //$('._Low_Stock_Items_').text(resp.LowStockItems);
        $('._All_Items_').text(resp.TotalItems);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function TopSellingItems() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Dashboard/TopSellingItems',
        type: "POST",
        async: true,
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        var li = "";
        $.each(resp, function (i, item) {
            if (item.ItemImage == "" || item.ItemImage == null) {
                li = "<li>" +
                    "<img src='/Images/ItemImages/NoImageAvailable.jpg' alt='" + item.ItemName + "'>" +
                    "<a class='users-list-name' href='#'>" + item.ItemName + "</a>" +
                    "<span class='users-list-date'>" + item.QuantitySold + "</span>" +
                    "</li>";
            } else {
                li = "<li>" +
                    "<img src='" + item.ItemImage + "' alt='" + item.ItemName + "'>" +
                    "<a class='users-list-name' href='#'>" + item.ItemName + "</a>" +
                    "<span class='users-list-date'>" + item.QuantitySold + "</span>" +
                    "</li>";
            }
            $('._Top_Selling_Items_').append(li);
        });

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function PurchaseOrder() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Dashboard/PurchaseOrder',
        type: "POST",
        async: true,
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $('._Quantuty_Ordered_').text(resp.QuantityOrdered);
        $('._Total_Cost_').text(resp.TotalCost);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function SalesOrder() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Dashboard/SalesOrder',
        type: "POST",
        async: true,
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $('._Quantity_Sold_').text(resp.QuantitySold);
        $('._Total_Cost_SaleOrder_').text(resp.TotalCost);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function InventorySummary() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Dashboard/InventorySummary',
        type: "POST",
        async: true,
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $('._Quantity_In_Hand_').text(resp.QuantityInHand);
        //$('._Quantity_To_Be_Received_').text(resp.QuantityToBeReceived);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function SalesOrderDetail() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Dashboard/SalesOrderDetail',
        type: "POST",
        async: true,
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        var row = "<tr>" +
            "<td>" + resp.Draft+"</td>" +
            "<td>" + resp.Confirmed +"</td>" +
            "<td>" + resp.Packed +"</td>" +
            "<td>" + resp.Shipped +"</td>" +
            "<td>" + resp.Invoiced +"</td>" +
            "</tr>";
        $("#_tbl_SalesOrderDetails_Body_").append(row);
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}