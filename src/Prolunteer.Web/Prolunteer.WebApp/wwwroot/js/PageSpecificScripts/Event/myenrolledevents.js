$('document').ready(function e() {

    $("#search-input").keyup(function () {
        $("#pagination-display").pagination(1);
    })
    $("#pagination-display").pagination({
        dataSource: "/Event/GetMyEnrolledEvents",
        ajax: function () {
            return {
                data: { filter: $("#search-input").val() }
            }
        },
        position: "top",
        locator: "elements",
        totalNumberLocator: function (response) {
            return +response.totalElementsCount;
        },
        pageSize: 3,
        callback: function (data, pagination) {
            var result = template(data);
            $("#data-container").empty();
            result.children().appendTo($("#data-container"));
        }
    });
    var template = function (data) {
        var response = $(document.createElement("div"));
        response.addClass("container");
        data.forEach(function (datum) {
            response.append("<hr />");

            var event_view = $(document.createElement("div"));
            event_view.addClass("event-view");
            event_view.appendTo(response);

            var name = $(document.createElement("h2"));
            name.text(datum.name);
            name.appendTo(event_view);
            response.append("\n");

            var eventType = $(document.createElement("h3"));
            eventType.text(datum.eventType);
            eventType.appendTo(event_view);
            response.append("\n");

            var positionName = $(document.createElement("h3"));
            positionName.text("Position: " + datum.positionName);
            positionName.appendTo(event_view);
            response.append("\n");

            var organizer = $(document.createElement("h4"));
            organizer.text(`By ${datum.organizer}`);
            organizer.appendTo(event_view);
            response.append("\n");

            var location = $(document.createElement("p"));
            location.text(`At ${datum.location} from ${datum.startDate} to ${datum.endDate}`);
            location.appendTo(event_view);
            response.append("\n");
        })
        return response;
    }
});