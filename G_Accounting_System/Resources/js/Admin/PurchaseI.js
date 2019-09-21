
var Purchasing_Order_Total_Amount = 0;
var Purchasing_Item_Msrmnt_Unit = "";

var purchase_order = new Array();
//$("#_Add_New_Purchase_Order_btn_").click(function () {
//    $('._Add_New_Purchase_Form_').slideDown("slow");

//});

$("#_Add_New_Purchase_Order_Form_Remover_ , #_Add_New_Purchase_Order_Form_Remover__").click(function () {
    $('#_Add_New_Purchase_Order_btn_').attr("disabled", false);
    $('._Add_New_Purchase_Form_').slideUp("slow");
    $('._Add_New_Purchase_Form_').html('');
    $("#_Add_New_Purchase_Order_Form_Save_").attr("disabled", false);
});

$("#BO_Error_Message_Display_").click(function () {
    $('#_Error_Message_Display_').slideUp();
});

$("#Add_New_Purchased_Item").click(function () {
    var rows = "";
    var item_up = "";
    var item_id = "";

    Purchasing_Item_Msrmnt_Unit = $("input[name=new_item_Msrmnt_Unit]").val();
    var GetNewItemsNameValue = $("select[name=new_item_Name] option:selected").text();
    var GetNewItemsVendorValue = $("select[name=new_vendor_Name] option:selected").text();
    var GetNewItemsQuantityValue = $("input[name=new_item_Quantity]").val();
    var GetNewItemsUnitPriceValue = $("input[name=new_item_Unit_Price]").val();
    var pdid = $('#newPurchaseorderdetail_id').val();
    if ($("select[name=new_item_Name] option:selected").val() === "0") {
        $('#_Error_Message_Display_ p').html("Please select an item to add into purchase order.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else if ($("select[name=new_vendor_Name] option:selected").val() === "0" || $("select[name=new_vendor_Name] option:selected").val() === null) {
        $('#_Error_Message_Display_ p').html("Please select a vendor.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else if (GetNewItemsQuantityValue == 0) {
        $('#_Error_Message_Display_ p').html("Please enter quantity for your item.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else if (Purchasing_Item_Msrmnt_Unit == "") {
        $('#_Error_Message_Display_ p').html("Measurement unit for your item not loaded successfully , update your item measurement unit by going at the item page.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else if (GetNewItemsUnitPriceValue == 0) {
        $('#_Error_Message_Display_ p').html("Please enter item unit price.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else if (GetNewItemsQuantityValue * GetNewItemsUnitPriceValue < 0) {
        $('#_Error_Message_Display_ p').html("Item unit price or Item Quantity is less than zero.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        if ($("#Add_New_Purchased_Item").val() === "Update") {
            $('#Add_New_Purchased_Item').val("Add");
            var newItem_id = $("select[name=new_item_Name] option:selected").val();
            var newItem_up = $("input[name=new_item_Unit_Price]").val();
            var newItem_Qty = $("input[name=new_item_Quantity]").val();
            $('#_tbl_New_Purchasing_Body_ tr').each(function (indexoftr, tr) {

                var lines = $('td', tr).map(function (indexoftd, td) {
                    if (indexoftd == 0) {
                        item_id = $(td).data('value-item');
                    }
                    if (indexoftd == 2) {
                        data_value_qty = $(td).attr('data-value-qty');
                    }
                    if (indexoftd == 4) {
                        item_up = $(td).data('value-unit-price');
                    }

                });

                if (newItem_id == item_id && newItem_up == item_up) {
                    //GetNewItemsQuantityValue = parseInt(data_value_qty) + parseInt(newItem_Qty);
                    GetNewItemsQuantityValue = parseInt(newItem_Qty);

                    GetNewItemsUnitPriceValue = item_up;
                    removeTrFrmTbl($(this));
                }
            });
            var rows = "<tr data-value-pdid='" + pdid + "' data-value-qty='" + GetNewItemsQuantityValue + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" +
                "<td data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "'>" + GetNewItemsNameValue + "</td>" +
                "<td data-value-vendor='" + $("select[name=new_vendor_Name] option:checked").val() + "'>" + GetNewItemsVendorValue + "</td>" +
                "<td data-value-qty='" + GetNewItemsQuantityValue + "'>" + GetNewItemsQuantityValue + "</td>" +
                "<td data-value-msrmnt-unit='" + Purchasing_Item_Msrmnt_Unit + "'> " + Purchasing_Item_Msrmnt_Unit + "</td>" +
                "<td data-value-unit-price='" + GetNewItemsUnitPriceValue + "'>" + GetNewItemsUnitPriceValue + "</td>" +
                "<td data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "</td>" +
                "<td><span id='Edit_PurchaseItems' data-value-pdid='" + pdid + "' data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "' " +
                "data-value-vendor='" + $("select[name=new_vendor_Name] option:checked").val() + "' data-value-qty='" + GetNewItemsQuantityValue + "' " +
                "data-value-msrmnt-unit='" + Purchasing_Item_Msrmnt_Unit + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' " +
                "data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'  class='fa fa-pencil-square-o EditPurchaseItems'></span>" +

                "<span class='fa fa-trash' onclick='removeTrFrmTbl(this)' title='Delete'></span></td>" +
                "</tr>";

            $('#_tbl_New_Purchasing_Body_').append(rows);
            $("select[name=new_vendor_Name]").val($("select[name=new_vendor_Name] option:checked").val()).change();
            SetAllInputsToDefault();

            $("select[name=new_vendor_Name]").attr("disabled", true);
            Purchasing_Order_Total_Amount = 0;
            $('#_tbl_New_Purchasing_Body_ tr').each(function (indexoftr, tr) {
                GetNewItemsQuantityValue = $(this).data('value-qty');
                GetNewItemsUnitPriceValue = $(this).data('value-unit-price');
                Purchasing_Order_Total_Amount = Purchasing_Order_Total_Amount + $(this).data('value-total');

            });
            $('#_Purchased_Items_Total_').html('Total = ' + Purchasing_Order_Total_Amount);
        }
        else if ($("#Add_New_Purchased_Item").val() === "Add") {

            var rowCount = $('#_tbl_New_Purchasing_Body_ tr').length;
            if (rowCount == 0) {
                pdid = 0;
                var rows = "<tr data-value-pdid='" + pdid + "' data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" +
                    "<td data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "'>" + GetNewItemsNameValue + "</td>" +
                    "<td data-value-vendor='" + $("select[name=new_vendor_Name] option:checked").val() + "'>" + GetNewItemsVendorValue + "</td>" +
                    "<td data-value-qty='" + GetNewItemsQuantityValue + "'>" + GetNewItemsQuantityValue + "</td>" +
                    "<td data-value-msrmnt-unit='" + Purchasing_Item_Msrmnt_Unit + "'> " + Purchasing_Item_Msrmnt_Unit + "</td>" +
                    "<td data-value-unit-price='" + GetNewItemsUnitPriceValue + "'>" + GetNewItemsUnitPriceValue + "</td>" +
                    "<td data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "</td>" +

                    "<td><span id='Edit_PurchaseItems' data-value-pdid='" + pdid + "' data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "' " +
                    "data-value-vendor='" + $("select[name=new_vendor_Name] option:checked").val() + "' data-value-qty='" + GetNewItemsQuantityValue + "' " +
                    "data-value-msrmnt-unit='" + Purchasing_Item_Msrmnt_Unit + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' " +
                    "data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'  class='fa fa-pencil-square-o EditPurchaseItems'></span>" +

                    "<span class='fa fa-trash' onclick='removeTrFrmTbl(this)' title='Delete'></span></td>" +
                    "</tr>";
                $('#_tbl_New_Purchasing_Body_').append(rows);

            }
            else {
                var newItem_id = $("select[name=new_item_Name] option:selected").val();
                var newItem_up = $("input[name=new_item_Unit_Price]").val();
                var newItem_Qty = $("input[name=new_item_Quantity]").val();

                $('#_tbl_New_Purchasing_Body_ tr').each(function (indexoftr, tr) {
                    var lines = $('td', tr).map(function (indexoftd, td) {
                        if (indexoftd == 0) {
                            item_id = $(td).data('value-item');
                        }
                        if (indexoftd == 2) {
                            data_value_qty = $(td).attr('data-value-qty');
                        }
                        if (indexoftd == 4) {
                            item_up = $(td).data('value-unit-price');
                        }

                    });
                    if (newItem_id == item_id && newItem_up == item_up) {
                        pdid = $(this).data('value-pdid');
                        GetNewItemsQuantityValue = parseInt(data_value_qty) + parseInt(newItem_Qty);

                        GetNewItemsUnitPriceValue = item_up;
                        removeTrFrmTbl($(this));

                    }
                    else {
                        pdid = 0;
                    }
                });
                rows = "<tr data-value-pdid='" + pdid + "' data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" +
                    "<td data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "'>" + GetNewItemsNameValue + "</td>" +
                    "<td data-value-vendor='" + $("select[name=new_vendor_Name] option:checked").val() + "'>" + GetNewItemsVendorValue + "</td>" +
                    "<td data-value-qty='" + GetNewItemsQuantityValue + "'>" + GetNewItemsQuantityValue + "</td>" +
                    "<td data-value-msrmnt-unit='" + Purchasing_Item_Msrmnt_Unit + "'> " + Purchasing_Item_Msrmnt_Unit + "</td>" +
                    "<td data-value-unit-price='" + GetNewItemsUnitPriceValue + "'>" + GetNewItemsUnitPriceValue + "</td>" +
                    "<td data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "</td>" +
                    "<td><span id='Edit_PurchaseItems' data-value-pdid='" + pdid + "' data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "' " +
                    "data-value-vendor='" + $("select[name=new_vendor_Name] option:checked").val() + "' data-value-qty='" + GetNewItemsQuantityValue + "' " +
                    "data-value-msrmnt-unit='" + Purchasing_Item_Msrmnt_Unit + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' " +
                    "data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'  class='fa fa-pencil-square-o EditPurchaseItems'></span>" +

                    "<span class='fa fa-trash' onclick='removeTrFrmTbl(this)' title='Delete'></span></td>" +
                    "</tr>";
                $('#_tbl_New_Purchasing_Body_').append(rows);
                rows = "";
            }
            $("select[name=new_vendor_Name]").val($("select[name=new_vendor_Name] option:checked").val()).change();
            SetAllInputsToDefault();
            Purchasing_Order_Total_Amount = 0
            $("select[name=new_vendor_Name]").attr("disabled", true);
            $('#_tbl_New_Purchasing_Body_ tr').each(function (indexoftr, tr) {
                Purchasing_Order_Total_Amount = Purchasing_Order_Total_Amount + ($(this).data('value-total'));
            });

            $('#_Purchased_Items_Total_').html('Total = ' + Purchasing_Order_Total_Amount);
        }
    }

    $("#get_searched_items").focus();
});

$(document).on('click', '.EditPurchaseItems', function (event) {
    $("select[name=new_item_Name]").val($(this).data('value-item')).change();
    $("select[name=new_vendor_Name]").val($(this).data('value-vendor')).change();
    $("input[name=new_item_Quantity]").val($(this).data('value-qty'));
    $("input[name=new_item_Msrmnt_Unit]").val($(this).data('value-msrmnt-unit'));
    $("input[name=new_item_Unit_Price]").val($(this).data('value-unit-price'));
    $("input[name=new_item_Price_Total]").val($(this).data('value-total'));
    $("#newPurchaseorderdetail_id").val($(this).data('value-pdid'));
    $('#Add_New_Purchased_Item').val("Update");
});

var Activity_Data = new Array();
$("#_Add_New_Purchase_Order_Form_Save_").click(function () {

    var data_value_item = "";
    var data_value_vendor = "";
    var data_value_qty = "";
    var data_value_msrmnt_unit = "";
    var data_value_unit_price = "";

    $('#_tbl_New_Purchasing_Body_ tr').each(function (indexoftr, tr) {
        var pdetailid = $(this).data('value-pdid');

        var lines = $('td', tr).map(function (indexoftd, td) {

            if (indexoftd == 0) {
                data_value_item = $(td).attr('data-value-item');
            }

            if (indexoftd == 1) {
                data_value_vendor = $(td).attr('data-value-vendor');
            }

            if (indexoftd == 2) {
                data_value_qty = $(td).attr('data-value-qty');
            }

            if (indexoftd == 3) {
                data_value_msrmnt_unit = $(td).attr('data-value-msrmnt-unit');
            }

            if (indexoftd == 4) {
                data_value_unit_price = $(td).attr('data-value-unit-price');
            }

            return "";
        });

        var data = { pdid: pdetailid, ItemId: data_value_item, VendorId: data_value_vendor, Quantity: data_value_qty, PriceUnit: data_value_unit_price, MsrmntUnit: data_value_msrmnt_unit }
        var ActivityData = { ActivityType_id: data_value_item, ActivityType: "Item", ActivityName: "Purchase Order", Icon: "fa fa-plus-square-o bg-blue" }
        purchase_order.push(data);
        Activity_Data.push(ActivityData);
    });

    if (purchase_order.length > 0) {

        AddPurchase();
    } else {
        $('#_Error_Message_Display_ p').html("Add an item first before you proceed your purchasing.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
});

$('input[name=new_item_Unit_Price]').keypress(function (e) {
    var key = e.which;
    if (key == 13)  // the enter key code
    {
        $('#Add_New_Purchased_Item').click();
        return false;
    }
});

var title;
$("select[name=new_item_Name]").on("change keyup paste", function () {

    GetAItem($("select[name=new_item_Name] option:selected").val());
    title = "Item " + "'" + $("select[name=new_item_Name] option:selected").text() + "'";
    PreviousOrders($("select[name=new_item_Name] option:selected").val(), 0, title);

});

$("select[name=new_vendor_Name]").on("change keyup paste", function () {

    GetAVendor($("select[name=new_vendor_Name] option:checked").val());
});
$("select[name=new_vendor_Name]").on("change keyup paste", function () {

    GetAVendor($("select[name=new_vendor_Name] option:checked").val());
    title = "Vendor " + "'" + $("select[name=new_vendor_Name] option:checked").text() + "'";
    PreviousOrders(0, $("select[name=new_vendor_Name] option:checked").val(), title);
});

$("input[name=new_item_Unit_Price] , input[name=new_item_Quantity]").on("change keyup paste", function () {
    $("input[name=new_item_Price_Total]").val($('input[name=new_item_Unit_Price]').val() * $('input[name=new_item_Quantity]').val());
});

function removeTrFrmTbl(el) {
    Purchasing_Order_Total_Amount = Purchasing_Order_Total_Amount - $(el).closest('tr').find("td:eq(5)").attr('data-value-total');
    $('#_Purchased_Items_Total_').html('Total = ' + Purchasing_Order_Total_Amount);
    $(el).closest('tr').remove();
}

function SetAllInputsToDefault() {
    $("select[name=new_item_Name]").val("0").trigger('change');
    //$("select[name=new_vendor_Name]").val("0").trigger('change');
    $("input[name=new_item_Quantity]").val("0");
    $("input[name=new_item_Unit_Price]").val("0");
    $("input[name=new_item_Price_Total]").val("0");
}

function AddPurchase() {
    $("#_Add_New_Purchase_Order_Form_Save_").attr("disabled", true);
    var url = '/Purchase/AddPurchase';
    var id = $('#newPurchaseorder_id').val();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "PurchaseData": JSON.stringify(purchase_order), id },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.pFlag == "1") {

            var id = resp.pPO_Output;
            $('#_Success_Message_Display_ > span').html(resp.Desc);
            $('#_Add_New_Purchase_Order_btn_').attr("disabled", false);
            $('._Add_New_Purchase_Form_').slideUp("slow");
            $('#_Success_Message_Display_').slideDown("slow");

            $("#newPurchaseorder_id").val("0");
            $("#newPurchaseorderdetail_id").val("0");
        }
        else {
            $("#_Add_New_Purchase_Order_Form_Save_").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html(resp.pDesc);
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }
        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);

        PurchasesList.ajax.reload(null, false);
        $("select[name=new_vendor_Name]").attr("disabled", true);
    }).fail(function () {
        $("#_Add_New_Purchase_Order_Form_Save_").attr("disabled", false);
        alert("post error 0");
    })


}

function formatRepo(repo) {
    if (repo.loading) {
        return repo.text;
    }
    var markup = "<div class='select2-result-repository clearfix'>" +
        "<div class='select2-result-repository__meta'>" +
        "<div class='select2-result-repository__title'>" + repo.text + "</div></div></div>";

    return markup;
}

function formatRepoSelection(repo) {
    return repo.text;
}

function GetAItem(x) {

    if (x == 0) {
        $('input[name=new_item_Msrmnt_Unit]').val("");
    } else {

        var token = $('[name=__RequestVerificationToken]').val();
        var _url_ = "/Item/Profile/" + x;
        $.ajax({
            url: _url_,
            type: "POST",
            data: { __RequestVerificationToken: token },
            datatype: 'json',
            async: false,
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp != "Provided Data is Incorrect") {
                $('input[name=new_item_Msrmnt_Unit]').val(resp.item.Item_Unit);
            }
            else {
                $('#_Error_Message_Display_ > p').html(resp);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
            return resp.item.Item_Name;

        }).fail(function () { })
    }
}

function GetAVendor(x) {
    if (x == 0) {
        $('#_Error_Message_Display_ > p').html("Please Select Vendor");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {

        var token = $('[name=__RequestVerificationToken]').val();
        var _url_ = "/Vendor/Profile/" + x;
        $.ajax({
            url: _url_,
            type: "POST",
            data: { __RequestVerificationToken: token },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp == "Provided Data is Incorrect") {
                $('#_Error_Message_Display_ > p').html(resp);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
        }).fail(function () { })
    }
}

//jQuery(document).keydown(function (event) {

//    if (event.which == 13) {
//        // Save Function
//        $('#Add_New_Purchased_Item').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$("#_PreviousOrders_Form_Remover_").click(function () {
    $('.previousorders').slideUp("slow");
});



$("#_Add_New_Vendor_Pop_").click(function () {
    $('.nav-tabs-custom a[href="#v-general-dtls"]').tab('show');
    CountriesDropdown();
    DropDownCompanies();
});

$("#_Add_New_Item_Pop_").click(function () {
    DropDownCategories();
    DropDownBrands();
    DropDownManufacturers();
    DropDownVendors();
    DropDownUnits();
});

