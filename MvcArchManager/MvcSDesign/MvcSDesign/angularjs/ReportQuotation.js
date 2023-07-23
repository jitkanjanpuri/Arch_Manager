﻿var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http,  $window) {
     
    $scope.loading = true;
    $scope.arrProjectType = ["All", "Exterior", "Interior", "3D_Floor", "Structure", "Planning", "Gujarat", "Walkthrough", "Bird_Eye_View", "Interactive_View(Exterior)", "Interactive_View(Interior)"];

    $scope.ptype = $scope.arrProjectType[0];
    $scope.totalAmount = "0";

   
    $scope.toEnd = new Date();
    $scope.fromStart = new Date();
     
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
 

    $scope.QuotationSend = function (varpid, varPType) {
        $scope.loading = false;
        $http({
            url: "/Client/SendClientQuotation",
            dataType: 'json',
            method: 'POST',
            params: {
                pid: varpid,
                projectType: varPType
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
            $scope.loading = true;

            if (d.data == "success") {
                alert("Quotation successfully sent");
            }
            else {

                alert(" Error " + d.data);
            }
        }).error(function (err) {
            alert("Error " + err);
        });

    }


     


    $scope.SearchQuotation = function () {
        var varPType = $scope.ptype;
        var vardt1 = $scope.fromStart;
        var vardt2 = $scope.toEnd;
        var ch = $scope.quotationValue;
        var varProjectID = $scope.txtProjectID;
        var varCName = $scope.txtName;

        $scope.recordmsg = "";
        $scope.searchdmsg = "";

        $scope.quotationAmount = 0;
        $scope.totalAmount = "0";
        var total = 0;
        if ((vardt1 == null) || (vardt2 == null)) {
            $scope.searchdmsg = "Please select date for search";
            return;
        }
        else if (ch == null) {
            $scope.searchdmsg = "Please select atleast one option for search";
            return;
        }
        else if (ch == "projectID") {
            if ((varProjectID == undefined) || (varProjectID == "")) {
                $scope.searchdmsg ="Please enter project ID";
                return;
            }
        }
        else if (ch == "clientName") {
            if ((varCName == undefined) || (varCName == "")) {
                $scope.searchdmsg ="Please enter Client name";
                return;
            }
        }

        $scope.loading = false;
        $http({
            url: "/Admin/RptQuotation",
            method: "POST",
            dataType: "json",
            params: {
                ptype: varPType,
                dt1: vardt1,
                dt2: vardt2,
                searchOpt: ch,
                projectID: varProjectID,
                cname: varCName
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
            $scope.quotationlist = d.data;
            $scope.loading = true;
            if ($scope.quotationlist.length == 0) {
                $scope.recordmsg = " Record not available";
                return;
            }
            var len = $scope.quotationlist.length - 1;
            slist = $scope.quotationlist[len];

            $scope.quotationAmount = slist.balance;
            var arr = [];
            for (var i = 0; i < $scope.quotationlist.length; i++) {
                arr = $scope.quotationlist[i];

                total = total + arr["amount"];
            }
            $scope.totalAmount = total;


        }).error(function (err) {
            alert(err);
        });
    }


     

     
});

 

