$('#_Add_New_Premises_Form_Save_').on('click', function () {
    if ((window.location.pathname.toString().toLowerCase()) == "/office/index") {
        AddPremises("/Office/");
    } else if ((window.location.pathname.toString().toLowerCase()).includes("/factory/index")) {
        AddPremises("/Factory/");
    } else if ((window.location.pathname.toString().toLowerCase()).includes("/store/index")) {
        AddPremises("/Store/");
    } else if ((window.location.pathname.toString().toLowerCase()).includes("/shop/index")) {
        AddPremises("/Shop/");
    }
});

function AddPremises(curl) {

    $('#_Error_Message_Display_ > span').html("");
    var MACregexp = /^(([A-Fa-f0-9]{2}[:]){5}[A-Fa-f0-9]{2}[,]?)+$/i;

    if ($("input[name=premises_Name]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter Premises Name<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=premises_Name]").val().length > 255) {
        $('#_Error_Message_Display_ > span').html("Premises Name is too long (Max 255 characters)<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=premises_phone]").val() === "") {
    //    $('#_Error_Message_Display_ > span').html("Please Enter Premises Phone Number<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if (($("input[name=premises_phone]").val().length != 13 && $("input[name=premises_phone]").val().length != 0) || $("input[name=premises_phone]").val().length < 0) {
        $('#_Error_Message_Display_ > span').html("Incorrect Phone No Format<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=premises_pcma]").val() === "") {
    //    $('#_Error_Message_Display_ > span').html("Please Enter Registered Device Mac Address<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if (!MACregexp.test($("input[name=premises_pcma]").val()) && $("input[name=premises_pcma]").val().length != 0) {
        $('#_Error_Message_Display_ > span').html("Incorrect MAC address Format<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    //else if ($("input[name=premises_address]").val() === "") {
    //    $('#_Error_Message_Display_ > span').html("Please Enter Premises Address<br />");
    //    $('#_Error_Message_Display_').slideDown("slow");
    //    setTimeout(function () {
    //        $('#_Error_Message_Display_').slideUp("slow");
    //    }, 5000);
    //}
    else if ($("input[name=premises_address]").val().length > 255) {
        $('#_Error_Message_Display_ > span').html("Address too Long (Max 255 characters)<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=premises_country] option:selected").val() === "0") {
        $('#_Error_Message_Display_ > span').html("Please Select Country<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=premises_city] option:selected").val() === "0" || $("select[name=premises_city] option:selected").val() === null) {
        $('#_Error_Message_Display_ > span').html("Please Select City<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        $("#_Add_New_Premises_Form_Save_").attr('disabled', true);
        var url = curl + 'InserUpdatePremises';

        var premises_data = {
            id: $('input[name=premisesid]').val(),
            Name: $("input[name=premises_Name]").val(),
            pc_mac_Address: $("input[name=premises_pcma]").val(),
            Phone: $("input[name=premises_phone]").val(),
            City: $("select[name=premises_city] option:selected").val(),
            Country: $("select[name=premises_country] option:selected").val(),
            Address: $("input[name=premises_address]").val()
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "PremisesData": JSON.stringify(premises_data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == "1") {

                var pPremisesid_Out = resp.pPremisesid_Out;
                $('#_Success_Message_Display_ > span').html(resp.pDesc);
                $('._Add_New_Premises_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                PremisesList.ajax.reload(null, false);
                PremisesFormRemover();
            }
            else {
                $("#_Add_New_Premises_Form_Save_").attr('disabled', false);
                $('#_Error_Message_Display_ > span').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }
            
            $('#_Add_New_Premises_btn_').attr("disabled", false);

        }).fail(function () {
            $("#_Add_New_Premises_Form_Save_").attr('disabled', false);
            alert("post error 0");
        });
    }
}

$('#_Add_New_Premises_Form_Remover__').on('click', function () {
    PremisesFormRemover();
});

function PremisesFormRemover() {
    $('._Add_New_Premises_Form_').slideUp("slow");
    $('#_Add_New_Premises_btn_').attr("disabled", false);
    $("input[name=premises_Name]").val("");
    $("input[name=premises_pcma]").val("");
    $("input[name=premises_phone]").val("");
    $("input[name=premises_address]").val("");
    $("select[name=premises_city]").val("0").change();
    $("select[name=premises_country]").val("0").change();
    $("#_Add_New_Premises_Form_Save_").attr('disabled', false);
}

$("#getpremises_country").change(function () {
    var Country_id = $("#getpremises_country").val();
    if (Country_id != 0) {
        CitiesDropdown(Country_id);
    }
});