

$('document').ready(function () {
    $("#search-input").keyup(function () {
        $("#pagination-display").pagination(1);
    })
    $("#pagination-display").pagination({
        dataSource: "/City/GetCities",
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

            var countyArea = $(document.createElement("div"));
            countyArea.addClass("col-sm");
            countyArea.text(datum.county);
            countyArea.appendTo(row);

            var buttonArea = $(document.createElement("div"));
            buttonArea.addClass("col-sm-2")
            buttonArea.addClass("text-right");
            buttonArea.appendTo(row);

            var editButton = $(document.createElement("a"));
            editButton.addClass("btn");
            editButton.addClass("btn-primary");
            editButton.attr("href", "/City/EditCity/" + datum.id);
            editButton.text("Edit");
            editButton.appendTo(buttonArea);

            buttonArea.append("\n");

            var deleteButton = $(document.createElement("button"));
            deleteButton.addClass("btn");
            deleteButton.addClass("btn-danger");
            deleteButton.data("id", datum.id);
            deleteButton.text("Delete");
            deleteButton.click(deleteFunction);
            deleteButton.appendTo(buttonArea);
        })
        return response;
    }

    var deleteFunction = function () {
        $.ajax({
            url: "/City/RemoveCity",
            type: "DELETE",
            data: { id: $(this).data("id") }
        }).done(function () {
            location.reload();
        }).fail(function () {
            alert("An error occured!");
        });
    }
});
