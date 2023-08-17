var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http) {

    $scope.loading = true;
    $scope.totalclientreceive = "0";

    $scope.fromStart = new Date();
    $scope.toEnd = new Date();

    $scope.viewby = 10;
    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize = 5; //Number of pager buttons to show

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };



    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage = 1; //reset to first page
    }



    $scope.ShowClientReceive = function () {
        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;
        var varcname = $scope.txtClientReceive;

        $scope.clientReceiveList = null;
        $scope.totalclientreceive = "0";
        $scope.recordmsg = "";
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
            $scope.totalItems = $scope.clientReceiveList.length;

            $scope.currentPage = 1;



            if ($scope.clientReceiveList.length == 0) {
                $scope.recordmsg = "Record is not available";
                return;
            }
            var total = 0;
            for (i = 0; i < $scope.clientReceiveList.length; i++) {
                slist = $scope.clientReceiveList[i];
                total += slist.receivedAmount;
            }
            
            $scope.totalclientreceive = total;
        }).error(function (err) {
            alert(" Error : " + err);
        });

    }


});
