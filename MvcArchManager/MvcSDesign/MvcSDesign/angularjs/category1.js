var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http, reg) {

    Show();

    function Show() {
        alert("Test");
    }

    reg.getRecord().then(function (d) {
        $scope.categorylist = d.data;
    });

    //$scope.AllClient = function () {
    //    $http({
    //        url: "/Client/AllClient",
    //        dataType: 'json',
    //        method: 'POST',
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        $scope.allclient = d.data;
    //        if ($scope.allclient.length == 0) {
    //            alert("Record not available");
    //            return;
    //        }
    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });
    //}

    //$scope.ShowAmountReceive = function (cid, cname) {
    //    $scope.errReceive = "";
    //    varClientID = cid;
    //    $scope.txtCName = cname;
    //    $scope.txtAmount = "";
    //    $scope.txtRemark = "";
    //}


    //$scope.ShowDelete = function (clientID, cname) {

    //    $scope.clientname = cname;
    //    varClientID = clientID;

    //}



    //$scope.Delete = function () {

    //    $http({
    //        url: "/Client/DeleteClient",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            cid: varClientID
    //        },
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "") {
    //            alert("Client successfully deleted");

    //            location.reload();
    //        }
    //        else {
    //            alert("Error " + d.data);
    //        }

    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });

    //}

});
app.factory('reg', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/GetAllCategory')
    }
    return fac;
});
