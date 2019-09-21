$("#_Add_New_Manufacturer_Form_Save_").click(function () {
    InsertUpdateManufacturer();
});

function InsertUpdateManufacturer() {

    if ($("input[name=manufacturer_Name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Manufacturer Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        $("#_Add_New_Manufacturer_Form_Save_").attr("disabled", true);
        var url = '/Manufacturer/InsertUpdateManufacturer';
        var ManufacturerData = {
            id: $("input[name=manufacturerid]").val(),
            Manufacturer_Name: $("input[name=manufacturer_Name]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "ManufacturerData": JSON.stringify(ManufacturerData) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == "1") {
                $("#_Add_New_Manufacturer_Form_Save_").attr("disabled", false);
                var pManufacturerid_Out = resp.pManufacturerid_Out;
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                ManufacturerList.ajax.reload(null, false);
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);

                $('._Add_New_Manufacturer_Form_').slideUp("slow");
                $("input[name=manufacturer_Name]").val("");
                $('input[name=manufacturerid]').val("0");
                $('#_Add_New_Manufacturer_Form_Save_').html("Save");
                $('#_Add_New_Manufacturer_btn_').attr("disabled", false);
            }
            else {

                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }


        }).fail(function () {
            $("#_Add_New_Manufacturer_Form_Save_").attr("disabled", false);
            alert("post error 0");
            $('input[name=manufacturerid]').val("0");
        });
    }
};

$('#_Add_New_Manufacturer_Form_Remover__').on('click', function () {
    $('._Add_New_Manufacturer_Form_').slideUp("slow");
    $("input[name=manufacturer_Name]").val("");
    $('input[name=manufacturerid]').val("0");
    $('#_Add_New_Manufacturer_Form_Save_').html("Save");
    $('#_Add_New_Manufacturer_btn_').attr("disabled", false);
    $("#_Add_New_Manufacturer_Form_Save_").attr("disabled", false);
});

$('#_Add_New_Manufacturer_Form_Remover_').on('click', function () {
    $('._Add_New_Manufacturer_Form_').slideUp("slow");
    $("input[name=manufacturer_Name]").val("");
    $('input[name=manufacturerid]').val("0");
    $('#_Add_New_Manufacturer_Form_Save_').html("Save");
    $('#_Add_New_Manufacturer_btn_').attr("disabled", false);
    $("#_Add_New_Manufacturer_Form_Save_").attr("disabled", false);
});