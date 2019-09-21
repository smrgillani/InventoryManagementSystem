var ItemsList;
var Items_For_Stock = new Array();
$('.select2').select2();
$("#_Add_New_Item_btn_").click(function () {
    $(this).attr("disabled", true);
    AddItemForm();

});

function AddItemForm() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Item/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_Item_Form_').html(resp);
            $('.nav-tabs a[href="#i-general-dtls"]').tab('show');
            $('#_Add_New_Item_Form_Remover__').html("Cancel");
            $('#_Add_New_Item_Form_Save_').html("Next");
            $('._Add_New_Item_Form_').slideDown("slow");
            $("#_Add_New_Item_Form_Save_").attr("disabled", false);
            $('input[name=item_Name]').focus();
            $('input[name=item_Sell_Price]').val();
            $('input[name=item_Tax]').val();
            $('input[name=item_Purchase_Price]').val();
            $('input[name=item_Opening_Stock]').val();
            $('input[name=item_Reorder_Level]').val();
            DropDownCategories();
            DropDownBrands();
            DropDownManufacturers();
            DropDownVendors();
            DropDownUnits();
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Item_Form_').slideUp("slow");
            $('._Add_New_Item_Form_').html("");
            $("#_Add_New_Item_btn_").attr("disabled", false);
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



$("#_Add_New_Item_Form_Remover_").click(function () {
    ItemFormRemover();
});

$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if ((window.location.pathname.toString().toLowerCase()) == "/item/items") {
        GetAllItemsList(null);
        DropDownCategories();
        DropDownBrands();
        DropDownManufacturers();
        DropDownVendors();
        DropDownUnits();
    } else if (url.includes("/item/profile/")) {
        var Item_id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
        Activities(Item_id, "Item");
        ProfileViewItems(url);
        if (ItemsTransactionList != null) {
            ItemsTransactionList.destroy();
        }
        ItemsTransactionSO(Item_id);
    }

})

