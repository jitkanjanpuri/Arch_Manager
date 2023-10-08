var app = angular.module("myApp", [])
app.controller('myCtr', function ($scope, $http) {

    var projectID = 0;
    $scope.loading = true;
    $scope.loading1 = true;
    $scope.err = "";

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
        else if ((opt == "projectName") && ((pname == null) || (pname == undefined) || (pname == ""))) {
            $scope.record = "Please enter project name";
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
            $scope.qlist = d.data;
            $scope.loading = true;
            if (d.data.length == 0) {
                $scope.record = " Record not available";
                return false;
            }
        }).error(function (err) {
            alert(" Error : " + err);
        });
    }
    $scope.SetClientID = function (item) {
        document.getElementById("txtCID").value = item.clientID;
        document.getElementById("txtName").value = item.clientName;
        document.getElementById("txtProjectName").value = item.projectName;
        document.getElementById("txtService").value = item.service;
        projectID = item.projectID;
        $http({
            url: "/Client/GetRevisedQuotation",
            method: "POST",
            dataType: "json",
            params: {
                projectID: item.projectID
            },
            contentType: "application/json; charaset =utf-8"
        }).then(function (d) {
            $scope.revlist = d.data;
        }).error(function (err) {

            alert("Error " + err);
        });


    }

    $scope.addProductCart = function () {

        var projectDetail = $scope.projectDetails;
        var services = $scope.services;
        var area = $scope.area;
        var rate = $scope.rate;
        var amount = $scope.amount;
      
        if ((projectDetail == undefined) || (projectDetail ==null)) {
            document.getElementById("projectDetails").focus();
            return;
        }
        else if ((services == undefined) || (services == null)) {
            document.getElementById("services").focus();
            return;
        }
        else if ((area == undefined) || (area == null)) {
            document.getElementById("area").focus();
            return;
        }
        else if ((rate == undefined) || (rate == null)) {
            document.getElementById("rate").focus();
            return;
        }
        else if ((amount == undefined) || (amount == null)) {
            document.getElementById("amount").focus();
            return;
        }
        
        $http({
            url: "/Client/addProjectItem",
            method: "POST",
            dataType: "json",
            params: {
                projectDetail: projectDetail,
                services: services,
                area: area,
                rate: rate,
                amount: amount
                 
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.revlist = d.data;
            $scope.projectDetails="";
            $scope.services="";
            $scope.area ="";
            $scope.rate ="";
            $scope.amount="";
        }).error(function (err) {
            alert("Error " + err);
        });

    }
    $scope.removeProjectItem = function (item) {
        $http({
            url: "/Client/removeRevQuotationItem",
            method: "POST",
            dataType: "json",
            params: {
                projectDetail: item.projectDetails,
                service: item.services
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
            $scope.revlist = d.data;

        }).error(function (err) {

        });
    }
    $scope.Update = function () {
        var pname = document.getElementById("txtProjectName").value;
        var service = document.getElementById("txtService").value;

        if ((pname == undefined) || (pname == "")) {
            alert("Please enter project name");
            return;
        }
        else if ((service == undefined) || (service == "")) {
            alert("Please enter service");
            return;
        }
        else if (($scope.revlist == undefined) || ($scope.revlist.length == 0)) {
            alert("Project item list empty");
            return;
        }
        $scope.loading1 = false;
        $scope.btnHide = true;
        $http({
            url: '/Client/UpdateQuotation',
            dataType: 'json',
            method: 'POST',
            params: {
                projectID: projectID,
                projectDetail: pname,
                service: service
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.loading1 = true;
            $scope.btnHide = false;
            if (d.data == "") {
                alert("Quotation successfully updated");
                location.reload();
            }
            else {
                alert("Error " + d.data);
            }
            
        }).error(function (err) {
            alert(" Error : " + err);
        });
    }
});
