var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {

    $scope.loading = true;
    $scope.totalclientreceive = "0";

    $scope.fromStart = new Date();
    $scope.toEnd = new Date();

    //$scope.ShowClientReceive = function () {
    //    var vardt1 = $scope.fromStart;
    //    var vardt2 = $scope.toEnd;
    //    var varcname = $scope.txtClientReceive;

    //    $scope.statuslist = null;
    //    $scope.totalclientreceive = "0";
    //    $scope.recordmsg = "";
    //    $scope.loading = false;
    //    var slist;

    //    $http({
    //        url: "/Admin/RptClientReceive",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            cname: varcname,
    //            fromDt: vardt1,
    //            toDt: vardt2
    //        },
    //        contentType: "application/json;charaset=utf-8"
    //    }).then(function (d) {
    //        $scope.statuslist = d.data;
    //        $scope.loading = true;
    //        if ($scope.statuslist.length == 0) {
    //            $scope.recordmsg = "Record is not available";
    //            return;
    //        }
    //        var total = 0;
    //        for (i = 0; i < $scope.statuslist.length; i++) {
    //            slist = $scope.statuslist[i];
    //            total += slist.receivedAmount;
    //        }

    //        $scope.totalclientreceive = total;
    //    }).error(function (err) {
    //        alert(" Error : " + err);
    //    });

    //}


});
