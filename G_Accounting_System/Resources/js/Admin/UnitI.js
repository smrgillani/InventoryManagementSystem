$("#_Add_New_Unit_Form_Save_").click(function () {
    InsertUpdateUnit();
});

function InsertUpdateUnit() {

    if ($("input[name=unit_Name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Unit Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        $("#_Add_New_Unit_Form_Save_").attr("disabled", true);
        var url = '/Unit/InsertUpdateUnit';
        var Unit_Data = {
            id: $("input[name=unitid]").val(),
            Unit_Name: $("input[name=unit_Name]").val(),
        }
        var token = $('[name=__RequestVerificationToken]').val();
        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "UnitData": JSON.stringify(Unit_Data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
            if (resp.pFlag == "1") {

                var pUnitid_Out = resp.pUnitid_Out;
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                UnitList.ajax.reload(null, false);
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);

                $('._Add_New_Unit_Form_').slideUp("slow");
                $("input[name=unit_Name]").val("");
                $('input[name=unitid]').val("0");
                //$('#_Add_New_Unit_Form_Save_').html("Save");
                $('#_Add_New_Unit_btn_').attr("disabled", false);
            }
            else {
                $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }

        }).fail(function () {
            $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
            alert("post error 0");
            $('input[name=unitid]').val("0");
        });
    }
};

$('#_Add_New_uNIT_Form_Remover__').on('click', function () {
    $('._Add_New_Unit_Form_').slideUp("slow");
    $("input[name=unit_Name]").val("");
    $('input[name=unitid]').val("0");
    $('#_Add_New_Unit_Form_Save_').html("Save");
    $('#_Add_New_Unit_btn_').attr("disabled", false);
    $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
});

$('#_Add_New_Unit_Form_Remover_').on('click', function () {
    $('._Add_New_Unit_Form_').slideUp("slow");
    $("input[name=unit_Name]").val("");
    $('input[name=unitid]').val("0");
    $('#_Add_New_Unit_Form_Save_').html("Save");
    $('#_Add_New_Unit_btn_').attr("disabled", false);
    $("#_Add_New_Unit_Form_Save_").attr("disabled", false);
});