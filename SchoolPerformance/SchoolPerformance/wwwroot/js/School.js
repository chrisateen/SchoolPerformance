/*
 * Get the index to use to retrieve data for the Charts
*/
function getDataIndex(index) {

    var dataIndex = {
        "Progress8": ["p8MEA", "p8MEA_FSM6CLA1A", "p8MEA_NFSM6CLA1A"],
        "Attainment8": ["atT8SCR", "atT8SCR_FSM6CLA1A", "atT8SCR_NFSM6CLA1A"],
        "Above4": ["ptL2BASICS_94", "ptfsM6CLA1ABASICS_94", "ptnotfsM6CLA1ABASICS_94"],
        "Above5": ["ptL2BASICS_95", "ptfsM6CLA1ABASICS_95", "ptnotfsM6CLA1ABASICS_95"]
    };

    return dataIndex[index];
}

/*
 * Function to convert decimal to percentage
*/
function convertToPercent(index, val) {

    if (index == "Above4" || index == "Above5") {

        return Math.round(val * 100) + "%";

    }

    return val;

}


/*
* Create the data and labels to be used for the chart
* any null result is ignored
*/
function dataForChart(schoolData, nationalData) {

    var possibleLables = ['All', 'Disadvantaged', 'Non Disadvantaged'];

    //Data to be used for the chart
    res = {
        school: [],
        national: [],
        labels: []
    }

    //Loop through the data and null result should be ignored
    for (var i = 0; i < schoolData.length; i++) {

        if (schoolData[i] != null) {

            res["school"].push(schoolData[i]);
            res["national"].push(nationalData[i]);
            res["labels"].push(possibleLables[i]);
        }

    }

    return res;
}

/*
 * Function that returns a suggested min and max Y axis value
 * depending on the chart being rendered
*/
function setMaxYValue(dataId) {

    if (dataId == "Above5" || dataId == "Above4") {
        return 1;
    } else if (dataId == "Attainment8") {
        return 90;
    } else {
        return 0.5;
    }
}

function getChart(dataId, chartData, ctx) {

    new Chart(ctx, {
        type: 'bar',
        data: {
            datasets: [{
                label: 'School',
                data: chartData["school"],
                backgroundColor: "rgb(0, 123, 255)",
                borderColor: "rgb(0, 123, 255)"
            },
            {
                label: 'National',
                data: chartData["national"],
                backgroundColor: 'rgb(206,212,218)',
                borderColor: 'rgb(206,212,218)'
            }],
            labels: chartData["labels"]
        },

        options: {
            scales: {
                yAxes: [{
                    scaleLabel: {
                        display: true
                    },
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: setMaxYValue(dataId),
                        callback: function (value) {
                            return convertToPercent(dataId, value);
                        }
                    }
                }]
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {

                        var label = data.datasets[tooltipItem.datasetIndex].label

                        return label + " " + convertToPercent(dataId, tooltipItem.yLabel);
                    }
                }
            },
            plugins: {
                datalabels: {
                    color: 'rgb(0,0,0)',
                    anchor: 'end',
                    align: 'top',
                    formatter: function (value) {
                        return convertToPercent(dataId, value);
                    }
                }
            }
        },
        plugins: [{
            beforeInit: function (chart) {
                chart.legend.afterFit = function () {
                    this.height = this.height + 18;
                };

            }
        }]
    });

}

