"use strict";

angular.module("BaseInfo", ['viewService'])
    .controller("BaseInfoController", function($scope, $http, baseInfoService) {
        $scope.initial = function() {
            baseInfoService.getList([window.sideMenuId, $('#userId').attr('data-userid')]).success(function (data) {
                $scope.Info = data;
            }).error(function(error) {
                alert(error);
            });
        };
        $scope.initial();
    });


angular.bootstrap(angular.element("#BaseInfo"), ["BaseInfo"]);
