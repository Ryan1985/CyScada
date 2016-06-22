"use strict";

angular.module("AuthorityList", ['viewService'])
    .controller("AuthorityListController", function ($scope, $http,authorityService) {
        $scope.initial = function () {
            authorityService.getList().success(function (data) {
                $scope.authorityList = data;
                $('#ListTable').bootstrapTable('load', data);
            }).error(function (error) {
                alert(error);
            });

            $('#infoAuthorityType').select2({
                placeholder: "全部",
                theme: "bootstrap"
            });
        };


        $scope.Query = function () {
            var params = $scope.authority;
            authorityService.getList(params).success(function (data) {
                $('#ListTable').bootstrapTable('load', data);
            }).error(function (error) {
                if (error.status == 403) {
                    window.location.href = "/Account/Login?ReturnUrl=" + window.location.pathname;
                } else {
                    modal.alertMsg("加载失败！");
                }
            });
        };

        $scope.Add = function () {
            $scope.info = {
                title: "新增权限"
            };
        };

        $scope.SaveInfo = function (valid) {
            if (!valid) {
                return;
            }
            authorityService.save($scope.info).success(function (status) {
                if (status!='') {
                    alert(status);
                    return;
                }
                $scope.Query();
                $('#InfoModal').modal('toggle');
            }).error(function (error) {

            });
        };

        $scope.DeleteAuth = function (id) {
            authorityService.del(id).success(function () {
                $scope.Query();
            });
        };

        $scope.initial();


    });


angular.bootstrap(angular.element("#AuthorityList"), ["AuthorityList"]);




function rowNumberFormatter(value, row, index) {
    return '<span>' + (Number(index) + 1) + '</span>';
}



function controlFormatter(value, row, index) {
    var controlFormat = '<div class="btn-group"><button class="btn btn-info  controlBtn detail" data-target="#InfoModal" data-toggle="modal"><i class="icon-edit"></i>修改</button><button class="btn btn-danger  controlBtn delete"><i class="icon-trash"></i>删除</button></div>';
    return controlFormat;
}

var operateEvents = {
    'click .detail': function (e, value, row, index) {
        var ctrlScope = angular.element('[ng-controller=AuthorityListController]').scope();
        ctrlScope.info = row;
        ctrlScope.info.title = "修改权限";
        $('#infoAuthorityType').value = row.AuthorityType;
        ctrlScope.$apply();
    },
    'click .delete': function (e, value, row, index) {
        var id = row.Id;
        var ctrlScope = angular.element('[ng-controller=AuthorityListController]').scope();
        ctrlScope.DeleteAuth(id);
        //$.ajax({
        //    url: '../api/Authority?id=' + id,
        //    type: 'DELETE',
        //    success: function (result) {
        //        angular.element('#btnQuery').triggerHandler('click');
        //    },
        //    error: function (error) {
        //        alert(error);
        //    }
        //});
    }

};