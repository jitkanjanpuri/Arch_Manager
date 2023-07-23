var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http) {

    $scope.composeWindow = true;

    $scope.ShowClient = function () {
        var varname = $scope.txtCName;
        var varcity = $scope.txtCity;

        if ((varname == undefined) && (varcity == undefined)) {
            alert("Please enter either client name or city ");
            return;
        }

        $http({
            url: "/Client/getClient_PromMail",
            dataType: 'json',
            method: "POST",
            params: {
                name: varname,
                city: varcity
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
            $scope.clist = d.data;
            if ($scope.clist.length == 0) {
                alert("Record not found");
                return;
            }

        }).error(function (err) {
            alert("Error  : " + err);
        });

    }

    $scope.toggleAll = function () {
        var toggleStatus =  $scope.isAllSelected;
        $scope.selected = toggleStatus;
       
    }

    $scope.SendMail = function () {
        $scope.itemArry = [];
        angular.forEach($scope.clist, function (item) {
            if (!!item.selected) $scope.itemArry.push(item.clientID);
        });



        if ($scope.itemArry.length == 0) {
            alert("Please select client for send Promo Mail");
            return;
        }
        else if ($scope.txtSubject == null) {
            alert("Please enter subject for client mail");
            return;
        }
        else if ($scope.txtSubject.length == 0) {
            alert("Please enter subject for client mail");
            return;
        }

        else if ($scope.message == null) {
            alert("Please enter message to send client ");
            return;
        }
        else if ($scope.message.length == 0) {
            alert("Please enter message to send client");
            return;
        }



        $http({
            url: "/Client/SendPromoMail",
            dataType: 'json',
            method: 'POST',
            params: {
                title: $scope.txtSubject,
                msg: $scope.message,
                clientList: $scope.itemArry
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Message successfully sent to client");
                reset();
            }
            else {
                alert("Error : " + d.data);
            }

        }).error(function (error) {
            alert(error);
        });

    }

    $scope.ShowComposeWindow = function () {
        $scope.composeWindow = false;
        $scope.txtSubject = "";
        $scope.message = "";

    }

    function reset() {
        $scope.composeWindow = true;
        $scope.txtSubject = "";
        $scope.message = "";
    }

    $scope.CloseCompose = function () {
        reset();

    }
});