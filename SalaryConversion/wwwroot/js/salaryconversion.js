$(document).ready(function () {
    var url = window.location.origin + '/salary-conversion/get-datasource';

    $.noConflict();
    $('#dataTable').DataTable({
        iDisplayLength: 25,
        processing: true,
        serverSide: true,
        ordering: false,
        searching: false,
        paging: false,        
        order: [
            [0, "asc"]
        ],
        "columns": [
            {
                "data": "id"
            },
            {
                "data": "name"
            },
            {
                "data": "username"
            },
            {
                "data": "email"
            },
            {
                "data": "address",
                "render": function (data) {
                    return data.street + ', ' + data.suite + ', ' + data.city + ' ' + data.zipcode ;
                }
            },
            {
                "data": "phone"
            },
            {
                "data": "salaryidr",
                "render": function (data) {
                   return '<center><span>' + data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';                   
                }
            },
            {
                "data": "salaryusd",
                "render": function (data) {
                    return '<center><span>' + data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            }
        ],
        "ajax": {

            url: url,
            type: "POST",
            data: {
            },           
            error: function (xhr) {
                $("#dataTable_processing", $('#dataTable')).css('display', 'none');
                $('#dataTable').append('<tbody class="dataTables_empty"><tr><th colspan="4">' + xhr.responseText + '</th></tr></tbody>');
            }
        },
        "fnInitComplete": function () {
            $("#dataTable_processing", $('#dataTable')).css('display', 'none');
        }
    });
});
