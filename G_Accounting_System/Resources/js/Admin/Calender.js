$(function () {
    GetCalenderEvents();
    /* initialize the external events
     -----------------------------------------------------------------*/

    init_events($('#external-events div.external-event'))

    /* initialize the calendar
     -----------------------------------------------------------------*/
    //Date for the calendar events (dummy data)
    var date = new Date()
    var d = date.getDate(),
        m = date.getMonth(),
        y = date.getFullYear()
    //$('#calendar').fullCalendar({
    //header: {mo
    //    left: 'prev,next today',
    //    center: 'title',
    //    right: 'month,agendaWeek,agendaDay'
    //},
    //buttonText: {
    //    today: 'today',
    //    month: 'month',
    //    week: 'week',
    //    day: 'day'
    //},
    ////Random default events
    //events: [
    //    {
    //        title: 'All Day Event',
    //        start: new Date(y, m, 1),
    //        backgroundColor: '#f56954', //red
    //        borderColor: '#f56954' //red
    //    },
    //    {
    //        title: 'Long Event',
    //        start: new Date(y, m, d - 5),
    //        end: new Date(y, m, d - 2),
    //        backgroundColor: '#f39c12', //yellow
    //        borderColor: '#f39c12' //yellow
    //    },
    //    {
    //        title: 'Meeting',
    //        start: new Date(y, m, d, 10, 30),
    //        allDay: false,
    //        backgroundColor: '#0073b7', //Blue
    //        borderColor: '#0073b7' //Blue
    //    },
    //    {
    //        title: 'Lunch',
    //        start: new Date(y, m, d, 12, 0),
    //        end: new Date(y, m, d, 14, 0),
    //        allDay: false,
    //        backgroundColor: '#00c0ef', //Info (aqua)
    //        borderColor: '#00c0ef' //Info (aqua)
    //    },
    //    {
    //        title: 'Birthday Party',
    //        start: new Date(y, m, d + 1, 19, 0),
    //        end: new Date(y, m, d + 1, 22, 30),
    //        allDay: false,
    //        backgroundColor: '#00a65a', //Success (green)
    //        borderColor: '#00a65a' //Success (green)
    //    },
    //    {
    //        title: 'Click for Google',
    //        start: new Date(y, m, 28),
    //        end: new Date(y, m, 29),
    //        url: 'http://google.com/',
    //        backgroundColor: '#3c8dbc', //Primary (light-blue)
    //        borderColor: '#3c8dbc' //Primary (light-blue)
    //    }
    //],


    //})

    /* ADDING EVENTS */
    var currColor = '#3c8dbc' //Red by default
    //Color chooser button
    var colorChooser = $('#color-chooser-btn')
    $('#color-chooser > li > a').click(function (e) {
        e.preventDefault()
        //Save color
        currColor = $(this).css('color')
        //Add color effect to button
        $('#add-new-event').css({ 'background-color': currColor, 'border-color': currColor })
    })


    $('#add-new-event').click(function (e) {
        e.preventDefault()
        //Get value and make sure it is not null
        var val = $('#new-event').val()
        if (val.length == 0) {
            return
        }
        //Create events
        var event = $('<div />')
        event.css({
            'background-color': currColor,
            'border-color': currColor,
            'color': '#fff'
        }).addClass('external-event')
        event.html(val)
        $('#external-events').prepend(event)

        //Add draggable funtionality
        init_events(event)
        AddEvents(currColor);
        //Remove event from text input
        $('#new-event').val('')
    })

    GetEventsName();
})
function init_events(ele) {
    ele.each(function () {

        // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
        // it doesn't need to have a start or end
        var eventObject = {
            title: $.trim($(this).text()), // use the element's text as the event title
            id: $.trim($(this).attr('data-id'))
        }

        // store the Event Object in the DOM element so we can get to it later
        $(this).data('eventObject', eventObject)

        // make the event draggable using jQuery UI
        $(this).draggable({
            zIndex: 1070,
            revert: true, // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        })

    })

}

function AddEvents(Colour) {
    var token = $('[name=__RequestVerificationToken]').val();
    var url = '/Calender/AddEvents';
    var Event_Data = {
        title: $('#new-event').val(),
        backgroundColor: Colour,
        borderColor: Colour
    }

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "EventData": JSON.stringify(Event_Data) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
        GetEventsName();

    }).fail(function () {
        alert("post error 0");
        $('#new-event').val("");
    });
};

function AddCalenderEvents(Event_id, date) {
    var token = $('[name=__RequestVerificationToken]').val();
    var url = '/Calender/AddCalenderEvents';
    var Event_Data = {
        id: Event_id,
        start: date
    }

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token, "EventData": JSON.stringify(Event_Data) },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
       

    }).fail(function () {
        alert("post error 0");
        $('#new-event').val("");
    });
};

function GetCalenderEvents() {
    var url = '/Calender/GetCalenderEvents';
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {

        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaDay'
            },
            events: resp,
            eventLimit: true, // for all non-agenda views
            views: {
                agenda: {
                    eventLimit: 2, // adjust to 6 only for agendaWeek/agendaDay

                }
            },
            eventRender: function (eventObj, $el) {

                $el.popover({
                    title: eventObj.title,
                    //content: eventObj.actionDescription + ' - ' + eventObj.actionNotes,
                    trigger: 'hover',
                    placement: 'top',
                    container: 'body'
                });
            },


            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar !!!
            drop: function (date, allDay) { // this function is called when something is dropped

                // retrieve the dropped element's stored Event Object
                var originalEventObject = $(this).data('eventObject')

                // we need to copy it, so that multiple events don't have a reference to the same object
                var copiedEventObject = $.extend({}, originalEventObject)

                // assign it the date that was reported
                copiedEventObject.start = date
                copiedEventObject.allDay = allDay
                copiedEventObject.id = originalEventObject.id
                copiedEventObject.backgroundColor = $(this).css('background-color')
                copiedEventObject.borderColor = $(this).css('border-color')

                // render the event on the calendar
                // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                $('#calendar').fullCalendar('renderEvent', copiedEventObject, true)

                // is the "remove after drop" checkbox checked?
                if ($('#drop-remove').is(':checked')) {
                    // if so, remove the element from the "Draggable Events" list
                    $(this).remove()
                }

                var day = date._d.getDate();
                var month = date._d.getMonth("mm") + 1;
                var year = date._d.getFullYear();
                var date = date.format("YYYY-MM-DD");
                //var date = year + "-" + month + "-" + day;
                AddCalenderEvents(copiedEventObject.id, date);
            }
        })


    }).fail(function () {

    });
}

function GetEventsName() {
    var url = '/Calender/GetEventsName';
    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: url,
        type: "POST",
        data: { __RequestVerificationToken: token },
        datatype: 'json',
        ContentType: 'application/json; charset=utf-8'
    }).done(function (resp) {
            $("#external-events").html("");
            $.each(resp, function (i, item) {
                init_events($('#external-events div.external-event'))
                var event = $("<div data-id=" + item.id + "/>")
                event.css({
                    'background-color': item.backgroundColor,
                    'border-color': item.backgroundColor,
                    'color': '#fff'
                }).addClass('external-event');
                event.html(item.title);
                $('#external-events').prepend(event);
                init_events(event);
            });
    }).fail(function () {
        $('#_Error_Message_Display_ > span').html('Network Error.');
        $('#_Error_Message_Display_').slideDown("slow");
        $('html, body').animate({ scrollTop: $('#_Error_Message_Display_').offset().top }, 'slow');
    });
}