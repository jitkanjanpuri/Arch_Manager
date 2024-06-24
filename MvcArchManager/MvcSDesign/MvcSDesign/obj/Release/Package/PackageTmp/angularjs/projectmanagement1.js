var app = angular.module("myApp", [])

app.controller('myController', function ($scope, $http, projectlist, $window) {
    var clientID = 0;
    var quotationID = 0

    $scope.loading = true;
    $scope.loadingMail = false;

    projectlist.getRecord().then(function (d) {
        $scope.projectquotationlist = d.data;

    });


    $scope.Search = function () {
        SearchQuoation();
    }


    function SearchQuoation() {
        var opt = $scope.opt;
        var varname = $scope.txtName;
        var varpname = $scope.txtProjectName;
        var varpid = $scope.txtQuotationNo;

        $scope.errRecord = "";
        $scope.errMessage = "";
        $scope.receiveList = null;

        $scope.projectquotationlist = null;
        if (opt == undefined) {
            $scope.errRecord = "Please select at least one option";
            return;
        }
        else if ((opt == "name") && ((varname == null) || (varname == undefined) || (varname == ""))) {
            $scope.errRecord = "Please enter client name";
            return;
        }
        else if ((opt == "quotationNo") && ((varpid == null) || (varpid == undefined) || (varpid == ""))) {
            $scope.errRecord = "Please enter Quotation no";
            return;
        }
        else if ((opt == "projectName") && ((varpname == null) || (varpname == undefined) || (varpname == ""))) {
            $scope.errRecord = "Please enter project name";
            return;
        }

        $scope.loading = false;
        $http({
            url: "/Admin/getProjectQuotation",
            dataType: 'json',
            method: 'POST',
            params: {
                opt: opt,
                quotationNo: varpid,
                cname: varname,
                pname: varpname

            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.projectquotationlist = d.data;
            $scope.loading = true;

            if ($scope.projectquotationlist.length == 0) {
                $scope.errRecord = "Record not found";
            }

        }).error(function (err) {
            alert("Error : " + err);
        });
    }



    $scope.ShowPanel = function (item) {
        clientID = item.clientID;
        $scope.QuotationNo = item.quotationNo;
        $scope.clientName = item.clientName;
        $scope.amount = item.amount;
        $scope.finalAmount = 0;

        ShowAlert();

    }


    function ShowAlert() {
         
        $("#successalert").alert();
         
        $window.setTimeout(function () {
            $("#success-alert").alert('close');
        }, 100);
    }

    $scope.SaveQuotation = function () {

        var varqno = $scope.QuotationNo;
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
                clientID: clientID,
                qno: varqno,
                finalAmount: varfamount
            },
            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {
            $scope.loading = true;
            $scope.loadingMail = false;
            if (d.data == "success") {
                alert("Project successfully Saved")
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

    $scope.ShowDeleteModel = function (id, no) {
        quotationID = id;
        $scope.quoNo = no;
    }


    $scope.QuotationDelete = function () {
        $.ajax({
            url: "/Admin/QuotationDelete",
            type: "GET",
            data: { quoID: quotationID },
            success: function (d) {
                if (d == "") {
                    alert("Quotation successfully deleted");
                    location.reload();
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

    $scope.PdfOpen = function (varpid) {

        $scope.loading = false;
        $http({
            url: "/Client/ShowQuotationPdf",
            dataType: 'json',
            method: 'POST',
            params: {
                quoID: varpid

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