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


$("#mid-input").change(function () {
    $('#player-role-mid').text($("#mid-input").val());
})

$("#top-input").change(function () {
    $('#player-role-top').text($("#top-input").val());
})

$("#jungle-input").change(function () {
    $('#player-role-jungle').text($("#jungle-input").val());
})

$("#support-input").change(function () {
    $('#player-role-support').text($("#support-input").val());
})

$("#bottom-input").change(function () {
    $('#player-role-bottom').text($("#bottom-input").val());
})

$("#role-input").change(function () {
    $('#player-role').text($("#role-input").val());
})

$("#myTeam-input").change(function () {
    $('#firstTeam').text($("#myTeam-input").val());
})

$("#enemyTeam-input").change(function () {
    $('#enemyTeam').text($("#enemyTeam-input").val());
})