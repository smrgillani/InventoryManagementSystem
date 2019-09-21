$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if ((window.location.pathname.toString().toLowerCase()) == "/bill/index") {
        GetAllBillsList(null);
    } else if (url.includes("/bill/billinvoice/")) {
        var id = window.location.pathname.toString().toLowerCase().split("/billinvoice/")[1];
        BillInvoice();
        POBillPaymentHistory(id);
    }
    else if (url.includes("/bill/billpayment/")) {
        var id = window.location.pathname.toString().toLowerCase().split("/billpayment/")[1];
        BillPayment();
        PaymentModesDropdown();
        POBillPaymentHistory(id);
    }
    //else if (url.includes("/bill/ViewPayments/")) {
    //    var billid = $("#billid").val();
    //    ViewPayments_(billid);
    //}
});

function GetAllBillsList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    BillsList = $('#bills_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No bills available"
        },
        "sAjaxSource": "/Bill/GetAllBills",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=BillStartDate]').val(), EndDate: $('input[name=BillEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[30].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/Bill/BillInvoice/" + data.id + "'>" + data.Bill_No + "</a>";
                }
            },
            { data: "Order_No" },
            { data: "BillDueDate" },
            { data: "Bill_Amount" },
            { data: "Balance_Amount" }
        ]
    });
}

function BillInvoice() {

    var token = $('[name=__RequestVerificationToken]').val();
    var id = window.location.pathname.toString().toLowerCase().split("/billinvoice/")[1];

    $('#billid').val(id);
    $.ajax({
        url: '/Bill/BillInvoice/' + id,
        type: "POST",
        async: false,
        data: {
            __RequestVerificationToken: token
        },
        //datatype: 'json',
        //ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $("#Idate").html('Date: ' + resp.Date);
        $("#Inameto").html(resp.PItems[0].VendorName);
        $("#Iaddressto").html(resp.PItems[0].VendorAddress + '<br> Landline: ' + resp.PItems[0].VendorLandline + '<br> Mobile: ' + resp.PItems[0].VendorMobile + '<br> Email: ' + resp.PItems[0].VendorEmail);
        $("#IBillno").html('<b>Bill # </b>' + resp.pBill.Bill_No);
        $("#IBillStatus").html(resp.pBill.Bill_Status);
        $.each(resp.PItems, function (i, item) {
            var rows = "<tr>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty + ' x ' + item.PriceUnit + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.TotalItems + "</td>" +
                "</tr>";
            $('#Iitemslistbody').append(rows);
        });
        $("#Isubtotal").html(resp.TotalPrice);
        $("#Itotal").html('<b>' + resp.TotalPrice + '</b>');

        $("#purchaseid").val(resp.InvoiceNum);
        if (resp.pBill.Bill_Status == "Open" || resp.pBill.Bill_Status == "Closed" || resp.pBill.Bill_Status == "Partially Paid" || resp.pBill.Bill_Status == "Draft") {
            $('._Record_Payment_').html('Record Payment');
        }

    }).fail(function () {

        alert("error 1");
    });
}

$("._Record_Payment_").click(function () {
    var id = $('#billid').val();
    if ($("#IBillStatus").html() == "Draft") {
        $.confirm({
            title: '',
            content: 'Bill will be automatically marked as Open once the payment is recorded.',
            buttons: {
                confirm: function () {
                    window.location.href = '/bill/billpayment/' + id;

                },
                cancel: function () {

                },
            }
        });
    }
    else {
        window.location.href = '/bill/billpayment/' + id;
    }

});

function BillPayment() {
    var token = $('[name=__RequestVerificationToken]').val();
    var id = window.location.pathname.toString().toLowerCase().split("/billpayment/")[1];
    $.ajax({
        url: '/Bill/BillPayment/' + id,
        type: "POST",
        data: {
            __RequestVerificationToken: token
        },

    }).done(function (resp) {
        $('#bill_id').val(id);
        $('#bill_no').val(resp.pBill.Bill_No);
        $('input[name=new_payment_made]').focus();
        if (resp.pBill.Bill_Status == "Closed") {
            total = resp.pBill.Balance_Amount;
            $("input[name=new_payment_made]").val(total);
            $("#_New_Payment_Save_").attr("disabled", true)
        }
        else {
            if (resp.pBill.Balance_Amount == '0') {
                total = resp.pBill.Bill_Amount;
                $("input[name=new_payment_made]").val(total);
                $('#total').val(total);
            }
            else {
                total = resp.pBill.Balance_Amount;
                $("input[name=new_payment_made]").val(total);
                $('#total').val(total);
            }
        }

        //if (resp.pBill.Balance_Amount == 0) {
        //    if (resp.pBill.Amount_Paid != 0) {
        //        total = resp.pBill.Balance_Amount;
        //        $("input[name=new_payment_made]").val(total);
        //        $("#_New_Payment_Save_").attr("disabled", true)
        //    }
        //    else {
        //        total = resp.pBill.Bill_Amount;
        //        $("input[name=new_payment_made]").val(total);
        //        $('#total').val(total);
        //        $("#_New_Payment_Save_").attr("disabled", false)
        //    }
        //}
        //else {
        //    total = resp.pBill.Balance_Amount;
        //    $("input[name=new_payment_made]").val(total);
        //    $('#total').val(total);
        //}

        //paid = resp.pBill.Amount_Paid;
        $('#payment_history_title').html('Payment History Against Bill # <b>' + resp.pBill.Bill_No + '</b>');
        //
    }).fail(function () {

        alert("error 1");
    });
}
var total;
var paid;
var balance;

