$(function () {
    $.ajax({
        url: 'http://localhost:9999/api/queries/4',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = obj.reduce((accumulator, currentValue) => accumulator +
            `<tr>
                             <td>${currentValue.AuthorName}</td>
                             <td>${currentValue.Title}</td>
                             <td>${currentValue.CategoryName}</td>
                             <td>${currentValue.Price}</td>
                             <td>${currentValue.Amount}</td>
                         </tr>`, '');

        $('#insert-body').html(html);
    }) // done.
}) // jQuery.