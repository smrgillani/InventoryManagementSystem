$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/unit/index/" || url == "/unit/index") {
        GetAllUnitsList(null);
    }
    if (url.includes("/unit/profile/")) {
        GetAUnit(url);
    }
})

$("#_Add_New_Unit_btn_").click(function () {
    $(this).attr("disabled", true);
    AddUnitForm();
});

function AddUnitForm() {
    $('._Add_New_Unit_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Unit/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        console.log(resp);
        if (resp != false) {
            $('._Add_New_Unit_Form_').html(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('#_Add_New_Unit_Form_Save_').html("Save");
            $('._Add_New_Unit_Form_').slideDown("slow");
            $('#_Add_New_Unit_btn_').attr("disabled", true);
            $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
            setTimeout(function () { $('input[name=unit_Name]').focus(); }, 1);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Unit_Form_').slideUp("slow");
            $('._Add_New_Unit_Form_').html("");
            $("#_Add_New_Unit_btn_").attr("disabled", false);
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

function GetAllUnitsList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    UnitList = $('#tbl_Unit').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No units available"
        },
        "sAjaxSource": "/Unit/GetAllUnits",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=UnitStartDate]').val(), EndDate: $('input[name=UnitEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/unit/profile/" + data.id + "'>" + data.Unit_Name + "</a>";

                }
            },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditUnit(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepUnit(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditUnit(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepUnit(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkUnitDel" data_value_unitid=' + data.id + ' data_value_unitname=' + data.Unit_Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkUnitDel" data_value_unitid=' + data.id + ' data_value_unitname=' + data.Unit_Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

var Activity_Data = new Array();
function UpdatepUnit(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Unit/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            UnitList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Unit Profile Visibility Updated Successfully.');
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

function GetAUnit(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteUnit").attr("disabled", true);
        } else {
            $("#PDeleteUnit").attr("disabled", false);
        }
        $("input[name=units_id]").val(resp.id);
        var ActivityType_id = resp.id;
        $("#cp__unit_name").html('');
        $("#cp__unit_name").html(resp.Unit_Name);
        $("#cp__unit_status").html(resp.IsEnabled_);
        var ActivityType = "Unit";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}



function EditUnit(id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Unit/Update/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        if (resp != false) {
            AddUnitForm();
            $("#_Add_New_Unit_btn_").attr("disabled", true);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Error_Message_Display_').html("");
            $('#_Add_New_Unit_Form_Save_').html('Update');
            $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
            
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Unit_Form_').slideDown("slow");
            $('input[name=unit_Name]').val(resp.Unit_Name);
            $('input[name=unitid]').val(id);
            $('input[name=unit_Name]').focus();
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Unit_Form_').slideUp("slow");
            $('._Add_New_Unit_Form_').html("");
            $("#_Add_New_Unit_btn_").attr("disabled", false);
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

function DelUnit(id) {
    $("#PDeleteUnit").attr("disabled", true);
    checked_Units = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/unit/profile/")) {
        id = $("input[name=units_id]").val();
        name = $("#cp__unit_name").text();
    }
    var data = { id: id, Unit_Name: name, Delete_Status: "Requested" }
    checked_Units.push(data);
    url = "/unit/index";
    SendRequestToDelUnits(url);

}



$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
})



$("input[name=UnitStartDate]").change(function () {
    UnitList.ajax.reload(null, false);
});

$("input[name=UnitEndDate]").change(function () {
    UnitList.ajax.reload(null, false);
});

var checked_Units;
function getCheckedUnitstoDel() {
    var data_value_unitid = "";
    var data_value_unitname = "";

    checked_Units = new Array();

    $('#_tbl_Unit_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkUnitDel')).is(':checked')) {
            data_value_unitid = $(this).find($('td')).find($('.chkUnitDel')).attr('data_value_unitid');
            data_value_unitname = $(this).find($('td')).find($('.chkUnitDel')).attr('data_value_unitname');
            var data = { id: data_value_unitid, Unit_Name: data_value_unitname, Delete_Status: "Requested" }
            checked_Units.push(data);
        }
    });
    if (checked_Units.length > 0) {
        SendRequestToDelUnits();
    } else {
        $('#_Error_Message_Display_').html("There are No Units to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteUnits").click(function () {
    getCheckedUnitstoDel();
});

function SendRequestToDelUnits(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Unit/SendRequestToDelUnits/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteUnitData": JSON.stringify(checked_Units)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.UnitsNotDelete.length != 0) {
                $('#_Error_Message_Display_ > p').html('Some of The Units cannot be Deleted as they are associated with some of the Items.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                UnitList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteUnit").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        UnitList.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                UnitList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteUnits').css('display', 'none');
        }
        else {
            $("#PDeleteUnit").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteUnit").attr("disabled", false);
    });
}

$(document).on("change", ".chkUnitDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteUnits').show();
    }
    else {
        $('#_Send_request_To_DeleteUnits').css('display', 'none');
    }
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Unit_Form_Save_').click();
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
//        $('#_Add_New_uNIT_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$(".ListUnitFilter").click(function () {
    UnitList.destroy();
    var filtertype = $(this).text();
    GetAllUnitsList(filtertype);
});