
$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/roles/index/" || url == "/roles/index") {
        GetAllRoles(null);
    }
})

$("#_Add_New_Role_btn_").click(function () {
    $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
    $('#_Add_New_Role_Form_Save_').html("Save");
    $('._Add_New_Role_Form_').slideDown("slow");
    $('#_Add_New_Role_btn_').attr("disabled", true);
    $("input[name=role_Name]").focus();
});

function GetAllRoles(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    RolesList = $('#tbl_Roles').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No roles available"
        },
        "sAjaxSource": "/Roles/GetAllRoles",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[15].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Role_Name" },
            {
                data: function (data, type, dataToSet) {
                    return "<span class='fa fa-pencil-square-o table_list_ops_icons EditRole' onclick='EditRole(" + data.id + ")' title='Edit'></span><span class='fa fa-trash table_list_ops_icons' onclick='DelRole(" + data.id + ",this)' title='Delete'></span>";
                }
            }
        ]
    });
}

function InsertUpdateRole() {
    if ($("input[name=role_Name]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter Role Name");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        var url = '/Roles/InsertUpdateRoles';
        var Role_Data = {
            id: $("input[name=Roleid]").val(),
            Role_Name: $("input[name=role_Name]").val(),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "RoleData": JSON.stringify(Role_Data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {

            if (resp.pFlag == "1") {
                $('#_Success_Message_Display_ > span').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                RolesList.ajax.reload(null, false);
                $("input[name=role_Name]").val("");
                $('input[name=Roleid]').val("0");
                $('#_Add_New_Role_Form_Save_').html("Save");
                $('#_Add_New_Role_btn_').attr("disabled", false);
            }
            else {
                $('#_Error_Message_Display_ > span').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Error_Message_Display_').slideUp("slow");
                }, 5000);
            }


        }).fail(function () {
            alert("post error 0");
            $('input[name=brandid]').val("0");
        });
    }
};

function EditRole(id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Roles/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token, id },
    }).done(function (resp) {
        console.log(resp);
        $('#_Error_Message_Display_ > span').html('');
        $('#_Error_Message_Display_').slideUp("slow");
        $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
        $('._Add_New_Role_Form_').slideDown("slow");
        $('#_Add_New_Role_Form_Save_').html('Update');

        $('input[name=role_Name]').val(resp.Role_Name);
        $('input[name=Roleid]').val(id);
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        //$('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });

}

function DelRole(id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Roles/Del/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp.length == 0) {
            $('#_Success_Message_Display_ > span').html('Role Deleted Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () { $('#_Success_Message_Display_').slideUp("slow"); }, 5000);
            $('input[name=Roleid]').val("0");
            RolesList.ajax.reload(null, false);
        } else {
            $('#_Error_Message_Display_ > span').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            $('input[name=Roleid]').val("0");
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        $('input[name=Roleid]').val("0");
    });
}

$("#_Add_New_Role_Form_Save_").click(function () {
    InsertUpdateRole();
});

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > span').html('');
})

$('#_Add_New_Role_Form_Remover__').on('click', function () {
    $('._Add_New_Role_Form_').slideUp("slow");
    $("input[name=role_Name]").val("");
    $('input[name=Roleid]').val("0");
    $('#_Add_New_Role_Form_Save_').html("Save");
    $('#_Add_New_Role_btn_').attr("disabled", false);
});

$('#_Add_New_Role_Form_Remover_').on('click', function () {
    $('._Add_New_Role_Form_').slideUp("slow");
    $("input[name=role_Name]").val("");
    $('input[name=Roleid]').val("0");
    $('#_Add_New_Role_Form_Save_').html("Save");
    $('#_Add_New_Role_btn_').attr("disabled", false);
});

jQuery(document).keydown(function (event) {
    // If Control or Command key is pressed and the S key is pressed
    // run save function. 83 is the key code for S.

    if (event.which == 13) {
        // Save Function
        $('#_Add_New_Role_Form_Save_').click();
        event.preventDefault();

        return false;
    }
}
);

jQuery(document).keydown(function (event) {
    // If Control or Command key is pressed and the S key is pressed
    // run save function. 83 is the key code for S.

    if (event.which == 27) {
        // Save Function
        $('#_Add_New_Role_Form_Remover__').click();
        event.preventDefault();

        return false;
    }
}
);