var paymentHistory = 0;
$("#_New_Payment_Save_").click(function () {
    $('#_Error_Message_Display_ > span').html("");
    if ($("input[name=new_payment_made]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter Amount to Pay<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=new_payment_made]").val() === "0") {
        $('#_Error_Message_Display_ > span').html("Amount cannot be 0<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=new_payment_date]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Select Payment Date<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=get_payment_mode] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > span').html("Please Select Payment Mode<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        total = $('#total').val();
        paid = $("input[name=new_payment_made]").val();
        balance = total - paid;
        if (total - paid == 0) {
            balance = 0;
        }
        var BillStatus;
        var url = '/Bill/InsertPayment';
        var Payment_data = {
            Bill_id: $("#bill_id").val(),
            Purchase_id: $("#purchaseid").val(),
            Payment_Mode: $("select[name=get_payment_mode] option:checked").val(),
            Payment_Date: $("input[name=new_payment_date]").val(),
            Total_Amount: total,
            Paid_Amount: paid,
            Balance_Amount: balance,
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "Paymentdata": JSON.stringify(Payment_data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == "1") {
                //$("input[name=new_payment_made]").val(balance);

                $('#_Success_Message_Display_ > span').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                if (total - paid > 0) {
                    BillStatus = "Partially Paid";
                    $("#_New_Payment_Save_").attr("disabled", false);
                }
                else if (total - paid == 0) {
                    BillStatus = "Closed";
                    $("#_New_Payment_Save_").attr("disabled", true);
                }
                else {
                    BillStatus = "Open";
                    $("#_New_Payment_Save_").attr("disabled", false);
                }
                var Bill_data = {
                    id: $("#bill_id").val(),
                    Bill_Status: BillStatus,
                }
                $.ajax({
                    url: '/Bill/UpdateBillStatus/',
                    type: "POST",
                    data: { __RequestVerificationToken: token, "BillData": JSON.stringify(Bill_data) },
                }).done(function (resp) {
                    //$("#billid").val(resp.pBill_id_Output),
                    //$('._Purchase_Order_Save_').html('Convert to Bill');
                    //    $('#_Success_Message_Display_ > span').html(resp.pDesc);
                    //    $('#_Success_Message_Display_').slideDown("slow");

                    //        setTimeout(function () {
                    //            $('#_Success_Message_Display_').slideUp("slow");
                    //        }, 5000);
                    status = 'bill';
                    BillPayment();
                }).fail(function () {

                    $('#_Error_Message_Display_ > span').html("Error!");
                    $('#_Error_Message_Display_').slideDown("slow");
                    setTimeout(function () {
                        $('#_Error_Message_Display_').slideUp("slow");
                    }, 5000);
                });
                POBillPaymentHistory($("#bill_id").val());
            }
            else {
                $('#_Error_Message_Display_ > span').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
        }).fail(function () {
            alert("post error 0");
        });
    }
});

function POBillPaymentHistory(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    //var id = window.location.pathname.toString().toLowerCase().split("/billpayment/")[1];
    $.ajax({
        url: '/Bill/SelectPaymentByBillId/' + id,
        type: "POST",
        async: false,
        data: {
            __RequestVerificationToken: token
        },
        //datatype: 'json',
        //ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        
        $("#_tbl_Payments_List_Body_").empty();
        if (resp.length >0) {
            alert();
            $('#payment_history_title').html('Payment History Against Bill # <b>' + resp[0].Bill_No + '</b>');
        }
        else {
            var bno = $("#IBillno").html()
            $('#payment_history_title').html('Payment History Against <b>' + bno + '</b>');
        }
        $.each(resp, function (i, item) {
            var rows = "<tr>" +
                "<td class='table_content_vertical_align_'>" + item.Bill_No + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.Total_Amount + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.Paid_Amount + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.Balance_Amount + "</td>" +
                "</tr>";
            $('#_tbl_Payments_List_Body_').append(rows);
        });

    }).fail(function () {

        alert("error 1");
    });
}

$("input[name=BillStartDate]").change(function () {
    BillsList.ajax.reload(null, false);
});

$("input[name=BillEndDate]").change(function () {
    BillsList.ajax.reload(null, false);
});

//function ViewPayments_(parameter) {
//    var token = $('[name=__RequestVerificationToken]').val();
//    _Payments_list_ = $('#_Payments_list_').DataTable({
//        "bServerSide": true,
//        "sAjaxSource": "/Bill/ViewPayments_/",
//        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
//            oSettings.jqXHR = $.ajax({
//                "dataType": 'json',
//                "type": "POST",
//                "url": sSource,
//                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=PurchaseStartDate]').val(), EndDate: $('input[name=PurchaseEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[55].value }) },
//                "success": fnCallback
//            });
//        },
//        responsive: {
//            details: false
//        },
//        "columns": [
//            { data: "Bill_No" },
//            { data: "Total_Amount" },
//            { data: "Paid_Amount" },
//            { data: "Balance_Amount" },
//            { data: "Date" },
//            { data: "Time" },
//        ]
//    });
//}

$("._View_Payments_").click(function () {
    //var token = $('[name=__RequestVerificationToken]').val();
    //var billid = $("#billid").val();
    window.location.href = '/Bill/ViewPayments/';
    //$.ajax({
    //    url: '/bill/ViewPayments/',
    //    type: "POST",
    //    data: { __RequestVerificationToken: token },
    //    datatype: 'json',
    //    ContentType: 'application/json; charset=utf-8'
    //}).done(function (resp) {
    //    console.log(resp);
    //    //ViewPayments_(billid);

    //    $('#_Error_Message_Display_ > span').html('');
    //    $('#_Error_Message_Display_').slideUp("slow");

    //}).fail(function () {




    //});
});