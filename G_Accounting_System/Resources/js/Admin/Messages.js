$(function () {
    //Socketsc();
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    //if (url == "/messaging/index/" || url == "/messaging/index") {
    //    //UsersList();
    //    Messages();
    //}
    getAllUsers();
    if (url.includes("/?id=")) {

        $('#receiver_id').val(window.location.search.toString().toLowerCase().split("?id=")[1]);
        var url = "/Messaging/Index/?id=" + window.location.search.toString().toLowerCase().split("?id=")[1];
        UpdateStatus(window.location.search.toString().toLowerCase().split("?id=")[1]);
        Messages(window.location.search.toString().toLowerCase().split("?id=")[1]);

    }



})

function Socketsc() {
    var ws;
    ws = new WebSocket("ws://localhost:65097/MyWebSocketHandler.ashx");
    ws.onopen = function () {

    };
    ws.onmessage = function (evt) {
        var jsonString = evt.data;
        var response = JSON.parse(jsonString);
        var responseuserId = response.userId;

        if (response.type == "chitchat") {
            if (response.Receiver_id != $('#receiver_id').val()) {
                var receiver = "<div class='direct-chat-msg right'>" +
                    "<div class='direct-chat-info clearfix'>" +
                    "<span class='direct-chat-name pull-right'>" + response.ReceiverName + "</span>" +
                    //"<span class='direct-chat-timestamp pull-left'>" + item.Date + " " + item.Time + "</span>" +
                    "</div>" +
                    "<img class='direct-chat-img' src='/Resources/dist/img/usericon.png' alt='message user image'>" +
                    "<div class='direct-chat-text'>" + response.message + "</div>" +
                    "</div>" +
                    "</br>";

                $('.chatCommentHeight').append(receiver);

            }
            else {
                var sender = "<div class='direct-chat-msg sender'>" +
                    "<div class='direct-chat-info clearfix'>" +
                    "<span class='direct-chat-name pull-left'>" + response.SenderName + "</span>" +
                    //"<span class='direct-chat-timestamp pull-right'>" + item.Date + " " + item.Time + "</span>" +
                    "</div>" +
                    "<img class='direct-chat-img' src='/Resources/dist/img/usericon.png' alt='message user image'>" +
                    "<div class='direct-chat-text'>" + response.message + "</div>" +
                    "</div>" +
                    "</br>";

                $('.chatCommentHeight').append(sender);

            }
            var h = $(".chatCommentHeight").get(0).scrollHeight;
            $(".chatCommentHeight").animate({ scrollTop: h }, 1000);
        }
        else if (response.type == "warning") {
            outputText = "WARN: " + response.message;
        }
        else if (response.type == "error") {
            outputText = responseuserId + "got some error";
        }
    };
    ws.onerror = function (evt) {

    };
    ws.onclose = function () {

    };

    $(".SendMessage").click(function () {
        if (ws.readyState == WebSocket.OPEN) {
            var obj = {
                Sender_id: $("#userID").text(),
                Receiver_id: $('#receiver_id').val(),
                strMessage: $("input[name=message]").val()
            };
            ws.send(JSON.stringify(obj));
        }
    });
}

function getAllUsers() {
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Messaging/getAllUsers',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $("#_tbl_MailBox_Body_").empty();
        $(".MainBoxTitle").text("Trash");
        if (resp.length != 0) {
            $("#cp__user_name").text(resp.CurrentUser);
            $("#cp__user_email").text(resp.CurrentUserEmail)
            $.each(resp.Users, function (i, item) {
                var li = "<li class='userlist_selectuser'>" +
                    "<a href='/Messaging/Index/?id=" + item.id + "'>" +
                    "<img class='contacts-list-img' src='/Resources/dist/img/usericon.png' alt='User Image'>" +
                    "<span class='contacts-list-msg hidden' id='select_user_id'>" + item.id + "</span>" +
                    "<div class='contacts-list-info' style='color:Green'>" +
                    "<span class='contacts-list-name' id='contacts-list-name'>" + item.attached_profile + "</span>" +
                    "<span class='contacts-list-msg'>" + item.email + "</span>" +
                    "</div>" +
                    "</a>" +
                    "</li>";


                $(".AllUserlist").append(li);
            });
        }
        else {
            $('#_Success_Message_Display_ > span').html("Trash is empty");
            $('#_Success_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
        }
        $('.IcmOverlaySpinner').hide();
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$(".userlist_selectuser").click(function () {
    select_user_id = "";
    if ($(this).class('active')) {
        $(this).addClass("active");
    } else {
        $(this).addClass("active");
    }

    var Receiver_id = $(this).find('#select_user_id').html();
    $('#receiver_id').val(Receiver_id);

    //$('.box-title').text($(this).find('.contacts-list-name').text());
    //Messages(Receiver_id);
    //var ReceiverName = $(this).find('#contacts-list-name').html();
    // $('.box-title').html(ReceiverName);
});

function Messages(Receiver_id) {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Messaging/Messages',
        type: "POST",
        async: false,
        data: { __RequestVerificationToken: token, Receiver_id },

    }).done(function (resp) {
        $('.chatCommentHeight').html('');
        console.log(resp);
        $(".headtitle").html(resp.ReceiverName.Name);
        $.each(resp.Messages, function (i, item) {

            if (item.Receiver_id != $('#receiver_id').val()) {
                var receiver = "<div class='direct-chat-msg right'>" +
                    "<div class='direct-chat-info clearfix'>" +
                    "<span class='direct-chat-name pull-right'>" + item.ReceiverName + "</span>" +
                    "<span class='direct-chat-timestamp pull-left'>" + item.Date + " " + item.Time + "</span>" +
                    "</div>" +
                    "<img class='direct-chat-img' src='/Resources/dist/img/usericon.png' alt='message user image'>" +
                    "<div class='direct-chat-text'>" + item.strMessage + "</div>" +
                    "</div>" +
                    "</br>";

                $('.chatCommentHeight').append(receiver);
            }
            else {
                var sender = "<div class='direct-chat-msg sender'>" +
                    "<div class='direct-chat-info clearfix'>" +
                    "<span class='direct-chat-name pull-left'>" + item.SenderName + "</span>" +
                    "<span class='direct-chat-timestamp pull-right'>" + item.Date + " " + item.Time + "</span>" +
                    "</div>" +
                    "<img class='direct-chat-img' src='/Resources/dist/img/usericon.png' alt='message user image'>" +
                    "<div class='direct-chat-text'>" + item.strMessage + "</div>" +
                    "</div>" +
                    "</br>";

                $('.chatCommentHeight').append(sender);
            }
        });
        setTimeout(function () {
            Receiver_id = $('#receiver_id').val();


        }, 500);

        if ((window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase()).includes("messaging/index/?id=")) {

            var h = $(".chatCommentHeight").get(0).scrollHeight;
            $(".chatCommentHeight").animate({ scrollTop: h }, 1000);
        }

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        //$('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');

    }).always(function () {

    });
}

