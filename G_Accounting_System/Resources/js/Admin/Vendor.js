$("#_Add_New_Vendor_btn_").click(function () {

    AddFormVendor();

});

function AddFormVendor() {
    $('._Add_New_Vendor_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Vendor/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $("#_Add_New_Vendor_btn_").attr("disabled", false);
            $('._Add_New_Vendor_Form_').append(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Vendor_Form_').slideDown("slow");

            CountriesDropdown();
            DropDownCompanies();
            $('.select2').select2();
            $('#_Add_New_Vendor_Form_Save_').attr("disabled", false);
            $('select[name=Salutation]').focus();
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Vendor_Form_').slideUp("slow");
            $('._Add_New_Vendor_Form_').html("");
            $("#_Add_New_Vendor_btn_").attr("disabled", false);
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
    if (url == "/vendor/" || url == "/vendor/index") {
        GetAllVendorsList(null);
    }
    else if (url.includes("/vendor/profile/")) {
        GetAVendor();
        var Vendor_id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
        VendorTransactions_Items(Vendor_id);
        VendorTransactions_Bills(Vendor_id);
        VendorTransactions_Payments(Vendor_id);
        //} else if (url.includes("/item/items")) {
        //    GetAllVendorsListForItem();
    }
    //else if (url.includes("/vendor?id=")) {
    //    GetAllVendorsList(window.location.search.toString().toLowerCase());
    //}
})

function GetAllVendorsList(parameter) {
    var token = $('[name=__RequestVerificationToken]').val();

    VendorList = $('#vendors_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No vendors available"
        },
        "sAjaxSource": "/Vendor/GetAllVendors",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=VendorStartDate]').val(), EndDate: $('input[name=VendorEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[50].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/vendor/profile/" + data.id + "'>" + data.Name + "</a>";

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
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden EditVendor' onclick='EditVendor(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepVendor(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons EditVendor' onclick='EditVendor(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepVendor(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkVendorDel" data_value_vendorid=' + data.id + ' data_value_vendorname=' + data.Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkVendorDel" data_value_vendorid=' + data.id + ' data_value_vendorname=' + data.Name + '>';
                    }
                    return data;
                }

            },
        ]
    });
}

var Activity_Data = new Array();
function UpdatepVendor(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Vendor/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            VendorList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Vendor Profile Visibility Updated Successfully.');
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

function GetAVendor() {
    var id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/vendor/profile/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteVendor").attr("disabled", true);
        } else {
            $("#PDeleteVendor").attr("disabled", false);
        }
        $("#vp__vendor_name").html('');
        $("#vp__vendor_name").html(resp.Full_name_);
        $("#vp__vendor_designation").html('');
        $("#vp__vendor_designation").html(resp.Designation);
        $("#vp__vendor_status").html('');
        $("#vp__vendor_status").html(resp.IsEnabled_);
        $("#vp__vendor_mobile").html('');
        $("#vp__vendor_mobile").html(resp.Mobile);
        $("#vp__vendor_phone").html('');
        $("#vp__vendor_phone").html(resp.Landline);
        $("#vp__vendor_email").html('');
        $("#vp__vendor_email").html(resp.Email);
        $("#vp__vendor_website").html('');
        $("#vp__vendor_website").html(resp.Website);
        $("#vp__vendor_company_name").html('');
        $("#vp__vendor_company_name").html(resp.Company);
        $("#vp__vendor_address").html('');
        $("#vp__vendor_address").html(resp.Address);
        $("#vp__vendor_address_phone").html('');
        $("#vp__vendor_address_phone").html(resp.AddressLandline);
        $("#vp__vendor_address_city").html('');
        $("#vp__vendor_address_city").html(resp.CityName);
        $("#vp__vendor_address_country").html('');
        $("#vp__vendor_address_country").html(resp.CountryName);
        $("#vp__vendor_bank_account_number").html('');
        $("#vp__vendor_bank_account_number").html(resp.BankAccountNumber);
        $("#vp__vendor_payment_method").html('');
        $("#vp__vendor_payment_method").html(resp.Payment_method_);

        $("input[name=vendors_id]").val(resp.id);
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
        var ActivityType = "Vendor";
        Activities(ActivityType_id, ActivityType);
        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");
        CountriesDropdown();
    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function DelVendor(id) {
    $("#PDeleteVendor").attr("disabled", true);
    checked_Vendor = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/vendor/profile/")) {
        id = $("input[name=vendors_id]").val();
        name = $("#vp__vendor_name").text();
    }
    var data = { id: id, Name: name, Delete_Status: "Requested" }
    checked_Vendor.push(data);
    url = "/vendor/Index";
    SendRequestToDelVendor(url);
}

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
});

