$('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
    checkboxClass: 'icheckbox_minimal-blue',
    radioClass: 'iradio_minimal-blue'
});

$('#_Add_New_User_Form_Save_').on('click', function () {
    InsertUpdateUser();
});

var Activity_Data = new Array();
function InsertUpdateUser() {
    $('#_Error_Message_Display_ > span').html("");
    if ($("input[name=users_email]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter email<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("input[name=users_password]").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter password<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=users_premises]").val() === "0") {
        $('#_Error_Message_Display_ > span').html("Please Select Premises<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=users_attached_profile]").val() === "0") {
        $('#_Error_Message_Display_ > span').html("Please Select Attached Profile<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else if ($("select[name=users_role]").val() === "0") {
        $('#_Error_Message_Display_ > span').html("Please Select Role<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        $('#_Add_New_User_Form_Save_').attr("disabled", true);
        var url = '/User/InsertUpdateUser';
        var user_data = {
            id: $("#userid").val(),
            email: $("input[name=users_email]").val(),
            attached_profile: $("select[name=users_attached_profile]").val(),
            password: $("input[name=users_password]").val(),
            status: $("select[name=users_status]").val(),
            Premises_id: $("select[name=users_premises]").val(),
            Role_id: $("select[name=users_role]").val(),
            //pao: $("input[name=users_pao]").is(':checked'),
            //paf: $("input[name=users_paf]").is(':checked'),
            //pas: $("input[name=users_pas]").is(':checked'),
            //pas_: $("input[name=users_pas_]").is(':checked'),
            //pav: $("input[name=users_pav]").is(':checked'),
            //pap: $("input[name=users_pap]").is(':checked'),
            //pac: $("input[name=users_pac]").is(':checked'),
            //pas__: $("input[name=users_pas__]").is(':checked'),
            //pae: $("input[name=users_pae]").is(':checked'),
            //pap_: $("input[name=users_pap_]").is(':checked'),
            //pai: $("input[name=users_pai]").is(':checked'),
            //pas___: $("input[name=users_pas___]").is(':checked'),
            //pau: $("input[name=users_pau]").is(':checked'),
            //puo: $("input[name=users_puo]").is(':checked'),
            //puf: $("input[name=users_puf]").is(':checked'),
            //pus: $("input[name=users_pus]").is(':checked'),
            //pus_: $("input[name=users_pus_]").is(':checked'),
            //puv: $("input[name=users_puv]").is(':checked'),
            //pup: $("input[name=users_pup]").is(':checked'),
            //puc: $("input[name=users_puc]").is(':checked'),
            //pus__: $("input[name=users_pus__]").is(':checked'),
            //pue: $("input[name=users_pue]").is(':checked'),
            //pup_: $("input[name=users_pup_]").is(':checked'),
            //pui: $("input[name=users_pui]").is(':checked'),
            //pus___: $("input[name=users_pus___]").is(':checked'),
            //puu: $("input[name=users_puu]").is(':checked'),
            //pdo: $("input[name=users_pdo]").is(':checked'),
            //pdf: $("input[name=users_pdf]").is(':checked'),
            //pds: $("input[name=users_pds]").is(':checked'),
            //pds_: $("input[name=users_pds_]").is(':checked'),
            //pdv: $("input[name=users_pdv]").is(':checked'),
            //pdp: $("input[name=users_pdp]").is(':checked'),
            //pdc: $("input[name=users_pdc]").is(':checked'),
            //pds__: $("input[name=users_pds__]").is(':checked'),
            //pde: $("input[name=users_pde]").is(':checked'),
            //pdp_: $("input[name=users_pdp_]").is(':checked'),
            //pdi: $("input[name=users_pdi]").is(':checked'),
            //pds___: $("input[name=users_pds___]").is(':checked'),
            //pdu: $("input[name=users_pdu]").is(':checked'),
            //pvo: $("input[name=users_pvo]").is(':checked'),
            //pvf: $("input[name=users_pvf]").is(':checked'),
            //pvs: $("input[name=users_pvs]").is(':checked'),
            //pvs_: $("input[name=users_pvs_]").is(':checked'),
            //pvv: $("input[name=users_pvv]").is(':checked'),
            //pvp: $("input[name=users_pvp]").is(':checked'),
            //pvc: $("input[name=users_pvc]").is(':checked'),
            //pvs__: $("input[name=users_pvs__]").is(':checked'),
            //pve: $("input[name=users_pve]").is(':checked'),
            //pvp_: $("input[name=users_pvp_]").is(':checked'),
            //pvi: $("input[name=users_pvi]").is(':checked'),
            //pvs___: $("input[name=users_pvs___]").is(':checked'),
            //pvu: $("input[name=users_pvu]").is(':checked'),
            //pvol: $("input[name=users_pvol]").is(':checked'),
            //pvfl: $("input[name=users_pvfl]").is(':checked'),
            //pvsl: $("input[name=users_pvsl]").is(':checked'),
            //pvsl_: $("input[name=users_pvsl_]").is(':checked'),
            //pvvl: $("input[name=users_pvvl]").is(':checked'),
            //pvpl: $("input[name=users_pvpl]").is(':checked'),
            //pvcl: $("input[name=users_pvcl]").is(':checked'),
            //pvsl__: $("input[name=users_pvsl__]").is(':checked'),
            //pvel: $("input[name=users_pvel]").is(':checked'),
            //pvpl_: $("input[name=users_pvpl_]").is(':checked'),
            //pvil: $("input[name=users_pvil]").is(':checked'),
            //pvsl___: $("input[name=users_pvsl___]").is(':checked'),
            //pvul: $("input[name=users_pvul]").is(':checked'),
        }

        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, "UserData": JSON.stringify(user_data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == "1") {
                $('#_Add_New_User_Form_Save_').attr("disabled", false);
                $('#_Add_New_User_btn_').attr("disabled", false);
                
                var User_id = resp.pUserid_Out;
                $('#_Success_Message_Display_ > span').html(resp.pDesc);
                $('._Add_New_User_Form_').slideUp("slow");
                $('#_Success_Message_Display_').slideDown("slow");
                $('#_Add_New_User_btn_').attr("disabled", false);
                UsersList.ajax.reload(null, false);
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
                $("#userid").val("0");
            } else {
                $('#_Add_New_User_Form_Save_').attr("disabled", false);
                $('#_Error_Message_Display_ > span').html(resp.pDesc);
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
                $("#userid").val("0");
            }
            }).fail(function () {
                $('#_Add_New_User_Form_Save_').attr("disabled", false);
            $('#_Error_Message_Display_ > span').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        });
    }
};

