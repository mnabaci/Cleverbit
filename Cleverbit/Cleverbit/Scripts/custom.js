$('#refreshResults').on('click', (e) => {
    e.preventDefault();
    request({
        endpoint: 'api/Match/CurrentResults',
        type: 'GET',
        success: function (result) {
            if (result && result.IsSuccess) {
                let resultTable = $('#resultTable');
                let resultTableBody = resultTable.find('tbody');
                let tableBodyNew = '';
                $.each(result.Data, (index, elem) => {
                    tableBodyNew += '<tr>'
                        + '<th scope="row">' + (index + 1) + '</th>'
                        + '<td>' + elem.UserName + '</td>'
                        + '<td>' + elem.Result + '</td>'
                        + '</tr > ';
                });

                resultTableBody.html(tableBodyNew);
            }
        }
    });
});

function request(config) {
    $.ajax({
        url: config.endpoint,
        type: config.type,
        data: config.data,
        contentType: "application/json",
        dataType: 'json',
        success: (result) => config.success(result)
    });
}