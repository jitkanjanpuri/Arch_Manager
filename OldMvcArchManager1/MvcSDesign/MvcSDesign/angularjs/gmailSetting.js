var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http, gm) {
 

    getGMailAccount();

    
    gm.getRecord().then(function (d) {
        alert("ID " + d.data.gmailID + " Pwd " + d.data.pwd);
    });


   
     

    //function getGMailAccount() {
    //    alert("TEst 5");
    //    $http({
    //        url: "/Admin/getGMailAccount",
    //        dataType: 'json',
    //        method: 'GET',
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        //$scope.allclient = d.data;
    //        //if ($scope.allclient.lenght == 0) {
    //        //    alert("Record not available");
    //        //    return;
    //        //}
    //        alert("ID " + d.data.gmailID + " Pwd " + d.data.pwd);
    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });
    //}

     
    

    $scope.AmountReceiveSave = function () {
        var varcid = $scope.txtCID;
        var varcname = $scope.txtCName;
        var varamount = $scope.txtAmount;
        var varremark = $scope.txtRemark;

        if (varcname == undefined) {
            alert("Please select client for amount receive ");
            return;
        }
        else if (varamount == undefined) {
            alert("Please enter amount");
            return;
        }
        else if ((varamount == "") || (varamount == "0")) {
            alert("Please enter amount");
            return;
        }
        var con = confirm("Do you want to receive Rs. " + varamount + " from " + varcname + " ?");
        if (!con) return;

        $http({
            url: "/Admin/AmountReceive",
            dataType: 'json',
            method: 'POST',
            params: {
                clientID: varcid,
                amount: varamount,
                remark: varremark

            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data == "Success") {
                alert("Client amount successfully received");
                ResetAmountReceiveWindow();
                return;
            }

        }).error(function (err) {
            alert("Error : " + err);
        });

    }
     

});


//app.factory("gm", function ($http) {
//    var fac = {};
//    fac.getRecord = function () {
//        return $http.get('/Admin/getGMailAccount');
//    }
//    return fac;

//});


//app.factory('gm', function ($http) {
//    alert("Ta");
//    var fac = {};
//    fac.getRecord = function () {
//        return $http.get('/Admin/getGMailAccount');
//    }
//    return fac;
//});

app.factory('gm', function ($http) {

    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getOperationDesigner');
    }
    return fac;
});