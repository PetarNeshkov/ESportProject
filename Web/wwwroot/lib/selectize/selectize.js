﻿
var $select = $('#role-input').selectize({
    create: false,
    sortField: 'text',
});

var $select = $('#division-input').selectize({
    create: false,
    sortField: 'text',
});

$select.each(function () {
    $(this)[0].selectize.clear(true);
});
