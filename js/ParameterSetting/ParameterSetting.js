
var dataQueue = [];

$(function () {

    $.get('../api/ParameterSetting?sideMenuId=' + $('#sideMenuId').text() + '&userId=' + $('#userId').attr('data-userid'), function (machineInfo) {
        setInterval(function () {
            $.get('../api/ParameterSetting', function (data) {
                dataQueue.push(data);
            });
        }, 2000);

        setInterval(function () {

            if (dataQueue.length > 0) {

                var data = dataQueue.splice(0, 1)[0];

                $('#Height').attr('data-TagKey', machineInfo[0].Key);
                $('#Distance').attr('data-TagKey', machineInfo[1].Key);
                $('#Weight').attr('data-TagKey', machineInfo[2].Key);
                $('#Speed').attr('data-TagKey', machineInfo[3].Key);
                $('#Range').attr('data-TagKey', machineInfo[4].Key);
                $('#Frequency').attr('data-TagKey', machineInfo[5].Key);
                $('#Pedal').attr('data-TagKey', machineInfo[6].Key);
                $('#Turn').attr('data-TagKey', machineInfo[7].Key);

                $('#lblHeight').text(Number(data[machineInfo[0].Key].Value));
                $('#lblDistance').text(Number(data[machineInfo[1].Key].Value));
                $('#lblWeight').text(Number(data[machineInfo[2].Key].Value));
                $('#lblSpeed').text(Number(data[machineInfo[3].Key].Value));
                $('#lblRange').text(Number(data[machineInfo[4].Key].Value));
                $('#lblFrequency').text(Number(data[machineInfo[5].Key].Value));
                $('#lblPedal').text(Number(data[machineInfo[6].Key].Value));
                $('#lblTurn').text(Number(data[machineInfo[7].Key].Value));
            }
        }, 500);


    });


});



function setValue(obj) {
    $.post('../api/ParameterSetting', { '': [$(obj).attr('data-TagKey'), $('#txt' + obj.id).val()] }, function () {
    }, function (error) { alert(error); });
}

