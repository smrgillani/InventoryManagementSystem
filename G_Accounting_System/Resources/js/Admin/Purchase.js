var PurchasesList;
var Items_For_Stock = new Array();
var Purchasing_Order_Total_Amount = 0;
var Purchasing_Item_Msrmnt_Unit = "";
var Rec = "";
var Bill = "";


$("#_Add_New_Purchase_Order_btn_").click(function () {
    $(this).attr("disabled", true);
    $("#newPurchaseorder_id").val("0");
    $("#newPurchaseorderdetail_id").val("0");
    AddNewForm();

});


$("#formcollapse").click(function () {
    $('#formcollapse').collapse();
});

function AddNewForm() {
    $('._Add_New_Purchase_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Purchase/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_Purchase_Form_').append(resp);
            $('.nav-tabs a[href="#p-general-dtls"]').tab('show');
            $('._Add_New_Purchase_Form_').slideDown("slow");
            $('.select2').select2();
            $("#_Add_New_Purchase_Order_Form_Save_").attr("disabled", false);
            $("#_Add_New_Purchase_Order_btn_").attr("disabled", false);
            $('#get_searched_items').focus();

            DropDownVendors();
            DropDownItems();
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Purchase_Form_').slideUp("slow");
            $('._Add_New_Purchase_Form_').html("");
            $("#_Add_New_Purchase_Order_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}


$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if ((window.location.pathname.toString().toLowerCase()) == "/purchase/index") {
        GetAllPurchasesList(null);
    } else if (url.includes("/purchase/invoice/")) {

        InvoicePurchase(tablebind);
    }
});

//function AddPurchase() {
//    var url = '/Purchase/Add';

//    var token = $('[name=__RequestVerificationToken]').val();

//    $.ajax({
//        url: url,
//        type: "POST",
//        data: { __RequestVerificationToken: token, "PurchaseData": JSON.stringify(purchase_order) },
//        datatype: 'json',
//        ContentType: 'application/json; charset=utf-8'
//    }).done(function (resp) {
//        purchase_order.pop();
//        $('#_tbl_New_Purchasing_Body_').html("");
//        Purchasing_Order_Total_Amount = 0;
//        $('#_Purchased_Items_Total_').html('Total = ' + Purchasing_Order_Total_Amount);
//        $('._Add_New_Purchase_Orders_Form_').slideUp("slow");
//        $('#_Success_Message_Display_').slideDown("slow");
//        setTimeout(function () {
//            $('#_Success_Message_Display_').slideUp("slow");
//            $('#_Error_Message_Display_').slideUp("slow");
//        }, 5000);
//    }).fail(function () {
//        alert("post error 0");
//    })
//}

