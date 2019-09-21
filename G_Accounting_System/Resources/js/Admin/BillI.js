$("#_Add_New_Bill_btn_").click(function () {
    $(this).attr("disabled", true);
    $('._Add_New_Bill_Form_').html('');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: '/Bill/Add',
        type: "Post",
        data: { __RequestVerificationToken: token },
    }).done(function (resp) {
        $('._Add_New_Bill_Form_').append(resp);
        $('.nav-tabs a[href="#v-general-dtls"]').tab('show');
        $('._Add_New_Bill_Form_').slideDown("slow");
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });

});
