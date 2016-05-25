"use strict";

angular.module("SideMenuList", ['viewService'])
    .controller("SideMenuListController", function ($scope, $http, sideMenuService, bindListService) {
        $scope.initial = function() {

            sideMenuService.getList()
                .success(function(data) {
                    $scope.sideMenuList = data;
                    $('#ListTable').bootstrapTable('load', data);
                }).error(function(error) {
                    alert(error);
                });

            bindListService.getAuthorityList()
                .success(function (data) {
                    $('#infoAuthorityName').select2({
                        data: data,
                        placeholder: "请选择一个权限",
                        theme: "bootstrap"
                    });
                    $('#txtAuthorityName').select2({
                        data: data,
                        placeholder: "全部",
                        allowClear: true,
                        theme: "bootstrap"
                    });
                    $('#txtMenuType').select2({
                        //data: data,
                        placeholder: "全部",
                        allowClear: true,
                        theme: "bootstrap"
                    });
                    $('#infoMenuType').select2({
                        //data: data,
                        placeholder: "请选择一个类型",
                        allowClear: true,
                        theme: "bootstrap"
                    });
                }).error(function(error) {
                    alert(error);
                });

            bindListService.getParentMenuList()
                .success(function(data) {
                    $('#infoParentName').select2({
                        data: data,
                        placeholder: "无父类",
                        allowClear: true,
                        theme: "bootstrap"
                    });
                }).error(function(error) {
                    alert(error);
                });

            bindListService.getClassList()
                .success(function(data) {
                    $('#infoClass').select2({
                        data: data,
                        placeholder: "请选择一个图标",
                        theme: "bootstrap",
                        templateResult: formatState,
                        templateSelection: formatState
                    });
                }).error(function(error) {
                    alert(error);
                });
        };


        $scope.Query = function () {
            if ($scope.sideMenu) {
                $scope.sideMenu.AuthorityCode = $('#txtAuthorityName').val();
                $scope.sideMenu.MenuType = $('#txtMenuType').val();
            }
            var params = $scope.sideMenu;
            sideMenuService.getList(params)
                .success(function(data) {
                    $('#ListTable').bootstrapTable('load', data);
                }).error(function(error) {
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

        $scope.SaveInfo = function () {
            $scope.info.ParentId = $('#infoParentName').val();
            $scope.info.AuthorityCode = $('#infoAuthorityName').val();
            $scope.info.MenuType = $('#infoMenuType').val();
            $scope.info.Class = $('#infoClass').val();
            sideMenuService.save($scope.info).success(function(status) {
                $scope.Query();
                $('#InfoModal').modal('toggle');
            }).error(function(error) {
                alert(error);
            });
        };


        $scope.DeleteSideMenu = function (id) {
            sideMenuService.del(id).success(function () {
                $scope.Query();
            });
        };



        $scope.initial();
    });


angular.bootstrap(angular.element("#SideMenuList"), ["SideMenuList"]);

function formatState(state) {
    if (!state.id) { return state.text; }
    var $state = $(
      '<span><i class ="' + state.element.value.toLowerCase() + '" ></i></span>'
    );
    return $state;
};


function rowNumberFormatter(value, row, index) {
    return '<span>' + (Number(index) + 1) + '</span>';
}


function menuTypeFormatter(value, row, index) {
    var displayText;
    switch (value.toString()) {
    case "0":
        {
            displayText = "树形菜单";
            break;
        }
        case "1":
            {
                displayText = "模块";
                break;
            }
        default:
            displayText = '';
    }

    return '<span>' + displayText + '</span>';
}


function controlFormatter(value, row, index) {
    var controlFormat = '<button class="btn btn-default  controlBtn detail" data-target="#InfoModal" data-toggle="modal">修改</button><button class="btn btn-default  controlBtn delete">删除</button>';
    return controlFormat;
}

function iconFormatter(value, row, index) {
    var iconFormat = '<span><i class="' + value + '"></i></span>';
    return iconFormat;
}

var operateEvents = {
    'click .detail': function (e, value, row, index) {
        var ctrlScope = angular.element('[ng-controller=SideMenuListController]').scope();
        ctrlScope.info = row;
        ctrlScope.info.title = "修改目录";
        ctrlScope.$apply();
        $('#infoAuthorityName').val(row.AuthorityCode).trigger("change");
        $('#infoMenuType').val(row.MenuType).trigger("change");
        $('#infoParentName').val(row.ParentId).trigger("change");
        $('#infoClass').val(row.Class).trigger("change");
        //$('#infoAuthorityName').selectpicker('val', row.AuthorityId);

    },
    'click .delete': function (e, value, row, index) {
        var id = row.Id;
        var ctrlScope = angular.element('[ng-controller=SideMenuListController]').scope();
        ctrlScope.DeleteSideMenu(id);
        //$.ajax({
        //    url: '../api/SideMenu?id=' + id,
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




