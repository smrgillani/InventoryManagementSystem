$("#_Add_New_Company_btn_").click(function () {

    $(this).attr("disabled", true);
    AddFormCompany();

});

function AddFormCompany() {
    $('._Add_New_Company_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Company/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_Company_Form_').append(resp);
            $('.nav-tabs a[href="#co-general-dtls"]').tab('show');
            $('._Add_New_Company_Form_').slideDown("slow");
            $('input[name=Full_name]').focus();
            CountriesDropdown();
            $('.select2').select2();
            $('#_Add_New_Company_Form_Save_').attr("disabled", false);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Company_Form_').slideUp("slow");
            $('._Add_New_Company_Form_').html("");
            $("#_Add_New_Company_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/company/index/" || url == "/company/index") {
        GetAllCompaniesList(null);
    } else if (url.includes("/company/profile/")) {
        GetACompany(url);
    } else if (url.includes("/company?id=")) {
        GetAllCompaniesList(window.location.search.toString().toLowerCase());
    }

})

function GetAllCompaniesList(parameter) {
    var token = $('[name=__RequestVerificationToken]').val();

    Companies_list = $('#companies_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No companies available"
        },
        "sAjaxSource": "/Company/GetAllCompanies",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=CompanyStartDate]').val(), EndDate: $('input[name=CompanyEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[50].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/company/profile/" + data.id + "'>" + data.Name + "</a>";

                }
            },
            { data: "Landline" },
            { data: "Email" },
            { data: "BankAccountNumber" },
            { data: "PaymentMethod" },
            { data: "CityName" },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden EditCompany' onclick='EditCompany(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepCompany(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons EditCompany' onclick='EditCompany(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepCompany(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkCompanyDel" data_value_companyid=' + data.id + ' data_value_companyname=' + data.Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkCompanyDel" data_value_companyid=' + data.id + ' data_value_companyname=' + data.Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

var Activity_Data = new Array();
function UpdatepCompany(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Company/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            Companies_list.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Company Profile Visibility Updated Successfully.');
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

function GetAllCompaniesListForContact() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Company/GetAllCompanies',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");
        var item = '';
        $.each(resp, function (i, item) {
            //var rows = "<option value='" + item.id + "'>" + item.Full_name + "</option>";
            //$("select[name=item_Preferred_Vendor]").append(rows);
        });
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function GetACompany(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteCompany").attr("disabled", true);
        } else {
            $("#PDeleteCompany").attr("disabled", false);
        }
        $("#cp__company_name").html('');
        $("#cp__company_name").html(resp.Name);
        $("#cp__company_status").html('');
        $("#cp__company_status").html(resp.IsEnabled_);
        $("#cp__company_mobile").html('');
        $("#cp__company_mobile").html(resp.Mobile);
        $("#cp__company_phone").html('');
        $("#cp__company_phone").html(resp.Landline);
        $("#cp__company_email").html('');
        $("#cp__company_email").html(resp.Email);
        $("#cp__company_website").html('');
        $("#cp__company_website").html(resp.Website);
        $("#cp__company_address").html('');
        $("#cp__company_address").html(resp.Address);
        $("#cp__company_address_city").html('');
        $("#cp__company_address_city").html(resp.CityName);
        $("#cp__company_address_country").html('');
        $("#cp__company_address_country").html(resp.CountryName);
        $("#cp__company_bank_account_number").html('');
        $("#cp__company_bank_account_number").html(resp.BankAccountNumber);
        $("#cp__company_payment_method").html('');
        $("#cp__company_payment_method").html(resp.Payment_method_);

        $("input[name=companies_id]").val(resp.id);
        $("input[name=Full_name]").val(resp.Name);
        $("input[name=Contact_phone_landline]").val(resp.Landline);
        $("input[name=Contact_phone_mobile]").val(resp.Mobile);
        $("input[name=Contact_email]").val(resp.Email);
        $("input[name=Website]").val(resp.Website);
        $("input[name=Address]").val(resp.Address);
        $("input[name=Address_city]").val(resp.City);
        $("input[name=Address_country]").val(resp.Country);
        $("input[name=Bank_account_number]").val(resp.BankAccountNumber);
        $("select[name=Payment_method]").val(resp.PaymentMethod).trigger('change');
        $("select[name=C_Status]").val(resp.IsEnabled).trigger('change');

        var ActivityType_id = resp.id;
        var ActivityType = "Company";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function DelCompany(el) {
    $("#PDeleteCompany").attr("disabled", true);
    checked_Comapany = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/company/profile/")) {
        id = $("input[name=companies_id]").val();
        name = $("#cp__company_name").text();
    }
    var data = { id: id, Name: name, Delete_Status: "Requested" }
    checked_Comapany.push(data);
    url = "/company/index";
    SendRequestToDelCompanies(url);
}

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
});

function EditCompany(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Company/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            AddFormCompany();
            $('input[name=companies_id]').val(id);
            $('input[name=Full_name]').val(resp.Name);
            $('input[name=Contact_phone_landline]').val(resp.Landline);
            $('input[name=Contact_email]').val(resp.Email);
            $('input[name=Contact_phone_mobile]').val(resp.Mobile);
            $('input[name=Website]').val(resp.Website);
            $('input[name=Address]').val(resp.Address);
            $('input[name=Bank_account_number]').val(resp.BankAccountNumber);
            $('select[name=Payment_method]').val(resp.PaymentMethod);
            $('select[name=C_Status]').val(resp.IsEnabled).change();
            $('#_Add_New_Company_Form_Save_').attr("disabled", false);
            setTimeout(function () {
                $('select[name=get_Address_country]').val(resp.Country).change();
            }, 200);
            setTimeout(function () {
                $('select[name=get_Address_city]').val(resp.City).change();
            }, 400);
            $('input[name=Full_name]').focus();
            CountriesDropdown();
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Company_Form_').slideUp("slow");
            $('._Add_New_Company_Form_').html("");
            $("#_Add_New_Company_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$('#Update_Existing_Company').on('click', function () {
    UpdateCompany();
});

function UpdateCompany() {
    var url = '/Company/UpdateCompany';
    var company = {
        id: $("input[name=companies_id]").val(),
        Name: $("input[name=Full_name]").val(),
        Landline: $("input[name=Contact_phone_landline]").val(),
        Mobile: $("input[name=Contact_phone_mobile]").val(),
        Email: $("input[name=Contact_email]").val(),
        Website: $("input[name=Website]").val(),
        Address: $("input[name=Address]").val(),
        City: $("input[name=Address_city]").val(),
        Country: $("input[name=Address_country]").val(),
        BankAccountNumber: $("input[name=Bank_account_number]").val(),
        PaymentMethod: $("select[name=Payment_method] option:checked").val(),
        IsEnabled: $("select[name=C_Status] option:checked").val()
    }
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "Company": JSON.stringify(company) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.length == 0) {

            $('#_Error_Message_Display_ > p').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            GetACompany(window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase());
            $('#_Success_Message_Display_ > p').html('Company Profile Updated Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
        } else {
            $('#_Error_Message_Display_ > p').html(resp);
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
};

$("input[name=CompanyStartDate]").change(function () {
    Companies_list.ajax.reload(null, false);
});

$("input[name=CompanyEndDate]").change(function () {
    Companies_list.ajax.reload(null, false);
});

$('.nav_tabs_for_new_company > li > a').click(function () {

    if ($(this).attr("href") == "#co-general-dtls") {
        $('._Add_New_Company_Form_Remover__').html("Cancel");
        $('#_Add_New_Company_Form_Save_').html("Next");
        $('#_Update_Existing_Company_Form_Save_').html("Next");
        setTimeout(function () { $('input[name=Full_name]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#co-address-dtls") {
        $('._Add_New_Company_Form_Remover__').html("Previous");
        $('#_Add_New_Company_Form_Save_').html("Next");
        $('#_Update_Existing_Company_Form_Save_').html("Next");
        setTimeout(function () { $('input[name=Address]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#co-payment-dtls") {
        $('._Add_New_Company_Form_Remover__').html("Previous");
        $('#_Add_New_Company_Form_Save_').html("Save");
        $('#_Update_Existing_Company_Form_Save_').html("Update");
        setTimeout(function () { $('input[name=Bank_account_number]').focus(); }, 1);
    }

});

$("._Add_New_Company_Form_Remover_").click(function () {
    CompanyFormRemover();
});

$('#_Add_New_Company_Form_Save_').on('click', function () {
    if ($(this).html() == "Next") {
        $('.nav-tabs > .active').next('li').find('a').trigger('click');
        if ($('.nav-tabs > .active > a').attr("href") == "#co-payment-dtls") {
            if ($(this).attr('id') == '_Update_Existing_Company_Form_Save_') {
                $(this).html("Update");
            } else {
                $(this).html("Save");
            }
        }
        $('._Add_New_Company_Form_Remover__').html("Previous");
    } else {
        if ($(this).attr('id') == '_Update_Existing_Company_Form_Save_') {
            UpdateCompany();
        } else {
            AddCompany();
        }
    }
});

$('._Add_New_Company_Form_Remover__').on('click', function () {
    if ($(this).html() == "Previous") {
        $('.nav-tabs > .active').prev('li').find('a').trigger('click');
        if ($('.nav-tabs > .active > a').attr("href") == "#co-general-dtls") {
            $(this).html("Cancel");
        }

        $('#_Add_New_Company_Form_Save_').html("Next");
        $('#_Update_Existing_Company_Form_Save_').html("Next");
    } else {
        CompanyFormRemover();
    }
});

function CompanyFormRemover() {
    $('._Add_New_Company_Form_').slideUp("slow");
    $('._Add_New_Company_Form_').html('');
    $("#_Add_New_Company_btn_").attr("disabled", false);
}

var checked_Comapany;
function getCheckedCompaniestoDel() {
    var data_value_companyid = "";
    var data_value_companyname = "";

    checked_Comapany = new Array();

    $('#_tbl_Company_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkCompanyDel')).is(':checked')) {
            data_value_companyid = $(this).find($('td')).find($('.chkCompanyDel')).attr('data_value_companyid');
            data_value_companyname = $(this).find($('td')).find($('.chkCompanyDel')).attr('data_value_companyname');
            var data = { id: data_value_companyid, Name: data_value_companyname, Delete_Status: "Requested" }
            checked_Comapany.push(data);
        }
    });
    if (checked_Comapany.length > 0) {
        SendRequestToDelCompanies();
    } else {
        $('#_Error_Message_Display_').html("There are No Brands to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteCompanies").click(function () {
    getCheckedCompaniestoDel();
});

function SendRequestToDelCompanies(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Company/SendRequestToDelCompanies/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteCompanyData": JSON.stringify(checked_Comapany)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.CompaniesNotDelete.length != 0) {
                $('#_Error_Message_Display_ > p').html('Some of The Companies cannot be deleted as they are associated with some of the Items.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                Companies_list.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteCompany").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        Companies_list.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                Companies_list.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteCompanies').css('display', 'none');
        }
        else {
            $("#PDeleteCompany").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteCompany").attr("disabled", false);
    });
}

$(document).on("change", ".chkCompanyDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteCompanies').show();
    }
    else {
        $('#_Send_request_To_DeleteCompanies').css('display', 'none');
    }
});

$(".ListCompanyFilter").click(function () {
    Companies_list.destroy();
    var filtertype = $(this).text();
    GetAllCompaniesList(filtertype);
});