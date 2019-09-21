$('.select2').select2();
CountriesDropdown();
DropDownCompanies();
$('#_Add_New_Item_Form_Save_').on('click', function () {

    if ($(this).html() == "Next") {
        $('.nav-tabs > .active').next('li').find('a').trigger('click');
        if ($('.nav-tabs > .active > a').attr("href") == "#i-openingstock-dtls") {
            if ($('input[name=itemid]').val() == "0") {
                $(this).html("Save");
            }
            else {
                $(this).html("Update");
            }
        }
        $('#_Add_New_Item_Form_Remover__').html("Previous");
    } else {
        AddItem();
    }

});

$('.nav_tabs_for_new_item > li > a').click(function () {
    if ($(this).attr("href") == "#i-general-dtls") {
        $('#_Add_New_Item_Form_Remover__').html("Cancel");
        $('#_Add_New_Item_Form_Save_').html("Next");
        setTimeout(function () { $('input[name=item_Name]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#i-sales-dtls") {
        $('#_Add_New_Item_Form_Remover__').html("Previous");
        $('#_Add_New_Item_Form_Save_').html("Next");
        setTimeout(function () { $('input[name=item_Sell_Price]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#i-purchase-dtls") {
        if (!$('.nav-tabs-custom a[href="#i-openingstock-dtls"]').tab().is(":visible")) {
            $('#_Add_New_Item_Form_Save_').html("Update");
        } else {
            $('#_Add_New_Item_Form_Save_').html("Next");
        }
        $('#_Add_New_Item_Form_Remover__').html("Previous");
        setTimeout(function () { $('input[name=item_Purchase_Price]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#i-openingstock-dtls") {
        $('#_Add_New_Item_Form_Remover__').html("Previous");
        if ($('input[name=itemid]').val() == "0") {
            $('#_Add_New_Item_Form_Save_').html("Save");
        }
        else {
            $('#_Add_New_Item_Form_Save_').html("Update");
        }
        setTimeout(function () { $('input[name=item_Opening_Stock]').focus(); }, 1);
    }
});

function ItemFormRemover() {
    $('._Add_New_Item_Form_').slideUp("slow");
    $("#_Add_New_Item_btn_").attr("disabled", false);
    $("select[name=item_type]").val("Good").trigger('change');
    $("input[name=item_file]").val("");
    $("input[name=item_Name]").val("");
    $("input[name=item_Sku]").val("");
    $("select[name=item_Category]").val("None").trigger('change');
    $("select[name=item_Unit]").val("None").trigger('change'),
    $("select[name=item_Manufacturer]").val("None").trigger('change');
    $("input[name=item_Upc]").val("");
    $("select[name=item_Brand]").val("None").trigger('change');
    $("input[name=item_Mpn]").val("");
    $("input[name=item_Ean]").val("");
    $("input[name=item_Isbn]").val("");
    $("input[name=item_Sell_Price]").val("");
    $("input[name=item_Tax]").val("");
    $("input[name=item_Purchase_Price]").val("");
    $("select[name=item_Preferred_Vendor]").val("None").trigger('change');
    $("input[name=item_Opening_Stock]").val("");
    $("input[name=item_Reorder_Level]").val("");

}

$('#_Add_New_Item_Form_Remover__').on('click', function () {
    if ($(this).html() == "Previous") {
        $('.nav-tabs > .active').prev('li').find('a').trigger('click');
        if ($('.nav-tabs > .active > a').attr("href") == "#i-general-dtls") {
            $(this).html("Cancel");
        }
        $('#_Add_New_Item_Form_Save_').html("Next");
    } else {
        ItemFormRemover();
    }
});

function handleFileSelect() {
    $("#base64").val("");

};

$("#item_file").change(function () {
    $("#base64").val("");
    var files = $("#item_file").get(0).files;
    var file = files[0];

    if (files && file) {
        var reader = new FileReader();

        reader.onload = function (readerEvt) {
            var binaryString = readerEvt.target.result;
            $("#base64").val(btoa(binaryString));
        };

        reader.readAsBinaryString(file);
    }
});

function AddItem() {

    $('#_Error_Message_Display_ > p').html("");
    if ($("input[name=item_Name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Item Name<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=item_Category] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Category<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=item_Sku]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter SKU<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else if ($("input[name=item_Sku]").length != 0 && $("input[name=item_Sku]").val() < 0) {
        $('#_Error_Message_Display_ > p').html("Invalid SKU<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=item_Unit] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Unit<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=item_Manufacturer] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Manufacturer<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=item_Brand] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Brand<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=item_Upc]").val() === "0") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter UPC<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=item_Upc]").length != 0 && $("input[name=item_Upc]").val() < 0) {
        $('#_Error_Message_Display_ > p').html("Invalid UPC<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=item_Mpn]").val() === "0") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter MPN<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=item_Mpn]").length != 0 && $("input[name=item_Mpn]").val() < 0) {
        $('#_Error_Message_Display_ > p').html("Invalid MPN<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=item_Ean]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter EAN<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=item_Ean]").length != 0 && $("input[name=item_Ean]").val() < 0) {
        $('#_Error_Message_Display_ > p').html("Invalid EAN<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=item_Isbn]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter ISBN<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=item_Isbn]").length != 0 && $("input[name=item_Isbn]").val() < 0) {
        $('#_Error_Message_Display_ > p').html("Invalid ISBN<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=item_Sell_Price]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Sell Price<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=item_Tax]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Tax<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=item_Purchase_Price]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Purchase Price<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=item_Preferred_Vendor] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Preferred Vendor<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        $("#_Add_New_Item_Form_Save_").attr("disabled", true);
        var url = '/Item/InsertUpdateItem';
        var Item_data = {
            id: $("input[name=itemid]").val(),
            Item_type: $("select[name=item_type] option:checked").val(),
            base64: $("#base64").val(),
            Item_Name: $("input[name=item_Name]").val(),
            Item_Sku: $("input[name=item_Sku]").val(),
            Item_Category: $("select[name=item_Category] option:checked").val(),
            Item_Unit: $("select[name=item_Unit] option:checked").val(),
            Item_Manufacturer: $("select[name=item_Manufacturer] option:checked").val(),
            Item_Upc: $("input[name=item_Upc]").val(),
            Item_Brand: $("select[name=item_Brand] option:checked").val(),
            Item_Mpn: $("input[name=item_Mpn]").val(),
            item_Ean: $("input[name=item_Ean]").val(),
            Item_Isbn: $("input[name=item_Isbn]").val(),
            Item_Sell_Price: $("input[name=item_Sell_Price]").val(),
            Item_Tax: $("input[name=item_Tax]").val(),
            Item_Purchase_Price: $("input[name=item_Purchase_Price]").val(),
            Item_Preferred_Vendor: $("select[name=item_Preferred_Vendor] option:checked").val(),
            OpeningStock: $("input[name=item_Opening_Stock]").val(),
            ReorderLevel: $("input[name=item_Reorder_Level]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "ItemData": JSON.stringify(Item_data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            $("#_Add_New_Item_Form_Save_").attr("disabled", false);
            $("#_Add_New_Item_btn_").attr("disabled", false);
            $("#base64").val("");

            if (resp.pFlag == "1") {
                $('._Add_New_Item_Form_').slideUp("slow");
                var Item_id = resp.pItem_id_Out;

                $("input[name=itemid]").val("0");

                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
            }
            else {
                $("#_Add_New_Item_Form_Save_").attr("disabled", false);
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }


            var token = $('[name=__RequestVerificationToken]').val();


            ItemFormRemover();
            ItemsList.ajax.reload(null, false);
        }).fail(function () {
            $("#_Add_New_Item_Form_Save_").attr("disabled", false);
            alert("post error 0");
        });
    }
};