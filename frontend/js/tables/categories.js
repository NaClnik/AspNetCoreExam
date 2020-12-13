$(function () {
    $.ajax({
        url: 'http://localhost:9999/api/categories',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = getHtmlTableBodyFromObject(obj, ['Id', 'CategoryName']);

        $('#insert-body').html(html);

        setBtnDeleteHandler('.btn-delete', 'categories');
    }) // done.
}) // jQuery.