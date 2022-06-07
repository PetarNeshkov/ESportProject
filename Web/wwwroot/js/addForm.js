$(function () {
    $('[data-toggle="tooltip"]').tooltip({
        html: true
    })
})

$("#name-input").focusout(function () {
    let name = $("#name-input").val();

    if (name.length != 0) {
        $('#player-name').text(name);
    }
})

$("#image-input").focusout(function () {
    let imageUrl = $("#image-input").val();

    if (imageUrl.length != 0) {
        $('#player-image').attr('src', imageUrl);
    }
})

$("#role-input").change(function () {
    $('#player-role').text($("#role-input").val());
})

$("#division-input").change(function () {
    $('#player-division').text($("#division-input").val());
})