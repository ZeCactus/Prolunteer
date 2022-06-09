(function e() {
    $.ajax({
        url: "/UserAccount/GetRoles",
    }).done(function (data) {
        var selectlist = $(".form-group>select");
        data.forEach(function (item) {
            let option = document.createElement('option');
            option.value = item.value;
            option.text = item.text;
            selectlist.append(option);
        });
    });
} ());