var app = angular.module("myApp", [])
app.controller('myCtr', function ($scope, $http) {

    var quotationID = 0;
    $scope.loading = true;
    $scope.loading1 = true;
    $scope.err = "";

    $scope.Search = function () {

        var opt = $scope.opt;
        var quotatioNo = $scope.varQuotationNo;
        var name = $scope.varName;
        var pname = $scope.varProjectName;

        $scope.record = "";

        if (opt == undefined) {
            $scope.record = "Please select at least one option";
            return;
        }
        else if ((opt == "quotationNo") && ((quotatioNo == null) || (quotatioNo == undefined) || (quotatioNo == ""))) {
            $scope.record = "Please enter Quotation No";
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
            url: '/Admin/SearchSiteVisitByNameOrQuotatioNo',
            dataType: 'json',
            method: 'POST',
            params: {
                opt: opt,
                qNo: quotatioNo,
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
        document.getElementById("txtName").value = item.clientName;
        document.getElementById("txtProjectName").value = item.projectName;
        document.getElementById("txtService").value = item.service;
        quotationID = item.quotationNo;

        $http({
            url: "/Client/GetRevisedQuotation",
            method: "POST",
            dataType: "json",
            params: {
                qID: item.quotationNo
            },
            contentType: "application/json; charaset =utf-8"
        }).then(function (d) {
            $scope.revlist = d.data;
            GetTotal();
        }).error(function (err) {

            alert("Error " + err);
        });


    }

    $scope.EditItem = function (item) {
        $scope.projectDetails = item.projectDetails;
        $scope.services = item.services;
        $scope.hsn = item.hsn;
        $scope.qty = item.qty;
        $scope.rate = item.rate;
        document.getElementById("unit").value = item.unit;
    }




    function GetTotal() {

        var total = 0;
        var arr1;
        if ($scope.revlist != undefined) {
            for (var i = 0; i < $scope.revlist.length; i++) {
                arr1 = $scope.revlist[i];

                total = total + arr1["amount"];
            }

            $scope.total = total;
        }
    }


    $scope.AddItem = function () {

        var projectDetail = $scope.projectDetails;
        var services = $scope.services;
        var hsn = $scope.hsn;
        var qty = $scope.qty;
        var rate = $scope.rate;

        if ((projectDetail == undefined) || (projectDetail == null)) {
            document.getElementById("projectDetails").focus();
            return;
        }
        else if ((services == undefined) || (services == null)) {
            document.getElementById("services").focus();
            return;
        }
        else if ((hsn == undefined) || (hsn == null)) {
            document.getElementById("hsn").focus();
            return;
        }

        else if ((qty == undefined) || (qty == null)) {
            document.getElementById("area").focus();
            return;
        }
        else if ((rate == undefined) || (rate == null)) {
            document.getElementById("rate").focus();
            return;
        }

        var selectUnit = document.getElementById("unit");
        var optValue = selectUnit.options[selectUnit.selectedIndex].value;

        $http({
            url: "/Client/addProjectItem",
            method: "POST",
            dataType: "json",
            params: {
                projectDetail: projectDetail,
                services: services,
                hsn: hsn,
                qty: qty,
                unit: optValue,
                rate: rate,

            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.revlist = d.data;

            $scope.projectDetails = "";
            $scope.services = "";
            $scope.hsn = "";
            $scope.rate = "";
            $scope.qty = "";

            GetTotal();



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
            GetTotal();
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
                quotationID: quotationID,
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
