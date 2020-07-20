﻿function loadTable(table,data, columns) {
    table.DataTable({
        data: data,
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
                    { extend: 'copy', className: 'btn btn-select-option' },
                    { extend: 'excel', className: 'btn btn-select-option' },
                    { extend: 'csv', className: 'btn btn-select-option' }
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