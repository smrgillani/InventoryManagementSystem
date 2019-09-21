$("#_Add_New_Brand_Form_Save_").click(function () {

    InsertUpdateBrands();
});

function InsertUpdateBrands() {

    if ($("input[name=brand_Name]").val() == "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Brand Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        $('#_Add_New_Brand_Form_Save_').attr("disabled", true);
        var url = '/Brand/InsertUpdateBrands';
        var Brand_Data = {
            id: $("input[name=brandid]").val(),
            Brand_Name: $("input[name=brand_Name]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "BrandData": JSON.stringify(Brand_Data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            $('#_Add_New_Brand_btn_').attr("disabled", false);

            if (resp.pFlag == "1") {
                var pBrandid_Out = resp.pBrandid_Out;
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);

                setTimeout(function () { $('input[name=brand_Name]').focus(); }, 1);
                BrandList.ajax.reload(null, false);
                $('._Add_New_Brand_Form_').slideUp("slow");
                $("input[name=brand_Name]").val("");
                $('input[name=brandid]').val("0");
                $('#_Add_New_Brand_Form_Save_').html("Save");
                $('#_Add_New_Brand_btn_').attr("disabled", false);
            }
            else {
                console.log(resp.pDesc);
                $('#_Add_New_Brand_Form_Save_').attr("disabled", false);
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }


        }).fail(function () {

            $('#_Add_New_Brand_Form_Save_').attr("disabled", false);
            alert("post error 0");
            $('input[name=brandid]').val("0");
        });
    }
};

$('#_Add_New_Brand_Form_Remover__').on('click', function () {
    $('._Add_New_Brand_Form_').slideUp("slow");
    $("input[name=brand_Name]").val("");
    $('input[name=brandid]').val("0");
    $('#_Add_New_Brand_Form_Save_').html("Save");
    $('#_Add_New_Brand_Form_Save_').attr("disabled", false);
    $('#_Add_New_Brand_btn_').attr("disabled", false);
});

$('#_Add_New_Brand_Form_Remover_').on('click', function () {
    $('._Add_New_Brand_Form_').slideUp("slow");
    $("input[name=brand_Name]").val("");
    $('input[name=brandid]').val("0");
    $('#_Add_New_Brand_Form_Save_').html("Save");
    $('#_Add_New_Brand_btn_').attr("disabled", false);
    $('#_Add_New_Brand_Form_Save_').attr("disabled", false);
});