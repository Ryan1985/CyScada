var viewServicesModule = angular.module('viewService', []);


//目录管理视图服务
viewServicesModule.factory('bindListService', function ($http) {
    return {
        //查询
        getAuthorityList: function () {
            return $http.get("../api/BindList?bindType=AuthorityList");
        },
        getParentMenuList: function () {
            return $http.get("../api/BindList?bindType=ParentMenuList");
        },
        getClassList: function () {
            return $http.get("../api/BindList?bindType=ClassList");
        }
    };
});

//用户管理视图服务
viewServicesModule.factory('employeeService', function($http) {
    return {
        //查询
        getList: function(params) {
            if (params == undefined) {
                return $http.get("../api/EmployeeList");
            } else {
                return $http.get("../api/EmployeeList/?paramstring=" + encodeURI(JSON.stringify(params)));
            }
        },
        //保存
        save: function(data) {
            return $http.post('/api/Employee', data);
        },
        //删除
        del: function(id) {
            return $http.delete("../api/Employee?id=" + id);
        }
    };
});


//权限管理视图服务
viewServicesModule.factory('authorityService', function ($http) {
    return {
        //查询
        getList: function (params) {
            if (params == undefined) {
                return $http.get("../api/AuthorityList");
            } else {
                return $http.get("../api/AuthorityList/?paramstring=" + encodeURI(JSON.stringify(params)));
            }
        },
        //保存
        save: function (data) {
            return $http.post('/api/Authority', data);
        },
        //删除
        del: function (id) {
            return $http.delete("../api/Authority?id=" + id);
        }
    };
});


//角色管理视图服务
viewServicesModule.factory('roleService', function ($http) {
    return {
        //查询
        getList: function (params) {
            if (params == undefined) {
                return $http.get("../api/RoleList");
            } else {
                return $http.get("../api/RoleList/?paramstring=" + encodeURI(JSON.stringify(params)));
            }
        },
        //保存
        save: function (data) {
            return $http.post('/api/Role', data);
        },
        //删除
        del: function (id) {
            return $http.delete("../api/Role?id=" + id);
        }
    };
});

//权限分配管理视图服务
viewServicesModule.factory('authorityAssignService', function ($http) {
    return {
        //查询
        getList: function () {
                return $http.get("../api/AuthorityAssign");
        },
        //查询用户附加权限
        getUserAuthList:function(userId) {
            return $http.get('../api/AuthorityAssign?userId=' + userId);
        },
        //查询角色权限
        getRoleAuthList:function(roleId) {
            return $http.get('../api/AuthorityAssign?userId=&roleId=' + roleId);
        },
        //切换用户权限
        save:function(data) {
            return $http.post('../api/AuthorityAssign', data);
        }
    };
});

//目录管理视图服务
viewServicesModule.factory('sideMenuService', function ($http) {
    return {
        //查询
        getList: function (params) {
            if (params == undefined) {
                return $http.get("../api/SideMenuList");
            } else {
                return $http.get("../api/SideMenuList/?paramstring=" + encodeURI(JSON.stringify(params)));
            }
        },
        //保存
        save: function (data) {
            return $http.post('/api/SideMenu', data);
        },
        //删除
        del: function (id) {
            return $http.delete("../api/SideMenu?id=" + id);
        }
    };
});

//控制桌面视图服务
viewServicesModule.factory('controlDeskService', function ($http) {
    return {
        //查询
        getList: function (params) {
            return $http.get("../api/ControlDesk?SideMenuId=" + params[0] + '&UserId=' + params[1]);
        }
    };
});

//基础信息视图服务
viewServicesModule.factory('baseInfoService', function($http) {
    return {
        //查询
        getList: function (params) {
            return $http.get("../api/BaseInfo?SideMenuId=" + params);
        }
    };
});
