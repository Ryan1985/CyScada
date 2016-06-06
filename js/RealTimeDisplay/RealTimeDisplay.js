var chart1Data = [];
var chart2Data = [];
var chart3Data = [];
var setValueTagKey = '';

$(function () {
    $('.led').ClassyLED({
        type: 'number',
        //format: 'ddd:hh:mm:ss',
        color: '#eee',
        backgroundColor: '#000',
        value:"4567",
        size: 3
    });

    $.get('../api/RealTimeDisplay?sideMenuId=' + $('#sideMenuId').text() + '&userId=' + $('#userId').attr('data-userid'), function(machineInfo) {
        if (machineInfo.Tags[0]) {
            setInterval(function() {
                $.get('../api/RealTimeDisplay', function(data) {
                    chart1Data.push(Number(data[machineInfo.Tags[0].Key].Value));
                    chart2Data.push(Number(data[machineInfo.Tags[1].Key].Value));
                    chart3Data.push(Number(data[machineInfo.Tags[2].Key].Value));
                    $('#temp').text(data[machineInfo.Tags[0].Key].Value);
                    $('#pressure').text(data[machineInfo.Tags[1].Key].Value);
                    $('#stress').text(data[machineInfo.Tags[2].Key].Value);
                    $('#lblValue').text(data[machineInfo.Tags[3].Key].Value);
                    setValueTagKey = machineInfo.Tags[3].Key;

                });
            }, 1000);

            $('#Chart1').highcharts({
                chart: {
                    type: 'spline',
                    animation: Highcharts.svg, // don't animate in old IE
                    marginRight: 10,
                    events: {
                        load: function() {
                            var series = this.series[0];
                            setInterval(function() {
                                var x = (new Date()).getTime(); // current time
                                if (chart1Data.length > 0) {
                                    series.addPoint([x, chart1Data[0]], true, true);
                                    chart1Data.splice(0, 1);
                                }
                            }, 1000);
                        }
                    }
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: '起重机' + machineInfo.Tags[0].Name
                },
                xAxis: {
                    type: 'datetime',
                    tickPixelInterval: 150
                },
                yAxis: {
                    title: {
                        text: '摄氏度℃'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    formatter: function() {
                        return '<b>' + this.series.name + '</b><br/>' +
                            Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                            Highcharts.numberFormat(this.y, 2);
                    }
                },
                legend: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },
                series: [{
                    name: machineInfo.Tags[0].Name,
                    data: (function() {
                        // generate an array of random data
                        var data = [],
                            time = (new Date()).getTime(),
                            i;

                        for (i = -19; i <= 0; i += 1) {
                            data.push({
                                x: time + i * 1000,
                                y: 0
                            });
                        }
                        return data;
                    }())
                }]
            });
        }

        if (machineInfo.Tags[1]) {
            $('#Chart2').highcharts({
                    chart: {
                        type: 'gauge',
                        plotBackgroundColor: null,
                        plotBackgroundImage: null,
                        plotBorderWidth: 0,
                        plotShadow: false
                    },

                    title: {
                        text: '起重机' + machineInfo.Tags[1].Name
                    },
                    credits: {
                        enabled: false
                    },
                    exporting: {
                        enabled: false
                    },
                    pane: {
                        startAngle: -150,
                        endAngle: 150,
                        background: [{
                                backgroundColor: {
                                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                                    stops: [
                                        [0, '#FFF'],
                                        [1, '#333']
                                    ]
                                },
                                borderWidth: 0,
                                outerRadius: '109%'
                            }, {
                                backgroundColor: {
                                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                                    stops: [
                                        [0, '#333'],
                                        [1, '#FFF']
                                    ]
                                },
                                borderWidth: 1,
                                outerRadius: '107%'
                            }, {
                                // default background
                            
                                
                            }, {
                                backgroundColor: '#DDD',
                                borderWidth: 0,
                                outerRadius: '105%',
                                innerRadius: '103%'
                            }]
                    },

                    // the value axis
                    yAxis: {
                        min: 0,
                        max: 10000,

                        minorTickInterval: 'auto',
                        minorTickWidth: 1,
                        minorTickLength: 10,
                        minorTickPosition: 'inside',
                        minorTickColor: '#666',

                        tickPixelInterval: 30,
                        tickWidth: 2,
                        tickPosition: 'inside',
                        tickLength: 10,
                        tickColor: '#666',
                        labels: {
                            step: 2,
                            rotation: 'auto'
                        },
                        title: {
                            text: 'Kg'
                        },
                        plotBands: [{
                                from: 0,
                                to: 6200,
                                color: '#55BF3B' // green
                            }, {
                                from: 6200,
                                to: 8600,
                                color: '#DDDF0D' // yellow
                            }, {
                                from: 8600,
                                to: 10000,
                                color: '#DF5353' // red
                            }]
                    },

                    series: [{
                        name: '压力',
                        data: [80],
                        tooltip: {
                            valueSuffix: '(kg)'
                        }
                    }]
                }, // Add some life
                function(chart) {
                    if (!chart.renderer.forExport) {
                        setInterval(function () {
                            if (chart2Data.length > 0) {
                                var point = chart.series[0].points[0];
                                var newVal = Number(chart2Data[0]);
                                chart2Data.splice(0, 1);
                                point.update(newVal);
                            }
                        }, 1000);
                    }
                }
            );
        }

        if (machineInfo.Tags[2]) {
            $('#Chart3').highcharts({
                    chart: {
                        type: 'gauge',
                        plotBorderWidth: 1,
                        plotBackgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF4C6'],
                                [0.3, '#FFFFFF'],
                                [1, '#FFF4C6']
                            ]
                        },
                        plotBackgroundImage: null,
                        height: 200
                    },

                    credits: {
                        enabled: false
                    },
                    exporting: {
                        enabled: false
                    },
                    title: {
                        text: '起重机' + machineInfo.Tags[2].Name
                    },

                    pane: [{
                        startAngle: -45,
                        endAngle: 45,
                        background: null,
                        center: ['50%', '145%'],
                        size: 300
                    }],

                    yAxis: [{
                        min: 0,
                        max: 10000,
                        minorTickPosition: 'outside',
                        tickPosition: 'outside',
                        labels: {
                            rotation: 'auto',
                            distance: 20
                        },
                        plotBands: [{
                            from: 8000,
                            to: 10000,
                            color: '#C02316',
                            innerRadius: '100%',
                            outerRadius: '105%'
                        }],
                        pane: 0,
                        title: {
                            text: machineInfo.Tags[2].Name,
                            y: 0
                        }
                    }],

                    plotOptions: {
                        gauge: {
                            dataLabels: {
                                enabled: false
                            },
                            dial: {
                                radius: '100%'
                            }
                        }
                    },
                    series: [{
                        data: [-20],
                        yAxis: 0
                    }]
                },
                // Let the music play
                function(chart) {
                    setInterval(function() {
                        if (chart3Data.length > 0) {
                            var point = chart.series[0].points[0];
                            var newVal = Number(chart3Data[0]);
                            chart3Data.splice(0, 1);
                            point.update(newVal);
                        }
                    }, 1000);


                });


        }


    });

  
});



function setValue() {
    $.post('../api/RealTimeDisplay', { '': [setValueTagKey, $('#txtValue').val()] }, function() {
    }, function(error) { alert(error); });
}

