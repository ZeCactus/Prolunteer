

$('document').ready(function () {
    $("#search-input").keyup(function () {
        $("#pagination-display").pagination(1);
    })
    $("#pagination-display").pagination({
        dataSource: "/Certification/GetCertifications",
        ajax: function () {
            return {
                data: { filter: $("#search-input").val() }
            }
        },
        locator: "elements",
        totalNumberLocator: function (response) {
            return +response.totalElementsCount;
        },
        pageSize: 10,
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

            var row = $(document.createElement("div"));
            row.addClass("row");
            row.appendTo(response);

            var nameArea = $(document.createElement("div"));
            nameArea.addClass("col-sm");
            nameArea.text(datum.name);
            nameArea.appendTo(row);

            var descriptionArea = $(document.createElement("div"));
            descriptionArea.addClass("col-sm");
            descriptionArea.text(datum.description);
            descriptionArea.appendTo(row);

            var buttonArea = $(document.createElement("div"));
            buttonArea.addClass("col-sm-1")
            buttonArea.addClass("text-right");
            buttonArea.appendTo(row);

            var editButton = $(document.createElement("a"));
            editButton.addClass("btn");
            editButton.addClass("btn-primary");
            editButton.attr("href", "/Certification/EditCertification/" + datum.id);
            editButton.text("Edit");
            editButton.appendTo(buttonArea);
        })
        return response;
    };

});
