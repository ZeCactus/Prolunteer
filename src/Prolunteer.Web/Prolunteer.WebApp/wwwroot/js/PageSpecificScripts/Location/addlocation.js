$('document').ready(function () {
    $.ajax({
        url: "/County/GetCountiesForSelect",
        type: "GET"
    }).done(function (data) {
        let selectList = $('#CountyId');
        data.forEach(datum => {
            let option = document.createElement("option");
            option.value = datum.value;
            option.text = datum.text;
            selectList.append(option);
        })
        selectList.select()
    });

    $('#CountyId').change(function () {
        let countyList = $("#CountyId")
        let selectList = $('#CityId');
        selectList.find("option").remove();
        var nullOption = document.createElement("option");
        nullOption.setAttribute("disabled", "");
        nullOption.setAttribute("selected", "");
        nullOption.text = "Pick a city";
        selectList.append(nullOption)
        $.ajax({
            url: "/City/GetCitiesForSelect",
            type: "GET",
            data: { id: countyList.find(":selected").val() }
        }).done(function (data) {
            var nullOption = document.createElement("option");
            nullOption.setAttribute("disabled", "");
            nullOption.setAttribute("selected", "");
            nullOption.text = "Pick a city";
            selectList.append(nullOption)
            data.forEach(datum => {
                let option = document.createElement("option");
                option.value = datum.value;
                option.text = datum.text;
                selectList.append(option);
            })
        })
    });
})