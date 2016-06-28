var chart1Data = [];
var chart2Data = [];
var chart3Data = [];
var chart4Data = [];
var chart5Data = [];
var setValueTagKey = '';

$(function () {

    $.get('../api/RealTimeDisplay?sideMenuId=' + $('#sideMenuId').text() + '&userId=' + $('#userId').attr('data-userid'), function(machineInfo) {
        if (machineInfo.Tags[0]) {
            setInterval(function() {
                $.get('../api/RealTimeDisplay', function (data) {
                    $('#temp').text(data[machineInfo.Tags[0].Key].Value);
                    $('#preUp').text(data[machineInfo.Tags[1].Key].Value);
                    $('#preLeft').text(data[machineInfo.Tags[2].Key].Value);
                    $('#preRight').text(data[machineInfo.Tags[3].Key].Value);
                    $('#preDown').text(data[machineInfo.Tags[4].Key].Value);
                    $('#torF').text(data[machineInfo.Tags[5].Key].Value);
                    $('#torB').text(data[machineInfo.Tags[6].Key].Value);
                    $('#temp1').text(data[machineInfo.Tags[7].Key].Value);
                    $('#preUp1').text(data[machineInfo.Tags[8].Key].Value);
                    $('#preLeft1').text(data[machineInfo.Tags[9].Key].Value);
                    $('#preRight1').text(data[machineInfo.Tags[10].Key].Value);
                    $('#preDown1').text(data[machineInfo.Tags[11].Key].Value);
                    $('#torF1').text(data[machineInfo.Tags[12].Key].Value);
                    $('#torB1').text(data[machineInfo.Tags[13].Key].Value);

                    chart1Data.push(Number(data[machineInfo.Tags[14].Key].Value));
                    chart2Data.push(Number(data[machineInfo.Tags[15].Key].Value));
                    chart3Data.push(Number(data[machineInfo.Tags[16].Key].Value));
                    chart4Data.push(Number(data[machineInfo.Tags[17].Key].Value));
                    chart5Data.push(Number(data[machineInfo.Tags[18].Key].Value));

                    $('#ValueB').attr('data-TagKey', machineInfo.Tags[19].Key);
                    $('#ValueB1').attr('data-TagKey', machineInfo.Tags[20].Key);
                    $('#lblValueB').text(Number(data[machineInfo.Tags[19].Key].Value));
                    $('#lblValueB1').text(Number(data[machineInfo.Tags[20].Key].Value));
                });
            }, 1000);

            $('#Chart1').highcharts({
                chart: {
                    type: 'spline',
                    
                    animation: Highcharts.svg, // don't animate in old IE
                    backgroundColor:'',
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
                    text: '起重机' + machineInfo.Tags[14].Name,
                },
                xAxis: {
                    type: 'datetime',
                    tickPixelInterval: 150
                },
                yAxis: {
                    title: {
                        text: machineInfo.Tags[14].Scale
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
                    name: machineInfo.Tags[14].Name,
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

        if (machineInfo.Tags[15]) {
            $('#Chart2').highcharts({
                    chart: {
                        type: 'gauge',
                        plotBackgroundColor: null,
                        plotBackgroundImage: null,
                        plotBorderWidth: 0,
                        plotShadow: false,
                        backgroundColor: '',
                    },

                    title: {
                        text:''// '起重机' + machineInfo.Tags[15].Name
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
                        min: machineInfo.Tags[15].MinScale,
                        max: machineInfo.Tags[15].MaxScale,

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
                            text: machineInfo.Tags[15].Name//machineInfo.Tags[15].Scale
                        },
                        plotBands: [{
                                from: Number(machineInfo.Tags[15].MinScale),
                                to: Number(machineInfo.Tags[15].MaxScale) - (Number(machineInfo.Tags[15].MaxScale) - Number(machineInfo.Tags[15].MinScale)) * 0.5,
                                color: '#55BF3B' // green
                            }, {
                                from: Number(machineInfo.Tags[15].MaxScale) - (Number(machineInfo.Tags[15].MaxScale) - Number(machineInfo.Tags[15].MinScale)) * 0.5,
                                to: Number(machineInfo.Tags[15].MaxScale) - (Number(machineInfo.Tags[15].MaxScale) - Number(machineInfo.Tags[15].MinScale)) * 0.2,
                                color: '#DDDF0D' // yellow
                            }, {
                                from: Number(machineInfo.Tags[15].MaxScale) - (Number(machineInfo.Tags[15].MaxScale) - Number(machineInfo.Tags[15].MinScale)) * 0.1,
                                to: Number(machineInfo.Tags[15].MaxScale),
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

        if (machineInfo.Tags[18]) {
            $('#Chart5').highcharts({
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false,
                    backgroundColor: '',
                },

                title: {
                    text:'' //'起重机' + machineInfo.Tags[18].Name
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
                    min: machineInfo.Tags[18].MinScale,
                    max: machineInfo.Tags[18].MaxScale,

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
                        text: machineInfo.Tags[18].Name// machineInfo.Tags[18].Scale
                    },
                    plotBands: [{
                        from: Number(machineInfo.Tags[18].MinScale),
                        to: Number(machineInfo.Tags[18].MaxScale) - (Number(machineInfo.Tags[18].MaxScale) - Number(machineInfo.Tags[18].MinScale)) * 0.5,
                        color: '#55BF3B' // green
                    }, {
                        from: Number(machineInfo.Tags[18].MaxScale) - (Number(machineInfo.Tags[18].MaxScale) - Number(machineInfo.Tags[18].MinScale)) * 0.5,
                        to: Number(machineInfo.Tags[18].MaxScale) - (Number(machineInfo.Tags[18].MaxScale) - Number(machineInfo.Tags[18].MinScale)) * 0.2,
                        color: '#DDDF0D' // yellow
                    }, {
                        from: Number(machineInfo.Tags[18].MaxScale) - (Number(machineInfo.Tags[18].MaxScale) - Number(machineInfo.Tags[18].MinScale)) * 0.1,
                        to: Number(machineInfo.Tags[18].MaxScale),
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
                function (chart) {
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
        if (machineInfo.Tags[16]) {
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
                    //title: {
                //    text: '起重机' + machineInfo.Tags[16].Name
                    //},

                    pane: [{
                        startAngle: -45,
                        endAngle: 45,
                        background: null,
                        center: ['50%', '145%'],
                        size: 300
                    }],

                    yAxis: [{
                        min: machineInfo.Tags[16].MinScale,
                        max: machineInfo.Tags[16].MaxScale,
                        minorTickPosition: 'outside',
                        tickPosition: 'outside',
                        labels: {
                            rotation: 'auto',
                            distance: 20
                        },
                        plotBands: [{
                            from: Number(machineInfo.Tags[16].MaxScale) - (Number(machineInfo.Tags[16].MaxScale) - Number(machineInfo.Tags[16].MinScale)) * 0.4,
                            to: Number(machineInfo.Tags[16].MaxScale),
                            color: '#C02316',
                            innerRadius: '100%',
                            outerRadius: '105%'
                        }],
                        pane: 0,
                        title: {
                            text: machineInfo.Tags[16].Name,
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
       
        if (machineInfo.Tags[17]) {
            $('#Chart4').highcharts({
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
                    text: '起重机' + machineInfo.Tags[17].Name
                },

                pane: [{
                    startAngle: -45,
                    endAngle: 45,
                    background: null,
                    center: ['50%', '145%'],
                    size: 300
                }],

                yAxis: [{
                    min: machineInfo.Tags[17].MinScale,
                    max: machineInfo.Tags[17].MaxScale,
                    minorTickPosition: 'outside',
                    tickPosition: 'outside',
                    labels: {
                        rotation: 'auto',
                        distance: 20
                    },
                    plotBands: [{
                        from: Number(machineInfo.Tags[17].MaxScale) - (Number(machineInfo.Tags[17].MaxScale) - Number(machineInfo.Tags[17].MinScale)) * 0.4,
                        to: Number(machineInfo.Tags[17].MaxScale),
                        color: '#C02316',
                        innerRadius: '100%',
                        outerRadius: '105%'
                    }],
                    pane: 0,
                    title: {
                        text: machineInfo.Tags[17].Name,
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
                function (chart) {
                    setInterval(function () {
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



function setValue(obj) {
    $.post('../api/RealTimeDisplay', { '': [$(obj).attr('data-TagKey'), $('#txt' + $(obj).id).val()] }, function () {
    }, function(error) { alert(error); });
}

