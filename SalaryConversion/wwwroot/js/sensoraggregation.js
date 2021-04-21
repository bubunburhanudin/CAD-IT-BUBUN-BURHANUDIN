$(document).ready(function () {
    var url = window.location.origin + '/sensor-aggregation/get-datasource';

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
                "data": "roomarea" 
            },
            {
                "data": "mintemperature",
                "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            },
            {
                "data": "maxtemperature",
                 "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            },
            {
                "data": "medtemperature",
                "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            },
            {
                "data": "avgtemperature",
                 "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            },
            
            {
                "data": "minhumidity",
                "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            },
            {
                "data": "maxhumidity",
                "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            },
            {
                "data": "medhumidity",
                "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
                }
            },
            {
                "data": "avghumidity",
                "render": function (data) {
                    return '<center><span>' + data.toFixed(4).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '</span></center>';
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
