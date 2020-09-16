//Convert string to int
function TryParseInt(str) {

    //checks string is not null
    if (str !== null) {
        if (str.length > 0) {

             //checks if the string is an integer or not
            if (!isNaN(str)) {

                return parseInt(str);

            } else {

            }
        }
    }
    return null;
}

// Get the performance measure selected from the drop down list
function getMeasure() {

    return document.getElementById("measure").value;
}

// Get the urn the user searched for
function getSchool() {

    //Attempt to convert the URN to an integer
    var urn = TryParseInt(document.getElementById("inputSchool").value);

    //Display alert if user enters anything other than a number
    if (urn === null) {

        alert("Invalid URN inputted");

    } else {

        return urn;
    } 
}


/*
Generates the x and y data for the scatter chart
and generate the school names for the tooltips
*/
function generateData(index, resultData, urn) {

    //Store all the x and y points for the ScatterChart
    var chartData = []

    //Store all the school names for the ScatterChart tooltips
    var schoolNames = []

    //Stores whether the school urn is found
    var foundSchool = false;

    for (let i = 0, len = resultData.length; i < len; i++) {

        //Exclude schools that do not have a result 
        if (resultData[i][index] !== null) {

            if (resultData[i]["urn"] === urn) {

                foundSchool = true;

                //First item in the list will be data 
                //for the school searched for
                chartData.unshift({

                    x: resultData[i]["ptfsM6CLA1A"],
                    y: resultData[i][index]

                });

                schoolNames.unshift(resultData[i]["schname"]);

            } else {

                chartData.push({

                    x: resultData[i]["ptfsM6CLA1A"],
                    y: resultData[i][index]

                });

                schoolNames.push(resultData[i]["schname"]);
            }

            //alert printed if school exist but has no data for chosen measure
        } else if (resultData[i]["urn"] === urn) {

            alert("Selected data does not exist for the selected school. Please search for another school");

            return null;
        }
    }

    //Checks if the urn entered could be found
    if (urn !== null && foundSchool === false) {

        alert("School could not be found or school has no disadvantaged pupil data available. Please try another school");

        return null;

    } else {

        return [chartData, schoolNames];
    }
}

/*
 * Get the index to use to retrieve data for the ScatterChart
*/ 
function getDataIndex(index) {

    var dataIndex = {
        "Progress 8 score": "p8MEA",
        "Attainment 8 score": "atT8SCR",
        "Attainment 8 score disadvantaged pupils": "atT8SCR_FSM6CLA1A",
        "Attainment 8 Score non-disadvantaged pupils": "atT8SCR_NFSM6CLA1A",
        "Progress 8 score disadvantaged Pupils": "p8MEA_FSM6CLA1A",
        "Progress 8 score non-disadvantaged Pupils": "p8MEA_NFSM6CLA1A",
        "Percentage of pupils achieving grade 9-4 in English and Maths": "ptL2BASICS_94",
        "Percentage of disadvantaged pupils achieving grade 9-4 in English and Maths": "ptfsM6CLA1ABASICS_94",
        "Percentage of non-disadvantaged pupils achieving grade 9-4 in English and Maths": "ptnotfsM6CLA1ABASICS_94",
        "Percentage of pupils achieving grade 9-5 in English and Maths": "ptL2BASICS_95",
        "Percentage of disadvantaged pupils achieving grade 9-5 in English and Maths": "ptfsM6CLA1ABASICS_95",
        "Percentage of non-disadvantaged pupils achieving grade 9-5 in English and Maths": "ptnotfsM6CLA1ABASICS_95"
    };

    return dataIndex[index];
}

//Set the y axis ticks to percentage or number
//depending on the data being displayed
function setTicks(yAxisLabel) {

    if (yAxisLabel.search("Percentage") > -1) {

        return function (value) {
            return Math.round(value * 100) + '%';
        }

    } else {

        return function (value) {
            return value;
        }
    }

}

//Sets the point lables y value to percentage or number
//depending on the data being displayed
function setYAxisLabel(yAxisLabel) {

    if (yAxisLabel.search("Percentage") > -1) {

        return function (tooltipItem, data) {

            var dataset = data.datasets[tooltipItem.datasetIndex];
            var index = tooltipItem.index;
            var label = dataset.labels[index];
            return label + ': (' + Math.round(tooltipItem.xLabel * 100) + '% , '
                            + Math.round(tooltipItem.yLabel * 100) + '%)';
        }

    } else {

        return function (tooltipItem, data) {

            var dataset = data.datasets[tooltipItem.datasetIndex];
            var index = tooltipItem.index;
            var label = dataset.labels[index];

            return label + ': (' + Math.round(tooltipItem.xLabel * 100) + '% , '
                + tooltipItem.yLabel + ')';
        }
    }

}

