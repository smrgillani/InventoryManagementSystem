var Sales_Order_Status = "";
var SO_Invoice_Status;
var SO_Shipment_Status;
var SO_Package_Status;
var data_value_item = "";
var data_value_customer = "";
var data_value_qty = "";
var data_value_msrmnt_unit = "";
var data_value_unit_price = "";
var sales_order = new Array();
var Activity_Data = new Array();

$("#_Add_New_Sales_Order_btn_").click(function () {
    $('._Add_New_SalesOrder_Form_').slideDown("slow");
    $(this).attr("disabled", true);
});

$("#_Add_New_Sales_Order_Form_Remover__ , #_Add_New_Sales_Order_Form_Remover_").click(function () {
    $('#_Add_New_Sales_Order_btn_').attr("disabled", false);
    $('._Add_New_SalesOrder_Form_').slideUp("slow");
    $('._Add_New_SalesOrder_Form_').html('');
    $("#_Add_New_Sales_Order_Form_Save_").attr("disabled", false);
    $("#_Add_New_Sales_Order_Form_Draft_").attr("disabled", false);
});


$(function () {
    if ((window.location.pathname.toString().toLowerCase()) == "/salesorder/index") {
        DropDownCustomers();
        DropDownItems();
        $('.select2').select2();
    }
})