function GetAllItemsList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    ItemsList = $('#Items_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No items available"
        },
        "Filter": false,
        "sAjaxSource": "/Item/GetAllItems/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=ItemStartDate]').val(), EndDate: $('input[name=ItemEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[55].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/item/profile/" + data.id + "'>" + data.Item_Name + "</a>";

                }
            },
            { data: "Item_type" },
            { data: "Item_Category" },
            { data: "Item_Upc" },
            { data: "Item_Sell_Price" },
            { data: "Item_Unit" },
            { data: "Item_Preferred_Vendor" },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditItem(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepItem(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditItem(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepItem(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkItemDel" data_value_itemid=' + data.id + ' data_value_itemname=' + data.Item_Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkItemDel" data_value_itemid=' + data.id + ' data_value_itemname=' + data.Item_Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

function UpdatepItem(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Item/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            ItemsList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Item Profile Visibility Updated Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
        } else {
            $('#_Error_Message_Display_ > p').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$("input[name=ItemStartDate]").change(function () {
    ItemsList.ajax.reload(null, false);
});

$("input[name=ItemEndDate]").change(function () {
    ItemsList.ajax.reload(null, false);
});

function GetAItem(id) {

    var token = $('[name=__RequestVerificationToken]').val();

    var _url_ = "/item/Profile/";

    $.ajax({
        url: _url_,
        type: "POST",
        data: {
            __RequestVerificationToken: token, id
        },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

    }).fail(function () {
        alert("error 1");
    });
}

function GetAllVendorsListForItem() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Vendor/GetAllVendors',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");
        var item = '';
        $.each(resp, function (i, item) {
            var rows = "<option value='" + item.id + "'>" + item.Full_name + "</option>";
            $("select[name=item_Preferred_Vendor]").append(rows);
        });
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function EditItem(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $('input[name=itemid]').val(id);
    $.ajax({
        async: false,
        url: '/Item/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        if (resp != false) {
            AddItemForm();
            $("#_Add_New_Item_Form_Save_").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            $('.nav-tabs a[href="#i-general-dtls"]').tab('show');
            $('._Add_New_Item_Form_').slideDown("slow");
            $('#_Add_New_Item_Form_Save_').html("Next");
            $('input[name=stockid]').val(resp.item.Stock_id);
            $('input[name=itemid]').val(resp.item.id);

            $("select[name=item_type]").val(resp.item.Item_type).change();
            $('input[name=item_Name]').val(resp.item.Item_Name);
            $('input[name=item_Sku]').val(resp.item.Item_Sku);
            $('input[name=item_Upc]').val(resp.item.Item_Upc);
            $('input[name=item_Mpn]').val(resp.item.Item_Mpn);
            $('input[name=item_Ean]').val(resp.item.Item_Ean);
            $('input[name=item_Isbn]').val(resp.item.Item_Isbn);
            $('input[name=item_Sell_Price]').val(resp.item.Item_Sell_Price);
            $('input[name=item_Tax]').val(resp.item.Item_Tax);
            $('input[name=item_Purchase_Price]').val(resp.item.Item_Purchase_Price);
            if (resp.check != null) {
                $('.nav-tabs-custom a[href="#i-openingstock-dtls"]').tab().hide();
            } else {
                $('.nav-tabs-custom a[href="#i-openingstock-dtls"]').tab().show();
            }
            $('input[name=item_Opening_Stock]').val(resp.item.OpeningStock);
            $('input[name=item_Reorder_Level]').val(resp.item.ReorderLevel);
            $("select[name=item_Manufacturer]").val(resp.item.Manufacturer_id).change();
            $("select[name=item_Category]").val(resp.item.Category_id).change();
            $("select[name=item_Unit]").val(resp.item.Unit_id).change();
            $("select[name=item_Brand]").val(resp.item.Brand_id).change();
            $("select[name=item_Preferred_Vendor]").val(resp.item.Vendor_id).change();
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Item_Form_').slideUp("slow");
            $('._Add_New_Item_Form_').html("");
            $("#_Add_New_Item_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        //$('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function DelItem(id) {
    $("#PDeleteItem").attr("disabled", true);
    checked_Items = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/item/profile/")) {
        id = $("input[name=items_id]").val();
        name = $("#pHname").text();
    }
    var data = { id: id, Item_Name: name, Delete_Status: "Requested" }
    checked_Items.push(data);
    url = "/item/items";
    SendRequestToDelItems(url);

}

function ProfileViewItems() {

    var token = $('[name=__RequestVerificationToken]').val();
    var id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
    $.ajax({
        //async: true,
        url: '/item/profile/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp.item.Delete_Status == "Requested") {
            $("#PDeleteItem").attr("disabled", true);
        } else {
            $("#PDeleteItem").attr("disabled", false);
        }
        $("input[name=items_id]").val(resp.item.id);
        if (resp.item.File_Name == null || resp.item.File_Name == "") {
            $("#PItemImage").attr("src", "/Images/ItemImages/NoImageAvailable.jpg");
        } else {
            $("#PItemImage").attr("src", resp.item.File_Name);
        }
        $("#cp__item_status").html(resp.item.IsEnabled_);
        $("#PItemType").html(resp.item.Item_type);
        $("#PItemSKU").html(resp.item.Item_Sku);
        $("#pHname").html(resp.item.Item_Name);
        $("#pHsku").html(resp.item.Item_Sku);
        $("#PItemUnit").html(resp.item.Item_Unit);
        if (resp.item.Item_Upc == null || resp.item.Item_Upc == "") {
            $("#PItemUPC").html("-");
        }
        else {
            $("#PItemUPC").html(resp.item.Item_Upc);
        }
        if (resp.item.Item_Ean == null || resp.item.Item_Ean == "") {
            $("#PItemEAN").html("-");
        }
        else {
            $("#PItemEAN").html(resp.item.Item_Ean);
        }
        if (resp.item.Item_Mpn == null || resp.item.Item_Mpn == "") {
            $("#PItemMPN").html("-");
        }
        else {
            $("#PItemMPN").html(resp.item.Item_Mpn);
        }
        if (resp.item.Item_Isbn == null || resp.item.Item_Isbn == "") {
            $("#PItemISBN").html("-");
        }
        else {
            $("#PItemISBN").html(resp.item.Item_Isbn);
        }
        $("#PItemManufacturer").html(resp.item.Item_Manufacturer);
        $("#PItemBrand").html(resp.item.Item_Brand);
        $("#PItemPurchasePrice").html(resp.item.Item_Purchase_Price);
        $("#PItemSellPrice").html(resp.item.Item_Sell_Price);

        $(".PAccStockOnHand").html(resp.stock.Accounting_Quantity);
        $(".pAccCommittedStock").html(resp.stock.Acc_Commited);
        $(".PAccAvail").html(resp.stock.Acc_Avail_ForSale);
        $(".PPhyStockOnHand").html(resp.stock.Physical_Quantity);
        $(".PPhyCommittedStock").html(resp.stock.Physical_Committed);
        $(".PPhyAvail").html(resp.stock.Physical_Avail_ForSale);
        $(".PReorderLevel").html(resp.stock.ReorderLevel);

    }).fail(function () {
        alert("error 1");
    });

}

$("select#item_Transaction_Filter").change(function () {
    var selectedText = $(this).find("option:selected").text();
    var Item_id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];

    if (selectedText === "Sales Order") {

        $("#no").html("Sales Order No");
        $("#contact").html("Customer Name");
        $("#qty").html("Quantity Ordered");
        $("#price").html("Price");
        $("#total").html("Total");
        $("#status").html("Status");
        $("#date").html("Date");
        if (ItemsTransactionList != null) {
            ItemsTransactionList.destroy();
        }
        ItemsTransactionSO(Item_id);
    } else if (selectedText === "Invoices") {

        $("#no").html("Invoice No");
        $("#contact").html("Customer Name");
        $("#qty").html("Quantity Sold");
        $("#price").html("Price");
        $("#total").html("Total");
        $("#status").html("Status");
        $("#date").html("Date");
        if (ItemsTransactionList != null) {
            ItemsTransactionList.destroy();
        }
        ItemsTransactionINV(Item_id);
    } else if (selectedText === "Purchase Order") {

        $("#no").html("Purchase Order No");
        $("#contact").html("Vendor Name");
        $("#qty").html("Quantity Ordered");
        $("#price").html("Price");
        $("#total").html("Total");
        $("#status").html("Status");
        $("#date").html("Date");
        if (ItemsTransactionList != null) {
            ItemsTransactionList.destroy();
        }
        ItemsTransactionPO(Item_id);
    }
    else if (selectedText === "Bills") {

        $("#no").html("Bill No");
        $("#contact").html("Vendor Name");
        $("#qty").html("Quantity Purchased");
        $("#price").html("Price");
        $("#total").html("Total");
        $("#status").html("Status");
        $("#date").html("Date");
        if (ItemsTransactionList != null) {
            ItemsTransactionList.destroy();
        }
        ItemsTransactionBill(Item_id);
    }
});

var ItemsTransactionList = null;
function ItemsTransactionSO(Item_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Item_id;
    ItemsTransactionList = $('#Items_Transactions').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Item/ItemsTransactionSO/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[40].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [

            { data: "SaleOrderNo" },
            { data: "Customer_Name" },
            { data: "ItemQty" },
            { data: "PriceUnit" },
            { data: "SO_Total_Amount" },
            { data: "SO_Status" },
            { data: "SO_DateTime" }

        ],

    });

}

function ItemsTransactionPO(Item_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Item_id;
    ItemsTransactionList = $('#Items_Transactions').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Item/ItemsTransactionPO/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[40].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [

            { data: "TempOrderNum" },
            { data: "VendorName" },
            { data: "ItemQty" },
            { data: "PriceUnit" },
            { data: "TotalPrice" },
            { data: "RecieveStatus" },
            { data: "RecieveDateTime" }

        ],

    });











    //$.ajax({
    //    url: '/Item/ItemsTransactionPO/',
    //    type: "POST",
    //    data: { __RequestVerificationToken: token, Item_id },
    //}).done(function (resp) {

    //    $('#_tbl_ItemTransaction_Body_').empty();
    //    $.each(resp, function (i, item) {
    //        var rows = "<tr><td class='table_content_vertical_align_'>" + item.TempOrderNum + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.VendorName + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.PriceUnit + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.ItemQty * item.PriceUnit + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.RecieveStatus + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.RecieveDateTime + "</td>" +
    //            "</tr>";
    //        $('#_tbl_ItemTransaction_Body_').append(rows);
    //    });



    //}).fail(function () {

    //    $('#_Error_Message_Display_ > p').html("Error!");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //});

    //$('#Items_Transactions').dataTable(

    //);
}

function ItemsTransactionINV(Item_id) {

    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Item_id;
    ItemsTransactionList = $('#Items_Transactions').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Item/ItemsTransactionINV/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[40].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [

            { data: "Invoice_No" },
            { data: "Customer_Name" },
            { data: "ItemQty" },
            { data: "PriceUnit" },
            { data: "Invoice_Amount" },
            { data: "Invoice_Status" },
            { data: "InvoiceDateTime" }

        ],

    });












    //$.ajax({
    //    url: '/Item/ItemsTransactionINV/',
    //    type: "POST",
    //    data: { __RequestVerificationToken: token, Item_id },
    //}).done(function (resp) {

    //    $('#_tbl_ItemTransaction_Body_').empty();
    //    $.each(resp, function (i, item) {
    //        var rows = "<tr><td class='table_content_vertical_align_'>" + item.Invoice_No + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.Customer_Name + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.PriceUnit + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.ItemQty * item.PriceUnit + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.Invoice_Status + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.InvoiceDateTime + "</td>" +
    //            "</tr>";
    //        $('#_tbl_ItemTransaction_Body_').append(rows);
    //    });



    //}).fail(function () {

    //    $('#_Error_Message_Display_ > p').html("Error!");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //});

    //$('#Items_Transactions').dataTable(

    //);
}

