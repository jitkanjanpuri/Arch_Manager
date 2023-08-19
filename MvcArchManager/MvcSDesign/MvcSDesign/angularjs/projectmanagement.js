var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http, projectlist, $window) {
    var clientID = 0;
    var projectLevel = "";
    $scope.loading = true;

    $scope.loadingMail = false;

    projectlist.getRecord().then(function (d) {
        $scope.projectquotationlist = d.data;
    },
        //function () {
        //    alert('Failed'); // Failed
        //  }
    );

    function ShowQuotation() {
        $http({
            url: "/Admin/getProjectQuotation",
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {
            $scope.projectquotationlist = d.data;
            if ($scope.projectquotationlist.length == 0) {
                alert("Record not available");
            }
        }).error(function (err) {
            alert("Error " + err);
        });
    }

    $scope.ShowPanel = function (item) {
        clientID = item.clientID;
        projectLevel = item.projectLevel;
        $scope.projectID = item.projectID;
        $scope.clientName = item.clientName;
        $scope.amount = item.amount;
        $scope.finalAmount = 0;
    }


    $scope.UpdateQuotation = function () {

        var varpid = $scope.projectID;
        var varfamount = $scope.finalAmount;
        if (varfamount == undefined) {
            alert("Please enter finalize amount");
            return
        }
        else if (varfamount == "0") {
            alert("Please enter finalize amount");
            return
        }
        $scope.loading = false;
        $scope.loadingMail = true;
        $http({
            url: "/Admin/SaveProjectManagement",
            method: 'POST',
            dataType: 'json',
            params: {
                clientid: clientID,
                level: projectLevel,
                projectID: varpid,
                finalAmount: varfamount
            },
            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {
            $scope.loading = true;
            $scope.loadingMail = false;
            if (d.data == "success") {
                location.reload();
                return;
            }
            else {
                alert(" Error " + d.data);
            }


        }).error(function (err) {
            alert("Error " + err);
        });
    }

    $scope.ShowDeleteModel = function (id) {

        $scope.projectID = id;
    }


    $scope.QuotationDelete = function () {
        $.ajax({
            url: "/Admin/QuotationDelete",
            type: "GET",
            data: { projectID: $scope.projectID },
            success: function (d) {
                if (d == "") {
                    alert("Project successfully deleted");
                    ShowQuotation();
                }
                else {
                    alert("Error :" + d);
                    location.reload();
                }
                return false;
            },
            error: function () {
                alert("Error ");
            }
        });

    }

    $scope.PdfOpen = function (varpid, varprojectType) {
        $scope.loading = false;
        $http({
            url: "/Client/ShowQuotationPdf",
            dataType: 'json',
            method: 'POST',
            params: {
                pid: varpid,
                projectType: varprojectType
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.loading = true;
            var arr = location.href.split('/');
            var url = "http://" + arr[2] + "/PDF_Files/" + d.data.replace('"', '');
            url = url.replace('"', '');
            $window.open(url, '_blank');

        }).error(function (err) {
            alert("Error " + err);
        });

    }

    $scope.QuotationSend = function (varpid, varprojectType) {
        $http({
            url: "/Client/SendClientQuotation",
            dataType: 'json',
            method: 'POST',
            params: {
                pid: varpid,
                projectType: varprojectType
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {

            if (d.data == "success") {
                alert("Quotation successfully sent");
            }
        }).error(function (err) {
            alert("Error " + err);
        });

    }



});


app.factory('projectlist', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getProjectQuotation');
    }

    return fac;
});