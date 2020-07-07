
// Get the performance measure selected from the dropdown list
function getMeasure() {

    return document.getElementById("measure").value;
}

/*
Generates the x and y data for the scatter chart
and generate the school names for the tooltips
*/
function generateData(index, resultData) {

    //Store all the x and y points for the ScatterChart
    var chartData = []

    //Store all the school names for the ScatterChart tooltips
    var schoolNames = []

    for (let i = 0, len = resultData.length; i < len; i++) {

        //Exclude schools that do not have a result
        if (resultData[i][index] !== null) {

            chartData.push({

                x: resultData[i]["ptfsM6CLA1A"],
                y: resultData[i][index]

            });

            schoolNames.push(resultData[i]["schname"]);
        }
    }

    return [chartData, schoolNames];
}

/*
 * Get the index to use to retrive data for the ScatterChart
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

//Draws the scatterChart
function graph(yAxisLabel, data, schools) {

    var ctx = document.getElementById('Scatterplot').getContext('2d');

    window.scatter = new Chart(ctx, {
        type: 'scatter',
        data: {
            labels: schools,
            datasets: [{
                data: data,
                backgroundColor: "rgba(255, 99, 132, 0.2)",
                borderColor: "rgba(255, 99, 132)"

            }]
        },
        options: {

            legend: {
                display: false
            },

            scales: {
                xAxes: [{
                    type: 'linear',
                    position: 'bottom',
                    scaleLabel: {
                        display: true,
                        labelString: 'Percentage disadvantaged pupils at the end of KS4'
                    },
                    ticks: {
                        // Change x axis to percentage
                        callback: function (value, index, values) {
                            return Math.round(value * 100) + '%';
                        }
                    }
                }],
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: yAxisLabel,
                    }
                }]
            },

            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {
                        var label = data.labels[tooltipItem.index];
                        return label + ': (' + Math.round(tooltipItem.xLabel * 100) + '% , '
                            + tooltipItem.yLabel + ')';
                    }
                }
            }
        }
    });
};