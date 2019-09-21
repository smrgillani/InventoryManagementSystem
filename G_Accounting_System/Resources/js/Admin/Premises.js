
var Activity_Data = new Array();
$("#_Add_New_Premises_btn_").click(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/office/")) {
        AddPremisesForm("/Office/");

    } else if (url.includes("/factory/")) {
        AddPremisesForm("/Factory/");

    } else if (url.includes("/store/")) {
        AddPremisesForm("/Store/");

    } else if (url.includes("/shop/")) {
        AddPremisesForm("/Shop/");
    }
   

});

function AddPremisesForm(url) {
    $('._Add_New_Premises_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: url + 'Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_Premises_Form_').html(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Premises_Form_').slideDown("slow");
            $('#_Add_New_Premises_btn_').attr("disabled", true);
            $("#_Add_New_Premises_Form_Save_").attr('disabled', false);
            $("input[name=premises_Name]").focus();
            CountriesDropdown();
            $('.select2').select2();
        }
        else {
            $('#_Error_Message_Display_ > span').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Premises_Form_').slideUp("slow");
            $('._Add_New_Premises_Form_').html("");
            $("#_Add_New_Premises_btn_").attr("disabled", false);
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

$("#_Add_New_Premises_Form_Remover_").click(function () {
    PremisesFormRemover();
});

$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();

    var ActivityType = "";
    if (url.includes("/office/index")) {
        GetAllOfPremises("/Office/", null);

    } else if (url.includes("/factory/index")) {
        GetAllOfPremises("/Factory/", null);

    } else if (url.includes("/store/index")) {
        GetAllOfPremises("/Store/", null);

    } else if (url.includes("/shop/index")) {
        GetAllOfPremises("/Shop/", null);

    } else if (url.includes("/office/profile/")) {
        ActivityType = "Office";
        GetAPremises(url, ActivityType);


    } else if (url.includes("factory/profile/")) {
        ActivityType = "Factory";
        GetAPremises(url, ActivityType);

    } else if (url.includes("/store/profile/")) {
        ActivityType = "Store";
        GetAPremises(url, ActivityType);

    } else if (url.includes("/shop/profile/")) {
        ActivityType = "Shop";
        GetAPremises(url, ActivityType);
    }
})