function GetAllPurchasesList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    PurchasesList = $('#purchasing_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No purchase orders available"
        },
        "sAjaxSource": "/Purchase/GetAllPurchases",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=PurchaseStartDate]').val(), EndDate: $('input[name=PurchaseEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[55].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/Purchase/Invoice/" + data.id + "'>" + data.TempOrderNum + "</a>";
                }
            },
            { data: "TotalItems" },
            { data: "TotalPrice" },
            { data: "Approved" },
            { data: "DateOfDay" },
            { data: "_Enable" },
            {
                data: function (data, type, dataToSet) {
                    //if (data.RecieveStatus === 'Issued' || data.RecieveStatus === 'Received') {
                    if (data.RecieveStatus === 'Issued') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" checked disabled>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" disabled>';
                    }
                    return data;
                }

            },
            {
                data: function (data, type, dataToSet) {
                    if (data.BillStatus === 'Open' || data.BillStatus === 'Paid' || data.BillStatus === 'Partially Paid' || data.BillStatus === 'Closed') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" checked disabled>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue" disabled>';
                    }
                    return data;
                }

            },
            {
                data: function (data, type, dataToSet) {
                    if (data.Bill_Stat === "True" || data.Rec_Stat === "True" || data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditPurchase(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepPurchase(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditPurchase(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepPurchase(" + data.id + ")' title='Visibility'></span>";
                    }
                }

            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested' || data.Bill_Stat === "True" || data.Rec_Stat === "True") {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkPurchaseDel" data_value_id=' + data.id + ' data_value_TempOrderNum=' + data.TempOrderNum + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkPurchaseDel" data_value_id=' + data.id + ' data_value_TempOrderNum=' + data.TempOrderNum + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

var Activity_Data = new Array();
function UpdatepPurchase(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Purchase/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {

        if (resp != null) {
            $('#_Error_Message_Display_ > span').html('');
            $('#_Success_Message_Display_ > span').html('');
            PurchasesList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > span').html('Purchase Order Profile Visibility Updated Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
        } else {
            $('#_Error_Message_Display_ > span').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function EditPurchase(id) {

    $("#newPurchaseorder_id").val(id);
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Purchase/Invoice/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {

        if (resp != false) {
            if (resp.pBill != null || (resp.RecieveStatus == "Issued" || resp.RecieveStatus == "Received")) {
                $('#_Error_Message_Display_ > P').html("This purchase order cannot be edited as Bill/Receiving has been created");
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
            else {

                AddNewForm();
                $("#_Add_New_Purchase_Order_Form_Save_").attr("disabled", false);
                //DropDownVendors();
                //DropDownItems();
                //$('#Add_New_Purchased_Item').val("Update");
                var Purchasing_Order_Total_Amount = 0;
                $.each(resp.PItems, function (i, item) {
                    var rows = "<tr class='filledField' data-value-item='" + item.ItemId + "' data-value-vendor='" + item.VendorId + "' data-value-pdid='" + item.pdid + "' data-value-qty='" + item.ItemQty + "' data-value-unit-price='" + item.PriceUnit + "' data-value-total='" + item.ItemQty * item.PriceUnit + "'>" +
                        "<td class='table_content_vertical_align_' data-value-item='" + item.ItemId + "' >" + item.ItemName + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-vendor='" + item.VendorId + "'>" + item.VendorName + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-qty='" + item.ItemQty + "'>" + item.ItemQty + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-msrmnt-unit='" + item.MsrmntUnit + "'>" + item.MsrmntUnit + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-unit-price='" + item.PriceUnit + "'>" + item.PriceUnit + "</td>" +
                        "<td class='table_content_vertical_align_' data-value-total='" + item.ItemQty * item.PriceUni + "'>" + item.ItemQty * item.PriceUnit + "</td>" +
                        "<td class='table_content_horizontal_align_'><span id='Edit_PurchaseItems' data-value-pdid='" + item.pdid + "' data-value-item='" + item.ItemId + "' " +
                        "data-value-vendor='" + item.VendorId + "' data-value-qty='" + item.ItemQty + "' data-value-msrmnt-unit='" + item.MsrmntUnit + "' " +
                        "data-value-unit-price='" + item.PriceUnit + "' data-value-total='" + item.ItemQty * item.PriceUnit + "'  class='fa fa-pencil-square-o EditPurchaseItems'></span>" +
                        "<span class='fa fa-trash' data-value-pdid='" + item.pdid + "' data-value-vendor='" + item.VendorId + "' data-value-total='" + item.ItemQty * item.PriceUnit + "' onclick='removeTrFrmTblE(this)' title='Delete'></span></td>" +
                        "</tr>";
                    Purchasing_Order_Total_Amount = Purchasing_Order_Total_Amount + (item.ItemQty * item.PriceUnit);
                    $('#_tbl_New_Purchasing_Body_').append(rows);
                });

                $('#_Purchased_Items_Total_').html('Total = ' + Purchasing_Order_Total_Amount);
            }
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Purchase_Form_').slideUp("slow");
            $('._Add_New_Purchase_Form_').html("");
            $("#_Add_New_Purchase_Order_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });

}

$(document).on('click', '.EditPurchaseItems', function (event) {
    $('#Add_New_Purchased_Item').val("Update");
    $("select[name=new_item_Name]").val($(this).data('value-item')).change();
    $("select[name=new_vendor_Name]").val($(this).data('value-vendor')).change();
    $("input[name=new_item_Quantity]").val($(this).data('value-qty'));
    $("input[name=new_item_Msrmnt_Unit]").val($(this).data('value-msrmnt-unit'));
    $("input[name=new_item_Unit_Price]").val($(this).data('value-unit-price'));
    $("input[name=new_item_Price_Total]").val($(this).data('value-total'));
    $("#newPurchaseorderdetail_id").val($(this).data('value-pdid'));
    $("select[name=new_vendor_Name]").attr("disabled", true);
});

function removeTrFrmTblE(eli) {
    Purchasing_Order_Total_Amount = Purchasing_Order_Total_Amount - $(eli).data('value-total');
    $('#_Purchased_Items_Total_').html('Total = ' + Purchasing_Order_Total_Amount);
    $("select[name=new_vendor_Name]").val($(eli).data('value-vendor')).change();
    $("select[name=new_vendor_Name]").attr("disabled", true);
    var pdid = $(eli).data('value-pdid');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Purchase/DeleteItemFromPurchase/',
        type: "POST",
        data: {
            __RequestVerificationToken: token, pdid: pdid
        },
    }).done(function (resp) {
        if (resp.length == 0) {

            $(eli).closest('tr').remove();
        }
        else {
            $('#_Error_Message_Display_ > p').html('Network Error');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {

    });
}

function InvoicePurchase(tablebind) {
    var token = $('[name=__RequestVerificationToken]').val();
    var id = window.location.pathname.toString().toLowerCase().split("/invoice/")[1];

    $.ajax({
        url: '/Purchase/Invoice/' + id,
        type: "POST",
        data: {
            __RequestVerificationToken: token
        },
        //datatype: 'json',
        //ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp != false) {
            $("#Idate").html('Date: ' + resp.Date);
            $("#Inameto").html(resp.PItems[0].VendorName);
            $("#Iaddressto").html(resp.PItems[0].VendorAddress + '<br> Landline: ' + resp.PItems[0].VendorLandline + '<br> Mobile: ' + resp.PItems[0].VendorMobile + '<br> Email: ' + resp.PItems[0].VendorEmail);
            $("#Iinvoiceno").html(resp.InvoiceNum);
            $("#IinvoiceStatus").html('<b>Status: </b>' + resp.RecieveStatus);
            $("#IRecDate").html(resp.RecieveDate);
            $("#Isubtotal").html(resp.TotalPrice);
            $("#Itotal").html('<b>' + resp.TotalPrice + '</b>');
            $("#purchaseid").val(resp.InvoiceNum);


            $("#po__VendorName").html(resp.PItems[0].VendorName);
            $("#po__TotalAmount").html(resp.TotalPrice);
            $("#po__TotalItems").html(resp.PItems.length);


            if (tablebind == 0) {
                $.each(resp.PItems, function (i, item) {
                    var rows = "<tr>" +
                        "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                        "<td class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                        "<td class='table_content_vertical_align_'>" + item.ItemQty + ' x ' + item.PriceUnit + "</td>" +
                        "<td class='table_content_vertical_align_'>" + item.TotalItems + "</td>" +
                        "</tr>";
                    $('#Iitemslistbody').append(rows);
                });
            }
            $('._Purchase_Order_Save_').html('Receive All');
            $("#_Purchase_Order_ConvertoBill_Save_").html("Convert to Bill");
            if (resp.pBill != null) {
                $("#billid").val(resp.pBill.id);
            }
            if (resp.Bill_Stat == "True") {
                if (resp.Rec_Stat == "True") {
                    $('._Purchase_Order_Save_').html('View Bill');
                    $("#_Purchase_Order_ConvertoBill_Save_").hide();
                }
                else {
                    $('._Purchase_Order_Save_').html('View Bill');
                    $("#_Purchase_Order_ConvertoBill_Save_").html("Receive All");
                }

            }

            else if (resp.Rec_Stat == "True") {
                if (resp.Bill_Stat != "True") {
                    $('._Purchase_Order_Save_').html('Convert to Bill');
                    $("#_Purchase_Order_ConvertoBill_Save_").hide();
                }
                else {

                }
            }
            var ActivityType_id = id;
            var ActivityType = "Purchase Order";
            Activities(ActivityType_id, ActivityType);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {

        alert("error 1");
    });
}
var tablebind = 0;
var RecStatus = "";
$("._Purchase_Order_Save_").click(function () {
    var token = $('[name=__RequestVerificationToken]').val();
    var billid = $("#billid").val();
    var id = window.location.pathname.toString().toLowerCase().split("/invoice/")[1];
    var Name = $(this).html();
    if (Name == 'Receive All') {
        RecieveAll();
    }
    if (Name == 'Convert to Bill') {
        CreateBill();
    }
    if (Name == 'View Bill') {
        window.location.href = '/bill/billinvoice/' + billid;
    }

});

$("#_Purchase_Order_ConvertoBill_Save_").click(function () {
    if ($(this).html() == "Convert to Bill") {

        CreateBill();
    }
    else if ($(this).html() == "Receive All") {
        RecieveAll();
    }
});

function RecieveAll() {
    var token = $('[name=__RequestVerificationToken]').val();

    $.confirm({
        closeIcon: true,
        closeIconClass: 'fa fa-close',
        type: 'purple',
        theme: 'modern',
        boxWidth: '500px',
        useBootstrap: false,
        typeAnimated: true,
        alignMiddle: true,
        animationBounce: 1.5,
        title: 'Receive All?',
        content: 'This action will mark all the items as received. Do you really want to proceed?',
        buttons: {
            confirm: function () {
                $.confirm({
                    closeIcon: true,
                    closeIconClass: 'fa fa-close',
                    type: 'purple',
                    //boxWidth: '500px',
                    theme: 'modern',
                    useBootstrap: false,
                    typeAnimated: true,
                    alignMiddle: false,
                    title: 'Receive All?',
                    animationBounce: 1.5,
                    onOpen: function () {
                        $(".datepicker").datepicker();
                    },
                    onContentReady: function () {
                        var self = this;
                        $.ajax({
                            url: '/Purchase/RecieveItems/',
                            type: "POST",
                            data: { __RequestVerificationToken: token },
                        }).done(function (resp) {
                            self.setContentAppend(resp);
                            $(".datepicker").datepicker();
                            getItemsForBill();

                        }).fail(function () {

                            $('#_Error_Message_Display_ > span').html("Error!");
                            $('#_Error_Message_Display_').slideDown("slow");
                            setTimeout(function () {
                                $('#_Error_Message_Display_').slideUp("slow");
                            }, 5000);
                        });

                    },
                    buttons: {
                        formSubmit: {
                            text: 'Save',
                            btnClass: 'btn-blue',
                            action: function () {
                                var RecieveDateTime = $("input[name=RecieveDateTime]").val();
                                if (!RecieveDateTime) {
                                    $.alert('Enter Recieve Date');
                                    return false;
                                }
                                var Purchase_data = {
                                    id: $("#purchaseid").val(),
                                    Item_Sku: $("input[name=item_Sku]").val(),
                                    RecieveStatus: 'Issued',
                                    RecieveDateTime: RecieveDateTime,
                                    Rec_Stat: "True"
                                }
                                $.ajax({
                                    url: '/Purchase/UpdatePurchaseStatus/',
                                    type: "POST",
                                    data: { __RequestVerificationToken: token, "PurchaseData": JSON.stringify(Purchase_data) },
                                }).done(function (resp) {
                                    $('._Purchase_Order_Save_').html('Convert to Bill');
                                    $('#_Success_Message_Display_ > span').html("Purchase Receive created Successfully");
                                    $('#_Success_Message_Display_').slideDown("slow");
                                    getDataForStockOnReceiving();
                                    Rec = "Yes";

                                    tablebind = 1;
                                    InvoicePurchase(tablebind);
                                    setTimeout(function () {
                                        $('#_Success_Message_Display_').slideUp("slow");
                                    }, 5000);
                                    status = 'bill';
                                }).fail(function () {

                                    $('#_Error_Message_Display_ > span').html("Error!");
                                    $('#_Error_Message_Display_').slideDown("slow");
                                    setTimeout(function () {
                                        $('#_Error_Message_Display_').slideUp("slow");
                                    }, 5000);
                                });
                            }
                        },
                        cancel: function () {
                            //close
                        },

                    },
                    //onContentReady: function () {
                    //    // bind to events
                    //    var jc = this;
                    //    this.$content.find('form').on('Save', function (e) {
                    //        // if the user submits the form by pressing enter in the field.
                    //        e.preventDefault();
                    //        jc.$$formSubmit.trigger('click'); // reference the button and click it
                    //    });
                    //}
                });

            },
            cancel: function () {

            }
        }
    });
}

function CreateBill() {
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
        title: 'Create Bill',
        content: '',

        onContentReady: function () {
            var self = this;
            $.ajax({
                url: '/Purchase/Bill/',
                type: "POST",
                data: { __RequestVerificationToken: token },
            }).done(function (resp) {
                self.setContentAppend(resp);

                getItemsForBill();
                $(".disblefuturedatepicker").datepicker({
                    format: 'dd/mm/yyyy',
                    endDate: "today",
                    maxDate: today
                });
                $(".datepicker").datepicker({
                    format: 'dd/mm/yyyy',
                    //startDate: "today",
                    //minDate: today
                });
            }).fail(function () {

                $('#_Error_Message_Display_ > span').html("Error!");
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            });

        },

        buttons: {
            'Save as Draft': function () {
                if (!$("input[name=BbillNo]").val()) {
                    $.alert('Please Enter Bill No');
                    return false;
                }
                else if (!$("input[name=BbillDate]").val()) {
                    $.alert('Please Enter Bill Date');
                    return false;
                }
                else if (!$("input[name=BdueDate]").val()) {
                    $.alert('Please Enter Bill Due Date');
                    return false;
                }
                else {
                    var Bill_data = {
                        Purchase_id: $("#purchaseid").val(),
                        Bill_No: $("input[name=BbillNo]").val(),
                        Bill_Amount: $("#Bsubtotal").html(),
                        Bill_Status: 'Draft',
                        BillDateTime: $("input[name=BbillDate]").val(),
                        BillDueDate: $("input[name=BdueDate]").val(),
                        Amount_Paid: "0.00",
                        Balance_Amount: $("#Bsubtotal").html(),
                    }
                    $.ajax({
                        url: '/Bill/InsertBill/',
                        type: "POST",
                        data: { __RequestVerificationToken: token, "BillData": JSON.stringify(Bill_data) },
                    }).done(function (resp) {
                        //$('._Purchase_Order_Save_').html('Convert to Bill');
                        //$('._Purchase_Order_ConvertoBill_Save_').hide();
                        var Purchase_data = {
                            id: $("#purchaseid").val(),
                            RecieveStatus: 'Issued',
                            RecieveDateTime: $("#IRecDate").html(),
                            Bill_Stat: "True"
                        }
                        $.ajax({
                            url: '/Purchase/UpdatePurchaseStatus/',
                            type: "POST",
                            data: { __RequestVerificationToken: token, "PurchaseData": JSON.stringify(Purchase_data) },
                        }).done(function (resp) {



                            $("#billid").val(resp.pBill_id_Output);

                            setTimeout(function () {
                                $('#_Success_Message_Display_').slideUp("slow");
                            }, 5000);
                            status = 'bill';
                        }).fail(function () {

                            $('#_Error_Message_Display_ > span').html("Error!");
                            $('#_Error_Message_Display_').slideDown("slow");
                            setTimeout(function () {
                                $('#_Error_Message_Display_').slideUp("slow");
                            }, 5000);
                        });

                        $('#_Success_Message_Display_ > span').html("Bill created Successfully");
                        $('#_Success_Message_Display_').slideDown("slow");
                        //tablebind = 1;
                        //InvoicePurchase(tablebind);
                        //$("#billid").val(resp.pBill_id_Output),
                    }).fail(function () {

                        $('#_Error_Message_Display_ > span').html("Error!");
                        $('#_Error_Message_Display_').slideDown("slow");
                        setTimeout(function () {
                            $('#_Error_Message_Display_').slideUp("slow");
                        }, 5000);
                    });

                    tablebind = 1;
                    InvoicePurchase(tablebind);
                }
            },
            confirm: function () {
                if (!$("input[name=BbillNo]").val()) {
                    $.alert('Please Enter Bill No');
                    return false;
                }
                else {
                    var Bill_data = {
                        Purchase_id: $("#purchaseid").val(),
                        Bill_No: $("input[name=BbillNo]").val(),
                        Bill_Amount: $("#Bsubtotal").html(),
                        Bill_Status: 'Open',
                        BillDateTime: $("input[name=BbillDate]").val(),
                        BillDueDate: $("input[name=BdueDate]").val(),
                        Amount_Paid: "0.00",
                        Balance_Amount: "0.00",
                    }
                    $.ajax({
                        url: '/Bill/InsertBill/',
                        type: "POST",
                        data: { __RequestVerificationToken: token, "BillData": JSON.stringify(Bill_data) },
                    }).done(function (resp) {
                        Bill = "Yes";
                        var Purchase_data = {
                            id: $("#purchaseid").val(),
                            RecieveStatus: 'Received',
                            RecieveDateTime: $("#IRecDate").html(),
                            Bill_Stat: "True"
                        }
                        $.ajax({
                            url: '/Purchase/UpdatePurchaseStatus/',
                            type: "POST",
                            data: { __RequestVerificationToken: token, "PurchaseData": JSON.stringify(Purchase_data) },
                        }).done(function (resp) {
                            $('._Purchase_Order_Save_').html('Convert to Bill');
                            $('#_Success_Message_Display_ > span').html("Bill created Successfully");
                            $('#_Success_Message_Display_').slideDown("slow");
                            //$('._Purchase_Order_ConvertoBill_Save_').tml("Recieve All");h
                            getDataForStockOnCreateBill();

                            $("#billid").val(resp.pBill_id_Output),

                                setTimeout(function () {
                                    $('#_Success_Message_Display_').slideUp("slow");
                                }, 5000);
                            status = 'bill';
                        }).fail(function () {

                            $('#_Error_Message_Display_ > span').html("Error!");
                            $('#_Error_Message_Display_').slideDown("slow");
                            setTimeout(function () {
                                $('#_Error_Message_Display_').slideUp("slow");
                            }, 5000);
                        });
                        $('._Purchase_Order_Save_').html('Convert to Bill');
                        $('#_Success_Message_Display_ > span').html("Bill created Successfully");
                        $('#_Success_Message_Display_').slideDown("slow");
                        tablebind = 1;
                        InvoicePurchase(tablebind);
                        $("#billid").val(resp.pBill_id_Output),

                            setTimeout(function () {
                                $('#_Success_Message_Display_').slideUp("slow");
                            }, 5000);
                        status = 'bill';


                    }).fail(function () {

                        $('#_Error_Message_Display_ > span').html("Error!");
                        $('#_Error_Message_Display_').slideDown("slow");
                        setTimeout(function () {
                            $('#_Error_Message_Display_').slideUp("slow");
                        }, 5000);
                    });
                }

            },
            cancel: function () {

            }
        }
    });
}

function getItemsForBill() {
    id = $("#purchaseid").val()
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Purchase/SelectItemsForBillByPOid/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {

        $('#Bitemslistbody').empty();
        $.each(resp.BillItems, function (i, item) {
            var rows = "<tr data-value-Item_id='" + item.ItemId + "', data-value-Item_qty='" + item.ItemQty + "'>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.ItemQty + ' x ' + item.PriceUnit + "</td>" +
                "<td class='table_content_vertical_align_'>" + item.TotalItems + "</td>" +
                "</tr>";
            $('#Bitemslistbody').append(rows);
            $('input[name=BvendorName]').val($("#Inameto").html());
            $('input[name=BorderNo]').val($("#Iinvoiceno").html());
            $('#Bsubtotal').html($("#Isubtotal").html());
            $('#Btotal').html($("#Itotal").html());
            $('input[name=BbillNo]').val(resp.NewBillNo);
        });


    }).fail(function () {

        $('#_Error_Message_Display_ > span').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });
}

function getDataForStockOnReceiving() {
    var data_value_Item_qty = "";
    var data_value_Item_id = "";
    var data = "";
    Items_For_Stock = new Array();
    $('#Bitemslistbody tr').each(function (indexoftr, tr) {
        data_value_Item_id = $(this).attr('data-value-Item_id');
        data_value_Item_qty = $(this).attr('data-value-Item_qty');
        data = {
            Item_id: data_value_Item_id,
            Physical_Quantity: data_value_Item_qty,
            Physical_Avail_ForSale: data_value_Item_qty,
        }
        Items_For_Stock.push(data);

    });

    if (Items_For_Stock.length > 0) {
        UpdateItemStockOnReceiving();
    } else {
        $('#_Error_Message_Display_ p').html("There are no items!");
        $('#_Error_Message_Display_').slideDown();
    }
}

function UpdateItemStockOnReceiving() {
    var url = '/Stock/UpdatePOitemStockOnReceiving';

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "StockData": JSON.stringify(Items_For_Stock) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        tablebind = 1;
        InvoicePurchase(tablebind);
    }).fail(function () {
        alert("post error 0");
    })
}

function getDataForStockOnCreateBill() {
    var token = $('[name=__RequestVerificationToken]').val();
    var data_value_Item_qty = "";
    var data_value_Item_id = "";
    var data = "";
    Items_For_Stock = new Array();
    $('#Bitemslistbody tr').each(function (indexoftr, tr) {
        data_value_Item_id = $(this).attr('data-value-Item_id');
        data_value_Item_qty = $(this).attr('data-value-Item_qty');
        data = {
            Item_id: data_value_Item_id,
            Physical_Quantity: data_value_Item_qty,
            Accounting_Quantity: data_value_Item_qty,
        }
        Items_For_Stock.push(data);

    });
    if (Items_For_Stock.length > 0) {
        UpdatePOitemStockOnCreateBill();
        data = "";
    } else {
        $('#_Error_Message_Display_ p').html("There are no items!");
        $('#_Error_Message_Display_').slideDown();
    }
}

function UpdatePOitemStockOnCreateBill() {

    var url = '/Stock/UpdatePOitemStockOnCreateBill';
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "StockData": JSON.stringify(Items_For_Stock) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        tablebind = 1;
        InvoicePurchase(tablebind);
    }).fail(function () {
        alert("post error 0");
    })
}

$("input[name=PurchaseStartDate]").change(function () {
    PurchasesList.ajax.reload(null, false);
});

$("input[name=PurchaseEndDate]").change(function () {
    PurchasesList.ajax.reload(null, false);
});

var checked_Purchases;
function getCheckedPurchasingtoDel() {
    var data_value_id = "";
    var data_value_TempOrderNum = "";

    checked_Purchases = new Array();

    $('#_tbl_Purchasing_List_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkPurchaseDel')).is(':checked')) {
            data_value_id = $(this).find($('td')).find($('.chkPurchaseDel')).attr('data_value_id');
            data_value_TempOrderNum = $(this).find($('td')).find($('.chkPurchaseDel')).attr('data_value_TempOrderNum');
            var data = { id: data_value_id, TempOrderNum: data_value_TempOrderNum, Delete_Status: "Requested" }
            checked_Purchases.push(data);
        }
    });
    if (checked_Purchases.length > 0) {
        SendRequestToDelPurchasing();
    } else {
        $('#_Error_Message_Display_').html("There are No Purchasings to Delete");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
}

$("#_Send_request_To_DeletePurchasing").click(function () {
    getCheckedPurchasingtoDel();
});

function SendRequestToDelPurchasing() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Purchase/SendRequestToDelPurchasing/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeletePurchasingData": JSON.stringify(checked_Purchases)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {

            $('#_Success_Message_Display_ > span').html('Request Sent Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            PurchasesList.ajax.reload(null, false);
            setTimeout(function () { $('#_Success_Message_Display_').slideUp("slow"); }, 5000);
            $('#_Send_request_To_DeletePurchasing').css('display', 'none');
        }
        else {
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {

    });
}

$(document).on("change", ".chkPurchaseDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeletePurchasing').show();
    }
    else {
        $('#_Send_request_To_DeletePurchasing').css('display', 'none');
    }
});

