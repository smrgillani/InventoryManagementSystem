$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/roleprivileges/index/" || url == "/roleprivileges/index") {
        RolesDropdown();
    }
})
//$("#_Add_New_Role_btn_").click(function () {
//    $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
//    $('._Add_New_RolePrivilege_Form_').slideDown("slow");
//    $('#_Add_New_Role_btn_').attr("disabled", true);
//});

function RolesDropdown() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('#get_roles').empty();
    $('#get_roles').append('<option value="0" selected="selected">Select Role</option>');

    $.ajax({
        url: '/RolePrivileges/RolesDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        dataType: "json",
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $("#get_roles");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $("#get_roles")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $("#get_roles").val("0");


    })
        .always(function () {

        });
}

function GetAllRolesList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    RolePrivilegesList = $('#tbl_RolePrivileges').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/RolePrivileges/getAllRolePrivileges",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[10].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Priv_Name" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Check_Status === '1') {
                        return '<input type="checkbox" data-Role_Priv_id=' + data.Role_Priv_id + ' data-Priv_id=' + data.Priv_id + ' class="icheckbox_minimal-blue chkCheckStatus" checked>';
                    } else {
                        return '<input type="checkbox" data-Role_Priv_id=' + data.Role_Priv_id + ' data-Priv_id=' + data.Priv_id + ' class="icheckbox_minimal-blue chkCheckStatus">';
                    }
                    return data;
                }

            }

        ]
    });
    RolePrivilegesList.destroy();
}

$("#get_roles").change(function () {
    var selectedText = $(this).find("option:selected").text();
    var selectedValue = $(this).find("option:selected").val();

    GetAllRolesList(selectedValue);
});

var checked_RolePriv;
function getCheckedPrivileges() {

    checked_RolePriv = new Array();
    var checkStatus = "";
    var enable = "";
    $('#_tbl_tbl_RolePrivileges_Body_ tr').each(function (indexoftr, tr) {

        var chk = $(this).find($('td')).find($('.chkCheckStatus'));
        var Role_Priv_id = chk.attr("data-Role_Priv_id");
        var Priv_id = chk.attr("data-Priv_id");


        if (chk.is(':checked')) {
            checkStatus = "1";
            enable = "1";
        }
        else {
            checkStatus = "0"
            enable = "0";
        }
        var data = { Role_id: $('#get_roles').val(), Priv_id: Priv_id, Role_Priv_id: Role_Priv_id, Check_Status: checkStatus, Enable: enable }
        checked_RolePriv.push(data);
    });
    if (checked_RolePriv.length > 0) {
        UpdateRolePrivileges();
    } else {
        $('#_Error_Message_Display_').html("There are No Privileges selected");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Add_Role_Privileges_Update_").click(function () {
    getCheckedPrivileges();
});

function UpdateRolePrivileges() {
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/RolePrivileges/UpdateRolePrivileges/',
        type: "POST",
        data: { __RequestVerificationToken: token, "RolePrivilegesData": JSON.stringify(checked_RolePriv) },
    }).done(function (resp) {
        $('#_Success_Message_Display_ > span').html("Role Privileges Updated Successfully");
        $('#_Success_Message_Display_').slideDown("slow");
        RolePrivilegesList.ajax.reload(null, false);
        checked_RolePriv = "";
        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html("Error!");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    })
}