function InsertActivity() {

    var url = '/Activity/InsertActivity';

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "ActivityData": JSON.stringify(Activity_Data) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

        Activity_Data = null;

    }).fail(function () {
        alert("post error 0");
        Activity_Data = null;
    });
}

function Activities(ActivityType_id, ActivityType) {

    var token = $('[name=__RequestVerificationToken]').val();
    //var ActivityType_id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
    $.ajax({
        url: '/Activity/Activities/',
        type: "POST",
        data: { __RequestVerificationToken: token, ActivityType_id, ActivityType },
    }).done(function (resp) {
        $.each(resp, function (i, item) {

            if (item.Receiver_id != $('#receiver_id').val()) {
                var li = "<li class='time-label'>" +
                    "<span class='bg-red ActivityDate'>" + item.Date + "</span>" +
                    "</li>" +
                    "<li>" +
                    "<i class='fa fa-calendar-check-o bg-blue'></i>" +

                    "<div class='timeline-item'>" +
                    "<span class='time ActivityTime'><i class='fa fa-clock-o'></i>" + item.Time + "</span>" +

                    "<h3 class='timeline-header ActivityName'> " + item.ActivityName + "</h3>" +

                    "<div class='timeline-body ActivityDescription'>" + item.Description + "</div>" +
                    "</div>" +
                    "</li>";
                $('.ACTIVITY').append(li);

            }
        });
        var li2 = "<li>" +
            "<i class='fa fa-clock-o bg-gray'></i>" +
            "</li>";
        $('.ACTIVITY').append(li2);
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        //$('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}


