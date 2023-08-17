var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http) {
 
    $scope.loading = true;
    $scope.fromStart = new Date();
    $scope.toEnd = new Date();

    $scope.ShowDesignerLedger = function () {
        $scope.loading = false;
        $http({
            url: "/Admin/RptDesignerLedger",
            dataType: 'json',
            method: 'POST',
            params:
            {
                dname: $scope.dname
            },

            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.designeramountlist = d.data;
            $scope.loading = true;

            if ($scope.designeramountlist.length == 0) {
                $scope.recordmsg ="Record is not available";
                return;
            }

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }


    $scope.ShowDesignerLedgerDetail = function (varsid, name, city) {
        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;

        $scope.designername = "Name :  " + name;
        $scope.designeraddress = "Address : " + city;


        $scope.designerbalance = "";
        $scope.ledgermsg = "";
        $scope.loading = false;
        $http({
            url: "/Admin/RptDesignerLedgerDetail",
            method: "POST",
            dataType: "json",
            params:
            {
                sid: varsid,
                dt1: vardt1,
                dt2: vardt2
            },
            contentType: "application/json; charaset =utf-8"
        }).then(function (d) {
            $scope.designerLedgerDeatil = d.data;

            $scope.loading = true;

            if ($scope.designerLedgerDeatil.length == 0) {
                $scope.ledgermsg= "Record is not available";
                return;
            }


            for (var i = 0; i < $scope.designerLedgerDeatil.length; i++) {
                slist = $scope.designerLedgerDeatil[i];
                $scope.designerbalance = slist.balance;
            }
        });
    }

     

     
});

 
