$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if (url == "/mailbox/index/" || url == "/mailbox/index") {
        //GetAllFolders();
        EmailsCount();
        MailboxMain("Inbox");
    }
    else if (url.includes("/mailbox/index#")) {
        ReadMail();
    }

})

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

function EmailsCount() {

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Mailbox/EmailsCount',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        $(".InboxCount").text(resp.InboxCount);
        $(".SentCount").text(resp.SentCount);
        $(".DraftsCount").text(resp.DraftsCount);
        $(".JunkCount").text(resp.JunkCount);
        $(".TrashCount").text(resp.TrashCount);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function Compose() {
    $('.IcmOverlaySpinner').show();
    //$('._Mail_Box_Content_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Mailbox/Compose',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        $('._Mail_Box_Content_').html(resp);
        $("#compose-textarea").wysihtml5();
        $('.IcmOverlaySpinner').hide();
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function GetAllFolders() {
    $('.IcmOverlaySpinner').show();
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Mailbox/GetAllFolders',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        $('.IcmOverlaySpinner').hide();
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function MailboxMain(foldername) {
    $('.IcmOverlaySpinner').show();
    $('._Mail_Box_Content_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: '/Mailbox/MailboxMain',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        $('._Mail_Box_Content_').append(resp);
        if (foldername == "Inbox") {
            $(".MainBoxTitle").text("Inbox");
            Inbox();
        } else if (foldername == "Sent") {
            $(".MainBoxTitle").html("Sent");
            Sent();
        } else if (foldername == "Drafts") {
            $(".MainBoxTitle").html("Drafts");
            Drafts();
        } else if (foldername == "Junk") {
            $(".MainBoxTitle").html("Junk");
            Junk();
        } else if (foldername == "Trash") {
            $(".MainBoxTitle").html("Trash");
            Trash();
        }
        $("#_Folder_Name_").val(foldername);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

function ReadMail(idOfEmail, validEmailCheck, getValidityOfEmail) {
    $('.IcmOverlaySpinner').show();
    var token = $('[name=__RequestVerificationToken]').val();
    var emailObject = { Id: idOfEmail, IsValid: validEmailCheck, Validity: getValidityOfEmail };
    $.ajax({
        url: '/Mailbox/ReadMail',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {

        $('._Mail_Box_Content_').html(resp);
        $('#Id').val(idOfEmail);
        $('#isValid').val(validEmailCheck);
        $('#Validity').val(getValidityOfEmail);
        var name = $("#_Folder_Name_").val();
        var url = "";
        if (name === "Inbox") {
            url = "/Mailbox/getSingleInboxEmail";
            getSingleEmail(emailObject, url);
        } else if (name === "Sent") {
            url = "/Mailbox/getSingleSentEmail";
            getSingleEmail(emailObject, url);
        } else if (name === "Drafts") {
            url = "/Mailbox/getSingleDraftEmail";
            getSingleEmail(emailObject, url);
        } else if (name === "Junk") {
            url = "/Mailbox/getSingleJunkEmail";
            getSingleEmail(emailObject, url);
        } else if (name === "Trash") {
            url = "/Mailbox/getSingleTrashEmail";
            getSingleEmail(emailObject, url);
        }
        $('.IcmOverlaySpinner').hide();
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$(".Folder").click(function () {
    $(this).addClass("active");
    var foldername = $(this).find($(".FolderName")).text();
    $("#_Folder_Name_").val(foldername);
    $("._Compose_Mail_").html("Compose");
    MailboxMain(foldername);

});


function Inbox() {
    $('.IcmOverlaySpinner').show();
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Mailbox/Inbox',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        if (resp.length != 0) {
            $.each(resp, function (i, item) {
                $(".MainBoxTitle").text("Inbox");
                var row = "<tr >" +
                    //"<td><div class='icheckbox_flat-blue' aria-checked='false' aria-disabled='false' style='position: relative;'><input type='checkbox'><ins class='iCheck-helper'></ins></div></td>" +
                    //"<td class='mailbox-star'><a href='#' onClick='markStar(" + item.mail_id.Id + ',' + item.mail_id.IsValid + ',' + item.mail_id.Validity + ")'><i class='fa fa-star text-yellow'></i></a></td>" +
                    "<td class='mailbox-name'><a href='#' onClick='ReadMail(" + item.mail_id.Id + ',' + item.mail_id.IsValid + ',' + item.mail_id.Validity + ")'>" + item.EmailFrom + "</a></td>" +
                    "<td class='mailbox-subject'>" +
                    "<b>" + item.Subject +
                    "</td>" +
                    //"<td class='mailbox - attachment'></td>" +
                    "<td class='mailbox - date'>" + item.Date + "</td>" +
                    "</tr>";

                $("#_tbl_MailBox_Body_").append(row);
                $('._tbl_MailBox_').html($(".mailbox-messages").html());


            });
        }
        else {
            $('#_Success_Message_Display_ > span').html("Inbox is empty");
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
        $('.IcmOverlaySpinner').hide();
    });

};

function Sent() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Mailbox/Sent',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $("#_tbl_MailBox_Body_").empty();
        $(".MainBoxTitle").text("Sent");
        if (resp.length != 0) {
            $.each(resp, function (i, item) {
                var row = "<tr >" +
                    //"<td><div class='icheckbox_flat-blue' aria-checked='false' aria-disabled='false' style='position: relative;'><input type='checkbox'><ins class='iCheck-helper'></ins></div></td>" +
                    //"<td class='mailbox-star'><a href='#'><i class='fa fa-star text-yellow'></i></a></td>" +
                    "<td class='mailbox-name'><a href='#' onClick='ReadMail(" + item.mail_id.Id + ',' + item.mail_id.IsValid + ',' + item.mail_id.Validity + ")'>" + item.EmailFrom + "</a></td>" +
                    "<td class='mailbox-subject'>" +
                    "<b>" + item.Subject +
                    "</td>" +
                    //"<td class='mailbox - attachment'></td>" +
                    "<td class='mailbox - date'>" + item.Date + "</td>" +
                    "</tr>";

                $("#_tbl_MailBox_Body_").append(row);
                $('._tbl_MailBox_').html($(".mailbox-messages").html());
            });
        }
        else {
            $('#_Success_Message_Display_ > span').html("Sent is empty");
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

};

function Drafts() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Mailbox/Drafts',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $("#_tbl_MailBox_Body_").empty();
        $(".MainBoxTitle").text("Drafts");
        if (resp.length != 0) {
            $.each(resp, function (i, item) {
                var row = "<tr >" +
                    //"<td><div class='icheckbox_flat-blue' aria-checked='false' aria-disabled='false' style='position: relative;'><input type='checkbox'><ins class='iCheck-helper'></ins></div></td>" +
                    //"<td class='mailbox-star'><a href='#'><i class='fa fa-star text-yellow'></i></a></td>" +
                    "<td class='mailbox-name'><a href='#' onClick='ReadMail(" + item.mail_id.Id + ',' + item.mail_id.IsValid + ',' + item.mail_id.Validity + ")'>" + item.EmailFrom + "</a></td>" +
                    "<td class='mailbox-subject'>" +
                    "<b>" + item.Subject +
                    "</td>" +
                    //"<td class='mailbox - attachment'></td>" +
                    "<td class='mailbox - date'>" + item.Date + "</td>" +
                    "</tr>";

                $("#_tbl_MailBox_Body_").append(row);
                $('._tbl_MailBox_').html($(".mailbox-messages").html());
            });
        }
        else {
            $('#_Success_Message_Display_ > span').html("Drafts is empty");
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

};

function Junk() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Mailbox/Junk',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $("#_tbl_MailBox_Body_").empty();
        $(".MainBoxTitle").text("Junk");
        if (resp.length != 0) {
            $.each(resp, function (i, item) {
                var row = "<tr >" +
                    //"<td><div class='icheckbox_flat-blue' aria-checked='false' aria-disabled='false' style='position: relative;'><input type='checkbox'><ins class='iCheck-helper'></ins></div></td>" +
                    //"<td class='mailbox-star'><a href='#'><i class='fa fa-star text-yellow'></i></a></td>" +
                    "<td class='mailbox-name'><a href='#' onClick='ReadMail(" + item.mail_id.Id + ',' + item.mail_id.IsValid + ',' + item.mail_id.Validity + ")'>" + item.EmailFrom + "</a></td>" +
                    "<td class='mailbox-subject'>" +
                    "<b>" + item.Subject +
                    "</td>" +
                    //"<td class='mailbox - attachment'></td>" +
                    "<td class='mailbox - date'>" + item.Date + "</td>" +
                    "</tr>";

                $("#_tbl_MailBox_Body_").append(row);
                $('._tbl_MailBox_').html($(".mailbox-messages").html());
            });
        }
        else {
            $('#_Success_Message_Display_ > span').html("Junk is empty");
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

};

function Trash() {

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Mailbox/Trash',
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        $("#_tbl_MailBox_Body_").empty();
        $(".MainBoxTitle").text("Trash");
        if (resp.length != 0) {
            $.each(resp, function (i, item) {
                var row = "<tr >" +
                    //"<td><div class='icheckbox_flat-blue' aria-checked='false' aria-disabled='false' style='position: relative;'><input type='checkbox'><ins class='iCheck-helper'></ins></div></td>" +
                    //"<td class='mailbox-star'><a href='#'><i class='fa fa-star text-yellow'></i></a></td>" +
                    "<td class='mailbox-name'><a href='#' onClick='ReadMail(" + item.mail_id.Id + ',' + item.mail_id.IsValid + ',' + item.mail_id.Validity + ")'>" + item.EmailFrom + "</a></td>" +
                    "<td class='mailbox-subject'>" +
                    "<b>" + item.Subject +
                    "</td>" +
                    //"<td class='mailbox - attachment'></td>" +
                    "<td class='mailbox - date'>" + item.Date + "</td>" +
                    "</tr>";

                $("#_tbl_MailBox_Body_").append(row);
                $('._tbl_MailBox_').html($(".mailbox-messages").html());
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

};

function getSingleEmail(emailObject, url) {
    $('.IcmOverlaySpinner').show();
    var token = $('[name=__RequestVerificationToken]').val();
    //var emailObject = { Id: idOfEmail, IsValid: validEmailCheck, Validity: getValidityOfEmail };
    $.ajax({
        url: url,
        type: "Post",
        data: { __RequestVerificationToken: token, mail_id: JSON.stringify(emailObject) },
    }).done(function (resp) {
        $('.readsubject').html(resp.Subject);
        $('.readFrom').html(resp.EmailFrom);
        $('.readDatetime').html(resp.Date);
        $('.mailbox-read-message').html(resp.Body);

        var icon = "";
        $.each(resp.MailAttachments, function (i, item) {
            if (item.FileName.includes(".jpeg") || item.FileName.includes(".jpg") || item.FileName.includes(".png")) {
                icon = 'fa fa-file-image-o';
            } else if (item.FileName.includes(".doc") || item.FileName.includes(".docx")) {
                icon = 'fa fa-file-word-o';
            } else if (item.FileName.includes(".xls") || item.FileName.includes(".csv")) {
                icon = 'fa fa-file-excel-o';
            } else if (item.FileName.includes(".pdf")) {
                icon = 'fa fa-file-pdf-o';
            } else if (item.FileName.includes(".zip") || item.FileName.includes(".rar")) {
                icon = 'fa fa-file-archive-o';
            } else if (item.FileName.includes(".zip") || item.FileName.includes(".rar")) {
                icon = 'fa fa-file-archive-o';
            }


            var li = "<li>" +
                "<span class='mailbox-attachment-icon'><i class='" + icon + "'></i></span>" +
                "<div class='mailbox-attachment-info'>" +
                "<a href='/ImapAttachments/" + item.FileName + "' class='mailbox-attachment-name'><i class='fa fa-paperclip'></i> " + item.FileName + "</a>" +
                "<span class='mailbox-attachment-size'>1,245 KB<a download href='/ImapAttachments/" + item.FileName + "' class='btn btn-default btn-xs pull-right'><i class='fa fa-cloud-download'></i></a>" +
                "</span>" +
                "</div>" +
                "</li>";

            $(".mailbox-attachments").append(li);
        });
        $('.IcmOverlaySpinner').hide();
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

$(document).on('click', '.mailrow', function (event) {
    var body = $(this).attr('data-body');
    ReadMail();
});

$("._Compose_Mail_").click(function () {
    if ($(this).html() == "Compose") {
        $(this).html("Back to Inbox");
        Compose();
    } else if ($(this).html() == "Back to Inbox") {
        $(this).html("Compose");
        Inbox();
    }
});

function emailNotification() {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: false,
        url: '/Mailbox/Notification',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        console.log(resp);
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}

//setInterval(function () {
//    var token = $('[name=__RequestVerificationToken]').val();
//    $.ajax({
//        async: true,
//        url: '/Mailbox/Notification',
//        type: "Post",
//        data: { __RequestVerificationToken: token },
//    }).done(function (resp) {

//    }).fail(function () {
//        $('#_Error_Message_Display_ > span').html('Network Error.');
//        $('#_Error_Message_Display_').slideDown("slow");
//        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
//    });
//}, 100000);