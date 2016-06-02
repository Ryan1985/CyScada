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
    //$('#allmap').css('height', $(window).height() - $('#navHeader').height());

    var myIcon = new BMap.Icon("../../img/markers.png", new BMap.Size(50, 25), {
        offset: new BMap.Size(0, 0), // 指定定位位置
        imageOffset: new BMap.Size(0, 0) // 设置图片偏移
    });

    $.get('../api/MapBoard?userId=' + $('#userId').attr('data-userid')).success(function (data) {
        var markers = [];
        for (var i = 0; i < data.length; i++) {
            var marker = new BMap.Marker(new BMap.Point(data[i].Longitude, data[i].Latitude), {icon:myIcon});  // 创建标注
            map.addOverlay(marker);              // 将标注添加到地图中
            markers.push(marker);
        }

        //最简单的用法，生成一个marker数组，然后调用markerClusterer类即可。
        var markerClusterer = new BMapLib.MarkerClusterer(map, { markers: markers });

    }).error(function(error) {
        alert(error);
    });




});