function ItemsTransactionBill(Item_id) {

    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Item_id;
    ItemsTransactionList = $('#Items_Transactions').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Item/ItemsTransactionBill/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[40].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [

            { data: "Bill_No" },
            { data: "Vendor_Name" },
            { data: "ItemQty" },
            { data: "PriceUnit" },
            { data: "Bill_Amount" },
            { data: "Bill_Status" },
            { data: "BillDateTime" }

        ],

    });
    //$.ajax({
    //    url: '/Item/ItemsTransactionBill/',
    //    type: "POST",
    //    data: { __RequestVerificationToken: token, Item_id },
    //}).done(function (resp) {

    //    $('#_tbl_ItemTransaction_Body_').empty();
    //    $.each(resp, function (i, item) {
    //        var rows = "<tr><td class='table_content_vertical_align_'>" + item.Bill_No + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.Vendor_Name + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.PriceUnit + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.ItemQty + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.ItemQty * item.PriceUnit + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.Bill_Status + "</td>" +
    //            "<td class='table_content_vertical_align_'>" + item.BillDateTime + "</td>" +
    //            "</tr>";
    //        $('#_tbl_ItemTransaction_Body_').append(rows);
    //    });



    //}).fail(function () {

    //    $('#_Error_Message_Display_ > p').html("Error!");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //});

    //$('#Items_Transactions').dataTable(

    //);
}

