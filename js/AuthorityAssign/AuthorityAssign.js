"use strict";

angular.module("AuthorityAssign", [])
    .controller("AuthorityAssignController", ['$scope', '$http', function($scope, $http) {
        $scope.initial = function() {
            $scope.userNameConfig = {
                placeholder: '输入用户名称'
            };


            //获取用户列表
            $http.get("../api/AuthorityAssign")
                .success(function(data) {
                    $('#userTable').bootstrapTable('load', data);
                }).error(function(error) {
                    alert(error);
                });

        };


        $scope.Query = function() {
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


        $scope.toggleRole = function(obj) {
            var toggleType;
            if (obj.css('UserRole')) {
                toggleType = "Delete";
            } else {
                toggleType = "Add";
            }

            $.ajax({
                url: '../api/AuthorityAssign',
                type: 'POST',
                data: { 'UserId': $scope.info.Id, "ToggleType": toggleType, 'Id': obj.attr('data-id'), 'ToggleHost': 'UserRole' },
                success: function(result) {
                    if (result == '""') {
                        $scope.refreshUserAuthority();
                    } else {
                        alert(error);
                    }
                },
                error: function(error) {
                    alert(error);
                }
            });
        };


        $scope.toggleAuthority = function(obj) {
            var toggleType;
            if (obj.css('UserAuth')) {
                toggleType = "Delete";
            } else {
                toggleType = "Add";
            }

            $.ajax({
                url: '../api/AuthorityAssign',
                type: 'POST',
                data: { 'UserId': $scope.info.Id, "ToggleType": toggleType, 'Id': obj.attr('data-id'), 'ToggleHost': 'UserAuthority' },
                success: function(result) {
                    if (result == '""') {
                        $scope.refreshUserAuthority();
                    } else {
                        alert(error);
                    }
                },
                error: function(error) {
                    alert(error);
                }
            });
        };

        $scope.initial();

        $scope.refreshUserAuthority = function(userId) {
            $.ajax({
                url: '../api/AuthorityAssign?userId=' + userId,
                type: 'GET',
                success: function(data) {
                    if (data != '') {
                        $scope.info = data;
                        $scope.info.CurrentAuthorityList = [];
                        //根据角色合并角色的所有权限
                        var auth = 0;//用户的当前权限汇总
                        var roleAuth = 0;//角色的权限汇总
                        for (var i = 0; i < data.EmpRoleList.length; i++) {
                            for (var j = 0; j < data.RoleList.length; j++) {
                                if (data.EmpRoleList[i].RoleId == data.RoleList[j].Id) {
                                    auth = auth | data.RoleList[j].Authority;
                                }
                            }
                        }
                        //合并用户的附加权限
                        auth = roleAuth | data.Authority;
                        //解析权限(将当前用户的权限填写到当前权限列表)
                        for (var k = 0; k < data.AuthorityList.length; k++) {
                            if ((data.AuthorityList[k].AuthorityId & auth) == data.AuthorityList[k].AuthorityId) {
                                $scope.info.CurrentAuthorityList.push(data.AuthorityList[k]);
                            }
                        }

                        $scope.$apply();

                        //修改各权限列表的样式(添加颜色)
                        //当前权限列表
                        var currentAuthList = $('#currentAuthList span');
                        for (var l = 0; l < currentAuthList.length; l++) {
                            var currentAuth = $(currentAuthList[l]);
                            if ((Number(currentAuth.attr('data-id')) & roleAuth) == Number(currentAuth.attr('data-id'))) {
                                currentAuthList[l].Class = currentAuthList[l].Class + ' RoleAuth';
                                //if (!currentAuth.css('RoleAuth')) {
                                //    currentAuth.addClass('RoleAuth');
                                //}
                            }
                            if ((Number(currentAuth.attr('data-id')) & data.Authority) == Number(currentAuth.attr('data-id'))) {
                                currentAuthList[l].Class = currentAuthList[l].Class + ' UserAuth';
                                //if (!currentAuth.css('UserAuth')) {
                                //    currentAuth.addClass('UserAuth');
                                //}
                            }
                        }
                        //角色列表

                        for (var m = 0; m < roleList.length; m++) {
                            for (var p = 0; p < $scope.info.RoleList.length ; p++) {
                                if (Number($scope.info.RoleList[p].Id) == roleList[m].Id) {
                                    $scope.info.RoleList[p].Class = $scope.info.RoleList[p].Class + ' UserRole';
                                    //if (!currentRole.css('UserRole')) {
                                    //    currentRole.addClass('UserRole');
                                    //}
                                }
                            }
                        }
                        //附加权限列表
                        for (var n = 0; n < $scope.info.AuthorityList.length; n++) {
                            if (Number($scope.info.AuthorityList[n].AuthorityId) & data.Authority == Number($scope.info.AuthorityList[n].AuthorityId)) {
                                $scope.info.AuthorityList[n].Class = $scope.info.AuthorityList[n].Class + ' UserAuth';
                                //if (!curAuth.css('UserAuth')) {
                                //    curAuth.addClass('UserAuth');
                                //}
                            }
                        }
                    } else {
                        alert('获取权限列表出错，请刷新页面再重试');
                    }
                },
                error: function(error) {
                    alert(error);
                }
            });
        };




    }]);
    //.directive('select2', function (select2Query) {
    //    return {
    //        restrict: 'A',
    //        scope: {
    //            config: '=',
    //            ngModel: '=',
    //            select2Model: '='
    //        },
    //        link: function (scope, element, attrs) {
    //            // 初始化
    //            var tagName = element[0].tagName,
    //                config = {
    //                    allowClear: true,
    //                    multiple: !!attrs.multiple,
    //                    placeholder: attrs.placeholder || ' '   // 修复不出现删除按钮的情况
    //                };

    //            // 生成select
    //            if (tagName === 'SELECT') {
    //                // 初始化
    //                var $element = $(element);
    //                delete config.multiple;

    //                $element
    //                    .prepend('<option value=""></option>')
    //                    .val('')
    //                    .select2(config);

    //                // model - view
    //                scope.$watch('ngModel', function (newVal) {
    //                    setTimeout(function () {
    //                        $element.find('[value^="?"]').remove();    // 清除错误的数据
    //                        $element.select2('val', newVal);
    //                    }, 0);
    //                }, true);
    //                return false;
    //            }

    //            // 处理input
    //            if (tagName === 'INPUT') {
    //                // 初始化
    //                var $element = $(element);

    //                // 获取内置配置
    //                if (attrs.query) {
    //                    scope.config = select2Query[attrs.query]();
    //                }

    //                // 动态生成select2
    //                scope.$watch('config', function () {
    //                    angular.extend(config, scope.config);
    //                    $element.select2('destroy').select2(config);
    //                }, true);

    //                // view - model
    //                $element.on('change', function () {
    //                    scope.$apply(function () {
    //                        scope.select2Model = $element.select2('data');
    //                    });
    //                });

    //                // model - view
    //                scope.$watch('select2Model', function (newVal) {
    //                    $element.select2('data', newVal);
    //                }, true);

    //                // model - view
    //                scope.$watch('ngModel', function (newVal) {
    //                    // 跳过ajax方式以及多选情况
    //                    if (config.ajax || config.multiple) {
    //                        return false; }

    //                    $element.select2('val', newVal);
    //                }, true);
    //            }
    //        }
    //    }
    //});;


angular.bootstrap(angular.element("#AuthorityAssign"), ["AuthorityAssign"]);




function rowNumberFormatter(value, row, index) {
    return '<span>' + (Number(index) + 1) + '</span>';
}




function controlFormatter(value, row, index) {
    var controlFormat = '<button class="btn btn-default  controlBtn detail" data-target="#AuthModal" data-toggle="modal">权限</button>';
    return controlFormat;
}

var operateEvents = {
    'click .detail': function (e, value, row, index) {
        var ctrlScope = angular.element('[ng-controller=AuthorityAssignController]').scope();
        ctrlScope.info = row;
        ctrlScope.info.title = ctrlScope.info.Name + "的权限";
        ctrlScope.refreshUserAuthority(row.Id);
        //ctrlScope.$apply();
    }
};