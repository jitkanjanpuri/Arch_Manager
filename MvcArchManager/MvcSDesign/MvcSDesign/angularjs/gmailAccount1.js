var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http, gm) {

    //ShowGmailAccount();


    gm.getRecord().then(function (d) {
        $scope.gmailList = d.data;

    });



    $scope.Submit = function () {
        var gmail = $scope.txtGmailID;
        var pw = $scope.txtPassword;
        if (gmail == null) {
            alert("Please enter Gmail ID")
            return;
        }
        else if (gmail == "") {
            alert("Please enter Gmail ID")
            return;
        }
        else if (pw == null) {
            alert("Please enter Gmail Password");
            return;
        }
        else if (pw == "") {
            alert("Please enter Gmail Password");
            return;
        }


        $http({
            url: "/Admin/SaveGMail",
            dataType: 'json',
            method: 'POST',
            params: {
                gmailID: gmail,
                pwd: pw

            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert(" Record successfully saved");
                $scope.txtGmailID = "";
                $scope.txtPassword = "";
                ShowGmailAccount();
            }
            else {
                alert(d.data);
            }
        }).error(function (error) {
            alert(error);
        });

    }


    function ShowGmailAccount() {
        $scope.gmailList = null;
        $http({
            url: "/Admin/getGmailAccount",
            dataType: 'json',
            method: 'POST',
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.gmailList = d.data;
            if ($scope.gmailList.length == 0) {
                alert("No record available");
                return;
            }

        }).error(function (error) {
            alert(error);
        });
         
    }
     

    $scope.GmailRemove = function (varID, gmail) {

        var con = confirm(" Do you want to Delete this " + gmail + " ID ?");
        if (con) {
            $http({
                url: "/Admin/RemoveGMailAccount",
                dataType: 'json',
                method: 'POST',
                params: {
                    id: varID
                },
                contentType: "application/json;charaset=utf-8"
            }).then(function (d) {
                alert(d);
                alert(d.data);
            }).error(function (err) {
                alert("Error " + err);
            });
        }
    }

});

app.factory('gm', function ($http) {

    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getGmailAccount');
    }
    return fac;
});