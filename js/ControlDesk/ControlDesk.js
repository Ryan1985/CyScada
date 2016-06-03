"use strict";

var controlDeskAppController = angular.module("ControlDeskList", ['viewService'])
    .controller("ControlDeskListController", function ($scope, $http, controlDeskService) {

        $scope.initial = function () {
            controlDeskService.getList([window.sideMenuId, userId]).success(function (data) {
                GenerateItems(data);
            }).error(function (error) {
                alert(error);
            });
        };

        
        $scope.initial();
    });


angular.bootstrap(angular.element("#ControlDeskList"), ["ControlDeskList"]);





function GenerateItems(data) {
    var articleTemplate = [
        '<article class="style1">',
        '<span class="image">',
        '<img src="../Images/Phantom/pic0@i.jpg" alt="" />',
        '</span>',
        '<a href="@url">',
        '<h2>@name</h2>',
        '<div class="content">',
        '<p>@desc</p>',
        '</div>',
        '</a>',
        '</article>'].join('');

    var sectionContents = [];
    for (var i = 0; i < data.length; i++) {
        sectionContents.push(articleTemplate
            .replace('@i', i + 1)
            .replace('@url', data[i].Url + '?sideMenuId=' + window.sideMenuId)
            .replace('@name', data[i].Name)
            .replace('@desc', data[i].Description));
    }

    if (data.length > 0) {
        $('#MenuTitle').text(data[0].Title);
    }
    $('section.tiles').append(sectionContents.join(''));
}




/*

                            <article class="style1">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic01.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>基础信息</h2>
                                    <div class="content">
                                        <p>基础信息显示</p>
                                    </div>
                                </a>
                            </article>
                            <article class="style2">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic02.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>当前GPS</h2>
                                    <div class="content">
                                        <p>显示设备的当前GPS</p>
                                    </div>
                                </a>
                            </article>
                            <article class="style3">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic03.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>GPS轨迹</h2>
                                    <div class="content">
                                        <p>显示设备的GPS运动轨迹</p>
                                    </div>
                                </a>
                            </article>
                            <article class="style4">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic04.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>报警记录</h2>
                                    <div class="content">
                                        <p>显示设备的报警记录信息</p>
                                    </div>
                                </a>
                            </article>
                            <article class="style5">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic05.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>实时工况</h2>
                                    <div class="content">
                                        <p>显示设备的实时工作状态</p>
                                    </div>
                                </a>
                            </article>
                            <article class="style6">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic06.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>历史记录</h2>
                                    <div class="content">
                                        <p>显示设备的历史记录信息</p>
                                    </div>
                                </a>
                            </article>
                            <article class="style2">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic07.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>现场视频</h2>
                                    <div class="content">
                                        <p>连接到现场监控设备</p>
                                    </div>
                                </a>
                            </article>
                            <article class="style3">
                                <span class="image">
                                    <img src="~/Images/Phantom/pic08.jpg" alt="" />
                                </span>
                                <a href="generic.html">
                                    <h2>参数设定</h2>
                                    <div class="content">
                                        <p>设定设备运行参数</p>
                                    </div>
                                </a>
                            </article>*/