function EditVendor(id) {
  

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Vendor/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            AddFormVendor();
            $('#_Add_New_Vendor_Form_Save_').attr("disabled", false);
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
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Vendor_Form_').slideDown("slow");
            //$('#_Add_New_Vendor_Form_Save_').attr('id', '_Update_Existing_Vendor_Form_Save_');
            $("#vendorid").val(id);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Vendor_Form_').slideUp("slow");
            $('._Add_New_Vendor_Form_').html("");
            $("#_Add_New_Vendor_btn_").attr("disabled", false);
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

$("input[name=VendorStartDate]").change(function () {
    VendorList.ajax.reload(null, false);
});

$("input[name=VendorEndDate]").change(function () {
    VendorList.ajax.reload(null, false);
});

var checked_Vendor;
function getCheckedVendorstoDel() {
    var data_value_vendorid = "";
    var data_value_vendorname = "";

    checked_Vendor = new Array();

    $('#_tbl_Vendor_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkVendorDel')).is(':checked')) {
            data_value_vendorid = $(this).find($('td')).find($('.chkVendorDel')).attr('data_value_vendorid');
            data_value_vendorname = $(this).find($('td')).find($('.chkVendorDel')).attr('data_value_vendorname');
            var data = { id: data_value_vendorid, Name: data_value_vendorname, Delete_Status: "Requested" }
            checked_Vendor.push(data);
        }
    });
    if (checked_Vendor.length > 0) {
        SendRequestToDelVendor();
    } else {
        $('#_Error_Message_Display_').html("There are No Vendors to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteVendor").click(function () {
    getCheckedVendorstoDel();
});

function SendRequestToDelVendor(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Vendor/SendRequestToDelVendor/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteVendorData": JSON.stringify(checked_Vendor)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.ContactsNotDelete.length != 0) {
                $('#_Error_Message_Display_ > p').html('Some of The Vendors cannot be deleted as they are part of other transactions.');
                $('#_Success_Mes_Error_Message_Display_sage_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                VendorList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteVendor").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        VendorList.ajax.reload(null, false);
                    }

                }, 5000);
            }
            if (url == null) {
                VendorList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteVendor').css('display', 'none');
        }
        else {
            $("#PDeleteVendor").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteVendor").attr("disabled", false);
    });
}

$("#PAddress_country").change(function () {
    var Country_id = $("select[name=PAddress_country] option:checked").val();
    CitiesDropdown(Country_id);
});

$(document).on("change", ".chkVendorDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteVendor').show();
    }
    else {
        $('#_Send_request_To_DeleteVendor').css('display', 'none');
    }
});

$(".ListVendorFilter").click(function () {
    VendorList.destroy();
    var filtertype = $(this).text();
    GetAllVendorsList(filtertype);
});

function VendorTransactions_Items(Vendor_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Vendor_id;
    VendorTransactions_ItemsList = $('#VendorTransactions_Items').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Vendor/VendorTransactions_Items/",
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
                    return "<a href='/Purchase/Invoice/" + data.id + "'>" + data.TempOrderNum + "</a>";

                }
            },
            { data: "ItemQty" },
            { data: "PriceUnit" },
            { data: "TotalPrice" },
            { data: "DateOfDay" },
            { data: "TimeOfDay" },

        ],

    });
}

function VendorTransactions_Bills(Vendor_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Vendor_id;
    VendorTransactions_ItemsList = $('#VendorTransactions_Bills').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Vendor/VendorTransactions_Bills/",
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
                    return "<a href='/Bill/BillInvoice/" + data.id + "'>" + data.Bill_No + "</a>";

                }
            },
            { data: "Bill_Status" },
            { data: "Bill_Amount" },
            { data: "Balance_Amount" },
            { data: "Date" },
            { data: "Time" },
        ],

    });
}

