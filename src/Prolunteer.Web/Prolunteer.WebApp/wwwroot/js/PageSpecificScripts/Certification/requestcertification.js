$("document").ready(function () {
    $.ajax({
        type: "GET",
        url: "/Certification/GetAvailableCertificationsForSelect"
    }).done(function (data) {
        var selectList = $("#CertificationId");
        data.forEach (function(datum) {
            var option = document.createElement("option");
            option.value = datum.value;
            option.text = datum.text;
            selectList.append(option);
        })
    })
})