$(".ListPurchasesFilter").click(function () {
    PurchasesList.destroy();
    var filtertype = $(this).text();
    GetAllPurchasesList(filtertype);
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 27) {
//        // Save Function
//        $('#_Add_New_Purchase_Order_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$("#_Save_New_Item_Pop_").click(function () {
    if ($(this).html() == "Next") {
        $('.nav_tabs_for_new_item > .active').next('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_item > .active > a').attr("href") == "#i-openingstock-dtls") {
            if ($('input[name=itemid]').val() == "0") {
                $(this).html("Save");
            }
            else {
                $(this).html("Update");
            }
        }
        $('#_Cancel_New_Item_Pop_').html("Previous");
    } else {
        AddItem();

        $('#modal-AddNewItem').modal('toggle');
        DropDownItems();
    }
});

$('.nav_tabs_for_new_item > li > a').click(function () {
    if ($(this).attr("href") == "#i-general-dtls") {
        $('#_Cancel_New_Item_Pop_').html("Cancel");
        $('#_Save_New_Item_Pop_').html("Next");
        setTimeout(function () { $('input[name=item_Name]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#i-sales-dtls") {
        $('#_Cancel_New_Item_Pop_').html("Previous");
        $('#_Save_New_Item_Pop_').html("Next");
        setTimeout(function () { $('input[name=item_Sell_Price]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#i-purchase-dtls") {
        if (!$('.nav-tabs-custom a[href="#i-openingstock-dtls"]').tab().is(":visible")) {
            $('#_Save_New_Item_Pop_').html("Update");
        } else {
            $('#_Save_New_Item_Pop_').html("Next");
        }
        $('#_Cancel_New_Item_Pop_').html("Previous");
        setTimeout(function () { $('input[name=item_Purchase_Price]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#i-openingstock-dtls") {
        $('#_Cancel_New_Item_Pop_').html("Previous");
        if ($('input[name=itemid]').val() == "0") {
            $('#_Save_New_Item_Pop_').html("Save");
        }
        else {
            $('#_Save_New_Item_Pop_').html("Update");
        }
        setTimeout(function () { $('input[name=item_Opening_Stock]').focus(); }, 1);
    }
});

$('#_Cancel_New_Item_Pop_').on('click', function () {
    if ($(this).html() == "Previous") {
        $('.nav_tabs_for_new_item > .active').prev('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_item > .active > a').attr("href") == "#i-general-dtls") {
            $(this).html("Cancel");
        }
        $('#_Save_New_Item_Pop_').html("Next");
    } else {
        $('#modal-AddNewItem').modal('toggle');
    }
});



function PreviousOrders(item_id, Vendor_id, title) {
    $('.previousorders').show();
    //$('#_tbl__PreviousOrders_list_Body_').empty();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Purchase/PreviousOrders/',
        type: "POST",
        data: { __RequestVerificationToken: token, ItemId: item_id, VendorId: Vendor_id },
    }).done(function (resp) {
        $("#previousOrderHeading").text("Previous Orders against " + title);
        if (resp.length != 0) {
            $('.previousorders').slideDown("slow");
            $.each(resp, function (i, item) {
                var rows = "<tr data-value-id='" + item.id + "'>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemName + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.TempOrderNum + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.VendorName + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.PriceUnit + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
                    "<td class='table_content_vertical_align_'>" + item.ItemQty * item.PriceUnit + "</td>" +
                    "</tr>";
                $('#_tbl__PreviousOrders_list_Body_').append(rows);
            });
        }
        else {
            $("#previousOrderHeading").text("No Previous Previous Orders against " + title);
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}