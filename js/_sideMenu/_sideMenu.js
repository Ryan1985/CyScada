
"use strict";

angular.module("SideMenu", [])
    .controller("SideMenuController", ['$scope', '$http', function($scope, $http) {
        $scope.initial = function() {
            $http.get("../api/SideMenu?userId=" + $('#userId').attr('data-userId'))
                .success(function(data) {
                    $scope.menuList = data;
                    var treeHtml = [];
                    for (var i = 0; i < data.length; i++) {
                        AppendBranch(treeHtml, data[i]);
                    }
                    $('#side-menu').append(treeHtml.join(''));
                    RefreshActive();
                }).error(function(error) {
                    alert('获取列表发生错误:' + error);
                });
        };
        $scope.initial();
    }]);

var leafTemplate = '<li><a data-id="@id" class="CyScadaSideItem" data-href="@url" onclick="Click(this)"><i class="@class"></i>@name</a></li>';
var branchTemplate = '<li><a data-id="@id" class="CyScadaSideItem" data-href="@url" onclick="Toggle(this)"><i class="@class"></i>@name<span class="fa arrow"></span></a>';//'</li>';


function AppendBranch(branchHtml, branch) {
    if (branch.SubMenus==undefined || branch.SubMenus.length == 0) {
        branchHtml.push(leafTemplate.replace('@url', branch.Url).replace('@name', branch.Name).replace('@class', branch.Class).replace('@id','SideMenu_'+branch.Id));
    } else {
        branchHtml.push(branchTemplate.replace('@url', branch.Url).replace('@name', branch.Name).replace('@class', branch.Class).replace('@id', 'SideMenu_' + branch.Id));
        branchHtml.push('<ul class="nav nav-second-level collapse" style="padding-left:30px;">');
        for (var i = 0; i < branch.SubMenus.length; i++) {
            AppendBranch(branchHtml, branch.SubMenus[i]);
        }
        branchHtml.push('</ul>');
        branchHtml.push('</li>');
    }
}


function RefreshActive() {
    $('#side-menu').find('.active').removeClass('active');
    $('#side-menu').find('.in').removeClass('in');

    var element = $('#side-menu').find('a');
    var params = window.location.href.split('?');
    var i = 0;
    var url;
    if (params.length > 1) {
        url = params[1];
        for (; i < element.length; i++) {
            if (element[i] != undefined && $(element[i]).attr('data-id') == 'SideMenu_' + url) {
                $(element[i]).addClass('active');
                ExpandParents(element[i]);
                return;
            }
        }
    } else {
        url = window.location.pathname;
        for (; i < element.length; i++) {
            if (element[i] != undefined && $(element[i]).attr('data-href').indexOf(url.pathname) >= 0) {
                $(element[i]).addClass('active');
                ExpandParents(element[i]);
                return;
            }
        }
    }


}


function ExpandParents(ele) {
    if (ele.id == 'side-menu') {
        return;
    }
    if ($(ele).is('li')) {
        $(ele).addClass('active');
    }
    if ($(ele).is('ul')) {
        $(ele).addClass('in');
    }
    ExpandParents(ele.parentElement);
}


function Toggle(ele) {
    if ($(ele).attr('data-href') == '') {
        var nextEle = $(ele).next();
        if (nextEle.is('ul') && nextEle.hasClass('in')) {
            nextEle.removeClass('in');
            $(ele).parent().removeClass('active');
        } else if (nextEle.is('ul') && nextEle.hasClass('in') == false) {
            nextEle.addClass('in');
            $(ele).parent().addClass('active');
        }
    }
}

function Click(ele) {
    if ($(ele).attr('data-href') == '') {
        return;
    }

    var href = $(ele).attr('data-href') + '?' + $(ele).attr('data-id').replace('SideMenu_', '');
    window.location.href = href;
}
    //.directive('sideMenu', function() {
    //    return {
    //        retrict: 'A',
    //        replace: true,
    //        scope: {
    //            menuUrl: '@',
    //            menuId: '@',
    //            menuName: '@',
    //            menuClass: '@'
    //        },
    //        template:
    //            '<a href="{{menuUrl}}" id="{{menuId}}">' +
    //                '<i class="{{menuClass}}"></i>{{menuName}}' +
    //                '<span class="fa arrow"></span>' +
    //                '</a>'
    //    };
    //}
    //).directive('sideSubMenu', function() {
    //    return {
    //        retrict: 'A',
    //        replace: true,
    //        scope: {
    //            menuUrl: '@',
    //            menuId: '@',
    //            menuName: '@',
    //            menuClass: '@'
    //        },
    //        template:
    //            '<a href="{{menuUrl}}" id="{{menuId}}" onclick="ShowTab(this)">' +
    //                '<i class="{{menuClass}}"></i>{{menuName}}' +
    //                '</a>'
    //    };
    //}
    //);

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

