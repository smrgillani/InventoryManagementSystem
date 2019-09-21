
$("#_Add_New_Employee_btn_").click(function () {
    $(this).attr("disabled", true);
    AddFormEmployee();
});

function AddFormEmployee() {
    $('._Add_New_Employee_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Employee/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_Employee_Form_').append(resp);
           
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Employee_Form_').slideDown("slow");
            

            $('select[name=Salutation]').focus();
            CountriesDropdown();
            DropDownCompanies();
            $("#_Add_New_Employee_Form_Save_").attr("disabled", false);
           
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
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });

}

$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/employee/" || url == "/employee/index") {
        GetAllEmployeesList(null);
    } else if (url.includes("/employee/profile/")) {
        GetAEmployee(url);
    } else if (url.includes("/employee?id=")) {
        GetAllEmployeesList(window.location.search.toString().toLowerCase());
    }
})

function GetAllEmployeesList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    Employees_list = $('#Employees_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No employees available"
        },
        "sAjaxSource": "/Employee/GetAllEmployees",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=EmployeeStartDate]').val(), EndDate: $('input[name=EmployeeEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[50].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/employee/profile/" + data.id + "'>" + data.Name + "</a>";

                }
            },
            { data: "Landline" },
            { data: "Email" },
            { data: "BankAccountNumber" },
            { data: "PaymentMethod" },
            { data: "Company" },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden EditEmployee' onclick='EditEmployee(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepEmployee(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons EditEmployee' onclick='EditEmployee(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepEmployee(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkEmployeeDel" data_value_employeeid=' + data.id + ' data_value_employeename=' + data.Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkEmployeeDel" data_value_employeeid=' + data.id + ' data_value_employeename=' + data.Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

var Activity_Data = new Array();
function UpdatepEmployee(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Employee/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            Employees_list.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Employee Profile Visibility Updated Successfully.');
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

function GetAEmployee(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteEmployee").attr("disabled", true);
        } else {
            $("#PDeleteEmployee").attr("disabled", false);
        }
        if (resp.filename == null || resp.filename == "") {
            $("#vp__employee_image").attr("src", "/Images/ItemImages/NoImageAvailable.jpg");
        } else {
            $("#vp__employee_image").attr("src", resp.filename);
        }
        $("#ep__employee_name").html('');
        $("#ep__employee_name").html(resp.Full_name_);
        $("#ep__employee_designation").html('');
        $("#ep__employee_designation").html(resp.Designation);
        $("#vp__employee_status").html('');
        $("#vp__employee_status").html(resp.IsEnabled_);
        $("#ep__employee_mobile").html('');
        $("#ep__employee_mobile").html(resp.Mobile);
        $("#ep__employee_phone").html('');
        $("#ep__employee_phone").html(resp.Landline);
        $("#ep__employee_email").html('');
        $("#ep__employee_email").html(resp.Email);
        $("#ep__employee_website").html('');
        $("#ep__employee_website").html(resp.Website);
        $("#ep__employee_company_name").html('');
        $("#ep__employee_company_name").html(resp.Company);
        $("#ep__employee_address").html('');
        $("#ep__employee_address").html(resp.Address);
        $("#ep__employee_address_phone").html('');
        $("#ep__employee_address_phone").html(resp.AddressLandline);
        $("#ep__employee_address_city").html('');
        $("#ep__employee_address_city").html(resp.CityName);
        $("#ep__employee_address_country").html('');
        $("#ep__employee_address_country").html(resp.CountryName);
        $("#ep__employee_bank_account_number").html('');
        $("#ep__employee_bank_account_number").html(resp.BankAccountNumber);
        $("#ep__employee_payment_method").html('');
        $("#ep__employee_payment_method").html(resp.Payment_method_);


        $("input[name=employees_id]").val(resp.id);
        $("select[name=Salutation]").val(resp.Salutation).trigger('change');
        $("input[name=Full_name]").val(resp.Name);
        $("input[name=Company_name]").val(resp.Company);
        $("input[name=Designation]").val(resp.Designation);
        $("input[name=Contact_phone_landline]").val(resp.Landline);
        $("input[name=Contact_phone_mobile]").val(resp.Mobile);
        $("input[name=Contact_email]").val(resp.Email);
        $("input[name=Website]").val(resp.Website);
        $("input[name=Address]").val(resp.Address);
        $("input[name=Address_phone]").val(resp.AddressLandline);
        $("select[name=Address_city]").val(resp.City).trigger('change');
        $("select[name=Address_country]").val(resp.Country).trigger('change');
        $("input[name=Bank_account_number]").val(resp.BankAccountNumber);
        $("select[name=Payment_method]").val(resp.PaymentMethod).trigger('change');
        $("select[name=C_Status]").val(resp.IsEnabled).trigger('change');
        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");

        var ActivityType_id = resp.id;
        var ActivityType = "Employee";
        Activities(ActivityType_id, ActivityType);

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function DelEmployee(id) {
    $("#PDeleteEmployee").attr("disabled", true);
    checked_Employees = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/employee/profile/")) {
        id = $("input[name=employees_id]").val();
        name = $("#ep__employee_name").text();
    }
    var data = { id: id, Name: name, Delete_Status: "Requested" }
    checked_Employees.push(data);
    url = "/employee/index";
    SendRequestToDelEmployees(url);
}

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
});

function EditEmployee(id) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        async: false,
        url: '/Employee/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            AddFormEmployee();
            $("#_Add_New_Employee_Form_Save_").attr("disabled", false);
            $('input[name=Salutation]').val(resp.Salutation).change();
            $('input[name=Full_name]').val(resp.Name);
            $('input[name=Designation]').val(resp.Designation);
            $('input[name=Contact_phone_landline]').val(resp.Landline);
            $('input[name=Contact_email]').val(resp.Email);
            $('input[name=Contact_phone_mobile]').val(resp.Mobile);
            $('input[name=Website]').val(resp.Website);
            $('input[name=Address]').val(resp.Address);
            $('input[name=Address_phone]').val(resp.AddressLandline);
            $('input[name=Bank_account_number]').val(resp.BankAccountNumber);
            $('select[name=Payment_method]').val(resp.PaymentMethod);
            $('select[name=C_Status]').val(resp.IsEnabled).change();
            //setTimeout(function () {
            $('select[name=Contact_company]').val(resp.CompanyId).change();
            //}, 200);
            //setTimeout(function () {
            $('select[name=Address_country]').val(resp.Country).change();
            //}, 400);
            //setTimeout(function () {
            $('select[name=Address_city]').val(resp.City).change();
            //}, 600);    
            $('select[name=Salutation]').focus();
            $('#_Add_New_Employee_Form_Save_').attr('id', '_Update_Existing_Employee_Form_Save_');
            $('input[name=Full_name]').focus();
            $("#employeeid").val(id);
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
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$('#Update_Existing_Employee').on('click', function () {
    UpdateEmployee();
});

function UpdateEmployee() {
    var url = '/Employee/UpdateEmployee';
    var employee = {
        id: $("input[name=employees_id]").val(),
        Salutation: $("select[name=Salutation] option:checked").val(),
        Name: $("input[name=Full_name]").val(),
        Company: $("input[name=Company_name]").val(),
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
        data: { __RequestVerificationToken: token, "Employee": JSON.stringify(employee) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.length == 0) {

            $('#_Error_Message_Display_ > p').html('');
            $('#_Error_Message_Display_').slideUp("slow");
            GetAEmployee(window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase());
            $('#_Success_Message_Display_ > p').html('Employee Profile Updated Successfully.');
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

var checked_Employees;
function getCheckedEmployeestoDel() {
    var data_value_employeeid = "";
    var data_value_employeename = "";

    checked_Employees = new Array();

    $('#_tbl_Employee_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkEmployeeDel')).is(':checked')) {
            data_value_employeeid = $(this).find($('td')).find($('.chkEmployeeDel')).attr('data_value_employeeid');
            data_value_employeename = $(this).find($('td')).find($('.chkEmployeeDel')).attr('data_value_employeename');
            var data = { id: data_value_employeeid, Name: data_value_employeename, Delete_Status: "Requested" }
            checked_Employees.push(data);
        }
    });
    if (checked_Employees.length > 0) {
        SendRequestToDelEmployees();
    } else {
        $('#_Error_Message_Display_').html("There are No Employees to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteEmployees").click(function () {
    getCheckedEmployeestoDel();
});

function SendRequestToDelEmployees(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Employee/SendRequestToDelEmployees/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteEmployeeData": JSON.stringify(checked_Employees)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.ContactsNotDelete.length != 0) {
                $("#PDeleteEmployee").attr("disabled", false);
                $('#_Error_Message_Display_ > p').html('Some of The Emplyees cannot be deleted as they are part of other transactions.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                Employees_list.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteEmployee").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');

                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        Employees_list.ajax.reload(null, false);
                    }

                }, 5000);
            }
            if (url == null) {
                Employees_list.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteEmployees').css('display', 'none');
        }
        else {
            $("#PDeleteEmployee").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteEmployee").attr("disabled", false);
    });
}

$("input[name=EmployeeStartDate]").change(function () {
    Employees_list.ajax.reload(null, false);
});

$("input[name=EmployeeEndDate]").change(function () {
    Employees_list.ajax.reload(null, false);
});

$(document).on("change", ".chkEmployeeDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteEmployees').show();
    }
    else {
        $('#_Send_request_To_DeleteEmployees').css('display', 'none');
    }
});

$(".ListEmployeeFilter").click(function () {
    Employees_list.destroy();
    var filtertype = $(this).text();
    GetAllEmployeesList(filtertype);
});

$("#_Add_New_Company_Pop_").click(function () {
    $('.nav-tabs-custom a[href="#co-general-dtls"]').tab('show');
});

$("#getAddress_country").change(function () {
    var Country_id = $("select[name=get_Address_country] option:checked").val();
    CitiesDropdown(Country_id);
});