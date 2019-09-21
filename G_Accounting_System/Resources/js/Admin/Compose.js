var Status = "";
$("#_Send_Email_").click(function () {
    Status = "Pending";
    SendMail(Status);
});

$("#_Draft_Email_").click(function () {
    Status = "Draft";
    SendMail(Status);
});
var array = new Array();

var Attachment_Data = new Array();
$("input[name=attachment]").change(function () {
    array = new Array();
    var files = $("input[type=file]").get(0).files;
    var file = files[0];
    var file = "";
    var base64 = "";
    var data = "";
    $.each($("input[type=file]"), function (i, obj) {
        $.each(obj.files, function (j, file) {
            array.push(obj.files[j]);


            var rows = "<tr data-file=" + file + ">" +
                "<td class='table_content_vertical_align_'>" + file.name + "</td>" +
                "<td class='table_content_vertical_align_'>" + Math.ceil(file.size / 1000000) + 'MB' + "</td>" +
                "<td class='table_content_vertical_align_ tdd'>" + file + "</td>" +
                "</tr>";
            $('#_Selected_Files_Body_').append(rows);
        })
    });
    $.each(array, function (key, value) {
        var reader = new FileReader();

        reader.onload = function (readerEvt) {

            var binaryString = readerEvt.target.result;
            base64 = btoa(binaryString);
            //file = array[key];
            var FileName = array[key].name;

            data = {
                base64: base64,
                FileName: FileName
            }

            Attachment_Data.push(data);
            //bas64array.push(btoa(binaryString));
        };

        reader.readAsBinaryString(array[key]);
    });
});

function SendMail(Status) {
    if ($("._Mail_To_Email").val() === "") {
        $('#_Error_Message_Display_ > span').html("Please Enter To email<br />");
        $('#_Error_Message_Display_').slideDown("slow");
        setTimeout(function () {
            $('#_Error_Message_Display_').slideUp("slow");
        }, 5000);
    }
    else {
        $('.IcmOverlaySpinner').show();
        var f = $('.wysihtml5-sandbox').contents();
        var body = $('.wysihtml5-sandbox').contents().find("body").html();

        var Email_Data = {
            EmailTo: $("._Mail_To_Email").val(),
            Subject: $("._Mail_Subject").val(),
            Body: body,
            Status: Status
        }


        var token = $('[name=__RequestVerificationToken]').val();

        $.ajax({
            url: '/Mailbox/SendMail',
            type: "POST",
            data: { __RequestVerificationToken: token, "EmailData": JSON.stringify(Email_Data), "AttachmentData": JSON.stringify(Attachment_Data) },
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8'
        }).done(function (resp) {
            if (resp.pFlag == 1) {
                $('#_Success_Message_Display_ > span').html(resp.pDesc);
                $('#_Success_Message_Display_').slideDown("slow");
                setTimeout(function () {
                    $('#_Success_Message_Display_').slideUp("slow");
                }, 5000);
            }
            else {
                $('#_Error_Message_Display_ > span').html('Network Error.');
                $('#_Error_Message_Display_').slideDown("slow");
                $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
            }

            $('.IcmOverlaySpinner').hide();

        }).fail(function () {
            $('#_Error_Message_Display_ > span').html('Network Error.');
            $('#_Error_Message_Display_').slideDown("slow");
            $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
        });
    }
};