$("#Add_New_SalesOrder_Item").click(function () {
    var rows = "";
    var item_up = "";
    var item_id = "";

    Sales_Order_Item_Msrmnt_Unit = $("input[name=new_item_Msrmnt_Unit]").val();
    var GetNewItemsNameValue = $("select[name=new_item_Name] option:checked").text();
    var GetNewItemsCustomerValue = $("select[name=new_customer_Name] option:checked").text();
    var GetNewItemsQuantityValue = $("input[name=new_item_Quantity]").val();
    var GetNewItemsUnitPriceValue = $("input[name=new_item_Unit_Price]").val();
    var sdid = $('#newSaleorderdetail_id').val();
    if ($("select[name=new_item_Name] option:selected").val() === "0") {
        $('#_Error_Message_Display_ p').html("Please select an item to add into sales order.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else if ($("select[name=new_customer_Name] option:selected").val() === "0" || $("select[name=new_customer_Name] option:selected").val() === null) {
        $('#_Error_Message_Display_ p').html("Please select a Customer.");
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
    } else if (Sales_Order_Item_Msrmnt_Unit == "") {
        $('#_Error_Message_Display_ p').html("Measurement unit for your item not loaded successfully , update your item measurement unit by going at the item page.");
        $('#_Error_Message_Di_Error_Message_Display_splay_').slideDown();
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
    } else {

        if ($("#Add_New_SalesOrder_Item").val() === "Update") {
            $('#Add_New_SalesOrder_Item').val("Add");
            var newItem_id = $("select[name=new_item_Name] option:selected").val();
            var newItem_up = $("input[name=new_item_Unit_Price]").val();
            var newItem_Qty = $("input[name=new_item_Quantity]").val();
            $('#_tbl_New_SaleOrder_Body_ tr').each(function (indexoftr, tr) {

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
                    removeTrFrmTblsso($(this));
                }
            });
            var rows = "<tr data-value-sdid='" + sdid + "' data-value-qty='" + GetNewItemsQuantityValue + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" +
                "<td data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "'>" + GetNewItemsNameValue + "</td>" +
                "<td data-value-customer='" + $("select[name=new_customer_Name] option:checked").val() + "'>" + GetNewItemsCustomerValue + "</td>" +
                "<td data-value-qty='" + GetNewItemsQuantityValue + "'>" + GetNewItemsQuantityValue + "</td>" +
                "<td data-value-msrmnt-unit='" + Sales_Order_Item_Msrmnt_Unit + "'> " + Sales_Order_Item_Msrmnt_Unit + "</td>" +
                "<td data-value-unit-price='" + GetNewItemsUnitPriceValue + "'>" + GetNewItemsUnitPriceValue + "</td>" +
                "<td data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "</td>" +

                "<td><span id='Edit_SaleItems' data-value-sdid='" + sdid + "' data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "' " +
                "data-value-customer='" + $("select[name=new_customer_Name] option:checked").val() + "' data-value-qty='" + GetNewItemsQuantityValue + "' " +
                "data-value-msrmnt-unit='" + Sales_Order_Item_Msrmnt_Unit + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' " +
                "data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'  class='fa fa-pencil-square-o EditSaleItems'></span>" +

                "<span class='fa fa-trash' onclick='removeTrFrmTblsso(this)' title='Delete'></span></td>" +
                "</tr>";

            $('#_tbl_New_SaleOrder_Body_').append(rows);
            $("select[name=new_customer_Name]").val($("select[name=new_customer_Name] option:checked").val()).change();
            SetAllInputsToDefault();
            Sales_Order_Total_Amount = 0;
            $("select[name=new_customer_Name]").attr("disabled", true);
            $('#_tbl_New_SaleOrder_Body_ tr').each(function (indexoftr, tr) {
                //GetNewItemsQuantityValue = $(this).data('value-qty');
                //GetNewItemsUnitPriceValue = $(this).data('value-unit-price');
                //Sales_Order_Total_Amount = Sales_Order_Total_Amount + (GetNewItemsQuantityValue * GetNewItemsUnitPriceValue);
                Sales_Order_Total_Amount = Sales_Order_Total_Amount + $(this).data('value-total');
            });

            $('#_SalesOrder_Items_Total_').html('Total = ' + Sales_Order_Total_Amount);

        } else if ($("#Add_New_SalesOrder_Item").val() === "Add") {

            var rowCount = $('#_tbl_New_SaleOrder_Body_ tr').length;
            if (rowCount == 0) {
                sdid = 0;
                var rows = "<tr data-value-sdid='" + sdid + "' data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" +
                    "<td data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "'>" + GetNewItemsNameValue + "</td>" +
                    "<td data-value-customer='" + $("select[name=new_customer_Name] option:checked").val() + "'>" + GetNewItemsCustomerValue + "</td>" +
                    "<td data-value-qty='" + GetNewItemsQuantityValue + "'>" + GetNewItemsQuantityValue + "</td>" +
                    "<td data-value-msrmnt-unit='" + Sales_Order_Item_Msrmnt_Unit + "'> " + Sales_Order_Item_Msrmnt_Unit + "</td>" +
                    "<td data-value-unit-price='" + GetNewItemsUnitPriceValue + "'>" + GetNewItemsUnitPriceValue + "</td>" +
                    "<td data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "</td>" +

                    "<td><span id='Edit_SaleItems' data-value-sdid='" + sdid + "' data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "' " +
                    "data-value-customer='" + $("select[name=new_customer_Name] option:checked").val() + "' data-value-qty='" + GetNewItemsQuantityValue + "' " +
                    "data-value-msrmnt-unit='" + Sales_Order_Item_Msrmnt_Unit + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' " +
                    "data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'  class='fa fa-pencil-square-o EditSaleItems'></span>" +

                    "<span class='fa fa-trash' onclick='removeTrFrmTblsso(this)' title='Delete'></span></td>" +
                    "</tr>";
                $('#_tbl_New_SaleOrder_Body_').append(rows);

            }
            else {
                var newItem_id = $("select[name=new_item_Name] option:selected").val();
                var newItem_up = $("input[name=new_item_Unit_Price]").val();
                var newItem_Qty = $("input[name=new_item_Quantity]").val();

                $('#_tbl_New_SaleOrder_Body_ tr').each(function (indexoftr, tr) {
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
                        sdid = $(this).data('value-sdid');
                        GetNewItemsQuantityValue = parseInt(data_value_qty) + parseInt(newItem_Qty);

                        GetNewItemsUnitPriceValue = item_up;
                        removeTrFrmTblsso($(this));

                    } else {
                        sdid = 0;
                    }
                });
                rows = "<tr data-value-sdid='" + sdid + "' data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" +
                    "<td data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "'>" + GetNewItemsNameValue + "</td>" +
                    "<td data-value-customer='" + $("select[name=new_customer_Name] option:checked").val() + "'>" + GetNewItemsCustomerValue + "</td>" +
                    "<td data-value-qty='" + GetNewItemsQuantityValue + "'>" + GetNewItemsQuantityValue + "</td>" +
                    "<td data-value-msrmnt-unit='" + Sales_Order_Item_Msrmnt_Unit + "'> " + Sales_Order_Item_Msrmnt_Unit + "</td>" +
                    "<td data-value-unit-price='" + GetNewItemsUnitPriceValue + "'>" + GetNewItemsUnitPriceValue + "</td>" +
                    "<td data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'>" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "</td>" +
                    "<td><span id='Edit_SaleItems' data-value-sdid='" + sdid + "' data-value-item='" + $("select[name=new_item_Name] option:checked").val() + "' " +
                    "data-value-customer='" + $("select[name=new_customer_Name] option:checked").val() + "' data-value-qty='" + GetNewItemsQuantityValue + "' " +
                    "data-value-msrmnt-unit='" + Sales_Order_Item_Msrmnt_Unit + "' data-value-unit-price='" + GetNewItemsUnitPriceValue + "' " +
                    "data-value-total='" + GetNewItemsQuantityValue * GetNewItemsUnitPriceValue + "'  class='fa fa-pencil-square-o EditSaleItems'></span>" +

                    "<span class='fa fa-trash' onclick='removeTrFrmTblsso(this)' title='Delete'></span></td>" +
                    "</tr>";
                $('#_tbl_New_SaleOrder_Body_').append(rows);
                rows = "";
            }
            $("select[name=new_customer_Name]").val($("select[name=new_customer_Name] option:checked").val()).change();
            SetAllInputsToDefault();

            $("select[name=new_customer_Name]").attr("disabled", true);
            Sales_Order_Total_Amount = 0;
            $('#_tbl_New_SaleOrder_Body_ tr').each(function (indexoftr, tr) {

                Sales_Order_Total_Amount = Sales_Order_Total_Amount + $(this).data('value-total');

            });
            $('#_SalesOrder_Items_Total_').html('Total = ' + Sales_Order_Total_Amount);
        }
    }
    $('#get_searched_items').focus();
});

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

