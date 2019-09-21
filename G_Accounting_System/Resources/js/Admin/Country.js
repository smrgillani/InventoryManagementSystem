
$("#_Add_New_Country_btn_").click(function () {
    $(this).attr("disabled", true);
    AddCountryForm();
});

function AddCountryForm() {
    $('._Add_New_Country_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Country/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_Country_Form_').append(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Country_Form_').slideDown("slow");
            $('input[name=Full_name]').focus();
            $('.select2').select2();
            $('#_Add_New_Country_btn_').attr("disabled", true);
            $('#_Add_New_Country_Form_Save_').attr("disabled", false);
        }
        else {
            $('#_Error_Message_Display_ > span').html('You are Not Authorized to perform this action');
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

$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/country/index/" || url == "/country/index") {
        GetAllCountriesList(null);
    } else if (url.includes("/country/profile/")) {
        GetACountry(url);
    } else if (url.includes("/country?id=")) {
        GetAllCountriesList(window.location.search.toString().toLowerCase());
    }
})

function GetAllCountriesList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    CountryList = $('#countries_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No countries available"
        },
        "sAjaxSource": "/Country/GetAllCountries/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=CountryStartDate]').val(), EndDate: $('input[name=CountryEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/country/profile/" + data.id + "'>" + data.Name + "</a>";

                }
            },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditCountry(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepCountry(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditCountry(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepCountry(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkCountryDel" data_value_countryid=' + data.id + ' data_value_countryname=' + data.Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkCountryDel" data_value_countryid=' + data.id + ' data_value_countryname=' + data.Name + '>';
                    }
                    return data;
                }

            },
        ]
    });

}

var Activity_Data = new Array();
function UpdatepCountry(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Country/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > span').html('');
            CountryList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > span').html('Country Profile Visibility Updated Successfully.');
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

function GetACountry(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteCountry").attr("disabled", true);
        } else {
            $("#PDeleteCountry").attr("disabled", false);
        }
        $("#cp__country_name").html('');
        $("#cp__country_name").html(resp.Name);
        $("#cp__country_status").html('');
        $("#cp__country_status").html(resp.IsEnabled_);

        $("input[name=countries_id]").val(resp.id);
        $("input[name=Full_name]").val(resp.Name);
        $("select[name=C_Status]").val(resp.IsEnabled).trigger('change');

        var ActivityType_id = resp.id;
        var ActivityType = "Country";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > span').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}


function DelCountry(id) {
    $("#PDeleteCountry").attr("disabled", true);
    checked_Countries = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/country/profile/")) {
        id = $("input[name=countries_id]").val();
        name = $("#cp__country_name").text();
    }
    var data = { id: id, Name: name, Delete_Status: "Requested" }
    checked_Countries.push(data);
    url = "/country/index";
    SendRequestToDelCountries(url);
}

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > span').html('');
});

function EditCountry(id) {
    
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        async: false,
        url: '/Country/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            AddCountryForm();
            $('._Add_New_Country_Form_').html('');
            $('._Add_New_Country_Form_').html(resp);
            $("#_Add_New_Country_btn_").attr("disabled", false);
            $('#_Error_Message_Display_ > span').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Country_Form_').slideDown("slow");
            $('#_Add_New_Country_Form_Save_').html('Update');
            $('#_Add_New_Country_Form_Save_').attr('id', '_Update_Existing_Country_Form_Save_');
            $('input[name=Full_name]').focus();
        }
        else {
            $('#_Error_Message_Display_ > span').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Country_Form_').slideUp("slow");
            $('._Add_New_Country_Form_').html("");
            $("#_Add_New_Country_btn_").attr("disabled", false);
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

$('#Update_Existing_Country').on('click', function () {
    UpdateCountry();
});

function UpdateCountry() {
    var url = '/Country/UpdateCountry';
    var country = {
        id: $("input[name=countries_id]").val(),
        Name: $("input[name=Full_name]").val(),
        IsEnabled: $("select[name=C_Status] option:checked").val()
    }
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "Country": JSON.stringify(country) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.length == 0) {

            $('#_Error_Message_Display_ > span').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            GetACountry(window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase());
            $('#_Success_Message_Display_ > span').html('Country Profile Updated Successfully.');
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

$("input[name=CountryStartDate]").change(function () {
    CountryList.ajax.reload(null, false);
});

$("input[name=CountryEndDate]").change(function () {
    CountryList.ajax.reload(null, false);
});

var checked_Countries;
function getCheckedCountriestoDel() {
    var data_value_countryid = "";
    var data_value_countryname = "";

    checked_Countries = new Array();

    $('#_tbl_Country_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkCountryDel')).is(':checked')) {
            data_value_countryid = $(this).find($('td')).find($('.chkCountryDel')).attr('data_value_countryid');
            data_value_countryname = $(this).find($('td')).find($('.chkCountryDel')).attr('data_value_countryname');
            var data = { id: data_value_countryid, Name: data_value_countryname, Delete_Status: "Requested" }
            checked_Countries.push(data);
        }
    });
    if (checked_Countries.length > 0) {
        SendRequestToDelCountries();
    } else {
        $('#_Error_Message_Display_').html("There are No Countries to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteCountries").click(function () {
    getCheckedCountriestoDel();
});

function SendRequestToDelCountries(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Country/SendRequestToDelCountries/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteCountryData": JSON.stringify(checked_Countries)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.CountriesNotDelete.length != 0) {
                $('#_Error_Message_Display_ > span').html('Some of The Countries cannot be deleted as they are being used.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                CountryList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteCountry").attr("disabled", true);
                $('#_Success_Message_Display_ > span').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        CountryList.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                CountryList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteCountries').css('display', 'none');
        }
        else {
            $("#PDeleteCountry").attr("disabled", false);
            $('#_Error_Message_Display_ > span').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteCountry").attr("disabled", false);
    });
}

$(document).on("change", ".chkCountryDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteCountries').show();
    }
    else {
        $('#_Send_request_To_DeleteCountries').css('display', 'none');
    }
});

$(".ListCountryFilter").click(function () {
    CountryList.destroy();
    var filtertype = $(this).text();
    GetAllCountriesList(filtertype);
});