//Draws the scatterChart
function graph(yAxisLabel, data, schools) {

    var ctx = document.getElementById('Scatterplot').getContext('2d');

    window.scatter = new Chart(ctx, {
        type: 'scatter',
        data: {
            datasets: [{
                label: "All Schools",
                //School names for tooltips
                labels: schools.slice(1, schools.length - 1), 
                data: data.slice(1, data.length - 1),
                backgroundColor: "rgba(255, 99, 132, 0.2)",
                borderColor: "rgb(255, 99, 132)",
                order:2

            },
                {
                    label: schools[0],
                    data: [data[0]],
                    labels: [schools[0]],
                    backgroundColor: "rgba(64, 74, 73, 2)",
                    borderColor: "rgb(64, 74, 73)",
                    pointRadius: 5,
                    order: 1

                }]
        },
        options: {

            legend: {
                display: true
            },

            scales: {
                xAxes: [{
                    type: 'linear',
                    position: 'bottom',
                    scaleLabel: {
                        display: true,
                        labelString: 'Percentage of disadvantaged pupils at the end of KS4'
                    },
                    ticks: {
                        callback: function (value) {
                            return Math.round(value * 100) + "%";
                        }
                    }
                }],
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: yAxisLabel,
                    },
                    ticks: {}
                }]
            },

            tooltips: {
                callbacks: {
                    label: setYAxisLabel(yAxisLabel)
                }
            },

            plugins: {
                zoom: {
                    pan: {
                        enabled: true,
                        mode: 'xy'
                    },
                    zoom: {
                        enabled: true,
                        mode: 'xy'
                    }
                }
            }
        }
    });
};


//Updates chart based on what measure a user has selected
function updateChart() {

    //Get the new y axis label based on option selected by user
    var newyAxisLabel = getMeasure();

    //Get the index to use to retrieve data for the ScatterChart
    var newDataIndex = getDataIndex(newyAxisLabel);

    //Get the new x and y points and school names for the chart
    var newScatterData = generateData(newDataIndex, results, selectedSchool);

    //Only update the chart if the highlighted school can be found and therefore 
    // data is generated
    if (newScatterData !== null) {

        var newSchools = newScatterData[1];
        var newData = newScatterData[0];

        //Update the school names labels list
        window.scatter.data.datasets[0].labels = newSchools.slice(1, newSchools.length - 1);

        //Update the x and y data for the chart
        window.scatter.data.datasets[0].data = newData.slice(1, newData.length - 1);

        //Update the data for the school data point to be in a different colour
        window.scatter.data.datasets[1].data = [newData[0]];
        window.scatter.data.datasets[1].label = newSchools[0];
        window.scatter.data.datasets[1].labels = [newSchools[0]];

        //Update the y axis label and ticks
        window.scatter.options.scales.yAxes[0].scaleLabel.labelString = newyAxisLabel;
        window.scatter.options.scales.yAxes[0].ticks.callback = setTicks(newyAxisLabel);

        //Update the tooltip labels
        window.scatter.options.tooltips.callbacks.label = setYAxisLabel(newyAxisLabel);


        resetScatterZoom();

        window.scatter.update();

    }

}

//Function to update the school highlighted on the chart
function updateHighligtedSchool() {

    //Gets the URN the user has searched for as an integer
    var newSchool = getSchool();


    if (newSchool !== undefined) {

        //Gets the y axis label of the current data been displayed
        var yAxisLabel = window.scatter.options.scales.yAxes[0].scaleLabel.labelString;

        var DataIndex = getDataIndex(yAxisLabel);

        //Generate the new data for the chart
        var newScatterData = generateData(DataIndex, results, newSchool);

        //Only update the scatterChart if the URN could be found
        if (newScatterData !== null) {

            selectedSchool = newSchool;

            var newSchools = newScatterData[1];
            var newData = newScatterData[0];

            //Update the school names labels list
            window.scatter.data.datasets[0].labels = newSchools.slice(1, newSchools.length - 1);

            //Update the x and y data for the chart
            window.scatter.data.datasets[0].data = newData.slice(1, newData.length - 1);

            //Update the data for the school data point to be in a different colour
            window.scatter.data.datasets[1].data = [newData[0]];
            window.scatter.data.datasets[1].label = newSchools[0];
            window.scatter.data.datasets[1].labels = [newSchools[0]];

            resetScatterZoom();

            window.scatter.update();

        }

    }

}

//Reset the scatter chart zoom
function resetScatterZoom() {
    window.scatter.resetZoom();
}

