
"use strict";

angular.module("EquipmentSelection", [])
    .controller("EquipmentSelectionController", ['$scope', '$http', function($scope, $http) {
        $scope.initial = function() {
            $scope.userInfo = {
                userId: $('#hiddenUserId').text()
            };

            $http.get("../api/EquipmentSelection?userId=" + $scope.userInfo.userId)
                .success(function(data) {
                    $scope.equipmentSelections = data;
                }).error(function(error) {
                    alert(error);
                });
        };

        //$scope.RedirectTo = function (equipmentId) {
        //    window.location = '/Equipment/Index?equipmentId=' + equipmentId;
            
        //};

        //window.location=“/控制器/方法?参数”
        $scope.initial();


    }]);



function Redirect(obj) {
    window.location = '/Equipment/Index?equipmentId=' + obj.id;
}


function Return() {
    window.location = '/Login/Index';
}



