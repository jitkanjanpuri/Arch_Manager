var app = angular.module("myApp", [])
app.controller('myCtr', function ($scope, $http) {


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
            url: "/Admin/SearchSiteVisitByNameOrProjectID",
            dataType: 'json',
            method: 'POST',
            params: {
                opt: opt,
                projectID: varpid,
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


    //$scope.AllClient = function () {
    //    $http({
    //        url: "/Client/AllClient",
    //        dataType: 'json',
    //        method: 'POST',
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        $scope.allclient = d.data;
    //        if ($scope.allclient.length == 0) {
    //            alert("Record not available");
    //            return;
    //        }
    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });
    //}


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
        chkGMail = document.getElementById("chkGMail").checked;
        $http({
            url: "/Admin/AmountReceive",
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
            $scope.loadingMail = false;
            if (d.data == "Success") {
                alert("Amount successfully received");
                location.reload();
                return;
            }
            else {
                $scope.errReceive = d.data;
            }

        }).error(function (err) {
            alert("Error : " + err);
        });

    }

    //$scope.GetValue = function (cid, clientName, orgName, address, city, state, phone, mobile, emailID) {

    //    $scope.clientID = cid;
    //    $scope.txtClientName = clientName;
    //    $scope.txtOrgName = orgName;
    //    $scope.txtAddress = address;
    //    $scope.txtCity = city;
    //    $scope.statename = state,
    //        $scope.txtPhone = phone;
    //    $scope.txtMobile = mobile;
    //    $scope.txtEmailID = emailID;
    //    $scope.assignwindow = false;

    //}






    //$scope.UpdateClient = function () {

    //    var varcid = $scope.clientID;
    //    var varname = $scope.txtClientName;
    //    var varorgName = $scope.txtOrgName;
    //    var varaddress = $scope.txtAddress;
    //    var varcity = $scope.txtCity;

    //    var varphone = $scope.txtPhone;
    //    var varmobile = $scope.txtMobile;
    //    var varemailID = $scope.txtEmailID;

    //    if ((varname == "") || (varname == undefined)) {
    //        alert("Please enter client name");
    //        return false;
    //    }
    //    else if ((varcity == "") || (varcity == undefined)) {
    //        alert("Please enter city ");
    //        return false;
    //    }
    //    else if ((varmobile == "") || (varmobile == undefined)) {
    //        alert("Please enter mobile no");
    //        return false;
    //    }
    //    else if ((varemailID == "") || (varemailID == undefined)) {
    //        alert("Please enter email ID");
    //        return false;
    //    }


    //    $http({
    //        url: "/Client/UpdateClient",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            cid: varcid,
    //            name: varname,
    //            orgName: varorgName,
    //            address: varaddress,
    //            city: varcity,
    //            state: $scope.statename,
    //            phone: varphone,
    //            mobile: varmobile,
    //            emailID: varemailID
    //        },
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "") {
    //            alert("Data successfully updated");
    //            Reset();
    //            SearchClient();
    //        }
    //        else {
    //            alert(d.data);
    //        }

    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });
    //}


    //$scope.ShowDelete = function (clientID, cname) {

    //    $scope.clientname = cname;
    //    varClientID = clientID;

    //}



    //$scope.Delete = function () {

    //    $http({
    //        url: "/Client/DeleteClient",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            cid: varClientID
    //        },
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "") {
    //            alert("Client successfully deleted");

    //            location.reload();
    //        }
    //        else {
    //            alert("Error " + d.data);
    //        }

    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });

    //}

});
