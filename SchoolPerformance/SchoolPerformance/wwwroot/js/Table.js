

function loadTable(table, columns) {
    table.DataTable({
        ajax: {
            url: "/Table/OnGet",
            dataType: 'json',
            type: "GET",
        },
        columns: columns,
        "columnDefs": [
            {
                //Make URN and LEAESTAB columns hidden
                targets: [0, 1],
                className: 'noVis',
                visible: false
            }
        ],
        "lengthMenu": [
            [10, 25, 50, 100, -1],
            [10, 25, 50, 100, "All"]
        ],
        serverSide: true,
        //Order by school name
        "order": [[2, "asc"]],
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'collection',
                text: 'Export All Records',
                className: 'btn btn-select mb-2',
                buttons: [
                    {
                        extend: 'copy',
                        className: 'btn btn-select-option',
                        exportOptions: {
                            modifier: {
                                page: 'all',
                                search: 'none'
                            }
                        }
                    },
                    {
                        extend: 'excel',
                        title: 'Data export',
                        className: 'btn btn-select-option',
                        exportOptions: {
                            modifier: {
                                page: 'all',
                                search: 'none'
                            }
                        }
                    },
                    {
                        extend: 'csv',
                        title: 'Data export',
                        className: 'btn btn-select-option',
                        exportOptions: {
                            modifier: {
                                page: 'all',
                                search: 'none'
                            }
                        }
                    }
                ]
            },
            {
                extend: 'collection',
                text: 'Export Filtered Records',
                className: 'btn btn-select mb-2',
                buttons: [
                    {
                        extend: 'copy',
                        className: 'btn btn-select-option'
                    },
                    {
                        extend: 'excel',
                        title: 'Data export',
                        className: 'btn btn-select-option'

                    },
                    {
                        extend: 'csv',
                        title: 'Data export',
                        className: 'btn btn-select-option'
                    }
                ]
            },
            {
                extend: 'collection',
                text: 'Export Current Page',
                className: 'btn btn-select mb-2',
                buttons: [
                    {
                        extend: 'copy',
                        className: 'btn btn-select-option',
                        exportOptions: {
                            rows: ':visible'
                        }
                    },
                    {
                        extend: 'excel',
                        title: 'Data export',
                        className: 'btn btn-select-option',
                        exportOptions: {
                            rows: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        title: 'Data export',
                        className: 'btn btn-select-option',
                        exportOptions: {
                            rows: ':visible'
                        }
                    }
                ]
            },
            {
                extend: 'colvis',
                text: 'Select Columns',
                className: 'btn btn-select mb-2',
                columns: ':not(.noVis)',
                postfixButtons: ['colvisRestore']
            }
        ],
        language: {
            buttons: {
                colvisRestore: "Select All"
            }
        }
    });
}


//function to format percentages
function formatPercentage() {
    return function (data, type, row) {
        if (data == null) {
            return "N/A";
        } else {
            return Math.round(data * 100) + '%';
        }
    }; 
}

//function to deal with null numbers
function formatNumber() {
    return function (data, type, row) {
        if (data == null) {
            return "N/A";
        } else {
            return Math.round(data,2);
        }
    };
}

//jQuery.extend(jQuery.fn.dataTableExt.oSort, {
//    "signed-num-pre": function (a) {
//        if (a == "N/A") {
//            return -100;
//        } else {
//            return a;
//        }
        
//    },

//    "signed-num-asc": function (a, b) {
//        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
//    },

//    "signed-num-desc": function (a, b) {
//        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
//    }
//});