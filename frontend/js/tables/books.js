$(function () {
    $.ajax({
        url: 'http://localhost:9999/api/queries/1',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = getHtmlTableBodyFromObject(obj,
            ['Id', 'Title', 'YearOfIssue', 'Price', 'Amount', 'AuthorName', 'CategoryName']);

        $('#insert-body').html(html);

        setBtnDeleteHandler('.btn-delete', 'books');
    }) // done.
}) // jQuery.