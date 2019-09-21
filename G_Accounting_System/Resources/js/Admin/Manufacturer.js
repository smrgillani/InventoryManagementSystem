$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/manufacturer/index/" || url == "/manufacturer/index") {
        GetAllManufacturersList(null);
    }
    if (url.includes("/manufacturer/profile/")) {
        GetAManufacturer(url);
    }
})

$("#_Add_New_Manufacturer_btn_").click(function () {
    $(this).attr("disabled", true);
    AddManufacturerForm();
});

function AddManufacturerForm() {
    $('._Add_New_Manufacturer_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Manufacturer/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        console.log(resp);
        if (resp != false) {
            $('._Add_New_Manufacturer_Form_').html(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('#_Add_New_Manufacturer_Form_Save_').html("Save");
            $('._Add_New_Manufacturer_Form_').slideDown("slow");
            $('#_Add_New_Manufacturer_btn_').attr("disabled", true);
            $("#_Add_New_Manufacturer_Form_Save_").attr("disabled", false);
            setTimeout(function () { $('input[name=manufacturer_Name]').focus(); }, 1);
        }
        else {

            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Manufacturer_Form_').slideUp("slow");
            $('._Add_New_Manufacturer_Form_').html("");
            $("#_Add_New_Manufacturer_btn_").attr("disabled", false);
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

function GetAllManufacturersList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    ManufacturerList = $('#tbl_Manufacturer').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No manufacturers available"
        },
        "sAjaxSource": "/Manufacturer/GetAllManufacturers",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=ManufacturerStartDate]').val(), EndDate: $('input[name=ManufacturerEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/manufacturer/profile/" + data.id + "'>" + data.Manufacturer_Name + "</a>";

                }
            },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditManufacturer(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepManufacturer(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditManufacturer(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepManufacturer(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkManufacturerDel" data_value_manufacturerid=' + data.id + ' data_value_manufacturername=' + data.Manufacturer_Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkManufacturerDel" data_value_manufacturerid=' + data.id + ' data_value_manufacturerdata_value_manufacturernamename=' + data.Manufacturer_Name + '>';
                    }
                    return data;
                }

            }
        ]
    });
}

var Activity_Data = new Array();
function UpdatepManufacturer(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Manufacturer/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            ManufacturerList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Manufacturer Profile Visibility Updated Successfully.');
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

function GetAManufacturer(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteManufacturer").attr("disabled", true);
        } else {
            $("#PDeleteManufacturer").attr("disabled", false);
        }
        $("input[name=manufacturers_id]").val(resp.id);
        var ActivityType_id = resp.id;
        $("#cp__manufacturer_name").html('');
        $("#cp__manufacturer_name").html(resp.Manufacturer_Name);
        $("#cp__manufacturer_status").html(resp.IsEnabled_);
        var ActivityType = "Manufacturer";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function EditManufacturer(id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Manufacturer/Update/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        if (resp != false) {
            
            AddManufacturerForm();
           
            $("#_Add_New_Manufacturer_Form_Save_").attr("disabled", false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Error_Message_Display_ > p').html('');
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Manufacturer_Form_').slideDown("slow");
            $('#_Add_New_Manufacturer_Form_Save_').html('Update');
            $('#_Add_New_Manufacturer_Form_Save_').attr('id', '_Update_Existing_Manufacturer_Form_Save_');
            $('input[name=manufacturer_Name]').val(resp.Manufacturer_Name);
            $('input[name=manufacturerid]').val(id);
            
        }
        else {
           
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Manufacturer_Form_').slideUp("slow");
            $('._Add_New_Manufacturer_Form_').html("");
            $("#_Add_New_Manufacturer_btn_").attr("disabled", false);
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

function DelManufacturer(id) {
    $("#PDeleteManufacturer").attr("disabled", true);
    checked_Manufacturers = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/manufacturer/profile/")) {
        id = $("input[name=manufacturers_id]").val();
        name = $("#cp__manufacturer_name").text();
    }
    var data = { id: id, Manufacturer_Name: name, Delete_Status: "Requested" }
    checked_Manufacturers.push(data);
    url = "/manufacturer/index";
    SendRequestToDelManufacturer(url);

}

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
})

$("input[name=ManufacturerStartDate]").change(function () {
    ManufacturerList.ajax.reload(null, false);
});

$("input[name=ManufacturerEndDate]").change(function () {
    ManufacturerList.ajax.reload(null, false);
});

var checked_Manufacturers;
function getCheckedManufacturertoDel() {
    var data_value_manufacturerid = "";
    var data_value_manufacturername = "";

    checked_Manufacturers = new Array();

    $('#_tbl_Manufacturer_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkManufacturerDel')).is(':checked')) {
            data_value_manufacturerid = $(this).find($('td')).find($('.chkManufacturerDel')).attr('data_value_manufacturerid');
            data_value_manufacturername = $(this).find($('td')).find($('.chkManufacturerDel')).attr('data_value_manufacturername');
            var data = { id: data_value_manufacturerid, Manufacturer_Name: data_value_manufacturername, Delete_Status: "Requested" }
            checked_Manufacturers.push(data);
        }
    });
    if (checked_Manufacturers.length > 0) {
        SendRequestToDelManufacturer();
    } else {
        $('#_Error_Message_Display_').html("There are No Manufacturers to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteManufacturer").click(function () {
    getCheckedManufacturertoDel();
});

function SendRequestToDelManufacturer(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Manufacturer/SendRequestToDelManufacturers/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteManufacturerData": JSON.stringify(checked_Manufacturers)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.ManufacturersNotDelete.length != 0) {
                $('#_Error_Message_Display_ > p').html('Some of The Brands cannot be Deleted as they are associated with some of the Items.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                ManufacturerList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            } else {
                $("#PDeleteManufacturer").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        ManufacturerList.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                ManufacturerList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteManufacturer').css('display', 'none');
        }
        else {
            $("#PDeleteManufacturer").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteManufacturer").attr("disabled", false);
    });
}

$(document).on("change", ".chkManufacturerDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteManufacturer').show();
    }
    else {
        $('#_Send_request_To_DeleteManufacturer').css('display', 'none');
    }
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Manufacturer_Form_Save_').click();
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
//        $('#_Add_New_Manufacturer_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$(".ListManufacturerFilter").click(function () {
    ManufacturerList.destroy();
    var filtertype = $(this).text();
    GetAllManufacturersList(filtertype);
});