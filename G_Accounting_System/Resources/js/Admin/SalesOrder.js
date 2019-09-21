var Activity_Data = new Array();
var Sales_Order_Total_Amount = 0;
var Sales_Order_Item_Msrmnt_Unit = "";
$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if ((window.location.pathname.toString().toLowerCase()) == "/salesorder/index") {
        GetAllSalesOrdersList(null);
    }
    else if (url.includes("/salesorder/soinvoice/")) {
        SOInvoice(0);
        getSaleReturnsofSO();
    }

});

$("#_Add_New_Sales_Order_btn_").click(function () {
    $("#newSaleorder_id").val("0");
    $("#newSaleorderdetail_id").val("0");
    AddFormSales();
});

function AddFormSales() {
    $(this).attr("disabled", true);
    $('._Add_New_SalesOrder_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/SalesOrder/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_SalesOrder_Form_').append(resp);
            $('.nav-tabs a[href="#s-general-dtls"]').tab('show');
            $('._Add_New_SalesOrder_Form_').slideDown("slow");
            $('#get_searched_items').focus();
            $("#_Add_New_Sales_Order_Form_Save_").attr("disabled", false);
            $("#_Add_New_Sales_Order_Form_Draft_").attr("disabled", false);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_SalesOrder_Form_').slideUp("slow");
            $('._Add_New_SalesOrder_Form_').html("");
            $("#_Add_New_Sales_Order_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function GetAllSalesOrdersList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    SalesOrderList = $('#salesorder_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No sales orders available"
        },
        "sAjaxSource": "/SalesOrder/GetAllSalesOrders",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=SalesStartDate]').val(), EndDate: $('input[name=SalesEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[65].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/SalesOrder/SOInvoice/" + data.SalesOrder_id + "'>" + data.SaleOrderNo + "</a>";
                }
            },
            { data: "Customer_Name" },
            { data: "TotalItems" },
            { data: "SO_Total_Amount" },
            { data: "SO_DateTime" },
            { data: "SO_Status" },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.SO_Invoice_Status === 'Invoiced') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" checked disabled>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" disabled>';
                    }
                    return data;
                }
            },
            {
                data: function (data, type, dataToSet) {
                    if (data.SO_Package_Status === '1' || data.SO_Package_Status === 'True') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" checked disabled>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" disabled>';
                    }
                    return data;
                }

            },
            {
                data: function (data, type, dataToSet) {
                    if (data.SO_Shipment_Status === '1' || data.SO_Shipment_Status === 'True') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" checked disabled>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" disabled>';
                    }
                    return data;
                }

            },
            {
                data: function (data, type, dataToSet) {
                    if (data.SO_Invoice_Status === "Invoiced" || (data.SO_Package_Status === '1' || data.SO_Package_Status === 'True') || (data.SO_Shipment_Status === '1' || data.SO_Shipment_Status === 'True') || data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditSaleOrder(" + data.SalesOrder_id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepSaleOrder(" + data.SalesOrder_id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditSaleOrder(" + data.SalesOrder_id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepSaleOrder(" + data.SalesOrder_id + ")' title='Visibility'></span>";
                    }
                }

            },
            {
                data: function (data, type, dataToSet) {

                    if (data.SO_Invoice_Status === "Invoiced" || (data.SO_Package_Status === '1' || data.SO_Package_Status === 'True') || (data.SO_Shipment_Status === '1' || data.SO_Shipment_Status === 'True') || data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkSalesDel" data_value_id=' + data.SalesOrder_id + ' data_value_SaleOrderNo=' + data.SaleOrderNo + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkSalesDel" data_value_id=' + data.SalesOrder_id + ' data_value_SaleOrderNo=' + data.SaleOrderNo + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

function UpdatepSaleOrder(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/SalesOrder/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            SalesOrderList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Sale Order Profile Visibility Updated Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);

        } else {
            $('#_Error_Message_Display_ > p').html(resp);
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function EditSaleOrder(SalesOrder_id) {

    $("#newSaleorder_id").val(SalesOrder_id);
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/SalesOrder/SOInvoice/' + SalesOrder_id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            if ((resp.SO_Package_Status === "True" || resp.SO_Package_Status === "1") || (resp.SO_Shipment_Status === "True" || resp.SO_Shipment_Status === "1") || resp.SO_Invoice_Status === "Invoiced") {
                $('#_Error_Message_Display_ > p').html("This Sales Order cannot be edited as Invoice/Packaging/Shipment has been created");
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
            else {

                AddFormSales();
                if (resp.SO_Status == "Confirm") {
                    $("#_Add_New_Sales_Order_Form_Save_").show();
                    $("#_Add_New_Sales_Order_Form_Save_").html("Proceed");
                    $("#_Add_New_Sales_Order_Form_Draft_ ").hide();
                } else if (resp.SO_Status == "Draft") {
                    $("#_Add_New_Sales_Order_Form_Save_").hide();
                    $("#_Add_New_Sales_Order_Form_Draft_").html("Proceed");
                    $("#_Add_New_Sales_Order_Form_Draft_ ").show();
                }
                var Sales_Order_Total_Amount = 0;
                $.each(resp.SOItems, function (i, item) {

                    var rows = "<tr class='filledField' data-value-item='" + item.ItemId + "' data-value-customer='" + item.Customer_id + "' data-value-sdid='" + item.sdid + "' data-value-qty='" + item.ItemQty + "' data-value-unit-price='" + item.PriceUnit + "' data-value-total='" + item.ItemQty * item.PriceUnit + "'>" +
                        "<td class='table_content_vertical_align_' data-value-item='" + item.ItemId + "'>" + item.ItemName + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-customer='" + item.Customer_id + "'>" + item.Customer_Name + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-qty='" + item.ItemQty + "'>" + item.ItemQty + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-msrmnt-unit='" + item.MsrmntUnit + "'> " + item.MsrmntUnit + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-unit-price='" + item.PriceUnit + "'>" + item.PriceUnit + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-total='" + item.ItemQty * item.PriceUnit + "'>" + item.ItemQty * item.PriceUnit + "</td>" +

                        "<td class='table_content_horizontal_align_'><span id='Edit_SaleItems' data-value-sdid='" + item.sdid + "' data-value-item='" + item.ItemId + "' " +
                        "data-value-customer='" + item.Customer_id + "' data-value-qty='" + item.ItemQty + "' data-value-msrmnt-unit='" + item.MsrmntUnit + "' " +
                        "data-value-unit-price='" + item.PriceUnit + "' data-value-total='" + item.ItemQty * item.PriceUnit + "'  class='fa fa-pencil-square-o EditSaleItems'></span>" +

                        "<span class='fa fa-trash' data-value-sdid='" + item.sdid + "' data-value-customer='" + item.Customer_id + "' data-value-total='" + item.ItemQty * item.PriceUnit + "' onclick='removeTrFrmTblSO(this)' title='Delete'></span></td>" +
                        "</tr>";
                    Sales_Order_Total_Amount = Sales_Order_Total_Amount + (item.ItemQty * item.PriceUnit);
                    $('#_tbl_New_SaleOrder_Body_').append(rows);
                });
                $('#_SalesOrder_Items_Total_').html('Total = ' + Sales_Order_Total_Amount);
            }
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_SalesOrder_Form_').slideUp("slow");
            $('._Add_New_SalesOrder_Form_').html("");
            $("#_Add_New_Sales_Order_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });

}

$(document).on('click', '.EditSaleItems', function (event) {
    $('#Add_New_SalesOrder_Item').val("Update");
    $("select[name=new_item_Name]").val($(this).data('value-item')).change();
    $("select[name=new_customer_Name]").val($(this).data('value-customer')).change();
    $("input[name=new_item_Quantity]").val($(this).data('value-qty'));
    $("input[name=new_item_Msrmnt_Unit]").val($(this).data('value-msrmnt-unit'));
    $("input[name=new_item_Unit_Price]").val($(this).data('value-unit-price'));
    $("input[name=new_item_Price_Total]").val($(this).data('value-total'));
    $("#newSaleorderdetail_id").val($(this).data('value-sdid'));
    $("select[name=new_customer_Name]").attr("disabled", true);
});

function removeTrFrmTblSO(elso) {
    console.log(Sales_Order_Total_Amount + " ------ " + $(elso).closest('tr').find("td:eq(5)").attr('data-value-total') + " -------- " + $(elso).data('value-total'));
    Sales_Order_Total_Amount = Sales_Order_Total_Amount - $(elso).closest('tr').find("td:eq(5)").attr('data-value-total');
    $('#_SalesOrder_Items_Total_').html('Total = ' + Sales_Order_Total_Amount);
    $("select[name=new_customer_Name]").val($(elso).data('value-customer')).change();
    $("select[name=new_customer_Name]").attr("disabled", true);
    var sdid = $(elso).data('value-sdid');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/SalesOrder/DeleteItemFromSaleOrder/',
        type: "POST",
        data: {
            __RequestVerificationToken: token, sdid: sdid
        },
    }).done(function (resp) {
        if (resp.length == 0) {

            $(elso).closest('tr').remove();

        }
        else {
            $('#_Error_Message_Display_ > p').html('Network Error');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');

        }

    }).fail(function () {

    });
}

$("input[name=SalesStartDate]").change(function () {
    SalesOrderList.ajax.reload(null, false);
});

$("input[name=SalesEndDate]").change(function () {
    SalesOrderList.ajax.reload(null, false);
});

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

var tablebind = 0;
function SOInvoice(tablebind) {
    var token = $('[name=__RequestVerificationToken]').val();
    var id = window.location.pathname.toString().toLowerCase().split("/soinvoice/")[1];

    $("#SO_id").val(id);
    $.ajax({
        url: '/SalesOrder/SOInvoice/' + id,
        type: "POST",
        data: {
            __RequestVerificationToken: token
        },
        //datatype: 'json',
        //ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

        $("#SOno").html(resp.SaleOrderNo);
        $("#PKGSOno").html(resp.SaleOrderNo);
        $("#SO_inv_id").val(resp.SO_Invoice_id);
        $("#SO_Customer_id").val(resp.SOItems[0].Customer_id);
        $("#ISOnameto").html(resp.SOItems[0].Customer_Name);
        $("#ISOaddressto").html(resp.SOItems[0].Customer_Address);
        $("#PKGnameto").html(resp.SOItems[0].Customer_Name);
        $("#PKGaddressto").html(resp.SOItems[0].Customer_Address);
        $("#IStatus").html(resp.SO_Status);
        $("#PKGSOStatus").html(resp.SO_Status);
        $("#IinvoiceStatus").html(resp.SO_Invoice_Status);
        $("#PKGinvoiceStatus").html(resp.SO_Invoice_Status);
        $("#SO_invno").html(resp.Invoice_No);

        $("#so__CustomerName").html(resp.SOItems[0].Customer_Name);
        $("#so__TotalAmount").html(resp.SO_Total_Amount);
        $("#so__TotalItems").html(resp.SOItems.length);
        $("#so__Status").html(resp.SO_Status);

        if (resp.SO_Package_Status === "False") {
            $("#Ipackagestatus").html("Not Packaged");
        } else {
            $("#Ipackagestatus").html("Packaged");
            $("#PKGFpackagestatus").html("Packaged");
        }
        if (resp.SO_Shipment_Status === "False") {
            $("#IshipStatus").html("Not Shipped");
        } else {
            $("#IshipStatus").html("Shipped");
            $("#PKGshipStatus").html("Shipped");
        }
        //if (resp.DeliveryStatus == null || resp.DeliveryStatus == "NULL" || resp.DeliveryStatus == "") {
        //    $("#IDeliveryStatus").html("N/A");
        //} else {
        //    $("#IDeliveryStatus").html(resp.DeliveryStatus);
        //}
        $("#Isubtotal").html(resp.SO_Total_Amount);
        $("#Itotalpackagescost").html(resp.TotalPackageCost);
        $("#Itotalshipmentcost").html(resp.TotalShipmentCost);
        $("#Itotal").html(parseFloat(resp.SO_Total_Amount) + parseFloat(resp.TotalPackageCost) + parseFloat(resp.TotalShipmentCost));
        $('input[name=new_customer_name]').val($("#ISOnameto").html());
        if (tablebind == 0) {
            $.each(resp.SOItems, function (i, item) {
                var rows = "<tr>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty + ' x ' + item.PriceUnit + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.TotalItems + "</td>" +
                    "</tr>";
                $('#Iitemslistbody').append(rows);
            });
        }

        if (resp.SO_Invoice_Status === "Invoiced") {
            $('#invoicedata').html($('#saleorderdata').html());
            $("#SOInvoiceno").html(resp.Invoice_No);
            getinvoicePaymentData();
            $('._SO_Invoice_Payment_Save_').removeClass("hidden");
            $("#ddinvoiceOption").find("#optioninv").addClass("hidden");
            $('.nav-tabs-custom a[href="#invoice"]').tab().show();
        }
        else {
            $('.nav-tabs-custom a[href="#invoice"]').tab().hide();
            $('.nav-tabs-custom a[href="#payments"]').tab().hide();
            //$("#ddinvoiceOption").find("#optionpkg").removeClass("hidden");
        }
        if (resp.SO_Package_Status == "1" || resp.SO_Package_Status == "True") {
            $('.nav-tabs-custom a[href="#packages"]').tab().show();
            //$("#ddinvoiceOption").find("#optionpkg").addClass("hidden");
            getPackagesofSO();
            getPackageItemDetails(0);
        } else {
            $('.nav-tabs-custom a[href="#packages"]').tab().hide();
            $("#ddinvoiceOption").find("#optionpkg").removeClass("hidden");
        }
        if (resp.SO_Shipment_Status == "1" || resp.SO_Shipment_Status == "True") {
            $("#ddinvoiceOption").find("#optionsalereturn").removeClass("hidden");
            //$("#ddinvoiceOption").find("#optionship").addClass("hidden");
        } else {
            $("#ddinvoiceOption").find("#optionsalereturn").addClass("hidden");
        }
        var ActivityType_id = $("#SO_id").val();
        var ActivityType = "Sales Order";
        Activities(ActivityType_id, ActivityType);
        PaymentModesDropdown();
        if (getUrlParameter('Invoice') == 'true') {
            $('.nav-tabs-custom a[href="#invoice"]').tab('show');
        } else if (getUrlParameter('Payments') == 'true') {
            $('.nav-tabs-custom a[href="#payments"]').tab('show');
        }
    }).fail(function () {

        alert("error 1");
    });
}
var SOStatus = "";

(function () {
    var flag = false;
    var OptionSelect = function () {
        var selectedText = $("#ddinvoiceOption").find("option:selected").text();
        SOStatus = $("#IStatus").html();

        if (selectedText === "Package") {
            ifCreatePackage();
        } else if (selectedText === "Shipment") {
            ifCreateShipment();
        } else if (selectedText === "Invoice") {
            ifCreateInvoice(SOStatus);
        }
        else if (selectedText === "Sale Return") {
            ifCreateSaleReturn();
        }
    }

    $("#ddinvoiceOption").click(function () {
        if (flag) {
            OptionSelect();
        }
        flag = !flag;
    });

    //$("#ddinvoiceOption").focusout(function () {
    //    flag = false;
    //});
}());

function ifCreateInvoice(SOStatus) {
    var SO_id = $("#SO_id").val();
    switch (SOStatus) {
        case "Draft":
            $.confirm({
                closeIcon: true,
                closeIconClass: 'fa fa-close',
                type: 'purple',
                //boxWidth: '500px',
                theme: 'modern',
                useBootstrap: false,
                typeAnimated: true,
                alignMiddle: false,
                animationBounce: 1.5,
                title: 'Do you want to continue?',
                content: 'Sales Order status will be changed to ' + 'Confirmed' + ' once package/shipment/invoice is created from Draft status.',

                buttons: {
                    confirm: function () {
                        SOBill_partialView(SO_id);
                    },
                    cancel: function () {

                    },
                }
            });
            break;
        case "Confirm":
            SOBill_partialView(SO_id);
            break;
        default:
        // code block
    }
}

function SOBill_partialView(SO_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.confirm({
        closeIcon: true,
        closeIconClass: 'fa fa-close',
        type: 'purple',
        //boxWidth: '500px',
        theme: 'modern',
        useBootstrap: false,
        typeAnimated: true,
        alignMiddle: false,
        animationBounce: 1.5,
        title: 'New Invoice',
        content: '',
        onContentReady: function () {
            var self = this;
            $.ajax({
                url: '/SalesOrder/Bill/',
                type: "POST",
                data: { __RequestVerificationToken: token, SO_id },
            }).done(function (resp) {
                self.setContentAppend(resp);
                $('input[name=customerName]').val($("#ISOnameto").html());
                $('input[name=BsalesorderNo]').val($("#SOno").html());
                $("#Bsubtotal").html($("#Isubtotal").html());
                $("#Btotal").html($("#Itotal").html());
                $('#tblBitemList').html($("#Itblitemslist").html());
            }).fail(function () {

                $('#_Error_Message_Display_ > p').html("Error!");
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            });
        },
        buttons: {
            confirm: function () {
                if (!$("input[name=BinvoiceNo]").val()) {
                    $.alert('Please Enter Invoice No');
                    return false;
                }
                else if (!$("input[name=BinvoiceDate]").val()) {
                    $.alert('Please Enter Invoice Date');
                    return false;
                }
                else if (!$("input[name=BinoicedueDate]").val()) {
                    $.alert('Please Enter Invoice Due Date');
                    return false;
                }
                else {
                    SaveNewInvoice();
                }
            },
            cancel: function () {
            },
        }
    });
}

function SaveNewInvoice() {
    var token = $('[name=__RequestVerificationToken]').val();
    var Invoice_data = {
        SalesOrder_id: $("#SO_id").val(),
        Customer_id: $("#SO_Customer_id").val(),
        Invoice_No: $("input[name=BinvoiceNo]").val(),
        Invoice_Amount: $("#Bsubtotal").html(),
        Invoice_Status: 'Draft',
        InvoiceDateTime: $("input[name=BinvoiceDate]").val(),
        InvoiceDueDate: $("input[name=BinoicedueDate]").val(),
        Amount_Paid: "0.00",
        Balance_Amount: $("#Bsubtotal").html(),
    }
    var SO_data = {
        SalesOrder_id: $("#SO_id").val(),
        SO_Invoice_Status: "Invoiced",
        SO_Status: "Closed",
    }
    $.ajax({
        url: '/SalesOrder/InsertInvoice/',
        type: "POST",
        data: { __RequestVerificationToken: token, "InvoiceData": JSON.stringify(Invoice_data), "SOData": JSON.stringify(SO_data) },
    }).done(function (resp) {
        if (resp.Invoice.pFlag == "1") {
            $("#SO_inv_id").val(resp.Invoice.pInvoice_id_Output);
            $('#_Success_Message_Display_ > p').html(resp.Invoice.pDesc);
            $('#_Success_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
            $('.nav-tabs-custom a[href="#invoice"]').tab('show');
            SOInvoice(1);
            PaymentHistory();

        }
        else {
            $('#_Error_Message_Display_ > p').html(resp.Invoice.pDesc);
            $('#_Error_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);
        }

    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });

}

$("._SO_Invoice_Payment_Save_").click(function () {
    $('.nav-tabs-custom a[href="#payments"]').tab().show();
    $('.nav-tabs-custom a[href="#payments"]').tab('show');
});

function getinvoicePaymentData() {
    var token = $('[name=__RequestVerificationToken]').val();
    var id = $("#SO_id").val();
    $.ajax({
        url: '/SalesOrder/SOInvoicePayment/',
        type: "POST",
        data: {
            __RequestVerificationToken: token, id
        },
    }).done(function (resp) {
        $('input[name=new_payment_made]').val(resp.Invoice.Balance_Amount);
        $('#SO_inv_Total_Amount').val(resp.Invoice.Balance_Amount);
        if (resp.Invoice.Balance_Amount == "0") {
            $('#_New_SOPayment_Save_').attr("disabled", true);
            //$("#ddinvoiceOption").find("#optionsalereturn").addClass("hidden");
        }
        else {
            $('#_New_SOPayment_Save_').attr("disabled", false);
            //$("#ddinvoiceOption").find("#optionsalereturn").removeClass("hidden");
        }
        PaymentHistory();
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

var Total;
var Paid;
var Balance;
var StockStatus = "";
$("#_New_SOPayment_Save_").click(function () {
    var Invoicestatus = "";
    var token = $('[name=__RequestVerificationToken]').val();
    Total = $('#SO_inv_Total_Amount').val();
    Paid = $("input[name=new_payment_made]").val();
    Balance = Total - Paid;
    var Invoice_Payment_data = {
        Invoice_id: $("#SO_inv_id").val(),
        Payment_Mode: $("select[name=get_payment_mode] option:checked").val(),
        Payment_Date: $("input[name=new_payment_date]").val(),
        Total_Amount: $("#SO_inv_Total_Amount").val(),
        Paid_Amount: $("input[name=new_payment_made]").val(),
        Balance_Amount: Balance,
    }
    if (Balance == "0") {
        Invoicestatus = "Paid";
    }
    else {
        Invoicestatus = "Draft";
    }
    var Invoice_data = {
        id: $("#SO_inv_id").val(),
        SalesOrder_id: $("#SO_id").val(),
        Invoice_Status: Invoicestatus,
    }
    $.ajax({
        url: '/SalesOrder/InsertSO_Payments/',
        type: "POST",
        data: { __RequestVerificationToken: token, "InvoicePaymentData": JSON.stringify(Invoice_Payment_data), "InvoiceStatus": JSON.stringify(Invoice_data) },
    }).done(function (resp) {
        if (resp.pFlag == "1") {
            var id = $("#SO_id").val();
            StockStatus = "Acc Stock";
            getSOItemsForStockOnPayment(id);
            $('#_Success_Message_Display_ > p').html("Payment created Successfully");
            $('#_Success_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);

        }
        SOInvoice(1);
        $('.nav-tabs-custom a[href="#payments"]').tab('show');
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
});

function PaymentHistory() {

    var token = $('[name=__RequestVerificationToken]').val();
    var id = $("#SO_inv_id").val();
    $.ajax({
        url: '/SalesOrder/SelectSO_IvnvoicePaymentHistory/',
        type: "POST",
        data: {
            __RequestVerificationToken: token, id
        },

    }).done(function (resp) {
        $("#payment_history_title").html('<b>Payment Histroy</b>');
        $("#_tbl_Payments_List_Body_").empty();
        $.each(resp, function (i, item) {
            var rows = "<tr>" +
                "<td class='table_content_vertical_align_'>" + item.Invoice_id + "</td>" +
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

function ifCreatePackage() {
    var SO_id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.confirm({
        closeIcon: true,
        closeIconClass: 'fa fa-close',
        type: 'purple',
        //boxWidth: '500px',
        theme: 'modern',
        useBootstrap: false,
        typeAnimated: true,
        alignMiddle: false,
        animationBounce: 1.5,
        title: 'New Package',
        content: '',
        onContentReady: function () {
            var self = this;
            $.ajax({
                url: '/SalesOrder/Package/',
                type: "POST",
                data: { __RequestVerificationToken: token, SO_id },
            }).done(function (resp) {
                self.setContentAppend(resp);
                getPackageItems();
            }).fail(function () {

                $('#_Error_Message_Display_ > p').html("Error!");
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            });

        },
        buttons: {
            confirm: function () {
                getCheckedItemsForPackage(SO_id);
            },
            cancel: function () {

            },
        }
    });
}

function checkAllItems() {
    $('.checkAll').on('change', function () {
        $(this).closest('table').find('tbody :checkbox')
            .prop('checked', this.checked)
            .closest('tr').toggleClass('selected', this.checked);
    });

    $('tbody :checkbox').on('change', function () {
        $(this).closest('tr').toggleClass('selected', this.checked); //Classe de seleção na row

        $(this).closest('table').find('.checkAll').prop('checked', ($(this).closest('table').find('tbody :checkbox:checked').length == $(this).closest('table').find('tbody :checkbox').length)); //Tira / coloca a seleção no .checkAll
    });
}

function getPackageItems() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SOInvoice/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $('input[name=PkgsalesorderNo]').val($("#SOno").html());
        $.each(resp.SOItems, function (i, item) {
            if (resp.SOItems[i].Packed_Qty == "0") {
                var rows = "<tr data-value-item='" + item.ItemId + "', data-value-qty='" + item.ItemQty + "', data-value-pkg_qty='" + item.ItemQty + "' data-value-itemunitprice='" + item.PriceUnit + "'>" +
                    "<td class='table_content_vertical_align_'><input type='checkbox' class='chkbox' onchange='checkAllItems();' name='check' /></td>" +
                    "<td  class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                    "</tr>";
                $('#Pkgitemslistbody').append(rows);
            }
        });
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function SaveNewPackage() {
    if (!$("input[name=PkginvoiceDate]").val()) {
        $.alert('Please Enter Package Date');
        return false;
    }
    var token = $('[name=__RequestVerificationToken]').val();
    var SO_data = {
        SalesOrder_id: $("#SO_id").val(),
        SO_Package_Status: "1",
        SO_Status: "Confirm",
    }
    $.ajax({
        url: '/SalesOrder/InsertSOPackage/',
        type: "POST",
        data: { __RequestVerificationToken: token, "Packagedata": JSON.stringify(checked_items), "SOData": JSON.stringify(SO_data), },
    }).done(function (resp) {
        $('#_Success_Message_Display_ > p').html("Package Created Successfully");
        $('#_Success_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
        $('.nav-tabs-custom a[href="#packages"]').tab('show');

        SOInvoice(1);

    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    })
}

var checked_items;
function getCheckedItemsForPackage(SO_id) {
    var check = "";
    var data_value_item = "";
    var data_value_qty = "";
    var data_value_pkg_qty = "";
    var data_value_itemunitprice = "";
    checked_items = new Array();
    $('#Pkgitemslistbody tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkbox')).is(':checked')) {
            data_value_item = $(this).attr('data-value-item');
            data_value_qty = $(this).attr('data-value-qty');
            data_value_pkg_qty = $(this).attr('data-value-pkg_qty');
            data_value_itemunitprice = $(this).attr('data-value-itemunitprice');
            var data = { Item_id: data_value_item, unitprice: data_value_itemunitprice, Qty: data_value_qty, Packed_Qty: data_value_pkg_qty, SalesOrder_id: SO_id, Package_Date: $('input[name=PkginvoiceDate]').val(), PackageStatus: "Not Shipped", PackageCost: $('input[name=Packagingcost]').val() }
            checked_items.push(data);
        }
    });
    if (checked_items.length > 0) {
        SaveNewPackage();
    } else {
        $('#_Error_Message_Display_ p').html("There are no Items selected for Packing");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
}

function getPackagesofSO() {
    var SHipment_Status = "";
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SelectPackagesForSO/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $('#_tbl_Pkg_List_Body_').empty();
        $.each(resp, function (i, item) {
            var rows = "<tr>" +
                "<td  class='table_content_vertical_align_'>" + item.Package_No + "</td>" +
                "<td  class='table_content_vertical_align_'>" + item.PackageCost + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.Package_Date + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.PackageStatus + "</td>" +
                "<td class='table_content_vertical_align_' style='text-align:left'><button type='button' data-value-PackageStatus='" + item.PackageStatus + "' data-value-packageid='" + item.Package_id + "' class='btn btn-info' id='_SO_Shipment_Mark_Deliver_'></button></td>"
            "</tr>";
            $('#_tbl_Pkg_List_Body_').append(rows);
        });

        $('#_tbl_Pkg_List_Body_ tr').each(function (indexoftr, tr) {
            var PKGStatus = $(this).find($('td')).find($('[id*="_SO_Shipment_Mark_Deliver_"]')).attr("data-value-PackageStatus");
            if (PKGStatus == "Not Shipped") {
                $(this).find($('td')).find($('[id*="_SO_Shipment_Mark_Deliver_"]')).hide();
            }
            else if (PKGStatus == "Shipped") {
                $(this).find($('td')).find($('[id*="_SO_Shipment_Mark_Deliver_"]')).show();
                $(this).find($('td')).find($('[id*="_SO_Shipment_Mark_Deliver_"]')).html('Mark as Delivered');
            }
            else if (PKGStatus == "Delivered") {
                $(this).find($('td')).find($('[id*="_SO_Shipment_Mark_Deliver_"]')).show();
                $(this).find($('td')).find($('[id*="_SO_Shipment_Mark_Deliver_"]')).html('Mark as Undelivered');
            }
        });

        $('[id*="_SO_Shipment_Mark_"]').click(function () {
            var token = $('[name=__RequestVerificationToken]').val();
            Package_id = $(this).attr("data-value-packageid");
            var PKGStatus = $(this).attr("data-value-PackageStatus");

            if (PKGStatus == "Shipped") {
                SHipment_Status = "Delivered";
            }

            else if (PKGStatus == "Delivered") {
                SHipment_Status = "Shipped";
            }

            var Shipment_data = {
                Package_id: Package_id,
                Shipment_Status: SHipment_Status,
            }
            $.ajax({
                url: '/SalesOrder/UpdateShipmentDeliver/',
                type: "POST",
                data: { __RequestVerificationToken: token, "Shipmentdata": JSON.stringify(Shipment_data) },
            }).done(function (resp) {
                if (SHipment_Status == "Shipped") {
                    $('#_Success_Message_Display_ > p').html("Marked as Undelivered Successfully");
                }
                else {
                    $('#_Success_Message_Display_ > p').html("Marked as " + SHipment_Status + " Successfully");
                }
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                SOInvoice(1);
                $('.nav-tabs-custom a[href="#packages"]').tab('show');
            }).fail(function () {

                $('#_Error_Message_Display_ > p').html("Error!");
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            });
        });

    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function getPackageItemDetails() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SelectPackagedItemsBySOid/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $('#IiPackedtemslistbody').empty();
        $.each(resp.packageditem, function (i, item) {
            var rows = "<tr data-value-Item_id='" + item.ItemId + "',data-value-SalesOrder_id='" + item.SalesOrder_id + "'>" +
                "<td class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.Packed_Qty + ' Packed' + '</br>' + item.InvoicedQty + ' Invoiced' + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.PriceUnit + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty * item.PriceUnit + "</td>" +
                "</tr>";
            $('#IiPackedtemslistbody').append(rows);
            //$("#pkgcost").html(resp.Packagesdata.PackageCost);
            //$("#pkgsubtotal").html($("#Isubtotal").html());
            //$("#pkgtotal").html($("#Itotal").html());
        });


    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function ifCreateShipment() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SelectSObyID/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        if (resp.SO_Package_Status == "0" || resp.SO_Package_Status == "False") {
            $.confirm({
                closeIcon: true,
                closeIconClass: 'fa fa-close',
                type: 'purple',
                //boxWidth: '500px',
                theme: 'modern',
                useBootstrap: false,
                typeAnimated: true,
                alignMiddle: false,
                animationBounce: 1.5,
                title: '',
                content: 'To create shipment, package is needed. No packages/unshipped packages available.',
                buttons: {
                    'Create Package': function () {
                        ifCreatePackage();
                    },
                    cancel: function () {

                    },
                }
            });
        }
        else {
            $.confirm({
                closeIcon: true,
                closeIconClass: 'fa fa-close',
                type: 'purple',
                //boxWidth: '500px',
                theme: 'modern',
                useBootstrap: false,
                typeAnimated: true,
                alignMiddle: false,
                animationBounce: 1.5,
                title: 'New Shipment',
                content: '',
                onContentReady: function () {
                    var self = this;
                    $.ajax({
                        url: '/SalesOrder/Shipment/',
                        type: "POST",
                        data: { __RequestVerificationToken: token, id },
                    }).done(function (resp) {
                        self.setContentAppend(resp);
                        getPackagesFroShipment();
                    }).fail(function () {

                        $('#_Error_Message_Display_ > p').html("Error!");
                        $('#_Error_Message_Display_').slideDown("slow");
                        setTimeout(function () {
                            $('#_Error_Message_Display_').slideUp("slow");
                        }, 5000);
                    });

                },
                buttons: {
                    confirm: function () {
                        if (!$("input[name=ShimentDate]").val()) {
                            $.alert('Please Enter Shipment Date');
                            return false;
                        }
                        else {
                            getCheckedPackagesForShipment(SO_id);
                        }
                    },
                    cancel: function () {

                    },
                }
            });
        }
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function getPackagesFroShipment() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SelectPackagesForShipment/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $('input[name=ShpsalesorderNo]').val($("#SOno").html());

        $.each(resp, function (i, item) {
            if (resp[i].PackageStatus == "Not Shipped") {
                var rows = "<tr data-value-SalesOrder_id='" + item.SalesOrder_id + "', data-value-Package_id='" + item.Package_id + "'>" +
                    "<td class='table_content_vertical_align_'><input type='checkbox' class='chkShipmentbox' onchange='checkAllPackages();' name='checkshipment' /></td>" +
                    "<td  class='table_content_vertical_align_' style='text-align:left'>" + item.Package_No + "</td>" +
                    "</tr>";
                $('#ShipPackagelistbody').append(rows);
            }
        });
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function checkAllPackages() {
    $('.checkAllPackages').on('change', function () {
        $(this).closest('table').find('tbody :checkbox')
            .prop('checked', this.checked)
            .closest('tr').toggleClass('selected', this.checked);
    });

    $('tbody :checkbox').on('change', function () {
        $(this).closest('tr').toggleClass('selected', this.checked); //Classe de seleção na row

        $(this).closest('table').find('.checkAllPackages').prop('checked', ($(this).closest('table').find('tbody :checkbox:checked').length == $(this).closest('table').find('tbody :checkbox').length)); //Tira / coloca a seleção no .checkAll
    });
}

var checked_packages;
function getCheckedPackagesForShipment() {
    var SO_id = $("#SO_id").val();
    var data_value_package = "";
    checked_packages = new Array();
    $('#ShipPackagelistbody tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkShipmentbox')).is(':checked')) {
            data_value_package = $(this).attr('data-value-Package_id');
            var data = { Package_id: data_value_package, SaleOrder_id: SO_id, Shipment_Date: $('input[name=ShimentDate]').val(), Shipment_Status: "Shipped", Shipment_Cost: $('input[name=Shipmentcharges]').val() }
            checked_packages.push(data);
        }
    });
    if (checked_packages.length > 0) {
        console.log(checked_packages);
        SaveNewShipments();
    } else {
        $('#_Error_Message_Display_ p').html("There are No Packages for Shipment");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
}

function SaveNewShipments() {
    var token = $('[name=__RequestVerificationToken]').val();
    var SO_data = {
        SalesOrder_id: $("#SO_id").val(),
        SO_Shipment_Status: "1",
        SO_Status: "Confirm",
    }
    $.ajax({
        url: '/SalesOrder/InsertSOShipment/',
        type: "POST",
        data: { __RequestVerificationToken: token, "ShipmentData": JSON.stringify(checked_packages), "SOData": JSON.stringify(SO_data) },
    }).done(function (resp) {
        $('#_Success_Message_Display_ > p').html("Shipment Created Successfully");
        $('#_Success_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
        $('.nav-tabs-custom a[href="#packages"]').tab('show');
        var id = $("#SO_id").val();
        StockStatus = "Physical Stock";
        getSOItemsForStockOnPShipment(id);

        SOInvoice(1);
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    })
}

function checkAllSaleReturnItems() {
    $('.checkSRItemsAll').on('change', function () {
        $(this).closest('table').find('tbody :checkbox')
            .prop('checked', this.checked)
            .closest('tr').toggleClass('selected', this.checked);
    });

    $('tbody :checkbox').on('change', function () {
        $(this).closest('tr').toggleClass('selected', this.checked); //Classe de seleção na row

        $(this).closest('table').find('.checkSRItemsAll').prop('checked', ($(this).closest('table').find('tbody :checkbox:checked').length == $(this).closest('table').find('tbody :checkbox').length)); //Tira / coloca a seleção no .checkAll
    });
}

function ifCreateSaleReturn() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SelectSObyID/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $.confirm({
            closeIcon: true,
            closeIconClass: 'fa fa-close',
            type: 'purple',
            //boxWidth: '500px',
            theme: 'modern',
            useBootstrap: false,
            typeAnimated: true,
            alignMiddle: false,
            title: 'Sale Return',
            animationBounce: 1.5,
            content: '',
            onContentReady: function () {
                var self = this;
                $.ajax({
                    url: '/SalesOrder/SaleReturn/',
                    type: "POST",
                    data: { __RequestVerificationToken: token, id },
                }).done(function (resp) {
                    self.setContentAppend(resp);
                    getItemForSaleReturn();
                }).fail(function () {

                    $('#_Error_Message_Display_ > p').html("Error!");
                    $('#_Error_Message_Display_').slideDown("slow");
                    setTimeout(function () {
                        $('#_Error_Message_Display_').slideUp("slow");
                    }, 5000);
                });

            },
            buttons: {
                confirm: function () {
                    getCheckedItemsForSaleReturn();
                },
                cancel: function () {

                },
            }
        });

    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function getItemForSaleReturn() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/GetItemsForSaleRetrurn/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $('input[name=PkgsalesorderNo]').val($("#SOno").html());
        $.each(resp.SOItems, function (i, item) {
            if (resp.SOItems[i].ReturnQty != resp.SOItems[i].Packed_Qty) {
                var rows = "<tr data-value-item='" + item.ItemId + "', data-value-package_id='" + item.Package_id + "', data-value-Return_Qty='" + item.ItemQty + "', data-value-ReturnQty_Cost='" + item.ItemQty * item.PriceUnit + "'>" +
                    "<td class='table_content_vertical_align_'><input type='checkbox' class='chkSaleReturnbox' onchange='checkAllItems();' name='check' /></td>" +
                    "<td  class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                    "</tr>";
                $('#SAleReturnItemslistbody').append(rows);
            }
        });
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function getCheckedItemsForSaleReturn() {
    var SO_id = $("#SO_id").val();
    var data_value_package = "";
    var data_value_item = "";
    var data_value_Return_Qty = "";
    var data_value_ReturnQty_Cost = "";
    checked_SRItems = new Array();
    $('#SAleReturnItemslistbody tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkSaleReturnbox')).is(':checked')) {
            data_value_package = $(this).attr('data-value-package_id');
            data_value_item = $(this).attr('data-value-item');
            data_value_Return_Qty = $(this).attr('data-value-Return_Qty');
            data_value_ReturnQty_Cost = $(this).attr('data-value-ReturnQty_Cost');
            var data = { Package_id: data_value_package, Item_id: data_value_item, Return_Qty: data_value_Return_Qty, ReturnQty_Cost: data_value_ReturnQty_Cost, SaleOrder_id: SO_id }
            checked_SRItems.push(data);
        }
    });
    if (checked_SRItems.length > 0) {
        SaveNewSaleReturn();
    } else {
        $('#_Error_Message_Display_ p').html("There are No Items selected for Sale Return");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
}

function SaveNewSaleReturn() {
    var SO_id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    var SaleReturn_data = {
        SaleOrder_id: SO_id,
        SaleReturn_Date: $('input[name=SaleReturnDate]').val(),
        SaleReturn_Status: 'Approved',
    }
    $.ajax({
        url: '/SalesOrder/InsertSaleReturn/',
        type: "POST",
        data: { __RequestVerificationToken: token, "SaleReturnItems": JSON.stringify(checked_SRItems), "SaleReturndata": JSON.stringify(SaleReturn_data) },
    }).done(function (resp) {
        $('#_Success_Message_Display_ > p').html("Sale Return Created Successfully");
        $('#_Success_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
        $('.nav-tabs-custom a[href="#salereturn"]').tab().show();
        $('.nav-tabs-custom a[href="#salereturn"]').tab('show');

        getSaleReturnsofSO();
        getSaleReturnedItemsOfSO();
        SOInvoice(1);
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    })
}

function getSaleReturnsofSO() {
    var SHipment_Status = "";
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SelectSaleReturnsForSO/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $('#_tbl_SaleReturn_List_Body_').empty();
        $.each(resp, function (i, item) {
            var rows = "<tr>" +
                "<td class='table_content_vertical_align_'>" + item.SaleReturnNo + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.SaleReturn_Date + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.SaleReturn_Status + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.TotalReturn_Cost + "</td>" +
                "</tr>";
            $('#_tbl_SaleReturn_List_Body_').append(rows);
        });

        if (resp.length != 0) {
            $('.nav-tabs-custom a[href="#salereturn"]').tab().show();
            getSaleReturnedItemsOfSO();
        } else {
            $('.nav-tabs-custom a[href="#salereturn"]').tab().hide();
        }

    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function getSaleReturnedItemsOfSO() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/SelectAllSRItemsSO_id/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        $('#IiSaleReturtemslistbody').empty();
        $.each(resp.SOItems, function (i, item) {
            var rows = "<tr data-value-Item_id='" + item.Item_id + "'>" +
                "<td class='table_content_vertical_align_'>" + item.Item_Name + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.Return_Qty + ' Returned' + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.PriceUnit + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty * item.PriceUnit + "</td>" +
                "</tr>";
            $('#IiSaleReturtemslistbody').append(rows);
        });

        if (resp.length != 0) {
            $('.nav-tabs-custom a[href="#salereturn"]').tab().show();
            $('._SaleRetun_Recieve_Save_').html("Receive");
        } else {
            $('.nav-tabs-custom a[href="#salereturn"]').tab().hide();
        }

    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function getSOItemsForStockOnPayment(id) {

    var url = '/SalesOrder/SelectSOItemsSOid';

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, id },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

        Items_For_Stock = new Array();
        $.each(resp.SOItems, function (index, i) {
            var data = {
                Item_id: i.ItemId,
                Physical_Quantity: i.ItemQty,
                Accounting_Quantity: i.ItemQty,
            }
            Items_For_Stock.push(data);
        });
        if (Items_For_Stock.length > 0) {
            UpdateSOitemStockOnPayment();
        }

    }).fail(function () {
        alert("post error 0");
    })
}

function UpdateSOitemStockOnPayment() {
    var url = '/Stock/UpdateSOitemStockOnPayment';

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "StockData": JSON.stringify(Items_For_Stock) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

    }).fail(function () {
        alert("post error 0");
    })
}

function getSOItemsForStockOnPShipment(id) {

    var url = '/SalesOrder/SelectSOItemsSOid';

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, id },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

        Items_For_Stock = new Array();
        $.each(resp.SOItems, function (index, i) {
            var data = {
                Item_id: i.ItemId,
                Physical_Quantity: i.ItemQty,
                Accounting_Quantity: i.ItemQty,
            }
            Items_For_Stock.push(data);
        });
        if (Items_For_Stock.length > 0) {
            UpdateSOitemStockOnPShipment();
        }

    }).fail(function () {
        alert("post error 0");
    })
}

function UpdateSOitemStockOnPShipment() {
    var url = '/Stock/UpdateSOitemStockOnPShipment';

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "StockData": JSON.stringify(Items_For_Stock) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

    }).fail(function () {
        alert("post error 0");
    })
}

$("._SaleRetun_Recieve_Save_").click(function () {
    RecieveReturnedItems();
});

function RecieveReturnedItems() {
    var id = $("#SO_id").val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.confirm({
        closeIcon: true,
        closeIconClass: 'fa fa-close',
        type: 'purple',
        //boxWidth: '500px',
        theme: 'modern',
        useBootstrap: false,
        typeAnimated: true,
        alignMiddle: false,
        animationBounce: 1.5,
        title: 'Receive',
        content: '',
        onContentReady: function () {
            var self = this;
            $.ajax({
                url: '/SalesOrder/ReturnReceive/',
                type: "POST",
                data: { __RequestVerificationToken: token, id },
            }).done(function (resp) {
                self.setContentAppend(resp);
                $.ajax({
                    url: '/SalesOrder/SelectAllSRItemsSO_id/',
                    type: "POST",
                    data: { __RequestVerificationToken: token, id },
                }).done(function (resp) {
                    $('#SAleReceiveItemslistbody').empty();
                    $.each(resp.SOItems, function (i, item) {
                        if (item.Received_Qty != item.Return_Qty) {
                            var rows = "<tr data-value-Item_id='" + item.Item_id + "', data-value-SO_id='" + item.SaleOrder_id + "', data-value-Package_id='" + item.Package_id + "', data-value-SaleReturn_id='" + item.SaleReturn_id + "', data-value-Return_Qty='" + item.Return_Qty + "'>" +
                                "<td class='table_content_vertical_align_'>" + item.Item_Name + "</td>" +
                                "<td class='table_content_vertical_align_'>" + item.Return_Qty + "</td>" +
                                "<td class='table_content_vertical_align_'>" + item.Received_Qty + "</td>" +
                                "<td class='table_content_vertical_align_'><input type='number' class='form-control' name='new_return_receive_qty'/></td>" +
                                "</tr>";
                            $('#SAleReceiveItemslistbody').append(rows);
                        }
                    });
                }).fail(function () {
                    $('#_Error_Message_Display_ > p').html("Error!");
                    $('#_Error_Message_Display_').slideDown("slow");
                    setTimeout(function () {
                        $('#_Error_Message_Display_').slideUp("slow");
                    }, 5000);
                });
            }).fail(function () {
                $('#_Error_Message_Display_ > p').html("Error!");
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            });

        },
        buttons: {
            confirm: function () {
                getReturnReceiving();
            },
            cancel: function () {

            },
        }
    });
}

var Return_Receiving;
function getReturnReceiving() {
    var data_value_Item_id = "";
    var data_value_Package_id = "";
    var data_value_SO_id = "";
    var data_value_SaleReturn_id = "";
    Return_Receiving = new Array();
    $('#SAleReceiveItemslistbody tr').each(function (indexoftr, tr) {
        data_value_Item_id = $(this).attr('data-value-Item_id');
        data_value_SO_id = $(this).attr('data-value-SO_id');
        data_value_Package_id = $(this).attr('data-value-Package_id');
        data_value_SaleReturn_id = $(this).attr('data-value-SaleReturn_id');
        var recqty = $(this).find($('input[name = new_return_receive_qty]')).val();
        var returnQty = $(this).attr('data-value-Return_Qty');
        var data = {
            Package_id: data_value_Package_id,
            SaleReturn_id: data_value_SaleReturn_id,
            SaleOrder_id: data_value_SO_id,
            Item_id: data_value_Item_id,
            Return_Qty: returnQty,
            Received_Qty: recqty,
        }
        Return_Receiving.push(data);
    });
    if (Return_Receiving.length > 0) {
        saveReturnReceivingQty();
    } else {
        $('#_Error_Message_Display_').html("There are no Returned items for Receiving");
        $('#_Error_Message_Display_').slideDown();
    }
}

function saveReturnReceivingQty() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/SalesOrder/ReturnReceivingQty/',
        type: "POST",
        data: { __RequestVerificationToken: token, "ReturnReceivingdata": JSON.stringify(Return_Receiving) },
    }).done(function (resp) {
        $('#_Success_Message_Display_ > p').html("Items Received Successfully");
        $('#_Success_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
        $('.nav-tabs-custom a[href="#salereturn"]').tab().show();

        getSaleReturnsofSO();
        getSaleReturnedItemsOfSO();
        SOInvoice(1);
    }).fail(function () {

        $('#_Error_Message_Display_ > p').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    })
}

$(".ListSaleorderFilter").click(function () {
    SalesOrderList.destroy();
    var filtertype = $(this).text();
    GetAllSalesOrdersList(filtertype);
});

$(document).on("change", ".chkSalesDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteSales').show();
    }
    else {
        $('#_Send_request_To_DeleteSales').css('display', 'none');
    }
});

