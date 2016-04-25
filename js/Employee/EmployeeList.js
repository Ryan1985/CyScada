"use strict";

var employeeAppController = angular.module("EmployeeList", ['viewService'])
    .controller("EmployeeListController", function($scope, $http, employeeService) {
        $scope.initial = function() {
            employeeService.getList().success(function(data) {
                $scope.employeeList = data;
                $('#ListTable').bootstrapTable('load', data);
            }).error(function(error) {
                alert(error);
            });
        };
        
        $scope.Query = function() {
            var params = $scope.employee;
            employeeService.getList(params).success(function (data) {
                $('#ListTable').bootstrapTable('load', data);
            }).error(function (error) {
                if (error.status == 403) {
                    window.location.href = "/Account/Login?ReturnUrl=" + window.location.pathname;
                } else {
                    modal.alertMsg("加载失败！");
                }
            });
        };

        $scope.Add = function() {
            $scope.info = {
                title: "新增用户"
            };
        };

        $scope.SaveInfo = function (valid) {
            if (!valid) {
                return;
            }
            employeeService.save($scope.info).success(function(status) {
                $scope.Query();
                $('#InfoModal').modal('toggle');
            }).error(function(error) {

            });
        };

        $scope.DeleteEmp = function(id) {
            employeeService.del(id).success(function () {
                $scope.Query();
            });
        };



        $scope.initial();
    });

var pwCheckDirective = function () {
    return {
        require: 'ngModel',
        link: function(scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function() {
                scope.$apply(function() {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('pwmatch', v);
                });
            });
        }
    };
};


employeeAppController.directive('pwCheck', pwCheckDirective);

angular.bootstrap(angular.element("#EmployeeList"), ["EmployeeList"]);




function rowNumberFormatter(value, row, index) {
    return '<span>' + (Number(index) + 1) + '</span>';
}



function controlFormatter(value, row, index) {
    var controlFormat = '<button class="btn btn-default  controlBtn detail" data-target="#InfoModal" data-toggle="modal">修改</button><button class="btn btn-default  controlBtn delete">删除</button>';
    return controlFormat;
}

var operateEvents = {
    'click .detail': function (e, value, row, index) {
        var ctrlScope = angular.element('[ng-controller=EmployeeListController]').scope();
        ctrlScope.info = row;
        ctrlScope.info.title = "修改用户";
        ctrlScope.info.Password2 = row.Password;
        ctrlScope.$apply();
    },
    'click .delete': function (e, value, row, index) {
        var id = row.Id;
        var ctrlScope = angular.element('[ng-controller=EmployeeListController]').scope();
        ctrlScope.DeleteEmp(id);
    }

};