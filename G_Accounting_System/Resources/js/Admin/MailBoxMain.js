$("._Refresh_").click(function () {
    var name = $("#_Folder_Name_").val();
    MailboxMain(name);
});

$("._Delete_").click(function () {
    var name = $("#_Folder_Name_").val();
    var url = "";
    if (name === "Inbox") {
        url = "/Mailbox/DeleteInboxMail";
        DeleteMail(url);
    } else if (name === "Sent") {
        url = "/Mailbox/DeleteSentMail";
        DeleteMail(url);
    } else if (name === "Drafts") {
        url = "/Mailbox/DeleteDraftsMail";
        DeleteMail(url);
    } else if (name === "Junk") {
        url = "/Mailbox/DeleteJunkMail";
        DeleteMail(url);
    } else if (name === "Trash") {
        url = "/Mailbox/DeleteTrashMail";
        DeleteMail(url);
    }
});




function DeleteMail(url) {
    $('.IcmOverlaySpinner').show();
    var idOfEmail = $("#Id").val();
    var validEmailCheck = $("#isValid").val();
    var getValidityOfEmail = $("#Validity").val();
    alert("Delete");
    var emailObject = { Id: idOfEmail, IsValid: validEmailCheck, Validity: getValidityOfEmail };

    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        url: url,
        type: "Post",
        data: { __RequestVerificationToken: token, mail_id: JSON.stringify(emailObject) },
    }).done(function (resp) {
        $('#_Success_Message_Display_ > span').html(resp);
        $('#_Success_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Success_Message_Display_').slideUp("slow");
        }, 5000);
        $('.IcmOverlaySpinner').hide();
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}