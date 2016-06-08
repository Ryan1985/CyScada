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


    $.get('../api/CurrentGPS?sideMenuId=' + $('#sideMenuId').text() + '&userId=' + $('#userId').attr('data-userid')).success(function (data) {
        map.centerAndZoom(new window.BMap.Point(data.Longitude, data.Latitude), 14);
        var marker = new BMap.Marker(new BMap.Point(data.Longitude, data.Latitude), { icon: myIcon });
        map.addOverlay(marker);
        var marker2 = new BMap.Marker(new BMap.Point(Number(data.Longitude)+5, Number(data.Latitude)-5), { icon: myIcon });
        map.addOverlay(marker2);
        var driving = new BMap.DrivingRoute(map, { renderOptions: { map: map, autoViewport: true } });
        driving.search(marker.point, marker2.point);
    }).error(function (error) {
        alert(error);
    });

});

