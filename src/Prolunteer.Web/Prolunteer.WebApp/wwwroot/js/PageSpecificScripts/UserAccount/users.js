

$('document').ready(function () {

    var currentPage;

    $("#search-input").keyup(function () {
        $("#pagination-display").pagination(1);
    })
    $("#pagination-display").pagination({
        dataSource: "/UserAccount/GetUsers",
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
            currentPage = pagination.pageNumber;
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

            var emailArea = $(document.createElement("div"));
            emailArea.addClass("col-sm");
            emailArea.text(datum.eMail);
            emailArea.appendTo(row);

            var roleArea = $(document.createElement("div"));
            roleArea.addClass("col-sm");
            roleArea.text(datum.role);
            roleArea.appendTo(row);

            var buttonArea = $(document.createElement("div"));
            buttonArea.addClass("col-sm-2")
            buttonArea.addClass("text-right");
            buttonArea.appendTo(row);

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
            url: "/UserAccount/RemoveUser",
            type: "DELETE",
            data: { userId: $(this).data("id") }
        }).done(function () {
            $("#pagination-display").pagination(currentPage);
        }).fail(function () {
            alert("An error occured!");
        });
    }
});
