﻿
"use strict";

angular.module("History", ['mgcrea.ngStrap', 'viewService'])
    .controller("HistoryController", function($scope, $http, bindListService, historyService) {
        $scope.initial = function() {
            $scope.condition = {};


            $('#dtStartDate').datetimepicker({
                minView: 2,
                language: "zh-CN",
                autoclose:true
            });
            $('#dtEndDate').datetimepicker({
                minView: 2,
                language: "zh-CN",
                autoclose: true
            });



        };

        bindListService.getMachineList([sideMenuId, $('#userId').attr('data-userid')]).success(function (data) {
            $('#selMachine').select2({
                data: data,
                placeholder: "全部",
                allowClear: true,
                theme: "bootstrap"
            });
        });

        historyService.getMachineTagList([sideMenuId, $('#userId').attr('data-userid'), $('#selMachine').val()]).success(function (data) {
            $('#selMachineTags').select2({
                data: data,
                placeholder: "全部",
                allowClear: true,
                theme: "bootstrap"
            });
        });

        

        $scope.Query = function () {
            $scope.condition.MachineId = $('#selMachine').val();
            $scope.condition.DeviceName = $('#selMachineTags').val();
            console.log($scope.condition);
            if ($scope.condition.MachineId == "全部") {
                $scope.condition.MachineId = ""; 
            }
            if ($scope.condition.DeviceName == "全部") {
                $scope.condition.DeviceName = "";
            }
            $scope.condition.StartDate = $("#dtStartDate").val();
            $scope.condition.EndDate = $("#dtEndDate").val();
            historyService.getList($scope.condition).success(function (data) {
                $('#ListTable').bootstrapTable('load', data);
                bindChart(data);
            });
        };



        $scope.initial();

    });

angular.bootstrap(angular.element("#History"), ["History"]);

firstQuery();
function firstQuery() {
    var curTimeF = new Date().getTime();
    var endTimeF = moment(curTimeF).format("YYYY-MM-DD");
    var startTimeF = moment(curTimeF).subtract("2", "month").format("YYYY-MM-DD");
    console.log(endTimeF, startTimeF);
    $("#dtStartDate").val(startTimeF);
    $("#dtEndDate").val(endTimeF);
    $("#btnQuery").trigger("click");
}

function rowNumberFormatter(value, row, index) {
    return '<span>' + (Number(index) + 1) + '</span>';
}

function bindChart(chartData) {
    var yAxises = [];

    for (var i = 0; i < chartData.length; i++) {
        yAxises.push([new Date(chartData[i].TimeStamp), Number(chartData[i].Value)]);
    }

    var startDate = new Date(chartData[0].TimeStamp);

    $('#Chart').highcharts({
        chart: { zoomType: 'x', spacingRight: 20 },
        title: { text: '历史数据查询', x: -20 },
        credits: {enabled: false},
        exporting: {enabled: false},
        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: {
                day: '%Y-%b-%e %H:%M:%S'
            }
        },
        yAxis: {
            title: { text: '历史记录' },
            plotLines: [{ value: 0, width: 1, color: '#808080' }]
        },
        tooltip: {
             valueSuffix: ''
        },
        legend: {
             layout: 'vertical', align: 'right', verticalAlign: 'middle', borderWidth: 0
        },
        series: [{
            name: '压力(kg)', data: yAxises,
            pointStart: Date.UTC(startDate.getFullYear(), startDate.getMonth(), startDate.getDate(), startDate.getHours(), startDate.getMinutes(), startDate.getSeconds()),
            pointInterval: 1000
        }]
    });

}