function removeTrFrmTblsso(elsso) {
    Sales_Order_Total_Amount = Sales_Order_Total_Amount - $(elsso).closest('tr').find("td:eq(5)").attr('data-value-total');
    $('#_SalesOrder_Items_Total_').html('Total = ' + Sales_Order_Total_Amount);
    $(elsso).closest('tr').remove();
}

$("#_Add_New_Sales_Order_Form_Save_").click(function () {
    $(this).attr("disabled", true);
    Sales_Order_Status = "Confirm";
    SO_Invoice_Status = "Not Invoiced";
    SO_Shipment_Status = "0";
    SO_Package_Status = "0";
    GetAddSOData();
});

$("#_Add_New_Sales_Order_Form_Draft_").click(function () {
    $(this).attr("disabled", true);
    Sales_Order_Status = "Draft";
    SO_Invoice_Status = "Not Invoiced";
    SO_Shipment_Status = "0";
    SO_Package_Status = "0";
    GetAddSOData();
});

$("select[name=new_item_Name]").on("change keyup paste", function () {
    GetAItem($("select[name=new_item_Name] option:checked").val());
});

$("input[name=new_item_Unit_Price] , input[name=new_item_Quantity]").on("change keyup paste", function () {
    $("input[name=new_item_Price_Total]").val($('input[name=new_item_Unit_Price]').val() * $('input[name=new_item_Quantity]').val());
});

$("input[name=new_item_Quantity]").on("change keyup paste", function () {
    var Item_id = $("select[name=new_item_Name] option:checked").val();
    if (Item_id != null || Item_id != "0") {
        var token = $('[name=__RequestVerificationToken]').val();

        var _url_ = "/Stock/SelectStockByItemid/";
        $.ajax({
            url: _url_,
            type: "POST",
            data: { __RequestVerificationToken: token, Item_id: Item_id },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            console.log(resp.Physical_Avail_ForSale);
            var AvailableForSale = resp.Physical_Avail_ForSale;
            console.log((parseInt($("input[name=new_item_Quantity]").val()) - parseInt(AvailableForSale)));
            if (parseInt(AvailableForSale) < parseInt($("input[name=new_item_Quantity]").val())) {
                $("#Add_New_SalesOrder_Item").attr("disabled", true);
                $('#_Error_Message_Display_ p').html("Available quantity is " + AvailableForSale);
                $('#_Error_Message_Display_').slideDown();
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
            else {
                $("#Add_New_SalesOrder_Item").attr("disabled", false);
            }

        }).fail(function () {
            $('#_Error_Message_Display_ p').html("Network Error");
            $('#_Error_Message_Display_').slideDown();
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);
        })
    }
});

function GetAItem(x) {
    if (x == 0) { $('input[name=new_item_Msrmnt_Unit]').val(""); } else {
        var token = $('[name=__RequestVerificationToken]').val();
        var _url_ = "/Item/Profile/" + x;
        $.ajax({
            url: _url_,
            type: "POST",
            data: { __RequestVerificationToken: token },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            $('input[name=new_item_Msrmnt_Unit]').val(resp.item.Item_Unit);
        }).fail(function () { })
    }
}

function SetAllInputsToDefault() {
    $("select[name=new_item_Name]").val("0").trigger('change');
    $("input[name=new_item_Quantity]").val("0");
    $("input[name=new_item_Unit_Price]").val("0");
    $("input[name=new_item_Price_Total]").val("0");
}

