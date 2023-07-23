var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http) {

    $scope.loading = true;
    $scope.totalclientreceive = "0";

    $scope.fromStart = new Date();
    $scope.toEnd = new Date();

    $scope.ShowClientReceive = function () {
        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;
        var varcname = $scope.txtClientReceive;

        $scope.clientReceiveList = null;
        $scope.totalclientreceive = "0";

        $scope.loading = false;
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
                $scope.recordmsg = "Record is not available";
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
 