var checked_Items;
function getCheckedItemstoDel() {
    var data_value_itemid = "";
    var data_value_itemname = "";

    checked_Items = new Array();

    $('#_tbl_Item_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkItemDel')).is(':checked')) {
            data_value_itemid = $(this).find($('td')).find($('.chkItemDel')).attr('data_value_itemid');
            data_value_itemname = $(this).find($('td')).find($('.chkItemDel')).attr('data_value_itemname');
            var data = { id: data_value_itemid, Item_Name: data_value_itemname, Delete_Status: "Requested" }
            checked_Items.push(data);
        }
    });
    if (checked_Items.length > 0) {
        SendRequestToDelItems();
    } else {
        $('#_Error_Message_Display_').html("There are No Items to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteItems").click(function () {
    getCheckedItemstoDel();
});

function SendRequestToDelItems(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Item/SendRequestToDelItems/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteItemsData": JSON.stringify(checked_Items)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.ItemsNotDelete.length != 0) {
                $('#_Error_Message_Display_ > p').html('Some of The Items cannot be deleted as they are part of other transactions.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                ItemsList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_ ').slideUp("slow"); }, 5000);

            }
            else {
                $("#PDeleteItem").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");

                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        ItemsList.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                ItemsList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteItems').css('display', 'none');
        }
        else {
            $("#PDeleteItem").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteItem").attr("disabled", false);
    });
}

