$("#_Add_New_User_btn_").click(function () {
    $(this).attr("disabled", true);
    AddUserForm();
    RolesDropdown();
});

function AddUserForm() {

    $('._Add_New_User_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/User/Add',
        type: "POST",
        data: { __RequestVerificationToken: token }
    }).done(function (resp) {
        $('._Add_New_User_Form_').append(resp);
        $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
        $('._Add_New_User_Form_').slideDown("slow");
        $('input[name=users_email]').focus();
        $('.select2').select2();
        $('#_Add_New_User_btn_').attr("disabled", true);
        $('#_Add_New_User_Form_Save_').attr("disabled", false);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/user/index/" || url == "/user/index") {
        GetAllOfUser(null);
    } else if (url.includes("/user/profile/")) {
        GetAUser(url);
    } else if (url.includes("/user?id=")) {
        GetAllOfUser(window.location.search.toString().toLowerCase());
    }
})

function GetAllOfUser(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    UsersList = $('#Users_list').DataTable({
        "bServerSide": true,
        "language": {
            "emptyTable": "No users available"
        },
        "sAjaxSource": "/User/GetAllUsers",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, StartDate: $('input[name=UserStartDate]').val(), EndDate: $('input[name=UserEndDate]').val(), Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[35].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/user/profile/" + data.id + "'>" + data.email + "</a>";

                }
            },
            { data: "password" },
            { data: "attached_profile" },
            { data: "PremisesName" },
            { data: "status" },
            {
                data: function (data, type, dataToSet) {
                    if (data.Delete_Status === 'Requested') {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons hidden EditUser' onclick='EditUser(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons hidden' onclick='UpdatepUser(" + data.id + ")' title='Visibility'></span>";
                    } else {
                        return "<span class='fa fa-pencil-square-o table_list_ops_icons EditUser' onclick='EditUser(" + data.id + ")' title='Edit'></span><span class='fa fa-eye table_list_ops_icons' onclick='UpdatepUser(" + data.id + ")' title='Visibility'></span>";
                    }
                }
            }
            //{
            //    data: function (data, type, dataToSet) {

            //        if (data.Delete_Status === 'Requested') {
            //            return '<input type="checkbox" class="icheckbox_minimal-blue hidden chkUserDel" data_value_userid=' + data.id + ' data_value_email=' + data.email + '>';
            //        } else {
            //            return '<input type="checkbox" class="icheckbox_minimal-blue chkUserDel" data_value_userid=' + data.id + ' data_value_email=' + data.email + '>';
            //        }
            //        return data;
            //    }

            //},
        ]
    });
}

