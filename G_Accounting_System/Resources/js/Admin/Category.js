$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/category/index/" || url == "/category/index") {
        GetAllCategoriesList(null);
    }
    if (url.includes("/category/profile/")) {
        GetACategory(url);
    }
})

$("#_Add_New_Category_btn_").click(function () {
    $(this).attr("disabled", true);
    AddCategoryForm();
});

function AddCategoryForm() {
    $('._Add_New_Category_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Category/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        console.log(resp);
        if (resp != false) {
            $('._Add_New_Category_Form_').html(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('#_Add_New_Category_Form_Save_').html("Save");
            $('._Add_New_Category_Form_').slideDown("slow");
            $('#_Add_New_Category_btn_').attr("disabled", true);
            $('#_Add_New_Category_Form_Save_').attr("disabled", false);
            setTimeout(function () { $('input[name=category_Name]').focus(); }, 1);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Category_Form_').slideUp("slow");
            $('._Add_New_Category_Form_').html("");
            $("#_Add_New_Category_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function GetAllCategoriesList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    CategoryList = $('#tbl_Category').DataTable({
        "language": {
            "emptyTable": "No categories available"
        },
        "bServerSide": true,
        "sAjaxSource": "/Category/GetAllCatgeories",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=CategoryStartDate]').val(), EndDate: $('input[name=CategoryEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/category/profile/" + data.id + "'>" + data.Category_Name + "</a>";

                }
            },
            { data: "IsEnabled_" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden' onclick='EditCategory(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepCategory(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons' onclick='EditCategory(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepCategory(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            },
            {
                data: function (data, type, dataToSet) {

                    if (data.Delete_Status === 'Requested') {
                        return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkCategoryDel" data_value_catid=' + data.id + ' data_value_catname=' + data.Category_Name + '>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkCategoryDel" data_value_catid=' + data.id + ' data_value_catname=' + data.Category_Name + '>';
                    }
                    return data;
                }

            }
        ]
    });
}

var Activity_Data = new Array();
function UpdatepCategory(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Category/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > p').html('');
            CategoryList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > p').html('Category Profile Visibility Updated Successfully.');
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



function GetACategory(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.Delete_Status == "Requested") {
            $("#PDeleteCategory").attr("disabled", true);
        } else {
            $("#PDeleteCategory").attr("disabled", false);
        }
        $("input[name=categories_id]").val(resp.id);
        var ActivityType_id = resp.id;
        $("#cp__category_name").html('');
        $("#cp__category_name").html(resp.Category_Name);
        $("#cp__category_status").html(resp.IsEnabled_);
        var ActivityType = "Category";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > p').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function EditCategory(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Category/Update/',
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        console.log(resp);
        if (resp != false) {
            AddCategoryForm();
            $("#_Add_New_Category_btn_").attr("disabled", false);
            $('#_Add_New_Category_Form_Save_').attr("disabled", false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_Category_Form_').slideDown("slow");
            $('#_Add_New_Category_Form_Save_').html('Update');
            $('#_Add_New_Category_Form_Save_').attr('id', '_Update_Existing_Category_Form_Save_');
            $('input[name=category_Name]').val(resp.Category_Name);
            $('input[name=categoryid]').val(id);
        }
        else {
            $('#_Error_Message_Display_ > p').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_Category_Form_').slideUp("slow");
            $('._Add_New_Category_Form_').html("");
            $("#_Add_New_Category_btn_").attr("disabled", false);
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);

        }

    }).fail(function () {
        $('#_Error_Message_Display_ > p').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        //$('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });

}

function DelCategory(id) {
    $("#PDeleteCategory").attr("disabled", true);
    checked_Cats = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/category/profile/")) {
        id = $("input[name=categories_id]").val();
        name = $("#cp__category_name").text();
    }
    var data = { id: id, Category_Name: name, Delete_Status: "Requested" }
    checked_Cats.push(data);
    url = "/brand/index";
    SendRequestToDelCats(url);

}



$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > p').html('');
});



$("input[name=CategoryStartDate]").change(function () {
    CategoryList.ajax.reload(null, false);
});

$("input[name=CategoryEndDate]").change(function () {
    CategoryList.ajax.reload(null, false);
});

var checked_Cats;
function getCheckedCatstoDel() {
    var data_value_catid = "";
    var data_value_catname = "";

    checked_Cats = new Array();

    $('#_tbl_Category_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkCategoryDel')).is(':checked')) {
            data_value_catid = $(this).find($('td')).find($('.chkCategoryDel')).attr('data_value_catid');
            data_value_catname = $(this).find($('td')).find($('.chkCategoryDel')).attr('data_value_catname');
            var data = { id: data_value_catid, Category_Name: data_value_catname, Delete_Status: "Requested" }
            checked_Cats.push(data);
        }
    });
    if (checked_Cats.length > 0) {
        SendRequestToDelCats();
    } else {
        $('#_Error_Message_Display_').html("There are No Categories to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteCategory").click(function () {
    getCheckedCatstoDel();
});

function SendRequestToDelCats(url) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Category/SendRequestToDelCategories/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteCategoryData": JSON.stringify(checked_Cats)
        },
    }).done(function (resp) {
        if (resp.Response.length == 0) {
            if (resp.CategoriesNotDelete.length != 0) {
                $("#PDeleteCategory").attr("disabled", false);
                $('#_Error_Message_Display_ > p').html('Some of the Categories cannot be Deleted as they are associated with some of the Items.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                CategoryList.ajax.reload(null, false);
                setTimeout(function () { $('#_Error_Message_Display_').slideUp("slow"); }, 5000);
            }
            else {
                $("#PDeleteCategory").attr("disabled", true);
                $('#_Success_Message_Display_ > p').html('Request Sent Successfully.');
                $('#_Success_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                    if (url != null) {
                        window.location.href = url;
                        CategoryList.ajax.reload(null, false);
                    }

                }, 5000);

            }
            if (url == null) {
                CategoryList.ajax.reload(null, false);
            }
            $('#_Send_request_To_DeleteCategory').css('display', 'none');
        }
        else {
            $("#PDeleteCategory").attr("disabled", false);
            $('#_Error_Message_Display_ > p').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {
        $("#PDeleteCategory").attr("disabled", false);
    });
}

$(document).on("change", ".chkCategoryDel", function () {
    if ($(this).is(":checked")) {
        $('#_Send_request_To_DeleteCategory').show();
    }
    else {
        $('#_Send_request_To_DeleteCategory').css('display', 'none');
    }
});

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_Category_Form_Save_').click();
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
//        $('#_Add_New_Category_Form_Remover__').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);

$(".ListCategoryFilter").click(function () {
    CategoryList.destroy();
    var filtertype = $(this).text();
    GetAllCategoriesList(filtertype);
});