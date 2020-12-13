$(function () {
    $.ajax({
        url: 'http://localhost:9999/api/queries/3',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = obj.reduce((accumulator, currentValue) => accumulator +
            `<tr>
                             <td>${currentValue.Title}</td>
                             <td>${currentValue.YearOfIssue}</td>
                             <td>${currentValue.Amount}</td>
                             <td>${currentValue.CategoryName}</td>
                         </tr>`, '');

        $('#insert-body').html(html);
    }) // done.
}) // jQuery.