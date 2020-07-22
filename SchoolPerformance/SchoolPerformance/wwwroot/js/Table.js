

function loadTable(table, columns) {
    table.DataTable({
        ajax: {
            url: "/Table/OnGet",
            dataType: 'json',
            type: "POST",
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
    return function (data) {
        if (data == null) {
            return "N/A";
        } else {
            return Math.round(data * 100) + '%';
        }
    }; 
}

//function to deal with null numbers
function formatNumber() {
    return function (data) {
        if (data == null) {
            return "N/A";
        } else {
            return data;
        }
    };
}

//Sort numbers and percentages putting N/A values last
//the datatables precentage sorting plugin was
//modified for my own purposes
//https://cdn.datatables.net/plug-ins/1.10.21/sorting/percent.js

jQuery.extend(jQuery.fn.dataTableExt.oSort, {

    "sort-num-asc": function (a, b) {

        //if the first row is N/A the second row b should come first
        if (a == "N/A") {

            return 1
        }

        //if the second row is N/A it should appear after the previous row
        if (b == "N/A") {
            return -1;
        }

        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "sort-num-desc": function (a, b) {
        if (a == "N/A") {

            return 1;
        }

        if (b == "N/A") {
            return -1;
        }
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    },

    "sort-percent-asc": function (a, b) {

        //if the first row is N/A the second row b should come first
        if (a == "N/A") {

            return 1;
        }

        //if the second row is N/A it should appear after the previous row
        if (b == "N/A") {
            return -1;
        }

        //Remove percentage sign from text
        var x = (a == "100%") ? 100 : a.replace(/%/, "");
        var y = (b == "100%") ? 100 : b.replace(/%/, "");

        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    },

    "sort-percent-desc": function (a, b) {
        if (a == "N/A") {

            return 1;
        }

        if (b == "N/A") {
            return -1;
        }

        var x = (a == "100%") ? 100 : a.replace(/%/, "");
        var y = (b == "100%") ? 100 : b.replace(/%/, "");

        return ((x < y) ? 1 : ((x > y) ? -1 : 0));
    }
});
