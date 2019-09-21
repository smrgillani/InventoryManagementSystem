$("#_Add_New_Category_Form_Save_").click(function () {
    InsertUpdateCategory();
});

function InsertUpdateCategory() {

    if ($("input[name=category_Name]").val() === "") {
        $('#_Error_Message_Display_ > p').html("Please Enter Category Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        $('#_Add_New_Category_Form_Save_').attr("disabled", true);
        var url = '/Category/InsertUpdateCategory';
        var Category_data = {
            id: $("input[name=categoryid]").val(),
            Category_Name: $("input[name=category_Name]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "CategoryData": JSON.stringify(Category_data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == "1") {

                $('#_Add_New_Category_btn_').attr("disabled", false);
                var pCategoryid_Out = resp.pCategoryid_Out;
                $('#_Success_Message_Display_ > p').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                CategoryList.ajax.reload(null, false);

                $('._Add_New_Category_Form_').slideUp("slow");
                $("input[name=category_Name]").val("");
                $('input[name=categoryid]').val("0");
                $('#_Add_New_Category_Form_Save_').html("Save");
                $('#_Add_New_Category_btn_').attr("disabled", false);
            }
            else {
                $('#_Add_New_Category_Form_Save_').attr("disabled", false);
                $('#_Error_Message_Display_ > p').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }

        }).fail(function () {
            $('#_Add_New_Category_Form_Save_').attr("disabled", false);
            alert("post error 0");
            $('input[name=categoryid]').val("0");
        });
    }
};

$('#_Add_New_Category_Form_Remover__').on('click', function () {
    $('._Add_New_Category_Form_').slideUp("slow");
    $("input[name=category_Name]").val("");
    $('input[name=categoryid]').val("0");
    $('#_Add_New_Category_Form_Save_').html("Save");
    $('#_Add_New_Category_btn_').attr("disabled", false);
    $('#_Add_New_Category_Form_Save_').attr("disabled", false);
});

$('#_Add_New_Category_Form_Remover_').on('click', function () {
    $('._Add_New_Category_Form_').slideUp("slow");
    $("input[name=category_Name]").val("");
    $('input[name=categoryid]').val("0");
    $('#_Add_New_Category_Form_Save_').html("Save");
    $('#_Add_New_Category_btn_').attr("disabled", false);
    $('#_Add_New_Category_Form_Save_').attr("disabled", false);
});