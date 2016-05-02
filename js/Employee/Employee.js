
"use strict";

angular.module("Employee", [])
    .controller("EmployeeController", ['$scope', '$http', function($scope, $http) {
        $scope.initial = function() {

            $scope.foo = {
                UserId : "1"
            };

            //$http.get("../api/SideMenu?userId=" + $('#userId').attr('data-userId'))
            //    .success(function (data) {
            //        $scope.menuList = data;
            //    }).error(function (error) {
            //        alert('获取列表发生错误:' + error);
            //    });

        };

        $scope.initial();


    }]);

angular.bootstrap(angular.element("#EmployeeApp"), ["Employee"]);

