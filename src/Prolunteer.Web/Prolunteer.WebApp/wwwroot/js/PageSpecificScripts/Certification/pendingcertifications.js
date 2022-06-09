$('document').ready(function () {
    $("#search-input").keyup(function () {
        $("#pagination-display").pagination(1);
    })
    $("#pagination-display").pagination({
        dataSource: "/Certification/GetPendingCertifications",
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

            var certificationNameArea = $(document.createElement("div"));
            certificationNameArea.addClass("col-sm");
            certificationNameArea.text(datum.userName);
            certificationNameArea.appendTo(row);

            var certificationNameArea = $(document.createElement("div"));
            certificationNameArea.addClass("col-sm");
            certificationNameArea.text(datum.certificationName);
            certificationNameArea.appendTo(row);

            var buttonArea = $(document.createElement("div"));
            buttonArea.addClass("col-sm-1")
            buttonArea.addClass("text-right");
            buttonArea.appendTo(row);

            var detailsButton = $(document.createElement("a"));
            detailsButton.addClass("btn");
            detailsButton.addClass("btn-primary");
            detailsButton.attr("href", "/Certification/PendingCertificationDetails/?userId=" + datum.userId + "&certificationId=" + datum.certificationId);
            detailsButton.text("Details");
            detailsButton.appendTo(buttonArea);
        })
        return response;
    }
});
