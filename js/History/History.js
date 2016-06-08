
"use strict";

angular.module("History", ['mgcrea.ngStrap', 'viewService'])
    .controller("HistoryController", function($scope, $http, bindListService, historyService) {
        $scope.initial = function() {


        };

        bindListService.getMachineList([window.sideMenuId, window.userId]).success(function (data) {
            $('#selMachine').select2({
                data: data,
                placeholder: "全部",
                allowClear: true,
                theme: "bootstrap"
            });
        });

        historyService.getMachineTagList([window.sideMenuId, window.userId, $('#selMachine').val()]).success(function (data) {
            $('#selMachineTags').select2({
                data: data,
                placeholder: "全部",
                allowClear: true,
                theme: "bootstrap"
            });
        });


        $scope.Query = function() {
            historyService.getList($scope.condition).success(function (data) {
                $('#ListTable').bootstrapTable('load', data);
            });
        };



        $scope.initial();


    });

angular.bootstrap(angular.element("#History"), ["History"]);





