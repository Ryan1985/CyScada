"use strict";

angular.module("AuthorityAssign", ['viewService'])
    .controller("AuthorityAssignController",  function($scope, $http,authorityAssignService) {
        $scope.initial = function() {
            $scope.userNameConfig = {
                placeholder: '输入用户名称'
            };


            authorityAssignService.getList().success(function(data) {
                $('#userTable').bootstrapTable('load', data[0]);
                $('#roleTable').bootstrapTable('load', data[1]);
            }).error(function (error) {
                alert(error);
            });

            ////获取列表
            //$http.get("../api/AuthorityAssign")
            //    .success(function(data) {
            //        $('#userTable').bootstrapTable('load', data[0]);
            //        $('#roleTable').bootstrapTable('load', data[1]);
            //    }).error(function(error) {
            //        alert(error);
            //    });
            
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
            if (obj.role.displayClass.indexOf('UserRole') > -1) {
                toggleType = "Delete";
            } else {
                toggleType = "Add";
            }

            authorityAssignService.save({ 'UserId': $scope.info.Id, "ToggleType": toggleType, 'Id': obj.role.Id, 'ToggleHost': 'UserRole' })
                .success(function(result) {
                    if (result == '') {
                        $scope.refreshUserAuthority($scope.info.Id);
                    } else {
                        alert(result);
                    }
                })
                .error(function(error) {
                    alert(error);
                });
        };

        $scope.toggleAuthority = function(obj) {
            var toggleType;
            if (obj.authority.displayClass.indexOf('UserAuth') > -1) {
                toggleType = "Delete";
            } else {
                toggleType = "Add";
            }

            authorityAssignService.save({ 'UserId': $scope.info.Id, "ToggleType": toggleType, 'Id': obj.authority.AuthorityCode, 'ToggleHost': 'UserAuthority' })
                .success(function(result) {
                    if (result == '') {
                        $scope.refreshUserAuthority($scope.info.Id);
                    } else {
                        alert(result);
                    }
                })
                .error(function(error) {
                    alert(error);
                });

        };

        $scope.toggleRoleAuthority = function (obj) {
            var toggleType;
            if (obj.authority.displayClass.indexOf('RoleAuth') > -1) {
                toggleType = "Delete";
            } else {
                toggleType = "Add";
            }

            authorityAssignService.save({ 'UserId': $scope.roleInfo.Id, "ToggleType": toggleType, 'Id': obj.authority.AuthorityCode, 'ToggleHost': 'RoleAuthority' })
            .success(function (result) {
                if (result == '') {
                    $scope.refreshRoleAuthority($scope.roleInfo.Id);
                } else {
                    alert(result);
                }
            })
            .error(function (error) {
                alert(error);
            });


            //$.ajax({
            //    url: '../api/AuthorityAssign',
            //    type: 'POST',
            //    data: { 'UserId': $scope.roleInfo.Id, "ToggleType": toggleType, 'Id': obj.authority.AuthorityId, 'ToggleHost': 'RoleAuthority' },
            //    success: function (result) {
            //        if (result == '') {
            //            $scope.refreshRoleAuthority($scope.roleInfo.Id);
            //        } else {
            //            alert(result);
            //        }
            //    },
            //    error: function (error) {
            //        alert(error);
            //    }
            //});
        };




        $scope.initial();

        $scope.refreshUserAuthority = function (userId) {
            authorityAssignService.getUserAuthList(userId).success(function(data) {
                if (data != '') {
                    $scope.info = data;
                    $scope.info.title = data.Name + "的权限";
                    $scope.info.CurrentAuthorityList = [];
                    var authList = [];
                    authList = jQuery.extend(true, [], data.AuthorityList);

                    //根据角色合并角色的所有权限
                    var auth = []; //用户的当前权限汇总
                    var roleAuth = []; //角色的权限汇总
                    for (var i = 0; i < data.EmpRoleList.length; i++) {
                        for (var j = 0; j < data.RoleList.length; j++) {
                            if (data.EmpRoleList[i].RoleId == data.RoleList[j].Id) {
                                var currentRoleAuth = data.RoleList[j].AuthorityCode.split(',');
                                roleAuth = roleAuth.concat(currentRoleAuth);
                                //roleAuth.push(data.RoleList[j].AuthorityCode);
                            }
                        }
                    }
                    //合并用户的附加权限
                    auth = roleAuth.concat(data.AuthorityCode.split(','));
                    //解析权限(将当前用户的权限填写到当前权限列表)
                    for (var k = 0; k < authList.length; k++) {
                        if (auth.contains(authList[k].AuthorityCode.replace(',', ''))) {
                            authList[k].displayClass = 'label AuthLabel  NoAuth';
                            $scope.info.CurrentAuthorityList.push(authList[k]);
                        }
                    }


                    //修改各权限列表的样式(添加颜色)
                    //当前权限列表
                    for (var l = 0; l < $scope.info.CurrentAuthorityList.length; l++) {
                        var currentAuth = $scope.info.CurrentAuthorityList[l];
                        if (roleAuth.contains(currentAuth.AuthorityCode)) {
                            currentAuth.displayClass = currentAuth.displayClass + ' RoleAuth';
                        }
                        if (data.AuthorityCode.split(',').contains(currentAuth.AuthorityCode)) {
                            currentAuth.displayClass = currentAuth.displayClass + ' UserAuth';
                        }
                    }
                    //角色列表
                    for (var p = 0; p < $scope.info.RoleList.length; p++) {
                        $scope.info.RoleList[p].displayClass = 'btn AuthBtn NoAuth';
                        if (roleAuth.contains($scope.info.RoleList[p].AuthorityCode)) {
                            $scope.info.RoleList[p].displayClass = $scope.info.RoleList[p].displayClass + ' UserRole';
                        }
                    }
                    //附加权限列表
                    for (var n = 0; n < $scope.info.AuthorityList.length; n++) {
                        $scope.info.AuthorityList[n].displayClass = 'btn AuthBtn NoAuth';
                        if (data.AuthorityCode.split(',').contains($scope.info.AuthorityList[n].AuthorityCode)) {
                            $scope.info.AuthorityList[n].displayClass = $scope.info.AuthorityList[n].displayClass + ' UserAuth';
                        }
                    }

                    //$scope.$apply();
                } else {
                    alert('获取权限列表出错，请刷新页面再重试');
                }
            }).error(function(error) {
                alert(error);
            });

        };

        $scope.refreshRoleAuthority = function (roleId) {

            authorityAssignService.getRoleAuthList(roleId).success(
                function(data) {
                    if (data != '') {
                        $scope.roleInfo = data;
                        $scope.roleInfo.title = data.Name + "的权限";
                        //$scope.$apply();
                        //修改各权限列表的样式(添加颜色)
                        //权限列表
                        for (var n = 0; n < $scope.roleInfo.AuthorityList.length; n++) {
                            $scope.roleInfo.AuthorityList[n].displayClass = 'btn AuthBtn NoAuth';
                            if (data.AuthorityCode.split(',').contains($scope.roleInfo.AuthorityList[n].AuthorityCode)) {
                                $scope.roleInfo.AuthorityList[n].displayClass = $scope.roleInfo.AuthorityList[n].displayClass + ' RoleAuth';
                                //if (!curAuth.css('UserAuth')) {
                                //    curAuth.addClass('UserAuth');
                                //}
                            }
                        }

                        //$scope.$apply();
                    } else {
                        alert('获取权限列表出错，请刷新页面再重试');
                    }
                }).error(function (error) {
                    alert(error);
                });

            //$.ajax({
            //    url: '../api/AuthorityAssign?userId=&roleId=' + roleId,
            //    type: 'GET',
            //    success: function (data) {
            //        if (data != '') {
            //            $scope.roleInfo = data;
            //            $scope.roleInfo.title = data.Name + "的权限";
            //            $scope.$apply();
            //            //修改各权限列表的样式(添加颜色)
            //            //权限列表
            //            for (var n = 0; n < $scope.roleInfo.AuthorityList.length; n++) {
            //                $scope.roleInfo.AuthorityList[n].displayClass = 'btn AuthBtn NoAuth';
            //                if ((Number($scope.roleInfo.AuthorityList[n].AuthorityId) & data.Authority) == Number($scope.roleInfo.AuthorityList[n].AuthorityId)) {
            //                    $scope.roleInfo.AuthorityList[n].displayClass = $scope.roleInfo.AuthorityList[n].displayClass + ' RoleAuth';
            //                    //if (!curAuth.css('UserAuth')) {
            //                    //    curAuth.addClass('UserAuth');
            //                    //}
            //                }
            //            }

            //            $scope.$apply();
            //        } else {
            //            alert('获取权限列表出错，请刷新页面再重试');
            //        }
            //    },
            //    error: function (error) {
            //        alert(error);
            //    }
            //});
        };
    });

