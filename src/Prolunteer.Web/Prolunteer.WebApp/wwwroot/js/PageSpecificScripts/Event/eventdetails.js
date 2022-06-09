(function e() {
    $('#volunteer-position-removal-confirmation-modal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var positionID = button.data('position-id');
        var positionName = button.data('position-name');
        var modal = $(this);

        var textParagraph = modal.find('.modal-body>p');
        textParagraph.text(`Are you sure you want to remove the position ${positionName}?`);

        var button = modal.find('.modal-footer>.volunteer-position-remove-button');
        button.attr('id', positionID);
    });
    $(".volunteer-position-remove-button")
        .each(function () {
            $(this).click(() => {
                $.ajax({
                    url: '/VolunteerPosition/RemoveVolunteerPosition/' + $(this).attr('id'),
                    type: 'DELETE'
                });
            });
        });
}) ();