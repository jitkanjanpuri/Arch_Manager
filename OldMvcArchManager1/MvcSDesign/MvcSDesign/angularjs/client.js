var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http) {


    $scope.loading = true;
    var varClientID = 0;


    $scope.Search = function () {
        SearchClient();
    }

    function SearchClient() {
        var varname = $scope.txtName;
        var varchkname = $scope.chkCName;
        var varchkcity = $scope.chkCity
        var varcityname = $scope.txtSearchCity;


        $scope.errMessage = "";

        if (((varchkname == undefined) || (varchkname == false)) && ((varchkcity == undefined) || (varchkcity == false))) {
            alert("Please select atleast one option for search");
            return;
        }
        $scope.loading = false;
        $http({
            url: "/Client/SearchClient",
            dataType: 'json',
            method: 'POST',
            params: {
                chkName: varchkname,
                name: varname,
                chkcity: varchkcity,
                cityname: varcityname

            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.clist = d.data;
            $scope.loading = true;
        
            if ($scope.clist.length == 0) {
                $scope.errMessage = "Record not found";
            }

        }).error(function (err) {
            alert("Error : " + err);
        });
    }


    $scope.AllClient = function () {
        $http({
            url: "/Client/AllClient",
            dataType: 'json',
            method: 'POST',
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.allclient = d.data;
            if ($scope.allclient.length == 0) {
                alert("Record not available");
                return;
            }
        }).error(function (err) {
            alert("Error : " + err);
        });
    }

    //Amount Receive from client
    $scope.ShowAmountReceive = function (cid, cname) {
        $scope.errReceive = "";
        varClientID = cid;
        $scope.txtCName = cname;
        $scope.txtAmount = "";
        $scope.txtRemark = "";
    }


    $scope.AmountReceiveSave = function () {

        var varcname = $scope.txtCName;
        var varamount = $scope.txtAmount;
        var varremark = $scope.txtRemark;
        $scope.errReceive = "";
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


        $http({
            url: "/Admin/AmountReceive",
            dataType: 'json',
            method: 'POST',
            params: {
                clientID: varClientID,
                amount: varamount,
                remark: varremark

            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data == "Success") {
                alert("Client amount successfully received");
                location.reload();
                return;
            }

        }).error(function (err) {
            alert("Error : " + err);
        });

    }

    $scope.GetValue = function (cid, clientName, orgName, address, city, state, phone, mobile, emailID) {

        $scope.clientID = cid;
        $scope.txtClientName = clientName;
        $scope.txtOrgName = orgName;
        $scope.txtAddress = address;
        $scope.txtCity = city;
        $scope.statename = state,
        $scope.txtPhone = phone;
        $scope.txtMobile = mobile;
        $scope.txtEmailID = emailID;
        $scope.assignwindow = false;

    }

    $scope.CloseAssignWindow = function () {
        Reset();

    }




    $scope.UpdateClient = function () {

        var varcid = $scope.clientID;
        var varname = $scope.txtClientName;
        var varorgName = $scope.txtOrgName;
        var varaddress = $scope.txtAddress;
        var varcity = $scope.txtCity;

        var varphone = $scope.txtPhone;
        var varmobile = $scope.txtMobile;
        var varemailID = $scope.txtEmailID;

        if ((varname == "") || (varname == undefined)) {
            alert("Please enter client name");
            return false;
        }
        else if ((varcity == "") || (varcity == undefined)) {
            alert("Please enter city ");
            return false;
        }
        else if ((varmobile == "") || (varmobile == undefined)) {
            alert("Please enter mobile no");
            return false;
        }
        else if ((varemailID == "") || (varemailID == undefined)) {
            alert("Please enter email ID");
            return false;
        }


        $http({
            url: "/Client/UpdateClient",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: varcid,
                name: varname,
                orgName: varorgName,
                address: varaddress,
                city: varcity,
                state: $scope.statename,
                phone: varphone,
                mobile: varmobile,
                emailID: varemailID
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Data successfully updated");
                Reset();
                SearchClient();
            }
            else {
                alert(d.data);
            }

        }).error(function (err) {
            alert("Error : " + err);
        });
    }


    $scope.ShowDelete = function (clientID, cname) {

        $scope.clientname = cname;
        varClientID = clientID;

    }



    $scope.Delete = function () {

        $http({
            url: "/Client/DeleteClient",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: varClientID
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Client successfully deleted");

                location.reload();
            }
            else {
                alert("Error " + d.data);
            }

        }).error(function (err) {
            alert("Error : " + err);
        });

    }

});
