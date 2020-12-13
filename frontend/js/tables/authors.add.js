$(function () {
    $('#add-btn').on('click', function () {
        let data = $('#author').val();

        $.ajax({
            data: `{"Author":"${data}"}`,
            url: 'http://localhost:9999/api/authors',
            method: 'POST',
            contentType: 'application/json'
        }).done(function (msg) {
            let obj = JSON.parse(msg);

            let html =
                `<tr>
                             <td>${obj.Id}</td>
                             <td>${obj.AuthorName}</td>
                             <td>
                                <div class="row">
                                    <button class="btn btn-danger btn-block col btn-delete" value="${obj.Id}">Удалить</button>
                                </div>
                             </td>
                         </tr>`;

            $('#insert-body').append(html);

            $('.btn-delete').on('click', function () {
                let id = $(this).val();
                console.log(id);
                $.ajax({
                    url: `http://localhost:9999/api/authors/${id}`,
                    method: 'DELETE'
                }).done((msg) => {
                    $(this).parent().parent().parent().remove();
                }) // done.
            })
        }) // done.

        return false;
    }) // click.
}) // jQuery.