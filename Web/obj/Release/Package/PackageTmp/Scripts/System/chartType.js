window.chartColors2 = {
    red: 'rgb(237, 85, 101)',
    orange: 'rgb(248, 172, 89)',
    bg_3: 'rgb(248, 172, 89)',
    bg_2: 'rgb(26, 179, 148)',
    bg_1: 'rgb(28, 132, 198)',
    bg_4: 'rgb(35, 198, 200)',
    bg_5: 'rgb(209, 218, 222)',
    bg_6: 'rgb(180, 185, 135)',
    bg_7: 'rgb(232, 135, 237)',  
    bg_8: 'rgb(198, 105, 28)',    
    bg_9: 'rgb(89, 182, 240)',        
    gray: 'rgb(228, 228, 228)',
    
};

window.chartColors = {
    //bg_1: 'rgb(36, 92, 143)',
    bg_1: 'rgb(47, 64, 80)',
    //bg_2: 'rgb(66, 112, 193)',
    bg_2: 'rgb(28, 132, 198)',
    bg_3: 'rgb(90, 153, 211)',
    bg_4: 'rgb(98, 98, 98)',
    bg_5: 'rgb(163, 163, 163)',    
    bg_6: 'rgb(180, 185, 135)',
    bg_7: 'rgb(198, 28, 161)',
    bg_8: 'rgb(198, 105, 28)',  
    bg_9: 'rgb(89, 182, 240)',    
}

Chart.plugins.register({
    afterDraw: function (chart) {
        if (chart.data.datasets.length === 0) {            
            var ctx = chart.chart.ctx;
            var width = chart.chart.width;
            var height = chart.chart.height
            chart.clear();

            ctx.save();
            ctx.textAlign = 'center';
            ctx.textBaseline = 'middle';
            ctx.font = "16px normal 'open sans', 'Helvetica Neue', Helvetica, Arial, sans-serif";
            ctx.fillText('Sin información', width / 2, height / 2);
            ctx.restore();
        }
    }
});


