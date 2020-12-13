$(function () {
    $('#add-btn').on('click', function () {
        let data = $('#category').val();

        $.ajax({
            data: `{"Category":"${data}"}`,
            url: 'http://localhost:9999/api/categories',
            method: 'POST',
            contentType: 'application/json'
        }).done(function (msg) {
            let obj = JSON.parse(msg);

            let html =
                `<tr>
                             <td>${obj.Id}</td>
                             <td>${obj.CategoryName}</td>
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
                    url: `http://localhost:9999/api/categories/${id}`,
                    method: 'DELETE'
                }).done((msg) => {
                    $(this).parent().parent().parent().remove();
                }) // done.
            })
        }) // done.

        return false;
    }) // click.
}) // jQuery.