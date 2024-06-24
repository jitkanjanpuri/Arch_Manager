var app = angular.module("myApp", []);
app.controller("myCntr", function ($scope, $http) {

    $scope.loading = true;
    $scope.fromStart = new Date();
    $scope.toEnd = new Date();
    $scope.ShowBalanceAdjust = function () {
        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;
        var varCName = $scope.txtCNameBalanceAdjust;

        $scope.totaloldbalance = "0";
        $scope.totaladjustbalance = "0";
        $scope.adjustbalancelist = null;
        var oldbal = 0, adjustbal = 0;
        $scope.searchmsg = "";
        $scope.recordmsg = "";
        if ((vardt1 == null) || (vardt2 == null))  {

            $scope.searchmsg  = "Please select date for data search";
            return;
        }
        $scope.loading = false;

        $http({
            url: "/Admin/ShowBalanceAdjust",
            method: "GET",
            dataType: "json",
            params: {

                dt1: vardt1,
                dt2: vardt2,
                cname: varCName
            },
            contentType: "application/json; charaset= utf-8"
        }).then(function (d) {
            $scope.adjustbalancelist = d.data;
            $scope.loading = true;


            if ($scope.adjustbalancelist.length == 0) {
                $scope.recordmsg = "Record is not available";
                return;
            }
            for (var i = 0; i < $scope.adjustbalancelist.length; i++) {
                slist = $scope.adjustbalancelist[i];
                oldbal = oldbal + parseInt(slist.oldBalance);
                adjustbal = adjustbal + parseInt(slist.adjustBalance);

            }
            $scope.totaloldbalance = oldbal;
            $scope.totaladjustbalance = adjustbal;
        }).error(function (err) {
            alert("Error : " + err);
        });

    }

    

    
});

 