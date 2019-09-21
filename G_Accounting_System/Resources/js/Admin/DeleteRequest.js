var List = "";
$(".delete_request").click(function () {
    $('#_tbl_Delete_Requests_Body_').empty();
    $(this).addClass("active"); 
    var type = $(this).find('#type').html();
    SelectDeleteRequests('/' + type + '/', null);
});

function SelectDeleteRequests(typee, parameter) {
    var type = typee.toString().replace(/[^a-z0-9\s]/gi, '');
    var token = $('[name=__RequestVerificationToken]').val();
    List = $('#_tbl_Delete_Requests_').DataTable({
        "searching": true,
        "bsorting": true,
        "bServerSide": true,
        "sAjaxSource": "/DeleteRequests/SelectAll",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, type: type, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[20].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            { data: "Name" },
            { data: "DeleteRequestedBy" },
            {
                data: function (data, type, dataToSet) {
                    return "<span class='fa fa-trash table_list_ops_icons' onclick='Delete(" + data.id + ", " + typee + ")' title='Delete'></span>";
                    return data;
                }

            },
        ]
    });
    List.destroy();
}

function Delete(id, typee) {
    var type = typee.toString().replace(/[^a-z0-9\s]/gi, '');
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        async: true,
        url: "/DeleteRequests/Delete",
        type: "POST",
        data: { __RequestVerificationToken: token, id, type },
    }).done(function (resp) {
        SelectDeleteRequests(typee, null);
        $('#_Success_Message_Display_ > span').html(type + " Deleted Successfully");
        $('#_Success_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Success_Message_Display_').offset().top }, 'slow');
        $('#_tbl_Delete_Requests_Body_').empty();
        //SelectDeleteRequests(type, null);
        
        setTimeout(function () { $('#_Success_Message_Display_').slideUp("slow"); }, 5000);

    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}