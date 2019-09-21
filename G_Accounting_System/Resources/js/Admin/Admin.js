function Logout() {
    $('.IcmOverlaySpinner').show();

    $.ajax({
        url: "/Admin/Logout",
        data: JSON.stringify({

        }),
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $("#LoggedInUser_id").val("0");

    })
        .always(function () {
            //$('.IcmOverlaySpinner').hide();
            //window.location.href = '/Admin/Login';
        });
}

$(function () {
    MessageNotifications();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: "/Admin/getLoggedInUser_id",
        type: "POST",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        $("#LoggedInUser_id").val(resp.id);

    }).fail(function () {

    });
})
var id = new Array();
function MessageNotifications() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Admin/MessagesNotifications',
        type: "POST",
        data: { __RequestVerificationToken: token },

    }).done(function (resp) {
        var message = "";
        if (resp.Messages.length <= 1) {
            message = " Message";
        }
        else {
            message = "Messages"
        }
        $(".Notification_Text").html("You have " + resp.Messages.length + " " + message);
        if (resp.Messages.length != 0) {
            $(".Notification_Count").html(resp.Messages.length);
        }
        else {
            $(".Notification_Count").html('');
        }
        $(".MessageNotification").html('');
        var url = "";
        $.each(resp.Messages, function (i, item) {

            if (item.Receiver_id == $("#LoggedInUser_id").val()) {

                id.push(item.id);
                url = '/Messaging/Index/?id=' + item.Sender_id;
                var li = "<li>" +
                    "<span class='contacts-list-msg hidden'>" + item.Receiver_id + "</span>" +
                    "<a href=" + url + ">" +
                    "<div class='pull-left'>" +
                    "<img src='/Resources/img/user2-160x160.jpg') class='img-circle' alt='User Image'>" +
                    "</div>" +
                    "<h4>" + item.SenderName +
                    "<small><i class='fa fa-clock-o'></i>" + item.Date + item.Time + "</small>" +
                    "</h4>" +
                    "<p>" + item.strMessage + "</p>" +
                    "</a>" +
                    "</li >";

                $(".MessageNotification").append(li);

            }


        });

        var lii = "<a href='/Messaging/Index'>See All Messages</a>"
        $(".msgnotf").html(lii);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        //$('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$(function () {


})

$("#_Add_Change_Password_Save_").click(function () {
    ChangePasswordUpdate();
});

function ChangePasswordUpdate() {

    var newPass = $("input[name=new_password]").val();
    var confirmPass = $("input[name=confirm_password]").val();

    var url = '/Admin/ChangePasswordUpdate';
    if (newPass != confirmPass) {
        $('#_Error_Message_Display_ > span').html("Password does not match");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    } else {
        var ChangePassword_Data = {
            CurrentPassword: $("input[name=current_password]").val(),
            NewPassword: $("input[name=new_password]").val(),
        }
    }
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "ChangePasswordData": JSON.stringify(ChangePassword_Data) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

        if (resp.pFlag == "1") {
            $('#_Success_Message_Display_ > span').html(resp.pDesc);
            $('#_Success_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);

            $("input[name=new_password]").val("");
            $('input[name=current_password]').val("");
            $("input[name=confirm_password]").val("");

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

//setInterval(function () {
//    MessageNotifications();
//}, 1000);  