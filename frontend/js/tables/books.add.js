$(function () {
    console.log('sasasa');
    $('#add-btn').on('click', function () {
        let title = $('#title').val();
        let yearOfIssue = $('#yearOfIssue').val();
        let price = $('#price').val();
        let amount = $('#amount').val();
        let authorId = $('#author-select').val()
        let categoryId = $('#category-select').val()

        $.ajax({
            data: `{"Title":"${title}",
                    "YearOfIssue":${yearOfIssue},
                    "Price":${price},
                    "Amount":${amount},
                    "AuthorId":${authorId},
                    "CategoryId":${categoryId}}`,
            url: 'http://localhost:9999/api/books',
            method: 'POST',
            contentType: 'application/json'
        }).done(function (msg) {
            let obj = JSON.parse(msg);

            let html =
                `<tr>
                             <td>${obj.Id}</td>
                             <td>${obj.Title}</td>
                             <td>${obj.YearOfIssue}</td>
                             <td>${obj.Price}</td>
                             <td>${obj.Amount}</td>
                             <td>${obj.AuthorName}</td>
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
                    url: `http://localhost:9999/api/books/${id}`,
                    method: 'DELETE'
                }).done((msg) => {
                    $(this).parent().parent().parent().remove();
                }) // done.
            })
        }) // done.

        return false;
    }) // click.
}) // jQuery.