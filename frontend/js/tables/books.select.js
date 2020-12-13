$(function () {
    $.ajax({
        url: 'http://localhost:9999/api/authors',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = obj.reduce((accumulator, currentValue) => accumulator +
            `<option value="${currentValue.Id}">${currentValue.AuthorName}</option>`, '');

        $('#author-select').append(html);
    }) // done.

    $.ajax({
        url: 'http://localhost:9999/api/categories',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = obj.reduce((accumulator, currentValue) => accumulator +
            `<option value="${currentValue.Id}">${currentValue.CategoryName}</option>`, '');

        $('#category-select').append(html);
    }) // done.
}) // jQuery.