var app = angular.module("myApp", [])
app.controller('myCtr', function ($scope, $http) {

    var projectID = 0;
    $scope.loading = true;
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

    function addProuctCart(pid, productName, price, dis, varqty) {
        $http({
            url: "/Home/addProductCard",
            method: "POST",
            dataType: "json",
            params: {
                pid: pid,
                pname: productName,
                price: price,
                discount: dis,
                qty: varqty,
                orderType: 'Regular'
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.cartcount = d.data;
            viewCart();
        }).error(function (err) {
            alert("Error " + err);
        });

    }
    $scope.removeCartItem = function (item) {
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

});
