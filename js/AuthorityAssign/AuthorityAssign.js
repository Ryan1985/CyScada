"use strict";

angular.module("AuthorityAssign", [])
    .controller("AuthorityAssignController", ['$scope', '$http', function ($scope, $http) {
        $scope.initial = function () {

            //$http.get("../api/EmployeeList")
            //    .success(function (data) {
            //        $scope.employeeList = data;
            //        $('#ListTable').bootstrapTable('load', data);
            //    }).error(function (error) {
            //        alert(error);
            //    });
        };


        $scope.Query = function () {
            //var params = $scope.employee;
            //$http.get("../api/EmployeeList/?paramstring=" + encodeURI(JSON.stringify(params)))
            //    .success(function (data) {
            //        $('#ListTable').bootstrapTable('load', data);
            //    }).error(function (error) {
            //        if (error.status == 403) {
            //            window.location.href = "/Account/Login?ReturnUrl=" + window.location.pathname;
            //        } else {
            //            modal.alertMsg("加载失败！");
            //        }
            //    });
        };


        $scope.initial();


    }]);


angular.bootstrap(angular.element("#AuthorityAssign"), ["AuthorityAssign"]);




//function rowNumberFormatter(value, row, index) {
//    return '<span>' + (Number(index) + 1) + '</span>';
//}



//function controlFormatter(value, row, index) {
//    var controlFormat = '<button class="btn btn-default  controlBtn detail" data-target="#InfoModal" data-toggle="modal">修改</button><button class="btn btn-default  controlBtn delete">删除</button>';
//    return controlFormat;
//}

//var operateEvents = {
//    'click .detail': function (e, value, row, index) {
//        var ctrlScope = angular.element('[ng-controller=EmployeeListController]').scope();
//        ctrlScope.info = row;
//        ctrlScope.info.title = "修改用户";
//        ctrlScope.info.Password2 = row.Password;
//        ctrlScope.$apply();
//    },
//    'click .delete': function (e, value, row, index) {
//        var id = row.Id;
//        $.ajax({
//            url: '../api/Employee?id=' + id,
//            type: 'DELETE',
//            success: function (result) {
//                angular.element('#btnQuery').triggerHandler('click');
//            },
//            error: function (error) {
//                alert(error);
//            }
//        });
//    }

//};