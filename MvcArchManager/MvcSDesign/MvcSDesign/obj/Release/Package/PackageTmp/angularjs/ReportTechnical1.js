var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, $window, tech) {

    $scope.loading = true;
    $scope.loading1 = true;

    var barChartData = null;
    var varCID = 0;

    $scope.fromStart = new Date();
    $scope.toEnd = new Date();

    tech.getRecord().then(function (d) {
        $scope.designerList = d.data;
        $scope.dname = $scope.designerList[0];
    });



    $scope.TechGraph = function () {

        $scope.employeeArray = [];
        $scope.targetArray = [];
        $scope.achieveArray = [];

        var dts = [];
        var slist, tmpList;
        var arr1 = {};
        $scope.bgcolorArr = ["#469EFB", "#469EFB", "#469EFB", "#469EFB", "#469EFB", "#469EFB"];
        $scope.staffArr = [];
        $scope.lableArr = [];
        $scope.dataArr = [];

        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;
        var designerID = $scope.dname.Value;
        var dname = $scope.dname.Text;
        ctx2 = null;

        if ((vardt1 == null) || (vardt2 == null)) {

            $scope.searchmsg = "Please select date for data search";
            return;
        }

        $scope.loading = false;


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
            url: '/Admin/RptTechnical',
            dataType: 'json',
            method: 'POST',
            params: {
                designerID: designerID,
                fromDt: vardt1,
                toDt: vardt2,
                dname :dname 
            },
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
            $scope.loading = true;
            getProjectCount(data1);
        })
    }

    function getProjectCount(data1) {

        if (barChartData) barChartData.destroy();
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
            barChartData = new Chart(ctx2, {
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
    }




});


app.factory('tech', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getOperationDesigner');
    }

    return fac;
});

