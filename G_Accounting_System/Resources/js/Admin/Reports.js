$(function () {

    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
    var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

    $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
    $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
    if (url.includes("/reports/inventory_productsalesreport")) {
        $('.sidebar-toggle').trigger('click');
        Inventory_ProductSalesReport(null);
    }
    if (url.includes("/reports/inventory_inventorydetailsreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Inventory_InventoryDetailsReport(null);
    }
    if (url.includes("/reports/inventory_inventoryvaluationsummaryreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Inventory_InventoryValuationSummaryReport(null);
    }
    if (url.includes("/reports/inventory_stocksummaryreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Inventory_StockSummaryReport(null);
    }
    if (url.includes("/reports/sales_salesorderhistoryreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_SalesOrderHistoryReport(null);
    }
    if (url.includes("/reports/sales_orderfulfillmentbyitemreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_OrderFulfillmentByItemReport(null);
    }
    if (url.includes("/reports/sales_invoicehistoryreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_InvoiceHistoryReport(null);
    }
    if (url.includes("/reports/sales_paymentsreceivedreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_PaymentsReceivedReport(null);
    }
    if (url.includes("/reports/sales_packinghistoryreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_PackingHistoryReport(null);
    }
    if (url.includes("/reports/sales_salesbycustomerreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_SalesByCustomerReport(null);
    }
    if (url.includes("/reports/sales_salesbyitemreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_SalesByItemReport(null);
    }
    if (url.includes("/reports/sales_salesbysalespersonreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Sales_SalesBySalesPersonReport(null);
    }
    if (url.includes("/reports/purchases_purchaseorderhistoryreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Purchases_PurchaseOrderHistoryReport(null);
    }
    if (url.includes("/reports/purchases_purchasebyvendorreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Purchases_PurchaseByVendorReport(null);
    }
    if (url.includes("/reports/purchases_purchasebyitemreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Purchases_PurchaseByItemReport(null);
    }
    if (url.includes("/reports/purchases_billdetailsreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        Purchases_BillDetailsReport(null);
    }
    if (url.includes("/reports/activitylogsreport")) {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
        $('.sidebar-toggle').trigger('click');
        ActivityLogsReport(null);
    }
})

$("#_Customize_Report_btn_").click(function () {
    $('#_Run_Report_').html("Run Report");
    $('._Customize_Report_Form_').slideDown("slow");
    $('#_Customize_Report_btn_').attr("disabled", true);
});

$('#_Add_Customize_Report_Remover_').on('click', function () {
    $('._Customize_Report_Form_').slideUp("slow");
    $('#_Run_Report_').html("Run Report");
    $('#_Customize_Report_btn_').attr("disabled", false);
});

$('#_Add_Customize_Report_Remover__').on('click', function () {
    $('._Customize_Report_Form_').slideUp("slow");
    $('#_Run_Report_').html("Run Report");
    $('#_Customize_Report_btn_').attr("disabled", false);
});

$("#_Run_Report_PSR").click(function () {
    ProductSalesReportList.ajax.reload(null, false);
});

$("#_Run_Report_IDR").click(function () {
    InventoryDetailsReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_IVSR").click(function () {
    InventoryValuationSummaryReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_SSR").click(function () {
    InventoryStockSummaryReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_SOHR").click(function () {
    SalesOrderHistoryReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_SOHR").click(function () {
    SalesOrderHistoryReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_OFBI").click(function () {
    OrderFulfillmentByItemReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_IHR").click(function () {
    InvoiceHistoryReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_PRR").click(function () {
    PaymentsReceivedReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_PHR").click(function () {
    PackingHistoryReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_SBCR").click(function () {
    SalesByCustomerReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_SBIR").click(function () {
    SalesByItemReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_SBSPR").click(function () {
    SalesBySalesPersonReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_POHR").click(function () {
    PurchaseOrderHistoryReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_PBVR").click(function () {
    PurchaseByVendorReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_PBIR").click(function () {
    PurchaseByItemReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_BDR").click(function () {
    BillDetailsReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});

$("#_Run_Report_ALR").click(function () {
    ActivityLogsReportList.ajax.reload(null, false);
    $("#reporttype").text("AS Of Date " + $('input[name=ToDate]').val());
});






$("select[name=Date_Range]").change(function () {

    var selectedText = $("select[name=Date_Range]").find("option:selected").text();
    if (selectedText === "Custom") {
        var date = new Date();
        $('input[name=FromDate]').val(date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear());
        $('input[name=ToDate]').val(date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear());

    } else if (selectedText === "Today") {
        var date = new Date();
        $('input[name=FromDate]').val(date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear());
        $('input[name=ToDate]').val(date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear());
    }
    else if (selectedText === "This Week") {

        var curr = new Date(); // get current date
        var first = curr.getDate() - curr.getDay(); // First day is the day of the month - the day of the week
        var last = first + 6; // last day is the first day + 6

        var firstDay = new Date(curr.setDate(first));
        curr = new Date(); // get current date
        var lastDay = new Date(curr.setDate(last));

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + (firstDay.getMonth() + 1) + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + (lastDay.getMonth() + 1) + "/" + lastDay.getFullYear());
        title = "As Of Date " + lastDay.getDate() + "/" + (lastDay.getMonth() + 1) + "/" + lastDay.getFullYear();
    }
    else if (selectedText === "This Month") {
        var date = new Date();
        var firstDay = new Date(date.getFullYear(), (date.getMonth() + 1), 1);
        var lastDay = new Date(date.getFullYear(), (date.getMonth() + 1) + 1, 0);

        $('input[name=FromDate]').val(firstDay.getDate() + "/" + firstDay.getMonth() + "/" + firstDay.getFullYear());
        $('input[name=ToDate]').val(lastDay.getDate() + "/" + lastDay.getMonth() + "/" + lastDay.getFullYear())
    }
    else if (selectedText === "This Year") {
        var date = new Date();
        var currentYear = date.getFullYear();


        $('input[name=FromDate]').val("01/01/" + currentYear);
        $('input[name=ToDate]').val("31/12/" + currentYear)
    }
    //$("#reporttype").text(title);
});

function Inventory_ProductSalesReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    ProductSalesReportList = $('#tbl_ProductSalesReport').DataTable({
        //"bpaginate": true,
        "searching": true,
        dom: 'Bftrip',
        "bServerSide": true,
        "sAjaxSource": '/Reports/Inventory_ProductSalesReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },

        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            total = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            sold = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(3, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            soldTotal = api
                .column(2, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );
            $(api.column(2).footer()).html(
                soldTotal
            );
            $(api.column(3).footer()).html(
                pageTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "ItemName" },
            { data: "SKU" },
            { data: "QuantitySold" },
            { data: "TotalSalePrice" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Inventory_InventoryDetailsReport(parameter) {
    var Stock_Availability = "";
    var selectedText = $("select[name=Stock_Availability]").find("option:selected").text();
    if (selectedText === "Greater than 0") {
        Stock_Availability = ">";
    } else if (selectedText === "Less than and equal to zero") {
        Stock_Availability = "<=";
    }
    else if (selectedText === "Less than zero") {
        Stock_Availability = "<";
    }
    else if (selectedText === "Equal to zero") {
        Stock_Availability = "=";
    }

    var token = $('[name=__RequestVerificationToken]').val();

    InventoryDetailsReportList = $('#tbl_InventoryDetailsReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Inventory_InventoryDetailsReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), ItemName: $('input[name=Item_Name]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[45].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "ItemName" },
            { data: "SKU" },
            { data: "QuantityOrdered" },
            { data: "QuantityIn" },
            { data: "QuantityOut" },
            { data: "StockInHand" },
            { data: "CommittedStock" },
            { data: "AvailableForSale" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Inventory_InventoryValuationSummaryReport(parameter) {
    var token = $('[name=__RequestVerificationToken]').val();

    InventoryValuationSummaryReportList = $('#tbl_InventoryValuationSummaryReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Inventory_InventoryValuationSummaryReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), ItemName: $('input[name=Item_Name]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            total = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(3, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(3).footer()).html(
                pageTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "ItemName" },
            { data: "SKU" },
            { data: "StockInHand" },
            { data: "InventoryAssetValue" },
        ],
        
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Inventory_StockSummaryReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    InventoryStockSummaryReportList = $('#tbl_StockSummaryReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Inventory_StockSummaryReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), ItemName: $('input[name=Item_Name]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "ItemName" },
            { data: "QuantityIn" },
            { data: "QuantityOut" },
            { data: "ClosingStock" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}





function Sales_SalesOrderHistoryReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    SalesOrderHistoryReportList = $('#tbl_SalesOrderHistoryReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_SalesOrderHistoryReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Status: $("select[name=Status] option:checked").val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[30].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            total = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(4).footer()).html(
                pageTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Date_Of_Day" },
            { data: "SaleOrderNo" },
            { data: "CustomerName" },
            { data: "SO_Status" },
            { data: "SO_Total_Amount" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Sales_OrderFulfillmentByItemReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    OrderFulfillmentByItemReportList = $('#tbl_SalesOrderFulfillmentByItem').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_OrderFulfillmentByItemReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[30].value
                    })
                },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "ItemName" },
            { data: "SKU" },
            { data: "Ordered" },
            { data: "DropShipped" },
            { data: "Fulfilled" },
            { data: "ToBePacked" },
            { data: "ToBeShipped" },
            { data: "ToBeDelivered" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Sales_InvoiceHistoryReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    InvoiceHistoryReportList = $('#tbl_InvoiceHistoryReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_InvoiceHistoryReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Status: $("select[name=Status] option:checked").val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[45].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            invoice = api
                .column(6)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            balance = api
                .column(7)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            invoiceTotal = api
                .column(6, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            balanceTotal = api
                .column(7, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(6).footer()).html(
                invoiceTotal
            );
            $(api.column(7).footer()).html(
                balanceTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Invoice_Status" },
            { data: "InvoiceDateTime" },
            { data: "InvoiceDueDate" },
            { data: "Invoice_No" },
            { data: "SalesOrderNo" },
            { data: "CustomerName" },
            { data: "Invoice_Amount" },
            { data: "Balance_Amount" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Sales_PaymentsReceivedReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    PaymentsReceivedReportList = $('#tbl_PaymentsReceivedReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_PaymentsReceivedReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[35].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            total = api
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(5, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
 
            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(5).footer()).html(
                pageTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "PaymentNo" },
            { data: "SO_Payment_Date" },
            { data: "CustomerName" },
            { data: "Payment_Mode" },
            { data: "Invoice_No" },
            { data: "Invoice_Amount" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Sales_PackingHistoryReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    PackingHistoryReportList = $('#tbl_PackingHistoryReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_PackingHistoryReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Status: $("select[name=Status] option:checked").val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[30].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            total = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(4).footer()).html(
                pageTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Date_Of_Day" },
            { data: "Package_No" },
            { data: "SaleOrderNo" },
            { data: "Package_Status" },
            { data: "Quantity" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Sales_SalesByCustomerReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    SalesByCustomerReportList = $('#tbl_SalesByCustomerReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_SalesByCustomerReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), CustomerName: $('input[name=Customer_Name]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[20].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            invoicecounttotal = api
                .column(1)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            salestotal = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageinvoicecountTotal = api
                .column(1, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pagesalesTotal = api
                .column(2, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(1).footer()).html(
                pageinvoicecountTotal
            );
            $(api.column(2).footer()).html(
                pagesalesTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "CustomerName" },
            { data: "InvoiceCount" },
            { data: "Sales" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Sales_SalesByItemReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    SalesByItemReportList = $('#tbl_SalesByItemReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_SalesByItemReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), ItemName: $('input[name=Item_Name]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            soldtotal = api
                .column(1)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            amounttotal = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pagesoldTotal = api
                .column(1, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pageamountTotal = api
                .column(2, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(1).footer()).html(
                pagesoldTotal
            );
            $(api.column(2).footer()).html(
                pageamountTotal
            );
         },
        responsive: {
            details: false
        },
        "columns": [
            { data: "ItemName" },
            { data: "QuantitySold" },
            { data: "Amount" },
            { data: "AveragePrice" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Sales_SalesBySalesPersonReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    SalesBySalesPersonReportList = $('#tbl_SalesBySalespersonReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Sales_SalesBySalesPersonReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), UserName: $('input[name=Salesperson_Name]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[20].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            invoicecounttotal = api
                .column(1)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            salestotal = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageinvoicecountTotal = api
                .column(1, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pagesalesTotal = api
                .column(2, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(1).footer()).html(
                pageinvoicecountTotal
            );
            $(api.column(2).footer()).html(
                pagesalesTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "AddedByName" },
            { data: "InvoiceCount" },
            { data: "Sales" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}





function Purchases_PurchaseOrderHistoryReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    PurchaseOrderHistoryReportList = $('#tbl_PurchaseOrderHistoryReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Purchases_PurchaseOrderHistoryReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Status: $("select[name=Status] option:checked").val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[40].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            orderedtotal = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            receivedtotal = api
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            amounttotal = api
                .column(6)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageorderedTotal = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pagereceivedTotal = api
                .column(5, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pageamountTotal = api
                .column(6, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(4).footer()).html(
                pageorderedTotal
            );
            $(api.column(5).footer()).html(
                pagereceivedTotal
            );
            $(api.column(6).footer()).html(
                pageamountTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Date_Of_Day" },
            { data: "PurchaseOrderNo" },
            { data: "VendorName" },
            { data: "PurchaseOrderStatus" },
            { data: "QuantityOrdered" },
            { data: "QuantityReceived" },
            { data: "Amount" },
        ],

        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }

    });
    table.buttons().container().appendTo('#example_wrapper .col-md-6:eq(0)');
}

function Purchases_PurchaseByVendorReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    PurchaseByVendorReportList = $('#tbl_PurchaseByVendorReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Purchases_PurchaseByVendorReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[20].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            orderedtotal = api
                .column(1)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            amounttotal = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageorderedTotal = api
                .column(1, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pageamountTotal = api
                .column(2, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(1).footer()).html(
                pageorderedTotal
            );
            $(api.column(2).footer()).html(
                pageamountTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "VendorName" },
            { data: "QuantityOrdered" },
            { data: "Amount" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Purchases_PurchaseByItemReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    PurchaseByItemReportList = $('#tbl_PurchaseByItemReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Purchases_PurchaseByItemReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[20].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            purchasedtotal = api
                .column(1)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            amounttotal = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pagepurchasedTotal = api
                .column(1, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pageamountTotal = api
                .column(2, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(1).footer()).html(
                pagepurchasedTotal
            );
            $(api.column(2).footer()).html(
                pageamountTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "ItemName" },
            { data: "QuantityPurchased" },
            { data: "Amount" },
            { data: "AveragePrice" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function Purchases_BillDetailsReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    BillDetailsReportList = $('#tbl_BillDetailsReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/Purchases_BillDetailsReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[40].value
                    })
                },
                "success": fnCallback
            });
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // Total over all pages
            billamounttotal = api
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            balanceamounttotal = api
                .column(6)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pagebillamountTotal = api
                .column(5, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            pagebalanceTotal = api
                .column(6, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(0).footer()).html(
                'TOTAL'
            );

            $(api.column(5).footer()).html(
                pagebillamountTotal
            );
            $(api.column(6).footer()).html(
                pagebalanceTotal
            );
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Bill_Status" },
            { data: "BillDateTime" },
            { data: "BillDueDate" },
            { data: "Bill_No" },
            { data: "VendorName" },
            { data: "Bill_Amount" },
            { data: "Balance_Amount" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

function ActivityLogsReport(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    ActivityLogsReportList = $('#tbl_ActivityLogsReport').DataTable({
        //"bpaginate": true,
        dom: 'Bftrip',
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": '/Reports/ActivityLogsReport',
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": {
                    __RequestVerificationToken: token, "Search": JSON.stringify({
                        Option: parameter, StartDate: $('input[name=FromDate]').val(), EndDate: $('input[name=ToDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[20].value
                    })
                },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "DateTime" },
            { data: "Activity" },
            { data: "Description" },
        ],
        buttons: [
            {
                extend: 'csv',
                "className": 'dt-button buttons-csv buttons-html5 btn btn-default',
            },
            {
                extend: 'excel',
                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-excel buttons-html5 btn btn-default',
            },
            {
                extend: 'pdfHtml5',

                customize: function (doc) {
                    doc.content[1].margin = [200, 0, 100, 0]
                    doc.content[2].margin = [50, 0, 100, 0]
                },

                title: $('#tit').text(),
                messageTop: $('#reporttype').text(),
                "className": 'dt-button buttons-pdf buttons-html5 btn btn-default'

            },
            {
                extend: 'print',
                title: $('#tit').text(),
                messageTop: $('#reporttype').html(),
                "className": 'dt-button buttons-print buttons-html5 btn btn-default',
                customize: function (win) {
                    $(win.document.body)
                        .css('text-align', 'center');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        initComplete: function () {
            $('.buttons-csv').html('<i class="fa fa-file-text-o" /> CSV')
            $('.buttons-excel').html('<i class="fa fa-file-excel-o" /> Excel')
            $('.buttons-pdf').html('<i class="fa fa-file-pdf-o"/> PDF')
            $('.buttons-print').html('<i class="fa fa-print" /> Print')
        }
    });
}

var doc = new jsPDF();
var specialElementHandlers = {
    '.repo': function (element, renderer) {
        return true;
    }
};

//$('.create_pdf').click(function () {
//    doc.fromHTML($('.repo').html(), 15, 15, {
//        'width': 500,
//        'elementHandlers': specialElementHandlers
//    });
//    doc.save('sample-file.pdf');
//});

//$('.create_pdf').click(function () {
//    var HTML_Width = $(".repo").width();
//    var HTML_Height = $(".repo").height();
//    var top_left_margin = 15;
//    var PDF_Width = HTML_Width + (top_left_margin * 2);
//    var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);

//    var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;
//    margins = {
//        top: 80,
//        bottom: 60,
//        left: 40,
//        width: HTML_Width
//    }
//    source = $('.repo').html();

//    var pdf = new jsPDF('l', 'pt', [HTML_Width, HTML_Height]);
//    specialElementHandlers = {
//        // element with id of "bypass" - jQuery style selector
//        '.dataTables_length': function (element, renderer) {
//            // true = "handled elsewhere, bypass text extraction"
//            return true
//        },
//        '.dataTables_filter': function (element, renderer) {
//            // true = "handled elsewhere, bypass text extraction"
//            return true
//        },
//        '.dataTables_paginate': function (element, renderer) {
//            // true = "handled elsewhere, bypass text extraction"
//            return true
//        },
//        '.paging_simple_numbers': function (element, renderer) {
//            // true = "handled elsewhere, bypass text extraction"
//            return true
//        },
//        '.dataTables_info': function (element, renderer) {
//            // true = "handled elsewhere, bypass text extraction"
//            return true
//        },
//        '.exportButton': function (element, renderer) {
//            // true = "handled elsewhere, bypass text extraction"
//            return true
//        }
//    };
//    pdf.fromHTML(
//        source, // HTML string or DOM elem ref.
//        margins.left, // x coord
//        margins.top, { // y coord
//            'width': PDF_Width, // max width of content on PDF
//            'elementHandlers': specialElementHandlers
//        },
//        function (dispose) {
//            // dispose: object with X, Y of the last line add to the PDF 
//            //          this allow the insertion of new lines after html
//        });


//    for (var i = 1; i <= totalPDFPages; i++) {
//        pdf.addPage(PDF_Width, PDF_Height);
//        pdf.fromHTML(
//            source, // HTML string or DOM elem ref.
//            margins.left, // x coord
//            margins.top, { // y coord
//                'width': margins.width, // max width of content on PDF
//                'elementHandlers': specialElementHandlers
//            },

//            function (dispose) {
//                // dispose: object with X, Y of the last line add to the PDF 
//                //          this allow the insertion of new lines after html
//            });
//    }

//    pdf.save("HTML-Document.pdf");

//});





    //var pdf = new jsPDF('l', 'pt', 'a4');
    //// source can be HTML-formatted string, or a reference
    //// to an actual DOM element from which the text will be scraped.
    //source = $('.repo')[0];
    //// we support special element handlers. Register them with jQuery-style 
    //// ID selector for either ID or node name. ("#iAmID", "div", "span" etc.)
    //// There is no support for any other type of selectors 
    //// (class, of compound) at this time.
    //specialElementHandlers = {
    //    // element with id of "bypass" - jQuery style selector
    //    '#bypassme': function (element, renderer) {
    //        // true = "handled elsewhere, bypass text extraction"
    //        return true
    //    }
    //};
    //margins = {
    //    top: 80,
    //    bottom: 60,
    //    left: 40,
    //    width: 1000
    //};
    //// all coords and widths are in jsPDF instance's declared units
    //// 'inches' in this case
    //pdf.fromHTML(
    //    source, // HTML string or DOM elem ref.
    //    margins.left, // x coord
    //    margins.top, { // y coord
    //        'width': margins.width, // max width of content on PDF
    //        'elementHandlers': specialElementHandlers
    //    },

    //    function (dispose) {
    //        // dispose: object with X, Y of the last line add to the PDF 
    //        //          this allow the insertion of new lines after html

    //        pdf.save('Test.pdf');
    //    }, margins);
