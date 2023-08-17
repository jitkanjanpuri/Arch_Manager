var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, $window) {

    $scope.loading = true;

    $scope.Search = function () {
        //var varProjectID = $scope.pid;
        //$scope.searchdmsg = "";
        //$scope.recordmsg = "";


        var opt = $scope.opt;
        var pid = $scope.varProjectID;
        var name = $scope.varName;
        var pname = $scope.varProjectName;
        $scope.recordmsg = "";
        $scope.searchdmsg = "";

        if (opt == undefined) {
            $scope.searchdmsg = "Please select at least one option";
            return;
        }
        else if ((opt == "projectID") && ((pid == null) || (pid == undefined) || (pid == ""))) {
            $scope.searchdmsg = "Please enter project ID";
            return;
        }
        else if ((opt == "name") && ((name == null) || (name == undefined) || (name == ""))) {
            $scope.searchdmsg = "Please enter client name";
            return;
        }
        else if ((opt == "projectName") && ((pname == null) || (pname == undefined) || (pname == ""))) {
            $scope.searchdmsg = "Please enter project name";
            return;
        }


        $scope.loading = false;
        $http({
            url: "/Admin/RptProjectHistory",
            method: "POST",
            dataType: "json",
            params: {
                opt: opt,
                projectID: pid,
                cname: name,
                pname: pname
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