$(document).on("change", ".chkItemDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteItems').show();
    }
    else {
        $('#_Send_request_To_DeleteItems').css('display', 'none');
    }
});

//jQuery(document).keydown(function (event) {

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Item_Form_Save_').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

//jQuery(document).keydown(function (event) {

//    if (event.which == 27) {
//        // Save Function
//        $('#_Add_New_Item_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$(".ListItemFilter").click(function () {
    ItemsList.destroy();
    var filtertype = $(this).text();
    GetAllItemsList(filtertype);
});

//$("#_Get_Uploaded_file_").click(function () {
//    if ($(this).html() == "Upload File") {
//        uploadFile();
//    }
//    if ($(this).html() == "Import") {
//        importItems();
//    }
//});

//function uploadFile() {
//    var token = $('[name=__RequestVerificationToken]').val();
//    var file = $("#ImportItemsFile").get(0).files;
//    var data = new FormData;
//    data.append("__RequestVerificationToken", token);
//    data.append("File", file[0]);

//    $.ajax({
//        async: false,
//        url: '/item/getuploadedfiledata/',
//        type: "post",
//        //data: { __requestverificationtoken: token, },
//        data: data,
//        contentType: false,
//        processData: false
//    }).done(function (resp) {
//        console.log(resp.Items);
//        $.each(resp.Items, function (i, item) {

//            var rows = "<tr>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_type + "</td>" +
//                "<td class='table_content_vertical_align_' hidden>" + item.File_Name + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Name + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Sku + "</td>" +
//                "<td class='table_content_vertical_align_' hidden>" + item.Item_Category + "</td>" +
//                "<td class='table_content_vertical_align_' hidden>" + item.Item_Unit + "</td>" +
//                "<td class='table_content_vertical_align_' hidden>" + item.Item_Manufacturer + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Upc + "</td>" +
//                "<td class='table_content_vertical_align_' hidden>" + item.Item_Brand + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Mpn + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Ean + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Isbn + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Sell_Price + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Tax + "</td>" +
//                "<td class='table_content_vertical_align_' >" + item.Item_Purchase_Price + "</td>" +
//                "<td class='table_content_vertical_align_' hidden>" + item.Item_Preferred_Vendor + "</td>" +
//                "</tr>";
//            $('#_tbl_Items_Import_body_').append(rows);
//        });
//        $("#_Get_Uploaded_file_").html("Import");

//    }).fail(function () {
//        //$('#_error_message_display_ > p').html('network error.');
//        //$('#_error_message_display_').slidedown("slow");
//        //$('html, body').animate({ scrolltop: $('#_error_message_display_').offset().top }, 'slow');
//    });
//}

//function importItems() {

//}

$("#_Save_New_Category_Pop_").click(function () {
    InsertUpdateCategory();
    $('#modal-AddNewCategory').modal('toggle');
    $('input[name=category_Name]').val("");
    DropDownCategories();
    setTimeout(function () {
        $('#_Success_Message_Display_').slideUp("slow");
    }, 5000);
});

