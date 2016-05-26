
"use strict";

angular.module("SideMenu", [])
    .controller("SideMenuController", ['$scope', '$http', function ($scope, $http) {
        $scope.initial = function () {
            $http.get("../api/SideMenu?userId=" + $('#userId').attr('data-userId')+"&themeType=1")
                .success(function (data) {
                    $scope.menuList = data;
                    var treeHtml = [];
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].SubMenus == undefined || data[i].SubMenus.length == 0) {
                            var mainLeafHtml = mainLeafTemplate.replace('@url', data[i].Url).replace('@name', data[i].Name).replace('@class', data[i].Class).replace('@id', 'menuId=' + data[i].Id);
                            treeHtml.push(mainLeafHtml);
                        } else {
                            var mainBranchHtml = mainBranchTemplate.replace('@url', data[i].Url).replace('@name', data[i].Name).replace('@class', data[i].Class).replace('@id', 'menuId=' + data[i].Id);
                            treeHtml.push(mainBranchHtml);
                            treeHtml.push('<ul class="list-unstyled menu-item">');
                            for (var j = 0; j < data[i].SubMenus.length; j++) {
                                AppendBranch(treeHtml, data[i].SubMenus[j], j);
                            }
                            treeHtml.push('</ul></li>');
                        }
                    }
                    $('#side-menu').append(treeHtml.join(''));
                    RefreshActive();
                }).error(function (error) {
                    alert('获取列表发生错误:' + error);
                });

        };
        $scope.initial();
    }]);

var leafTemplate = '<li><a data-id="@id" data-href="@url" onclick="Click(this)">@name</a></li>';
var branchTemplate = '<li class="CyScadaSideItem"><a data-id="@id" data-href="@url"  onclick="Click(this)">@name</a>';
var mainLeafTemplate = '<li><a data-id="@id" class="sa-side-widget CyScadaSideItem" data-href="@url" onclick="Click(this)"><span class="menu-item">@name</span></a></li>';
var mainBranchTemplate = '<li class="dropdown"><a  data-id="@id" class="sa-side-widget" data-href="" onclick="Click(this)"><span class="menu-item">@name</span></a>';


