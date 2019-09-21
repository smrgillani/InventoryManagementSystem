var Activity_Data = new Array();
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
        InsertUpdateCompanies();
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
    $('#_Add_New_Company_Form_Save_').attr("disabled", false);
}

function InsertUpdateCompanies() {
    $('#_Error_Message_Display_ > p').html("");
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if ($("input[name=Full_name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Enter Company Full Name In General Details.<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=Contact_phone_landline]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Enter Landline Number Of Company In General Details.<br />");
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
        $('#_Error_Message_Display_ > p').html("Enter Mobile Number Of Company In General Details.<br />");
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
    //    $('#_Error_Message_Display_ > p').html("Please Enter Contact Email<br />");
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
    //else if ($("input[name=Website]").val() === "") {
    //    $('#_Error_Message_Display_ > p').html("Please Enter Website<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=Address]").val() === "") {
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
        $('#_Add_New_Company_Form_Save_').attr("disabled", true);
        var url = '/Company/InsertUpdateCompanies';
        var company = {
            id: $("input[name=companies_id]").val(),
            Name: $("input[name=Full_name]").val(),
            Landline: $("input[name=Contact_phone_landline]").val(),
            Mobile: $("input[name=Contact_phone_mobile]").val(),
            Email: $("input[name=Contact_email]").val(),
            Website: $("input[name=Website]").val(),
            Address: $("input[name=Address]").val(),
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
                $('#_Add_New_Company_Form_Save_').attr("disabled", false);
                var Company_id = resp.pCompanyid_Out;
                $('#_Error_Message_Display_ > p').html('');
                $('#_Error_Message_Display_').slideUp("slow");

                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('._Add_New_Company_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                $("#_Add_New_Company_btn_").attr("disabled", false);
                Companies_list.ajax.reload(null, false);

                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
            } else {
                $('#_Add_New_Company_Form_Save_').attr("disabled", false);
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            }
            }).fail(function () {
                $('#_Add_New_Company_Form_Save_').attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        });
    }
};

$("#Address_country").change(function () {
    var Country_id = $(this).find("option:selected").val();
    CitiesDropdown(Country_id);
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Company_Form_Save_').click();
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
//        $('._Add_New_Company_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);