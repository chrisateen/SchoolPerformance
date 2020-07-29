﻿
//Creates footer th tags 
//depending on the number of cols in a table
function createFooterHTML(cols) {
    var thTags = "";
    for (var col in cols) {
        thTags += "<th></th>";
    }
    return thTags;
}

function loadTable(table, columns) {
    table.DataTable({
        ajax: {
            url: "/Tables/GetResultsAll",
            dataType: 'json',
            type: "POST",
        },
        deferRender: true,
        columns: columns,
        columnDefs: [
            {
                //Make URN and LEAESTAB columns hidden
                targets: [0, 1],
                className: 'noVis',
                visible: false
            }
        ],
        lengthMenu: [
            [10, 25, 50, 100, -1],
            [10, 25, 50, 100, "All"]
        ],
        //Order by school name
        order: [[2, "asc"]],
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
        },
        footerCallback: addFooterData(columns),
        rowCallback: highlightCell("ptfsM6CLA1A")

    });
}


//function to add the national data to the table footer
function addFooterData(columns) {

    return function () {

        var api = this.api();

        var response = this.api().ajax.json();

        if (response) {

            //Loop through each column in the table
            //and get the footer data
            for (i = 0; i < columns.length; i++) {

                var colName = columns[i]['data'];
                var data = response['national'][colName];
                
                //Get the type to check if it is a percentage column
                var colType = columns[i]['type'];

                //Convert decimal to percentage
                if (colType == "sort-percent") {
                    data = Math.round(data * 100) + '%';
                }
                
                $(api.column(i).footer()).html(data);
            }
        }

    }
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

//function to highlight a cell value that is greater than or equal to the national figures
function highlightCell(colName) {

    return function (row, data) {

        var response = this.api().ajax.json();

        //Get the national figure
        var national = response['national'][colName];

        //Compare data to national
        if (data[colName] >= national) {

            $('td:eq(1)', row)
                .html(Math.round(data[colName] * 100)
                    + "% " + '<span class="fas fa-star"></span> ');
        }
    }
}

//Sort numbers and percentages putting N/A values last
//the datatables percentage sorting plug-in was
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
        var x = parseInt((a == "100%") ? 100 : a.replace(/%/, ""));
        var y = parseInt((b == "100%") ? 100 : b.replace(/%/, ""));

        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    },

    "sort-percent-desc": function (a, b) {
        if (a == "N/A") {

            return 1;
        }

        if (b == "N/A") {
            return -1;
        }

        var x = parseInt((a == "100%") ? 100 : a.replace(/%/, ""));
        var y = parseInt((b == "100%") ? 100 : b.replace(/%/, ""));

        return ((x < y) ? 1 : ((x > y) ? -1 : 0));
    }
});