function AppendBranch(branchHtml, branch,level) {
    if (branch.SubMenus == undefined || branch.SubMenus.length == 0) {
        branchHtml.push(leafTemplate.replace('@url', branch.Url).replace('@name', branch.Name).replace('@class', branch.Class).replace('@id', 'menuId=' + branch.Id));
    } else {
        branchHtml.push(branchTemplate.replace('@url', branch.Url).replace('@name', branch.Name).replace('@class', branch.Class).replace('@id', 'menuId=' + branch.Id));
        branchHtml.push('<ul class="list-unstyled CyScadaSideMenu" style="top:' + level * 30 + 'px;">');
        for (var i = 0; i < branch.SubMenus.length; i++) {
            AppendBranch(branchHtml, branch.SubMenus[i], i);
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
            if (element[i] != undefined && $(element[i]).attr('data-id') == url) {
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

    var href = $(ele).attr('data-href') + '?menuId=' + $(ele).attr('data-id').replace('menuId=', '');
    window.location.href = href;

}

angular.bootstrap(angular.element("#SideMenu"), ["SideMenu"]);


//$('#side-menu').append(['<li class="active">',
//               '<a class="sa-side-table" href="tables.html">',
//                   '<span class="menu-item">Tables</span>',
//               '</a>',
//           '</li>',
//           '<li class="dropdown">',
//               '<a class="sa-side-form" href="">',
//                   '<span class="menu-item">Form</span>',
//               '</a>',
//               '<ul class="list-unstyled CyScadaSideMenu">',
//                    '<li class="dropdown">',
//                       '<li class="CyScadaSideItem"><a href="form-elements.html">Basic Form Elements</a>',
//                       '<ul class="list-unstyled CyScadaSideMenu">',
//                            '<li class="CyScadaSideItem"><a href="form-elements.html">Basic Form Elements</a>',
//                               '<ul class="list-unstyled CyScadaSideMenu">',
//                                   '<li><a href="form-elements.html">Basic Form Elements</a></li>',
//                                   '<li><a href="form-components.html">Form Components</a></li>',
//                                   '<li><a href="form-examples.html">Form Examples</a></li>',
//                                   '<li><a href="form-validation.html">Form Validation</a></li>',
//                               '</ul>',
//                           '</li>',
//                           '<li><a href="form-components.html">Form Components</a></li>',
//                           '<li><a href="form-examples.html">Form Examples</a></li>',
//                           '<li><a href="form-validation.html">Form Validation</a></li>',
//                       '</ul>',
//                   '</li>',
//                   '</li>',
//                   '<li><a href="form-components.html">Form Components</a></li>',
//                   '<li><a href="form-examples.html">Form Examples</a></li>',
//                   '<li><a href="form-validation.html">Form Validation</a></li>',
//               '</ul>',
//           '</li>'].join(''));







//"use strict";

//angular.module("SideMenu", [])
//    .controller("SideMenuController", ['$scope', '$http', function($scope, $http) {
//        $scope.initial = function() {
//            $http.get("../api/SideMenu?userId=" + $('#userId').attr('data-userId'))
//                .success(function(data) {
//                    $scope.menuList = data;
//                    var treeHtml = [];
//                    for (var i = 0; i < data.length; i++) {
//                        AppendBranch(treeHtml, data[i]);
//                    }
//                    $('#side-menu').append(treeHtml.join(''));
//                    RefreshActive();
//                }).error(function(error) {
//                    alert('获取列表发生错误:' + error);
//                });
//        };
//        $scope.initial();
//    }]);

//var leafTemplate = '<li><a data-id="@id" class="CyScadaSideItem" data-href="@url" onclick="Click(this)"><i class="@class"></i>@name</a></li>';
//var branchTemplate = '<li><a data-id="@id" class="CyScadaSideItem" data-href="@url" onclick="Toggle(this)"><i class="@class"></i>@name<span class="fa arrow"></span></a>';//'</li>';


//function AppendBranch(branchHtml, branch) {
//    if (branch.SubMenus==undefined || branch.SubMenus.length == 0) {
//        branchHtml.push(leafTemplate.replace('@url', branch.Url).replace('@name', branch.Name).replace('@class', branch.Class).replace('@id', 'menuId=' + branch.Id));
//    } else {
//        branchHtml.push(branchTemplate.replace('@url', branch.Url).replace('@name', branch.Name).replace('@class', branch.Class).replace('@id', 'menuId=' + branch.Id));
//        branchHtml.push('<ul class="nav nav-second-level collapse" style="padding-left:30px;">');
//        for (var i = 0; i < branch.SubMenus.length; i++) {
//            AppendBranch(branchHtml, branch.SubMenus[i]);
//        }
//        branchHtml.push('</ul>');
//        branchHtml.push('</li>');
//    }
//}


//function RefreshActive() {
//    $('#side-menu').find('.active').removeClass('active');
//    $('#side-menu').find('.in').removeClass('in');

//    var element = $('#side-menu').find('a');
//    var params = window.location.href.split('?');
//    var i = 0;
//    var url;
//    if (params.length > 1) {
//        url = params[1];
//        for (; i < element.length; i++) {
//            if (element[i] != undefined && $(element[i]).attr('data-id') == url) {
//                $(element[i]).addClass('active');
//                ExpandParents(element[i]);
//                return;
//            }
//        }
//    } else {
//        url = window.location.pathname;
//        for (; i < element.length; i++) {
//            if (element[i] != undefined && $(element[i]).attr('data-href').indexOf(url.pathname) >= 0) {
//                $(element[i]).addClass('active');
//                ExpandParents(element[i]);
//                return;
//            }
//        }
//    }


//}


//function ExpandParents(ele) {
//    if (ele.id == 'side-menu') {
//        return;
//    }
//    if ($(ele).is('li')) {
//        $(ele).addClass('active');
//    }
//    if ($(ele).is('ul')) {
//        $(ele).addClass('in');
//    }
//    ExpandParents(ele.parentElement);
//}


//function Toggle(ele) {
//    if ($(ele).attr('data-href') == '') {
//        var nextEle = $(ele).next();
//        if (nextEle.is('ul') && nextEle.hasClass('in')) {
//            nextEle.removeClass('in');
//            $(ele).parent().removeClass('active');
//        } else if (nextEle.is('ul') && nextEle.hasClass('in') == false) {
//            nextEle.addClass('in');
//            $(ele).parent().addClass('active');
//        }
//    }
//}

//function Click(ele) {
//    if ($(ele).attr('data-href') == '') {
//        return;
//    }

//    var href = $(ele).attr('data-href') + '?menuId=' + $(ele).attr('data-id').replace('menuId=', '');
//    window.location.href = href;
//}

//angular.bootstrap(angular.element("#SideMenu"), ["SideMenu"]);


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

