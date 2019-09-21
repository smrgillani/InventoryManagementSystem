var Activity_Data = new Array();
$('.nav_tabs_for_new_vendor > li > a').click(function () {

    if ($(this).attr("href") == "#v-general-dtls") {
        $('._Add_New_Vendor_Form_Remover__').html("Cancel");
        $('#_Add_New_Vendor_Form_Save_').html("Next");
        $('#_Update_Existing_Vendor_Form_Save_').html("Next");
        setTimeout(function () { $('input[name=Full_name]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#v-address-dtls") {
        $('._Add_New_Vendor_Form_Remover__').html("Previous");
        $('#_Add_New_Vendor_Form_Save_').html("Next");
        $('#_Update_Existing_Vendor_Form_Save_').html("Next");
        setTimeout(function () { $('input[name=Address]').focus(); }, 1);
    }

    if ($(this).attr("href") == "#v-payment-dtls") {
        $('._Add_New_Vendor_Form_Remover__').html("Previous");
        $('#_Add_New_Vendor_Form_Save_').html("Save");
        $('#_Update_Existing_Vendor_Form_Save_').html("Update");
        setTimeout(function () { $('input[name=Bank_account_number]').focus(); }, 1);
    }

});

$("._Add_New_Vendor_Form_Remover_").click(function () {
    VendorFormRemover();
});

$('#_Add_New_Vendor_Form_Save_').on('click', function () {
    if ($(this).html() == "Next") {
        $('.nav-tabs > .active').next('li').find('a').trigger('click');
        if ($('.nav-tabs > .active > a').attr("href") == "#v-payment-dtls") {
            if ($(this).attr('id') == '_Update_Existing_Vendor_Form_Save_') {
                $(this).html("Update");
            } else {
                $(this).html("Save");
            }
        }
        $('._Add_New_Vendor_Form_Remover__').html("Previous");
    } else {
        InsertUpdateVendor();
    }
});

$('._Add_New_Vendor_Form_Remover__').on('click', function () {
    if ($(this).html() == "Previous") {
        $('.nav-tabs > .active').prev('li').find('a').trigger('click');
        if ($('.nav-tabs > .active > a').attr("href") == "#v-general-dtls") {
            $(this).html("Cancel");
        }

        $('#_Add_New_Vendor_Form_Save_').html("Next");
        $('#_Update_Existing_Vendor_Form_Save_').html("Next");
    } else {
        VendorFormRemover();
    }
});

function VendorFormRemover() {
    $('._Add_New_Vendor_Form_').slideUp("slow");
    $('._Add_New_Vendor_Form_').html('');
    $("#_Add_New_Vendor_btn_").attr("disabled", false);
    $('#_Add_New_Vendor_Form_Save_').attr("disabled", false);
}

function InsertUpdateVendor() {
   
    $('#_Error_Message_Display_ > p').html("");
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if ($("input[name=Full_name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Name<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Full_name]").val().length > 255) {
        $('#_Error_Message_Display_ > p').html("Name is too long (Max 255 characters)<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=Contact_company] option:checked").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Company<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Designation]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Designation<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=Contact_phone_landline]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Landline<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if (($("input[name=Contact_phone_landline]").val().length != 13 && $("input[name=Contact_phone_landline]").val().length != 0) || $("input[name=Contact_phone_landline]").val().length < 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Landline Phone No Format, example format 0092xx1234567<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Contact_phone_mobile]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Mobile No");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if (($("input[name=Contact_phone_mobile]").val().length != 14 && $("input[name=Contact_phone_mobile]").val().length != 0) || $("input[name=Contact_phone_mobile]").val().length < 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Mobile No Format. example format 0092xxx1234567<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Contact_email]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Contact Email");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if (!testEmail.test($("input[name=Contact_email]").val()) && $("input[name=Contact_email]").val().length != 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Email Format<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Address]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Address<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Address]").val().length > 255) {
        $('#_Error_Message_Display_ > p').html("Address too Long (Max 255 characters)<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Address_phone]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Address Phone No<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if (($("input[name=Address_phone]").val().length != 13 && $("input[name=Address_phone]").val().length != 0) || $("input[name=Address_phone]").val().length < 0) {
        $('#_Error_Message_Display_ > p').html("Incorrect Phone No format<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=Address_country] option:selected").val() == "0") {
        $('#_Error_Message_Display_ > p').html("Please Select Country<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=Address_city] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > p').html("Please Select City<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=Bank_account_number]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Bank Account No<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else {
        $('#_Add_New_Vendor_Form_Save_').attr("disabled", true);
        var url = '/Vendor/InsertUpdateVendor';
        var vendor = {
            id: $("#vendorid").val(),
            Salutation: $("select[name=Salutation] option:checked").val(),
            Name: $("input[name=Full_name]").val(),
            CompanyId: $("select[name=Contact_company] option:checked").val(),
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
            data: { __RequestVerificationToken: token, "Vendor": JSON.stringify(vendor) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            $('#_Add_New_Vendor_Form_Save_').attr("disabled", false);
            if (resp.pFlag == 1) {
               
                var vendor_id = resp.pContactid_Out;
                $('#_Error_Message_Display_ > p').html('');
                $('#_Error_Message_Display_').slideUp("slow");

                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('._Add_New_Vendor_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                $("#_Add_New_Vendor_btn_").attr("disabled", false);
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);

                $("#vendorid").val("0");
                VendorList.ajax.reload(null, false);
            } else {
                $('#_Add_New_Vendor_Form_Save_').attr("disabled", false);
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            }
            }).fail(function () {
                $('#_Add_New_Vendor_Form_Save_').attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        });
    }
};

function UpdateVendor() {
    var url = '/Vendor/UpdateVendor';
    var vendor = {
        id: $("input[name=vendors_id]").val(),
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
        data: { __RequestVerificationToken: token, "Vendor": JSON.stringify(vendor) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.length == 0) {

            $('#_Error_Message_Display_ > p').html('');
            $('#_Error_Message_Display_').slideUp("slow");

            $('#_Success_Message_Display_ > p').html('Vendor Profile Updated Successfully.');
            $('._Add_New_Vendor_Form_').slideUp("slow");
            $('#_Success_Message_Display_').slideDown("slow");
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

$("#getAddress_country").change(function () {
    var Country_id = $("select[name=Address_country] option:checked").val();
    CitiesDropdown(Country_id);
});

$("#Update_Existing_Vendor").click(function () {
    var id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
    $("#vendorid").val(id);
    InsertUpdateVendor();
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Vendor_Form_Save_').click();
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
//        $('._Add_New_Vendor_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$("#_Add_New_Company_Pop_").click(function () {
    $('.nav-tabs-custom a[href="#co-general-dtls"]').tab('show');
});

$("#Address_country").change(function () {
    var Country_id = $("select[name=get_Address_country] option:checked").val();
    CitiesDropdown(Country_id);
});