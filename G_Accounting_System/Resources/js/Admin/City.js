$("#_Add_New_City_btn_").click(function () {
    $(this).attr("disabled", true);
    AddCityForm();
   
});

function AddCityForm() {
    $('._Add_New_City_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/City/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_City_Form_').append(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_City_Form_').slideDown("slow");
            $('input[name=Full_name]').focus();
            CountriesDropdown();

            $('#_Add_New_City_Form_Save_').attr("disabled", false);
            $('.select2').select2();
        }
        else {
            $('#_Error_Message_Display_ > span').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_City_Form_').slideUp("slow");
            $('._Add_New_City_Form_').html("");
            $("#_Add_New_City_btn_").attr("disabled", false);
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
    if (url == "/city/index/" || url == "/city/index") {
        GetAllCitiesList(null);
    } else if (url.includes("/city/profile/")) {
        GetACity(url);
    } else if (url.includes("/city?id=")) {
        GetAllCitiesList(window.location.search.toString().toLowerCase().split("?id=")[1]);
    }
})

function GetAllCitiesList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    CitiesList = $('#cities_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No cities available"
        },
        "sAjaxSource": "/City/GetAllCities/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=CityStartDate]').val(), EndDate: $('input[name=CityEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[30].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/city/profile/" + data.id + "'>" + data.Name + "</a>";
                }
            },
            { data: "CountryName" },
            { data: "IsEnabled" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditCity(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepCity(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditCity(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepCity(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkCityDel" data_value_cityid=' + data.id + ' data_value_cityname=' + data.Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkCityDel" data_value_cityid=' + data.id + ' data_value_cityname=' + data.Name + '>';
                    }
                    return data;
                }

            }
        ]
    });
}


$("input[name=CityStartDate]").change(function () {
    CitiesList.ajax.reload(null, false);
});

$("input[name=CityEndDate]").change(function () {
    CitiesList.ajax.reload(null, false);
});

function GetACity(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteCity").attr("disabled", true);
        } else {
            $("#PDeleteCity").attr("disabled", false);
        }
        $("#cp__city_name").html('');
        $("#cp__city_name").html(resp.Name);
        $("#cp__city_status").html('');
        $("#cp__city_status").html(resp.IsEnabled_);
        $("#cp__city_country").html('');
        $("#cp__city_country").html(resp.Country_);


        $("input[name=cities_id]").val(resp.id);
        $("input[name=Full_name]").val(resp.Name);
        $("input[name=Address_country]").val(resp.Country);
        $("select[name=C_Status]").val(resp.IsEnabled).trigger('change');

        var ActivityType_id = resp.id;
        var ActivityType = "City";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > span').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}


function DelCity(id, el) {
    $("#PDeleteCity").attr("disabled", true);
    checked_Cities = new Array();
    var id = $("input[name=cities_id]").val();
    var name = $("#cp__city_name").text();

    var data = { id: id, Name: name, Delete_Status: "Requested" }
    checked_Cities.push(data);
    url = "/city/index";
    SendRequestToDelCities(url);
}

var Activity_Data = new Array();
function UpdatepCity(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/City/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > span').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            CitiesList.ajax.reload(null, false);
            $('#_Success_Message_Display_ > span').html('City Profile Visibility Updated Successfully.');
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

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > span').html('');
});

function EditCity(id) {
    
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        async: false,
        url: '/City/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            AddCityForm();
            //$('._Add_New_City_Form_').html('');
            //$('._Add_New_City_Form_').html(resp);

            $("#_Add_New_Brand_btn_").attr("disabled", false);
            $('#_Error_Message_Display_ > span').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('#_Add_New_City_Form_Save_').html('Update');
            $('#_Add_New_City_Form_Save_').attr('id', '_Update_Existing_City_Form_Save_');
            $('input[name=cities_id]').val(resp.City.id);
            $('input[name=Full_name]').val(resp.City.Name);
            $('select[name=C_Status]').val(resp.City.IsEnabled).change();
            $('input[name=Full_name]').focus();
            $("select[name=Address_country]").val(resp.City.Country).change();
            $('.select2').select2();
        }
        else {
            $('#_Error_Message_Display_ > span').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_City_Form_').slideUp("slow");
            $('._Add_New_City_Form_').html("");
            $("#_Add_New_City_btn_").attr("disabled", false);
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

$('#Update_Existing_City').on('click', function () {
    UpdateCity();
});

function UpdateCity() {
    var url = '/City/UpdateCity';
    var city = {
        id: $("input[name=cities_id]").val(),
        Name: $("input[name=Full_name]").val(),
        Country: $("input[name=Address_country]").val(),
        IsEnabled: $("select[name=C_Status] option:checked").val()
    }
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "City": JSON.stringify(city) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.length == 0) {

            $('#_Error_Message_Display_ > span').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            GetACity(window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase());
            $('#_Success_Message_Display_ > span').html('City Profile Updated Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
        } else {
            $('#_Error_Message_Display_ > span').html(resp);
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
};

var checked_Cities;
function getCheckedCitiestoDel() {
    var data_value_cityid = "";
    var data_value_cityname = "";

    checked_Cities = new Array();

    $('#_tbl_City_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkCityDel')).is(':checked')) {
            data_value_cityid = $(this).find($('td')).find($('.chkCityDel')).attr('data_value_cityid');
            data_value_cityname = $(this).find($('td')).find($('.chkCityDel')).attr('data_value_cityname');
            var data = { id: data_value_cityid, Name: data_value_cityname, Delete_Status: "Requested" }
            checked_Cities.push(data);
        }
    });
    if (checked_Cities.length > 0) {
        SendRequestToDelCities();
    } else {
        $('#_Error_Message_Display_').html("There are No Cities to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteCities").click(function () {
    getCheckedCitiestoDel();
});

function SendRequestToDelCities(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/City/SendRequestToDelCities/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteCityData": JSON.stringify(checked_Cities)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.CitiesNotDelete.length != 0) {
                $('#_Error_Message_Display_ > span').html('Some of The Brands cannot be deleted as they are associated with some of the Items.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                CitiesList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteCity").attr("disabled", true);
                $('#_Success_Message_Display_ > span').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        CitiesList.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                CitiesList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteCities').css('display', 'none');
        }
        else {
            $("#PDeleteCity").attr("disabled", false);
            $('#_Error_Message_Display_ > span').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteCity").attr("disabled", false);
    });
}

$(document).on("change", ".chkCityDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteCities').show();
    }
    else {
        $('#_Send_request_To_DeleteCities').css('display', 'none');
    }
});

$(".ListCityFilter").click(function () {
    CitiesList.destroy();
    var filtertype = $(this).text();
    GetAllCitiesList(filtertype);
});