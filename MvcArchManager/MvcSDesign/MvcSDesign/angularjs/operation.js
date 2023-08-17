var app = angular.module("myApp", []);
app.controller("myCntr", function ($scope, $http, opt) {
     




    opt.getRecord().then(function (d) {
        var perday = d.data / 30;
        var perdayval = perday / 10;
        $('.average-per-day-circle').circleProgress({
            value: perdayval,
            fill: { gradient: ['#5ce497', '#469EFB'] },
            size: 260,
        }).on('circle-animation-progress', function (event, progress) {
            $(this).find('strong').html(Math.round(perday * progress));
        });

        showWeeklyPerformance();

    });



    function showWeeklyPerformance() {

        $scope.dtArr = [];
        $scope.prjcntArr = [];
        $scope.achieveArr = [];
        var slist;
        $http({
            url: "/Render/getWeeklyPerformance",
            dataType: "json",
            method: "POST",
            contentType: "application/json; charaset =utf-8"
        }).then(function (d) {
            for (var i = 0; i < d.data.length; i++) {
                slist = d.data[i];
                $scope.dtArr.push(slist.dtstr);
                $scope.prjcntArr.push(slist.amount);
                $scope.achieveArr.push(slist.amount);
            }
            var ctx = document.getElementById('dvMonthTarget');
            if (ctx) {
                var mq = window.matchMedia("(max-width: 570px)");
                if (mq.matches) {
                    ctx.height = 600;
                }
                else {
                    ctx.height = 160;
                }


                var chart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        datasets: [{
                            label: 'Submitted Projects',
                            data: $scope.prjcntArr,
                            backgroundColor: '#469EFB',
                            order: 2,
                        }, {
                            label: 'Average Line',
                            borderColor: '#5CE497',
                            backgroundColor: 'transparent',
                            data: $scope.achieveArr,
                            type: 'line',
                            order: 1
                        }],
                        labels: $scope.dtArr,
                    },

                    options: {
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true,
                                    stepSize: 2
                                }
                            }]
                        }
                    }
                });
            }

         //   getProjectCategoryPerformance();

        });
    }
    function getProjectCategoryPerformance() {
        var slist;
        $scope.prjTypeArr = [];
        $scope.draftArr = [];
        $scope.revisedArr = [];
        $scope.finalArr = [];
        $http({
            url: "/Render/getProjectCategoryPerformance",
            dataType: "json",
            method: "POST",
            contentType: "application/json; charaset =utf-8"
        }).then(function (d) {
            for (var i = 0; i < d.data.length; i++) {
                slist = d.data[i];
                $scope.prjTypeArr.push(slist.projectType);
                $scope.draftArr.push(slist.draft);
                $scope.revisedArr.push(slist.revised);
                $scope.finalArr.push(slist.final);
            }


            var ctx2 = document.getElementById('stacked-bars-chart').getContext('2d');
            if (ctx2) {
                var mq = window.matchMedia("(max-width: 570px)");


                if (mq.matches) {
                    ctx2.height = 200;

                }
                else {

                    ctx2.height = 100;
                }
                var barChartData = new Chart(ctx2, {
                    type: 'horizontalBar',
                    data: {
                        labels: $scope.prjTypeArr,
                        datasets: [{
                            label: 'Draft View',
                            backgroundColor: "#DC828F",
                            data: $scope.draftArr
                        }, {
                            label: 'Revised View',
                            backgroundColor: "#F7CE76",
                            data: $scope.revisedArr
                        }, {
                            label: 'Final View',
                            backgroundColor: "#8C7386",
                            data: $scope.finalArr,
                        }]
                    },
                    options: {
                        responsive: true,
                        scales: {
                            xAxes: [{
                                stacked: true,
                            }],
                            yAxes: [{
                                stacked: true
                            }]
                        }
                    }
                });
            }
        });



    }




    $scope.ShowHistory = function (id) {
        $scope.projectID = id;
        $scope.imglist = null;
        $scope.historyStatus = "";
        $scope.rowHeight = 0;
        $scope.chkThumbnail = false;
        $scope.buttonDisable = true;
        $scope.thumbnailwidth = 400;
        $scope.thumbnailheight = 400;
        projectID = id;
        $http({
            url: "/Render/getThumbnail",
            dataType: "json",
            method: "POST",
            params: {
                projectID: $scope.projectID
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {

            $scope.imglist = d.data;

        }).error(function (err) {
            alert("Error " + err);
        });
    }

    $scope.closeHistory = function () {
        $scope.projectID = "";
        $scope.imglist = null;

    }

    $scope.buttonActive = function () {
        if ($scope.chkThumbnail == true)
            $scope.buttonDisable = false;
        else
            $scope.buttonDisable = true;
    }


    $scope.acceptTerms = function () {
        if ($scope.chkThumbnail == true) {
            $scope.withoutAuth_[projectID] = true;
            $scope.withAuth_[projectID] = true;

        }
        else {
            $scope.lblTermErr = "Please select check box";
        }

    }


});

app.factory('opt', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get("/Render/fillMonthlyPerformance");
    }
    return fac;

});
