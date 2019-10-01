$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/userpreviliges/index/" || url == "/userpreviliges/index") {
        DropDownUsers();
    }
})

function GetAllUserPrivilegesList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    UserPrivilegesList = $('#tbl_UserPrivileges').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/UserPreviliges/getAllUserPrivileges",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[25].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "PrivName" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Add === 1) {
                        return '<input type="checkbox" data-id=' + data.id + ' data-priv_ID=' + data.priv_ID + ' class="icheckbox_minimal-blue chkAdd" checked>';
                    } else {
                        return '<input type="checkbox" data-id=' + data.id + ' data-priv_ID=' + data.priv_ID + ' class="icheckbox_minimal-blue chkAdd">';
                    }
                    return data;
                }

            },
            {
                data: function (data, type, dataToSet) {
                    if (data.Edit === 1) {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkEdit" checked>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkEdit">';
                    }
                    return data;
                }

            },
            //{
            //    data: function (data, type, dataToSet) {
            //        if (data.Delete === 1) {
            //            return '<input type="checkbox" class="icheckbox_minimal-blue chkDelete" checked>';
            //        } else {
            //            return '<input type="checkbox" class="icheckbox_minimal-blue chkDelete">';
            //        }
            //        return data;
            //    }

            {
                data: function (data, type, dataToSet) {
                    if (data.View === 1) {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkView" checked>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkView">';
                    }
                    return data;
                }

            },
            {
                data: function (data, type, dataToSet) {
                    if (data.Profile === 1) {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkProfile" checked>';
                    } else {
                        return '<input type="checkbox" class="icheckbox_minimal-blue chkProfile">';
                    }
                    return data;
                }

            }
        ]
    });
    UserPrivilegesList.destroy();
}

$("#get_users").change(function () {
    var selectedText = $(this).find("option:selected").text();
    var selectedValue = $(this).find("option:selected").val();

    GetAllUserPrivilegesList(selectedValue);
});

var checked_UserPriv;
function getCheckedUserPrivileges() {

    checked_UserPriv = new Array();
    var Add = "";
    var Edit = "";
    var Delete = "";
    var View = "";
    var Profile = "";
    $('#_tbl_UserPrivileges_Body_ tr').each(function (indexoftr, tr) {
        var chkAdd = $(this).find($('td')).find($('.chkAdd'));
        var chkEdit = $(this).find($('td')).find($('.chkEdit'));
        //var chkDelete = $(this).find($('td')).find($('.chkDelete'));
        var chkView = $(this).find($('td')).find($('.chkView'));
        var chkProfile = $(this).find($('td')).find($('.chkProfile'));
        var Priv_id = chkAdd.attr("data-priv_ID");
        var id = chkAdd.attr("data-id");

        if (chkAdd.is(':checked')) {
            Add = "1";
        }
        else {
            Add = "0";
        }
        if (chkEdit.is(':checked')) {
            Edit = "1";
        }
        else {
            Edit = "0";
        }
        //if (chkDelete.is(':checked')) {
        //    Delete = "1";
        //}
        //else {
        //    Delete = "0";
        //}
        if (chkView.is(':checked')) {
            View = "1";
        }
        else {
            View = "0";
        }
        if (chkProfile.is(':checked')) {
            Profile = "1";
        }
        else {
            Profile = "0";
        }
        //var data = { id: id, priv_ID: Priv_id, User_id: $("#get_users").val(), Add: Add, Edit: Edit, Delete: Delete }
        var data = { id: id, priv_ID: Priv_id, User_id: $("#get_users").val(), Add: Add, Edit: Edit, View: View, Profile: Profile }
        checked_UserPriv.push(data);
    });
    if (checked_UserPriv.length > 0) {
        UpdateUserPrivileges();
    } else {
        $('#_Error_Message_Display_').html("There are No Privileges selected");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Add_User_Privileges_Update_").click(function () {
    getCheckedUserPrivileges();
});

function UpdateUserPrivileges() {
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/UserPreviliges/UpdateUserPrivileges/',
        type: "POST",
        data: { __RequestVerificationToken: token, "UserPrivilegesData": JSON.stringify(checked_UserPriv) },
    }).done(function (resp) {
        $('#_Success_Message_Display_ > span').html("User Privileges Updated Successfully");
        $('#_Success_Message_Display_').slideDown("slow");
        UserPrivilegesList.ajax.reload(null, false);
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