var app = angular.module("myApp", []);
app.controller("cntr", function ($scope, $http, $interval) {
    getProjectType();
    var mycall = 1;
    function getProjectType() {

        $scope.employeeArray = [];
        $scope.targetArray = [];
        $scope.achieveArray = [];

        var dts = [];
        var slist, tmpList;
        var arr1 = {};
        $scope.bgcolorArr = ["#DC828F", "#F7CE76", "#E8D6CF", "#8C7386", "#9C9359", "#5CE497"];
        $scope.staffArr = [];
        $scope.lableArr = [];
        $scope.dataArr = [];
        $scope.dataValues = [
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        ];


        $http({
            url: '/Admin/DashBoard_getProjectType',
            dataType: 'json',
            method: 'POST',

            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            tmpList = d.data;
            if (tmpList.length == 0) {
                getProjectCount();
                return;
            }
            var j;
            for (var i = 0; i < tmpList.length; i++) {
                slist = tmpList[i];
                $scope.staffArr.push(slist.clientName);
                $scope.lableArr.push(slist.projectType);
                $scope.dataArr.push(slist.package);
            }


            var ptArr = [];
            var strArr = [];
            var tmp = [];
            var ch = $scope.dataArr[0];
            ptArr = ch.split(",");
            ch = $scope.lableArr[0];
            strArr = ch.split(",");
            //put value in 2D array


            for (var i = 0; i < ptArr.length; i++) { // for rows
                for (j = 0; j < $scope.staffArr.length; j++) {// for column 
                    ch = $scope.dataArr[j];
                    tmp = ch.split(",");
                    $scope.dataValues[i][j] = tmp[i];
                }
            }
            for (var i = 0; i < ptArr.length; i++) {
                var arr1 = {
                    label: strArr[i],

                    backgroundColor: $scope.bgcolorArr[i],
                    data: $scope.dataValues[i]
                };
                dts.push(arr1);
            }

            var data1 = {
                labels: $scope.staffArr,
                datasets: dts
            };

            //var ctx = document.getElementById('chartProjectType');
            //if (ctx) {
            //    var mq = window.matchMedia("(max-width: 570px)");
            //    if (mq.matches) {
            //        ctx.height = 600;
            //    }
            //    else {
            //        ctx.height = 160;
            //    }
               
            //    var chart = new Chart(ctx, {
            //        type: 'bar',
            //        data: data1,

            //        options: {
            //            legend: {
            //                position: 'top',


            //            },
            //            scales: {
            //                xAxes: [{

            //                }],
            //                yAxes: [{
            //                    ticks: {
            //                        beginAtZero: true,

            //                    }
            //                }]
            //            }
            //        }
            //    });
            //}
            getProjectCount(data1);
        })
    }




    function getProjectCount(data1) {

        var ctx2 = document.getElementById('chartProjectCount');
        if (ctx2) {
            var ctx2 = ctx2.getContext('2d');
            var mq = window.matchMedia("(max-width: 570px)");

            if (mq.matches) {
                ctx2.height = 600;
            }
            else {
                ctx2.height = 160;
            }
            var barChartData = new Chart(ctx2, {
                type: 'bar',
                data: data1,

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
        getTopPerformers();
    }

    //function getNotification() {

    //    $scope.notificationlist = null;
    //    $http({
    //        url: '/Technical/getNotificationList',
    //        dataType: 'json',
    //        method: 'POST',
    //        contentType: "application/json;charaset=utf-8"
    //    }).then(function (d) {
    //        $scope.notificationlist = d.data;

    //        getTopPerformers()

    //    });
    //}

    function getTopPerformers() {
        $scope.topperformerslist = null;
        $http({
            url: '/Admin/getTopPerformers',
            dataType: 'json',
            method: 'POST',
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.topperformerslist = d.data;
            CurrentMonthQuotation();
        });


    }
    

    function CurrentMonthQuotation() {
        $scope.projectTypeArray = [];
        $scope.dataArray = [];
        var cnt = 0;


        $scope.lbl3DPlan = "";
        $scope.lblBirdEyeView = "";
        $scope.lblExterior = "";
        $scope.lblInterior = "";
        $scope.lblOther = "";
        $scope.lblPlanning = "";
        $scope.lblStructure = "";
        $scope.lblWalkthrough = "";

        //alert("getQuotation ");
        var slist, tmpList;
        $http({
            url: '/admin/getQuotation',
            dataType: 'json',
            method: 'POST',
            
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            tmpList = d.data;

            for (var i = 0; i < tmpList.length; i++) {
                slist = tmpList[i];
                $scope.projectTypeArray.push(slist.projectType);
                $scope.dataArray.push(slist.sno);
                cnt = cnt + slist.sno;

                switch (slist.projectType) {
                    case "3D_Floor":
                    case "3D Floor Plan":
                        $scope.lbl3DPlan = slist.sno;
                        break;
                    case "Bird_Eye_View":
                        $scope.lblBirdEyeView = slist.sno;
                        break;
                    case "Exterior":
                        $scope.lblExterior = slist.sno;
                        break;
                    case "Interior":
                        $scope.lblInterior = slist.sno;
                        break;
                    //case "Other":
                    //    $scope.lblOther = slist.sno;
                    //    break;
                    case "Planning":
                        $scope.lblPlanning = slist.sno;
                        break;
                    //case "Structure":
                    //    $scope.lblStructure = slist.sno;
                    //    break;
                    case "Walkthrough":
                        $scope.lblWalkthrough = slist.sno;
                        break;

                }
            }

            // type: 'pie',
            $scope.lblquotation = cnt;
            var ctx_pie_1 = document.getElementById("quotationPieChart");

            var quotationPie = new Chart(ctx_pie_1, {
                type: 'doughnut',
                data: {
                    labels: $scope.projectTypeArray,
                    datasets: [{
                        data: $scope.dataArray,
                        backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#5A6268', '#47A846', '#FEC22A', '#25A2B8', '#343A40'],
                        hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#47A246', '#FEC-2A', '#15A2B8', '#341A40'],
                        hoverBorderColor: "rgba(234, 236, 244, 1)",
                    }],
                },
                options: {
                    maintainAspectRatio: false,
                    tooltips: {
                        backgroundColor: "rgb(255,255,255)",
                        bodyFontColor: "#858796",
                        borderColor: '#dddfeb',
                        borderWidth: 1,
                        xPadding: 15,
                        yPadding: 15,
                        displayColors: false,
                        caretPadding: 10,
                    },
                    legend: {
                        display: false
                    },
                    cutoutPercentage: 80,
                },
            });


            var ctx1 = document.getElementById("dvCanvasQuotationPie");
            var tqChart = new Chart(ctx1, {
                type: 'doughnut',
                data: {
                    labels: $scope.projectTypeArray,
                    datasets: [{
                        data: $scope.dataArray,
                        backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#5A6268', '#47A846', '#FEC22A', '#25A2B8', '#343A40'],
                        hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#47A246', '#FEC-2A', '#15A2B8', '#341A40'],
                        hoverBorderColor: "rgba(234, 236, 244, 1)",
                    }],
                },
                options: {
                    maintainAspectRatio: false,
                    tooltips: {
                        backgroundColor: "rgb(255,255,255)",
                        bodyFontColor: "#858796",
                        borderColor: '#dddfeb',
                        borderWidth: 1,
                        xPadding: 15,
                        yPadding: 15,
                        displayColors: false,
                        caretPadding: 10,
                    },
                    legend: {
                        display: false
                    },
                    cutoutPercentage: 80,
                },
            });


            HightestMonthQuotation();

        }).error(function (error) {
            alert(error);
        });


    }
 




});




