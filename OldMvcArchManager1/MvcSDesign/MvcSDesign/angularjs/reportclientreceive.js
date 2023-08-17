var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http) {

     
    $scope.loading = true;

    var d = new Date();
    var cur_date = d.getDate();
    var cur_month = d.getMonth() + 1;
    var cur_yr = d.getFullYear();
    $scope.totalclientreceive = "0";
     
    $scope.toEnd = cur_month + "-" + cur_date + "-" + cur_yr;
    $scope.fromStat = cur_month + "-" + cur_date + "-" + cur_yr;

     
    $scope.ShowClientReceive = function () {
        var vardt1 = $scope.fromStat;
        var vardt2 = $scope.toEnd;
        var varcname = $scope.txtClientReceive;

        $scope.clientReceiveList = null;
        $scope.totalclientreceive = "0";

        $scope.loading = true;
        var slist;
        $http({
            url: "/Admin/RptClientReceive",
            dataType: 'json',
            method: 'POST',
            params: {
                cname: varcname,
                fromDt: vardt1,
                toDt: vardt2
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.clientReceiveList = d.data;
            $scope.loading = true;
            if ($scope.clientReceiveList.length == 0) {
                $scope.recordmsg= "Record is not available";
                return;
            }


            var len = $scope.clientReceiveList.length - 1;
            slist = $scope.clientReceiveList[len];
            $scope.totalclientreceive = slist.balance;
        }).error(function (err) {
            alert(" Error : " + err);
        });

    }

     

     
     
});

app.directive("datepicker", function () {

    function link(scope, element, attrs) {
        element.datepicker({
            dateFormat: "mm-dd-yy"
        });
    }
    return {
        require: 'ngModel',
        link: link
    };
});