$(".SendMessage").click(function () {
    SendMessage();
});

function SendMessage() {

    if ($('#receiver_id').val() === "" || $('#receiver_id').val() === null) {
        $('#_Error_Message_Display_ > span').html("Please Enter a receiver to send message<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    var Message = {
        Receiver_id: $('#receiver_id').val(),
        strMessage: $("input[name=message]").val(),
        Status: "Unread"
    }

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Messaging/SendMessage',
        type: "POST",
        data: { __RequestVerificationToken: token, "Message": JSON.stringify(Message) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        Receiver_id = $('#receiver_id').val();
        if (resp.pFlag == "1") {

            Messages(Receiver_id);
            $("input[name=message]").val();
            //var sender = "<div class='direct-chat-msg sender'>" +
            //    "<div class='direct-chat-info clearfix'>" +
            //    "<span class='direct-chat-name pull-left'>Sender Name</span>" +
            //    "<span class='direct-chat-timestamp pull-right'>Date</span>" +
            //    "</div>" +
            //    "<img class='direct-chat-img' src='/Resources/dist/img/usericon.png' alt='message user image'>" +
            //    "<div class='direct-chat-text'>Sender Message on send</div>" +
            //    "</div>" +
            //    "</br>";

            //$('.chatCommentHeight').append(sender);

        }
        else {

        }
        $("input[name=message]").val('');

    }).fail(function () {
        alert("post error 0");
        $('input[name=brandid]').val("0");
    });
};

$('input[name=message]').keypress(function (e) {
    var key = e.which;
    if (key == 13)  // the enter key code
    {
        $('.SendMessage').click();
        return false;
    }
});

function UpdateStatus(Receiver_id) {

    var UpdateStatus = {
        Receiver_id: Receiver_id,
        Status: "Read"
    }

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Messaging/UpdateStatus',
        type: "POST",
        data: { __RequestVerificationToken: token, "UpdateStatus": JSON.stringify(UpdateStatus) },
        datatype: 'json',
        async: false,
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.pFlag == "1") {

        }
        else {
            ;
        }


    }).fail(function () {
        alert("post error 0");
        $('input[name=brandid]').val("0");
    });
};

$("#_DeleteChat_").click(function () {
    var Receiver_id = $('#receiver_id').val();
    DeleteChat(Receiver_id)
});

function DeleteChat(Receiver_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    var Chatdata = {
        Receiver_id: Receiver_id
    }
    $.ajax({
        url: '/Messaging/DeleteChat',
        type: "POST",
        data: { __RequestVerificationToken: token, "ChatData": JSON.stringify(Chatdata) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.length != 0) {
            $('#_Success_Message_Display_ > span').html(resp.pDesc);
            $('#_Success_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Success_Message_Display_').slideUp("slow");
            }, 5000);
            Messages(Receiver_id);
        }
        else {
            $('#_Error_Message_Display_ > span').html("Trash is empty");
            $('#_Error_Message_Display_').slideDown("slow");
            setTimeout(function () {
                $('#_Error_Message_Display_').slideUp("slow");
            }, 5000);
        }
        $('.IcmOverlaySpinner').hide();
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$('.searchUser').on('keyup', function ($event) {
    var inputValue = $(".searchUser").val().toLowerCase();
    $(".userlist_selectuser").each(function () {
        if ($(this).html().toLowerCase().indexOf(inputValue) == -1) {
            $(this).hide();
        }
        else {
            $(this).show();
        }
    });
});

//setInterval(function () {
//    Receiver_id = $('#receiver_id').val();
//    Messages(Receiver_id);
//    UpdateStatus();
//}, 1000);


jQuery(document).keydown(function (event) {
    // If Control or Command key is pressed and the S key is pressed
    // run save function. 13 is the key code for Enter.

    if (event.which == 13) {
        // Save Function
        $('.SendMessage').click();
        event.preventDefault();

        return false;
    }
}
);