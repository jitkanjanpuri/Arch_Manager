var app = angular.module("myApp", [])
app.controller('myCnt', function ($scope, $http, dashFac) {
 

    dashFac.getRecord().then(function (d) {
        var confirmamount = 0;
        var pendingqty = 0;
        var pendingamount = 0;
        var confirmqty = 0;
        var qlist = [];
        for (var i = 0; i < d.data.length; i++) {
            qlist = d.data[i];
            if (qlist.status == "request") {
                pendingqty++;
            }
            else {
                confirmqty++;
                confirmamount += qlist.amount;
            }
        }
        $scope.quotation = confirmqty + pendingqty;
        $scope.sales = confirmamount;
        $scope.prjDay = confirmqty;
    });
 

    
});

app.factory('dashFac', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Client/Dashboard_getMonthQuotation');
    };
    return fac;


});

 