angular.bootstrap(angular.element("#AuthorityAssign"), ["AuthorityAssign"]);




function rowNumberFormatter(value, row, index) {
    return '<span>' + (Number(index) + 1) + '</span>';
}




function controlFormatter(value, row, index) {
    var controlFormat = '<button class="btn btn-default  controlBtn detail" data-target="#AuthModal" data-toggle="modal">权限</button>';
    return controlFormat;
}

function controlRoleFormatter(value, row, index) {
    var controlFormat = '<button class="btn btn-default  controlBtn detail" data-target="#RoleModal" data-toggle="modal">权限</button>';
    return controlFormat;
}

var operateEvents = {
    'click .detail': function (e, value, row, index) {
        var ctrlScope = angular.element('[ng-controller=AuthorityAssignController]').scope();
        //ctrlScope.info = row;
        //ctrlScope.info.title = ctrlScope.info.Name + "的权限";
        ctrlScope.refreshUserAuthority(row.Id);
        //ctrlScope.$apply();
    }
};

var operateRoleEvents = {
    'click .detail': function (e, value, row, index) {
        var ctrlScope = angular.element('[ng-controller=AuthorityAssignController]').scope();
        //ctrlScope.roleInfo = row;
        //ctrlScope.roleInfo.title = ctrlScope.roleInfo.Name + "的权限";
        ctrlScope.refreshRoleAuthority(row.Id);
        //ctrlScope.$apply();
    }
};



Array.prototype.contains = function (item) {
    return RegExp(item).test(this);
};


