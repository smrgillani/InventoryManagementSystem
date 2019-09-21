$('.select2').select2();
$("#_Add_New_Customer_btn_").click(function () {

    AddFormCustomer();
});

function AddFormCustomer() {
    $('._Add_New_Customer_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Customer/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $("#_Add_New_Customer_btn_").attr("disabled", true);

            $('._Add_New_Customer_Form_').append(resp);
            $('.nav-tabs a[href="#c-general-dtls"]').tab('show');
            $('._Add_New_Customer_Form_').slideDown("slow");

            CountriesDropdown();
            DropDownCompanies();
            $('#_Add_New_Customer_Form_Save_').attr("disabled", false);
            $('select[name=Salutation]').focus();
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Customer_Form_').slideUp("slow");
            $('._Add_New_Customer_Form_').html("");
            $("#_Add_New_Customer_btn_").attr("disabled", false);
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
    if (url == "/customer/" || url == "/customer/index") {
        GetAllCustomersList(null);
    } else if (url.includes("/customer/profile/")) {
        var Customer_id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
        GetACustomer(url);
        CustomerTransactions_Items(Customer_id);
        CustomerTransactions_Invoices(Customer_id);
        CustomerTransactions_Payments(Customer_id);
    } else if (url.includes("/customer?id=")) {
        GetAllCustomersList(window.location.search.toString().toLowerCase());
    }
})

var CustomerList = null;
function GetAllCustomersList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    CustomerList = $('#customers_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No customers available"
        },
        "sAjaxSource": "/Customer/GetAllCustomers",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=CustomerStartDate]').val(), EndDate: $('input[name=CustomerEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[50].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/customer/profile/" + data.id + "'>" + data.Name + "</a>";

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
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden EditVendor' onclick='EditCustomer(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepCustomer(" + data.id + ")' title='Visibility'></span>";
                    }
                    else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons EditVendor' onclick='EditCustomer(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepCustomer(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkCustomerDel" data_value_customerid=' + data.id + ' data_value_customername=' + data.Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkCustomerDel" data_value_customerid=' + data.id + ' data_value_customername=' + data.Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

var Activity_Data = new Array();
function UpdatepCustomer(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Customer/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        console.log(resp);
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            CustomerList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Customer Profile Visibility Updated Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
            Activity_Data = new Array();
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

function GetACustomer(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteCustomer").attr("disabled", true);
        } else {
            $("#PDeleteCustomer").attr("disabled", false);
        }
        $("#cp__customer_name").html('');
        $("#cp__customer_name").html(resp.Full_name_);
        $("#cp__customer_designation").html('');
        $("#cp__customer_designation").html(resp.Designation);
        $("#cp__customer_status").html('');
        $("#cp__customer_status").html(resp.IsEnabled_);
        $("#cp__customer_mobile").html('');
        $("#cp__customer_mobile").html(resp.Mobile);
        $("#cp__customer_phone").html('');
        $("#cp__customer_phone").html(resp.Landline);
        $("#cp__customer_email").html('');
        $("#cp__customer_email").html(resp.Email);
        $("#cp__customer_website").html('');
        $("#cp__customer_website").html(resp.Website);
        $("#cp__customer_company_name").html('');
        $("#cp__customer_company_name").html(resp.Company);
        $("#cp__customer_address").html('');
        $("#cp__customer_address").html(resp.Address);
        $("#cp__customer_address_phone").html('');
        $("#cp__customer_address_phone").html(resp.AddressLandline);
        $("#cp__customer_address_city").html('');
        $("#cp__customer_address_city").html(resp.CityName);
        $("#cp__customer_address_country").html('');
        $("#cp__customer_address_country").html(resp.CountryName);
        $("#cp__customer_bank_account_number").html('');
        $("#cp__customer_bank_account_number").html(resp.BankAccountNumber);
        $("#cp__customer_payment_method").html('');
        $("#cp__customer_payment_method").html(resp.Payment_method_);

        $("input[name=customers_id]").val(resp.id);
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
        $("input[name=Address_city]").val(resp.City);
        $("input[name=Address_country]").val(resp.Country);
        $("input[name=Bank_account_number]").val(resp.BankAccountNumber);
        $("select[name=Payment_method]").val(resp.PaymentMethod).trigger('change');
        $("select[name=C_Status]").val(resp.IsEnabled).trigger('change');

        var ActivityType_id = resp.id;
        var ActivityType = "Customer";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}


function DelCustomer(id) {
    $("#PDeleteCustomer").attr("disabled", true);
    checked_Customers = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/customer/profile/")) {
        id = $("input[name=customers_id]").val();
        name = $("#cp__customer_name").text();
    }
    var data = { id: id, Name: name, Delete_Status: "Requested" }
    checked_Customers.push(data);
    url = "/customer/Index";
    SendRequestToDelCustomers(url);
}

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
});

