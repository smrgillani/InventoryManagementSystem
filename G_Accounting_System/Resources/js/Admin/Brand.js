$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/brand/index/" || url == "/brand/index") {
        GetAllBrandsList(null);

    }
    if (url.includes("/brand/profile/")) {
        GetABrand(url);
    }


})

$("#_Add_New_Brand_btn_").click(function () {
    $(this).attr("disabled", true);
    AddBrandForm();
});

function AddBrandForm() {
    $('._Add_New_Brand_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Brand/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_Brand_Form_').html(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('#_Add_New_Brand_Form_Save_').html("Save");
            $('._Add_New_Brand_Form_').slideDown("slow");
            $('#_Add_New_Brand_btn_').attr("disabled", false);
            $('#_Add_New_Brand_Form_Save_').attr("disabled", false);
            setTimeout(function () { $('input[name=brand_Name]').focus(); }, 1);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Brand_Form_').slideUp("slow");
            $('._Add_New_Brand_Form_').html("");
            $("#_Add_New_Brand_btn_").attr("disabled", false);
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

function GetAllBrandsList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    BrandList = $('#tbl_Brand').DataTable({
        //"bpaginate": true,
        "language": {
            "emptyTable": "No brands available"
        },
        "searching": true,
        "bsorting": true,
        "bServerSide": true,
        "sAjaxSource": "/Brand/GetAllBrands",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=BrandStartDate]').val(), EndDate: $('input[name=BrandEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/brand/profile/" + data.id + "'>" + data.Brand_Name + "</a>";

                }
            },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        //return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden EditBrand' onclick='EditBrand(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepBrand(" + data.id + ")' title='Visibility'></span>";
                        return "N/A";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons EditBrand' onclick='EditBrand(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepBrand(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        //return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkBrandDel" data_value_brandid=' + data.id + ' data_value_brandname=' + data.Brand_Name + '>';
                        return "N/A";
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkBrandDel" data_value_brandid=' + data.id + ' data_value_brandname=' + data.Brand_Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

var Activity_Data = new Array();
function UpdatepBrand(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Brand/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            BrandList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Brand Profile Visibility Updated Successfully.');
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

function GetABrand(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteBrand").attr("disabled", true);
        } else {
            $("#PDeleteBrand").attr("disabled", false);
        }
        $("input[name=brands_id]").val(resp.id);
        var ActivityType_id = resp.id;
        $("#cp__brand_name").html('');
        $("#cp__brand_name").html(resp.Brand_Name);
        $("#cp__brand_status").html(resp.IsEnabled_);
        var ActivityType = "Brand";
        Activities(ActivityType_id, ActivityType);
        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}



function EditBrand(id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Brand/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        console.log(resp);
        if (resp != false) {
            AddBrandForm();
            $("#_Add_New_Brand_btn_").attr("disabled", false);
            $('#_Add_New_Brand_Form_Save_').attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Brand_Form_').slideDown("slow");
            $('#_Add_New_Brand_Form_Save_').html('Update');

            $('input[name=brand_Name]').val(resp.Brand_Name);
            $('input[name=brandid]').val(id);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Brand_Form_').slideUp("slow");
            $('._Add_New_Brand_Form_').html("");
            $("#_Add_New_Brand_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    });

}

function DelBrand(id) {
    $("#PDeleteBrand").attr("disabled", true);
    checked_Brands = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/brand/profile/")) {
        id = $("input[name=brands_id]").val();
        name = $("#cp__brand_name").text();
    }
    var data = { id: id, Brand_Name: name, Delete_Status: "Requested" }
    checked_Brands.push(data);
    url = "/brand/index";
    SendRequestToDelBrands(url);
}



$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
})



$("input[name=BrandStartDate]").change(function () {
    BrandList.ajax.reload(null, false);
});

$("input[name=BrandEndDate]").change(function () {
    BrandList.ajax.reload(null, false);
});

var checked_Brands;
function getCheckedBrandstoDel() {
    var data_value_brandid = "";
    var data_value_brandname = "";

    checked_Brands = new Array();

    $('#_tbl_Brand_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkBrandDel')).is(':checked')) {
            data_value_brandid = $(this).find($('td')).find($('.chkBrandDel')).attr('data_value_brandid');
            data_value_brandname = $(this).find($('td')).find($('.chkBrandDel')).attr('data_value_brandname');
            var data = { id: data_value_brandid, Brand_Name: data_value_brandname, Delete_Status: "Requested" }
            checked_Brands.push(data);
        }
    });
    if (checked_Brands.length > 0) {
        SendRequestToDelBrands();
    } else {
        $('#_Error_Message_Display_').html("There are No Brands to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteBrands").click(function () {
    getCheckedBrandstoDel();
});

function SendRequestToDelBrands(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Brand/SendRequestToDelBrands/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteBrandData": JSON.stringify(checked_Brands)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.BrandsNotDelete.length != 0) {
                $('#_Error_Message_Display_ > p').html('Some of The Brands cannot be deleted as they are associated with some of the Items.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                BrandList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteBrand").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        BrandList.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                BrandList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteBrands').css('display', 'none');
        }
        else {
            $("#PDeleteBrand").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }
    }).fail(function () {
        $("#PDeleteBrand").attr("disabled", false);
    })
}

$(document).on("change", ".chkBrandDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteBrands').show();
    }
    else {
        $('#_Send_request_To_DeleteBrands').css('display', 'none');
    }
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Brand_Form_Save_').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 27) {
//        // Save Function
//        $('#_Add_New_Brand_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$(".ListBrandFilter").click(function () {
    BrandList.destroy();
    var filtertype = $(this).text();
    GetAllBrandsList(filtertype);
});