var checked_Sales;
function getCheckedSalestoDel() {
    var data_value_id = "";
    var data_value_SaleOrderNo = "";

    checked_Sales = new Array();

    $('#_tbl_SalesOrder_List_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkSalesDel')).is(':checked')) {
            data_value_id = $(this).find($('td')).find($('.chkSalesDel')).attr('data_value_id');
            data_value_SaleOrderNo = $(this).find($('td')).find($('.chkSalesDel')).attr('data_value_SaleOrderNo');
            var data = { SalesOrder_id: data_value_id, SaleOrderNo: data_value_SaleOrderNo, Delete_Status: "Requested" }
            checked_Sales.push(data);
        }
    });
    if (checked_Sales.length > 0) {
        SendRequestToDelSales();
    } else {
        $('#_Error_Message_Display_').html("There are No Sales to Delete");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteSales").click(function () {
    getCheckedSalestoDel();
});

function SendRequestToDelSales() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/SalesOrder/SendRequestToDelSales/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteSalesData": JSON.stringify(checked_Sales)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {

            $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            SalesOrderList.ajax.reload(null, false);
            setTimeout(function () { $('#_Success_Message_Display_').slideUp("slow"); }, 5000);
            $('#_Send_request_To_DeleteSales').css('display', 'none');
        }
        else {
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {

    });
}