var color = Chart.helpers.color;
Chart.defaults.global.legend.labels.usePointStyle = true;
function renderChar(paramOptions) {
    var urlChart = paramOptions.urlChart;
    var paramsChart = paramOptions.paramsChart;    
    var typChart = paramOptions.typChart;
    var isVertical = (paramOptions.isVertical != undefined) ? (paramOptions.isVertical) ? 'bar' : 'horizontalBar' : 'bar';    
    var container = paramOptions.container;
    var titleDisplay = paramOptions.titleDisplay;
    var titleChart= paramOptions.titleChart;
    var titlePosition = paramOptions.titlePosition;            
    var legendPosition = paramOptions.legendPosition;
    var ledendDisplay = (paramOptions.ledendDisplay != undefined) ? paramOptions.ledendDisplay : false;
    var viewAxisX = (paramOptions.viewAxis != undefined) ? paramOptions.viewAxis.x : true;
    var viewAxisY = (paramOptions.viewAxis != undefined) ? paramOptions.viewAxis.y : true;
    var labelX = (paramOptions.viewAxis != undefined) ? paramOptions.viewAxis.labelX : '';
    var labelY = (paramOptions.viewAxis != undefined) ? paramOptions.viewAxis.labelY : '';

    this.chart;

    var chartBarData = {
        labels: [],
        datasets: [],
        borderWidth: 0        
    };

    //var paramsChart = { nTypeChart: typChart };    

    function renderBarChart() {

        var isStacked = (typChart === 3) ? true : false;

        var ctx = document.getElementById(container).getContext('2d');
        var chartBar = new Chart(ctx, {
            type: isVertical,
            data: chartBarData,
            options: {
                title: {
                    display: titleDisplay,
                    text: titleChart,
                    position: titlePosition
                },
                tooltips: {
                    mode: 'index',
                    intersect: false
                },
                responsive: true,
                scales: {
                    xAxes: [{
                        stacked: isStacked,
                        barThickness: (isStacked) ? 40: 12,
                        ticks: {
                            min: 0,
                            fontSize: 11,
                            userCallback: function (label, index, labels) {                               
                                if (isVertical == 'horizontalBar') {
                                    if (Math.floor(label) === label) {
                                        return label;
                                    }
                                } else { return label;}
                            }
                        },
                        gridLines: {
                            display: viewAxisX,
                        },
                        scaleLabel: {
                            display: true,
                            labelString: labelX,
                            fontSize:'12'
                        }
                    }],
                    yAxes: [{
                        stacked: isStacked,
                        barThickness: (isStacked) ? 40 : 12,
                        ticks: {
                            min: 0,
                            fontSize: 11,
                            userCallback: function (label, index, labels) {
                                if (isVertical != 'horizontalBar') {
                                    if (Math.floor(label) === label) {
                                        return label;
                                    }
                                } else { return label; }
                            }
                        },
                        gridLines: {
                            display: viewAxisY,
                        },
                        scaleLabel: {
                            display: true,
                            labelString: labelY,  
                            fontSize: '12'
                        }
                    }]
                },
                legend: {
                    display: ledendDisplay,
                    labels: {
                        padding: 14,
                        boxWidth: 16
                    },
                    position: legendPosition
                }
            }
        });

        chart = chartBar;              
    }

    function renderPieChart() {
        var ctx = document.getElementById(container).getContext('2d');
        var chartBar = new Chart(ctx, {
            type: 'pie',
            data: chartBarData,
            options: {
                title: {
                    display: titleDisplay,
                    text: titleChart,
                    position: titlePosition
                },
                responsive: true,   
                maintainAspectRatio: false,
                aspectRatio: 1,
                legend: {
                    display: ledendDisplay,
                    labels: {
                        padding: 14,
                        boxWidth: 16
                    },
                    position: legendPosition
                }
            }
        });

        chart = chartBar;
    }       

    function renderDoughnutChart() {
        var ctx = document.getElementById(container).getContext('2d');
        var chartBar = new Chart(ctx, {
            type: 'doughnut',
            data: chartBarData,
            options: {
                //title: {
                //    display: titleDisplay,
                //    text: titleChart,
                //    position: titlePosition
                //},                
                responsive: true,
                maintainAspectRatio: false,
                cutoutPercentage: 54,//74,
                aspectRatio: 1,
                legend: {
                    display: ledendDisplay,
                    labels: {
                        x:10,
                        padding: 8,// 14,
                        boxWidth:0,// 16
                    },                    
                    position: legendPosition,
                    align: 'start'

                },
             
                //legendCallback: function (chart) {
                //    var text = [];
                //    text.push('<ul class="' + chart.id + '-legend">');
                //    for (var i = 0; i < chart.data.datasets[0].data.length; i++) {
                //        text.push('<li><span style="background-color:' + chart.data.datasets[0].backgroundColor[i] + '">');
                //        if (chart.data.labels[i]) {
                //            text.push(chart.data.labels[i]);
                //        }
                //        text.push('</span></li>');
                //    }
                //    text.push('</ul>');
                //    return text.join("");
                //},

            }
        });       

        chart = chartBar;
        //$("#chartjs-legend").html(chartBar.generateLegend());

        //$("#chartjs-legend").on('click', "li", function () {
        //    chartBar.data.datasets[0].data[$(this).index()] += 50;
        //    chartBar.update();
        //  //  console.log('legend: ' + data.datasets[0].data[$(this).index()]);
        //});
        //$('#chart_6').on('click', function (evt) {
        //    var activePoints = myChart.getElementsAtEvent(evt);
        //    var firstPoint = activePoints[0];
        //    if (firstPoint !== undefined) {
        //        console.log('canvas: ' + data.datasets[firstPoint._datasetIndex].data[firstPoint._index]);
        //    } else {
        //        myChart.data.labels.push("New");
        //        myChart.data.datasets[0].data.push(100);
        //        myChart.data.datasets[0].backgroundColor.push("red");
        //        myChart.options.animation.animateRotate = false;
        //        myChart.options.animation.animateScale = false;
        //        myChart.update();
        //        $("#chartjs-legend").html(myChart.generateLegend());
        //    }
        //});

        //generateLegend();
        //document.getElementById('js-legend').innerHTML = "This dynamically generated line of text should be within chart-container but it is incorrect"
    }

    function renderPolarChart() {
        var ctx = document.getElementById(container).getContext('2d');
        var chartBar = new Chart(ctx, {
            type: 'polarArea',
            data: chartBarData,
            options: {
                title: {
                    display: titleDisplay,
                    text: titleChart,
                    position: titlePosition
                },
                responsive: true, 
                maintainAspectRatio: false,                
                aspectRatio: 1,
                legend: {
                    display: ledendDisplay,
                    labels: {
                        padding: 14,
                        boxWidth: 16
                    },
                    position: legendPosition
                },
                scale: {
                    ticks: {
                        beginAtZero: false
                    },
                    reverse: false
                },
                animation: {
                    animateRotate: true,
                    animateScale: true
                }
            }
        });

        chart = chartBar; 
    }

    function createData() {

        $.ajax({
            url: urlChart,
            contentType: 'application/json',
            data: paramsChart,
            success: function (data) {
                var dataChart = data.cb;
                if (dataChart.length != 0) {

                    $.each(dataChart[0].sLabels, function (key, val) {
                        chartBarData.labels.push(val);
                    });

                    $.each(dataChart, function (key, val) {                    
                        var newDataSet = {
                            label: val.sSerie,
                            backgroundColor: [],//window.chartColors['bg_' + val.nIdSerie],                            
                            data: []
                        }
                    
                        for (var i = 0; i < val.nValSerie.length; i++) {
                            newDataSet.data.push(val.nValSerie[i]);
                        
                            if (typChart === 1 || typChart === 2 || typChart === 3) {
                                var newColor = window.chartColors2['bg_' + val.nIdSerie];                                
                                //newColor = color(newColor).alpha(0.75).rgbString()                                                        
                                newDataSet.borderWidth = 1;
                                newDataSet.borderColor = newColor;
                                newDataSet.backgroundColor.push(newColor);

                            } else {

                                if (typChart === 4 || typChart === 5 || typChart === 6) {
                                    var newColor = window.chartColors2['bg_' + (i + 1)];
                                    //newColor = color(newColor).alpha(0.8).rgbString()
                                    if (typChart != 6) {
                                        newDataSet.borderColor = window.chartColors2.gray;
                                        newDataSet.borderWidth = 3;
                                    }
                                    newDataSet.backgroundColor.push(newColor);
                                }
                            }
                        }                                                               

                        chartBarData.datasets.push(newDataSet);

                    });

                }
                switch (typChart) {
                    case 1:
                    case 2:
                    case 3:
                        renderBarChart();
                        break;
                    case 4:
                        renderPieChart();
                        break;
                    case 5:
                        renderDoughnutChart();
                        
                        break;
                    case 6:
                        renderPolarChart();
                        break;
                    default: break;

                }                
            },
            error: function () {
                console.log("No se ha podido obtener la información");
            }
        });

    }


    function renderPrueba() {
        var chart = new Chart('chart_6', {
            type: 'doughnut',
            data: {
                labels: ['Etronin Home Appliances Service & trading Pte Ltd', 'Giant'],
                datasets: [{
                    data: [30, 70],
                    backgroundColor: ['#2196f3', '#4caf50']
                }]
            },
            options: {
                responsive: false,
                legend: {
                    display: true,
                    position: 'right',
                    onClick: null
                },
            }
        });
    }

    function TortaPrueba() {
        var ctx = document.getElementById('chart_5').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['800000', '950000', '700000', '500000', '580000', '99000'],
                datasets: [{
                    label: '# of Votes',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }

   
    createData();
   // TortaPrueba();
}