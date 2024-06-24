var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, upd) {

    var clientLedgerID = 0;
    $scope.finalizeWindow = true;

    upd.getRecord().then(function (d) {
        $scope.ledgerList = d.data;

    });

    $scope.CloseAssignWindow = function () {
        $scope.finalizeWindow = true;
    }
    function getLedger() {
        $scope.ledgerList = null;
        $http(
            {
                url: "/Admin/GetClientLedger",
                dataType: 'json',
                method: 'POST',
                contentType: "application/json; charaset=utf-8"
            }).then(function (d) {
                $scope.ledgerList = d.data;
            });
    }


    $scope.UpdateLedger = function (item) {
        //alert("Test");
        //$http(
        //    {
        //        url: "/Admin/UpdateLedger",
        //        dataType: 'json',
        //        method: 'POST',
        //        params: {
        //            clientLedgerID: item.clientLedgerID,
        //            amount: item.amount,
        //            receivedAmount: item.receivedAmount,
        //            balance: item.balance,
        //            remark: item.remark
        //        },
        //        contentType: "application/json; charaset=utf-8"
        //    }).then(function (d) {
        //        $scope.ledgerList = d.data;
        //    });

        //var arr = [];
        //for (var i = 0; i < $scope.ledgerList.length; i++) {

        //    arr = $scope.ledgerList[i];
        //    if (arr["clientLedgerID"] == item.clientLedgerID) {
        //        if (arr["amount"] != 0) {
        //            var amt = document.getElementById("amount_" + item.clientLedgerID).value;
        //            var bal = amt - item.amount;
        //            arr["amount"] = amt;
        //            arr["balance"] = arr["balance"] + bal;
        //        }
        //        else {
        //            var amt = document.getElementById("receivedAmount_" + item.clientLedgerID).value;
        //            var bal = amt - item.receivedAmount;
        //            arr["amount"] = amt;
        //            arr["balance"] = arr["balance"] + bal;

        //        }

        //    }
        //}
        clientLedgerID = item.clientLedgerID;
        $scope.finalizeWindow = false;

        $scope.projectID = item.projectID;
        $scope.amount = item.amount;
        $scope.receivedAmount = item.receivedAmount;
        $scope.newAmount = 0;
        $scope.remark = item.remark;
    }

    $scope.Save = function () {

        if (($scope.newAmount == null) || ($scope.newAmount == null) || ($scope.newAmount == null)) {

            alert("Please enter amount");
            return;
        }


        $http({
            url: "/Admin/UpdateLedger",
            dataType: 'json',
            method: 'POST',
            params: {
                id: clientLedgerID,
                amount: $scope.amount,
                receivedAmount: $scope.receivedAmount,
                newAmount: $scope.newAmount,
                remark: $scope.remark
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {


                clientLedgerID = 0;
                $scope.finalizeWindow = true;

                $scope.projectID = 0;
                $scope.amount = 0;
                $scope.receivedAmount = 0;
                $scope.newAmount = 0;
                $scope.remark = "";
                alert("Record successfully updated");
                getLedger();

            }
            else {
                alert(" Result " + d.data);
            }

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }






    //$scope.ShowClientReceive = function () {
    //    var vardt1 = $scope.fromStat;
    //    var vardt2 = $scope.toEnd;
    //    var varcname = $scope.txtClientReceive;

    //    $scope.clientReceiveList = null;
    //    $scope.totalclientreceive = "0";
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
    //        $scope.clientReceiveList = d.data;

    //        if ($scope.clientReceiveList.length == 0) {
    //            alert("Record is not available");
    //            return;
    //        }


    //        var len = $scope.clientReceiveList.length - 1;
    //        slist = $scope.clientReceiveList[len];
    //        $scope.totalclientreceive = slist.balance;
    //    }).error(function (err) {
    //        alert(" Error : " + err);
    //    });

    //}




});



app.factory('upd', function ($http) {

    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/GetClientLedger');
    }
    return fac;
});