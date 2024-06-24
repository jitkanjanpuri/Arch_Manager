var app = angular.module("myApp", [])
app.controller('myCtr', function ($scope, $http, $window) {


    $scope.btnHide = false;
    $scope.loading = true;
    $scope.loadingMail = true;
    var varClientID = 0;

    $scope.Search = function () {
        SearchClient();
    }

    function SearchClient() {
        var opt = $scope.opt;
        var varname = $scope.txtName;
        var varpname = $scope.txtProjectName;
        var varpid = $scope.txtProjectID;

        $scope.errRecord = "";
        $scope.errMessage = "";
        $scope.receiveList = null;
        if (opt == undefined) {
            $scope.errRecord = "Please select at least one option";
            return;
        }
        else if ((opt == "name") && ((varname == null) || (varname == undefined) || (varname == ""))) {
            $scope.errRecord = "Please enter client name";
            return;
        }
        else if ((opt == "projectID") && ((varpid == null) || (varpid == undefined) || (varpid == ""))) {
            $scope.errRecord = "Please enter project ID";
            return;
        }
        else if ((opt == "projectName") && ((varpname == null) || (varpname == undefined) || (varpname == ""))) {
            $scope.errRecord = "Please enter project name";
            return;
        }

        $scope.loading = false;
        $http({
            url: "/Admin/SearchByNameOrProjectID",
            dataType: 'json',
            method: 'POST',
            params: {
                opt: opt,
                pid: varpid,
                cname: varname,
                pname: varpname

            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.receiveList = d.data;
            $scope.loading = true;

            if ($scope.receiveList.length == 0) {
                $scope.errMessage = "Record not found";
            }

        }).error(function (err) {
            alert("Error : " + err);
        });
    }



    $scope.ShowAmountReceive = function (item) {

        varClientID = item.clientID;
        $scope.txtPID = item.projectID;
        $scope.txtCName = item.clientName;
        $scope.txtAmount = "";
        $scope.txtRemark = "";
    }


    $scope.AmountReceiveSave = function () {

        var varcname = $scope.txtCName;
        var varamount = $scope.txtAmount;
        var varremark = $scope.txtRemark;
        $scope.errReceive = "";
        var chkGMail = "false";

        if (varcname == undefined) {
            $scope.errReceive = "Please select client for amount receive ";
            return;
        }
        else if (varamount == undefined) {
            $scope.errReceive = ""; "Please enter amount";
            return;
        }
        else if ((varamount == "") || (varamount == "0")) {
            $scope.errReceive = "Please enter amount";
            return;
        }
        $scope.loadingMail = false;
        $scope.btnHide = true;
        chkGMail = document.getElementById("chkGMail").checked;
        $http({
            url: "/Admin/SaveAmountReceive",
            dataType: 'json',
            method: 'POST',
            params: {
                clientID: varClientID,
                projectID: $scope.txtPID,
                amount: varamount,
                remark: varremark,
                gmail: chkGMail
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
          //  $scope.loadingMail = true;
            $scope.btnHide = false; 
            var arr = d.data.split('_');
            if (arr[1] == "Success") {
                alert("Amount successfully received");
                RptClientReceivePrint(arr[0]);
            }
            else {
                $scope.errReceive = d.data;
            }

        }).error(function (err) {
            alert("Error : " + err);
        });

    }

    function RptClientReceivePrint(arID) {
         
        $http({
            url: "/Admin/RptClientReceivePrint",
            dataType: 'json',
            method: 'POST',
            params: {
                recID: arID
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            var arr = location.href.split('/');
            var url = "http://" + arr[2] + "/pdf_files/" + d.data.replace('"', '');
            url = url.replace('"', '');
            $window.open(url, '_blank');

            location.reload();
        }).error(function (err) {
            alert(" Error : " + err);
        });

    }





});