var Activity_Data = new Array();
function UpdatepUser(id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/User/Updatep/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != null) {
            $('#_Error_Message_Display_ > span').html('');
            UsersList.ajax.reload(null, false);
            $('#_Error_Message_Display_').slideUp("slow");
            $('#_Success_Message_Display_ > span').html('User Profile Visibility Updated Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
        } else {
            $('#_Error_Message_Display_ > span').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function GetAUser(url) {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $("input[name=users_id]").val(resp.id);
        $("#cp__user_status").html('');
        $("#cp__user_status").html(resp.status);
        $("#cp__user_email").html('');
        $("#cp__user_name").html(resp.attached_profile);
        $("#cp__user_email").html('');
        $("#cp__user_email").html(resp.email);
        $("#cp__user_Company").html('');
        $("#cp__user_Company").html(resp.CompanyName);
        $("#cp__user_Designation").html('');
        $("#cp__user_Designation").html(resp.Designation);
        $("#cp__user_Landline").html('');
        $("#cp__user_Landline").html(resp.Landline);
        $("#cp__user_Mobile").html('');
        $("#cp__user_Mobile").html(resp.Mobile);
        $("#cp__user_Contact_email").html('');
        $("#cp__user_Contact_email").html(resp.ContactEmail);
        $("#cp__user_Address").html('');
        $("#cp__user_Address").html(resp.Address);
        $("#cp__user_Landline").html('');
        $("#cp__user_Landline").html(resp.AddressLandline);
        $("#cp__user_user_City").html('');
        $("#cp__user_user_City").html(resp.CityName);
        $("#cp__user_user_Country").html('');
        $("#cp__user_user_Country").html(resp.CountryName);
        $("#cp__customer_bank_account_number").html('');
        $("#cp__customer_bank_account_number").html(resp.BankAccountNumber);
        $("#cp__user_premises_name").html('');
        $("#cp__user_premises_name").html(resp.PremisesName);
        $("#cp__user_premises_phone").html('');
        $("#cp__user_premises_phone").html(resp.PremisesPhone);
        $("#cp__user_premises_address").html('');
        $("#cp__user_premises_address").html(resp.PremisesAddress);
        $("#cp__user_premises_city").html('');
        $("#cp__user_premises_city").html(resp.PremisesCityName);
        $("#cp__user_premises_country").html('');
        $("#cp__user_premises_country").html(resp.PremisesCountry);

        var ActivityType_id = resp.id;
        var ActivityType = "User";
        Activities(ActivityType_id, ActivityType);

        $('#_Error_Message_Display_ > span').html('');
        $('#_Error_Message_Display_').slideUp("slow");

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function DelUser(id) {
    checked_Users = new Array();
    var id;
    var name;
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url.includes("/user/profile/")) {
        id = $("input[name=users_id]").val();
        email = $("#cp__user_email").text();
    }
    var data = { id: id, email: email, Delete_Status: "Requested" }
    checked_Users.push(data);

    SendRequestToDelUsers();
}

$('#_Error_Message_Display_Btn_').on('click', function () {
    $('#_Error_Message_Display_').slideUp("slow");
    $('#_Error_Message_Display_ > span').html('');
});


function EditUser(id) {
    RolesDropdown();
    $('._Add_New_User_Form_').html('');

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/User/Update/' + id,
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        if (resp != false) {
            $('._Add_New_User_Form_').append(resp);
            $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
            $('._Add_New_User_Form_').slideDown("slow");
            $('#_Add_New_User_Form_Save_').html('Update');
            $('#_Add_New_User_Form_Save_').attr('id', '_Update_Existing_User_Form_Save_');
            $("#userid").val(id);
            $('input[name=users_email]').focus();
            $("#_Add_New_User_btn_").attr("disabled", false);
            $('.select2').select2();
            RolesDropdown();
        }
        else {
            $('#_Error_Message_Display_ > span').html('You are Not Authorized to perform this action');
            $('#_Error_Message_Display_').slideDown("slow");
            $('._Add_New_User_Form_').slideUp("slow");
            $('._Add_New_User_Form_').html("");
            $("#_Add_New_User_btn_").attr("disabled", false);
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

//$('#_Add_New_User_Form_Save_').on('click', function () {
//    if ($(this).attr('id') == '_Update_Existing_User_Form_Save_') {
//        UpdateUser();
//    } else {
//        AddUser();
//    }
//});

$("input[name=UserStartDate]").change(function () {
    UsersList.ajax.reload(null, false);
});

$("input[name=UserEndDate]").change(function () {
    UsersList.ajax.reload(null, false);
});

var checked_Users;
function getCheckedUserstoDel() {
    var data_value_userid = "";
    var data_value_email = "";

    checked_Users = new Array();

    $('#_tbl_Users_Body_ tr').each(function (indexoftr, tr) {
        if ($(this).find($('td')).find($('.chkUserDel')).is(':checked')) {
            data_value_userid = $(this).find($('td')).find($('.chkUserDel')).attr('data_value_userid');
            data_value_email = $(this).find($('td')).find($('.chkUserDel')).attr('data_value_email');
            var data = { id: data_value_userid, email: data_value_email, Delete_Status: "Requested" }
            checked_Users.push(data);
        }
    });
    if (checked_Users.length > 0) {
        SendRequestToDelUsers();
    } else {
        $('#_Error_Message_Display_').html("There are No Users to Delete");
        $('#_Error_Message_Display_').slideDown();
    }
}

$("#_Send_request_To_DeleteUsers").click(function () {
    getCheckedUserstoDel();
});

function SendRequestToDelUsers() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/User/SendRequestToDelUsers/',
        type: "POST",
        data: {
            __RequestVerificationToken: token,
            "DeleteUserData": JSON.stringify(checked_Users)
        },
    }).done(function (resp) {
        if (resp.length == 0) {
            $('#_Success_Message_Display_ > span').html('Request Sent Successfully.');
            $('#_Success_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
            UsersList.ajax.reload(null, false);
            setTimeout(function () { $('#_Success_Message_Display_').slideUp("slow"); }, 5000);
        }
        else {
            $('#_Error_Message_Display_ > span').html('Network Error/Request sending failure ');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        }

    }).fail(function () {

    });
}

$(".ListUserFilter").click(function () {
    UsersList.destroy();
    var filtertype = $(this).text();
    GetAllOfUser(filtertype);
});