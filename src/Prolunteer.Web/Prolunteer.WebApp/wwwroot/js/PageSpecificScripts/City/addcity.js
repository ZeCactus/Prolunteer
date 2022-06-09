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
    })
})