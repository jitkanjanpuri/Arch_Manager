var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http) {

    $scope.adustBalanceWindow = true;
    $scope.clientledgerwindow = true;
    $scope.designerledgerwindow = true;
    $scope.pdfPreviewWindow = true;
    $scope.clientbalance = "";
    $scope.designerbalance = "";
    $scope.totalclientbalance = "0";
    $scope.totaloldbalance = "0";
    $scope.totaladjustbalance = "";
    $scope.pdfQuotation = "";
    $scope.divPDF = "";
    $scope.totalclientreceive = 0;
    $scope.quotationAmount = 0;

    var d = new Date();
    var cur_date = d.getDate();
    var cur_month = d.getMonth() + 1;
    var cur_yr = d.getFullYear();


    $scope.arrProjectType = ["All", "Exterior", "Interior", "3D_Floor", "Structure", "Planning", "Gujarat", "Walkthrough", "Bird_Eye_View", "Interactive_View(Exterior)", "Interactive_View(Interior)"];

    $scope.ptype = $scope.arrProjectType[0];

    $scope.toEnd = cur_month + "-" + cur_date + "-" + cur_yr;
    $scope.fromStat = cur_month + "-" + cur_date + "-" + cur_yr;


    $scope.ShowClientLeager = function () {
        var vardt1 = $scope.fromStat;
        var vardt2 = $scope.toEnd;
        var varcname = $scope.txtCName;
        var balance = 0;
        var clist;

        $http({
            url: "/Admin/RptClientLedger",
            dataType: 'json',
            method: 'POST',
            params: {
                cname: varcname
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.clientLedger = d.data;
            if ($scope.clientLedger.length == 0) {
                alert("Record is not available");
                return;
            }
            for (i = 0; i < $scope.clientLedger.length; i++) {
                clist = $scope.clientLedger[i];
                balance += parseInt(clist.mobile);

            }
            $scope.totalclientbalance = balance;

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }

    $scope.BalanceAdjust = function () {

        if ($scope.txtBalance == undefined) {
            alert("Remaining balance is not available")
            return;
        }
        else if ($scope.txtBalance.length == 0) {
            alert("Remaining balance is not available")
            return;
        }
        $scope.clientledgerwindow = true;
        $scope.adustBalanceWindow = false;
    }

    $scope.CloseAdustBalanceWindow = function () {
        $scope.adustBalanceWindow = true;
    }

    $scope.SaveAdjustBalance = function () {

        if ($scope.txtAmount == undefined) {
            alert("Please enter adjust amount");
            return;
        }
        else if (($scope.txtAmount.length == 0) || ($scope.txtAmount == "0")) {
            alert("Please enter adjust amount");
            return;
        }

        $http({
            url: "/Admin/SaveBalanceAdjust",
            method: "POST",
            dataType: "json",
            params: {
                clientID: $scope.txtClientID,
                balance: $scope.txtBalance,
                amount: $scope.txtAmount,
                remark: $scope.txtRemark
            },
            contentType: "application/json;charaset = utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Data successfully saved");
                $scope.adustBalanceWindow = true;
                return;
            }
            else {
                alert(" Error " + d.data);
            }

        }).error(function (err) {
            alert(" Error : " + err);
        });

    }



    $scope.closeclientledgerwindow = function () {
        $scope.clientledgerwindow = true;
    }
    $scope.closeDesignLedgerwindow = function () {
        $scope.designerledgerwindow = true;
    }

    $scope.ShowLedgerDetail = function (cid, clientName, city, state) {
        var vardt1 = $scope.fromStat;
        var vardt2 = $scope.toEnd;
        $scope.txtBalance = "";
        

        var slist;
        $scope.txtClientID = cid;
        $scope.txtClientName = clientName;

        $scope.clientledgerwindow = false;
        $scope.clientbalance = "";
        $scope.clientname = "Client name : " + clientName;
        $scope.clientaddress = "Address : " + city + ", " + state;
        $scope.clientID = cid;
        $scope.previousbalance = "0";


        
        $http({
            url: "/Admin/PreviousAmount",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: cid,
                fromDt: vardt1 
                
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.previousbalance = d.data;

            getLedger(cid, vardt1, vardt2)
             
        }).error(function (err) {
            alert(" Error : " + err);
        });
        
        
       
    }


    function getLedger(cid, vardt1, vardt2) {

        $http({
            url: "/Admin/RptClientLedgerDetail",
            dataType: 'json',
            method: 'POST',
            params: {
                clientID: cid,
                fromDt: vardt1,
                toDt: vardt2
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.ledgerDeatil = d.data;


            if ($scope.ledgerDeatil.length == 0) {
                alert("Record is not available");
                return;
            }
            for (var i = 0; i < $scope.ledgerDeatil.length; i++) {
                slist = $scope.ledgerDeatil[i];
                $scope.clientbalance = slist.balance;
                $scope.txtBalance = $scope.clientbalance;
            }

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }


    $scope.ShowClientReceive = function () {
        var vardt1 = $scope.fromStat;
        var vardt2 = $scope.toEnd;
        var varcname = $scope.txtClientReceive;

        $scope.clientReceiveList = null;
        $scope.totalclientreceive = "0";
        var slist;
        $http({
            url: "/Admin/RptClientReceive",
            dataType: 'json',
            method: 'POST',
            params: {
                cname: varcname,
                fromDt: vardt1,
                toDt: vardt2
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.clientReceiveList = d.data;

            if ($scope.clientReceiveList.length == 0) {
                alert("Record is not available");
                return;
            }


            var len = $scope.clientReceiveList.length - 1;
            slist = $scope.clientReceiveList[len];
            $scope.totalclientreceive = slist.balance;
        }).error(function (err) {
            alert(" Error : " + err);
        });

    }

    $scope.ClientReportExport = function () {
        $("#tblClientLedger").table2excel(
            {
                filename: "ClientLedger.xls"
            })

    }



    $scope.ClientPDFReport = function () {

        $http({
            url: "/Admin/ClientLedgerPDF",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: $scope.clientID,
                cname: $scope.clientname,
                addr: $scope.clientaddress,
                fromDt: $scope.fromStat,
                toDt: $scope.toEnd
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {

            var ch = "../PDF_Files/ClientlLedger/" + d.data;
            $scope.pdfFilePath = ch;
            $scope.clientledgerwindow = true;
            $scope.pdfPreviewWindow = false;
        }).error(function (err) {
            alert("Error " + err);
        });
    }

    $scope.PdfOpen = function (varpid, varPType) {
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
            $scope.pdfPreviewWindow = false;


            var ch = d.data;//.Replace(".", "_");
            ch = ch.replace(".", "_");

            $scope.pdfQuotation = ch;
            $scope.tblQuotation = ch + "tbl";
            $scope.divPDF = "Div" + ch;

            ch = "../PDF_Files/" + d.data;
            $scope.clientledgerwindow = true;
            $scope.pdfFilePath = ch;


        }).error(function (err) {
            alert("Error " + err);
        });

    }

    $scope.closePdfPreviewWindow = function () {
        $scope.pdfPreviewWindow = true;

    }

    $scope.QuotationSend = function (varpid, varPType) {
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
            if (d.data == "success") {
                alert("Quotation successfully sent");
            }
        }).error(function (err) {
            alert("Error " + err);
        });

    }


    $scope.ShowDesignerLedger = function () {

        $http({
            url: "/Admin/RptDesignerLedger",
            dataType: 'json',
            method: 'POST',
            params:
            {
                dname: $scope.txtDName
            },

            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.designeramountlist = d.data;
            if ($scope.designeramountlist.length == 0) {
                alert("Record is not available");
                return;
            }

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }


    $scope.SearchQuotation = function () {
        var varPType = $scope.ptype;
        var vardt1 = $scope.fromStat;
        var vardt2 = $scope.toEnd;
        var ch = $scope.quotationValue;
        var varProjectID = $scope.txtProjectID;
        var varCName = $scope.txtName;


        $scope.quotationAmount = 0;

        if (ch == "projectID") {
            if ((varProjectID == undefined) || (varProjectID == "")) {
                alert("Please enter project ID");
                return;
            }
        }
        else if (ch == "clientName") {
            if ((varCName == undefined) || (varCName == "")) {
                alert("Please enter Client name");
                return;
            }
        }


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

            if ($scope.quotationlist.length == 0) {
                alert(" Record not available");
                return;
            }
            var len = $scope.quotationlist.length - 1;
            slist = $scope.quotationlist[len];

            $scope.quotationAmount = slist.balance;

        }).error(function (err) {
            alert(err);
        });
    }


    $scope.ShowBalanceAdjust = function () {
        var vardt1 = $scope.fromStat;
        var vardt2 = $scope.toEnd;
        var varCName = $scope.txtCNameBalanceAdjust;
        $scope.totaloldbalance = "0";
        $scope.totaladjustbalance = "0";
        $scope.adjustbalancelist = null;
        var oldbal = 0, adjustbal = 0;
        $http({
            url: "/Admin/ShowBalanceAdjust",
            method: "GET",
            dataType: "json",
            params: {

                dt1: vardt1,
                dt2: vardt2,
                cname: varCName
            },
            contentType: "application/json; charaset= utf-8"
        }).then(function (d) {
            $scope.adjustbalancelist = d.data;
            if ($scope.adjustbalancelist.length == 0) {
                alert("Record is not available");
                return;
            }
            for (var i = 0; i < $scope.adjustbalancelist.length; i++) {
                slist = $scope.adjustbalancelist[i];
                oldbal = oldbal + parseInt(slist.oldBalance);
                adjustbal = adjustbal + parseInt(slist.adjustBalance);

            }
            $scope.totaloldbalance = oldbal;
            $scope.totaladjustbalance = adjustbal;
        }).error(function (err) {
            alert("Error : " + err);
        });

    }

    $scope.ShowDesignerLedgerDetail = function (varsid, name, city) {
        var vardt1 = $scope.fromStat
        var vardt2 = $scope.toEnd;

        $scope.designername = "Name :  " + name;
        $scope.designeraddress = "Address : " + city;


        $scope.designerbalance = "";
        $scope.designerledgerwindow = false;

        $http({
            url: "/Admin/RptDesignerLedgerDetail",
            method: "POST",
            dataType: "json",
            params:
            {
                sid: varsid,
                dt1: vardt1,
                dt2: vardt2
            },
            contentType: "application/json; charaset =utf-8"
        }).then(function (d) {
            $scope.designerLedgerDeatil = d.data;
            if ($scope.designerLedgerDeatil.length == 0) {
                alert("Record is not available");
                return;
            }


            for (var i = 0; i < $scope.designerLedgerDeatil.length; i++) {
                slist = $scope.designerLedgerDeatil[i];
                $scope.designerbalance = slist.balance;
            }
        });
    }

    $scope.DesignerReportExport = function () {
        $("#tblDesignerLedger").table2excel({
            filename: "DesignerLedger.xls"
        })
    }
});

app.directive("datepicker", function () {

    function link(scope, element, attrs) {
        element.datepicker({
            dateFormat: "mm-dd-yy"
        });
    }
    return {
        require: 'ngModel',
        link: link
    };
});