$("#_Save_New_Customer_Pop_").click(function () {
    if ($(this).html() == "Next") {
        $('.nav_tabs_for_new_customer > .active').next('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_customer > .active > a').attr("href") == "#c-payment-dtls") {
            $(this).html("Save");
        }
        $('#_Cancel_New_Customer_Pop_').html("Previous");
    } else {
        InsertUpdateCustomer();
        $('._Add_New_Customer_Form_').html('');

        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
    }
});

$('.nav_tabs_for_new_customer > li > a').click(function () {
    if ($(this).attr("href") == "#c-general-dtls") {
        $('#_Cancel_New_Customer_Pop_').html("Cancel");
        $('#_Save_New_Customer_Pop_').html("Next");
        setTimeout(function () { $('input[name=Full_name]').focus(); }, 1);
    }
    if ($(this).attr("href") == "#c-address-dtls") {
        $('#_Cancel_New_Customer_Pop_').html("Previous");
        $('#_Save_New_Customer_Pop_').html("Next");
        setTimeout(function () { $('input[name=Address]').focus(); }, 1);
    }
    if ($(this).attr("href") == "#c-payment-dtls") {
        $('#_Cancel_New_Customer_Pop_').html("Previous");
        $('#_Save_New_Customer_Pop_').html("Save");
        setTimeout(function () { $('input[name=Bank_account_number]').focus(); }, 1);
    }
});

