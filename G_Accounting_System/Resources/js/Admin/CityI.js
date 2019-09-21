$('.nav_tabs_for_new_city > li > a').click(function () {
    if ($(this).attr("href") == "#v-general-dtls") {
        setTimeout(function () { $('input[name=Full_name]').focus(); }, 1);
    }
});

$("._Add_New_City_Form_Remover_").click(function () {
    CityFormRemover();
});

$('#_Add_New_City_Form_Save_').on('click', function () {
    InsertUpdateCities();
});

$('._Add_New_City_Form_Remover__').on('click', function () {
    CityFormRemover();
});

function CityFormRemover() {
    $('._Add_New_City_Form_').slideUp("slow");
    $('._Add_New_City_Form_').html('');
    $("#_Add_New_City_btn_").attr("disabled", false);
    $('#_Add_New_City_Form_Save_').attr("disabled", false);
    $('#_Add_New_City_Form_Save_').attr("disabled", false);
}

var Activity_Data = new Array();
function InsertUpdateCities() {
    $('#_Error_Message_Display_ > span').html("");
    if ($("input[name=Full_name]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter City Name<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=Address_country] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > span').html("Please Select Country");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        $('#_Add_New_City_Form_Save_').attr("disabled", true);
        var url = '/City/InsertUpdateCities';
        var city = {
            id: $("input[name=cities_id]").val(),
            Name: $("input[name=Full_name]").val(),
            Country: $("select[name=Address_country] option:selected").val(),
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
            if (resp.pFlag == 1) {
                $("#_Add_New_City_btn_").attr("disabled", false);
                
                var City_id = resp.pCityid_Out;
                $('#_Error_Message_Display_ > span').html('');
                $('#_Error_Message_Display_').slideUp("slow");

                $('#_Success_Message_Display_ > span').html(resp.pDesc);
                $('._Add_New_City_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                $("#_Add_New_City_btn_").attr("disabled", false);

                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                CitiesList.ajax.reload(null, false);
            } else {
                $("#_Add_New_City_btn_").attr("disabled", false);
                $('#_Error_Message_Display_ > span').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            }
            }).fail(function () {
                $("#_Add_New_City_btn_").attr("disabled", false);
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
//        $('#_Add_New_City_Form_Save_').click();
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
//        $('._Add_New_City_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);