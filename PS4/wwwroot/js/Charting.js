// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
Author: Haoran Geng
Partner:   None
Date:      11 / 24 / 2021
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Haoran Geng - This work may not be copied for use in Academic Coursework.


File Contents:
JavaScript for PS9
*/



var chart;
$(document).ready(function () {
    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'container'
        },
        title: {
            text: 'Enrollments Over Time'
        },

        subtitle: {
            text: ''
        },

        yAxis: {
            title: {
                text: 'Students'
            }
        },

        xAxis: {

            title: { text: 'Dates' },
            categories: ['Nov 1', 'Nov 2', 'Nov 3', 'Nov 4', 'Nov 5', 'Nov 6', 'Nov 7', 'Nov 8', 'Nov 9', 'Nov 10', 'Nov 11',
                'Nov 12', 'Nov 13', 'Nov 14', 'Nov 15', 'Nov 16', 'Nov 17', 'Nov 18', 'Nov 19', 'Nov 20', 'Nov 21', 'Nov 22', 'Nov 23',
                'Nov 24', 'Nov 25', 'Nov 26', 'Nov 27', 'Nov 28', 'Nov 29', 'Nov 30'
            ],
            type: 'datetime'
        },

        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },
        plotOptions: {
            series: {
                label: {
                    connectorAllowed: false
                },
                pointStart: 0
            }
        },
        responsive: {
            rules: [{

                condition: {
                    maxWidth: 800
                },
                chartOptions: {
                    legend: {
                        layout: 'horizontal',
                        align: 'center',
                        verticalAlign: 'bottom'
                    }
                }
            }]
        }
    });
    var result;
    var URL = "/Administrator/GetAllEnrollmentData";
    var DATA = {
    };
    $.post(URL, DATA)
        .done(function (result) {
            console.log("get the enrollment");
            console.log(result);
            for (let k = 0; k < result.length; k++) {
                chart.addSeries(
                    {
                        name: result[k].name,
                        data: result[k].number,
                    });
            }
        })
        .fail(function (result) {
            console.log(result);
        })
        .always(function (result) {

        });


});

var st_date = 0;//0 if no changed the date
var ed_date = 0;//0 if no changed the date


$('#submit').on('click', function () {
    if (st_date == 0) {
        alert("set up a start date first!");
    }
    else if (ed_date == 0) {
        alert("set up a end date first!");
    }
    else {
        var sdate = new Date(st_date.valueOf() + 1000 * 3600 * 24);
        var edate = new Date(ed_date.valueOf() + 1000 * 3600 * 24);
        var txtbox = document.getElementById('className');
        console.log(sdate);
        console.log(edate);
        var class_name = txtbox.value;
        console.log(class_name);
        var URL = "/Administrator/GetEnrollmentData";
        var DATA = {
            start_date: sdate,
            end_date: edate,
            course: class_name
        };
        $.post(URL, DATA)
            .done(function (result) {
                console.log("get the enrollment");
                console.log(result);
                for (let k = 0; k < result.length; k++) {
                    while (chart.series.length > 0)
                        chart.series[0].remove(true);
                    chart.addSeries(
                        {
                            name: result[k].name,
                            data: result[k].number,
                        });
                }
            })
            .fail(function (result) {
                console.log(result);
            })
            .always(function (result) {

            });
    }

});

$('#start_date').change(function () {
    st_date = new Date(this.value);
});

$('#end_date').change(function () {
    ed_date = new Date(this.value);
});
