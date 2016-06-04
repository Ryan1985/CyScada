$(document).ready(function () {
    // 百度地图API功能
    var map = new window.BMap.Map("allmap", { enableMapClick: false });
    // 创建Map实例
    map.centerAndZoom(new window.BMap.Point(105.404, 58.915), 5);
    // 初始化地图,设置中心点坐标和地图级别
    map.addControl(new window.BMap.MapTypeControl({ mapTypes: [window.BMAP_NORMAL_MAP, window.BMAP_SATELLITE_MAP] }));
    //添加地图类型控件
    //map.setCurrentCity("北京");
    map.setMapStyle({ style: 'light' });
    
    // 添加带有定位的导航控件
    var navigationControl = new window.BMap.NavigationControl({
        // 靠左上角位置
        anchor: window.BMAP_ANCHOR_TOP_LEFT,
        // LARGE类型
        type: window.BMAP_NAVIGATION_CONTROL_LARGE,
        // 启用显示定位
        enableGeolocation: true
    });
    map.addControl(navigationControl);
    // 设置地图显示的城市 此项是必须设置的
    map.enableScrollWheelZoom(true);
    //设置地图高度
    $('#allmap').css('height', $(window).height() - $('#navHeader').height());


    var myIcon = new BMap.Icon("../../img/markers.png", new BMap.Size(25, 25), {
        offset: new BMap.Size(0, 0), // 指定定位位置
        imageOffset: new BMap.Size(0, -275) // 设置图片偏移
    });

    var opts = {
        width: 300,     // 信息窗口宽度
        height: 200,     // 信息窗口高度
        title: "设备信息", // 信息窗口标题
        panel : "panel", //检索结果面板
        enableAutoPan: true, //自动平移
        searchTypes: [
        ]
    };


    var markers = [];
    var contents = [];
    $.get('../api/MapBoard?userId=' + $('#userId').attr('data-userid')).success(function (data) {
        for (var i = 0; i < data.length; i++) {
            var marker = new BMap.Marker(new BMap.Point(data[i].Longitude, data[i].Latitude), {icon:myIcon});
            map.addOverlay(marker);// 将标注添加到地图中
            var content = getBaseInfoTemplates()
                .replace('@Pic', data[i].Pic)
                .replace('@Name', data[i].Name)
                .replace('@Status', data[i].Status)
                .replace('@AuthorityCode', data[i].AuthorityCode)
                .replace('@Longitude', data[i].Longitude)
                .replace('@Latitude', data[i].Latitude)
                .replace('@WorkSite', data[i].WorkSite)
                .replace('@Company', data[i].Company)
                .replace('@Description', data[i].Description);
            contents.push(content);
            markers.push(marker);
            marker.addEventListener("mouseover", function (e) {
                var searchWindow = new BMapLib.SearchInfoWindow(map, getCurrentContent(contents, markers, e.currentTarget), opts);
                searchWindow.open(e.currentTarget);
            });
            marker.addEventListener("click", function (e) {
                var searchWindow = new BMapLib.SearchInfoWindow(map, getCurrentContent(contents, markers, e.currentTarget), opts);
                searchWindow.open(e.currentTarget);
            });
        }

        //最简单的用法，生成一个marker数组，然后调用markerClusterer类即可。
        var markerClusterer = new BMapLib.MarkerClusterer(map, { markers: markers });

    }).error(function(error) {
        alert(error);
    });





});


function getCurrentContent(contentArray, markerArray, currentMarker) {
    for (var i = 0; i < markerArray.length; i++) {
        if (markerArray[i].ba == currentMarker.ba) {
            return contentArray[i];
        }
    }
    return '';
}






function hrefTo(authCode) {
    $.get('../api/MapBoard?userId=' + $('#userId').attr('data-userid') + '&authorityCode=' + authCode).success(function(data) {
        if (data == '') {
            return;
        }

        window.location.href = '../ControlDesk/Index?menuId=' + data;
    }).error(function(error) {
        alert(error);
    });
}

function getBaseInfoTemplates() {
    return ['<table>',
        '<tr>',
        '<td>名称:<span onclick="hrefTo(\'@AuthorityCode\')" class="BaseInfoName">@Name</span></td>',
        '<td rowspan="6"><img src="@Pic" alt="" style="float:right;zoom:1;overflow:hidden;width:100px;height:100px;margin-left:3px;"/></td>',
        '</tr>',
        '<tr>',
        '<td>状态:<span>@Status</span></td>',
        '<td></td>',
        '</tr>',
        '<tr>',
        '<td>经度:<span>@Longitude</span></td>',
        '<td></td>',
        '</tr>',
        '<tr>',
        '<td>纬度:<span>@Latitude</span></td>',
        '<td></td>',
        '</tr>',
        '<tr>',
        '<td>工地:<span>@WorkSite</span></td>',
        '<td></td>',
        '</tr>',
        '<tr>',
        '<td>公司:<span>@Company</span></td>',
        '<td></td>',
        '</tr>',
        '<tr>',
        '<td colspan="2">备注:<span>@Description</span></td>',
        '</tr>',
        '</table>'
    ].join('');
    //return ['<img src="@Pic" alt="" style="float:right;zoom:1;overflow:hidden;width:100px;height:100px;margin-left:3px;"/>',
    //    '名称:<span>@Name</span><br/>',
    //    '状态:<span>@Status</span><br/>',
    //    '经度:<span>@Longitude</span><br/>',
    //    '纬度:<span>@Latitude</span><br/>',
    //    '工地:<span>@WorkSite</span><br/>',
    //    '公司:<span>@Company</span><br/>',
    //    '备注:<span>@Description</span>'].join('');
}



