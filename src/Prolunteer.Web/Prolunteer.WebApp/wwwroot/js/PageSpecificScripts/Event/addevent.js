$('document').ready((function e() {
    function createPlaceholderOption(text) {
        var placeholder = document.createElement("option");
        placeholder.text = text;
        placeholder.setAttribute("disabled", "");
        placeholder.setAttribute("selected", "");
        return placeholder;
    }
    let countySelect = $("#CountyId");
    let citySelect = $("#CityId");
    let locationSelect = $("#LocationId");
    $.ajax({
        url: "/EventType/GetEventTypesForSelect",
        success: function (data) {
            var selectlist = $("#EventTypeId");
            data.forEach(function (item) {
                let option = document.createElement('option');
                option.value = item.value;
                option.text = item.text;
                selectlist.append(option);
            });
        }
    });

    $.ajax({
        url: "/County/GetCountiesForSelect",
        type: "GET",
        success: function (data) {
            data.forEach(function (item) {
                let option = document.createElement('option');
                option.value = item.value;
                option.text = item.text;
                countySelect.append(option);
            });
        }
    });
    $("#CountyId").change(function (event) {
        citySelect.find("option").remove();
        locationSelect.find("option").remove();
        citySelect.append(createPlaceholderOption("Pick a city"));
        locationSelect.append(createPlaceholderOption("First pick a city"));
        $.ajax({
            url: "/City/GetCitiesForSelect",
            type: "GET",
            data: { id: countySelect.find(":selected").val() },
            success: function (data) {
                data.forEach(function (item) {
                    let option = document.createElement('option');
                    option.value = item.value;
                    option.text = item.text;
                    citySelect.append(option);
                });
            }
        });
    });

    $("#CityId").change(function (event) {
        locationSelect.find("option").remove();
        locationSelect.append(createPlaceholderOption("Pick a location"));
        $.ajax({
            url: "/Location/GetLocationsForSelect",
            type: "GET",
            data: { cityId: citySelect.find(":selected").val() },
            success: function (data) {
                data.forEach(function (item) {
                    console.log(item)
                    let option = document.createElement('option');
                    option.value = item.value;
                    option.text = item.text;
                    locationSelect.append(option);
                });
            }
        });
    });
}));