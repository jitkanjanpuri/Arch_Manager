var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, $window) {

    $scope.loading = true;

    $scope.SearchQuotation = function () {
        var varProjectID = $scope.pid;
        $scope.searchdmsg = "";
        $scope.recordmsg = "";


        if ((varProjectID == undefined) || (varProjectID == "")) {
            $scope.searchdmsg = "Enter project ID";
            return;
        }

        $scope.loading = false;
        $http({
            url: "/Admin/RptProjectHistory",
            method: "POST",
            dataType: "json",
            params: {
                projectID: varProjectID
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
            $scope.uploadlist = d.data;
            $scope.loading = true;
            if ($scope.uploadlist.length == 0) {
                $scope.recordmsg = " Record not available";
                return;
            }

        }).error(function (err) {
            alert(err);
        });
    }


});