function GetAddSOData() {

    $('#_tbl_New_SaleOrder_Body_ tr').each(function (indexoftr, tr) {
        var sdetailid = $(this).data('value-sdid');
        var lines = $('td', tr).map(function (indexoftd, td) {

            if (indexoftd == 0) {
                data_value_item = $(td).attr('data-value-item');
            }

            if (indexoftd == 1) {
                data_value_customer = $(td).attr('data-value-customer');
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

        var data = {
            sdid: sdetailid,
            ItemId: data_value_item,
            Customer_id: data_value_customer,
            Quantity: data_value_qty,
            PriceUnit: data_value_unit_price,
            MsrmntUnit: data_value_msrmnt_unit,
            SO_Total_Amount: Sales_Order_Total_Amount,
            SO_Status: Sales_Order_Status,
            SO_Invoice_Status: SO_Invoice_Status,
            SO_Shipment_Status: SO_Shipment_Status,
            SO_Package_Status: SO_Package_Status,

        }
        var ActivityData = {
            ActivityType_id: data_value_item,
            ActivityType: "Item",
            ActivityName: "Sales Order",
            Icon: "fa fa-check bg-blue"
        }
        sales_order.push(data);
        Activity_Data.push(ActivityData);
    });
    if (sales_order.length > 0) {

        AddSalesOrder();
    } else {
        $("#_Add_New_Sales_Order_Form_Save_").attr("disabled", false);
        $("#_Add_New_Sales_Order_Form_Draft_").attr("disabled", false);
        $('#_Error_Message_Display_ p').html("Add an item first before you proceed your Sales Order.");
        $('#_Error_Message_Display_').slideDown();
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
}

function AddSalesOrder() {
    $("#_Add_New_Sales_Order_Form_Save_").attr("disabled", true);
    $("#_Add_New_Sales_Order_Form_Draft_").attr("disabled", true);
    var url = '/SalesOrder/AddSalesOrder';
    var SalesOrder_id = $('#newSaleorder_id').val();
    console.log(SalesOrder_id);
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "SalesOrderData": JSON.stringify(sales_order), SalesOrder_id },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $('#_Add_New_Sales_Order_btn_').attr("disabled", false);
        $("#_Add_New_Sales_Order_Form_Save_").attr("disabled", false);
        $("#_Add_New_Sales_Order_Form_Draft_").attr("disabled", false);
        if (resp.pFlag == "1") {
            var id = resp.pSO_Output;
            getSOItemsForStock(id)
            $('#_Success_Message_Display_ > p').html('Sales Order Created Successfully.');
            $('._Add_New_SalesOrder_Form_').slideUp("slow");
            $('#_Success_Message_Display_').slideDown("slow");
            SalesOrderList.ajax.reload(null, false);

            $("#newSaleorder_id").val("0");
            $("#newSaleorderdetail_id").val("0");
        }

        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
        $("select[name=new_customer_Name]").attr("disabled", true);
    }).fail(function () {
        $("#_Add_New_Sales_Order_Form_Save_").attr("disabled", false);
        $("#_Add_New_Sales_Order_Form_Draft_").attr("disabled", false);
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    })


}

function getSOItemsForStock(id) {
    Items_For_Stock = new Array();
    var url = '/SalesOrder/SelectSOItemsSOid';

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, id },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $.each(resp.SOItems, function (index, i) {

            var data = {
                Item_id: i.ItemId,
                Physical_Quantity: i.ItemQty,
                Accounting_Quantity: i.ItemQty,
            }
            console.log(data);
            Items_For_Stock.push(data);
        });
        if (Items_For_Stock.length > 0) {
            UpdateSOItemStock();
        }

    }).fail(function () {
        alert("post error 0");
    })
}

function UpdateSOItemStock() {
    var url = '/Stock/UpdateSOitemStockOnInsert';

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

//jQuery(document).keydown(function (event) {

//    if (event.which == 27) {
//        // Save Function
//        $('#_Add_New_Sales_Order_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

//jQuery(document).keydown(function (event) {

//    if (event.which == 13) {
//        // Save Function
//        $('#Add_New_SalesOrder_Item').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$("#_Add_New_Customer_Pop_").click(function () {
    CountriesDropdown();
    DropDownCompanies();
});

$("#_Add_New_Item_Pop_").click(function () {
    $('.nav-tabs-custom a[href="#i-general-dtls"]').tab('show');
    DropDownCategories();
    DropDownBrands();
    DropDownManufacturers();
    DropDownVendors();
    DropDownUnits();
});

$("#_Add_New_Company_Pop_").click(function () {
    $('.nav-tabs-custom a[href="#c-general-dtls"]').tab('show');
    CountriesDropdown();
});




$("#getAddress_country").change(function () {
    var Country_id = $("select[name=Address_country] option:checked").val();
    CitiesDropdown(Country_id);
});
