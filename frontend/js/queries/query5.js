$(function () {
    $.ajax({
        url: 'http://localhost:9999/api/queries/5',
        method: 'GET',
    }).done(function (msg) {
        let obj = JSON.parse(msg);

        let html = obj.reduce((accumulator, currentValue) => accumulator +
            `<tr>
                             <td>${currentValue.Key}</td>
                             <td>${currentValue.Amount}</td>
                         </tr>`, '');

        $('#insert-body').html(html);
    }) // done.
}) // jQuery.