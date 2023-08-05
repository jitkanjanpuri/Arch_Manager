var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, $window) {

    $scope.loading = true;

     
    $scope.PdfOpen = function (varpid, varPType) {

        $scope.loading = false;
        $http({
            url: "/Client/ShowQuotationPdf",
            dataType: 'json',
            method: 'POST',
            params: {
                pid: varpid,
                projectType: varPType
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {


            $scope.loading = true;
            var arr = location.href.split('/');
            var url = "http://" + arr[2] + "/pdf_files/" + d.data.replace('"', '');
            url = url.replace('"', '');
            $window.open(url, '_blank');


        }).error(function (err) {
            alert("Error " + err);
        });

    }

     




    $scope.SearchQuotation = function () {
        var varProjectID = $scope.pid;
        $scope.searchdmsg = "";
        if ((varProjectID == undefined) || (varProjectID == "")) {
            $scope.searchdmsg = "Please enter project ID";
            return;
        }
         
        $scope.loading = false;
        $http({
            url: "/Admin/RptSiteVisit",
            method: "POST",
            dataType: "json",
            params: {
                projectID: varProjectID
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
            $scope.sitevisitlist = d.data;
            $scope.loading = true;
            if ($scope.sitevisitlist.length == 0) {
                $scope.recordmsg = " Record not available";
                return;
            }
             

             


        }).error(function (err) {
            alert(err);
        });
    }
     

});



