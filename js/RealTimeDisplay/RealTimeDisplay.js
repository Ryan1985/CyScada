
//实时温度
$(function () {
    if ($('#temperatureChart')) {
        //Get context with jQuery - using jQuery's .get() method.
        var ctx = $("#temperatureChart").get(0).getContext("2d");
        //This will get the first returned node in the jQuery collection.

        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "温度曲线",
                    data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()],
                    fill: true,
                    borderDash: [5, 5],
                }]
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: '起重机温度'
                },
                tooltips: {
                    mode: 'label',
                    callbacks: {
                        // beforeTitle: function() {
                        //     return '...beforeTitle';
                        // },
                        // afterTitle: function() {
                        //     return '...afterTitle';
                        // },
                        // beforeBody: function() {
                        //     return '...beforeBody';
                        // },
                        // afterBody: function() {
                        //     return '...afterBody';
                        // },
                        // beforeFooter: function() {
                        //     return '...beforeFooter';
                        // },
                        // footer: function() {
                        //     return 'Footer';
                        // },
                        // afterFooter: function() {
                        //     return '...afterFooter';
                        // },
                    }
                },
                hover: {
                    mode: 'dataset'
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            show: true,
                            labelString: '时间'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            show: true,
                            labelString: '摄氏度(℃)'
                        },
                        ticks: {
                            suggestedMin: -10,
                            suggestedMax: 250,
                        }
                    }]
                }
            }
        };


        $.each(config.data.datasets, function (i, dataset) {
            dataset.borderColor = 'rgba(230,230,230,1)';
            dataset.backgroundColor = 'rgba(230,230,230,0.5)';
            dataset.pointBorderColor = 'rgba(230,230,230,1)';
            dataset.pointBackgroundColor = 'rgba(230,230,230,1)';
            dataset.pointBorderWidth = 1;
        });

        var tempChart = new Chart(ctx,config);

        setInterval(update, 1000);

        function update() {

            if (config.data.datasets.length > 0) {
                //var month = MONTHS[config.data.labels.length % MONTHS.length];
                //config.data.labels.push(month);


                config.data.labels.push($('#hours').text() + ':' + $('#min').text() + ':' + $('#sec').text());

                $.each(config.data.datasets, function (i, dataset) {
                    dataset.data.push(randomScalingFactor());
                });

                if (config.data.labels.length > 30) {
                    config.data.labels.splice(0, 1); // remove the label first

                    config.data.datasets.forEach(function (dataset, datasetIndex) {
                        dataset.data.splice(0, 1);
                    });
                }

                tempChart.update();
            }
        };



    }
});

var MONTHS = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

var randomScalingFactor = function () {
    return Math.round(Math.random() * 100);
    //return 0;
};