$("#_Save_New_Brand_Pop_").click(function () {
    InsertUpdateBrands();
    $('input[name=brand_Name]').val("");
    $('#modal-AddNewBrand').modal('toggle');
    DropDownBrands();
    setTimeout(function () {
        $('#_Success_Message_Display_').slideUp("slow");
    }, 5000);
});

$("#_Save_New_Manufacturer_Pop_").click(function () {
    InsertUpdateManufacturer();
    $('input[name=manufacturer_Name]').val("");
    DropDownManufacturers();
    $('#modal-AddNewManufacturer').modal('toggle');
    setTimeout(function () {
        $('#_Success_Message_Display_').slideUp("slow");
    }, 5000);
});

$("#_Save_New_Unit_Pop_").click(function () {
    InsertUpdateUnit();
    $('input[name=unit_Name]').val("");
    $('#modal-AddNewUnit').modal('toggle');
    DropDownUnits();
    setTimeout(function () {
        $('#_Success_Message_Display_').slideUp("slow");
    }, 5000);
});

$("#_Add_New_Vendor_Pop_").click(function () {
    $('.nav-tabs-custom a[href="#v-general-dtls"]').tab('show');
    CountriesDropdown();
    DropDownCompanies();
});

$("#_Save_New_Vendor_Pop_").click(function () {
    if ($(this).html() == "Next") {
        $('.nav_tabs_for_new_vendor > .active').next('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_vendor > .active > a').attr("href") == "#v-payment-dtls") {
            $(this).html("Save");
        }
        $('#_Cancel_New_Vendor_Pop_').html("Previous");
    } else {
        InsertUpdateVendor();
        $('._Add_New_Vendor_Form_').html('');

        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
    }
});

$('.nav_tabs_for_new_vendor > li > a').click(function () {
    if ($(this).attr("href") == "#v-general-dtls") {
        $('#_Cancel_New_Vendor_Pop_').html("Cancel");
        $('#_Save_New_Vendor_Pop_').html("Next");
        setTimeout(function () { $('input[name=Full_name]').focus(); }, 1);
    }
    if ($(this).attr("href") == "#v-address-dtls") {
        $('#_Cancel_New_Vendor_Pop_').html("Previous");
        $('#_Save_New_Vendor_Pop_').html("Next");
        setTimeout(function () { $('input[name=Address]').focus(); }, 1);
    }
    if ($(this).attr("href") == "#v-payment-dtls") {
        $('#_Cancel_New_Vendor_Pop_').html("Previous");
        $('#_Save_New_Vendor_Pop_').html("Save");
        setTimeout(function () { $('input[name=Bank_account_number]').focus(); }, 1);
    }
});

$('#_Cancel_New_Vendor_Pop_').on('click', function () {
    if ($(this).html() == "Previous") {
        $('.nav_tabs_for_new_vendor > .active').prev('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_vendor > .active > a').attr("href") == "#v-general-dtls") {
            $(this).html("Cancel");
        }
        $('#_Save_New_Vendor_Pop_').html("Next");
    } else {
        $('#modal-AddNewVendor').modal('toggle');
    }
});


$("#getAddress_country").change(function () {
    var Country_id = $("select[name=Address_country] option:checked").val();
    CitiesDropdown(Country_id);
});