var PremisesList = null;
function GetAllOfPremises(curl, parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    PremisesList = $('#Premises_list').DataTable({
        "bpaginate": true,
        "language": {
            "emptyTable": "No Prmeises available"
        },
        "searching": true,
        "bServerSide": true,
        "sAjaxSource": curl + "GetAllPremises",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=StartDate]').val(), EndDate: $('input[name=EndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[45].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='" + curl + "profile/" + data.id + "'>" + data.Name + "</a>";

                }
            },
            { data: "pc_mac_Address" },
            { data: "Phone" },
            { data: "CityName" },
            { data: "CountryName" },
            { data: "_Enable" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditPremises(" + data.id + ", " + curl + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepPremises(" + data.id + ", " + curl + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditPremises(" + data.id + ", " + curl + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepPremises(" + data.id + ", " + curl + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkPremisesDel" data_value_premisestype=' + curl + ' data_value_premisesid=' + data.id + ' data_value_premisesname=' + data.Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkPremisesDel" data_value_premisestype=' + curl + ' data_value_premisesid=' + data.id + ' data_value_premisesname=' + data.Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

function UpdatepPremises(id, curl) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: curl + 'Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > span').html('');
            PremisesList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > span').html('Premises Profile Visibility Updated Successfully.');
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

$("input[name=StartDate]").change(function () {
    PremisesList.ajax.reload(null, false);
});

$("input[name=EndDate]").change(function () {
    PremisesList.ajax.reload(null, false);
});

function EditPremises(id, curl) {
    CountriesDropdown();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: curl + "Update",
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        console.log(resp);
        if (resp != false) {
            AddPremisesForm(curl);
            $("#_Add_New_Premises_Form_Save_").attr('disabled', false);
            $('input[name=premises_Name]').val(resp.Name);
            $('input[name=premises_pcma]').val(resp.pc_mac_Address);
            $('input[name=premises_phone]').val(resp.Phone);
            $('input[name=premises_address]').val(resp.Address);

            $('select[name=premises_country]').val(resp.Country).change();
            $('select[name=premises_city]').val(resp.City).change();
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Premises_Form_').slideDown("slow");
            $('#_Add_New_Premises_Form_Save_').html('Update');
            $('#_Add_New_Premises_Form_Save_').attr('id', '_Update_Existing_Category_Form_Save_');

            $('input[name=premisesid]').val(id);
        }
        else {
            $('#_Error_Message_Display_ > span').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Premises_Form_').slideUp("slow");
            $('._Add_New_Premises_Form_').html("");
            $("#_Add_New_Premises_btn_").attr("disabled", false);
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

function DelPremises(id) {
    $(".PDeletePremises").attr("disabled", true);
    checked_Premises = new Array();
    var id;
    var name;
    var premisestype = "";
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/office/profile/")) {
        id = $("input[name=premises_id]").val();
        name = $("#ep__premises_name").text();
        premisestype = "/office/";
    }
    if (url.includes("/factory/profile/")) {
        id = $("input[name=premises_id]").val();
        name = $("#ep__premises_name").text();
        premisestype = "/factory/";
    }
    if (url.includes("/store/profile/")) {
        id = $("input[name=premises_id]").val();
        name = $("#ep__premises_name").text();
        premisestype = "/store/";
    }
    if (url.includes("/shop/profile/")) {
        id = $("input[name=premises_id]").val();
        name = $("#ep__premises_name").text();
        premisestype = "/shop/";
    }
    var data = { id: id, Name: name, Delete_Status: "Requested" }
    checked_Premises.push(data);
    url = premisestype + "Index/";
    SendRequestToDelPremises(premisestype, url);

}

function GetAPremises(url, ActivityType) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        console.log(resp);  
        if (resp.premises.Delete_Status == "Requested") {
            $(".PDeletePremises").attr("disabled", true);
        } else {
            $(".PDeletePremises").attr("disabled", false);
        }
        $("#ep__premises_name").html('');
        $("#ep__premises_mac").html('');
        $("#ep__premises_phone").html('');
        $("#ep__premises_address_phone").html('');
        $("#ep__premises_address_city").html('');
        $("#ep__premises_address_country").html('');
        $("#cp__premises_status").html('');

        $("input[name=premises_id]").val(resp.premises.id);
        $("#ep__premises_name").html(resp.premises.Name);
        $("#cp__premises_status").html(resp.premises._Enable);
        $("#ep__premises_mac").html(resp.premises.pc_mac_Address);
        $("#ep__premises_address_phone").html(resp.premises.Phone);
        $("#ep__premises_address").html(resp.premises.Address);
        $("#ep__premises_address_city").html(resp.premises.CityName);
        $("#ep__premises_address_country").html(resp.premises.CountryName);
        var ActivityType_id = resp.premises.id;
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > span').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

var checked_Premises;
function getCheckedPremisestoDel() {
    var data_value_premisesid = "";
    var data_value_premisesname = "";
    var premisestype = "";

    checked_Premises = new Array();

    $('#_tbl_Premises_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkPremisesDel')).is(':checked')) {
            data_value_premisesid = $(this).find($('td')).find($('.chkPremisesDel')).attr('data_value_premisesid');
            data_value_premisesname = $(this).find($('td')).find($('.chkPremisesDel')).attr('data_value_premisesname');
            premisestype = $(this).find($('td')).find($('.chkPremisesDel')).attr('data_value_premisestype');
            var data = { id: data_value_premisesid, Name: data_value_premisesname, Delete_Status: "Requested" }
            checked_Premises.push(data);
        }
    });
    if (checked_Premises.length > 0) {
        SendRequestToDelPremises(premisestype);
    } else {
        $('#_Error_Message_Display_').html("There are No Premises to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeletePremises").click(function () {
    getCheckedPremisestoDel();
});

function SendRequestToDelPremises(premisestype, url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: premisestype + "SendRequestToDelPremises",
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeletePremisesData": JSON.stringify(checked_Premises)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.PremisesNotDelete.length != 0) {
                $('#_Error_Message_Display_ > span').html('Some of The ' + resp.type + ' cannot be Deleted as they are being used');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                setTimeout(function () { $('#_Success_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $('#_Success_Message_Display_ > span').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        PremisesList.ajax.reload(null, false);
                    }

                }, 5000);
            }
            if (url == null) {
                PremisesList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeletePremises').css('display', 'none');

        }
        else {
            $(".PDeletePremises").attr("disabled", true);
            $('#_Error_Message_Display_ > span').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $(".PDeletePremises").attr("disabled", true);
    });
}

$(document).on("change", ".chkPremisesDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeletePremises').show();
    }
    else {
        $('#_Send_request_To_DeletePremises').css('display', 'none');
    }
});

$(".ListPremisesFilter").click(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    PremisesList.destroy();
    var filtertype = $(this).text();
    console.log(filtertype);
    if (url.includes("/office/index")) {
        GetAllOfPremises("/Office/", filtertype);
    } else if (url.includes("/factory/index")) {
        GetAllOfPremises("/Factory/", filtertype);
    } else if (url.includes("/store/index")) {
        GetAllOfPremises("/Store/", filtertype);
    } else if (url.includes("/shop/index")) {
        GetAllOfPremises("/Shop/", filtertype);
    }
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Premises_Form_Save_').click();
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
//        $('#_Add_New_Premises_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