function VendorTransactions_Payments(Vendor_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    parameter = Vendor_id;
    VendorTransactions_ItemsList = $('#VendorTransactions_Payments').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Vendor/VendorTransactions_Payments/",
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
                    return "<a href='/bill/billpayment/" + data.Bill_id + "'>" + data.Bill_No + "</a>";

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

$("#_Save_New_Company_Pop_").click(function () {
    if ($(this).html() == "Next") {
        $('.nav_tabs_for_new_company > .active').next('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_company > .active > a').attr("href") == "#co-payment-dtls") {
            $(this).html("Save");
        }
        $('#_Cancel_New_Company_Pop_').html("Previous");
    } else {
        InsertUpdateCompanies();
        $('._Add_New_Company_Form_').html('');

        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
    }
});

$('.nav_tabs_for_new_company > li > a').click(function () {
    if ($(this).attr("href") == "#co-general-dtls") {
        $('#_Cancel_New_Company_Pop_').html("Cancel");
        $('#_Save_New_Company_Pop_').html("Next");
        setTimeout(function () { $('input[name=Full_name]').focus(); }, 1);
    }
    if ($(this).attr("href") == "#co-address-dtls") {
        $('#_Cancel_New_Company_Pop_').html("Previous");
        $('#_Save_New_Company_Pop_').html("Next");
        setTimeout(function () { $('input[name=Address]').focus(); }, 1);
    }
    if ($(this).attr("href") == "#c-payment-dtls") {
        $('#_Cancel_New_Company_Pop_').html("Previous");
        $('#_Save_New_Company_Pop_').html("Save");
        setTimeout(function () { $('input[name=Bank_account_number]').focus(); }, 1);
    }
});

$('#_Cancel_New_Company_Pop_').on('click', function () {
    if ($(this).html() == "Previous") {
        $('.nav_tabs_for_new_company > .active').prev('li').find('a').trigger('click');
        if ($('.nav_tabs_for_new_company > .active > a').attr("href") == "#co-general-dtls") {
            $(this).html("Cancel");
        }
        $('#_Save_New_Company_Pop_').html("Next");
    } else {
        $('#modal-AddNewCompany').modal('toggle');
    }
});

function InsertUpdateCompanies() {
    $('#_Error_Message_Display_ > p').html("");
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if ($("input[name=Company_Full_name]").val() == "") {
        $('#_Error_Message_Display_ > p').html("Enter Company Full Name In General Details.<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Company_phone_landline]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Enter Landline Number Of Company In General Details.<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Company_phone_landline]").val() != 0 && $("input[name=Company_phone_landline]").val().length > 13) {
        $('#_Error_Message_Display_ > p').html("Invalid Landline Format.<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Company_phone_mobile]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Enter Mobile Number Of Company In General Details.<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Company_phone_mobile]").val() != 0 && $("input[name=Company_phone_mobile]").val().length > 14) {
        $('#_Error_Message_Display_ > p').html("Invalid Mobile Format.<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Company_email]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Contact Email<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if (!testEmail.test($("input[name=Company_email]").val()) && $("input[name=Company_email]").val().length != 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Email Format<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Website_Company]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Website<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=Address_Company]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Enter Office Location Address Of Company In Address Details.<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=get_Address_country] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Select Country for Company in Address Details<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=get_Address_city] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Select City for Company in Address Details<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Bank_account_number]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Bank Account No");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else {
        var url = '/Company/InsertUpdateCompanies';
        var company = {
            id: $("input[name=companies_id]").val(),
            Name: $("input[name=Company_Full_name]").val(),
            Landline: $("input[name=Company_phone_landline]").val(),
            Mobile: $("input[name=Company_phone_mobile]").val(),
            Email: $("input[name=Company_email]").val(),
            Website: $("input[name=Website_Company]").val(),
            Address: $("input[name=Address_Company]").val(),
            City: $("select[name=get_Address_city] option:checked").val(),
            Country: $("select[name=get_Address_country] option:checked").val(),
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
            if (resp.pFlag == 1) {
                DropDownCompanies();
                $('#modal-AddNewCompany').modal('toggle');
                $('#_Error_Message_Display_ > p').html('');
                $('#_Error_Message_Display_').slideUp("slow");

                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('._Add_New_Company_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
            } else {
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            }
        }).fail(function () {
            $('#_Error_Message_Display_ > p').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        });
    }
};