function EditCustomer(id) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        async: false,
        url: '/Customer/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            AddFormCustomer();
            $("#_Add_New_Customer_btn_").attr("disabled", false);
            $('#_Add_New_Customer_Form_Save_').attr("disabled", false);
            CountriesDropdown();
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

            $('.nav-tabs a[href="#c-general-dtls"]').tab('show');
            $('._Add_New_Customer_Form_').slideDown("slow");
            $('#_Add_New_Customer_Form_Save_').attr('id', '_Update_Existing_Customer_Form_Save_');
            $('input[name=Full_name]').focus();
            $("#customerid").val(id);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Customer_Form_').slideUp("slow");
            $('._Add_New_Customer_Form_').html("");
            $("#_Add_New_Customer_btn_").attr("disabled", false);
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

$("input[name=CustomerStartDate]").change(function () {
    CustomerList.ajax.reload(null, false);
});

$("input[name=CustomerEndDate]").change(function () {
    CustomerList.ajax.reload(null, false);
});

function getCheckedCustomerstoDel() {
    var data_value_customerid = "";
    var data_value_customername = "";

    checked_Customers = new Array();

    $('#_tbl_Customer_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkCustomerDel')).is(':checked')) {
            data_value_customerid = $(this).find($('td')).find($('.chkCustomerDel')).attr('data_value_customerid');
            data_value_customername = $(this).find($('td')).find($('.chkCustomerDel')).attr('data_value_customername');
            var data = { id: data_value_customerid, Name: data_value_customername, Delete_Status: "Requested" }
            checked_Customers.push(data);
        }
    });
    if (checked_Customers.length > 0) {
        SendRequestToDelCustomers();
    } else {
        $('#_Error_Message_Display_ > p').html("There are No Customers to Delete");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
}

$("#_Send_request_To_DeleteCustomers").click(function () {
    getCheckedCustomerstoDel();
});

function SendRequestToDelCustomers(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Customer/SendRequestToDelCustomers/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteCustomerData": JSON.stringify(checked_Customers)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.ContactsNotDelete.length != 0) {
                $('#_Error_Message_Display_ > p').html('Some of The Customers cannot be deleted as they are part of other transactions.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                CustomerList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteCustomer").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');

                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    window.location.href = url;
                    CustomerList.ajax.reload(null, false);
                }, 5000);
            }
            if (url == null) {
                CustomerList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteCustomers').css('display', 'none');
        }
        else {
            $("#PDeleteCustomer").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');

            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);
        }

    }).fail(function () {
        $("#PDeleteCustomer").attr("disabled", false);
    });
}

$(document).on("change", ".chkCustomerDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteCustomers').show();
    }
    else {
        $('#_Send_request_To_DeleteCustomers').css('display', 'none');
    }
});

$(".ListCustomerFilter").click(function () {
    CustomerList.destroy();
    var filtertype = $(this).text();
    GetAllCustomersList(filtertype);
});

function CustomerTransactions_Items(Customer_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Customer_id;
    CustomerTransactions_ItemsList = $('#CustomerTransactions_Items').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Customer/CustomerTransactions_Items/",
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

            { data: "ItemName" },
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/SalesOrder/SOInvoice/" + data.SalesOrder_id + "'>" + data.SaleOrderNo + "</a>";

                }
            },
            { data: "ItemQty" },
            { data: "PriceUnit" },
            { data: "SO_Total_Amount" },
            { data: "Date_Of_Day" },
            { data: "Time_Of_Day" },

        ],

    });
}

function CustomerTransactions_Invoices(Customer_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Customer_id;
    CustomerTransactions_ItemsList = $('#CustomerTransactions_Invoices').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Customer/CustomerTransactions_Invoices/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[35].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/SalesOrder/SOInvoice/" + data.SalesOrder_id + "?Invoice=true'>" + data.Invoice_No + "</a>";

                }
            },
            { data: "Invoice_Status" },
            { data: "Invoice_Amount" },
            { data: "Balance_Amount" },
            { data: "Date" },
            { data: "Time" },
        ],
    });
}

function CustomerTransactions_Payments(Customer_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Customer_id;
    CustomerTransactions_ItemsList = $('#CustomerTransactions_Payments').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Customer/CustomerTransactions_Payments/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[35].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/SalesOrder/SOInvoice/" + data.Invoice_id + "?Payments=true'>" + data.Invoice_No + "</a>";
                }
            },
            { data: "PaymentMode" },
            { data: "Total_Amount" },
            { data: "Paid_Amount" },
            { data: "Date" },
            { data: "Time" },
        ],
    });
}
