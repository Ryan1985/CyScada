var chart1Data = 0;
var chart2Data = 0;
var chart3Data = 0;
var chart4Data = 0;
var chart5Data = 0;
var setValueTagKey = '';
var dataQueue = [];
var chart1;
var chart2;
var chart3;
var chart4;
var chart5;
var tagList = [];
var machineInfo = {};
var switchLock = 1;
$(function () {
    var cw = $(document).width();
    console.log(cw);
    if (cw < 600) {
        //' <div id="'+'chart-1"'+' style="'+'height:200px;"'+'></div>'
        $("#chart-1-box").html(' <div id="'+'chart-1"'+' style="'+'height:200px;"'+'></div>');
    }
    $.get('../api/RealTimeDisplay?sideMenuId=' + $('#sideMenuId').text() + '&userId=' + $('#userId').attr('data-userid'), function (data) {
        //for (var i = 0; i < machineInfo.Tags.length; i++) {
        //    tagList.push(machineInfo.Tags[i].Key);
        //}
        machineInfo = data;
        if (machineInfo.Tags[0]) {
            chart1 =new Highcharts.Chart({
                chart: {
                    type: 'spline',
                    renderTo: 'chart-1',
                    animation: Highcharts.svg,//Highcharts.svg, // don't animate in old IE
                    marginRight: 10,
                    //events: {
                    //    load: function() {
                    //        var series = this.series[0];
                    //        setInterval(function() {
                    //            var x = (new Date()).getTime(); // current time
                    //            //if (chart1Data.length > 0) {
                    //                series.addPoint([x, chart1Data], true, true);
                    //                //chart1Data.splice(0, 1);
                    //            //}
                    //        }, 1000);
                    //    }
                    //}
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: '起重机' + machineInfo.Tags[14].Name,
                },
                xAxis: {
                    type: 'datetime',
                    tickPixelInterval: 100
                },
                yAxis: {
                    min: machineInfo.Tags[14].MinScale,
                    max: machineInfo.Tags[14].MaxScale,
                    tickPositions: [0, 10,20,30,40,50,60,70,80,90,100],
                    title: {
                        text: null
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080',

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

                        for (i = -99; i <= 0; i += 1) {
                            data.push({
                                x: time + i * 1000,
                                y: null
                            });
                        }
                        return data;
                    })()
                }]
            });
        }

        if (machineInfo.Tags[15]) {
            chart2 =new Highcharts.Chart({
                    chart: {
                        type: 'gauge',
                        renderTo:'Chart2',
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
                } // Add some life
                //function(chart) {
                //    if (!chart.renderer.forExport) {
                //        setInterval(function () {
                //            //if (chart2Data.length > 0) {
                //                var point = chart.series[0].points[0];
                //                var newVal = Number(chart2Data);
                //                //chart2Data.splice(0, 1);
                //                point.update(newVal);
                //            //}
                //        }, 1000);
                //    }
                //}
            );
        }

        if (machineInfo.Tags[18]) {
            chart5 = new Highcharts.Chart({
                chart: {
                    type: 'gauge',
                    renderTo: 'Chart5',
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
            } // Add some life
                //function (chart) {
                //    if (!chart.renderer.forExport) {
                //        setInterval(function () {
                //            //if (chart5Data.length > 0) {
                //                var point = chart.series[0].points[0];
                //                var newVal = Number(chart5Data);
                //                //chart5Data.splice(0, 1);
                //                point.update(newVal);
                //            //}
                //        }, 1000);
                //    }
                //}
            );
        }
        if (machineInfo.Tags[16]) {
            chart3 = new Highcharts.Chart({
                    chart: {
                        type: 'gauge',
                        renderTo: 'Chart3',
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
                        text: '起重机' + machineInfo.Tags[16].Name
                    },

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
                }
                // Let the music play
                //function(chart) {
                //    setInterval(function() {
                //        //if (chart3Data.length > 0) {
                //            var point = chart.series[0].points[0];
                //            var newVal = Number(chart3Data);
                //            //chart3Data.splice(0, 1);
                //            point.update(newVal);
                //        //}
                //    }, 1000);


                //}
                );


        }
       
        if (machineInfo.Tags[17]) {
            chart4 = new Highcharts.Chart({
                chart: {
                    type: 'gauge',
                    renderTo: 'Chart4',
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
            }
                // Let the music play
                //function (chart) {
                //    setInterval(function () {
                //        //if (chart4Data.length > 0) {
                //            var point = chart.series[0].points[0];
                //            var newVal = Number(chart4Data);
                //            //chart4Data.splice(0, 1);
                //            point.update(newVal);
                //       // }
                //    }, 1000);


                //}
                );


        }

        refreshItemValues();

    });

  
});



function refreshDisplay(data) {
    if (data) {
       // console.log(data[machineInfo.Tags[23].Key].Name);
        //console.log('DeQueue|' +(new Date()).toLocaleString());
        //var data = dataQueue.splice(0, 1)[0];

        //console.log('splice|' +(new Date()).toLocaleString());
        //Chart1
        var series = chart1.series[0];
        var x = (new Date(data[machineInfo.Tags[14].Key].TimeStamp)).getTime();
        series.addPoint([x, Number(data[machineInfo.Tags[14].Key].Value)], true, true);
        //console.log('Chart1|' + (new Date()).toLocaleString());
        //Chart2
        var point = chart2.series[0].points[0];
        var newVal = Number(data[machineInfo.Tags[15].Key].Value);
        point.update(newVal);
        //console.log('Chart2|' +(new Date()).toLocaleString());
        //Chart3
        point = chart3.series[0].points[0];
        newVal = Number(data[machineInfo.Tags[16].Key].Value);
        point.update(newVal);
        //console.log('Chart3|' +(new Date()).toLocaleString());
        //Chart4
        point = chart4.series[0].points[0];
        newVal = Number(data[machineInfo.Tags[17].Key].Value);
        point.update(newVal);
        //console.log('Chart4|' + (new Date()).toLocaleString());

        //Chart5
        point = chart5.series[0].points[0];
        newVal = Number(data[machineInfo.Tags[18].Key].Value);
        point.update(newVal);
        //console.log('Chart5|' +(new Date()).toLocaleString());

        $('#temp').text(data[machineInfo.Tags[0].Key].Value + ' ℃');
        $('#preUp').text(data[machineInfo.Tags[1].Key].Value + ' kg');
        $('#preLeft').text(data[machineInfo.Tags[2].Key].Value + ' kg');
        $('#preRight').text(data[machineInfo.Tags[3].Key].Value + ' kg');
        $('#preDown').text(data[machineInfo.Tags[4].Key].Value + ' kg');
        $('#torF').text(data[machineInfo.Tags[5].Key].Value + ' kg');
        $('#torB').text(data[machineInfo.Tags[6].Key].Value + ' kg');
        $('#temp1').text(data[machineInfo.Tags[7].Key].Value + ' ℃');
        $('#preUp1').text(data[machineInfo.Tags[8].Key].Value + ' kg');
        $('#preLeft1').text(data[machineInfo.Tags[9].Key].Value + ' kg');
        $('#preRight1').text(data[machineInfo.Tags[10].Key].Value + ' kg');
        $('#preDown1').text(data[machineInfo.Tags[11].Key].Value + ' kg');
        $('#torF1').text(data[machineInfo.Tags[12].Key].Value + ' kg');
        $('#torB1').text(data[machineInfo.Tags[13].Key].Value + ' kg');

        //console.log('upShow|' + (new Date()).toLocaleString());
        //chart1Data = Number(data[machineInfo.Tags[14].Key].Value);
        //chart2Data = Number(data[machineInfo.Tags[15].Key].Value);
        //chart3Data = Number(data[machineInfo.Tags[16].Key].Value);
        //chart4Data = Number(data[machineInfo.Tags[17].Key].Value);
        //chart5Data = Number(data[machineInfo.Tags[18].Key].Value);



        $('#ValueFor').attr('data-TagKey', machineInfo.Tags[19].Key);
        $('#ValueFor1').attr('data-TagKey', machineInfo.Tags[20].Key);
        $('#lblValueFor').text(Number(data[machineInfo.Tags[19].Key].Value));
        $('#lblValueFor1').text(Number(data[machineInfo.Tags[20].Key].Value));

        $('#ValueB').attr('data-TagKey', machineInfo.Tags[21].Key);
        $('#ValueB1').attr('data-TagKey', machineInfo.Tags[22].Key);
        $('#lblValueB').text(Number(data[machineInfo.Tags[21].Key].Value));
        $('#lblValueB1').text(Number(data[machineInfo.Tags[22].Key].Value));
        //console.log('downShow|' +(new Date()).toLocaleString());

        //console.log('DataTreatEnd|' +(new Date()).toLocaleString());
        //设备开关 start
        var machineSwitchValue;
            for (var i=23;i<200;i++){
                if (machineInfo.Tags[i]) {
                    $('#switch-' + (i - 22)).attr('data-TagKey', machineInfo.Tags[i].Key);
                    if (data[machineInfo.Tags[i].Key]) {
                        machineSwitchValue=data[machineInfo.Tags[i].Key].Value;
                    }else{
                        machineSwitchValue=2;
                    }
                    $('#switchLight-' + (i - 22)).attr('data-bj-switch', machineSwitchValue);
                    $('#switchName-' + (i - 22)).text(machineInfo.Tags[i].Name);
                }
            }
            $(".bj-switchClick").removeClass("bj-switchClick");
            switchLock = 1;
    }
}



function refreshItemValues() {
    $.get('../api/RealTimeDisplay?tagList =' + JSON.stringify(tagList), function (data) {
        //dataQueue.push(data);
        refreshDisplay(data);
        setTimeout(refreshItemValues, 1000);
    });
}




function setValue(obj) {
    if (switchLock == 1) {
        switchLock = 0;
        var statusValue = obj.parentElement.children[0].children[0].attributes["data-bj-switch"].value;
        var currentLightId = obj.parentElement.children[0].children[0]["id"];
        var currentId = obj["id"];
        console.log(currentLightId);
        console.log(obj.parentElement.children[0].children[0].attributes["data-bj-switch"].value);
        $('#' + currentId).addClass("bj-switchClick");
        if (statusValue == 2) {
            return false;//数据data里面不包含这个 点的值
        }
        if (statusValue == 1) {
            $.post('../api/RealTimeDisplay', { '': [$(obj).attr('data-TagKey'), 0] }, function (d) {
                console.log(d, "==1发送成功返回信息");
                // obj.parentElement.children[0].children[0].attributes["data-bj-switch"].value = 0;
                // $('#' + currentLightId).attr('data-bj-switch', 0);//可能需要
                switchLock = 1;
            }, function (error) {
                alert(error);
            });
        }
        if (statusValue == 0) {
            $.post('../api/RealTimeDisplay', { '': [$(obj).attr('data-TagKey'), 1] }, function (d) {
                console.log(d,"==0发送成功返回信息");
                //  obj.parentElement.children[0].children[0].attributes["data-bj-switch"].value = 1;
                 // $('#' + currentLightId).attr('data-bj-switch', 1);//可能需要
                switchLock = 1;
            }, function (error) {
                alert(error);
            });
        }
        $(".bj-switchClick").removeClass("bj-switchClick");
    }
   
}
$(function () {
    Highcharts.setOptions({
        global: {
            useUTC:false

        }
    })
})