$('._Add_New_User_Form_Remover_').on('click', function () {
    UserFormRemover();
});

function UserFormRemover() {
    $('._Add_New_User_Form_').slideUp("slow");
    $('._Add_New_User_Form_').html('');
    $('#_Add_New_User_btn_').attr("disabled", false);
}

$('input').on('ifClicked', function (event) {
    if ($(this).attr("name") != 'users_sal') {
        if ($(this).is(':checked')) {
            $("input[name=users_sal]").iCheck('uncheck');
        }
    }
});

$('input[name=users_sal]').on('ifClicked', function (event) {
    if ($(this).is(':checked') == false) {
        $("input[name=users_pao]").iCheck('check');
        $("input[name=users_paf]").iCheck('check');
        $("input[name=users_pas]").iCheck('check');
        $("input[name=users_pas_]").iCheck('check');
        $("input[name=users_pav]").iCheck('check');
        $("input[name=users_pap]").iCheck('check');
        $("input[name=users_pac]").iCheck('check');
        $("input[name=users_pas__]").iCheck('check');
        $("input[name=users_pae]").iCheck('check');
        $("input[name=users_pap_]").iCheck('check');
        $("input[name=users_pai]").iCheck('check');
        $("input[name=users_pas___]").iCheck('check');
        $("input[name=users_pau]").iCheck('check');
        $("input[name=users_puo]").iCheck('check');
        $("input[name=users_puf]").iCheck('check');
        $("input[name=users_pus]").iCheck('check');
        $("input[name=users_pus_]").iCheck('check');
        $("input[name=users_puv]").iCheck('check');
        $("input[name=users_pup]").iCheck('check');
        $("input[name=users_puc]").iCheck('check');
        $("input[name=users_pus__]").iCheck('check');
        $("input[name=users_pue]").iCheck('check');
        $("input[name=users_pup_]").iCheck('check');
        $("input[name=users_pui]").iCheck('check');
        $("input[name=users_pus___]").iCheck('check');
        $("input[name=users_puu]").iCheck('check');
        $("input[name=users_pdo]").iCheck('check');
        $("input[name=users_pdf]").iCheck('check');
        $("input[name=users_pds]").iCheck('check');
        $("input[name=users_pds_]").iCheck('check');
        $("input[name=users_pdv]").iCheck('check');
        $("input[name=users_pdp]").iCheck('check');
        $("input[name=users_pdc]").iCheck('check');
        $("input[name=users_pds__]").iCheck('check');
        $("input[name=users_pde]").iCheck('check');
        $("input[name=users_pdp_]").iCheck('check');
        $("input[name=users_pdi]").iCheck('check');
        $("input[name=users_pds___]").iCheck('check');
        $("input[name=users_pdu]").iCheck('check');
        $("input[name=users_pvo]").iCheck('check');
        $("input[name=users_pvf]").iCheck('check');
        $("input[name=users_pvs]").iCheck('check');
        $("input[name=users_pvs_]").iCheck('check');
        $("input[name=users_pvv]").iCheck('check');
        $("input[name=users_pvp]").iCheck('check');
        $("input[name=users_pvc]").iCheck('check');
        $("input[name=users_pvs__]").iCheck('check');
        $("input[name=users_pve]").iCheck('check');
        $("input[name=users_pvp_]").iCheck('check');
        $("input[name=users_pvi]").iCheck('check');
        $("input[name=users_pvs___]").iCheck('check');
        $("input[name=users_pvu]").iCheck('check');
        $("input[name=users_pvol]").iCheck('check');
        $("input[name=users_pvfl]").iCheck('check');
        $("input[name=users_pvsl]").iCheck('check');
        $("input[name=users_pvsl_]").iCheck('check');
        $("input[name=users_pvvl]").iCheck('check');
        $("input[name=users_pvpl]").iCheck('check');
        $("input[name=users_pvcl]").iCheck('check');
        $("input[name=users_pvsl__]").iCheck('check');
        $("input[name=users_pvel]").iCheck('check');
        $("input[name=users_pvpl_]").iCheck('check');
        $("input[name=users_pvil]").iCheck('check');
        $("input[name=users_pvsl___]").iCheck('check');
        $("input[name=users_pvul]").iCheck('check');
    } else {
        $("input[name=users_pao]").iCheck('uncheck');
        $("input[name=users_paf]").iCheck('uncheck');
        $("input[name=users_pas]").iCheck('uncheck');
        $("input[name=users_pas_]").iCheck('uncheck');
        $("input[name=users_pav]").iCheck('uncheck');
        $("input[name=users_pap]").iCheck('uncheck');
        $("input[name=users_pac]").iCheck('uncheck');
        $("input[name=users_pas__]").iCheck('uncheck');
        $("input[name=users_pae]").iCheck('uncheck');
        $("input[name=users_pap_]").iCheck('uncheck');
        $("input[name=users_pai]").iCheck('uncheck');
        $("input[name=users_pas___]").iCheck('uncheck');
        $("input[name=users_pau]").iCheck('uncheck');
        $("input[name=users_puo]").iCheck('uncheck');
        $("input[name=users_puf]").iCheck('uncheck');
        $("input[name=users_pus]").iCheck('uncheck');
        $("input[name=users_pus_]").iCheck('uncheck');
        $("input[name=users_puv]").iCheck('uncheck');
        $("input[name=users_pup]").iCheck('uncheck');
        $("input[name=users_puc]").iCheck('uncheck');
        $("input[name=users_pus__]").iCheck('uncheck');
        $("input[name=users_pue]").iCheck('uncheck');
        $("input[name=users_pup_]").iCheck('uncheck');
        $("input[name=users_pui]").iCheck('uncheck');
        $("input[name=users_pus___]").iCheck('uncheck');
        $("input[name=users_puu]").iCheck('uncheck');
        $("input[name=users_pdo]").iCheck('uncheck');
        $("input[name=users_pdf]").iCheck('uncheck');
        $("input[name=users_pds]").iCheck('uncheck');
        $("input[name=users_pds_]").iCheck('uncheck');
        $("input[name=users_pdv]").iCheck('uncheck');
        $("input[name=users_pdp]").iCheck('uncheck');
        $("input[name=users_pdc]").iCheck('uncheck');
        $("input[name=users_pds__]").iCheck('uncheck');
        $("input[name=users_pde]").iCheck('uncheck');
        $("input[name=users_pdp_]").iCheck('uncheck');
        $("input[name=users_pdi]").iCheck('uncheck');
        $("input[name=users_pds___]").iCheck('uncheck');
        $("input[name=users_pdu]").iCheck('uncheck');
        $("input[name=users_pvo]").iCheck('uncheck');
        $("input[name=users_pvf]").iCheck('uncheck');
        $("input[name=users_pvs]").iCheck('uncheck');
        $("input[name=users_pvs_]").iCheck('uncheck');
        $("input[name=users_pvv]").iCheck('uncheck');
        $("input[name=users_pvp]").iCheck('uncheck');
        $("input[name=users_pvc]").iCheck('uncheck');
        $("input[name=users_pvs__]").iCheck('uncheck');
        $("input[name=users_pve]").iCheck('uncheck');
        $("input[name=users_pvp_]").iCheck('uncheck');
        $("input[name=users_pvi]").iCheck('uncheck');
        $("input[name=users_pvs___]").iCheck('uncheck');
        $("input[name=users_pvu]").iCheck('uncheck');
        $("input[name=users_pvol]").iCheck('uncheck');
        $("input[name=users_pvfl]").iCheck('uncheck');
        $("input[name=users_pvsl]").iCheck('uncheck');
        $("input[name=users_pvsl_]").iCheck('uncheck');
        $("input[name=users_pvvl]").iCheck('uncheck');
        $("input[name=users_pvpl]").iCheck('uncheck');
        $("input[name=users_pvcl]").iCheck('uncheck');
        $("input[name=users_pvsl__]").iCheck('uncheck');
        $("input[name=users_pvel]").iCheck('uncheck');
        $("input[name=users_pvpl_]").iCheck('uncheck');
        $("input[name=users_pvil]").iCheck('uncheck');
        $("input[name=users_pvsl___]").iCheck('uncheck');
        $("input[name=users_pvul]").iCheck('uncheck');
    }
});

//function RolesDropdown() {
//    var token = $('[name=__RequestVerificationToken]').val();
//    $('#get_users_role').empty();
//    $('#get_users_role').append('<option value="0" selected="selected">Select Role</option>');

//    $.ajax({
//        url: '/User/RolesDropdown',
//        data: { __RequestVerificationToken: token },
//        type: "POST",
//        dataType: "json",
//        datatype: 'json',
//        ContentType: 'application/json; charset=utf-8'
//    }).done(function (data) {
//        $response = JSON.parse(data.Response);
//        var options = $("#get_users_role");
//        var rowCount = $response.length;

//        for (var i = 0; i < rowCount; i++) {
//            $("#get_users_role")
//                .append($('<option>', { value: $response[i].id })
//                    .text($response[i].name));

//        }
//        $("#get_users_role").val("0");


//    })
//        .always(function () {

//        });
//}

//jQuery(document).keydown(function (event) {
//    // If Control or Command key is pressed and the S key is pressed
//    // run save function. 83 is the key code for S.

//    if (event.which == 13) {
//        // Save Function
//        $('#_Add_New_User_Form_Save_').click();
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
//        $('._Add_New_User_Form_Remover_').click();
//        event.preventDefault();

//        return false;
//    }
//}
//);