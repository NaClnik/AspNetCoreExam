function setBtnDeleteHandler(selector, table){
    $(selector).on('click', function () {
        let id = $(this).val();
        console.log(id);
        $.ajax({
            url: `http://localhost:9999/api/${table}/${id}`,
            method: 'DELETE'
        }).done((msg) => {
            $(this).parent().parent().parent().remove();
        }) // done.
    }) // click.
} // setBtnDeleteHandler.

function getHtmlTableBodyFromObject(obj, columns){
    return obj.reduce((accumulator, currentValue) => {
        let buf = '<tr>';

        for (let item of columns) {
            buf += `<td>${currentValue[item]}</td>`;
        } // for.

        buf +=
            `<td>
            <div class="row">
                <button class="btn btn-danger btn-block col btn-delete" value="${currentValue.Id}">Удалить</button>
            </div>
         </td></tr>`;

        return accumulator + buf;
    }, '');
} // getHtmlTableBodyFromObject.