function InsertUpdateVendor() {

    $('#_Error_Message_Display_ > p').html("");
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if ($("input[name=Full_name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Name<br />");
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
    else if (($("input[name=Contact_phone_landline]").val().length != 13 && $("input[name=Contact_phone_landline]").val().length != 0) || $("input[name=Contact_phone_landline]").val().length < 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Phone No Format, example format 0092xx1234567<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Contact_phone_mobile]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Mobile No");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if (($("input[name=Contact_phone_mobile]").val().length != 14 && $("input[name=Contact_phone_mobile]").val().length != 0) || $("input[name=Contact_phone_mobile]").val().length < 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Mobile No Format. example format 0092xxx1234567<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Contact_email]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Contact Email");
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
    else if ($("select[name=Address_country] option:selected").val() == "0") {
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
        $('#_Add_New_Vendor_Form_Save_').attr("disabled", true);
        var url = '/Vendor/InsertUpdateVendor';
        var vendor = {
            id: $("#vendorid").val(),
            Salutation: $("select[name=Salutation] option:checked").val(),
            Name: $("input[name=Full_name]").val(),
            CompanyId: $("select[name=Contact_company] option:checked").val(),
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
            async: false,
            data: { __RequestVerificationToken: token, "Vendor": JSON.stringify(vendor) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == 1) {
                $('#modal-AddNewVendor').modal('toggle');
                DropDownVendors();
                $('#_Error_Message_Display_ > p').html('');
                $('#_Error_Message_Display_').slideUp("slow");

                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('._Add_New_Vendor_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);

                $("#vendorid").val("0");
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

function InsertUpdateCategory() {

    if ($("input[name=category_Name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Category Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        $('#_Add_New_Category_Form_Save_').attr("disabled", true);
        var url = '/Category/InsertUpdateCategory';
        var Category_data = {
            id: $("input[name=categoryid]").val(),
            Category_Name: $("input[name=category_Name]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "CategoryData": JSON.stringify(Category_data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == "1") {
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                $("input[name=category_Name]").val("");
                $('input[name=categoryid]').val("0");
                $('#_Add_New_Category_Form_Save_').html("Save");
            }
            else {
                $('#_Add_New_Category_Form_Save_').attr("disabled", false);
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }

        }).fail(function () {
            alert("post error 0");
            $('input[name=categoryid]').val("0");
        });
    }
};

function InsertUpdateBrands() {

    if ($("input[name=brand_Name]").val() == "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Brand Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        $('#_Add_New_Brand_Form_Save_').attr("disabled", true);
        var url = '/Brand/InsertUpdateBrands';
        var Brand_Data = {
            id: $("input[name=brandid]").val(),
            Brand_Name: $("input[name=brand_Name]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            async: false,
            type: "POST",
            data: { __RequestVerificationToken: token, "BrandData": JSON.stringify(Brand_Data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            $('#_Add_New_Brand_btn_').attr("disabled", false);

            if (resp.pFlag == "1") {
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                $("input[name=brand_Name]").val("");
                $('input[name=brandid]').val("0");
                $('#_Add_New_Brand_Form_Save_').html("Save");
            }
            else {
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }


        }).fail(function () {

            alert("post error 0");
            $('input[name=brandid]').val("0");
        });
    }
};

function InsertUpdateManufacturer() {

    if ($("input[name=manufacturer_Name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Manufacturer Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        var url = '/Manufacturer/InsertUpdateManufacturer';
        var ManufacturerData = {
            id: $("input[name=manufacturerid]").val(),
            Manufacturer_Name: $("input[name=manufacturer_Name]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "ManufacturerData": JSON.stringify(ManufacturerData) },
            datatype: 'json',
            async: false,
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == "1") {
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                $("input[name=manufacturer_Name]").val("");
                $('input[name=manufacturerid]').val("0");
                $('#_Add_New_Manufacturer_Form_Save_').html("Save");
            }
            else {
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }


        }).fail(function () {
            alert("post error 0");
            $('input[name=manufacturerid]').val("0");
        });
    }
};

function InsertUpdateUnit() {

    if ($("input[name=unit_Name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Unit Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        var url = '/Unit/InsertUpdateUnit';
        var Unit_Data = {
            id: $("input[name=unitid]").val(),
            Unit_Name: $("input[name=unit_Name]").val(),
        }
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "UnitData": JSON.stringify(Unit_Data) },
            datatype: 'json',
            async: false,
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
            if (resp.pFlag == "1") {
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                $("input[name=unit_Name]").val("");
                $('input[name=unitid]').val("0");
            }
            else {
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
        }).fail(function () {
            alert("post error 0");
            $('input[name=unitid]').val("0");
        });
    }
};