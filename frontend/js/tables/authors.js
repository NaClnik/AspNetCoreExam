$(function () {
    $.ajax({
        url: 'http://localhost:9999/api/authors',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = getHtmlTableBodyFromObject(obj, ['Id', 'AuthorName']);

        $('#insert-body').html(html);

        setBtnDeleteHandler('.btn-delete', 'authors');
    }) // done.
}) // jQuery.