$('#_Cancel_New_Customer_Pop_').on('click', function () {
    if ($(this).html() == "Previous") {
        $('.nav_tabs_for_new_customer > .active').prev('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_customer > .active > a').attr("href") == "#c-general-dtls") {
            $(this).html("Cancel");
        }
        $('#_Save_New_Customer_Pop_').html("Next");
    } else {
        $('#modal-AddNewCustomer').modal('toggle');
    }
});

function InsertUpdateCustomer() {

    $('#_Error_Message_Display_ > p').html("");
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if ($("input[name=Full_name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Full_name]").val().length > 255) {
        $('#_Error_Message_Display_ > p').html("Name is too long (Max 255 characters)<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=Contact_company] option:checked").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Company<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Designation]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Designation<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=Contact_phone_landline]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Landline<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Contact_phone_landline]").val().length > 13) {
        $('#_Error_Message_Display_ > p').html("Incorrect Phone No Format, example format 0092xx1234567<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Contact_phone_mobile]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Mobile No<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Contact_phone_mobile]").val().length > 14) {
        $('#_Error_Message_Display_ > p').html("Incorrect Mobile No Format. example format 0092xxx1234567<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Contact_email]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Contact Email<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if (!testEmail.test($("input[name=Contact_email]").val()) && $("input[name=Contact_email]").val().length != 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Email Format<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Address]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Address<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Address]").val().length > 255) {
        $('#_Error_Message_Display_ > p').html("Address too Long (Max 255 characters)<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Address_phone]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Address Phone No<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Address_phone]").val().length > 13) {
        $('#_Error_Message_Display_ > p').html("Incorrect Phone No format<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=Address_country] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Country<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=Address_city] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select City<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Bank_account_number]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Bank Account No<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else {
        var url = '/Customer/InserUpdateCustomer';
        var customer = {
            id: $("#customerid").val(),
            Salutation: $("select[name=Salutation] option:checked").val(),
            Name: $("input[name=Full_name]").val(),
            CompanyId: $("select[name=Contact_company] option:selected").val(),
            Designation: $("input[name=Designation]").val(),
            Landline: $("input[name=Contact_phone_landline]").val(),
            Mobile: $("input[name=Contact_phone_mobile]").val(),
            Email: $("input[name=Contact_email]").val(),
            Website: $("input[name=Website]").val(),
            Address: $("input[name=Address]").val(),
            AddressLandline: $("input[name=Address_phone]").val(),
            City: $("select[name=Address_city] option:checked").val(),
            Country: $("select[name=Address_country] option:checked").val(),
            BankAccountNumber: $("input[name=Bank_account_number]").val(),
            PaymentMethod: $("select[name=Payment_method] option:checked").val(),
            IsEnabled: $("select[name=C_Status] option:checked").val()
        }
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "Customer": JSON.stringify(customer) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == 1) {
                DropDownCustomers();
                $('#modal-AddNewCustomer').modal('toggle');
                $('#_Error_Message_Display_ > p').html('');
                $('#_Error_Message_Display_').slideUp("slow");

                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('._Add_New_Customer_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                $("#customerid").val("0")
            } else {
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            }
        }).fail(function () {
            $('#_Error_Message_Display_ > p').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        });
    }
};