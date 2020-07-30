
//Creates footer th tags 
//depending on the number of cols in a table
function createFooterHTML(cols) {
    var thTags = "";
    for (var col in cols) {
        thTags += "<th></th>";
    }
    return thTags;
}


//Create the table include url of where to get the data
function loadTable(table, columns, url) {
    table.DataTable({
        ajax: {
            url: url,
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
        rowCallback: highlightCell(columns)

    });
}


//Function to add the national data to the table footer
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

                //Format data depending on if it is a percentage column
                if (colType) {
                    data = dataFormat(data, colType);
                }

                //Add data to footer
                $(api.column(i).footer()).css("background-color", "#e0dae0").html(data);
            }
        }

    }
}


//Function to format percentages and null types
function dataFormat(data, type) {

    if (data == null) {
        return "N/A";
    }

    if (type.search('percent') != -1) {
        return Math.round(data * 100) + '%';
    }

    else {
        return data;
    }

}


//Function to highlight a cell value 
//that is greater than the national figures
function highlightCell(columns) {

    return function (row, data) {

        var response = this.api().ajax.json();

        //Loop through each column in a row
        for (var index in columns) {

            var colType = columns[index]['type'];

            //Check if it is a column that requires highlighting
            if (colType && colType.search('highlight') != -1) {

                var colName = columns[index]['data'];

                //Get the national figure
                var national = response['national'][colName];

                var htmlText;

                //Compare data to national and if the data is above national
                //include a green star icon
                if (data[colName] > national && data[colName] != null) {

                    //Format number to a percentage if column type is a percentage
                    htmlText = dataFormat(data[colName], colType);

                    //Include star in element. index - 2 as the first two columns are hidden
                    $('td:eq(' + (index - 2) + ')', row)
                        .html(htmlText + " " + '<span class="fas fa-star"></span> ');
                }
            }
        }
 
    }
}


//Sort numbers and percentages putting N/A values last
jQuery.extend(jQuery.fn.dataTableExt.oSort, {

    "sort-num-asc": sortCol('asc', 'num'),

    "highlight-sort-num-asc": sortCol('asc', 'num'),

    "sort-num-desc": sortCol('desc', 'num'),

    "highlight-sort-num-desc": sortCol('desc', 'num'),

    "sort-percent-asc": sortCol('asc', 'percent'),

    "highlight-sort-percent-asc": sortCol('asc', 'percent'),

    "sort-percent-desc": sortCol('desc', 'percent'),

    "highlight-sort-percent-desc": sortCol('desc', 'percent')
});


//Function to sort columns in ascending or descending order
//the datatables percentage sorting plug-in was
//modified for my own purposes
//https://cdn.datatables.net/plug-ins/1.10.21/sorting/percent.js
function sortCol(order, type) {

    return function (a,b) {

        //if the first row is N/A the second row b should come first
        if (a == "N/A") {

            return 1;
        }

        //if the second row is N/A the first row a should come first
        if (b == "N/A") {
            return -1;
        }

        var x;
        var y;

        //Remove percentage sign if the data type is a string with a percentage sign
        if (type == 'percent') {
            x = parseInt((a == "100%") ? 100 : a.replace(/%/, ""));
            y = parseInt((b == "100%") ? 100 : b.replace(/%/, ""));
        }
        else {
            x = a;
            y = b;
        }

        if (order == 'asc') {

            return ((x < y) ? -1 : ((x > y) ? 1 : 0));

        }

        if (order == 'desc') {

            return ((x < y) ? 1 : ((x > y) ? -1 : 0));
        }
    };
}






