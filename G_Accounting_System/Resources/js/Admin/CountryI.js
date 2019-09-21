
var Activity_Data = new Array();
$('.nav_tabs_for_new_country > li > a').click(function () {
    if ($(this).attr("href") == "#v-general-dtls") {
        setTimeout(function () { $('input[name=Full_name]').focus(); }, 1);
    }
});

$("._Add_New_Country_Form_Remover_").click(function () {
    CountryFormRemover();
});

$('#_Add_New_Country_Form_Save_').on('click', function () {
    InsertUpdateCountry();
});

$('._Add_New_Country_Form_Remover__').on('click', function () {
    CountryFormRemover();
});

function CountryFormRemover() {
    $('._Add_New_Country_Form_').slideUp("slow");
    $('._Add_New_Country_Form_').html('');
    $("#_Add_New_Country_btn_").attr("disabled", false);
    $('#_Add_New_Country_Form_Save_').attr("disabled", false);
}

function InsertUpdateCountry() {
    $('#_Error_Message_Display_ > span').html("");
    if ($("input[name=Full_name]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter Country Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        $('#_Add_New_Country_Form_Save_').attr("disabled", true);
        var url = '/Country/InsertUpdateCountry';
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
            if (resp.pFlag == 1) {
                $('#_Add_New_Country_btn_').attr("disabled", false);
                
                $('#_Error_Message_Display_ > span').html('');
                $('#_Error_Message_Display_').slideUp("slow");

                $('#_Success_Message_Display_ > span').html(resp.pDesc);
                $('._Add_New_Country_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                $("#_Add_New_Country_btn_").attr("disabled", false);

                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                CountryList.ajax.reload(null, false);
            } else {
                $('#_Add_New_Country_btn_').attr("disabled", false);
                $('#_Error_Message_Display_ > span').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            }
            }).fail(function () {
                $('#_Add_New_Country_btn_').attr("disabled", false);
            $('#_Error_Message_Display_ > span').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        });
    }
};

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Country_Form_Save_').click();
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
//        $('._Add_New_Country_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);