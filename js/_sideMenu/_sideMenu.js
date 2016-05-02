
"use strict";

angular.module("SideMenu", [])
    .controller("SideMenuController", ['$scope', '$http', function($scope, $http) {
        $scope.initial = function() {

            $http.get("../api/SideMenu?userId=" + $('#userId').attr('data-userId'))
                .success(function(data) {
                    $scope.menuList = data;
                }).error(function(error) {
                    alert('获取列表发生错误:' + error);
                });

        };

        $scope.initial();


    }])
    .directive('sideMenu', function() {
        return {
            retrict: 'A',
            replace: true,
            scope: {
                menuUrl: '@',
                menuId: '@',
                menuName: '@',
                menuClass: '@'
            },
            template:
                '<a href="{{menuUrl}}" id="{{menuId}}">' +
                    '<i class="{{menuClass}}"></i>{{menuName}}' +
                    '<span class="fa arrow"></span>' +
                    '</a>'
        };
    }
    ).directive('sideSubMenu', function() {
        return {
            retrict: 'A',
            replace: true,
            scope: {
                menuUrl: '@',
                menuId: '@',
                menuName: '@',
                menuClass: '@'
            },
            template:
                '<a href="{{menuUrl}}" id="{{menuId}}" onclick="ShowTab(this)">' +
                    '<i class="{{menuClass}}"></i>{{menuName}}' +
                    '</a>'
        };
    }
    );

angular.bootstrap(angular.element("#SideMenu"), ["SideMenu"]);

//function ShowTab(obj) {
//    ClearAllTabSelection();
//    if (GetTab(obj.))
//    $('#page-wrapper div ul').append('<li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">Home</a></li>');
//}


//function ClearAllTabSelection() {
//    $('#page-wrapper div ul li').removeClass('active');
//}

//function GetTab(id) {
//    var tabs = $('#page-wrapper div ul li');
//    for (var i = 0; i < tabs.length; i++) {
//        var anchor = tabs[i].find('a');
//        if (id == anchor.attr('aria-controls')) {
//            return anchor;
//        }
//    }
//    return null;
//}

/*
 <div side-menu menu-url="{{menu.Url}}" menu-id="{{menu.Id}}" menu-name="{{menu.Name}}" menu-Class="{{menu.Class}}">
                            <ul class="nav nav-second-level" >
                                <li ng-repeat="submenu in menu.SubMenus">
                                    <div side-menu menu-url="{{submenu.Url}}" menu-id="{{submenu.Id}}" menu-name="{{submenu.Name}}" menu-class="{{submenu.Class}}">
                                    </div>
                                </li>
                            </ul>
                        </div>
*/

