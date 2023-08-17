var app = angular.module("myApp", [])
app.controller("myController", function ($scope, $http) {

    $scope.loading = true;
   
    $scope.Search = function () {
        
        
        var opt = $scope.opt;
        var pid = $scope.varProjectID;
        var name = $scope.varName;
        var pname = $scope.varProjectName;

        $scope.record = "";
        if (opt == undefined) {
            $scope.record = "Please select at least one option";
            return;
        }
        else if ((opt == "projectID") && ((pid == null) || (pid == undefined) || (pid == ""))) {
            $scope.record = "Please enter project ID";
            return;
        }
        else if ((opt == "name") && ((name == null) || (name == undefined) || (name == ""))) {
            $scope.record = "Please enter client name";
            return;
        }
        $scope.loading = false;

         
        $http({
            url: '/Admin/SearchSiteVisitByNameOrProjectID',
            dataType: 'json',
            method: 'POST',
            params: {
                opt: opt,
                projectID: pid,
                cname: name,
                pname: pname
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.sitevisitlist = d.data;
            $scope.loading = true;
            if (d.data.length ==0) {
                $scope.record = " Record not available";
                return false;
            }
        }).error(function (err) {
            alert(" Error : " + err);
        });
    }
   
    $scope.ShowCurrentWorking = function (item) {
        document.getElementById("projectID").value = item.projectID;
    }


});

 