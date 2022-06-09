$('document').ready(function () {
    $.ajax({
        url: "/Certification/GetCertificationsForSelect/",
        type: "GET"
    }).done(function (data) {
        let selectList = $('#RequiredCertifications');
        data.forEach(function (item) {
            var newOption = document.createElement('option');
            newOption.value = item.value;
            newOption.text = item.text;
            selectList.append(newOption);
        })
        selectList.select2({
            closeOnSelect: false,
            placeHolder: "Add certifications"
        });
    })
});