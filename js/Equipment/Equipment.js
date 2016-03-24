
"use strict";

angular.module("Equipment", [])
    .controller("EquipmentController", ['$scope', '$http', function ($scope, $http) {
        $scope.initial = function () {
            $scope.equipmentInfo = {
                equipmentId: $('#hiddenEquipmentId').text()
            };

            $http.get("../api/Equipment?equipmentId=" + $('#hiddenEquipmentId').text())
                   .success(function (data) {
                       $scope.equipmentInfo = data;

                       var handle = setInterval(function () {
                           $http.get("../api/Equipment?equipmentId=" + $('#hiddenEquipmentId').text())
                               .success(function (data) {
                                   $scope.equipmentInfo = data;
                               }).error(function (error) {
                                   alert('服务端获取数据发生错误,事实刷新被终止，可以尝试重新刷新页面:' + error);
                                   clearInterval(handle);
                               });
                       }, 5000);

                   }).error(function (error) {
                       alert('服务端获取数据发生错误,事实刷新被终止，可以尝试重新刷新页面:' + error);
                   });





        };

        $scope.initial();


    }]);



function Return() {
    window.location = '/EquipmentSelection/Index';
}



