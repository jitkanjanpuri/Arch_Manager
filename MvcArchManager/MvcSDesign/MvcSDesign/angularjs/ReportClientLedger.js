var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, $window) {

    $scope.loading = true;
    $scope.loading1 = true;
    

    var varCID = 0;

    $scope.fromStart = new Date();
    $scope.toEnd = new Date();

    $scope.ShowClientLadger = function () {
        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;
        var varcname = $scope.txtCName;
        var balance = 0;
        var clist;
        $scope.clientLedger = null;
        $scope.totalclientbalance = "0";
        $scope.searchdmsg = "";
         
        if ((vardt1 == null) || (vardt2 == null)) {

            $scope.searchmsg = "Please select date for data search";
            return;
        }

        $scope.loading = false;;

        $http({
            url: "/Admin/RptClientLedger",
            dataType: 'json',
            method: 'POST',
            params: {
                cname: varcname
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.clientLedger = d.data;
            $scope.loading = true;
            if ($scope.clientLedger.length == 0) {
                alert("Record is not available");
                return;
            }
            for (i = 0; i < $scope.clientLedger.length; i++) {
                clist = $scope.clientLedger[i];
                balance += parseInt(clist.mobile);

            }
            $scope.totalclientbalance = balance;

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }

    $scope.BalanceAdjust = function () {
        $scope.cName = $scope.clientname;
        $scope.balAmount = $scope.clientbalance;
    }


    $scope.SaveAdjustBalance = function () {

        if ($scope.txtAmount == undefined) {
            alert("Please enter adjust amount");
            return;
        }
        else if (($scope.txtAmount.length == 0) || ($scope.txtAmount == "0")) {
            alert("Please enter adjust amount");
            return;
        }

        $http({
            url: "/Admin/SaveBalanceAdjust",
            method: "POST",
            dataType: "json",
            params: {
                clientID: varCID,
                balance: $scope.balAmount,
                amount: $scope.txtAmount,
                remark: $scope.txtRemark
            },
            contentType: "application/json;charaset = utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Data successfully saved");
                location.reload();
            }
            else {
                alert(" Error " + d.data);
            }

        }).error(function (err) {
            alert(" Error : " + err);
        });

    }




    $scope.ShowLedgerDetail = function (cid, clientName, city, state) {
        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;
        $scope.txtBalance = "";

        var slist;
        $scope.clientname = clientName;
        $scope.clientaddress = city + ", " + state;
        $scope.previousbalance = "0";
        varCID = cid;

        $scope.ledgerDeatil = null;
        $scope.loading1 = false;
        $http({
            url: "/Admin/PreviousAmount",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: cid,
                fromDt: vardt1

            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.previousbalance = d.data;
            getLedger(clientName, cid, vardt1, vardt2);
        }).error(function (err) {
            alert(" Error : " + err);
        });



    }


    function getLedger(clientName, cid, vardt1, vardt2) {
        $scope.clientbalance = null;
        $scope.recordMsg = "";


        $http({
            url: "/Admin/RptClientLedgerDetail",
            dataType: 'json',
            method: 'POST',
            params: {
                clientName: clientName,
                clientID: cid,
                fromDt: vardt1,
                toDt: vardt2
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.ledgerDeatil = d.data;
            $scope.loading1 = true;
            if ($scope.ledgerDeatil.length == 0) {
                $scope.recordMsg ="Record is not available";
                return;
            }
            for (var i = 0; i < $scope.ledgerDeatil.length; i++) {
                slist = $scope.ledgerDeatil[i];
                $scope.clientbalance = slist.balance;
                $scope.txtBalance = $scope.clientbalance;
            }
          

        });
    }


    $scope.ClientReportExport = function () {
        
        $("#tblClientLedger").table2excel(
            {
                filename: "ClientLedger.xls"
            })

    }

   


    $scope.ClientPDFReport = function () {

        if ($scope.ledgerDeatil.length == 0) {
            return;
        }
        $scope.loading = false;
        $http({
            url: "/Admin/ClientLedgerPDF",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: varCID,
                cname: $scope.clientname,
                addr: $scope.clientaddress,
                fromDt: $scope.fromStart,
                toDt: $scope.toEnd
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.loading = true;
            var arr = location.href.split('/');
            var url = "http://" + arr[2] + "/pdf_files/" + d.data.replace('"', '');
            url = url.replace('"', '');
            $window.open(url, '_blank');
             
        }).error(function (err) {
            alert("Error " + err);
        });
    }

     



});



