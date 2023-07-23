var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http, projectlist) {

    $scope.finalizeWindow = true;
    $scope.pdfPreviewWindow = true;
    projectlist.getRecord().then(function (d) {
        $scope.projectquotationlist = d.data;
    },
      function () {
          alert('Failed'); // Failed
      });

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

    $scope.ShowPanel = function (pid, cname, amount) {
        $scope.finalizeWindow = false;
        $scope.projectID = pid;
        $scope.clientName = cname;
        $scope.amount = amount;
        $scope.finalAmount = 0;
    }
    $scope.CloseAssignWindow = function () {
        ResetValue();
    }

    $scope.closePdfPreviewWindow = function () {
        $scope.pdfPreviewWindow = true;
    }

    function ResetValue() {
        $scope.finalizeWindow = true;
        $scope.projectID = 0;
        $scope.clientName = "";
        $scope.amount = 0;
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

        $http({
            url: "/Admin/SaveProjectManagement",
            method: 'POST',
            dataType: 'json',
            params: {
                projectID: varpid,
                finalAmount: varfamount
            },
            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {

            if (d.data == "success") {
                alert(" Quotation's finalize amount successfully saved");
                ResetValue();

                ShowQuotation();

                return;
            }
            else {
                alert(" Error " + d.data);
            }


        }).error(function (err) {
            alert("Error " + err);
        });
    }

    $scope.QuotationDelete = function (pid) {
        var con = confirm("Do you want to delete project " + pid + " ?");
        if (con) {
            $.ajax({
                url: "/Admin/QuotationDelete",
                type: "GET",
                data: { projectID: pid },
                success: function (d) {
                    if (d == "") {
                        alert("Project successfully deleted");
                        ShowQuotation();
                    }
                    else
                        alert("Error :" + d);

                    return false;
                },
                error: function () {
                    alert("Error ");
                }
            });
        }
    }

    $scope.PdfOpen = function (varpid) {

        $http({
            url: "/Client/ShowQuotationPdf",
            dataType: 'json',
            method: 'POST',
            params: {
                pid: varpid
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.pdfPreviewWindow = false;
            var ch = "../PDF_Files/" + d.data;
            $scope.pdfFilePath = ch;
        }).error(function (err) {
            alert("Error " + err);
        });

    }

    $scope.QuotationSend = function (varpid) {
        $http({
            url: "/Client/SendClientQuotation",
            dataType: 'json',
            method: 'POST',
            params: {
                pid: varpid
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
          
            if (d.data == "success")
            {
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